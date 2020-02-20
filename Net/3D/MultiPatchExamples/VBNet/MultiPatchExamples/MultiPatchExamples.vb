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
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto


Partial Public Class MultiPatchExamples : Inherits Form
    Private _missing As Object = Type.Missing
    Private _axesGraphicsContainer3D As IGraphicsContainer3D
    Private _outlineGraphicsContainer3D As IGraphicsContainer3D
    Private _multiPatchGraphicsContainer3D As IGraphicsContainer3D

    Public Sub New()
        InitializeComponent()

        Initialize()
    End Sub

    Private Sub Initialize()
        _axesGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("Axes")
        _multiPatchGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("MultiPatch")
        _outlineGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("Outline")

        GraphicsLayer3DUtilities.DisableLighting(_multiPatchGraphicsContainer3D)

        axSceneControl.Scene.AddLayer(TryCast(_axesGraphicsContainer3D, ILayer), True)
        axSceneControl.Scene.AddLayer(TryCast(_multiPatchGraphicsContainer3D, ILayer), True)
        axSceneControl.Scene.AddLayer(TryCast(_outlineGraphicsContainer3D, ILayer), True)

        DrawUtilities.DrawAxes(_axesGraphicsContainer3D)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleStrip1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleStrip1Button.Click
        Dim geometry As IGeometry = TriangleStripExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleStrip2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleStrip2Button.Click
        Dim geometry As IGeometry = TriangleStripExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleStrip3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleStrip3Button.Click
        Dim geometry As IGeometry = TriangleStripExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleStrip4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleStrip4Button.Click
        Dim geometry As IGeometry = TriangleStripExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleStrip5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleStrip5Button.Click
        Dim geometry As IGeometry = TriangleStripExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan1Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan2Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan3Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan4Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan5Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangleFan6Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangleFan6Button.Click
        Dim geometry As IGeometry = TriangleFanExamples.GetExample6()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles1Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles2Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles3Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles4Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles5Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub triangles6Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triangles6Button.Click
        Dim geometry As IGeometry = TrianglesExamples.GetExample6()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ring1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ring1Button.Click
        Dim geometry As IGeometry = RingExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ring2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ring2Button.Click
        Dim geometry As IGeometry = RingExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ring3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ring3Button.Click
        Dim geometry As IGeometry = RingExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ring4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ring4Button.Click
        Dim geometry As IGeometry = RingExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ring5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ring5Button.Click
        Dim geometry As IGeometry = RingExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub vector3D1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vector3D1Button.Click
        Dim geometry As IGeometry = Vector3DExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub vector3D2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vector3D2Button.Click
        Dim geometry As IGeometry = Vector3DExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub vector3D3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vector3D3Button.Click
        Dim geometry As IGeometry = Vector3DExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub transform3D1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles transform3D1Button.Click
        Dim geometry As IGeometry = Transform3DExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub transform3D2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles transform3D2Button.Click
        Dim geometry As IGeometry = Transform3DExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub transform3D3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles transform3D3Button.Click
        Dim geometry As IGeometry = Transform3DExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub transform3D4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles transform3D4Button.Click
        Dim geometry As IGeometry = Transform3DExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion1Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion2Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion3Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion4Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion5Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion6Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion6Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample6()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion7Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion7Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample7()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ringGroup1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ringGroup1Button.Click
        Dim geometry As IGeometry = RingGroupExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ringGroup2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ringGroup2Button.Click
        Dim geometry As IGeometry = RingGroupExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ringGroup3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ringGroup3Button.Click
        Dim geometry As IGeometry = RingGroupExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ringGroup4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ringGroup4Button.Click
        Dim geometry As IGeometry = RingGroupExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub ringGroup5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ringGroup5Button.Click
        Dim geometry As IGeometry = RingGroupExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion8Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion8Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample8()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusionButton9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusionButton9.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample9()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion10Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion10Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample10()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion11Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion11Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample11()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion12Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion12Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample12()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion13Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion13Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample13()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub vector3D4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vector3D4Button.Click
        Dim geometry As IGeometry = Vector3DExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub vector3D5Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles vector3D5Button.Click
        Dim geometry As IGeometry = Vector3DExamples.GetExample5()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub composite1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles composite1Button.Click
        Dim geometry As IGeometry = CompositeExamples.GetExample1()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub composite2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles composite2Button.Click
        Dim geometry As IGeometry = CompositeExamples.GetExample2()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub composite3Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles composite3Button.Click
        Dim geometry As IGeometry = CompositeExamples.GetExample3()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub composite4Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles composite4Button.Click
        Dim geometry As IGeometry = CompositeExamples.GetExample4()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion14Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion14Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample14()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub

    Private Sub extrusion15Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles extrusion15Button.Click
        Dim geometry As IGeometry = ExtrusionExamples.GetExample15()

        DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry)
        DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry)

        axSceneControl.SceneGraph.RefreshViewers()
    End Sub
End Class
