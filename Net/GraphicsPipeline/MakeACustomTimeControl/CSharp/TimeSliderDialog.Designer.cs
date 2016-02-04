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
namespace MakeACustomTimeControl2008
{
  partial class TimeSliderDialog
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
      this.m_timeSlider = new System.Windows.Forms.TrackBar();
      this.m_datePicker = new System.Windows.Forms.DateTimePicker();
      ((System.ComponentModel.ISupportInitialize)(this.m_timeSlider)).BeginInit();
      this.SuspendLayout();
      // 
      // m_timeSlider
      // 
      this.m_timeSlider.Location = new System.Drawing.Point(12, 12);
      this.m_timeSlider.Maximum = 100;
      this.m_timeSlider.Name = "m_timeSlider";
      this.m_timeSlider.Size = new System.Drawing.Size(260, 45);
      this.m_timeSlider.TabIndex = 1;
      this.m_timeSlider.ValueChanged += new System.EventHandler(this.TimeSlider_ValueChanged);
      // 
      // m_datePicker
      // 
      this.m_datePicker.CustomFormat = "MM/dd/yyyy HH:mm:ss";
      this.m_datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.m_datePicker.Location = new System.Drawing.Point(43, 63);
      this.m_datePicker.Name = "m_datePicker";
      this.m_datePicker.Size = new System.Drawing.Size(200, 20);
      this.m_datePicker.TabIndex = 2;
      this.m_datePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
      // 
      // TimeSliderDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(287, 101);
      this.Controls.Add(this.m_datePicker);
      this.Controls.Add(this.m_timeSlider);
      this.Name = "TimeSliderDialog";
      this.Text = "TimeSliderDialog";
      ((System.ComponentModel.ISupportInitialize)(this.m_timeSlider)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TrackBar m_timeSlider;
    private System.Windows.Forms.DateTimePicker m_datePicker;
  }
}