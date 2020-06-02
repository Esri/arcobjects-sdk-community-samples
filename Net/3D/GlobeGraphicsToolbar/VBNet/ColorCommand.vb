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
