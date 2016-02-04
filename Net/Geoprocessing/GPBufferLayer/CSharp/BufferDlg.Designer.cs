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
namespace GpBufferLayer
{
  partial class BufferDlg
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnBuffer = new System.Windows.Forms.Button();
      this.btnOutputLayer = new System.Windows.Forms.Button();
      this.txtOutputPath = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cboUnits = new System.Windows.Forms.ComboBox();
      this.txtBufferDistance = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblLayers = new System.Windows.Forms.Label();
      this.cboLayers = new System.Windows.Forms.ComboBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtMessages = new System.Windows.Forms.TextBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnBuffer);
      this.groupBox1.Controls.Add(this.btnOutputLayer);
      this.groupBox1.Controls.Add(this.txtOutputPath);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cboUnits);
      this.groupBox1.Controls.Add(this.txtBufferDistance);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.lblLayers);
      this.groupBox1.Controls.Add(this.cboLayers);
      this.groupBox1.Location = new System.Drawing.Point(2, -1);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(356, 166);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      // 
      // btnBuffer
      // 
      this.btnBuffer.Location = new System.Drawing.Point(133, 130);
      this.btnBuffer.Name = "btnBuffer";
      this.btnBuffer.Size = new System.Drawing.Size(88, 23);
      this.btnBuffer.TabIndex = 8;
      this.btnBuffer.Text = "Buffer";
      this.btnBuffer.UseVisualStyleBackColor = true;
      this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
      // 
      // btnOutputLayer
      // 
      this.btnOutputLayer.Location = new System.Drawing.Point(319, 86);
      this.btnOutputLayer.Name = "btnOutputLayer";
      this.btnOutputLayer.Size = new System.Drawing.Size(24, 23);
      this.btnOutputLayer.TabIndex = 7;
      this.btnOutputLayer.Text = ">";
      this.btnOutputLayer.UseVisualStyleBackColor = true;
      this.btnOutputLayer.Click += new System.EventHandler(this.btnOutputLayer_Click);
      // 
      // txtOutputPath
      // 
      this.txtOutputPath.Location = new System.Drawing.Point(93, 88);
      this.txtOutputPath.Name = "txtOutputPath";
      this.txtOutputPath.ReadOnly = true;
      this.txtOutputPath.Size = new System.Drawing.Size(224, 20);
      this.txtOutputPath.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 88);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Output layer:";
      // 
      // cboUnits
      // 
      this.cboUnits.FormattingEnabled = true;
      this.cboUnits.Items.AddRange(new object[] {
            "Unknown",
            "Inches",
            "Points",
            "Feet",
            "Yards",
            "Miles",
            "NauticalMiles",
            "Millimeters",
            "Centimeters",
            "Meters",
            "Kilometers",
            "DecimalDegrees",
            "Decimeters"});
      this.cboUnits.Location = new System.Drawing.Point(158, 51);
      this.cboUnits.Name = "cboUnits";
      this.cboUnits.Size = new System.Drawing.Size(118, 21);
      this.cboUnits.TabIndex = 4;
      // 
      // txtBufferDistance
      // 
      this.txtBufferDistance.Location = new System.Drawing.Point(93, 51);
      this.txtBufferDistance.Name = "txtBufferDistance";
      this.txtBufferDistance.Size = new System.Drawing.Size(55, 20);
      this.txtBufferDistance.TabIndex = 3;
      this.txtBufferDistance.Text = "0.1";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 54);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(81, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Buffer distance:";
      // 
      // lblLayers
      // 
      this.lblLayers.AutoSize = true;
      this.lblLayers.Location = new System.Drawing.Point(7, 19);
      this.lblLayers.Name = "lblLayers";
      this.lblLayers.Size = new System.Drawing.Size(36, 13);
      this.lblLayers.TabIndex = 1;
      this.lblLayers.Text = "Layer:";
      // 
      // cboLayers
      // 
      this.cboLayers.FormattingEnabled = true;
      this.cboLayers.Location = new System.Drawing.Point(93, 19);
      this.cboLayers.Name = "cboLayers";
      this.cboLayers.Size = new System.Drawing.Size(250, 21);
      this.cboLayers.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtMessages);
      this.groupBox2.Location = new System.Drawing.Point(2, 167);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(356, 146);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Messages";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(135, 319);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Dismiss";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // txtMessages
      // 
      this.txtMessages.Location = new System.Drawing.Point(6, 15);
      this.txtMessages.Multiline = true;
      this.txtMessages.Name = "txtMessages";
      this.txtMessages.ReadOnly = true;
      this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtMessages.Size = new System.Drawing.Size(344, 125);
      this.txtMessages.TabIndex = 0;
      // 
      // BufferDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(361, 347);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "BufferDlg";
      this.ShowInTaskbar = false;
      this.Text = "Buffer";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.bufferDlg_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cboLayers;
    private System.Windows.Forms.ComboBox cboUnits;
    private System.Windows.Forms.TextBox txtBufferDistance;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblLayers;
    private System.Windows.Forms.Button btnOutputLayer;
    private System.Windows.Forms.TextBox txtOutputPath;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBuffer;
    private System.Windows.Forms.TextBox txtMessages;
  }
}