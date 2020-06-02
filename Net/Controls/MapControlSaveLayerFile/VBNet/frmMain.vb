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
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS


''' <summary>
''' Summary description for frmMain.
''' </summary>
Public Class frmMain : Inherits System.Windows.Forms.Form



#Region "GUI element members"
    Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Private WithEvents axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Private WithEvents axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Private axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Private splitter1 As System.Windows.Forms.Splitter
    Private statusBar1 As System.Windows.Forms.StatusBar
    Private mainMenu1 As System.Windows.Forms.MainMenu
    Private menuFile As System.Windows.Forms.MenuItem
    Private WithEvents menuNewDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuOpenDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuSaveDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuSaveAsDoc As System.Windows.Forms.MenuItem
    Private menuSeparator As System.Windows.Forms.MenuItem
    Private WithEvents menuExitApp As System.Windows.Forms.MenuItem
#End Region

    'private class members
    Private m_mapControl As IMapControl3 = Nothing
    Private m_mapDocumentName As String = String.Empty
    Private statusBarXY As StatusBarPanel
    Private components As IContainer
    Private m_contextMenu As ContextMenuClass = Nothing

    Public Sub New()
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()

        '
        ' TODO: Add any constructor code after InitializeComponent call
        ''
    End Sub

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
        'Failure to do this may result in random crashes on exit due to the operating system unloading 
        'the libraries in the incorrect order. 
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.statusBar1 = New System.Windows.Forms.StatusBar()
        Me.statusBarXY = New System.Windows.Forms.StatusBarPanel()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuFile = New System.Windows.Forms.MenuItem()
        Me.menuNewDoc = New System.Windows.Forms.MenuItem()
        Me.menuOpenDoc = New System.Windows.Forms.MenuItem()
        Me.menuSaveDoc = New System.Windows.Forms.MenuItem()
        Me.menuSaveAsDoc = New System.Windows.Forms.MenuItem()
        Me.menuSeparator = New System.Windows.Forms.MenuItem()
        Me.menuExitApp = New System.Windows.Forms.MenuItem()
        Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
        Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
        Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
        Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        CType(Me.statusBarXY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' statusBar1
        ' 
        Me.statusBar1.Location = New System.Drawing.Point(0, 512)
        Me.statusBar1.Name = "statusBar1"
        Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarXY})
        Me.statusBar1.ShowPanels = True
        Me.statusBar1.Size = New System.Drawing.Size(784, 22)
        Me.statusBar1.TabIndex = 4
        ' 
        ' statusBarXY
        ' 
        Me.statusBarXY.Name = "statusBarXY"
        Me.statusBarXY.Width = 210
        ' 
        ' mainMenu1
        ' 
        Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile})
        ' 
        ' menuFile
        ' 
        Me.menuFile.Index = 0
        Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuNewDoc, Me.menuOpenDoc, Me.menuSaveDoc, Me.menuSaveAsDoc, Me.menuSeparator, Me.menuExitApp})
        Me.menuFile.Text = "File"
        ' 
        ' menuNewDoc
        ' 
        Me.menuNewDoc.Index = 0
        Me.menuNewDoc.Text = "New Document"
        '	  Me.menuNewDoc.Click += New System.EventHandler(Me.menuNewDoc_Click);
        ' 
        ' menuOpenDoc
        ' 
        Me.menuOpenDoc.Index = 1
        Me.menuOpenDoc.Text = "Open Document..."
        '	  Me.menuOpenDoc.Click += New System.EventHandler(Me.menuOpenDoc_Click);
        ' 
        ' menuSaveDoc
        ' 
        Me.menuSaveDoc.Index = 2
        Me.menuSaveDoc.Text = "SaveDocuement"
        '	  Me.menuSaveDoc.Click += New System.EventHandler(Me.menuSaveDoc_Click);
        ' 
        ' menuSaveAsDoc
        ' 
        Me.menuSaveAsDoc.Index = 3
        Me.menuSaveAsDoc.Text = "Save As..."
        '	  Me.menuSaveAsDoc.Click += New System.EventHandler(Me.menuSaveAsDoc_Click);
        ' 
        ' menuSeparator
        ' 
        Me.menuSeparator.Index = 4
        Me.menuSeparator.Text = "-"
        ' 
        ' menuExitApp
        ' 
        Me.menuExitApp.Index = 5
        Me.menuExitApp.Text = "Exit"
        '	  Me.menuExitApp.Click += New System.EventHandler(Me.menuExitApp_Click);
        ' 
        ' axLicenseControl1
        ' 
        Me.axLicenseControl1.Enabled = True
        Me.axLicenseControl1.Location = New System.Drawing.Point(320, 176)
        Me.axLicenseControl1.Name = "axLicenseControl1"
        Me.axLicenseControl1.OcxState = (CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.axLicenseControl1.TabIndex = 5
        ' 
        ' axMapControl1
        ' 
        Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.axMapControl1.Location = New System.Drawing.Point(0, 28)
        Me.axMapControl1.Name = "axMapControl1"
        Me.axMapControl1.OcxState = (CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axMapControl1.Size = New System.Drawing.Size(784, 484)
        Me.axMapControl1.TabIndex = 6
        '	  Me.axMapControl1.OnMouseMove += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(Me.axMapControl1_OnMouseMove);
        '	  Me.axMapControl1.OnMapReplaced += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(Me.axMapControl1_OnMapReplaced);
        ' 
        ' axTOCControl1
        ' 
        Me.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.axTOCControl1.Location = New System.Drawing.Point(3, 28)
        Me.axTOCControl1.Name = "axTOCControl1"
        Me.axTOCControl1.OcxState = (CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axTOCControl1.Size = New System.Drawing.Size(209, 484)
        Me.axTOCControl1.TabIndex = 7
        '	  Me.axTOCControl1.OnMouseDown += New ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(Me.axTOCControl1_OnMouseDown);
        ' 
        ' axToolbarControl1
        ' 
        Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.axToolbarControl1.Location = New System.Drawing.Point(0, 0)
        Me.axToolbarControl1.Name = "axToolbarControl1"
        Me.axToolbarControl1.OcxState = (CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axToolbarControl1.Size = New System.Drawing.Size(784, 28)
        Me.axToolbarControl1.TabIndex = 8
        ' 
        ' splitter1
        ' 
        Me.splitter1.Location = New System.Drawing.Point(0, 28)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(3, 484)
        Me.splitter1.TabIndex = 9
        Me.splitter1.TabStop = False
        ' 
        ' frmMain
        ' 
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(784, 534)
        Me.Controls.Add(Me.axTOCControl1)
        Me.Controls.Add(Me.axLicenseControl1)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.axMapControl1)
        Me.Controls.Add(Me.axToolbarControl1)
        Me.Controls.Add(Me.statusBar1)
        Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
        Me.Menu = Me.mainMenu1
        Me.Name = "frmMain"
        Me.Text = "ArcEngine Controls Application"
        '	  Me.Load += New System.EventHandler(Me.frmMain_Load);
        CType(Me.statusBarXY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    <STAThread()> _
    Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
        Application.Run(New frmMain())
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get the MapControl
        m_mapControl = CType(axMapControl1.Object, IMapControl3)

        'disable the Save menu (since there is no document yet)
        menuSaveDoc.Enabled = False

        m_contextMenu = New ContextMenuClass()
        m_contextMenu.SetHook(axMapControl1.Object)

        'add the load layer file command to the map
        axToolbarControl1.AddItem(New LoadLayerFileCmd(), -1, 2, False, -1, esriCommandStyles.esriCommandStyleIconOnly)

        m_contextMenu.ContextMenu.AddItem(New SaveLayerFileCmd(), -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
        m_contextMenu.ContextMenu.AddItem(New RemoveLayerCmd(), -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
    End Sub

#Region "Main Menu event handlers"
    Private Sub menuExitApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExitApp.Click
        'exit the application
        Application.Exit()
    End Sub

    Private Sub menuNewDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuNewDoc.Click
        'execute New Document command
        Dim command As ICommand = New CreateNewDocument()
        command.OnCreate(m_mapControl.Object)
        command.OnClick()
    End Sub

    Private Sub menuOpenDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenDoc.Click
        'execute Open Document command
        Dim command As ICommand = New ControlsOpenDocCommandClass()
        command.OnCreate(m_mapControl.Object)
        command.OnClick()
    End Sub

    Private Sub menuSaveDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveDoc.Click
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

    Private Sub menuSaveAsDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveAsDoc.Click
        'execute SaveAs Document command
        Dim command As ICommand = New ControlsSaveAsDocCommandClass()
        command.OnCreate(m_mapControl.Object)
        command.OnClick()
    End Sub
#End Region

    'listen to MapReplaced event in order to update the statusbar and the Save menu
    Private Sub axMapControl1_OnMapReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent) Handles axMapControl1.OnMapReplaced
        'get the current document name from the MapControl
        m_mapDocumentName = m_mapControl.DocumentFilename

        'if there is no MapDocument, disable the Save menu and clear the statusbar
        If m_mapDocumentName = String.Empty Then
            menuSaveDoc.Enabled = False
            statusBar1.Text = String.Empty
        Else
            'enable the Save menu and write the doc name to the statusbar
            menuSaveDoc.Enabled = True
            statusBar1.Text = Path.GetFileName(m_mapDocumentName)
        End If
    End Sub

    Private Sub axMapControl1_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent) Handles axMapControl1.OnMouseMove
        statusBarXY.Text = String.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4))
    End Sub

    Private Sub axTOCControl1_OnMouseDown(ByVal sender As Object, ByVal e As ITOCControlEvents_OnMouseDownEvent) Handles axTOCControl1.OnMouseDown
        'make sure that the user right clicked
        If 2 <> e.button Then
            Return
        End If

        'use HitTest in order to test whether the user has selected a featureLayer
        Dim item As esriTOCControlItem = esriTOCControlItem.esriTOCControlItemNone
        Dim map As IBasicMap = Nothing
        Dim layer As ILayer = Nothing
        Dim other As Object = Nothing
        Dim index As Object = Nothing

        'do the HitTest
        axTOCControl1.HitTest(e.x, e.y, item, map, layer, other, index)

        'Determine what kind of item has been clicked on
        If Nothing Is layer OrElse Not (TypeOf layer Is IFeatureLayer) Then
            Return
        End If

        'set the featurelayer as the custom property of the MapControl
        axMapControl1.CustomProperty = layer

        'popup a context menu with a 'Properties' command
        m_contextMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd)
    End Sub
End Class
