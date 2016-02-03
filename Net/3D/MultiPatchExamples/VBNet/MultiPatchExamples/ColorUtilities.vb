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
