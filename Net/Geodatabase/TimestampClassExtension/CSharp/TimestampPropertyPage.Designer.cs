namespace Timestamper
{
	partial class TimestampPropertyPage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimestampPropertyPage));
			this.cmbModifiedField = new System.Windows.Forms.ComboBox();
			this.lblModifiedField = new System.Windows.Forms.Label();
			this.cmbCreatedField = new System.Windows.Forms.ComboBox();
			this.lblCreatedField = new System.Windows.Forms.Label();
			this.lblUserField = new System.Windows.Forms.Label();
			this.cmbUserField = new System.Windows.Forms.ComboBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmbModifiedField
			// 
			this.cmbModifiedField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbModifiedField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbModifiedField.FormattingEnabled = true;
			this.cmbModifiedField.Items.AddRange(new object[] {
            "Not Used"});
			this.cmbModifiedField.Location = new System.Drawing.Point(3, 70);
			this.cmbModifiedField.Name = "cmbModifiedField";
			this.cmbModifiedField.Size = new System.Drawing.Size(267, 21);
			this.cmbModifiedField.TabIndex = 7;
			// 
			// lblModifiedField
			// 
			this.lblModifiedField.AutoSize = true;
			this.lblModifiedField.Location = new System.Drawing.Point(3, 54);
			this.lblModifiedField.Name = "lblModifiedField";
			this.lblModifiedField.Size = new System.Drawing.Size(75, 13);
			this.lblModifiedField.TabIndex = 6;
			this.lblModifiedField.Text = "Modified Field:";
			// 
			// cmbCreatedField
			// 
			this.cmbCreatedField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbCreatedField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCreatedField.FormattingEnabled = true;
			this.cmbCreatedField.Items.AddRange(new object[] {
            "Not Used"});
			this.cmbCreatedField.Location = new System.Drawing.Point(3, 25);
			this.cmbCreatedField.Name = "cmbCreatedField";
			this.cmbCreatedField.Size = new System.Drawing.Size(267, 21);
			this.cmbCreatedField.TabIndex = 5;
			// 
			// lblCreatedField
			// 
			this.lblCreatedField.AutoSize = true;
			this.lblCreatedField.Location = new System.Drawing.Point(3, 9);
			this.lblCreatedField.Name = "lblCreatedField";
			this.lblCreatedField.Size = new System.Drawing.Size(72, 13);
			this.lblCreatedField.TabIndex = 4;
			this.lblCreatedField.Text = "Created Field:";
			// 
			// lblUserField
			// 
			this.lblUserField.AutoSize = true;
			this.lblUserField.Location = new System.Drawing.Point(3, 100);
			this.lblUserField.Name = "lblUserField";
			this.lblUserField.Size = new System.Drawing.Size(54, 13);
			this.lblUserField.TabIndex = 8;
			this.lblUserField.Text = "User field:";
			// 
			// cmbUserField
			// 
			this.cmbUserField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbUserField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUserField.FormattingEnabled = true;
			this.cmbUserField.Items.AddRange(new object[] {
            "Not Used"});
			this.cmbUserField.Location = new System.Drawing.Point(3, 116);
			this.cmbUserField.Name = "cmbUserField";
			this.cmbUserField.Size = new System.Drawing.Size(267, 21);
			this.cmbUserField.TabIndex = 9;
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
			this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.txtDescription.Location = new System.Drawing.Point(3, 150);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.Size = new System.Drawing.Size(267, 85);
			this.txtDescription.TabIndex = 15;
			this.txtDescription.TabStop = false;
			this.txtDescription.Text = resources.GetString("txtDescription.Text");
			// 
			// TimestampPropertyPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.cmbUserField);
			this.Controls.Add(this.lblUserField);
			this.Controls.Add(this.cmbModifiedField);
			this.Controls.Add(this.lblModifiedField);
			this.Controls.Add(this.cmbCreatedField);
			this.Controls.Add(this.lblCreatedField);
			this.Name = "TimestampPropertyPage";
			this.Size = new System.Drawing.Size(273, 241);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cmbModifiedField;
		private System.Windows.Forms.Label lblModifiedField;
		private System.Windows.Forms.ComboBox cmbCreatedField;
		private System.Windows.Forms.Label lblCreatedField;
		private System.Windows.Forms.Label lblUserField;
		private System.Windows.Forms.ComboBox cmbUserField;
		private System.Windows.Forms.TextBox txtDescription;
	}
}
