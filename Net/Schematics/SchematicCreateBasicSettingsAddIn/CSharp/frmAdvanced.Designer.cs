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
    partial class frmAdvanced
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
					this.tabControl1 = new System.Windows.Forms.TabControl();
					this.tabPage1 = new System.Windows.Forms.TabPage();
					this.cboRoot = new System.Windows.Forms.ComboBox();
					this.label2 = new System.Windows.Forms.Label();
					this.label1 = new System.Windows.Forms.Label();
					this.cboDirection = new System.Windows.Forms.ComboBox();
					this.chkApplyAlgo = new System.Windows.Forms.CheckBox();
					this.tabPage2 = new System.Windows.Forms.TabPage();
					this.chkUseAttributes = new System.Windows.Forms.CheckBox();
					this.chkFields = new System.Windows.Forms.CheckedListBox();
					this.tvFeatureClasses = new System.Windows.Forms.TreeView();
					this.btnDone = new System.Windows.Forms.Button();
					this.tabControl1.SuspendLayout();
					this.tabPage1.SuspendLayout();
					this.tabPage2.SuspendLayout();
					this.SuspendLayout();
					// 
					// tabControl1
					// 
					this.tabControl1.Controls.Add(this.tabPage1);
					this.tabControl1.Controls.Add(this.tabPage2);
					this.tabControl1.Location = new System.Drawing.Point(10, 11);
					this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
					this.tabControl1.Name = "tabControl1";
					this.tabControl1.SelectedIndex = 0;
					this.tabControl1.Size = new System.Drawing.Size(344, 292);
					this.tabControl1.TabIndex = 0;
					// 
					// tabPage1
					// 
					this.tabPage1.Controls.Add(this.cboRoot);
					this.tabPage1.Controls.Add(this.label2);
					this.tabPage1.Controls.Add(this.label1);
					this.tabPage1.Controls.Add(this.cboDirection);
					this.tabPage1.Controls.Add(this.chkApplyAlgo);
					this.tabPage1.Location = new System.Drawing.Point(4, 22);
					this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
					this.tabPage1.Name = "tabPage1";
					this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
					this.tabPage1.Size = new System.Drawing.Size(336, 251);
					this.tabPage1.TabIndex = 0;
					this.tabPage1.Text = "Algorithm";
					this.tabPage1.UseVisualStyleBackColor = true;
					// 
					// cboRoot
					// 
					this.cboRoot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					this.cboRoot.Enabled = false;
					this.cboRoot.FormattingEnabled = true;
					this.cboRoot.Location = new System.Drawing.Point(124, 58);
					this.cboRoot.Margin = new System.Windows.Forms.Padding(2);
					this.cboRoot.Name = "cboRoot";
					this.cboRoot.Size = new System.Drawing.Size(210, 21);
					this.cboRoot.TabIndex = 4;
					// 
					// label2
					// 
					this.label2.AutoSize = true;
					this.label2.Location = new System.Drawing.Point(17, 63);
					this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
					this.label2.Name = "label2";
					this.label2.Size = new System.Drawing.Size(100, 13);
					this.label2.TabIndex = 3;
					this.label2.Text = "Root Feature Class:";
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(17, 33);
					this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(52, 13);
					this.label1.TabIndex = 2;
					this.label1.Text = "Direction:";
					// 
					// cboDirection
					// 
					this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					this.cboDirection.Enabled = false;
					this.cboDirection.FormattingEnabled = true;
					this.cboDirection.Items.AddRange(new object[] {
            "Left to Right",
            "Right to Left",
            "Top to Bottom",
            "Bottom to Top"});
					this.cboDirection.Location = new System.Drawing.Point(73, 28);
					this.cboDirection.Margin = new System.Windows.Forms.Padding(2);
					this.cboDirection.Name = "cboDirection";
					this.cboDirection.Size = new System.Drawing.Size(261, 21);
					this.cboDirection.TabIndex = 1;
					// 
					// chkApplyAlgo
					// 
					this.chkApplyAlgo.AutoSize = true;
					this.chkApplyAlgo.Location = new System.Drawing.Point(5, 6);
					this.chkApplyAlgo.Margin = new System.Windows.Forms.Padding(2);
					this.chkApplyAlgo.Name = "chkApplyAlgo";
					this.chkApplyAlgo.Size = new System.Drawing.Size(152, 17);
					this.chkApplyAlgo.TabIndex = 0;
					this.chkApplyAlgo.Text = "Apply Smart Tree algorithm";
					this.chkApplyAlgo.UseVisualStyleBackColor = true;
					this.chkApplyAlgo.CheckedChanged += new System.EventHandler(this.chkApplyAlgo_CheckedChanged);
					// 
					// tabPage2
					// 
					this.tabPage2.Controls.Add(this.chkUseAttributes);
					this.tabPage2.Controls.Add(this.chkFields);
					this.tabPage2.Controls.Add(this.tvFeatureClasses);
					this.tabPage2.Location = new System.Drawing.Point(4, 22);
					this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
					this.tabPage2.Name = "tabPage2";
					this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
					this.tabPage2.Size = new System.Drawing.Size(336, 266);
					this.tabPage2.TabIndex = 1;
					this.tabPage2.Text = "Attributes";
					this.tabPage2.UseVisualStyleBackColor = true;
					// 
					// chkUseAttributes
					// 
					this.chkUseAttributes.AutoSize = true;
					this.chkUseAttributes.Location = new System.Drawing.Point(4, 5);
					this.chkUseAttributes.Margin = new System.Windows.Forms.Padding(2);
					this.chkUseAttributes.Name = "chkUseAttributes";
					this.chkUseAttributes.Size = new System.Drawing.Size(157, 17);
					this.chkUseAttributes.TabIndex = 2;
					this.chkUseAttributes.Text = "Create associated attributes";
					this.chkUseAttributes.UseVisualStyleBackColor = true;
					this.chkUseAttributes.CheckedChanged += new System.EventHandler(this.chkUseAttributes_CheckedChanged);
					// 
					// chkFields
					// 
					this.chkFields.CheckOnClick = true;
					this.chkFields.Enabled = false;
					this.chkFields.FormattingEnabled = true;
					this.chkFields.Location = new System.Drawing.Point(136, 26);
					this.chkFields.Margin = new System.Windows.Forms.Padding(2);
					this.chkFields.Name = "chkFields";
					this.chkFields.Size = new System.Drawing.Size(198, 229);
					this.chkFields.TabIndex = 1;
					this.chkFields.SelectedIndexChanged += new System.EventHandler(this.chkFields_SelectedIndexChanged);
					// 
					// tvFeatureClasses
					// 
					this.tvFeatureClasses.Enabled = false;
					this.tvFeatureClasses.HideSelection = false;
					this.tvFeatureClasses.Location = new System.Drawing.Point(2, 26);
					this.tvFeatureClasses.Margin = new System.Windows.Forms.Padding(2);
					this.tvFeatureClasses.Name = "tvFeatureClasses";
					this.tvFeatureClasses.Size = new System.Drawing.Size(130, 229);
					this.tvFeatureClasses.TabIndex = 0;
					this.tvFeatureClasses.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFeatureClasses_AfterSelect);
					// 
					// btnDone
					// 
					this.btnDone.Location = new System.Drawing.Point(294, 307);
					this.btnDone.Margin = new System.Windows.Forms.Padding(2);
					this.btnDone.Name = "btnDone";
					this.btnDone.Size = new System.Drawing.Size(56, 19);
					this.btnDone.TabIndex = 1;
					this.btnDone.Text = "Done";
					this.btnDone.UseVisualStyleBackColor = true;
					this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
					// 
					// frmAdvanced
					// 
					this.AcceptButton = this.btnDone;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(355, 333);
					this.ControlBox = false;
					this.Controls.Add(this.btnDone);
					this.Controls.Add(this.tabControl1);
					this.Margin = new System.Windows.Forms.Padding(2);
					this.Name = "frmAdvanced";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
					this.Text = "Advanced Options";
					this.tabControl1.ResumeLayout(false);
					this.tabPage1.ResumeLayout(false);
					this.tabPage1.PerformLayout();
					this.tabPage2.ResumeLayout(false);
					this.tabPage2.PerformLayout();
					this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkApplyAlgo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cboRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDirection;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.CheckBox chkUseAttributes;
        private System.Windows.Forms.CheckedListBox chkFields;
        private System.Windows.Forms.TreeView tvFeatureClasses;

    }
}