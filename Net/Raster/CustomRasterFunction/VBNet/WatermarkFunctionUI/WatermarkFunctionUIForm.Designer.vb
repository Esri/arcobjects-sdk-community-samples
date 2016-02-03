Partial Class WatermarkFunctionUIForm
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
		Me.inputRasterLbl = New System.Windows.Forms.Label()
		Me.watermarkImageLbl = New System.Windows.Forms.Label()
		Me.blendPercentLbl = New System.Windows.Forms.Label()
		Me.inputRasterTxtbox = New System.Windows.Forms.TextBox()
		Me.watermarkImageTxtbox = New System.Windows.Forms.TextBox()
		Me.blendPercentTxtbox = New System.Windows.Forms.TextBox()
		Me.inputRasterBtn = New System.Windows.Forms.Button()
		Me.watermarkImageBtn = New System.Windows.Forms.Button()
		Me.label1 = New System.Windows.Forms.Label()
		Me.watermarkImageDlg = New System.Windows.Forms.OpenFileDialog()
		Me.LocationLbl = New System.Windows.Forms.Label()
		Me.LocationComboBx = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		' 
		' inputRasterLbl
		' 
		Me.inputRasterLbl.AutoSize = True
		Me.inputRasterLbl.Location = New System.Drawing.Point(23, 49)
		Me.inputRasterLbl.Name = "inputRasterLbl"
		Me.inputRasterLbl.Size = New System.Drawing.Size(68, 13)
		Me.inputRasterLbl.TabIndex = 0
		Me.inputRasterLbl.Text = "Input Raster:"
		' 
		' watermarkImageLbl
		' 
		Me.watermarkImageLbl.AutoSize = True
		Me.watermarkImageLbl.Location = New System.Drawing.Point(23, 129)
		Me.watermarkImageLbl.Name = "watermarkImageLbl"
		Me.watermarkImageLbl.Size = New System.Drawing.Size(119, 13)
		Me.watermarkImageLbl.TabIndex = 1
		Me.watermarkImageLbl.Text = "Watermark Image Path:"
		' 
		' blendPercentLbl
		' 
		Me.blendPercentLbl.AutoSize = True
		Me.blendPercentLbl.Location = New System.Drawing.Point(23, 175)
		Me.blendPercentLbl.Name = "blendPercentLbl"
		Me.blendPercentLbl.Size = New System.Drawing.Size(95, 13)
		Me.blendPercentLbl.TabIndex = 2
		Me.blendPercentLbl.Text = "Blend Percentage:"
		' 
		' inputRasterTxtbox
		' 
		Me.inputRasterTxtbox.Location = New System.Drawing.Point(191, 41)
		Me.inputRasterTxtbox.Name = "inputRasterTxtbox"
		Me.inputRasterTxtbox.Size = New System.Drawing.Size(186, 20)
		Me.inputRasterTxtbox.TabIndex = 3
		' 
		' watermarkImageTxtbox
		' 
		Me.watermarkImageTxtbox.Location = New System.Drawing.Point(191, 122)
		Me.watermarkImageTxtbox.Name = "watermarkImageTxtbox"
		Me.watermarkImageTxtbox.Size = New System.Drawing.Size(186, 20)
		Me.watermarkImageTxtbox.TabIndex = 4
		' 
		' blendPercentTxtbox
		' 
		Me.blendPercentTxtbox.Location = New System.Drawing.Point(191, 168)
		Me.blendPercentTxtbox.Name = "blendPercentTxtbox"
		Me.blendPercentTxtbox.Size = New System.Drawing.Size(186, 20)
		Me.blendPercentTxtbox.TabIndex = 5
		AddHandler Me.blendPercentTxtbox.ModifiedChanged, New System.EventHandler(AddressOf Me.blendPercentTxtbox_ModifiedChanged)
		' 
		' inputRasterBtn
		' 
		Me.inputRasterBtn.Location = New System.Drawing.Point(384, 38)
		Me.inputRasterBtn.Name = "inputRasterBtn"
		Me.inputRasterBtn.Size = New System.Drawing.Size(31, 23)
		Me.inputRasterBtn.TabIndex = 6
		Me.inputRasterBtn.Text = "..."
		Me.inputRasterBtn.UseVisualStyleBackColor = True
		AddHandler Me.inputRasterBtn.Click, New System.EventHandler(AddressOf Me.inputRasterBtn_Click)
		' 
		' watermarkImageBtn
		' 
		Me.watermarkImageBtn.Location = New System.Drawing.Point(384, 119)
		Me.watermarkImageBtn.Name = "watermarkImageBtn"
		Me.watermarkImageBtn.Size = New System.Drawing.Size(31, 23)
		Me.watermarkImageBtn.TabIndex = 7
		Me.watermarkImageBtn.Text = "..."
		Me.watermarkImageBtn.UseVisualStyleBackColor = True
		AddHandler Me.watermarkImageBtn.Click, New System.EventHandler(AddressOf Me.watermarkImageBtn_Click)
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(381, 171)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(15, 13)
		Me.label1.TabIndex = 8
		Me.label1.Text = "%"
		' 
		' LocationLbl
		' 
		Me.LocationLbl.AutoSize = True
		Me.LocationLbl.Location = New System.Drawing.Point(23, 87)
		Me.LocationLbl.Name = "LocationLbl"
		Me.LocationLbl.Size = New System.Drawing.Size(103, 13)
		Me.LocationLbl.TabIndex = 10
		Me.LocationLbl.Text = "Watermark Location"
		' 
		' LocationComboBx
		' 
		Me.LocationComboBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.LocationComboBx.Items.AddRange(New Object() {"Top Left", "Top Right", "Center", "Bottom Left", "Bottom Right"})
		Me.LocationComboBx.Location = New System.Drawing.Point(191, 79)
		Me.LocationComboBx.Name = "LocationComboBx"
		Me.LocationComboBx.Size = New System.Drawing.Size(186, 21)
		Me.LocationComboBx.TabIndex = 9
		AddHandler Me.LocationComboBx.SelectedIndexChanged, New System.EventHandler(AddressOf Me.LocationComboBx_SelectedIndexChanged)
		' 
		' WatermarkFunctionUIForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(457, 201)
		Me.Controls.Add(Me.LocationLbl)
		Me.Controls.Add(Me.LocationComboBx)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.watermarkImageBtn)
		Me.Controls.Add(Me.inputRasterBtn)
		Me.Controls.Add(Me.blendPercentTxtbox)
		Me.Controls.Add(Me.watermarkImageTxtbox)
		Me.Controls.Add(Me.inputRasterTxtbox)
		Me.Controls.Add(Me.blendPercentLbl)
		Me.Controls.Add(Me.watermarkImageLbl)
		Me.Controls.Add(Me.inputRasterLbl)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Name = "WatermarkFunctionUIForm"
		Me.Text = "Watermark Raster Function"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private inputRasterLbl As System.Windows.Forms.Label
	Private watermarkImageLbl As System.Windows.Forms.Label
	Private blendPercentLbl As System.Windows.Forms.Label
	Private inputRasterTxtbox As System.Windows.Forms.TextBox
	Private watermarkImageTxtbox As System.Windows.Forms.TextBox
	Private blendPercentTxtbox As System.Windows.Forms.TextBox
	Private inputRasterBtn As System.Windows.Forms.Button
	Private watermarkImageBtn As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private watermarkImageDlg As System.Windows.Forms.OpenFileDialog
	Private LocationLbl As System.Windows.Forms.Label
	Private LocationComboBx As System.Windows.Forms.ComboBox
End Class

