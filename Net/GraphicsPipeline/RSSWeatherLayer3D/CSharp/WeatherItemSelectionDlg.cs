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
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;

namespace RSSWeatherLayer3D
{
	/// <summary>
	/// Select by city name dialog
	/// </summary>
  /// <remarks>Allows users to select items according to city names.</remarks>
	public class WeatherItemSelectionDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpWeatherItems;
		private System.Windows.Forms.ListBox lstWeatherItemNames;
		private System.Windows.Forms.Button btnRefreshList;
		private System.Windows.Forms.Label lblSelect;
		private System.Windows.Forms.TextBox txtSelect;
		private System.Windows.Forms.CheckBox chkNewSelection;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnDismiss;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.ContextMenu contextMenu1;
    private System.Windows.Forms.MenuItem menuZoomTo;

		//Class members
    private RSSWeatherLayer3DClass	m_weatherLayer				= null;
		private string[]									m_cityNames						= null;
		private DataTable									m_weatherItemsTable		= null;


    //Updating the UI must be done from the main thread, therefore the calling thread must delegate
    //the command to the main thread with the relevant data
    private delegate void IncrementProgressBarCallback();
    private delegate void AddListItmCallback(string item);
    private delegate void ShowProgressBarCallBack();
    private delegate void HideProgressBarCallBack();


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="weatherLayer"></param>
		public WeatherItemSelectionDlg(RSSWeatherLayer3DClass weatherLayer)
		{

			InitializeComponent();
			
      //get the layer
			m_weatherLayer	=	weatherLayer;
      //get the list of all citynames for all items in the layer
			m_cityNames			= m_weatherLayer.GetCityNames();

      //create a table to host the citynames
			m_weatherItemsTable = new DataTable("CityNames");
			m_weatherItemsTable.Columns.Add("CITYNAME", typeof(string));

			//populate the listbox and build a table containing the items
			PopulateList();
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
      this.grpWeatherItems = new System.Windows.Forms.GroupBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.txtSelect = new System.Windows.Forms.TextBox();
      this.lblSelect = new System.Windows.Forms.Label();
      this.btnRefreshList = new System.Windows.Forms.Button();
      this.lstWeatherItemNames = new System.Windows.Forms.ListBox();
      this.contextMenu1 = new System.Windows.Forms.ContextMenu();
      this.menuZoomTo = new System.Windows.Forms.MenuItem();
      this.chkNewSelection = new System.Windows.Forms.CheckBox();
      this.btnSelect = new System.Windows.Forms.Button();
      this.btnDismiss = new System.Windows.Forms.Button();
      this.grpWeatherItems.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpWeatherItems
      // 
      this.grpWeatherItems.Controls.Add(this.progressBar1);
      this.grpWeatherItems.Controls.Add(this.txtSelect);
      this.grpWeatherItems.Controls.Add(this.lblSelect);
      this.grpWeatherItems.Controls.Add(this.btnRefreshList);
      this.grpWeatherItems.Controls.Add(this.lstWeatherItemNames);
      this.grpWeatherItems.Location = new System.Drawing.Point(4, 8);
      this.grpWeatherItems.Name = "grpWeatherItems";
      this.grpWeatherItems.Size = new System.Drawing.Size(200, 328);
      this.grpWeatherItems.TabIndex = 0;
      this.grpWeatherItems.TabStop = false;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(8, 256);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(184, 23);
      this.progressBar1.Step = 1;
      this.progressBar1.TabIndex = 4;
      // 
      // txtSelect
      // 
      this.txtSelect.Location = new System.Drawing.Point(92, 296);
      this.txtSelect.Name = "txtSelect";
      this.txtSelect.TabIndex = 3;
      this.txtSelect.Text = "";
      this.txtSelect.TextChanged += new System.EventHandler(this.txtSelect_TextChanged);
      // 
      // lblSelect
      // 
      this.lblSelect.Location = new System.Drawing.Point(8, 300);
      this.lblSelect.Name = "lblSelect";
      this.lblSelect.Size = new System.Drawing.Size(52, 16);
      this.lblSelect.TabIndex = 2;
      this.lblSelect.Text = "Select";
      // 
      // btnRefreshList
      // 
      this.btnRefreshList.Location = new System.Drawing.Point(64, 256);
      this.btnRefreshList.Name = "btnRefreshList";
      this.btnRefreshList.TabIndex = 1;
      this.btnRefreshList.Text = "Refresh List";
      this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
      // 
      // lstWeatherItemNames
      // 
      this.lstWeatherItemNames.ContextMenu = this.contextMenu1;
      this.lstWeatherItemNames.Location = new System.Drawing.Point(8, 16);
      this.lstWeatherItemNames.Name = "lstWeatherItemNames";
      this.lstWeatherItemNames.Size = new System.Drawing.Size(184, 225);
      this.lstWeatherItemNames.Sorted = true;
      this.lstWeatherItemNames.TabIndex = 0;
      this.lstWeatherItemNames.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstWeatherItemNames_MouseDown);
      this.lstWeatherItemNames.DoubleClick += new System.EventHandler(this.lstWeatherItemNames_DoubleClick);
      // 
      // contextMenu1
      // 
      this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuZoomTo});
      // 
      // menuZoomTo
      // 
      this.menuZoomTo.Index = 0;
      this.menuZoomTo.Text = "Zoom To";
      this.menuZoomTo.Click += new System.EventHandler(this.menuZoomTo_Click);
      // 
      // chkNewSelection
      // 
      this.chkNewSelection.Checked = true;
      this.chkNewSelection.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkNewSelection.Location = new System.Drawing.Point(12, 344);
      this.chkNewSelection.Name = "chkNewSelection";
      this.chkNewSelection.TabIndex = 1;
      this.chkNewSelection.Text = "New Selection";
      // 
      // btnSelect
      // 
      this.btnSelect.Location = new System.Drawing.Point(8, 380);
      this.btnSelect.Name = "btnSelect";
      this.btnSelect.TabIndex = 2;
      this.btnSelect.Text = "Select";
      this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
      // 
      // btnDismiss
      // 
      this.btnDismiss.Location = new System.Drawing.Point(124, 384);
      this.btnDismiss.Name = "btnDismiss";
      this.btnDismiss.TabIndex = 3;
      this.btnDismiss.Text = "Dismiss";
      this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
      // 
      // WeatherItemSelectionDlg
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(206, 416);
      this.Controls.Add(this.btnDismiss);
      this.Controls.Add(this.btnSelect);
      this.Controls.Add(this.chkNewSelection);
      this.Controls.Add(this.grpWeatherItems);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "WeatherItemSelectionDlg";
      this.ShowInTaskbar = false;
      this.Text = "Select weather item";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.WeatherItemSelectionDlg_Load);
      this.VisibleChanged += new System.EventHandler(this.WeatherItemSelectionDlg_VisibleChanged);
      this.grpWeatherItems.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    /// <summary>
    /// Event handler for Form::Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void WeatherItemSelectionDlg_Load(object sender, System.EventArgs e)
		{
			txtSelect.Text = "";
			lstWeatherItemNames.ClearSelected();
		}

    /// <summary>
    /// dialog visible change event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void WeatherItemSelectionDlg_VisibleChanged(object sender, System.EventArgs e)
		{
			if(this.Visible)
			{
				txtSelect.Text = "";
				lstWeatherItemNames.ClearSelected();
			}
		}

    /// <summary>
    /// listbox's event handler for mousedown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void lstWeatherItemNames_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
      //if right click then select a record
      if(e.Button == MouseButtons.Right)
      {
        //given the point, return the item index
        Point pt = new Point(e.X,e.Y);
        int index = lstWeatherItemNames.IndexFromPoint(pt);
        if(index > 0)
        {
          //select the item pointed by the index
          lstWeatherItemNames.ClearSelected();
          lstWeatherItemNames.SelectedIndex = index;
          lstWeatherItemNames.Refresh();
        }
      }	
		}

    /// <summary>
    /// listbox's double-click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void lstWeatherItemNames_DoubleClick(object sender, System.EventArgs e)
		{
      //set the dialog results to OK
			this.DialogResult = DialogResult.OK;

      //select the items which the user double-clicked
			SelectWeatherItems();
		}

    /// <summary>
    /// refresh botton click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void btnRefreshList_Click(object sender, System.EventArgs e)
		{
      //clear all the items on the list
			m_weatherItemsTable.Rows.Clear();

      //get an up-to-date list of citynames
			m_cityNames = m_weatherLayer.GetCityNames();

      //add the citynames to the listbox
			PopulateList();
		}

    /// <summary>
    /// selection textbox text change event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void txtSelect_TextChanged(object sender, System.EventArgs e)
		{
      //clear the items of the listbox
      lstWeatherItemNames.Items.Clear();

      //spawn a thread to populate the list with items that match the selection criteria
      Thread t = new Thread(new ThreadStart(PopulateSubListProc));
			t.Start();
		}

    /// <summary>
    /// Select button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void btnSelect_Click(object sender, System.EventArgs e)
		{
      //set the dialog results to OK
			this.DialogResult = DialogResult.OK;

      //select all the weather items with are selected in the listbox
			SelectWeatherItems();
		}

    /// <summary>
    /// dismiss button event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void btnDismiss_Click(object sender, System.EventArgs e)
		{
      //set the dialog results to OK
			this.DialogResult = DialogResult.No;

      //hide the dialog
			this.Hide();
		}

    /// <summary>
    /// Populate the listbox with the citynames
    /// </summary>
		private void PopulateList()
		{
      //spawn the population thread (it populate both the listbox and the DataTable)
      Thread t = new Thread(new ThreadStart(PopulateWeatherItemsTableProc));
			t.Start();

			return;
		}

		private void PopulateWeatherItemsTableProc()
		{
      //hide the refresh button and show the progressbar
      ShowProgressBar();

      //iterate through the citynames
      foreach(string s in m_cityNames)
			{
        //create new record
				DataRow r = m_weatherItemsTable.NewRow();

        //add the cityname to the record
				r[0] = s;

        //add the record to the table
				lock(m_weatherItemsTable)
				{
					m_weatherItemsTable.Rows.Add(r);
				}
				
				//add the cityName to the listbox
        AddListItemString(s);

        //set the progress of the progressbar
        IncrementProgressBar();
			}

      //hide the progressbar and show the refresh button
      HideProgressBar();
		}

    //Make Thread-Safe Calls to Windows Forms Controls
    private void AddListItemString(string item)
    {
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      if (this.lstWeatherItemNames.InvokeRequired)
      {
        //call itself on the main thread
        AddListItmCallback d = new AddListItmCallback(AddListItemString);
        this.Invoke(d, new object[] { item });
      }
      else
      {
        //guaranteed to run on the main UI thread 
        this.lstWeatherItemNames.Items.Add(item);
      }
    }

    /// <summary>
    /// show the progressbar and hide the refresh button
    /// </summary>
    private void ShowProgressBar()
    {
      //test whether Invoke is required (was this call made on a different thread than the main thread?)
      if (this.lstWeatherItemNames.InvokeRequired)
      {
        //call itself on the main thread
        ShowProgressBarCallBack d = new ShowProgressBarCallBack(ShowProgressBar);
        this.Invoke(d);
      }
      else
      {
        //clear all the rows from the table
        m_weatherItemsTable.Rows.Clear();
        
        //clear all the items of the listbox
        lstWeatherItemNames.Items.Clear();

        //set the progressbar properties
        int count = m_cityNames.Length;
        progressBar1.Maximum = count;
        progressBar1.Value = 0;
        btnRefreshList.Visible = false;
        progressBar1.Visible = true;
      }
    }

    /// <summary>
    /// hide the progressbar and show the refresh button
    /// </summary>
    private void HideProgressBar()
    {
      //test whether Invoke is required (was this call made on a different thread than the main thread?)
      if (this.progressBar1.InvokeRequired)
      {
        //call itself on the main thread
        ShowProgressBarCallBack d = new ShowProgressBarCallBack(HideProgressBar);
        this.Invoke(d);
      }
      else
      {
       //set the visibility
        btnRefreshList.Visible = true;
        progressBar1.Visible = false;
      }
    }

    //increments the progressbar of the refresh list button
    private void IncrementProgressBar()
    {
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      if (this.progressBar1.InvokeRequired)
      {
        //call itself on the main thread
        IncrementProgressBarCallback d = new IncrementProgressBarCallback(IncrementProgressBar);
        this.Invoke(d);
      }
      else
      {
        this.progressBar1.Increment(1);
      }

    }

    /// <summary>
    /// select a weather items given selected items from the listbox
    /// </summary>
		private void SelectWeatherItems()
		{
			//get the selected list from the listbox
			bool newSelection = this.chkNewSelection.Checked;

      //in case of a new selection, unselect all items first
			if(newSelection)
				m_weatherLayer.UnselectAll();

			IPropertySet propSet = null;
			object o;
			long zipCode;
      //iterate through the selected items of the listbox
			foreach(int index in lstWeatherItemNames.SelectedIndices)
			{
        //get the weatheritem properties according to the zipCode of the item in the listbox
				propSet = m_weatherLayer.GetWeatherItem(Convert.ToString(lstWeatherItemNames.Items[index]));
				if(null == propSet)
					continue;

				o = propSet.GetProperty("ZIPCODE");
				if(null == o)
					continue;
				
				zipCode = Convert.ToInt64(o);

        //select the item in the weather layer
				m_weatherLayer.Select(zipCode, false);
			}
		}


    /// <summary>
    /// Populate the listbox according to a selection criteria
    /// </summary>
		private void PopulateSubListProc()
		{
			//get the selection criteria
      string exp = txtSelect.Text;

      //in case that the user did not specify a criteria, populate the entire citiname list
			if(exp == "")
			{
				PopulateWeatherItemsTableProc();
				return;
			}

			//set the query
			exp = "CITYNAME LIKE '" + exp + "%'";

      //do the criteria selection against the table
			DataRow[] rows = m_weatherItemsTable.Select(exp);

      //iterate through the selectionset
			foreach(DataRow r in rows)
			{
        //add the cityName to the listbox
        AddListItemString(Convert.ToString(r[0]));
			}
		}

    /// <summary>
    /// ZoomTo menu click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void menuZoomTo_Click(object sender, System.EventArgs e)
    {
      if(null == m_weatherLayer)
        return;

      //get the selected item
      string cityName = Convert.ToString(lstWeatherItemNames.SelectedItem);

      //ask the layer to zoom to that cityname
      m_weatherLayer.ZoomTo(cityName);
    }
	}
}
