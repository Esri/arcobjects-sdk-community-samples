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
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;

namespace RoutingSample
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class RestrictionsForm : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
				components.Dispose();
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.m_pnlRestrictions = new System.Windows.Forms.Panel();
			this.m_btnCancel = new System.Windows.Forms.Button();
			this.m_btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//m_pnlRestrictions
			//
			this.m_pnlRestrictions.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_pnlRestrictions.AutoScroll = true;
			this.m_pnlRestrictions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_pnlRestrictions.Location = new System.Drawing.Point(6, 12);
			this.m_pnlRestrictions.Name = "m_pnlRestrictions";
			this.m_pnlRestrictions.Size = new System.Drawing.Size(333, 274);
			this.m_pnlRestrictions.TabIndex = 0;
			//
			//m_btnCancel
			//
			this.m_btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnCancel.Location = new System.Drawing.Point(266, 295);
			this.m_btnCancel.Name = "m_btnCancel";
			this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
			this.m_btnCancel.TabIndex = 2;
			this.m_btnCancel.Text = "Cancel";
			this.m_btnCancel.UseVisualStyleBackColor = true;
			//
			//m_btnOK
			//
			this.m_btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_btnOK.Location = new System.Drawing.Point(185, 295);
			this.m_btnOK.Name = "m_btnOK";
			this.m_btnOK.Size = new System.Drawing.Size(75, 23);
			this.m_btnOK.TabIndex = 1;
			this.m_btnOK.Text = "OK";
			this.m_btnOK.UseVisualStyleBackColor = true;
			//
			//RestrictionsForm
			//
			this.AcceptButton = this.m_btnOK;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.m_btnCancel;
			this.ClientSize = new System.Drawing.Size(348, 326);
			this.Controls.Add(this.m_btnOK);
			this.Controls.Add(this.m_btnCancel);
			this.Controls.Add(this.m_pnlRestrictions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RestrictionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Restrictions";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			m_btnOK.Click += new System.EventHandler(m_btnOK_Click);
			m_btnCancel.Click += new System.EventHandler(m_btnCancel_Click);

		}
		internal System.Windows.Forms.Panel m_pnlRestrictions;
		internal System.Windows.Forms.Button m_btnCancel;
		internal System.Windows.Forms.Button m_btnOK;
	}

} //end of root namespace