using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace CommandSubtypeCS
{
    /// <summary>
    /// Summary description for PolyFeedbackTools.
    /// </summary>
    [Guid("4bcc9528-bc20-42eb-baf7-0b08373ce986")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CommandSubtypeCS.PolyFeedbackTools")]
    public sealed class PolyFeedbackTools : BaseTool, ICommandSubType
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

        private INewPolygonFeedback m_polygonFeedback;
        private IScreenDisplay m_screenDisplay;
        private int m_maxSides;
        private int m_currentSides;

        public PolyFeedbackTools()
        {
            base.m_category = ".NET Samples";
            base.m_cursor = Cursors.Cross;
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            IMxDocument mxDoc = m_application.Document as IMxDocument;
            m_screenDisplay = mxDoc.ActiveView.ScreenDisplay;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                IPoint cursorPoint = m_screenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                if (m_polygonFeedback == null)
                {
                    m_currentSides = 0;
                    m_polygonFeedback = new NewPolygonFeedbackClass();
                    m_polygonFeedback.Display = m_screenDisplay;

                    m_polygonFeedback.Start(cursorPoint);
                }
                else
                {
                    m_polygonFeedback.AddPoint(cursorPoint);
                }

                m_currentSides++;
                if (m_currentSides == m_maxSides) //Finish
                {
                    IPolygon polygon = m_polygonFeedback.Stop();

                    //Report area on status bar
                    IArea feedBackArea = polygon as IArea;
                    m_application.StatusBar.set_Message(0, "Feedback: area = " + Math.Abs(feedBackArea.Area).ToString());

                    m_polygonFeedback = null;
                }
                else
                {
                    //Report vertex remaining
                    m_application.StatusBar.set_Message(0, string.Format("Feedback: {0} point(s) remaining", m_maxSides - m_currentSides));
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (m_polygonFeedback != null)
            {
                IPoint cursorPoint = m_screenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_polygonFeedback.MoveTo(cursorPoint);
            }
        }
        public override bool Deactivate()
        {
            if (m_polygonFeedback != null)
            {
                m_polygonFeedback.Stop();
                m_polygonFeedback = null;
            }
            m_screenDisplay = null;

            return true;
        }

        #endregion

        #region ICommandSubType Members

        public int GetCount()
        {
            return 3;
        }

        public void SetSubType(int SubType)
        {
            //Set up bitmap, caption, tooltip etc.
            if (base.Bitmap == 0)
            {
                switch (SubType)
                {
                    case 1:
                        base.m_bitmap = Properties.Resources.FeedBack3;
                        break;
                    case 2:
                        base.m_bitmap = Properties.Resources.FeedBack4;
                        break;
                    case 3:
                        base.m_bitmap = Properties.Resources.FeedBack5;
                        break;
                }
            }

            base.m_name = string.Format("CSNETSamples_SubTypeTool{0}", SubType);

            m_maxSides = SubType + 2; //3, 4 or 5 sides
            base.m_caption = string.Format("{0} sides feedback (C#)", m_maxSides);
            base.m_toolTip = string.Format("{0} sides feedback", m_maxSides);
            base.m_message = string.Format("Tool demonstrating {0} sides polygon feedback", m_maxSides);
        }

        #endregion
    }
}
