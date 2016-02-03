<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestrictionsForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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
		Me.m_pnlRestrictions = New System.Windows.Forms.Panel
		Me.m_btnCancel = New System.Windows.Forms.Button
		Me.m_btnOK = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'm_pnlRestrictions
		'
		Me.m_pnlRestrictions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_pnlRestrictions.AutoScroll = True
		Me.m_pnlRestrictions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.m_pnlRestrictions.Location = New System.Drawing.Point(6, 12)
		Me.m_pnlRestrictions.Name = "m_pnlRestrictions"
		Me.m_pnlRestrictions.Size = New System.Drawing.Size(333, 274)
		Me.m_pnlRestrictions.TabIndex = 0
		'
		'm_btnCancel
		'
		Me.m_btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.m_btnCancel.Location = New System.Drawing.Point(266, 295)
		Me.m_btnCancel.Name = "m_btnCancel"
		Me.m_btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.m_btnCancel.TabIndex = 2
		Me.m_btnCancel.Text = "Cancel"
		Me.m_btnCancel.UseVisualStyleBackColor = True
		'
		'm_btnOK
		'
		Me.m_btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.m_btnOK.Location = New System.Drawing.Point(185, 295)
		Me.m_btnOK.Name = "m_btnOK"
		Me.m_btnOK.Size = New System.Drawing.Size(75, 23)
		Me.m_btnOK.TabIndex = 1
		Me.m_btnOK.Text = "OK"
		Me.m_btnOK.UseVisualStyleBackColor = True
		'
		'RestrictionsForm
		'
		Me.AcceptButton = Me.m_btnOK
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
		Me.CancelButton = Me.m_btnCancel
		Me.ClientSize = New System.Drawing.Size(348, 326)
		Me.Controls.Add(Me.m_btnOK)
		Me.Controls.Add(Me.m_btnCancel)
		Me.Controls.Add(Me.m_pnlRestrictions)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "RestrictionsForm"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Restrictions"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents m_pnlRestrictions As System.Windows.Forms.Panel
	Friend WithEvents m_btnCancel As System.Windows.Forms.Button
	Friend WithEvents m_btnOK As System.Windows.Forms.Button
End Class
