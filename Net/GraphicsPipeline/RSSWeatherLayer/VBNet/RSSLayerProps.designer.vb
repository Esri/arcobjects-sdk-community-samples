Imports Microsoft.VisualBasic
Imports System

  Public Partial Class RSSLayerProps
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
	  Me.listBoxCityNames = New System.Windows.Forms.ListBox()
	  Me.label1 = New System.Windows.Forms.Label()
	  Me.SuspendLayout()
	  ' 
	  ' listBoxCityNames
	  ' 
	  Me.listBoxCityNames.FormattingEnabled = True
	  Me.listBoxCityNames.Location = New System.Drawing.Point(12, 25)
	  Me.listBoxCityNames.Name = "listBoxCityNames"
	  Me.listBoxCityNames.Size = New System.Drawing.Size(161, 264)
	  Me.listBoxCityNames.Sorted = True
	  Me.listBoxCityNames.TabIndex = 0
	  ' 
	  ' label1
	  ' 
	  Me.label1.AutoSize = True
	  Me.label1.Location = New System.Drawing.Point(12, 9)
	  Me.label1.Name = "label1"
	  Me.label1.Size = New System.Drawing.Size(63, 13)
	  Me.label1.TabIndex = 1
	  Me.label1.Text = "City Names:"
	  ' 
	  ' RSSLayerProps
	  ' 
	  Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
	  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	  Me.ClientSize = New System.Drawing.Size(180, 313)
	  Me.Controls.Add(Me.label1)
	  Me.Controls.Add(Me.listBoxCityNames)
	  Me.Name = "RSSLayerProps"
	  Me.Text = "RSS Layer output"
	  Me.ResumeLayout(False)
	  Me.PerformLayout()

	End Sub

	#End Region

	Private listBoxCityNames As System.Windows.Forms.ListBox
	Private label1 As System.Windows.Forms.Label
  End Class