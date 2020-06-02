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
namespace ClosestFacilitySolver
{
    partial class frmClosestFacilitySolver
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
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.cmdSolve = new System.Windows.Forms.Button();
            this.txtCutOff = new System.Windows.Forms.TextBox();
            this.txtTargetFacility = new System.Windows.Forms.TextBox();
            this.cboCostAttribute = new System.Windows.Forms.ComboBox();
            this.chkUseHierarchy = new System.Windows.Forms.CheckBox();
            this.chkUseRestriction = new System.Windows.Forms.CheckBox();
            this.lblCutOff = new System.Windows.Forms.Label();
            this.lblNumFacility = new System.Windows.Forms.Label();
            this.lblCostAttribute = new System.Windows.Forms.Label();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstOutput
            // 
            this.lstOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(20, 229);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(300, 186);
            this.lstOutput.TabIndex = 21;
            // 
            // cmdSolve
            // 
            this.cmdSolve.Location = new System.Drawing.Point(20, 194);
            this.cmdSolve.Name = "cmdSolve";
            this.cmdSolve.Size = new System.Drawing.Size(147, 21);
            this.cmdSolve.TabIndex = 20;
            this.cmdSolve.Text = "Find Closest Facilities";
            this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
            // 
            // txtCutOff
            // 
            this.txtCutOff.Location = new System.Drawing.Point(187, 90);
            this.txtCutOff.Name = "txtCutOff";
            this.txtCutOff.Size = new System.Drawing.Size(133, 20);
            this.txtCutOff.TabIndex = 19;
            // 
            // txtTargetFacility
            // 
            this.txtTargetFacility.Location = new System.Drawing.Point(187, 55);
            this.txtTargetFacility.Name = "txtTargetFacility";
            this.txtTargetFacility.Size = new System.Drawing.Size(133, 20);
            this.txtTargetFacility.TabIndex = 18;
            // 
            // cboCostAttribute
            // 
            this.cboCostAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCostAttribute.Location = new System.Drawing.Point(187, 28);
            this.cboCostAttribute.Name = "cboCostAttribute";
            this.cboCostAttribute.Size = new System.Drawing.Size(133, 21);
            this.cboCostAttribute.TabIndex = 17;
            // 
            // chkUseHierarchy
            // 
            this.chkUseHierarchy.Location = new System.Drawing.Point(20, 159);
            this.chkUseHierarchy.Name = "chkUseHierarchy";
            this.chkUseHierarchy.Size = new System.Drawing.Size(140, 21);
            this.chkUseHierarchy.TabIndex = 16;
            this.chkUseHierarchy.Text = "Use Hierarchy";
            // 
            // chkUseRestriction
            // 
            this.chkUseRestriction.Location = new System.Drawing.Point(20, 132);
            this.chkUseRestriction.Name = "chkUseRestriction";
            this.chkUseRestriction.Size = new System.Drawing.Size(164, 21);
            this.chkUseRestriction.TabIndex = 15;
            this.chkUseRestriction.Text = "Use Oneway Restriction";
            // 
            // lblCutOff
            // 
            this.lblCutOff.Location = new System.Drawing.Point(20, 97);
            this.lblCutOff.Name = "lblCutOff";
            this.lblCutOff.Size = new System.Drawing.Size(133, 21);
            this.lblCutOff.TabIndex = 14;
            this.lblCutOff.Text = "Cutoff";
            // 
            // lblNumFacility
            // 
            this.lblNumFacility.Location = new System.Drawing.Point(20, 62);
            this.lblNumFacility.Name = "lblNumFacility";
            this.lblNumFacility.Size = new System.Drawing.Size(133, 20);
            this.lblNumFacility.TabIndex = 13;
            this.lblNumFacility.Text = "Target Facility Count";
            // 
            // lblCostAttribute
            // 
            this.lblCostAttribute.Location = new System.Drawing.Point(20, 28);
            this.lblCostAttribute.Name = "lblCostAttribute";
            this.lblCostAttribute.Size = new System.Drawing.Size(133, 21);
            this.lblCostAttribute.TabIndex = 12;
            this.lblCostAttribute.Text = "Cost Attribute";
            // 
            // axMapControl
            // 
            this.axMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl.Location = new System.Drawing.Point(2, 2);
            this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.Size = new System.Drawing.Size(391, 383);
            this.axMapControl.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.axMapControl, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(326, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(395, 387);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // frmClosestFacilitySolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 424);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.cmdSolve);
            this.Controls.Add(this.txtCutOff);
            this.Controls.Add(this.txtTargetFacility);
            this.Controls.Add(this.cboCostAttribute);
            this.Controls.Add(this.chkUseHierarchy);
            this.Controls.Add(this.chkUseRestriction);
            this.Controls.Add(this.lblCutOff);
            this.Controls.Add(this.lblNumFacility);
            this.Controls.Add(this.lblCostAttribute);
            this.Name = "frmClosestFacilitySolver";
            this.Text = "ArcGIS Network Analyst extension - Closest Facility Demonstration";
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Button cmdSolve;
        private System.Windows.Forms.TextBox txtCutOff;
        private System.Windows.Forms.TextBox txtTargetFacility;
        private System.Windows.Forms.ComboBox cboCostAttribute;
        private System.Windows.Forms.CheckBox chkUseHierarchy;
        private System.Windows.Forms.CheckBox chkUseRestriction;
        private System.Windows.Forms.Label lblCutOff;
        private System.Windows.Forms.Label lblNumFacility;
        private System.Windows.Forms.Label lblCostAttribute;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}

