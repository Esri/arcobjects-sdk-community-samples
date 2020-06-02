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
namespace TAUpdateControlSample
{
	partial class TAUpdateControlForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkManualUpdate = new System.Windows.Forms.CheckBox();
			this.checkAutoRefresh = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUpdateRate = new System.Windows.Forms.TextBox();
			this.txtRefreshRate = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnStats = new System.Windows.Forms.Button();
			this.txtStatistics = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cbRefreshType = new System.Windows.Forms.ComboBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.cbUpdateMethod = new System.Windows.Forms.ComboBox();
			this.txtUpdateValue = new System.Windows.Forms.TextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkManualUpdate
			// 
			this.checkManualUpdate.AutoSize = true;
			this.checkManualUpdate.Location = new System.Drawing.Point(10, 19);
			this.checkManualUpdate.Name = "checkManualUpdate";
			this.checkManualUpdate.Size = new System.Drawing.Size(61, 17);
			this.checkManualUpdate.TabIndex = 0;
			this.checkManualUpdate.Text = "Manual";
			this.checkManualUpdate.UseVisualStyleBackColor = true;
			this.checkManualUpdate.CheckedChanged += new System.EventHandler(this.checkManualUpdate_CheckedChanged);
			// 
			// checkAutoRefresh
			// 
			this.checkAutoRefresh.AutoSize = true;
			this.checkAutoRefresh.Location = new System.Drawing.Point(10, 22);
			this.checkAutoRefresh.Name = "checkAutoRefresh";
			this.checkAutoRefresh.Size = new System.Drawing.Size(73, 17);
			this.checkAutoRefresh.TabIndex = 1;
			this.checkAutoRefresh.Text = "Automatic";
			this.checkAutoRefresh.UseVisualStyleBackColor = true;
			this.checkAutoRefresh.CheckedChanged += new System.EventHandler(this.checkAutoRefresh_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtUpdateValue);
			this.groupBox1.Controls.Add(this.cbUpdateMethod);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtUpdateRate);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.checkManualUpdate);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(308, 80);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Update Settings";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtRefreshRate);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.checkAutoRefresh);
			this.groupBox2.Location = new System.Drawing.Point(13, 106);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(151, 80);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Refresh Settings";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Rate:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Rate:";
			// 
			// txtUpdateRate
			// 
			this.txtUpdateRate.Location = new System.Drawing.Point(46, 44);
			this.txtUpdateRate.Name = "txtUpdateRate";
			this.txtUpdateRate.Size = new System.Drawing.Size(68, 20);
			this.txtUpdateRate.TabIndex = 2;
			this.txtUpdateRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtRefreshRate
			// 
			this.txtRefreshRate.Location = new System.Drawing.Point(46, 48);
			this.txtRefreshRate.Name = "txtRefreshRate";
			this.txtRefreshRate.Size = new System.Drawing.Size(68, 20);
			this.txtRefreshRate.TabIndex = 3;
			this.txtRefreshRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(120, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "sec";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(120, 52);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "sec";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtStatistics);
			this.groupBox3.Controls.Add(this.btnStats);
			this.groupBox3.Location = new System.Drawing.Point(13, 192);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(308, 199);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Statistics";
			// 
			// btnStats
			// 
			this.btnStats.Location = new System.Drawing.Point(10, 20);
			this.btnStats.Name = "btnStats";
			this.btnStats.Size = new System.Drawing.Size(75, 23);
			this.btnStats.TabIndex = 0;
            this.btnStats.Text = "Retrieve";
			this.btnStats.UseVisualStyleBackColor = true;
			this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
			// 
			// txtStatistics
			// 
			this.txtStatistics.Location = new System.Drawing.Point(10, 49);
			this.txtStatistics.Multiline = true;
			this.txtStatistics.Name = "txtStatistics";
			this.txtStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtStatistics.Size = new System.Drawing.Size(288, 144);
			this.txtStatistics.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.btnRefresh);
			this.groupBox4.Controls.Add(this.cbRefreshType);
			this.groupBox4.Location = new System.Drawing.Point(170, 106);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(151, 80);
			this.groupBox4.TabIndex = 5;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Display Refresh";
			// 
			// cbRefreshType
			// 
			this.cbRefreshType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRefreshType.FormattingEnabled = true;
			this.cbRefreshType.Items.AddRange(new object[] {
            "Short Update",
            "Quick Update",
            "Full Update",
            "Asynchronous Update",
            "Full Screen Redraw"});
			this.cbRefreshType.Location = new System.Drawing.Point(58, 20);
			this.cbRefreshType.Name = "cbRefreshType";
			this.cbRefreshType.Size = new System.Drawing.Size(83, 21);
			this.cbRefreshType.TabIndex = 0;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(66, 46);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 1;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// cbUpdateMethod
			// 
			this.cbUpdateMethod.FormattingEnabled = true;
			this.cbUpdateMethod.Items.AddRange(new object[] {
            "Event-based",
            "CPU Usage-based"});
			this.cbUpdateMethod.Location = new System.Drawing.Point(202, 17);
			this.cbUpdateMethod.Name = "cbUpdateMethod";
			this.cbUpdateMethod.Size = new System.Drawing.Size(96, 21);
			this.cbUpdateMethod.TabIndex = 4;
			// 
			// txtUpdateValue
			// 
			this.txtUpdateValue.Location = new System.Drawing.Point(202, 44);
			this.txtUpdateValue.Name = "txtUpdateValue";
			this.txtUpdateValue.Size = new System.Drawing.Size(96, 20);
			this.txtUpdateValue.TabIndex = 5;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(243, 397);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 6;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(150, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Method:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(150, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Value:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(46, 13);
			this.label7.TabIndex = 8;
			this.label7.Text = "Method:";
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(162, 397);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(75, 23);
			this.btnHelp.TabIndex = 7;
			this.btnHelp.Text = "Help";
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// TAUpdateControlForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 428);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "TAUpdateControlForm";
			this.ShowInTaskbar = false;
			this.Text = "TAUpdateControl Settings";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TAUpdateControlForm_FormClosing);
			this.Load += new System.EventHandler(this.TAUpdateControlForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox checkManualUpdate;
		private System.Windows.Forms.CheckBox checkAutoRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUpdateRate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtRefreshRate;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtStatistics;
		private System.Windows.Forms.Button btnStats;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox cbRefreshType;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.ComboBox cbUpdateMethod;
		private System.Windows.Forms.TextBox txtUpdateValue;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnHelp;

	}
}