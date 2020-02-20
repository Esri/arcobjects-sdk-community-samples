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
using System.Runtime.InteropServices;
using System.Windows.Forms;

// This command brings up the property pages for the ArcGIS Network Analyst extension environment.
namespace NAEngine
{
	[Guid("7E98FE97-DA7A-4069-BC85-091D75B1AF65")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NAEngine.NAProperties")]
	public sealed class cmdNAProperties : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
	{
		public cmdNAProperties()
		{
			base.m_caption = "Properties...";
		}

		public override void OnClick()
		{
			// Show the Property Page form for ArcGIS Network Analyst extension
			var props = new frmNAProperties();
			props.ShowModal();
		}

		public override void OnCreate(object hook)
		{
			// Since this ToolbarMenu item is on the ToolbarControl the Hook is initialized by the ToolbarControl.
			var toolbarControl = hook as ESRI.ArcGIS.Controls.IToolbarControl;
		}
	}
}
