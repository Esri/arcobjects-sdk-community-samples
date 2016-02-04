/*

   Copyright 2016 Esri

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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace ToolAndControlSampleCS
{
    /// <summary>
    /// Summary description for ToolSample.
    /// </summary>
    [Guid("7352d43a-8941-42ac-80ab-5cbf916d75d1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ToolAndControlSampleCS.ToolSample")]
    public sealed class ToolSample : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        private INewCircleFeedback m_circleFeedBack;
        private IPoint m_centerPoint, m_lastPoint;

        private IGeometryEnvironment m_geomEnv; //singleton, use it to calculate angle
        private ISimpleLineSymbol m_feedbackSymbol;

        public ToolSample()
        {
            base.m_category = ".NET Samples"; 
            base.m_caption = "Feedback Tool (C#)";
            base.m_message = "Circle feedback with color changes when pressing Ctrl key (C#)";
            base.m_toolTip = "Circle feedback (C#)";  
            base.m_name = "CSNETSamples_ToolSampleFeedback";  
            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = Cursors.Cross;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Set up
            m_geomEnv = new GeometryEnvironmentClass();

            m_feedbackSymbol = new SimpleLineSymbolClass();
            ((ISymbol)m_feedbackSymbol).ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            IRgbColor solidColor = new RgbColorClass();
            solidColor.Red = 255;
            m_feedbackSymbol.Color = solidColor;
            m_feedbackSymbol.Width = 2;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 1) //not the left mouse button, exit
                return;

            if (m_circleFeedBack == null)
            {
                m_circleFeedBack = new NewCircleFeedbackClass();
                m_circleFeedBack.Symbol = (ISymbol)m_feedbackSymbol;

                //Use AppDisplay to work on both Data and PageLayout view
                IScreenDisplay disp = ((IMxApplication)m_application).Display;
                m_centerPoint = disp.DisplayTransformation.ToMapPoint(X, Y);

                m_circleFeedBack.Display = disp;
                m_circleFeedBack.Start(m_centerPoint);
            }
            else
            {
                Reset(false);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (m_circleFeedBack != null)
            {
                IMxDocument mxDoc = m_application.Document as IMxDocument;

                //Get and cache last location just in case color change is requested in keydown to
                //refresh the feedback
                IScreenDisplay disp = ((IMxApplication)m_application).Display;
                m_lastPoint = disp.DisplayTransformation.ToMapPoint(X, Y);

                //Move feedback
                m_circleFeedBack.MoveTo(m_lastPoint);

                //Calculate angle to determine cursor
                IConstructAngle constructAngle = (IConstructAngle)m_geomEnv;
                ILine angleLine = new LineClass();
                angleLine.PutCoords(m_centerPoint, m_lastPoint);
                double angle = constructAngle.ConstructLine(angleLine);
                SetCursor(angle);
            }
        }
        
        public override void OnKeyDown(int keyCode, int Shift)
        {
            //Change feedback symbology
            if (Shift == 2 && m_circleFeedBack != null) //Ctrl
            {
                m_circleFeedBack.Stop();

                //change color randomly
                IRgbColor solidColor = new RgbColorClass();
                Random num = new Random();  
                solidColor.Red = num.Next(255);  
                solidColor.Green = num.Next(255);
                solidColor.Blue = num.Next(255);
                m_feedbackSymbol.Color = solidColor;

                //Restart the feedback with newly assigned color
                m_circleFeedBack.Start(m_centerPoint);
                m_circleFeedBack.MoveTo(m_lastPoint);
            }
        }

        public override bool Deactivate()
        {
            //Option 1: Always allow to deactivate. 
            //Reset things before deactivate (easier and recommended)
            Reset(true);
            return true;

            //Option 2: Do not allow deactivate in the middle of the feedback.
            //return (m_circleFeedBack == null);
        }

        public override void Refresh(int hDC)
        {
            //Refresh the feedback (IDisplayFeedback::Refresh)
            // (e.g. if the application has been covered and uncovered during the feedback operation)
            if (m_circleFeedBack != null)
                m_circleFeedBack.Refresh(hDC);
        }


        #endregion
        private void Reset(bool resetColor)
        {
            if (m_circleFeedBack != null)
            {
                m_circleFeedBack.Stop();
                m_circleFeedBack = null;
                base.m_cursor = Cursors.Cross;

                if (resetColor)
                {
                    //Reset symbol color to red
                    IRgbColor solidColor = new RgbColorClass();
                    solidColor.Red = 255;
                    m_feedbackSymbol.Color = solidColor;
                }
            }
        }
        private void SetCursor(double radianAngle)
        {
            double absAngle = Math.Abs(radianAngle);
            if (radianAngle < 0) // Southern portion
            {
                if (absAngle >= 0 && absAngle < Math.PI / 8) //E
                {
                    base.m_cursor = Cursors.PanEast;
                }
                else if (absAngle >= Math.PI / 8 && absAngle < 3 * Math.PI / 8) //SE
                {
                    base.m_cursor = Cursors.PanSE;
                }
                else if (absAngle >= 3 * Math.PI / 8 && absAngle < 5 * Math.PI / 8) //S
                {
                    base.m_cursor = Cursors.PanSouth;
                }
                else if (absAngle >= 5 * Math.PI / 8 && absAngle < 7 * Math.PI / 8)//SW
                {
                    base.m_cursor = Cursors.PanSW;
                }
                else //W
                {
                    base.m_cursor = Cursors.PanWest;
                }
            }
            else //northern
            {
                if (absAngle >= 0 && absAngle < Math.PI / 8) //E
                {
                    base.m_cursor = Cursors.PanEast;
                }
                else if (absAngle >= Math.PI / 8 && absAngle < 3 * Math.PI / 8) //NE
                {
                    base.m_cursor = Cursors.PanNE;
                }
                else if (absAngle >= 3 * Math.PI / 8 && absAngle < 5 * Math.PI / 8) //N
                {
                    base.m_cursor = Cursors.PanNorth;
                }
                else if (absAngle >= 5 * Math.PI / 8 && absAngle < 7 * Math.PI / 8)//NW
                {
                    base.m_cursor = Cursors.PanNW;
                }
                else //W
                {
                    base.m_cursor = Cursors.PanWest;
                }
            }

        }
    }
}
