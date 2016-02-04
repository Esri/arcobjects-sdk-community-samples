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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZoomFactorExtensionCSharp
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("e9ea3574-e45f-4197-9e07-9c0c323f8791")]
	public sealed class SetZoomFactor: BaseCommand
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

		public SetZoomFactor()
		{
			//Set the command properties			
			base.m_caption = "Set Variable Zoom";
			base.m_message = "Set Variable Zoom";
			base.m_toolTip = "Set Variable Zoom";
			base.m_category = "ZoomExtension Sample(CSharp)";
			base.m_name = "ZoomExtension Sample(CSharp)_Set Variable Zoom";
			base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("Commands.zoomfactor.bmp"));
		}
	
		public override void OnCreate(object hook)
		{
			//Not implemented
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
			//Get the extension manager
			ExtensionManager extensionManager = new ExtensionManagerClass();
			//Get the extension from the extension manager
			IExtension extension = extensionManager.FindExtension("Zoom Factor Extension");

			IZoomExtension zoomExtension = (IZoomExtension) extension;
			double zoomFactor = zoomExtension.ZoomFactor;

			//Get a zoom factor from the user
			InputFormResult res = InputForm.ShowModal(null,"Enter a zoom factor","ZoomExtension Sample", zoomExtension.ZoomFactor.ToString() );

			if (res.Result == DialogResult.Cancel) return;
			string zoomString = res.InputString;
			if (zoomString.Trim() == "") return;

			//Set the zoom factor
			if (System.Char.IsNumber(zoomString,0) == true) zoomExtension.ZoomFactor = System.Convert.ToDouble(zoomString);
		}
	}
}