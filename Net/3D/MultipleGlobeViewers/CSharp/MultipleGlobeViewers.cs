using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;


namespace MultipleGlobeViewers
{
    public class MultipleGlobeViewers : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        #region Member Variables

        private ESRI.ArcGIS.GlobeCore.IGlobe globe;
        private ESRI.ArcGIS.GlobeCore.IGlobeDisplay globeDisplay;
        private ESRI.ArcGIS.GlobeCore.IGlobeCamera globeCamera;
        private ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event globeDispEvent;
        private SecondaryViewerForm theForm;
        private ESRI.ArcGIS.Analyst3D.ISceneViewer viewer;
        private ESRI.ArcGIS.GlobeCore.IGlobeCamera viewerGlobeCamera;
        private bool topDownView = false;
        private string viewerCaption = "";

        #endregion

        #region DLLImportFunction

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        static extern int ShowWindow(int hwnd, int nCmdShow);


        #endregion

        public MultipleGlobeViewers()
        {
            globe = ArcGlobe.Globe;
            globeDisplay = globe.GlobeDisplay;
            globeCamera = (ESRI.ArcGIS.GlobeCore.IGlobeCamera)globeDisplay.ActiveViewer.Camera;
            globeDispEvent = (ESRI.ArcGIS.GlobeCore.IGlobeDisplayEvents_Event)globeDisplay;
        }

        protected override void OnClick()
        {
            theForm = new SecondaryViewerForm();
            //register form's events
            theForm.topDownButton.Click += new EventHandler(topDownButton_Click);
            theForm.normalButton.Click += new EventHandler(normalButton_Click);
            theForm.FormClosing += new FormClosingEventHandler(theForm_FormClosing);
            //get viewer list
            getListOfSecondaryViewers();
            //register the ArcGlobe globe's display afterdraw event
            globeDispEvent.AfterDraw += new IGlobeDisplayEvents_AfterDrawEventHandler(globeDispEvent_AfterDraw);

            theForm.Show();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }

        #region Custom Methods

        void globeDispEvent_AfterDraw(ISceneViewer pViewer)
        {
            double obsLat;
            double obsLon;
            double obsAlt;
            double tarLat;
            double tarLon;
            double tarAlt;
            globeCamera.GetObserverLatLonAlt(out obsLat, out obsLon, out obsAlt);
            globeCamera.GetTargetLatLonAlt(out tarLat, out tarLon, out tarAlt);
            //set the observer and target of the secondary viewer to be the same as main viewer if top-down view = false
            if (topDownView == false || globeCamera.OrientationMode == esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal)
            {
                viewerGlobeCamera.OrientationMode = globeCamera.OrientationMode;
                viewerGlobeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt);
                viewerGlobeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt);
            }
            //set the observer top down view for the secondary viewer
            else if (topDownView == true && globeCamera.OrientationMode == esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal)
            {
                viewerGlobeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal;
                tarLat = obsLat + 0.0000001;
                tarLon = obsLon + 0.0000001;
                viewerGlobeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt);
            }
        }

        void getListOfSecondaryViewers()
        {
            ESRI.ArcGIS.esriSystem.IArray viewers = globeDisplay.GetAllViewers();
            if (viewers.Count < 2) return;
            for (int i = 0; i < viewers.Count; i++)
            {
                ESRI.ArcGIS.Analyst3D.ISceneViewer viewerElement = (ESRI.ArcGIS.Analyst3D.ISceneViewer)viewers.get_Element(i);
                if (viewerElement.Caption != "Globe view")
                {
                    theForm.viewerListBox.Items.Add(viewerElement.Caption);
                }
            }

        }

        void normalButton_Click(object sender, EventArgs e)
        {
            topDownView = false;
            viewerCaption = theForm.viewerListBox.SelectedItem.ToString();
            viewer = (ESRI.ArcGIS.Analyst3D.ISceneViewer)globeDisplay.FindViewer(viewerCaption);
            viewerGlobeCamera = (ESRI.ArcGIS.GlobeCore.IGlobeCamera)viewer.Camera;
        }

        void topDownButton_Click(object sender, EventArgs e)
        {
            topDownView = true;
            viewerCaption = theForm.viewerListBox.SelectedItem.ToString();
            viewer = (ESRI.ArcGIS.Analyst3D.ISceneViewer)globeDisplay.FindViewer(viewerCaption);
            viewerGlobeCamera = (ESRI.ArcGIS.GlobeCore.IGlobeCamera)viewer.Camera;
        }

        void theForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //unregister the ArcGlobe globe's display afterdraw event
            globeDispEvent.AfterDraw -= new IGlobeDisplayEvents_AfterDrawEventHandler(globeDispEvent_AfterDraw);
        }
        #endregion
    }
}
