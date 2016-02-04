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
using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.PublisherControls;

namespace Query
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SpatialQuery : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button cmdQuery;
		public System.Windows.Forms.Button cmdFullExtent;
		public System.Windows.Forms.RadioButton optTool2;
		public System.Windows.Forms.RadioButton optTool1;
		public System.Windows.Forms.RadioButton optTool0;
		public System.Windows.Forms.Button cmdLoad;
		public System.Windows.Forms.GroupBox Frame1;
		public System.Windows.Forms.Button cmdFeatureSet1;
		public System.Windows.Forms.Button cmdFeature3;
		public System.Windows.Forms.Button cmdFeature2;
		public System.Windows.Forms.Button cmdFeature1;
		public System.Windows.Forms.Button cmdFeature0;
		public System.Windows.Forms.Button cmdFeatureSet0;
		public System.Windows.Forms.Label lblRecords;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label5;
        public System.Windows.Forms.Label Label6;

		private IARFeature m_feature;
		private IARFeatureSet m_featureSet;
		private bool m_queryFeatures;
		private bool m_queryValues;
		private int m_record;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
			private ESRI.ArcGIS.PublisherControls.AxArcReaderControl axArcReaderControl1;
            private DataGridView dataGridView1;
            private DataGridViewTextBoxColumn Column1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SpatialQuery()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            this.cmdQuery = new System.Windows.Forms.Button();
            this.cmdFullExtent = new System.Windows.Forms.Button();
            this.optTool2 = new System.Windows.Forms.RadioButton();
            this.optTool1 = new System.Windows.Forms.RadioButton();
            this.optTool0 = new System.Windows.Forms.RadioButton();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdFeatureSet1 = new System.Windows.Forms.Button();
            this.cmdFeature3 = new System.Windows.Forms.Button();
            this.cmdFeature2 = new System.Windows.Forms.Button();
            this.cmdFeature1 = new System.Windows.Forms.Button();
            this.cmdFeature0 = new System.Windows.Forms.Button();
            this.cmdFeatureSet0 = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axArcReaderControl1 = new ESRI.ArcGIS.PublisherControls.AxArcReaderControl();
            this.Frame1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdQuery
            // 
            this.cmdQuery.BackColor = System.Drawing.SystemColors.Control;
            this.cmdQuery.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdQuery.Enabled = false;
            this.cmdQuery.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdQuery.Location = new System.Drawing.Point(368, 8);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdQuery.Size = new System.Drawing.Size(73, 49);
            this.cmdQuery.TabIndex = 11;
            this.cmdQuery.Text = "Spatial Query";
            this.cmdQuery.UseVisualStyleBackColor = false;
            this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
            // 
            // cmdFullExtent
            // 
            this.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFullExtent.Enabled = false;
            this.cmdFullExtent.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFullExtent.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdFullExtent.Location = new System.Drawing.Point(296, 8);
            this.cmdFullExtent.Name = "cmdFullExtent";
            this.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFullExtent.Size = new System.Drawing.Size(73, 49);
            this.cmdFullExtent.TabIndex = 10;
            this.cmdFullExtent.Text = "FullExtent";
            this.cmdFullExtent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdFullExtent.UseVisualStyleBackColor = false;
            this.cmdFullExtent.Click += new System.EventHandler(this.cmdFullExtent_Click);
            // 
            // optTool2
            // 
            this.optTool2.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTool2.BackColor = System.Drawing.SystemColors.Control;
            this.optTool2.Cursor = System.Windows.Forms.Cursors.Default;
            this.optTool2.Enabled = false;
            this.optTool2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTool2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optTool2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.optTool2.Location = new System.Drawing.Point(224, 8);
            this.optTool2.Name = "optTool2";
            this.optTool2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optTool2.Size = new System.Drawing.Size(73, 49);
            this.optTool2.TabIndex = 9;
            this.optTool2.TabStop = true;
            this.optTool2.Text = "Pan";
            this.optTool2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.optTool2.UseVisualStyleBackColor = false;
            this.optTool2.Click += new System.EventHandler(this.CurrentTool_Click);
            // 
            // optTool1
            // 
            this.optTool1.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTool1.BackColor = System.Drawing.SystemColors.Control;
            this.optTool1.Cursor = System.Windows.Forms.Cursors.Default;
            this.optTool1.Enabled = false;
            this.optTool1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTool1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optTool1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.optTool1.Location = new System.Drawing.Point(152, 8);
            this.optTool1.Name = "optTool1";
            this.optTool1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optTool1.Size = new System.Drawing.Size(73, 49);
            this.optTool1.TabIndex = 8;
            this.optTool1.TabStop = true;
            this.optTool1.Text = "ZoomOut";
            this.optTool1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.optTool1.UseVisualStyleBackColor = false;
            this.optTool1.Click += new System.EventHandler(this.CurrentTool_Click);
            // 
            // optTool0
            // 
            this.optTool0.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTool0.BackColor = System.Drawing.SystemColors.Control;
            this.optTool0.Cursor = System.Windows.Forms.Cursors.Default;
            this.optTool0.Enabled = false;
            this.optTool0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTool0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optTool0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.optTool0.Location = new System.Drawing.Point(80, 8);
            this.optTool0.Name = "optTool0";
            this.optTool0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optTool0.Size = new System.Drawing.Size(73, 49);
            this.optTool0.TabIndex = 7;
            this.optTool0.TabStop = true;
            this.optTool0.Text = "ZoomIn";
            this.optTool0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.optTool0.UseVisualStyleBackColor = false;
            this.optTool0.Click += new System.EventHandler(this.CurrentTool_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdLoad.Location = new System.Drawing.Point(8, 8);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdLoad.Size = new System.Drawing.Size(73, 49);
            this.cmdLoad.TabIndex = 6;
            this.cmdLoad.Text = "Open";
            this.cmdLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdLoad.UseVisualStyleBackColor = false;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.dataGridView1);
            this.Frame1.Controls.Add(this.cmdFeatureSet1);
            this.Frame1.Controls.Add(this.cmdFeature3);
            this.Frame1.Controls.Add(this.cmdFeature2);
            this.Frame1.Controls.Add(this.cmdFeature1);
            this.Frame1.Controls.Add(this.cmdFeature0);
            this.Frame1.Controls.Add(this.cmdFeatureSet0);
            this.Frame1.Controls.Add(this.lblRecords);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(448, 128);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(241, 369);
            this.Frame1.TabIndex = 19;
            this.Frame1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 62);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(233, 260);
            this.dataGridView1.TabIndex = 25;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 5;
            // 
            // cmdFeatureSet1
            // 
            this.cmdFeatureSet1.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeatureSet1.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeatureSet1.Enabled = false;
            this.cmdFeatureSet1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeatureSet1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeatureSet1.Location = new System.Drawing.Point(120, 16);
            this.cmdFeatureSet1.Name = "cmdFeatureSet1";
            this.cmdFeatureSet1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeatureSet1.Size = new System.Drawing.Size(113, 25);
            this.cmdFeatureSet1.TabIndex = 14;
            this.cmdFeatureSet1.Text = "<< Previous Feature";
            this.cmdFeatureSet1.UseVisualStyleBackColor = false;
            this.cmdFeatureSet1.Click += new System.EventHandler(this.cmdFeatureSet_Click);
            // 
            // cmdFeature3
            // 
            this.cmdFeature3.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeature3.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeature3.Enabled = false;
            this.cmdFeature3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeature3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeature3.Location = new System.Drawing.Point(176, 328);
            this.cmdFeature3.Name = "cmdFeature3";
            this.cmdFeature3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeature3.Size = new System.Drawing.Size(57, 33);
            this.cmdFeature3.TabIndex = 11;
            this.cmdFeature3.Text = "Flicker";
            this.cmdFeature3.UseVisualStyleBackColor = false;
            this.cmdFeature3.Click += new System.EventHandler(this.cmdFeature_Click);
            // 
            // cmdFeature2
            // 
            this.cmdFeature2.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeature2.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeature2.Enabled = false;
            this.cmdFeature2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeature2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeature2.Location = new System.Drawing.Point(120, 328);
            this.cmdFeature2.Name = "cmdFeature2";
            this.cmdFeature2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeature2.Size = new System.Drawing.Size(57, 33);
            this.cmdFeature2.TabIndex = 10;
            this.cmdFeature2.Text = "Flash";
            this.cmdFeature2.UseVisualStyleBackColor = false;
            this.cmdFeature2.Click += new System.EventHandler(this.cmdFeature_Click);
            // 
            // cmdFeature1
            // 
            this.cmdFeature1.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeature1.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeature1.Enabled = false;
            this.cmdFeature1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeature1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeature1.Location = new System.Drawing.Point(64, 328);
            this.cmdFeature1.Name = "cmdFeature1";
            this.cmdFeature1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeature1.Size = new System.Drawing.Size(57, 33);
            this.cmdFeature1.TabIndex = 9;
            this.cmdFeature1.Text = "CenterAt";
            this.cmdFeature1.UseVisualStyleBackColor = false;
            this.cmdFeature1.Click += new System.EventHandler(this.cmdFeature_Click);
            // 
            // cmdFeature0
            // 
            this.cmdFeature0.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeature0.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeature0.Enabled = false;
            this.cmdFeature0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeature0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeature0.Location = new System.Drawing.Point(8, 328);
            this.cmdFeature0.Name = "cmdFeature0";
            this.cmdFeature0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeature0.Size = new System.Drawing.Size(57, 33);
            this.cmdFeature0.TabIndex = 8;
            this.cmdFeature0.Text = "ZoomTo";
            this.cmdFeature0.UseVisualStyleBackColor = false;
            this.cmdFeature0.Click += new System.EventHandler(this.cmdFeature_Click);
            // 
            // cmdFeatureSet0
            // 
            this.cmdFeatureSet0.BackColor = System.Drawing.SystemColors.Control;
            this.cmdFeatureSet0.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdFeatureSet0.Enabled = false;
            this.cmdFeatureSet0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFeatureSet0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdFeatureSet0.Location = new System.Drawing.Point(8, 16);
            this.cmdFeatureSet0.Name = "cmdFeatureSet0";
            this.cmdFeatureSet0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdFeatureSet0.Size = new System.Drawing.Size(113, 25);
            this.cmdFeatureSet0.TabIndex = 7;
            this.cmdFeatureSet0.Text = "Next Feature >>";
            this.cmdFeatureSet0.UseVisualStyleBackColor = false;
            this.cmdFeatureSet0.Click += new System.EventHandler(this.cmdFeatureSet_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.BackColor = System.Drawing.SystemColors.Control;
            this.lblRecords.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRecords.Enabled = false;
            this.lblRecords.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRecords.Location = new System.Drawing.Point(8, 48);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRecords.Size = new System.Drawing.Size(225, 17);
            this.lblRecords.TabIndex = 13;
            this.lblRecords.Text = "0 of 0 features";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label2.Location = new System.Drawing.Point(448, 8);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(249, 17);
            this.Label2.TabIndex = 23;
            this.Label2.Text = "1) Browse to a PMF to load.";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label3.Location = new System.Drawing.Point(448, 24);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(249, 17);
            this.Label3.TabIndex = 22;
            this.Label3.Text = "2) Navigate to some features of interest. ";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label5.Location = new System.Drawing.Point(448, 72);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(225, 56);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "4) Loop through the features to display field values and use the buttons to ident" +
                "ify each feature on the map.";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Label6.Location = new System.Drawing.Point(448, 48);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(249, 30);
            this.Label6.TabIndex = 20;
            this.Label6.Text = "3) Query the focus map for all visible features within the current map extent. ";
            // 
            // axArcReaderControl1
            // 
            this.axArcReaderControl1.Location = new System.Drawing.Point(8, 64);
            this.axArcReaderControl1.Name = "axArcReaderControl1";
            this.axArcReaderControl1.Size = new System.Drawing.Size(432, 424);
            this.axArcReaderControl1.TabIndex = 24;
            this.axArcReaderControl1.OnCurrentViewChanged += new ESRI.ArcGIS.PublisherControls.IARControlEvents_Ax_OnCurrentViewChangedEventHandler(this.axArcReaderControl1_OnCurrentViewChanged);
            // 
            // SpatialQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(696, 502);
            this.Controls.Add(this.axArcReaderControl1);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.cmdQuery);
            this.Controls.Add(this.cmdFullExtent);
            this.Controls.Add(this.optTool2);
            this.Controls.Add(this.optTool1);
            this.Controls.Add(this.optTool0);
            this.Controls.Add(this.cmdLoad);
            this.Name = "SpatialQuery";
            this.Text = "SpatialQuery";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.Frame1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axArcReaderControl1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
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

			Application.Run(new SpatialQuery());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			//Load command button images from the resource editor
			System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "browse.bmp"));
			bitmap1.MakeTransparent(System.Drawing.Color.Teal);
			cmdLoad.Image = bitmap1;
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "ZoomIn.bmp"));
			bitmap2.MakeTransparent(System.Drawing.Color.Teal);
			optTool0.Image = bitmap2;
			System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "ZoomOut.bmp"));
			bitmap3.MakeTransparent(System.Drawing.Color.Teal);
			optTool1.Image = bitmap3;    
			System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "Pan.bmp"));
			bitmap4.MakeTransparent(System.Drawing.Color.Teal);
			optTool2.Image = bitmap4;
			System.Drawing.Bitmap bitmap5 = new System.Drawing.Bitmap (GetType().Assembly.GetManifestResourceStream(GetType(), "FullExtent.bmp"));
			bitmap5.MakeTransparent(System.Drawing.Color.Teal);
			cmdFullExtent.Image = bitmap5;
		}
		private void cmdFeature_Click(object sender, System.EventArgs e)
		{
			Button b = (Button) sender;
			//Navigate or show the selected feature
			switch (b.Name)
			{
				case "cmdFeature0":
					m_feature.ZoomTo();
					break;
				case "cmdFeature1":
					m_feature.CenterAt();
					break;
				case "cmdFeature2":
					m_feature.Flash();
					break;
				case "cmdFeature3":
					m_feature.Flicker();
					break;
			}
		}

		private void cmdFeatureSet_Click(object sender, System.EventArgs e)
		{
			Button b = (Button) sender;
			switch (b.Name)
			{
				case "cmdFeatureSet0":
					//Next record
					m_record = m_record + 1;
					break;
				case "cmdFeatureSet1":
					//Previous record
					m_record = m_record - 1;
					break;
			}

			//Get the next/previous feature
			m_feature = m_featureSet.get_ARFeature(m_record);
			//Display attribute values
			UpdateValueDisplay();
		}

		private void cmdFullExtent_Click(object sender, System.EventArgs e)
		{
			double dXmax=0; double dXmin=0; double dYmin=0; double dYmax=0;
			//Get the coordinates of data's full extent
			axArcReaderControl1.ARPageLayout.FocusARMap.GetFullExtent(ref dXmin, ref dYmin, ref dXmax, ref dYmax);
			//Set the extent of the focus map
			axArcReaderControl1.ARPageLayout.FocusARMap.SetExtent(dXmin, dYmin, dXmax, dYmax);
			//Refresh the display
			axArcReaderControl1.ARPageLayout.FocusARMap.Refresh(true);
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
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

			//Determine whether permission to search layers and query field values
			m_queryFeatures = axArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryFeatures);
			m_queryValues = axArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryValues);

			//Set current tool
			optTool0.Checked = true;
		}

		private void cmdQuery_Click(object sender, System.EventArgs e)
		{
			//Determine whether permission to search layers
			if (m_queryFeatures == false)
			{
				System.Windows.Forms.MessageBox.Show("You do not have permission to search for features!");
				return;
			}

			//Get IARQueryDef interface
			ArcReaderSearchDefClass searchDef = new ArcReaderSearchDefClass();
			//Set the spatial searching to intersects
			searchDef.SpatialRelationship = esriARSpatialRelationship.esriARSpatialRelationshipIntersects;

			//Get the coordinates of the current extent
			double dXmax=0; double dXmin=0; double dYmin=0; double dYmax=0;
			axArcReaderControl1.ARPageLayout.FocusARMap.GetExtent(ref dXmin, ref dYmin, ref dXmax, ref dYmax);
			//Set the envelope coordinates as the search shape
			searchDef.SetEnvelopeShape(dXmin, dYmin, dXmax, dYmax,0);

			//Get IARFeatureSet interface
			m_featureSet = axArcReaderControl1.ARPageLayout.FocusARMap.QueryARFeatures(searchDef);
			//Reset the featureset
			m_featureSet.Reset();
			//Get the IARFeature interface
			m_feature = m_featureSet.Next();
			//Display attribute values
			m_record = 0;
			UpdateValueDisplay();
		}
		private void CurrentTool_Click(object sender, System.EventArgs e)
		{
			RadioButton b = (RadioButton) sender;
			//Navigate or show the selected feature
			switch (b.Name)
			{
				//Set current tool
				case "optTool0":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn;
					break;
				case "optTool1":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut;
					break;
				case "optTool2":
					axArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan;
					break;
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Release COM objects 
			ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
		}

		private void axArcReaderControl1_OnCurrentViewChanged(object sender, ESRI.ArcGIS.PublisherControls.IARControlEvents_OnCurrentViewChangedEvent e)
		{
			 bool enabled;

			//Set the current tool
			if (axArcReaderControl1.CurrentViewType == esriARViewType.esriARViewTypeNone)
			{
				enabled = false;
			}
			else if (axArcReaderControl1.CurrentViewType == esriARViewType.esriARViewTypePageLayout)
			{
				enabled = false;
				if (axArcReaderControl1.CurrentARTool != esriARTool.esriARToolNoneSelected)
				{
					 axArcReaderControl1.CurrentARTool = esriARTool.esriARToolNoneSelected;
				}											
			}	
			else
			{
				enabled = true;
				if (axArcReaderControl1.CurrentARTool != esriARTool.esriARToolMapZoomIn)
				{
					optTool0.Checked = true;
				}
			}

			//Enable\disable controls
			cmdQuery.Enabled = enabled;
			optTool0.Enabled = enabled;
			optTool1.Enabled = enabled;
			optTool2.Enabled = enabled;
			cmdFullExtent.Enabled = enabled;
		}
		private void UpdateValueDisplay()
		{
			Graphics graphics = this.CreateGraphics();

			dataGridView1.Rows.Clear();
			
			//For each field that isn't the 'Shape' field
			int iRow=0; 
			string fieldName; string fieldValue;
			
			for (int i = 0; i<= m_feature.FieldCount - 1; i++)
			{
				if (m_feature.get_FieldType(i) != esriARFieldType.esriARFieldTypeGeometry & (m_feature.get_FieldType(i) != esriARFieldType.esriARFieldTypeRaster) & (m_feature.get_FieldType(i) != esriARFieldType.esriARFieldTypeBlob))
				{
                    DataGridViewRow dataGridViewRow = new DataGridViewRow();                    
                   
					//Display field names					
					fieldName = m_feature.get_FieldAliasName(i);	                    
                    dataGridViewRow.HeaderCell.Value = fieldName;
                    					
                    //display field values
					if (m_queryValues == true)
					{
						fieldValue = m_feature.get_ValueAsString(i);
					}
					else
					{
						fieldValue = "No Permission";
					}                   
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = fieldValue;

                    dataGridViewRow.Cells.Add(cell);
                    dataGridView1.Rows.Add(dataGridViewRow);

					iRow = iRow + 1;
				}
			}
         
			//Enabled/disbale controls
			bool enabled;
			if( m_featureSet.ARFeatureCount == 0)
			{
				enabled = false;
				cmdFeatureSet0.Enabled = false;
				cmdFeatureSet1.Enabled = false;
				lblRecords.Text = m_record + " of " + m_featureSet.ARFeatureCount;
			}	
			else if (m_featureSet.ARFeatureCount == 1)
			{
				enabled = true;
				cmdFeatureSet0.Enabled = false;
				cmdFeatureSet1.Enabled = false;
				lblRecords.Text = m_record + 1 + " of " + m_featureSet.ARFeatureCount;
			}
			else
			{
				enabled = true;
				if (m_record == 0) 
				{
					cmdFeatureSet1.Enabled = false;
				}
				else 
				{
					cmdFeatureSet1.Enabled = true;
				}
				if (m_record + 1 == m_featureSet.ARFeatureCount) 
				{
					cmdFeatureSet0.Enabled = false;
				}
				else 
				{
					cmdFeatureSet0.Enabled = true;
				}	
				lblRecords.Text = m_record + 1 + " of " + m_featureSet.ARFeatureCount;
			}																											   
		
			cmdFeature0.Enabled = enabled;
			cmdFeature1.Enabled = enabled;
			cmdFeature2.Enabled = enabled;
			cmdFeature3.Enabled = enabled;

			//Clean up the Graphics object
			graphics.Dispose();
		}
	}
}
