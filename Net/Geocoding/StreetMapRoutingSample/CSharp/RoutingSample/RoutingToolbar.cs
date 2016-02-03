using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;


namespace RoutingSample
{

	[Guid("802B4297-9F7E-438d-B8A8-CB62C9658301"),
	ComVisible(true),
	ClassInterface(ClassInterfaceType.None),
	ProgId("RoutingSample.RoutingToolbar")]
	public class RoutingToolbar : ESRI.ArcGIS.SystemUI.IToolBarDef
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
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommandBars.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommandBars.Unregister(regKey);
		}

	#endregion
	#endregion

	#region Public Methods + Properties

		// A creatable COM class must have a Public Sub New() 
		// with no parameters, otherwise, the class will not be 
		// registered in the COM registry and cannot be created 
		// via CreateObject.
		public RoutingToolbar() : base()
		{
		}

		public string Caption
		{
			get
			{
				return "Routing Sample";
			}
		}

		public void GetItemInfo(int pos, ESRI.ArcGIS.SystemUI.IItemDef itemDef)
		{
			if (pos == 0)
			{
				itemDef.ID = "RoutingSample.RoutingCommand";
				itemDef.Group = false;
			}
		}

		public int ItemCount
		{
			get
			{
				return 1;
			}
		}

		public string Name
		{
			get
			{
				return "RoutingSampleCSharpCommand";
			}
		}

	#endregion

	}



} //end of root namespace