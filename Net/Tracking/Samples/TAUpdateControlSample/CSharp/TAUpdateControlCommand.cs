using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace TAUpdateControlSample
{
	/// <summary>
	/// Command that works in ArcMap/Map/PageLayout
	/// </summary>
	[Guid("943e3b92-a090-485f-9a5d-2d0dddc35409")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("TAUpdateControlSample.TAUpdateControlCommand")]
	public sealed class TAUpdateControlCommand : BaseCommand
	{
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
			MxCommands.Register(regKey);
			ControlsCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Unregister(regKey);
			ControlsCommands.Unregister(regKey);
		}

		#endregion
		#endregion

		private TAUpdateControlForm m_TAUpdateControlForm = null;
		private IHookHelper m_hookHelper = null;
		public TAUpdateControlCommand()
		{
			//
			// TODO: Define values for the public properties
			//
			base.m_category = ".NET Samples"; //localizable text
			base.m_caption = "Demonstrates the use of the TAUpdateControl.";  //localizable text 
			base.m_message = "Demonstrates the use of the TAUpdateControl.";  //localizable text
			base.m_toolTip = "Demonstrates the use of the TAUpdateControl.";  //localizable text
			base.m_name = "TAUpdateControlSample_TAUpdateControlCommand";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

			try
			{
				//
				// TODO: change bitmap name if necessary
				//
				string bitmapResourceName = GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}

			m_TAUpdateControlForm = new TAUpdateControlForm();
		}

		#region Overriden Class Methods

		/// <summary>
		/// Occurs when this command is created
		/// </summary>
		/// <param name="hook">Instance of the application</param>
		public override void OnCreate(object hook)
		{
			if (hook == null)
				return;

			try
			{
				m_hookHelper = new HookHelperClass();
				m_hookHelper.Hook = hook;
				if (m_hookHelper.ActiveView == null)
					m_hookHelper = null;
			}
			catch
			{
				m_hookHelper = null;
			}

			if (m_hookHelper == null)
				base.m_enabled = false;
			else
				base.m_enabled = true;

			// TODO:  Add other initialization code
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			if (!m_TAUpdateControlForm.Visible)
			{
				m_TAUpdateControlForm.Show();
				m_TAUpdateControlForm.PopulateDialog();
			}
			else
			{
				m_TAUpdateControlForm.Hide();
			}
		}

		public override bool Checked
		{
			get
			{
				return m_TAUpdateControlForm.Visible;
			}
		}
		#endregion
	}
}
