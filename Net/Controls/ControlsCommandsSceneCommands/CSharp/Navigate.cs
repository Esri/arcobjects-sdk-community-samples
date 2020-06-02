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
	[Guid("9A3B648E-4543-438c-84C1-41CC6AD29A81")]

	public sealed class Navigate : BaseTool
	{
		[DllImport("user32")] public static extern int SetCapture(int hwnd); 
		[DllImport("user32")] public static extern int SetCursor(int hCursor);
		[DllImport("user32")] public static extern int GetClientRect(int hwnd, ref  Rectangle lpRect); 
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

		private ISceneHookHelper m_pSceneHookHelper;
		private bool m_bInUse;
		private bool m_bGesture;
		private bool bCancel = false;
		private long m_lMouseX, m_lMouseY;
		private bool m_bSpinning;
		private double m_dSpinStep;
		private System.Windows.Forms.Cursor m_pCursorNav;
		private System.Windows.Forms.Cursor m_pCursorPan;
		private System.Windows.Forms.Cursor m_pCursorZoom;
		private System.Windows.Forms.Cursor m_pCursorGest;

		public Navigate()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Navigate";
			base.m_toolTip = "Navigate";
			base.m_name = "Sample_SceneControl(C#)/Navigate";
			base.m_message = "Navigates the scene";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.Navigation.bmp"));
			}
			m_pCursorNav = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.navigation.cur"));
			m_pCursorPan = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.movehand.cur"));
			m_pCursorZoom = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.ZOOMINOUT.CUR"));
			m_pCursorGest = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.gesture.cur"));

			m_pSceneHookHelper = new SceneHookHelperClass ();
		}

		public override void OnCreate(object hook)
		{
			m_pSceneHookHelper.Hook = hook;

			if(m_pSceneHookHelper != null)
			{
				m_bGesture = m_pSceneHookHelper.ActiveViewer.GestureEnabled;
				m_bSpinning = false;
			}
		}
	
		public override bool Enabled
		{
			get
			{
				//Disable if orthographic (2D) view
				if(m_pSceneHookHelper.Hook == null || m_pSceneHookHelper.Scene == null)
					return false;
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
				if(m_bGesture == true)
					return m_pCursorGest.Handle.ToInt32();
				else
					return m_pCursorNav.Handle.ToInt32();
			}
		}
	
		public override bool Deactivate()
		{
			return true;
		}
	
		public override void OnMouseDown(int Button, int Shift, int X, int Y)
		{
			if (Button == 3)
				bCancel = true;
			else
			{
				m_bInUse = true;
			
				SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd);

				m_lMouseX = X;
				m_lMouseY = Y;
			}
		}
	
		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			if(m_bInUse == false) return;

			if(X - m_lMouseX == 0 && Y - m_lMouseY == 0) return;

			long dx, dy;

			dx = X - m_lMouseX;
			dy = Y - m_lMouseY;

			//If right mouse clicked
			if (Button == 2)
			{
				//Set the zoom cursor
				SetCursor(m_pCursorZoom.Handle.ToInt32());

				if (dy < 0)
					m_pSceneHookHelper.Camera.Zoom(1.1);
				else if (dy > 0)
					m_pSceneHookHelper.Camera.Zoom(0.9);
			}
			//If two mouse buttons clicked
			if(Button == 3)
			{
				//Set the pan cursor
				SetCursor(m_pCursorPan.Handle.ToInt32());

				//Create a point with previous mouse coordinates
				IPoint pStartPoint;
				pStartPoint = new PointClass();
				pStartPoint.PutCoords((double) m_lMouseX, (double) m_lMouseY); 

				//Create point with a new mouse coordinates
				IPoint pEndPoint;
				pEndPoint = new PointClass();
				pEndPoint.PutCoords((double) X, (double) Y);

				//Pan camera
				m_pSceneHookHelper.Camera.Pan(pStartPoint, pEndPoint);
			}

			//If left mouse clicked
			if(Button == 1)
			{
				//If scene viewer gesturing is disabled move the camera observer
				if(m_bGesture == false)
					m_pSceneHookHelper.Camera.PolarUpdate(1, dx, dy, true); 
				else
				{
					//if camera already spinning
					if(m_bSpinning == true)
					{
						//Move the camera observer
						m_pSceneHookHelper.Camera.PolarUpdate(1, dx, dy, true); 
					}
					else
					{
						//Windows API call to get windows client coordinates
						Rectangle rect;
						rect = new Rectangle();

						GetClientRect(m_pSceneHookHelper.ActiveViewer.hWnd, ref rect);

						//Recalculate the spin step
						if(dx < 0)
							m_dSpinStep = (180.0 / rect.Right - rect.Left ) * (dx - m_pSceneHookHelper.ActiveViewer.GestureSensitivity);
						else
							m_dSpinStep = (180.0 / rect.Right - rect.Left) * (dx + m_pSceneHookHelper.ActiveViewer.GestureSensitivity);

						//Start spinning
						StartSpin();
					}
				}
			}

			//Set Mouse coordinates for the next
			//OnMouse Event
			m_lMouseX = X;
			m_lMouseY = Y;

			//Redraw the scene viewer
			m_pSceneHookHelper.ActiveViewer.Redraw(true);
		}
	
		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			//Set the navigation cursor
			if(m_bGesture == true)
				SetCursor(m_pCursorGest.Handle.ToInt32());
			else
				SetCursor(m_pCursorNav.Handle.ToInt32());
			
			if(GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) != 0)
				ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd);
		}

		public void StartSpin()
		{
			m_bSpinning = true;

			//Get IMessageDispatcher interface
			IMessageDispatcher pMessageDispatcher;
			pMessageDispatcher = new MessageDispatcherClass();

			//Set the ESC key to be seen as a cancel action
			pMessageDispatcher.CancelOnClick = false;
			pMessageDispatcher.CancelOnEscPress = true;

			do
			{
				//Move the camera observer
				m_pSceneHookHelper.Camera.PolarUpdate(1, m_dSpinStep, 0, true);

				//Redraw the scene viewer
				m_pSceneHookHelper.ActiveViewer.Redraw(true);

				//Dispatch any waiting messages: OnMouseMove/ OnMouseDown/ OnKeyUp/ OnKeyDown events
				object b_oCancel;

				pMessageDispatcher.Dispatch(m_pSceneHookHelper.ActiveViewer.hWnd, false, out b_oCancel);

				if(bCancel == true)
					m_bSpinning = false;
			}
			while(bCancel == false);

			bCancel = false;

		}
	
		public override void OnKeyDown(int keyCode, int Shift)
		{
			if (keyCode == 27)
			{
				bCancel = true;
				SetCursor(m_pCursorNav.Handle.ToInt32());
			}

			switch(Shift)
			{
				case 1: //Shift key
					//Set pan cursor
					SetCursor(m_pCursorPan.Handle.ToInt32());
					break;

				case 2: //Control key
					//Set ZoomIn/Out cursor
					SetCursor(m_pCursorZoom.Handle.ToInt32());
					break;

				case 3: //shift + control keys
					//Set scene viewer gesture enabled property
					if(m_bSpinning == false)
					{
						if(m_bGesture == true)
						{
							m_pSceneHookHelper.ActiveViewer.GestureEnabled = false;
							m_bGesture = false;
							SetCursor(m_pCursorNav.Handle.ToInt32());
						}
						else
						{
							m_pSceneHookHelper.ActiveViewer.GestureEnabled = true;
							m_bGesture = true;
							SetCursor(m_pCursorGest.Handle.ToInt32());
						}
					}
					break;

				default:
					return;
			}
		}
	
		public override void OnKeyUp(int keyCode, int Shift)
		{
			if(Shift == 1) //Shift key
			{
				//Pan the camera
				switch(keyCode)
				{
					case 38: //Up key
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveDown, 0.01);
						break;

					case 40: //Down key
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveUp, 0.01);
						break;

					case 37: //Left key
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveRight, 0.01);
						break;

					case 39: // Right key
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveLeft, 0.01);
						break;

					default:
						return;
				}
			}
			else if(Shift == 2) //Control key
			{
				//Move camera in/out or turn camera around the observer
				switch(keyCode)
				{
					case 38:
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveAway, 0.01);
						break;

					case 40:
						m_pSceneHookHelper.Camera.Move(esriCameraMovementType.esriCameraMoveToward, 0.01);
						break;

					case 37:
						m_pSceneHookHelper.Camera.HTurnAround(-1);
						break;

					case 39:
						m_pSceneHookHelper.Camera.HTurnAround(1);
						break;

					default:
						return;
				}
			}
			else
			{
				double d = 5, dAltitude = 2, dAzimuth = 2;

				//Move the camera observer by the given polar
				//increments or increase/decrease the spin speed

				switch(keyCode)
				{
					case 38:
						m_pSceneHookHelper.Camera.PolarUpdate(1, 0, -dAltitude * d, true);
						break;

					case 40:
						m_pSceneHookHelper.Camera.PolarUpdate(1, 0, dAltitude * d, true);
						break;

					case 37:
						m_pSceneHookHelper.Camera.PolarUpdate(1, dAzimuth * d, 0, true);
						break;

					case 39:
						m_pSceneHookHelper.Camera.PolarUpdate(1, -dAzimuth * d, 0, true);
						break;

					case 33: //Page Up
						m_dSpinStep = m_dSpinStep * 1.1;
						break;

					case 34: //Page Down
						m_dSpinStep = m_dSpinStep / 1.1;
						break;

					default:
						return;
				}
			}

			//Set the navigation cursor
			if(m_bGesture == true)
				SetCursor(m_pCursorGest.Handle.ToInt32());
			else
				SetCursor(m_pCursorNav.Handle.ToInt32());

			//Redraw the scene viewer
			m_pSceneHookHelper.ActiveViewer.Redraw(true);
		}
	}
}
