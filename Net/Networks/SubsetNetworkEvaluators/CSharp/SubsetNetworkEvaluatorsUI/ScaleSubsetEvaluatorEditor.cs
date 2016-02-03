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
	/// The ScaleSubsetEvaluatorEditor is used to help you to assign the scale subset evaluator manually
	/// using the evaluator dialog launched from the attributes page of the network dataset property sheet.
	/// The registration of the class as a NetworkEvaluatorEditor component allows the evaluator to show up
	/// as a choice of types in the evaluators dialog.
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("08aed73c-da8a-4d02-9e3a-453870cda178")]
	[ProgId("SubsetNetworkEvaluatorsUI.ScaleSubsetEvaluatorEditor")]
	public class ScaleSubsetEvaluatorEditor : IEvaluatorEditor
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
@"Scale Subset Evaluator:

Usage: Returns the base attribute value times a ScaleSubset_Factor for a subset of
elements of a base attribute (and non-scaled elements simply return a non-scaled
base attribute value for the element).  The base attribute and this evaluator's
attribute should both be a double cost attribute of the same units.  The base
attribute is implied by this evaluator's attribute name. The base attribute name
is the part of this evaluator's attribute name after the first underscore ('_')
character. So, if this attribute is 'ScaleSubset_Minutes', then the base attribute is
'Minutes'. The parameter values indicate the ScaleSubset_Factor and the subset of elements to
be scaled.

Parameters: (name:defaultvalue; type:domain; description)
-ScaleSubset_Factor:1; double:>0; the amount to scale applicable base attribute values
-ScaleSubset_EIDs_<Source>:<null>; integer[]; subset of EIDs for a given source name to scale
(replace <source> with a specific network source name such as streets without angle
brackets to scale a specified subset of element ids for that source.  Multiple parameter
EID subset list parameters can be added to scale a subset of multiple network sources.";

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
				uid.Value = "{67cf8446-22a2-4baf-9c97-3c22a33cc0c7}";    // Returns the GUID of ScaleEvaluator
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
