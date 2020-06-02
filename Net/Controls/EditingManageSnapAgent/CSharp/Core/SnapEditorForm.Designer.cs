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
using ESRI.ArcGIS.SystemUI;
namespace Core
{
  partial class SnapEditor
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
      this.clearAgents = new System.Windows.Forms.Button();
      this.turnOffAgents = new System.Windows.Forms.Button();
      this.addFeatureSnapAgent = new System.Windows.Forms.Button();
      this.reverseAgentsPriority = new System.Windows.Forms.Button();
      this.snapToleranceLabel = new System.Windows.Forms.Label();
      this.snapTips = new System.Windows.Forms.CheckBox();
      this.snapTolUnits = new System.Windows.Forms.ComboBox();
      this.snapTolerance = new System.Windows.Forms.MaskedTextBox();
      this.snapAgents = new System.Windows.Forms.DataGridView();
      this.snapAgentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ftrSnapAgentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.addSketchSnapAgent = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.snapAgents)).BeginInit();
      this.SuspendLayout();
      // 
      // clearAgents
      // 
      this.clearAgents.Location = new System.Drawing.Point(12, 233);
      this.clearAgents.Name = "clearAgents";
      this.clearAgents.Size = new System.Drawing.Size(131, 23);
      this.clearAgents.TabIndex = 0;
      this.clearAgents.Text = "Clear Agents";
      this.clearAgents.UseVisualStyleBackColor = true;
      this.clearAgents.Click += new System.EventHandler(this.clearAgents_Click);
      // 
      // turnOffAgents
      // 
      this.turnOffAgents.Location = new System.Drawing.Point(149, 233);
      this.turnOffAgents.Name = "turnOffAgents";
      this.turnOffAgents.Size = new System.Drawing.Size(131, 23);
      this.turnOffAgents.TabIndex = 1;
      this.turnOffAgents.Text = "Turn Off Agents";
      this.turnOffAgents.UseVisualStyleBackColor = true;
      this.turnOffAgents.Click += new System.EventHandler(this.turnOffAgents_Click);
      // 
      // addFeatureSnapAgent
      // 
      this.addFeatureSnapAgent.Location = new System.Drawing.Point(418, 147);
      this.addFeatureSnapAgent.Name = "addFeatureSnapAgent";
      this.addFeatureSnapAgent.Size = new System.Drawing.Size(147, 23);
      this.addFeatureSnapAgent.TabIndex = 2;
      this.addFeatureSnapAgent.Text = "Add Feature Snap Agent";
      this.addFeatureSnapAgent.UseVisualStyleBackColor = true;
      this.addFeatureSnapAgent.Click += new System.EventHandler(this.addFeatureSnapAgent_Click);
      // 
      // reverseAgentsPriority
      // 
      this.reverseAgentsPriority.Location = new System.Drawing.Point(286, 233);
      this.reverseAgentsPriority.Name = "reverseAgentsPriority";
      this.reverseAgentsPriority.Size = new System.Drawing.Size(131, 23);
      this.reverseAgentsPriority.TabIndex = 3;
      this.reverseAgentsPriority.Text = "Reverse Agents\' Priority";
      this.reverseAgentsPriority.UseVisualStyleBackColor = true;
      this.reverseAgentsPriority.Click += new System.EventHandler(this.reverseAgentsPriority_Click);
      // 
      // snapToleranceLabel
      // 
      this.snapToleranceLabel.AutoSize = true;
      this.snapToleranceLabel.Location = new System.Drawing.Point(12, 44);
      this.snapToleranceLabel.Name = "snapToleranceLabel";
      this.snapToleranceLabel.Size = new System.Drawing.Size(83, 13);
      this.snapToleranceLabel.TabIndex = 7;
      this.snapToleranceLabel.Text = "Snap Tolerance";
      // 
      // snapTips
      // 
      this.snapTips.AutoSize = true;
      this.snapTips.Location = new System.Drawing.Point(15, 16);
      this.snapTips.Name = "snapTips";
      this.snapTips.Size = new System.Drawing.Size(74, 17);
      this.snapTips.TabIndex = 9;
      this.snapTips.Text = "Snap Tips";
      this.snapTips.UseVisualStyleBackColor = true;
      this.snapTips.CheckedChanged += new System.EventHandler(this.snapTips_CheckedChanged);
      // 
      // snapTolUnits
      // 
      this.snapTolUnits.FormattingEnabled = true;
      this.snapTolUnits.Items.AddRange(new object[] {
            "Pixels",
            "Map Units"});
      this.snapTolUnits.Location = new System.Drawing.Point(159, 41);
      this.snapTolUnits.Name = "snapTolUnits";
      this.snapTolUnits.Size = new System.Drawing.Size(121, 21);
      this.snapTolUnits.TabIndex = 12;
      this.snapTolUnits.SelectedIndexChanged += new System.EventHandler(this.snapTolUnits_SelectedIndexChanged);
      // 
      // snapTolerance
      // 
      this.snapTolerance.AllowPromptAsInput = false;
      this.snapTolerance.AsciiOnly = true;
      this.snapTolerance.Location = new System.Drawing.Point(101, 41);
      this.snapTolerance.Mask = "00000";
      this.snapTolerance.Name = "snapTolerance";
      this.snapTolerance.Size = new System.Drawing.Size(52, 20);
      this.snapTolerance.TabIndex = 14;
      this.snapTolerance.ValidatingType = typeof(int);
      this.snapTolerance.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.snapTolerance_TypeValidationEventHandler);
      // 
      // snapAgents
      // 
      this.snapAgents.AllowUserToAddRows = false;
      this.snapAgents.AllowUserToDeleteRows = false;
      this.snapAgents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
      this.snapAgents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.snapAgentNameColumn,
            this.Column1,
            this.ftrSnapAgentColumn});
      this.snapAgents.Location = new System.Drawing.Point(17, 68);
      this.snapAgents.MultiSelect = false;
      this.snapAgents.Name = "snapAgents";
      this.snapAgents.ReadOnly = true;
      this.snapAgents.Size = new System.Drawing.Size(400, 141);
      this.snapAgents.TabIndex = 15;
      // 
      // snapAgentNameColumn
      // 
      this.snapAgentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.snapAgentNameColumn.HeaderText = "Snap Agent Name";
      this.snapAgentNameColumn.MinimumWidth = 100;
      this.snapAgentNameColumn.Name = "snapAgentNameColumn";
      this.snapAgentNameColumn.ReadOnly = true;
      this.snapAgentNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // Column1
      // 
      this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
      this.Column1.HeaderText = "Feature Class";
      this.Column1.MinimumWidth = 85;
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column1.Width = 85;
      // 
      // ftrSnapAgentColumn
      // 
      this.ftrSnapAgentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
      this.ftrSnapAgentColumn.HeaderText = "Feature Agent Hit Type";
      this.ftrSnapAgentColumn.MinimumWidth = 145;
      this.ftrSnapAgentColumn.Name = "ftrSnapAgentColumn";
      this.ftrSnapAgentColumn.ReadOnly = true;
      this.ftrSnapAgentColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.ftrSnapAgentColumn.Width = 145;
      // 
      // addSketchSnapAgent
      // 
      this.addSketchSnapAgent.Location = new System.Drawing.Point(418, 176);
      this.addSketchSnapAgent.Name = "addSketchSnapAgent";
      this.addSketchSnapAgent.Size = new System.Drawing.Size(146, 23);
      this.addSketchSnapAgent.TabIndex = 17;
      this.addSketchSnapAgent.Text = "Add Sketch Snap Agent";
      this.addSketchSnapAgent.UseVisualStyleBackColor = true;
      this.addSketchSnapAgent.Click += new System.EventHandler(this.addSketchSnapAgent_Click);
      // 
      // SnapEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 269);
      this.Controls.Add(this.addSketchSnapAgent);
      this.Controls.Add(this.snapAgents);
      this.Controls.Add(this.snapTolerance);
      this.Controls.Add(this.snapTolUnits);
      this.Controls.Add(this.snapTips);
      this.Controls.Add(this.snapToleranceLabel);
      this.Controls.Add(this.reverseAgentsPriority);
      this.Controls.Add(this.addFeatureSnapAgent);
      this.Controls.Add(this.turnOffAgents);
      this.Controls.Add(this.clearAgents);
      this.Name = "SnapEditor";
      this.Text = "Snap Editor";
      ((System.ComponentModel.ISupportInitialize)(this.snapAgents)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button clearAgents;
    private System.Windows.Forms.Button turnOffAgents;
    private System.Windows.Forms.Button addFeatureSnapAgent;
    private System.Windows.Forms.Button reverseAgentsPriority;
    private System.Windows.Forms.Label snapToleranceLabel;
    private System.Windows.Forms.CheckBox snapTips;
    private System.Windows.Forms.ComboBox snapTolUnits;
    private System.Windows.Forms.MaskedTextBox snapTolerance;
    private System.Windows.Forms.DataGridView snapAgents;
    private System.Windows.Forms.Button addSketchSnapAgent;
    private System.Windows.Forms.DataGridViewTextBoxColumn snapAgentNameColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn ftrSnapAgentColumn;
  }
}