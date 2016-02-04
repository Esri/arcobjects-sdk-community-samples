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
Partial Class DirectionsForm
	Inherits System.Windows.Forms.Form

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
		Me.m_txtDirections = New System.Windows.Forms.TextBox
		Me.m_btnClose = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'm_txtDirections
		'
		Me.m_txtDirections.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtDirections.Location = New System.Drawing.Point(8, 8)
		Me.m_txtDirections.Multiline = True
		Me.m_txtDirections.Name = "m_txtDirections"
		Me.m_txtDirections.ReadOnly = True
		Me.m_txtDirections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.m_txtDirections.Size = New System.Drawing.Size(384, 472)
		Me.m_txtDirections.TabIndex = 0
		Me.m_txtDirections.Text = ""
		'
		'm_btnClose
		'
		Me.m_btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.m_btnClose.Location = New System.Drawing.Point(320, 488)
		Me.m_btnClose.Name = "m_btnClose"
		Me.m_btnClose.TabIndex = 1
		Me.m_btnClose.Text = "Close"
		'
		'DirectionsForm
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(402, 522)
		Me.Controls.Add(Me.m_btnClose)
		Me.Controls.Add(Me.m_txtDirections)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "DirectionsForm"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Driving Directions"
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents m_txtDirections As System.Windows.Forms.TextBox
	Friend WithEvents m_btnClose As System.Windows.Forms.Button

End Class

