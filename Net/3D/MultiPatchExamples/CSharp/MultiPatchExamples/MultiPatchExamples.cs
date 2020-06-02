/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;

namespace MultiPatchExamples
{
    public partial class MultiPatchExamples : Form
    {
        private object _missing = Type.Missing;
        private IGraphicsContainer3D _axesGraphicsContainer3D;
        private IGraphicsContainer3D _outlineGraphicsContainer3D;
        private IGraphicsContainer3D _multiPatchGraphicsContainer3D;

        public MultiPatchExamples()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            _axesGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("Axes");
            _multiPatchGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("MultiPatch");
            _outlineGraphicsContainer3D = GraphicsLayer3DUtilities.ConstructGraphicsLayer3D("Outline");

            GraphicsLayer3DUtilities.DisableLighting(_multiPatchGraphicsContainer3D);

            axSceneControl.Scene.AddLayer(_axesGraphicsContainer3D as ILayer, true);
            axSceneControl.Scene.AddLayer(_multiPatchGraphicsContainer3D as ILayer, true);
            axSceneControl.Scene.AddLayer(_outlineGraphicsContainer3D as ILayer, true);

            DrawUtilities.DrawAxes(_axesGraphicsContainer3D);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleStrip1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleStripExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleStrip2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleStripExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleStrip3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleStripExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleStrip4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleStripExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleStrip5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleStripExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangleFan6Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TriangleFanExamples.GetExample6();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void triangles6Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = TrianglesExamples.GetExample6();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ring1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ring2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ring3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ring4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ring5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void vector3D1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Vector3DExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void vector3D2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Vector3DExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void vector3D3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Vector3DExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void transform3D1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Transform3DExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void transform3D2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Transform3DExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void transform3D3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Transform3DExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void transform3D4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Transform3DExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion6Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample6();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion7Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample7();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ringGroup1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingGroupExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ringGroup2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingGroupExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ringGroup3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingGroupExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ringGroup4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingGroupExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void ringGroup5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = RingGroupExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion8Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample8();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusionButton9_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample9();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion10Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample10();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion11Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample11();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion12Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample12();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion13Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample13();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void vector3D4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Vector3DExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void vector3D5Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = Vector3DExamples.GetExample5();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void composite1Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = CompositeExamples.GetExample1();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void composite2Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = CompositeExamples.GetExample2();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void composite3Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = CompositeExamples.GetExample3();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void composite4Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = CompositeExamples.GetExample4();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion14Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample14();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }

        private void extrusion15Button_Click(object sender, EventArgs e)
        {
            IGeometry geometry = ExtrusionExamples.GetExample15();

            DrawUtilities.DrawMultiPatch(_multiPatchGraphicsContainer3D, geometry);
            DrawUtilities.DrawOutline(_outlineGraphicsContainer3D, geometry);

            axSceneControl.SceneGraph.RefreshViewers();
        }
    }
}