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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.NetworkAnalystUI;
using SubsetNetworkEvaluators;

namespace SubsetNetworkEvaluatorsUI
{
	/// <summary>
	/// The RemoveSubsetAttributesCommand is a context menu item automatically added to the ArcCatalog
	/// Network Dataset context menu.  If the network analyst extension license is checked out
	/// you can use this command to quickly reverse the affects of the AddSubsetAttributesCommand.  It only
	/// removes those subset attributes that were or could have been added by the add command.  You can always
	/// just remove the subset attributes manually using the attributes page of the network dataset property sheet.
	/// Removing the attribute will also remove the parameters and evaluator assignments.
	/// </summary>
	[Guid("EDD273A2-2AC2-4f2f-B096-7088B58F0667")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("SubsetNetworkEvaluatorsUI.RemoveSubsetAttributesCommand")]
	public sealed class RemoveSubsetAttributesCommand : BaseCommand
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
			GxCommands.Register(regKey);
			GxNetworkDatasetContextMenuCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			GxCommands.Unregister(regKey);
			GxNetworkDatasetContextMenuCommands.Unregister(regKey);
		}

		#endregion
		#endregion

		private IApplication m_application = null;
		private INetworkAnalystExtension m_nax = null;

		public RemoveSubsetAttributesCommand()
		{
			base.m_category = "Network Analyst Samples";   //localizable text
			base.m_caption = "Remove Subset Attributes";      //localizable text
			base.m_message = "Remove Subset Attributes";      //localizable text 
			base.m_toolTip = "Remove Subset Attributes";      //localizable text 
			base.m_name = "NASamples_RemoveSubsetAttributes"; //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
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

			m_application = hook as IApplication;
			m_nax = SubsetHelperUI.GetNAXConfiguration(m_application) as INetworkAnalystExtension;
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			IGxApplication gxApp = m_application as IGxApplication;
			IGxDataset gxDataset = null;
			esriDatasetType dsType = esriDatasetType.esriDTAny;

			if (gxApp != null)
			{
				gxDataset = gxApp.SelectedObject as IGxDataset;
				dsType = gxDataset.Type;
			}

			if (dsType != esriDatasetType.esriDTNetworkDataset)
				return;

			IDataset ds = gxDataset.Dataset;
			if (ds == null)
				return;

			INetworkDataset nds = ds as INetworkDataset;
			if (nds == null)
				return;

			if (!nds.Buildable)
				return;

			INetworkBuild netBuild = nds as INetworkBuild;
			if (netBuild == null)
				return;

			IDatasetComponent dsComponent = nds as IDatasetComponent;
			IDENetworkDataset deNet = null;
			if (dsComponent != null)
				deNet = dsComponent.DataElement as IDENetworkDataset;

			if (deNet == null)
				return;

			FilterSubsetEvaluator.RemoveFilterSubsetAttribute(deNet);
			ScaleSubsetEvaluator.RemoveScaleSubsetAttributes(deNet);

			netBuild.UpdateSchema(deNet);
		}

		public override bool Enabled
		{
			get
			{
				IGxApplication gxApp = m_application as IGxApplication;
				IGxDataset gxDataset = null;
				esriDatasetType dsType = esriDatasetType.esriDTAny;

				bool naxEnabled = false;
				IExtensionConfig naxConfig = m_nax as IExtensionConfig;
				if (naxConfig != null)
					naxEnabled = naxConfig.State == esriExtensionState.esriESEnabled;

				if (naxEnabled)
				{
					if (gxApp != null)
					{
						gxDataset = gxApp.SelectedObject as IGxDataset;
						dsType = gxDataset.Type;
					}
				}

				return (dsType == esriDatasetType.esriDTNetworkDataset);
			}
		}

		#endregion
	}
}
