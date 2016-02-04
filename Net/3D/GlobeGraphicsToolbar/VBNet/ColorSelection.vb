'Copyright 2016 Esri

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
Imports ESRI.ArcGIS.Display

Namespace GlobeGraphicsToolbar
	Public Class ColorSelection
		Private Shared _color As IColor = Nothing

		Public Shared Sub SetColor(ByVal red As Integer, ByVal green As Integer, ByVal blue As Integer)
			Dim rgbColor As IRgbColor = New RgbColorClass()
			rgbColor.Red = red
			rgbColor.Green = green
			rgbColor.Blue = blue

			_color = TryCast(rgbColor, IColor)
		End Sub

        Public Shared Function GetColor() As IColor

            'set default color = red if _color = nothing
            If _color Is Nothing Then
                Dim rgbColor As IRgbColor = New RgbColorClass()
                rgbColor.Red = 255
                rgbColor.Green = 0
                rgbColor.Blue = 0

                _color = TryCast(rgbColor, IColor)
            End If
            Return _color
        End Function
	End Class
End Namespace