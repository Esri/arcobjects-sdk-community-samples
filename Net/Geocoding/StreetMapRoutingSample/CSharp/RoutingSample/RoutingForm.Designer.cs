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
using System;

using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Location;
using ESRI.ArcGIS.LocationUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace RoutingSample
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class RoutingForm : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
				components.Dispose();
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoutingForm));
			this.m_btnFindRoute = new System.Windows.Forms.Button();
			this.m_btnClose = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.m_txtRoutingService = new System.Windows.Forms.TextBox();
			this.m_dlgRoutingSrvc = new System.Windows.Forms.OpenFileDialog();
			this.m_btnRoutingService = new System.Windows.Forms.Button();
			this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
			this.m_btnAddressLocator = new System.Windows.Forms.Button();
			this.m_txtAddressLocator = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.m_txtStartAddress = new System.Windows.Forms.TextBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.m_txtStartCity = new System.Windows.Forms.TextBox();
			this.m_txtStartState = new System.Windows.Forms.TextBox();
			this.m_txtStartCode = new System.Windows.Forms.TextBox();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.m_txtFinishAddress = new System.Windows.Forms.TextBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label8 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.m_txtFinishCity = new System.Windows.Forms.TextBox();
			this.m_txtFinishState = new System.Windows.Forms.TextBox();
			this.m_txtFinishCode = new System.Windows.Forms.TextBox();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.m_cmbDistanceUnit = new System.Windows.Forms.ComboBox();
			this.Label14 = new System.Windows.Forms.Label();
			this.m_trackUseRoad = new System.Windows.Forms.TrackBar();
			this.Label12 = new System.Windows.Forms.Label();
			this.m_btnRestrictions = new System.Windows.Forms.Button();
			this.m_rbtnQuickest = new System.Windows.Forms.RadioButton();
			this.Label11 = new System.Windows.Forms.Label();
			this.m_rbtnShortest = new System.Windows.Forms.RadioButton();
			this.Label13 = new System.Windows.Forms.Label();
			this.Label15 = new System.Windows.Forms.Label();
			this.m_txtBarriers = new System.Windows.Forms.TextBox();
			this.m_btnBarriersOpen = new System.Windows.Forms.Button();
			this.m_btnShowDirections = new System.Windows.Forms.Button();
			this.m_btnBarriersClear = new System.Windows.Forms.Button();
			this.GroupBox4 = new System.Windows.Forms.GroupBox();
			this.GroupBox1.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.m_trackUseRoad).BeginInit();
			this.SuspendLayout();
			//
			//m_btnFindRoute
			//
			this.m_btnFindRoute.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnFindRoute.Enabled = false;
			this.m_btnFindRoute.Location = new System.Drawing.Point(96, 488);
			this.m_btnFindRoute.Name = "m_btnFindRoute";
			this.m_btnFindRoute.Size = new System.Drawing.Size(104, 23);
			this.m_btnFindRoute.TabIndex = 14;
			this.m_btnFindRoute.Text = "Find Route";
			//
			//m_btnClose
			//
			this.m_btnClose.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnClose.Location = new System.Drawing.Point(320, 488);
			this.m_btnClose.Name = "m_btnClose";
			this.m_btnClose.Size = new System.Drawing.Size(75, 23);
			this.m_btnClose.TabIndex = 16;
			this.m_btnClose.Text = "Close";
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(8, 8);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(100, 23);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Routing Service:";
			//
			//m_txtRoutingService
			//
			this.m_txtRoutingService.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtRoutingService.Location = new System.Drawing.Point(112, 8);
			this.m_txtRoutingService.Name = "m_txtRoutingService";
			this.m_txtRoutingService.ReadOnly = true;
			this.m_txtRoutingService.Size = new System.Drawing.Size(248, 20);
			this.m_txtRoutingService.TabIndex = 1;
			//
			//m_dlgRoutingSrvc
			//
			this.m_dlgRoutingSrvc.Filter = "Routing Service Files (*.rs)|*.rs";
			this.m_dlgRoutingSrvc.RestoreDirectory = true;
			this.m_dlgRoutingSrvc.Title = "Choose Routing Service";
			//
			//m_btnRoutingService
			//
			this.m_btnRoutingService.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnRoutingService.ImageIndex = 0;
			this.m_btnRoutingService.ImageList = this.ImageList1;
			this.m_btnRoutingService.Location = new System.Drawing.Point(368, 8);
			this.m_btnRoutingService.Name = "m_btnRoutingService";
			this.m_btnRoutingService.Size = new System.Drawing.Size(24, 23);
			this.m_btnRoutingService.TabIndex = 2;
			//
			//ImageList1
			//
			this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream"));
			this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.ImageList1.Images.SetKeyName(0, "");
			//
			//m_btnAddressLocator
			//
			this.m_btnAddressLocator.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnAddressLocator.ImageIndex = 0;
			this.m_btnAddressLocator.ImageList = this.ImageList1;
			this.m_btnAddressLocator.Location = new System.Drawing.Point(368, 37);
			this.m_btnAddressLocator.Name = "m_btnAddressLocator";
			this.m_btnAddressLocator.Size = new System.Drawing.Size(24, 23);
			this.m_btnAddressLocator.TabIndex = 5;
			//
			//m_txtAddressLocator
			//
			this.m_txtAddressLocator.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtAddressLocator.Location = new System.Drawing.Point(112, 40);
			this.m_txtAddressLocator.Name = "m_txtAddressLocator";
			this.m_txtAddressLocator.ReadOnly = true;
			this.m_txtAddressLocator.Size = new System.Drawing.Size(248, 20);
			this.m_txtAddressLocator.TabIndex = 4;
			//
			//Label2
			//
			this.Label2.Location = new System.Drawing.Point(8, 40);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(100, 23);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "Address Locator:";
			//
			//GroupBox1
			//
			this.GroupBox1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.GroupBox1.Controls.Add(this.m_txtStartAddress);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.m_txtStartCity);
			this.GroupBox1.Controls.Add(this.m_txtStartState);
			this.GroupBox1.Controls.Add(this.m_txtStartCode);
			this.GroupBox1.Location = new System.Drawing.Point(8, 64);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(386, 128);
			this.GroupBox1.TabIndex = 6;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Start:";
			//
			//m_txtStartAddress
			//
			this.m_txtStartAddress.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtStartAddress.Location = new System.Drawing.Point(136, 24);
			this.m_txtStartAddress.Name = "m_txtStartAddress";
			this.m_txtStartAddress.Size = new System.Drawing.Size(240, 20);
			this.m_txtStartAddress.TabIndex = 1;
			this.m_txtStartAddress.Text = "2001 ARDEN WAY";
			//
			//Label6
			//
			this.Label6.Location = new System.Drawing.Point(8, 96);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(100, 23);
			this.Label6.TabIndex = 6;
			this.Label6.Text = "Postal Code:";
			//
			//Label5
			//
			this.Label5.Location = new System.Drawing.Point(8, 72);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(100, 23);
			this.Label5.TabIndex = 4;
			this.Label5.Text = "State or Province:";
			//
			//Label4
			//
			this.Label4.Location = new System.Drawing.Point(8, 48);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(100, 23);
			this.Label4.TabIndex = 2;
			this.Label4.Text = "City:";
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(8, 24);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(128, 23);
			this.Label3.TabIndex = 0;
			this.Label3.Text = "Address or Intersection:";
			//
			//m_txtStartCity
			//
			this.m_txtStartCity.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtStartCity.Location = new System.Drawing.Point(136, 48);
			this.m_txtStartCity.Name = "m_txtStartCity";
			this.m_txtStartCity.Size = new System.Drawing.Size(240, 20);
			this.m_txtStartCity.TabIndex = 3;
			this.m_txtStartCity.Text = "Sacramento";
			//
			//m_txtStartState
			//
			this.m_txtStartState.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtStartState.Location = new System.Drawing.Point(136, 72);
			this.m_txtStartState.Name = "m_txtStartState";
			this.m_txtStartState.Size = new System.Drawing.Size(240, 20);
			this.m_txtStartState.TabIndex = 5;
			this.m_txtStartState.Text = "CA";
			//
			//m_txtStartCode
			//
			this.m_txtStartCode.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtStartCode.Location = new System.Drawing.Point(136, 96);
			this.m_txtStartCode.Name = "m_txtStartCode";
			this.m_txtStartCode.Size = new System.Drawing.Size(240, 20);
			this.m_txtStartCode.TabIndex = 7;
			//
			//GroupBox2
			//
			this.GroupBox2.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.GroupBox2.Controls.Add(this.m_txtFinishAddress);
			this.GroupBox2.Controls.Add(this.Label7);
			this.GroupBox2.Controls.Add(this.Label8);
			this.GroupBox2.Controls.Add(this.Label9);
			this.GroupBox2.Controls.Add(this.Label10);
			this.GroupBox2.Controls.Add(this.m_txtFinishCity);
			this.GroupBox2.Controls.Add(this.m_txtFinishState);
			this.GroupBox2.Controls.Add(this.m_txtFinishCode);
			this.GroupBox2.Location = new System.Drawing.Point(8, 200);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(386, 128);
			this.GroupBox2.TabIndex = 7;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Finish:";
			//
			//m_txtFinishAddress
			//
			this.m_txtFinishAddress.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtFinishAddress.Location = new System.Drawing.Point(136, 24);
			this.m_txtFinishAddress.Name = "m_txtFinishAddress";
			this.m_txtFinishAddress.Size = new System.Drawing.Size(240, 20);
			this.m_txtFinishAddress.TabIndex = 1;
			this.m_txtFinishAddress.Text = "4760 MACK RD";
			//
			//Label7
			//
			this.Label7.Location = new System.Drawing.Point(8, 96);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(100, 23);
			this.Label7.TabIndex = 6;
			this.Label7.Text = "Postal Code:";
			//
			//Label8
			//
			this.Label8.Location = new System.Drawing.Point(8, 72);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(100, 23);
			this.Label8.TabIndex = 4;
			this.Label8.Text = "State or Province:";
			//
			//Label9
			//
			this.Label9.Location = new System.Drawing.Point(8, 48);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(100, 23);
			this.Label9.TabIndex = 2;
			this.Label9.Text = "City:";
			//
			//Label10
			//
			this.Label10.Location = new System.Drawing.Point(8, 24);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(128, 23);
			this.Label10.TabIndex = 0;
			this.Label10.Text = "Address or Intersection:";
			//
			//m_txtFinishCity
			//
			this.m_txtFinishCity.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtFinishCity.Location = new System.Drawing.Point(136, 48);
			this.m_txtFinishCity.Name = "m_txtFinishCity";
			this.m_txtFinishCity.Size = new System.Drawing.Size(240, 20);
			this.m_txtFinishCity.TabIndex = 3;
			this.m_txtFinishCity.Text = "Sacramento";
			//
			//m_txtFinishState
			//
			this.m_txtFinishState.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtFinishState.Location = new System.Drawing.Point(136, 72);
			this.m_txtFinishState.Name = "m_txtFinishState";
			this.m_txtFinishState.Size = new System.Drawing.Size(240, 20);
			this.m_txtFinishState.TabIndex = 5;
			this.m_txtFinishState.Text = "CA";
			//
			//m_txtFinishCode
			//
			this.m_txtFinishCode.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtFinishCode.Location = new System.Drawing.Point(136, 96);
			this.m_txtFinishCode.Name = "m_txtFinishCode";
			this.m_txtFinishCode.Size = new System.Drawing.Size(240, 20);
			this.m_txtFinishCode.TabIndex = 7;
			//
			//GroupBox3
			//
			this.GroupBox3.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.GroupBox3.Controls.Add(this.m_rbtnShortest);
			this.GroupBox3.Controls.Add(this.m_rbtnQuickest);
			this.GroupBox3.Controls.Add(this.m_cmbDistanceUnit);
			this.GroupBox3.Controls.Add(this.Label14);
			this.GroupBox3.Controls.Add(this.m_trackUseRoad);
			this.GroupBox3.Controls.Add(this.Label12);
			this.GroupBox3.Controls.Add(this.m_btnRestrictions);
			this.GroupBox3.Controls.Add(this.Label11);
			this.GroupBox3.Controls.Add(this.Label13);
			this.GroupBox3.Location = new System.Drawing.Point(8, 336);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(386, 100);
			this.GroupBox3.TabIndex = 8;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Options:";
			//
			//m_cmbDistanceUnit
			//
			this.m_cmbDistanceUnit.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_cmbDistanceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cmbDistanceUnit.Items.AddRange(new object[] {"Feet", "Yards", "Miles", "Meters", "Kilometers"});
			this.m_cmbDistanceUnit.Location = new System.Drawing.Point(264, 16);
			this.m_cmbDistanceUnit.Name = "m_cmbDistanceUnit";
			this.m_cmbDistanceUnit.Size = new System.Drawing.Size(114, 21);
			this.m_cmbDistanceUnit.TabIndex = 3;
			//
			//Label14
			//
			this.Label14.Location = new System.Drawing.Point(176, 16);
			this.Label14.Name = "Label14";
			this.Label14.Size = new System.Drawing.Size(88, 23);
			this.Label14.TabIndex = 2;
			this.Label14.Text = "Distance Units:";
			//
			//m_trackUseRoad
			//
			this.m_trackUseRoad.AutoSize = false;
			this.m_trackUseRoad.Location = new System.Drawing.Point(96, 62);
			this.m_trackUseRoad.Maximum = 100;
			this.m_trackUseRoad.Name = "m_trackUseRoad";
			this.m_trackUseRoad.Size = new System.Drawing.Size(104, 32);
			this.m_trackUseRoad.TabIndex = 5;
			this.m_trackUseRoad.TickFrequency = 10;
			this.m_trackUseRoad.Value = 50;
			//
			//Label12
			//
			this.Label12.Location = new System.Drawing.Point(8, 64);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(96, 23);
			this.Label12.TabIndex = 4;
			this.Label12.Text = "Use Local Roads";
			//
			//m_btnRestrictions
			//
			this.m_btnRestrictions.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnRestrictions.Enabled = false;
			this.m_btnRestrictions.Location = new System.Drawing.Point(282, 64);
			this.m_btnRestrictions.Name = "m_btnRestrictions";
			this.m_btnRestrictions.Size = new System.Drawing.Size(96, 23);
			this.m_btnRestrictions.TabIndex = 7;
			this.m_btnRestrictions.Text = "Restrictions";
			//
			//m_rbtnQuickest
			//
			this.m_rbtnQuickest.Checked = true;
			this.m_rbtnQuickest.Location = new System.Drawing.Point(46, 13);
			this.m_rbtnQuickest.Name = "m_rbtnQuickest";
			this.m_rbtnQuickest.Size = new System.Drawing.Size(104, 24);
			this.m_rbtnQuickest.TabIndex = 0;
			this.m_rbtnQuickest.TabStop = true;
			this.m_rbtnQuickest.Text = "Quickest Route";
			//
			//Label11
			//
			this.Label11.Location = new System.Drawing.Point(8, 16);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(32, 23);
			this.Label11.TabIndex = 0;
			this.Label11.Text = "Find:";
			//
			//m_rbtnShortest
			//
			this.m_rbtnShortest.Location = new System.Drawing.Point(46, 37);
			this.m_rbtnShortest.Name = "m_rbtnShortest";
			this.m_rbtnShortest.Size = new System.Drawing.Size(104, 24);
			this.m_rbtnShortest.TabIndex = 1;
			this.m_rbtnShortest.TabStop = true;
			this.m_rbtnShortest.Text = "Shortest Route";
			//
			//Label13
			//
			this.Label13.Location = new System.Drawing.Point(200, 64);
			this.Label13.Name = "Label13";
			this.Label13.Size = new System.Drawing.Size(96, 23);
			this.Label13.TabIndex = 6;
			this.Label13.Text = "Use Highways";
			//
			//Label15
			//
			this.Label15.Location = new System.Drawing.Point(8, 448);
			this.Label15.Name = "Label15";
			this.Label15.Size = new System.Drawing.Size(100, 23);
			this.Label15.TabIndex = 9;
			this.Label15.Text = "Barriers Table:";
			//
			//m_txtBarriers
			//
			this.m_txtBarriers.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.m_txtBarriers.Location = new System.Drawing.Point(112, 448);
			this.m_txtBarriers.Name = "m_txtBarriers";
			this.m_txtBarriers.ReadOnly = true;
			this.m_txtBarriers.Size = new System.Drawing.Size(168, 20);
			this.m_txtBarriers.TabIndex = 10;
			//
			//m_btnBarriersOpen
			//
			this.m_btnBarriersOpen.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnBarriersOpen.Enabled = false;
			this.m_btnBarriersOpen.ImageIndex = 0;
			this.m_btnBarriersOpen.ImageList = this.ImageList1;
			this.m_btnBarriersOpen.Location = new System.Drawing.Point(288, 448);
			this.m_btnBarriersOpen.Name = "m_btnBarriersOpen";
			this.m_btnBarriersOpen.Size = new System.Drawing.Size(24, 23);
			this.m_btnBarriersOpen.TabIndex = 11;
			//
			//m_btnShowDirections
			//
			this.m_btnShowDirections.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnShowDirections.Enabled = false;
			this.m_btnShowDirections.Location = new System.Drawing.Point(208, 488);
			this.m_btnShowDirections.Name = "m_btnShowDirections";
			this.m_btnShowDirections.Size = new System.Drawing.Size(104, 23);
			this.m_btnShowDirections.TabIndex = 15;
			this.m_btnShowDirections.Text = "Show Directions";
			//
			//m_btnBarriersClear
			//
			this.m_btnBarriersClear.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.m_btnBarriersClear.Location = new System.Drawing.Point(320, 448);
			this.m_btnBarriersClear.Name = "m_btnBarriersClear";
			this.m_btnBarriersClear.Size = new System.Drawing.Size(75, 23);
			this.m_btnBarriersClear.TabIndex = 12;
			this.m_btnBarriersClear.Text = "Clear";
			//
			//GroupBox4
			//
			this.GroupBox4.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.GroupBox4.Location = new System.Drawing.Point(8, 480);
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.Size = new System.Drawing.Size(384, 2);
			this.GroupBox4.TabIndex = 13;
			this.GroupBox4.TabStop = false;
			//
			//RoutingForm
			//
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(402, 522);
			this.Controls.Add(this.GroupBox4);
			this.Controls.Add(this.m_btnBarriersClear);
			this.Controls.Add(this.GroupBox3);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.m_btnRoutingService);
			this.Controls.Add(this.m_txtRoutingService);
			this.Controls.Add(this.m_txtAddressLocator);
			this.Controls.Add(this.m_txtBarriers);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.m_btnClose);
			this.Controls.Add(this.m_btnFindRoute);
			this.Controls.Add(this.m_btnAddressLocator);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.GroupBox2);
			this.Controls.Add(this.Label15);
			this.Controls.Add(this.m_btnBarriersOpen);
			this.Controls.Add(this.m_btnShowDirections);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RoutingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Routing Sample";
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			this.GroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.m_trackUseRoad).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			//INSTANT C# NOTE: Converted event handlers:
			base.Closing += new System.ComponentModel.CancelEventHandler(RoutingForm_Closing);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(RoutingForm_KeyDown);
			m_btnRoutingService.Click += new System.EventHandler(m_btnRoutingService_Click);
			m_btnAddressLocator.Click += new System.EventHandler(m_btnAddressLocator_Click);
			m_btnBarriersOpen.Click += new System.EventHandler(m_btnBarriersOpen_Click);
			m_btnBarriersClear.Click += new System.EventHandler(m_btnBarriersClear_Click);
			m_btnFindRoute.Click += new System.EventHandler(m_btnFindRoute_Click);
			m_btnShowDirections.Click += new System.EventHandler(m_btnShowDirections_Click);
			m_btnClose.Click += new System.EventHandler(m_btnClose_Click);
			base.Load += new System.EventHandler(RoutingForm_Load);
			m_btnRestrictions.Click += new System.EventHandler(m_btnRestrictions_Click);

		}

		internal System.Windows.Forms.TabControl m_ctrlPages;
		internal System.Windows.Forms.Button m_btnFindRoute;
		internal System.Windows.Forms.TabPage m_ctrlPageStops;
		internal System.Windows.Forms.TabPage m_ctrlPageBarriers;
		internal System.Windows.Forms.TabPage m_ctrlPageDD;
		internal System.Windows.Forms.TabPage m_ctrlPageOptions;
		internal System.Windows.Forms.ImageList m_imgList;
		internal System.Windows.Forms.Button m_btnStopAdd;
		internal System.Windows.Forms.Button m_btnStopsRemoveAll;
		internal System.Windows.Forms.ListBox ListBox1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.Button m_btnStopRemove;
		internal System.Windows.Forms.Button m_btnStopUp;
		internal System.Windows.Forms.Button m_btnStopDown;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.GroupBox GroupBox4;
		internal System.Windows.Forms.OpenFileDialog m_dlgRoutingSrvc;
		internal System.Windows.Forms.Label Label15;
		internal System.Windows.Forms.Label Label14;
		internal System.Windows.Forms.Label Label13;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.ImageList ImageList1;
		internal System.Windows.Forms.Button m_btnClose;
		internal System.Windows.Forms.TextBox m_txtRoutingService;
		internal System.Windows.Forms.Button m_btnRoutingService;
		internal System.Windows.Forms.Button m_btnAddressLocator;
		internal System.Windows.Forms.TextBox m_txtAddressLocator;
		internal System.Windows.Forms.TextBox m_txtStartAddress;
		internal System.Windows.Forms.TextBox m_txtStartCity;
		internal System.Windows.Forms.TextBox m_txtStartState;
		internal System.Windows.Forms.TextBox m_txtStartCode;
		internal System.Windows.Forms.TextBox m_txtFinishAddress;
		internal System.Windows.Forms.TextBox m_txtFinishCity;
		internal System.Windows.Forms.TextBox m_txtFinishState;
		internal System.Windows.Forms.TextBox m_txtFinishCode;
		internal System.Windows.Forms.ComboBox m_cmbDistanceUnit;
		internal System.Windows.Forms.TrackBar m_trackUseRoad;
		internal System.Windows.Forms.Button m_btnRestrictions;
		internal System.Windows.Forms.RadioButton m_rbtnQuickest;
		internal System.Windows.Forms.RadioButton m_rbtnShortest;
		internal System.Windows.Forms.TextBox m_txtBarriers;
		internal System.Windows.Forms.Button m_btnBarriersOpen;
		internal System.Windows.Forms.Button m_btnShowDirections;
		internal System.Windows.Forms.Button m_btnBarriersClear;

	}


} //end of root namespace