Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace SubsetNetworkEvaluatorsUI
	''' <summary>
	''' Simple informational properties dialog for displaying read only information
	''' about the chosen subset evaluator from the evaluators dialog in ArcCatalog.
	''' </summary>
	Partial Public Class SimpleEvaluatorProperties : Inherits Form
		Public Sub New(ByVal description As String)
			InitializeComponent()
			lblEvaluatorDescription.Text = description
		End Sub
	End Class
End Namespace