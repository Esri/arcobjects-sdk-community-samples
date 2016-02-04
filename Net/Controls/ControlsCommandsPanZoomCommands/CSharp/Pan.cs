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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace PanZoom
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("4BD2DDAE-AA6F-4718-AC2E-F39C5618895C")]

	public class Pan :  ICommand, ITool
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

		[DllImport("gdi32.dll")]
		static extern bool DeleteObject(IntPtr hObject);

		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;
		private IHookHelper m_pHookHelper;
		private bool m_enabled;
		private IScreenDisplay m_focusScreenDisplay;
		private bool m_PanOperation;
		private bool m_check;
		private System.Windows.Forms.Cursor m_cursor;
		private System.Windows.Forms.Cursor m_cursorMove;
				
		public Pan()
		{
			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "Pan.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~Pan()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}
	   
		#region ICommand Members

		public void OnClick()
		{
		}

		public string Message
		{
			get
			{
				return "Pans the Display by Grabbing";
			}
		}

		public int Bitmap
		{
			get
			{
				return m_hBitmap.ToInt32();
			}
		}

		public void OnCreate(object hook)
		{
			m_pHookHelper.Hook = hook;
            m_enabled = true;
			m_check = false;
			m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream (GetType(), "Hand.cur"));
			m_cursorMove = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "MoveHand.cur"));
		}

		public string Caption
		{
			get
			{
				return "Pan";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Pan by Grab";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add Pan.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Pan";
			}
		}

		public bool Checked
		{
			get
			{
				return m_check;
			}
		}

		public bool Enabled
		{
			get
			{
				return m_enabled;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add Pan.HelpFile getter implementation
				return null;
			}
		}

		public string Category
		{
			get
			{
				return "Sample_Pan/Zoom";
			}
		}

		#endregion
	
		#region ITool Members

		public void OnMouseDown(int button, int shift, int x, int y)
		{
			if (button != 1) return;

			IActiveView activeView = m_pHookHelper.ActiveView;
			IPoint point;

			//If in PageLayout view, find the closest map
			if(!(activeView is IMap))
			{
				point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
				IMap hitMap = activeView.HitTestMap(point);

				//Exit if no map found
				if(hitMap == null)
					return;
	
				if(activeView != m_pHookHelper.FocusMap)
					activeView.FocusMap = hitMap;
			}

			//Start the pan operation
			m_focusScreenDisplay = getFocusDisplay();
			point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
			m_focusScreenDisplay.PanStart(point);
			m_PanOperation = true;
		}

        private IScreenDisplay getFocusDisplay()
        {
            //Get the ScreenDisplay that has focus
            IActiveView activeView = m_pHookHelper.ActiveView;

            activeView = m_pHookHelper.ActiveView.FocusMap as IActiveView; //layout view
            return activeView.ScreenDisplay;

        }

		public void OnMouseMove(int button, int shift, int x, int y)
		{
			if(button != 1) return;

			if(! m_PanOperation) return;

			IPoint point = m_focusScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
			m_focusScreenDisplay.PanMoveTo(point);
		}
		
		public void OnMouseUp(int button, int shift, int x, int y)
		{
			if(button != 1) return;

			if(! m_PanOperation) return;

			IEnvelope extent = m_focusScreenDisplay.PanStop();

			//Check if user dragged a rectangle or just clicked.
			//If a rectangle was dragged, m_ipFeedback in non-NULL
			if(extent != null)
			{
				m_focusScreenDisplay.DisplayTransformation.VisibleBounds = extent;
				m_focusScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
			}

			m_PanOperation = false;
		}

		public void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  Add Pan.OnKeyDown implementation
		}

		public void OnKeyUp(int keyCode, int shift)
		{
			// TODO:  Add Pan.OnKeyUp implementation
		}

		public int Cursor
		{
			get
			{
				if(m_PanOperation)
                    return m_cursorMove.Handle.ToInt32();
				else
					return m_cursor.Handle.ToInt32();
			}
		}

		public bool OnContextMenu(int x, int y)
		{
			// TODO:  Add Pan.OnContextMenu implementation
			return false;
		}

		public bool Deactivate()
		{
			// TODO:  Add Pan.Deactivate implementation
			return true;
		}

		public void Refresh(int hdc)
		{
			// TODO:  Add Pan.Refresh implementation
		}

		public void OnDblClick()
		{
			// TODO:  Add Pan.OnDblClick implementation
		}

		#endregion
	}
}
