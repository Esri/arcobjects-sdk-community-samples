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
Imports System.IO
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Analyst3D
Imports System.Windows.Forms
Imports ESRI.ArcGIS


Public Class Effects
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

    Application.Run(New Effects())
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
  Public WithEvents TxtTipDelay As System.Windows.Forms.TextBox
  Public WithEvents cmbTipType As System.Windows.Forms.ComboBox
  Public WithEvents ChkTip As System.Windows.Forms.CheckBox
  Public WithEvents LblTips As System.Windows.Forms.Label
  Public WithEvents lblDelay As System.Windows.Forms.Label
  Public WithEvents lblLatVal As System.Windows.Forms.Label
  Public WithEvents LblLonVal As System.Windows.Forms.Label
  Public WithEvents LblAltVal As System.Windows.Forms.Label
  Public WithEvents LblLat As System.Windows.Forms.Label
  Public WithEvents LblLon As System.Windows.Forms.Label
  Public WithEvents LblALt As System.Windows.Forms.Label
  Public WithEvents CmdAmbient As System.Windows.Forms.Button
  Public WithEvents TxtAmbient As System.Windows.Forms.TextBox
  Public WithEvents Label2 As System.Windows.Forms.Label
  Public WithEvents _Frame2_0 As System.Windows.Forms.GroupBox
  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.
  'Do not modify it using the code editor.
  Public WithEvents ChkHUD As System.Windows.Forms.CheckBox
  Public WithEvents ChkArrow As System.Windows.Forms.CheckBox
  Public WithEvents Frame2_1 As System.Windows.Forms.GroupBox
  Public WithEvents Frame2 As System.Windows.Forms.GroupBox
  Public WithEvents CmdSetSun As System.Windows.Forms.Button
  Public WithEvents chkSun As System.Windows.Forms.CheckBox
  Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
  Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
  Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
  Friend WithEvents AxGlobeControl1 As ESRI.ArcGIS.Controls.AxGlobeControl
  Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Effects))
    Me.ChkHUD = New System.Windows.Forms.CheckBox
    Me.ChkArrow = New System.Windows.Forms.CheckBox
    Me.Frame2_1 = New System.Windows.Forms.GroupBox
    Me.lblDelay = New System.Windows.Forms.Label
    Me.TxtTipDelay = New System.Windows.Forms.TextBox
    Me.cmbTipType = New System.Windows.Forms.ComboBox
    Me.ChkTip = New System.Windows.Forms.CheckBox
    Me.LblTips = New System.Windows.Forms.Label
    Me.Frame2 = New System.Windows.Forms.GroupBox
    Me.lblLatVal = New System.Windows.Forms.Label
    Me.LblLonVal = New System.Windows.Forms.Label
    Me.LblAltVal = New System.Windows.Forms.Label
    Me.LblLat = New System.Windows.Forms.Label
    Me.LblLon = New System.Windows.Forms.Label
    Me.LblALt = New System.Windows.Forms.Label
    Me._Frame2_0 = New System.Windows.Forms.GroupBox
    Me.CmdAmbient = New System.Windows.Forms.Button
    Me.TxtAmbient = New System.Windows.Forms.TextBox
    Me.CmdSetSun = New System.Windows.Forms.Button
    Me.chkSun = New System.Windows.Forms.CheckBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
    Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
    Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
    Me.AxGlobeControl1 = New ESRI.ArcGIS.Controls.AxGlobeControl
    Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
    Me.Frame2_1.SuspendLayout()
    Me.Frame2.SuspendLayout()
    Me._Frame2_0.SuspendLayout()
    CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ChkHUD
    '
    Me.ChkHUD.BackColor = System.Drawing.SystemColors.Control
    Me.ChkHUD.Cursor = System.Windows.Forms.Cursors.Default
    Me.ChkHUD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ChkHUD.ForeColor = System.Drawing.SystemColors.ControlText
    Me.ChkHUD.Location = New System.Drawing.Point(13, 19)
    Me.ChkHUD.Name = "ChkHUD"
    Me.ChkHUD.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.ChkHUD.Size = New System.Drawing.Size(51, 16)
    Me.ChkHUD.TabIndex = 15
    Me.ChkHUD.Text = "HUD"
    '
    'ChkArrow
    '
    Me.ChkArrow.BackColor = System.Drawing.SystemColors.Control
    Me.ChkArrow.Cursor = System.Windows.Forms.Cursors.Default
    Me.ChkArrow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ChkArrow.ForeColor = System.Drawing.SystemColors.ControlText
    Me.ChkArrow.Location = New System.Drawing.Point(13, 33)
    Me.ChkArrow.Name = "ChkArrow"
    Me.ChkArrow.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.ChkArrow.Size = New System.Drawing.Size(59, 31)
    Me.ChkArrow.TabIndex = 14
    Me.ChkArrow.Text = "North Arrow"
    '
    'Frame2_1
    '
    Me.Frame2_1.BackColor = System.Drawing.SystemColors.Control
    Me.Frame2_1.Controls.Add(Me.lblDelay)
    Me.Frame2_1.Controls.Add(Me.TxtTipDelay)
    Me.Frame2_1.Controls.Add(Me.cmbTipType)
    Me.Frame2_1.Controls.Add(Me.ChkHUD)
    Me.Frame2_1.Controls.Add(Me.ChkArrow)
    Me.Frame2_1.Controls.Add(Me.ChkTip)
    Me.Frame2_1.Controls.Add(Me.LblTips)
    Me.Frame2_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Frame2_1.ForeColor = System.Drawing.SystemColors.ControlText
    Me.Frame2_1.Location = New System.Drawing.Point(248, 448)
    Me.Frame2_1.Name = "Frame2_1"
    Me.Frame2_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Frame2_1.Size = New System.Drawing.Size(224, 69)
    Me.Frame2_1.TabIndex = 13
    Me.Frame2_1.TabStop = False
    Me.Frame2_1.Text = "HUD"
    '
    'lblDelay
    '
    Me.lblDelay.BackColor = System.Drawing.SystemColors.Control
    Me.lblDelay.Cursor = System.Windows.Forms.Cursors.Default
    Me.lblDelay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblDelay.ForeColor = System.Drawing.SystemColors.ControlText
    Me.lblDelay.Location = New System.Drawing.Point(136, 9)
    Me.lblDelay.Name = "lblDelay"
    Me.lblDelay.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblDelay.Size = New System.Drawing.Size(72, 13)
    Me.lblDelay.TabIndex = 18
    Me.lblDelay.Text = "Delay(mSec.)"
    '
    'TxtTipDelay
    '
    Me.TxtTipDelay.AcceptsReturn = True
    Me.TxtTipDelay.AutoSize = False
    Me.TxtTipDelay.BackColor = System.Drawing.SystemColors.Window
    Me.TxtTipDelay.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.TxtTipDelay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TxtTipDelay.ForeColor = System.Drawing.SystemColors.WindowText
    Me.TxtTipDelay.Location = New System.Drawing.Point(160, 24)
    Me.TxtTipDelay.MaxLength = 0
    Me.TxtTipDelay.Name = "TxtTipDelay"
    Me.TxtTipDelay.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.TxtTipDelay.Size = New System.Drawing.Size(48, 15)
    Me.TxtTipDelay.TabIndex = 16
    Me.TxtTipDelay.Text = "500"
    '
    'cmbTipType
    '
    Me.cmbTipType.BackColor = System.Drawing.SystemColors.Window
    Me.cmbTipType.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmbTipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbTipType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmbTipType.ForeColor = System.Drawing.SystemColors.WindowText
    Me.cmbTipType.Location = New System.Drawing.Point(69, 42)
    Me.cmbTipType.Name = "cmbTipType"
    Me.cmbTipType.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.cmbTipType.Size = New System.Drawing.Size(139, 22)
    Me.cmbTipType.TabIndex = 17
    '
    'ChkTip
    '
    Me.ChkTip.BackColor = System.Drawing.SystemColors.Control
    Me.ChkTip.Cursor = System.Windows.Forms.Cursors.Default
    Me.ChkTip.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ChkTip.ForeColor = System.Drawing.SystemColors.ControlText
    Me.ChkTip.Location = New System.Drawing.Point(69, 25)
    Me.ChkTip.Name = "ChkTip"
    Me.ChkTip.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.ChkTip.Size = New System.Drawing.Size(14, 15)
    Me.ChkTip.TabIndex = 20
    Me.ChkTip.Text = "Check1"
    '
    'LblTips
    '
    Me.LblTips.BackColor = System.Drawing.SystemColors.Control
    Me.LblTips.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblTips.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblTips.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblTips.Location = New System.Drawing.Point(84, 16)
    Me.LblTips.Name = "LblTips"
    Me.LblTips.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblTips.Size = New System.Drawing.Size(58, 28)
    Me.LblTips.TabIndex = 19
    Me.LblTips.Text = "Enable Globe Tips"
    '
    'Frame2
    '
    Me.Frame2.BackColor = System.Drawing.SystemColors.Control
    Me.Frame2.Controls.Add(Me.lblLatVal)
    Me.Frame2.Controls.Add(Me.LblLonVal)
    Me.Frame2.Controls.Add(Me.LblAltVal)
    Me.Frame2.Controls.Add(Me.LblLat)
    Me.Frame2.Controls.Add(Me.LblLon)
    Me.Frame2.Controls.Add(Me.LblALt)
    Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
    Me.Frame2.Location = New System.Drawing.Point(480, 448)
    Me.Frame2.Name = "Frame2"
    Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Frame2.Size = New System.Drawing.Size(236, 69)
    Me.Frame2.TabIndex = 6
    Me.Frame2.TabStop = False
    Me.Frame2.Text = "Alternate HUD"
    '
    'lblLatVal
    '
    Me.lblLatVal.BackColor = System.Drawing.SystemColors.Control
    Me.lblLatVal.Cursor = System.Windows.Forms.Cursors.Default
    Me.lblLatVal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblLatVal.ForeColor = System.Drawing.SystemColors.ControlText
    Me.lblLatVal.Location = New System.Drawing.Point(33, 30)
    Me.lblLatVal.Name = "lblLatVal"
    Me.lblLatVal.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblLatVal.Size = New System.Drawing.Size(86, 13)
    Me.lblLatVal.TabIndex = 9
    Me.lblLatVal.Text = "lblLatVal"
    '
    'LblLonVal
    '
    Me.LblLonVal.BackColor = System.Drawing.SystemColors.Control
    Me.LblLonVal.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblLonVal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblLonVal.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblLonVal.Location = New System.Drawing.Point(33, 45)
    Me.LblLonVal.Name = "LblLonVal"
    Me.LblLonVal.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblLonVal.Size = New System.Drawing.Size(86, 13)
    Me.LblLonVal.TabIndex = 7
    Me.LblLonVal.Text = "LblLonVal"
    '
    'LblAltVal
    '
    Me.LblAltVal.BackColor = System.Drawing.SystemColors.Control
    Me.LblAltVal.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblAltVal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblAltVal.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblAltVal.Location = New System.Drawing.Point(134, 44)
    Me.LblAltVal.Name = "LblAltVal"
    Me.LblAltVal.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblAltVal.Size = New System.Drawing.Size(86, 11)
    Me.LblAltVal.TabIndex = 8
    Me.LblAltVal.Text = "LblAltVal"
    '
    'LblLat
    '
    Me.LblLat.BackColor = System.Drawing.SystemColors.Control
    Me.LblLat.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblLat.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblLat.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblLat.Location = New System.Drawing.Point(8, 32)
    Me.LblLat.Name = "LblLat"
    Me.LblLat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblLat.Size = New System.Drawing.Size(24, 15)
    Me.LblLat.TabIndex = 12
    Me.LblLat.Text = "Lat:"
    '
    'LblLon
    '
    Me.LblLon.BackColor = System.Drawing.SystemColors.Control
    Me.LblLon.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblLon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblLon.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblLon.Location = New System.Drawing.Point(9, 46)
    Me.LblLon.Name = "LblLon"
    Me.LblLon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblLon.Size = New System.Drawing.Size(27, 16)
    Me.LblLon.TabIndex = 11
    Me.LblLon.Text = "Lon:"
    '
    'LblALt
    '
    Me.LblALt.BackColor = System.Drawing.SystemColors.Control
    Me.LblALt.Cursor = System.Windows.Forms.Cursors.Default
    Me.LblALt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblALt.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LblALt.Location = New System.Drawing.Point(133, 23)
    Me.LblALt.Name = "LblALt"
    Me.LblALt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LblALt.Size = New System.Drawing.Size(83, 19)
    Me.LblALt.TabIndex = 10
    Me.LblALt.Text = "Alt (in Kms.)"
    '
    '_Frame2_0
    '
    Me._Frame2_0.BackColor = System.Drawing.SystemColors.Control
    Me._Frame2_0.Controls.Add(Me.CmdAmbient)
    Me._Frame2_0.Controls.Add(Me.TxtAmbient)
    Me._Frame2_0.Controls.Add(Me.CmdSetSun)
    Me._Frame2_0.Controls.Add(Me.chkSun)
    Me._Frame2_0.Controls.Add(Me.Label2)
    Me._Frame2_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me._Frame2_0.ForeColor = System.Drawing.SystemColors.ControlText
    Me._Frame2_0.Location = New System.Drawing.Point(4, 446)
    Me._Frame2_0.Name = "_Frame2_0"
    Me._Frame2_0.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me._Frame2_0.Size = New System.Drawing.Size(236, 69)
    Me._Frame2_0.TabIndex = 0
    Me._Frame2_0.TabStop = False
    Me._Frame2_0.Text = "Sun and Ambient Light Prop"
    '
    'CmdAmbient
    '
    Me.CmdAmbient.BackColor = System.Drawing.SystemColors.Control
    Me.CmdAmbient.Cursor = System.Windows.Forms.Cursors.Default
    Me.CmdAmbient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CmdAmbient.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CmdAmbient.Location = New System.Drawing.Point(10, 43)
    Me.CmdAmbient.Name = "CmdAmbient"
    Me.CmdAmbient.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.CmdAmbient.Size = New System.Drawing.Size(78, 22)
    Me.CmdAmbient.TabIndex = 4
    Me.CmdAmbient.Text = "Set Ambient"
    '
    'TxtAmbient
    '
    Me.TxtAmbient.AcceptsReturn = True
    Me.TxtAmbient.AutoSize = False
    Me.TxtAmbient.BackColor = System.Drawing.SystemColors.Window
    Me.TxtAmbient.Cursor = System.Windows.Forms.Cursors.IBeam
    Me.TxtAmbient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TxtAmbient.ForeColor = System.Drawing.SystemColors.WindowText
    Me.TxtAmbient.Location = New System.Drawing.Point(168, 45)
    Me.TxtAmbient.MaxLength = 0
    Me.TxtAmbient.Name = "TxtAmbient"
    Me.TxtAmbient.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.TxtAmbient.Size = New System.Drawing.Size(48, 20)
    Me.TxtAmbient.TabIndex = 3
    Me.TxtAmbient.Text = ""
    '
    'CmdSetSun
    '
    Me.CmdSetSun.BackColor = System.Drawing.SystemColors.Control
    Me.CmdSetSun.Cursor = System.Windows.Forms.Cursors.Default
    Me.CmdSetSun.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CmdSetSun.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CmdSetSun.Location = New System.Drawing.Point(144, 15)
    Me.CmdSetSun.Name = "CmdSetSun"
    Me.CmdSetSun.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.CmdSetSun.Size = New System.Drawing.Size(80, 20)
    Me.CmdSetSun.TabIndex = 2
    Me.CmdSetSun.Text = "Set Sun Color"
    '
    'chkSun
    '
    Me.chkSun.BackColor = System.Drawing.SystemColors.Control
    Me.chkSun.Cursor = System.Windows.Forms.Cursors.Default
    Me.chkSun.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.chkSun.ForeColor = System.Drawing.SystemColors.ControlText
    Me.chkSun.Location = New System.Drawing.Point(15, 17)
    Me.chkSun.Name = "chkSun"
    Me.chkSun.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.chkSun.Size = New System.Drawing.Size(81, 22)
    Me.chkSun.TabIndex = 1
    Me.chkSun.Text = "Enable Sun"
    '
    'Label2
    '
    Me.Label2.BackColor = System.Drawing.SystemColors.Control
    Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
    Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
    Me.Label2.Location = New System.Drawing.Point(96, 48)
    Me.Label2.Name = "Label2"
    Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Label2.Size = New System.Drawing.Size(64, 16)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "Values  0 -1"
    '
    'AxToolbarControl1
    '
    Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
    Me.AxToolbarControl1.Name = "AxToolbarControl1"
    Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxToolbarControl1.Size = New System.Drawing.Size(704, 28)
    Me.AxToolbarControl1.TabIndex = 14
    '
    'AxTOCControl1
    '
    Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
    Me.AxTOCControl1.Name = "AxTOCControl1"
    Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxTOCControl1.Size = New System.Drawing.Size(192, 400)
    Me.AxTOCControl1.TabIndex = 15
    '
    'AxGlobeControl1
    '
    Me.AxGlobeControl1.Location = New System.Drawing.Point(208, 40)
    Me.AxGlobeControl1.Name = "AxGlobeControl1"
    Me.AxGlobeControl1.OcxState = CType(resources.GetObject("AxGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxGlobeControl1.Size = New System.Drawing.Size(504, 400)
    Me.AxGlobeControl1.TabIndex = 16
    '
    'AxLicenseControl1
    '
    Me.AxLicenseControl1.Enabled = True
    Me.AxLicenseControl1.Location = New System.Drawing.Point(504, 48)
    Me.AxLicenseControl1.Name = "AxLicenseControl1"
    Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
    Me.AxLicenseControl1.TabIndex = 17
    '
    'frmGlbCntrl
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(722, 520)
    Me.Controls.Add(Me.AxLicenseControl1)
    Me.Controls.Add(Me.AxGlobeControl1)
    Me.Controls.Add(Me.AxTOCControl1)
    Me.Controls.Add(Me.AxToolbarControl1)
    Me.Controls.Add(Me.Frame2_1)
    Me.Controls.Add(Me.Frame2)
    Me.Controls.Add(Me._Frame2_0)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.Name = "frmGlbCntrl"
    Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Text = "GlobeControl"
    Me.Frame2_1.ResumeLayout(False)
    Me.Frame2.ResumeLayout(False)
    Me._Frame2_0.ResumeLayout(False)
    CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

  Private m_penumTips As esriGlobeTipsType
  Public WithEvents m_pglbDisplay As GlobeDisplay

  Private Sub cmbTipType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbTipType.SelectedIndexChanged

    m_penumTips = cmbTipType.SelectedIndex
    Dim sVal As Object
    If Not ChkTip.CheckState = 0 Then
      sVal = TxtTipDelay.Text
      On Error Resume Next 'handle non numeric characters...
      sVal = CInt(sVal)
      If sVal = 0 Then sVal = 500 'set it to default..miliseconds
      AxGlobeControl1.TipDelay = sVal
      AxGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid
      AxGlobeControl1.ShowGlobeTips = m_penumTips
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    End If
  End Sub

  Private Sub CmdAmbient_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAmbient.Click

    Dim sVal As String
    sVal = TxtAmbient.Text
    sVal = CSng(sVal)
    If sVal > 1.0# Then sVal = 1
    If sVal < 0.0# Then sVal = 0
    Dim pglbDispRend As IGlobeDisplayRendering
    pglbDispRend = AxGlobeControl1.GlobeDisplay
    pglbDispRend.AmbientLight = sVal
    'update textbox
    TxtAmbient.Text = sVal
    AxGlobeControl1.GlobeDisplay.RefreshViewers()

  End Sub

  Private Sub CmdSetSun_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSetSun.Click

    Dim pCmDRgb As New RgbColorClass

    If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
    pCmDRgb.Red = ColorDialog1.Color.R
    pCmDRgb.Blue = ColorDialog1.Color.B
    pCmDRgb.Green = ColorDialog1.Color.G

    ChangeIllumination(pCmDRgb)

  End Sub

  Private Sub frmGlbCntrl_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load


    'relative file path to the sample data from project location
    Dim sGlbData As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    sGlbData = Path.Combine(sGlbData, "ArcGIS\data\Globe\World Imagery.3dd")
    Dim filePath as DirectoryInfo = New DirectoryInfo(sGlbData)
    System.Diagnostics.Debug.WriteLine(String.Format("File path for data root: {0} [{1}]", filePath.FullName, Directory.GetCurrentDirectory()))
    If (not System.IO.File.Exists(sGlbData)) Then Throw New Exception(String.Format("Fix code to point to your sample data: {0} [{1}] was not found", filePath.FullName, Directory.GetCurrentDirectory()))
    If AxGlobeControl1.Check3dFile(sGlbData) Then AxGlobeControl1.Load3dFile(sGlbData)
    
    'Enable north arrow, HUD and GlobeTips..
    Dim bChkArrow As Boolean, bHUD As Boolean
    bChkArrow = AxGlobeControl1.GlobeViewer.NorthArrowEnabled
    bHUD = AxGlobeControl1.GlobeViewer.HUDEnabled
    ChkHUD.Checked = bHUD
    ChkArrow.Checked = bChkArrow
    'get the state of globetips from the loaded doc.....
    m_penumTips = AxGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips
    'if no tip value (not set) in the loaded doc set it to default..
    If m_penumTips <= 0 Then
      m_penumTips = esriGlobeTipsType.esriGlobeTipsTypeLatLon
    End If
    cmbTipType.Items.Insert(0, "esriGlobeTipsTypeNone")
    cmbTipType.Items.Insert(1, "esriGlobeTipsTypeLatLon")
    cmbTipType.Items.Insert(2, "esriGlobeTipsTypeElevation")
    cmbTipType.Items.Insert(3, "esriGlobeTipsTypeLatLonElevation")

    ChkTip.Checked = True 'tip value of the doc...
    'set the list...
    cmbTipType.SelectedIndex = m_penumTips

    'populate tip type values..
    AxGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid
    AxGlobeControl1.TipDelay = 500 'default..
    AxGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips = m_penumTips
    AxGlobeControl1.GlobeDisplay.RefreshViewers()

    'Get current sun property..
    Dim pglbDispRend As IGlobeDisplayRendering
    pglbDispRend = AxGlobeControl1.GlobeDisplay
    Dim bsun As Boolean
    bsun = pglbDispRend.IsSunEnabled
    If bsun = True Then chkSun.Checked = True 'checked
    'Get Ambient light...
    TxtAmbient.Text = CStr(pglbDispRend.AmbientLight)
    'Listen to events..
    m_pglbDisplay = AxGlobeControl1.GlobeDisplay

  End Sub

  Private Sub ChangeIllumination(ByRef prgb As RgbColorClass)

    Dim pglbDispRend As IGlobeDisplayRendering
    pglbDispRend = AxGlobeControl1.GlobeDisplay

    Dim platitude As Double, plongitude As Double
    Dim pSunred As Single, pSungreen As Single, pSunblue As Single

    If pglbDispRend.IsSunEnabled = True And chkSun.Checked = True Then
      'get the Default position and color...
      pglbDispRend.GetSunPosition(platitude, plongitude)
      pglbDispRend.GetSunColor(pSunred, pSungreen, pSunblue)
      'Set AmbientLght
      Dim sVal As String
      sVal = TxtAmbient.Text
      sVal = CSng(sVal)
      If sVal > 1 Then sVal = 1
      If sVal < 0 Then sVal = 0
      pglbDispRend.AmbientLight = sVal
      'update textbox
      TxtAmbient.Text = sVal

      Dim pAmbientLght As Single
      pAmbientLght = pglbDispRend.AmbientLight

      Dim pIcolor As IColor
      pIcolor = prgb

      Dim pglbDisp As IGlobeDisplay
      pglbDisp = EnableSetSun(pAmbientLght, platitude, plongitude, pIcolor)
      AxGlobeControl1.GlobeDisplay = pglbDisp
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    End If

  End Sub

  Private Function EnableSetSun(ByRef pAmbientLght As Single, ByRef platitude As Double, ByRef plongitude As Double, ByRef pColor As IColor) As IGlobeDisplay

    Dim pRgbColor As IRgbColor
    pRgbColor = New RgbColorClass
    pRgbColor.RGB = System.Convert.ToInt32(pColor.RGB)

    Dim pSunred As Single, pSungreen As Single, pSunblue As Single
    pSunred = CSng(pRgbColor.Red)
    pSungreen = CSng(pRgbColor.Green)
    pSunblue = CSng(pRgbColor.Blue)

    Dim pglbDispRend As IGlobeDisplayRendering
    pglbDispRend = AxGlobeControl1.GlobeDisplay

    pglbDispRend.SetSunColor(pSunred, pSungreen, pSunblue)
    pglbDispRend.SetSunPosition(platitude, plongitude)
    pglbDispRend.AmbientLight = pAmbientLght
    EnableSetSun = AxGlobeControl1.GlobeDisplay

  End Function

  Public Sub GetObserVerLatLong(ByRef pViewer As ESRI.ArcGIS.Analyst3D.ISceneViewer, ByRef pLatDD As Double, ByRef pLonDD As Double, ByRef pAltKms As Double, ByRef pRoll As Double, ByRef pIncl As Double)

    Dim pCam As IGlobeCamera
    pCam = pViewer.Camera

    pCam.GetObserverLatLonAlt(pLatDD, pLonDD, pAltKms)
    Dim pIcam As ICamera
    pIcam = pCam
    pRoll = pIcam.RollAngle
    pIncl = pIcam.Inclination

  End Sub

  Public Sub UpdateCustomHUD(ByRef pLatDD As Double, ByRef pLonDD As Double, ByRef pAltKms As Double, ByRef pRoll As Double, ByRef pIncl As Double)

    LblAltVal.Text = pAltKms.ToString
    lblLatVal.Text = pLatDD.ToString
    LblLonVal.Text = pLonDD.ToString

  End Sub

  Private Sub m_pglbDisplay_AfterDraw(ByVal pViewer As ESRI.ArcGIS.Analyst3D.ISceneViewer) Handles m_pglbDisplay.AfterDraw
    Dim pLatDD As Double, pLonDD As Double, pAltKms As Double, pRoll As Double, pIncl As Double
    GetObserVerLatLong(pViewer, pLatDD, pLonDD, pAltKms, pRoll, pIncl)
    UpdateCustomHUD(pLatDD, pLonDD, pAltKms, pRoll, pIncl)
  End Sub

  Private Sub ChkArrow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkArrow.CheckedChanged
    Dim bChkArrow As Boolean
    bChkArrow = AxGlobeControl1.GlobeViewer.NorthArrowEnabled
    If ChkArrow.Checked = False And bChkArrow = True Then
      AxGlobeControl1.GlobeViewer.NorthArrowEnabled = False 'unchecked
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    ElseIf ChkArrow.Checked = True And bChkArrow = False Then
      AxGlobeControl1.GlobeViewer.NorthArrowEnabled = True 'checked
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    End If
  End Sub

  Private Sub ChkHUD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkHUD.CheckedChanged
    'Default HUD
    Dim bHUD As Boolean
    bHUD = AxGlobeControl1.GlobeViewer.HUDEnabled
    If ChkHUD.Checked = False And bHUD = True Then
      AxGlobeControl1.GlobeViewer.HUDEnabled = False 'unchecked
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    ElseIf ChkHUD.Checked = True And bHUD = False Then
      AxGlobeControl1.GlobeViewer.HUDEnabled = True 'checked
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    End If
  End Sub

  Private Sub chkSun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSun.CheckedChanged
    Dim pglbDispRend As IGlobeDisplayRendering
    pglbDispRend = AxGlobeControl1.GlobeDisplay
    Dim bsun As Boolean
    bsun = pglbDispRend.IsSunEnabled
    If chkSun.Checked = False And bsun = True Then
      pglbDispRend.IsSunEnabled = False 'unchecked
      CmdSetSun.Enabled = False
    ElseIf chkSun.Checked = True And bsun = False Then
      pglbDispRend.IsSunEnabled = True 'checked
      CmdSetSun.Enabled = True
    End If
  End Sub

  Private Sub ChkTip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTip.CheckedChanged

    If ChkTip.Checked = False Then
      AxGlobeControl1.ShowGlobeTips = esriGlobeTipsType.esriGlobeTipsTypeNone
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
      cmbTipType.Enabled = False
      TxtTipDelay.Enabled = False
    Else
      Dim sVal As String
      cmbTipType.Enabled = True
      TxtTipDelay.Enabled = True
      sVal = TxtTipDelay.Text
      If sVal = 0 Then sVal = 500 'set it to default..miliseconds
      If cmbTipType.SelectedIndex >= 0 Then m_penumTips = cmbTipType.SelectedIndex
      AxGlobeControl1.TipDelay = sVal
      AxGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid
      AxGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips = m_penumTips
      AxGlobeControl1.GlobeDisplay.RefreshViewers()
    End If

  End Sub

  Private Sub AxGlobeControl1_OnGlobeReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IGlobeControlEvents_OnGlobeReplacedEvent) Handles AxGlobeControl1.OnGlobeReplaced
    Dim pglbbDispRend As IGlobeDisplayRendering
    pglbbDispRend = AxGlobeControl1.GlobeDisplay
    Dim bsun As Boolean
    bsun = pglbbDispRend.IsSunEnabled
    If bsun = True Then chkSun.Checked = True 'checked
    'get the state of globetips from the loaded doc.....
    m_penumTips = AxGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips
  End Sub
End Class