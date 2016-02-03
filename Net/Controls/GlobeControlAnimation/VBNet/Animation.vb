Option Strict Off
Option Explicit On

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GlobeCore
Imports System.Security.Permissions
Imports ESRI.ArcGIS


Friend Class frmGlbCntrl
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

        Application.Run(New frmGlbCntrl())
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
	Public WithEvents CmdPlay As System.Windows.Forms.Button
	Public WithEvents txtDuration As System.Windows.Forms.TextBox
	Public WithEvents TxtFrequency As System.Windows.Forms.TextBox
	Public WithEvents OptDuration As System.Windows.Forms.RadioButton
	Public WithEvents OptIteration As System.Windows.Forms.RadioButton
	Public WithEvents CmdLoad As System.Windows.Forms.Button
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents FrmAnim As System.Windows.Forms.GroupBox
	Public WithEvents lblStatus As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxLicenseControl2 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxGlobeControl1 As ESRI.ArcGIS.Controls.AxGlobeControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGlbCntrl))
        Me.FrmAnim = New System.Windows.Forms.GroupBox
        Me.CmdPlay = New System.Windows.Forms.Button
        Me.txtDuration = New System.Windows.Forms.TextBox
        Me.TxtFrequency = New System.Windows.Forms.TextBox
        Me.OptDuration = New System.Windows.Forms.RadioButton
        Me.OptIteration = New System.Windows.Forms.RadioButton
        Me.CmdLoad = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxGlobeControl1 = New ESRI.ArcGIS.Controls.AxGlobeControl
        Me.AxLicenseControl2 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.FrmAnim.SuspendLayout()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FrmAnim
        '
        Me.FrmAnim.BackColor = System.Drawing.SystemColors.Control
        Me.FrmAnim.Controls.Add(Me.CmdPlay)
        Me.FrmAnim.Controls.Add(Me.txtDuration)
        Me.FrmAnim.Controls.Add(Me.TxtFrequency)
        Me.FrmAnim.Controls.Add(Me.OptDuration)
        Me.FrmAnim.Controls.Add(Me.OptIteration)
        Me.FrmAnim.Controls.Add(Me.CmdLoad)
        Me.FrmAnim.Controls.Add(Me.Label1)
        Me.FrmAnim.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrmAnim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrmAnim.Location = New System.Drawing.Point(143, 433)
        Me.FrmAnim.Name = "FrmAnim"
        Me.FrmAnim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrmAnim.Size = New System.Drawing.Size(461, 55)
        Me.FrmAnim.TabIndex = 3
        Me.FrmAnim.TabStop = False
        '
        'CmdPlay
        '
        Me.CmdPlay.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPlay.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPlay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPlay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPlay.Location = New System.Drawing.Point(266, 10)
        Me.CmdPlay.Name = "CmdPlay"
        Me.CmdPlay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPlay.Size = New System.Drawing.Size(81, 38)
        Me.CmdPlay.TabIndex = 9
        Me.CmdPlay.Text = "Play Animation"
        '
        'txtDuration
        '
        Me.txtDuration.AcceptsReturn = True
        Me.txtDuration.AutoSize = False
        Me.txtDuration.BackColor = System.Drawing.SystemColors.Window
        Me.txtDuration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDuration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDuration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDuration.Location = New System.Drawing.Point(220, 13)
        Me.txtDuration.MaxLength = 0
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDuration.Size = New System.Drawing.Size(40, 19)
        Me.txtDuration.TabIndex = 8
        Me.txtDuration.Text = "10"
        '
        'TxtFrequency
        '
        Me.TxtFrequency.AcceptsReturn = True
        Me.TxtFrequency.AutoSize = False
        Me.TxtFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtFrequency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFrequency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtFrequency.Location = New System.Drawing.Point(400, 13)
        Me.TxtFrequency.MaxLength = 0
        Me.TxtFrequency.Name = "TxtFrequency"
        Me.TxtFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtFrequency.Size = New System.Drawing.Size(40, 19)
        Me.TxtFrequency.TabIndex = 7
        Me.TxtFrequency.Text = "2"
        '
        'OptDuration
        '
        Me.OptDuration.BackColor = System.Drawing.SystemColors.Control
        Me.OptDuration.Checked = True
        Me.OptDuration.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptDuration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptDuration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDuration.Location = New System.Drawing.Point(122, 8)
        Me.OptDuration.Name = "OptDuration"
        Me.OptDuration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptDuration.Size = New System.Drawing.Size(98, 17)
        Me.OptDuration.TabIndex = 6
        Me.OptDuration.TabStop = True
        Me.OptDuration.Text = "Duration (secs)"
        '
        'OptIteration
        '
        Me.OptIteration.BackColor = System.Drawing.SystemColors.Control
        Me.OptIteration.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptIteration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptIteration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptIteration.Location = New System.Drawing.Point(122, 22)
        Me.OptIteration.Name = "OptIteration"
        Me.OptIteration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptIteration.Size = New System.Drawing.Size(94, 19)
        Me.OptIteration.TabIndex = 5
        Me.OptIteration.TabStop = True
        Me.OptIteration.Text = "No. Iterations"
        '
        'CmdLoad
        '
        Me.CmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.CmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdLoad.Location = New System.Drawing.Point(9, 10)
        Me.CmdLoad.Name = "CmdLoad"
        Me.CmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdLoad.Size = New System.Drawing.Size(94, 38)
        Me.CmdLoad.TabIndex = 4
        Me.CmdLoad.Text = "Load Animation  File (*.aga)"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(354, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(36, 17)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Cycles:"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(6, 439)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(129, 54)
        Me.lblStatus.TabIndex = 11
        '
        'AxGlobeControl1
        '
        Me.AxGlobeControl1.Location = New System.Drawing.Point(144, 40)
        Me.AxGlobeControl1.Name = "AxGlobeControl1"
        Me.AxGlobeControl1.OcxState = CType(resources.GetObject("AxGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGlobeControl1.Size = New System.Drawing.Size(456, 392)
        Me.AxGlobeControl1.TabIndex = 12
        '
        'AxLicenseControl2
        '
        Me.AxLicenseControl2.Enabled = True
        Me.AxLicenseControl2.Location = New System.Drawing.Point(152, 368)
        Me.AxLicenseControl2.Name = "AxLicenseControl2"
        Me.AxLicenseControl2.OcxState = CType(resources.GetObject("AxLicenseControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl2.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl2.TabIndex = 13
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(136, 392)
        Me.AxTOCControl1.TabIndex = 14
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(592, 28)
        Me.AxToolbarControl1.TabIndex = 15
        '
        'frmGlbCntrl
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(609, 494)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.AxLicenseControl2)
        Me.Controls.Add(Me.AxGlobeControl1)
        Me.Controls.Add(Me.FrmAnim)
        Me.Controls.Add(Me.lblStatus)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmGlbCntrl"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "GlobeControlAnimation"
        Me.FrmAnim.ResumeLayout(False)
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region 

    Private m_sAnimFilePath As String

	Private Sub CmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdLoad.Click

        OpenFileDialog1.Title = "Open ArcGlobe Animation File"
        OpenFileDialog1.Filter = "ArcGlobe Animation Files (*.aga)|*.aga"
        'set ArcGlobeAnimaton path folder as default path...
        If Not m_sAnimFilePath = "" Then
            OpenFileDialog1.InitialDirectory = m_sAnimFilePath
        Else
            OpenFileDialog1.InitialDirectory = Application.StartupPath
        End If
        OpenFileDialog1.ShowDialog()

        'if the user selected an animation file
        Dim sAnimFilePath As String
        sAnimFilePath = OpenFileDialog1.FileName
        If sAnimFilePath = "" Then Exit Sub

        'sAnimFilePath
        Dim pBasicScene As IBasicScene
        Dim pGlobe As IGlobe
        pGlobe = AxGlobeControl1.Globe
        pBasicScene = pglobe
        pBasicScene.LoadAnimation(sAnimFilePath)

        'if loading of the animation succeeded, enable player controls...
        'Enable Animation Player controls...
        OptDuration.Enabled = True
        OptIteration.Enabled = True
        txtDuration.Enabled = True
        TxtFrequency.Enabled = True
        TxtFrequency.Enabled = True
        CmdPlay.Enabled = True
		
    End Sub

    Private Sub frmGlbCntrl_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim pBasicScene As IBasicScene
        Dim pGlobe As IGlobe

        'check and load if the animation file is present...
        m_sAnimFilePath = "..\..\..\..\..\Data\ArcGlobeAnimation\AnimationSample.aga"
        If System.IO.File.Exists(m_sAnimFilePath) Then
            'Load the sample animation file into the animation file into the doc...
            pGlobe = AxGlobeControl1.Globe
            pBasicScene = pGlobe
            pBasicScene.LoadAnimation(m_sAnimFilePath)
        Else
            'Disable Animation Player controls...
            OptDuration.Enabled = False
            OptIteration.Enabled = False
            txtDuration.Enabled = False
            TxtFrequency.Enabled = False
            TxtFrequency.Enabled = False
            CmdPlay.Enabled = False
        End If
        Icon = Nothing

    End Sub

    Private Sub CmdPlay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPlay.Click

        Dim duration As Double, numCycle As Integer
        Dim iteration As Integer, cycles As Integer

        'allows to handle when non integer character is entered in txtDuration.Text or TxtFrequency.Text
        On Error Resume Next

        If OptDuration.Checked Then
            duration = CInt(txtDuration.Text)
            numCycle = CInt(TxtFrequency.Text)
            'play the animation via duration
            PlayAnimation(duration, numCycle)
        Else
            iteration = CInt(txtDuration.Text)
            cycles = CInt(TxtFrequency.Text)
            'play animation via iteration...
            PlayAnimationFast(cycles, iteration)
        End If

    End Sub

    Private Sub PlayAnimation(ByRef duration As Double, ByRef numCycles As Integer)

        Dim pGlobe As IGlobe
        Dim pTracks As IAnimationTracks
        Dim viewers3D As IViewers3D

        pGlobe = AxGlobeControl1.Globe
        pTracks = pGlobe
        viewers3D = pGlobe.GlobeDisplay

        'exit if document doesn't contain animation..
        Dim sError As String
        If pTracks.TrackCount = 0 Then
            sError = m_sAnimFilePath
            If sError = "" Then
                sError = "To get a Sample animation file, Developer Kit Samples need to be installed!"
                System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." & Chr(13) & sError)
            Else
                System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." & Chr(13) & "Load " & m_sAnimFilePath & "\AnimationSample.aga for sample.")
            End If
            Exit Sub
        End If

        Dim startTime As DateTime, timeSpan As TimeSpan
        Dim elapsedTime As Integer, i As Integer, j As Integer

        For i = 1 To numCycles Step 1
            startTime = DateTime.Now
            j = 0
            Do
                timeSpan = DateTime.Now().Subtract(startTime)
                elapsedTime = timeSpan.TotalSeconds
                If (elapsedTime > duration) Then
                    elapsedTime = duration
                End If
                pTracks.ApplyTracks(Nothing, elapsedTime, duration)
                viewers3D.RefreshViewers()
                j = j + 1
            Loop While elapsedTime < duration
        Next i

    End Sub

    Private Sub PlayAnimationFast(Optional ByVal cycles As Integer = 0, Optional ByVal iteration As Integer = 0)

        Dim pGlobe As IGlobe
        Dim pGlobeDisplay As IGlobeDisplay
        Dim pScene As Scene
        Dim pSceneTracks As IAnimationTracks

        pGlobe = AxGlobeControl1.Globe
        pGlobeDisplay = pGlobe.GlobeDisplay
        pScene = pGlobeDisplay.Scene
        pSceneTracks = pScene

        Dim pCollection As Collection
        Dim pTrackCamArray As IArray
        Dim pTrackGlbArray As IArray
        Dim pTrackLyrArray As IArray

        pCollection = New Collection
        pTrackCamArray = New Array
        pTrackGlbArray = New Array
        pTrackLyrArray = New Array

        Dim sError As String
        If pSceneTracks.TrackCount = 0 Then
            sError = m_sAnimFilePath
            If sError = "" Then
                sError = "To get a Sample animation file, Developer Kit Samples need to be installed!"
                System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." & Chr(13) & sError)
            Else
                System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." & Chr(13) & "Load " & m_sAnimFilePath & "\AnimationSample.aga for sample.")
            End If
            Exit Sub
        End If

        Dim i As Integer, count() As Integer, k As Object
        Dim pTrack As IAnimationTrack
        Dim pTrackLayer As IAnimationTrack
        Dim pTrackGlobe As IAnimationTrack = Nothing
        Dim pAnimType As IAnimationType
        Dim pAnimLayer As IAnimationType
        Dim pAnimGlobeCam As IAnimationType = Nothing
        Dim pKFGlbCam As IKeyframe
        Dim pKFGlbLayer As IKeyframe

        'get each track from the scene and store tracks of the same kind in an Array
        For i = 0 To pSceneTracks.TrackCount - 1
            pTrack = pSceneTracks.Tracks.Element(i)
            ReDim Preserve count(i)
            k = i
            pAnimType = pTrack.AnimationType

            If pAnimType.CLSID.Value = "{7CCBA704-3933-4D7A-8E89-4DFEE88AA937}" Then
                'GlobeLayer
                pTrackLayer = New AnimationTrackClass
                pTrackLayer = pTrack
                pTrackLayer.AnimationType = pAnimType
                pKFGlbLayer = New GlobeLayerKeyframeClass
                pAnimLayer = pAnimType
                'Store the keyframe count of each track in an array
                count(i) = pTrackLayer.KeyframeCount
                pTrackLyrArray.Add(pTrackLayer)
            ElseIf pAnimType.CLSID.Value = "{D4565495-E2F9-4D89-A8A7-D0B69FD7A424}" Then
                'Globe Camera type
                pTrackGlobe = New AnimationTrackClass
                pTrackGlobe = pTrack
                pTrackGlobe.AnimationType = pAnimType
                pKFGlbCam = New GlobeCameraKeyframeClass
                pAnimGlobeCam = pAnimType
                'Store the keyframe count of each track in an array
                count(i) = pTrackGlobe.KeyframeCount
                pTrackGlbArray.Add(pTrackGlobe)
            Else
                System.Windows.Forms.MessageBox.Show("Animation Type " & pAnimType.Name & " Not Supported. Check if the animation File is Valid!")
                Exit Sub
            End If
        Next

        Dim larger As Integer
        larger = Greatest(count)
        ' if nothing gets passed by the argument it takes the max no of keyframes
        If IsNothing(iteration) Then iteration = larger


        Dim start As Integer, j As Integer, t As Integer
        Dim keyFrameCount As Integer, keyFrameCameraCount As Integer, keyFrameLayerCount As Integer
        Dim time As Double
        Dim pTrackCamera As IAnimationTrack
        Dim pAnimCam As IAnimationType = Nothing
        Dim pKFBkmark As IKeyframe

        For i = 1 To cycles 'Total number of cycles
            For start = 0 To iteration ' no of iterations...
                For j = 0 To pTrackCamArray.Count - 1
                    pTrackCamera = pTrackCamArray.Element(j)
                    If Not pTrackCamera Is Nothing Then
                        If time >= pTrackCamera.BeginTime Then
                            keyFrameCameraCount = pTrackGlobe.KeyframeCount
                            pKFBkmark = pTrackCamera.Keyframe(keyFrameCameraCount - keyFrameCameraCount)
                            'reset object
                            pAnimCam.ResetObject(pScene, pKFBkmark)
                            ' interpolate by using track
                            pTrackCamera.InterpolateObjectProperties(pScene, time)
                            keyFrameCameraCount = keyFrameCameraCount - 1
                        End If
                    End If
                Next
                For k = 0 To pTrackGlbArray.Count - 1
                    pTrackGlobe = pTrackGlbArray.Element(k)
                    If Not pTrackGlobe Is Nothing Then
                        If time >= pTrackGlobe.BeginTime Then
                            keyFrameCount = pTrackGlobe.KeyframeCount
                            pKFGlbCam = pTrackGlobe.Keyframe(pTrackGlobe.KeyframeCount - keyFrameCount)
                            'reset object
                            pAnimGlobeCam.ResetObject(pScene, pKFGlbCam)
                            ' interpolate by using track
                            pTrackGlobe.InterpolateObjectProperties(pScene, time)
                            keyFrameCount = keyFrameCount - 1
                        End If
                    End If
                Next

                For t = 0 To pTrackLyrArray.Count - 1
                    pTrackLayer = pTrackLyrArray.Element(t)
                    If Not pTrackLayer Is Nothing Then
                        If time >= pTrackLayer.BeginTime Then
                            keyFrameLayerCount = pTrackLayer.KeyframeCount
                            pKFGlbLayer = pTrackLayer.Keyframe(pTrackLayer.KeyframeCount - keyFrameLayerCount)
                            'interpolate by using track
                            pTrackLayer.InterpolateObjectProperties(pScene, time)
                            keyFrameLayerCount = keyFrameLayerCount - 1
                        End If
                    End If
                Next
                'reset interpolation Point
                time = start / iteration
                'refresh the globeviewer(s)
                pGlobeDisplay.RefreshViewers()
            Next start
        Next

    End Sub

    Private Function Greatest(ByRef pArray() As Integer) As Integer

        ' Function to find the largest count of keyframes (in any one of the Animation tracks)
        Dim iLength As Integer, i As Integer, iMax As Integer
        iLength = UBound(pArray)
        ReDim Preserve pArray(iLength)
        For i = 0 To iLength - 1
            If IsNothing(iMax) Then
                iMax = pArray(i)
            ElseIf pArray(i) > iMax Then
                iMax = pArray(i)
            End If
        Next
        Greatest = iMax

    End Function

    Private Sub OptDuration_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptDuration.CheckedChanged

        If eventSender.Checked Then
            txtDuration.Text = CStr(10) 'set default values
            TxtFrequency.Text = CStr(2) 'set Default values
        End If

    End Sub

    Private Sub OptIteration_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptIteration.CheckedChanged

        If eventSender.Checked Then
            'Set Default cycle and iteration..
            txtDuration.Text = CStr(500)
            TxtFrequency.Text = CStr(2)
        End If

    End Sub

    Private Function routin_ReadRegistry(ByRef sKey As String) As String

        'Open the subkey for reading
        'Dim rk As Microsoft.Win32.RegistryKey
        'rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sKey, True)
        'If rk Is Nothing Then routin_ReadRegistry = ""
        'Get the data from a specified item in the key.
        Dim installationFolder As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
        'routin_ReadRegistry = rk.GetValue("InstallDir")
        routin_ReadRegistry = installationFolder


    End Function

    Private Sub AxToolbarControl1_OnItemClick(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent) Handles AxToolbarControl1.OnItemClick
        'Display help when fly tool and walk tools are selected...
        'check if key intercept is enabled,if not enable it.
        If Not AxGlobeControl1.KeyIntercept = 1 Then AxGlobeControl1.KeyIntercept = 1

        Dim puid As UID
        puid = AxToolbarControl1.GetItem(e.index).UID
        'uid for fly tool={2C327C42-8CA9-4FC3-8C7B-F6290680FABB}
        'uid for walk tool={56C3BDD4-C51A-4574-8954-D3E1F5F54E57}

        If puid.Value = "{2C327C42-8CA9-4FC3-8C7B-F6290680FABB}" Then
            'fly...
            lblStatus.Text = "Use arrow up or arrow left keys to decelerate and arrow up or arrow left keys to accelerate."
            'Fly tool
        ElseIf puid.Value = "{56C3BDD4-C51A-4574-8954-D3E1F5F54E57}" Then
            'walk...
            lblStatus.Text = "Use arrow up or down keys to change elevation and the arrow left or right keys to fine-tune travel speed."
        Else
            lblStatus.Text = ""
        End If
    End Sub
End Class