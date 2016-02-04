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

Public Class MapTools
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
    Public WithEvents cmdRedo As System.Windows.Forms.Button
    Public WithEvents cmdUndo As System.Windows.Forms.Button
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents cmdLoad As System.Windows.Forms.Button
    Public WithEvents txbPath As System.Windows.Forms.TextBox
    Public WithEvents _Label3_1 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents _Label3_0 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents optTool2 As System.Windows.Forms.RadioButton
    Public WithEvents optTool1 As System.Windows.Forms.RadioButton
    Public WithEvents optTool0 As System.Windows.Forms.RadioButton
    Friend WithEvents AxArcReaderControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MapTools))
        Me.cmdRedo = New System.Windows.Forms.Button
        Me.cmdUndo = New System.Windows.Forms.Button
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.optTool2 = New System.Windows.Forms.RadioButton
        Me.optTool1 = New System.Windows.Forms.RadioButton
        Me.optTool0 = New System.Windows.Forms.RadioButton
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.txbPath = New System.Windows.Forms.TextBox
        Me._Label3_1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me._Label3_0 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AxArcReaderControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderControl
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRedo
        '
        Me.cmdRedo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRedo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRedo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRedo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRedo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRedo.Location = New System.Drawing.Point(616, 288)
        Me.cmdRedo.Name = "cmdRedo"
        Me.cmdRedo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRedo.Size = New System.Drawing.Size(64, 49)
        Me.cmdRedo.TabIndex = 11
        Me.cmdRedo.Text = "Redo"
        Me.cmdRedo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdUndo
        '
        Me.cmdUndo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUndo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUndo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUndo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUndo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUndo.Location = New System.Drawing.Point(544, 288)
        Me.cmdUndo.Name = "cmdUndo"
        Me.cmdUndo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUndo.Size = New System.Drawing.Size(64, 49)
        Me.cmdUndo.TabIndex = 10
        Me.cmdUndo.Text = "Undo"
        Me.cmdUndo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdFullExtent.Location = New System.Drawing.Point(472, 288)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(64, 49)
        Me.cmdFullExtent.TabIndex = 9
        Me.cmdFullExtent.Text = "FullExtent"
        Me.cmdFullExtent.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optTool2
        '
        Me.optTool2.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool2.BackColor = System.Drawing.SystemColors.Control
        Me.optTool2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool2.Location = New System.Drawing.Point(616, 232)
        Me.optTool2.Name = "optTool2"
        Me.optTool2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool2.Size = New System.Drawing.Size(64, 49)
        Me.optTool2.TabIndex = 0
        Me.optTool2.TabStop = True
        Me.optTool2.Text = "Pan"
        Me.optTool2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optTool1
        '
        Me.optTool1.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool1.BackColor = System.Drawing.SystemColors.Control
        Me.optTool1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool1.Location = New System.Drawing.Point(544, 232)
        Me.optTool1.Name = "optTool1"
        Me.optTool1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool1.Size = New System.Drawing.Size(64, 49)
        Me.optTool1.TabIndex = 8
        Me.optTool1.TabStop = True
        Me.optTool1.Text = "ZoomOut"
        Me.optTool1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optTool0
        '
        Me.optTool0.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool0.BackColor = System.Drawing.SystemColors.Control
        Me.optTool0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool0.Location = New System.Drawing.Point(472, 232)
        Me.optTool0.Name = "optTool0"
        Me.optTool0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool0.Size = New System.Drawing.Size(64, 49)
        Me.optTool0.TabIndex = 7
        Me.optTool0.TabStop = True
        Me.optTool0.Text = "ZoomIn"
        Me.optTool0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoad.Location = New System.Drawing.Point(392, 8)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoad.Size = New System.Drawing.Size(81, 25)
        Me.cmdLoad.TabIndex = 2
        Me.cmdLoad.Text = "Load PMF"
        '
        'txbPath
        '
        Me.txbPath.AcceptsReturn = True
        Me.txbPath.AutoSize = False
        Me.txbPath.BackColor = System.Drawing.SystemColors.Window
        Me.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbPath.Location = New System.Drawing.Point(8, 8)
        Me.txbPath.MaxLength = 0
        Me.txbPath.Name = "txbPath"
        Me.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbPath.Size = New System.Drawing.Size(377, 25)
        Me.txbPath.TabIndex = 1
        Me.txbPath.Text = ""
        '
        '_Label3_1
        '
        Me._Label3_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_1.ForeColor = System.Drawing.SystemColors.Highlight
        Me._Label3_1.Location = New System.Drawing.Point(488, 128)
        Me._Label3_1.Name = "_Label3_1"
        Me._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_1.Size = New System.Drawing.Size(201, 33)
        Me._Label3_1.TabIndex = 12
        Me._Label3_1.Text = "4) Use the 'Layout' button to display the layout view. "
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label6.Location = New System.Drawing.Point(488, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(201, 56)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "5) Use the 'tools below to navigate the data within any of the data frames on the" & _
        " page layout."
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label4.Location = New System.Drawing.Point(488, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(201, 33)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "3) Use the tools below to navigate the focus map."
        '
        '_Label3_0
        '
        Me._Label3_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_0.ForeColor = System.Drawing.SystemColors.Highlight
        Me._Label3_0.Location = New System.Drawing.Point(488, 50)
        Me._Label3_0.Name = "_Label3_0"
        Me._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_0.Size = New System.Drawing.Size(201, 33)
        Me._Label3_0.TabIndex = 4
        Me._Label3_0.Text = "2) Use the 'Map' button to display the data view. "
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(488, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(201, 33)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "1) Enter a valid file path and load the PMF."
        '
        'AxArcReaderControl1
        '
        Me.AxArcReaderControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxArcReaderControl1.Name = "AxArcReaderControl1"
        Me.AxArcReaderControl1.OcxState = CType(resources.GetObject("AxArcReaderControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxArcReaderControl1.Size = New System.Drawing.Size(464, 448)
        Me.AxArcReaderControl1.TabIndex = 13
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(687, 498)
        Me.Controls.Add(Me.AxArcReaderControl1)
        Me.Controls.Add(Me.cmdRedo)
        Me.Controls.Add(Me.cmdUndo)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.optTool2)
        Me.Controls.Add(Me.optTool1)
        Me.Controls.Add(Me.optTool0)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.txbPath)
        Me.Controls.Add(Me._Label3_1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me._Label3_0)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Map Tools"
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Dim pARTool As esriARTool

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        'Set extent to full data extent
        Dim xMax, xMin, yMin, yMax As Double
        AxArcReaderControl1.ARPageLayout.FocusARMap.GetFullExtent(xMin, yMin, xMax, yMax)
        AxArcReaderControl1.ARPageLayout.FocusARMap.SetExtent(xMin, yMin, xMax, yMax)
        'Refresh the display
        AxArcReaderControl1.ARPageLayout.FocusARMap.Refresh()

    End Sub

    Private Sub cmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoad.Click

        'Load the specified pmf
        If txbPath.Text = "" Then Exit Sub
        If AxArcReaderControl1.CheckDocument(txbPath.Text) = True Then
            AxArcReaderControl1.LoadDocument(txbPath.Text)
        Else
            MsgBox("This document cannot be loaded!")
            Exit Sub
        End If

        optTool0.Checked = True

    End Sub

    Private Sub cmdRedo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRedo.Click

        'Redo extent
        AxArcReaderControl1.ARPageLayout.FocusARMap.RedoExtent()

    End Sub

    Private Sub cmdUndo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUndo.Click

        'Undo extent
        AxArcReaderControl1.ARPageLayout.FocusARMap.UndoExtent()

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Load option/command button images
        Dim pImage As System.Drawing.Image
        Dim pBitmap As System.Drawing.Bitmap
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "ZoomIn.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool0.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "ZoomOut.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool1.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "Pan.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool2.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "FullExtent.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdFullExtent.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "UnDoDraw.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdUndo.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(MapTools).Assembly.GetManifestResourceStream(GetType(MapTools), "ReDoDraw.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdRedo.Image = pImage

        'Disable controls
        optTool0.Enabled = False
        optTool1.Enabled = False
        optTool2.Enabled = False
        cmdUndo.Enabled = False
        cmdRedo.Enabled = False
        cmdFullExtent.Enabled = False

    End Sub
    Private Sub AxArcReaderControl1_OnAfterScreenDraw(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxArcReaderControl1.OnAfterScreenDraw
        Dim bEnabled As Boolean
        'Determine whether can undo/redo extent and enable/disbale control
        If AxArcReaderControl1.ARPageLayout.FocusARMap.CanUndoExtent = True Then bEnabled = True Else bEnabled = False
        cmdUndo.Enabled = bEnabled
        If AxArcReaderControl1.ARPageLayout.FocusARMap.CanRedoExtent = True Then bEnabled = True Else bEnabled = False
        cmdRedo.Enabled = bEnabled
    End Sub

    Private Sub AxArcReaderControl1_OnCurrentViewChanged(ByVal sender As Object, ByVal e As ESRI.ArcGIS.PublisherControls.IARControlEvents_OnCurrentViewChangedEvent) Handles AxArcReaderControl1.OnCurrentViewChanged
        Dim bEnabled As Boolean
        'Determine view type
        If AxArcReaderControl1.CurrentViewType = esriARViewType.esriARViewTypeNone Then
            bEnabled = False
        Else
            bEnabled = True
            'Update the current tool if necessary
            If AxArcReaderControl1.CurrentARTool <> pARTool Then
                AxArcReaderControl1.CurrentARTool = pARTool
            End If
        End If

        'Enable/ disable controls
        optTool0.Enabled = bEnabled
        optTool1.Enabled = bEnabled
        optTool2.Enabled = bEnabled
        cmdFullExtent.Enabled = bEnabled
    End Sub

    Private Sub MixedControls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTool0.Click, optTool1.Click, optTool2.Click

        Dim senderName As String
        senderName = sender.Name

        Select Case senderName
            'Set current tool
            Case "optTool0"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn
            Case "optTool1"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut
            Case "optTool2"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan
        End Select

        'Remember the current tool
        pARTool = AxArcReaderControl1.CurrentARTool

    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
    End Sub
End Class