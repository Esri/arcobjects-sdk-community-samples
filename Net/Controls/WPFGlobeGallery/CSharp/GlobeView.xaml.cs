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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Analyst3D;


namespace GlobeGallery
{
    public partial class GlobeView : Window
    {
				private Map _map;
        public AxGlobeControl globeControl;
				private string mapPath=@"..\..\..\..\..\data\Globe\";

        public GlobeView()
        {
            InitializeComponent();
        }

				public Map SelectedMap
				{
					get { return _map; }
					set { _map = value; }
				}

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
					CreateEngineControls();
				}

				// Create ArcGIS Engine Controls and set them to be child of each WindowsFormsHost elements
				private void CreateEngineControls ()
				{
					//set Engine controls to the child of each hosts 
					globeControl = new AxGlobeControl ();
					mapHost.Child = globeControl;

					//set Engine controls properties
          globeControl.BackColor = System.Drawing.Color.Black;
					globeControl.OnMouseMove += new IGlobeControlEvents_Ax_OnMouseMoveEventHandler (globeControl_OnMouseMove);
					//style
					globeControl.BorderStyle = 0;
          // set default tool
          Navigate(null, null);
          // listen to events
          GlobeDisplay glbDisplay = globeControl.GlobeDisplay as GlobeDisplay;
          glbDisplay.AfterDraw += new IGlobeDisplayEvents_AfterDrawEventHandler(glbDisplay_AfterDraw);
				}
   
        private void glbDisplay_AfterDraw(ISceneViewer pViewer)
        {
					// load 3dd on first time render only, let the WPF drawing thread render the window controls first
          if (globeControl.DocumentFilename == null)
              globeControl.Load3dFile(mapPath + _map.MapName + ".3dd");   
        }

				private void globeControl_OnMouseMove (object sender, IGlobeControlEvents_OnMouseMoveEvent e)
				{
					//get scale range in Kilometers
					IGlobeViewUtil globeCamera = globeControl.GlobeCamera as IGlobeViewUtil;
					double distanceInKM = globeCamera.ScalingDistance/1000;
	        Coordinates.Text = string.Format("{0} {1} {2}", "Distance: ", distanceInKM.ToString("######.##"), globeControl.Globe.GlobeUnits.ToString().Substring(4));
        }

        private void Navigate(object sender, RoutedEventArgs e)
        {
            ICommand command = new ControlsGlobeNavigateTool();
            command.OnCreate(globeControl.Object);
            globeControl.CurrentTool = (ITool)command;
        }

				private void Fly (object sender, RoutedEventArgs e)
				{
					ICommand command = new ControlsGlobeFlyTool ();
					command.OnCreate (globeControl.Object);
					globeControl.CurrentTool = (ITool) command;
				}

				private void FullExtent (object sender, RoutedEventArgs e)
				{
					ICommand command = new ControlsGlobeFullExtentCommand ();
					command.OnCreate (globeControl.Object);
					command.OnClick ();
				}

        private void SpinLeft(object sender, RoutedEventArgs e)
        {
            ICommand command = new ControlsGlobeSpinClockwiseCommand();
            command.OnCreate(globeControl.Object);
            command.OnClick();
        }

        private void SpinRight(object sender, RoutedEventArgs e)
        {
            ICommand command = new ControlsGlobeSpinCounterClockwiseCommand();
            command.OnCreate(globeControl.Object);
            command.OnClick();
        }

        private void SpinStop(object sender, RoutedEventArgs e)
        {
            ICommand command = new ControlsGlobeSpinStopCommand();
            command.OnCreate(globeControl.Object);
            command.OnClick();
        }

				private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
				{
					//Important: must release control and other related com objects.
					//otherwise application may not shut down properly.
					globeControl.Dispose ();
					ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown ();
				}
    }
}