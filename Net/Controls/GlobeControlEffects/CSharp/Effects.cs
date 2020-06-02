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
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GlobeCore;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS;
using System.IO;


namespace GlobeControlEffects
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Effects : System.Windows.Forms.Form
	{
		public System.Windows.Forms.GroupBox Frame2_1;
		public System.Windows.Forms.Label lblDelay;
		public System.Windows.Forms.TextBox TxtTipDelay;
		public System.Windows.Forms.ComboBox cmbTipType;
		public System.Windows.Forms.CheckBox ChkHUD;
		public System.Windows.Forms.CheckBox ChkArrow;
		public System.Windows.Forms.CheckBox ChkTip;
		public System.Windows.Forms.Label LblTips;
		public System.Windows.Forms.GroupBox Frame2;
		public System.Windows.Forms.Label lblLatVal;
		public System.Windows.Forms.Label LblLonVal;
		public System.Windows.Forms.Label LblAltVal;
		public System.Windows.Forms.Label LblLat;
		public System.Windows.Forms.Label LblLon;
		public System.Windows.Forms.Label LblALt;
		public System.Windows.Forms.GroupBox _Frame2_0;
		public System.Windows.Forms.Button CmdAmbient;
		public System.Windows.Forms.TextBox TxtAmbient;
		public System.Windows.Forms.Button CmdSetSun;
		public System.Windows.Forms.CheckBox chkSun;
		public System.Windows.Forms.Label Label2;

		private esriGlobeTipsType m_penumTips;
		private System.Windows.Forms.ColorDialog colorDialog1; 
		private IGlobeDisplayEvents_AfterDrawEventHandler afterDrawE;
    private IGlobeDisplay m_globeDisplay;
    private IGlobeDisplayEvents_Event globeDisplayEvents; 
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Effects()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            //Release COM objects
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Effects));
			this.Frame2_1 = new System.Windows.Forms.GroupBox();
			this.lblDelay = new System.Windows.Forms.Label();
			this.TxtTipDelay = new System.Windows.Forms.TextBox();
			this.cmbTipType = new System.Windows.Forms.ComboBox();
			this.ChkHUD = new System.Windows.Forms.CheckBox();
			this.ChkArrow = new System.Windows.Forms.CheckBox();
			this.ChkTip = new System.Windows.Forms.CheckBox();
			this.LblTips = new System.Windows.Forms.Label();
			this.Frame2 = new System.Windows.Forms.GroupBox();
			this.lblLatVal = new System.Windows.Forms.Label();
			this.LblLonVal = new System.Windows.Forms.Label();
			this.LblAltVal = new System.Windows.Forms.Label();
			this.LblLat = new System.Windows.Forms.Label();
			this.LblLon = new System.Windows.Forms.Label();
			this.LblALt = new System.Windows.Forms.Label();
			this._Frame2_0 = new System.Windows.Forms.GroupBox();
			this.CmdAmbient = new System.Windows.Forms.Button();
			this.TxtAmbient = new System.Windows.Forms.TextBox();
			this.CmdSetSun = new System.Windows.Forms.Button();
			this.chkSun = new System.Windows.Forms.CheckBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			this.Frame2_1.SuspendLayout();
			this.Frame2.SuspendLayout();
			this._Frame2_0.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// Frame2_1
			// 
			this.Frame2_1.BackColor = System.Drawing.SystemColors.Control;
			this.Frame2_1.Controls.Add(this.lblDelay);
			this.Frame2_1.Controls.Add(this.TxtTipDelay);
			this.Frame2_1.Controls.Add(this.cmbTipType);
			this.Frame2_1.Controls.Add(this.ChkHUD);
			this.Frame2_1.Controls.Add(this.ChkArrow);
			this.Frame2_1.Controls.Add(this.ChkTip);
			this.Frame2_1.Controls.Add(this.LblTips);
			this.Frame2_1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Frame2_1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame2_1.Location = new System.Drawing.Point(256, 376);
			this.Frame2_1.Name = "Frame2_1";
			this.Frame2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame2_1.Size = new System.Drawing.Size(224, 69);
			this.Frame2_1.TabIndex = 16;
			this.Frame2_1.TabStop = false;
			this.Frame2_1.Text = "HUD";
			// 
			// lblDelay
			// 
			this.lblDelay.BackColor = System.Drawing.SystemColors.Control;
			this.lblDelay.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblDelay.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDelay.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDelay.Location = new System.Drawing.Point(136, 9);
			this.lblDelay.Name = "lblDelay";
			this.lblDelay.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblDelay.Size = new System.Drawing.Size(72, 13);
			this.lblDelay.TabIndex = 18;
			this.lblDelay.Text = "Delay(mSec.)";
			// 
			// TxtTipDelay
			// 
			this.TxtTipDelay.AcceptsReturn = true;
			this.TxtTipDelay.AutoSize = false;
			this.TxtTipDelay.BackColor = System.Drawing.SystemColors.Window;
			this.TxtTipDelay.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.TxtTipDelay.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TxtTipDelay.ForeColor = System.Drawing.SystemColors.WindowText;
			this.TxtTipDelay.Location = new System.Drawing.Point(160, 24);
			this.TxtTipDelay.MaxLength = 0;
			this.TxtTipDelay.Name = "TxtTipDelay";
			this.TxtTipDelay.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.TxtTipDelay.Size = new System.Drawing.Size(48, 15);
			this.TxtTipDelay.TabIndex = 16;
			this.TxtTipDelay.Text = "500";
			// 
			// cmbTipType
			// 
			this.cmbTipType.BackColor = System.Drawing.SystemColors.Window;
			this.cmbTipType.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmbTipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTipType.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbTipType.ForeColor = System.Drawing.SystemColors.WindowText;
			this.cmbTipType.Location = new System.Drawing.Point(69, 42);
			this.cmbTipType.Name = "cmbTipType";
			this.cmbTipType.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmbTipType.Size = new System.Drawing.Size(139, 22);
			this.cmbTipType.TabIndex = 17;
			this.cmbTipType.SelectedIndexChanged += new System.EventHandler(this.cmbTipType_SelectedIndexChanged);
			// 
			// ChkHUD
			// 
			this.ChkHUD.BackColor = System.Drawing.SystemColors.Control;
			this.ChkHUD.Cursor = System.Windows.Forms.Cursors.Default;
			this.ChkHUD.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkHUD.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ChkHUD.Location = new System.Drawing.Point(13, 19);
			this.ChkHUD.Name = "ChkHUD";
			this.ChkHUD.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ChkHUD.Size = new System.Drawing.Size(51, 16);
			this.ChkHUD.TabIndex = 15;
			this.ChkHUD.Text = "HUD";
			this.ChkHUD.CheckedChanged += new System.EventHandler(this.ChkHUD_CheckedChanged);
			// 
			// ChkArrow
			// 
			this.ChkArrow.BackColor = System.Drawing.SystemColors.Control;
			this.ChkArrow.Cursor = System.Windows.Forms.Cursors.Default;
			this.ChkArrow.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkArrow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ChkArrow.Location = new System.Drawing.Point(13, 33);
			this.ChkArrow.Name = "ChkArrow";
			this.ChkArrow.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ChkArrow.Size = new System.Drawing.Size(59, 31);
			this.ChkArrow.TabIndex = 14;
			this.ChkArrow.Text = "North Arrow";
			this.ChkArrow.CheckedChanged += new System.EventHandler(this.ChkArrow_CheckedChanged);
			// 
			// ChkTip
			// 
			this.ChkTip.BackColor = System.Drawing.SystemColors.Control;
			this.ChkTip.Cursor = System.Windows.Forms.Cursors.Default;
			this.ChkTip.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkTip.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ChkTip.Location = new System.Drawing.Point(69, 25);
			this.ChkTip.Name = "ChkTip";
			this.ChkTip.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ChkTip.Size = new System.Drawing.Size(14, 15);
			this.ChkTip.TabIndex = 20;
			this.ChkTip.Text = "Check1";
			this.ChkTip.CheckedChanged += new System.EventHandler(this.ChkTip_CheckedChanged);
			// 
			// LblTips
			// 
			this.LblTips.BackColor = System.Drawing.SystemColors.Control;
			this.LblTips.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblTips.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblTips.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblTips.Location = new System.Drawing.Point(84, 16);
			this.LblTips.Name = "LblTips";
			this.LblTips.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblTips.Size = new System.Drawing.Size(58, 28);
			this.LblTips.TabIndex = 19;
			this.LblTips.Text = "Enable Globe Tips";
			// 
			// Frame2
			// 
			this.Frame2.BackColor = System.Drawing.SystemColors.Control;
			this.Frame2.Controls.Add(this.lblLatVal);
			this.Frame2.Controls.Add(this.LblLonVal);
			this.Frame2.Controls.Add(this.LblAltVal);
			this.Frame2.Controls.Add(this.LblLat);
			this.Frame2.Controls.Add(this.LblLon);
			this.Frame2.Controls.Add(this.LblALt);
			this.Frame2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Frame2.Location = new System.Drawing.Point(488, 376);
			this.Frame2.Name = "Frame2";
			this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Frame2.Size = new System.Drawing.Size(236, 69);
			this.Frame2.TabIndex = 15;
			this.Frame2.TabStop = false;
			this.Frame2.Text = "Alternate HUD";
			// 
			// lblLatVal
			// 
			this.lblLatVal.BackColor = System.Drawing.SystemColors.Control;
			this.lblLatVal.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblLatVal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLatVal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLatVal.Location = new System.Drawing.Point(33, 30);
			this.lblLatVal.Name = "lblLatVal";
			this.lblLatVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblLatVal.Size = new System.Drawing.Size(86, 13);
			this.lblLatVal.TabIndex = 9;
			this.lblLatVal.Text = "lblLatVal";
			// 
			// LblLonVal
			// 
			this.LblLonVal.BackColor = System.Drawing.SystemColors.Control;
			this.LblLonVal.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblLonVal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblLonVal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblLonVal.Location = new System.Drawing.Point(33, 45);
			this.LblLonVal.Name = "LblLonVal";
			this.LblLonVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblLonVal.Size = new System.Drawing.Size(86, 13);
			this.LblLonVal.TabIndex = 7;
			this.LblLonVal.Text = "LblLonVal";
			// 
			// LblAltVal
			// 
			this.LblAltVal.BackColor = System.Drawing.SystemColors.Control;
			this.LblAltVal.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblAltVal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblAltVal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblAltVal.Location = new System.Drawing.Point(134, 44);
			this.LblAltVal.Name = "LblAltVal";
			this.LblAltVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblAltVal.Size = new System.Drawing.Size(86, 11);
			this.LblAltVal.TabIndex = 8;
			this.LblAltVal.Text = "LblAltVal";
			// 
			// LblLat
			// 
			this.LblLat.BackColor = System.Drawing.SystemColors.Control;
			this.LblLat.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblLat.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblLat.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblLat.Location = new System.Drawing.Point(8, 32);
			this.LblLat.Name = "LblLat";
			this.LblLat.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblLat.Size = new System.Drawing.Size(24, 15);
			this.LblLat.TabIndex = 12;
			this.LblLat.Text = "Lat:";
			// 
			// LblLon
			// 
			this.LblLon.BackColor = System.Drawing.SystemColors.Control;
			this.LblLon.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblLon.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblLon.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblLon.Location = new System.Drawing.Point(9, 46);
			this.LblLon.Name = "LblLon";
			this.LblLon.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblLon.Size = new System.Drawing.Size(27, 16);
			this.LblLon.TabIndex = 11;
			this.LblLon.Text = "Lon:";
			// 
			// LblALt
			// 
			this.LblALt.BackColor = System.Drawing.SystemColors.Control;
			this.LblALt.Cursor = System.Windows.Forms.Cursors.Default;
			this.LblALt.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblALt.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LblALt.Location = new System.Drawing.Point(133, 23);
			this.LblALt.Name = "LblALt";
			this.LblALt.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.LblALt.Size = new System.Drawing.Size(83, 19);
			this.LblALt.TabIndex = 10;
			this.LblALt.Text = "Alt (in Kms.)";
			// 
			// _Frame2_0
			// 
			this._Frame2_0.BackColor = System.Drawing.SystemColors.Control;
			this._Frame2_0.Controls.Add(this.CmdAmbient);
			this._Frame2_0.Controls.Add(this.TxtAmbient);
			this._Frame2_0.Controls.Add(this.CmdSetSun);
			this._Frame2_0.Controls.Add(this.chkSun);
			this._Frame2_0.Controls.Add(this.Label2);
			this._Frame2_0.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._Frame2_0.ForeColor = System.Drawing.SystemColors.ControlText;
			this._Frame2_0.Location = new System.Drawing.Point(8, 376);
			this._Frame2_0.Name = "_Frame2_0";
			this._Frame2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._Frame2_0.Size = new System.Drawing.Size(236, 69);
			this._Frame2_0.TabIndex = 14;
			this._Frame2_0.TabStop = false;
			this._Frame2_0.Text = "Sun and Ambient Light Prop";
			// 
			// CmdAmbient
			// 
			this.CmdAmbient.BackColor = System.Drawing.SystemColors.Control;
			this.CmdAmbient.Cursor = System.Windows.Forms.Cursors.Default;
			this.CmdAmbient.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CmdAmbient.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CmdAmbient.Location = new System.Drawing.Point(10, 43);
			this.CmdAmbient.Name = "CmdAmbient";
			this.CmdAmbient.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CmdAmbient.Size = new System.Drawing.Size(78, 22);
			this.CmdAmbient.TabIndex = 4;
			this.CmdAmbient.Text = "Set Ambient";
			this.CmdAmbient.Click += new System.EventHandler(this.CmdAmbient_Click);
			// 
			// TxtAmbient
			// 
			this.TxtAmbient.AcceptsReturn = true;
			this.TxtAmbient.AutoSize = false;
			this.TxtAmbient.BackColor = System.Drawing.SystemColors.Window;
			this.TxtAmbient.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.TxtAmbient.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TxtAmbient.ForeColor = System.Drawing.SystemColors.WindowText;
			this.TxtAmbient.Location = new System.Drawing.Point(168, 45);
			this.TxtAmbient.MaxLength = 0;
			this.TxtAmbient.Name = "TxtAmbient";
			this.TxtAmbient.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.TxtAmbient.Size = new System.Drawing.Size(48, 20);
			this.TxtAmbient.TabIndex = 3;
			this.TxtAmbient.Text = "";
			// 
			// CmdSetSun
			// 
			this.CmdSetSun.BackColor = System.Drawing.SystemColors.Control;
			this.CmdSetSun.Cursor = System.Windows.Forms.Cursors.Default;
			this.CmdSetSun.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CmdSetSun.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CmdSetSun.Location = new System.Drawing.Point(144, 15);
			this.CmdSetSun.Name = "CmdSetSun";
			this.CmdSetSun.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CmdSetSun.Size = new System.Drawing.Size(80, 20);
			this.CmdSetSun.TabIndex = 2;
			this.CmdSetSun.Text = "Set Sun Color";
			this.CmdSetSun.Click += new System.EventHandler(this.CmdSetSun_Click);
			// 
			// chkSun
			// 
			this.chkSun.BackColor = System.Drawing.SystemColors.Control;
			this.chkSun.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkSun.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkSun.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSun.Location = new System.Drawing.Point(15, 17);
			this.chkSun.Name = "chkSun";
			this.chkSun.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkSun.Size = new System.Drawing.Size(81, 22);
			this.chkSun.TabIndex = 1;
			this.chkSun.Text = "Enable Sun";
			this.chkSun.CheckedChanged += new System.EventHandler(this.chkSun_CheckedChanged);
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(96, 48);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(64, 16);
			this.Label2.TabIndex = 5;
			this.Label2.Text = "Values  0 -1";
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(720, 28);
			this.axToolbarControl1.TabIndex = 17;
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Location = new System.Drawing.Point(8, 40);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(192, 328);
			this.axTOCControl1.TabIndex = 18;
			// 
			// axGlobeControl1
			// 
			this.axGlobeControl1.Location = new System.Drawing.Point(208, 40);
			this.axGlobeControl1.Name = "axGlobeControl1";
			this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
			this.axGlobeControl1.Size = new System.Drawing.Size(520, 328);
			this.axGlobeControl1.TabIndex = 19;
			this.axGlobeControl1.OnGlobeReplaced += new ESRI.ArcGIS.Controls.IGlobeControlEvents_Ax_OnGlobeReplacedEventHandler(this.axGlobeControl1_OnGlobeReplaced);
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(512, 56);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 20;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 454);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axGlobeControl1);
			this.Controls.Add(this.axTOCControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.Frame2_1);
			this.Controls.Add(this.Frame2);
			this.Controls.Add(this._Frame2_0);
			this.Name = "Form1";
			this.Text = "GlobeControl";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Frame2_1.ResumeLayout(false);
			this.Frame2.ResumeLayout(false);
			this._Frame2_0.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            Application.Run(new Effects());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

            //relative file path to the sample data from project location
		    string sGlbData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sGlbData  = Path.Combine(sGlbData, @"ArcGIS\data\Globe\World Imagery.3dd");
            var filePath = new DirectoryInfo(sGlbData);
            System.Diagnostics.Debug.WriteLine(string.Format("File path for data root: {0} [{1}]", filePath.FullName, Directory.GetCurrentDirectory()));
            if (!File.Exists(sGlbData)) throw new Exception(string.Format("Fix code to point to your sample data: {0} [{1}] was not found", filePath.FullName, Directory.GetCurrentDirectory()));

            if (axGlobeControl1.Check3dFile(sGlbData)) axGlobeControl1.Load3dFile(sGlbData);

			//Enable north arrow, HUD and GlobeTips.
			bool bChkArrow = axGlobeControl1.GlobeViewer.NorthArrowEnabled;
			bool bHUD = axGlobeControl1.GlobeViewer.HUDEnabled;
			ChkHUD.Checked = bHUD;
			ChkArrow.Checked = bChkArrow;
			//get the state of globetips from the loaded doc.....
			m_penumTips = axGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips;
			//if no tip value (not set) in the loaded doc set it to default..
			if (m_penumTips <= 0)
			{
				m_penumTips = esriGlobeTipsType.esriGlobeTipsTypeLatLon;
			}
			cmbTipType.Items.Insert(0, "esriGlobeTipsTypeNone");
			cmbTipType.Items.Insert(1, "esriGlobeTipsTypeLatLon");
			cmbTipType.Items.Insert(2, "esriGlobeTipsTypeElevation");
			cmbTipType.Items.Insert(3, "esriGlobeTipsTypeLatLonElevation");

			ChkTip.Checked = true;//tip value of the doc...
			//set the list...
			cmbTipType.SelectedIndex = (int) m_penumTips;

			//populate tip type values..
			axGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid;
			axGlobeControl1.TipDelay = 500; //default..
			axGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips = m_penumTips;
			axGlobeControl1.GlobeDisplay.RefreshViewers();

			//Get current sun property..
			IGlobeDisplayRendering pglbDispRend = (IGlobeDisplayRendering)axGlobeControl1.GlobeDisplay;
			bool bsun = pglbDispRend.IsSunEnabled;
			if (bsun == true) chkSun.Checked = true; //checked
			//Get Ambient light...
			TxtAmbient.Text = pglbDispRend.AmbientLight.ToString();
			//Listen to events..
      m_globeDisplay = axGlobeControl1.GlobeDisplay;
      globeDisplayEvents = (IGlobeDisplayEvents_Event)m_globeDisplay;
      globeDisplayEvents.AfterDraw += new IGlobeDisplayEvents_AfterDrawEventHandler(OnAfterDraw);
      //globeDisplayEvents += new IGlobeDisplayEvents_AfterDrawEventHandler(OnAfterDraw);
			//afterDrawE = new IGlobeDisplayEvents_AfterDrawEventHandler(OnAfterDraw);
			//((IGlobeDisplayEvents_Event)axGlobeControl1.GlobeDisplay).AfterDraw+=afterDrawE;


     
      
      //globeDisplayEvents.AfterDraw += new (globeDisplayEvents_AfterDraw);
		}

		private void cmbTipType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_penumTips = (esriGlobeTipsType) cmbTipType.SelectedIndex;
			if (ChkTip.Checked == true)
			{
				string sVal = TxtTipDelay.Text;
				if (Convert.ToSingle(sVal) == 0) sVal="500";	//set it to default..miliseconds
				axGlobeControl1.TipDelay = Convert.ToInt32(sVal);
				axGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid;
				axGlobeControl1.ShowGlobeTips = m_penumTips;
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
		}

		private void CmdAmbient_Click(object sender, System.EventArgs e)
		{
			string sVal = TxtAmbient.Text;
			if (Convert.ToSingle(sVal) > (double) 1.0) sVal = "1";
			if (Convert.ToSingle(sVal) < (double) 0.0) sVal = "0";
			IGlobeDisplayRendering pglbDispRend = (IGlobeDisplayRendering) axGlobeControl1.GlobeDisplay;
			pglbDispRend.AmbientLight = Convert.ToSingle(sVal);
			//update textbox
			TxtAmbient.Text = sVal;
			axGlobeControl1.GlobeDisplay.RefreshViewers();
		}

		private void CmdSetSun_Click(object sender, System.EventArgs e)
		{
			IRgbColor pCmDRgb = new RgbColorClass();
			if (colorDialog1.ShowDialog() == DialogResult.Cancel) return;
			pCmDRgb.Red = colorDialog1.Color.R;
			pCmDRgb.Blue = colorDialog1.Color.B;
			pCmDRgb.Green = colorDialog1.Color.G;

			ChangeIllumination(pCmDRgb);
		}

		private void ChangeIllumination(IRgbColor prgb)
		{
			IGlobeDisplayRendering pglbDispRend = (IGlobeDisplayRendering) axGlobeControl1.GlobeDisplay;

			double platitude=0; double plongitude=0;
			Single pSunred; Single pSungreen; Single pSunblue; Single pAmbientLght;

			if ((pglbDispRend.IsSunEnabled == true) & (chkSun.Checked == true))
			{
				//get the Default position and color...
				pglbDispRend.GetSunPosition(out platitude, out plongitude);
				pglbDispRend.GetSunColor(out pSunred, out pSungreen, out pSunblue);
				//Set AmbientLght
				string sVal = TxtAmbient.Text;
				if (Convert.ToSingle(sVal) > 1) sVal = "1";
				if (Convert.ToSingle(sVal) < 0) sVal = "0";
				pglbDispRend.AmbientLight = Convert.ToSingle(sVal);
				//update textbox
				TxtAmbient.Text = sVal;
				pAmbientLght = pglbDispRend.AmbientLight;
				IColor pIcolor = prgb;
				IGlobeDisplay pglbDisp = EnableSetSun(pAmbientLght, platitude, plongitude, pIcolor);
				axGlobeControl1.GlobeDisplay = pglbDisp;
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
		}

		private IGlobeDisplay EnableSetSun(Single pAmbientLght, double platitude, double plongitude, IColor pColor )
		{ 
			Single pSunred; Single pSungreen; Single pSunblue; 
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.RGB = Convert.ToInt32(pColor.RGB);
			pSunred = pRgbColor.Red;
			pSungreen = pRgbColor.Green;
			pSunblue = pRgbColor.Blue;
			IGlobeDisplayRendering pglbDispRend = (IGlobeDisplayRendering) axGlobeControl1.GlobeDisplay;
			pglbDispRend.SetSunColor(pSunred, pSungreen, pSunblue);
			pglbDispRend.SetSunPosition(platitude, plongitude);
			pglbDispRend.AmbientLight = pAmbientLght;
			return axGlobeControl1.GlobeDisplay;
		}


		private void ChkArrow_CheckedChanged(object sender, System.EventArgs e)
		{
			bool bChkArrow = axGlobeControl1.GlobeViewer.NorthArrowEnabled;
			if ((ChkArrow.Checked == false) & (bChkArrow == true)) 
			{
				axGlobeControl1.GlobeViewer.NorthArrowEnabled = false; //unchecked
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
			else if ((ChkArrow.Checked == true) & (bChkArrow == false))
			{
				axGlobeControl1.GlobeViewer.NorthArrowEnabled = true; //checked
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
		}

		private void ChkHUD_CheckedChanged(object sender, System.EventArgs e)
		{
			//Default HUD
			bool bHUD = axGlobeControl1.GlobeViewer.HUDEnabled;
			if ((ChkHUD.Checked == false) & (bHUD == true)) 
			{
				axGlobeControl1.GlobeViewer.HUDEnabled = false; //unchecked
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
			else if ((ChkHUD.Checked == true) & (bHUD==false))
			{
				axGlobeControl1.GlobeViewer.HUDEnabled = true; //checked
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
		}

		private void chkSun_CheckedChanged(object sender, System.EventArgs e)
		{
			IGlobeDisplayRendering pglbDispRend = (IGlobeDisplayRendering) axGlobeControl1.GlobeDisplay;
			bool bsun = pglbDispRend.IsSunEnabled;
			if ((chkSun.Checked == false) & (bsun == true))
			{
				pglbDispRend.IsSunEnabled = false; //unchecked
				CmdSetSun.Enabled = false;
			}
			else if ((chkSun.Checked == true) & (bsun==false))
			{
				pglbDispRend.IsSunEnabled = true; //checked
				CmdSetSun.Enabled = true;
			}
		}

		private void ChkTip_CheckedChanged(object sender, System.EventArgs e)
		{
			if (ChkTip.Checked == false)
			{
				axGlobeControl1.ShowGlobeTips = esriGlobeTipsType.esriGlobeTipsTypeNone;
				axGlobeControl1.GlobeDisplay.RefreshViewers();
				cmbTipType.Enabled = false;
				TxtTipDelay.Enabled = false;
			}
			else
			{
				cmbTipType.Enabled = true;
				TxtTipDelay.Enabled = true;
				string sVal = TxtTipDelay.Text;
				if (Convert.ToInt32(sVal) == 0) sVal="500"; //set it to default..miliseconds
				if (cmbTipType.SelectedIndex >= 0) m_penumTips = (esriGlobeTipsType) cmbTipType.SelectedIndex;
				axGlobeControl1.TipDelay = Convert.ToInt32(sVal);
				axGlobeControl1.TipStyle = esriTipStyle.esriTipStyleSolid;
				axGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips = m_penumTips;
				axGlobeControl1.GlobeDisplay.RefreshViewers();
			}
		}

		private void GetObserVerLatLong(ISceneViewer pViewer, out double pLatDD, out double pLonDD, out double pAltKms, out double pRoll, out double pIncl)
		{
			IGlobeCamera pCam = (IGlobeCamera) pViewer.Camera;
			pCam.GetObserverLatLonAlt( out pLatDD, out pLonDD, out pAltKms);
			ICamera pIcam = (ICamera) pCam;
			pRoll = pIcam.RollAngle;
			pIncl = pIcam.Inclination;
		}

		private void UpdateCustomHUD(double pLatDD, double pLonDD, double pAltKms, double pRoll, double pIncl)
		{
			LblAltVal.Text = pAltKms.ToString();
			lblLatVal.Text = pLatDD.ToString();
			LblLonVal.Text = pLonDD.ToString();
		}

		private void OnAfterDraw(ISceneViewer pViewer) 
		{
			double pLatDD=0; double pLonDD=0; double pAltKms=0; double pRoll=0; double pIncl=0;
			GetObserVerLatLong(pViewer, out pLatDD, out pLonDD, out pAltKms, out pRoll, out pIncl);
			UpdateCustomHUD(pLatDD, pLonDD, pAltKms, pRoll, pIncl);
		}

		private void axGlobeControl1_OnGlobeReplaced(object sender, ESRI.ArcGIS.Controls.IGlobeControlEvents_OnGlobeReplacedEvent e)
		{
			IGlobeDisplayRendering pglbbDispRend = (IGlobeDisplayRendering) axGlobeControl1.GlobeDisplay;
			bool bsun = pglbbDispRend.IsSunEnabled;
			if (bsun==true) chkSun.Checked = true; //checked
			//get the state of globetips from the loaded doc.....
			m_penumTips = axGlobeControl1.GlobeViewer.GlobeDisplay.Globe.ShowGlobeTips;
		}
	}
}
