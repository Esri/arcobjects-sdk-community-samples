Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Windows.Forms


Namespace GlobeGraphicsToolbar
	Public Class ColorCommand
		Inherits ESRI.ArcGIS.Desktop.AddIns.Button
		Private _colorPalette As ColorPalette = Nothing

		Public Sub New()
			_colorPalette = New ColorPalette()

			ColorSelection.SetColor(_colorPalette.Red, _colorPalette.Green, _colorPalette.Blue)
		End Sub

		Protected Overrides Sub OnClick()
			If _colorPalette.IsColorSelected() = True Then
				ColorSelection.SetColor(_colorPalette.Red, _colorPalette.Green, _colorPalette.Blue)
			End If
		End Sub

		Protected Overrides Sub OnUpdate()
			Enabled = ArcGlobe.Application IsNot Nothing
		End Sub
	End Class
End Namespace
