// Copyright 2010 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at &lt;your ArcGIS install location&gt;/DeveloperKit10.0/userestrictions.txt.
// 
namespace SchematicCreateBasicSettingsAddIn
{
    partial class frmDatasetTemplateName
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
					this.btnNext = new System.Windows.Forms.Button();
					this.label1 = new System.Windows.Forms.Label();
					this.label2 = new System.Windows.Forms.Label();
					this.txtDatasetName = new System.Windows.Forms.TextBox();
					this.txtTemplateName = new System.Windows.Forms.TextBox();
					this.btnCancel = new System.Windows.Forms.Button();
					this.chkVertices = new System.Windows.Forms.CheckBox();
					this.SuspendLayout();
					// 
					// btnNext
					// 
					this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.btnNext.Enabled = false;
					this.btnNext.Location = new System.Drawing.Point(183, 118);
					this.btnNext.Margin = new System.Windows.Forms.Padding(2);
					this.btnNext.Name = "btnNext";
					this.btnNext.Size = new System.Drawing.Size(56, 19);
					this.btnNext.TabIndex = 0;
					this.btnNext.Text = "Next >";
					this.btnNext.UseVisualStyleBackColor = true;
					this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(9, 19);
					this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(78, 13);
					this.label1.TabIndex = 1;
					this.label1.Text = "Dataset Name:";
					// 
					// label2
					// 
					this.label2.AutoSize = true;
					this.label2.Location = new System.Drawing.Point(9, 51);
					this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
					this.label2.Name = "label2";
					this.label2.Size = new System.Drawing.Size(85, 13);
					this.label2.TabIndex = 2;
					this.label2.Text = "Template Name:";
					// 
					// txtDatasetName
					// 
					this.txtDatasetName.Location = new System.Drawing.Point(90, 16);
					this.txtDatasetName.Margin = new System.Windows.Forms.Padding(2);
					this.txtDatasetName.Name = "txtDatasetName";
					this.txtDatasetName.Size = new System.Drawing.Size(150, 20);
					this.txtDatasetName.TabIndex = 3;
					this.txtDatasetName.TextChanged += new System.EventHandler(this.txtDatasetName_TextChanged);
					// 
					// txtTemplateName
					// 
					this.txtTemplateName.Location = new System.Drawing.Point(98, 49);
					this.txtTemplateName.Margin = new System.Windows.Forms.Padding(2);
					this.txtTemplateName.Name = "txtTemplateName";
					this.txtTemplateName.Size = new System.Drawing.Size(143, 20);
					this.txtTemplateName.TabIndex = 4;
					this.txtTemplateName.TextChanged += new System.EventHandler(this.txtTemplateName_TextChanged);
					// 
					// btnCancel
					// 
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Location = new System.Drawing.Point(122, 118);
					this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(56, 19);
					this.btnCancel.TabIndex = 5;
					this.btnCancel.Text = "Cancel";
					this.btnCancel.UseVisualStyleBackColor = true;
					this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
					// 
					// chkVertices
					// 
					this.chkVertices.AutoSize = true;
					this.chkVertices.Location = new System.Drawing.Point(11, 82);
					this.chkVertices.Margin = new System.Windows.Forms.Padding(2);
					this.chkVertices.Name = "chkVertices";
					this.chkVertices.Size = new System.Drawing.Size(126, 17);
					this.chkVertices.TabIndex = 6;
					this.chkVertices.Text = "Use digitized vertices";
					this.chkVertices.UseVisualStyleBackColor = true;
					// 
					// frmDatasetTemplateName
					// 
					this.AcceptButton = this.btnNext;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.CancelButton = this.btnCancel;
					this.ClientSize = new System.Drawing.Size(251, 141);
					this.ControlBox = false;
					this.Controls.Add(this.chkVertices);
					this.Controls.Add(this.btnCancel);
					this.Controls.Add(this.txtTemplateName);
					this.Controls.Add(this.txtDatasetName);
					this.Controls.Add(this.label2);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.btnNext);
					this.Margin = new System.Windows.Forms.Padding(2);
					this.Name = "frmDatasetTemplateName";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
					this.Text = "Dataset and Template Name";
					this.Load += new System.EventHandler(this.frmDatasetTemplateName_Load);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDatasetName;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkVertices;
    }
}