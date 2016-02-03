Imports ESRI.ArcGIS.PublisherControls
Imports ESRI.ArcGIS

Public Class GlobeTools
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
  Friend WithEvents btnFullExtent As System.Windows.Forms.Button
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents optTool0 As System.Windows.Forms.RadioButton
  Friend WithEvents optTool1 As System.Windows.Forms.RadioButton
  Friend WithEvents optTool2 As System.Windows.Forms.RadioButton
  Friend WithEvents optTool3 As System.Windows.Forms.RadioButton
  Friend WithEvents AxArcReaderGlobeControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
  Friend WithEvents optTool4 As System.Windows.Forms.RadioButton
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GlobeTools))
    Me.btnLoad = New System.Windows.Forms.Button
    Me.btnFullExtent = New System.Windows.Forms.Button
    Me.optTool0 = New System.Windows.Forms.RadioButton
    Me.optTool1 = New System.Windows.Forms.RadioButton
    Me.optTool2 = New System.Windows.Forms.RadioButton
    Me.optTool3 = New System.Windows.Forms.RadioButton
    Me.optTool4 = New System.Windows.Forms.RadioButton
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
    Me.AxArcReaderGlobeControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
    CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnLoad
    '
    Me.btnLoad.Location = New System.Drawing.Point(8, 12)
    Me.btnLoad.Name = "btnLoad"
    Me.btnLoad.Size = New System.Drawing.Size(72, 44)
    Me.btnLoad.TabIndex = 0
    Me.btnLoad.Text = "Load"
    '
    'btnFullExtent
    '
    Me.btnFullExtent.Location = New System.Drawing.Point(448, 12)
    Me.btnFullExtent.Name = "btnFullExtent"
    Me.btnFullExtent.Size = New System.Drawing.Size(84, 44)
    Me.btnFullExtent.TabIndex = 1
    Me.btnFullExtent.Text = "Full Extent"
    '
    'optTool0
    '
    Me.optTool0.Appearance = System.Windows.Forms.Appearance.Button
    Me.optTool0.Location = New System.Drawing.Point(80, 12)
    Me.optTool0.Name = "optTool0"
    Me.optTool0.Size = New System.Drawing.Size(72, 44)
    Me.optTool0.TabIndex = 3
    Me.optTool0.Text = "Pan"
    Me.optTool0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'optTool1
    '
    Me.optTool1.Appearance = System.Windows.Forms.Appearance.Button
    Me.optTool1.Location = New System.Drawing.Point(152, 12)
    Me.optTool1.Name = "optTool1"
    Me.optTool1.Size = New System.Drawing.Size(72, 44)
    Me.optTool1.TabIndex = 4
    Me.optTool1.Text = "Pivot"
    Me.optTool1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'optTool2
    '
    Me.optTool2.Appearance = System.Windows.Forms.Appearance.Button
    Me.optTool2.Location = New System.Drawing.Point(224, 12)
    Me.optTool2.Name = "optTool2"
    Me.optTool2.Size = New System.Drawing.Size(72, 44)
    Me.optTool2.TabIndex = 5
    Me.optTool2.Text = "Navigate"
    Me.optTool2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'optTool3
    '
    Me.optTool3.Appearance = System.Windows.Forms.Appearance.Button
    Me.optTool3.Location = New System.Drawing.Point(296, 12)
    Me.optTool3.Name = "optTool3"
    Me.optTool3.Size = New System.Drawing.Size(72, 44)
    Me.optTool3.TabIndex = 6
    Me.optTool3.Text = "Target"
    Me.optTool3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'optTool4
    '
    Me.optTool4.Appearance = System.Windows.Forms.Appearance.Button
    Me.optTool4.Location = New System.Drawing.Point(368, 12)
    Me.optTool4.Name = "optTool4"
    Me.optTool4.Size = New System.Drawing.Size(84, 44)
    Me.optTool4.TabIndex = 7
    Me.optTool4.Text = "Zoom In\Out"
    Me.optTool4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'AxArcReaderGlobeControl1
    '
    Me.AxArcReaderGlobeControl1.Location = New System.Drawing.Point(14, 65)
    Me.AxArcReaderGlobeControl1.Name = "AxArcReaderGlobeControl1"
    Me.AxArcReaderGlobeControl1.OcxState = CType(resources.GetObject("AxArcReaderGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxArcReaderGlobeControl1.Size = New System.Drawing.Size(517, 368)
    Me.AxArcReaderGlobeControl1.TabIndex = 8
    '
    'GlobeTools
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(542, 442)
    Me.Controls.Add(Me.AxArcReaderGlobeControl1)
    Me.Controls.Add(Me.optTool4)
    Me.Controls.Add(Me.optTool3)
    Me.Controls.Add(Me.optTool2)
    Me.Controls.Add(Me.optTool1)
    Me.Controls.Add(Me.optTool0)
    Me.Controls.Add(Me.btnFullExtent)
    Me.Controls.Add(Me.btnLoad)
    Me.Name = "GlobeTools"
    Me.Text = "GlobeTools"
    CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Dim pARGlobeTool As esriARGlobeTool

  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'Disable controls
    optTool0.Enabled = False
    optTool1.Enabled = False
    optTool2.Enabled = False
    optTool3.Enabled = False
    optTool4.Enabled = False
    btnFullExtent.Enabled = False

  End Sub

  Private Sub MixedControls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTool0.Click, optTool1.Click, optTool2.Click, optTool3.Click, optTool4.Click

    Dim senderName As String
    senderName = sender.Name

    Select Case senderName
      'Set current tool
      Case "optTool0"
        AxArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolPan
      Case "optTool1"
        AxArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolPivot
      Case "optTool2"
        AxArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolNavigate
      Case "optTool3"
        AxArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolTarget
      Case "optTool4"
        AxArcReaderGlobeControl1.CurrentARGlobeTool = esriARGlobeTool.esriARGlobeToolZoomInOut
    End Select

    'Remember the current tool
    pARGlobeTool = AxArcReaderGlobeControl1.CurrentARGlobeTool

  End Sub

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

  End Sub

  Private Sub AxArcReaderGlobeControl1_OnDocumentLoaded(ByVal sender As Object, ByVal e As ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnDocumentLoadedEvent) Handles AxArcReaderGlobeControl1.OnDocumentLoaded

    'Enable Tools
    optTool0.Enabled = True
    optTool1.Enabled = True
    optTool2.Enabled = True
    optTool3.Enabled = True
    optTool4.Enabled = True
    btnFullExtent.Enabled = True

  End Sub

  Private Sub AxArcReaderGlobeControl1_OnDocumentUnloaded(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxArcReaderGlobeControl1.OnDocumentUnloaded

    'Enable Tools
    optTool0.Enabled = False
    optTool1.Enabled = False
    optTool2.Enabled = False
    optTool3.Enabled = False
    optTool4.Enabled = False
    btnFullExtent.Enabled = False

  End Sub

  Private Sub btnFullExtent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFullExtent.Click

    'Zoom to Full Extent
    AxArcReaderGlobeControl1.ARGlobe.ZoomToFullExtent()

  End Sub

  Private Sub GlobeTools_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

    'Release COM objects
    ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

  End Sub
End Class
