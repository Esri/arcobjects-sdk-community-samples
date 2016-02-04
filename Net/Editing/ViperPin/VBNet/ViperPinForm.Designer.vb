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
Namespace ViperPin
  Partial Public Class ViperPinForm
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
	  Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(ViperPinForm))
	  Me.cmdOK = New System.Windows.Forms.Button()
	  Me.Label4 = New System.Windows.Forms.Label()
	  Me.Label1 = New System.Windows.Forms.Label()
	  Me.Label3 = New System.Windows.Forms.Label()
	  Me.txtlot = New System.Windows.Forms.TextBox()
	  Me.lblEditLayer = New System.Windows.Forms.Label()
	  Me.GroupBox3 = New System.Windows.Forms.GroupBox()
	  Me.chkEnds = New System.Windows.Forms.CheckBox()
	  Me.txtlotinc = New System.Windows.Forms.TextBox()
	  Me.Label2 = New System.Windows.Forms.Label()
	  Me.GroupBox2 = New System.Windows.Forms.GroupBox()
	  Me.cmbPINField = New System.Windows.Forms.ComboBox()
	  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
	  Me.cmdCancel = New System.Windows.Forms.Button()
	  Me.GroupBox3.SuspendLayout()
	  Me.GroupBox2.SuspendLayout()
	  Me.GroupBox1.SuspendLayout()
	  Me.SuspendLayout()
	  ' 
	  ' cmdOK
	  ' 
	  Me.cmdOK.Location = New System.Drawing.Point(14, 237)
	  Me.cmdOK.Name = "cmdOK"
	  Me.cmdOK.Size = New System.Drawing.Size(75, 23)
	  Me.cmdOK.TabIndex = 6
	  Me.cmdOK.Text = "Ok"
