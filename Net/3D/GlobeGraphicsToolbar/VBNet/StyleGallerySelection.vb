Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Display

Namespace GlobeGraphicsToolbar
	Public Class StyleGallerySelection
		Private Shared _styleGalleryItem As IStyleGalleryItem = Nothing

		Public Shared Sub SetStyleGalleryItem(ByVal styleGalleryItem As IStyleGalleryItem)
			_styleGalleryItem = styleGalleryItem
		End Sub

		Public Shared Function GetStyleGalleryItem() As IStyleGalleryItem
			Return _styleGalleryItem
		End Function
	End Class
End Namespace