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
namespace ViperPin
{
  partial class ViperPinForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViperPinForm));
      this.cmdOK = new System.Windows.Forms.Button();
      this.Label4 = new System.Windows.Forms.Label();
      this.Label1 = new System.Windows.Forms.Label();
      this.Label3 = new System.Windows.Forms.Label();
      this.txtlot = new System.Windows.Forms.TextBox();
      this.lblEditLayer = new System.Windows.Forms.Label();
      this.GroupBox3 = new System.Windows.Forms.GroupBox();
      this.chkEnds = new System.Windows.Forms.CheckBox();
      this.txtlotinc = new System.Windows.Forms.TextBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.GroupBox2 = new System.Windows.Forms.GroupBox();
      this.cmbPINField = new System.Windows.Forms.ComboBox();
      this.GroupBox1 = new System.Windows.Forms.GroupBox();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.GroupBox3.SuspendLayout();
      this.GroupBox2.SuspendLayout();
      this.GroupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmdOK
      // 
      this.cmdOK.Location = new System.Drawing.Point(14, 237);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 6;
      this.cmdOK.Text = "Ok";
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // Label4
      // 
      this.Label4.Location = new System.Drawing.Point(8, 48);
      this.Label4.Name = "Label4";
      this.Label4.Size = new System.Drawing.Size(64, 16);
      this.Label4.TabIndex = 6;
      this.Label4.Text = "Increment:";
      // 
      // Label1
      // 
      this.Label1.Location = new System.Drawing.Point(8, 16);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(72, 16);
      this.Label1.TabIndex = 4;
      this.Label1.Text = "Parcel Layer:";
      // 
      // Label3
      // 
      this.Label3.Location = new System.Drawing.Point(8, 24);
      this.Label3.Name = "Label3";
      this.Label3.Size = new System.Drawing.Size(72, 16);
      this.Label3.TabIndex = 5;
      this.Label3.Text = "Start Value:";
      // 
      // txtlot
      // 
      this.txtlot.Location = new System.Drawing.Point(96, 24);
      this.txtlot.Name = "txtlot";
      this.txtlot.Size = new System.Drawing.Size(32, 20);
      this.txtlot.TabIndex = 5;
      this.txtlot.Text = "1";
      // 
      // lblEditLayer
      // 
      this.lblEditLayer.Location = new System.Drawing.Point(96, 16);
      this.lblEditLayer.Name = "lblEditLayer";
      this.lblEditLayer.Size = new System.Drawing.Size(144, 16);
      this.lblEditLayer.TabIndex = 4;
      this.lblEditLayer.Text = "lblEditLayer";
      // 
      // GroupBox3
      // 
      this.GroupBox3.Controls.Add(this.chkEnds);
      this.GroupBox3.Location = new System.Drawing.Point(14, 173);
      this.GroupBox3.Name = "GroupBox3";
      this.GroupBox3.Size = new System.Drawing.Size(256, 56);
      this.GroupBox3.TabIndex = 11;
      this.GroupBox3.TabStop = false;
      this.GroupBox3.Text = "Sketch Ends";
      // 
      // chkEnds
      // 
      this.chkEnds.Checked = true;
      this.chkEnds.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkEnds.Location = new System.Drawing.Point(8, 24);
      this.chkEnds.Name = "chkEnds";
      this.chkEnds.Size = new System.Drawing.Size(152, 24);
      this.chkEnds.TabIndex = 0;
      this.chkEnds.Text = "Include ends of sketch";
      // 
      // txtlotinc
      // 
      this.txtlotinc.Location = new System.Drawing.Point(96, 48);
      this.txtlotinc.Name = "txtlotinc";
      this.txtlotinc.Size = new System.Drawing.Size(32, 20);
      this.txtlotinc.TabIndex = 7;
      this.txtlotinc.Text = "1";
      // 
      // Label2
      // 
      this.Label2.Location = new System.Drawing.Point(24, 40);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(56, 16);
      this.Label2.TabIndex = 5;
      this.Label2.Text = "PIN Field:";
      // 
      // GroupBox2
      // 
      this.GroupBox2.Controls.Add(this.txtlotinc);
      this.GroupBox2.Controls.Add(this.Label4);
      this.GroupBox2.Controls.Add(this.Label3);
      this.GroupBox2.Controls.Add(this.txtlot);
      this.GroupBox2.Location = new System.Drawing.Point(14, 85);
      this.GroupBox2.Name = "GroupBox2";
      this.GroupBox2.Size = new System.Drawing.Size(256, 80);
      this.GroupBox2.TabIndex = 10;
      this.GroupBox2.TabStop = false;
      this.GroupBox2.Text = "Parcel PIN Value";
      // 
      // cmbPINField
      // 
      this.cmbPINField.Location = new System.Drawing.Point(96, 40);
      this.cmbPINField.Name = "cmbPINField";
      this.cmbPINField.Size = new System.Drawing.Size(152, 21);
      this.cmbPINField.TabIndex = 4;
      this.cmbPINField.Text = "cmbPINField";
      // 
      // GroupBox1
      // 
      this.GroupBox1.Controls.Add(this.Label1);
      this.GroupBox1.Controls.Add(this.lblEditLayer);
      this.GroupBox1.Controls.Add(this.Label2);
      this.GroupBox1.Controls.Add(this.cmbPINField);
      this.GroupBox1.Location = new System.Drawing.Point(14, 5);
      this.GroupBox1.Name = "GroupBox1";
      this.GroupBox1.Size = new System.Drawing.Size(256, 72);
      this.GroupBox1.TabIndex = 9;
      this.GroupBox1.TabStop = false;
      this.GroupBox1.Text = "Parcel PIN Field";
      // 
      // cmdCancel
      // 
      this.cmdCancel.Location = new System.Drawing.Point(187, 237);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(75, 23);
      this.cmdCancel.TabIndex = 7;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // ViperPinForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 264);
      this.Controls.Add(this.cmdOK);
      this.Controls.Add(this.cmdCancel);
      this.Controls.Add(this.GroupBox3);
      this.Controls.Add(this.GroupBox2);
      this.Controls.Add(this.GroupBox1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ViperPinForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "ViperPinForm";
      this.GroupBox3.ResumeLayout(false);
      this.GroupBox2.ResumeLayout(false);
      this.GroupBox2.PerformLayout();
      this.GroupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    internal System.Windows.Forms.Button cmdOK;
    internal System.Windows.Forms.Label Label4;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.Label Label3;
    internal System.Windows.Forms.TextBox txtlot;
    internal System.Windows.Forms.Label lblEditLayer;
    internal System.Windows.Forms.GroupBox GroupBox3;
    internal System.Windows.Forms.CheckBox chkEnds;
    internal System.Windows.Forms.TextBox txtlotinc;
    internal System.Windows.Forms.Label Label2;
    internal System.Windows.Forms.GroupBox GroupBox2;
    internal System.Windows.Forms.ComboBox cmbPINField;
    internal System.Windows.Forms.GroupBox GroupBox1;
    internal System.Windows.Forms.Button cmdCancel;
  }
}