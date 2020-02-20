'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
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
