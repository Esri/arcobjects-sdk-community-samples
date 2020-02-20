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
Imports ESRI.ArcGIS.Display


Public Enum TransparencyType
    Transparent = 0
    Opaque = 255
End Enum

Public Class ColorUtilities
    Private Shared _transparency As TransparencyType = TransparencyType.Opaque

    Private Sub New()
    End Sub
    Public Shared Function GetColor(ByVal red As Integer, ByVal green As Integer, ByVal blue As Integer) As IColor
        Dim rgbColor As IRgbColor = New RgbColorClass()
        rgbColor.Red = red
        rgbColor.Green = green
        rgbColor.Blue = blue

        Dim color As IColor = TryCast(rgbColor, IColor)
        color.Transparency = CByte(_transparency)

        Return color
    End Function
End Class
