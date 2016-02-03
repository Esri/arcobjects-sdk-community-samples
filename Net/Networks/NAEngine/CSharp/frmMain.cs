using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.Geodatabase;


// This is the main form of the application.

namespace NAEngine
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Splitter splitter1;

		// Context menu objects for NAWindow's context menu
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem miLoadLocations;
		private System.Windows.Forms.MenuItem miClearLocations;
		private System.Windows.Forms.MenuItem miAddItem;

		// ArcGIS Controls on the form
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;

		// Listen for context menu on NAWindow
		private IEngineNAWindowEventsEx_OnContextMenuEventHandler m_onContextMenu;
        private IEngineNetworkAnalystEnvironmentEvents_OnNetworkLayersChangedEventHandler m_OnNetworkLayersChanged;
        private IEngineNetworkAnalystEnvironmentEvents_OnCurrentNetworkLayerChangedEventHandler m_OnCurrentNetworkLayerChanged;

		// Reference to ArcGIS Network Analyst extension Environment
		private IEngineNetworkAnalystEnvironment m_naEnv;

		// Reference to NAWindow.  Need to hold on to reference for events to work.
		private IEngineNAWindow m_naWindow;

		// Menu for our commands on the TOC context menu
		private IToolbarMenu m_menuLayer;

		// incrementor for auto generated names
		private static int autogenInt = 0;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.miLoadLocations = new System.Windows.Forms.MenuItem();
			this.miClearLocations = new System.Windows.Forms.MenuItem();
			this.miAddItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// axMapControl1
			// 
			this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axMapControl1.Location = new System.Drawing.Point(227, 28);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(645, 472);
			this.axMapControl1.TabIndex = 2;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(664, 0);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 1;
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.axToolbarControl1.Location = new System.Drawing.Point(0, 0);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(872, 28);
			this.axToolbarControl1.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(224, 28);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 472);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.axTOCControl1.Location = new System.Drawing.Point(0, 28);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(224, 472);
			this.axTOCControl1.TabIndex = 1;
			this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miLoadLocations,
            this.miClearLocations});
			// 
			// miLoadLocations
			// 
			this.miLoadLocations.Index = 0;
			this.miLoadLocations.Text = "Load Locations...";
			this.miLoadLocations.Click += new System.EventHandler(this.miLoadLocations_Click);
			// 
			// miClearLocations
			// 
			this.miClearLocations.Index = 1;
			this.miClearLocations.Text = "Clear Locations";
			this.miClearLocations.Click += new System.EventHandler(this.miClearLocations_Click);
			// 
			// miAddItem
			// 
			this.miAddItem.Index = -1;
			this.miAddItem.Text = "Add Item";
			this.miAddItem.Click += new System.EventHandler(this.miAddItem_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(872, 500);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axMapControl1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.axTOCControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Name = "frmMain";
			this.Text = "Network Analyst Engine Application";
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool succeeded = ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
			if (succeeded)
			{
				ESRI.ArcGIS.RuntimeInfo activeRunTimeInfo = ESRI.ArcGIS.RuntimeManager.ActiveRuntime;
				System.Diagnostics.Debug.Print(activeRunTimeInfo.Product.ToString());

				Application.Run(new frmMain());
			}
			else
				System.Windows.Forms.MessageBox.Show("Failed to bind to an active ArcGIS runtime");
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			// Add commands to the NALayer context menu
			m_menuLayer = new ToolbarMenuClass();

			int nItem = -1;
			m_menuLayer.AddItem(new cmdLoadLocations(), -1, ++nItem, false, esriCommandStyles.esriCommandStyleTextOnly);
			m_menuLayer.AddItem(new cmdRemoveLayer(), -1, ++nItem, false, esriCommandStyles.esriCommandStyleTextOnly);
			m_menuLayer.AddItem(new cmdClearAnalysisLayer(), -1, ++nItem, true, esriCommandStyles.esriCommandStyleTextOnly);
			m_menuLayer.AddItem(new cmdNALayerProperties(), -1, ++nItem, true, esriCommandStyles.esriCommandStyleTextOnly);

			// Since this ToolbarMenu is a standalone popup menu use the SetHook method to
			//  specify the object that will be sent as a "hook" to the menu commands in their OnCreate methods.
			m_menuLayer.SetHook(axMapControl1);

			// Add command for ArcGIS Network Analyst extension env properties to end of "Network Analyst" dropdown menu
			nItem = -1;
			for (int i = 0; i < axToolbarControl1.Count; ++i)
			{
				IToolbarItem item = axToolbarControl1.GetItem(i);
				IToolbarMenu mnu = item.Menu;

				if (mnu == null) continue;

				IMenuDef mnudef = mnu.GetMenuDef();
				string name = mnudef.Name;

				// Find the ArcGIS Network Analyst extension solver menu drop down and note the index
				if (name == "ControlToolsNetworkAnalyst_SolverMenu")
				{
					nItem = i;
					//break;
				}
			}

			if (nItem >= 0)
			{
				// Using the index found above, get the solver menu drop down and add the Properties command to the end of it.
				IToolbarItem item = axToolbarControl1.GetItem(nItem);
				IToolbarMenu mnu = item.Menu;
				if (mnu != null)
					mnu.AddItem(new cmdNAProperties(), -1, mnu.Count, true, esriCommandStyles.esriCommandStyleTextOnly);

				// Since this ToolbarMenu is an item on the ToolbarControl the Hook is shared and initialized by the ToolbarControl.
				//  Therefore, SetHook is not called here, like it is for the menu above.
			}

			// Initialize naEnv variables
			m_naEnv = CommonFunctions.GetTheEngineNetworkAnalystEnvironment();
			if (m_naEnv == null)
			{
				MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured");
				return;
			}

			m_naEnv.ZoomToResultAfterSolve = false;
			m_naEnv.ShowAnalysisMessagesAfterSolve = (int)(esriEngineNAMessageType.esriEngineNAMessageTypeInformative | 
														   esriEngineNAMessageType.esriEngineNAMessageTypeWarning);

			// Set up the buddy control and initialize the NA extension, so we can get to NAWindow to listen to window events.
			// This is necessary, as the various controls are not yet set up. They need to be in order to get the NAWindow's events.
			axToolbarControl1.SetBuddyControl(axMapControl1);
			IExtension ext = m_naEnv as IExtension;
			object obj = axToolbarControl1.Object;
			
			ext.Startup(ref obj);
			
			// m_naWindow is set after Startup of the Network Analyst extension
			m_naWindow = m_naEnv.NAWindow;
			if (m_naWindow == null)
			{
				MessageBox.Show("Error: Unexpected null NAWindow");
				return;
			}

			m_onContextMenu = new IEngineNAWindowEventsEx_OnContextMenuEventHandler(OnContextMenu);
			((IEngineNAWindowEventsEx_Event)m_naWindow).OnContextMenu += m_onContextMenu;

            m_OnNetworkLayersChanged = new IEngineNetworkAnalystEnvironmentEvents_OnNetworkLayersChangedEventHandler(OnNetworkLayersChanged);
            ((IEngineNetworkAnalystEnvironmentEvents_Event)m_naEnv).OnNetworkLayersChanged += m_OnNetworkLayersChanged;

            m_OnCurrentNetworkLayerChanged = new IEngineNetworkAnalystEnvironmentEvents_OnCurrentNetworkLayerChangedEventHandler(OnCurrentNetworkLayerChanged);
            ((IEngineNetworkAnalystEnvironmentEvents_Event)m_naEnv).OnCurrentNetworkLayerChanged += m_OnCurrentNetworkLayerChanged;
		}

		//  Show the TOC context menu when an NALayer is right-clicked on
		private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
		{
			if (e.button != 2) return;

			esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
			IBasicMap map = null;
			ILayer layer = null;
			object other = null;
			object index = null;

			//Determine what kind of item has been clicked on
			axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

			// Only implemented a context menu for NALayers.  Exit if the layer is anything else.
			if ((layer as INALayer) == null)
				return;

			axTOCControl1.SelectItem(layer);

			// Set the layer into the CustomProperty.
			// This is used by the other commands to know what layer was right-clicked on
			// in the table of contents.			
			axMapControl1.CustomProperty = layer;

			//Popup the correct context menu and update the TOC when it's done.
			if (item == esriTOCControlItem.esriTOCControlItemLayer)
			{
				m_menuLayer.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
				ITOCControl toc = axTOCControl1.Object as ITOCControl;
				toc.Update();
			}
		}

        public void OnNetworkLayersChanged()
        {
            // The OnNetworkLayersChanged event is fired when a new INetworkLayer object is 
            //  added, removed, or renamed within a map.

            // If the INetworkLayer is renamed interactively through the user interface 
            //  OnNetworkLayersChanged is fired. If the INetworkLayer is renamed programmatically 
            //  using the ILayer::Name property OnNetworkLayersChanged is not fired.
        }

        public void OnCurrentNetworkLayerChanged()
        {
            // The OnCurrentNetworkLayerChanged event is fired when the user interactively 
            //  changes the NetworkDataset or the IEngineNetworkAnalystEnvironment::CurrentNetworkLayer 
            //  is set programatically.
        }

        //The OnContextMenu event is fired when a user right clicks within the 
        //  IEngineNetworkAnalystEnvironment::NAWindow and can be used to supply a context menu.		
        public bool OnContextMenu(int x, int y)
		{
			System.Drawing.Point pt = this.PointToClient(System.Windows.Forms.Cursor.Position);

			// Get the active category
			var activeCategory = m_naWindow.ActiveCategory as IEngineNAWindowCategory2;
			if (activeCategory == null)
				return false;

			MenuItem separator = new MenuItem("-");

			miLoadLocations.Enabled = false;
			miClearLocations.Enabled = false;

			// in order for the AddItem choice to appear in the context menu, the class
			// should be an input class, and it should not be editable
			INAClassDefinition pNAClassDefinition = activeCategory.NAClass.ClassDefinition;
			if (pNAClassDefinition.IsInput)
			{

				miLoadLocations.Enabled = true;
				miClearLocations.Enabled = true;

				// canEditShape should be false for AddItem to Apply (default is false)
				// if it's a StandaloneTable canEditShape is implicitly false (there's no shape to edit)
				bool canEditShape = false;
				IFields pFields = pNAClassDefinition.Fields;
				int nField = -1;
				nField = pFields.FindField("Shape");
				if (nField >= 0)
				{
					int naFieldType = 0;
					naFieldType = pNAClassDefinition.get_FieldType("Shape");

					// determining whether or not the shape field can be edited consists of running a bitwise comparison
					// on the FieldType of the shape field.  See the online help for a list of the possible field types.
					// For our case, we want to verify that the shape field is an input field.  If it is an input field, 
					// then we do NOT want to display the Add Item menu option.
					canEditShape = ((naFieldType & (int)esriNAFieldType.esriNAFieldTypeInput) == (int)esriNAFieldType.esriNAFieldTypeInput) ? true : false;
				}

				if (!canEditShape)
				{
					contextMenu1.MenuItems.Add(separator);
					contextMenu1.MenuItems.Add(miAddItem);
				}
			}

			contextMenu1.Show(this, pt);

			// even if the miAddItem menu item has not been added, Remove() won't crash.
			contextMenu1.MenuItems.Remove(separator);
			contextMenu1.MenuItems.Remove(miAddItem);

			return true;
		}

		private void miLoadLocations_Click(object sender, System.EventArgs e)
		{
			var mapControl = axMapControl1.Object as IMapControl3;

			// Show the Property Page form for ArcGIS Network Analyst extension
			var loadLocations = new frmLoadLocations();
			if (loadLocations.ShowModal(mapControl, m_naEnv))
			{
				// notify that the context has changed because we have added locations to a NAClass within it
				var contextEdit = m_naEnv.NAWindow.ActiveAnalysis.Context as INAContextEdit;
				contextEdit.ContextChanged();

				// If loaded locations, refresh the NAWindow and the Screen
				INALayer naLayer = m_naWindow.ActiveAnalysis;
				mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent);
				m_naWindow.UpdateContent(m_naWindow.ActiveCategory);
			}
		}

		private void miClearLocations_Click(object sender, System.EventArgs e)
		{
			var mapControl = axMapControl1.Object as IMapControl3;

			var naHelper = m_naEnv as IEngineNetworkAnalystHelper;
			IEngineNAWindow naWindow = m_naWindow;
			INALayer naLayer = naWindow.ActiveAnalysis;

			// we do not have to run ContextChanged() as with adding an item and loading locations,
			// because that is done by the DeleteAllNetworkLocations method.
			naHelper.DeleteAllNetworkLocations();

			mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent);
		}

		private void miAddItem_Click(object sender, System.EventArgs e)
		{
			// Developers Note:
			// Once an item has been added, the user can double click on the item to edit the properties
			// of the item.  For the purposes of this sample, only the default values from the InitDefaultValues method
			// and an auto generated Name value are populated initially for the new item.

			var mapControl = axMapControl1.Object as IMapControl3;

			var activeCategory = m_naWindow.ActiveCategory as IEngineNAWindowCategory2;
			IDataLayer pDataLayer = activeCategory.DataLayer;

			// In order to add an item, we need to create a new row in the class and populate it 
			// with the initial default values for that class.
			var table = pDataLayer as ITable;
			IRow row = table.CreateRow();
			var rowSubtypes = row as IRowSubtypes;
			rowSubtypes.InitDefaultValues();

			// we need to auto generate a display name for the newly added item.
			// In some cases (depending on how the schema is set up) InitDefaultValues may result in a nonempty name string 
			// in these cases do not override the preexisting non-empty name string with our auto generated one.
			var ipFeatureLayer = activeCategory.Layer as IFeatureLayer;
			var ipStandaloneTable = pDataLayer as IStandaloneTable;
			string name = "";
			if (ipFeatureLayer != null)
				name = ipFeatureLayer.DisplayField;
			else if (ipStandaloneTable != null)
				name = ipStandaloneTable.DisplayField;

			//If the display field is an empty string or does not represent an actual field on the NAClass just skip the auto generation.  
			// (Some custom solvers may not have set the DisplayField for example).
			// Note:  The name we are auto generating does not have any spaces in it.  This is to ensure that any classes 
			// that are space sensitive will be able to handle the name (ex Specialties).
			string currentName = "";
			int fieldIndex = row.Fields.FindField(name);
			if (fieldIndex >= 0)
			{
				currentName = row.get_Value(fieldIndex) as string;
				if (currentName.Length <= 0)
					row.set_Value(fieldIndex, "Item" + ++autogenInt);
			}

			// A special case is OrderPairs NAClass because that effectively has a combined 2 field display field.  
			// You will have to hard code to look for that NAClassName and create a default name for 
			// both first order and second order field names so the name will display correctly 
			// (look for the NAClass Name and NOT the layer name).
			INAClassDefinition naClassDef = activeCategory.NAClass.ClassDefinition;
			if (naClassDef.Name == "OrderPairs")
			{
				fieldIndex = row.Fields.FindField("SecondOrderName");
				if (fieldIndex >= 0)
				{
					string secondName = row.get_Value(fieldIndex) as string;
					if (secondName.Length <= 0)
						row.set_Value(fieldIndex, "Item" + ++autogenInt);
				}
			}

			row.Store();

			// notify that the context has changed because we have added an item to a NAClass within it
			var contextEdit = m_naEnv.NAWindow.ActiveAnalysis.Context as INAContextEdit;
			contextEdit.ContextChanged();

			// refresh the NAWindow and the Screen
			INALayer naLayer = m_naWindow.ActiveAnalysis;
			mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, mapControl.Extent);
			m_naWindow.UpdateContent(m_naWindow.ActiveCategory);
		}
	}
}
