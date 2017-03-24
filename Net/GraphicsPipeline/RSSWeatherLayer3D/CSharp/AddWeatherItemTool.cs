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
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS;

namespace RSSWeatherLayer3D
{
	/// <summary>
	/// Summary description for AddWeatherItemTool.
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)]
  [Guid("35EE24A3-9C07-48db-9A37-AA0C24357B24")]
	[ProgId("RSSWeatherLayer3D.AddWeatherItemTool")]
  [ComVisible(true)]
	public sealed class AddWeatherItemTool: BaseTool
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
			GMxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GMxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
		}

		#endregion
		#endregion

    private IGlobeHookHelper          m_globeHookHelper = null;
		private RSSWeatherLayer3DClass	m_weatherLayer		= null;
		private IFeatureClass				      m_featureClass		= null;
		private IScene					          m_scene				    = null;
		private IGlobeDisplay             m_globeDsp        = null;
		private IGlobeViewUtil            m_globeViewUtil   = null;

		public AddWeatherItemTool()
		{
      base.m_category = "Weather3D";
			base.m_caption = "Add Item";
			base.m_message = "Add Weather item";
			base.m_toolTip = "Add Weather item";
			base.m_name = base.m_category + "_" + base.m_caption;

			try
			{
        base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Bitmaps.AddWeatherItemTool.bmp"));
        base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Cursors.AddWeatherItemTool.cur"));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		#region Overriden Class Methods

		/// <summary>
		/// Occurs when this command is created
		/// </summary>
		/// <param name="hook">Instance of the application</param>
		public override void OnCreate(object hook)
        {
            //Instantiate the hook helper
            if (null == m_globeHookHelper)
                m_globeHookHelper = new GlobeHookHelperClass();

            //set the hook
            m_globeHookHelper.Hook = hook;

            //get the relevant members
            IGlobe globe = m_globeHookHelper.Globe;
            m_scene = globe as IScene;
            m_globeDsp = m_globeHookHelper.GlobeDisplay;
            m_globeViewUtil = m_globeHookHelper.Camera as IGlobeViewUtil;

            //connect to the ZipCodes featureclass
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(path, @"ArcGIS\data\USZipCodeData");
            if (!Directory.Exists(path)) throw new Exception(string.Format("Fix code to point to your sample data: {0} was not found", path));

            IWorkspaceFactory wf = new ShapefileWorkspaceFactoryClass() as IWorkspaceFactory;
            IWorkspace ws = wf.OpenFromFile(path, 0);
            IFeatureWorkspace fw = ws as IFeatureWorkspace;
            m_featureClass = fw.OpenFeatureClass("US_ZipCodes");
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
		{
			try
			{
        IScene scene = m_scene;
				
				//get the weather layer
				IEnumLayer layers = scene.get_Layers(null, false);
				layers.Reset();
				ILayer layer = layers.Next();
				while(layer != null)
				{
					if(layer is RSSWeatherLayer3DClass)
					{
						m_weatherLayer = (RSSWeatherLayer3DClass)layer;
						break;
					}
					layer = layers.Next();
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
		}

    /// <summary>
    /// Occurs when the user click inside the globe
    /// </summary>
    /// <param name="Button"></param>
    /// <param name="Shift"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
		public override void OnMouseDown(int Button, int Shift, int X, int Y)
		{
			//make sure that the weatherlayer and the featureclass exists
      if(null == m_weatherLayer || null == m_featureClass)
				return;

			//get the current point (convert the mouse coordinate into geographics coordinate)
			double lat, lon, alt;
			m_globeViewUtil.WindowToGeographic(m_globeDsp, m_globeDsp.ActiveViewer, X, Y, true, out lon, out lat, out alt);
			IPoint point = new PointClass();
			((IZAware)point).ZAware = true;
			point.PutCoords(lon, lat);
			
      //Set a default elevation of 1000M. Alternatively, the user can get the 
      //elevation from the globesurface (ISurface)
      point.Z = 1000.0;

			//create the spatial filter in order to select the relevant zipCode
			ISpatialFilter spatialFilter = new SpatialFilterClass();
			spatialFilter.Geometry = point as IGeometry;
			spatialFilter.GeometryField = m_featureClass.ShapeFieldName;
			
      //The spatial filter supposed to filter all the polygons containing the point
			spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;

			//query the featureclass for the containing features
			IFeatureCursor featureCursor = m_featureClass.Search(spatialFilter, true);
			IFeature feature = featureCursor.NextFeature();
			if(null == feature)
				return;
			
			//get the zip code from the containing feature
			long zipCode = Convert.ToInt64(feature.get_Value(feature.Fields.FindField("ZIP")));
			
			//add the weather item to the layer
			m_weatherLayer.AddItem(zipCode, lat, lon);

      //release the featurecursor
			Marshal.ReleaseComObject(featureCursor);
		}
  
    /// <summary>
    /// Occurs when the user move the mouse
    /// </summary>
    /// <param name="Button"></param>
    /// <param name="Shift"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			// TODO:  Add AddWeatherItemTool.OnMouseMove implementation
		}
  
    /// <summary>
    /// Occurs when then user release the mouse button
    /// </summary>
    /// <param name="Button"></param>
    /// <param name="Shift"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			// TODO:  Add AddWeatherItemTool.OnMouseUp implementation
		}
		#endregion
	}
}
