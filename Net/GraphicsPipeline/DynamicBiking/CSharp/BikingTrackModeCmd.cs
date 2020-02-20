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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace DynamicBiking
{
	/// <summary>
	/// Summary description for BikingTrackModeCmd.
	/// </summary>
	[Guid("5a26e262-9e4c-498f-b77c-a6fdeee0dd4b")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("DynamicBiking.BikingTrackModeCmd")]
	public sealed class BikingTrackModeCmd : BaseCommand
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
			ControlsCommands.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ControlsCommands.Unregister(regKey);

		}

		#endregion
		#endregion

		private IHookHelper m_hookHelper = null;

		private DynamicBikingCmd m_dynamicBikingCmd = null;

		public BikingTrackModeCmd()
		{
			base.m_category = ".NET Samples";
			base.m_caption = "Dynamic biking track";
			base.m_message = "Dynamic biking track mode";
			base.m_toolTip = "Dynamic biking track mode";
			base.m_name = "DynamicBiking_BikingTrackModeCmd";

			try
			{
				string bitmapResourceName = GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
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

			if (m_hookHelper == null)
				m_hookHelper = new HookHelperClass();

			m_hookHelper.Hook = hook;
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			if (m_dynamicBikingCmd != null)
			{
				m_dynamicBikingCmd.TrackMode = !m_dynamicBikingCmd.TrackMode;
			}
		}

		public override bool Enabled
		{
			get
			{
				m_dynamicBikingCmd = GetBikingCmd();
				if (m_dynamicBikingCmd != null)
					return m_dynamicBikingCmd.IsPlaying;

				return false;
			}
		}

		public override bool Checked
		{
			get
			{
				if (m_dynamicBikingCmd != null)
				{
					return m_dynamicBikingCmd.TrackMode;
				}
				return false;
			}
		}

		#endregion

		private DynamicBikingCmd GetBikingCmd()
		{
			if (m_hookHelper.Hook == null)
				return null;

			DynamicBikingCmd dynamicBikingCmd = null;
			if (m_hookHelper.Hook is IToolbarControl2)
			{
				IToolbarControl2 toolbarCtrl = (IToolbarControl2)m_hookHelper.Hook;
				ICommandPool2 commandPool = toolbarCtrl.CommandPool as ICommandPool2;
				int commantCount = commandPool.Count;
				ICommand command = null;
				for (int i = 0; i < commantCount; i++)
				{
					command = commandPool.get_Command(i);
					if (command is DynamicBikingCmd)
					{
						dynamicBikingCmd = (DynamicBikingCmd)command;
					}
				}
			}

			return dynamicBikingCmd;
		}
	}
}
