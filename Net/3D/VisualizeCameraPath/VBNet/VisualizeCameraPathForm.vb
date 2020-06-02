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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace VisualizeCameraPath
  ''' <summary>
  ''' Summary description for VisualizeCameraPathForm.
  ''' </summary>
  Public Class VisualizeCameraPathForm : Inherits System.Windows.Forms.Form
#Region "Member Variables"

    Public label1 As System.Windows.Forms.Label
    Public panel1 As System.Windows.Forms.Panel
    Public label2 As System.Windows.Forms.Label
    Public label3 As System.Windows.Forms.Label
    Public playButton As System.Windows.Forms.Button
    Public generatePathButton As System.Windows.Forms.Button
    Public stopButton As System.Windows.Forms.Button
    Public animTracksListBox As System.Windows.Forms.ListBox
    Public WithEvents generateCamPathCheckBox As System.Windows.Forms.CheckBox
    Private groupBox2 As System.Windows.Forms.GroupBox
    Private groupBox3 As System.Windows.Forms.GroupBox
    Public ptsPerSecRadioButton As System.Windows.Forms.RadioButton
    Public ptsBtwnKframeRadioButton As System.Windows.Forms.RadioButton
    Public numPtsPerSecTextBox As System.Windows.Forms.TextBox
    Public ptsBtwnKframeTextBox As System.Windows.Forms.TextBox
    Public animDurationTextBox As System.Windows.Forms.TextBox
    Public camToTargetDirectionCheckBox As System.Windows.Forms.CheckBox
    Public symbolTypeListBox As System.Windows.Forms.ListBox
    Private components As System.ComponentModel.IContainer = Nothing
#End Region

#Region "Constructor/Dispose"

    Public Sub New()
      InitializeComponent()
      'load symbol types
      loadSymbolTypes()
    End Sub

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Me.label1 = New System.Windows.Forms.Label()
      Me.panel1 = New System.Windows.Forms.Panel()
      Me.generateCamPathCheckBox = New System.Windows.Forms.CheckBox()
      Me.groupBox2 = New System.Windows.Forms.GroupBox()
      Me.animDurationTextBox = New System.Windows.Forms.TextBox()
      Me.animTracksListBox = New System.Windows.Forms.ListBox()
      Me.label3 = New System.Windows.Forms.Label()
      Me.stopButton = New System.Windows.Forms.Button()
      Me.playButton = New System.Windows.Forms.Button()
      Me.groupBox3 = New System.Windows.Forms.GroupBox()
      Me.generatePathButton = New System.Windows.Forms.Button()
      Me.ptsPerSecRadioButton = New System.Windows.Forms.RadioButton()
      Me.numPtsPerSecTextBox = New System.Windows.Forms.TextBox()
      Me.ptsBtwnKframeRadioButton = New System.Windows.Forms.RadioButton()
      Me.ptsBtwnKframeTextBox = New System.Windows.Forms.TextBox()
      Me.camToTargetDirectionCheckBox = New System.Windows.Forms.CheckBox()
      Me.symbolTypeListBox = New System.Windows.Forms.ListBox()
      Me.label2 = New System.Windows.Forms.Label()
      Me.panel1.SuspendLayout()
      Me.groupBox2.SuspendLayout()
      Me.groupBox3.SuspendLayout()
      Me.SuspendLayout()
      ' 
      ' label1
      ' 
      Me.label1.Location = New System.Drawing.Point(16, 24)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(120, 16)
      Me.label1.TabIndex = 0
      Me.label1.Text = "Select Camera Track:"
      ' 
      ' panel1
      ' 
      Me.panel1.Controls.Add(Me.generateCamPathCheckBox)
      Me.panel1.Controls.Add(Me.groupBox2)
      Me.panel1.Controls.Add(Me.groupBox3)
      Me.panel1.Location = New System.Drawing.Point(0, 0)
      Me.panel1.Name = "panel1"
      Me.panel1.Size = New System.Drawing.Size(328, 344)
      Me.panel1.TabIndex = 1
      ' 
      ' generateCamPathCheckBox
      ' 
      Me.generateCamPathCheckBox.Location = New System.Drawing.Point(16, 136)
      Me.generateCamPathCheckBox.Name = "generateCamPathCheckBox"
      Me.generateCamPathCheckBox.Size = New System.Drawing.Size(176, 16)
      Me.generateCamPathCheckBox.TabIndex = 3
      Me.generateCamPathCheckBox.Text = "Generate Camera path"
'			Me.generateCamPathCheckBox.CheckedChanged += New System.EventHandler(Me.generateCamPathCheckBox_CheckedChanged);
      ' 
      ' groupBox2
      ' 
      Me.groupBox2.Controls.Add(Me.animDurationTextBox)
      Me.groupBox2.Controls.Add(Me.label1)
      Me.groupBox2.Controls.Add(Me.animTracksListBox)
      Me.groupBox2.Controls.Add(Me.label3)
      Me.groupBox2.Controls.Add(Me.stopButton)
      Me.groupBox2.Controls.Add(Me.playButton)
      Me.groupBox2.Location = New System.Drawing.Point(8, 8)
      Me.groupBox2.Name = "groupBox2"
      Me.groupBox2.Size = New System.Drawing.Size(312, 120)
      Me.groupBox2.TabIndex = 2
      Me.groupBox2.TabStop = False
      ' 
      ' animDurationTextBox
      ' 
      Me.animDurationTextBox.Location = New System.Drawing.Point(168, 56)
      Me.animDurationTextBox.Name = "animDurationTextBox"
      Me.animDurationTextBox.Size = New System.Drawing.Size(136, 20)
      Me.animDurationTextBox.TabIndex = 6
      Me.animDurationTextBox.Text = "10"
      ' 
      ' animTracksListBox
      ' 
      Me.animTracksListBox.Location = New System.Drawing.Point(168, 16)
      Me.animTracksListBox.Name = "animTracksListBox"
      Me.animTracksListBox.Size = New System.Drawing.Size(136, 30)
      Me.animTracksListBox.TabIndex = 1
      ' 
      ' label3
      ' 
      Me.label3.Location = New System.Drawing.Point(16, 56)
      Me.label3.Name = "label3"
      Me.label3.Size = New System.Drawing.Size(136, 16)
      Me.label3.TabIndex = 5
      Me.label3.Text = "Animation Duration (sec):"
      ' 
      ' stopButton
      ' 
      Me.stopButton.Enabled = False
      Me.stopButton.Location = New System.Drawing.Point(168, 88)
      Me.stopButton.Name = "stopButton"
      Me.stopButton.Size = New System.Drawing.Size(88, 23)
      Me.stopButton.TabIndex = 7
      Me.stopButton.Text = "Stop"
      ' 
      ' playButton
      ' 
      Me.playButton.Location = New System.Drawing.Point(48, 88)
      Me.playButton.Name = "playButton"
      Me.playButton.Size = New System.Drawing.Size(88, 23)
      Me.playButton.TabIndex = 2
      Me.playButton.Text = "Play"
      ' 
      ' groupBox3
      ' 
      Me.groupBox3.Controls.Add(Me.generatePathButton)
      Me.groupBox3.Controls.Add(Me.ptsPerSecRadioButton)
      Me.groupBox3.Controls.Add(Me.numPtsPerSecTextBox)
      Me.groupBox3.Controls.Add(Me.ptsBtwnKframeRadioButton)
      Me.groupBox3.Controls.Add(Me.ptsBtwnKframeTextBox)
      Me.groupBox3.Controls.Add(Me.camToTargetDirectionCheckBox)
      Me.groupBox3.Controls.Add(Me.symbolTypeListBox)
      Me.groupBox3.Controls.Add(Me.label2)
      Me.groupBox3.Enabled = False
      Me.groupBox3.Location = New System.Drawing.Point(8, 160)
      Me.groupBox3.Name = "groupBox3"
      Me.groupBox3.Size = New System.Drawing.Size(312, 176)
      Me.groupBox3.TabIndex = 2
      Me.groupBox3.TabStop = False
      Me.groupBox3.Text = "Camera path options"
      ' 
      ' generatePathButton
      ' 
      Me.generatePathButton.Enabled = False
      Me.generatePathButton.Location = New System.Drawing.Point(112, 144)
      Me.generatePathButton.Name = "generatePathButton"
      Me.generatePathButton.Size = New System.Drawing.Size(88, 23)
      Me.generatePathButton.TabIndex = 8
      Me.generatePathButton.Text = "Generate Path"
      ' 
      ' ptsPerSecRadioButton
      ' 
      Me.ptsPerSecRadioButton.Checked = True
      Me.ptsPerSecRadioButton.Location = New System.Drawing.Point(16, 24)
      Me.ptsPerSecRadioButton.Name = "ptsPerSecRadioButton"
      Me.ptsPerSecRadioButton.Size = New System.Drawing.Size(176, 16)
      Me.ptsPerSecRadioButton.TabIndex = 7
      Me.ptsPerSecRadioButton.TabStop = True
      Me.ptsPerSecRadioButton.Text = "Points per second :"
      ' 
      ' numPtsPerSecTextBox
      ' 
      Me.numPtsPerSecTextBox.Location = New System.Drawing.Point(232, 16)
      Me.numPtsPerSecTextBox.Name = "numPtsPerSecTextBox"
      Me.numPtsPerSecTextBox.Size = New System.Drawing.Size(72, 20)
      Me.numPtsPerSecTextBox.TabIndex = 6
      Me.numPtsPerSecTextBox.Text = ""
      ' 
      ' ptsBtwnKframeRadioButton
      ' 
      Me.ptsBtwnKframeRadioButton.Location = New System.Drawing.Point(16, 48)
      Me.ptsBtwnKframeRadioButton.Name = "ptsBtwnKframeRadioButton"
      Me.ptsBtwnKframeRadioButton.Size = New System.Drawing.Size(208, 16)
      Me.ptsBtwnKframeRadioButton.TabIndex = 8
      Me.ptsBtwnKframeRadioButton.Text = "Points between keyframe positions :"
      ' 
      ' ptsBtwnKframeTextBox
      ' 
      Me.ptsBtwnKframeTextBox.Location = New System.Drawing.Point(232, 48)
      Me.ptsBtwnKframeTextBox.Name = "ptsBtwnKframeTextBox"
      Me.ptsBtwnKframeTextBox.Size = New System.Drawing.Size(72, 20)
      Me.ptsBtwnKframeTextBox.TabIndex = 9
      Me.ptsBtwnKframeTextBox.Text = ""
      ' 
      ' camToTargetDirectionCheckBox
      ' 
      Me.camToTargetDirectionCheckBox.Location = New System.Drawing.Point(16, 120)
      Me.camToTargetDirectionCheckBox.Name = "camToTargetDirectionCheckBox"
      Me.camToTargetDirectionCheckBox.Size = New System.Drawing.Size(160, 16)
      Me.camToTargetDirectionCheckBox.TabIndex = 4
      Me.camToTargetDirectionCheckBox.Text = "Camera to Target direction"
      ' 
      ' symbolTypeListBox
      ' 
      Me.symbolTypeListBox.Items.AddRange(New Object() {"Cone", "Sphere", "Tetrahedron", "Diamond", "Cylinder", "Cube"})
      Me.symbolTypeListBox.Location = New System.Drawing.Point(168, 80)
      Me.symbolTypeListBox.Name = "symbolTypeListBox"
      Me.symbolTypeListBox.Size = New System.Drawing.Size(136, 30)
      Me.symbolTypeListBox.TabIndex = 3
      ' 
      ' label2
      ' 
      Me.label2.Location = New System.Drawing.Point(16, 88)
      Me.label2.Name = "label2"
      Me.label2.Size = New System.Drawing.Size(112, 16)
      Me.label2.TabIndex = 2
      Me.label2.Text = "Select Symbol Type:"
      ' 
      ' VisualizeCameraPathForm
      ' 
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(328, 342)
      Me.Controls.Add(Me.panel1)
      Me.Name = "VisualizeCameraPathForm"
      Me.Text = "Trace Camera Path"
      Me.TopMost = True
      Me.panel1.ResumeLayout(False)
      Me.groupBox2.ResumeLayout(False)
      Me.groupBox3.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Custom Functions/Event Handlers"

    Private Sub generateCamPathCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles generateCamPathCheckBox.CheckedChanged
      If generateCamPathCheckBox.Checked = True Then
      groupBox3.Enabled = True
      ElseIf generateCamPathCheckBox.Checked = False Then
      groupBox3.Enabled = False
      End If
    End Sub

    Public Sub loadSymbolTypes()
      'first clear collection and then load
      symbolTypeListBox.Items.Clear()
      symbolTypeListBox.Items.Add("Cone")
      symbolTypeListBox.Items.Add("Sphere")
      symbolTypeListBox.Items.Add("Tetrahedron")
      symbolTypeListBox.Items.Add("Diamond")
      symbolTypeListBox.Items.Add("Cylinder")
      symbolTypeListBox.Items.Add("Cube")
    End Sub

#End Region

  End Class

End Namespace