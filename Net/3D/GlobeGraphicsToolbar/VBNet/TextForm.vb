Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace GlobeGraphicsToolbar
	Partial Public Class TextForm
		Inherits Form
		Private _inputText As String

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub textBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles textBox1.KeyUp
			If e.KeyCode = Keys.Enter Then
				_inputText = Me.textBox1.Text
				Me.Close()
			ElseIf e.KeyCode = Keys.Escape Then
				Me.Close()
			End If
		End Sub

		Public ReadOnly Property InputText() As String
			Get
				Return _inputText
			End Get
		End Property
	End Class
End Namespace