Imports Microsoft.VisualBasic
Imports System
Partial Public Class TrackSelectionDlg
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
    Me.chkOrthogonal = New System.Windows.Forms.RadioButton()
    Me.chkTerrain = New System.Windows.Forms.RadioButton()
    Me.btnOK = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.groupBox1.SuspendLayout()
    Me.SuspendLayout()
    ' 
    ' groupBox1
    ' 
    Me.groupBox1.Controls.Add(Me.chkTerrain)
    Me.groupBox1.Controls.Add(Me.chkOrthogonal)
    Me.groupBox1.Location = New System.Drawing.Point(11, 5)
    Me.groupBox1.Name = "groupBox1"
    Me.groupBox1.Size = New System.Drawing.Size(200, 90)
    Me.groupBox1.TabIndex = 0
    Me.groupBox1.TabStop = False
    Me.groupBox1.Text = "Select tracking mode"
    ' 
    ' chkOrthogonal
    ' 
    Me.chkOrthogonal.AutoSize = True
    Me.chkOrthogonal.Checked = True
    Me.chkOrthogonal.Location = New System.Drawing.Point(18, 25)
    Me.chkOrthogonal.Name = "chkOrthogonal"
    Me.chkOrthogonal.Size = New System.Drawing.Size(109, 17)
    Me.chkOrthogonal.TabIndex = 0
    Me.chkOrthogonal.TabStop = True
    Me.chkOrthogonal.Text = "Stay above target"
    Me.chkOrthogonal.UseVisualStyleBackColor = True
    ' 
    ' chkTerrain
    ' 
    Me.chkTerrain.AutoSize = True
    Me.chkTerrain.Location = New System.Drawing.Point(18, 58)
    Me.chkTerrain.Name = "chkTerrain"
    Me.chkTerrain.Size = New System.Drawing.Size(85, 17)
    Me.chkTerrain.TabIndex = 1
    Me.chkTerrain.Text = "Follow target"
    Me.chkTerrain.UseVisualStyleBackColor = True
    ' 
    ' btnOK
    ' 
    Me.btnOK.Location = New System.Drawing.Point(13, 117)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(75, 23)
    Me.btnOK.TabIndex = 1
    Me.btnOK.Text = "OK"
    Me.btnOK.UseVisualStyleBackColor = True
    '	  Me.btnOK.Click += New System.EventHandler(Me.btnOK_Click);
    ' 
    ' btnCancel
    ' 
    Me.btnCancel.Location = New System.Drawing.Point(136, 116)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 23)
    Me.btnCancel.TabIndex = 2
    Me.btnCancel.Text = "Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '	  Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
    ' 
    ' TrackSelectionDlg
    ' 
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(222, 153)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOK)
    Me.Controls.Add(Me.groupBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "TrackSelectionDlg"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "TrackSelectionDlg"
    Me.groupBox1.ResumeLayout(False)
    Me.groupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private groupBox1 As System.Windows.Forms.GroupBox
  Private chkTerrain As System.Windows.Forms.RadioButton
  Private chkOrthogonal As System.Windows.Forms.RadioButton
  Private WithEvents btnOK As System.Windows.Forms.Button
  Private WithEvents btnCancel As System.Windows.Forms.Button
End Class