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

namespace RSSWeatherLayer
{
	/// <summary>
	/// Gets the input zipCode from the user
	/// </summary>
	public class ZipCodeDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtZipCode;
		private System.Windows.Forms.Label lblZipCode;
		private System.Windows.Forms.CheckBox chkZoomTo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ZipCodeDlg()
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
			this.txtZipCode = new System.Windows.Forms.TextBox();
			this.lblZipCode = new System.Windows.Forms.Label();
			this.chkZoomTo = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtZipCode
			// 
			this.txtZipCode.Location = new System.Drawing.Point(64, 24);
			this.txtZipCode.Name = "txtZipCode";
			this.txtZipCode.TabIndex = 0;
			this.txtZipCode.Text = "";
			// 
			// lblZipCode
			// 
			this.lblZipCode.Location = new System.Drawing.Point(8, 24);
			this.lblZipCode.Name = "lblZipCode";
			this.lblZipCode.Size = new System.Drawing.Size(48, 16);
			this.lblZipCode.TabIndex = 1;
			this.lblZipCode.Text = "ZipCode:";
			// 
			// chkZoomTo
			// 
			this.chkZoomTo.Location = new System.Drawing.Point(8, 56);
			this.chkZoomTo.Name = "chkZoomTo";
			this.chkZoomTo.TabIndex = 2;
			this.chkZoomTo.Text = "Zoom to item";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkZoomTo);
			this.groupBox1.Controls.Add(this.txtZipCode);
			this.groupBox1.Controls.Add(this.lblZipCode);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 88);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(8, 120);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(112, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ZipCodeDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(194, 152);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ZipCodeDlg";
			this.ShowInTaskbar = false;
			this.Text = "Add by zip code dialog";
			this.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// The Ok button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
    private void btnOK_Click(object sender, System.EventArgs e)
		{
      //set the dialog result
			this.DialogResult = DialogResult.OK;

      //close the dialog
			this.Close();
		}

    /// <summary>
    /// Cancel button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
      //set the dialog result
			this.DialogResult = DialogResult.Cancel;

      //close the dialog
			this.Close();
		}

    /// <summary>
    /// Returns the zipCode entered by the user
    /// </summary>
		public long ZipCode
		{
			get 
			{ 
        //make sure that the zipcode is a number
				if(IsNumber(txtZipCode.Text))
					return long.Parse(txtZipCode.Text); 
				else
					return 0;
			}	
		}

    /// <summary>
    /// Returns whether the user checked the option to zoom to the given zipCode weather item
    /// </summary>
		public bool ZoomToItem
		{
			get { return chkZoomTo.Checked; }
		}

		/// <summary>
		/// test whether a string is a number
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
    private bool IsNumber(string input) 
		{
			foreach(char c in input) 
			{
				if(!char.IsNumber(c)) return false;
			}
			return true;
		}
	}
}
