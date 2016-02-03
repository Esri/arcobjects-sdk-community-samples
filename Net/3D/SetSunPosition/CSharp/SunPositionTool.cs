using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Analyst3D;

namespace SetSunPosition
{
    public class SunPositionTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        #region Member Variables

        private IGlobe m_globe = null;
        private IGlobeCamera m_globeCamera = null;
        private IGlobeViewUtil m_globeViewUtil = null;
        private IGlobeDisplay3 m_globeDisplay = null;
        private IGlobeDisplayRendering m_globeDisplayRendering = null;
        private ISceneViewer m_sceneViewer = null;
        private bool m_bDrawPoint = false;

        #endregion

        public SunPositionTool()
        {
            //get the different members
            m_globe = ArcGlobe.Globe;
            m_globeDisplay = m_globe.GlobeDisplay as IGlobeDisplay3;
            m_globeDisplayRendering = m_globeDisplay as IGlobeDisplayRendering;
            m_globeCamera = m_globeDisplay.ActiveViewer.Camera as IGlobeCamera;
            m_globeViewUtil = m_globeCamera as IGlobeViewUtil;
            m_sceneViewer = m_globeDisplay.ActiveViewer;
        }

        #region Tool overrides

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;
        }

        protected override void OnActivate()
        {
            //Enable the light source
            m_globeDisplayRendering.IsSunEnabled = true;
            //set an ambient light
            m_globeDisplayRendering.AmbientLight = 0.1f;
            m_globeDisplayRendering.SunContrast = 30;
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            m_bDrawPoint = true;

            //move the light-source according to the mouse coordinate
            double lat, lon, alt;
            lat = lon = alt = 0;

            m_globeViewUtil.WindowToGeographic(m_globeDisplay, m_sceneViewer, arg.X, arg.Y, false, out lon, out lat, out alt);

            m_globeDisplayRendering.SetSunPosition(lat, lon);

            //Refresh the display so that the AfterDraw will get called
            m_sceneViewer.Redraw(false);
        }

        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            m_bDrawPoint = false;
        }

        protected override bool OnDeactivate()
        {
            //disable the light source
            m_globeDisplayRendering.IsSunEnabled = false;
            return base.OnDeactivate();
        }

        #endregion

    }

}
