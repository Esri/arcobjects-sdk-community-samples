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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimestampPropertyPage
		Inherits System.Windows.Forms.UserControl

		'UserControl overrides dispose to clean up the component list.
		<System.Diagnostics.DebuggerNonUserCode()> _
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
				Try
						If disposing AndAlso components IsNot Nothing Then
								components.Dispose()
						End If
				Finally
						MyBase.Dispose(disposing)
				End Try
		End Sub

		'Required by the Windows Form Designer
		Private components As System.ComponentModel.IContainer

		'NOTE: The following procedure is required by the Windows Form Designer
		'It can be modified using the Windows Form Designer.  
		'Do not modify it using the code editor.
		<System.Diagnostics.DebuggerStepThrough()> _
		Private Sub InitializeComponent()
				Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimestampPropertyPage))
				Me.txtDescription = New System.Windows.Forms.TextBox
				Me.cmbUserField = New System.Windows.Forms.ComboBox
				Me.lblUserField = New System.Windows.Forms.Label
				Me.cmbModifiedField = New System.Windows.Forms.ComboBox
				Me.lblModifiedField = New System.Windows.Forms.Label
				Me.cmbCreatedField = New System.Windows.Forms.ComboBox
				Me.lblCreatedField = New System.Windows.Forms.Label
				Me.SuspendLayout()
				'
				'txtDescription
				'
				Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
				Me.txtDescription.BackColor = System.Drawing.SystemColors.Control
				Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
				Me.txtDescription.Cursor = System.Windows.Forms.Cursors.Arrow
				Me.txtDescription.Location = New System.Drawing.Point(3, 150)
				Me.txtDescription.Multiline = True
				Me.txtDescription.Name = "txtDescription"
				Me.txtDescription.ReadOnly = True
				Me.txtDescription.Size = New System.Drawing.Size(267, 85)
				Me.txtDescription.TabIndex = 22
				Me.txtDescription.TabStop = False
				Me.txtDescription.Text = resources.GetString("txtDescription.Text")
				'
				'cmbUserField
				'
				Me.cmbUserField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
				Me.cmbUserField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
				Me.cmbUserField.FormattingEnabled = True
				Me.cmbUserField.Items.AddRange(New Object() {"Not Used"})
				Me.cmbUserField.Location = New System.Drawing.Point(3, 116)
				Me.cmbUserField.Name = "cmbUserField"
				Me.cmbUserField.Size = New System.Drawing.Size(267, 21)
				Me.cmbUserField.TabIndex = 21
				'
				'lblUserField
				'
				Me.lblUserField.AutoSize = True
				Me.lblUserField.Location = New System.Drawing.Point(3, 100)
				Me.lblUserField.Name = "lblUserField"
				Me.lblUserField.Size = New System.Drawing.Size(54, 13)
				Me.lblUserField.TabIndex = 20
				Me.lblUserField.Text = "User field:"
				'
				'cmbModifiedField
				'
				Me.cmbModifiedField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
				Me.cmbModifiedField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
				Me.cmbModifiedField.FormattingEnabled = True
				Me.cmbModifiedField.Items.AddRange(New Object() {"Not Used"})
				Me.cmbModifiedField.Location = New System.Drawing.Point(3, 70)
				Me.cmbModifiedField.Name = "cmbModifiedField"
				Me.cmbModifiedField.Size = New System.Drawing.Size(267, 21)
				Me.cmbModifiedField.TabIndex = 19
				'
				'lblModifiedField
				'
				Me.lblModifiedField.AutoSize = True
				Me.lblModifiedField.Location = New System.Drawing.Point(3, 54)
				Me.lblModifiedField.Name = "lblModifiedField"
				Me.lblModifiedField.Size = New System.Drawing.Size(75, 13)
				Me.lblModifiedField.TabIndex = 18
				Me.lblModifiedField.Text = "Modified Field:"
				'
				'cmbCreatedField
				'
				Me.cmbCreatedField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
				Me.cmbCreatedField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
				Me.cmbCreatedField.FormattingEnabled = True
				Me.cmbCreatedField.Items.AddRange(New Object() {"Not Used"})
				Me.cmbCreatedField.Location = New System.Drawing.Point(3, 25)
				Me.cmbCreatedField.Name = "cmbCreatedField"
				Me.cmbCreatedField.Size = New System.Drawing.Size(267, 21)
				Me.cmbCreatedField.TabIndex = 17
				'
				'lblCreatedField
				'
				Me.lblCreatedField.AutoSize = True
				Me.lblCreatedField.Location = New System.Drawing.Point(3, 9)
				Me.lblCreatedField.Name = "lblCreatedField"
				Me.lblCreatedField.Size = New System.Drawing.Size(72, 13)
				Me.lblCreatedField.TabIndex = 16
				Me.lblCreatedField.Text = "Created Field:"
				'
				'TimestampPropertyPage
				'
				Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
				Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
				Me.Controls.Add(Me.txtDescription)
				Me.Controls.Add(Me.cmbUserField)
				Me.Controls.Add(Me.lblUserField)
				Me.Controls.Add(Me.cmbModifiedField)
				Me.Controls.Add(Me.lblModifiedField)
				Me.Controls.Add(Me.cmbCreatedField)
				Me.Controls.Add(Me.lblCreatedField)
				Me.Name = "TimestampPropertyPage"
				Me.Size = New System.Drawing.Size(273, 241)
				Me.ResumeLayout(False)
				Me.PerformLayout()

		End Sub
		Private WithEvents txtDescription As System.Windows.Forms.TextBox
		Private WithEvents cmbUserField As System.Windows.Forms.ComboBox
		Private WithEvents lblUserField As System.Windows.Forms.Label
		Private WithEvents cmbModifiedField As System.Windows.Forms.ComboBox
		Private WithEvents lblModifiedField As System.Windows.Forms.Label
		Private WithEvents cmbCreatedField As System.Windows.Forms.ComboBox
		Private WithEvents lblCreatedField As System.Windows.Forms.Label

End Class

