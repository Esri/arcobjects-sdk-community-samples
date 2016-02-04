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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.ADF.CATIDs;

namespace SubsetNetworkEvaluatorsUI
{
	/// <summary>
	/// The FilterSubsetEvaluatorEditor is used to help you to assign the filter subset evaluator manually
	/// using the evaluator dialog launched from the attributes page of the network dataset property sheet.
	/// The registration of the class as a NetworkEvaluatorEditor component allows the evaluator to show up
	/// as a choice of types in the evaluators dialog.
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("bce52bcb-f560-4cce-94a5-a7625626de8c")]
	[ProgId("SubsetNetworkEvaluatorsUI.FilterSubsetEvaluatorEditor")]
	public class FilterSubsetEvaluatorEditor : IEvaluatorEditor
	{
		#region Component Category Registration

		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			NetworkEvaluatorEditor.Register(regKey);
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			NetworkEvaluatorEditor.Unregister(regKey);
		}

		#endregion

		#region Member Variables

		private IEditEvaluators m_EditEvaluators;
		private static string s_description =
@"Filter Subset Evaluator:

Usage: Restricts a subset of network elements included in a list of EIDs, or optionally restricts
those elements NOT in the list of EIDs if FilterSubset_Restrict is false.  If FilterSubset_Restrict is false
then in the special case that a source has NO EIDs in the list then no elements are restricted
rather than restricting all elements.

Parameters: (name:defaultvalue; type:domain; description)
-FilterSubset_Restrict:true; Boolean; indicated if listed EIDs should be restricted (FilterSubset_Restrict=true),
or those not in the EID list should be restricted (FilterSubset_Restrict=false)
-FilterSubset_EIDs_<Source>:<null>; integer[]; subset of EIDs for a given source name to filter
(replace <source> with a specific network source name such as streets without angle
brackets to filter a specified subset of element ids for that source.  Multiple parameter
EID subset list parameters can be added to filter a subset of multiple network sources.";

		#endregion

		#region IEvaluatorEditor Members

		public bool ContextSupportsEditDescriptors
		{
			// TODO: implement alternative ContextSupportsEditDescriptors logic here
			get { return false; }
		}

		public bool ContextSupportsEditProperties
		{
			// TODO: implement alternative ContextSupportsEditProperties logic here
			get { return true; }
		}

		public void EditDescriptors(string value)
		{
			// TODO: implement EditDescriptors
		}

		public IEditEvaluators EditEvaluators
		{
			// TODO: implement alternative EditEvaluators logic here
			set { m_EditEvaluators = value; }
		}

		public bool EditProperties(int parentWindow)
		{
			SimpleEvaluatorProperties dlg = new SimpleEvaluatorProperties(s_description);
			IWin32Window parentWin32Handle = (IWin32Window)(new WindowWrapper((IntPtr)parentWindow));
			dlg.ShowDialog(parentWin32Handle);

			return false; // edited
		}

		public UID EvaluatorCLSID
		{
			get
			{
				UID uid = new UIDClass();
				uid.Value = "{e2a9fbbf-8950-48cb-b487-0ee3f43dccca}";    // Returns the GUID of FilterEvaluator
				return uid;
			}
		}

		public void SetDefaultProperties(int index)
		{
			// TODO: implement SetDefaultProperties
		}

		public int ValueChoice
		{
			set
			{
				// TODO: implement ValueChoice
			}
		}

		public int ValueChoiceCount
		{
			// TODO: implement alternative ValueChoiceCount logic here
			get { return 0; }
		}

		public string get_Descriptor(int index)
		{
			// TODO: implement alternative get_Descriptor logic here
			return string.Empty;
		}

		public string get_FullDescription(int index)
		{
			// TODO: implement alternative get_FullDescription logic here
			return string.Empty;
		}

		public string get_ValueChoiceDescriptor(int choice)
		{
			// TODO: implement alternative get_ValueChoiceDescriptor logic here
			return string.Empty;
		}

		#endregion
	}
}
