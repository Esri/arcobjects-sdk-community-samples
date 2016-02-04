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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace RSSWeatherLayer3D
{
	/// <summary>
	/// The IdentifyDlg is used by the Identify object to display the identify results
	/// </summary>
	public class IdentifyDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnField;
		private System.Windows.Forms.ColumnHeader columnValue;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    /// <summary>
    /// CTor
    /// </summary>
		public IdentifyDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnField = new System.Windows.Forms.ColumnHeader();
			this.columnValue = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																								this.columnField,
																																								this.columnValue});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(296, 222);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnField
			// 
			this.columnField.Text = "Field";
			this.columnField.Width = 100;
			// 
			// columnValue
			// 
			this.columnValue.Text = "Value";
			this.columnValue.Width = 200;
			// 
			// IdentifyDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 222);
			this.Controls.Add(this.listView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "IdentifyDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "IdentifyDlg";
			this.Resize += new System.EventHandler(this.IdentifyDlg_Resize);
			this.ResumeLayout(false);

		}
		#endregion

    /// <summary>
    /// Populates the dialog's listview in order to display the identify results
    /// </summary>
    /// <param name="propSet"></param>
    /// <remarks>The identify results are passed by the layer by the IdentifyObject through a PropertySet</remarks>
		public void SetProperties(IPropertySet propSet)
		{
			if(null == propSet)
				return;
			
			//The listView gets pairs of items since it has two columns for fields and value

      string zipCode = Convert.ToString(propSet.GetProperty("ZIPCODE"));
			listView1.Items.Add(new ListViewItem(new string[2] {"ZIPCODE", zipCode}));

			string cityName = Convert.ToString(propSet.GetProperty("CITYNAME"));
			listView1.Items.Add(new ListViewItem(new string[2] {"CITYNAME", cityName}));

			string latitude = Convert.ToString(propSet.GetProperty("LATITUDE"));
			listView1.Items.Add(new ListViewItem(new string[2] {"LATITUDE", latitude}));

			string longitude = Convert.ToString(propSet.GetProperty("LONGITUDE"));
			listView1.Items.Add(new ListViewItem(new string[2] {"LONGITUDE", longitude}));

			string temperature = Convert.ToString(propSet.GetProperty("TEMPERATURE"));
			listView1.Items.Add(new ListViewItem(new string[2] {"TEMPERATURE", temperature}));

			string description = Convert.ToString(propSet.GetProperty("DESCRIPTION"));
			listView1.Items.Add(new ListViewItem(new string[2] {"DESCRIPTION", description}));

			string day = Convert.ToString(propSet.GetProperty("DAY"));
			listView1.Items.Add(new ListViewItem(new string[2] {"DAY", day}));

			string date = Convert.ToString(propSet.GetProperty("DATE"));
			listView1.Items.Add(new ListViewItem(new string[2] {"DATE", date}));

			string low = Convert.ToString(propSet.GetProperty("LOW"));
			listView1.Items.Add(new ListViewItem(new string[2] {"LOW", low}));

			string high = Convert.ToString(propSet.GetProperty("HIGH"));
			listView1.Items.Add(new ListViewItem(new string[2] {"HIGH", high}));

      string updated = Convert.ToDateTime(propSet.GetProperty("UPDATED")).ToLongTimeString();
      listView1.Items.Add(new ListViewItem(new string[2] {"UPDATED", updated}));

      string icon = Convert.ToString(propSet.GetProperty("ICON"));
      listView1.Items.Add(new ListViewItem(new string[2] {"ICON", icon}));
		}

    /// <summary>
    /// Resize the dialog in order to match the size of the hosting parent dialog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void IdentifyDlg_Resize(object sender, System.EventArgs e)
		{
			listView1.Refresh();
		}
	}
}
