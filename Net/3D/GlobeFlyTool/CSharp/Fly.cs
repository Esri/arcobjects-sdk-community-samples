using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace GlobeFlyTool
{
    public class Fly : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        #region DllImport

        [DllImport("user32")]
        public static extern int SetCursor(int hCursor);
        [DllImport("user32")]
        public static extern int GetClientRect(int hwnd, ref  Rectangle lpRect);
        [DllImport("user32")]
        static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);
        [DllImport("user32")]
        public static extern int GetWindowRect(int hwnd, ref  Rectangle lpRect);

        #endregion

        #region Member Variables

        private IGlobe globe;
        private IGlobeDisplay globeDisplay;
        private IGlobeCamera globeCamera;
        private ICamera camera;
        private IScene scene;
        private bool inUse;
        bool bCancel = false;
        bool orbitalFly = false;
        private long mouseX;
        private long mouseY;
        private double motion = 2; //speed of the scene fly through in scene units
        private double distance; //distance between target and observer
        private double currentElevation; //normal fly angles in radians
        private double currentAzimut;  //normal fly angles in radians
        private int speed;
        private System.Windows.Forms.Cursor flyCur;
        private System.Windows.Forms.Cursor moveFlyCur;
        private Microsoft.VisualBasic.Devices.Clock theClock;
        private long lastClock;
        GlobeFlyTool.PointZ observer;
        GlobeFlyTool.PointZ target;
        GlobeFlyTool.PointZ viewVec;

        #endregion

        #region Constructor/Destructor

        public Fly()
        {
            globe = ArcGlobe.Globe;
            scene = globe as IScene;
            globeDisplay = globe.GlobeDisplay;
            camera = globeDisplay.ActiveViewer.Camera;
            globeCamera = camera as IGlobeCamera;
            theClock = new Microsoft.VisualBasic.Devices.Clock();
            flyCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("GlobeFlyTool.Fly.cur"));
            moveFlyCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("GlobeFlyTool.fly1.cur"));
            speed = 0;
        }

        ~Fly()
        {
            flyCur = null;
            moveFlyCur = null;
        }

        #endregion

        protected override void OnUpdate()
        {
            Enabled = ArcGlobe.Application != null;

            if (inUse)
                Cursor = moveFlyCur;
            else
                Cursor = flyCur;
        }

        #region Tool overrides

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (arg.Button == MouseButtons.Left || arg.Button == MouseButtons.Right)
            {
                if (!inUse)
                {
                    mouseX = arg.X;
                    mouseY = arg.Y;

                    if (speed == 0)
                        StartFlight(arg.X, arg.Y);
                }
                else
                {
                    //Set the speed
                    if (arg.Button == MouseButtons.Left)
                        speed = speed + 1;
                    else if (arg.Button == MouseButtons.Right)
                        speed = speed - 1;
                }
            }
            else
            {
                //EndFlight();
                inUse = false;
                bCancel = true;
            }
        }

        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (!inUse) return;

            mouseX = arg.X;
            mouseY = arg.Y;
        }

        protected override void OnKeyUp(ESRI.ArcGIS.Desktop.AddIns.Tool.KeyEventArgs arg)
        {
            if (inUse == true)
            {
                //Slow down the speed of the fly through
                if (arg.KeyCode == Keys.Down || arg.KeyCode == Keys.Left)
                    motion = motion / 2;
                //Speed up the speed of the fly through
                else if (arg.KeyCode == Keys.Up || arg.KeyCode == Keys.Right)
                    motion = motion * 2;
                else if (arg.KeyCode == Keys.Escape)
                    bCancel = true;

            }
        }

        protected override void OnKeyDown(ESRI.ArcGIS.Desktop.AddIns.Tool.KeyEventArgs arg)
        {
            if (arg.KeyCode == Keys.Escape) //ESC is pressed
            {
                bCancel = true;
            }
        }

        #endregion

        #region Fly Functions

        public void StartFlight(double x, double y)
        {
            inUse = true;

            globeDisplay.IsNavigating = true;
            ESRI.ArcGIS.GlobeCore.esriGlobeCameraOrientationMode camOrientMode =
                globeCamera.OrientationMode;

            orbitalFly = (camOrientMode == ESRI.ArcGIS.GlobeCore.esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal) ? true : false;

            IPoint pObs = camera.Observer;
            IPoint pTar = camera.Target;

            observer = new GlobeFlyTool.PointZ(pObs.X, pObs.Y, pObs.Z);
            target = new GlobeFlyTool.PointZ(pTar.X, pTar.Y, pTar.Z);

            viewVec = target - observer;
            distance = viewVec.Norm();

            //avoid center of globe
            if (target.Norm() < 0.25)
            {
                target = target + viewVec;
                distance = distance * 2;
            }

            currentElevation = Math.Atan(viewVec.z / Math.Sqrt((viewVec.x * viewVec.x) + (viewVec.y + viewVec.y)));
            currentAzimut = Math.Atan2(viewVec.y, viewVec.x);//2.26892;//

            //Windows API call to get windows client coordinates
            System.Drawing.Point pt = new System.Drawing.Point();
            bool ans = GetCursorPos(ref pt);
            Rectangle rect = new Rectangle();
            if (GetWindowRect(globeDisplay.ActiveViewer.hWnd, ref rect) == 0) return;

            mouseX = pt.X - rect.Left;
            mouseY = pt.Y - rect.Top;

            if (!orbitalFly)
            {
                globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationGlobal;
            }
            else
            {
                globeCamera.OrientationMode = esriGlobeCameraOrientationMode.esriGlobeCameraOrientationLocal;
            }
            globeCamera.NavigationType = esriGlobeNavigationType.esriGlobeNavigationFree;
            globeCamera.RollFactor = 1.0;

            globeDisplay.IsNavigating = true;
            globeDisplay.IsNavigating = false;
            globeDisplay.IsNavigating = true;

            lastClock = theClock.TickCount;

            //Windows API call to set cursor
            SetCursor(moveFlyCur.Handle.ToInt32());
            //Continue the flight
            Flight();
        }

        public void Flight()
        {
            //speed in scene units
            double motionUnit = (0.000001 + Math.Abs(observer.Norm() - 1.0) / 200.0) * motion;
            //Get IMessageDispatcher interface
            IMessageDispatcher pMessageDispatcher;
            pMessageDispatcher = new MessageDispatcherClass();

            //Set the ESC key to be seen as a cancel action
            pMessageDispatcher.CancelOnClick = false;
            pMessageDispatcher.CancelOnEscPress = true;
            bCancel = false;
            do
            {
                //Get the elapsed time
                long currentClock = theClock.TickCount;
                double lastFrameDuration = (double)(currentClock - lastClock) / 1000;
                lastClock = currentClock;

                if (lastFrameDuration < 0.01)
                    lastFrameDuration = 0.01;

                if (lastFrameDuration > 1)
                    lastFrameDuration = 0.1;

                System.Diagnostics.Debug.Print(lastFrameDuration.ToString());

                //Windows API call to get windows client coordinates
                Rectangle rect = new Rectangle();
                if (GetClientRect(globeDisplay.ActiveViewer.hWnd, ref rect) == 0) return;

                //Get normal vectors
                double dXMouseNormal, dYMouseNormal;

                dXMouseNormal = 2 * ((double)mouseX / (double)(rect.Right - rect.Left)) - 1;
                dYMouseNormal = 2 * ((double)mouseY / (double)(rect.Bottom - rect.Top)) - 1;

                PointZ dir = this.RotateNormal(lastFrameDuration, dXMouseNormal, dYMouseNormal);

                PointZ visTarget = new PointZ(observer.x + distance * dir.x, observer.y + distance * dir.y, observer.z + distance * dir.z);
                target.x = visTarget.x;
                target.y = visTarget.y;
                target.z = visTarget.z;

                if (speed != 0)
                {
                    int speedFactor = (speed > 0) ? (1 << speed) : -(1 << (-speed));

                    //Move the camera in the viewing directions
                    observer.x = observer.x + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.x);
                    observer.y = observer.y + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.y);
                    observer.z = observer.z + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.z);
                    target.x = target.x + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.x);
                    target.y = target.y + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.y);
                    target.z = target.z + (lastFrameDuration * (2 ^ speedFactor) * motionUnit * dir.z);
                }

                ESRI.ArcGIS.GlobeCore.IGlobeViewUtil globeViewUtil = globeCamera as ESRI.ArcGIS.GlobeCore.IGlobeViewUtil;
                double obsLat;
                double obsLon;
                double obsAlt;
                double tarLat;
                double tarLon;
                double tarAlt;

                globeViewUtil.GeocentricToGeographic(observer.x, observer.y, observer.z, out obsLon, out obsLat, out obsAlt);
                globeViewUtil.GeocentricToGeographic(target.x, target.y, target.z, out tarLon, out tarLat, out tarAlt);
                globeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt / 1000);
                globeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt / 1000);

                globeCamera.SetAccurateViewDirection(target.x - observer.x, target.y - observer.y, target.z - observer.z);

                double rollAngle = 0;
                if (speed > 0)
                {
                    rollAngle = 10 * dXMouseNormal * Math.Abs(dXMouseNormal);
                }
                camera.RollAngle = rollAngle;

                //Redraw the scene viewer 
                globeDisplay.RefreshViewers();

                //Dispatch any waiting messages: OnMouseMove / OnMouseUp / OnKeyUp events
                object objCancel = bCancel as object;
                pMessageDispatcher.Dispatch(globeDisplay.ActiveViewer.hWnd, false, out objCancel);

                //End flight if ESC key pressed
                if (bCancel == true)
                    EndFlight();

            }
            while (inUse == true && bCancel == false);

            bCancel = false;
        }

        public void EndFlight()
        {
            inUse = false;
            bCancel = true;
            speed = 0;
            globeDisplay.IsNavigating = false;

            // reposition target
            PointZ currentObs = new PointZ();
            IPoint newTarget = new PointClass();
            currentObs.x = camera.Observer.X;
            currentObs.y = camera.Observer.Y;
            currentObs.z = camera.Observer.Z;

            int orX = 0;
            int orY = 0;
            int width = 0;
            int height = 0;
            camera.GetViewport(ref orX, ref orY, ref width, ref height);

            object obj1;
            object obj2;
            try
            {
                globeDisplay.Locate(globeDisplay.ActiveViewer, width / 2, height / 2, true, true, out newTarget, out obj1, out obj2);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace.ToString());
            }

            if (newTarget == null) // no intersection with globe, but don't let the target to be too far
            {
                newTarget = camera.Target;
                PointZ tar = new PointZ(currentObs.x, currentObs.y, currentObs.z);

                double elevObs = tar.Norm() - 1.0;
                if (elevObs <= 0.0001)
                    elevObs = 0.0001;

                PointZ oldTarget = new PointZ(newTarget.X, newTarget.Y, newTarget.Z);
                PointZ dir = (oldTarget - tar);
                double val = dir.Norm();
                if (val > 0.0)
                {
                    dir.x = dir.x * elevObs * 10 / val;
                    dir.y = dir.y * elevObs * 10 / val;
                    dir.z = dir.z * elevObs * 10 / val;
                }

                tar = tar + dir;
                newTarget.X = tar.x;
                newTarget.Y = tar.y;
                newTarget.Z = tar.z;
            }

            ESRI.ArcGIS.GlobeCore.IGlobeViewUtil globeViewUtil = globeCamera as ESRI.ArcGIS.GlobeCore.IGlobeViewUtil;
            double obsLat;
            double obsLon;
            double obsAlt;
            double tarLat;
            double tarLon;
            double tarAlt;
            globeViewUtil.GeocentricToGeographic(currentObs.x, currentObs.y, currentObs.z, out obsLon, out obsLat, out obsAlt);
            globeViewUtil.GeocentricToGeographic(newTarget.X, newTarget.Y, newTarget.Z, out tarLon, out tarLat, out tarAlt);
            globeCamera.SetObserverLatLonAlt(obsLat, obsLon, obsAlt / 1000);
            globeCamera.SetTargetLatLonAlt(tarLat, tarLon, tarAlt / 1000);
            camera.RollAngle = 0;
            camera.PropertiesChanged();
            globeDisplay.RefreshViewers();

            //Windows API call to set cursor
            SetCursor(moveFlyCur.Handle.ToInt32());
        }

        public PointZ RotateNormal(double lastFrameDuration, double mouseXNorm, double mouseYNorm)
        {
            currentElevation = currentElevation - (lastFrameDuration * mouseYNorm * (Math.Abs(mouseYNorm)));
            currentAzimut = currentAzimut - (lastFrameDuration * mouseXNorm * (Math.Abs(mouseXNorm)));

            if (currentElevation > 0.45 * 3.141592)
            {
                currentElevation = 0.45 * 3.141592;
            }
            if (currentElevation < -0.45 * 3.141592)
            {
                currentElevation = -0.45 * 3.141592;
            }
            while (currentAzimut < 0)
            {
                currentAzimut = currentAzimut + (2 * 3.141592);
            }
            while (currentAzimut > 2 * 3.141592)
            {
                currentAzimut = currentAzimut - (2 * 3.141592);
            }

            double x = Math.Cos(currentElevation) * Math.Cos(currentAzimut);
            double y = Math.Cos(currentElevation) * Math.Sin(currentAzimut);
            double z = Math.Sin(currentElevation);

            GlobeFlyTool.PointZ p = new PointZ(x, y, z);
            return p;
        }

        #endregion
    }

}
