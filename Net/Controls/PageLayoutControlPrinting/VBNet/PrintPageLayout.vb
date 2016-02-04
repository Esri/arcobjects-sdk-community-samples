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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Output
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
    Public WithEvents cmdLoadMxFile As System.Windows.Forms.Button
    Public WithEvents txbMxFilePath As System.Windows.Forms.TextBox
    Public WithEvents txbOverlap As System.Windows.Forms.TextBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents txbStartPage As System.Windows.Forms.TextBox
    Public WithEvents txbEndPage As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents fraPrint As System.Windows.Forms.GroupBox
    Public WithEvents lblPrinterOrientation As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblPrinterName As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblPrinterSize As System.Windows.Forms.Label
    Public WithEvents lblPdcdcrinter As System.Windows.Forms.Label
    Public WithEvents fraPrinter As System.Windows.Forms.GroupBox
    Public WithEvents optLandscape As System.Windows.Forms.RadioButton
    Public WithEvents optPortrait As System.Windows.Forms.RadioButton
    Public WithEvents cboPageToPrinterMapping As System.Windows.Forms.ComboBox
    Public WithEvents cboPageSize As System.Windows.Forms.ComboBox
    Public WithEvents lblPageCount As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents Line2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdLoadMxFile = New System.Windows.Forms.Button
        Me.txbMxFilePath = New System.Windows.Forms.TextBox
        Me.fraPrint = New System.Windows.Forms.GroupBox
        Me.txbOverlap = New System.Windows.Forms.TextBox
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.txbStartPage = New System.Windows.Forms.TextBox
        Me.txbEndPage = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.fraPrinter = New System.Windows.Forms.GroupBox
        Me.lblPrinterOrientation = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblPrinterName = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblPrinterSize = New System.Windows.Forms.Label
        Me.lblPdcdcrinter = New System.Windows.Forms.Label
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.optLandscape = New System.Windows.Forms.RadioButton
        Me.optPortrait = New System.Windows.Forms.RadioButton
        Me.cboPageToPrinterMapping = New System.Windows.Forms.ComboBox
        Me.cboPageSize = New System.Windows.Forms.ComboBox
        Me.lblPageCount = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Line2 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.Label3 = New System.Windows.Forms.Label
        Me.fraPrint.SuspendLayout()
        Me.fraPrinter.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdLoadMxFile
        '
        Me.cmdLoadMxFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadMxFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadMxFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadMxFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadMxFile.Location = New System.Drawing.Point(494, 6)
        Me.cmdLoadMxFile.Name = "cmdLoadMxFile"
        Me.cmdLoadMxFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadMxFile.Size = New System.Drawing.Size(136, 31)
        Me.cmdLoadMxFile.TabIndex = 15
        Me.cmdLoadMxFile.Text = "Load Mx Document"
        Me.cmdLoadMxFile.UseVisualStyleBackColor = False
        '
        'txbMxFilePath
        '
        Me.txbMxFilePath.AcceptsReturn = True
        Me.txbMxFilePath.BackColor = System.Drawing.SystemColors.Window
        Me.txbMxFilePath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbMxFilePath.Enabled = False
        Me.txbMxFilePath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbMxFilePath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbMxFilePath.Location = New System.Drawing.Point(10, 14)
        Me.txbMxFilePath.MaxLength = 0
        Me.txbMxFilePath.Name = "txbMxFilePath"
        Me.txbMxFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbMxFilePath.Size = New System.Drawing.Size(478, 23)
        Me.txbMxFilePath.TabIndex = 14
        '
        'fraPrint
        '
        Me.fraPrint.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrint.Controls.Add(Me.txbOverlap)
        Me.fraPrint.Controls.Add(Me.cmdPrint)
        Me.fraPrint.Controls.Add(Me.txbStartPage)
        Me.fraPrint.Controls.Add(Me.txbEndPage)
        Me.fraPrint.Controls.Add(Me.Label5)
        Me.fraPrint.Controls.Add(Me.Label1)
        Me.fraPrint.Controls.Add(Me.Label2)
        Me.fraPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrint.Location = New System.Drawing.Point(393, 481)
        Me.fraPrint.Name = "fraPrint"
        Me.fraPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrint.Size = New System.Drawing.Size(304, 129)
        Me.fraPrint.TabIndex = 5
        Me.fraPrint.TabStop = False
        Me.fraPrint.Text = "Print"
        '
        'txbOverlap
        '
        Me.txbOverlap.AcceptsReturn = True
        Me.txbOverlap.BackColor = System.Drawing.SystemColors.Window
        Me.txbOverlap.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbOverlap.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbOverlap.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbOverlap.Location = New System.Drawing.Point(221, 30)
        Me.txbOverlap.MaxLength = 0
        Me.txbOverlap.Name = "txbOverlap"
        Me.txbOverlap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbOverlap.Size = New System.Drawing.Size(68, 23)
        Me.txbOverlap.TabIndex = 9
        Me.txbOverlap.Text = "0"
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(10, 89)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(288, 30)
        Me.cmdPrint.TabIndex = 8
        Me.cmdPrint.Text = "Print Page Layout"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'txbStartPage
        '
        Me.txbStartPage.AcceptsReturn = True
        Me.txbStartPage.BackColor = System.Drawing.SystemColors.Window
        Me.txbStartPage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbStartPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbStartPage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbStartPage.Location = New System.Drawing.Point(85, 59)
        Me.txbStartPage.MaxLength = 0
        Me.txbStartPage.Name = "txbStartPage"
        Me.txbStartPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbStartPage.Size = New System.Drawing.Size(58, 23)
        Me.txbStartPage.TabIndex = 7
        Me.txbStartPage.Text = "1"
        '
        'txbEndPage
        '
        Me.txbEndPage.AcceptsReturn = True
        Me.txbEndPage.BackColor = System.Drawing.SystemColors.Window
        Me.txbEndPage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbEndPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbEndPage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbEndPage.Location = New System.Drawing.Point(224, 59)
        Me.txbEndPage.MaxLength = 0
        Me.txbEndPage.Name = "txbEndPage"
        Me.txbEndPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbEndPage.Size = New System.Drawing.Size(68, 23)
        Me.txbEndPage.TabIndex = 6
        Me.txbEndPage.Text = "0"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(10, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(69, 21)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Pages"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(170, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(48, 21)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "To"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(261, 40)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Overlap between pages"
        '
        'fraPrinter
        '
        Me.fraPrinter.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrinter.Controls.Add(Me.lblPrinterOrientation)
        Me.fraPrinter.Controls.Add(Me.Label10)
        Me.fraPrinter.Controls.Add(Me.lblPrinterName)
        Me.fraPrinter.Controls.Add(Me.Label7)
        Me.fraPrinter.Controls.Add(Me.lblPrinterSize)
        Me.fraPrinter.Controls.Add(Me.lblPdcdcrinter)
        Me.fraPrinter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrinter.Location = New System.Drawing.Point(393, 371)
        Me.fraPrinter.Name = "fraPrinter"
        Me.fraPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrinter.Size = New System.Drawing.Size(304, 99)
        Me.fraPrinter.TabIndex = 0
        Me.fraPrinter.TabStop = False
        Me.fraPrinter.Text = "Printer"
        '
        'lblPrinterOrientation
        '
        Me.lblPrinterOrientation.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrinterOrientation.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrinterOrientation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterOrientation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrinterOrientation.Location = New System.Drawing.Point(125, 72)
        Me.lblPrinterOrientation.Name = "lblPrinterOrientation"
        Me.lblPrinterOrientation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrinterOrientation.Size = New System.Drawing.Size(173, 21)
        Me.lblPrinterOrientation.TabIndex = 25
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(10, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(116, 21)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Paper Orientation:"
        '
        'lblPrinterName
        '
        Me.lblPrinterName.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrinterName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrinterName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrinterName.Location = New System.Drawing.Point(83, 19)
        Me.lblPrinterName.Name = "lblPrinterName"
        Me.lblPrinterName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrinterName.Size = New System.Drawing.Size(215, 30)
        Me.lblPrinterName.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(10, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(87, 20)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Name:"
        '
        'lblPrinterSize
        '
        Me.lblPrinterSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrinterSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrinterSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrinterSize.Location = New System.Drawing.Point(93, 49)
        Me.lblPrinterSize.Name = "lblPrinterSize"
        Me.lblPrinterSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrinterSize.Size = New System.Drawing.Size(205, 21)
        Me.lblPrinterSize.TabIndex = 2
        '
        'lblPdcdcrinter
        '
        Me.lblPdcdcrinter.BackColor = System.Drawing.SystemColors.Control
        Me.lblPdcdcrinter.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPdcdcrinter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdcdcrinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPdcdcrinter.Location = New System.Drawing.Point(10, 49)
        Me.lblPdcdcrinter.Name = "lblPdcdcrinter"
        Me.lblPdcdcrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPdcdcrinter.Size = New System.Drawing.Size(87, 21)
        Me.lblPdcdcrinter.TabIndex = 1
        Me.lblPdcdcrinter.Text = "Paper Size:"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.optLandscape)
        Me.Frame2.Controls.Add(Me.optPortrait)
        Me.Frame2.Controls.Add(Me.cboPageToPrinterMapping)
        Me.Frame2.Controls.Add(Me.cboPageSize)
        Me.Frame2.Controls.Add(Me.lblPageCount)
        Me.Frame2.Controls.Add(Me.Label9)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(393, 59)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(304, 303)
        Me.Frame2.TabIndex = 16
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Page"
        '
        'optLandscape
        '
        Me.optLandscape.BackColor = System.Drawing.SystemColors.Control
        Me.optLandscape.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLandscape.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLandscape.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLandscape.Location = New System.Drawing.Point(108, 148)
        Me.optLandscape.Name = "optLandscape"
        Me.optLandscape.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLandscape.Size = New System.Drawing.Size(117, 30)
        Me.optLandscape.TabIndex = 22
        Me.optLandscape.TabStop = True
        Me.optLandscape.Text = "Landscape"
        Me.optLandscape.UseVisualStyleBackColor = False
        '
        'optPortrait
        '
        Me.optPortrait.BackColor = System.Drawing.SystemColors.Control
        Me.optPortrait.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPortrait.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPortrait.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPortrait.Location = New System.Drawing.Point(10, 148)
        Me.optPortrait.Name = "optPortrait"
        Me.optPortrait.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPortrait.Size = New System.Drawing.Size(87, 30)
        Me.optPortrait.TabIndex = 21
        Me.optPortrait.TabStop = True
        Me.optPortrait.Text = "Portrait"
        Me.optPortrait.UseVisualStyleBackColor = False
        '
        'cboPageToPrinterMapping
        '
        Me.cboPageToPrinterMapping.BackColor = System.Drawing.SystemColors.Window
        Me.cboPageToPrinterMapping.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPageToPrinterMapping.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPageToPrinterMapping.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPageToPrinterMapping.Location = New System.Drawing.Point(10, 108)
        Me.cboPageToPrinterMapping.Name = "cboPageToPrinterMapping"
        Me.cboPageToPrinterMapping.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPageToPrinterMapping.Size = New System.Drawing.Size(288, 24)
        Me.cboPageToPrinterMapping.TabIndex = 20
        Me.cboPageToPrinterMapping.Text = "Combo2"
        '
        'cboPageSize
        '
        Me.cboPageSize.BackColor = System.Drawing.SystemColors.Window
        Me.cboPageSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPageSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPageSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPageSize.Location = New System.Drawing.Point(10, 49)
        Me.cboPageSize.Name = "cboPageSize"
        Me.cboPageSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPageSize.Size = New System.Drawing.Size(288, 24)
        Me.cboPageSize.TabIndex = 18
        Me.cboPageSize.Text = "Combo1"
        '
        'lblPageCount
        '
        Me.lblPageCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblPageCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPageCount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPageCount.Location = New System.Drawing.Point(111, 187)
        Me.lblPageCount.Name = "lblPageCount"
        Me.lblPageCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPageCount.Size = New System.Drawing.Size(187, 21)
        Me.lblPageCount.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(10, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(106, 21)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Page Count: "
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(10, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(288, 30)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Page to Printer Mapping"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(10, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(288, 40)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Page Size"
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line2.Location = New System.Drawing.Point(10, 49)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(610, 1)
        Me.Line2.TabIndex = 17
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(10, 59)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(377, 551)
        Me.AxPageLayoutControl1.TabIndex = 18
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(58, 69)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(10, 223)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 71)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Changing the page orientation or size may result in the map frame shrinking in re" & _
            "lation to the page. This is dependant on the IPage::StretchGraphicsWithPage prop" & _
            "erty."
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 16)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(710, 621)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.cmdLoadMxFile)
        Me.Controls.Add(Me.txbMxFilePath)
        Me.Controls.Add(Me.fraPrint)
        Me.Controls.Add(Me.fraPrinter)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Line2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Print Page Layout"
        Me.fraPrint.ResumeLayout(False)
        Me.fraPrint.PerformLayout()
        Me.fraPrinter.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private Sub cmdLoadMxFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadMxFile.Click

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
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
            AxPageLayoutControl1.LoadMxFile((sFilePath))
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
            txbMxFilePath.Text = sFilePath
        Else
            MsgBox(sFilePath & " is not a valid ArcMap document")
        End If

        'Update page display
        cboPageSize.SelectedIndex = AxPageLayoutControl1.Page.FormID
        cboPageToPrinterMapping.SelectedIndex = AxPageLayoutControl1.Page.PageToPrinterMapping
        If AxPageLayoutControl1.Page.Orientation = 1 Then
            optPortrait.Checked = True
        Else
            optLandscape.Checked = True
        End If

        'Zoom to whole page
        AxPageLayoutControl1.ZoomToWholePage()

        'Update printer page display
        UpdatePrintPageDisplay()

    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        Dim pPrinter As IPrinter
        If Not AxPageLayoutControl1.Printer Is Nothing Then
            'Set mouse pointer
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass

            'Get IPrinter interface through the PageLayoutControl's printer
            pPrinter = AxPageLayoutControl1.Printer

            'Determine whether printer paper's orientation needs changing
            If pPrinter.Paper.Orientation <> AxPageLayoutControl1.Page.Orientation Then
                pPrinter.Paper.Orientation = AxPageLayoutControl1.Page.Orientation
                'Update the display
                UpdatePrintingDisplay()
            End If

            'Print the page range with the specified overlap
            AxPageLayoutControl1.PrintPageLayout(Val(txbStartPage.Text), Val(txbEndPage.Text), Val(txbOverlap.Text))

            'Set the mouse pointer
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
        End If

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Add esriPageFormID constants to drop down
        cboPageSize.Items.Add("Letter - 8.5in x 11in. ")
        cboPageSize.Items.Add("Legal - 8.5in x 14in.")
        cboPageSize.Items.Add("Tabloid - 11in x 17in.")
        cboPageSize.Items.Add("C - 17in x 22in.")
        cboPageSize.Items.Add("D - 22in x 34in.")
        cboPageSize.Items.Add("E - 34in x 44in.")
        cboPageSize.Items.Add("A5 - 148mm x 210mm.")
        cboPageSize.Items.Add("A4 - 210mm x 297mm.")
        cboPageSize.Items.Add("A3 - 297mm x 420mm.")
        cboPageSize.Items.Add("A2 - 420mm x 594mm.")
        cboPageSize.Items.Add("A1 - 594mm x 841mm.")
        cboPageSize.Items.Add("A0 - 841mm x 1189mm.")
        cboPageSize.Items.Add("Custom Page Size.")
        cboPageSize.Items.Add("Same as Printer Form.")
        cboPageSize.SelectedIndex = 7

        'Add esriPageToPrinterMapping constants to drop down
        cboPageToPrinterMapping.Items.Add("0: Crop")
        cboPageToPrinterMapping.Items.Add("1: Scale")
        cboPageToPrinterMapping.Items.Add("2: Tile")
        cboPageToPrinterMapping.SelectedIndex = 1
        optPortrait.Checked = 1
        EnableOrientation(False)

        'Display printer details
        UpdatePrintingDisplay()

    End Sub

    Private Sub UpdatePrintPageDisplay()

        'Determine the number of pages
        Dim iPageCount As Short
        iPageCount = AxPageLayoutControl1.get_PrinterPageCount(Val(txbOverlap.Text))
        lblPageCount.Text = CStr(iPageCount)

        'Validate start and end pages
        Dim iPageStart As Short
        Dim iPageEnd As Short
        iPageStart = Val(txbStartPage.Text)
        iPageEnd = Val(txbEndPage.Text)
        If iPageStart < 1 Or iPageStart > iPageCount Then
            txbStartPage.Text = CStr(1)
        End If
        If iPageEnd < 1 Or iPageEnd > iPageCount Then
            txbEndPage.Text = CStr(iPageCount)
        End If

    End Sub

    Private Sub UpdatePrintingDisplay()

        Dim pPrinter As IPrinter
        Dim dWidth As Double
        Dim dheight As Double
        If Not AxPageLayoutControl1.Printer Is Nothing Then
            'Get IPrinter interface through the PageLayoutControl's printer
            pPrinter = AxPageLayoutControl1.Printer

            'Determine the orientation of the printer's paper
            If pPrinter.Paper.Orientation = 1 Then
                lblPrinterOrientation.Text = "Portrait"
            Else
                lblPrinterOrientation.Text = "Landscape"
            End If

            'Determine the printer name
            lblPrinterName.Text = pPrinter.Paper.PrinterName

            'Determine the printer's paper size
            pPrinter.Paper.QueryPaperSize(dWidth, dheight)
            lblPrinterSize.Text = Format(dWidth, "###.000") & " by " & Format(dheight, "###.000") & " Inches"
        End If

    End Sub

    Private Sub txbOverlap_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txbOverlap.Leave

        'Update printer page display
        UpdatePrintPageDisplay()

    End Sub

    Private Sub cboPageToPrinterMapping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageToPrinterMapping.Click
        'Set the printer to page mapping
        AxPageLayoutControl1.Page.PageToPrinterMapping = cboPageToPrinterMapping.SelectedIndex
        'Update printer page display
        UpdatePrintPageDisplay()
    End Sub

    Private Sub optLandscape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optLandscape.Click
        If sender.Checked Then
            'Set the page orientation
            If AxPageLayoutControl1.Page.FormID <> esriPageFormID.esriPageFormSameAsPrinter Then
                AxPageLayoutControl1.Page.Orientation = 2
            End If
            'Update printer page display
            UpdatePrintPageDisplay()
        End If
    End Sub

    Private Sub optPortrait_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPortrait.Click
        If sender.Checked Then
            'Set the page orientation
            If AxPageLayoutControl1.Page.FormID <> esriPageFormID.esriPageFormSameAsPrinter Then
                AxPageLayoutControl1.Page.Orientation = 1
            End If
            'Update printer page display
            UpdatePrintPageDisplay()
        End If
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        'Orientation cannot change if the page size is set to 'Same as Printer'
        If cboPageSize.SelectedIndex = 13 Then
            EnableOrientation(False)
        Else
            EnableOrientation(True)
        End If
        'Set the page size
        AxPageLayoutControl1.Page.FormID = cboPageSize.SelectedIndex
        'Update printer page display
        UpdatePrintPageDisplay()

    End Sub

    Private Sub EnableOrientation(ByVal b As Boolean)
        optPortrait.Enabled = b
        optLandscape.Enabled = b
    End Sub

End Class