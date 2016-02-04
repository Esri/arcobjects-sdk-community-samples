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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace sceneTools
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("485BE349-31DA-4cd5-B6A4-69E4758F2541")]

	public sealed class Fly : BaseTool 
	{
		[DllImport("user32")] public static extern int SetCursor(int hCursor);  
		[DllImport("user32")] public static extern int GetClientRect(int hwnd, ref  Rectangle lpRect);

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

		private ISceneHookHelper m_pSceneHookHelper;
		private bool m_bInUse;
		bool bCancel = false;
		private long m_lMouseX;
		private long m_lMouseY;
		private double m_dMotion; //speed of the scene fly through in scene units
		private IPoint m_pPointObs; //observer
		private IPoint m_pPointTgt; //target
		private double m_dDistance; //distance between target and observer
		private double m_dElevation; //normal fly angles in radians
		private double m_dAzimut;  //normal fly angles in radians
		private int m_iSpeed; 
		private System.Windows.Forms.Cursor m_flyCur;
		private System.Windows.Forms.Cursor m_moveFlyCur;
		
		public Fly()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Fly";
			base.m_toolTip = "Fly";
			base.m_name = "Sample_SceneControl(C#)/Fly";
			base.m_message = "Flies through the scene";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.fly.bmp"));
			}
			m_flyCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.fly.cur"));
			m_moveFlyCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.fly1.cur"));
			m_pSceneHookHelper = new SceneHookHelperClass ();
			m_iSpeed = 0;
		}

       	public override void OnCreate(object hook)
		{
			m_pSceneHookHelper.Hook = hook;
		}
	
		public override bool Enabled
		{
			get
			{
				//Disable if orthographic (2D) view
				if (m_pSceneHookHelper.Hook == null || m_pSceneHookHelper.Scene == null)
				{
					return false;
				}
				else
				{
					ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;
					if(pCamera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
						return false;
					else
						return true;
				}	
			}
		}
	
		public override int Cursor
		{
			get
			{
				if(m_bInUse)
					return m_moveFlyCur.Handle.ToInt32();
				else
					return m_flyCur.Handle.ToInt32();
			}
		}
			
		public override bool Deactivate()
		{
			return true;
		}
		
		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			if (! m_bInUse)
			{
				m_lMouseX = X;
				m_lMouseY = Y;

				if(m_iSpeed == 0)
					StartFlight();
			}
			else
			{
				//Set the speed
				if (Button == 1)
					m_iSpeed = m_iSpeed + 1;
				else if (Button == 2)
					m_iSpeed = m_iSpeed - 1;

				//Start or end the flight
				if (m_iSpeed == 0)
					EndFlight();
				else
					StartFlight();
			}
		}
	
		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			if (! m_bInUse) return;

			m_lMouseX = X;
			m_lMouseY = Y;
		}

		public override void OnKeyUp(int keyCode, int Shift)
		{
			if(m_bInUse == true)
			{
				//Slow down the speed of the fly through
				if(keyCode == 40 || keyCode == 37)
					m_dMotion = m_dMotion / 2;
				//Speed up the speed of the fly through
				else if (keyCode == 38 || keyCode == 39)
					m_dMotion = m_dMotion * 2;
				else if (keyCode == 27)
					bCancel = true;
			}
		}
				
		public void StartFlight()
		{
			m_bInUse = true;
			
			//Get the extent of the scene graph
			IEnvelope pEnvelope;
			pEnvelope = m_pSceneHookHelper.SceneGraph.Extent;

			if (pEnvelope.IsEmpty) return;

			//Query the coordinates of the extent
			double dXmin, dXmax, dYmin, dYmax;
			pEnvelope.QueryCoords(out dXmin, out dYmin, out dXmax, out dYmax);

			//Set the speed of the scene
			if((dXmax - dXmin) > (dYmax - dYmin))
				m_dMotion = (dXmax - dXmin)/100;
			else
				m_dMotion = (dYmax - dYmin) / 100;

			//Get camera's current observer and target
			ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;
			m_pPointObs = pCamera.Observer;
			m_pPointTgt = pCamera.Target;
			
			//Get the differences between the observer and target
			double dx, dy, dz;

			dx = m_pPointTgt.X - m_pPointObs.X;
			dy = m_pPointTgt.Y - m_pPointObs.Y;
			dz = m_pPointTgt.Z - m_pPointObs.Z;

			//Determine the elevation and azimuth in radians and
			//the distance between the target and observer
			m_dElevation = Math.Atan(dz/ Math.Sqrt(dx*dx + dy*dy));
			m_dAzimut = Math.Atan(dy / dx);
			m_dDistance = Math.Sqrt((dx*dx) + (dy*dy) + (dz*dz));
			
			//Windows API call to set cursor
			SetCursor(m_moveFlyCur.Handle.ToInt32());
	
			//Continue the flight
			Flight();
		}

		public void Flight()
		{
			//Get IMessageDispatcher interface
			IMessageDispatcher pMessageDispatcher;
			pMessageDispatcher = new MessageDispatcherClass();

			//Set the ESC key to be seen as a cancel action
			pMessageDispatcher.CancelOnClick = false;
			pMessageDispatcher.CancelOnEscPress = true;

			//Get the scene graph
			ISceneGraph pSceneGraph = (ISceneGraph) m_pSceneHookHelper.SceneGraph;

			//Get the scene viewer
			ISceneViewer pSceneViewer = (ISceneViewer) m_pSceneHookHelper.ActiveViewer;

			//Get the camera
			ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;

			bCancel = false;

			do
			{
				//Get the elapsed time
				double dlastFrameDuration, dMeanFrameRate;

				pSceneGraph.GetDrawingTimeInfo(out dlastFrameDuration, out dMeanFrameRate);

				if(dlastFrameDuration < 0.01)
					dlastFrameDuration = 0.01;

				if(dlastFrameDuration > 1)
					dlastFrameDuration = 1;

				//Windows API call to get windows client coordinates
				Rectangle rect = new Rectangle();
				if (GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, ref rect) == 0) return;
				
				//Get normal vectors
				double dXMouseNormal, dYMouseNormal;

				dXMouseNormal = 2 * ((double)m_lMouseX / (double)rect.Right) - 1;
				dYMouseNormal = 2 * ((double)m_lMouseY / (double)rect.Bottom) - 1;
				
				//Set elevation and azimuth in radians for normal rotation
				m_dElevation = m_dElevation - (dlastFrameDuration * dYMouseNormal * Math.Abs(dYMouseNormal));
				m_dAzimut = m_dAzimut - (dlastFrameDuration * dXMouseNormal * Math.Abs(dXMouseNormal));
				if(m_dElevation > 0.45 * 3.141592)
					m_dElevation = 0.45 * 3.141592;

				if(m_dElevation < -0.45 * 3.141592)
					m_dElevation = -0.45 * 3.141592;

				if(m_dAzimut < 0)
					m_dAzimut = m_dAzimut + (2 * 3.141592);

				if(m_dAzimut > 2 * 3.141592)
					m_dAzimut = m_dAzimut - (2 * 3.141592);
				
				double dx, dy, dz;

				dx = Math.Cos(m_dElevation) * Math.Cos(m_dAzimut);
				dy = Math.Cos(m_dElevation) * Math.Sin(m_dAzimut);
				dz = Math.Sin(m_dElevation);

				//Change the viewing directions (target)
				m_pPointTgt.X = m_pPointObs.X + (m_dDistance * dx);
				m_pPointTgt.Y = m_pPointObs.Y + (m_dDistance * dy);
				m_pPointTgt.Z = m_pPointObs.Z + (m_dDistance * dz);

				//Move the camera in the viewing directions
				m_pPointObs.X = m_pPointObs.X + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dx);
				m_pPointObs.Y = m_pPointObs.Y + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dy);
				m_pPointTgt.X = m_pPointTgt.X + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dx);
				m_pPointTgt.Y = m_pPointTgt.Y + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dy);
				m_pPointObs.Z = m_pPointObs.Z + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dz);
				m_pPointTgt.Z = m_pPointTgt.Z + (dlastFrameDuration * (2 ^ m_iSpeed) * m_dMotion * dz);

				pCamera.Observer = m_pPointObs;
				pCamera.Target = m_pPointTgt;
				
				//Set the angle of the camera about the line of sight between the observer and target
				pCamera.RollAngle = 10 * dXMouseNormal * Math.Abs(dXMouseNormal);

				//Redraw the scene viewer 
				pSceneViewer.Redraw(true);

				object objCancel;

				//Dispatch any waiting messages: OnMouseMove / OnMouseUp / OnKeyUp events
				//object objCancel = bCancel as object;
				pMessageDispatcher.Dispatch(m_pSceneHookHelper.ActiveViewer.hWnd, false, out objCancel);
				
				//End flight if ESC key pressed
				if (bCancel == true)
					EndFlight();
			}
			while(m_bInUse == true && bCancel == false);

			bCancel = false;
		}

		public void EndFlight()
		{
			m_bInUse = false;

			//Get the scene graph
			ISceneGraph pSceneGraph = (ISceneGraph) m_pSceneHookHelper.SceneGraph;

			IPoint pPointTgt;
			pPointTgt = new PointClass();
			object pOwner, pObject;
			Rectangle rect = new Rectangle();

			//Windows API call to get windows client coordinates
			if(GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, ref rect) != 0)
			{
				//Translate coordinates into a 3D point
				pSceneGraph.Locate(pSceneGraph.ActiveViewer, rect.Right / 2, rect.Bottom / 2, esriScenePickMode.esriScenePickAll, true, out pPointTgt, out pOwner, out pObject);
			}

			//Get the camera
			ICamera pCamera = (ICamera) m_pSceneHookHelper.Camera;

			if(pPointTgt != null)
			{
				//Reposition target and observer
				pCamera.Target = pPointTgt;
				pCamera.Observer = m_pPointObs;
			}

			//Set the angle of the camera about the line
			//of sight between the observer and target
			pCamera.RollAngle = 0;
			pCamera.PropertiesChanged();

			//Windows API call to set cursor
			SetCursor(m_moveFlyCur.Handle.ToInt32());
			m_iSpeed = 0;
		}
	
		public override void OnKeyDown(int keyCode, int Shift)
		{
			if(keyCode == 27) //ESC is pressed
			{
				bCancel = true;
			}
		}
	}
}
