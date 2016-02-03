Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Display


Namespace GlobeGraphicsToolbar
	Partial Public Class StyleGalleryForm
		Inherits Form
		Private _styleGalleryItem As IStyleGalleryItem = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub axSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles axSymbologyControl1.OnItemSelected
			_styleGalleryItem = TryCast(e.styleGalleryItem, IStyleGalleryItem)
		End Sub

		Public ReadOnly Property StyleGalleryItem() As IStyleGalleryItem
			Get
				Return _styleGalleryItem
			End Get
		End Property

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Hide()
		End Sub


	End Class
End Namespace
