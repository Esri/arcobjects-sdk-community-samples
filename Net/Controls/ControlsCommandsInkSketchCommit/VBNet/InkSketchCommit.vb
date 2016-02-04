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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form

    'Tablet PC system metric value used by GetSystemMetrics to identify whether the application
    'is running on a Tablet PC.
    Private Const SM_TABLETPC As Integer = 86
    'The GetSystemMetrics function retrieves system metrics and system configuration settings.
    Declare Function GetSystemMetrics Lib "user32" Alias "GetSystemMetrics" (ByVal nIndex As Integer) As Integer
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

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
        m_EngineInkEnvironment = Nothing
        m_EngineInkEnvironmentEvents = Nothing
        m_Map = Nothing

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
  Friend WithEvents gpbInkSketch As System.Windows.Forms.GroupBox
  Friend WithEvents gpbReport As System.Windows.Forms.GroupBox
  Friend WithEvents lblCollectingStatus As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents tbxNumber As System.Windows.Forms.TextBox
  Friend WithEvents lblInfo As System.Windows.Forms.Label
  Friend WithEvents radManual As System.Windows.Forms.RadioButton
  Friend WithEvents radAutoGraphic As System.Windows.Forms.RadioButton
  Friend WithEvents radAutoText As System.Windows.Forms.RadioButton
  Friend WithEvents tbrAutoComplete As System.Windows.Forms.TrackBar
  Friend WithEvents lblAutoComplete As System.Windows.Forms.Label
  Friend WithEvents lbl1sec As System.Windows.Forms.Label
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
  Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
  Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
  Friend WithEvents lbl10sec As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.gpbInkSketch = New System.Windows.Forms.GroupBox
        Me.lbl10sec = New System.Windows.Forms.Label
        Me.lbl1sec = New System.Windows.Forms.Label
        Me.lblAutoComplete = New System.Windows.Forms.Label
        Me.tbrAutoComplete = New System.Windows.Forms.TrackBar
        Me.radAutoText = New System.Windows.Forms.RadioButton
        Me.radAutoGraphic = New System.Windows.Forms.RadioButton
        Me.radManual = New System.Windows.Forms.RadioButton
        Me.lblInfo = New System.Windows.Forms.Label
        Me.gpbReport = New System.Windows.Forms.GroupBox
        Me.tbxNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCollectingStatus = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.gpbInkSketch.SuspendLayout()
        CType(Me.tbrAutoComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpbReport.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gpbInkSketch
        '
        Me.gpbInkSketch.Controls.Add(Me.lbl10sec)
        Me.gpbInkSketch.Controls.Add(Me.lbl1sec)
        Me.gpbInkSketch.Controls.Add(Me.lblAutoComplete)
        Me.gpbInkSketch.Controls.Add(Me.tbrAutoComplete)
        Me.gpbInkSketch.Controls.Add(Me.radAutoText)
        Me.gpbInkSketch.Controls.Add(Me.radAutoGraphic)
        Me.gpbInkSketch.Controls.Add(Me.radManual)
        Me.gpbInkSketch.Controls.Add(Me.lblInfo)
        Me.gpbInkSketch.Location = New System.Drawing.Point(364, 32)
        Me.gpbInkSketch.Name = "gpbInkSketch"
        Me.gpbInkSketch.Size = New System.Drawing.Size(250, 317)
        Me.gpbInkSketch.TabIndex = 3
        Me.gpbInkSketch.TabStop = False
        Me.gpbInkSketch.Text = "Ink Sketch Commit Options"
        '
        'lbl10sec
        '
        Me.lbl10sec.Location = New System.Drawing.Point(173, 263)
        Me.lbl10sec.Name = "lbl10sec"
        Me.lbl10sec.Size = New System.Drawing.Size(54, 14)
        Me.lbl10sec.TabIndex = 7
        Me.lbl10sec.Text = "(10 sec)"
        '
        'lbl1sec
        '
        Me.lbl1sec.Location = New System.Drawing.Point(7, 263)
        Me.lbl1sec.Name = "lbl1sec"
        Me.lbl1sec.Size = New System.Drawing.Size(46, 14)
        Me.lbl1sec.TabIndex = 6
        Me.lbl1sec.Text = "(1 sec)"
        '
        'lblAutoComplete
        '
        Me.lblAutoComplete.Location = New System.Drawing.Point(20, 208)
        Me.lblAutoComplete.Name = "lblAutoComplete"
        Me.lblAutoComplete.Size = New System.Drawing.Size(220, 20)
        Me.lblAutoComplete.TabIndex = 5
        Me.lblAutoComplete.Text = "Automatically Commit the Ink Sketch after:"
        '
        'tbrAutoComplete
        '
        Me.tbrAutoComplete.Location = New System.Drawing.Point(7, 229)
        Me.tbrAutoComplete.Minimum = 1
        Me.tbrAutoComplete.Name = "tbrAutoComplete"
        Me.tbrAutoComplete.Size = New System.Drawing.Size(220, 42)
        Me.tbrAutoComplete.TabIndex = 4
        Me.tbrAutoComplete.Value = 1
        '
        'radAutoText
        '
        Me.radAutoText.Location = New System.Drawing.Point(13, 153)
        Me.radAutoText.Name = "radAutoText"
        Me.radAutoText.Size = New System.Drawing.Size(220, 34)
        Me.radAutoText.TabIndex = 3
        Me.radAutoText.Text = "Automatically Committed and Recognized as Text (Tablet PC only)"
        '
        'radAutoGraphic
        '
        Me.radAutoGraphic.Location = New System.Drawing.Point(13, 125)
        Me.radAutoGraphic.Name = "radAutoGraphic"
        Me.radAutoGraphic.Size = New System.Drawing.Size(207, 21)
        Me.radAutoGraphic.TabIndex = 2
        Me.radAutoGraphic.Text = "Automatically Committed to Graphic"
        '
        'radManual
        '
        Me.radManual.Location = New System.Drawing.Point(13, 97)
        Me.radManual.Name = "radManual"
        Me.radManual.Size = New System.Drawing.Size(180, 21)
        Me.radManual.TabIndex = 1
        Me.radManual.Text = "Manually Committed"
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(13, 35)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(220, 48)
        Me.lblInfo.TabIndex = 0
        Me.lblInfo.Text = "Ink sketches can be committed manually or automatically. Click on the buttons belo" & _
            "w to change the commit method."
        '
        'gpbReport
        '
        Me.gpbReport.Controls.Add(Me.tbxNumber)
        Me.gpbReport.Controls.Add(Me.Label1)
        Me.gpbReport.Controls.Add(Me.lblCollectingStatus)
        Me.gpbReport.Location = New System.Drawing.Point(364, 355)
        Me.gpbReport.Name = "gpbReport"
        Me.gpbReport.Size = New System.Drawing.Size(246, 98)
        Me.gpbReport.TabIndex = 4
        Me.gpbReport.TabStop = False
        Me.gpbReport.Text = "Sketch Report"
        '
        'tbxNumber
        '
        Me.tbxNumber.BackColor = System.Drawing.SystemColors.Control
        Me.tbxNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxNumber.Location = New System.Drawing.Point(147, 28)
        Me.tbxNumber.Name = "tbxNumber"
        Me.tbxNumber.ReadOnly = True
        Me.tbxNumber.Size = New System.Drawing.Size(93, 13)
        Me.tbxNumber.TabIndex = 2
        Me.tbxNumber.Text = "0"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Number of Ink Sketches = "
        '
        'lblCollectingStatus
        '
        Me.lblCollectingStatus.Location = New System.Drawing.Point(7, 62)
        Me.lblCollectingStatus.Name = "lblCollectingStatus"
        Me.lblCollectingStatus.Size = New System.Drawing.Size(233, 14)
        Me.lblCollectingStatus.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.85257!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.14743!))
        Me.TableLayoutPanel1.Controls.Add(Me.gpbInkSketch, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxMapControl1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AxToolbarControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.gpbReport, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AxLicenseControl1, 0, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.275862!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.72414!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(624, 456)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(3, 32)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(355, 317)
        Me.AxMapControl1.TabIndex = 7
        '
        'AxToolbarControl1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AxToolbarControl1, 2)
        Me.AxToolbarControl1.Location = New System.Drawing.Point(3, 3)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(618, 28)
        Me.AxToolbarControl1.TabIndex = 6
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(3, 355)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(638, 476)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximumSize = New System.Drawing.Size(646, 503)
        Me.MinimumSize = New System.Drawing.Size(646, 503)
        Me.Name = "Form1"
        Me.Text = "Ink Sketch Commit"
        Me.gpbInkSketch.ResumeLayout(False)
        Me.gpbInkSketch.PerformLayout()
        CType(Me.tbrAutoComplete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpbReport.ResumeLayout(False)
        Me.gpbReport.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

  Private m_EngineInkEnvironment As ESRI.ArcGIS.Controls.IEngineInkEnvironment
  Private WithEvents m_EngineInkEnvironmentEvents As ESRI.ArcGIS.Controls.EngineInkEnvironment
  Private m_Map As IMap

  Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'Set buddy control 
    AxToolbarControl1.SetBuddyControl(AxMapControl1)

    'Add items to the ToolbarControl
    AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsInkToolbar", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
    AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

    'Set the EngineInkEnviroment Singleton
    m_EngineInkEnvironment = New EngineInkEnvironmentClass
    m_EngineInkEnvironmentEvents = m_EngineInkEnvironment

    'Set the Ink Tool commit type to be manual
    m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeManual

    'Set the Form Controls 
    tbrAutoComplete.Enabled = False
    tbrAutoComplete.Minimum = 1
    tbrAutoComplete.Maximum = 10
    tbrAutoComplete.TickFrequency = 1
    tbrAutoComplete.TickStyle = TickStyle.BottomRight
    lblAutoComplete.Enabled = False
    lbl1sec.Enabled = False
    lbl10sec.Enabled = False
    lblCollectingStatus.Text = "Not Collecting Ink"
    tbxNumber.Text = "0"
    radManual.Checked = True

    'The radAutoText Radio button is only available on a Tablet PC.
    'Converting ink to text requires a Recognizer which can only 
    'run on Windows XP Tablet PC Edition.
    If IsRunningOnTabletPC() Then
      radAutoText.Enabled = True
    Else
      radAutoText.Enabled = False
    End If

  End Sub

  Private Sub tbrAutoComplete_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbrAutoComplete.MouseUp
    'Set the ToolCommitDelay using the value of the TrackBar
    m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value
  End Sub

  Private Sub m_EngineInkEnvironmentEvents_OnStop() Handles m_EngineInkEnvironmentEvents.OnStop
    'Report to the user the mode of the Ink Collector
    lblCollectingStatus.Text = "Not Collecting Ink Sketch"
  End Sub

  Private Sub m_EngineInkEnvironmentEvents_OnStart() Handles m_EngineInkEnvironmentEvents.OnStart

    'Report to the user the mode of the Ink Collector
    lblCollectingStatus.Text = "Collecting Ink Sketch"
  End Sub

  Private Sub m_EngineInkEnvironmentEvents_OnGesture(ByVal gestureType As ESRI.ArcGIS.Controls.esriEngineInkGesture, ByVal hotPoint As Object) Handles m_EngineInkEnvironmentEvents.OnGesture

    'Report to the user that a Gesture has been made
    lblCollectingStatus.Text = "Gesture Made Sketch"

  End Sub

  Private Sub AxMapControl1_OnAfterScreenDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent) Handles AxMapControl1.OnAfterScreenDraw

    'Report to the user the number of Ink Sketches that are present
    Dim pElement As IElement
    Dim pContainer As IGraphicsContainer
    Dim i As Integer = 0

    m_Map = AxMapControl1.Map
    pContainer = m_Map
    pContainer.Reset()
    pElement = pContainer.Next

    Do While Not pElement Is Nothing
      If TypeOf pElement Is InkGraphic Then
        i = i + 1
      End If
      pElement = pContainer.Next
    Loop
    tbxNumber.Text = i.ToString

  End Sub

  Private Sub radManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radManual.CheckedChanged

    'Manually committed ink sketch
    If radManual.Checked Then
      tbrAutoComplete.Enabled = False
      lblAutoComplete.Enabled = False
      lbl1sec.Enabled = False
      lbl10sec.Enabled = False
      m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeManual
    End If

  End Sub

  Private Sub radAutoGraphic_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radAutoGraphic.CheckedChanged

    'Automatically commit and save as ink graphic
    If radAutoGraphic.Checked Then
      tbrAutoComplete.Enabled = True
      lblAutoComplete.Enabled = True
      lbl1sec.Enabled = True
      lbl10sec.Enabled = True
      m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeAutoGraphic
      m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value
    End If

  End Sub

  Private Sub radAutoText_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radAutoText.CheckedChanged

        'Automatically commit and recognize as ink text
    'This is only available on a Tablet PC
    If radAutoText.Checked Then
      tbrAutoComplete.Enabled = True
      lblAutoComplete.Enabled = True
      lbl1sec.Enabled = True
      lbl10sec.Enabled = True
      m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeAutoText
      m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value
    End If

  End Sub

    Private Function IsRunningOnTabletPC() As Boolean

        ' Check to see if the application is running on a Tablet PC
        ' MSDN Help GetSystemMetrics(86) 
        ' Nonzero if the current operating system is the Windows XP Tablet PC edition,
        ' 0 (zero) if not.

        IsRunningOnTabletPC = False

        If GetSystemMetrics(SM_TABLETPC) <> 0 Then
            IsRunningOnTabletPC = True
        End If

    End Function
End Class
