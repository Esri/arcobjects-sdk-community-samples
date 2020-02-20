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
using ESRI.ArcGIS.DataSourcesFile;

namespace RoutingSample
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class DirectionsForm : System.Windows.Forms.Form
	{

		//UserControl overrides dispose to clean up the component list.
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
			this.m_txtDirections = new System.Windows.Forms.TextBox();
			this.m_btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//m_txtDirections
			//
			this.m_txtDirections.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtDirections.Location = new System.Drawing.Point(8, 8);
			this.m_txtDirections.Multiline = true;
			this.m_txtDirections.Name = "m_txtDirections";
			this.m_txtDirections.ReadOnly = true;
			this.m_txtDirections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.m_txtDirections.Size = new System.Drawing.Size(384, 472);
			this.m_txtDirections.TabIndex = 0;
			this.m_txtDirections.Text = "";
			//
			//m_btnClose
			//
			this.m_btnClose.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnClose.Location = new System.Drawing.Point(320, 488);
			this.m_btnClose.Name = "m_btnClose";
			this.m_btnClose.TabIndex = 1;
			this.m_btnClose.Text = "Close";
			//
			//DirectionsForm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(402, 522);
			this.Controls.Add(this.m_btnClose);
			this.Controls.Add(this.m_txtDirections);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DirectionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Driving Directions";
			this.ResumeLayout(false);

		}

		internal System.Windows.Forms.TextBox m_txtDirections;
		internal System.Windows.Forms.Button m_btnClose;

	}


} //end of root namespace