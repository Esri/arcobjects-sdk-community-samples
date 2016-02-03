namespace CustomRulesPageCS
{
    partial class FrmNodeReductionRule
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
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbReducedNodeClass = new System.Windows.Forms.ComboBox();
            this.lblReducedNode = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.GroupBox();
            this.txtLinkAttribute = new System.Windows.Forms.TextBox();
            this.chkLinkAttribute = new System.Windows.Forms.CheckBox();
            this.cmbAttributeName = new System.Windows.Forms.ComboBox();
            this.chkKeepVertices = new System.Windows.Forms.CheckBox();
            this.lblTargetSuperspan = new System.Windows.Forms.Label();
            this.cmbTargetSuperspanClass = new System.Windows.Forms.ComboBox();
            this.lblAttributeName = new System.Windows.Forms.Label();
            this.lblGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtDescription
            // 
            this.TxtDescription.Location = new System.Drawing.Point(27, 29);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(336, 20);
            this.TxtDescription.TabIndex = 0;
            this.TxtDescription.TextChanged += new System.EventHandler(this.Changed);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(24, 10);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(64, 14);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description:";
            // 
            // cmbReducedNodeClass
            // 
            this.cmbReducedNodeClass.FormattingEnabled = true;
            this.cmbReducedNodeClass.Location = new System.Drawing.Point(27, 84);
            this.cmbReducedNodeClass.Name = "cmbReducedNodeClass";
            this.cmbReducedNodeClass.Size = new System.Drawing.Size(336, 22);
            this.cmbReducedNodeClass.TabIndex = 1;
            this.cmbReducedNodeClass.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cmbReducedNodeClass.Click += new System.EventHandler(this.Changed);
            // 
            // lblReducedNode
            // 
            this.lblReducedNode.AutoSize = true;
            this.lblReducedNode.Location = new System.Drawing.Point(24, 64);
            this.lblReducedNode.Name = "lblReducedNode";
            this.lblReducedNode.Size = new System.Drawing.Size(185, 14);
            this.lblReducedNode.TabIndex = 6;
            this.lblReducedNode.Text = "Select the schematic class to reduce";
            // 
            // lblGroup
            // 
            this.lblGroup.Controls.Add(this.txtLinkAttribute);
            this.lblGroup.Controls.Add(this.chkLinkAttribute);
            this.lblGroup.Controls.Add(this.cmbAttributeName);
            this.lblGroup.Controls.Add(this.chkKeepVertices);
            this.lblGroup.Controls.Add(this.lblTargetSuperspan);
            this.lblGroup.Controls.Add(this.cmbTargetSuperspanClass);
            this.lblGroup.Controls.Add(this.lblAttributeName);
            this.lblGroup.Location = new System.Drawing.Point(27, 123);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(336, 221);
            this.lblGroup.TabIndex = 7;
            this.lblGroup.TabStop = false;
            this.lblGroup.Text = "Target superspan link";
            // 
            // txtLinkAttribute
            // 
            this.txtLinkAttribute.Location = new System.Drawing.Point(11, 160);
            this.txtLinkAttribute.Name = "txtLinkAttribute";
            this.txtLinkAttribute.Size = new System.Drawing.Size(304, 20);
            this.txtLinkAttribute.TabIndex = 5;
            this.txtLinkAttribute.TextChanged += new System.EventHandler(this.Changed);
            // 
            // chkLinkAttribute
            // 
            this.chkLinkAttribute.AutoSize = true;
            this.chkLinkAttribute.Checked = true;
            this.chkLinkAttribute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLinkAttribute.Location = new System.Drawing.Point(12, 136);
            this.chkLinkAttribute.Name = "chkLinkAttribute";
            this.chkLinkAttribute.Size = new System.Drawing.Size(158, 17);
            this.chkLinkAttribute.TabIndex = 4;
            this.chkLinkAttribute.Text = "By Connected Link Attribute";
            this.chkLinkAttribute.UseVisualStyleBackColor = true;
            this.chkLinkAttribute.CheckedChanged += new System.EventHandler(this.chkLinkAttribute_CheckedChanged);
            // 
            // cmbAttributeName
            // 
            this.cmbAttributeName.FormattingEnabled = true;
            this.cmbAttributeName.Location = new System.Drawing.Point(12, 99);
            this.cmbAttributeName.Name = "cmbAttributeName";
            this.cmbAttributeName.Size = new System.Drawing.Size(303, 22);
            this.cmbAttributeName.TabIndex = 3;
            this.cmbAttributeName.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cmbAttributeName.Click += new System.EventHandler(this.Changed);
            // 
            // chkKeepVertices
            // 
            this.chkKeepVertices.Checked = true;
            this.chkKeepVertices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepVertices.Location = new System.Drawing.Point(11, 195);
            this.chkKeepVertices.Name = "chkKeepVertices";
            this.chkKeepVertices.Size = new System.Drawing.Size(305, 18);
            this.chkKeepVertices.TabIndex = 6;
            this.chkKeepVertices.Text = "Keep vertices";
            this.chkKeepVertices.UseVisualStyleBackColor = true;
            this.chkKeepVertices.CheckStateChanged += new System.EventHandler(this.Changed);
            // 
            // lblTargetSuperspan
            // 
            this.lblTargetSuperspan.AutoSize = true;
            this.lblTargetSuperspan.Location = new System.Drawing.Point(7, 24);
            this.lblTargetSuperspan.Name = "lblTargetSuperspan";
            this.lblTargetSuperspan.Size = new System.Drawing.Size(136, 14);
            this.lblTargetSuperspan.TabIndex = 8;
            this.lblTargetSuperspan.Text = "Select the schematic class";
            // 
            // cmbTargetSuperspanClass
            // 
            this.cmbTargetSuperspanClass.FormattingEnabled = true;
            this.cmbTargetSuperspanClass.Location = new System.Drawing.Point(11, 41);
            this.cmbTargetSuperspanClass.Name = "cmbTargetSuperspanClass";
            this.cmbTargetSuperspanClass.Size = new System.Drawing.Size(306, 22);
            this.cmbTargetSuperspanClass.TabIndex = 2;
            this.cmbTargetSuperspanClass.SelectedIndexChanged += new System.EventHandler(this.FillAttNames);
            this.cmbTargetSuperspanClass.Click += new System.EventHandler(this.FillAttNames);
            // 
            // lblAttributeName
            // 
            this.lblAttributeName.AutoSize = true;
            this.lblAttributeName.Location = new System.Drawing.Point(8, 78);
            this.lblAttributeName.Name = "lblAttributeName";
            this.lblAttributeName.Size = new System.Drawing.Size(130, 14);
            this.lblAttributeName.TabIndex = 10;
            this.lblAttributeName.Text = "Cumulative attribute name";
            // 
            // FrmNodeReductionRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(384, 355);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblReducedNode);
            this.Controls.Add(this.cmbReducedNodeClass);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.TxtDescription);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNodeReductionRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmNodeReductionRule";
            this.lblGroup.ResumeLayout(false);
            this.lblGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TxtDescription;
        public System.Windows.Forms.ComboBox cmbReducedNodeClass;
        public System.Windows.Forms.ComboBox cmbTargetSuperspanClass;
        public System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.Label lblReducedNode;
        public System.Windows.Forms.Label lblTargetSuperspan;
        public System.Windows.Forms.Label lblAttributeName;
        public System.Windows.Forms.GroupBox lblGroup;
        public System.Windows.Forms.CheckBox chkKeepVertices;
        public System.Windows.Forms.ComboBox cmbAttributeName;
        public System.Windows.Forms.TextBox txtLinkAttribute;
        public System.Windows.Forms.CheckBox chkLinkAttribute;
    }
}