namespace NetSimpleRESTSOEWithProperties.Cat
{
    partial class PropertyForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.LayerTypeBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxNumFeaturesBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ReturnFormatBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.IsEditableBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Layer Type:";
            // 
            // LayerTypeBox
            // 
            this.LayerTypeBox.Location = new System.Drawing.Point(176, 47);
            this.LayerTypeBox.Name = "LayerTypeBox";
            this.LayerTypeBox.Size = new System.Drawing.Size(109, 20);
            this.LayerTypeBox.TabIndex = 2;
            this.LayerTypeBox.Text = "feature";
            this.LayerTypeBox.TextChanged += new System.EventHandler(this.LayerTypeBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Maximum Number of Features:";
            // 
            // MaxNumFeaturesBox
            // 
            this.MaxNumFeaturesBox.Location = new System.Drawing.Point(176, 86);
            this.MaxNumFeaturesBox.Name = "MaxNumFeaturesBox";
            this.MaxNumFeaturesBox.Size = new System.Drawing.Size(109, 20);
            this.MaxNumFeaturesBox.TabIndex = 4;
            this.MaxNumFeaturesBox.Text = "100";
            this.MaxNumFeaturesBox.TextChanged += new System.EventHandler(this.MaxNumFeaturesBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Return Format:";
            // 
            // ReturnFormatBox
            // 
            this.ReturnFormatBox.Location = new System.Drawing.Point(176, 129);
            this.ReturnFormatBox.Name = "ReturnFormatBox";
            this.ReturnFormatBox.Size = new System.Drawing.Size(109, 20);
            this.ReturnFormatBox.TabIndex = 6;
            this.ReturnFormatBox.Text = "JSON";
            this.ReturnFormatBox.TextChanged += new System.EventHandler(this.ReturnFormatBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Editable?:";
            // 
            // IsEditableBox
            // 
            this.IsEditableBox.Location = new System.Drawing.Point(176, 170);
            this.IsEditableBox.Name = "IsEditableBox";
            this.IsEditableBox.Size = new System.Drawing.Size(109, 20);
            this.IsEditableBox.TabIndex = 8;
            this.IsEditableBox.Text = "false";
            this.IsEditableBox.TextChanged += new System.EventHandler(this.IsEditableBox_TextChanged);
            // 
            // PropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 273);
            this.Controls.Add(this.IsEditableBox);
            this.Controls.Add(this.ReturnFormatBox);
            this.Controls.Add(this.MaxNumFeaturesBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LayerTypeBox);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PropertyForm";
            this.Text = "PropertyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LayerTypeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox MaxNumFeaturesBox;
        private System.Windows.Forms.TextBox ReturnFormatBox;
        private System.Windows.Forms.TextBox IsEditableBox;
    }
}