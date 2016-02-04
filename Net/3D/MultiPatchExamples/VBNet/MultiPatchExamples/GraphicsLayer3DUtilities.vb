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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Analyst3D


Public Class GraphicsLayer3DUtilities
    Private Sub New()
    End Sub
    Public Shared Function ConstructGraphicsLayer3D(ByVal name As String) As IGraphicsContainer3D
        Dim graphicsContainer3D As IGraphicsContainer3D = New GraphicsLayer3DClass()

        Dim layer As ILayer = TryCast(graphicsContainer3D, ILayer)
        layer.Name = name

        Return graphicsContainer3D
    End Function

    Public Shared Sub DisableLighting(ByVal graphicsContainer3D As IGraphicsContainer3D)
        Dim properties3D As I3DProperties = New Basic3DPropertiesClass()
        properties3D.Illuminate = False

        Dim layerExtensions As ILayerExtensions = TryCast(graphicsContainer3D, ILayerExtensions)
        layerExtensions.AddExtension(properties3D)

        properties3D.Apply3DProperties(graphicsContainer3D)
    End Sub

    Public Shared Sub AddAxisToGraphicsLayer3D(ByVal graphicsContainer3D As IGraphicsContainer3D, ByVal geometry As IGeometry, ByVal color As IColor, ByVal style As esriSimple3DLineStyle, ByVal width As Double)
        graphicsContainer3D.AddElement(ElementUtilities.ConstructPolylineElement(geometry, color, style, width))
    End Sub

    Public Shared Sub AddOutlineToGraphicsLayer3D(ByVal graphicsContainer3D As IGraphicsContainer3D, ByVal geometryCollection As IGeometryCollection, ByVal color As IColor, ByVal style As esriSimple3DLineStyle, ByVal width As Double)
        Dim i As Integer = 0
        Do While i < geometryCollection.GeometryCount
            Dim geometry As IGeometry = geometryCollection.Geometry(i)

            graphicsContainer3D.AddElement(ElementUtilities.ConstructPolylineElement(geometry, color, style, width))
            i += 1
        Loop
    End Sub

    Public Shared Sub AddMultiPatchToGraphicsLayer3D(ByVal graphicsContainer3D As IGraphicsContainer3D, ByVal geometry As IGeometry, ByVal color As IColor)
        graphicsContainer3D.AddElement(ElementUtilities.ConstructMultiPatchElement(geometry, color))
    End Sub
End Class
