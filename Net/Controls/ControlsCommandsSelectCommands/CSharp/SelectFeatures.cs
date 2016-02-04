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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;

namespace SelectCommands
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("3362ba66-bf63-442a-a639-d18d583a028d")]
	public sealed class SelectFeatures : BaseTool
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
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion


        private IHookHelper m_hookHelper;
        private System.Windows.Forms.Cursor m_CursorMove;
																	  
		public SelectFeatures()
		{
			//Create an IHookHelper object
            m_hookHelper = new HookHelperClass();

			//Set the tool properties
			base.m_caption = "Select Features";
			base.m_category = "Sample_Select(C#)";
			base.m_name = "Sample_Select(C#)_Select Features";
			base.m_message = "Selects Features By Rectangle Or Single Click";
			base.m_toolTip = "Selects Features";
			base.m_deactivate = true;
			base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "SelectFeatures.bmp"));
            m_CursorMove = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "SelectFeatures.cur"));
		}

		public override void OnCreate(object hook)
		{
            m_hookHelper.Hook = hook;
		}
	
		public override bool Enabled
		{
			get
			{
                if (m_hookHelper.FocusMap == null) return false;
                return (m_hookHelper.FocusMap.LayerCount > 0);
			}
		}
	
		public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IMap map;
            IPoint clickedPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            //If ActiveView is a PageLayout 
            if (m_hookHelper.ActiveView is IPageLayout)
            {
                //See whether the mouse has been clicked over a Map in the PageLayout 
                map = m_hookHelper.ActiveView.HitTestMap(clickedPoint);

                //If mouse click isn't over a Map exit 
                if (map == null)
                    return; 


                //Ensure the Map is the FocusMap 
                if ((!object.ReferenceEquals(map, m_hookHelper.FocusMap)))
                {
                    m_hookHelper.ActiveView.FocusMap = map;
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }

                //Still need to convert the clickedPoint into map units using the map's IActiveView 
                clickedPoint = ((IActiveView)map).ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            }

            else //Or ActiveView is a Map 
            {
                map = m_hookHelper.FocusMap;
            }

            IActiveView activeView = (IActiveView)map;
            IRubberBand rubberEnv = new RubberEnvelopeClass();
            IGeometry geom = rubberEnv.TrackNew(activeView.ScreenDisplay, null);
            IArea area = (IArea)geom;

            //Extra logic to cater for the situation where the user simply clicks a point on the map 
            //or where envelope is so small area is zero 
            if ((geom.IsEmpty == true) || (area.Area == 0))
            {

                //create a new envelope 
                IEnvelope tempEnv = new EnvelopeClass();

                //create a small rectangle 
                ESRI.ArcGIS.esriSystem.tagRECT RECT = new tagRECT();
                RECT.bottom = 0;
                RECT.left = 0;
                RECT.right = 5;
                RECT.top = 5;

                //transform rectangle into map units and apply to the tempEnv envelope 
                IDisplayTransformation dispTrans = activeView.ScreenDisplay.DisplayTransformation;
                dispTrans.TransformRect(tempEnv,ref RECT, 4); //4 = esriDisplayTransformationEnum.esriTransformToMap)
                tempEnv.CenterAt(clickedPoint);
                geom = (IGeometry)tempEnv;
            }

            //Set the spatial reference of the search geometry to that of the Map 
            ISpatialReference spatialReference = map.SpatialReference;
            geom.SpatialReference = spatialReference;

            map.SelectByShape(geom, null, false);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);
        } 
	
		public override int Cursor
		{
			get
			{
				return m_CursorMove.Handle.ToInt32();
				
			}
		}
	}
}
