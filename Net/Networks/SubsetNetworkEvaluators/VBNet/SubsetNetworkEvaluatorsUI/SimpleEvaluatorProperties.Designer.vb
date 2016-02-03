Imports Microsoft.VisualBasic
Imports System
Namespace SubsetNetworkEvaluatorsUI
	Public Partial Class SimpleEvaluatorProperties
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
			Me.lblEvaluatorDescription = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' lblEvaluatorDescription
			' 
			Me.lblEvaluatorDescription.AutoSize = True
			Me.lblEvaluatorDescription.Dock = System.Windows.Forms.DockStyle.Fill
			Me.lblEvaluatorDescription.Location = New System.Drawing.Point(0, 0)
			Me.lblEvaluatorDescription.Name = "lblEvaluatorDescription"
			Me.lblEvaluatorDescription.Size = New System.Drawing.Size(93, 13)
			Me.lblEvaluatorDescription.TabIndex = 0
			Me.lblEvaluatorDescription.Text = "Custom Evaluator:"
			' 
			' SimpleEvaluatorProperties
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(673, 323)
			Me.Controls.Add(Me.lblEvaluatorDescription)
			Me.Name = "SimpleEvaluatorProperties"
			Me.Text = "Parameterized Evaluator Properties"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private lblEvaluatorDescription As System.Windows.Forms.Label
	End Class
End Namespace