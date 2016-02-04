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
using System.Collections;
using System.Data;
using System.Diagnostics;

using System.Drawing;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;

namespace MultivariateRenderers
{
    [Guid("FE338F20-B39C-49B8-9F28-FAC2F0DE0C0D")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [ProgId("MultiVariateRenderers.MultivariateRendPropPageCS")]
	public class MultivariateRendPropPageCS : IComPropertyPage, IComEmbeddedPropertyPage, IRendererPropertyPage
	{
		// custom renderer property page class for MultivariateRenderer

		// a renderer property page must implement these interfaces:

		[DllImport("gdi32.dll")]
		private extern static bool DeleteObject(IntPtr hObject);

		private PropPageForm m_Page;
		private IFeatureRenderer m_pRend;
		private long m_Priority;
		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;


		public MultivariateRendPropPageCS()
		{
			//'MsgBox("New (color prop page)")
			m_Page = new PropPageForm();
			m_Priority = 550; // 5th category is for multiple attribute renderers

			string[] res = typeof(MultivariateRendPropPageCS).Assembly.GetManifestResourceNames();
			if (res.GetLength(0) > 0)
			{
				
                

                try
                {   
                    string bitmapResourceName = GetType().Name + ".bmp";
                    //creating a new bitmap
                    m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                }
                 catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
                }


				if (m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1, 1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}

		}

		~MultivariateRendPropPageCS()
		{
			if (m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}


	#region Component Category Registration
		[ComRegisterFunction()]
		public static void Reg(string regKey)
		{
			RendererPropertyPages.Register(regKey);
		}

		[ComUnregisterFunction()]
		public static void Unreg(string regKey)
		{
			RendererPropertyPages.Unregister(regKey);
		}
	#endregion

		public int Activate()
		{

			return m_Page.Handle.ToInt32();

		}

		public bool Applies(ESRI.ArcGIS.esriSystem.ISet objects)
		{

			object pObj = null;

			if (objects.Count <= 0)
			{
				return false;
				return false;
			}

			objects.Reset();
			pObj = objects.Next();
			while (! (pObj is IFeatureRenderer))
			{
				pObj = objects.Next();
				if (pObj == null)
				{
					return false;
					return false;
				}
			}

			return (pObj is IMultivariateRenderer);

		}

		public void Apply()
		{
			QueryObject(m_pRend);

		}

		public void Cancel()
		{
			// doing nothing discards any changes made on the page since last Apply.  this is
			//   what we want.

		}

		public void Deactivate()
		{
			// Unload m_Page in VB6

		}

		public int Height
		{
			get
			{
				//MsgBox("Height")
				return m_Page.Height;
			}
		}

		public int get_HelpContextID(int controlID)
		{
		return 0;
		}

		public string HelpFile
		{
			get
			{
			return null;
			}
		}

		public void Hide()
		{
			m_Page.Hide();
			

		}

		public bool IsPageDirty
		{
			get
			{
				// check flag on form to see if page is dirty
				// this tells the property sheet whether or not to redraw page
				return m_Page.IsDirty;
			}
		}

		public IComPropertyPageSite PageSite
		{
			set
			{
				m_Page.PageSite = value;
			}
		}

		public int Priority
		{
			get
			{
				return (int)m_Priority;
			}
			set
			{
				m_Priority = value;
			}
		}

		public void SetObjects(ESRI.ArcGIS.esriSystem.ISet objects)
		{
			// supplies the page with the object(s) to be edited including the map, feature layer,
			//   feature class, and renderer
			// note:  the feature renderer passed in as part of Objects is the one created
			//   in CreateCompatibleObject

			object pObj = null;

			if (objects.Count <= 0)
				return;
			objects.Reset();
			pObj = objects.Next();

			IMap pMap = null;
			IGeoFeatureLayer pGeoLayer = null;

			// in this implementation we need info from the map and the renderer
			while (pObj != null)
			{
				if (pObj is IMap)
						pMap = pObj as IMap;
				if (pObj is IGeoFeatureLayer)
						pGeoLayer = pObj as IGeoFeatureLayer;
				if (pObj is IFeatureRenderer)
						m_pRend = pObj as IFeatureRenderer;

				pObj = objects.Next();
			}
			if ((pMap != null) & (pGeoLayer != null) & (m_pRend != null))
			{
				m_Page.InitControls(m_pRend as IMultivariateRenderer, pMap, pGeoLayer);
			}

		}

		public void Show()
		{
			m_Page.Show();

		}

		public string Title
		{
			get
			{
				return m_Page.Name;
			}
			set
			{
				m_Page.Name = value;
			}
		}

		public int Width
		{
			get
			{
				return m_Page.Width;
			}
		}

		public object CreateCompatibleObject(object kind)
		{
			// check to see if the renderer is compatible with the property page...
			//    ...if so, return the renderer.  If not, create a new one.

			IFeatureRenderer pFeatRend = null;

			if ((kind is IMultivariateRenderer) & (kind != null))
				pFeatRend = kind as IFeatureRenderer;
			else
			{
				// create a new MultivariateRenderer 
				pFeatRend = new MultivariateRenderer();
			}

			return pFeatRend;
		}

		public void QueryObject(object theObject)
		{
			// triggered when OK or Apply is pressed on the property page
			IFeatureRenderer pRend = null;

			if ((theObject is IMultivariateRenderer) & (theObject != null))
			{
				pRend = theObject as IFeatureRenderer;
				m_Page.InitRenderer(pRend as IMultivariateRenderer);
			}
		}

		public bool CanEdit(IFeatureRenderer obj)
		{
			return (obj is IMultivariateRenderer);

		}

		ESRI.ArcGIS.esriSystem.UID IRendererPropertyPage.ClassID
		{
			get
			{
				return ClassID1;
			}
		}
		public ESRI.ArcGIS.esriSystem.UID ClassID1
		{
			get
			{
				// return prog id of the property page object
				UID pUID = new UID();
				pUID.Value = "MultivariateRenderers.MultivariateRendPropPageCS";
				return pUID;
			}
		}

		public string Description
		{
			get
			{
				// appears on ArcMap symbology property page
				return "Display features with multivariate symbology";
			}
		}

		public string Name
		{
			get
			{
				return "Multivariate Renderer CS";
			}
		}

		public int PreviewImage
		{
			get
			{
				return m_hBitmap.ToInt32();
			}
		}

		public ESRI.ArcGIS.esriSystem.UID RendererClassID
		{
			get
			{
				UID pUID = new UID();
				pUID.Value = "MultivariateRenderers.MultivariateRendererCS";
				return pUID;
			}
		}

		public string Type
		{
			get
			{
				// text that appears for category in "Show" tree view
				//   on symbology property page

				return "Multiple Attributes";
			}
		}
	}



} //end of root namespace