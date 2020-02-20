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
'Namespace AnimationDeveloperSamples
Partial Public Class frmCreateGraphicTrackOptions
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (Not components Is Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.label1 = New System.Windows.Forms.Label()
        Me.trackBar1 = New System.Windows.Forms.TrackBar()
        Me.textBoxTrackName = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.checkBoxOverwriteTrack = New System.Windows.Forms.CheckBox()
        Me.buttonImport = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.checkBoxReverseOrder = New System.Windows.Forms.CheckBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.checkBoxTracePath = New System.Windows.Forms.CheckBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.radioButtonLineGraphic = New System.Windows.Forms.RadioButton()
        Me.radioButtonLineFeature = New System.Windows.Forms.RadioButton()
        Me.helpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' label1
        ' 
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(11, 120)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(101, 13)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Simplification factor:"
        ' 
        ' trackBar1
        ' 
        Me.trackBar1.Location = New System.Drawing.Point(118, 114)
        Me.trackBar1.Name = "trackBar1"
        Me.trackBar1.Size = New System.Drawing.Size(174, 45)
        Me.trackBar1.TabIndex = 2
        Me.trackBar1.TabStop = False
        Me.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        ' 
        ' textBoxTrackName
        ' 
        Me.textBoxTrackName.Location = New System.Drawing.Point(83, 173)
        Me.textBoxTrackName.Name = "textBoxTrackName"
        Me.textBoxTrackName.Size = New System.Drawing.Size(184, 20)
        Me.textBoxTrackName.TabIndex = 4
        ' 
        ' label2
        ' 
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(10, 176)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(67, 13)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Track name:"
        ' 
        ' checkBoxOverwriteTrack
        ' 
        Me.checkBoxOverwriteTrack.AutoSize = True
        Me.checkBoxOverwriteTrack.Location = New System.Drawing.Point(13, 201)
        Me.checkBoxOverwriteTrack.Name = "checkBoxOverwriteTrack"
        Me.checkBoxOverwriteTrack.Size = New System.Drawing.Size(187, 17)
        Me.checkBoxOverwriteTrack.TabIndex = 6
        Me.checkBoxOverwriteTrack.Text = "Overwrite tracks of the same name"
        Me.checkBoxOverwriteTrack.UseVisualStyleBackColor = True
        ' 
        ' buttonImport
        ' 
        Me.buttonImport.Location = New System.Drawing.Point(142, 230)
        Me.buttonImport.Name = "buttonImport"
        Me.buttonImport.Size = New System.Drawing.Size(75, 23)
        Me.buttonImport.TabIndex = 7
        Me.buttonImport.Text = "Import"
        Me.buttonImport.UseVisualStyleBackColor = True
        '			Me.buttonImport.Click += New System.EventHandler(Me.buttonImport_Click);
        ' 
        ' buttonCancel
        ' 
        Me.buttonCancel.Location = New System.Drawing.Point(223, 230)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
        Me.buttonCancel.TabIndex = 8
        Me.buttonCancel.Text = "Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '			Me.buttonCancel.Click += New System.EventHandler(Me.buttonCancel_Click);
        ' 
        ' checkBoxReverseOrder
        ' 
        Me.checkBoxReverseOrder.AutoSize = True
        Me.checkBoxReverseOrder.Location = New System.Drawing.Point(6, 65)
        Me.checkBoxReverseOrder.Name = "checkBoxReverseOrder"
        Me.checkBoxReverseOrder.Size = New System.Drawing.Size(128, 17)
        Me.checkBoxReverseOrder.TabIndex = 9
        Me.checkBoxReverseOrder.Text = "Apply in reverse order"
        Me.checkBoxReverseOrder.UseVisualStyleBackColor = True
        ' 
        ' groupBox1
        ' 
        Me.groupBox1.Controls.Add(Me.checkBoxTracePath)
        Me.groupBox1.Controls.Add(Me.label4)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.radioButtonLineGraphic)
        Me.groupBox1.Controls.Add(Me.radioButtonLineFeature)
        Me.groupBox1.Controls.Add(Me.checkBoxReverseOrder)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.trackBar1)
        Me.groupBox1.Location = New System.Drawing.Point(6, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(298, 162)
        Me.groupBox1.TabIndex = 10
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Path source"
        ' 
        ' checkBoxTracePath
        ' 
        Me.checkBoxTracePath.AutoSize = True
        Me.checkBoxTracePath.Location = New System.Drawing.Point(6, 88)
        Me.checkBoxTracePath.Name = "checkBoxTracePath"
        Me.checkBoxTracePath.Size = New System.Drawing.Size(78, 17)
        Me.checkBoxTracePath.TabIndex = 14
        Me.checkBoxTracePath.Text = "Trace path"
        Me.checkBoxTracePath.UseVisualStyleBackColor = True
        ' 
        ' label4
        ' 
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(263, 98)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(29, 13)
        Me.label4.TabIndex = 13
        Me.label4.Text = "High"
        ' 
        ' label3
        ' 
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(115, 98)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(27, 13)
        Me.label3.TabIndex = 12
        Me.label3.Text = "Low"
        ' 
        ' radioButtonLineGraphic
        ' 
        Me.radioButtonLineGraphic.AutoSize = True
        Me.radioButtonLineGraphic.Enabled = False
        Me.radioButtonLineGraphic.Location = New System.Drawing.Point(7, 42)
        Me.radioButtonLineGraphic.Name = "radioButtonLineGraphic"
        Me.helpProvider1.SetShowHelp(Me.radioButtonLineGraphic, True)
        Me.radioButtonLineGraphic.Size = New System.Drawing.Size(124, 17)
        Me.radioButtonLineGraphic.TabIndex = 11
        Me.radioButtonLineGraphic.TabStop = True
        Me.radioButtonLineGraphic.Text = "Selected line graphic"
        Me.radioButtonLineGraphic.UseVisualStyleBackColor = True
        ' 
        ' radioButtonLineFeature
        ' 
        Me.radioButtonLineFeature.AutoSize = True
        Me.radioButtonLineFeature.Enabled = False
        Me.radioButtonLineFeature.Location = New System.Drawing.Point(7, 19)
        Me.radioButtonLineFeature.Name = "radioButtonLineFeature"
        Me.helpProvider1.SetShowHelp(Me.radioButtonLineFeature, True)
        Me.radioButtonLineFeature.Size = New System.Drawing.Size(122, 17)
        Me.radioButtonLineFeature.TabIndex = 10
        Me.radioButtonLineFeature.TabStop = True
        Me.radioButtonLineFeature.Text = "Selected line feature"
        Me.radioButtonLineFeature.UseVisualStyleBackColor = True
        ' 
        ' frmCreateGraphicTrackOptions
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 265)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonImport)
        Me.Controls.Add(Me.checkBoxOverwriteTrack)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.textBoxTrackName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreateGraphicTrackOptions"
        Me.Text = "Move Graphic along Path "
        '			Me.Load += New System.EventHandler(Me.frmCreateGraphicTrackOptions_Load);
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private label1 As System.Windows.Forms.Label
    Private trackBar1 As System.Windows.Forms.TrackBar
    Private textBoxTrackName As System.Windows.Forms.TextBox
    Private label2 As System.Windows.Forms.Label
    Private checkBoxOverwriteTrack As System.Windows.Forms.CheckBox
    Private WithEvents buttonImport As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button
    Private checkBoxReverseOrder As System.Windows.Forms.CheckBox
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private radioButtonLineGraphic As System.Windows.Forms.RadioButton
    Private radioButtonLineFeature As System.Windows.Forms.RadioButton
    Private label4 As System.Windows.Forms.Label
    Private label3 As System.Windows.Forms.Label
    Private checkBoxTracePath As System.Windows.Forms.CheckBox
    Private helpProvider1 As System.Windows.Forms.HelpProvider
End Class
'End Namespace