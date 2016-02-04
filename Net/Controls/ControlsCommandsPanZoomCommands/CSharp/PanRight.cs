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
using ESRI.ArcGIS.SystemUI;
using System.Runtime.InteropServices;

namespace PanZoom
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("C5320014-6485-4d24-9E11-554FDC52CB5E")]

	public class PanRight : ICommand
	{
		#region Component Category Registration
  
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(String sKey)
		{
			string fullKey = sKey.Remove(0, 18) + @"\Implemented Categories";
			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, true);
			if (regKey != null)
			{
				regKey.CreateSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}");
			}
		}
  
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(String sKey)
		{
			string fullKey = sKey.Remove(0, 18) + @"\Implemented Categories";
			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, true);
			if (regKey != null)
			{
				regKey.DeleteSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}");
			}
		}
  #endregion#

		[DllImport("gdi32.dll")]
		static extern bool DeleteObject(IntPtr hObject);

		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;
		private IHookHelper m_pHookHelper;

		public PanRight()
		{
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "PanRight.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~PanRight()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}
	
		#region ICommand Members

		public void OnClick()
		{
			if(m_pHookHelper == null) return;

			//Get the active view
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//Get the extent
			IEnvelope pEnvelope = (IEnvelope) pActiveView.Extent;

			//Create a point to pan to
			IPoint pPoint;
			pPoint = new PointClass();
			pPoint.X = ((pEnvelope.XMin + pEnvelope.XMax) / 2) + (pEnvelope.Height / (100 / GetPanFactor()));
			pPoint.Y = ((pEnvelope.YMin + pEnvelope.YMax) / 2); 

			//Center the envelope on the point
			pEnvelope.CenterAt(pPoint);

			//Set the new extent
			pActiveView.Extent = pEnvelope;

			//Refresh the active view
			pActiveView.Refresh();
		}

		private long GetPanFactor()
		{
			return 50;
		}

		public string Message
		{
			get
			{
				return "Pan display right by the pan factor percentage";
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
				return "Pan Right";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Pan Right";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add PanRight.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Pan Right";
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
				if(m_pHookHelper.FocusMap == null) return false;
				
				return true;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add PanRight.HelpFile getter implementation
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
