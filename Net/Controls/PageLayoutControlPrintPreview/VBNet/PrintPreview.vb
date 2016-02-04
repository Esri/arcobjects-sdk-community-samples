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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
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

    'Declare a PrintDocument object named document.
    Private WithEvents document As New System.Drawing.Printing.PrintDocument
    Private m_CurrentPrintPage As Short 'the page number of the currently printed page
    Private m_TrackCancel As ITrackCancel 'a cancel tracker for the printing process

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
    Me.Button1 = New System.Windows.Forms.Button
    Me.Button2 = New System.Windows.Forms.Button
    Me.Button3 = New System.Windows.Forms.Button
    Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
    Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
    Me.Button4 = New System.Windows.Forms.Button
    Me.ComboBox1 = New System.Windows.Forms.ComboBox
    Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
    Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
    Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
    CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(16, 8)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(96, 32)
    Me.Button1.TabIndex = 1
    Me.Button1.Text = "Open File"
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(128, 8)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(96, 32)
    Me.Button2.TabIndex = 2
    Me.Button2.Text = "Page Setup"
    '
    'Button3
    '
    Me.Button3.Location = New System.Drawing.Point(240, 8)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(96, 32)
    Me.Button3.TabIndex = 3
    Me.Button3.Text = "Print Preview"
    '
    'PrintPreviewDialog1
    '
    Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
    Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
    Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
    Me.PrintPreviewDialog1.Enabled = True
    Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
    Me.PrintPreviewDialog1.Location = New System.Drawing.Point(154, 203)
    Me.PrintPreviewDialog1.MinimumSize = New System.Drawing.Size(375, 250)
    Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
    Me.PrintPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty
    Me.PrintPreviewDialog1.Visible = False
    '
    'Button4
    '
    Me.Button4.Location = New System.Drawing.Point(352, 8)
    Me.Button4.Name = "Button4"
    Me.Button4.Size = New System.Drawing.Size(96, 32)
    Me.Button4.TabIndex = 7
    Me.Button4.Text = "Print"
    '
    'ComboBox1
    '
    Me.ComboBox1.Location = New System.Drawing.Point(464, 16)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(144, 21)
    Me.ComboBox1.TabIndex = 8
    '
    'AxPageLayoutControl1
    '
    Me.AxPageLayoutControl1.Location = New System.Drawing.Point(0, 48)
    Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
    Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxPageLayoutControl1.Size = New System.Drawing.Size(624, 448)
    Me.AxPageLayoutControl1.TabIndex = 9
    '
    'AxLicenseControl1
    '
    Me.AxLicenseControl1.Enabled = True
    Me.AxLicenseControl1.Location = New System.Drawing.Point(440, 112)
    Me.AxLicenseControl1.Name = "AxLicenseControl1"
    Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
    Me.AxLicenseControl1.TabIndex = 10
    '
    'Form1
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(624, 494)
    Me.Controls.Add(Me.AxLicenseControl1)
    Me.Controls.Add(Me.AxPageLayoutControl1)
    Me.Controls.Add(Me.ComboBox1)
    Me.Controls.Add(Me.Button4)
    Me.Controls.Add(Me.Button3)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.Button1)
    Me.Name = "Form1"
    Me.Text = "Print Preview / Print dialog Sample"
    CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim myStream As System.IO.Stream

        'create a new open file dialog object
        Dim openFileDialog1 As New OpenFileDialog

        'initialize the filter, filter index and restore directory flag
        openFileDialog1.Filter = "template files (*.mxt)|*.mxt|mxd files (*.mxd)|*.mxd"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        'display the dialog and wait for user input
        If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            myStream = openFileDialog1.OpenFile()
            If Not (myStream Is Nothing) Then

                Dim fileName As String
                fileName = openFileDialog1.FileName 'get the selected file

                If AxPageLayoutControl1.CheckMxFile(fileName) Then
                    'load the file in the page layout control
                    AxPageLayoutControl1.LoadMxFile(fileName, "")
                End If

                myStream.Close()

            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        PageSetupDialog1.Document = document

        'Show the page setup dialog storing the result.
        Dim result As DialogResult
        result = PageSetupDialog1.ShowDialog()

        'set the printer settings of the preview document to the selected printer settings
        document.PrinterSettings = PageSetupDialog1.PrinterSettings

        'set the page settings of the preview document to the selected page settings
        document.DefaultPageSettings = PageSetupDialog1.PageSettings

        'due to a bug in PageSetupDialog the PaperSize has to be set explicitly by iterating through the
        'available PaperSizes in the PageSetupDialog finding the selected PaperSize
        Dim i As Integer
        For i = 0 To PageSetupDialog1.PrinterSettings.PaperSizes.Count - 1
            If PageSetupDialog1.PrinterSettings.PaperSizes.Item(i).Kind = document.DefaultPageSettings().PaperSize().Kind Then
                document.DefaultPageSettings().PaperSize() = PageSetupDialog1.PrinterSettings.PaperSizes.Item(i)
            End If
        Next i

        '------------------------------------------------------------
        'initialize the current printer from the printer settings selected
        'in the page setup dialog
        '-------------------------------------------------------------
        Dim paper As IPaper
        paper = New PaperClass   'create a paper object

        Dim printer As IPrinter
        printer = New EmfPrinterClass 'create a printer object
        'in this case an EMF printer, alternatively a PS printer could be used

        'initialize the paper with the DEVMODE and DEVNAMES structures from the windows GDI
        'these structures specify information about the initialization and environment of a printer as well as
        'driver, device, and output port names for a printer
        paper.Attach(PageSetupDialog1.PrinterSettings.GetHdevmode(PageSetupDialog1.PageSettings).ToInt32(), PageSetupDialog1.PrinterSettings.GetHdevnames().ToInt32())

        'pass the paper to the emf printer
        printer.Paper = paper

        'set the page layout control's printer to the currently selected printer
        AxPageLayoutControl1.Printer = printer

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        'initialize the currently printed page number
        m_CurrentPrintPage = 0

        If AxPageLayoutControl1.DocumentFilename Is Nothing Then Exit Sub

        'set the print document's name to that if the document in the PageLayoutControl
        document.DocumentName = AxPageLayoutControl1.DocumentFilename

        'set the Document property to the PrintDocument object selected by the user
        PrintPreviewDialog1.Document = document

        'show the dialog - this triggers the document's PrintPage event
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        'allow the user to choose the page range to be printed
        PrintDialog1.AllowSomePages = True
        'show the help button.
        PrintDialog1.ShowHelp = True

        'set the Document property to the PrintDocument for which the PrintPage Event 
        'has been handled. To display the dialog, either this property or the 
        'PrinterSettings property must be set 
        PrintDialog1.Document = document

        'display the print dialog and wait for user input
        Dim result As DialogResult = PrintDialog1.ShowDialog()

        'if the result is OK then print the document.
        If (result = Windows.Forms.DialogResult.OK) Then document.Print()

    End Sub

    Private Sub InitializePrintPreviewDialog()

        'set the size, location, name and the minimum size the dialog can be resized to
        PrintPreviewDialog1.ClientSize() = New System.Drawing.Size(800, 600)
        PrintPreviewDialog1.Location() = New System.Drawing.Point(29, 29)
        PrintPreviewDialog1.Name() = "PrintPreviewDialog1"
        PrintPreviewDialog1.MinimumSize = New System.Drawing.Size(375, 250)
        'set UseAntiAlias to true to allow the operating system to smooth fonts
        PrintPreviewDialog1.UseAntiAlias = True

    End Sub

    Private Sub InitializePageSetupDialog()

        'initialize the dialog's PrinterSettings property to hold user defined printer settings.
        PageSetupDialog1.PageSettings = New System.Drawing.Printing.PageSettings
        'Initialize dialog's PrinterSettings property to hold user set printer settings
        PageSetupDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings
        'Do not show the network in the printer dialog
        PageSetupDialog1.ShowNetwork = False

    End Sub

    Private Sub document_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles document.PrintPage

        'this code will be called when the PrintPreviewDialog.Show method is called
        'set the PageToPrinterMapping property of the Page. This specifies how the page 
        'is mapped onto the printer page.
        Dim sPageToPrinterMapping As String
        sPageToPrinterMapping = Me.ComboBox1.SelectedItem 'get the selected mapping option
        If sPageToPrinterMapping Is Nothing Then 'if no selection has been made the default is tiling
            AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile
        Else
            If sPageToPrinterMapping.Equals("esriPageMappingTile") = True Then
                AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile
            ElseIf sPageToPrinterMapping.Equals("esriPageMappingCrop") = True Then
                AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingCrop
            ElseIf sPageToPrinterMapping.Equals("esriPageMappingScale") = True Then
                AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale
            Else
                AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile
            End If
        End If

        Dim deviceRect As ESRI.ArcGIS.esriSystem.tagRECT 'structure for the device boundaries

        Dim dpi As Short
        dpi = e.Graphics.DpiX 'get the resolution of the graphics device used by the print preview
        'e holds print preview arguments, including the graphics device

        Dim devBounds As IEnvelope 'envelope for the device boundaries
        devBounds = New EnvelopeClass

        Dim page As IPage 'the page of the page layout control
        page = AxPageLayoutControl1.Page

        Dim printer As IPrinter 'the currently selected printer
        printer = AxPageLayoutControl1.Printer

        Dim printPageCount As Short 'the number of printer pages the page will be printed on 

        printPageCount = AxPageLayoutControl1.get_PrinterPageCount(0) '.Page.PrinterPageCount(printer, 0, printPageCount)
        m_CurrentPrintPage += 1

        'get the device bounds of the currently selected printer
        page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds)

        'Returns the coordinates of lower, left and upper, right corners
        Dim xmin, ymin, xmax, ymax As Double
        devBounds.QueryCoords(xmin, ymin, xmax, ymax)

        'initialize the structure for the device boundaries
        deviceRect.bottom = ymax
        deviceRect.left = xmin
        deviceRect.top = ymin
        deviceRect.right = xmax

        'determine the visible bounds of the currently printed page
        Dim visBounds As IEnvelope
        visBounds = New EnvelopeClass
        page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds)

        'get a handle to the graphics device
        'this is the device the print preview will be drawn to
        Dim hdc As IntPtr = e.Graphics.GetHdc()

        'print the page of the page layout control to the graphics device using the specified boundaries 
        AxPageLayoutControl1.ActiveView.Output(hdc.ToInt32(), dpi, deviceRect, visBounds, m_TrackCancel)

        'release the graphics device handle
        e.Graphics.ReleaseHdc(hdc)

        'check if further pages have to be printed
        If m_CurrentPrintPage < printPageCount Then
            e.HasMorePages = True 'document_PrintPage event will be called again
        Else
            e.HasMorePages = False
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        InitializePrintPreviewDialog()
        InitializePageSetupDialog()

        ComboBox1.Items.Add("esriPageMappingTile")
        ComboBox1.Items.Add("esriPageMappingCrop")
        ComboBox1.Items.Add("esriPageMappingScale")
        ComboBox1.SelectedIndex = 0

    End Sub
End Class
