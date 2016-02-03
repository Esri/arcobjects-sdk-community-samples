using System;
using System.Runtime.InteropServices;
using System.Drawing;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace RoutingSample
{
	[Guid("86E98EC6-9C64-4ffe-B639-23B3EEA3ABF5"),
	 ComVisible(true),
	 ClassInterface(ClassInterfaceType.None), 
	 ProgId("RoutingSample.RoutingCommand")]
	public sealed class RoutingCommand : BaseCommand
	{

	#region COM Registration Function(s)
		[ComRegisterFunction(), ComVisibleAttribute(false)]
		public static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);
		}

		[ComUnregisterFunction(), ComVisibleAttribute(false)]
		public static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);
		}

	#region ArcGIS Component Category Registrar generated code
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Register(regKey);
		}

		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Unregister(regKey);
		}

	#endregion
	#endregion

	#region Private members

		// ArcGIS application
		private IMxApplication m_application;
		// Routing form
		private RoutingForm m_dlgRouting;

	#endregion

	#region Public methods + properties

		// A creatable COM class must have a Public Sub New() 
		// with no parameters, otherwise, the class will not be 
		// registered in the COM registry and cannot be created 
		// via CreateObject.
		public RoutingCommand() : base()
		{

			base.m_category = "Developer Samples";
			base.m_caption = "Routing Sample";
			base.m_message = "Routing Sample in C#. Click for Route finding.";
			base.m_toolTip = "Routing Sample in C#";
			base.m_name = "RoutingSampleCSharpCommand";

			try
			{
				string bitmapResourceName = "Res." + this.GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(this.GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		public override bool Checked
		{
			get
			{
				// Checked if Routing Form is Visible
				if (m_dlgRouting == null)
						return false;

				return m_dlgRouting.Visible;
			}
		}

		public override void OnCreate(object hook)
		{
			if (hook != null)
			{
				if ((hook) is IMxApplication)
					m_application = (IMxApplication)hook;
			}
		}

		public override void OnClick()
		{
			if (m_dlgRouting == null)
			{
				// Create Routing Form
				m_dlgRouting = new RoutingForm();
				m_dlgRouting.Init((IApplication)m_application);

				// show form
				m_dlgRouting.Show();

				// Set ArcMap window as owner for Routing Form
				SetWindowLong(m_dlgRouting.Handle.ToInt32(), GWL_HWNDPARENT, ((IApplication)m_application).hWnd);
			}
			else
			{
				// just show/hide form
				if (m_dlgRouting.Visible)
					m_dlgRouting.Hide();
				else
					m_dlgRouting.Show();
			}
		}

	#endregion

	#region Imported functions

		// Needed to show non-modal Routing form
		[System.Runtime.InteropServices.DllImport("user32", EntryPoint="SetWindowLongA", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Ansi, SetLastError=true)]
		public static extern int SetWindowLong(int hwnd, int nIndex, int dwNewLong);
		public const int GWL_HWNDPARENT = (-8);

	#endregion

	}




} //end of root namespace