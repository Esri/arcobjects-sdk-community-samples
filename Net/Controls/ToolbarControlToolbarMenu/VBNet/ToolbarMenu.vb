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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS


Public Class frmToolbarMenu
    Inherits System.Windows.Forms.Form

    <STAThread()> _
    Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New frmToolbarMenu())
    End Sub

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        'Release COM objects 
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents cmdAddSubMenu As System.Windows.Forms.Button
    Public WithEvents cmdAddMenu As System.Windows.Forms.Button
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolbarMenu))
        Me.cmdAddSubMenu = New System.Windows.Forms.Button
        Me.cmdAddMenu = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.Label5 = New System.Windows.Forms.Label
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdAddSubMenu
        '
        Me.cmdAddSubMenu.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAddSubMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAddSubMenu.Enabled = False
        Me.cmdAddSubMenu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddSubMenu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddSubMenu.Location = New System.Drawing.Point(419, 240)
        Me.cmdAddSubMenu.Name = "cmdAddSubMenu"
        Me.cmdAddSubMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAddSubMenu.Size = New System.Drawing.Size(97, 33)
        Me.cmdAddSubMenu.TabIndex = 6
        Me.cmdAddSubMenu.Text = "Add Sub Menu"
        Me.cmdAddSubMenu.UseVisualStyleBackColor = False
        '
        'cmdAddMenu
        '
        Me.cmdAddMenu.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAddMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAddMenu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddMenu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddMenu.Location = New System.Drawing.Point(417, 157)
        Me.cmdAddMenu.Name = "cmdAddMenu"
        Me.cmdAddMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAddMenu.Size = New System.Drawing.Size(97, 33)
        Me.cmdAddMenu.TabIndex = 5
        Me.cmdAddMenu.Text = "Add Menu"
        Me.cmdAddMenu.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(416, 205)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(137, 41)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Add a sub-menu to the Navigation menu."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(416, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(144, 33)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Browse to a map document to load into the MapControl."
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(414, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(145, 33)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Add the Navigation menu onto the ToolbarControl."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(416, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(137, 41)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Navigate around the data using the commands on the ToolbarControl."
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(560, 28)
        Me.AxToolbarControl1.TabIndex = 8
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(400, 352)
        Me.AxMapControl1.TabIndex = 9
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(24, 56)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(417, 287)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(145, 33)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Right click on the display to popup the Navigation menu"
        '
        'frmToolbarMenu
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(574, 403)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.cmdAddSubMenu)
        Me.Controls.Add(Me.cmdAddMenu)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmToolbarMenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Toolbar Menu"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private m_navigationMenu As IToolbarMenu = New ToolbarMenuClass

    Private Sub frmToolbarMenu_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxMapControl1)

        'Create UID's and add new items to the ToolbarControl
        Dim pUid As New UID
        pUid.Value = "esriControls.ControlsOpenDocCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapZoomInTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapZoomOutTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapPanTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapFullExtentCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

        'Create a MenuDef object
        Dim pMenuDef As IMenuDef
        pMenuDef = New NavigationMenu
        'Create a ToolbarMenu
        m_navigationMenu.AddItem(pMenuDef, 0, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
        'Set the Toolbarmenu's hook
        m_navigationMenu.SetHook(AxToolbarControl1.Object)
        'Set the ToolbarMenu's caption
        m_navigationMenu.Caption = "Navigation"
    End Sub

    Private Sub cmdAddMenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAddMenu.Click

        'Add to the end of the Toolbar - it will be the 6th item
        AxToolbarControl1.AddItem(m_navigationMenu, -1, -1, False, 0, esriCommandStyles.esriCommandStyleMenuBar)

        cmdAddMenu.Enabled = False
        cmdAddSubMenu.Enabled = True
    End Sub

    Private Sub cmdAddSubMenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAddSubMenu.Click

        'Create a MenuDef object
        Dim pMenuDef As IMenuDef
        pMenuDef = New ToolbarSubMenu

        'Get the menu, which is the 6th item on the toolbar (indexing from 0)
        Dim pToolbarItem As IToolbarItem
        pToolbarItem = AxToolbarControl1.GetItem(5)
        Dim pToolbarMenu As IToolbarMenu
        pToolbarMenu = pToolbarItem.Menu

        'Add the sub-menu as the third item on the Navigation menu, making it
        'start a new group
        pToolbarMenu.AddSubMenu(pMenuDef, 2, True)

        cmdAddSubMenu.Enabled = False
    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        If e.button = 2 Then
            'Popup the menu
            m_navigationMenu.PopupMenu(e.x, e.y, AxMapControl1.hWnd)
        End If
    End Sub
End Class