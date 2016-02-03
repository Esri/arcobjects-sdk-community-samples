Partial Class NDVICustomFunctionUIForm
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
		Me.inputRasterBtn = New System.Windows.Forms.Button()
		Me.inputRasterTxtbox = New System.Windows.Forms.TextBox()
		Me.inputRasterLbl = New System.Windows.Forms.Label()
		Me.BandIndicesTxtBox = New System.Windows.Forms.TextBox()
		Me.HintLbl = New System.Windows.Forms.Label()
		Me.BandIndicesLbl = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		' 
		' inputRasterBtn
		' 
		Me.inputRasterBtn.Location = New System.Drawing.Point(347, 51)
		Me.inputRasterBtn.Name = "inputRasterBtn"
		Me.inputRasterBtn.Size = New System.Drawing.Size(31, 23)
		Me.inputRasterBtn.TabIndex = 12
		Me.inputRasterBtn.Text = "..."
		Me.inputRasterBtn.UseVisualStyleBackColor = True
		' 
		' inputRasterTxtbox
		' 
		Me.inputRasterTxtbox.Location = New System.Drawing.Point(154, 54)
		Me.inputRasterTxtbox.Name = "inputRasterTxtbox"
		Me.inputRasterTxtbox.Size = New System.Drawing.Size(186, 20)
		Me.inputRasterTxtbox.TabIndex = 11
		' 
		' inputRasterLbl
		' 
		Me.inputRasterLbl.AutoSize = True
		Me.inputRasterLbl.Location = New System.Drawing.Point(22, 61)
		Me.inputRasterLbl.Name = "inputRasterLbl"
		Me.inputRasterLbl.Size = New System.Drawing.Size(68, 13)
		Me.inputRasterLbl.TabIndex = 10
		Me.inputRasterLbl.Text = "Input Raster:"
		' 
		' BandIndicesTxtBox
		' 
		Me.BandIndicesTxtBox.Location = New System.Drawing.Point(154, 123)
		Me.BandIndicesTxtBox.Name = "BandIndicesTxtBox"
		Me.BandIndicesTxtBox.Size = New System.Drawing.Size(186, 20)
		Me.BandIndicesTxtBox.TabIndex = 15
		AddHandler Me.BandIndicesTxtBox.TextChanged, New System.EventHandler(AddressOf Me.BandIndicesTxtBox_TextChanged)
		' 
		' HintLbl
		' 
		Me.HintLbl.AutoSize = True
		Me.HintLbl.Location = New System.Drawing.Point(151, 107)
		Me.HintLbl.Name = "HintLbl"
		Me.HintLbl.Size = New System.Drawing.Size(0, 13)
		Me.HintLbl.TabIndex = 14
		' 
		' BandIndicesLbl
		' 
		Me.BandIndicesLbl.AutoSize = True
		Me.BandIndicesLbl.Location = New System.Drawing.Point(22, 130)
		Me.BandIndicesLbl.Name = "BandIndicesLbl"
		Me.BandIndicesLbl.Size = New System.Drawing.Size(69, 13)
		Me.BandIndicesLbl.TabIndex = 13
		Me.BandIndicesLbl.Text = "Band Indices"
		' 
		' NDVICustomFunctionUIForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(402, 197)
		Me.Controls.Add(Me.BandIndicesTxtBox)
		Me.Controls.Add(Me.HintLbl)
		Me.Controls.Add(Me.BandIndicesLbl)
		Me.Controls.Add(Me.inputRasterBtn)
		Me.Controls.Add(Me.inputRasterTxtbox)
		Me.Controls.Add(Me.inputRasterLbl)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Name = "NDVICustomFunctionUIForm"
		Me.Text = "NDVICustomFunction"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private inputRasterBtn As System.Windows.Forms.Button
	Private inputRasterTxtbox As System.Windows.Forms.TextBox
	Private inputRasterLbl As System.Windows.Forms.Label
	Private BandIndicesTxtBox As System.Windows.Forms.TextBox
	Private HintLbl As System.Windows.Forms.Label
	Private BandIndicesLbl As System.Windows.Forms.Label
End Class

