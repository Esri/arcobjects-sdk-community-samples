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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Geodatabase


' This is the main form of the application.

Namespace NAEngine
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class frmMain : Inherits System.Windows.Forms.Form
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		Private splitter1 As System.Windows.Forms.Splitter

		' Context menu objects for NAWindow's context menu
		Private contextMenu1 As System.Windows.Forms.ContextMenu
		Private WithEvents miLoadLocations As System.Windows.Forms.MenuItem
        Private WithEvents miClearLocations As System.Windows.Forms.MenuItem
        Private WithEvents miAddItem As System.Windows.Forms.MenuItem

        ' ArcGIS Controls on the form
        Private axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
        Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
        Private axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
        Private WithEvents axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl

        ' Listen for context menu on NAWindow
        Private m_onContextMenu As IEngineNAWindowEventsEx_OnContextMenuEventHandler
        Private m_OnNetworkLayersChanged As IEngineNetworkAnalystEnvironmentEvents_OnNetworkLayersChangedEventHandler
        Private m_OnCurrentNetworkLayerChanged As IEngineNetworkAnalystEnvironmentEvents_OnCurrentNetworkLayerChangedEventHandler

        ' Reference to ArcGIS Network Analyst extension Environment
        Private m_naEnv As IEngineNetworkAnalystEnvironment

        ' Reference to NAWindow.  Need to hold on to reference for events to work.
        Private m_naWindow As IEngineNAWindow

        ' Menu for our commands on the TOC context menu
        Private m_menuLayer As IToolbarMenu

        ' incrementor for auto generated names
        Private Shared autogenInt As Integer = 0

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
            Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
            Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
            Me.contextMenu1 = New System.Windows.Forms.ContextMenu()
            Me.miLoadLocations = New System.Windows.Forms.MenuItem()
            Me.miClearLocations = New System.Windows.Forms.MenuItem()
            Me.miAddItem = New System.Windows.Forms.MenuItem()
            CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' axMapControl1
            ' 
            Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.axMapControl1.Location = New System.Drawing.Point(227, 28)
            Me.axMapControl1.Name = "axMapControl1"
            Me.axMapControl1.OcxState = (CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State))
            Me.axMapControl1.Size = New System.Drawing.Size(645, 472)
            Me.axMapControl1.TabIndex = 2
            ' 
            ' axLicenseControl1
            ' 
            Me.axLicenseControl1.Enabled = True
            Me.axLicenseControl1.Location = New System.Drawing.Point(664, 0)
            Me.axLicenseControl1.Name = "axLicenseControl1"
            Me.axLicenseControl1.OcxState = (CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State))
            Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
            Me.axLicenseControl1.TabIndex = 1
            ' 
            ' axToolbarControl1
            ' 
            Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
            Me.axToolbarControl1.Location = New System.Drawing.Point(0, 0)
            Me.axToolbarControl1.Name = "axToolbarControl1"
            Me.axToolbarControl1.OcxState = (CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State))
            Me.axToolbarControl1.Size = New System.Drawing.Size(872, 28)
            Me.axToolbarControl1.TabIndex = 0
            ' 
            ' splitter1
            ' 
            Me.splitter1.Location = New System.Drawing.Point(224, 28)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(3, 472)
            Me.splitter1.TabIndex = 4
            Me.splitter1.TabStop = False
            ' 
            ' axTOCControl1
            ' 
            Me.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left
            Me.axTOCControl1.Location = New System.Drawing.Point(0, 28)
            Me.axTOCControl1.Name = "axTOCControl1"
            Me.axTOCControl1.OcxState = (CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State))
            Me.axTOCControl1.Size = New System.Drawing.Size(224, 472)
            Me.axTOCControl1.TabIndex = 1
            '			Me.axTOCControl1.OnMouseDown += New ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(Me.axTOCControl1_OnMouseDown);
            ' 
            ' contextMenu1
            ' 
            Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miLoadLocations, Me.miClearLocations})
            ' 
            ' miLoadLocations
            ' 
            Me.miLoadLocations.Index = 0
            Me.miLoadLocations.Text = "Load Locations..."
            '			Me.miLoadLocations.Click += New System.EventHandler(Me.miLoadLocations_Click);
            ' 
            ' miClearLocations
            ' 
            Me.miClearLocations.Index = 1
            Me.miClearLocations.Text = "Clear Locations"
            '			Me.miClearLocations.Click += New System.EventHandler(Me.miClearLocations_Click);
            ' 
            ' miAddItem
            ' 
            Me.miAddItem.Index = -1
            Me.miAddItem.Text = "Add Item"
            '			Me.miAddItem.Click += New System.EventHandler(Me.miAddItem_Click);
            ' 
            ' frmMain
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(872, 500)
            Me.Controls.Add(Me.axLicenseControl1)
            Me.Controls.Add(Me.axMapControl1)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.axTOCControl1)
            Me.Controls.Add(Me.axToolbarControl1)
            Me.Name = "frmMain"
            Me.Text = "Network Analyst Engine Application"
            '			Me.Load += New System.EventHandler(Me.frmMain_Load);
            CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()> _
        Shared Sub Main()
            Dim succeeded As Boolean = ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop)
            If succeeded Then
                Dim activeRunTimeInfo As ESRI.ArcGIS.RuntimeInfo = ESRI.ArcGIS.RuntimeManager.ActiveRuntime
                System.Diagnostics.Debug.Print(activeRunTimeInfo.Product.ToString())

                Application.Run(New frmMain())
            Else
                System.Windows.Forms.MessageBox.Show("Failed to bind to an active ArcGIS runtime")
            End If
        End Sub

        Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Add commands to the NALayer context menu
            m_menuLayer = New ToolbarMenuClass()

            Dim nItem As Integer = -1
            m_menuLayer.AddItem(New cmdLoadLocations(), -1, ++nItem, False, esriCommandStyles.esriCommandStyleTextOnly)
            m_menuLayer.AddItem(New cmdRemoveLayer(), -1, ++nItem, False, esriCommandStyles.esriCommandStyleTextOnly)
            m_menuLayer.AddItem(New cmdClearAnalysisLayer(), -1, ++nItem, True, esriCommandStyles.esriCommandStyleTextOnly)
            m_menuLayer.AddItem(New cmdNALayerProperties(), -1, ++nItem, True, esriCommandStyles.esriCommandStyleTextOnly)

            ' Since this ToolbarMenu is a standalone popup menu use the SetHook method to
            '  specify the object that will be sent as a "hook" to the menu commands in their OnCreate methods.
            m_menuLayer.SetHook(axMapControl1)

            ' Add command for ArcGIS Network Analyst extension env properties to end of "Network Analyst" dropdown menu
            nItem = -1
            Dim i As Integer = 0
            Do While i < axToolbarControl1.Count
                Dim item As IToolbarItem = axToolbarControl1.GetItem(i)
                Dim mnu As IToolbarMenu = item.Menu

                If mnu Is Nothing Then
                    i += 1
                    Continue Do
                End If

                Dim mnudef As IMenuDef = mnu.GetMenuDef()
                Dim name As String = mnudef.Name

                ' Find the ArcGIS Network Analyst extension solver menu drop down and note the index
                If name = "ControlToolsNetworkAnalyst_SolverMenu" Then
                    nItem = i
                    Exit Do
                End If
                i += 1
            Loop

            If nItem >= 0 Then
                ' Using the index found above, get the solver menu drop down and add the Properties command to the end of it.
                Dim item As IToolbarItem = axToolbarControl1.GetItem(nItem)
                Dim mnu As IToolbarMenu = item.Menu
                If Not mnu Is Nothing Then
                    mnu.AddItem(New cmdNAProperties(), -1, mnu.Count, True, esriCommandStyles.esriCommandStyleTextOnly)
                End If

                ' Since this ToolbarMenu is an item on the ToolbarControl the Hook is shared and initialized by the ToolbarControl.
                '  Therefore, SetHook is not called here, like it is for the menu above.
            End If

            ' Initialize naEnv variables
            m_naEnv = CommonFunctions.GetTheEngineNetworkAnalystEnvironment()
            If m_naEnv Is Nothing Then
                MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured")
                Return
            End If

            m_naEnv.ZoomToResultAfterSolve = False
            m_naEnv.ShowAnalysisMessagesAfterSolve = CInt(esriEngineNAMessageType.esriEngineNAMessageTypeInformative Or esriEngineNAMessageType.esriEngineNAMessageTypeWarning)

            ' Set up the buddy control and initialize the NA extension, so we can get to NAWindow to listen to window events.
            ' This is necessary, as the various controls are not yet set up. They need to be in order to get the NAWindow's events.
            axToolbarControl1.SetBuddyControl(axMapControl1)
            Dim ext As IExtension = TryCast(m_naEnv, IExtension)
            Dim obj As Object = axToolbarControl1.Object

            ext.Startup(obj)

            ' m_naWindow is set after Startup of the Network Analyst extension
            m_naWindow = m_naEnv.NAWindow
            If m_naWindow Is Nothing Then
                MessageBox.Show("Error: Unexpected null NAWindow")
                Return
            End If

            m_onContextMenu = New IEngineNAWindowEventsEx_OnContextMenuEventHandler(AddressOf OnContextMenu)
            AddHandler CType(m_naWindow, IEngineNAWindowEventsEx_Event).OnContextMenu, m_onContextMenu

            m_OnNetworkLayersChanged = New IEngineNetworkAnalystEnvironmentEvents_OnNetworkLayersChangedEventHandler(AddressOf OnNetworkLayersChanged)
            AddHandler CType(m_naEnv, IEngineNetworkAnalystEnvironmentEvents_Event).OnNetworkLayersChanged, m_OnNetworkLayersChanged

            m_OnCurrentNetworkLayerChanged = New IEngineNetworkAnalystEnvironmentEvents_OnCurrentNetworkLayerChangedEventHandler(AddressOf OnCurrentNetworkLayerChanged)
            AddHandler CType(m_naEnv, IEngineNetworkAnalystEnvironmentEvents_Event).OnCurrentNetworkLayerChanged, m_OnCurrentNetworkLayerChanged
        End Sub

		'  Show the TOC context menu when an NALayer is right-clicked on
		Private Sub axTOCControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent) Handles axTOCControl1.OnMouseDown
			If e.button <> 2 Then
			Return
			End If

			Dim item As esriTOCControlItem = esriTOCControlItem.esriTOCControlItemNone
			Dim map As IBasicMap = Nothing
			Dim layer As ILayer = Nothing
			Dim other As Object = Nothing
			Dim index As Object = Nothing

			'Determine what kind of item has been clicked on
			axTOCControl1.HitTest(e.x, e.y, item, map, layer, other, index)

			' Only implemented a context menu for NALayers.  Exit if the layer is anything else.
			If (TryCast(layer, INALayer)) Is Nothing Then
				Return
			End If

			axTOCControl1.SelectItem(layer)

			' Set the layer into the CustomProperty.
			' This is used by the other commands to know what layer was right-clicked on
			' in the table of contents.			
			axMapControl1.CustomProperty = layer

			'Popup the correct context menu and update the TOC when it's done.
			If item = esriTOCControlItem.esriTOCControlItemLayer Then
				m_menuLayer.PopupMenu(e.x, e.y, axTOCControl1.hWnd)
				Dim toc As ITOCControl = TryCast(axTOCControl1.Object, ITOCControl)
				toc.Update()
			End If
		End Sub

        Public Sub OnNetworkLayersChanged()
            ' The OnNetworkLayersChanged event is fired when a new INetworkLayer object is 
            '  added, removed, or renamed within a map.

            ' If the INetworkLayer is renamed interactively through the user interface 
            '  OnNetworkLayersChanged is fired. If the INetworkLayer is renamed programmatically 
            '  using the ILayer::Name property OnNetworkLayersChanged is not fired.
        End Sub

        Public Sub OnCurrentNetworkLayerChanged()
            ' The OnCurrentNetworkLayerChanged event is fired when the user interactively 
            '  changes the NetworkDataset or the IEngineNetworkAnalystEnvironment::CurrentNetworkLayer 
            '  is set programatically.
        End Sub

        Public Function OnContextMenu(ByVal x As Integer, ByVal y As Integer) As Boolean
            Dim pt As System.Drawing.Point = Me.PointToClient(System.Windows.Forms.Cursor.Position)

            ' Get the active category
            Dim activeCategory As IEngineNAWindowCategory2 = TryCast(m_naWindow.ActiveCategory, IEngineNAWindowCategory2)
            If activeCategory Is Nothing Then
                Return False
            End If

            Dim separator As MenuItem = New MenuItem("-")

            miLoadLocations.Enabled = False
            miClearLocations.Enabled = False

            ' in order for the AddItem choice to appear in the context menu, the class
            ' should be an input class, and it should not be editable
            Dim pNAClassDefinition As INAClassDefinition = activeCategory.NAClass.ClassDefinition
            If pNAClassDefinition.IsInput Then

                miLoadLocations.Enabled = True
                miClearLocations.Enabled = True

                ' canEditShape should be false for AddItem to Apply (default is false)
                ' if it's a StandaloneTable canEditShape is implicitly false (there's no shape to edit)
                Dim canEditShape As Boolean = False
                Dim pFields As IFields = pNAClassDefinition.Fields
                Dim nField As Integer = -1
                nField = pFields.FindField("Shape")
                If nField >= 0 Then
                    Dim naFieldType As Integer = 0
                    naFieldType = pNAClassDefinition.FieldType("Shape")

                    ' determining whether or not the shape field can be edited consists of running a bitwise comparison
                    ' on the FieldType of the shape field.  See the online help for a list of the possible field types.
                    ' For our case, we want to verify that the shape field is an input field.  If it is an input field, 
                    ' then we do NOT want to display the Add Item menu option.
                    If ((naFieldType And CInt(esriNAFieldType.esriNAFieldTypeInput)) = CInt(esriNAFieldType.esriNAFieldTypeInput)) Then
                        canEditShape = True
                    Else
                        canEditShape = False
                    End If
                End If

                If (Not canEditShape) Then
                    contextMenu1.MenuItems.Add(separator)
                    contextMenu1.MenuItems.Add(miAddItem)
                End If
            End If

            contextMenu1.Show(Me, pt)

            ' even if the miAddItem menu item has not been added, Remove() won't crash.
            contextMenu1.MenuItems.Remove(separator)
            contextMenu1.MenuItems.Remove(miAddItem)

            Return True
        End Function

        Private Sub miLoadLocations_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miLoadLocations.Click
            Dim mapControl As IMapControl3 = TryCast(axMapControl1.Object, IMapControl3)

            ' Show the Property Page form for ArcGIS Network Analyst extension
            Dim loadLocations As frmLoadLocations = New frmLoadLocations()
            If loadLocations.ShowModal(mapControl, m_naEnv) Then
                ' notify that the context has changed because we have added locations to a NAClass within it
                Dim contextEdit As INAContextEdit = TryCast(m_naEnv.NAWindow.ActiveAnalysis.Context, INAContextEdit)
                contextEdit.ContextChanged()

                ' If loaded locations, refresh the NAWindow and the Screen
                Dim naLayer As INALayer = m_naWindow.ActiveAnalysis
                mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent)
                m_naWindow.UpdateContent(m_naWindow.ActiveCategory)
            End If
        End Sub

        Private Sub miClearLocations_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miClearLocations.Click
            Dim mapControl As IMapControl3 = TryCast(axMapControl1.Object, IMapControl3)

            Dim naHelper As IEngineNetworkAnalystHelper = TryCast(m_naEnv, IEngineNetworkAnalystHelper)
            Dim naWindow As IEngineNAWindow = m_naWindow
            Dim naLayer As INALayer = naWindow.ActiveAnalysis

            ' we do not have to run ContextChanged() as with adding an item and loading locations,
            ' because that is done by the DeleteAllNetworkLocations method.
            naHelper.DeleteAllNetworkLocations()

            mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent)
        End Sub

        Private Sub miAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miAddItem.Click
            ' Developers Note:
            ' Once an item has been added, the user can double click on the item to edit the properties
            ' of the item.  For the purposes of this sample, only the default values from the InitDefaultValues method
            ' and an auto generated Name value are populated initially for the new item.

            Dim mapControl As IMapControl3 = TryCast(axMapControl1.Object, IMapControl3)

            Dim activeCategory As IEngineNAWindowCategory2 = TryCast(m_naWindow.ActiveCategory, IEngineNAWindowCategory2)
            Dim pDataLayer As IDataLayer = activeCategory.DataLayer

            ' In order to add an item, we need to create a new row in the class and populate it 
            ' with the initial default values for that class.
            Dim table As ITable = TryCast(pDataLayer, ITable)
            Dim row As IRow = table.CreateRow()
            Dim rowSubtypes As IRowSubtypes = TryCast(row, IRowSubtypes)
            rowSubtypes.InitDefaultValues()

            ' we need to auto generate a display name for the newly added item.
            ' In some cases (depending on how the schema is set up) InitDefaultValues may result in a nonempty name string 
            ' in these cases do not override the preexisting non-empty name string with our auto generated one.
            Dim ipFeatureLayer As IFeatureLayer = TryCast(activeCategory.Layer, IFeatureLayer)
            Dim ipStandaloneTable As IStandaloneTable = TryCast(pDataLayer, IStandaloneTable)
            Dim name As String = ""
            If Not ipFeatureLayer Is Nothing Then
                name = ipFeatureLayer.DisplayField
            ElseIf Not ipStandaloneTable Is Nothing Then
                name = ipStandaloneTable.DisplayField
            End If

            '  If the display field is an empty string or does not represent an actual field on the NAClass just skip the auto generation.  
            ' (Some custom solvers may not have set the DisplayField for example).
            ' Note:  The name we are auto generating does not have any spaces in it.  This is to ensure that that any classes 
            ' that are space sensitive will be able to handle the name (ex Specialties).
            Dim currentName As String = ""
            Dim fieldIndex As Integer = row.Fields.FindField(name)
            If (fieldIndex >= 0) Then
                currentName = CType(row.Value(fieldIndex), String)
                If (currentName.Length <= 0) Then
                    autogenInt += 1
                    row.Value(fieldIndex) = "Item" & autogenInt
                End If
            End If

            ' A special case is OrderPairs NAClass because that effectively has a combined 2 field display field.  
            ' You will have to hard code to look for that NAClassName and create a default name for 
            ' both first order and second order field names so the name will display correctly 
            ' (look for the NAClass Name and NOT the layer name).
            Dim naClassDef As INAClassDefinition = activeCategory.NAClass.ClassDefinition
            If (naClassDef.Name = "OrderPairs") Then
                fieldIndex = row.Fields.FindField("SecondOrderName")
                If (fieldIndex >= 0) Then
                    Dim secondName As String = CType(row.Value(fieldIndex), String)
                    If (secondName.Length <= 0) Then
                        autogenInt += 1
                        row.Value(fieldIndex) = "Item" & autogenInt
                    End If
                End If
            End If

            row.Store()

            ' notify that the context has changed because we have added an item to a NAClass within it
            Dim contextEdit As INAContextEdit = CType(m_naEnv.NAWindow.ActiveAnalysis.Context, INAContextEdit)
            contextEdit.ContextChanged()

            ' refresh the NAWindow and the Screen
            Dim naLayer As INALayer = m_naWindow.ActiveAnalysis
            mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent)
            m_naWindow.UpdateContent(m_naWindow.ActiveCategory)
        End Sub
    End Class
End Namespace
