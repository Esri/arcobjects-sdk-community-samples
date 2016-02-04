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
using System.Windows.Forms;
using System.Drawing;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;

namespace WPFMapViewer
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MapWindow: Window
	{
		AxMapControl mapControl;
		AxToolbarControl toolbarControl;
		AxTOCControl tocControl;

		public MapWindow ()
		{
			InitializeComponent ();
			CreateEngineControls ();
		}

		// Create ArcGIS Engine Controls and set them to be child of each WindowsFormsHost elements
		void CreateEngineControls ()
		{
			//set Engine controls to the child of each hosts 
			mapControl = new AxMapControl ();
			mapHost.Child = mapControl;

			toolbarControl = new AxToolbarControl ();
			toolbarHost.Child = toolbarControl;

			tocControl = new AxTOCControl ();
			tocHost.Child = tocControl;
		}

		private void LoadMap ()
		{
			//Buddy up controls
			tocControl.SetBuddyControl (mapControl);
			toolbarControl.SetBuddyControl (mapControl);

			//add command and tools to the toolbar
			toolbarControl.AddItem ("esriControls.ControlsOpenDocCommand");
			toolbarControl.AddItem ("esriControls.ControlsAddDataCommand");
			toolbarControl.AddItem ("esriControls.ControlsSaveAsDocCommand");
			toolbarControl.AddItem ("esriControls.ControlsMapNavigationToolbar");
			toolbarControl.AddItem ("esriControls.ControlsMapIdentifyTool");
			
			//set controls' properties
			toolbarControl.BackColor =Color.FromArgb (245, 245, 220);

			//wire up events
			mapControl.OnMouseMove +=new IMapControlEvents2_Ax_OnMouseMoveEventHandler(mapControl_OnMouseMove);
		}

		private void Window_Loaded (object sender, RoutedEventArgs e)
		{
			LoadMap ();
		}

		private void mapControl_OnMouseMove (object sender, IMapControlEvents2_OnMouseMoveEvent e)
		{
			textBlock1.Text = " X,Y Coordinates on Map: " + string.Format ("{0}, {1}  {2}", e.mapX.ToString ("#######.##"), e.mapY.ToString ("#######.##"), mapControl.MapUnits.ToString ().Substring (4));
		}

		private void Window_Closing (object sender, System.ComponentModel.CancelEventArgs e)
		{
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown ();
		}
	}
}
