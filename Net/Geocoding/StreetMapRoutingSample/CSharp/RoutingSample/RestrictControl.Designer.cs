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

namespace RoutingSample
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class RestrictControl : System.Windows.Forms.UserControl
	{

		//UserControl overrides dispose to clean up the component list.
		internal RestrictControl()
		{
			InitializeComponent();
		}
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
			this.Panel1 = new System.Windows.Forms.Panel();
			this.m_txtParameter = new System.Windows.Forms.TextBox();
			this.m_cmbType = new System.Windows.Forms.ComboBox();
			this.m_chkCheck = new System.Windows.Forms.CheckBox();
			this.Panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Panel1
			// 
			this.Panel1.Controls.Add(this.m_txtParameter);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.Panel1.Location = new System.Drawing.Point(305, 0);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(72, 22);
			this.Panel1.TabIndex = 4;
			// 
			// m_txtParameter
			// 
			this.m_txtParameter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_txtParameter.Location = new System.Drawing.Point(0, 0);
			this.m_txtParameter.Name = "m_txtParameter";
			this.m_txtParameter.Size = new System.Drawing.Size(72, 20);
			this.m_txtParameter.TabIndex = 0;
			this.m_txtParameter.Visible = false;
			// 
			// m_cmbType
			// 
			this.m_cmbType.Dock = System.Windows.Forms.DockStyle.Right;
			this.m_cmbType.FormattingEnabled = true;
			this.m_cmbType.Items.AddRange(new object[] {
						"Strict",
						"Relaxed"});
			this.m_cmbType.Location = new System.Drawing.Point(222, 0);
			this.m_cmbType.Name = "m_cmbType";
			this.m_cmbType.Size = new System.Drawing.Size(83, 21);
			this.m_cmbType.TabIndex = 1;
			// 
			// m_chkCheck
			// 
			this.m_chkCheck.AutoSize = true;
			this.m_chkCheck.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.m_chkCheck.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_chkCheck.Location = new System.Drawing.Point(0, 0);
			this.m_chkCheck.Name = "m_chkCheck";
			this.m_chkCheck.Size = new System.Drawing.Size(222, 22);
			this.m_chkCheck.TabIndex = 0;
			this.m_chkCheck.Text = "Restriction";
			this.m_chkCheck.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.m_chkCheck.UseVisualStyleBackColor = true;
			// 
			// RestrictControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.m_chkCheck);
			this.Controls.Add(this.m_cmbType);
			this.Controls.Add(this.Panel1);
			this.Name = "RestrictControl";
			this.Size = new System.Drawing.Size(377, 22);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.TextBox m_txtParameter;
		internal System.Windows.Forms.ComboBox m_cmbType;
		internal System.Windows.Forms.CheckBox m_chkCheck;

	}

} //end of root namespace