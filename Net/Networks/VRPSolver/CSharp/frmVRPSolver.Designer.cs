namespace VRP_CSharp
{
    partial class frmVRPSolver
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVRPSolver));
			this.checkUseRestriction = new System.Windows.Forms.CheckBox();
			this.checkUseHierarchy = new System.Windows.Forms.CheckBox();
			this.cmdSolve = new System.Windows.Forms.Button();
			this.listOutput = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboTimeAttribute = new System.Windows.Forms.ComboBox();
			this.comboDistanceAttribute = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboTimeUnits = new System.Windows.Forms.ComboBox();
			this.comboDistUnits = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboTWImportance = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
			this.SuspendLayout();
			// 
			// checkUseRestriction
			// 
			this.checkUseRestriction.AutoSize = true;
			this.checkUseRestriction.Location = new System.Drawing.Point(22, 261);
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
			this.checkUseHierarchy.Location = new System.Drawing.Point(22, 297);
			this.checkUseHierarchy.Margin = new System.Windows.Forms.Padding(2);
			this.checkUseHierarchy.Name = "checkUseHierarchy";
			this.checkUseHierarchy.Size = new System.Drawing.Size(93, 17);
			this.checkUseHierarchy.TabIndex = 7;
			this.checkUseHierarchy.Text = "Use Hierarchy";
			this.checkUseHierarchy.UseVisualStyleBackColor = true;
			// 
			// cmdSolve
			// 
			this.cmdSolve.Location = new System.Drawing.Point(74, 346);
			this.cmdSolve.Margin = new System.Windows.Forms.Padding(2);
			this.cmdSolve.Name = "cmdSolve";
			this.cmdSolve.Size = new System.Drawing.Size(128, 24);
			this.cmdSolve.TabIndex = 8;
			this.cmdSolve.Text = "Find VRP Solution";
			this.cmdSolve.UseVisualStyleBackColor = true;
			this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
			// 
			// listOutput
			// 
			this.listOutput.FormattingEnabled = true;
			this.listOutput.Location = new System.Drawing.Point(22, 404);
			this.listOutput.Margin = new System.Windows.Forms.Padding(2);
			this.listOutput.Name = "listOutput";
			this.listOutput.Size = new System.Drawing.Size(636, 108);
			this.listOutput.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 45);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Time Attribute";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 84);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Distance Attribute";
			// 
			// comboTimeAttribute
			// 
			this.comboTimeAttribute.FormattingEnabled = true;
			this.comboTimeAttribute.Location = new System.Drawing.Point(154, 45);
			this.comboTimeAttribute.Margin = new System.Windows.Forms.Padding(2);
			this.comboTimeAttribute.Name = "comboTimeAttribute";
			this.comboTimeAttribute.Size = new System.Drawing.Size(90, 21);
			this.comboTimeAttribute.TabIndex = 12;
			// 
			// comboDistanceAttribute
			// 
			this.comboDistanceAttribute.FormattingEnabled = true;
			this.comboDistanceAttribute.Location = new System.Drawing.Point(154, 84);
			this.comboDistanceAttribute.Margin = new System.Windows.Forms.Padding(2);
			this.comboDistanceAttribute.Name = "comboDistanceAttribute";
			this.comboDistanceAttribute.Size = new System.Drawing.Size(92, 21);
			this.comboDistanceAttribute.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 124);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Time Field Units";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(20, 166);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Distance Field Units";
			// 
			// comboTimeUnits
			// 
			this.comboTimeUnits.FormattingEnabled = true;
			this.comboTimeUnits.Location = new System.Drawing.Point(154, 124);
			this.comboTimeUnits.Margin = new System.Windows.Forms.Padding(2);
			this.comboTimeUnits.Name = "comboTimeUnits";
			this.comboTimeUnits.Size = new System.Drawing.Size(92, 21);
			this.comboTimeUnits.TabIndex = 18;
			// 
			// comboDistUnits
			// 
			this.comboDistUnits.FormattingEnabled = true;
			this.comboDistUnits.Location = new System.Drawing.Point(154, 160);
			this.comboDistUnits.Margin = new System.Windows.Forms.Padding(2);
			this.comboDistUnits.Name = "comboDistUnits";
			this.comboDistUnits.Size = new System.Drawing.Size(92, 21);
			this.comboDistUnits.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20, 202);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 37);
			this.label1.TabIndex = 22;
			this.label1.Text = "Time Window Violations Importance";
			// 
			// comboTWImportance
			// 
			this.comboTWImportance.FormattingEnabled = true;
			this.comboTWImportance.Location = new System.Drawing.Point(154, 202);
			this.comboTWImportance.Margin = new System.Windows.Forms.Padding(2);
			this.comboTWImportance.Name = "comboTWImportance";
			this.comboTWImportance.Size = new System.Drawing.Size(92, 21);
			this.comboTWImportance.TabIndex = 23;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.axMapControl, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(266, 45);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 325);
			this.tableLayoutPanel1.TabIndex = 25;
			// 
			// axMapControl
			// 
			this.axMapControl.Location = new System.Drawing.Point(2, 2);
			this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
			this.axMapControl.Name = "axMapControl";
			this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
			this.axMapControl.Size = new System.Drawing.Size(388, 321);
			this.axMapControl.TabIndex = 11;
			// 
			// frmVRPSolver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 540);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.comboTWImportance);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboDistUnits);
			this.Controls.Add(this.comboTimeUnits);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboDistanceAttribute);
			this.Controls.Add(this.comboTimeAttribute);
			this.Controls.Add(this.listOutput);
			this.Controls.Add(this.cmdSolve);
			this.Controls.Add(this.checkUseHierarchy);
			this.Controls.Add(this.checkUseRestriction);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmVRPSolver";
			this.Text = "ArcGIS Network Analyst extension - VRP Solver Demonstration";
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkUseRestriction;
        private System.Windows.Forms.CheckBox checkUseHierarchy;
        private System.Windows.Forms.Button cmdSolve;
        private System.Windows.Forms.ListBox listOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboTimeAttribute;
        private System.Windows.Forms.ComboBox comboDistanceAttribute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboTimeUnits;
        private System.Windows.Forms.ComboBox comboDistUnits;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboTWImportance;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
    }
}

