using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;
using System.Windows.Forms;
using System.Collections.Generic;

// This form allows users to load locations from another point feature layer into the selected NALayer and active category.
namespace NAEngine
{
	public class frmLoadLocations : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblInputData;
		private System.Windows.Forms.CheckBox chkUseSelection;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox cboInputData;

		bool m_okClicked;
		System.Collections.IList m_listDisplayTable;

		public frmLoadLocations()
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
			this.cboInputData = new System.Windows.Forms.ComboBox();
			this.lblInputData = new System.Windows.Forms.Label();
			this.chkUseSelection = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboInputData
			// 
			this.cboInputData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboInputData.Location = new System.Drawing.Point(82, 9);
			this.cboInputData.Name = "cboInputData";
			this.cboInputData.Size = new System.Drawing.Size(381, 21);
			this.cboInputData.TabIndex = 0;
			this.cboInputData.SelectedIndexChanged += new System.EventHandler(this.cboInputData_SelectedIndexChanged);
			// 
			// lblInputData
			// 
			this.lblInputData.Location = new System.Drawing.Point(12, 12);
			this.lblInputData.Name = "lblInputData";
			this.lblInputData.Size = new System.Drawing.Size(64, 24);
			this.lblInputData.TabIndex = 1;
			this.lblInputData.Text = "Input Data";
			// 
			// chkUseSelection
			// 
			this.chkUseSelection.Location = new System.Drawing.Point(15, 39);
			this.chkUseSelection.Name = "chkUseSelection";
			this.chkUseSelection.Size = new System.Drawing.Size(419, 16);
			this.chkUseSelection.TabIndex = 2;
			this.chkUseSelection.Text = "Use Selection";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(351, 61);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 32);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(223, 61);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 32);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// frmLoadLocations
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(479, 100);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkUseSelection);
			this.Controls.Add(this.lblInputData);
			this.Controls.Add(this.cboInputData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmLoadLocations";
			this.ShowInTaskbar = false;
			this.Text = "Load Locations";
			this.ResumeLayout(false);

		}
		#endregion

		public bool ShowModal(IMapControl3 mapControl, IEngineNetworkAnalystEnvironment naEnv)
		{
			// Initialize variables
			m_okClicked = false;
			m_listDisplayTable = new System.Collections.ArrayList();

			var activeCategory = naEnv.NAWindow.ActiveCategory as IEngineNAWindowCategory2;
			if (activeCategory == null)
				return false;

			IDataLayer dataLayer = activeCategory.DataLayer;
			if (dataLayer == null)
				return false;

			// Set up the title of this dialog
			String dataLayerName = GetDataLayerName(dataLayer);
			if (dataLayerName.Length == 0)
				return false;

			this.Text = "Load Items into " + dataLayerName;

			// Make sure the combo box lists only the appropriate possible input data layers
			PopulateInputDataComboBox(mapControl, dataLayer);

			//Select the first display table from the list
			if (cboInputData.Items.Count > 0)
				cboInputData.SelectedIndex = 0;

			// Show the window
			this.ShowDialog();

			// If we selected a layer and clicked OK, load the locations
			if (m_okClicked && (cboInputData.SelectedIndex >= 0))
			{
				try
				{
					// Get a cursor on the source display table (either though the selection set or table)
					// Use IDisplayTable because it accounts for joins, querydefs, etc.
					// IDisplayTable is implemented by FeatureLayers and StandaloneTables.
					//
					IDisplayTable displayTable = m_listDisplayTable[cboInputData.SelectedIndex] as IDisplayTable;
					ICursor cursor;
					if (chkUseSelection.Checked)
					{
						ISelectionSet selSet;
						selSet = displayTable.DisplaySelectionSet;
						selSet.Search(null, false, out cursor);
					}
					else
						cursor = displayTable.SearchDisplayTable(null, false);

					// Get the NAContext from the active analysis layer
					INAContext naContext = naEnv.NAWindow.ActiveAnalysis.Context;

					// Get the dataset for the active NAClass  
					IDataset naDataset = activeCategory.NAClass as IDataset;

					// Setup NAClassLoader and Load Locations
					INAClassLoader2 naClassLoader = new NAClassLoader() as INAClassLoader2;
					naClassLoader.Initialize(naContext, naDataset.Name, cursor);

					// Avoid loading network locations onto non-traversable portions of elements
					INALocator3 locator = naContext.Locator as INALocator3;
					locator.ExcludeRestrictedElements = true;
					locator.CacheRestrictedElements(naContext);

					int rowsIn = 0;
					int rowsLocated = 0;
					naClassLoader.Load(cursor, null, ref rowsIn, ref rowsLocated);

					// Let the user know if some of the rows failed to locate
					if (rowsIn != rowsLocated)
						MessageBox.Show("Out of " + rowsIn + " + rows, " + rowsLocated + " rows were located", 
										"Loading locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Loading locations failure", MessageBoxButtons.OK, MessageBoxIcon.Error );					
				}

				return true;
			}

			return false;
		}

		private String GetDataLayerName(IDataLayer dataLayer)
		{
			var pFeatureLayer = dataLayer as IFeatureLayer;
			var pStandaloneTable = dataLayer as IStandaloneTable;
			var pLayer = dataLayer as ILayer;

			if (pFeatureLayer != null && pLayer.Valid)
				return pLayer.Name;
			else if (pStandaloneTable != null)
				return pStandaloneTable.Name;

			return "";
		}

		private esriGeometryType GetDataLayerGeometryType(IDataLayer dataLayer)
		{
			var pFeatureLayer = dataLayer as IFeatureLayer;
			var pLayer = dataLayer as ILayer;
			if (pFeatureLayer != null && pLayer.Valid)
			{
				IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
				if (pFeatureClass != null)
					return pFeatureClass.ShapeType;
			}

			return esriGeometryType.esriGeometryNull;
		}

		private void PopulateInputDataComboBox(IMapControl3 mapControl, IDataLayer dataLayer)
		{
			esriGeometryType targetGeoType = GetDataLayerGeometryType(dataLayer);

			IEnumLayer sourceLayers = null;
			ILayer sourceLayer = null;
			IDisplayTable sourceDisplayTable = null;
			UID searchInterfaceUID = new UID();

			if (targetGeoType != esriGeometryType.esriGeometryNull)
			{
				// Only include layers that are of type IFeatureLayer
				searchInterfaceUID.Value = typeof(IFeatureLayer).GUID.ToString("B");
				sourceLayers = mapControl.Map.get_Layers(searchInterfaceUID, true);

				// iterate over all of the feature layers
				sourceLayer = sourceLayers.Next();
				while (sourceLayer != null)
				{
					// Verify that the layer is a feature layer and a display table
					IFeatureLayer sourceFeatureLayer = sourceLayer as IFeatureLayer;
					sourceDisplayTable = sourceLayer as IDisplayTable;
					if ((sourceFeatureLayer != null) && (sourceDisplayTable != null))
					{
						// Make sure that the geometry of the feature layer matches the geometry
						//   of the class into which we are loading
						IFeatureClass sourceFeatureClass = sourceFeatureLayer.FeatureClass;
						esriGeometryType sourceGeoType = sourceFeatureClass.ShapeType;
						if ((sourceGeoType == targetGeoType) ||
						   (targetGeoType == esriGeometryType.esriGeometryPoint && sourceGeoType == esriGeometryType.esriGeometryMultipoint))
						{
							// Add the layer name to the combobox and the layer to the list
							cboInputData.Items.Add(sourceLayer.Name);
							m_listDisplayTable.Add(sourceDisplayTable);
						}
					}

					sourceLayer = sourceLayers.Next();
				}
			}
			// The layer being loaded into has no geometry type
			else
			{
				// Find all of the standalone table that are not part of an NALayer
				IStandaloneTableCollection sourceStandaloneTables = mapControl.Map as IStandaloneTableCollection;
				IStandaloneTable sourceStandaloneTable = null;
				sourceDisplayTable = null;

				int count = 0;
				if (sourceStandaloneTables != null)
					count = sourceStandaloneTables.StandaloneTableCount;

				for (int i = 0; i < count; ++i)
				{
					sourceStandaloneTable = sourceStandaloneTables.get_StandaloneTable(i);
					sourceDisplayTable = sourceStandaloneTable as IDisplayTable;

					if ((sourceStandaloneTable != null) && (sourceDisplayTable != null))
					{
						// Add the table name to the combobox and the layer to the list
						cboInputData.Items.Add(sourceStandaloneTable.Name);
						m_listDisplayTable.Add(sourceDisplayTable);
					}
				}

				// Find all of the standalone tables that are part of an NALayer
				searchInterfaceUID.Value = typeof(INALayer).GUID.ToString("B");
				sourceLayers = mapControl.Map.get_Layers(searchInterfaceUID, true);

				sourceLayer = sourceLayers.Next();
				while (sourceLayer != null)
				{
					INALayer sourceNALayer = sourceLayer as INALayer;
					if (sourceNALayer != null)
					{
						sourceStandaloneTables = sourceNALayer as IStandaloneTableCollection;
						sourceStandaloneTable = null;
						sourceDisplayTable = null;

						count = 0;
						if (sourceStandaloneTables != null)
							count = sourceStandaloneTables.StandaloneTableCount;

						for (int i = 0; i < count; ++i)
						{
							sourceStandaloneTable = sourceStandaloneTables.get_StandaloneTable(i);
							sourceDisplayTable = sourceStandaloneTable as IDisplayTable;

							if ((sourceStandaloneTable != null) && (sourceDisplayTable != null))
							{
								// Add the table name to the combobox and the layer to the list
								cboInputData.Items.Add(sourceStandaloneTable.Name);
								m_listDisplayTable.Add(sourceDisplayTable);
							}
						}
					}

					sourceLayer = sourceLayers.Next();
				}
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			m_okClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_okClicked = false;
			this.Close();
		}

		private void cboInputData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Set the chkUseSelectedFeatures control based on if anything is selected or not
			if (cboInputData.SelectedIndex >= 0)
			{
				IDisplayTable displayTable = m_listDisplayTable[cboInputData.SelectedIndex] as IDisplayTable;
				chkUseSelection.Checked = (displayTable.DisplaySelectionSet.Count > 0);
				chkUseSelection.Enabled = (displayTable.DisplaySelectionSet.Count > 0);
			}
		}
	}
}
