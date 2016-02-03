namespace CustomRulesPageCS
{
    partial class FrmBisectorRule
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
            this.cmbParentNodeClass = new System.Windows.Forms.ComboBox();
            this.lblParentNode = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.GroupBox();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblTargetLink = new System.Windows.Forms.Label();
            this.cmbTargetLinkClass = new System.Windows.Forms.ComboBox();
            this.lblTargetNode = new System.Windows.Forms.Label();
            this.cmbTargetNodeClass = new System.Windows.Forms.ComboBox();
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
            // cmbParentNodeClass
            // 
            this.cmbParentNodeClass.FormattingEnabled = true;
            this.cmbParentNodeClass.Location = new System.Drawing.Point(27, 84);
            this.cmbParentNodeClass.Name = "cmbParentNodeClass";
            this.cmbParentNodeClass.Size = new System.Drawing.Size(336, 22);
            this.cmbParentNodeClass.TabIndex = 1;
            this.cmbParentNodeClass.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cmbParentNodeClass.Click += new System.EventHandler(this.Changed);
            // 
            // lblParentNode
            // 
            this.lblParentNode.AutoSize = true;
            this.lblParentNode.Location = new System.Drawing.Point(24, 64);
            this.lblParentNode.Name = "lblParentNode";
            this.lblParentNode.Size = new System.Drawing.Size(238, 14);
            this.lblParentNode.TabIndex = 6;
            this.lblParentNode.Text = "Select the parent node schematic feature class:";
            // 
            // lblGroup
            // 
            this.lblGroup.Controls.Add(this.txtDistance);
            this.lblGroup.Controls.Add(this.lblDistance);
            this.lblGroup.Controls.Add(this.lblTargetLink);
            this.lblGroup.Controls.Add(this.cmbTargetLinkClass);
            this.lblGroup.Controls.Add(this.lblTargetNode);
            this.lblGroup.Controls.Add(this.cmbTargetNodeClass);
            this.lblGroup.Location = new System.Drawing.Point(27, 123);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(336, 169);
            this.lblGroup.TabIndex = 7;
            this.lblGroup.TabStop = false;
            this.lblGroup.Text = "Target schematic feature classes";
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(67, 135);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(250, 20);
            this.txtDistance.TabIndex = 4;
            this.txtDistance.TextChanged += new System.EventHandler(this.Changed);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(9, 139);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(52, 14);
            this.lblDistance.TabIndex = 10;
            this.lblDistance.Text = "Distance:";
            // 
            // lblTargetLink
            // 
            this.lblTargetLink.AutoSize = true;
            this.lblTargetLink.Location = new System.Drawing.Point(9, 78);
            this.lblTargetLink.Name = "lblTargetLink";
            this.lblTargetLink.Size = new System.Drawing.Size(226, 14);
            this.lblTargetLink.TabIndex = 9;
            this.lblTargetLink.Text = "Select the target link schematic feature class:";
            // 
            // cmbTargetLinkClass
            // 
            this.cmbTargetLinkClass.FormattingEnabled = true;
            this.cmbTargetLinkClass.Location = new System.Drawing.Point(10, 95);
            this.cmbTargetLinkClass.Name = "cmbTargetLinkClass";
            this.cmbTargetLinkClass.Size = new System.Drawing.Size(307, 22);
            this.cmbTargetLinkClass.TabIndex = 3;
            this.cmbTargetLinkClass.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cmbTargetLinkClass.Click += new System.EventHandler(this.Changed);
            // 
            // lblTargetNode
            // 
            this.lblTargetNode.AutoSize = true;
            this.lblTargetNode.Location = new System.Drawing.Point(7, 24);
            this.lblTargetNode.Name = "lblTargetNode";
            this.lblTargetNode.Size = new System.Drawing.Size(235, 14);
            this.lblTargetNode.TabIndex = 8;
            this.lblTargetNode.Text = "Select the target node schematic feature class:";
            // 
            // cmbTargetNodeClass
            // 
            this.cmbTargetNodeClass.FormattingEnabled = true;
            this.cmbTargetNodeClass.Location = new System.Drawing.Point(11, 41);
            this.cmbTargetNodeClass.Name = "cmbTargetNodeClass";
            this.cmbTargetNodeClass.Size = new System.Drawing.Size(306, 22);
            this.cmbTargetNodeClass.TabIndex = 2;
            this.cmbTargetNodeClass.SelectedIndexChanged += new System.EventHandler(this.Changed);
            this.cmbTargetNodeClass.Click += new System.EventHandler(this.Changed);
            // 
            // FrmBisectorRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(384, 307);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblParentNode);
            this.Controls.Add(this.cmbParentNodeClass);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.TxtDescription);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBisectorRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmBisectorRule";
            this.lblGroup.ResumeLayout(false);
            this.lblGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TxtDescription;
        public System.Windows.Forms.ComboBox cmbParentNodeClass;
        public System.Windows.Forms.ComboBox cmbTargetLinkClass;
        public System.Windows.Forms.ComboBox cmbTargetNodeClass;
        public System.Windows.Forms.TextBox txtDistance;
        public System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.Label lblParentNode;
        public System.Windows.Forms.Label lblTargetLink;
        public System.Windows.Forms.Label lblTargetNode;
        public System.Windows.Forms.Label lblDistance;
        public System.Windows.Forms.GroupBox lblGroup;
    }
}