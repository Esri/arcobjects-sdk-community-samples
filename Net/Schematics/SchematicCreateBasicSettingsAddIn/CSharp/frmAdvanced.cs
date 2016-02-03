// Copyright 2010 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at &lt;your ArcGIS install location&gt;/DeveloperKit10.0/userestrictions.txt.
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Schematic;

namespace SchematicCreateBasicSettingsAddIn
{
	public partial class frmAdvanced : Form
	{
		public event EventHandler<AdvancedEvents> doneFormEvent;
		public string strLayers;
		public string strNodeLayers;
		public NameValueCollection m_myCol = new NameValueCollection();
		public NameValueCollection m_colFieldsToCreate = new NameValueCollection();

		public frmAdvanced()
		{
			InitializeComponent();
			this.Load += new EventHandler(frmAdvanced_Load);
		}

		void frmAdvanced_Load(object sender, EventArgs e)
		{
			string[] myItems;
			char[] splitter = { ';' };

			//handle the tab for attributes
			myItems = strLayers.Split(splitter);
			foreach (string s in myItems)
			{
				if (s.Length > 0)
				{
					this.tvFeatureClasses.Nodes.Add(s, s);
				}
			}

			//handle the root nodes dropdown
			myItems = strNodeLayers.Split(splitter);
			foreach (string s in myItems)
			{
				if (s.Length > 0)
				{
					this.cboRoot.Items.Add(s);
				}
			}
		}

		private void chkApplyAlgo_CheckedChanged(object sender, EventArgs e)
		{
			if (chkApplyAlgo.Checked == true)
			{
				cboDirection.Enabled = true;
				cboRoot.Enabled = true;
			}
			else
			{
				cboDirection.Enabled = false;
				cboDirection.Text = "";
				cboRoot.Enabled = false;
				cboRoot.Text = "";
			}
		}

		private void btnDone_Click(object sender, EventArgs e)
		{
			//raise event back to controller
			Dictionary<string, string> dicAlgoParams = new Dictionary<string, string>();
			string strAlgo = "";
			string strRoot = "";

			if (chkApplyAlgo.Checked == true)
			{
				dicAlgoParams.Add("Direction", cboDirection.Text);
				strAlgo = "SmartTree";
				strRoot = cboRoot.Text;
			}
			AdvancedEvents evts = new AdvancedEvents(strAlgo, dicAlgoParams, strRoot, m_colFieldsToCreate);
			this.doneFormEvent(sender, evts);
			m_myCol.Clear();
			m_colFieldsToCreate.Clear();
		}

		private void chkUseAttributes_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUseAttributes.Checked == true)
			{
				tvFeatureClasses.Enabled = true;
				chkFields.Enabled = true;
			}
			else
			{
				tvFeatureClasses.Enabled = false;
				chkFields.Enabled = false;
				chkFields.Items.Clear();
				m_colFieldsToCreate.Clear();
			}
		}

		private void chkFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] strFields = m_colFieldsToCreate.GetValues(tvFeatureClasses.SelectedNode.Name.ToString());

			if (strFields != null)
			{
				//clear that key and start over
				m_colFieldsToCreate.Remove(tvFeatureClasses.SelectedNode.Name.ToString());
			}

			if (chkFields.CheckedItems.Count > 0)
			{
				foreach (string s in chkFields.CheckedItems)
				{
					m_colFieldsToCreate.Add(tvFeatureClasses.SelectedNode.Name.ToString(), s);
				}
			}
		}

		private void tvFeatureClasses_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string strFCName = e.Node.Text.ToString();
			//load chkfields
			chkFields.Items.Clear();
			string[] strFields = m_myCol.GetValues(strFCName);

			foreach (string s in strFields)
			{
				chkFields.Items.Add(s);
			}

			//re-check the boxes if they already did check some
			if (m_colFieldsToCreate.Count > 0)
			{
				int x = -1;
				string[] strCheckedItems = m_colFieldsToCreate.GetValues(tvFeatureClasses.SelectedNode.Name.ToString());
				if (strCheckedItems != null)
				{
					foreach (string s in strCheckedItems)
					{
						x = chkFields.FindStringExact(s, -1);
						if (x != -1)
						{
							chkFields.SetItemChecked(x, true);
						}
					}
				}
			}

		}

	}
}
