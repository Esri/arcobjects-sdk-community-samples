using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.NetworkAnalystUI;

namespace NABarrierLocationEditor
{
	/// <summary>
	/// Summary description for BarrierLocationEditor.
	/// 
	/// This sample teaches how to load polygon and polyline barrier values programmatically,
	/// while also providing a way to visualize and edit the underlying network element values that make up a polygon or polyline barrier.
	/// As a side benefit, the programmer also learns how to flash the geometry of a network element 
	/// (with a corresponding digitized direction arrow), as well as how to set up a context menu command for the NAWindow.
	///   
	/// </summary>
	[Guid("7a93ba10-9dee-11da-a746-0800200c9a66")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("NABarrierLocationEditor.NABarrierLocationEditor")]
	public sealed class NABarrierLocationEditor : BaseCommand, INAWindowCommand2
	{
		public INetworkAnalystExtension m_naExt; // Hook into the Desktop NA Extension
		public IApplication m_application; // Hook into ArcMap

		#region COM Registration Function(s)
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
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
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Register(regKey);
			ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Register(regKey);
			// Register with NetworkAnalystWindowItemsCommand to get the 
			// command to show up when you right click on the class in the NAWindow
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowItemsCommand.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ESRI.ArcGIS.ADF.CATIDs.MxCommands.Unregister(regKey);
			ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Unregister(regKey);
			ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowItemsCommand.Unregister(regKey);
		}

		#endregion

		#endregion

		public NABarrierLocationEditor()
		{
			base.m_category = "Developer Samples";
			base.m_caption = "Edit Network Analyst Barrier Location Ranges";
			base.m_message = "Edit Network Analyst Barrier Location Ranges";
			base.m_toolTip = "Edit Network Analyst Barrier Location Ranges";
			base.m_name = "EditNABarrierLocationRanges";

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

		~NABarrierLocationEditor()
		{
			m_application = null;
			m_naExt = null;
			GC.Collect();
		}

		#region Overriden Class Methods

		/// <summary>
		/// Occurs when this command is created
		/// </summary>
		/// <param name="hook">Instance of the application</param>
		/// 
		public override void OnCreate(object hook)
		{
			if (hook == null)
				return;

			m_application = hook as IApplication;
			base.m_enabled = true;

			if (m_application != null)
				m_naExt = m_application.FindExtensionByName("Network Analyst") as INetworkAnalystExtension;
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			try
			{
				OpenBarrierEditorForm();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
			}
		}

		#endregion

		#region Overridden INAWindowCommand Methods

		public bool Applies(INALayer naLayer, INAWindowCategory category)
		{
			// The category is associated with an NAClass.
			// In our case, we want the PolygonBarriers and PolylineBarriers classes
			if (category != null)
			{
				string categoryName = category.NAClass.ClassDefinition.Name;
				if (categoryName == "PolygonBarriers" || categoryName == "PolylineBarriers")
					return true;
			}

			return false;
		}

		#region INAWindowCommand2 Members

		/// <summary>
		/// Occurs in determining whether or not to include the command as a context menu item
		/// <param name="naLayer">The active analysis layer</param>
		/// <param name="categoryGroup">The selected group in the NAWindow</param>
		/// </summary>
		bool INAWindowCommand2.AppliesToGroup(INALayer naLayer, INAWindowCategoryGroup categoryGroup)
		{
			if (categoryGroup != null)
			{
				return Applies(naLayer, categoryGroup.Category);
			}

			return false;
		}

		int INAWindowCommand2.Priority
		{
			get { return 1; }
		}

		#endregion

		#endregion

		/// <summary>
		/// This command will be enabled for Polygon and Polyline Barriers
		/// </summary>
		public override bool Enabled
		{
			get
			{
				return Applies(null, GetActiveCategory());
			}
		}

		/// <summary>
		/// To open the editor form, we need to first determine which barrier is
		///  being edited, then pass that value to the form
		/// </summary>
		private void OpenBarrierEditorForm()
		{
			// get the barrier layer by using the category name to as the NAClassName
			INAWindowCategory activeCategory = GetActiveCategory();
			string categoryName = activeCategory.NAClass.ClassDefinition.Name;
			INALayer naLayer = GetActiveAnalysisLayer();
			ILayer layer = naLayer.get_LayerByNAClassName(categoryName);

			// get a selection count and popup a message if more or less than one item is selected
			IFeatureSelection fSel = layer as IFeatureSelection;
			ISelectionSet selSet = fSel.SelectionSet;
			if (selSet.Count != 1)
				System.Windows.Forms.MessageBox.Show("Only one barrier in a category can be selected at a time for this command to execute", "Barrier Location Editor Warning");
			else
			{
				// get the object IDs of the selected item
				int id = selSet.IDs.Next();

				// Get the barrier feature by using the selected ID
				IFeatureClass fClass = naLayer.Context.NAClasses.get_ItemByName(categoryName) as IFeatureClass;
				IFeature barrierFeature = fClass.GetFeature(id);

				// display the form for editing the barrier
				EditorForm form = new EditorForm(m_application, naLayer.Context, barrierFeature);
				form.ShowDialog();
				form = null;
			}
		}

		#region "NAWindow Interaction"

		private INALayer GetActiveAnalysisLayer()
		{
			if (m_naExt != null)
				return m_naExt.NAWindow.ActiveAnalysis;
			else
				return null;
		}

		private INAWindowCategory2 GetActiveCategory()
		{
			if (m_naExt != null)
				return m_naExt.NAWindow.ActiveCategory as INAWindowCategory2;
			else
				return null;
		}

		#endregion

	}
}
