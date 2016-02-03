namespace TAPurgeRuleCommand
{
	partial class PurgeRuleForm
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
			this.cbTrackingLayers = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbPurgeRule = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkAutoPurge = new System.Windows.Forms.CheckBox();
			this.txtThreshold = new System.Windows.Forms.TextBox();
			this.txtPurgePercent = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbTrackingLayers
			// 
			this.cbTrackingLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbTrackingLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTrackingLayers.FormattingEnabled = true;
			this.cbTrackingLayers.Location = new System.Drawing.Point(12, 29);
			this.cbTrackingLayers.Name = "cbTrackingLayers";
			this.cbTrackingLayers.Size = new System.Drawing.Size(182, 21);
			this.cbTrackingLayers.TabIndex = 0;
			this.cbTrackingLayers.SelectionChangeCommitted += new System.EventHandler(this.cbTrackingLayers_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Tracking Layer:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtPurgePercent);
			this.groupBox1.Controls.Add(this.txtThreshold);
			this.groupBox1.Controls.Add(this.checkAutoPurge);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbPurgeRule);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 128);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Purge Rule Settings";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Purge Rule:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Threshold:";
			// 
			// cbPurgeRule
			// 
			this.cbPurgeRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbPurgeRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPurgeRule.FormattingEnabled = true;
			this.cbPurgeRule.Items.AddRange(new object[] {
            "Oldest",
            "All Except Latest"});
			this.cbPurgeRule.Location = new System.Drawing.Point(77, 40);
			this.cbPurgeRule.Name = "cbPurgeRule";
			this.cbPurgeRule.Size = new System.Drawing.Size(105, 21);
			this.cbPurgeRule.TabIndex = 2;
			this.cbPurgeRule.SelectedIndexChanged += new System.EventHandler(this.cbPurgeRule_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Percent to Purge:";
			// 
			// checkAutoPurge
			// 
			this.checkAutoPurge.AutoSize = true;
			this.checkAutoPurge.Location = new System.Drawing.Point(10, 19);
			this.checkAutoPurge.Name = "checkAutoPurge";
			this.checkAutoPurge.Size = new System.Drawing.Size(79, 17);
			this.checkAutoPurge.TabIndex = 4;
			this.checkAutoPurge.Text = "Auto Purge";
			this.checkAutoPurge.UseVisualStyleBackColor = true;
			// 
			// txtThreshold
			// 
			this.txtThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtThreshold.Location = new System.Drawing.Point(77, 67);
			this.txtThreshold.Name = "txtThreshold";
			this.txtThreshold.Size = new System.Drawing.Size(105, 20);
			this.txtThreshold.TabIndex = 5;
			this.txtThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtPurgePercent
			// 
			this.txtPurgePercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPurgePercent.Location = new System.Drawing.Point(103, 93);
			this.txtPurgePercent.Name = "txtPurgePercent";
			this.txtPurgePercent.Size = new System.Drawing.Size(79, 20);
			this.txtPurgePercent.TabIndex = 6;
			this.txtPurgePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(44, 191);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(125, 191);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 6;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// PurgeRuleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(212, 221);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbTrackingLayers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "PurgeRuleForm";
			this.ShowInTaskbar = false;
			this.Text = "Purge Rule";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PurgeRuleForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbTrackingLayers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbPurgeRule;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPurgePercent;
		private System.Windows.Forms.TextBox txtThreshold;
		private System.Windows.Forms.CheckBox checkAutoPurge;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnApply;
	}
}