'	  Me.cmdOK.Click += New System.EventHandler(Me.cmdOK_Click);
	  ' 
	  ' Label4
	  ' 
	  Me.Label4.Location = New System.Drawing.Point(8, 48)
	  Me.Label4.Name = "Label4"
	  Me.Label4.Size = New System.Drawing.Size(64, 16)
	  Me.Label4.TabIndex = 6
	  Me.Label4.Text = "Increment:"
	  ' 
	  ' Label1
	  ' 
	  Me.Label1.Location = New System.Drawing.Point(8, 16)
	  Me.Label1.Name = "Label1"
	  Me.Label1.Size = New System.Drawing.Size(72, 16)
	  Me.Label1.TabIndex = 4
	  Me.Label1.Text = "Parcel Layer:"
	  ' 
	  ' Label3
	  ' 
	  Me.Label3.Location = New System.Drawing.Point(8, 24)
	  Me.Label3.Name = "Label3"
	  Me.Label3.Size = New System.Drawing.Size(72, 16)
	  Me.Label3.TabIndex = 5
	  Me.Label3.Text = "Start Value:"
	  ' 
	  ' txtlot
	  ' 
	  Me.txtlot.Location = New System.Drawing.Point(96, 24)
	  Me.txtlot.Name = "txtlot"
	  Me.txtlot.Size = New System.Drawing.Size(32, 20)
	  Me.txtlot.TabIndex = 5
	  Me.txtlot.Text = "1"
	  ' 
	  ' lblEditLayer
	  ' 
	  Me.lblEditLayer.Location = New System.Drawing.Point(96, 16)
	  Me.lblEditLayer.Name = "lblEditLayer"
	  Me.lblEditLayer.Size = New System.Drawing.Size(144, 16)
	  Me.lblEditLayer.TabIndex = 4
	  Me.lblEditLayer.Text = "lblEditLayer"
	  ' 
	  ' GroupBox3
	  ' 
	  Me.GroupBox3.Controls.Add(Me.chkEnds)
	  Me.GroupBox3.Location = New System.Drawing.Point(14, 173)
	  Me.GroupBox3.Name = "GroupBox3"
	  Me.GroupBox3.Size = New System.Drawing.Size(256, 56)
	  Me.GroupBox3.TabIndex = 11
	  Me.GroupBox3.TabStop = False
	  Me.GroupBox3.Text = "Sketch Ends"
	  ' 
	  ' chkEnds
	  ' 
	  Me.chkEnds.Checked = True
	  Me.chkEnds.CheckState = System.Windows.Forms.CheckState.Checked
	  Me.chkEnds.Location = New System.Drawing.Point(8, 24)
	  Me.chkEnds.Name = "chkEnds"
	  Me.chkEnds.Size = New System.Drawing.Size(152, 24)
	  Me.chkEnds.TabIndex = 0
	  Me.chkEnds.Text = "Include ends of sketch"
	  ' 
	  ' txtlotinc
	  ' 
	  Me.txtlotinc.Location = New System.Drawing.Point(96, 48)
	  Me.txtlotinc.Name = "txtlotinc"
	  Me.txtlotinc.Size = New System.Drawing.Size(32, 20)
	  Me.txtlotinc.TabIndex = 7
	  Me.txtlotinc.Text = "1"
	  ' 
	  ' Label2
	  ' 
	  Me.Label2.Location = New System.Drawing.Point(24, 40)
	  Me.Label2.Name = "Label2"
	  Me.Label2.Size = New System.Drawing.Size(56, 16)
	  Me.Label2.TabIndex = 5
	  Me.Label2.Text = "PIN Field:"
	  ' 
	  ' GroupBox2
	  ' 
	  Me.GroupBox2.Controls.Add(Me.txtlotinc)
	  Me.GroupBox2.Controls.Add(Me.Label4)
	  Me.GroupBox2.Controls.Add(Me.Label3)
	  Me.GroupBox2.Controls.Add(Me.txtlot)
	  Me.GroupBox2.Location = New System.Drawing.Point(14, 85)
	  Me.GroupBox2.Name = "GroupBox2"
	  Me.GroupBox2.Size = New System.Drawing.Size(256, 80)
	  Me.GroupBox2.TabIndex = 10
	  Me.GroupBox2.TabStop = False
	  Me.GroupBox2.Text = "Parcel PIN Value"
	  ' 
	  ' cmbPINField
	  ' 
	  Me.cmbPINField.Location = New System.Drawing.Point(96, 40)
	  Me.cmbPINField.Name = "cmbPINField"
	  Me.cmbPINField.Size = New System.Drawing.Size(152, 21)
	  Me.cmbPINField.TabIndex = 4
	  Me.cmbPINField.Text = "cmbPINField"
	  ' 
	  ' GroupBox1
	  ' 
	  Me.GroupBox1.Controls.Add(Me.Label1)
	  Me.GroupBox1.Controls.Add(Me.lblEditLayer)
	  Me.GroupBox1.Controls.Add(Me.Label2)
	  Me.GroupBox1.Controls.Add(Me.cmbPINField)
	  Me.GroupBox1.Location = New System.Drawing.Point(14, 5)
	  Me.GroupBox1.Name = "GroupBox1"
	  Me.GroupBox1.Size = New System.Drawing.Size(256, 72)
	  Me.GroupBox1.TabIndex = 9
	  Me.GroupBox1.TabStop = False
	  Me.GroupBox1.Text = "Parcel PIN Field"
	  ' 
	  ' cmdCancel
	  ' 
	  Me.cmdCancel.Location = New System.Drawing.Point(187, 237)
	  Me.cmdCancel.Name = "cmdCancel"
	  Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
	  Me.cmdCancel.TabIndex = 7
	  Me.cmdCancel.Text = "Cancel"
'	  Me.cmdCancel.Click += New System.EventHandler(Me.cmdCancel_Click);
	  ' 
	  ' ViperPinForm
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.ClientSize = New System.Drawing.Size(284, 264)
	  Me.Controls.Add(Me.cmdOK)
	  Me.Controls.Add(Me.cmdCancel)
	  Me.Controls.Add(Me.GroupBox3)
	  Me.Controls.Add(Me.GroupBox2)
	  Me.Controls.Add(Me.GroupBox1)
	  Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
	  Me.Name = "ViperPinForm"
	  Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
	  Me.Text = "ViperPinForm"
	  Me.GroupBox3.ResumeLayout(False)
	  Me.GroupBox2.ResumeLayout(False)
	  Me.GroupBox2.PerformLayout()
	  Me.GroupBox1.ResumeLayout(False)
	  Me.ResumeLayout(False)

	End Sub

	#End Region

	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend Label4 As System.Windows.Forms.Label
	Friend Label1 As System.Windows.Forms.Label
	Friend Label3 As System.Windows.Forms.Label
	Friend txtlot As System.Windows.Forms.TextBox
	Friend lblEditLayer As System.Windows.Forms.Label
	Friend GroupBox3 As System.Windows.Forms.GroupBox
	Friend chkEnds As System.Windows.Forms.CheckBox
	Friend txtlotinc As System.Windows.Forms.TextBox
	Friend Label2 As System.Windows.Forms.Label
	Friend GroupBox2 As System.Windows.Forms.GroupBox
	Friend cmbPINField As System.Windows.Forms.ComboBox
	Friend GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
  End Class
End Namespace