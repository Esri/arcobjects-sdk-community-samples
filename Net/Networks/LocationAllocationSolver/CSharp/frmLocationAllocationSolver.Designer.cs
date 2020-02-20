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
using System.Windows.Forms;

namespace LocationAllocationSolver
{
    partial class frmLocationAllocationSolver : Form
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
            this.cmdSolve = new System.Windows.Forms.Button();
            this.txtFacilitiesToLocate = new System.Windows.Forms.TextBox();
            this.lblNumFacilities = new System.Windows.Forms.Label();
            this.txtCutOff = new System.Windows.Forms.TextBox();
            this.lblCutOff = new System.Windows.Forms.Label();
            this.cboProblemType = new System.Windows.Forms.ComboBox();
            this.lblProblemType = new System.Windows.Forms.Label();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.cboCostAttribute = new System.Windows.Forms.ComboBox();
            this.lblCostAttribute = new System.Windows.Forms.Label();
            this.lblImpTransformation = new System.Windows.Forms.Label();
            this.txtImpParameter = new System.Windows.Forms.TextBox();
            this.lblImpParameter = new System.Windows.Forms.Label();
            this.cboImpTransformation = new System.Windows.Forms.ComboBox();
            this.txtTargetMarketShare = new System.Windows.Forms.TextBox();
            this.lblTargetMarketShare = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.lblDefaultCapacity = new System.Windows.Forms.Label();
            this.txtDefaultCapacity = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSolve
            // 
            this.cmdSolve.Location = new System.Drawing.Point(171, 255);
            this.cmdSolve.Margin = new System.Windows.Forms.Padding(2);
            this.cmdSolve.Name = "cmdSolve";
            this.cmdSolve.Size = new System.Drawing.Size(117, 23);
            this.cmdSolve.TabIndex = 0;
            this.cmdSolve.Text = "Solve";
            this.cmdSolve.UseVisualStyleBackColor = true;
            this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
            // 
            // txtFacilitiesToLocate
            // 
            this.txtFacilitiesToLocate.Location = new System.Drawing.Point(171, 69);
            this.txtFacilitiesToLocate.Margin = new System.Windows.Forms.Padding(2);
            this.txtFacilitiesToLocate.Name = "txtFacilitiesToLocate";
            this.txtFacilitiesToLocate.Size = new System.Drawing.Size(117, 20);
            this.txtFacilitiesToLocate.TabIndex = 1;
            // 
            // lblNumFacilities
            // 
            this.lblNumFacilities.AutoSize = true;
            this.lblNumFacilities.Location = new System.Drawing.Point(10, 72);
            this.lblNumFacilities.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumFacilities.Name = "lblNumFacilities";
            this.lblNumFacilities.Size = new System.Drawing.Size(91, 13);
            this.lblNumFacilities.TabIndex = 2;
            this.lblNumFacilities.Text = "Facilities to locate";
            // 
            // txtCutOff
            // 
            this.txtCutOff.Location = new System.Drawing.Point(171, 99);
            this.txtCutOff.Margin = new System.Windows.Forms.Padding(2);
            this.txtCutOff.Name = "txtCutOff";
            this.txtCutOff.Size = new System.Drawing.Size(117, 20);
            this.txtCutOff.TabIndex = 3;
            // 
            // lblCutOff
            // 
            this.lblCutOff.AutoSize = true;
            this.lblCutOff.Location = new System.Drawing.Point(11, 102);
            this.lblCutOff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCutOff.Name = "lblCutOff";
            this.lblCutOff.Size = new System.Drawing.Size(90, 13);
            this.lblCutOff.TabIndex = 4;
            this.lblCutOff.Text = "Impedance cutoff";
            // 
            // cboProblemType
            // 
            this.cboProblemType.FormattingEnabled = true;
            this.cboProblemType.Location = new System.Drawing.Point(171, 39);
            this.cboProblemType.Margin = new System.Windows.Forms.Padding(2);
            this.cboProblemType.Name = "cboProblemType";
            this.cboProblemType.Size = new System.Drawing.Size(117, 21);
            this.cboProblemType.TabIndex = 5;
            this.cboProblemType.SelectedIndexChanged += new System.EventHandler(this.cboProblemType_SelectedIndexChanged);
            // 
            // lblProblemType
            // 
            this.lblProblemType.AutoSize = true;
            this.lblProblemType.Location = new System.Drawing.Point(9, 42);
            this.lblProblemType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProblemType.Name = "lblProblemType";
            this.lblProblemType.Size = new System.Drawing.Size(68, 13);
            this.lblProblemType.TabIndex = 6;
            this.lblProblemType.Text = "Problem type";
            // 
            // lstOutput
            // 
            this.lstOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(11, 296);
            this.lstOutput.Margin = new System.Windows.Forms.Padding(2);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.ScrollAlwaysVisible = true;
            this.lstOutput.Size = new System.Drawing.Size(278, 212);
            this.lstOutput.TabIndex = 7;
            // 
            // cboCostAttribute
            // 
            this.cboCostAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCostAttribute.FormattingEnabled = true;
            this.cboCostAttribute.Location = new System.Drawing.Point(171, 11);
            this.cboCostAttribute.Margin = new System.Windows.Forms.Padding(2);
            this.cboCostAttribute.Name = "cboCostAttribute";
            this.cboCostAttribute.Size = new System.Drawing.Size(117, 21);
            this.cboCostAttribute.TabIndex = 8;
            // 
            // lblCostAttribute
            // 
            this.lblCostAttribute.AutoSize = true;
            this.lblCostAttribute.Location = new System.Drawing.Point(10, 14);
            this.lblCostAttribute.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCostAttribute.Name = "lblCostAttribute";
            this.lblCostAttribute.Size = new System.Drawing.Size(69, 13);
            this.lblCostAttribute.TabIndex = 9;
            this.lblCostAttribute.Text = "Cost attribute";
            // 
            // lblImpTransformation
            // 
            this.lblImpTransformation.AutoSize = true;
            this.lblImpTransformation.Location = new System.Drawing.Point(10, 130);
            this.lblImpTransformation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImpTransformation.Name = "lblImpTransformation";
            this.lblImpTransformation.Size = new System.Drawing.Size(129, 13);
            this.lblImpTransformation.TabIndex = 12;
            this.lblImpTransformation.Text = "Impedance transformation";
            // 
            // txtImpParameter
            // 
            this.txtImpParameter.Location = new System.Drawing.Point(171, 158);
            this.txtImpParameter.Margin = new System.Windows.Forms.Padding(2);
            this.txtImpParameter.Name = "txtImpParameter";
            this.txtImpParameter.Size = new System.Drawing.Size(117, 20);
            this.txtImpParameter.TabIndex = 13;
            // 
            // lblImpParameter
            // 
            this.lblImpParameter.AutoSize = true;
            this.lblImpParameter.Location = new System.Drawing.Point(10, 161);
            this.lblImpParameter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImpParameter.Name = "lblImpParameter";
            this.lblImpParameter.Size = new System.Drawing.Size(110, 13);
            this.lblImpParameter.TabIndex = 14;
            this.lblImpParameter.Text = "Impedance parameter";
            // 
            // cboImpTransformation
            // 
            this.cboImpTransformation.FormattingEnabled = true;
            this.cboImpTransformation.Location = new System.Drawing.Point(171, 127);
            this.cboImpTransformation.Margin = new System.Windows.Forms.Padding(2);
            this.cboImpTransformation.Name = "cboImpTransformation";
            this.cboImpTransformation.Size = new System.Drawing.Size(117, 21);
            this.cboImpTransformation.TabIndex = 15;
            // 
            // txtTargetMarketShare
            // 
            this.txtTargetMarketShare.Location = new System.Drawing.Point(171, 188);
            this.txtTargetMarketShare.Margin = new System.Windows.Forms.Padding(2);
            this.txtTargetMarketShare.Name = "txtTargetMarketShare";
            this.txtTargetMarketShare.Size = new System.Drawing.Size(117, 20);
            this.txtTargetMarketShare.TabIndex = 16;
            // 
            // lblTargetMarketShare
            // 
            this.lblTargetMarketShare.AutoSize = true;
            this.lblTargetMarketShare.Location = new System.Drawing.Point(10, 191);
            this.lblTargetMarketShare.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTargetMarketShare.Name = "lblTargetMarketShare";
            this.lblTargetMarketShare.Size = new System.Drawing.Size(102, 13);
            this.lblTargetMarketShare.TabIndex = 17;
            this.lblTargetMarketShare.Text = "Target market share";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.axMapControl, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(304, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(352, 495);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // axMapControl
            // 
            this.axMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl.Location = new System.Drawing.Point(2, 2);
            this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.Size = new System.Drawing.Size(348, 491);
            this.axMapControl.TabIndex = 12;
            // 
            // lblDefaultCapacity
            // 
            this.lblDefaultCapacity.AutoSize = true;
            this.lblDefaultCapacity.Location = new System.Drawing.Point(11, 222);
            this.lblDefaultCapacity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDefaultCapacity.Name = "lblDefaultCapacity";
            this.lblDefaultCapacity.Size = new System.Drawing.Size(84, 13);
            this.lblDefaultCapacity.TabIndex = 19;
            this.lblDefaultCapacity.Text = "Default capacity";
            // 
            // txtDefaultCapacity
            // 
            this.txtDefaultCapacity.Location = new System.Drawing.Point(171, 219);
            this.txtDefaultCapacity.Margin = new System.Windows.Forms.Padding(2);
            this.txtDefaultCapacity.Name = "txtDefaultCapacity";
            this.txtDefaultCapacity.Size = new System.Drawing.Size(117, 20);
            this.txtDefaultCapacity.TabIndex = 20;
            // 
            // frmLocationAllocationSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 514);
            this.Controls.Add(this.txtDefaultCapacity);
            this.Controls.Add(this.lblDefaultCapacity);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblTargetMarketShare);
            this.Controls.Add(this.txtTargetMarketShare);
            this.Controls.Add(this.cboImpTransformation);
            this.Controls.Add(this.lblImpParameter);
            this.Controls.Add(this.txtImpParameter);
            this.Controls.Add(this.lblImpTransformation);
            this.Controls.Add(this.lblCostAttribute);
            this.Controls.Add(this.cboCostAttribute);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.lblProblemType);
            this.Controls.Add(this.cboProblemType);
            this.Controls.Add(this.lblCutOff);
            this.Controls.Add(this.txtCutOff);
            this.Controls.Add(this.lblNumFacilities);
            this.Controls.Add(this.txtFacilitiesToLocate);
            this.Controls.Add(this.cmdSolve);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmLocationAllocationSolver";
            this.Text = "Location-Allocation Solver";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private Button cmdSolve;
        private TextBox txtFacilitiesToLocate;
        private Label lblNumFacilities;
        private TextBox txtCutOff;
        private Label lblCutOff;
        private ComboBox cboProblemType;
        private Label lblProblemType;
        private ListBox lstOutput;
        private ComboBox cboCostAttribute;
		private Label lblCostAttribute;
        private Label lblImpTransformation;
        private TextBox txtImpParameter;
        private Label lblImpParameter;
        private ComboBox cboImpTransformation;
        private TextBox txtTargetMarketShare;
        private Label lblTargetMarketShare;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblDefaultCapacity;
        private TextBox txtDefaultCapacity;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;

    }
}