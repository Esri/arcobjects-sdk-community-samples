Imports Microsoft.VisualBasic
Imports System
Namespace PointsAlongLine
  Partial Public Class PointsAlongLineForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
	  Me.label1 = New System.Windows.Forms.Label()
	  Me.tbLineLength = New System.Windows.Forms.TextBox()
	  Me.groupBox1 = New System.Windows.Forms.GroupBox()
	  Me.txtDist = New System.Windows.Forms.TextBox()
	  Me.txtNOP = New System.Windows.Forms.TextBox()
	  Me.chkEnds = New System.Windows.Forms.CheckBox()
	  Me.rbDist = New System.Windows.Forms.RadioButton()
	  Me.rbNOP = New System.Windows.Forms.RadioButton()
	  Me.cmdCancel = New System.Windows.Forms.Button()
	  Me.cmdOK = New System.Windows.Forms.Button()
	  Me.groupBox1.SuspendLayout()
	  Me.SuspendLayout()
	  ' 
	  ' label1
	  ' 
	  Me.label1.AutoSize = True
	  Me.label1.Location = New System.Drawing.Point(12, 9)
	  Me.label1.Name = "label1"
	  Me.label1.Size = New System.Drawing.Size(66, 13)
	  Me.label1.TabIndex = 0
	  Me.label1.Text = "Line Length:"
	  ' 
	  ' tbLineLength
	  ' 
	  Me.tbLineLength.Location = New System.Drawing.Point(84, 6)
	  Me.tbLineLength.Name = "tbLineLength"
	  Me.tbLineLength.ReadOnly = True
	  Me.tbLineLength.Size = New System.Drawing.Size(188, 20)
	  Me.tbLineLength.TabIndex = 1
	  ' 
	  ' groupBox1
	  ' 
	  Me.groupBox1.Controls.Add(Me.txtDist)
	  Me.groupBox1.Controls.Add(Me.txtNOP)
	  Me.groupBox1.Controls.Add(Me.chkEnds)
	  Me.groupBox1.Controls.Add(Me.rbDist)
	  Me.groupBox1.Controls.Add(Me.rbNOP)
	  Me.groupBox1.Location = New System.Drawing.Point(15, 32)
	  Me.groupBox1.Name = "groupBox1"
	  Me.groupBox1.Size = New System.Drawing.Size(257, 107)
	  Me.groupBox1.TabIndex = 2
	  Me.groupBox1.TabStop = False
	  Me.groupBox1.Text = "Construction Options"
	  ' 
	  ' txtDist
	  ' 
	  Me.txtDist.Location = New System.Drawing.Point(151, 42)
	  Me.txtDist.Name = "txtDist"
	  Me.txtDist.Size = New System.Drawing.Size(100, 20)
	  Me.txtDist.TabIndex = 4
	  ' 
	  ' txtNOP
	  ' 
	  Me.txtNOP.Location = New System.Drawing.Point(151, 19)
	  Me.txtNOP.Name = "txtNOP"
	  Me.txtNOP.Size = New System.Drawing.Size(100, 20)
	  Me.txtNOP.TabIndex = 3
	  Me.txtNOP.Text = "1"
	  ' 
	  ' chkEnds
	  ' 
	  Me.chkEnds.AutoSize = True
	  Me.chkEnds.Location = New System.Drawing.Point(6, 77)
	  Me.chkEnds.Name = "chkEnds"
	  Me.chkEnds.Size = New System.Drawing.Size(177, 17)
	  Me.chkEnds.TabIndex = 2
	  Me.chkEnds.Text = "Create additional points on ends"
	  Me.chkEnds.UseVisualStyleBackColor = True
	  ' 
	  ' rbDist
	  ' 
	  Me.rbDist.AutoSize = True
	  Me.rbDist.Location = New System.Drawing.Point(6, 42)
	  Me.rbDist.Name = "rbDist"
	  Me.rbDist.Size = New System.Drawing.Size(67, 17)
	  Me.rbDist.TabIndex = 1
	  Me.rbDist.TabStop = True
	  Me.rbDist.Text = "Distance"
	  Me.rbDist.UseVisualStyleBackColor = True
	  ' 
	  ' rbNOP
	  ' 
	  Me.rbNOP.AutoSize = True
	  Me.rbNOP.Checked = True
	  Me.rbNOP.Location = New System.Drawing.Point(6, 19)
	  Me.rbNOP.Name = "rbNOP"
	  Me.rbNOP.Size = New System.Drawing.Size(105, 17)
	  Me.rbNOP.TabIndex = 0
	  Me.rbNOP.TabStop = True
	  Me.rbNOP.Text = "Number of points"
	  Me.rbNOP.UseVisualStyleBackColor = True
	  ' 
	  ' cmdCancel
	  ' 
	  Me.cmdCancel.Location = New System.Drawing.Point(193, 145)
	  Me.cmdCancel.Name = "cmdCancel"
	  Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
	  Me.cmdCancel.TabIndex = 3
	  Me.cmdCancel.Text = "Cancel"
	  Me.cmdCancel.UseVisualStyleBackColor = True
'	  Me.cmdCancel.Click += New System.EventHandler(Me.cmdCancel_Click);
	  ' 
	  ' cmdOK
	  ' 
	  Me.cmdOK.Location = New System.Drawing.Point(112, 145)
	  Me.cmdOK.Name = "cmdOK"
	  Me.cmdOK.Size = New System.Drawing.Size(75, 23)
	  Me.cmdOK.TabIndex = 4
	  Me.cmdOK.Text = "OK"
	  Me.cmdOK.UseVisualStyleBackColor = True
'	  Me.cmdOK.Click += New System.EventHandler(Me.cmdOK_Click);
	  ' 
	  ' PointsAlongLineForm
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.ClientSize = New System.Drawing.Size(284, 180)
	  Me.Controls.Add(Me.cmdOK)
	  Me.Controls.Add(Me.cmdCancel)
	  Me.Controls.Add(Me.groupBox1)
	  Me.Controls.Add(Me.tbLineLength)
	  Me.Controls.Add(Me.label1)
	  Me.MaximizeBox = False
	  Me.MinimizeBox = False
	  Me.Name = "PointsAlongLineForm"
	  Me.ShowIcon = False
	  Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
	  Me.Text = "Construct points along a line"
	  Me.groupBox1.ResumeLayout(False)
	  Me.groupBox1.PerformLayout()
	  Me.ResumeLayout(False)
	  Me.PerformLayout()

	End Sub

	#End Region

	Private label1 As System.Windows.Forms.Label
	Private tbLineLength As System.Windows.Forms.TextBox
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private WithEvents cmdCancel As System.Windows.Forms.Button
	Private txtDist As System.Windows.Forms.TextBox
	Private txtNOP As System.Windows.Forms.TextBox
	Private chkEnds As System.Windows.Forms.CheckBox
	Private rbDist As System.Windows.Forms.RadioButton
	Private rbNOP As System.Windows.Forms.RadioButton
	Private WithEvents cmdOK As System.Windows.Forms.Button
  End Class
End Namespace