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
