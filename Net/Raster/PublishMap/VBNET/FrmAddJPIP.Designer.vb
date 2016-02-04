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
Partial Class FrmAddJPIP
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
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
        Me.txtJPIPUrl = New System.Windows.Forms.TextBox()
        Me.button1 = New System.Windows.Forms.Button()
        Me.button2 = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtLayerName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtJPIPUrl
        '
        Me.txtJPIPUrl.AcceptsReturn = True
        Me.txtJPIPUrl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJPIPUrl.Location = New System.Drawing.Point(85, 24)
        Me.txtJPIPUrl.Name = "txtJPIPUrl"
        Me.txtJPIPUrl.Size = New System.Drawing.Size(271, 20)
        Me.txtJPIPUrl.TabIndex = 1
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(139, 126)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(55, 23)
        Me.button1.TabIndex = 3
        Me.button1.Text = "Connect"
        Me.button1.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Location = New System.Drawing.Point(231, 126)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(57, 23)
        Me.button2.TabIndex = 4
        Me.button2.Text = "Cancel"
        Me.button2.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 27)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(57, 13)
        Me.label1.TabIndex = 3
        Me.label1.Text = "JPIP URL:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 93)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(67, 13)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Layer Name:"
        '
        'txtLayerName
        '
        Me.txtLayerName.AcceptsReturn = True
        Me.txtLayerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtLayerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtLayerName.Location = New System.Drawing.Point(85, 90)
        Me.txtLayerName.Name = "txtLayerName"
        Me.txtLayerName.Size = New System.Drawing.Size(271, 20)
        Me.txtLayerName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Example:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(82, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(213, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "jpip://myserver:8080/JP2Server/imagealias"
        '
        'FrmAddJPIP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 162)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.txtLayerName)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button1)
        Me.Controls.Add(Me.txtJPIPUrl)
        Me.Name = "FrmAddJPIP"
        Me.Text = "Add a JPIP Connection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	#End Region

    Private WithEvents txtJPIPUrl As System.Windows.Forms.TextBox
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents button2 As System.Windows.Forms.Button
    Private label1 As System.Windows.Forms.Label
    Private label2 As System.Windows.Forms.Label
    Private txtLayerName As System.Windows.Forms.TextBox
    Private Label3 As System.Windows.Forms.Label
    Private Label4 As System.Windows.Forms.Label
End Class
