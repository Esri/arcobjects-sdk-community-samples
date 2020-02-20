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
Imports Microsoft.VisualBasic
Imports System

Partial Public Class CacheManagerDlg
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
    Me.lblLayer = New System.Windows.Forms.Label
    Me.cboLayerNames = New System.Windows.Forms.ComboBox
    Me.btnOK = New System.Windows.Forms.Button
    Me.btnApply = New System.Windows.Forms.Button
    Me.btnDismiss = New System.Windows.Forms.Button
    Me.groupDrawingProps = New System.Windows.Forms.GroupBox
    Me.numDetaildThreshold = New System.Windows.Forms.NumericUpDown
    Me.lblDetailsThreshold = New System.Windows.Forms.Label
    Me.chkAlwaysDrawCoarsestLevel = New System.Windows.Forms.CheckBox
    Me.lblUseDefaultTexture = New System.Windows.Forms.Label
    Me.lblCacheFolderName = New System.Windows.Forms.Label
    Me.lblFolderName = New System.Windows.Forms.Label
    Me.btnFolderPath = New System.Windows.Forms.Button
    Me.lblCacheFolderPath = New System.Windows.Forms.Label
    Me.lblCachePathName = New System.Windows.Forms.Label
    Me.btnRestoreDefaults = New System.Windows.Forms.Button
    Me.numMaxCacheScale = New System.Windows.Forms.NumericUpDown
    Me.lblMaxCacheScale = New System.Windows.Forms.Label
    Me.chkStrictOnDemandMode = New System.Windows.Forms.CheckBox
    Me.lblStrictOnDemandMode = New System.Windows.Forms.Label
    Me.numProgressiveFetchingLevels = New System.Windows.Forms.NumericUpDown
    Me.lblProgressiveFetchingLevels = New System.Windows.Forms.Label
    Me.numProgressiveDrawingLevels = New System.Windows.Forms.NumericUpDown
    Me.lblProgressiveDrawingLevels = New System.Windows.Forms.Label
    Me.rdoJPEG = New System.Windows.Forms.RadioButton
    Me.rdoPNG = New System.Windows.Forms.RadioButton
    Me.label1 = New System.Windows.Forms.Label
    Me.groupDrawingProps.SuspendLayout()
    CType(Me.numDetaildThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.numMaxCacheScale, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.numProgressiveFetchingLevels, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.numProgressiveDrawingLevels, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'lblLayer
    '
    Me.lblLayer.AutoSize = True
    Me.lblLayer.Location = New System.Drawing.Point(13, 13)
    Me.lblLayer.Name = "lblLayer"
    Me.lblLayer.Size = New System.Drawing.Size(69, 13)
    Me.lblLayer.TabIndex = 0
    Me.lblLayer.Text = "Active Layer:"
    '
    'cboLayerNames
    '
    Me.cboLayerNames.FormattingEnabled = True
    Me.cboLayerNames.Location = New System.Drawing.Point(88, 10)
    Me.cboLayerNames.Name = "cboLayerNames"
    Me.cboLayerNames.Size = New System.Drawing.Size(320, 21)
    Me.cboLayerNames.TabIndex = 1
    '
    'btnOK
    '
    Me.btnOK.Location = New System.Drawing.Point(12, 634)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(75, 23)
    Me.btnOK.TabIndex = 2
    Me.btnOK.Text = "OK"
    Me.btnOK.UseVisualStyleBackColor = True
    '
    'btnApply
    '
    Me.btnApply.Location = New System.Drawing.Point(171, 634)
    Me.btnApply.Name = "btnApply"
    Me.btnApply.Size = New System.Drawing.Size(75, 23)
    Me.btnApply.TabIndex = 3
    Me.btnApply.Text = "Apply"
    Me.btnApply.UseVisualStyleBackColor = True
    '
    'btnDismiss
    '
    Me.btnDismiss.Location = New System.Drawing.Point(333, 634)
    Me.btnDismiss.Name = "btnDismiss"
    Me.btnDismiss.Size = New System.Drawing.Size(75, 23)
    Me.btnDismiss.TabIndex = 4
    Me.btnDismiss.Text = "Dismiss"
    Me.btnDismiss.UseVisualStyleBackColor = True
    '
    'groupDrawingProps
    '
    Me.groupDrawingProps.Controls.Add(Me.numDetaildThreshold)
    Me.groupDrawingProps.Controls.Add(Me.lblDetailsThreshold)
    Me.groupDrawingProps.Controls.Add(Me.chkAlwaysDrawCoarsestLevel)
    Me.groupDrawingProps.Controls.Add(Me.lblUseDefaultTexture)
    Me.groupDrawingProps.Controls.Add(Me.lblCacheFolderName)
    Me.groupDrawingProps.Controls.Add(Me.lblFolderName)
    Me.groupDrawingProps.Controls.Add(Me.btnFolderPath)
    Me.groupDrawingProps.Controls.Add(Me.lblCacheFolderPath)
    Me.groupDrawingProps.Controls.Add(Me.lblCachePathName)
    Me.groupDrawingProps.Controls.Add(Me.btnRestoreDefaults)
    Me.groupDrawingProps.Controls.Add(Me.numMaxCacheScale)
    Me.groupDrawingProps.Controls.Add(Me.lblMaxCacheScale)
    Me.groupDrawingProps.Controls.Add(Me.chkStrictOnDemandMode)
    Me.groupDrawingProps.Controls.Add(Me.lblStrictOnDemandMode)
    Me.groupDrawingProps.Controls.Add(Me.numProgressiveFetchingLevels)
    Me.groupDrawingProps.Controls.Add(Me.lblProgressiveFetchingLevels)
    Me.groupDrawingProps.Controls.Add(Me.numProgressiveDrawingLevels)
    Me.groupDrawingProps.Controls.Add(Me.lblProgressiveDrawingLevels)
    Me.groupDrawingProps.Controls.Add(Me.rdoJPEG)
    Me.groupDrawingProps.Controls.Add(Me.rdoPNG)
    Me.groupDrawingProps.Controls.Add(Me.label1)
    Me.groupDrawingProps.Location = New System.Drawing.Point(12, 37)
    Me.groupDrawingProps.Name = "groupDrawingProps"
    Me.groupDrawingProps.Size = New System.Drawing.Size(402, 571)
    Me.groupDrawingProps.TabIndex = 5
    Me.groupDrawingProps.TabStop = False
    Me.groupDrawingProps.Text = "Cache drawing properties"
    '
    'numDetaildThreshold
    '
    Me.numDetaildThreshold.DecimalPlaces = 2
    Me.numDetaildThreshold.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
    Me.numDetaildThreshold.Location = New System.Drawing.Point(261, 338)
    Me.numDetaildThreshold.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.numDetaildThreshold.Name = "numDetaildThreshold"
    Me.numDetaildThreshold.Size = New System.Drawing.Size(135, 20)
    Me.numDetaildThreshold.TabIndex = 20
    '
    'lblDetailsThreshold
    '
    Me.lblDetailsThreshold.AutoSize = True
    Me.lblDetailsThreshold.Location = New System.Drawing.Point(10, 346)
    Me.lblDetailsThreshold.Name = "lblDetailsThreshold"
    Me.lblDetailsThreshold.Size = New System.Drawing.Size(88, 13)
    Me.lblDetailsThreshold.TabIndex = 19
    Me.lblDetailsThreshold.Text = "Details threshold:"
    '
    'chkAlwaysDrawCoarsestLevel
    '
    Me.chkAlwaysDrawCoarsestLevel.AutoSize = True
    Me.chkAlwaysDrawCoarsestLevel.Location = New System.Drawing.Point(260, 266)
    Me.chkAlwaysDrawCoarsestLevel.Name = "chkAlwaysDrawCoarsestLevel"
    Me.chkAlwaysDrawCoarsestLevel.Size = New System.Drawing.Size(15, 14)
    Me.chkAlwaysDrawCoarsestLevel.TabIndex = 18
    Me.chkAlwaysDrawCoarsestLevel.UseVisualStyleBackColor = True
    '
    'lblUseDefaultTexture
    '
    Me.lblUseDefaultTexture.AutoSize = True
    Me.lblUseDefaultTexture.Location = New System.Drawing.Point(10, 266)
    Me.lblUseDefaultTexture.Name = "lblUseDefaultTexture"
    Me.lblUseDefaultTexture.Size = New System.Drawing.Size(137, 13)
    Me.lblUseDefaultTexture.TabIndex = 17
    Me.lblUseDefaultTexture.Text = "Always draw coarsest level:"
    '
    'lblCacheFolderName
    '
    Me.lblCacheFolderName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblCacheFolderName.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
    Me.lblCacheFolderName.Location = New System.Drawing.Point(10, 58)
    Me.lblCacheFolderName.Name = "lblCacheFolderName"
    Me.lblCacheFolderName.Size = New System.Drawing.Size(386, 20)
    Me.lblCacheFolderName.TabIndex = 16
    '
    'lblFolderName
    '
    Me.lblFolderName.AutoSize = True
    Me.lblFolderName.Location = New System.Drawing.Point(10, 36)
    Me.lblFolderName.Name = "lblFolderName"
    Me.lblFolderName.Size = New System.Drawing.Size(70, 13)
    Me.lblFolderName.TabIndex = 15
    Me.lblFolderName.Text = "Folder Name:"
    '
    'btnFolderPath
    '
    Me.btnFolderPath.Location = New System.Drawing.Point(368, 119)
    Me.btnFolderPath.Name = "btnFolderPath"
    Me.btnFolderPath.Size = New System.Drawing.Size(28, 93)
    Me.btnFolderPath.TabIndex = 14
    Me.btnFolderPath.Text = "..."
    Me.btnFolderPath.UseVisualStyleBackColor = True
    '
    'lblCacheFolderPath
    '
    Me.lblCacheFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblCacheFolderPath.Location = New System.Drawing.Point(10, 119)
    Me.lblCacheFolderPath.Name = "lblCacheFolderPath"
    Me.lblCacheFolderPath.Size = New System.Drawing.Size(352, 93)
    Me.lblCacheFolderPath.TabIndex = 13
    Me.lblCacheFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblCachePathName
    '
    Me.lblCachePathName.AutoSize = True
    Me.lblCachePathName.Location = New System.Drawing.Point(7, 96)
    Me.lblCachePathName.Name = "lblCachePathName"
    Me.lblCachePathName.Size = New System.Drawing.Size(63, 13)
    Me.lblCachePathName.TabIndex = 12
    Me.lblCachePathName.Text = "Folder path:"
    '
    'btnRestoreDefaults
    '
    Me.btnRestoreDefaults.Location = New System.Drawing.Point(260, 531)
    Me.btnRestoreDefaults.Name = "btnRestoreDefaults"
    Me.btnRestoreDefaults.Size = New System.Drawing.Size(136, 23)
    Me.btnRestoreDefaults.TabIndex = 11
    Me.btnRestoreDefaults.Text = "Restore Defaults"
    Me.btnRestoreDefaults.UseVisualStyleBackColor = True
    '
    'numMaxCacheScale
    '
    Me.numMaxCacheScale.Location = New System.Drawing.Point(261, 488)
    Me.numMaxCacheScale.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
    Me.numMaxCacheScale.Name = "numMaxCacheScale"
    Me.numMaxCacheScale.Size = New System.Drawing.Size(135, 20)
    Me.numMaxCacheScale.TabIndex = 10
    '
    'lblMaxCacheScale
    '
    Me.lblMaxCacheScale.AutoSize = True
    Me.lblMaxCacheScale.Location = New System.Drawing.Point(10, 490)
    Me.lblMaxCacheScale.Name = "lblMaxCacheScale"
    Me.lblMaxCacheScale.Size = New System.Drawing.Size(115, 13)
    Me.lblMaxCacheScale.TabIndex = 9
    Me.lblMaxCacheScale.Text = "Maximum cache scale:"
    '
    'chkStrictOnDemandMode
    '
    Me.chkStrictOnDemandMode.AutoSize = True
    Me.chkStrictOnDemandMode.Location = New System.Drawing.Point(261, 304)
    Me.chkStrictOnDemandMode.Name = "chkStrictOnDemandMode"
    Me.chkStrictOnDemandMode.Size = New System.Drawing.Size(15, 14)
    Me.chkStrictOnDemandMode.TabIndex = 8
    Me.chkStrictOnDemandMode.UseVisualStyleBackColor = True
    '
    'lblStrictOnDemandMode
    '
    Me.lblStrictOnDemandMode.AutoSize = True
    Me.lblStrictOnDemandMode.Location = New System.Drawing.Point(7, 304)
    Me.lblStrictOnDemandMode.Name = "lblStrictOnDemandMode"
    Me.lblStrictOnDemandMode.Size = New System.Drawing.Size(119, 13)
    Me.lblStrictOnDemandMode.TabIndex = 7
    Me.lblStrictOnDemandMode.Text = "Strict on demand mode:"
    '
    'numProgressiveFetchingLevels
    '
    Me.numProgressiveFetchingLevels.Location = New System.Drawing.Point(261, 438)
    Me.numProgressiveFetchingLevels.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
    Me.numProgressiveFetchingLevels.Name = "numProgressiveFetchingLevels"
    Me.numProgressiveFetchingLevels.Size = New System.Drawing.Size(135, 20)
    Me.numProgressiveFetchingLevels.TabIndex = 6
    '
    'lblProgressiveFetchingLevels
    '
    Me.lblProgressiveFetchingLevels.AutoSize = True
    Me.lblProgressiveFetchingLevels.Location = New System.Drawing.Point(7, 438)
    Me.lblProgressiveFetchingLevels.Name = "lblProgressiveFetchingLevels"
    Me.lblProgressiveFetchingLevels.Size = New System.Drawing.Size(136, 13)
    Me.lblProgressiveFetchingLevels.TabIndex = 5
    Me.lblProgressiveFetchingLevels.Text = "Progressive fetching levels:"
    '
    'numProgressiveDrawingLevels
    '
    Me.numProgressiveDrawingLevels.Location = New System.Drawing.Point(261, 387)
    Me.numProgressiveDrawingLevels.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
    Me.numProgressiveDrawingLevels.Name = "numProgressiveDrawingLevels"
    Me.numProgressiveDrawingLevels.Size = New System.Drawing.Size(135, 20)
    Me.numProgressiveDrawingLevels.TabIndex = 4
    '
    'lblProgressiveDrawingLevels
    '
    Me.lblProgressiveDrawingLevels.AutoSize = True
    Me.lblProgressiveDrawingLevels.Location = New System.Drawing.Point(7, 387)
    Me.lblProgressiveDrawingLevels.Name = "lblProgressiveDrawingLevels"
    Me.lblProgressiveDrawingLevels.Size = New System.Drawing.Size(135, 13)
    Me.lblProgressiveDrawingLevels.TabIndex = 3
    Me.lblProgressiveDrawingLevels.Text = "Progressive drawing levels:"
    '
    'rdoJPEG
    '
    Me.rdoJPEG.AutoSize = True
    Me.rdoJPEG.Location = New System.Drawing.Point(344, 224)
    Me.rdoJPEG.Name = "rdoJPEG"
    Me.rdoJPEG.Size = New System.Drawing.Size(52, 17)
    Me.rdoJPEG.TabIndex = 2
    Me.rdoJPEG.TabStop = True
    Me.rdoJPEG.Text = "JPEG"
    Me.rdoJPEG.UseVisualStyleBackColor = True
    '
    'rdoPNG
    '
    Me.rdoPNG.AutoSize = True
    Me.rdoPNG.Location = New System.Drawing.Point(260, 224)
    Me.rdoPNG.Name = "rdoPNG"
    Me.rdoPNG.Size = New System.Drawing.Size(48, 17)
    Me.rdoPNG.TabIndex = 1
    Me.rdoPNG.TabStop = True
    Me.rdoPNG.Text = "PNG"
    Me.rdoPNG.UseVisualStyleBackColor = True
    '
    'label1
    '
    Me.label1.AutoSize = True
    Me.label1.Location = New System.Drawing.Point(7, 224)
    Me.label1.Name = "label1"
    Me.label1.Size = New System.Drawing.Size(42, 13)
    Me.label1.TabIndex = 0
    Me.label1.Text = "Format:"
    '
    'CacheManagerDlg
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(426, 669)
    Me.Controls.Add(Me.groupDrawingProps)
    Me.Controls.Add(Me.btnDismiss)
    Me.Controls.Add(Me.btnApply)
    Me.Controls.Add(Me.btnOK)
    Me.Controls.Add(Me.cboLayerNames)
    Me.Controls.Add(Me.lblLayer)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "CacheManagerDlg"
    Me.ShowInTaskbar = False
    Me.Text = "CacheManagerDlg"
    Me.TopMost = True
    Me.groupDrawingProps.ResumeLayout(False)
    Me.groupDrawingProps.PerformLayout()
    CType(Me.numDetaildThreshold, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.numMaxCacheScale, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.numProgressiveFetchingLevels, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.numProgressiveDrawingLevels, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

#End Region

	Private lblLayer As System.Windows.Forms.Label
	Private WithEvents cboLayerNames As System.Windows.Forms.ComboBox
	Private WithEvents btnOK As System.Windows.Forms.Button
	Private WithEvents btnApply As System.Windows.Forms.Button
	Private WithEvents btnDismiss As System.Windows.Forms.Button
	Private groupDrawingProps As System.Windows.Forms.GroupBox
	Private rdoJPEG As System.Windows.Forms.RadioButton
	Private rdoPNG As System.Windows.Forms.RadioButton
	Private label1 As System.Windows.Forms.Label
	Private lblProgressiveFetchingLevels As System.Windows.Forms.Label
	Private numProgressiveDrawingLevels As System.Windows.Forms.NumericUpDown
	Private lblProgressiveDrawingLevels As System.Windows.Forms.Label
	Private chkStrictOnDemandMode As System.Windows.Forms.CheckBox
	Private lblStrictOnDemandMode As System.Windows.Forms.Label
	Private numProgressiveFetchingLevels As System.Windows.Forms.NumericUpDown
	Private numMaxCacheScale As System.Windows.Forms.NumericUpDown
	Private lblMaxCacheScale As System.Windows.Forms.Label
	Private WithEvents btnRestoreDefaults As System.Windows.Forms.Button
	Private lblCacheFolderPath As System.Windows.Forms.Label
	Private lblCachePathName As System.Windows.Forms.Label
	Private WithEvents btnFolderPath As System.Windows.Forms.Button
	Private lblCacheFolderName As System.Windows.Forms.Label
	Private lblFolderName As System.Windows.Forms.Label
	Private chkAlwaysDrawCoarsestLevel As System.Windows.Forms.CheckBox
	Private lblUseDefaultTexture As System.Windows.Forms.Label
	Private lblDetailsThreshold As System.Windows.Forms.Label
	Private numDetaildThreshold As System.Windows.Forms.NumericUpDown
End Class
