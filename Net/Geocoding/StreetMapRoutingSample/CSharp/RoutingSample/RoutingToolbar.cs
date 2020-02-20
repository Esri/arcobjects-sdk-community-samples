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