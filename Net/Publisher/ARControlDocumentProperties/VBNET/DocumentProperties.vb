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
#Region "Windows Form Designer generated code "

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
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents cmdMapProperties As System.Windows.Forms.Button
    Public WithEvents chkTOC As System.Windows.Forms.CheckBox
    Public WithEvents cmdProperties As System.Windows.Forms.Button
    Public WithEvents cmdLoad As System.Windows.Forms.Button
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxArcReaderControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DocumentProperties))
        Me.cmdMapProperties = New System.Windows.Forms.Button
        Me.chkTOC = New System.Windows.Forms.CheckBox
        Me.cmdProperties = New System.Windows.Forms.Button
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxArcReaderControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderControl
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdMapProperties
        '
        Me.cmdMapProperties.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMapProperties.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMapProperties.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMapProperties.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMapProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdMapProperties.Location = New System.Drawing.Point(264, 8)
        Me.cmdMapProperties.Name = "cmdMapProperties"
        Me.cmdMapProperties.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMapProperties.Size = New System.Drawing.Size(121, 33)
        Me.cmdMapProperties.TabIndex = 8
        Me.cmdMapProperties.Text = "Map / Layer Properties"
        Me.cmdMapProperties.UseVisualStyleBackColor = False
        '
        'chkTOC
        '
        Me.chkTOC.BackColor = System.Drawing.SystemColors.Control
        Me.chkTOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTOC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTOC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTOC.Location = New System.Drawing.Point(392, 16)
        Me.chkTOC.Name = "chkTOC"
        Me.chkTOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTOC.Size = New System.Drawing.Size(88, 17)
        Me.chkTOC.TabIndex = 4
        Me.chkTOC.Text = "TOC Visible"
        Me.chkTOC.UseVisualStyleBackColor = False
        '
        'cmdProperties
        '
        Me.cmdProperties.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProperties.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProperties.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProperties.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProperties.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdProperties.Location = New System.Drawing.Point(136, 8)
        Me.cmdProperties.Name = "cmdProperties"
        Me.cmdProperties.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProperties.Size = New System.Drawing.Size(121, 33)
        Me.cmdProperties.TabIndex = 3
        Me.cmdProperties.Text = "File Properties"
        Me.cmdProperties.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.Color.Black
        Me.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdLoad.Location = New System.Drawing.Point(8, 8)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoad.Size = New System.Drawing.Size(121, 33)
        Me.cmdLoad.TabIndex = 0
        Me.cmdLoad.Text = "Load PMF"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label6.Location = New System.Drawing.Point(488, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(249, 17)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "6) Display the map and layer properties."
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(488, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(249, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "5) Hide the TOC."
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label4.Location = New System.Drawing.Point(488, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(249, 33)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "4) Right click on a layer in the TOC to display the layer properties."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(488, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(249, 33)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "3) Right click on a map in the TOC to display the data frame properties."
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(488, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(249, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "2) Display the file properties."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(488, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(249, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "1) Browse to a PMF to load."
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(480, 168)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(256, 320)
        Me.RichTextBox1.TabIndex = 14
        Me.RichTextBox1.Text = ""
        '
        'AxArcReaderControl1
        '
        Me.AxArcReaderControl1.Location = New System.Drawing.Point(8, 48)
        Me.AxArcReaderControl1.Name = "AxArcReaderControl1"
        Me.AxArcReaderControl1.OcxState = CType(resources.GetObject("AxArcReaderControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxArcReaderControl1.Size = New System.Drawing.Size(464, 440)
        Me.AxArcReaderControl1.TabIndex = 15
        '
        'DocumentProperties
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(742, 498)
        Me.Controls.Add(Me.AxArcReaderControl1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.cmdMapProperties)
        Me.Controls.Add(Me.chkTOC)
        Me.Controls.Add(Me.cmdProperties)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DocumentProperties"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "DocumentProperties"
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub cmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoad.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Select Published Map Document"
        OpenFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Load the specified pmf
        If AxArcReaderControl1.CheckDocument(sFilePath) = True Then
            AxArcReaderControl1.LoadDocument(sFilePath)
        Else
            MsgBox("This document cannot be loaded!")
            Exit Sub
        End If

        'Determine whether permission to toggle TOC visibility
        If AxArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsViewTOC) = True Then
            chkTOC.Enabled = True
            If AxArcReaderControl1.TOCVisible = True Then
                chkTOC.CheckState = CheckState.Checked
            Else
                chkTOC.CheckState = CheckState.Unchecked
            End If
        Else
            MsgBox("You do not have permission to toggle TOC visibility!")
        End If

        RichTextBox1.Text = ""

    End Sub

    Private Sub cmdMapProperties_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMapProperties.Click

        'Determine whether a document is loaded
        If AxArcReaderControl1.CurrentViewType = esriARViewType.esriARViewTypeNone Then
            MsgBox("You must load a map document first!")
            Exit Sub
        End If

        Dim i As Short
        Dim j As Short
        Dim pMap As IARMap
        Dim pLayer As IARLayer
        Dim sProps As String
        Dim pExtent As esriARExtentType

        sProps = "Map and Layer Properties" & Chr(10) & Chr(10)

        'Loop through each map in the document 
        For i = 0 To AxArcReaderControl1.ARPageLayout.ARMapCount - 1

            'Get the IARMap interface
            pMap = AxArcReaderControl1.ARPageLayout.ARMap(i)
            'Get map properties
            sProps = sProps & UCase(pMap.Name) & Chr(10) & "Description:" & pMap.Description & Chr(10) & "Spatial Reference:" & Chr(9) & pMap.SpatialReferenceName & Chr(10) & "Units:" & Chr(9) & Chr(9) & AxArcReaderControl1.ARUnitConverter.EsriUnitsAsString(pMap.DistanceUnits, esriARCaseAppearance.esriARCaseAppearanceUnchanged, True) & Chr(10)
            'Get map extent type
            pExtent = AxArcReaderControl1.ARPageLayout.MapExtentType(pMap)
            If pExtent = esriARExtentType.esriARExtentTypeFixedExtent Then
                sProps = sProps & "Extent Type:" & Chr(9) & "Fixed Extent" & Chr(10)
            ElseIf pExtent = esriARExtentType.esriARExtentTypeFixedScale Then
                sProps = sProps & "Extent Type:" & Chr(9) & "Fixed Scale" & Chr(10)
            Else
                sProps = sProps & "Extent Type:" & Chr(9) & "Automatic" & Chr(10)
            End If
            sProps = sProps & Chr(10)

            'Loop through each layer in the map
            For j = 0 To pMap.ARLayerCount - 1

                'Get the IARLayer interface
                pLayer = pMap.ARLayer(j)

                'Get the layer properties
                sProps = sProps & pLayer.Name & Chr(10) & "Description:" & Chr(9) & pLayer.Description & Chr(10) & "Minimum Scale:" & Chr(9) & pLayer.MinimumScale & Chr(10) & "Maximum Scale:" & Chr(9) & pLayer.MaximumScale & Chr(10) & Chr(10)
            Next j

            sProps = sProps & Chr(10) & Chr(10)
        Next i

        RichTextBox1.Text = sProps

    End Sub

    Private Sub cmdProperties_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProperties.Click

        'Show map properties
        If AxArcReaderControl1.CurrentViewType = esriARViewType.esriARViewTypeNone Then
            MsgBox("You must load a map document first!")
        Else
            AxArcReaderControl1.ShowARWindow(esriARWindows.esriARWindowsFileProperties, True, Type.Missing)
        End If

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Load command button images 
        Dim pImage As System.Drawing.Image
        Dim pBitmap As System.Drawing.Bitmap
        pImage = New System.Drawing.Bitmap(GetType(DocumentProperties).Assembly.GetManifestResourceStream(GetType(DocumentProperties), "browse.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdLoad.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(DocumentProperties).Assembly.GetManifestResourceStream(GetType(DocumentProperties), "properti.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)

        cmdProperties.Image = pImage
        cmdMapProperties.Image = pImage
        chkTOC.Enabled = False

    End Sub

    Private Sub chkTOC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTOC.Click

        'Toggle TOC visibility
        If chkTOC.CheckState = 1 Then
            AxArcReaderControl1.TOCVisible = True
        Else
            AxArcReaderControl1.TOCVisible = False
        End If

    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

    End Sub
End Class