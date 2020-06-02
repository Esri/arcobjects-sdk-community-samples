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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS


Public Class CulturalResources
    Inherits System.Windows.Forms.Form
    Friend WithEvents lblCulture2 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Private m_pToolbarMenu As ToolbarMenu


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CulturalResources))
        Me.lblCulture2 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCulture2
        '
        Me.lblCulture2.AutoSize = True
        Me.lblCulture2.Location = New System.Drawing.Point(3, 479)
        Me.lblCulture2.Name = "lblCulture2"
        Me.lblCulture2.Size = New System.Drawing.Size(53, 17)
        Me.lblCulture2.TabIndex = 10
        Me.lblCulture2.Text = "Culture"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.18451!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.81549!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblCulture2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AxTOCControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxPageLayoutControl1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxLicenseControl1, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.30689!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.69311!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(878, 513)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'AxToolbarControl1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AxToolbarControl1, 2)
        Me.AxToolbarControl1.Location = New System.Drawing.Point(3, 3)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(870, 28)
        Me.AxToolbarControl1.TabIndex = 11
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(3, 38)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(177, 436)
        Me.AxTOCControl1.TabIndex = 12
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(188, 38)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(686, 436)
        Me.AxPageLayoutControl1.TabIndex = 13
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(188, 482)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 14
        '
        'CulturalResources
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(892, 528)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximumSize = New System.Drawing.Size(900, 568)
        Me.MinimumSize = New System.Drawing.Size(900, 568)
        Me.Name = "CulturalResources"
        Me.Text = "Form1"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    <STAThread()> _
    Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New CulturalResources())
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim pCulture As System.Globalization.CultureInfo

        'Set the Thread UI Culture manually by uncommenting one of the three cultures 
        'that you wish to set below.

        pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR")
        'pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        'pCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")

        'Set the Thread UI Culture
        System.Threading.Thread.CurrentThread.CurrentUICulture = pCulture

        'Confirm that the Thread UI Culture is set.
        lblCulture2.Text = "Current Thread UI Culture = " + System.Threading.Thread.CurrentThread.CurrentUICulture.DisplayName

        Dim sProgID As String

        'Add command to open an mxd document
        sProgID = "esriControls.ControlsOpenDocCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add Map navigation commands
        sProgID = "esriControls.ControlsMapZoomInTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsMapZoomOutTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsMapPanTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsMapFullExtentCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add PageLayout navigation commands
        sProgID = "esriControls.ControlsPageZoomInTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsPageZoomOutTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsPagePanTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsPageZoomWholePageCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsPageZoomPageToLastExtentBackCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsPageZoomPageToLastExtentForwardCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        sProgID = "esriControls.ControlsSelectTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add Culture Tool
        sProgID = "VBDotNETCultureSample.CultureTool"
        AxToolbarControl1.AddItem(sProgID, -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconAndText)

        'Add Culture Command
        sProgID = "VBDotNETCultureSample.CultureCommand"
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

        'Add Culture Menu to Toolbar and Popup
        sProgID = "VBDotNETCultureSample.CultureMenu"

        'Add the Menu to a new ToolbarMenu to be used as the popup
        m_pToolbarMenu = New ToolbarMenu
        m_pToolbarMenu.AddItem(sProgID)
        m_pToolbarMenu.SetHook(AxToolbarControl1)

        'Add the Culture Menu to the ToolbarControl
        AxToolbarControl1.AddItem(sProgID, -1, -1, False, 0, esriCommandStyles.esriCommandStyleTextOnly)

        'Set Buddy Controls
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)

    End Sub


    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent)

        'Display the Popup Menu so that it appears to the right of the click

        If e.button = 2 Then

            'Cast to the IToolbarMenu2 interface of the m_pToolbarMenu
            Dim m_pToolbarMenu2 As IToolbarMenu2
            m_pToolbarMenu2 = CType(m_pToolbarMenu, IToolbarMenu)

            'Align the Menu so that it appears to the right of the user mouse click
            m_pToolbarMenu2.AlignLeft = True

            'Popup the menu
            m_pToolbarMenu.PopupMenu(e.x, e.y, AxPageLayoutControl1.hWnd)
        End If

    End Sub

End Class
