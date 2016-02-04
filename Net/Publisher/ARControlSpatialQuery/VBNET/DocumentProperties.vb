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
Imports ESRI.ArcGIS.PublisherControls
Imports ESRI.ArcGIS

Public Class DocumentProperties
  Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.ArcReader) Then
            If Not RuntimeManager.Bind(ProductCode.EngineOrDesktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit 
            End If
        End If

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnProperties As System.Windows.Forms.Button
    Friend WithEvents btnGlobeProperties As System.Windows.Forms.Button
    Friend WithEvents ChkTOC As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AxArcReaderGlobeControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DocumentProperties))
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnProperties = New System.Windows.Forms.Button
        Me.btnGlobeProperties = New System.Windows.Forms.Button
        Me.ChkTOC = New System.Windows.Forms.CheckBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.AxArcReaderGlobeControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
        CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLoad.Location = New System.Drawing.Point(8, 8)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(88, 44)
        Me.btnLoad.TabIndex = 1
        Me.btnLoad.Text = "Load PMF"
        Me.btnLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnProperties
        '
        Me.btnProperties.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnProperties.Location = New System.Drawing.Point(104, 8)
        Me.btnProperties.Name = "btnProperties"
        Me.btnProperties.Size = New System.Drawing.Size(88, 44)
        Me.btnProperties.TabIndex = 2
        Me.btnProperties.Text = "File Properties"
        Me.btnProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnGlobeProperties
        '
        Me.btnGlobeProperties.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGlobeProperties.Location = New System.Drawing.Point(200, 8)
        Me.btnGlobeProperties.Name = "btnGlobeProperties"
        Me.btnGlobeProperties.Size = New System.Drawing.Size(136, 44)
        Me.btnGlobeProperties.TabIndex = 3
        Me.btnGlobeProperties.Text = "Globe / Layer Properties"
        Me.btnGlobeProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ChkTOC
        '
        Me.ChkTOC.Location = New System.Drawing.Point(384, 23)
        Me.ChkTOC.Name = "ChkTOC"
        Me.ChkTOC.Size = New System.Drawing.Size(88, 16)
        Me.ChkTOC.TabIndex = 4
        Me.ChkTOC.Text = "TOC Visible"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(480, 168)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(248, 320)
        Me.RichTextBox1.TabIndex = 15
        Me.RichTextBox1.Text = ""
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(480, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "1) Browse to a 3D PMF to load."
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(480, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(248, 16)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "2) Display the file properties."
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(480, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(248, 32)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "3) Right click on the globe in the TOC to display the data frame properties."
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label4.Location = New System.Drawing.Point(480, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(248, 32)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "4) Right click on a layer in the TOC to display the layer properties."
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(480, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(248, 32)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "5) Hide the TOC."
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label6.Location = New System.Drawing.Point(480, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(248, 24)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "6) Display the globe and layer properties."
        '
        'AxArcReaderGlobeControl1
        '
        Me.AxArcReaderGlobeControl1.Location = New System.Drawing.Point(8, 58)
        Me.AxArcReaderGlobeControl1.Name = "AxArcReaderGlobeControl1"
        Me.AxArcReaderGlobeControl1.OcxState = CType(resources.GetObject("AxArcReaderGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxArcReaderGlobeControl1.Size = New System.Drawing.Size(464, 430)
        Me.AxArcReaderGlobeControl1.TabIndex = 22
        '
        'DocumentProperties
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(736, 494)
        Me.Controls.Add(Me.AxArcReaderGlobeControl1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.ChkTOC)
        Me.Controls.Add(Me.btnGlobeProperties)
        Me.Controls.Add(Me.btnProperties)
        Me.Controls.Add(Me.btnLoad)
        Me.Name = "DocumentProperties"
        Me.Text = "DocumentProperties"
        CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Select Published Map Document"
        OpenFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Load the specified pmf
        If AxArcReaderGlobeControl1.CheckDocument(sFilePath) = True Then
            AxArcReaderGlobeControl1.LoadDocument(sFilePath)
        Else
            MsgBox("This document cannot be loaded!")
            Exit Sub
        End If

        'Determine whether permission to toggle TOC visibility
        If AxArcReaderGlobeControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsViewTOC) = True Then
            ChkTOC.Enabled = True
            If AxArcReaderGlobeControl1.TOCVisible = True Then
                ChkTOC.CheckState = CheckState.Checked
            Else
                ChkTOC.CheckState = CheckState.Unchecked
            End If
        Else
            MsgBox("You do not have permission to toggle TOC visibility!")
        End If

        RichTextBox1.Text = ""
    End Sub

    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click

        'Show map properties
        If AxArcReaderGlobeControl1.CurrentGlobeViewType = esriARGlobeViewType.esriARGlobeViewTypeNone Then
            MsgBox("You must load a map document first!")
        Else
            AxArcReaderGlobeControl1.ShowARGlobeWindow(esriARGlobeWindows.esriARGlobeWindowsFileProperties, True, "")
        End If

    End Sub

    Private Sub DocumentProperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Load command button images 
        Dim pImage As System.Drawing.Image
        Dim pBitmap As System.Drawing.Bitmap
        pImage = New System.Drawing.Bitmap(GetType(DocumentProperties).Assembly.GetManifestResourceStream(GetType(DocumentProperties), "browse.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        btnLoad.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(DocumentProperties).Assembly.GetManifestResourceStream(GetType(DocumentProperties), "properti.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)

        btnProperties.Image = pImage
        btnGlobeProperties.Image = pImage
        ChkTOC.Enabled = False

    End Sub

    Private Sub DocumentProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

        'Release COM objects
    ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

    End Sub

    Private Sub btnGlobeProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGlobeProperties.Click

        'Determine whether a document is loaded
        If AxArcReaderGlobeControl1.CurrentGlobeViewType = esriARGlobeViewType.esriARGlobeViewTypeNone Then
            MsgBox("You must load a Published Map File (PMF) first!")
            Exit Sub
        End If

        Dim j As Integer
        Dim pGlobe As IARGlobe
        Dim pLayer As IARLayer
        Dim sProps As String

        sProps = "Globe and Layer Properties" & Chr(10) & Chr(10)

        'Populate rich text box (Chr(9)->tab, Chr(10)->new line)
        'Get the IARGlobe interface
        pGlobe = AxArcReaderGlobeControl1.ARGlobe
        'Get pmf properties
        sProps = sProps & UCase(AxArcReaderGlobeControl1.DocumentFilename) & Chr(10) & _
        "Description:" & AxArcReaderGlobeControl1.DocumentComment & Chr(10) & _
        "Spatial Reference:" & Chr(9) & pGlobe.SpatialReferenceName & Chr(10) & _
        "Units:" & Chr(9) & Chr(9) & AxArcReaderGlobeControl1.ARUnitConverter.EsriUnitsAsString(pGlobe.GlobeUnits, esriARCaseAppearance.esriARCaseAppearanceUnchanged, True) & Chr(10)
        sProps = sProps & Chr(10)

        'Loop through each layer in the pmf
        For j = 0 To pGlobe.ARLayerCount - 1

            'Get the IARLayer interface
            pLayer = pGlobe.ARLayer(j)

            'Get the layer properties
            sProps = sProps & pLayer.Name & Chr(10) & _
            "Description:" & Chr(9) & pLayer.Description & Chr(10) & _
            "Minimum Scale:" & Chr(9) & pLayer.MinimumScale & Chr(10) & _
            "Maximum Scale:" & Chr(9) & pLayer.MaximumScale & Chr(10) & Chr(10)
        Next j

        sProps = sProps & Chr(10) & Chr(10)
        RichTextBox1.Text = sProps

    End Sub

    Private Sub ChkTOC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTOC.CheckedChanged
        'Toggle TOC visibility
        If ChkTOC.CheckState = 1 Then
            AxArcReaderGlobeControl1.TOCVisible = True
        Else
            AxArcReaderGlobeControl1.TOCVisible = False
        End If
    End Sub
End Class
