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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.PublisherControls;

namespace AttributeQuery
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AttributeQuery : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button cmdMeetZoomTo;
		private System.Windows.Forms.Button cmdMeetCenterAt;
		private System.Windows.Forms.Button cmdMeetFlash;
		private System.Windows.Forms.Button cmdFailZoomTo;
		private System.Windows.Forms.Button cmdFailCenterAt;
		private System.Windows.Forms.Button cmdFailFlash;
		private System.Windows.Forms.Button cmdOpen;
		private System.Windows.Forms.RadioButton optZoomIn;
		private System.Windows.Forms.RadioButton optZoomOut;
		private System.Windows.Forms.RadioButton optPan;
		private System.Windows.Forms.Button cmdFullExtent;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.GroupBox grpBox;
		private System.Windows.Forms.RadioButton optString;
		private System.Windows.Forms.RadioButton optNumber;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.ComboBox cboFields;
		private System.Windows.Forms.ComboBox cboLayers;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Label lblMeets;
		private System.Windows.Forms.Label lblFails;
		private System.Windows.Forms.Button cmdQuery; 

		private System.Windows.Forms.Label lblLayerToQuery;
		private System.Windows.Forms.Label lblFieldType;
		private System.Windows.Forms.Label lblField;
		private System.Windows.Forms.Label lblOperator;
		private System.Windows.Forms.Label lblValue;


		private System.Collections.Hashtable m_LayersIndex;
		private ARFeatureSet m_arFeatureSetMeets;
		private ARFeatureSet m_arFeatureSetFails;
		private m_InverseOperators[] InverseOperator = new m_InverseOperators[7];
			private ESRI.ArcGIS.PublisherControls.AxArcReaderControl axArcReaderControl1;
            private DataGridView dataGridView1;
            private DataGridView dataGridView2;

		private System.ComponentModel.Container components = null;
		struct m_InverseOperators
		{
			public string input;
			public string inverse;
		}
		public AttributeQuery()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.cmdMeetZoomTo = new System.Windows.Forms.Button();
            this.cmdMeetCenterAt = new System.Windows.Forms.Button();
            this.cmdMeetFlash = new System.Windows.Forms.Button();
            this.cmdFailZoomTo = new System.Windows.Forms.Button();
            this.cmdFailCenterAt = new System.Windows.Forms.Button();
            this.cmdFailFlash = new System.Windows.Forms.Button();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.optZoomIn = new System.Windows.Forms.RadioButton();
            this.optZoomOut = new System.Windows.Forms.RadioButton();
            this.optPan = new System.Windows.Forms.RadioButton();
            this.cmdFullExtent = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblLayerToQuery = new System.Windows.Forms.Label();
            this.cmdQuery = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboLayers = new System.Windows.Forms.ComboBox();
            this.optString = new System.Windows.Forms.RadioButton();
            this.optNumber = new System.Windows.Forms.RadioButton();
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.cboFields = new System.Windows.Forms.ComboBox();
            this.lblMeets = new System.Windows.Forms.Label();
            this.lblFails = new System.Windows.Forms.Label();
            this.axArcReaderControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdMeetZoomTo
            // 
            this.cmdMeetZoomTo.Location = new System.Drawing.Point(8, 488);
            this.cmdMeetZoomTo.Name = "cmdMeetZoomTo";
            this.cmdMeetZoomTo.Size = new System.Drawing.Size(112, 24);
            this.cmdMeetZoomTo.TabIndex = 3;
            this.cmdMeetZoomTo.Text = "Zoom To";
            this.cmdMeetZoomTo.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdMeetCenterAt
            // 
            this.cmdMeetCenterAt.Location = new System.Drawing.Point(128, 488);
            this.cmdMeetCenterAt.Name = "cmdMeetCenterAt";
            this.cmdMeetCenterAt.Size = new System.Drawing.Size(112, 24);
            this.cmdMeetCenterAt.TabIndex = 4;
            this.cmdMeetCenterAt.Text = "Center At";
            this.cmdMeetCenterAt.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdMeetFlash
            // 
            this.cmdMeetFlash.Location = new System.Drawing.Point(248, 488);
            this.cmdMeetFlash.Name = "cmdMeetFlash";
            this.cmdMeetFlash.Size = new System.Drawing.Size(104, 24);
            this.cmdMeetFlash.TabIndex = 5;
            this.cmdMeetFlash.Text = "Flash";
            this.cmdMeetFlash.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdFailZoomTo
            // 
            this.cmdFailZoomTo.Location = new System.Drawing.Point(360, 488);
            this.cmdFailZoomTo.Name = "cmdFailZoomTo";
            this.cmdFailZoomTo.Size = new System.Drawing.Size(112, 24);
            this.cmdFailZoomTo.TabIndex = 6;
            this.cmdFailZoomTo.Text = "Zoom To";
            this.cmdFailZoomTo.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdFailCenterAt
            // 
            this.cmdFailCenterAt.Location = new System.Drawing.Point(480, 488);
            this.cmdFailCenterAt.Name = "cmdFailCenterAt";
            this.cmdFailCenterAt.Size = new System.Drawing.Size(112, 24);
            this.cmdFailCenterAt.TabIndex = 7;
            this.cmdFailCenterAt.Text = "Center At";
            this.cmdFailCenterAt.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdFailFlash
            // 
            this.cmdFailFlash.Location = new System.Drawing.Point(600, 488);
            this.cmdFailFlash.Name = "cmdFailFlash";
            this.cmdFailFlash.Size = new System.Drawing.Size(104, 24);
            this.cmdFailFlash.TabIndex = 8;
            this.cmdFailFlash.Text = "Flash";
            this.cmdFailFlash.Click += new System.EventHandler(this.MixedDisplayResults_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Location = new System.Drawing.Point(8, 8);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(72, 24);
            this.cmdOpen.TabIndex = 9;
            this.cmdOpen.Text = "Open";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // optZoomIn
            // 
            this.optZoomIn.Appearance = System.Windows.Forms.Appearance.Button;
            this.optZoomIn.Location = new System.Drawing.Point(80, 8);
            this.optZoomIn.Name = "optZoomIn";
            this.optZoomIn.Size = new System.Drawing.Size(72, 24);
            this.optZoomIn.TabIndex = 10;
            this.optZoomIn.Text = "Zoom In";
            this.optZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optZoomIn.Click += new System.EventHandler(this.MixedControls_Click);
            // 
            // optZoomOut
            // 
            this.optZoomOut.Appearance = System.Windows.Forms.Appearance.Button;
            this.optZoomOut.Location = new System.Drawing.Point(152, 8);
            this.optZoomOut.Name = "optZoomOut";
            this.optZoomOut.Size = new System.Drawing.Size(72, 24);
            this.optZoomOut.TabIndex = 11;
            this.optZoomOut.Text = "Zoom Out";
            this.optZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optZoomOut.Click += new System.EventHandler(this.MixedControls_Click);
            // 
            // optPan
            // 
            this.optPan.Appearance = System.Windows.Forms.Appearance.Button;
            this.optPan.Location = new System.Drawing.Point(296, 8);
            this.optPan.Name = "optPan";
            this.optPan.Size = new System.Drawing.Size(72, 24);
            this.optPan.TabIndex = 13;
            this.optPan.Text = "Pan";
            this.optPan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optPan.Click += new System.EventHandler(this.MixedControls_Click);
            // 
            // cmdFullExtent
            // 
            this.cmdFullExtent.Location = new System.Drawing.Point(224, 8);
            this.cmdFullExtent.Name = "cmdFullExtent";
            this.cmdFullExtent.Size = new System.Drawing.Size(72, 24);
            this.cmdFullExtent.TabIndex = 14;
            this.cmdFullExtent.Text = "Full Extent";
            this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.lblValue);
            this.grpBox.Controls.Add(this.lblOperator);
            this.grpBox.Controls.Add(this.lblField);
            this.grpBox.Controls.Add(this.lblFieldType);
            this.grpBox.Controls.Add(this.lblLayerToQuery);
            this.grpBox.Controls.Add(this.cmdQuery);
            this.grpBox.Controls.Add(this.txtValue);
            this.grpBox.Controls.Add(this.cboLayers);
            this.grpBox.Controls.Add(this.optString);
            this.grpBox.Controls.Add(this.optNumber);
            this.grpBox.Controls.Add(this.cboOperator);
            this.grpBox.Controls.Add(this.cboFields);
            this.grpBox.Location = new System.Drawing.Point(560, 40);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(144, 312);
            this.grpBox.TabIndex = 20;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "Query Criteria";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(8, 224);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(120, 16);
            this.lblValue.TabIndex = 31;
            this.lblValue.Text = "Value:";
            // 
            // lblOperator
            // 
            this.lblOperator.Location = new System.Drawing.Point(8, 176);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(88, 16);
            this.lblOperator.TabIndex = 30;
            this.lblOperator.Text = "Operator:";
            // 
            // lblField
            // 
            this.lblField.Location = new System.Drawing.Point(8, 128);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(96, 16);
            this.lblField.TabIndex = 29;
            this.lblField.Text = "Field to Query:";
            // 
            // lblFieldType
            // 
            this.lblFieldType.Location = new System.Drawing.Point(8, 80);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(104, 16);
            this.lblFieldType.TabIndex = 28;
            this.lblFieldType.Text = "Field Type:";
            // 
            // lblLayerToQuery
            // 
            this.lblLayerToQuery.Location = new System.Drawing.Point(8, 24);
            this.lblLayerToQuery.Name = "lblLayerToQuery";
            this.lblLayerToQuery.Size = new System.Drawing.Size(120, 16);
            this.lblLayerToQuery.TabIndex = 27;
            this.lblLayerToQuery.Text = "Layer to Query:";
            // 
            // cmdQuery
            // 
            this.cmdQuery.Location = new System.Drawing.Point(24, 272);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Size = new System.Drawing.Size(104, 32);
            this.cmdQuery.TabIndex = 26;
            this.cmdQuery.Text = "Query";
            this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(8, 240);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(120, 20);
            this.txtValue.TabIndex = 25;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // cboLayers
            // 
            this.cboLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayers.Location = new System.Drawing.Point(8, 40);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Size = new System.Drawing.Size(120, 21);
            this.cboLayers.TabIndex = 24;
            this.cboLayers.SelectedIndexChanged += new System.EventHandler(this.cboLayers_SelectedIndexChanged);
            // 
            // optString
            // 
            this.optString.Location = new System.Drawing.Point(80, 96);
            this.optString.Name = "optString";
            this.optString.Size = new System.Drawing.Size(56, 16);
            this.optString.TabIndex = 23;
            this.optString.Text = "String";
            this.optString.Click += new System.EventHandler(this.DataType_Click);
            // 
            // optNumber
            // 
            this.optNumber.Location = new System.Drawing.Point(8, 96);
            this.optNumber.Name = "optNumber";
            this.optNumber.Size = new System.Drawing.Size(64, 16);
            this.optNumber.TabIndex = 22;
            this.optNumber.Text = "Number";
            this.optNumber.Click += new System.EventHandler(this.DataType_Click);
            // 
            // cboOperator
            // 
            this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperator.Location = new System.Drawing.Point(8, 192);
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Size = new System.Drawing.Size(120, 21);
            this.cboOperator.TabIndex = 21;
            // 
            // cboFields
            // 
            this.cboFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFields.Location = new System.Drawing.Point(8, 144);
            this.cboFields.Name = "cboFields";
            this.cboFields.Size = new System.Drawing.Size(120, 21);
            this.cboFields.TabIndex = 20;
            // 
            // lblMeets
            // 
            this.lblMeets.Location = new System.Drawing.Point(8, 360);
            this.lblMeets.Name = "lblMeets";
            this.lblMeets.Size = new System.Drawing.Size(344, 24);
            this.lblMeets.TabIndex = 21;
            // 
            // lblFails
            // 
            this.lblFails.Location = new System.Drawing.Point(368, 360);
            this.lblFails.Name = "lblFails";
            this.lblFails.Size = new System.Drawing.Size(336, 24);
            this.lblFails.TabIndex = 22;
            // 
            // axArcReaderControl1
            // 
            this.axArcReaderControl1.Location = new System.Drawing.Point(8, 40);
            this.axArcReaderControl1.Name = "axArcReaderControl1";
            this.axArcReaderControl1.Size = new System.Drawing.Size(544, 312);
            this.axArcReaderControl1.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 384);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(344, 98);
            this.dataGridView1.TabIndex = 24;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(360, 384);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(344, 98);
            this.dataGridView2.TabIndex = 25;
            // 
            // AttributeQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(712, 517);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.axArcReaderControl1);
            this.Controls.Add(this.lblFails);
            this.Controls.Add(this.lblMeets);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.cmdFullExtent);
            this.Controls.Add(this.optPan);
            this.Controls.Add(this.optZoomOut);
            this.Controls.Add(this.optZoomIn);
            this.Controls.Add(this.cmdOpen);
            this.Controls.Add(this.cmdFailFlash);
            this.Controls.Add(this.cmdFailCenterAt);
            this.Controls.Add(this.cmdFailZoomTo);
            this.Controls.Add(this.cmdMeetFlash);
            this.Controls.Add(this.cmdMeetCenterAt);
            this.Controls.Add(this.cmdMeetZoomTo);
            this.Name = "AttributeQuery";
            this.Text = "AttributeQuery (LesserThan / GreaterThan) ";
            this.Load += new System.EventHandler(this.AttributeQuery_Load);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.ArcReader))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            Application.Run(new AttributeQuery());
		}

		private void AttributeQuery_Load(object sender, System.EventArgs e)
		{
			//Disable Search Tools
			EnableSearchTools(false);
			EnableMapTools(false);
			EnableMeetHighlightTools(false);
			EnableFailHighlightTools(false);

			//Populate Inverse Operators array
			PopulateInverseOperators();

			optNumber.Checked=true;
		}	

		private void MixedControls_Click(object sender, System.EventArgs e)
		{
			//Added handler in InitializeComponent() for OptionButtons

			RadioButton b = (RadioButton) sender;
			//Set current tool
			switch (b.Name)
			{
				case "optZoomIn":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn;
					break;
				case "optZoomOut":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut;
					break;
				case "optPan":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan;
					break;
			}
		}
		private void MixedDisplayResults_Click(object sender, System.EventArgs e)
		{
			//Added handler in InitializeComponent() for OptionButtons

			Button b = (Button) sender;
			//Set current tool
			switch (b.Name)
			{
				case "cmdMeetZoomTo":
					m_arFeatureSetMeets.ZoomTo();
					break;
				case "cmdMeetCenterAt":
					m_arFeatureSetMeets.CenterAt();
					break;
				case "cmdMeetFlash":
					m_arFeatureSetMeets.Flash();
					break;
				case "cmdFailZoomTo":
					m_arFeatureSetFails.ZoomTo();
					break;
				case "cmdFailCenterAt":
					m_arFeatureSetFails.CenterAt();
					break;
				case "cmdFailFlash":
					m_arFeatureSetFails.Flash();
					break;
			}
		}
		private void DataType_Click(object sender, System.EventArgs e)
		{
			//Added handler in InitializeComponent() for OptionButtons
				
			RadioButton b = (RadioButton) sender;
			//Set current tool
			switch (b.Name)
			{
				case "optNumber":
					PopulateFields(false);
					PopulateOperators(false);
					break;
				case "optString":
					PopulateFields(true);
					PopulateOperators(true);
					break;
			}
		}
		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			axArcReaderControl1.ARPageLayout.FocusARMap.ZoomToFullExtent();
		}
		private void cmdOpen_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Select Published Map Document";
			openFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf";
			openFileDialog1.ShowDialog();

			//Exit if no map document is selected
			string sFilePath = openFileDialog1.FileName;
			if (sFilePath == "") return;

			//Load the specified pmf
			if (axArcReaderControl1.CheckDocument(sFilePath) == true)
			{
				axArcReaderControl1.LoadDocument(sFilePath,"");
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
				return;
			}

			 //Disable search  & map tools
			cboLayers.Items.Clear();
			cboFields.Items.Clear();
			EnableSearchTools (false);
			EnableMeetHighlightTools (false);
			EnableFailHighlightTools (false);
			
			//Determine whether permission to search layers and query field values
			bool bqueryFeatures = axArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryFeatures);
			bool bqueryValues = axArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryValues);

			if (bqueryFeatures==false || bqueryValues==false)
			{
				System.Windows.Forms.MessageBox.Show("The selected Document does not have Query Permissions.");
				return;
			}

			//Add map layers to combo and store in HashTable with combo index
			m_LayersIndex = new Hashtable();
			ARPopulateComboWithMapLayers(cboLayers, m_LayersIndex);

			//Select first searchable layer
			for(int i=0;  i <= cboLayers.Items.Count-1; i++)
			{
				ARLayer arLayer = (ARLayer)m_LayersIndex[i];
				if (arLayer.Searchable==true)
				{
					cboLayers.SelectedIndex=i;
					break;
				}
			}

			//Enable Search & Map Tools
			EnableSearchTools(true);
			EnableMapTools(true);
		}
		private void ARPopulateComboWithMapLayers(ComboBox Layers, System.Collections.Hashtable LayersIndex)
		{
			//In case cboLayers is already populated
			Layers.Items.Clear();
			LayersIndex.Clear();

			ARLayer arLayer;
			ARLayer arGroupLayer;
			
			// Get the focus map
			ARMap arMap = axArcReaderControl1.ARPageLayout.FocusARMap;

			// Loop through each layer in the focus map
			for (int i=0; i <= arMap.ARLayerCount-1; i++)
			{
				// Get the layer name and add to combo
				arLayer = arMap.get_ARLayer(i);
				if (arLayer.IsGroupLayer == true)
				{
					//If a GroupLayer add the ARChildLayers to the combo and HashTable
					for (int g=0; g <= arLayer.ARLayerCount-1; g++)
					{
						arGroupLayer = arMap.get_ARLayer(i).get_ChildARLayer(g);
						Layers.Items.Add(arGroupLayer.Name);
						LayersIndex.Add(Layers.Items.Count-1,arGroupLayer);
					}
				}
				else if (arLayer.Searchable==true)
				{
					Layers.Items.Add(arLayer.Name);
					LayersIndex.Add(Layers.Items.Count-1,arLayer);
				}
			}
		}
		private void PopulateFields(bool bIsStringField)
		{
			try 
			{
				// Clear all items in fields combo
				cboFields.Items.Clear();
				ARLayer arLayer = (ARLayer)m_LayersIndex[cboLayers.SelectedIndex];
				ArcReaderSearchDef arSearchDef = new ArcReaderSearchDefClass();
				ARFeatureCursor arFeatureCursor = arLayer.SearchARFeatures(arSearchDef);
			
				// Get the first feature in order to access the field names
				ARFeature arFeature = arFeatureCursor.NextARFeature();
        
				// Loop through fields and add field names to combo
				int i;
				i = 0;
				while (i <  arFeature.FieldCount)
				{
					if (bIsStringField ==  true)
					{
						if (arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeString)
						{
							cboFields.Items.Add(arFeature.get_FieldName(i));
						}
					}
					else
					{
						if ((arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeDouble) ||  (arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeInteger) ||  (arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeSingle) ||  (arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeSmallInteger) ||  (arFeature.get_FieldType(i) ==  esriARFieldType.esriARFieldTypeOID))
						{
							cboFields.Items.Add(arFeature.get_FieldName(i));
						}
					}
				
					i = i+ 1;

					if(cboFields.Items.Count != 0)
					{
						cboFields.SelectedIndex=0;
					}
				};
			}
			catch 
			{
				MessageBox.Show("An error occurred populating the Field ComboBox.");
			}
		}
		private void PopulateOperators(bool bIsStringField)
		{
			// Clear any current values from combo
			cboOperator.Items.Clear();

			if (bIsStringField ==  true)
			{
				cboOperator.Items.Insert(0, "=");
				cboOperator.Items.Insert(1, "<>");
			}
			else
			{
				cboOperator.Items.Insert(0, "=");
				cboOperator.Items.Insert(1, "<>");
				cboOperator.Items.Insert(2, ">");
				cboOperator.Items.Insert(3, ">=");
				cboOperator.Items.Insert(4, "<=");
				cboOperator.Items.Insert(5, "<");
			}

			cboOperator.SelectedIndex = 0;

		}
		private void PopulateInverseOperators()
		{
			InverseOperator[0].input = "=";
			InverseOperator[0].inverse = "<>";
			InverseOperator[1].input = "<>";
			InverseOperator[1].inverse = "=";
			InverseOperator[2].input = ">";
			InverseOperator[2].inverse = "<=";
			InverseOperator[3].input = ">=";
			InverseOperator[3].inverse = "<";
			InverseOperator[4].input = "<=";
			InverseOperator[4].inverse = ">";
			InverseOperator[5].input = "<";
			InverseOperator[5].inverse = ">=";
		}
		private void EnableSearchTools(bool EnabledState)
		{	
			txtValue.Text = "";
			optNumber.Enabled = EnabledState;
			optString.Enabled = EnabledState;
			cboFields.Enabled = EnabledState;
			cboOperator.Enabled = EnabledState;
			txtValue.Enabled = EnabledState;
			cmdQuery.Enabled = EnabledState;
		}
		private void EnableMapTools(bool EnabledState)
		{	
			optZoomIn.Enabled = EnabledState;
			optZoomOut.Enabled = EnabledState;
			optPan.Enabled = EnabledState;
			cmdFullExtent.Enabled = EnabledState;
		}
		private void EnableMeetHighlightTools(bool EnabledState)
		{	
			cmdMeetFlash.Enabled = EnabledState;
			cmdMeetZoomTo.Enabled = EnabledState;
			cmdMeetCenterAt.Enabled = EnabledState;
		}
		private void EnableFailHighlightTools(bool EnabledState)
		{	
			cmdFailFlash.Enabled = EnabledState;
			cmdFailZoomTo.Enabled = EnabledState;
			cmdFailCenterAt.Enabled = EnabledState;
		}
		private void cboLayers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ARLayer arLayer = (ARLayer)m_LayersIndex[cboLayers.SelectedIndex];
			//Check if layer can be searched
			if (arLayer.Searchable)
			{
				EnableSearchTools(true);
				PopulateFields(optString.Checked);
				PopulateOperators(optString.Checked);
			}
			else
			{
				MessageBox.Show("The Layer you have selected is not Searchable.");
				EnableSearchTools(false);
			}

			//Clear Grids, Labels and disable display tools
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();			
			lblMeets.Text="";
			lblFails.Text="";
			EnableMeetHighlightTools(false);
			EnableFailHighlightTools(false);
		}
		private void cmdQuery_Click(object sender, System.EventArgs e)
		{
			//Set mouse cursor as this can take some time with large datasets
			Cursor.Current = Cursors.WaitCursor;

			//Check value has been entered in field combo
			if (cboFields.Text == "")
			{
				System.Windows.Forms.MessageBox.Show("You have not selected a field.");
				Cursor.Current = Cursors.Default;
				return;
			}

			//Check value has been entered in operator combo
			if (cboOperator.Text == "")
			{
				System.Windows.Forms.MessageBox.Show("You have not selected an operator.");
				Cursor.Current = Cursors.Default;
				return;
			}

			//Check value has been entered in value textbox
			if (txtValue.Text == "")
			{
				System.Windows.Forms.MessageBox.Show("You have not entered a query value.");
				txtValue.Focus();
				Cursor.Current = Cursors.Default;
				return;
			}

			//Get layer to query
			ARMap arMap = axArcReaderControl1.ARPageLayout.FocusARMap;

			ARLayer arLayer = (ARLayer)m_LayersIndex[cboLayers.SelectedIndex];
	
			//Build the ARSearchDef
			ArcReaderSearchDef arSearchDef = new ArcReaderSearchDefClass();

			//Build WhereClause that meets search criteria
			string sWhereClause;

			//Remove quotes from WhereClause if search is numeric
			if (optNumber.Checked == true)
			{
				sWhereClause = cboFields.Text + " " + cboOperator.Text + " " + txtValue.Text;
			}
			else
			{
				sWhereClause = cboFields.Text + " " + cboOperator.Text + " '" + txtValue.Text + "'";
			}

			arSearchDef.WhereClause = sWhereClause;

			//Get ARFeatureSet that meets the search criteria
			m_arFeatureSetMeets = arLayer.QueryARFeatures(arSearchDef);

			//Build WhereClause that fails search criteria
			//Remove quotes from WhereClause if search is numeric
			if (optNumber.Checked == true)
			{
				sWhereClause = cboFields.Text + " " + InverseOperator[cboOperator.SelectedIndex].inverse.ToString() + " " + txtValue.Text;
			}
			else
			{
				sWhereClause = cboFields.Text + " " + InverseOperator[cboOperator.SelectedIndex].inverse.ToString() + " '" + txtValue.Text + "'";
			}

			arSearchDef.WhereClause = sWhereClause;

			//Get ARFeatureSet that fails search criteria
			m_arFeatureSetFails = arLayer.QueryARFeatures(arSearchDef);

			//Reset mouse cursor
			Cursor.Current = Cursors.Default;

			//Populate the DataGrid Controls with the ARFeatureSets
			PopulateFlexGrids(dataGridView1, m_arFeatureSetMeets);
			PopulateFlexGrids(dataGridView2, m_arFeatureSetFails);
		

			//Give the user some feedback regarding the number of features that meet criteria
			if (m_arFeatureSetMeets.ARFeatureCount > 0)
			{ 
				EnableMeetHighlightTools(true);
				lblMeets.Text = "Features MEETING the search criteria: " + m_arFeatureSetMeets.ARFeatureCount.ToString();
			}
			else
			{
				EnableMeetHighlightTools(false);
				dataGridView1.Rows.Clear();
				lblMeets.Text = "Features MEETING the search criteria: 0";
			}

			if (m_arFeatureSetFails.ARFeatureCount > 0)
			{
				EnableFailHighlightTools(true);
				lblFails.Text = "Features FAILING the search criteria: " + m_arFeatureSetFails.ARFeatureCount.ToString();
			}
			else
			{
				EnableFailHighlightTools(false);
                dataGridView2.Rows.Clear();
				lblMeets.Text = "Features FAILING the search criteria: 0";
			}

		}
		private void PopulateFlexGrids(DataGridView pDataGrid, ARFeatureSet arFeatureSet)
		{
			//Get first feature in ARFeatureSet
			arFeatureSet.Reset();
			ARFeature arFeature = arFeatureSet.Next();

			//Exit if no features in set
			if (arFeature == null)
			{
				return;
			}

			//Change cursor while grid populates
			Cursor = Cursors.WaitCursor;

			//Clear Grid of any existing data
            pDataGrid.Rows.Clear();		

			//Loop through and add field names        
            for (int i = 0; i < arFeature.FieldCount; i++)
			{
                pDataGrid.Columns.Add(arFeature.get_FieldName(i), arFeature.get_FieldName(i));             
			}

			//add values                  
            object[] values = new object[arFeature.FieldCount];
			
			//Populate Grid
			while (arFeature != null)
			{
                for (int i = 0; i < arFeature.FieldCount; i++)
                {
                    values[i] = ARFeatureValueAsString(arFeature, i);
                }
				
                pDataGrid.Rows.Add(values);
                
				//Move to next Feature in the FeatureSet
				arFeature = arFeatureSet.Next();
			}
			//Reset mouse cursor
			Cursor = Cursors.Default;
		}
		private string ARFeatureValueAsString(ARFeature pARFeature, int pFieldNameIndex)
		{
			// If there is an issue accessing the value the function returns a string of asterisks
			// There are many reason Asterisks may be returned...
			// The return value cant be cast into a string e.g. a BLOB value
			// The return value is stored within a hidden field in the PMF
			// The return value is a Geometry Object
			try
			{
				string pARFeatureValueAsString = pARFeature.get_Value(pARFeature.get_FieldName(pFieldNameIndex)).ToString();
				return pARFeatureValueAsString;	
			}
			catch
			{
				string pARFeatureValueAsString = "***";
				return pARFeatureValueAsString;	
			}
		}
		public static bool IsDecimal(string theValue)
		{
			//A function to mimic the VB.NET VB6 function Is Numeric
			try
			{
				Convert.ToDouble(theValue);
				return true;
			} 
			catch 
			{
				return false;
			}
		}
		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
			//Prevent user entering no numeric value if querying numeric field
			if (optNumber.Checked==true)
			{
				if (IsDecimal(txtValue.Text) == false)
				{
					txtValue.Clear();
				}
			}
		}
	}
}
