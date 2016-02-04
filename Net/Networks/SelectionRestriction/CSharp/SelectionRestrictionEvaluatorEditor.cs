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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.ADF.CATIDs;

namespace SelectionRestriction
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("2200611c-4602-459b-abea-0ba8fb149a39")]
	public class SelectionRestrictionEvaluatorEditor : IEvaluatorEditor
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

		#region IEvaluatorEditor Members

		public bool ContextSupportsEditDescriptors
		{
			// The descriptor text is the single line of text in the Evaluators dialog that appears under the Value column
			// This property indicates whether the descriptor text can be directly edited in the dialog by the user
			// Since this evaluator editor does not make use of descriptors, it returns false
			get { return false; }
		}

		public bool ContextSupportsEditProperties
		{
			// This property indicates whether the ArcCatalog user is able to bring up a dialog by clicking the Properties button (or pressing F12) in order to specify settings for the evaluator
			// This evaluator editor does not support editing of the evaluator properties
			get { return false; }
		}

		public void EditDescriptors(string value)
		{
			// This evaluator editor does not make use of descriptors
		}

		public IEditEvaluators EditEvaluators
		{
			// This property is used by ArcCatalog to set a reference to its EditEvaluators object on each registered EvaluatorEditor
			// This allows each EvaluatorEditor to access the current state of ArcCatalog's Evaluators dialog, such as how many evaluators are listed and which evaluators are currently selected
			// This evaluator editor does not make use of EditEvaluators
			set { }
		}

		public bool EditProperties(int parentWindow)
		{
			// This evaluator editor does not support editing of the evaluator properties
			return false;
		}

		public UID EvaluatorCLSID
		{
			get
			{
				// This property returns the GUID of this EvaluatorEditor's associated INetworkEvaluator (e.g., SelectionRestrictionEvaluator)
				UID uid = new UIDClass();
				uid.Value = "{1f75097c-7224-4d1f-ae38-1242e26efcef}";
				return uid;
			}
		}

		public void SetDefaultProperties(int index)
		{
			// This method is called when the ArcCatalog user selects this evaluator under the Type column of the Evaluators dialog.
			// This method can be used to initialize any dialogs that the evaluator editor uses
			// Since this evaluator editor has no dialogs, it does not need to initialize anything
		}

		public int ValueChoice
		{
			// This evaluator editor does not support value choices
			set { }
		}

		public int ValueChoiceCount
		{
			// This evaluator editor has no value choices
			get { return 0; }
		}

		public string get_Descriptor(int index)
		{
			// This evaluator editor does not make use of descriptors
			return string.Empty;
		}

		public string get_FullDescription(int index)
		{
			// This property is the text representation of all of the settings made on this evaluator
			// This evaluator editor does not make any settings changes, so it returns an empty string
			return string.Empty;
		}

		public string get_ValueChoiceDescriptor(int choice)
		{
			// This evaluator editor does not make use of value choices
			return string.Empty;
		}

		#endregion
	}
}
