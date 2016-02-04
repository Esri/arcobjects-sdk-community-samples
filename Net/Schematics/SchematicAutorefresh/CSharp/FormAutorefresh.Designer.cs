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
namespace Autorefresh
{
  partial class FormAutoRefresh
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void  Dispose(bool disposing)
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
        this.components = new System.ComponentModel.Container();
        this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
        this.IntervalMinute = new System.Windows.Forms.ComboBox();
        this.IntervalSecond = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.buttonOK = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.AutoOn = new System.Windows.Forms.RadioButton();
        this.AutoOff = new System.Windows.Forms.RadioButton();
        this.SuspendLayout();
        // 
        // timerAutoRefresh
        // 
        this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
        // 
        // IntervalMinute
        // 
        this.IntervalMinute.FormattingEnabled = true;
        this.IntervalMinute.Location = new System.Drawing.Point(91, 26);
        this.IntervalMinute.Name = "IntervalMinute";
        this.IntervalMinute.Size = new System.Drawing.Size(50, 21);
        this.IntervalMinute.TabIndex = 0;
        this.IntervalMinute.SelectedIndexChanged += new System.EventHandler(this.IntervalMinute_SelectedIndexChanged);
        // 
        // IntervalSecond
        // 
        this.IntervalSecond.FormattingEnabled = true;
        this.IntervalSecond.Location = new System.Drawing.Point(218, 24);
        this.IntervalSecond.Name = "IntervalSecond";
        this.IntervalSecond.Size = new System.Drawing.Size(57, 21);
        this.IntervalSecond.TabIndex = 1;
        this.IntervalSecond.SelectedIndexChanged += new System.EventHandler(this.IntervalSecond_SelectedIndexChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(16, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(68, 13);
        this.label1.TabIndex = 2;
        this.label1.Text = "Time Interval";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(150, 30);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(49, 13);
        this.label2.TabIndex = 3;
        this.label2.Text = "minute(s)";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(285, 28);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(53, 13);
        this.label3.TabIndex = 4;
        this.label3.Text = "second(s)";
        // 
        // buttonOK
        // 
        this.buttonOK.Location = new System.Drawing.Point(167, 145);
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Size = new System.Drawing.Size(77, 26);
        this.buttonOK.TabIndex = 5;
        this.buttonOK.Text = "OK";
        this.buttonOK.UseVisualStyleBackColor = true;
        this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
        // 
        // buttonCancel
        // 
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonCancel.Location = new System.Drawing.Point(261, 145);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(75, 26);
        this.buttonCancel.TabIndex = 6;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // AutoOn
        // 
        this.AutoOn.AutoSize = true;
        this.AutoOn.Location = new System.Drawing.Point(25, 88);
        this.AutoOn.Name = "AutoOn";
        this.AutoOn.Size = new System.Drawing.Size(104, 17);
        this.AutoOn.TabIndex = 7;
        this.AutoOn.Text = "Auto Refresh On";
        this.AutoOn.UseVisualStyleBackColor = true;
        // 
        // AutoOff
        // 
        this.AutoOff.AutoSize = true;
        this.AutoOff.Checked = true;
        this.AutoOff.Location = new System.Drawing.Point(153, 86);
        this.AutoOff.Name = "AutoOff";
        this.AutoOff.Size = new System.Drawing.Size(104, 17);
        this.AutoOff.TabIndex = 8;
        this.AutoOff.TabStop = true;
        this.AutoOff.Text = "Auto Refresh Off";
        this.AutoOff.UseVisualStyleBackColor = true;
        // 
        // FormAutoRefresh
        // 
        this.AcceptButton = this.buttonOK;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new System.Drawing.Size(351, 182);
        this.Controls.Add(this.AutoOff);
        this.Controls.Add(this.AutoOn);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.IntervalSecond);
        this.Controls.Add(this.IntervalMinute);
        this.Name = "FormAutoRefresh";
        this.Text = "Schematic Auto Refresh Properties";
        this.Load += new System.EventHandler(this.FormAutoRefresh_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer timerAutoRefresh;
    private System.Windows.Forms.ComboBox IntervalMinute;
    private System.Windows.Forms.ComboBox IntervalSecond;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.RadioButton AutoOn;
    private System.Windows.Forms.RadioButton AutoOff;
  }
}