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
namespace ServiceAreaSolver
{
    partial class frmServiceAreaSolver
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
            this.btnSolve = new System.Windows.Forms.Button();
            this.ckbUseRestriction = new System.Windows.Forms.CheckBox();
            this.cbCostAttribute = new System.Windows.Forms.ComboBox();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCutOff = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFeatureDataset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInputFacilities = new System.Windows.Forms.TextBox();
            this.txtNetworkDataset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorkspacePath = new System.Windows.Forms.TextBox();
            this.gbServiceAreaSolver = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.groupBox1.SuspendLayout();
            this.gbServiceAreaSolver.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSolve
            // 
            this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Location = new System.Drawing.Point(54, 243);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(89, 34);
            this.btnSolve.TabIndex = 1;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // ckbUseRestriction
            // 
            this.ckbUseRestriction.AutoSize = true;
            this.ckbUseRestriction.Location = new System.Drawing.Point(9, 81);
            this.ckbUseRestriction.Name = "ckbUseRestriction";
            this.ckbUseRestriction.Size = new System.Drawing.Size(98, 17);
            this.ckbUseRestriction.TabIndex = 3;
            this.ckbUseRestriction.Text = "Use Restriction";
            this.ckbUseRestriction.UseVisualStyleBackColor = true;
            // 
            // cbCostAttribute
            // 
            this.cbCostAttribute.FormattingEnabled = true;
            this.cbCostAttribute.Location = new System.Drawing.Point(78, 27);
            this.cbCostAttribute.Name = "cbCostAttribute";
            this.cbCostAttribute.Size = new System.Drawing.Size(121, 21);
            this.cbCostAttribute.TabIndex = 4;
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.Location = new System.Drawing.Point(9, 108);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(189, 121);
            this.lstOutput.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cost Attribute";
            // 
            // txtCutOff
            // 
            this.txtCutOff.Location = new System.Drawing.Point(78, 55);
            this.txtCutOff.Name = "txtCutOff";
            this.txtCutOff.Size = new System.Drawing.Size(120, 20);
            this.txtCutOff.TabIndex = 9;
            this.txtCutOff.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cut Off";
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadMap.Location = new System.Drawing.Point(443, 38);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(94, 38);
            this.btnLoadMap.TabIndex = 11;
            this.btnLoadMap.Text = "Setup Service Area Problem";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFeatureDataset);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInputFacilities);
            this.groupBox1.Controls.Add(this.btnLoadMap);
            this.groupBox1.Controls.Add(this.txtNetworkDataset);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtWorkspacePath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 125);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Configuration";
            // 
            // txtFeatureDataset
            // 
            this.txtFeatureDataset.Location = new System.Drawing.Point(106, 69);
            this.txtFeatureDataset.Name = "txtFeatureDataset";
            this.txtFeatureDataset.Size = new System.Drawing.Size(308, 20);
            this.txtFeatureDataset.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Feature Dataset";
            // 
            // txtInputFacilities
            // 
            this.txtInputFacilities.Location = new System.Drawing.Point(106, 95);
            this.txtInputFacilities.Name = "txtInputFacilities";
            this.txtInputFacilities.Size = new System.Drawing.Size(308, 20);
            this.txtInputFacilities.TabIndex = 13;
            // 
            // txtNetworkDataset
            // 
            this.txtNetworkDataset.Location = new System.Drawing.Point(106, 45);
            this.txtNetworkDataset.Name = "txtNetworkDataset";
            this.txtNetworkDataset.Size = new System.Drawing.Size(308, 20);
            this.txtNetworkDataset.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input Facilities";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Network Dataset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Workspace Path";
            // 
            // txtWorkspacePath
            // 
            this.txtWorkspacePath.Location = new System.Drawing.Point(106, 19);
            this.txtWorkspacePath.Name = "txtWorkspacePath";
            this.txtWorkspacePath.Size = new System.Drawing.Size(308, 20);
            this.txtWorkspacePath.TabIndex = 8;
            // 
            // gbServiceAreaSolver
            // 
            this.gbServiceAreaSolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbServiceAreaSolver.Controls.Add(this.tableLayoutPanel1);
            this.gbServiceAreaSolver.Controls.Add(this.btnSolve);
            this.gbServiceAreaSolver.Controls.Add(this.label3);
            this.gbServiceAreaSolver.Controls.Add(this.label2);
            this.gbServiceAreaSolver.Controls.Add(this.txtCutOff);
            this.gbServiceAreaSolver.Controls.Add(this.ckbUseRestriction);
            this.gbServiceAreaSolver.Controls.Add(this.lstOutput);
            this.gbServiceAreaSolver.Controls.Add(this.cbCostAttribute);
            this.gbServiceAreaSolver.Enabled = false;
            this.gbServiceAreaSolver.Location = new System.Drawing.Point(12, 142);
            this.gbServiceAreaSolver.Name = "gbServiceAreaSolver";
            this.gbServiceAreaSolver.Size = new System.Drawing.Size(555, 295);
            this.gbServiceAreaSolver.TabIndex = 13;
            this.gbServiceAreaSolver.TabStop = false;
            this.gbServiceAreaSolver.Text = "Service Area Solver";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.axMapControl, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(205, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(332, 258);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // axMapControl
            // 
            this.axMapControl.Location = new System.Drawing.Point(2, 2);
            this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.Size = new System.Drawing.Size(265, 254);
            this.axMapControl.TabIndex = 11;
            // 
            // frmServiceAreaSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 449);
            this.Controls.Add(this.gbServiceAreaSolver);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmServiceAreaSolver";
            this.Text = "ArcGIS Engine Network Analyst Service Area Solver";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbServiceAreaSolver.ResumeLayout(false);
            this.gbServiceAreaSolver.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.CheckBox ckbUseRestriction;
        public System.Windows.Forms.ComboBox cbCostAttribute;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCutOff;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNetworkDataset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorkspacePath;
        private System.Windows.Forms.TextBox txtInputFacilities;
		private System.Windows.Forms.GroupBox gbServiceAreaSolver;
		private System.Windows.Forms.TextBox txtFeatureDataset;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
    }
}

