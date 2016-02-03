Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Drawing

Namespace GlobeGraphicsToolbar
	Public Class ColorPalette
		Private _colorDialog As ColorDialog

		Public Sub New()
			_colorDialog = New ColorDialog()

			InitializeUI()

			SetDefaultColor()
		End Sub

		Private Sub InitializeUI()
			_colorDialog.FullOpen = True
		End Sub

		Private Sub SetDefaultColor()
			_colorDialog.Color = Color.Yellow
		End Sub

		Public Function IsColorSelected() As Boolean
			Return _colorDialog.ShowDialog() = DialogResult.OK
		End Function

		Public ReadOnly Property Red() As Integer
			Get
				Return CInt(Fix(_colorDialog.Color.R))
			End Get
		End Property

		Public ReadOnly Property Green() As Integer
			Get
				Return CInt(Fix(_colorDialog.Color.G))
			End Get
		End Property

		Public ReadOnly Property Blue() As Integer
			Get
				Return CInt(Fix(_colorDialog.Color.B))
			End Get
		End Property
	End Class
End Namespace