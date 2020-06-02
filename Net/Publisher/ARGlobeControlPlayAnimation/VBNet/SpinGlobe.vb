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
Imports ESRI.ArcGIS

Public Class SpinGlobe
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
    Friend WithEvents btnAntiClockwise As System.Windows.Forms.Button
    Friend WithEvents btnClockwise As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents lblSlower As System.Windows.Forms.Label
    Friend WithEvents lblFaster As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents AxArcReaderGlobeControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpinGlobe))
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnAntiClockwise = New System.Windows.Forms.Button
        Me.btnClockwise = New System.Windows.Forms.Button
        Me.btnStop = New System.Windows.Forms.Button
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.lblSlower = New System.Windows.Forms.Label
        Me.lblFaster = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxArcReaderGlobeControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderGlobeControl
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(8, 8)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(76, 36)
        Me.btnLoad.TabIndex = 1
        Me.btnLoad.Text = "Load"
        '
        'btnAntiClockwise
        '
        Me.btnAntiClockwise.Location = New System.Drawing.Point(596, 8)
        Me.btnAntiClockwise.Name = "btnAntiClockwise"
        Me.btnAntiClockwise.Size = New System.Drawing.Size(76, 36)
        Me.btnAntiClockwise.TabIndex = 2
        Me.btnAntiClockwise.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnClockwise
        '
        Me.btnClockwise.Location = New System.Drawing.Point(428, 8)
        Me.btnClockwise.Name = "btnClockwise"
        Me.btnClockwise.Size = New System.Drawing.Size(76, 36)
        Me.btnClockwise.TabIndex = 3
        Me.btnClockwise.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(512, 8)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(76, 36)
        Me.btnStop.TabIndex = 4
        Me.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(196, 4)
        Me.TrackBar1.Maximum = 1000
        Me.TrackBar1.Minimum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(164, 45)
        Me.TrackBar1.TabIndex = 5
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Value = 100
        '
        'lblSlower
        '
        Me.lblSlower.Location = New System.Drawing.Point(364, 8)
        Me.lblSlower.Name = "lblSlower"
        Me.lblSlower.Size = New System.Drawing.Size(40, 20)
        Me.lblSlower.TabIndex = 6
        Me.lblSlower.Text = "Slower"
        '
        'lblFaster
        '
        Me.lblFaster.Location = New System.Drawing.Point(152, 8)
        Me.lblFaster.Name = "lblFaster"
        Me.lblFaster.Size = New System.Drawing.Size(40, 20)
        Me.lblFaster.TabIndex = 7
        Me.lblFaster.Text = "Faster"
        '
        'Timer1
        '
        '
        'AxArcReaderGlobeControl1
        '
        Me.AxArcReaderGlobeControl1.Location = New System.Drawing.Point(10, 52)
        Me.AxArcReaderGlobeControl1.Name = "AxArcReaderGlobeControl1"
        Me.AxArcReaderGlobeControl1.OcxState = CType(resources.GetObject("AxArcReaderGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxArcReaderGlobeControl1.Size = New System.Drawing.Size(662, 367)
        Me.AxArcReaderGlobeControl1.TabIndex = 8
        '
        'SpinGlobe
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(688, 430)
        Me.Controls.Add(Me.AxArcReaderGlobeControl1)
        Me.Controls.Add(Me.lblFaster)
        Me.Controls.Add(Me.lblSlower)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnClockwise)
        Me.Controls.Add(Me.btnAntiClockwise)
        Me.Controls.Add(Me.btnLoad)
        Me.Name = "SpinGlobe"
        Me.Text = "SpinGlobe"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxArcReaderGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    'Variable that indicates direction of rotation
    'Clockwise = True; AntiClockwise = False
    Dim m_RotateDirection As Boolean
    Dim m_TimerInterval As Long
    Dim m_CurLat As Double
    Dim m_CurLong As Double
    Dim m_CurElev As Double
    Dim i As Double

    Private Sub SpinGlobe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Load option/command button images
        Dim pImage As System.Drawing.Image
        Dim pBitmap As System.Drawing.Bitmap
        pImage = New System.Drawing.Bitmap(GetType(SpinGlobe).Assembly.GetManifestResourceStream(GetType(SpinGlobe), "spin_counterclockwise.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        btnAntiClockwise.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(SpinGlobe).Assembly.GetManifestResourceStream(GetType(SpinGlobe), "spin_clockwise.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        btnClockwise.Image = pImage
        pImage = New System.Drawing.Bitmap(GetType(SpinGlobe).Assembly.GetManifestResourceStream(GetType(SpinGlobe), "spin_stop.bmp"))
        pBitmap = pImage
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        btnStop.Image = pImage

        'Set the current Slider value and timer interval to 100 milliseconds
        'Any faster and the Globe will not be able to refresh fast enough
        TrackBar1.Value = 100
        Timer1.Interval = 100
        Timer1.Enabled = False
        i = 0

        'Disable controls until doc is loaded
        btnAntiClockwise.Enabled = False
        btnClockwise.Enabled = False
        btnStop.Enabled = False

    End Sub

    Private Sub AxArcReaderGlobeControl1_OnMouseUp(ByVal sender As Object, ByVal e As ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnMouseUpEvent)

        'Update m_CurLong incase observer has been repositioned as a consequence of using another tool.
        AxArcReaderGlobeControl1.ARGlobe.GetObserverLocation(m_CurLong, m_CurLat, m_CurElev)

    End Sub

    Private Sub btnAntiClockwise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAntiClockwise.Click

        'Get latest location and start timer
        AxArcReaderGlobeControl1.ARGlobe.GetObserverLocation(m_CurLong, m_CurLat, m_CurElev)
        m_RotateDirection = True
        Timer1.Enabled = True

        'Unselect the current tool
        AxArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNoneSelected

    End Sub

    Private Sub btnClockwise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockwise.Click

        'Get latest location and start timer
        AxArcReaderGlobeControl1.ARGlobe.GetObserverLocation(m_CurLong, m_CurLat, m_CurElev)
        m_RotateDirection = False
        Timer1.Enabled = True

        'Unselect the current tool
        AxArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNoneSelected

    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click

        'Stops Timer
        Timer1.Enabled = False

        'Set the current tool to Globe Navigate
        AxArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNavigate

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

        'Zoom to Full Extent
        AxArcReaderGlobeControl1.ARGlobe.ZoomToFullExtent()

        'Set current tool to Globe Navigate
        AxArcReaderGlobeControl1.CurrentARGlobeTool = ESRI.ArcGIS.PublisherControls.esriARGlobeTool.esriARGlobeToolNavigate


    End Sub


    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged

        'Update the timer interval to match the slider value
        Timer1.Interval = TrackBar1.Value

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        'Longitude Counter
        i = m_CurLong

        'Increment Longitude by 1 decimal degree
        If m_RotateDirection = False Then
            If i = 360 Then
                i = 0
            End If
            i = i + 1
        Else
            If i = -360 Then
                i = 0
            End If
            i = i - 1
        End If

        'Update the current location
        AxArcReaderGlobeControl1.ARGlobe.SetObserverLocation(i, m_CurLat, m_CurElev)
        AxArcReaderGlobeControl1.ARGlobe.GetObserverLocation(m_CurLong, m_CurLat, m_CurElev)

    End Sub

    Private Sub AxArcReaderGlobeControl1_OnDocumentUnloaded(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxArcReaderGlobeControl1.OnDocumentUnloaded

        btnAntiClockwise.Enabled = False
        btnClockwise.Enabled = False
        btnStop.Enabled = False

    End Sub

    Private Sub AxArcReaderGlobeControl1_OnDocumentLoaded(ByVal sender As Object, ByVal e As ESRI.ArcGIS.PublisherControls.IARGlobeControlEvents_OnDocumentLoadedEvent) Handles AxArcReaderGlobeControl1.OnDocumentLoaded

        btnAntiClockwise.Enabled = True
        btnClockwise.Enabled = True
        btnStop.Enabled = True

    End Sub
End Class
