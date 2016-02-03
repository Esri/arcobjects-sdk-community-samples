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
	[Guid("6BA62F0E-5337-47fe-8B45-E5359B0D85D8")]

	public sealed class ZoomIn : BaseTool
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
		
		public ZoomIn()
		{
			base.m_category = "Sample_SceneControl(C#)";
			base.m_caption = "Zoom In";
			base.m_toolTip = "Zoom In";
			base.m_name = "Sample_SceneControl(C#)/Zoom In";
			base.m_message = "Zooms in on the scene";

			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.ZoomIn.bmp"));
			}
			m_pCursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("sceneTools.ZoomIn.cur"));
		
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
		}

		public override void OnMouseMove(int Button, int Shift, int X, int Y)
		{
			if(!m_bInUse) return;

			IEnvelope pEnvelope;
			
			//Draw rectangle on the device
			CreateEnvelope(X, Y, out pEnvelope);
			DrawRectangle(pEnvelope);
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
                    pCamera.Zoom(0.75);
                }
                else
                {
                    //if perspective (3D) view
                    if (pCamera.ProjectionType == esri3DProjectionType.esriPerspectiveProjection)
                    {
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

                        //Get dimension of the scene viewer window
                        Rectangle rect;
                        rect = new Rectangle();

                        if (GetWindowRect(m_pSceneHookHelper.ActiveViewer.hWnd, ref rect) == 0) return;

                        double dx, dy;
                        dx = pEnvelope.Width;
                        dy = pEnvelope.Height;

                        //Determine zoom factor
                        if (dx > 0 && dy > 0)
                        {
                            dx = dx / Math.Abs(rect.Right - rect.Left);
                            dy = dy / Math.Abs(rect.Top - rect.Bottom);
                            if (dx > dy)
                                pCamera.Zoom(dx);
                            else
                                pCamera.Zoom(dy);
                        }
                        else
                            pCamera.Zoom(0.75);

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
