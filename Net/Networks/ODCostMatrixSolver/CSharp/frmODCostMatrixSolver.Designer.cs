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
namespace OD_Cost_Matrix_CSharp
{
    partial class frmODCostMatrixSolver
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmODCostMatrixSolver));
			this.comboCostAttribute = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textTargetFacility = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textCutoff = new System.Windows.Forms.TextBox();
			this.checkUseRestriction = new System.Windows.Forms.CheckBox();
			this.checkUseHierarchy = new System.Windows.Forms.CheckBox();
			this.cmdSolve = new System.Windows.Forms.Button();
			this.lstOutput = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
			this.SuspendLayout();
			// 
			// comboCostAttribute
			// 
			this.comboCostAttribute.FormattingEnabled = true;
			this.comboCostAttribute.Location = new System.Drawing.Point(135, 17);
			this.comboCostAttribute.Margin = new System.Windows.Forms.Padding(2);
			this.comboCostAttribute.Name = "comboCostAttribute";
			this.comboCostAttribute.Size = new System.Drawing.Size(90, 21);
			this.comboCostAttribute.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 23);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Cost Attribute";
			// 
			// textTargetFacility
			// 
			this.textTargetFacility.Location = new System.Drawing.Point(135, 49);
			this.textTargetFacility.Margin = new System.Windows.Forms.Padding(2);
			this.textTargetFacility.Name = "textTargetFacility";
			this.textTargetFacility.Size = new System.Drawing.Size(90, 20);
			this.textTargetFacility.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 52);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Target Facility Count";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 85);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Cutoff";
			// 
			// textCutoff
			// 
			this.textCutoff.Location = new System.Drawing.Point(135, 81);
			this.textCutoff.Margin = new System.Windows.Forms.Padding(2);
			this.textCutoff.Name = "textCutoff";
			this.textCutoff.Size = new System.Drawing.Size(90, 20);
			this.textCutoff.TabIndex = 5;
			// 
			// checkUseRestriction
			// 
			this.checkUseRestriction.AutoSize = true;
			this.checkUseRestriction.Checked = true;
			this.checkUseRestriction.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkUseRestriction.Location = new System.Drawing.Point(22, 124);
			this.checkUseRestriction.Margin = new System.Windows.Forms.Padding(2);
			this.checkUseRestriction.Name = "checkUseRestriction";
			this.checkUseRestriction.Size = new System.Drawing.Size(140, 17);
			this.checkUseRestriction.TabIndex = 6;
			this.checkUseRestriction.Text = "Use Oneway Restriction";
			this.checkUseRestriction.UseVisualStyleBackColor = true;
			// 
			// checkUseHierarchy
			// 
			this.checkUseHierarchy.AutoSize = true;
			this.checkUseHierarchy.Location = new System.Drawing.Point(22, 153);
			this.checkUseHierarchy.Margin = new System.Windows.Forms.Padding(2);
			this.checkUseHierarchy.Name = "checkUseHierarchy";
			this.checkUseHierarchy.Size = new System.Drawing.Size(93, 17);
			this.checkUseHierarchy.TabIndex = 7;
			this.checkUseHierarchy.Text = "Use Hierarchy";
			this.checkUseHierarchy.UseVisualStyleBackColor = true;
			// 
			// cmdSolve
			// 
			this.cmdSolve.Location = new System.Drawing.Point(58, 184);
			this.cmdSolve.Margin = new System.Windows.Forms.Padding(2);
			this.cmdSolve.Name = "cmdSolve";
			this.cmdSolve.Size = new System.Drawing.Size(128, 24);
			this.cmdSolve.TabIndex = 8;
			this.cmdSolve.Text = "Find OD Cost Matrix";
			this.cmdSolve.UseVisualStyleBackColor = true;
			this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
			// 
			// listOutput
			// 
			this.lstOutput.FormattingEnabled = true;
			this.lstOutput.Location = new System.Drawing.Point(22, 223);
			this.lstOutput.Margin = new System.Windows.Forms.Padding(2);
			this.lstOutput.Name = "listOutput";
			this.lstOutput.Size = new System.Drawing.Size(202, 160);
			this.lstOutput.TabIndex = 9;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.axMapControl, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(230, 17);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(307, 367);
			this.tableLayoutPanel1.TabIndex = 23;
			// 
			// axMapControl
			// 
			this.axMapControl.Location = new System.Drawing.Point(2, 2);
			this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
			this.axMapControl.Name = "axMapControl";
			this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
			this.axMapControl.Size = new System.Drawing.Size(303, 363);
			this.axMapControl.TabIndex = 11;
			// 
			// frmODCostMatrixSolver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(549, 396);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.lstOutput);
			this.Controls.Add(this.cmdSolve);
			this.Controls.Add(this.checkUseHierarchy);
			this.Controls.Add(this.checkUseRestriction);
			this.Controls.Add(this.textCutoff);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textTargetFacility);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboCostAttribute);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmODCostMatrixSolver";
			this.Text = "ArcGIS Network Analyst extension - OD Cost Matrix Demonstration";
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCostAttribute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTargetFacility;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textCutoff;
        private System.Windows.Forms.CheckBox checkUseRestriction;
        private System.Windows.Forms.CheckBox checkUseHierarchy;
        private System.Windows.Forms.Button cmdSolve;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
    }
}

