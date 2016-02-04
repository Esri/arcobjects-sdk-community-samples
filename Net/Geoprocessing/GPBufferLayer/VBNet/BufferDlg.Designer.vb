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
Imports Microsoft.VisualBasic
Imports System

Partial Public Class BufferDlg
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
    Me.groupBox1 = New System.Windows.Forms.GroupBox()
    Me.btnBuffer = New System.Windows.Forms.Button()
    Me.btnOutputLayer = New System.Windows.Forms.Button()
    Me.txtOutputPath = New System.Windows.Forms.TextBox()
    Me.label2 = New System.Windows.Forms.Label()
    Me.cboUnits = New System.Windows.Forms.ComboBox()
    Me.txtBufferDistance = New System.Windows.Forms.TextBox()
    Me.label1 = New System.Windows.Forms.Label()
    Me.lblLayers = New System.Windows.Forms.Label()
    Me.cboLayers = New System.Windows.Forms.ComboBox()
    Me.groupBox2 = New System.Windows.Forms.GroupBox()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.txtMessages = New System.Windows.Forms.TextBox()
    Me.groupBox1.SuspendLayout()
    Me.groupBox2.SuspendLayout()
    Me.SuspendLayout()
    ' 
    ' groupBox1
    ' 
    Me.groupBox1.Controls.Add(Me.btnBuffer)
    Me.groupBox1.Controls.Add(Me.btnOutputLayer)
    Me.groupBox1.Controls.Add(Me.txtOutputPath)
    Me.groupBox1.Controls.Add(Me.label2)
    Me.groupBox1.Controls.Add(Me.cboUnits)
    Me.groupBox1.Controls.Add(Me.txtBufferDistance)
    Me.groupBox1.Controls.Add(Me.label1)
    Me.groupBox1.Controls.Add(Me.lblLayers)
    Me.groupBox1.Controls.Add(Me.cboLayers)
    Me.groupBox1.Location = New System.Drawing.Point(2, -1)
    Me.groupBox1.Name = "groupBox1"
    Me.groupBox1.Size = New System.Drawing.Size(356, 166)
    Me.groupBox1.TabIndex = 0
    Me.groupBox1.TabStop = False
    ' 
    ' btnBuffer
    ' 
    Me.btnBuffer.Location = New System.Drawing.Point(133, 130)
    Me.btnBuffer.Name = "btnBuffer"
    Me.btnBuffer.Size = New System.Drawing.Size(88, 23)
    Me.btnBuffer.TabIndex = 8
    Me.btnBuffer.Text = "Buffer"
    Me.btnBuffer.UseVisualStyleBackColor = True
    '	  Me.btnBuffer.Click += New System.EventHandler(Me.btnBuffer_Click);
    ' 
    ' btnOutputLayer
    ' 
    Me.btnOutputLayer.Location = New System.Drawing.Point(319, 86)
    Me.btnOutputLayer.Name = "btnOutputLayer"
    Me.btnOutputLayer.Size = New System.Drawing.Size(24, 23)
    Me.btnOutputLayer.TabIndex = 7
    Me.btnOutputLayer.Text = ">"
    Me.btnOutputLayer.UseVisualStyleBackColor = True
    '	  Me.btnOutputLayer.Click += New System.EventHandler(Me.btnOutputLayer_Click);
    ' 
    ' txtOutputPath
    ' 
    Me.txtOutputPath.Location = New System.Drawing.Point(93, 88)
    Me.txtOutputPath.Name = "txtOutputPath"
    Me.txtOutputPath.ReadOnly = True
    Me.txtOutputPath.Size = New System.Drawing.Size(224, 20)
    Me.txtOutputPath.TabIndex = 6
    ' 
    ' label2
    ' 
    Me.label2.AutoSize = True
    Me.label2.Location = New System.Drawing.Point(7, 88)
    Me.label2.Name = "label2"
    Me.label2.Size = New System.Drawing.Size(67, 13)
    Me.label2.TabIndex = 5
    Me.label2.Text = "Output layer:"
    ' 
    ' cboUnits
    ' 
    Me.cboUnits.FormattingEnabled = True
    Me.cboUnits.Items.AddRange(New Object() {"Unknown", "Inches", "Points", "Feet", "Yards", "Miles", "NauticalMiles", "Millimeters", "Centimeters", "Meters", "Kilometers", "DecimalDegrees", "Decimeters"})
    Me.cboUnits.Location = New System.Drawing.Point(158, 51)
    Me.cboUnits.Name = "cboUnits"
    Me.cboUnits.Size = New System.Drawing.Size(118, 21)
    Me.cboUnits.TabIndex = 4
    ' 
    ' txtBufferDistance
    ' 
    Me.txtBufferDistance.Location = New System.Drawing.Point(93, 51)
    Me.txtBufferDistance.Name = "txtBufferDistance"
    Me.txtBufferDistance.Size = New System.Drawing.Size(55, 20)
    Me.txtBufferDistance.TabIndex = 3
    Me.txtBufferDistance.Text = "0.1"
    ' 
    ' label1
    ' 
    Me.label1.AutoSize = True
    Me.label1.Location = New System.Drawing.Point(7, 54)
    Me.label1.Name = "label1"
    Me.label1.Size = New System.Drawing.Size(81, 13)
    Me.label1.TabIndex = 2
    Me.label1.Text = "Buffer distance:"
    ' 
    ' lblLayers
    ' 
    Me.lblLayers.AutoSize = True
    Me.lblLayers.Location = New System.Drawing.Point(7, 19)
    Me.lblLayers.Name = "lblLayers"
    Me.lblLayers.Size = New System.Drawing.Size(36, 13)
    Me.lblLayers.TabIndex = 1
    Me.lblLayers.Text = "Layer:"
    ' 
    ' cboLayers
    ' 
    Me.cboLayers.FormattingEnabled = True
    Me.cboLayers.Location = New System.Drawing.Point(93, 19)
    Me.cboLayers.Name = "cboLayers"
    Me.cboLayers.Size = New System.Drawing.Size(250, 21)
    Me.cboLayers.TabIndex = 0
    ' 
    ' groupBox2
    ' 
    Me.groupBox2.Controls.Add(Me.txtMessages)
    Me.groupBox2.Location = New System.Drawing.Point(2, 167)
    Me.groupBox2.Name = "groupBox2"
    Me.groupBox2.Size = New System.Drawing.Size(356, 146)
    Me.groupBox2.TabIndex = 1
    Me.groupBox2.TabStop = False
    Me.groupBox2.Text = "Messages"
    ' 
    ' btnCancel
    ' 
    Me.btnCancel.Location = New System.Drawing.Point(135, 319)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(88, 23)
    Me.btnCancel.TabIndex = 2
    Me.btnCancel.Text = "Dismiss"
    Me.btnCancel.UseVisualStyleBackColor = True
    '	  Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
    ' 
    ' txtMessages
    ' 
    Me.txtMessages.Location = New System.Drawing.Point(6, 15)
    Me.txtMessages.Multiline = True
    Me.txtMessages.Name = "txtMessages"
    Me.txtMessages.ReadOnly = True
    Me.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtMessages.Size = New System.Drawing.Size(344, 125)
    Me.txtMessages.TabIndex = 0
    ' 
    ' BufferDlg
    ' 
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(361, 347)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.groupBox2)
    Me.Controls.Add(Me.groupBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "BufferDlg"
    Me.ShowInTaskbar = False
    Me.Text = "Buffer"
    Me.TopMost = True
    '	  Me.Load += New System.EventHandler(Me.bufferDlg_Load);
    Me.groupBox1.ResumeLayout(False)
    Me.groupBox1.PerformLayout()
    Me.groupBox2.ResumeLayout(False)
    Me.groupBox2.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private groupBox1 As System.Windows.Forms.GroupBox
  Private cboLayers As System.Windows.Forms.ComboBox
  Private cboUnits As System.Windows.Forms.ComboBox
  Private txtBufferDistance As System.Windows.Forms.TextBox
  Private label1 As System.Windows.Forms.Label
  Private lblLayers As System.Windows.Forms.Label
  Private WithEvents btnOutputLayer As System.Windows.Forms.Button
  Private txtOutputPath As System.Windows.Forms.TextBox
  Private label2 As System.Windows.Forms.Label
  Private groupBox2 As System.Windows.Forms.GroupBox
  Private WithEvents btnCancel As System.Windows.Forms.Button
  Private WithEvents btnBuffer As System.Windows.Forms.Button
  Private txtMessages As System.Windows.Forms.TextBox
End Class