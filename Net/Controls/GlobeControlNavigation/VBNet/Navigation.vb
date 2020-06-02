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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.controls
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS


Public Class frmGlobeNavigation
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

        Application.Run(New frmGlobeNavigation())
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
    Public WithEvents chkSpin As System.Windows.Forms.CheckBox
    Public WithEvents cmdLoadDocument As System.Windows.Forms.Button
    Public WithEvents cmdZoomOut As System.Windows.Forms.Button
    Public WithEvents cmdZoomIn As System.Windows.Forms.Button
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblNavigate As System.Windows.Forms.Label
    Public WithEvents lblZoom As System.Windows.Forms.Label
    Public WithEvents lblLoad As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Public WithEvents optTools1 As System.Windows.Forms.RadioButton
    Public WithEvents optTools0 As System.Windows.Forms.RadioButton
    Friend WithEvents AxGlobeControl1 As ESRI.ArcGIS.Controls.AxGlobeControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGlobeNavigation))
        Me.chkSpin = New System.Windows.Forms.CheckBox
        Me.optTools1 = New System.Windows.Forms.RadioButton
        Me.optTools0 = New System.Windows.Forms.RadioButton
        Me.cmdLoadDocument = New System.Windows.Forms.Button
        Me.cmdZoomOut = New System.Windows.Forms.Button
        Me.cmdZoomIn = New System.Windows.Forms.Button
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblNavigate = New System.Windows.Forms.Label
        Me.lblZoom = New System.Windows.Forms.Label
        Me.lblLoad = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.AxGlobeControl1 = New ESRI.ArcGIS.Controls.AxGlobeControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkSpin
        '
        Me.chkSpin.BackColor = System.Drawing.SystemColors.Control
        Me.chkSpin.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSpin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSpin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSpin.Location = New System.Drawing.Point(496, 408)
        Me.chkSpin.Name = "chkSpin"
        Me.chkSpin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSpin.Size = New System.Drawing.Size(57, 17)
        Me.chkSpin.TabIndex = 12
        Me.chkSpin.Text = "Spin"
        '
        'optTools1
        '
        Me.optTools1.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTools1.BackColor = System.Drawing.SystemColors.Control
        Me.optTools1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTools1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTools1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTools1.Location = New System.Drawing.Point(488, 328)
        Me.optTools1.Name = "optTools1"
        Me.optTools1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTools1.Size = New System.Drawing.Size(81, 25)
        Me.optTools1.TabIndex = 6
        Me.optTools1.TabStop = True
        Me.optTools1.Text = "Zoom In/Out"
        Me.optTools1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optTools0
        '
        Me.optTools0.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTools0.BackColor = System.Drawing.SystemColors.Control
        Me.optTools0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTools0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTools0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTools0.Location = New System.Drawing.Point(488, 288)
        Me.optTools0.Name = "optTools0"
        Me.optTools0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTools0.Size = New System.Drawing.Size(81, 25)
        Me.optTools0.TabIndex = 5
        Me.optTools0.TabStop = True
        Me.optTools0.Text = "Navigate"
        Me.optTools0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdLoadDocument
        '
        Me.cmdLoadDocument.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadDocument.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadDocument.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadDocument.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadDocument.Location = New System.Drawing.Point(496, 48)
        Me.cmdLoadDocument.Name = "cmdLoadDocument"
        Me.cmdLoadDocument.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadDocument.Size = New System.Drawing.Size(67, 25)
        Me.cmdLoadDocument.TabIndex = 4
        Me.cmdLoadDocument.Text = "Load ..."
        '
        'cmdZoomOut
        '
        Me.cmdZoomOut.BackColor = System.Drawing.SystemColors.Control
        Me.cmdZoomOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdZoomOut.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdZoomOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdZoomOut.Location = New System.Drawing.Point(480, 168)
        Me.cmdZoomOut.Name = "cmdZoomOut"
        Me.cmdZoomOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdZoomOut.Size = New System.Drawing.Size(99, 25)
        Me.cmdZoomOut.TabIndex = 3
        Me.cmdZoomOut.Text = "Fixed Zoom Out"
        '
        'cmdZoomIn
        '
        Me.cmdZoomIn.BackColor = System.Drawing.SystemColors.Control
        Me.cmdZoomIn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdZoomIn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdZoomIn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdZoomIn.Location = New System.Drawing.Point(480, 136)
        Me.cmdZoomIn.Name = "cmdZoomIn"
        Me.cmdZoomIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdZoomIn.Size = New System.Drawing.Size(99, 25)
        Me.cmdZoomIn.TabIndex = 2
        Me.cmdZoomIn.Text = "Fixed Zoom In"
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.Location = New System.Drawing.Point(480, 200)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(99, 25)
        Me.cmdFullExtent.TabIndex = 1
        Me.cmdFullExtent.Text = "Full Extent"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(480, 360)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(97, 49)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Control the spin speed with the slider."
        '
        'lblNavigate
        '
        Me.lblNavigate.BackColor = System.Drawing.SystemColors.Control
        Me.lblNavigate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNavigate.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNavigate.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblNavigate.Location = New System.Drawing.Point(480, 232)
        Me.lblNavigate.Name = "lblNavigate"
        Me.lblNavigate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNavigate.Size = New System.Drawing.Size(105, 49)
        Me.lblNavigate.TabIndex = 9
        Me.lblNavigate.Text = "Use the option buttons to select a navigation tool. "
        '
        'lblZoom
        '
        Me.lblZoom.BackColor = System.Drawing.SystemColors.Control
        Me.lblZoom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblZoom.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZoom.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblZoom.Location = New System.Drawing.Point(480, 80)
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblZoom.Size = New System.Drawing.Size(97, 49)
        Me.lblZoom.TabIndex = 8
        Me.lblZoom.Text = "Use the buttons to navigate the Globe data."
        '
        'lblLoad
        '
        Me.lblLoad.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoad.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoad.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblLoad.Location = New System.Drawing.Point(480, 8)
        Me.lblLoad.Name = "lblLoad"
        Me.lblLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLoad.Size = New System.Drawing.Size(97, 33)
        Me.lblLoad.TabIndex = 7
        Me.lblLoad.Text = "Browse to a 3dd file to load."
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(480, 432)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(104, 45)
        Me.TrackBar1.TabIndex = 14
        '
        'AxGlobeControl1
        '
        Me.AxGlobeControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxGlobeControl1.Name = "AxGlobeControl1"
        Me.AxGlobeControl1.OcxState = CType(resources.GetObject("AxGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGlobeControl1.Size = New System.Drawing.Size(464, 448)
        Me.AxGlobeControl1.TabIndex = 15
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(256, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 16
        '
        'frmGlobeNavigation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(593, 465)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxGlobeControl1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.chkSpin)
        Me.Controls.Add(Me.optTools1)
        Me.Controls.Add(Me.optTools0)
        Me.Controls.Add(Me.cmdLoadDocument)
        Me.Controls.Add(Me.cmdZoomOut)
        Me.Controls.Add(Me.cmdZoomIn)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblNavigate)
        Me.Controls.Add(Me.lblZoom)
        Me.Controls.Add(Me.lblLoad)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(182, 132)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGlobeNavigation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Globe Navigation"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private m_pActiveView As ISceneViewer
    Private m_pCamera As ICamera
    Private m_pMousePos As IPoint
    Private m_bMouseDown As Boolean
    Private m_bZooming As Boolean
    Private m_dSpinSpeed As Double
    Private m_dZoom As Double

    Const cMinZoom As Double = 1.00002
    Const cMaxZoom As Double = 1.1
    Const cDistanceZoomLimit As Double = 200.0#

    Private Sub frmGlobeNavigation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Initialize member variables
        m_bZooming = False
        m_dSpinSpeed = 0
        m_pMousePos = New Point
        m_pActiveView = AxGlobeControl1.GlobeDisplay.ActiveViewer
        m_pCamera = m_pActiveView.Camera

    End Sub

    Private Sub cmdLoadDocument_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadDocument.Click
        On Error GoTo load_err

        OpenFileDialog1.Filter = "Globe Files (*.3dd)|*.3dd"
        OpenFileDialog1.ShowDialog()

        'Check a file is selected and that it's a valid Globe document
        'before attempting to load it
        If Not OpenFileDialog1.FileName = "" Then
            If Not AxGlobeControl1.Check3dFile(OpenFileDialog1.FileName) Then
                MsgBox("Cannot load " & OpenFileDialog1.FileName, MsgBoxStyle.Exclamation)
            Else
                AxGlobeControl1.Load3dFile(OpenFileDialog1.FileName)
            End If
        End If

        Exit Sub
load_err:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        Dim pCamera As IGlobeCamera
        pCamera = m_pCamera

        'Make sure that the camera is using global coordinates
        pCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal

        'Point the camera at 0,0 in lat,long - this is the default target
        Dim pTarget As IPoint
        pTarget = New PointClass
        pTarget.PutCoords(0.0#, 0.0#)
        pTarget.Z = 0.0#
        m_pCamera.Target = pTarget

        'Reset the camera to its default values
        m_pCamera.Azimuth = 140
        m_pCamera.Inclination = 45
        m_pCamera.ViewingDistance = 4.0#
        m_pCamera.ViewFieldAngle = 30.0#
        m_pCamera.RollAngle = 0.0#
        m_pCamera.RecalcUp()
        m_pActiveView.Redraw(False)

    End Sub

    Private Sub cmdZoomIn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdZoomIn.Click

        'Reset the camera field of view angle to 0.9 of its previous value
        Dim vfa As Double
        vfa = m_pCamera.ViewFieldAngle
        m_pCamera.ViewFieldAngle = vfa * 0.9
        m_pActiveView.Redraw(False)

    End Sub

    Private Sub cmdZoomOut_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdZoomOut.Click

        'Reset the camera field of view angle to 1.1 times its previous value
        Dim vfa As Double
        vfa = m_pCamera.ViewFieldAngle
        m_pCamera.ViewFieldAngle = vfa * 1.1
        m_pActiveView.Redraw(False)

    End Sub

    Private Sub CalculateMoveFactors(ByRef dist As Double)

        Dim bIsAtCenter As Boolean
        Dim indexGlobe As Integer
        Dim pGlobeViewer As IGlobeViewer

        'See if the camera is pointing at the center of the globe.
        pGlobeViewer = m_pActiveView
        pGlobeViewer.GetIsTargetAtCenter(bIsAtCenter, indexGlobe)

        'If the camera is pointing at the center of the globe then the zoom speed
        'depends on how far away it is, otherwise the zoom factor is fixed. As the
        'camera approaches the globe surface (where dist = 1) the zoom speed slows
        'down. The other factors were worked out by trial and error.
        If bIsAtCenter Then
            m_dZoom = cMinZoom + (cMaxZoom - cMinZoom) * (dist - 1.0#) / 3.0#
        Else
            m_dZoom = 1.1
        End If

    End Sub

    Private Sub chkSpin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSpin.Click

        If chkSpin.CheckState Then
            AxGlobeControl1.GlobeViewer.StartSpinning(esriGlobeSpinDirection.esriCounterClockwise)
            AxGlobeControl1.GlobeViewer.SpinSpeed = 3
            TrackBar1.Enabled = True
            m_dSpinSpeed = 3
        Else
            AxGlobeControl1.GlobeViewer.StopSpinning()
            TrackBar1.Enabled = False
            m_dSpinSpeed = 0
        End If

    End Sub

    Private Sub MixedControls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTools0.Click, optTools1.Click
        Dim radioButton As RadioButton = sender
        Select Case radioButton.Name
            'Set current tool
            Case "optTools0"
                m_bZooming = False
                AxGlobeControl1.Navigate = True
                AxGlobeControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
            Case "optTools1"
                m_bZooming = True
                AxGlobeControl1.Navigate = False
                AxGlobeControl1.MousePointer = esriControlsMousePointer.esriPointerZoom
        End Select
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll

        Dim sliderPos As Short

        'The globe should increase its spin speed with every increment greater than
        '5 and decrease it for every increment less.
        sliderPos = (TrackBar1.Value - 5)
        If sliderPos = 0 Then
            m_dSpinSpeed = 3
        ElseIf sliderPos > 0 Then
            m_dSpinSpeed = 3 * (sliderPos + 1)
        Else
            m_dSpinSpeed = 3 / (1 - sliderPos)
        End If
        AxGlobeControl1.GlobeViewer.SpinSpeed = m_dSpinSpeed

    End Sub

    Private Sub AxGlobeControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseDownEvent) Handles AxGlobeControl1.OnMouseDown

        'Mouse down should initiate a zoom only if the Zoom check box is checked
        If Not m_bZooming Then Exit Sub

        If e.button = 1 Then
            m_bMouseDown = True
            m_pMousePos.X = e.x
            m_pMousePos.Y = e.y
        End If

    End Sub

    Private Sub AxGlobeControl1_OnMouseMove(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseMoveEvent) Handles AxGlobeControl1.OnMouseMove

        If Not m_bMouseDown Then Exit Sub

        'If we're in a zoom operation then
        'Get the difference in mouse position between this and the last one
        Dim dx, dy As Integer
        dx = e.x - m_pMousePos.X
        dy = e.y - m_pMousePos.Y
        If dx = 0 And dy = 0 Then Exit Sub

        'Work out how far the observer is currently from the globe and use this
        'distance to determine how far to move
        Dim pObserver As IPoint
        pObserver = m_pCamera.Observer
        Dim zObs, xObs, yObs, dist As Double
        pObserver.QueryCoords(xObs, yObs)
        zObs = pObserver.Z
        dist = System.Math.Sqrt(xObs * xObs + yObs * yObs + zObs * zObs)
        CalculateMoveFactors(dist)

        'Zoom out and in as the mouse moves up and down the screen respectively
        If dy < 0 And dist < cDistanceZoomLimit Then
            m_pCamera.Zoom(m_dZoom)
        ElseIf dy > 0 Then
            m_pCamera.Zoom((1 / m_dZoom))
        End If

        m_pMousePos.X = e.x
        m_pMousePos.Y = e.y
        m_pActiveView.Redraw(False)

    End Sub


    Private Sub AxGlobeControl1_OnMouseUp(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseUpEvent) Handles AxGlobeControl1.OnMouseUp

        'Cancel the zoom operation
        m_bMouseDown = False
        'Navigate can cancel spin - make sure it starts again
        If m_dSpinSpeed > 0 Then
            AxGlobeControl1.GlobeViewer.StartSpinning(esriGlobeSpinDirection.esriCounterClockwise)
        End If

    End Sub
End Class