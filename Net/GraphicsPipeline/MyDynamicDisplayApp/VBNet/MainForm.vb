'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI

  Public NotInheritable Partial Class MainForm : Inherits Form
	#Region "class private members"
	Private m_mapControl As IMapControl3 = Nothing
	Private m_mapDocumentName As String = String.Empty
	#End Region

	#Region "class constructor"
	Public Sub New()
	  InitializeComponent()
	End Sub
	#End Region

	Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
	  'get the MapControl
	  m_mapControl = CType(axMapControl1.Object, IMapControl3)

	  'disable the Save menu (since there is no document yet)
	  menuSaveDoc.Enabled = False

	  axToolbarControl1.AddItem(New ToggleDynamicDisplayCmd())
	  axToolbarControl1.AddItem(New LoadDynamicLayerCmd())
	End Sub

	#Region "Main Menu event handlers"
	Private Sub menuNewDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuNewDoc.Click
	  'execute New Document command
	  Dim command As ICommand = New CreateNewDocument()
	  command.OnCreate(m_mapControl.Object)
	  command.OnClick()
	End Sub

	Private Sub menuOpenDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuOpenDoc.Click
	  'execute Open Document command
	  Dim command As ICommand = New ControlsOpenDocCommandClass()
	  command.OnCreate(m_mapControl.Object)
	  command.OnClick()
	End Sub

	Private Sub menuSaveDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuSaveDoc.Click
	  'execute Save Document command
	  If m_mapControl.CheckMxFile(m_mapDocumentName) Then
			'create a new instance of a MapDocument
			Dim mapDoc As IMapDocument = New MapDocumentClass()
			mapDoc.Open(m_mapDocumentName, String.Empty)

			'Make sure that the MapDocument is not readonly
			If mapDoc.IsReadOnly(m_mapDocumentName) Then
			 MessageBox.Show("Map document is read only!")
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
	  Dim command As ICommand = New ControlsSaveAsDocCommandClass()
	  command.OnCreate(m_mapControl.Object)
	  command.OnClick()
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

        'if there is no MapDocument, disable the Save menu and clear the status bar
	  If m_mapDocumentName = String.Empty Then
			menuSaveDoc.Enabled = False
			statusBarXY.Text = String.Empty
	  Else
            'enable the Save menu and write the doc name to the status bar
			menuSaveDoc.Enabled = True
			statusBarXY.Text = Path.GetFileName(m_mapDocumentName)
	  End If
	End Sub

	Private Sub axMapControl1_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent) Handles axMapControl1.OnMouseMove
	  statusBarXY.Text = String.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4))
	End Sub
  End Class
