Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Display
Imports GlobeGraphicsToolbar
Namespace GlobeGraphicsToolbar
	Public Class StyleGallery
		Private _styleForm As StyleGalleryForm

		Public Sub New()
			_styleForm = New StyleGalleryForm()

			InitializeUI()
		End Sub

		Private Sub InitializeUI()
			_styleForm.AutoSize = True
			_styleForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
			_styleForm.MaximizeBox = False
			_styleForm.MinimizeBox = False
			_styleForm.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
			_styleForm.Text = "Style Gallery"
		End Sub

		Public Function IsStyleSelected() As Boolean
			Return _styleForm.ShowDialog() = DialogResult.OK
		End Function

		Public ReadOnly Property StyleGalleryItem() As IStyleGalleryItem
			Get
				Return _styleForm.StyleGalleryItem
			End Get
		End Property
	End Class
End Namespace