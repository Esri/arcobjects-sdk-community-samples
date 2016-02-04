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
//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlgorithmicColorRamp
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	internal partial class frmAlgorithmicColorRamp
	{
	#region Windows Form Designer generated code 
		[System.Diagnostics.DebuggerNonUserCode()]
		public frmAlgorithmicColorRamp() : base()
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();
		}
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool Disposing)
		{
			if (Disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(Disposing);
		}
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.ToolTip ToolTip1;
		public System.Windows.Forms.CheckBox chkShowColors;
		public System.Windows.Forms.Button cmdEnumColorsFirst;
		public System.Windows.Forms.Button cmdEnumColorsNext;
		public System.Windows.Forms.GroupBox fraColors;
		public System.Windows.Forms.Button cmdCancel;
		public System.Windows.Forms.Button cmdOK;
		public System.Windows.Forms.ComboBox cmbAlgorithm;
		public System.Windows.Forms.TextBox txtEndColor;
		public System.Windows.Forms.TextBox txtStartColor;
		public System.Windows.Forms.Label lblStartColor;
		public System.Windows.Forms.Label lblEndColor;
		public System.Windows.Forms.Label lblAlgorithm;
		public System.Windows.Forms.GroupBox fraRamp;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkShowColors = new System.Windows.Forms.CheckBox();
			this.fraColors = new System.Windows.Forms.GroupBox();
			this.TextBox9 = new System.Windows.Forms.TextBox();
			this.TextBox8 = new System.Windows.Forms.TextBox();
			this.TextBox7 = new System.Windows.Forms.TextBox();
			this.TextBox6 = new System.Windows.Forms.TextBox();
			this.TextBox5 = new System.Windows.Forms.TextBox();
			this.TextBox4 = new System.Windows.Forms.TextBox();
			this.TextBox3 = new System.Windows.Forms.TextBox();
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label8 = new System.Windows.Forms.Label();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdEnumColorsFirst = new System.Windows.Forms.Button();
			this.cmdEnumColorsNext = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.fraRamp = new System.Windows.Forms.GroupBox();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button1 = new System.Windows.Forms.Button();
			this.cmbAlgorithm = new System.Windows.Forms.ComboBox();
			this.txtEndColor = new System.Windows.Forms.TextBox();
			this.txtStartColor = new System.Windows.Forms.TextBox();
			this.lblStartColor = new System.Windows.Forms.Label();
			this.lblEndColor = new System.Windows.Forms.Label();
			this.lblAlgorithm = new System.Windows.Forms.Label();
			this.TextBox10 = new System.Windows.Forms.TextBox();
			this.fraColors.SuspendLayout();
			this.fraRamp.SuspendLayout();
			this.SuspendLayout();
			//
			//chkShowColors
			//
			this.chkShowColors.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkShowColors.BackColor = System.Drawing.SystemColors.Control;
			this.chkShowColors.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkShowColors.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.chkShowColors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkShowColors.Location = new System.Drawing.Point(60, 172);
			this.chkShowColors.Name = "chkShowColors";
			this.chkShowColors.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkShowColors.Size = new System.Drawing.Size(81, 25);
			this.chkShowColors.TabIndex = 33;
			this.chkShowColors.Text = "Show Colors";
			this.chkShowColors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkShowColors.UseVisualStyleBackColor = false;
			//
			//fraColors
			//
			this.fraColors.BackColor = System.Drawing.SystemColors.Control;
			this.fraColors.Controls.Add(this.TextBox10);
			this.fraColors.Controls.Add(this.TextBox9);
			this.fraColors.Controls.Add(this.TextBox8);
			this.fraColors.Controls.Add(this.TextBox7);
			this.fraColors.Controls.Add(this.TextBox6);
			this.fraColors.Controls.Add(this.TextBox5);
			this.fraColors.Controls.Add(this.TextBox4);
			this.fraColors.Controls.Add(this.TextBox3);
			this.fraColors.Controls.Add(this.TextBox2);
			this.fraColors.Controls.Add(this.TextBox1);
			this.fraColors.Controls.Add(this.Label10);
			this.fraColors.Controls.Add(this.Label9);
			this.fraColors.Controls.Add(this.Label8);
			this.fraColors.Controls.Add(this.Label7);
			this.fraColors.Controls.Add(this.Label6);
			this.fraColors.Controls.Add(this.Label5);
			this.fraColors.Controls.Add(this.Label4);
			this.fraColors.Controls.Add(this.Label3);
			this.fraColors.Controls.Add(this.Label2);
			this.fraColors.Controls.Add(this.Label1);
			this.fraColors.Controls.Add(this.cmdEnumColorsFirst);
			this.fraColors.Controls.Add(this.cmdEnumColorsNext);
			this.fraColors.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.fraColors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fraColors.Location = new System.Drawing.Point(156, 0);
			this.fraColors.Name = "fraColors";
			this.fraColors.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fraColors.Size = new System.Drawing.Size(81, 249);
			this.fraColors.TabIndex = 9;
			this.fraColors.TabStop = false;
			this.fraColors.Text = "Colors:";
			//
			//TextBox9
			//
			this.TextBox9.AcceptsReturn = true;
			this.TextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox9.Location = new System.Drawing.Point(28, 144);
			this.TextBox9.MaxLength = 0;
			this.TextBox9.Name = "TextBox9";
			this.TextBox9.Size = new System.Drawing.Size(41, 13);
			this.TextBox9.TabIndex = 50;
			//
			//TextBox8
			//
			this.TextBox8.AcceptsReturn = true;
			this.TextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox8.Location = new System.Drawing.Point(28, 128);
			this.TextBox8.MaxLength = 0;
			this.TextBox8.Name = "TextBox8";
			this.TextBox8.Size = new System.Drawing.Size(41, 13);
			this.TextBox8.TabIndex = 49;
			//
			//TextBox7
			//
			this.TextBox7.AcceptsReturn = true;
			this.TextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox7.Location = new System.Drawing.Point(28, 112);
			this.TextBox7.MaxLength = 0;
			this.TextBox7.Name = "TextBox7";
			this.TextBox7.Size = new System.Drawing.Size(41, 13);
			this.TextBox7.TabIndex = 48;
			//
			//TextBox6
			//
			this.TextBox6.AcceptsReturn = true;
			this.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox6.Location = new System.Drawing.Point(28, 96);
			this.TextBox6.MaxLength = 0;
			this.TextBox6.Name = "TextBox6";
			this.TextBox6.Size = new System.Drawing.Size(41, 13);
			this.TextBox6.TabIndex = 47;
			//
			//TextBox5
			//
			this.TextBox5.AcceptsReturn = true;
			this.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox5.Location = new System.Drawing.Point(28, 80);
			this.TextBox5.MaxLength = 0;
			this.TextBox5.Name = "TextBox5";
			this.TextBox5.Size = new System.Drawing.Size(41, 13);
			this.TextBox5.TabIndex = 46;
			//
			//TextBox4
			//
			this.TextBox4.AcceptsReturn = true;
			this.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox4.Location = new System.Drawing.Point(28, 64);
			this.TextBox4.MaxLength = 0;
			this.TextBox4.Name = "TextBox4";
			this.TextBox4.Size = new System.Drawing.Size(41, 13);
			this.TextBox4.TabIndex = 45;
			//
			//TextBox3
			//
			this.TextBox3.AcceptsReturn = true;
			this.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox3.Location = new System.Drawing.Point(28, 48);
			this.TextBox3.MaxLength = 0;
			this.TextBox3.Name = "TextBox3";
			this.TextBox3.Size = new System.Drawing.Size(41, 13);
			this.TextBox3.TabIndex = 44;
			//
			//TextBox2
			//
			this.TextBox2.AcceptsReturn = true;
			this.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox2.Location = new System.Drawing.Point(28, 32);
			this.TextBox2.MaxLength = 0;
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.Size = new System.Drawing.Size(41, 13);
			this.TextBox2.TabIndex = 43;
			//
			//TextBox1
			//
			this.TextBox1.AcceptsReturn = true;
			this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox1.Location = new System.Drawing.Point(28, 16);
			this.TextBox1.MaxLength = 0;
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(41, 13);
			this.TextBox1.TabIndex = 42;
			//
			//Label10
			//
			this.Label10.Location = new System.Drawing.Point(8, 160);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(17, 17);
			this.Label10.TabIndex = 41;
			this.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label9
			//
			this.Label9.Location = new System.Drawing.Point(8, 144);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(17, 17);
			this.Label9.TabIndex = 40;
			this.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label8
			//
			this.Label8.Location = new System.Drawing.Point(8, 128);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(14, 14);
			this.Label8.TabIndex = 39;
			this.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label7
			//
			this.Label7.Location = new System.Drawing.Point(8, 112);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(17, 17);
			this.Label7.TabIndex = 38;
			this.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label6
			//
			this.Label6.Location = new System.Drawing.Point(8, 96);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(17, 17);
			this.Label6.TabIndex = 37;
			this.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label5
			//
			this.Label5.Location = new System.Drawing.Point(8, 80);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(17, 17);
			this.Label5.TabIndex = 36;
			this.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label4
			//
			this.Label4.Location = new System.Drawing.Point(8, 64);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(17, 17);
			this.Label4.TabIndex = 35;
			this.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(8, 48);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(17, 17);
			this.Label3.TabIndex = 34;
			this.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label2
			//
			this.Label2.Location = new System.Drawing.Point(8, 33);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(17, 17);
			this.Label2.TabIndex = 33;
			this.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(8, 16);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(17, 17);
			this.Label1.TabIndex = 32;
			this.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//cmdEnumColorsFirst
			//
			this.cmdEnumColorsFirst.BackColor = System.Drawing.SystemColors.Control;
			this.cmdEnumColorsFirst.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdEnumColorsFirst.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmdEnumColorsFirst.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdEnumColorsFirst.Location = new System.Drawing.Point(16, 196);
			this.cmdEnumColorsFirst.Name = "cmdEnumColorsFirst";
			this.cmdEnumColorsFirst.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdEnumColorsFirst.Size = new System.Drawing.Size(25, 21);
			this.cmdEnumColorsFirst.TabIndex = 11;
			this.cmdEnumColorsFirst.Text = "|<";
			this.cmdEnumColorsFirst.UseVisualStyleBackColor = false;
			//
			//cmdEnumColorsNext
			//
			this.cmdEnumColorsNext.BackColor = System.Drawing.SystemColors.Control;
			this.cmdEnumColorsNext.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdEnumColorsNext.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmdEnumColorsNext.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdEnumColorsNext.Location = new System.Drawing.Point(40, 196);
			this.cmdEnumColorsNext.Name = "cmdEnumColorsNext";
			this.cmdEnumColorsNext.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdEnumColorsNext.Size = new System.Drawing.Size(25, 21);
			this.cmdEnumColorsNext.TabIndex = 10;
			this.cmdEnumColorsNext.Text = ">";
			this.cmdEnumColorsNext.UseVisualStyleBackColor = false;
			//
			//cmdCancel
			//
			this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdCancel.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdCancel.Location = new System.Drawing.Point(84, 224);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdCancel.Size = new System.Drawing.Size(65, 25);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			//
			//cmdOK
			//
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Location = new System.Drawing.Point(12, 224);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.Size = new System.Drawing.Size(65, 25);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = false;
			//
			//fraRamp
			//
			this.fraRamp.BackColor = System.Drawing.SystemColors.Control;
			this.fraRamp.Controls.Add(this.Button2);
			this.fraRamp.Controls.Add(this.Button1);
			this.fraRamp.Controls.Add(this.cmbAlgorithm);
			this.fraRamp.Controls.Add(this.txtEndColor);
			this.fraRamp.Controls.Add(this.txtStartColor);
			this.fraRamp.Controls.Add(this.lblStartColor);
			this.fraRamp.Controls.Add(this.lblEndColor);
			this.fraRamp.Controls.Add(this.lblAlgorithm);
			this.fraRamp.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.fraRamp.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fraRamp.Location = new System.Drawing.Point(0, 0);
			this.fraRamp.Name = "fraRamp";
			this.fraRamp.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fraRamp.Size = new System.Drawing.Size(149, 205);
			this.fraRamp.TabIndex = 2;
			this.fraRamp.TabStop = false;
			this.fraRamp.Text = "AlgorithmicColorRamp:";
			//
			//Button2
			//
			this.Button2.Location = new System.Drawing.Point(119, 83);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(24, 22);
			this.Button2.TabIndex = 36;
			this.Button2.UseVisualStyleBackColor = true;
			//
			//Button1
			//
			this.Button1.Location = new System.Drawing.Point(119, 36);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(24, 22);
			this.Button1.TabIndex = 35;
			this.Button1.UseVisualStyleBackColor = true;
			//
			//cmbAlgorithm
			//
			this.cmbAlgorithm.BackColor = System.Drawing.SystemColors.Window;
			this.cmbAlgorithm.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbAlgorithm.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.cmbAlgorithm.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbAlgorithm.Location = new System.Drawing.Point(4, 132);
			this.cmbAlgorithm.Name = "cmbAlgorithm";
			this.cmbAlgorithm.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbAlgorithm.Size = new System.Drawing.Size(137, 22);
			this.cmbAlgorithm.TabIndex = 32;
			//
			//txtEndColor
			//
			this.txtEndColor.AcceptsReturn = true;
			this.txtEndColor.BackColor = System.Drawing.SystemColors.Window;
			this.txtEndColor.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEndColor.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtEndColor.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtEndColor.Location = new System.Drawing.Point(4, 84);
			this.txtEndColor.MaxLength = 0;
			this.txtEndColor.Name = "txtEndColor";
			this.txtEndColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtEndColor.Size = new System.Drawing.Size(139, 20);
			this.txtEndColor.TabIndex = 7;
			//
			//txtStartColor
			//
			this.txtStartColor.AcceptsReturn = true;
			this.txtStartColor.BackColor = System.Drawing.SystemColors.Window;
			this.txtStartColor.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtStartColor.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.txtStartColor.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStartColor.Location = new System.Drawing.Point(4, 36);
			this.txtStartColor.MaxLength = 0;
			this.txtStartColor.Name = "txtStartColor";
			this.txtStartColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtStartColor.Size = new System.Drawing.Size(139, 20);
			this.txtStartColor.TabIndex = 6;
			//
			//lblStartColor
			//
			this.lblStartColor.AutoSize = true;
			this.lblStartColor.BackColor = System.Drawing.SystemColors.Control;
			this.lblStartColor.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblStartColor.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.lblStartColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblStartColor.Location = new System.Drawing.Point(8, 20);
			this.lblStartColor.Name = "lblStartColor";
			this.lblStartColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblStartColor.Size = new System.Drawing.Size(61, 14);
			this.lblStartColor.TabIndex = 5;
			this.lblStartColor.Text = "Start Color:";
			this.lblStartColor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//lblEndColor
			//
			this.lblEndColor.AutoSize = true;
			this.lblEndColor.BackColor = System.Drawing.SystemColors.Control;
			this.lblEndColor.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblEndColor.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.lblEndColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblEndColor.Location = new System.Drawing.Point(8, 68);
			this.lblEndColor.Name = "lblEndColor";
			this.lblEndColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblEndColor.Size = new System.Drawing.Size(56, 14);
			this.lblEndColor.TabIndex = 4;
			this.lblEndColor.Text = "End Color:";
			this.lblEndColor.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//lblAlgorithm
			//
			this.lblAlgorithm.AutoSize = true;
			this.lblAlgorithm.BackColor = System.Drawing.SystemColors.Control;
			this.lblAlgorithm.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblAlgorithm.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.lblAlgorithm.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblAlgorithm.Location = new System.Drawing.Point(8, 116);
			this.lblAlgorithm.Name = "lblAlgorithm";
			this.lblAlgorithm.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblAlgorithm.Size = new System.Drawing.Size(55, 14);
			this.lblAlgorithm.TabIndex = 3;
			this.lblAlgorithm.Text = "Algorithm:";
			this.lblAlgorithm.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			//
			//TextBox10
			//
			this.TextBox10.AcceptsReturn = true;
			this.TextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox10.Location = new System.Drawing.Point(28, 160);
			this.TextBox10.MaxLength = 0;
			this.TextBox10.Name = "TextBox10";
			this.TextBox10.Size = new System.Drawing.Size(41, 13);
			this.TextBox10.TabIndex = 51;
			//
			//frmAlgorithmicColorRamp
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF((float)(6.0), (float)(14.0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(262, 251);
			this.Controls.Add(this.chkShowColors);
			this.Controls.Add(this.fraColors);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.fraRamp);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Font = new System.Drawing.Font("Arial", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
			this.Location = new System.Drawing.Point(4, 23);
			this.Name = "frmAlgorithmicColorRamp";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Form1";
			this.fraColors.ResumeLayout(false);
			this.fraColors.PerformLayout();
			this.fraRamp.ResumeLayout(false);
			this.fraRamp.PerformLayout();
			this.ResumeLayout(false);

			cmbAlgorithm.SelectedIndexChanged += new System.EventHandler(cmbAlgorithm_SelectedIndexChanged);
			cmdCancel.Click += new System.EventHandler(cmdCancel_Click);
			cmdEnumColorsNext.Click += new System.EventHandler(cmdEnumColorsNext_Click);
			cmdEnumColorsFirst.Click += new System.EventHandler(cmdEnumColorsFirst_Click);
			cmdOK.Click += new System.EventHandler(cmdOK_Click);
			base.Load += new System.EventHandler(frmAlgorithmicColorRamp_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmAlgorithmicColorRamp_FormClosing);
			chkShowColors.Click += new System.EventHandler(chkShowColors_Click);
			chkShowColors.CheckStateChanged += new System.EventHandler(chkShowColors_CheckStateChanged);
			this.Validating += new System.ComponentModel.CancelEventHandler(frmAlgorithmicColorRamp_Validating);
			Button1.Click += new System.EventHandler(Button1_Click);
			Button2.Click += new System.EventHandler(Button1_Click);
			Label9.Click += new System.EventHandler(Label9_Click);

		}
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.TextBox TextBox9;
		internal System.Windows.Forms.TextBox TextBox8;
		internal System.Windows.Forms.TextBox TextBox7;
		internal System.Windows.Forms.TextBox TextBox6;
		internal System.Windows.Forms.TextBox TextBox5;
		internal System.Windows.Forms.TextBox TextBox4;
		internal System.Windows.Forms.TextBox TextBox3;
		internal System.Windows.Forms.TextBox TextBox2;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.TextBox TextBox10;
	#endregion
	}
} //end of root namespace