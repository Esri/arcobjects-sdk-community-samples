'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.SchematicControls
Imports ESRI.ArcGIS.Schematic

Public Class MainForm
#Region "class private members"
	Private m_tocControl As ITOCControl2
	Private m_mapControl As IMapControl3 = Nothing

	Private m_mapDocumentName As String = String.Empty
	Private m_menuSchematicLayer As IToolbarMenu
	Private m_menuLayer As IToolbarMenu
	Private m_CreateMenu As IToolbarMenu = New ToolbarMenuClass()
	Private m_blnExistingMap As Boolean = False
	Private m_arrMaps As ESRI.ArcGIS.esriSystem.IArray = New ESRI.ArcGIS.esriSystem.Array()
	Private m_blnToolbarItemClick As Boolean = False
	Private m_blnOpenDoc As Boolean = False
#End Region

#Region "class constructor"
	Public Sub New()
		InitializeComponent()
	End Sub
#End Region

	Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		'get the MapControl and tocControl
		m_tocControl = CType(axTOCControl1.Object, ITOCControl2)
		m_mapControl = CType(axMapControl1.Object, IMapControl3)

		'Set buddy control for tocControl
		m_tocControl.SetBuddyControl(m_mapControl)
		axToolbarControl2.SetBuddyControl(m_mapControl)

		'disable the Save menu (since there is no document yet)
		menuSaveDoc.Enabled = False
		axToolbarControl2.Select()

		'Create a SchematicEditor's MenuDef object
		Dim menuDefSchematicEditor As IMenuDef = New CreateMenuSchematicEditor()

		'Add SchematicEditor on the ToolBarMenu
		m_CreateMenu.AddItem(menuDefSchematicEditor, 0, -1, False, esriCommandStyles.esriCommandStyleIconAndText)

		'Set the ToolbarMenu's hook
		m_CreateMenu.SetHook(axToolbarControl2.Object)

		'Set the ToolbarMenu's caption
		m_CreateMenu.Caption = "SchematicEditor"

		''' Add ToolbarMenu on the ToolBarControl
		axToolbarControl2.AddItem(m_CreateMenu, -1, -1, False, 0, esriCommandStyles.esriCommandStyleMenuBar)

		'''Create a other ToolbarMenu for layer
		m_menuSchematicLayer = New ToolbarMenuClass()
		m_menuLayer = New ToolbarMenuClass()

		'''Add 3 items on the SchematicLayer properties menu 
		m_menuSchematicLayer.AddItem(New RemoveLayer(), -1, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
		m_menuSchematicLayer.AddItem("esriControls.ControlsSchematicSaveEditsCommand", -1, 1, True, esriCommandStyles.esriCommandStyleIconAndText)
		m_menuSchematicLayer.AddItem("esriControls.ControlsSchematicUpdateDiagramCommand", -1, 2, False, esriCommandStyles.esriCommandStyleIconAndText)

		Dim subMenuDef As IMenuDef = New CreateSubMenuSchematic()
		m_menuSchematicLayer.AddSubMenu(subMenuDef, 3, True)
		'''/Add the sub-menu as the third item on the Layer properties menu, making it start a new group
		m_menuSchematicLayer.AddItem(New ZoomToLayer(), -1, 4, True, esriCommandStyles.esriCommandStyleTextOnly)

		m_menuLayer.AddItem(New RemoveLayer(), -1, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
		m_menuLayer.AddItem(New ZoomToLayer(), -1, 1, True, esriCommandStyles.esriCommandStyleTextOnly)

		'''/Set the hook of each menu
		m_menuSchematicLayer.SetHook(m_mapControl)
		m_menuLayer.SetHook(m_mapControl)
	End Sub

	Private Sub axTOCControl1_OnMouseDown(ByVal sender As Object, ByVal e As ITOCControlEvents_OnMouseDownEvent) Handles axTOCControl1.OnMouseDown
		If (e.button <> 2) Then Return

		Dim item As esriTOCControlItem = esriTOCControlItem.esriTOCControlItemNone
		Dim map As IBasicMap = Nothing
		Dim layer As ILayer = Nothing
		Dim other As Object = Nothing
		Dim index As Object = Nothing

		'Determine what kind of item is selected
		m_tocControl.HitTest(e.x, e.y, item, map, layer, other, index)

		'Ensure the item gets selected 
		If (item = esriTOCControlItem.esriTOCControlItemMap) Then
			m_tocControl.SelectItem(map, Nothing)
		Else
			m_tocControl.SelectItem(layer, Nothing)
		End If

		'Set the layer into the CustomProperty (me is used by the custom layer commands)			
		m_mapControl.CustomProperty = layer

		Dim schLayer As ISchematicLayer = TryCast(layer, ISchematicLayer)
		If (schLayer IsNot Nothing) Then	''' attach menu for SchematicLayer
			Dim schematicTarget As ISchematicTarget = TryCast(New ESRI.ArcGIS.SchematicControls.EngineSchematicEnvironmentClass(), ISchematicTarget)
			If (schematicTarget IsNot Nothing) Then schematicTarget.SchematicTarget = schLayer

			'Popup the correct context menu
			If (item = esriTOCControlItem.esriTOCControlItemLayer) Then m_menuSchematicLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd)

		Else ''' attach menu for Layer
			'Popup the correct context menu
			If (item = esriTOCControlItem.esriTOCControlItemLayer) Then m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd)
		End If
	End Sub


#Region "Main Menu event handlers"
	Private Sub menuNewDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNewDoc.Click
		'execute New Document command
		Dim Command As ICommand = New CreateNewDocument()
		Command.OnCreate(m_mapControl.Object)
		Command.OnClick()
	End Sub

	Private Sub menuOpenDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuOpenDoc.Click
		'execute Open Document command
		Dim Command As ICommand = New ControlsOpenDocCommandClass()
		Command.OnCreate(m_mapControl.Object)
		Command.OnClick()
		m_blnOpenDoc = True
		m_blnExistingMap = False
		cboFrame.Items.Clear()
	End Sub

	Private Sub menuSaveDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSaveDoc.Click
		'execute Save Document command
		If (m_mapControl.CheckMxFile(m_mapDocumentName)) Then

			'create a new instance of a MapDocument
			Dim mapDoc As IMapDocument = New MapDocumentClass()
			mapDoc.Open(m_mapDocumentName, String.Empty)

			'Make sure that the MapDocument is not read only
			If (mapDoc.IsReadOnly(m_mapDocumentName)) Then

				MsgBox("Map document is read only!")
				mapDoc.Close()
				Return
			End If

			'Replace its contents with the current map
			mapDoc.ReplaceContents(CType(m_mapControl.Map, IMxdContents))

			'save the MapDocument in order to persist it
			mapDoc.Save(mapDoc.UsesRelativePaths, False)

			'close the MapDocument
			mapDoc.Close()
		End If
	End Sub

	Private Sub menuSaveAs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSaveAs.Click
		'execute SaveAs Document command
		Dim Command As ICommand = New ControlsSaveAsDocCommandClass()
		Command.OnCreate(m_mapControl.Object)
		Command.OnClick()
	End Sub

	Private Sub menuExitApp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuExitApp.Click
		'exit the application
		Application.Exit()
	End Sub

#End Region


	'listen to MapReplaced event in order to update the status bar and the Save menu
	Private Sub axMapControl1_OnMapReplaced(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMapReplacedEvent) Handles axMapControl1.OnMapReplaced

		'get the current document name from the MapControl
		m_mapDocumentName = m_mapControl.DocumentFilename

		If (m_blnToolbarItemClick = True) Then
			m_blnToolbarItemClick = False
			m_blnExistingMap = True
			'need to add the new diagram to the combobox
			Dim p As IMap = CType(e.newMap, IMap)
			If (Not cboFrame.Items.Contains(p.Name)) Then cboFrame.Items.Add(p.Name.ToString())

			m_arrMaps.Add(p)
		End If

		If (m_blnExistingMap = False) Then
			Dim m As IMap
			m_arrMaps = axMapControl1.ReadMxMaps(m_mapDocumentName)
			Dim i As Integer
			For i = 0 To m_arrMaps.Count - 1
				m = CType(m_arrMaps.Element(i), IMap)

				If (Not cboFrame.Items.Contains(m.Name.ToString())) Then
					cboFrame.Items.Add(m.Name.ToString())
				End If
			Next
			cboFrame.Text = Me.axMapControl1.ActiveView.FocusMap.Name

		End If

		'if there is no MapDocument, disable the Save menu and clear the status bar
		If (m_mapDocumentName = String.Empty) Then
			menuSaveDoc.Enabled = False
			statusBarXY.Text = String.Empty
		Else
			'enable the Save menu and write the doc name to the status bar
			menuSaveDoc.Enabled = True
			statusBarXY.Text = Path.GetFileName(m_mapDocumentName)
		End If

		m_blnExistingMap = True

		cboFrame.Text = axMapControl1.Map.Name.ToString()
	End Sub

	Private Sub axMapControl1_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent) Handles axMapControl1.OnMouseMove
		statusBarXY.Text = String.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4))
	End Sub

	Private Sub onSelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFrame.SelectedValueChanged
		Dim m As IMap
		Dim i As Integer

		For i = 0 To m_arrMaps.Count - 1
			m = CType(m_arrMaps.Element(i), IMap)
			If (m.Name = cboFrame.Text) Then
				m_blnExistingMap = True
				m_mapControl.Map = CType(m_arrMaps.Element(i), IMap)
				m_mapControl.Refresh()
				m_blnExistingMap = False
				Return
			End If
		Next
	End Sub

	'specific for toolbar2
	Private Sub onToolbarItemClick(ByVal sender As Object, ByVal e As IToolbarControlEvents_OnItemClickEvent) Handles axToolbarControl2.OnItemClick
		If (e.index = 0) Then
			'clicked generate new diagram
			m_blnToolbarItemClick = True
		End If
		'if (m_blnOpenDoc = false)
		'{
		'    m_blnToolbarItemClick = true

		'}
		'else
		'{
		'    m_blnOpenDoc = false
		'}
	End Sub

End Class
