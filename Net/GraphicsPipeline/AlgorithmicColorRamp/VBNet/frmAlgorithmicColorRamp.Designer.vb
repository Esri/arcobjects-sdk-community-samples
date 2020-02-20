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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAlgorithmicColorRamp
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents chkShowColors As System.Windows.Forms.CheckBox
    Public WithEvents cmdEnumColorsFirst As System.Windows.Forms.Button
	Public WithEvents cmdEnumColorsNext As System.Windows.Forms.Button
    Public WithEvents fraColors As System.Windows.Forms.GroupBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmbAlgorithm As System.Windows.Forms.ComboBox
    Public WithEvents txtEndColor As System.Windows.Forms.TextBox
	Public WithEvents txtStartColor As System.Windows.Forms.TextBox
	Public WithEvents lblStartColor As System.Windows.Forms.Label
	Public WithEvents lblEndColor As System.Windows.Forms.Label
	Public WithEvents lblAlgorithm As System.Windows.Forms.Label
	Public WithEvents fraRamp As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
Me.chkShowColors = New System.Windows.Forms.CheckBox
Me.fraColors = New System.Windows.Forms.GroupBox
Me.TextBox10 = New System.Windows.Forms.TextBox
Me.TextBox9 = New System.Windows.Forms.TextBox
Me.TextBox8 = New System.Windows.Forms.TextBox
Me.TextBox7 = New System.Windows.Forms.TextBox
Me.TextBox6 = New System.Windows.Forms.TextBox
Me.TextBox5 = New System.Windows.Forms.TextBox
Me.TextBox4 = New System.Windows.Forms.TextBox
Me.TextBox3 = New System.Windows.Forms.TextBox
Me.TextBox2 = New System.Windows.Forms.TextBox
Me.TextBox1 = New System.Windows.Forms.TextBox
Me.Label10 = New System.Windows.Forms.Label
Me.Label9 = New System.Windows.Forms.Label
Me.Label8 = New System.Windows.Forms.Label
Me.Label7 = New System.Windows.Forms.Label
Me.Label6 = New System.Windows.Forms.Label
Me.Label5 = New System.Windows.Forms.Label
Me.Label4 = New System.Windows.Forms.Label
Me.Label3 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.cmdEnumColorsFirst = New System.Windows.Forms.Button
Me.cmdEnumColorsNext = New System.Windows.Forms.Button
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.fraRamp = New System.Windows.Forms.GroupBox
Me.Button2 = New System.Windows.Forms.Button
Me.Button1 = New System.Windows.Forms.Button
Me.cmbAlgorithm = New System.Windows.Forms.ComboBox
Me.txtEndColor = New System.Windows.Forms.TextBox
Me.txtStartColor = New System.Windows.Forms.TextBox
Me.lblStartColor = New System.Windows.Forms.Label
Me.lblEndColor = New System.Windows.Forms.Label
Me.lblAlgorithm = New System.Windows.Forms.Label
Me.fraColors.SuspendLayout()
Me.fraRamp.SuspendLayout()
Me.SuspendLayout()
'
'chkShowColors
'
Me.chkShowColors.Appearance = System.Windows.Forms.Appearance.Button
Me.chkShowColors.BackColor = System.Drawing.SystemColors.Control
Me.chkShowColors.Cursor = System.Windows.Forms.Cursors.Default
Me.chkShowColors.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.chkShowColors.ForeColor = System.Drawing.SystemColors.ControlText
Me.chkShowColors.Location = New System.Drawing.Point(60, 172)
Me.chkShowColors.Name = "chkShowColors"
Me.chkShowColors.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.chkShowColors.Size = New System.Drawing.Size(81, 25)
Me.chkShowColors.TabIndex = 33
Me.chkShowColors.Text = "Show Colors"
Me.chkShowColors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
Me.chkShowColors.UseVisualStyleBackColor = False
'
'fraColors
'
Me.fraColors.BackColor = System.Drawing.SystemColors.Control
Me.fraColors.Controls.Add(Me.TextBox10)
Me.fraColors.Controls.Add(Me.TextBox9)
Me.fraColors.Controls.Add(Me.TextBox8)
Me.fraColors.Controls.Add(Me.TextBox7)
Me.fraColors.Controls.Add(Me.TextBox6)
Me.fraColors.Controls.Add(Me.TextBox5)
Me.fraColors.Controls.Add(Me.TextBox4)
Me.fraColors.Controls.Add(Me.TextBox3)
Me.fraColors.Controls.Add(Me.TextBox2)
Me.fraColors.Controls.Add(Me.TextBox1)
Me.fraColors.Controls.Add(Me.Label10)
Me.fraColors.Controls.Add(Me.Label9)
Me.fraColors.Controls.Add(Me.Label8)
Me.fraColors.Controls.Add(Me.Label7)
Me.fraColors.Controls.Add(Me.Label6)
Me.fraColors.Controls.Add(Me.Label5)
Me.fraColors.Controls.Add(Me.Label4)
Me.fraColors.Controls.Add(Me.Label3)
Me.fraColors.Controls.Add(Me.Label2)
Me.fraColors.Controls.Add(Me.Label1)
Me.fraColors.Controls.Add(Me.cmdEnumColorsFirst)
Me.fraColors.Controls.Add(Me.cmdEnumColorsNext)
Me.fraColors.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.fraColors.ForeColor = System.Drawing.SystemColors.ControlText
Me.fraColors.Location = New System.Drawing.Point(156, 0)
Me.fraColors.Name = "fraColors"
Me.fraColors.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.fraColors.Size = New System.Drawing.Size(81, 249)
Me.fraColors.TabIndex = 9
Me.fraColors.TabStop = False
Me.fraColors.Text = "Colors:"
'
'TextBox10
'
Me.TextBox10.AcceptsReturn = True
Me.TextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox10.Location = New System.Drawing.Point(28, 160)
Me.TextBox10.MaxLength = 0
Me.TextBox10.Name = "TextBox10"
Me.TextBox10.Size = New System.Drawing.Size(41, 13)
Me.TextBox10.TabIndex = 51
'
'TextBox9
'
Me.TextBox9.AcceptsReturn = True
Me.TextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox9.Location = New System.Drawing.Point(28, 144)
Me.TextBox9.MaxLength = 0
Me.TextBox9.Name = "TextBox9"
Me.TextBox9.Size = New System.Drawing.Size(41, 13)
Me.TextBox9.TabIndex = 50
'
'TextBox8
'
Me.TextBox8.AcceptsReturn = True
Me.TextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox8.Location = New System.Drawing.Point(28, 128)
Me.TextBox8.MaxLength = 0
Me.TextBox8.Name = "TextBox8"
Me.TextBox8.Size = New System.Drawing.Size(41, 13)
Me.TextBox8.TabIndex = 49
'
'TextBox7
'
Me.TextBox7.AcceptsReturn = True
Me.TextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox7.Location = New System.Drawing.Point(28, 112)
Me.TextBox7.MaxLength = 0
Me.TextBox7.Name = "TextBox7"
Me.TextBox7.Size = New System.Drawing.Size(41, 13)
Me.TextBox7.TabIndex = 48
'
'TextBox6
'
Me.TextBox6.AcceptsReturn = True
Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox6.Location = New System.Drawing.Point(28, 96)
Me.TextBox6.MaxLength = 0
Me.TextBox6.Name = "TextBox6"
Me.TextBox6.Size = New System.Drawing.Size(41, 13)
Me.TextBox6.TabIndex = 47
'
'TextBox5
'
Me.TextBox5.AcceptsReturn = True
Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox5.Location = New System.Drawing.Point(28, 80)
Me.TextBox5.MaxLength = 0
Me.TextBox5.Name = "TextBox5"
Me.TextBox5.Size = New System.Drawing.Size(41, 13)
Me.TextBox5.TabIndex = 46
'
'TextBox4
'
Me.TextBox4.AcceptsReturn = True
Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox4.Location = New System.Drawing.Point(28, 64)
Me.TextBox4.MaxLength = 0
Me.TextBox4.Name = "TextBox4"
Me.TextBox4.Size = New System.Drawing.Size(41, 13)
Me.TextBox4.TabIndex = 45
'
'TextBox3
'
Me.TextBox3.AcceptsReturn = True
Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox3.Location = New System.Drawing.Point(28, 48)
Me.TextBox3.MaxLength = 0
Me.TextBox3.Name = "TextBox3"
Me.TextBox3.Size = New System.Drawing.Size(41, 13)
Me.TextBox3.TabIndex = 44
'
'TextBox2
'
Me.TextBox2.AcceptsReturn = True
Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox2.Location = New System.Drawing.Point(28, 32)
Me.TextBox2.MaxLength = 0
Me.TextBox2.Name = "TextBox2"
Me.TextBox2.Size = New System.Drawing.Size(41, 13)
Me.TextBox2.TabIndex = 43
'
'TextBox1
'
Me.TextBox1.AcceptsReturn = True
Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TextBox1.Location = New System.Drawing.Point(28, 16)
Me.TextBox1.MaxLength = 0
Me.TextBox1.Name = "TextBox1"
Me.TextBox1.Size = New System.Drawing.Size(41, 13)
Me.TextBox1.TabIndex = 42
'
'Label10
'
Me.Label10.Location = New System.Drawing.Point(8, 160)
Me.Label10.Name = "Label10"
Me.Label10.Size = New System.Drawing.Size(17, 17)
Me.Label10.TabIndex = 41
Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label9
'
Me.Label9.Location = New System.Drawing.Point(8, 144)
Me.Label9.Name = "Label9"
Me.Label9.Size = New System.Drawing.Size(17, 17)
Me.Label9.TabIndex = 40
Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label8
'
Me.Label8.Location = New System.Drawing.Point(8, 128)
Me.Label8.Name = "Label8"
Me.Label8.Size = New System.Drawing.Size(14, 14)
Me.Label8.TabIndex = 39
Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label7
'
Me.Label7.Location = New System.Drawing.Point(8, 112)
Me.Label7.Name = "Label7"
Me.Label7.Size = New System.Drawing.Size(17, 17)
Me.Label7.TabIndex = 38
Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label6
'
Me.Label6.Location = New System.Drawing.Point(8, 96)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(17, 17)
Me.Label6.TabIndex = 37
Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label5
'
Me.Label5.Location = New System.Drawing.Point(8, 80)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(17, 17)
Me.Label5.TabIndex = 36
Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label4
'
Me.Label4.Location = New System.Drawing.Point(8, 64)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(17, 17)
Me.Label4.TabIndex = 35
Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label3
'
Me.Label3.Location = New System.Drawing.Point(8, 48)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(17, 17)
Me.Label3.TabIndex = 34
Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label2
'
Me.Label2.Location = New System.Drawing.Point(8, 33)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(17, 17)
Me.Label2.TabIndex = 33
Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'Label1
'
Me.Label1.Location = New System.Drawing.Point(8, 16)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(17, 17)
Me.Label1.TabIndex = 32
Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'cmdEnumColorsFirst
'
Me.cmdEnumColorsFirst.BackColor = System.Drawing.SystemColors.Control
Me.cmdEnumColorsFirst.Cursor = System.Windows.Forms.Cursors.Default
Me.cmdEnumColorsFirst.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdEnumColorsFirst.ForeColor = System.Drawing.SystemColors.ControlText
Me.cmdEnumColorsFirst.Location = New System.Drawing.Point(16, 196)
Me.cmdEnumColorsFirst.Name = "cmdEnumColorsFirst"
Me.cmdEnumColorsFirst.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.cmdEnumColorsFirst.Size = New System.Drawing.Size(25, 21)
Me.cmdEnumColorsFirst.TabIndex = 11
Me.cmdEnumColorsFirst.Text = "|<"
Me.cmdEnumColorsFirst.UseVisualStyleBackColor = False
'
'cmdEnumColorsNext
'
Me.cmdEnumColorsNext.BackColor = System.Drawing.SystemColors.Control
Me.cmdEnumColorsNext.Cursor = System.Windows.Forms.Cursors.Default
Me.cmdEnumColorsNext.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdEnumColorsNext.ForeColor = System.Drawing.SystemColors.ControlText
Me.cmdEnumColorsNext.Location = New System.Drawing.Point(40, 196)
Me.cmdEnumColorsNext.Name = "cmdEnumColorsNext"
Me.cmdEnumColorsNext.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.cmdEnumColorsNext.Size = New System.Drawing.Size(25, 21)
Me.cmdEnumColorsNext.TabIndex = 10
Me.cmdEnumColorsNext.Text = ">"
Me.cmdEnumColorsNext.UseVisualStyleBackColor = False
'
'cmdCancel
'
Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
Me.cmdCancel.Location = New System.Drawing.Point(84, 224)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.cmdCancel.Size = New System.Drawing.Size(65, 25)
Me.cmdCancel.TabIndex = 1
Me.cmdCancel.Text = "&Cancel"
Me.cmdCancel.UseVisualStyleBackColor = False
'
'cmdOK
'
Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
Me.cmdOK.Location = New System.Drawing.Point(12, 224)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.cmdOK.Size = New System.Drawing.Size(65, 25)
Me.cmdOK.TabIndex = 0
Me.cmdOK.Text = "&OK"
Me.cmdOK.UseVisualStyleBackColor = False
'
'fraRamp
'
Me.fraRamp.BackColor = System.Drawing.SystemColors.Control
Me.fraRamp.Controls.Add(Me.Button2)
Me.fraRamp.Controls.Add(Me.Button1)
Me.fraRamp.Controls.Add(Me.cmbAlgorithm)
Me.fraRamp.Controls.Add(Me.txtEndColor)
Me.fraRamp.Controls.Add(Me.txtStartColor)
Me.fraRamp.Controls.Add(Me.lblStartColor)
Me.fraRamp.Controls.Add(Me.lblEndColor)
Me.fraRamp.Controls.Add(Me.lblAlgorithm)
Me.fraRamp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.fraRamp.ForeColor = System.Drawing.SystemColors.ControlText
Me.fraRamp.Location = New System.Drawing.Point(0, 0)
Me.fraRamp.Name = "fraRamp"
Me.fraRamp.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.fraRamp.Size = New System.Drawing.Size(149, 205)
Me.fraRamp.TabIndex = 2
Me.fraRamp.TabStop = False
Me.fraRamp.Text = "AlgorithmicColorRamp:"
'
'Button2
'
Me.Button2.Location = New System.Drawing.Point(119, 83)
Me.Button2.Name = "Button2"
Me.Button2.Size = New System.Drawing.Size(24, 22)
Me.Button2.TabIndex = 36
Me.Button2.UseVisualStyleBackColor = True
'
'Button1
'
Me.Button1.Location = New System.Drawing.Point(119, 36)
Me.Button1.Name = "Button1"
Me.Button1.Size = New System.Drawing.Size(24, 22)
Me.Button1.TabIndex = 35
Me.Button1.UseVisualStyleBackColor = True
'
'cmbAlgorithm
'
Me.cmbAlgorithm.BackColor = System.Drawing.SystemColors.Window
Me.cmbAlgorithm.Cursor = System.Windows.Forms.Cursors.Default
Me.cmbAlgorithm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.cmbAlgorithm.ForeColor = System.Drawing.SystemColors.WindowText
Me.cmbAlgorithm.Location = New System.Drawing.Point(4, 132)
Me.cmbAlgorithm.Name = "cmbAlgorithm"
Me.cmbAlgorithm.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.cmbAlgorithm.Size = New System.Drawing.Size(137, 22)
Me.cmbAlgorithm.TabIndex = 32
'
'txtEndColor
'
Me.txtEndColor.AcceptsReturn = True
Me.txtEndColor.BackColor = System.Drawing.SystemColors.Window
Me.txtEndColor.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txtEndColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txtEndColor.ForeColor = System.Drawing.SystemColors.WindowText
Me.txtEndColor.Location = New System.Drawing.Point(4, 84)
Me.txtEndColor.MaxLength = 0
Me.txtEndColor.Name = "txtEndColor"
Me.txtEndColor.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.txtEndColor.Size = New System.Drawing.Size(139, 20)
Me.txtEndColor.TabIndex = 7
'
'txtStartColor
'
Me.txtStartColor.AcceptsReturn = True
Me.txtStartColor.BackColor = System.Drawing.SystemColors.Window
Me.txtStartColor.Cursor = System.Windows.Forms.Cursors.IBeam
Me.txtStartColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txtStartColor.ForeColor = System.Drawing.SystemColors.WindowText
Me.txtStartColor.Location = New System.Drawing.Point(4, 36)
Me.txtStartColor.MaxLength = 0
Me.txtStartColor.Name = "txtStartColor"
Me.txtStartColor.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.txtStartColor.Size = New System.Drawing.Size(139, 20)
Me.txtStartColor.TabIndex = 6
'
'lblStartColor
'
Me.lblStartColor.AutoSize = True
Me.lblStartColor.BackColor = System.Drawing.SystemColors.Control
Me.lblStartColor.Cursor = System.Windows.Forms.Cursors.Default
Me.lblStartColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lblStartColor.ForeColor = System.Drawing.SystemColors.ControlText
Me.lblStartColor.Location = New System.Drawing.Point(8, 20)
Me.lblStartColor.Name = "lblStartColor"
Me.lblStartColor.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.lblStartColor.Size = New System.Drawing.Size(61, 14)
Me.lblStartColor.TabIndex = 5
Me.lblStartColor.Text = "Start Color:"
Me.lblStartColor.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'lblEndColor
'
Me.lblEndColor.AutoSize = True
Me.lblEndColor.BackColor = System.Drawing.SystemColors.Control
Me.lblEndColor.Cursor = System.Windows.Forms.Cursors.Default
Me.lblEndColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lblEndColor.ForeColor = System.Drawing.SystemColors.ControlText
Me.lblEndColor.Location = New System.Drawing.Point(8, 68)
Me.lblEndColor.Name = "lblEndColor"
Me.lblEndColor.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.lblEndColor.Size = New System.Drawing.Size(56, 14)
Me.lblEndColor.TabIndex = 4
Me.lblEndColor.Text = "End Color:"
Me.lblEndColor.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'lblAlgorithm
'
Me.lblAlgorithm.AutoSize = True
Me.lblAlgorithm.BackColor = System.Drawing.SystemColors.Control
Me.lblAlgorithm.Cursor = System.Windows.Forms.Cursors.Default
Me.lblAlgorithm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lblAlgorithm.ForeColor = System.Drawing.SystemColors.ControlText
Me.lblAlgorithm.Location = New System.Drawing.Point(8, 116)
Me.lblAlgorithm.Name = "lblAlgorithm"
Me.lblAlgorithm.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.lblAlgorithm.Size = New System.Drawing.Size(55, 14)
Me.lblAlgorithm.TabIndex = 3
Me.lblAlgorithm.Text = "Algorithm:"
Me.lblAlgorithm.TextAlign = System.Drawing.ContentAlignment.TopRight
'
'frmAlgorithmicColorRamp
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.BackColor = System.Drawing.SystemColors.Control
Me.ClientSize = New System.Drawing.Size(262, 251)
Me.Controls.Add(Me.chkShowColors)
Me.Controls.Add(Me.fraColors)
Me.Controls.Add(Me.cmdCancel)
Me.Controls.Add(Me.cmdOK)
Me.Controls.Add(Me.fraRamp)
Me.Cursor = System.Windows.Forms.Cursors.Default
Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Location = New System.Drawing.Point(4, 23)
Me.Name = "frmAlgorithmicColorRamp"
Me.RightToLeft = System.Windows.Forms.RightToLeft.No
Me.Text = "Form1"
Me.fraColors.ResumeLayout(False)
Me.fraColors.PerformLayout()
Me.fraRamp.ResumeLayout(False)
Me.fraRamp.PerformLayout()
Me.ResumeLayout(False)

End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
#End Region 
End Class