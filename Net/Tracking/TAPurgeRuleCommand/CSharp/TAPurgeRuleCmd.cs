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
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace TAPurgeRuleCommand
{
	/// <summary>
	/// Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
	/// or ArcGlobe/GlobeControl
	/// </summary>
	[Guid("4a9ae2c3-dfdb-4b55-922d-558a1a9ccfe1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("TAPurgeRuleCommand.TAPurgeRuleCmd")]
	public sealed class TAPurgeRuleCmd : BaseCommand
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
			GMxCommands.Register(regKey);
			MxCommands.Register(regKey);
			SxCommands.Register(regKey);
			ControlsCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GMxCommands.Unregister(regKey);
			MxCommands.Unregister(regKey);
			SxCommands.Unregister(regKey);
			ControlsCommands.Unregister(regKey);
		}

		#endregion
		#endregion

		private IHookHelper m_hookHelper = null;
		private IGlobeHookHelper m_globeHookHelper = null;
		private const string TEMPORALLAYERCLSID = "{78C7430C-17CF-11D5-B7CF-00010265ADC5}"; //CLSID for ITemporalLayer
		private PurgeRuleForm m_PRForm;

		public TAPurgeRuleCmd()
		{
			//
			// TODO: Define values for the public properties
			//
			base.m_category = ".NET Samples"; //localizable text
			base.m_caption = "Change the purge rule for temporal layers";  //localizable text
			base.m_message = "Change the purge rule for temporal layers"; //localizable text 
			base.m_toolTip = "Change the purge rule for temporal layers";  //localizable text 
			base.m_name = "TAPurgeRuleCommand_TAPurgeRuleCmd";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

			m_PRForm = new PurgeRuleForm();

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

			// Test the hook that calls this command and disable if nothing is valid
			try
			{
				m_hookHelper = new HookHelperClass();
				m_hookHelper.Hook = hook;
				if (m_hookHelper.ActiveView == null)
				{
					m_hookHelper = null;
				}
			}
			catch
			{
				m_hookHelper = null;
			}
			if (m_hookHelper == null)
			{
				//Can be globe
				try
				{
					m_globeHookHelper = new GlobeHookHelperClass();
					m_globeHookHelper.Hook = hook;
					if (m_globeHookHelper.ActiveViewer == null)
					{
						m_globeHookHelper = null;
					}
				}
				catch
				{
					m_globeHookHelper = null;
				}
			}

			if (m_globeHookHelper == null && m_hookHelper == null)
				base.m_enabled = false;
			else
				base.m_enabled = true;

			//TODO: Add other initialization code
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			//Show the dialog, pass it the temporal layers from the map, have it initialize the dialog
			if (!m_PRForm.Visible)
			{
				m_PRForm.Show();
				m_PRForm.TrackingLayers = GetAllTrackingLayers();
				m_PRForm.PopulateDialog();
			}
			else
			{
				m_PRForm.Hide();
			}
		}

		//Show the command as depressed when the dialog is visible
		public override bool Checked
		{
			get
			{
				return m_PRForm.Visible;
			}
		}
		#endregion

		//Query the map for all the tracking layers in it
		private IEnumLayer GetAllTrackingLayers()
		{
			IEnumLayer eLayers = null;
			try
			{
				IBasicMap basicMap = null;
				IUID uidTemoralLayer = new UIDClass();
				uidTemoralLayer.Value = TEMPORALLAYERCLSID;

				if (m_hookHelper != null)
				{

					basicMap = m_hookHelper.FocusMap as IBasicMap;
				}
				else if (m_globeHookHelper != null)
				{
					basicMap = m_globeHookHelper.Globe as IBasicMap;
				}

				//This call throws an E_FAIL exception if the map has no layers, caught below
				if (basicMap != null)
				{
					eLayers = basicMap.get_Layers((UID)uidTemoralLayer, true);
				}
			}
			catch
			{
			}
			
			return eLayers;
		}

	}
}
