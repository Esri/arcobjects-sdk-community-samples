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
Imports System
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Geodatabase

'This sample shows the steps to create a customized pixelfilter
'INodataFilter filters out a range of values in a raster to be nodata
Public Interface INodataFilter
  Inherits IPixelFilter

  'INodataFilter members
  Property MinNodataValue() As Integer
  Property MaxNodataValue() As Integer

End Interface

Public Class NodataFilter
  Implements INodataFilter
  Sub Filter(ByVal pPixelBlock As IPixelBlock) Implements INodataFilter.Filter
    Dim x As Integer
    Dim y As Integer
    Try
      Dim pPixelBlock3 As IPixelBlock3 = CType(pPixelBlock, IPixelBlock3)

      Dim lookup() As Byte = New Byte(7) {128, 64, 32, 16, 8, 4, 2, 1}

      'get number of bands
      Dim plane As Integer = pPixelBlock.Planes

      'loop through each band
      Dim i As Integer
      For i = 0 To plane - 1
        'get nodata mask array
        Dim outputArray() As Byte = CType(pPixelBlock3.NoDataMask(i), Byte())

        'loop through each pixel in the pixelblock and do calculation

        For y = 0 To pPixelBlock.Height - 1
          For x = 0 To pPixelBlock.Width - 1
            'get index in the nodata mask byte array
            Dim ind As Integer = x + y * (pPixelBlock.Width)

            'get nodata mask byte 
            Dim nd As Byte = outputArray(ind \ 8)

            'get pixel value and check if it is nodata
            Dim tempVal As Object = pPixelBlock3.GetVal(i, x, y)

            If Not tempVal Is Nothing Then
              'convert pixel value to int and compare with nodata range

              Dim curVal As Integer = Convert.ToInt32(tempVal)
              If curVal >= MinNodataValue And curVal <= MaxNodataValue Then
                outputArray(ind \ 8) = CType((nd - lookup(ind Mod 8)), Byte)
              End If
            End If
          Next
        Next
        'set nodata mask array
        pPixelBlock3.NoDataMask(i) = outputArray
      Next
    Catch e As Exception
      Console.WriteLine(e.Message)
    End Try
  End Sub

  'implements IPixelFilter:GetCenterPosition
  Public Sub GetCenterPosition(ByRef x As Integer, ByRef y As Integer) Implements INodataFilter.GetCenterPosition
    x = 0
    y = 0
  End Sub

  'implements IPixelFilter:GetSize
  Public Sub GetSize(ByRef nCols As Integer, ByRef nRows As Integer) Implements INodataFilter.GetSize
    nCols = 0
    nRows = 0
  End Sub

  'get/set max range of nodata 
  Public Property MaxNodataValue() As Integer Implements INodataFilter.MaxNodataValue
    Get
      Return maxNDValue
    End Get
    Set(ByVal Value As Integer)
      maxNDValue = Value
    End Set
  End Property

  'get/set min range of nodata
  Public Property MinNodataValue() As Integer Implements INodataFilter.MinNodataValue
    Get
      Return minNDValue
    End Get
    Set(ByVal Value As Integer)
      minNDValue = Value
    End Set
  End Property

  Private minNDValue As Integer
  Private maxNDValue As Integer

End Class
