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
	[Guid("38ec31b1-a442-4231-b304-afe32f186fa2")]
	public sealed class ZoomOut: BaseCommand
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

		public ZoomOut()
		{
			//Set the command properties			
			base.m_caption = "Variable Zoom Out";
			base.m_message = "Variable Zoom Out";
			base.m_toolTip = "Variable Zoom Out";
			base.m_category = "ZoomExtension Sample(CSharp)";
			base.m_name = "ZoomExtension Sample(CSharp)_Variable Zoom Out";
			base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("Commands.zoomoutfxd.bmp"));
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
			envelope.Expand(zoomFactor,zoomFactor,true);
			activeView.Extent = envelope;
			activeView.Refresh();
		}
	}
}