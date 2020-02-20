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
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS

''' <summary>
''' Summary description for frmMain.
''' </summary>
Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region "GUI elements"

    Private statusBar1 As System.Windows.Forms.StatusBar
    Private statusBarXYUnits As System.Windows.Forms.StatusBarPanel
    Private mainMenu1 As System.Windows.Forms.MainMenu
    Private menuFile As System.Windows.Forms.MenuItem
    Private WithEvents menuOpenDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuNewDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuSaveDoc As System.Windows.Forms.MenuItem
    Private WithEvents menuSaveAsDoc As System.Windows.Forms.MenuItem
    Private menuSeparator As System.Windows.Forms.MenuItem
    Private WithEvents menuAppExit As System.Windows.Forms.MenuItem
    Private axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Private axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl
    Private tabPage1 As System.Windows.Forms.TabPage
    Private tabPage2 As System.Windows.Forms.TabPage
    Private axToolbarControl2 As ESRI.ArcGIS.Controls.AxToolbarControl
    Private WithEvents axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Private WithEvents axPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Private splitter1 As System.Windows.Forms.Splitter
#End Region

    Private m_mapControl As ESRI.ArcGIS.Controls.IMapControl3 = Nothing
    Private m_pageLayoutControl As ESRI.ArcGIS.Controls.IPageLayoutControl2 = Nothing
    Private m_controlsSynchronizer As ControlsSynchronizer = Nothing
    Private axLicenseControl1 As AxLicenseControl
    Private components As IContainer
    Private m_documentFileName As String = String.Empty

    Public Sub New()
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

#Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuFile = New System.Windows.Forms.MenuItem()
        Me.menuOpenDoc = New System.Windows.Forms.MenuItem()
        Me.menuNewDoc = New System.Windows.Forms.MenuItem()
        Me.menuSaveDoc = New System.Windows.Forms.MenuItem()
        Me.menuSaveAsDoc = New System.Windows.Forms.MenuItem()
        Me.menuSeparator = New System.Windows.Forms.MenuItem()
        Me.menuAppExit = New System.Windows.Forms.MenuItem()
        Me.statusBar1 = New System.Windows.Forms.StatusBar()
        Me.statusBarXYUnits = New System.Windows.Forms.StatusBarPanel()
        Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl()
        Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.axPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl()
        Me.axToolbarControl2 = New ESRI.ArcGIS.Controls.AxToolbarControl()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        CType(Me.statusBarXYUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage2.SuspendLayout()
        CType(Me.axPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.axToolbarControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' mainMenu1
        ' 
        Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile})
        ' 
        ' menuFile
        ' 
        Me.menuFile.Index = 0
        Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuOpenDoc, Me.menuNewDoc, Me.menuSaveDoc, Me.menuSaveAsDoc, Me.menuSeparator, Me.menuAppExit})
        Me.menuFile.Text = "File"
        ' 
        ' menuOpenDoc
        ' 
        Me.menuOpenDoc.Index = 0
        Me.menuOpenDoc.Text = "Open..."
        '	  Me.menuOpenDoc.Click += New System.EventHandler(Me.menuOpenDoc_Click);
        ' 
        ' menuNewDoc
        ' 
        Me.menuNewDoc.Index = 1
        Me.menuNewDoc.Text = "New..."
        '	  Me.menuNewDoc.Click += New System.EventHandler(Me.menuNewDoc_Click);
        ' 
        ' menuSaveDoc
        ' 
        Me.menuSaveDoc.Index = 2
        Me.menuSaveDoc.Text = "Save"
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
        ' menuAppExit
        ' 
        Me.menuAppExit.Index = 5
        Me.menuAppExit.Text = "Exit"
        '	  Me.menuAppExit.Click += New System.EventHandler(Me.menuAppExit_Click);
        ' 
        ' statusBar1
        ' 
        Me.statusBar1.Location = New System.Drawing.Point(0, 544)
        Me.statusBar1.Name = "statusBar1"
        Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarXYUnits})
        Me.statusBar1.ShowPanels = True
        Me.statusBar1.Size = New System.Drawing.Size(784, 22)
        Me.statusBar1.TabIndex = 5
        ' 
        ' statusBarXYUnits
        ' 
        Me.statusBarXYUnits.Name = "statusBarXYUnits"
        Me.statusBarXYUnits.Width = 200
        ' 
        ' axToolbarControl1
        ' 
        Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.axToolbarControl1.Location = New System.Drawing.Point(0, 0)
        Me.axToolbarControl1.Name = "axToolbarControl1"
        Me.axToolbarControl1.OcxState = (CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axToolbarControl1.Size = New System.Drawing.Size(784, 28)
        Me.axToolbarControl1.TabIndex = 7
        ' 
        ' axTOCControl1
        ' 
        Me.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.axTOCControl1.Location = New System.Drawing.Point(0, 28)
        Me.axTOCControl1.Name = "axTOCControl1"
        Me.axTOCControl1.OcxState = (CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axTOCControl1.Size = New System.Drawing.Size(200, 516)
        Me.axTOCControl1.TabIndex = 8
        ' 
        ' tabControl1
        ' 
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(200, 28)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(584, 488)
        Me.tabControl1.TabIndex = 9
        '	  Me.tabControl1.SelectedIndexChanged += New System.EventHandler(Me.tabControl1_SelectedIndexChanged);
        ' 
        ' tabPage1
        ' 
        Me.tabPage1.Controls.Add(Me.axLicenseControl1)
        Me.tabPage1.Controls.Add(Me.axMapControl1)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Size = New System.Drawing.Size(576, 462)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Map"
        ' 
        ' axLicenseControl1
        ' 
        Me.axLicenseControl1.Enabled = True
        Me.axLicenseControl1.Location = New System.Drawing.Point(210, 220)
        Me.axLicenseControl1.Name = "axLicenseControl1"
        Me.axLicenseControl1.OcxState = (CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.axLicenseControl1.TabIndex = 1
        ' 
        ' axMapControl1
        ' 
        Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.axMapControl1.Location = New System.Drawing.Point(0, 0)
        Me.axMapControl1.Name = "axMapControl1"
        Me.axMapControl1.OcxState = (CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axMapControl1.Size = New System.Drawing.Size(576, 462)
        Me.axMapControl1.TabIndex = 0
        '	  Me.axMapControl1.OnMouseMove += New ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(Me.axMapControl1_OnMouseMove);
        ' 
        ' tabPage2
        ' 
        Me.tabPage2.Controls.Add(Me.axPageLayoutControl1)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Size = New System.Drawing.Size(576, 462)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Layout"
        ' 
        ' axPageLayoutControl1
        ' 
        Me.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.axPageLayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.axPageLayoutControl1.Name = "axPageLayoutControl1"
        Me.axPageLayoutControl1.OcxState = (CType(resources.GetObject("axPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axPageLayoutControl1.Size = New System.Drawing.Size(576, 462)
        Me.axPageLayoutControl1.TabIndex = 0
        '	  Me.axPageLayoutControl1.OnMouseMove += New ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseMoveEventHandler(Me.axPageLayoutControl1_OnMouseMove);
        ' 
        ' axToolbarControl2
        ' 
        Me.axToolbarControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.axToolbarControl2.Location = New System.Drawing.Point(200, 516)
        Me.axToolbarControl2.Name = "axToolbarControl2"
        Me.axToolbarControl2.OcxState = (CType(resources.GetObject("axToolbarControl2.OcxState"), System.Windows.Forms.AxHost.State))
        Me.axToolbarControl2.Size = New System.Drawing.Size(584, 28)
        Me.axToolbarControl2.TabIndex = 10
        ' 
        ' splitter1
        ' 
        Me.splitter1.Location = New System.Drawing.Point(200, 28)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(3, 488)
        Me.splitter1.TabIndex = 11
        Me.splitter1.TabStop = False
        ' 
        ' frmMain
        ' 
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(784, 566)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.axToolbarControl2)
        Me.Controls.Add(Me.axTOCControl1)
        Me.Controls.Add(Me.axToolbarControl1)
        Me.Controls.Add(Me.statusBar1)
        Me.Menu = Me.mainMenu1
        Me.Name = "frmMain"
        Me.Text = "Map & PageLayout synchronization"
        '	  Me.Load += New System.EventHandler(Me.frmMain_Load);
        CType(Me.statusBarXYUnits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage2.ResumeLayout(False)
        CType(Me.axPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.axToolbarControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "menu event handlers"
    ''' <summary>
    ''' Open New Document menu event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub menuOpenDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenDoc.Click
        ' switch to map view
        tabControl1.SelectedTab = CType(tabControl1.Controls(0), TabPage)

        'launch the OpenMapDoc command
        Dim openMapDoc As OpenNewMapDocument = New OpenNewMapDocument(m_controlsSynchronizer)
        openMapDoc.OnCreate(m_controlsSynchronizer.MapControl.Object)
        openMapDoc.OnClick()

        m_documentFileName = openMapDoc.DocumentFileName
    End Sub

    ''' <summary>
    ''' New Map document menu event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub menuNewDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuNewDoc.Click
        'ask the user whether he'd like to save the current doc
        Dim res As DialogResult = MessageBox.Show("Would you like to save the current document?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If res = DialogResult.Yes Then
            'if yes, launch the SaveAs command
            Dim command As ICommand = New ControlsSaveAsDocCommandClass()
            command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object)
            command.OnClick()
        End If

        ' switch to map view
        tabControl1.SelectedTab = CType(tabControl1.Controls(0), TabPage)

        'create a new Map instance
        Dim map As IMap = New MapClass()
        map.Name = "Map"
        'replace the shared map with the new Map instance
        m_controlsSynchronizer.ReplaceMap(map)

        m_documentFileName = String.Empty
    End Sub

    ''' <summary>
    ''' Save document menu event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Save the current MapDocument</remarks>
    Private Sub menuSaveDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveDoc.Click
        'make sure that the current MapDoc is valid first
        If m_documentFileName <> String.Empty AndAlso m_mapControl.CheckMxFile(m_documentFileName) Then
            'create a new instance of a MapDocument class
            Dim mapDoc As IMapDocument = New MapDocumentClass()
            'Open the current document into the MapDocument
            mapDoc.Open(m_documentFileName, String.Empty)

            'Replace the map with the one of the PageLayout
            mapDoc.ReplaceContents(CType(m_pageLayoutControl.PageLayout, IMxdContents))

            'save the document
            mapDoc.Save(mapDoc.UsesRelativePaths, False)
            mapDoc.Close()
        End If
    End Sub

    ''' <summary>
    ''' SaveAs document menu event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Save the current MapDocument as a new doc</remarks>
    Private Sub menuSaveAsDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveAsDoc.Click
        'launch the SaveAs command. Always use the PageLayoutControl in order to keep 
        ' all of the map surrounds. 
        Dim command As ICommand = New ControlsSaveAsDocCommandClass()
        command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object)
        command.OnClick()
    End Sub

    ''' <summary>
    ''' Exit menu event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub menuAppExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAppExit.Click
        Application.Exit()
    End Sub
#End Region

    ''' <summary>
    ''' Form.Load method
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get a reference to the MapControl and the PageLayoutControl
        m_mapControl = CType(axMapControl1.Object, IMapControl3)
        m_pageLayoutControl = CType(axPageLayoutControl1.Object, IPageLayoutControl2)

        'initialize the controls synchronization class
        m_controlsSynchronizer = New ControlsSynchronizer(m_mapControl, m_pageLayoutControl)

        'bind the controls together (both point at the same map) and set the MapControl as the active control
        m_controlsSynchronizer.BindControls(True)

        'add the framework controls (TOC and Toolbars) in order to synchronize then when the
        'active control changes (call SetBuddyControl)
        m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object)
        m_controlsSynchronizer.AddFrameworkControl(axToolbarControl2.Object)
        m_controlsSynchronizer.AddFrameworkControl(axTOCControl1.Object)

        'add the Open Map Document command onto the toolbar
        Dim openMapDoc As OpenNewMapDocument = New OpenNewMapDocument(m_controlsSynchronizer)
        axToolbarControl1.AddItem(openMapDoc, -1, 0, False, -1, esriCommandStyles.esriCommandStyleIconOnly)
    End Sub

    ''' <summary>
    ''' SelectedIndexChanged event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabControl1.SelectedIndexChanged
        If tabControl1.SelectedIndex = 0 Then 'map view
            'activate the MapControl and deactivate the PageLayoutControl
            m_controlsSynchronizer.ActivateMap()
        Else 'layout view
            'activate the PageLayoutControl and deactivate the MapControl
            m_controlsSynchronizer.ActivatePageLayout()
        End If
    End Sub

    ''' <summary>
    ''' MapControl MouseMove event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub axMapControl1_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent) Handles axMapControl1.OnMouseMove
        statusBarXYUnits.Text = String.Format("{0} {1} {2}", e.mapX.ToString("#######.###"), e.mapY.ToString("#######.###"), axMapControl1.MapUnits.ToString().Substring(4))
    End Sub

    ''' <summary>
    ''' PageLayoutControl MouseMove event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub axPageLayoutControl1_OnMouseMove(ByVal sender As Object, ByVal e As IPageLayoutControlEvents_OnMouseMoveEvent) Handles axPageLayoutControl1.OnMouseMove
        statusBarXYUnits.Text = String.Format("{0} {1} {2}", e.pageX.ToString("###.##"), e.pageY.ToString("###.##"), axPageLayoutControl1.Page.Units.ToString().Substring(4))
    End Sub
End Class
