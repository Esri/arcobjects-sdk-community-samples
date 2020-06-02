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
namespace MultiItemBookmarks
{
	public class frmBookmark : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private string m_bookmark;
		private int m_check;
		private System.Windows.Forms.TextBox txtBookmark;

		private System.ComponentModel.Container components = null;

		public frmBookmark()
		{
			InitializeComponent();
		}

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
			this.label1 = new System.Windows.Forms.Label();
			this.txtBookmark = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please enter the name for the spatial bookmark in the text box below and press OK" +
				".";
			// 
			// txtBookmark
			// 
			this.txtBookmark.Location = new System.Drawing.Point(88, 64);
			this.txtBookmark.Name = "txtBookmark";
			this.txtBookmark.Size = new System.Drawing.Size(152, 20);
			this.txtBookmark.TabIndex = 1;
			this.txtBookmark.Text = "";
			this.txtBookmark.TextChanged += new System.EventHandler(this.txtBookmark_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Name:";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(40, 104);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 32);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(152, 104);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 32);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// frmBookmark
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 166);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtBookmark);
			this.Controls.Add(this.label1);
			this.Name = "frmBookmark";
			this.Text = "Spatial Bookmark";
			this.Load += new System.EventHandler(this.frmBookmark_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			//On Cancel button pushed
			txtBookmark.Text = "";
			m_check = 0;
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			//On OK button pushed
			m_bookmark = txtBookmark.Text;
			txtBookmark.Text = "";
			m_check = 1;
			this.Close();
		}

		public string Bookmark
		{
			get {return m_bookmark; }
		}

		public int Check
		{
			get { return m_check; }
		}


        private void frmBookmark_Load(object sender, System.EventArgs e)
		{
			cmdOK.Enabled = false;
		}

		private void txtBookmark_TextChanged(object sender, System.EventArgs e)
		{
			//Set Enabled property
			if(txtBookmark.Text == "") 
				cmdOK.Enabled = false;
			else
				cmdOK.Enabled = true;
		}
	}
}
