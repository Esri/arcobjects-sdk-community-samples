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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace sceneTools
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("3D3C8C68-7B65-4441-B614-792D5606FF8D")]

	public sealed class ZoomOut : BaseTool
	{
		[DllImport("user32")] public static extern int ReleaseCapture(int hwnd); 
		[DllImport("user32")] public static extern int SetCapture(int hwnd); 
		[DllImport("user32")] public static extern int GetCapture(int fuFlags);
		[DllImport("user32")] public static extern int GetWindowRect(int hwnd, ref Rectangle lpRect);

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

		private System.Windows.Forms.Cursor m_pCursor;
		private ISceneHookHelper m_pSceneHookHelper;
		private long m_lMouseX, m_lMouseY;
		private bool m_bInUse;
        private INewEnvelopeFeedback m_feedBack;
        private IPoint m_point;

		public ZoomOut()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Zoom Out";
			base.m_toolTip = "Zoom Out";
			base.m_name = "Sample_SceneControl(C#)/Zoom Out";
			base.m_message = "Zooms in out the scene";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.zoomout.bmp"));
			}
			m_pCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.ZOOMOUT.CUR"));
		
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

		public override int Cursor
		{
			get
			{
				return m_pCursor.Handle.ToInt32();
			}
		}

		public override bool Deactivate()
		{
			return true;
		}

		public override void OnKeyDown(int keyCode, int Shift)
		{
			if(m_bInUse == true)
			{
				if(keyCode == 27) //If ESC was pressed 
				{
					//Redraw the scene viewer
					ISceneViewer pSceneViewer = (ISceneViewer) m_pSceneHookHelper.ActiveViewer;
					pSceneViewer.Redraw(true);

					ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd);

					m_bInUse = false;
				}
			}
		}

		public override void OnMouseDown(int Button, int Shift, int X, int Y)
		{
			//Initialize mouse coordinates
			m_bInUse = true;
			m_lMouseX = X;
			m_lMouseY = Y;
            
			//Get the scene viewer
			ISceneViewer pSceneViewer = (ISceneViewer) m_pSceneHookHelper.ActiveViewer;

			SetCapture(m_pSceneHookHelper.ActiveViewer.hWnd);

            //Create a point in map coordinates
            IActiveView pActiveView = (IActiveView)m_pSceneHookHelper.Scene;
            m_point = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

		}

		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			if(!m_bInUse) return;

            //Get the focus map
            IActiveView pActiveView = (IActiveView)m_pSceneHookHelper.Scene;

            //Start an envelope feedback
            if (m_feedBack == null)
            {
                m_feedBack = new NewEnvelopeFeedbackClass();
                m_feedBack.Display = pActiveView.ScreenDisplay;
                m_feedBack.Start(m_point);
            }

            //Move the envelope feedback
            m_feedBack.MoveTo(pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));

		}

		public override void OnMouseUp(int Button, int Shift, int X, int Y)
		{
			if(!m_bInUse) return;

            try
            {
                if (GetCapture(m_pSceneHookHelper.ActiveViewer.hWnd) != 0)
                    ReleaseCapture(m_pSceneHookHelper.ActiveViewer.hWnd);

                //Get the scene viewer's camera
                ICamera pCamera = (ICamera)m_pSceneHookHelper.Camera;

                //Get the scene graph
                ISceneGraph pSceneGraph = (ISceneGraph)m_pSceneHookHelper.SceneGraph;

                //Create envelope
                IEnvelope pEnvelope;
                CreateEnvelope(X, Y, out pEnvelope);

                IPoint pPoint;
                object pOwner, pObject;

                if (pEnvelope.Width == 0 || pEnvelope.Height == 0)
                {
                    //Translate screen coordinates into a 3D point
                    pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickAll, true,
                        out pPoint, out pOwner, out pObject);

                    //Set camera target and zoom in
                    pCamera.Target = pPoint;
                    pCamera.Zoom(1.3333333333333);
                }
                else
                {
                    //Get dimension of the scene viewer window
                    Rectangle rect;
                    rect = new Rectangle();

                    if (GetWindowRect(m_pSceneHookHelper.ActiveViewer.hWnd, ref rect) == 0) return;

                    //If perspective (3D) view
                    if (pCamera.ProjectionType == esri3DProjectionType.esriPerspectiveProjection)
                    {
                        double dWidth, dHeight;

                        dWidth = Math.Abs(rect.Right - rect.Left) * (Math.Abs(rect.Right - rect.Left) / pEnvelope.Width);
                        dHeight = Math.Abs(rect.Top - rect.Bottom) * (Math.Abs(rect.Top - rect.Bottom) / pEnvelope.Height);

                        pPoint = new PointClass();
                        pPoint.PutCoords(pEnvelope.XMin + (pEnvelope.Width / 2), pEnvelope.YMin + (pEnvelope.Height / 2));

                        //Redimension envelope based on scene viewer dimensions
                        pEnvelope.XMin = pPoint.X - (dWidth / 2);
                        pEnvelope.YMin = pPoint.Y - (dHeight / 2);
                        pEnvelope.Width = dWidth;
                        pEnvelope.Height = dHeight;

                        //Zoom camera to the envelope
                        pCamera.ZoomToRect(pEnvelope);
                    }
                    else
                    {
                        //Translate screen coordinates into a 3D point
                        pSceneGraph.Locate(pSceneGraph.ActiveViewer, (int)(pEnvelope.XMin + (pEnvelope.Width / 2)),
                            (int)(pEnvelope.YMin + (pEnvelope.Height / 2)), esriScenePickMode.esriScenePickAll, true,
                            out pPoint, out pOwner, out pObject);

                        //Set camera target
                        pCamera.Target = pPoint;

                        double dx, dy;
                        dx = pEnvelope.Width;
                        dy = pEnvelope.Height;

                        //Determine zoom factor
                        if (dx > 0 && dy > 0)
                        {
                            dx = Math.Abs(rect.Right - rect.Left) / dx;
                            dy = Math.Abs(rect.Top - rect.Bottom) / dy;

                            if (dx < dy)
                                pCamera.Zoom(dx);
                            else
                                pCamera.Zoom(dy);
                        }
                        else
                            pCamera.Zoom(1.3333333333333);
                    }
                }
            }
            finally
            {
                //Redraw the scene viewer
                ISceneViewer pSceneViewer = (ISceneViewer)m_pSceneHookHelper.ActiveViewer;
                pSceneViewer.Redraw(true);


                m_bInUse = false;
            }
		}

		public void CreateEnvelope(int X, int Y, out IEnvelope pEnvelope)
		{
			//Create envelope based upon the initial
			//and current mouse coordinates
			pEnvelope = new EnvelopeClass();
			if((double)m_lMouseX <= (double)X)
			{
				pEnvelope.XMin = (double) m_lMouseX;
				pEnvelope.XMax = (double) X;
			}
			else
			{
				pEnvelope.XMin = (double) X;
				pEnvelope.XMax = (double) m_lMouseX;
			}

			if((double) m_lMouseY <= (double) Y)
			{
				pEnvelope.YMin = (double) m_lMouseY;
				pEnvelope.YMax = (double) Y;
			}
			else
			{
				pEnvelope.YMin = (double) Y;
				pEnvelope.YMax = (double) m_lMouseY;
			}
		}

		public void DrawRectangle(IEnvelope pEnvelope)
		{
			//Get the scene viewer
			ISceneViewer pSceneViewer = (ISceneViewer) m_pSceneHookHelper.ActiveViewer;

            using (Graphics myGraphics = Graphics.FromHdc((IntPtr)m_pSceneHookHelper.ActiveViewer.hDC))
            {
                using (Brush brush = new SolidBrush(Color.Transparent))//hollow brush
                {
                    //GDI+ call to fill a rectangle with a hollow brush
                    myGraphics.FillRectangle(brush, (int)pEnvelope.XMin, (int)pEnvelope.YMin,
                        (int)pEnvelope.Width, (int)pEnvelope.Height);
                }

                using (Pen pen = new System.Drawing.Pen(Color.Black, 2)) //A solid, width of 2 black pen
                {
                    //GDI+ call to draw a rectangle with a specified pen 
                    myGraphics.DrawRectangle(pen, (int)pEnvelope.XMin, (int)pEnvelope.YMin,
                        (int)pEnvelope.Width, (int)pEnvelope.Height);
                }
            }

		}
	}
}
