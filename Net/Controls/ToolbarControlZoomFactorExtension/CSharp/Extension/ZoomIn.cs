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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace ZoomFactorExtensionCSharp
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("745f7a73-ac7b-418d-a709-fb5b4cb179ae")]
	public sealed class ZoomIn: BaseCommand
	{
		//The HookHelper object that deals with the hook passed to the OnCreate event
		private IHookHelper m_HookHelper = new HookHelperClass();

		#region "Component Category Registration"
		[ComRegisterFunction()]
		static void Reg(string regKey)
		{
			ControlsCommands.Register(regKey);
		}

		[ComUnregisterFunction()]
		static void Unreg(string regKey)
		{
			ControlsCommands.Unregister(regKey);
		}
		#endregion

		public ZoomIn()
		{
			//Set the command properties			
			base.m_caption = "Variable Zoom In";
			base.m_message = "Variable Zoom In";
			base.m_toolTip = "Variable Zoom In";
			base.m_category = "ZoomExtension Sample(CSharp)";
			base.m_name = "ZoomExtension Sample(CSharp)_Variable Zoom In";
			base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("Commands.zoominfxd.bmp"));
		}
	
		public override void OnCreate(object hook)
		{
			m_HookHelper.Hook = hook;
		}
		
		public override bool Enabled
		{
			get
			{
				//Get the extension manager
				IExtensionManager extensionManager = new ExtensionManagerClass();

				//Get the extension from the extension manager
				IExtension extension = extensionManager.FindExtension("Zoom Factor Extension");

				//Get the state of the extension
				IExtensionConfig extensionConfig = (IExtensionConfig) extension;
				if (extensionConfig != null)
				{
					if (extensionConfig.State == esriExtensionState.esriESEnabled) return true;
					else return false;
				}
				else
				{
					return false;
				}
			}
		}

		public override void OnClick()
		{
			//Get the current extent of the active view
			IActiveView activeView = m_HookHelper.ActiveView;
			IEnvelope envelope = activeView.Extent;

			//Get the extension manager
			ExtensionManager extensionManager = new ExtensionManagerClass();
			//Get the extension from the extension manager
			IExtension extension = extensionManager.FindExtension("Zoom Factor Extension");

			//Get the zoom factor from the extension
			double zoomFactor = 1.1;
			if (extension != null)
			{
				IZoomExtension zoomExtension = (IZoomExtension) extension;
				zoomFactor = zoomExtension.ZoomFactor;
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("The extension cannot be found!");
			}

			//Update the current extent of the active view
			envelope.Expand((System.Convert.ToDouble(1.0)/ zoomFactor), (System.Convert.ToDouble(1.0) / zoomFactor), true);
			activeView.Extent = envelope;
			activeView.Refresh();
		}
	}
}