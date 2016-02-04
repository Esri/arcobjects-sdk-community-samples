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
namespace LoadMapControl
{
	public class frmPassword : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private string m_password;
		private int m_check;

		private System.ComponentModel.Container components = null;

		public frmPassword()
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
			this.txtPassword = new System.Windows.Forms.TextBox();
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
			this.label1.Text = "Please enter the password for the published map in the text box below and press O" +
				"K.";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(88, 64);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(152, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Text = "";
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password:";
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
			// frmPassword
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 166);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label1);
			this.Name = "frmPassword";
			this.Text = "Password Dialog";
			this.Load += new System.EventHandler(this.frmPassword_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			//On Cancel button pushed
			txtPassword.Text = "";
			m_check = 0;
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			//On OK button pushed
			m_password = txtPassword.Text;
			txtPassword.Text = "";
			m_check = 1;
			this.Close();
		}

		public string Password
		{
			//Retrieve the password 
			get {return m_password; }
		}

		public int Check
		{
			get { return m_check; }
		}
	
  	private void txtPassword_TextChanged(object sender, System.EventArgs e)
		{
			//Set Enabled property
			if(txtPassword.Text == "") 
				cmdOK.Enabled = false;
			else
				cmdOK.Enabled = true;
		}

		private void frmPassword_Load(object sender, System.EventArgs e)
		{
			//Set placeholder character for password
			txtPassword.PasswordChar = '*';
			cmdOK.Enabled = false;
		}
	}
}
