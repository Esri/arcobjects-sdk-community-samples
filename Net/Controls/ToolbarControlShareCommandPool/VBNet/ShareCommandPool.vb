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
Imports ESRI.ArcGIS.RuntimeManager
Imports ESRI.ArcGIS

Public Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
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
    Public WithEvents lstCommandPool2 As System.Windows.Forms.ListBox
    Public WithEvents lstCommandPool1 As System.Windows.Forms.ListBox
    Public WithEvents chkShare As System.Windows.Forms.CheckBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxToolbarControl2 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.lstCommandPool2 = New System.Windows.Forms.ListBox
        Me.lstCommandPool1 = New System.Windows.Forms.ListBox
        Me.chkShare = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxToolbarControl2 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstCommandPool2
        '
        Me.lstCommandPool2.BackColor = System.Drawing.SystemColors.Window
        Me.lstCommandPool2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCommandPool2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCommandPool2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCommandPool2.ItemHeight = 14
        Me.lstCommandPool2.Location = New System.Drawing.Point(304, 312)
        Me.lstCommandPool2.Name = "lstCommandPool2"
        Me.lstCommandPool2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCommandPool2.Size = New System.Drawing.Size(281, 88)
        Me.lstCommandPool2.TabIndex = 9
        '
        'lstCommandPool1
        '
        Me.lstCommandPool1.BackColor = System.Drawing.SystemColors.Window
        Me.lstCommandPool1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCommandPool1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCommandPool1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCommandPool1.ItemHeight = 14
        Me.lstCommandPool1.Location = New System.Drawing.Point(8, 312)
        Me.lstCommandPool1.Name = "lstCommandPool1"
        Me.lstCommandPool1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCommandPool1.Size = New System.Drawing.Size(281, 88)
        Me.lstCommandPool1.TabIndex = 8
        '
        'chkShare
        '
        Me.chkShare.BackColor = System.Drawing.SystemColors.Control
        Me.chkShare.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShare.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShare.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShare.Location = New System.Drawing.Point(400, 32)
        Me.chkShare.Name = "chkShare"
        Me.chkShare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShare.Size = New System.Drawing.Size(129, 25)
        Me.chkShare.TabIndex = 6
        Me.chkShare.Text = "Share CommandPool"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(304, 296)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(193, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "CommandPool2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 296)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(193, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "CommandPool1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(392, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(193, 49)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "2) Select a ZoomIn, ZoomOut or Pan tool. Notice that only one tool is depressed. " & _
        ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(392, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(193, 33)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "1) Browse to a map document to load into the MapControl."
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(392, 168)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(193, 49)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "3) Share the same CommandPool between both ToolbarControls. Notice that the Usage" & _
        "Count changes."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(392, 216)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(193, 57)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "4) Select a ZoomIn, ZoomOut or Pan tool. Notice that the same tool on both Toolba" & _
        "rControls becomes depressed."
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(376, 28)
        Me.AxToolbarControl1.TabIndex = 12
        '
        'AxToolbarControl2
        '
        Me.AxToolbarControl2.Location = New System.Drawing.Point(8, 40)
        Me.AxToolbarControl2.Name = "AxToolbarControl2"
        Me.AxToolbarControl2.OcxState = CType(resources.GetObject("AxToolbarControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl2.Size = New System.Drawing.Size(376, 28)
        Me.AxToolbarControl2.TabIndex = 13
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 72)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(376, 216)
        Me.AxMapControl1.TabIndex = 14
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(24, 80)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 15
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(599, 425)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.AxToolbarControl2)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.lstCommandPool2)
        Me.Controls.Add(Me.lstCommandPool1)
        Me.Controls.Add(Me.chkShare)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "ShareCommandPool"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Dim m_pCommandPool1 As ICommandPool
    Dim m_pCommandPool2 As ICommandPool

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Set the Buddy property
        AxToolbarControl1.SetBuddyControl(AxMapControl1)
        AxToolbarControl2.SetBuddyControl(AxMapControl1)

        'Add items to the ToolbarControls
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

        AxToolbarControl2.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl2.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl2.AddItem("esriControls.ControlsMapPanTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

        'Get the CommandPool of ToolbarControl's
        m_pCommandPool1 = AxToolbarControl1.CommandPool
        m_pCommandPool2 = AxToolbarControl2.CommandPool

        UpdateUsageCount()

    End Sub

    Public Sub UpdateUsageCount()

        Dim i As Short
        Dim pCommand As ICommand

        'Clear list boxes
        lstCommandPool1.Items.Clear()
        lstCommandPool2.Items.Clear()

        lstCommandPool1.Items.Add("UsageCount" & Chr(9) & "Name")
        'Loop through each command in CommandPool1 to get its UsageCount
        For i = 0 To m_pCommandPool1.Count - 1
            pCommand = m_pCommandPool1.Command(i)
            lstCommandPool1.Items.Add(m_pCommandPool1.UsageCount(pCommand) & Chr(9) & pCommand.Name)
        Next i

        lstCommandPool2.Items.Add("UsageCount" & Chr(9) & "Name")
        'Loop through each command in CommandPool2 to get its UsageCount
        For i = 0 To m_pCommandPool2.Count - 1
            pCommand = m_pCommandPool2.Command(i)
            lstCommandPool2.Items.Add(m_pCommandPool2.UsageCount(pCommand) & Chr(9) & pCommand.Name)
        Next i

    End Sub

    Private Sub chkShare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShare.Click
        If chkShare.CheckState = 1 Then
            'Share the same CommandPool between both ToolbarControl's
            AxToolbarControl2.CommandPool = m_pCommandPool1
        Else
            'Do not share the same CommandPool between both ToolbarControl's
            AxToolbarControl2.CommandPool = m_pCommandPool2
        End If

        UpdateUsageCount()
    End Sub

End Class