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
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestrictControl
	Inherits System.Windows.Forms.UserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.m_txtParameter = New System.Windows.Forms.TextBox
		Me.m_cmbType = New System.Windows.Forms.ComboBox
		Me.m_chkCheck = New System.Windows.Forms.CheckBox
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.m_txtParameter)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
		Me.Panel1.Location = New System.Drawing.Point(305, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(72, 22)
		Me.Panel1.TabIndex = 4
		'
		'm_txtParameter
		'
		Me.m_txtParameter.Dock = System.Windows.Forms.DockStyle.Fill
		Me.m_txtParameter.Location = New System.Drawing.Point(0, 0)
		Me.m_txtParameter.Name = "m_txtParameter"
		Me.m_txtParameter.Size = New System.Drawing.Size(72, 20)
		Me.m_txtParameter.TabIndex = 0
		Me.m_txtParameter.Visible = False
		'
		'm_cmbType
		'
		Me.m_cmbType.Dock = System.Windows.Forms.DockStyle.Right
		Me.m_cmbType.FormattingEnabled = True
		Me.m_cmbType.Items.AddRange(New Object() {"Strict", "Relaxed"})
		Me.m_cmbType.Location = New System.Drawing.Point(222, 0)
		Me.m_cmbType.Name = "m_cmbType"
		Me.m_cmbType.Size = New System.Drawing.Size(83, 21)
		Me.m_cmbType.TabIndex = 1
		'
		'm_chkCheck
		'
		Me.m_chkCheck.AutoSize = True
		Me.m_chkCheck.CheckAlign = System.Drawing.ContentAlignment.TopLeft
		Me.m_chkCheck.Dock = System.Windows.Forms.DockStyle.Fill
		Me.m_chkCheck.Location = New System.Drawing.Point(0, 0)
		Me.m_chkCheck.Name = "m_chkCheck"
		Me.m_chkCheck.Size = New System.Drawing.Size(222, 22)
		Me.m_chkCheck.TabIndex = 0
		Me.m_chkCheck.Text = "Restriction"
		Me.m_chkCheck.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.m_chkCheck.UseVisualStyleBackColor = True
		'
		'RestrictControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.m_chkCheck)
		Me.Controls.Add(Me.m_cmbType)
		Me.Controls.Add(Me.Panel1)
		Me.Name = "RestrictControl"
		Me.Size = New System.Drawing.Size(377, 22)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents m_txtParameter As System.Windows.Forms.TextBox
	Friend WithEvents m_cmbType As System.Windows.Forms.ComboBox
	Friend WithEvents m_chkCheck As System.Windows.Forms.CheckBox

End Class
