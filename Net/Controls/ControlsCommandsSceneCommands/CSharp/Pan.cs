/*

   Copyright 2019 Esri

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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace sceneTools
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("15E39132-A7CA-4e03-87D9-726D6548AC47")]

	public sealed class Pan :BaseTool
	{
		[DllImport("user32")] public static extern int SetCapture(int hwnd); 
		[DllImport("user32")] public static extern int GetCapture(int fuFlags);
		[DllImport("user32")] public static extern int ReleaseCapture(int hwnd);

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

		private System.Windows.Forms.Cursor m_pCursorStop;
		private System.Windows.Forms.Cursor m_pCursorMove;
		private ISceneHookHelper m_pSceneHookHelper;
		private long m_lMouseX, m_lMouseY;
		private bool m_bInUse = false;

		public Pan()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Pan";
			base.m_toolTip = "Pan";
			base.m_name = "Sample_SceneControl(C#)/Pan";
			base.m_message = "Pans the scene";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.pan.bmp"));
			}
			m_pCursorStop = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.HAND.CUR"));
			m_pCursorMove = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.movehand.cur"));
		
			m_pSceneHookHelper = new SceneHookHelperClass ();
		}

		public override bool Enabled
		{
			get
			{
				if(m_pSceneHookHelper.Scene == null)
					return false;
				else
					return true;
			}
		}
	
		public override void OnCreate(object hook)
		{
			m_pSceneHookHelper.Hook = hook;
		}
	
		public override bool Deactivate()
		{
			return true;
		}
	
		public override int Cursor
		{
			get
			{
				if(m_bInUse)
					return m_pCursorMove.Handle.ToInt32();
				else
					return m_pCursorStop.Handle.ToInt32();
			}
		}
	
		public override void OnMouseDown(int Button, int Shift, int X, int Y)
		{
			//Initialize mouse coordinates
			m_bInUse = true;
			m_lMouseX = X;
			m_lMouseY = Y;

			SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd);
		}
	
		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			if(!m_bInUse) return;

			if(X - m_lMouseX == 0 && Y - m_lMouseY == 0) return;

			//Create point with previous mouse coordinates
			IPoint pStartPoint;
			pStartPoint = new PointClass();
			pStartPoint.PutCoords((double) m_lMouseX, (double) m_lMouseY);
  
			//Create point with a new mouse coordinates
			IPoint pEndPoint;
			pEndPoint = new PointClass();
			pEndPoint.PutCoords((double) X, (double) Y);

			//Set mouse coordinates for the next OnMouseMove event
			m_lMouseX = X;
			m_lMouseY = Y;

			//Get scene viewer's camera
			ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;

			//Pan the camera
			pCamera.Pan(pStartPoint, pEndPoint);

			//Redraw the scene viewer
			ISceneViewer pSceneViewer = m_pSceneHookHelper.ActiveViewer;
			pSceneViewer.Redraw(false);
		}
	
		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			if(!m_bInUse) return;
			
			if(GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) != 0)
				ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd);

			m_bInUse = false;
		}
	}
}
