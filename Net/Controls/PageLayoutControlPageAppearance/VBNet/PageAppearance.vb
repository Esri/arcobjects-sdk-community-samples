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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS


Public Class Form1
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

        Application.Run(New Form1())
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
    Public WithEvents txbPath As System.Windows.Forms.TextBox
    Public WithEvents cmdLoadDocument As System.Windows.Forms.Button
    Public WithEvents chkShowPrintableArea As System.Windows.Forms.CheckBox
    Public WithEvents cmdReset As System.Windows.Forms.Button
    Public WithEvents optIPropertySupport As System.Windows.Forms.RadioButton
    Public WithEvents optIFrameProperties As System.Windows.Forms.RadioButton
    Public WithEvents optIPage As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdZoomPage As System.Windows.Forms.Button
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxSymbologyControl2 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxSymbologyControl3 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxSymbologyControl4 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.txbPath = New System.Windows.Forms.TextBox
        Me.cmdLoadDocument = New System.Windows.Forms.Button
        Me.chkShowPrintableArea = New System.Windows.Forms.CheckBox
        Me.cmdReset = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.optIPropertySupport = New System.Windows.Forms.RadioButton
        Me.optIFrameProperties = New System.Windows.Forms.RadioButton
        Me.optIPage = New System.Windows.Forms.RadioButton
        Me.cmdZoomPage = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.AxSymbologyControl2 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.AxSymbologyControl3 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.AxSymbologyControl4 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.Frame1.SuspendLayout()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxSymbologyControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxSymbologyControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxSymbologyControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txbPath
        '
        Me.txbPath.AcceptsReturn = True
        Me.txbPath.BackColor = System.Drawing.SystemColors.Window
        Me.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbPath.Enabled = False
        Me.txbPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbPath.Location = New System.Drawing.Point(136, 8)
        Me.txbPath.MaxLength = 0
        Me.txbPath.Name = "txbPath"
        Me.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbPath.Size = New System.Drawing.Size(489, 20)
        Me.txbPath.TabIndex = 18
        '
        'cmdLoadDocument
        '
        Me.cmdLoadDocument.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadDocument.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadDocument.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadDocument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadDocument.Location = New System.Drawing.Point(8, 8)
        Me.cmdLoadDocument.Name = "cmdLoadDocument"
        Me.cmdLoadDocument.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadDocument.Size = New System.Drawing.Size(121, 25)
        Me.cmdLoadDocument.TabIndex = 17
        Me.cmdLoadDocument.Text = "Load Map Document"
        Me.cmdLoadDocument.UseVisualStyleBackColor = False
        '
        'chkShowPrintableArea
        '
        Me.chkShowPrintableArea.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowPrintableArea.Checked = True
        Me.chkShowPrintableArea.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowPrintableArea.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowPrintableArea.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowPrintableArea.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowPrintableArea.Location = New System.Drawing.Point(335, 362)
        Me.chkShowPrintableArea.Name = "chkShowPrintableArea"
        Me.chkShowPrintableArea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowPrintableArea.Size = New System.Drawing.Size(128, 17)
        Me.chkShowPrintableArea.TabIndex = 16
        Me.chkShowPrintableArea.Text = "Show Printable Area"
        Me.chkShowPrintableArea.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReset.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(240, 362)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReset.Size = New System.Drawing.Size(89, 25)
        Me.cmdReset.TabIndex = 15
        Me.cmdReset.Text = "Reset Page"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optIPropertySupport)
        Me.Frame1.Controls.Add(Me.optIFrameProperties)
        Me.Frame1.Controls.Add(Me.optIPage)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(496, 359)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(129, 81)
        Me.Frame1.TabIndex = 10
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "How to apply symbol"
        '
        'optIPropertySupport
        '
        Me.optIPropertySupport.BackColor = System.Drawing.SystemColors.Control
        Me.optIPropertySupport.Cursor = System.Windows.Forms.Cursors.Default
        Me.optIPropertySupport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optIPropertySupport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIPropertySupport.Location = New System.Drawing.Point(16, 56)
        Me.optIPropertySupport.Name = "optIPropertySupport"
        Me.optIPropertySupport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optIPropertySupport.Size = New System.Drawing.Size(105, 17)
        Me.optIPropertySupport.TabIndex = 13
        Me.optIPropertySupport.TabStop = True
        Me.optIPropertySupport.Text = "IPropertySupport"
        Me.optIPropertySupport.UseVisualStyleBackColor = False
        '
        'optIFrameProperties
        '
        Me.optIFrameProperties.BackColor = System.Drawing.SystemColors.Control
        Me.optIFrameProperties.Cursor = System.Windows.Forms.Cursors.Default
        Me.optIFrameProperties.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optIFrameProperties.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIFrameProperties.Location = New System.Drawing.Point(16, 36)
        Me.optIFrameProperties.Name = "optIFrameProperties"
        Me.optIFrameProperties.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optIFrameProperties.Size = New System.Drawing.Size(105, 17)
        Me.optIFrameProperties.TabIndex = 12
        Me.optIFrameProperties.TabStop = True
        Me.optIFrameProperties.Text = "IFrameProperties"
        Me.optIFrameProperties.UseVisualStyleBackColor = False
        '
        'optIPage
        '
        Me.optIPage.BackColor = System.Drawing.SystemColors.Control
        Me.optIPage.Checked = True
        Me.optIPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.optIPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optIPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIPage.Location = New System.Drawing.Point(16, 16)
        Me.optIPage.Name = "optIPage"
        Me.optIPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optIPage.Size = New System.Drawing.Size(105, 17)
        Me.optIPage.TabIndex = 11
        Me.optIPage.TabStop = True
        Me.optIPage.Text = "IPage "
        Me.optIPage.UseVisualStyleBackColor = False
        '
        'cmdZoomPage
        '
        Me.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdZoomPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdZoomPage.Location = New System.Drawing.Point(240, 394)
        Me.cmdZoomPage.Name = "cmdZoomPage"
        Me.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdZoomPage.Size = New System.Drawing.Size(89, 25)
        Me.cmdZoomPage.TabIndex = 1
        Me.cmdZoomPage.Text = "Zoom to Page"
        Me.cmdZoomPage.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(332, 403)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(137, 17)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Right mouse button to pan."
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(332, 386)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(153, 17)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Left mouse button to zoom in."
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(240, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(377, 17)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Symbols from Style Classes (double click a on a symbol to apply it to the page):"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(528, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(97, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Shadow"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(432, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Colors"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(336, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(97, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Backgrounds"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(240, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(97, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Borders"
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(7, 88)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(224, 331)
        Me.AxPageLayoutControl1.TabIndex = 21
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(192, 160)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 22
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(237, 88)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(92, 265)
        Me.AxSymbologyControl1.TabIndex = 23
        '
        'AxSymbologyControl2
        '
        Me.AxSymbologyControl2.Location = New System.Drawing.Point(335, 88)
        Me.AxSymbologyControl2.Name = "AxSymbologyControl2"
        Me.AxSymbologyControl2.OcxState = CType(resources.GetObject("AxSymbologyControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl2.Size = New System.Drawing.Size(91, 265)
        Me.AxSymbologyControl2.TabIndex = 24
        '
        'AxSymbologyControl3
        '
        Me.AxSymbologyControl3.Location = New System.Drawing.Point(431, 88)
        Me.AxSymbologyControl3.Name = "AxSymbologyControl3"
        Me.AxSymbologyControl3.OcxState = CType(resources.GetObject("AxSymbologyControl3.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl3.Size = New System.Drawing.Size(93, 265)
        Me.AxSymbologyControl3.TabIndex = 25
        '
        'AxSymbologyControl4
        '
        Me.AxSymbologyControl4.Location = New System.Drawing.Point(532, 88)
        Me.AxSymbologyControl4.Name = "AxSymbologyControl4"
        Me.AxSymbologyControl4.OcxState = CType(resources.GetObject("AxSymbologyControl4.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl4.Size = New System.Drawing.Size(93, 265)
        Me.AxSymbologyControl4.TabIndex = 26
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(635, 450)
        Me.Controls.Add(Me.AxSymbologyControl4)
        Me.Controls.Add(Me.AxSymbologyControl3)
        Me.Controls.Add(Me.AxSymbologyControl2)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.txbPath)
        Me.Controls.Add(Me.cmdLoadDocument)
        Me.Controls.Add(Me.chkShowPrintableArea)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdZoomPage)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Set Page Appearance"
        Me.Frame1.ResumeLayout(False)
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxSymbologyControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxSymbologyControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxSymbologyControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region


    Private Sub cmdLoadDocument_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadDocument.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Browse Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Validate and load the Mx document
        If AxPageLayoutControl1.CheckMxFile(sFilePath) Then
            AxPageLayoutControl1.LoadMxFile((sFilePath))
            txbPath.Text = sFilePath
        Else
            MsgBox(sFilePath & " is not a valid ArcMap document")
        End If

    End Sub

    Private Sub cmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click
        'Replace the PageLayout object to reset all the changed values
        AxPageLayoutControl1.PageLayout = New PageLayout
        chkShowPrintableArea.CheckState = CheckState.Checked
    End Sub

    Private Sub cmdZoomPage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdZoomPage.Click
        'Zoom to page
        AxPageLayoutControl1.ZoomToWholePage()
    End Sub

    Private Sub chkShowPrintableArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowPrintableArea.Click
        'Toggle whether the printable area is visible
        AxPageLayoutControl1.Page.IsPrintableAreaVisible = (chkShowPrintableArea.CheckState = System.Windows.Forms.CheckState.Checked)
    End Sub

    Private Overloads Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown
        'Zoom in or pan
        If e.button = 1 Then
            AxPageLayoutControl1.Extent = AxPageLayoutControl1.TrackRectangle
        ElseIf e.button = 2 Then
            AxPageLayoutControl1.Pan()
        End If
    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Get the ArcGIS install location by opening the subkey for reading
        Dim installationFolder As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
        'Load the ESRI.ServerStyle file into the SymbologyControl's
        AxSymbologyControl1.LoadStyleFile(installationFolder & "\\Styles\\ESRI.ServerStyle")
        AxSymbologyControl2.LoadStyleFile(installationFolder & "\\Styles\\ESRI.ServerStyle")
        AxSymbologyControl3.LoadStyleFile(installationFolder & "\\Styles\\ESRI.ServerStyle")
        AxSymbologyControl4.LoadStyleFile(installationFolder & "\\Styles\\ESRI.ServerStyle")
        'Set the SymbologyStyleClass
        AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders
        AxSymbologyControl2.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds
        AxSymbologyControl3.StyleClass = esriSymbologyStyleClass.esriStyleClassColors
        AxSymbologyControl4.StyleClass = esriSymbologyStyleClass.esriStyleClassShadows
        'Set the SymbologyControl's display style
        AxSymbologyControl1.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList
        AxSymbologyControl2.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList
        AxSymbologyControl3.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList
        AxSymbologyControl4.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList

    End Sub

    Private Sub AxSymbologyControl1_OnDoubleClick(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent) Handles AxSymbologyControl1.OnDoubleClick
        'Update symbols with selected symbol
        UpdateSymbol(AxSymbologyControl1.GetStyleClass(AxSymbologyControl1.StyleClass).GetSelectedItem())
    End Sub

    Private Sub AxSymbologyControl2_OnDoubleClick(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent) Handles AxSymbologyControl2.OnDoubleClick
        'Update symbols with selected symbol
        UpdateSymbol(AxSymbologyControl2.GetStyleClass(AxSymbologyControl2.StyleClass).GetSelectedItem())
    End Sub

    Private Sub AxSymbologyControl3_OnDoubleClick(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent) Handles AxSymbologyControl3.OnDoubleClick
        'Update symbols with selected symbol
        UpdateSymbol(AxSymbologyControl3.GetStyleClass(AxSymbologyControl3.StyleClass).GetSelectedItem())
    End Sub

    Private Sub AxSymbologyControl4_OnDoubleClick(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnDoubleClickEvent) Handles AxSymbologyControl4.OnDoubleClick
        'Update symbols with selected symbol
        UpdateSymbol(AxSymbologyControl4.GetStyleClass(AxSymbologyControl4.StyleClass).GetSelectedItem())
    End Sub

    Private Sub UpdateSymbol(ByVal styleGalleryItem As IStyleGalleryItem)

        'Get IPage interface
        Dim pPage As IPage = AxPageLayoutControl1.Page

        'Apply the symbol as a property to the page
        If optIPropertySupport.Checked Then
            'Query interface for IPropertySupport
            Dim pPropertySupport As IPropertySupport = pPage
            'If the symbol can be applied
            If pPropertySupport.CanApply(styleGalleryItem.Item) Then
                'Apply the object
                pPropertySupport.Apply(styleGalleryItem.Item)
            Else
                MessageBox.Show("Unable to apply this symbol!")
            End If
        End If

        'Apply the symbol as an IFrameProperties property
        If optIFrameProperties.Checked Then
            'Query interface for IFrameProperties
            Dim pFrameProperties As IFrameProperties = pPage
            If TypeOf styleGalleryItem.Item Is IBorder Then
                'Set the frame's border
                pFrameProperties.Border = styleGalleryItem.Item
            ElseIf TypeOf styleGalleryItem.Item Is IBackground Then
                'Set the frame's background
                pFrameProperties.Background = styleGalleryItem.Item
            ElseIf TypeOf styleGalleryItem.Item Is IColor Then
                MessageBox.Show("There is no color property on IFrameProperties!")
            ElseIf TypeOf styleGalleryItem.Item Is IShadow Then
                'Set the frame's shadow
                pFrameProperties.Shadow = styleGalleryItem.Item
            End If
        End If

        'Apply the symbol as an IPage property
        If optIPage.Checked Then
            If TypeOf styleGalleryItem.Item Is IBorder Then
                'Set the frame's border
                pPage.Border = styleGalleryItem.Item
            ElseIf TypeOf styleGalleryItem.Item Is IBackground Then
                'Set the frame's background
                pPage.Background = styleGalleryItem.Item
            ElseIf TypeOf styleGalleryItem.Item Is IColor Then
                pPage.BackgroundColor = styleGalleryItem.Item
            ElseIf TypeOf styleGalleryItem.Item Is IShadow Then
                MessageBox.Show("There is no shadow property on IPage!")
            End If
        End If

        'Refresh
        AxPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewBackground, Nothing, Nothing)
    End Sub

End Class