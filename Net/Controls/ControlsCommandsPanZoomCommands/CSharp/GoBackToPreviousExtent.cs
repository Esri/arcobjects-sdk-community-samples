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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace PanZoom
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("41631605-6B69-4778-8907-B161B24D98AF")]

	public class GoBackToPreviousExtent : ICommand
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

		private bool m_enabled;
		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;
		private IHookHelper m_pHookHelper;

		public GoBackToPreviousExtent()
		{
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "UnDoDraw.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~GoBackToPreviousExtent()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}
	
		#region ICommand Members

		public void OnClick()
		{
			//Get the active view
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//Get the extent stack
			IExtentStack pMapExtent = (IExtentStack) pActiveView.ExtentStack;

			//Undo the extent view	
			if (pMapExtent.CanUndo())
			{
				pMapExtent.Undo();
			}
		}

		public string Message
		{
			get
			{
				return "Go to Previous Screen location";
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
		}

		public string Caption
		{
			get
			{
				return "Undo Extent";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Undo Previous Extent";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add GoBackToPreviousExtent.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Undo Extent";
			}
		}

		public bool Checked
		{
			get
			{
				return false;
			}
		}

		public bool Enabled
		{
			get
			{
				if (m_pHookHelper.FocusMap != null)
				{
					//Get the active view
					IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

					//Get the extent stack
					IExtentStack pMapStack = (IExtentStack) pActiveView.ExtentStack;
					
					//Can the extent be undone

					return m_enabled = pMapStack.CanUndo();
				}
				return false;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add GoBackToPreviousExtent.HelpFile getter implementation
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
	}
}
