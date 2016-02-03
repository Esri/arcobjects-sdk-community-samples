Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace GlobeGraphicsToolbar
	Public Class StyleGalleryCommand
		Inherits ESRI.ArcGIS.Desktop.AddIns.Button

		Private _styleGallery As StyleGallery = Nothing

		Public Sub New()
		End Sub

		Protected Overrides Sub OnClick()
			_styleGallery = New StyleGallery()

			If _styleGallery.IsStyleSelected() = True Then
				StyleGallerySelection.SetStyleGalleryItem(_styleGallery.StyleGalleryItem)
			End If
		End Sub

		Protected Overrides Sub OnUpdate()
		End Sub
	End Class
End Namespace
