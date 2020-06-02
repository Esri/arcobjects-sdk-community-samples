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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Collections.Generic;
using System.Linq;


// This form allows users to change the NALayer/NAContext/NASolver properties

namespace NAEngine
{
	/// <summary>
	/// Summary description for frmNALayerProperties.
	/// </summary>
	public class frmNALayerProperties : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code (defining controls)
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private TabControl tabPropPages;
		private TabPage tabGeneral;
		private TabPage tabRoute;
		private TabPage tabServiceArea;
		private TabPage tabClosestFacility;
		private TabPage tabODCostMatrix;
		private TabPage tabVRP;
		private Button btnOK;
		private Button btnCancel;
		private CheckBox chkRouteIgnoreInvalidLocations;
		private ComboBox cboRouteRestrictUTurns;
		private Label lblRouteRestrictUTurns;
		private Label lblRouteAccumulateAttributeNames;
		private CheckedListBox chklstRouteAccumulateAttributeNames;
		private Label lblRouteRestrictionAttributeNames;
		private CheckedListBox chklstRouteRestrictionAttributeNames;
		private ComboBox cboRouteImpedance;
		private Label lblRouteImpedance;
		private CheckBox chkRouteUseHierarchy;
		private Label labelRouteOutputLines;
		private ComboBox cboRouteOutputLines;
		private CheckBox chkRouteUseTimeWindows;
		private CheckBox chkRoutePreserveLastStop;
		private CheckBox chkRoutePreserveFirstStop;
		private CheckBox chkRouteFindBestSequence;
		private CheckBox chkRouteUseStartTime;
		private TextBox txtRouteStartTime;
		private TextBox txtLayerName;
		private Label lblLayerName;
		private System.ComponentModel.Container components = null;
		private ComboBox cboCFOutputLines;
		private Label lblCFOutputLines;
		private ComboBox cboCFTravelDirection;
		private Label lblCFTravelDirection;
		private TextBox txtCFDefaultTargetFacilityCount;
		private Label lblCFDefaultTargetFacilityCount;
		private TextBox txtCFDefaultCutoff;
		private Label lblCFDefaultCutoff;
		private CheckBox chkCFIgnoreInvalidLocations;
		private ComboBox cboCFRestrictUTurns;
		private Label lblCFRestrictUTurns;
		private Label lblCFAccumulateAttributeNames;
		private CheckedListBox chklstCFAccumulateAttributeNames;
		private Label lblCFRestrictionAttributeNames;
		private CheckedListBox chklstCFRestrictionAttributeNames;
		private ComboBox cboCFImpedance;
		private Label lblCFImpedance;
		private CheckBox chkCFUseHierarchy;
		private CheckBox chkODIgnoreInvalidLocations;
		private ComboBox cboODRestrictUTurns;
		private Label lblODRestrictUTurns;
		private Label lblODAccumulateAttributeNames;
		private CheckedListBox chklstODAccumulateAttributeNames;
		private Label lblODRestrictionAttributeNames;
		private CheckedListBox chklstODRestrictionAttributeNames;
		private ComboBox cboODImpedance;
		private Label lblODImpedance;
		private CheckBox chkODUseHierarchy;
		private ComboBox cboODOutputLines;
		private Label lblODOutputLines;
		private TextBox txtODDefaultTargetDestinationCount;
		private Label lblODDefaultTargetDestinationCount;
		private TextBox txtODDefaultCutoff;
		private Label lblODDefaultCutoff;
		private TextBox txtSADefaultBreaks;
		private Label lblSADefaultBreaks;
		private ComboBox cboSAImpedance;
		private Label lblSAImpedance;
		private Label lblSAOutputPolygons;
		private ComboBox cboSAOutputPolygons;
		private Label lblSAOutputLines;
		private ComboBox cboSAOutputLines;
		private CheckBox chkSAMergeSimilarPolygonRanges;
		private CheckBox chkSAIgnoreInvalidLocations;
		private ComboBox cboSARestrictUTurns;
		private Label lblSARestrictUTurns;
		private Label lblSAAccumulateAttributeNames;
		private CheckedListBox chklstSAAccumulateAttributeNames;
		private Label lblSARestrictionAttributeNames;
		private CheckedListBox chklstSARestrictionAttributeNames;
		private CheckBox chkSAOverlapLines;
		private CheckBox chkSASplitPolygonsAtBreaks;
		private CheckBox chkSAOverlapPolygons;
		private CheckBox chkSASplitLinesAtBreaks;
		private ComboBox cboSATrimPolygonDistanceUnits;
		private TextBox txtSATrimPolygonDistance;
		private CheckBox chkSATrimOuterPolygon;
		private CheckBox chkSAIncludeSourceInformationOnLines;
		private ComboBox cboSATravelDirection;
		private Label lblSATravelDirection;
		private Label lblMaxSearchTolerance;
		private ComboBox cboMaxSearchToleranceUnits;
		private GroupBox gbSettings;
		private CheckBox chkVRPUseHierarchy;
		private ComboBox cboVRPOutputShapeType;
		private ComboBox cboVRPAllowUTurns;
		private ComboBox cboVRPTimeFieldUnits;
		private TextBox txtVRPCapacityCount;
		private TextBox txtVRPDefaultDate;
		private ComboBox cboVRPDistanceAttribute;
		private ComboBox cboVRPTimeAttribute;
		private Label label7;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label2;
		private Label label1;
		private Label lblTimeAttribute;
		private GroupBox gbRestrictions;
		private CheckedListBox chklstVRPRestrictionAttributeNames;
		private ComboBox cboVRPTimeWindow;
		private Label label10;
		private Label label9;
		private ComboBox cboVRPTransitTime;
		private ComboBox cboVRPDistanceFieldUnits;
		private TabPage tabLocationAllocation;
		private Label lblTargetMarketShare;
		private TextBox txtLATargetMarketShare;
		private ComboBox cboLAImpTransformation;
		private Label lblImpParameter;
		private TextBox txtLAImpParameter;
		private Label lblImpTransformation;
		private Label lblCostAttribute;
		private ComboBox cboLAImpedance;
		private Label lblProblemType;
		private ComboBox cboLAProblemType;
		private Label lblCutOff;
		private TextBox txtLACutOff;
		private Label lblNumFacilities;
		private TextBox txtLAFacilitiesToLocate;
		private ComboBox cboLAOutputLines;
		private Label label11;
		private ComboBox cboLATravelDirection;
		private Label label12;
		private Label lblLAAccumulateAttributeNames;
		private CheckedListBox chklstLAAccumulateAttributeNames;
		private Label lblLARestrictionAttributeNames;
		private CheckedListBox chklstLARestrictionAttributeNames;
		private CheckBox chkLAUseHierarchy;
		private GroupBox grpLASettings;
		private CheckBox chkLAIgnoreInvalidLocations;
        private Label label8;
        private ComboBox cboCFTimeUsage;
        private TextBox txtCFUseTime;
        private CheckBox chkODUseTime;
        private TextBox txtODUseTime;
        private CheckBox chkSAUseTime;
        private TextBox txtSAUseTime;
        private CheckBox chkLAUseTime;
        private TextBox txtLAUseTime;
        private Label label13;
        private TabPage tabAttributeParameters;
        private DataGridView attributeParameterGrid;
        private DataGridViewTextBoxColumn dgvcAttribute;
        private DataGridViewTextBoxColumn dgvcParameter;
        private DataGridViewTextBoxColumn dgvcValue;
        private Label label14;
        private Button btnReset;
		private TextBox txtMaxSearchTolerance;

		#endregion

		#region Windows Form Designer generated code (InitializeComponent)
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabPropPages = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.lblMaxSearchTolerance = new System.Windows.Forms.Label();
            this.cboMaxSearchToleranceUnits = new System.Windows.Forms.ComboBox();
            this.txtMaxSearchTolerance = new System.Windows.Forms.TextBox();
            this.txtLayerName = new System.Windows.Forms.TextBox();
            this.lblLayerName = new System.Windows.Forms.Label();
            this.tabRoute = new System.Windows.Forms.TabPage();
            this.labelRouteOutputLines = new System.Windows.Forms.Label();
            this.cboRouteOutputLines = new System.Windows.Forms.ComboBox();
            this.chkRouteUseTimeWindows = new System.Windows.Forms.CheckBox();
            this.chkRoutePreserveLastStop = new System.Windows.Forms.CheckBox();
            this.chkRoutePreserveFirstStop = new System.Windows.Forms.CheckBox();
            this.chkRouteFindBestSequence = new System.Windows.Forms.CheckBox();
            this.chkRouteUseStartTime = new System.Windows.Forms.CheckBox();
            this.txtRouteStartTime = new System.Windows.Forms.TextBox();
            this.chkRouteIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
            this.cboRouteRestrictUTurns = new System.Windows.Forms.ComboBox();
            this.lblRouteRestrictUTurns = new System.Windows.Forms.Label();
            this.lblRouteAccumulateAttributeNames = new System.Windows.Forms.Label();
            this.chklstRouteAccumulateAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblRouteRestrictionAttributeNames = new System.Windows.Forms.Label();
            this.chklstRouteRestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.cboRouteImpedance = new System.Windows.Forms.ComboBox();
            this.lblRouteImpedance = new System.Windows.Forms.Label();
            this.chkRouteUseHierarchy = new System.Windows.Forms.CheckBox();
            this.tabClosestFacility = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboCFTimeUsage = new System.Windows.Forms.ComboBox();
            this.txtCFUseTime = new System.Windows.Forms.TextBox();
            this.chkCFIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
            this.cboCFRestrictUTurns = new System.Windows.Forms.ComboBox();
            this.lblCFRestrictUTurns = new System.Windows.Forms.Label();
            this.lblCFAccumulateAttributeNames = new System.Windows.Forms.Label();
            this.chklstCFAccumulateAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblCFRestrictionAttributeNames = new System.Windows.Forms.Label();
            this.chklstCFRestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.cboCFImpedance = new System.Windows.Forms.ComboBox();
            this.lblCFImpedance = new System.Windows.Forms.Label();
            this.chkCFUseHierarchy = new System.Windows.Forms.CheckBox();
            this.cboCFOutputLines = new System.Windows.Forms.ComboBox();
            this.lblCFOutputLines = new System.Windows.Forms.Label();
            this.cboCFTravelDirection = new System.Windows.Forms.ComboBox();
            this.lblCFTravelDirection = new System.Windows.Forms.Label();
            this.txtCFDefaultTargetFacilityCount = new System.Windows.Forms.TextBox();
            this.lblCFDefaultTargetFacilityCount = new System.Windows.Forms.Label();
            this.txtCFDefaultCutoff = new System.Windows.Forms.TextBox();
            this.lblCFDefaultCutoff = new System.Windows.Forms.Label();
            this.tabODCostMatrix = new System.Windows.Forms.TabPage();
            this.chkODUseTime = new System.Windows.Forms.CheckBox();
            this.txtODUseTime = new System.Windows.Forms.TextBox();
            this.chkODIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
            this.cboODRestrictUTurns = new System.Windows.Forms.ComboBox();
            this.lblODRestrictUTurns = new System.Windows.Forms.Label();
            this.lblODAccumulateAttributeNames = new System.Windows.Forms.Label();
            this.chklstODAccumulateAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblODRestrictionAttributeNames = new System.Windows.Forms.Label();
            this.chklstODRestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.cboODImpedance = new System.Windows.Forms.ComboBox();
            this.lblODImpedance = new System.Windows.Forms.Label();
            this.chkODUseHierarchy = new System.Windows.Forms.CheckBox();
            this.cboODOutputLines = new System.Windows.Forms.ComboBox();
            this.lblODOutputLines = new System.Windows.Forms.Label();
            this.txtODDefaultTargetDestinationCount = new System.Windows.Forms.TextBox();
            this.lblODDefaultTargetDestinationCount = new System.Windows.Forms.Label();
            this.txtODDefaultCutoff = new System.Windows.Forms.TextBox();
            this.lblODDefaultCutoff = new System.Windows.Forms.Label();
            this.tabServiceArea = new System.Windows.Forms.TabPage();
            this.chkSAUseTime = new System.Windows.Forms.CheckBox();
            this.txtSAUseTime = new System.Windows.Forms.TextBox();
            this.cboSATrimPolygonDistanceUnits = new System.Windows.Forms.ComboBox();
            this.txtSATrimPolygonDistance = new System.Windows.Forms.TextBox();
            this.chkSATrimOuterPolygon = new System.Windows.Forms.CheckBox();
            this.chkSAIncludeSourceInformationOnLines = new System.Windows.Forms.CheckBox();
            this.cboSATravelDirection = new System.Windows.Forms.ComboBox();
            this.lblSATravelDirection = new System.Windows.Forms.Label();
            this.chkSASplitPolygonsAtBreaks = new System.Windows.Forms.CheckBox();
            this.chkSAOverlapPolygons = new System.Windows.Forms.CheckBox();
            this.chkSASplitLinesAtBreaks = new System.Windows.Forms.CheckBox();
            this.chkSAOverlapLines = new System.Windows.Forms.CheckBox();
            this.chkSAIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
            this.cboSARestrictUTurns = new System.Windows.Forms.ComboBox();
            this.lblSARestrictUTurns = new System.Windows.Forms.Label();
            this.lblSAAccumulateAttributeNames = new System.Windows.Forms.Label();
            this.chklstSAAccumulateAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblSARestrictionAttributeNames = new System.Windows.Forms.Label();
            this.chklstSARestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblSAOutputPolygons = new System.Windows.Forms.Label();
            this.cboSAOutputPolygons = new System.Windows.Forms.ComboBox();
            this.lblSAOutputLines = new System.Windows.Forms.Label();
            this.cboSAOutputLines = new System.Windows.Forms.ComboBox();
            this.chkSAMergeSimilarPolygonRanges = new System.Windows.Forms.CheckBox();
            this.txtSADefaultBreaks = new System.Windows.Forms.TextBox();
            this.lblSADefaultBreaks = new System.Windows.Forms.Label();
            this.cboSAImpedance = new System.Windows.Forms.ComboBox();
            this.lblSAImpedance = new System.Windows.Forms.Label();
            this.tabVRP = new System.Windows.Forms.TabPage();
            this.gbRestrictions = new System.Windows.Forms.GroupBox();
            this.chklstVRPRestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.cboVRPDistanceFieldUnits = new System.Windows.Forms.ComboBox();
            this.cboVRPTransitTime = new System.Windows.Forms.ComboBox();
            this.cboVRPTimeWindow = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkVRPUseHierarchy = new System.Windows.Forms.CheckBox();
            this.cboVRPOutputShapeType = new System.Windows.Forms.ComboBox();
            this.cboVRPAllowUTurns = new System.Windows.Forms.ComboBox();
            this.cboVRPTimeFieldUnits = new System.Windows.Forms.ComboBox();
            this.txtVRPCapacityCount = new System.Windows.Forms.TextBox();
            this.txtVRPDefaultDate = new System.Windows.Forms.TextBox();
            this.cboVRPDistanceAttribute = new System.Windows.Forms.ComboBox();
            this.cboVRPTimeAttribute = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimeAttribute = new System.Windows.Forms.Label();
            this.tabLocationAllocation = new System.Windows.Forms.TabPage();
            this.chkLAUseTime = new System.Windows.Forms.CheckBox();
            this.txtLAUseTime = new System.Windows.Forms.TextBox();
            this.chkLAIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
            this.grpLASettings = new System.Windows.Forms.GroupBox();
            this.lblTargetMarketShare = new System.Windows.Forms.Label();
            this.txtLATargetMarketShare = new System.Windows.Forms.TextBox();
            this.cboLAImpTransformation = new System.Windows.Forms.ComboBox();
            this.lblImpParameter = new System.Windows.Forms.Label();
            this.txtLAImpParameter = new System.Windows.Forms.TextBox();
            this.lblImpTransformation = new System.Windows.Forms.Label();
            this.lblProblemType = new System.Windows.Forms.Label();
            this.cboLAProblemType = new System.Windows.Forms.ComboBox();
            this.lblCutOff = new System.Windows.Forms.Label();
            this.txtLACutOff = new System.Windows.Forms.TextBox();
            this.lblNumFacilities = new System.Windows.Forms.Label();
            this.txtLAFacilitiesToLocate = new System.Windows.Forms.TextBox();
            this.chkLAUseHierarchy = new System.Windows.Forms.CheckBox();
            this.lblLAAccumulateAttributeNames = new System.Windows.Forms.Label();
            this.chklstLAAccumulateAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.lblLARestrictionAttributeNames = new System.Windows.Forms.Label();
            this.chklstLARestrictionAttributeNames = new System.Windows.Forms.CheckedListBox();
            this.cboLAOutputLines = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboLATravelDirection = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCostAttribute = new System.Windows.Forms.Label();
            this.cboLAImpedance = new System.Windows.Forms.ComboBox();
            this.tabAttributeParameters = new System.Windows.Forms.TabPage();
            this.btnReset = new System.Windows.Forms.Button();
            this.attributeParameterGrid = new System.Windows.Forms.DataGridView();
            this.dgvcAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPropPages.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabRoute.SuspendLayout();
            this.tabClosestFacility.SuspendLayout();
            this.tabODCostMatrix.SuspendLayout();
            this.tabServiceArea.SuspendLayout();
            this.tabVRP.SuspendLayout();
            this.gbRestrictions.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.tabLocationAllocation.SuspendLayout();
            this.grpLASettings.SuspendLayout();
            this.tabAttributeParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attributeParameterGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPropPages
            // 
            this.tabPropPages.Controls.Add(this.tabGeneral);
            this.tabPropPages.Controls.Add(this.tabRoute);
            this.tabPropPages.Controls.Add(this.tabClosestFacility);
            this.tabPropPages.Controls.Add(this.tabODCostMatrix);
            this.tabPropPages.Controls.Add(this.tabServiceArea);
            this.tabPropPages.Controls.Add(this.tabVRP);
            this.tabPropPages.Controls.Add(this.tabLocationAllocation);
            this.tabPropPages.Controls.Add(this.tabAttributeParameters);
            this.tabPropPages.Location = new System.Drawing.Point(8, 8);
            this.tabPropPages.Name = "tabPropPages";
            this.tabPropPages.SelectedIndex = 0;
            this.tabPropPages.Size = new System.Drawing.Size(719, 499);
            this.tabPropPages.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.lblMaxSearchTolerance);
            this.tabGeneral.Controls.Add(this.cboMaxSearchToleranceUnits);
            this.tabGeneral.Controls.Add(this.txtMaxSearchTolerance);
            this.tabGeneral.Controls.Add(this.txtLayerName);
            this.tabGeneral.Controls.Add(this.lblLayerName);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(711, 473);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // lblMaxSearchTolerance
            // 
            this.lblMaxSearchTolerance.Location = new System.Drawing.Point(24, 64);
            this.lblMaxSearchTolerance.Name = "lblMaxSearchTolerance";
            this.lblMaxSearchTolerance.Size = new System.Drawing.Size(100, 24);
            this.lblMaxSearchTolerance.TabIndex = 123;
            this.lblMaxSearchTolerance.Text = "Search Tolerance";
            // 
            // cboMaxSearchToleranceUnits
            // 
            this.cboMaxSearchToleranceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaxSearchToleranceUnits.ItemHeight = 13;
            this.cboMaxSearchToleranceUnits.Items.AddRange(new object[] {
            "Unknown Units",
            "Inches",
            "Points",
            "Feet",
            "Yards",
            "Miles",
            "Nautical Miles",
            "Millimeters",
            "Centimeters",
            "Meters",
            "Kilometers",
            "DecimalDegrees",
            "Decimeters"});
            this.cboMaxSearchToleranceUnits.Location = new System.Drawing.Point(258, 61);
            this.cboMaxSearchToleranceUnits.Name = "cboMaxSearchToleranceUnits";
            this.cboMaxSearchToleranceUnits.Size = new System.Drawing.Size(130, 21);
            this.cboMaxSearchToleranceUnits.TabIndex = 122;
            // 
            // txtMaxSearchTolerance
            // 
            this.txtMaxSearchTolerance.Location = new System.Drawing.Point(130, 62);
            this.txtMaxSearchTolerance.Name = "txtMaxSearchTolerance";
            this.txtMaxSearchTolerance.Size = new System.Drawing.Size(122, 20);
            this.txtMaxSearchTolerance.TabIndex = 121;
            // 
            // txtLayerName
            // 
            this.txtLayerName.Location = new System.Drawing.Point(130, 32);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new System.Drawing.Size(258, 20);
            this.txtLayerName.TabIndex = 1;
            // 
            // lblLayerName
            // 
            this.lblLayerName.Location = new System.Drawing.Point(24, 35);
            this.lblLayerName.Name = "lblLayerName";
            this.lblLayerName.Size = new System.Drawing.Size(88, 24);
            this.lblLayerName.TabIndex = 0;
            this.lblLayerName.Text = "Layer Name";
            // 
            // tabRoute
            // 
            this.tabRoute.Controls.Add(this.labelRouteOutputLines);
            this.tabRoute.Controls.Add(this.cboRouteOutputLines);
            this.tabRoute.Controls.Add(this.chkRouteUseTimeWindows);
            this.tabRoute.Controls.Add(this.chkRoutePreserveLastStop);
            this.tabRoute.Controls.Add(this.chkRoutePreserveFirstStop);
            this.tabRoute.Controls.Add(this.chkRouteFindBestSequence);
            this.tabRoute.Controls.Add(this.chkRouteUseStartTime);
            this.tabRoute.Controls.Add(this.txtRouteStartTime);
            this.tabRoute.Controls.Add(this.chkRouteIgnoreInvalidLocations);
            this.tabRoute.Controls.Add(this.cboRouteRestrictUTurns);
            this.tabRoute.Controls.Add(this.lblRouteRestrictUTurns);
            this.tabRoute.Controls.Add(this.lblRouteAccumulateAttributeNames);
            this.tabRoute.Controls.Add(this.chklstRouteAccumulateAttributeNames);
            this.tabRoute.Controls.Add(this.lblRouteRestrictionAttributeNames);
            this.tabRoute.Controls.Add(this.chklstRouteRestrictionAttributeNames);
            this.tabRoute.Controls.Add(this.cboRouteImpedance);
            this.tabRoute.Controls.Add(this.lblRouteImpedance);
            this.tabRoute.Controls.Add(this.chkRouteUseHierarchy);
            this.tabRoute.Location = new System.Drawing.Point(4, 22);
            this.tabRoute.Name = "tabRoute";
            this.tabRoute.Size = new System.Drawing.Size(711, 473);
            this.tabRoute.TabIndex = 1;
            this.tabRoute.Text = "Route";
            this.tabRoute.UseVisualStyleBackColor = true;
            // 
            // labelRouteOutputLines
            // 
            this.labelRouteOutputLines.Location = new System.Drawing.Point(20, 209);
            this.labelRouteOutputLines.Name = "labelRouteOutputLines";
            this.labelRouteOutputLines.Size = new System.Drawing.Size(40, 16);
            this.labelRouteOutputLines.TabIndex = 96;
            this.labelRouteOutputLines.Text = "Shape";
            // 
            // cboRouteOutputLines
            // 
            this.cboRouteOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRouteOutputLines.ItemHeight = 13;
            this.cboRouteOutputLines.Items.AddRange(new object[] {
            "No Lines",
            "Straight Lines",
            "True Shape",
            "True Shape With Measures"});
            this.cboRouteOutputLines.Location = new System.Drawing.Point(148, 204);
            this.cboRouteOutputLines.Name = "cboRouteOutputLines";
            this.cboRouteOutputLines.Size = new System.Drawing.Size(200, 21);
            this.cboRouteOutputLines.TabIndex = 95;
            // 
            // chkRouteUseTimeWindows
            // 
            this.chkRouteUseTimeWindows.Location = new System.Drawing.Point(20, 76);
            this.chkRouteUseTimeWindows.Name = "chkRouteUseTimeWindows";
            this.chkRouteUseTimeWindows.Size = new System.Drawing.Size(128, 16);
            this.chkRouteUseTimeWindows.TabIndex = 92;
            this.chkRouteUseTimeWindows.Text = "Use Time Windows";
            // 
            // chkRoutePreserveLastStop
            // 
            this.chkRoutePreserveLastStop.Location = new System.Drawing.Point(39, 151);
            this.chkRoutePreserveLastStop.Name = "chkRoutePreserveLastStop";
            this.chkRoutePreserveLastStop.Size = new System.Drawing.Size(331, 23);
            this.chkRoutePreserveLastStop.TabIndex = 91;
            this.chkRoutePreserveLastStop.Text = "Preserve Last Stop";
            // 
            // chkRoutePreserveFirstStop
            // 
            this.chkRoutePreserveFirstStop.Location = new System.Drawing.Point(39, 123);
            this.chkRoutePreserveFirstStop.Name = "chkRoutePreserveFirstStop";
            this.chkRoutePreserveFirstStop.Size = new System.Drawing.Size(331, 28);
            this.chkRoutePreserveFirstStop.TabIndex = 90;
            this.chkRoutePreserveFirstStop.Text = "Preserve First Stop";
            // 
            // chkRouteFindBestSequence
            // 
            this.chkRouteFindBestSequence.Checked = true;
            this.chkRouteFindBestSequence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRouteFindBestSequence.Location = new System.Drawing.Point(20, 98);
            this.chkRouteFindBestSequence.Name = "chkRouteFindBestSequence";
            this.chkRouteFindBestSequence.Size = new System.Drawing.Size(336, 32);
            this.chkRouteFindBestSequence.TabIndex = 89;
            this.chkRouteFindBestSequence.Text = "Find Best Sequence";
            this.chkRouteFindBestSequence.CheckedChanged += new System.EventHandler(this.chkRouteFindBestSequence_CheckedChanged);
            // 
            // chkRouteUseStartTime
            // 
            this.chkRouteUseStartTime.Checked = true;
            this.chkRouteUseStartTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRouteUseStartTime.Location = new System.Drawing.Point(20, 54);
            this.chkRouteUseStartTime.Name = "chkRouteUseStartTime";
            this.chkRouteUseStartTime.Size = new System.Drawing.Size(104, 16);
            this.chkRouteUseStartTime.TabIndex = 93;
            this.chkRouteUseStartTime.Text = "Use Start Time";
            this.chkRouteUseStartTime.CheckedChanged += new System.EventHandler(this.chkRouteUseStartTime_CheckedChanged);
            // 
            // txtRouteStartTime
            // 
            this.txtRouteStartTime.Location = new System.Drawing.Point(151, 50);
            this.txtRouteStartTime.Name = "txtRouteStartTime";
            this.txtRouteStartTime.Size = new System.Drawing.Size(200, 20);
            this.txtRouteStartTime.TabIndex = 94;
            // 
            // chkRouteIgnoreInvalidLocations
            // 
            this.chkRouteIgnoreInvalidLocations.Location = new System.Drawing.Point(20, 252);
            this.chkRouteIgnoreInvalidLocations.Name = "chkRouteIgnoreInvalidLocations";
            this.chkRouteIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 29);
            this.chkRouteIgnoreInvalidLocations.TabIndex = 81;
            this.chkRouteIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
            // 
            // cboRouteRestrictUTurns
            // 
            this.cboRouteRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRouteRestrictUTurns.ItemHeight = 13;
            this.cboRouteRestrictUTurns.Items.AddRange(new object[] {
            "No U-Turns",
            "Allow U-Turns",
            "Only At Dead Ends"});
            this.cboRouteRestrictUTurns.Location = new System.Drawing.Point(148, 177);
            this.cboRouteRestrictUTurns.Name = "cboRouteRestrictUTurns";
            this.cboRouteRestrictUTurns.Size = new System.Drawing.Size(200, 21);
            this.cboRouteRestrictUTurns.TabIndex = 80;
            // 
            // lblRouteRestrictUTurns
            // 
            this.lblRouteRestrictUTurns.Location = new System.Drawing.Point(20, 182);
            this.lblRouteRestrictUTurns.Name = "lblRouteRestrictUTurns";
            this.lblRouteRestrictUTurns.Size = new System.Drawing.Size(88, 16);
            this.lblRouteRestrictUTurns.TabIndex = 88;
            this.lblRouteRestrictUTurns.Text = "UTurn Policy";
            // 
            // lblRouteAccumulateAttributeNames
            // 
            this.lblRouteAccumulateAttributeNames.Location = new System.Drawing.Point(236, 284);
            this.lblRouteAccumulateAttributeNames.Name = "lblRouteAccumulateAttributeNames";
            this.lblRouteAccumulateAttributeNames.Size = new System.Drawing.Size(120, 16);
            this.lblRouteAccumulateAttributeNames.TabIndex = 87;
            this.lblRouteAccumulateAttributeNames.Text = "Accumulate Attributes";
            // 
            // chklstRouteAccumulateAttributeNames
            // 
            this.chklstRouteAccumulateAttributeNames.CheckOnClick = true;
            this.chklstRouteAccumulateAttributeNames.Location = new System.Drawing.Point(236, 300);
            this.chklstRouteAccumulateAttributeNames.Name = "chklstRouteAccumulateAttributeNames";
            this.chklstRouteAccumulateAttributeNames.ScrollAlwaysVisible = true;
            this.chklstRouteAccumulateAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstRouteAccumulateAttributeNames.TabIndex = 84;
            // 
            // lblRouteRestrictionAttributeNames
            // 
            this.lblRouteRestrictionAttributeNames.Location = new System.Drawing.Point(20, 284);
            this.lblRouteRestrictionAttributeNames.Name = "lblRouteRestrictionAttributeNames";
            this.lblRouteRestrictionAttributeNames.Size = new System.Drawing.Size(72, 16);
            this.lblRouteRestrictionAttributeNames.TabIndex = 86;
            this.lblRouteRestrictionAttributeNames.Text = "Restrictions";
            // 
            // chklstRouteRestrictionAttributeNames
            // 
            this.chklstRouteRestrictionAttributeNames.CheckOnClick = true;
            this.chklstRouteRestrictionAttributeNames.Location = new System.Drawing.Point(20, 300);
            this.chklstRouteRestrictionAttributeNames.Name = "chklstRouteRestrictionAttributeNames";
            this.chklstRouteRestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstRouteRestrictionAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstRouteRestrictionAttributeNames.TabIndex = 83;
            // 
            // cboRouteImpedance
            // 
            this.cboRouteImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRouteImpedance.ItemHeight = 13;
            this.cboRouteImpedance.Location = new System.Drawing.Point(151, 23);
            this.cboRouteImpedance.Name = "cboRouteImpedance";
            this.cboRouteImpedance.Size = new System.Drawing.Size(200, 21);
            this.cboRouteImpedance.TabIndex = 79;
            // 
            // lblRouteImpedance
            // 
            this.lblRouteImpedance.Location = new System.Drawing.Point(20, 28);
            this.lblRouteImpedance.Name = "lblRouteImpedance";
            this.lblRouteImpedance.Size = new System.Drawing.Size(64, 16);
            this.lblRouteImpedance.TabIndex = 85;
            this.lblRouteImpedance.Text = "Impedance";
            // 
            // chkRouteUseHierarchy
            // 
            this.chkRouteUseHierarchy.Location = new System.Drawing.Point(20, 228);
            this.chkRouteUseHierarchy.Name = "chkRouteUseHierarchy";
            this.chkRouteUseHierarchy.Size = new System.Drawing.Size(96, 26);
            this.chkRouteUseHierarchy.TabIndex = 82;
            this.chkRouteUseHierarchy.Text = "Use Hierarchy";
            // 
            // tabClosestFacility
            // 
            this.tabClosestFacility.Controls.Add(this.label13);
            this.tabClosestFacility.Controls.Add(this.label8);
            this.tabClosestFacility.Controls.Add(this.cboCFTimeUsage);
            this.tabClosestFacility.Controls.Add(this.txtCFUseTime);
            this.tabClosestFacility.Controls.Add(this.chkCFIgnoreInvalidLocations);
            this.tabClosestFacility.Controls.Add(this.cboCFRestrictUTurns);
            this.tabClosestFacility.Controls.Add(this.lblCFRestrictUTurns);
            this.tabClosestFacility.Controls.Add(this.lblCFAccumulateAttributeNames);
            this.tabClosestFacility.Controls.Add(this.chklstCFAccumulateAttributeNames);
            this.tabClosestFacility.Controls.Add(this.lblCFRestrictionAttributeNames);
            this.tabClosestFacility.Controls.Add(this.chklstCFRestrictionAttributeNames);
            this.tabClosestFacility.Controls.Add(this.cboCFImpedance);
            this.tabClosestFacility.Controls.Add(this.lblCFImpedance);
            this.tabClosestFacility.Controls.Add(this.chkCFUseHierarchy);
            this.tabClosestFacility.Controls.Add(this.cboCFOutputLines);
            this.tabClosestFacility.Controls.Add(this.lblCFOutputLines);
            this.tabClosestFacility.Controls.Add(this.cboCFTravelDirection);
            this.tabClosestFacility.Controls.Add(this.lblCFTravelDirection);
            this.tabClosestFacility.Controls.Add(this.txtCFDefaultTargetFacilityCount);
            this.tabClosestFacility.Controls.Add(this.lblCFDefaultTargetFacilityCount);
            this.tabClosestFacility.Controls.Add(this.txtCFDefaultCutoff);
            this.tabClosestFacility.Controls.Add(this.lblCFDefaultCutoff);
            this.tabClosestFacility.Location = new System.Drawing.Point(4, 22);
            this.tabClosestFacility.Name = "tabClosestFacility";
            this.tabClosestFacility.Size = new System.Drawing.Size(711, 473);
            this.tabClosestFacility.TabIndex = 3;
            this.tabClosestFacility.Text = "Closest Facility";
            this.tabClosestFacility.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(20, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 16);
            this.label13.TabIndex = 117;
            this.label13.Text = "Time";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 16);
            this.label8.TabIndex = 116;
            this.label8.Text = "Time Usage";
            // 
            // cboCFTimeUsage
            // 
            this.cboCFTimeUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCFTimeUsage.ItemHeight = 13;
            this.cboCFTimeUsage.Items.AddRange(new object[] {
            "Not used",
            "End time",
            "Start time"});
            this.cboCFTimeUsage.Location = new System.Drawing.Point(151, 49);
            this.cboCFTimeUsage.Name = "cboCFTimeUsage";
            this.cboCFTimeUsage.Size = new System.Drawing.Size(200, 21);
            this.cboCFTimeUsage.TabIndex = 115;
            // 
            // txtCFUseTime
            // 
            this.txtCFUseTime.Location = new System.Drawing.Point(151, 75);
            this.txtCFUseTime.Name = "txtCFUseTime";
            this.txtCFUseTime.Size = new System.Drawing.Size(200, 20);
            this.txtCFUseTime.TabIndex = 114;
            // 
            // chkCFIgnoreInvalidLocations
            // 
            this.chkCFIgnoreInvalidLocations.Location = new System.Drawing.Point(20, 270);
            this.chkCFIgnoreInvalidLocations.Name = "chkCFIgnoreInvalidLocations";
            this.chkCFIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 29);
            this.chkCFIgnoreInvalidLocations.TabIndex = 105;
            this.chkCFIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
            // 
            // cboCFRestrictUTurns
            // 
            this.cboCFRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCFRestrictUTurns.ItemHeight = 13;
            this.cboCFRestrictUTurns.Items.AddRange(new object[] {
            "No U-Turns",
            "Allow U-Turns",
            "Only At Dead Ends"});
            this.cboCFRestrictUTurns.Location = new System.Drawing.Point(151, 179);
            this.cboCFRestrictUTurns.Name = "cboCFRestrictUTurns";
            this.cboCFRestrictUTurns.Size = new System.Drawing.Size(200, 21);
            this.cboCFRestrictUTurns.TabIndex = 104;
            // 
            // lblCFRestrictUTurns
            // 
            this.lblCFRestrictUTurns.Location = new System.Drawing.Point(20, 184);
            this.lblCFRestrictUTurns.Name = "lblCFRestrictUTurns";
            this.lblCFRestrictUTurns.Size = new System.Drawing.Size(88, 16);
            this.lblCFRestrictUTurns.TabIndex = 112;
            this.lblCFRestrictUTurns.Text = "UTurn Policy";
            // 
            // lblCFAccumulateAttributeNames
            // 
            this.lblCFAccumulateAttributeNames.Location = new System.Drawing.Point(236, 302);
            this.lblCFAccumulateAttributeNames.Name = "lblCFAccumulateAttributeNames";
            this.lblCFAccumulateAttributeNames.Size = new System.Drawing.Size(120, 16);
            this.lblCFAccumulateAttributeNames.TabIndex = 111;
            this.lblCFAccumulateAttributeNames.Text = "Accumulate Attributes";
            // 
            // chklstCFAccumulateAttributeNames
            // 
            this.chklstCFAccumulateAttributeNames.CheckOnClick = true;
            this.chklstCFAccumulateAttributeNames.Location = new System.Drawing.Point(236, 318);
            this.chklstCFAccumulateAttributeNames.Name = "chklstCFAccumulateAttributeNames";
            this.chklstCFAccumulateAttributeNames.ScrollAlwaysVisible = true;
            this.chklstCFAccumulateAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstCFAccumulateAttributeNames.TabIndex = 108;
            // 
            // lblCFRestrictionAttributeNames
            // 
            this.lblCFRestrictionAttributeNames.Location = new System.Drawing.Point(20, 302);
            this.lblCFRestrictionAttributeNames.Name = "lblCFRestrictionAttributeNames";
            this.lblCFRestrictionAttributeNames.Size = new System.Drawing.Size(72, 16);
            this.lblCFRestrictionAttributeNames.TabIndex = 110;
            this.lblCFRestrictionAttributeNames.Text = "Restrictions";
            // 
            // chklstCFRestrictionAttributeNames
            // 
            this.chklstCFRestrictionAttributeNames.CheckOnClick = true;
            this.chklstCFRestrictionAttributeNames.Location = new System.Drawing.Point(20, 318);
            this.chklstCFRestrictionAttributeNames.Name = "chklstCFRestrictionAttributeNames";
            this.chklstCFRestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstCFRestrictionAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstCFRestrictionAttributeNames.TabIndex = 107;
            // 
            // cboCFImpedance
            // 
            this.cboCFImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCFImpedance.ItemHeight = 13;
            this.cboCFImpedance.Location = new System.Drawing.Point(151, 23);
            this.cboCFImpedance.Name = "cboCFImpedance";
            this.cboCFImpedance.Size = new System.Drawing.Size(200, 21);
            this.cboCFImpedance.TabIndex = 103;
            // 
            // lblCFImpedance
            // 
            this.lblCFImpedance.Location = new System.Drawing.Point(20, 28);
            this.lblCFImpedance.Name = "lblCFImpedance";
            this.lblCFImpedance.Size = new System.Drawing.Size(64, 16);
            this.lblCFImpedance.TabIndex = 109;
            this.lblCFImpedance.Text = "Impedance";
            // 
            // chkCFUseHierarchy
            // 
            this.chkCFUseHierarchy.Location = new System.Drawing.Point(20, 238);
            this.chkCFUseHierarchy.Name = "chkCFUseHierarchy";
            this.chkCFUseHierarchy.Size = new System.Drawing.Size(96, 26);
            this.chkCFUseHierarchy.TabIndex = 106;
            this.chkCFUseHierarchy.Text = "Use Hierarchy";
            // 
            // cboCFOutputLines
            // 
            this.cboCFOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCFOutputLines.ItemHeight = 13;
            this.cboCFOutputLines.Items.AddRange(new object[] {
            "No Lines",
            "Straight Lines",
            "True Shape",
            "True Shape With Measures"});
            this.cboCFOutputLines.Location = new System.Drawing.Point(151, 206);
            this.cboCFOutputLines.Name = "cboCFOutputLines";
            this.cboCFOutputLines.Size = new System.Drawing.Size(200, 21);
            this.cboCFOutputLines.TabIndex = 101;
            // 
            // lblCFOutputLines
            // 
            this.lblCFOutputLines.Location = new System.Drawing.Point(20, 211);
            this.lblCFOutputLines.Name = "lblCFOutputLines";
            this.lblCFOutputLines.Size = new System.Drawing.Size(114, 16);
            this.lblCFOutputLines.TabIndex = 102;
            this.lblCFOutputLines.Text = "Shape";
            // 
            // cboCFTravelDirection
            // 
            this.cboCFTravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCFTravelDirection.ItemHeight = 13;
            this.cboCFTravelDirection.Items.AddRange(new object[] {
            "From Facility",
            "To Facility"});
            this.cboCFTravelDirection.Location = new System.Drawing.Point(151, 152);
            this.cboCFTravelDirection.Name = "cboCFTravelDirection";
            this.cboCFTravelDirection.Size = new System.Drawing.Size(200, 21);
            this.cboCFTravelDirection.TabIndex = 99;
            // 
            // lblCFTravelDirection
            // 
            this.lblCFTravelDirection.Location = new System.Drawing.Point(20, 157);
            this.lblCFTravelDirection.Name = "lblCFTravelDirection";
            this.lblCFTravelDirection.Size = new System.Drawing.Size(114, 16);
            this.lblCFTravelDirection.TabIndex = 100;
            this.lblCFTravelDirection.Text = "Travel Direction";
            // 
            // txtCFDefaultTargetFacilityCount
            // 
            this.txtCFDefaultTargetFacilityCount.Location = new System.Drawing.Point(151, 126);
            this.txtCFDefaultTargetFacilityCount.Name = "txtCFDefaultTargetFacilityCount";
            this.txtCFDefaultTargetFacilityCount.Size = new System.Drawing.Size(200, 20);
            this.txtCFDefaultTargetFacilityCount.TabIndex = 98;
            // 
            // lblCFDefaultTargetFacilityCount
            // 
            this.lblCFDefaultTargetFacilityCount.Location = new System.Drawing.Point(20, 130);
            this.lblCFDefaultTargetFacilityCount.Name = "lblCFDefaultTargetFacilityCount";
            this.lblCFDefaultTargetFacilityCount.Size = new System.Drawing.Size(114, 16);
            this.lblCFDefaultTargetFacilityCount.TabIndex = 97;
            this.lblCFDefaultTargetFacilityCount.Text = "Number of Facilities";
            // 
            // txtCFDefaultCutoff
            // 
            this.txtCFDefaultCutoff.Location = new System.Drawing.Point(151, 100);
            this.txtCFDefaultCutoff.Name = "txtCFDefaultCutoff";
            this.txtCFDefaultCutoff.Size = new System.Drawing.Size(200, 20);
            this.txtCFDefaultCutoff.TabIndex = 96;
            // 
            // lblCFDefaultCutoff
            // 
            this.lblCFDefaultCutoff.Location = new System.Drawing.Point(20, 104);
            this.lblCFDefaultCutoff.Name = "lblCFDefaultCutoff";
            this.lblCFDefaultCutoff.Size = new System.Drawing.Size(114, 16);
            this.lblCFDefaultCutoff.TabIndex = 95;
            this.lblCFDefaultCutoff.Text = "Default Cutoff";
            // 
            // tabODCostMatrix
            // 
            this.tabODCostMatrix.Controls.Add(this.chkODUseTime);
            this.tabODCostMatrix.Controls.Add(this.txtODUseTime);
            this.tabODCostMatrix.Controls.Add(this.chkODIgnoreInvalidLocations);
            this.tabODCostMatrix.Controls.Add(this.cboODRestrictUTurns);
            this.tabODCostMatrix.Controls.Add(this.lblODRestrictUTurns);
            this.tabODCostMatrix.Controls.Add(this.lblODAccumulateAttributeNames);
            this.tabODCostMatrix.Controls.Add(this.chklstODAccumulateAttributeNames);
            this.tabODCostMatrix.Controls.Add(this.lblODRestrictionAttributeNames);
            this.tabODCostMatrix.Controls.Add(this.chklstODRestrictionAttributeNames);
            this.tabODCostMatrix.Controls.Add(this.cboODImpedance);
            this.tabODCostMatrix.Controls.Add(this.lblODImpedance);
            this.tabODCostMatrix.Controls.Add(this.chkODUseHierarchy);
            this.tabODCostMatrix.Controls.Add(this.cboODOutputLines);
            this.tabODCostMatrix.Controls.Add(this.lblODOutputLines);
            this.tabODCostMatrix.Controls.Add(this.txtODDefaultTargetDestinationCount);
            this.tabODCostMatrix.Controls.Add(this.lblODDefaultTargetDestinationCount);
            this.tabODCostMatrix.Controls.Add(this.txtODDefaultCutoff);
            this.tabODCostMatrix.Controls.Add(this.lblODDefaultCutoff);
            this.tabODCostMatrix.Location = new System.Drawing.Point(4, 22);
            this.tabODCostMatrix.Name = "tabODCostMatrix";
            this.tabODCostMatrix.Size = new System.Drawing.Size(711, 473);
            this.tabODCostMatrix.TabIndex = 4;
            this.tabODCostMatrix.Text = "Origin-Destination Cost Matrix";
            this.tabODCostMatrix.UseVisualStyleBackColor = true;
            // 
            // chkODUseTime
            // 
            this.chkODUseTime.Location = new System.Drawing.Point(20, 54);
            this.chkODUseTime.Name = "chkODUseTime";
            this.chkODUseTime.Size = new System.Drawing.Size(104, 16);
            this.chkODUseTime.TabIndex = 131;
            this.chkODUseTime.Text = "Use Time";
            // 
            // txtODUseTime
            // 
            this.txtODUseTime.Location = new System.Drawing.Point(151, 50);
            this.txtODUseTime.Name = "txtODUseTime";
            this.txtODUseTime.Size = new System.Drawing.Size(200, 20);
            this.txtODUseTime.TabIndex = 132;
            // 
            // chkODIgnoreInvalidLocations
            // 
            this.chkODIgnoreInvalidLocations.Location = new System.Drawing.Point(20, 223);
            this.chkODIgnoreInvalidLocations.Name = "chkODIgnoreInvalidLocations";
            this.chkODIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 29);
            this.chkODIgnoreInvalidLocations.TabIndex = 123;
            this.chkODIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
            // 
            // cboODRestrictUTurns
            // 
            this.cboODRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboODRestrictUTurns.ItemHeight = 13;
            this.cboODRestrictUTurns.Items.AddRange(new object[] {
            "No U-Turns",
            "Allow U-Turns",
            "Only At Dead Ends"});
            this.cboODRestrictUTurns.Location = new System.Drawing.Point(151, 132);
            this.cboODRestrictUTurns.Name = "cboODRestrictUTurns";
            this.cboODRestrictUTurns.Size = new System.Drawing.Size(200, 21);
            this.cboODRestrictUTurns.TabIndex = 122;
            // 
            // lblODRestrictUTurns
            // 
            this.lblODRestrictUTurns.Location = new System.Drawing.Point(20, 137);
            this.lblODRestrictUTurns.Name = "lblODRestrictUTurns";
            this.lblODRestrictUTurns.Size = new System.Drawing.Size(88, 16);
            this.lblODRestrictUTurns.TabIndex = 130;
            this.lblODRestrictUTurns.Text = "UTurn Policy";
            // 
            // lblODAccumulateAttributeNames
            // 
            this.lblODAccumulateAttributeNames.Location = new System.Drawing.Point(236, 255);
            this.lblODAccumulateAttributeNames.Name = "lblODAccumulateAttributeNames";
            this.lblODAccumulateAttributeNames.Size = new System.Drawing.Size(120, 16);
            this.lblODAccumulateAttributeNames.TabIndex = 129;
            this.lblODAccumulateAttributeNames.Text = "Accumulate Attributes";
            // 
            // chklstODAccumulateAttributeNames
            // 
            this.chklstODAccumulateAttributeNames.CheckOnClick = true;
            this.chklstODAccumulateAttributeNames.Location = new System.Drawing.Point(236, 271);
            this.chklstODAccumulateAttributeNames.Name = "chklstODAccumulateAttributeNames";
            this.chklstODAccumulateAttributeNames.ScrollAlwaysVisible = true;
            this.chklstODAccumulateAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstODAccumulateAttributeNames.TabIndex = 126;
            // 
            // lblODRestrictionAttributeNames
            // 
            this.lblODRestrictionAttributeNames.Location = new System.Drawing.Point(20, 255);
            this.lblODRestrictionAttributeNames.Name = "lblODRestrictionAttributeNames";
            this.lblODRestrictionAttributeNames.Size = new System.Drawing.Size(72, 16);
            this.lblODRestrictionAttributeNames.TabIndex = 128;
            this.lblODRestrictionAttributeNames.Text = "Restrictions";
            // 
            // chklstODRestrictionAttributeNames
            // 
            this.chklstODRestrictionAttributeNames.CheckOnClick = true;
            this.chklstODRestrictionAttributeNames.Location = new System.Drawing.Point(20, 271);
            this.chklstODRestrictionAttributeNames.Name = "chklstODRestrictionAttributeNames";
            this.chklstODRestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstODRestrictionAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstODRestrictionAttributeNames.TabIndex = 125;
            // 
            // cboODImpedance
            // 
            this.cboODImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboODImpedance.ItemHeight = 13;
            this.cboODImpedance.Location = new System.Drawing.Point(151, 23);
            this.cboODImpedance.Name = "cboODImpedance";
            this.cboODImpedance.Size = new System.Drawing.Size(200, 21);
            this.cboODImpedance.TabIndex = 121;
            // 
            // lblODImpedance
            // 
            this.lblODImpedance.Location = new System.Drawing.Point(20, 28);
            this.lblODImpedance.Name = "lblODImpedance";
            this.lblODImpedance.Size = new System.Drawing.Size(64, 16);
            this.lblODImpedance.TabIndex = 127;
            this.lblODImpedance.Text = "Impedance";
            // 
            // chkODUseHierarchy
            // 
            this.chkODUseHierarchy.Location = new System.Drawing.Point(20, 191);
            this.chkODUseHierarchy.Name = "chkODUseHierarchy";
            this.chkODUseHierarchy.Size = new System.Drawing.Size(96, 26);
            this.chkODUseHierarchy.TabIndex = 124;
            this.chkODUseHierarchy.Text = "Use Hierarchy";
            // 
            // cboODOutputLines
            // 
            this.cboODOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboODOutputLines.ItemHeight = 13;
            this.cboODOutputLines.Items.AddRange(new object[] {
            "No Lines",
            "Straight Lines"});
            this.cboODOutputLines.Location = new System.Drawing.Point(151, 159);
            this.cboODOutputLines.Name = "cboODOutputLines";
            this.cboODOutputLines.Size = new System.Drawing.Size(200, 21);
            this.cboODOutputLines.TabIndex = 119;
            // 
            // lblODOutputLines
            // 
            this.lblODOutputLines.Location = new System.Drawing.Point(20, 164);
            this.lblODOutputLines.Name = "lblODOutputLines";
            this.lblODOutputLines.Size = new System.Drawing.Size(114, 16);
            this.lblODOutputLines.TabIndex = 120;
            this.lblODOutputLines.Text = "Shape";
            // 
            // txtODDefaultTargetDestinationCount
            // 
            this.txtODDefaultTargetDestinationCount.Location = new System.Drawing.Point(151, 103);
            this.txtODDefaultTargetDestinationCount.Name = "txtODDefaultTargetDestinationCount";
            this.txtODDefaultTargetDestinationCount.Size = new System.Drawing.Size(200, 20);
            this.txtODDefaultTargetDestinationCount.TabIndex = 116;
            // 
            // lblODDefaultTargetDestinationCount
            // 
            this.lblODDefaultTargetDestinationCount.Location = new System.Drawing.Point(20, 107);
            this.lblODDefaultTargetDestinationCount.Name = "lblODDefaultTargetDestinationCount";
            this.lblODDefaultTargetDestinationCount.Size = new System.Drawing.Size(125, 16);
            this.lblODDefaultTargetDestinationCount.TabIndex = 115;
            this.lblODDefaultTargetDestinationCount.Text = "Number of Destinations";
            // 
            // txtODDefaultCutoff
            // 
            this.txtODDefaultCutoff.Location = new System.Drawing.Point(151, 77);
            this.txtODDefaultCutoff.Name = "txtODDefaultCutoff";
            this.txtODDefaultCutoff.Size = new System.Drawing.Size(200, 20);
            this.txtODDefaultCutoff.TabIndex = 114;
            // 
            // lblODDefaultCutoff
            // 
            this.lblODDefaultCutoff.Location = new System.Drawing.Point(20, 81);
            this.lblODDefaultCutoff.Name = "lblODDefaultCutoff";
            this.lblODDefaultCutoff.Size = new System.Drawing.Size(114, 16);
            this.lblODDefaultCutoff.TabIndex = 113;
            this.lblODDefaultCutoff.Text = "Default Cutoff";
            // 
            // tabServiceArea
            // 
            this.tabServiceArea.Controls.Add(this.chkSAUseTime);
            this.tabServiceArea.Controls.Add(this.txtSAUseTime);
            this.tabServiceArea.Controls.Add(this.cboSATrimPolygonDistanceUnits);
            this.tabServiceArea.Controls.Add(this.txtSATrimPolygonDistance);
            this.tabServiceArea.Controls.Add(this.chkSATrimOuterPolygon);
            this.tabServiceArea.Controls.Add(this.chkSAIncludeSourceInformationOnLines);
            this.tabServiceArea.Controls.Add(this.cboSATravelDirection);
            this.tabServiceArea.Controls.Add(this.lblSATravelDirection);
            this.tabServiceArea.Controls.Add(this.chkSASplitPolygonsAtBreaks);
            this.tabServiceArea.Controls.Add(this.chkSAOverlapPolygons);
            this.tabServiceArea.Controls.Add(this.chkSASplitLinesAtBreaks);
            this.tabServiceArea.Controls.Add(this.chkSAOverlapLines);
            this.tabServiceArea.Controls.Add(this.chkSAIgnoreInvalidLocations);
            this.tabServiceArea.Controls.Add(this.cboSARestrictUTurns);
            this.tabServiceArea.Controls.Add(this.lblSARestrictUTurns);
            this.tabServiceArea.Controls.Add(this.lblSAAccumulateAttributeNames);
            this.tabServiceArea.Controls.Add(this.chklstSAAccumulateAttributeNames);
            this.tabServiceArea.Controls.Add(this.lblSARestrictionAttributeNames);
            this.tabServiceArea.Controls.Add(this.chklstSARestrictionAttributeNames);
            this.tabServiceArea.Controls.Add(this.lblSAOutputPolygons);
            this.tabServiceArea.Controls.Add(this.cboSAOutputPolygons);
            this.tabServiceArea.Controls.Add(this.lblSAOutputLines);
            this.tabServiceArea.Controls.Add(this.cboSAOutputLines);
            this.tabServiceArea.Controls.Add(this.chkSAMergeSimilarPolygonRanges);
            this.tabServiceArea.Controls.Add(this.txtSADefaultBreaks);
            this.tabServiceArea.Controls.Add(this.lblSADefaultBreaks);
            this.tabServiceArea.Controls.Add(this.cboSAImpedance);
            this.tabServiceArea.Controls.Add(this.lblSAImpedance);
            this.tabServiceArea.Location = new System.Drawing.Point(4, 22);
            this.tabServiceArea.Name = "tabServiceArea";
            this.tabServiceArea.Size = new System.Drawing.Size(711, 473);
            this.tabServiceArea.TabIndex = 2;
            this.tabServiceArea.Text = "Service Area";
            this.tabServiceArea.UseVisualStyleBackColor = true;
            // 
            // chkSAUseTime
            // 
            this.chkSAUseTime.Location = new System.Drawing.Point(20, 52);
            this.chkSAUseTime.Name = "chkSAUseTime";
            this.chkSAUseTime.Size = new System.Drawing.Size(104, 16);
            this.chkSAUseTime.TabIndex = 133;
            this.chkSAUseTime.Text = "Use Time";
            // 
            // txtSAUseTime
            // 
            this.txtSAUseTime.Location = new System.Drawing.Point(151, 48);
            this.txtSAUseTime.Name = "txtSAUseTime";
            this.txtSAUseTime.Size = new System.Drawing.Size(200, 20);
            this.txtSAUseTime.TabIndex = 134;
            // 
            // cboSATrimPolygonDistanceUnits
            // 
            this.cboSATrimPolygonDistanceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSATrimPolygonDistanceUnits.ItemHeight = 13;
            this.cboSATrimPolygonDistanceUnits.Items.AddRange(new object[] {
            "Unknown Units",
            "Inches",
            "Points",
            "Feet",
            "Yards",
            "Miles",
            "Nautical Miles",
            "Millimeters",
            "Centimeters",
            "Meters",
            "Kilometers",
            "DecimalDegrees",
            "Decimeters"});
            this.cboSATrimPolygonDistanceUnits.Location = new System.Drawing.Point(241, 176);
            this.cboSATrimPolygonDistanceUnits.Name = "cboSATrimPolygonDistanceUnits";
            this.cboSATrimPolygonDistanceUnits.Size = new System.Drawing.Size(110, 21);
            this.cboSATrimPolygonDistanceUnits.TabIndex = 120;
            // 
            // txtSATrimPolygonDistance
            // 
            this.txtSATrimPolygonDistance.Location = new System.Drawing.Point(169, 177);
            this.txtSATrimPolygonDistance.Name = "txtSATrimPolygonDistance";
            this.txtSATrimPolygonDistance.Size = new System.Drawing.Size(66, 20);
            this.txtSATrimPolygonDistance.TabIndex = 119;
            // 
            // chkSATrimOuterPolygon
            // 
            this.chkSATrimOuterPolygon.Location = new System.Drawing.Point(41, 177);
            this.chkSATrimOuterPolygon.Name = "chkSATrimOuterPolygon";
            this.chkSATrimOuterPolygon.Size = new System.Drawing.Size(122, 22);
            this.chkSATrimOuterPolygon.TabIndex = 118;
            this.chkSATrimOuterPolygon.Text = "Trim Outer Polygon";
            // 
            // chkSAIncludeSourceInformationOnLines
            // 
            this.chkSAIncludeSourceInformationOnLines.Location = new System.Drawing.Point(329, 232);
            this.chkSAIncludeSourceInformationOnLines.Name = "chkSAIncludeSourceInformationOnLines";
            this.chkSAIncludeSourceInformationOnLines.Size = new System.Drawing.Size(215, 22);
            this.chkSAIncludeSourceInformationOnLines.TabIndex = 117;
            this.chkSAIncludeSourceInformationOnLines.Text = "Include Source Information On Lines";
            // 
            // cboSATravelDirection
            // 
            this.cboSATravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSATravelDirection.ItemHeight = 13;
            this.cboSATravelDirection.Items.AddRange(new object[] {
            "From Facility",
            "To Facility"});
            this.cboSATravelDirection.Location = new System.Drawing.Point(151, 98);
            this.cboSATravelDirection.Name = "cboSATravelDirection";
            this.cboSATravelDirection.Size = new System.Drawing.Size(200, 21);
            this.cboSATravelDirection.TabIndex = 115;
            // 
            // lblSATravelDirection
            // 
            this.lblSATravelDirection.Location = new System.Drawing.Point(20, 103);
            this.lblSATravelDirection.Name = "lblSATravelDirection";
            this.lblSATravelDirection.Size = new System.Drawing.Size(114, 16);
            this.lblSATravelDirection.TabIndex = 116;
            this.lblSATravelDirection.Text = "Travel Direction";
            // 
            // chkSASplitPolygonsAtBreaks
            // 
            this.chkSASplitPolygonsAtBreaks.Location = new System.Drawing.Point(169, 153);
            this.chkSASplitPolygonsAtBreaks.Name = "chkSASplitPolygonsAtBreaks";
            this.chkSASplitPolygonsAtBreaks.Size = new System.Drawing.Size(154, 22);
            this.chkSASplitPolygonsAtBreaks.TabIndex = 114;
            this.chkSASplitPolygonsAtBreaks.Text = "Split Polygons At Breaks";
            // 
            // chkSAOverlapPolygons
            // 
            this.chkSAOverlapPolygons.Location = new System.Drawing.Point(41, 152);
            this.chkSAOverlapPolygons.Name = "chkSAOverlapPolygons";
            this.chkSAOverlapPolygons.Size = new System.Drawing.Size(122, 22);
            this.chkSAOverlapPolygons.TabIndex = 113;
            this.chkSAOverlapPolygons.Text = "Overlap Polygons";
            // 
            // chkSASplitLinesAtBreaks
            // 
            this.chkSASplitLinesAtBreaks.Location = new System.Drawing.Point(169, 232);
            this.chkSASplitLinesAtBreaks.Name = "chkSASplitLinesAtBreaks";
            this.chkSASplitLinesAtBreaks.Size = new System.Drawing.Size(154, 22);
            this.chkSASplitLinesAtBreaks.TabIndex = 112;
            this.chkSASplitLinesAtBreaks.Text = "Split Lines At Breaks";
            // 
            // chkSAOverlapLines
            // 
            this.chkSAOverlapLines.Location = new System.Drawing.Point(41, 232);
            this.chkSAOverlapLines.Name = "chkSAOverlapLines";
            this.chkSAOverlapLines.Size = new System.Drawing.Size(122, 22);
            this.chkSAOverlapLines.TabIndex = 111;
            this.chkSAOverlapLines.Text = "Overlap Lines";
            // 
            // chkSAIgnoreInvalidLocations
            // 
            this.chkSAIgnoreInvalidLocations.Location = new System.Drawing.Point(23, 286);
            this.chkSAIgnoreInvalidLocations.Name = "chkSAIgnoreInvalidLocations";
            this.chkSAIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 29);
            this.chkSAIgnoreInvalidLocations.TabIndex = 105;
            this.chkSAIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
            // 
            // cboSARestrictUTurns
            // 
            this.cboSARestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSARestrictUTurns.ItemHeight = 13;
            this.cboSARestrictUTurns.Items.AddRange(new object[] {
            "No U-Turns",
            "Allow U-Turns",
            "Only At Dead Ends"});
            this.cboSARestrictUTurns.Location = new System.Drawing.Point(151, 262);
            this.cboSARestrictUTurns.Name = "cboSARestrictUTurns";
            this.cboSARestrictUTurns.Size = new System.Drawing.Size(200, 21);
            this.cboSARestrictUTurns.TabIndex = 104;
            // 
            // lblSARestrictUTurns
            // 
            this.lblSARestrictUTurns.Location = new System.Drawing.Point(20, 267);
            this.lblSARestrictUTurns.Name = "lblSARestrictUTurns";
            this.lblSARestrictUTurns.Size = new System.Drawing.Size(88, 16);
            this.lblSARestrictUTurns.TabIndex = 110;
            this.lblSARestrictUTurns.Text = "UTurn Policy";
            // 
            // lblSAAccumulateAttributeNames
            // 
            this.lblSAAccumulateAttributeNames.Location = new System.Drawing.Point(238, 317);
            this.lblSAAccumulateAttributeNames.Name = "lblSAAccumulateAttributeNames";
            this.lblSAAccumulateAttributeNames.Size = new System.Drawing.Size(120, 16);
            this.lblSAAccumulateAttributeNames.TabIndex = 109;
            this.lblSAAccumulateAttributeNames.Text = "Accumulate Attributes";
            // 
            // chklstSAAccumulateAttributeNames
            // 
            this.chklstSAAccumulateAttributeNames.CheckOnClick = true;
            this.chklstSAAccumulateAttributeNames.Location = new System.Drawing.Point(238, 333);
            this.chklstSAAccumulateAttributeNames.Name = "chklstSAAccumulateAttributeNames";
            this.chklstSAAccumulateAttributeNames.ScrollAlwaysVisible = true;
            this.chklstSAAccumulateAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstSAAccumulateAttributeNames.TabIndex = 107;
            // 
            // lblSARestrictionAttributeNames
            // 
            this.lblSARestrictionAttributeNames.Location = new System.Drawing.Point(22, 317);
            this.lblSARestrictionAttributeNames.Name = "lblSARestrictionAttributeNames";
            this.lblSARestrictionAttributeNames.Size = new System.Drawing.Size(72, 16);
            this.lblSARestrictionAttributeNames.TabIndex = 108;
            this.lblSARestrictionAttributeNames.Text = "Restrictions";
            // 
            // chklstSARestrictionAttributeNames
            // 
            this.chklstSARestrictionAttributeNames.CheckOnClick = true;
            this.chklstSARestrictionAttributeNames.Location = new System.Drawing.Point(22, 333);
            this.chklstSARestrictionAttributeNames.Name = "chklstSARestrictionAttributeNames";
            this.chklstSARestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstSARestrictionAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstSARestrictionAttributeNames.TabIndex = 106;
            // 
            // lblSAOutputPolygons
            // 
            this.lblSAOutputPolygons.Location = new System.Drawing.Point(20, 130);
            this.lblSAOutputPolygons.Name = "lblSAOutputPolygons";
            this.lblSAOutputPolygons.Size = new System.Drawing.Size(122, 16);
            this.lblSAOutputPolygons.TabIndex = 103;
            this.lblSAOutputPolygons.Text = "Output Polygons";
            // 
            // cboSAOutputPolygons
            // 
            this.cboSAOutputPolygons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSAOutputPolygons.ItemHeight = 13;
            this.cboSAOutputPolygons.Items.AddRange(new object[] {
            "No Polygons",
            "Simplified Polygons",
            "Detailed Polygons"});
            this.cboSAOutputPolygons.Location = new System.Drawing.Point(151, 125);
            this.cboSAOutputPolygons.Name = "cboSAOutputPolygons";
            this.cboSAOutputPolygons.Size = new System.Drawing.Size(200, 21);
            this.cboSAOutputPolygons.TabIndex = 102;
            this.cboSAOutputPolygons.SelectedIndexChanged += new System.EventHandler(this.cboSAOutputPolygons_SelectedIndexChanged);
            // 
            // lblSAOutputLines
            // 
            this.lblSAOutputLines.Location = new System.Drawing.Point(20, 210);
            this.lblSAOutputLines.Name = "lblSAOutputLines";
            this.lblSAOutputLines.Size = new System.Drawing.Size(122, 16);
            this.lblSAOutputLines.TabIndex = 101;
            this.lblSAOutputLines.Text = "Output Lines";
            // 
            // cboSAOutputLines
            // 
            this.cboSAOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSAOutputLines.ItemHeight = 13;
            this.cboSAOutputLines.Items.AddRange(new object[] {
            "No Lines",
            "True Shape",
            "True Shape With Measures"});
            this.cboSAOutputLines.Location = new System.Drawing.Point(151, 205);
            this.cboSAOutputLines.Name = "cboSAOutputLines";
            this.cboSAOutputLines.Size = new System.Drawing.Size(200, 21);
            this.cboSAOutputLines.TabIndex = 100;
            this.cboSAOutputLines.SelectedIndexChanged += new System.EventHandler(this.cboSAOutputLines_SelectedIndexChanged);
            // 
            // chkSAMergeSimilarPolygonRanges
            // 
            this.chkSAMergeSimilarPolygonRanges.Location = new System.Drawing.Point(329, 152);
            this.chkSAMergeSimilarPolygonRanges.Name = "chkSAMergeSimilarPolygonRanges";
            this.chkSAMergeSimilarPolygonRanges.Size = new System.Drawing.Size(192, 22);
            this.chkSAMergeSimilarPolygonRanges.TabIndex = 99;
            this.chkSAMergeSimilarPolygonRanges.Text = "Merge Similar Polygon Ranges";
            // 
            // txtSADefaultBreaks
            // 
            this.txtSADefaultBreaks.Location = new System.Drawing.Point(151, 72);
            this.txtSADefaultBreaks.Name = "txtSADefaultBreaks";
            this.txtSADefaultBreaks.Size = new System.Drawing.Size(200, 20);
            this.txtSADefaultBreaks.TabIndex = 98;
            // 
            // lblSADefaultBreaks
            // 
            this.lblSADefaultBreaks.Location = new System.Drawing.Point(20, 76);
            this.lblSADefaultBreaks.Name = "lblSADefaultBreaks";
            this.lblSADefaultBreaks.Size = new System.Drawing.Size(114, 16);
            this.lblSADefaultBreaks.TabIndex = 97;
            this.lblSADefaultBreaks.Text = "Default Breaks";
            // 
            // cboSAImpedance
            // 
            this.cboSAImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSAImpedance.ItemHeight = 13;
            this.cboSAImpedance.Location = new System.Drawing.Point(151, 23);
            this.cboSAImpedance.Name = "cboSAImpedance";
            this.cboSAImpedance.Size = new System.Drawing.Size(200, 21);
            this.cboSAImpedance.TabIndex = 86;
            // 
            // lblSAImpedance
            // 
            this.lblSAImpedance.Location = new System.Drawing.Point(20, 28);
            this.lblSAImpedance.Name = "lblSAImpedance";
            this.lblSAImpedance.Size = new System.Drawing.Size(64, 16);
            this.lblSAImpedance.TabIndex = 87;
            this.lblSAImpedance.Text = "Impedance";
            // 
            // tabVRP
            // 
            this.tabVRP.Controls.Add(this.gbRestrictions);
            this.tabVRP.Controls.Add(this.gbSettings);
            this.tabVRP.Location = new System.Drawing.Point(4, 22);
            this.tabVRP.Name = "tabVRP";
            this.tabVRP.Size = new System.Drawing.Size(711, 473);
            this.tabVRP.TabIndex = 5;
            this.tabVRP.Text = "VRP";
            this.tabVRP.UseVisualStyleBackColor = true;
            // 
            // gbRestrictions
            // 
            this.gbRestrictions.Controls.Add(this.chklstVRPRestrictionAttributeNames);
            this.gbRestrictions.Location = new System.Drawing.Point(349, 3);
            this.gbRestrictions.Name = "gbRestrictions";
            this.gbRestrictions.Size = new System.Drawing.Size(206, 90);
            this.gbRestrictions.TabIndex = 1;
            this.gbRestrictions.TabStop = false;
            this.gbRestrictions.Text = "Restrictions";
            // 
            // chklstVRPRestrictionAttributeNames
            // 
            this.chklstVRPRestrictionAttributeNames.CheckOnClick = true;
            this.chklstVRPRestrictionAttributeNames.Location = new System.Drawing.Point(6, 14);
            this.chklstVRPRestrictionAttributeNames.Name = "chklstVRPRestrictionAttributeNames";
            this.chklstVRPRestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstVRPRestrictionAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstVRPRestrictionAttributeNames.TabIndex = 109;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.cboVRPDistanceFieldUnits);
            this.gbSettings.Controls.Add(this.cboVRPTransitTime);
            this.gbSettings.Controls.Add(this.cboVRPTimeWindow);
            this.gbSettings.Controls.Add(this.label10);
            this.gbSettings.Controls.Add(this.label9);
            this.gbSettings.Controls.Add(this.chkVRPUseHierarchy);
            this.gbSettings.Controls.Add(this.cboVRPOutputShapeType);
            this.gbSettings.Controls.Add(this.cboVRPAllowUTurns);
            this.gbSettings.Controls.Add(this.cboVRPTimeFieldUnits);
            this.gbSettings.Controls.Add(this.txtVRPCapacityCount);
            this.gbSettings.Controls.Add(this.txtVRPDefaultDate);
            this.gbSettings.Controls.Add(this.cboVRPDistanceAttribute);
            this.gbSettings.Controls.Add(this.cboVRPTimeAttribute);
            this.gbSettings.Controls.Add(this.label7);
            this.gbSettings.Controls.Add(this.label6);
            this.gbSettings.Controls.Add(this.label5);
            this.gbSettings.Controls.Add(this.label4);
            this.gbSettings.Controls.Add(this.label3);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.lblTimeAttribute);
            this.gbSettings.Location = new System.Drawing.Point(3, 3);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(340, 321);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // cboVRPDistanceFieldUnits
            // 
            this.cboVRPDistanceFieldUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPDistanceFieldUnits.ItemHeight = 13;
            this.cboVRPDistanceFieldUnits.Items.AddRange(new object[] {
            "Inches",
            "Points",
            "Feet",
            "Yards",
            "Miles",
            "Nautical Miles",
            "Millimeters",
            "Centimeters",
            "Meters",
            "Kilometers",
            "DecimalDegrees",
            "Decimeters"});
            this.cboVRPDistanceFieldUnits.Location = new System.Drawing.Point(189, 151);
            this.cboVRPDistanceFieldUnits.Name = "cboVRPDistanceFieldUnits";
            this.cboVRPDistanceFieldUnits.Size = new System.Drawing.Size(136, 21);
            this.cboVRPDistanceFieldUnits.TabIndex = 123;
            // 
            // cboVRPTransitTime
            // 
            this.cboVRPTransitTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPTransitTime.FormattingEnabled = true;
            this.cboVRPTransitTime.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.cboVRPTransitTime.Location = new System.Drawing.Point(189, 265);
            this.cboVRPTransitTime.Name = "cboVRPTransitTime";
            this.cboVRPTransitTime.Size = new System.Drawing.Size(136, 21);
            this.cboVRPTransitTime.TabIndex = 20;
            // 
            // cboVRPTimeWindow
            // 
            this.cboVRPTimeWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPTimeWindow.FormattingEnabled = true;
            this.cboVRPTimeWindow.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.cboVRPTimeWindow.Location = new System.Drawing.Point(189, 238);
            this.cboVRPTimeWindow.Name = "cboVRPTimeWindow";
            this.cboVRPTimeWindow.Size = new System.Drawing.Size(136, 21);
            this.cboVRPTimeWindow.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Excess Transit Time Importance:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Time Window Violation Importance:";
            // 
            // chkVRPUseHierarchy
            // 
            this.chkVRPUseHierarchy.AutoSize = true;
            this.chkVRPUseHierarchy.Location = new System.Drawing.Point(12, 294);
            this.chkVRPUseHierarchy.Name = "chkVRPUseHierarchy";
            this.chkVRPUseHierarchy.Size = new System.Drawing.Size(93, 17);
            this.chkVRPUseHierarchy.TabIndex = 16;
            this.chkVRPUseHierarchy.Text = "Use Hierarchy";
            this.chkVRPUseHierarchy.UseVisualStyleBackColor = true;
            // 
            // cboVRPOutputShapeType
            // 
            this.cboVRPOutputShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPOutputShapeType.FormattingEnabled = true;
            this.cboVRPOutputShapeType.Items.AddRange(new object[] {
            "None",
            "Straight Line",
            "True Shape",
            "True Shape with Measure"});
            this.cboVRPOutputShapeType.Location = new System.Drawing.Point(189, 208);
            this.cboVRPOutputShapeType.Name = "cboVRPOutputShapeType";
            this.cboVRPOutputShapeType.Size = new System.Drawing.Size(136, 21);
            this.cboVRPOutputShapeType.TabIndex = 15;
            // 
            // cboVRPAllowUTurns
            // 
            this.cboVRPAllowUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPAllowUTurns.FormattingEnabled = true;
            this.cboVRPAllowUTurns.Items.AddRange(new object[] {
            "No U-Turns",
            "Allow U-Turns",
            "Only At Dead Ends"});
            this.cboVRPAllowUTurns.Location = new System.Drawing.Point(189, 180);
            this.cboVRPAllowUTurns.Name = "cboVRPAllowUTurns";
            this.cboVRPAllowUTurns.Size = new System.Drawing.Size(136, 21);
            this.cboVRPAllowUTurns.TabIndex = 14;
            // 
            // cboVRPTimeFieldUnits
            // 
            this.cboVRPTimeFieldUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPTimeFieldUnits.FormattingEnabled = true;
            this.cboVRPTimeFieldUnits.Items.AddRange(new object[] {
            "Seconds",
            "Minutes",
            "Hours",
            "Days"});
            this.cboVRPTimeFieldUnits.Location = new System.Drawing.Point(189, 124);
            this.cboVRPTimeFieldUnits.Name = "cboVRPTimeFieldUnits";
            this.cboVRPTimeFieldUnits.Size = new System.Drawing.Size(136, 21);
            this.cboVRPTimeFieldUnits.TabIndex = 12;
            // 
            // txtVRPCapacityCount
            // 
            this.txtVRPCapacityCount.Location = new System.Drawing.Point(189, 97);
            this.txtVRPCapacityCount.Name = "txtVRPCapacityCount";
            this.txtVRPCapacityCount.Size = new System.Drawing.Size(136, 20);
            this.txtVRPCapacityCount.TabIndex = 11;
            // 
            // txtVRPDefaultDate
            // 
            this.txtVRPDefaultDate.Location = new System.Drawing.Point(189, 70);
            this.txtVRPDefaultDate.Name = "txtVRPDefaultDate";
            this.txtVRPDefaultDate.Size = new System.Drawing.Size(136, 20);
            this.txtVRPDefaultDate.TabIndex = 10;
            // 
            // cboVRPDistanceAttribute
            // 
            this.cboVRPDistanceAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPDistanceAttribute.FormattingEnabled = true;
            this.cboVRPDistanceAttribute.Items.AddRange(new object[] {
            "",
            "Meters (Meters)"});
            this.cboVRPDistanceAttribute.Location = new System.Drawing.Point(189, 42);
            this.cboVRPDistanceAttribute.Name = "cboVRPDistanceAttribute";
            this.cboVRPDistanceAttribute.Size = new System.Drawing.Size(136, 21);
            this.cboVRPDistanceAttribute.TabIndex = 9;
            // 
            // cboVRPTimeAttribute
            // 
            this.cboVRPTimeAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVRPTimeAttribute.FormattingEnabled = true;
            this.cboVRPTimeAttribute.Location = new System.Drawing.Point(189, 14);
            this.cboVRPTimeAttribute.Name = "cboVRPTimeAttribute";
            this.cboVRPTimeAttribute.Size = new System.Drawing.Size(136, 21);
            this.cboVRPTimeAttribute.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Distance Attribute:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Default Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Capacity Count:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Time Field Units:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Distance Field Units:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "U-Turn Policy:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Output Shape Type:";
            // 
            // lblTimeAttribute
            // 
            this.lblTimeAttribute.AutoSize = true;
            this.lblTimeAttribute.Location = new System.Drawing.Point(9, 22);
            this.lblTimeAttribute.Name = "lblTimeAttribute";
            this.lblTimeAttribute.Size = new System.Drawing.Size(75, 13);
            this.lblTimeAttribute.TabIndex = 0;
            this.lblTimeAttribute.Text = "Time Attribute:";
            // 
            // tabLocationAllocation
            // 
            this.tabLocationAllocation.Controls.Add(this.chkLAUseTime);
            this.tabLocationAllocation.Controls.Add(this.txtLAUseTime);
            this.tabLocationAllocation.Controls.Add(this.chkLAIgnoreInvalidLocations);
            this.tabLocationAllocation.Controls.Add(this.grpLASettings);
            this.tabLocationAllocation.Controls.Add(this.chkLAUseHierarchy);
            this.tabLocationAllocation.Controls.Add(this.lblLAAccumulateAttributeNames);
            this.tabLocationAllocation.Controls.Add(this.chklstLAAccumulateAttributeNames);
            this.tabLocationAllocation.Controls.Add(this.lblLARestrictionAttributeNames);
            this.tabLocationAllocation.Controls.Add(this.chklstLARestrictionAttributeNames);
            this.tabLocationAllocation.Controls.Add(this.cboLAOutputLines);
            this.tabLocationAllocation.Controls.Add(this.label11);
            this.tabLocationAllocation.Controls.Add(this.cboLATravelDirection);
            this.tabLocationAllocation.Controls.Add(this.label12);
            this.tabLocationAllocation.Controls.Add(this.lblCostAttribute);
            this.tabLocationAllocation.Controls.Add(this.cboLAImpedance);
            this.tabLocationAllocation.Location = new System.Drawing.Point(4, 22);
            this.tabLocationAllocation.Name = "tabLocationAllocation";
            this.tabLocationAllocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocationAllocation.Size = new System.Drawing.Size(711, 473);
            this.tabLocationAllocation.TabIndex = 6;
            this.tabLocationAllocation.Text = "Location-Allocation";
            this.tabLocationAllocation.UseVisualStyleBackColor = true;
            // 
            // chkLAUseTime
            // 
            this.chkLAUseTime.Location = new System.Drawing.Point(10, 51);
            this.chkLAUseTime.Name = "chkLAUseTime";
            this.chkLAUseTime.Size = new System.Drawing.Size(104, 16);
            this.chkLAUseTime.TabIndex = 133;
            this.chkLAUseTime.Text = "Use Time";
            // 
            // txtLAUseTime
            // 
            this.txtLAUseTime.Location = new System.Drawing.Point(141, 47);
            this.txtLAUseTime.Name = "txtLAUseTime";
            this.txtLAUseTime.Size = new System.Drawing.Size(179, 20);
            this.txtLAUseTime.TabIndex = 134;
            // 
            // chkLAIgnoreInvalidLocations
            // 
            this.chkLAIgnoreInvalidLocations.Location = new System.Drawing.Point(13, 149);
            this.chkLAIgnoreInvalidLocations.Name = "chkLAIgnoreInvalidLocations";
            this.chkLAIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 29);
            this.chkLAIgnoreInvalidLocations.TabIndex = 123;
            this.chkLAIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
            // 
            // grpLASettings
            // 
            this.grpLASettings.Controls.Add(this.lblTargetMarketShare);
            this.grpLASettings.Controls.Add(this.txtLATargetMarketShare);
            this.grpLASettings.Controls.Add(this.cboLAImpTransformation);
            this.grpLASettings.Controls.Add(this.lblImpParameter);
            this.grpLASettings.Controls.Add(this.txtLAImpParameter);
            this.grpLASettings.Controls.Add(this.lblImpTransformation);
            this.grpLASettings.Controls.Add(this.lblProblemType);
            this.grpLASettings.Controls.Add(this.cboLAProblemType);
            this.grpLASettings.Controls.Add(this.lblCutOff);
            this.grpLASettings.Controls.Add(this.txtLACutOff);
            this.grpLASettings.Controls.Add(this.lblNumFacilities);
            this.grpLASettings.Controls.Add(this.txtLAFacilitiesToLocate);
            this.grpLASettings.Location = new System.Drawing.Point(230, 129);
            this.grpLASettings.Name = "grpLASettings";
            this.grpLASettings.Size = new System.Drawing.Size(342, 241);
            this.grpLASettings.TabIndex = 122;
            this.grpLASettings.TabStop = false;
            this.grpLASettings.Text = "Advanced Settings";
            // 
            // lblTargetMarketShare
            // 
            this.lblTargetMarketShare.AccessibleDescription = "grpLA";
            this.lblTargetMarketShare.AutoSize = true;
            this.lblTargetMarketShare.Location = new System.Drawing.Point(20, 205);
            this.lblTargetMarketShare.Name = "lblTargetMarketShare";
            this.lblTargetMarketShare.Size = new System.Drawing.Size(122, 13);
            this.lblTargetMarketShare.TabIndex = 31;
            this.lblTargetMarketShare.Text = "Target Market Share (%)";
            // 
            // txtLATargetMarketShare
            // 
            this.txtLATargetMarketShare.AccessibleDescription = "grpLA";
            this.txtLATargetMarketShare.Location = new System.Drawing.Point(199, 201);
            this.txtLATargetMarketShare.Name = "txtLATargetMarketShare";
            this.txtLATargetMarketShare.Size = new System.Drawing.Size(129, 20);
            this.txtLATargetMarketShare.TabIndex = 30;
            this.txtLATargetMarketShare.Text = "10.0";
            // 
            // cboLAImpTransformation
            // 
            this.cboLAImpTransformation.AccessibleDescription = "grpLA";
            this.cboLAImpTransformation.FormattingEnabled = true;
            this.cboLAImpTransformation.Items.AddRange(new object[] {
            "Linear",
            "Power",
            "Exponential"});
            this.cboLAImpTransformation.Location = new System.Drawing.Point(202, 135);
            this.cboLAImpTransformation.Name = "cboLAImpTransformation";
            this.cboLAImpTransformation.Size = new System.Drawing.Size(128, 21);
            this.cboLAImpTransformation.TabIndex = 29;
            this.cboLAImpTransformation.Text = "Linear";
            // 
            // lblImpParameter
            // 
            this.lblImpParameter.AccessibleDescription = "grpLA";
            this.lblImpParameter.AutoSize = true;
            this.lblImpParameter.Location = new System.Drawing.Point(19, 171);
            this.lblImpParameter.Name = "lblImpParameter";
            this.lblImpParameter.Size = new System.Drawing.Size(111, 13);
            this.lblImpParameter.TabIndex = 28;
            this.lblImpParameter.Text = "Impedance Parameter";
            // 
            // txtLAImpParameter
            // 
            this.txtLAImpParameter.AccessibleDescription = "grpLA";
            this.txtLAImpParameter.Location = new System.Drawing.Point(200, 166);
            this.txtLAImpParameter.Name = "txtLAImpParameter";
            this.txtLAImpParameter.Size = new System.Drawing.Size(129, 20);
            this.txtLAImpParameter.TabIndex = 27;
            this.txtLAImpParameter.Text = "1.0";
            // 
            // lblImpTransformation
            // 
            this.lblImpTransformation.AccessibleDescription = "grpLA";
            this.lblImpTransformation.AutoSize = true;
            this.lblImpTransformation.Location = new System.Drawing.Point(19, 135);
            this.lblImpTransformation.Name = "lblImpTransformation";
            this.lblImpTransformation.Size = new System.Drawing.Size(133, 13);
            this.lblImpTransformation.TabIndex = 26;
            this.lblImpTransformation.Text = "Impedance Transformation";
            // 
            // lblProblemType
            // 
            this.lblProblemType.AccessibleDescription = "grpLA";
            this.lblProblemType.AutoSize = true;
            this.lblProblemType.Location = new System.Drawing.Point(19, 30);
            this.lblProblemType.Name = "lblProblemType";
            this.lblProblemType.Size = new System.Drawing.Size(72, 13);
            this.lblProblemType.TabIndex = 23;
            this.lblProblemType.Text = "Problem Type";
            // 
            // cboLAProblemType
            // 
            this.cboLAProblemType.AccessibleDescription = "grpLA";
            this.cboLAProblemType.FormattingEnabled = true;
            this.cboLAProblemType.Items.AddRange(new object[] {
            "Minimize Impedance",
            "Maximize Coverage",
            "Minimize Facilities",
            "Maximize Attendance",
            "Maximize Market Share",
            "Target Market Share"});
            this.cboLAProblemType.Location = new System.Drawing.Point(202, 24);
            this.cboLAProblemType.Name = "cboLAProblemType";
            this.cboLAProblemType.Size = new System.Drawing.Size(128, 21);
            this.cboLAProblemType.TabIndex = 22;
            this.cboLAProblemType.Text = "Minimize Impedance";
            this.cboLAProblemType.SelectedIndexChanged += new System.EventHandler(this.cboLAProblemType_SelectedIndexChanged);
            // 
            // lblCutOff
            // 
            this.lblCutOff.AccessibleDescription = "grpLA";
            this.lblCutOff.AutoSize = true;
            this.lblCutOff.Location = new System.Drawing.Point(20, 98);
            this.lblCutOff.Name = "lblCutOff";
            this.lblCutOff.Size = new System.Drawing.Size(91, 13);
            this.lblCutOff.TabIndex = 21;
            this.lblCutOff.Text = "Impedance Cutoff";
            // 
            // txtLACutOff
            // 
            this.txtLACutOff.AccessibleDescription = "grpLA";
            this.txtLACutOff.Location = new System.Drawing.Point(202, 98);
            this.txtLACutOff.Name = "txtLACutOff";
            this.txtLACutOff.Size = new System.Drawing.Size(129, 20);
            this.txtLACutOff.TabIndex = 20;
            this.txtLACutOff.Text = "<None>";
            // 
            // lblNumFacilities
            // 
            this.lblNumFacilities.AccessibleDescription = "grpLA";
            this.lblNumFacilities.AutoSize = true;
            this.lblNumFacilities.Location = new System.Drawing.Point(20, 63);
            this.lblNumFacilities.Name = "lblNumFacilities";
            this.lblNumFacilities.Size = new System.Drawing.Size(102, 13);
            this.lblNumFacilities.TabIndex = 19;
            this.lblNumFacilities.Text = "Facilities To Choose";
            // 
            // txtLAFacilitiesToLocate
            // 
            this.txtLAFacilitiesToLocate.AccessibleDescription = "grpLA";
            this.txtLAFacilitiesToLocate.Location = new System.Drawing.Point(202, 63);
            this.txtLAFacilitiesToLocate.Name = "txtLAFacilitiesToLocate";
            this.txtLAFacilitiesToLocate.Size = new System.Drawing.Size(130, 20);
            this.txtLAFacilitiesToLocate.TabIndex = 18;
            this.txtLAFacilitiesToLocate.Text = "1";
            // 
            // chkLAUseHierarchy
            // 
            this.chkLAUseHierarchy.AutoSize = true;
            this.chkLAUseHierarchy.Location = new System.Drawing.Point(13, 120);
            this.chkLAUseHierarchy.Name = "chkLAUseHierarchy";
            this.chkLAUseHierarchy.Size = new System.Drawing.Size(93, 17);
            this.chkLAUseHierarchy.TabIndex = 121;
            this.chkLAUseHierarchy.Text = "Use Hierarchy";
            this.chkLAUseHierarchy.UseVisualStyleBackColor = true;
            // 
            // lblLAAccumulateAttributeNames
            // 
            this.lblLAAccumulateAttributeNames.Location = new System.Drawing.Point(11, 280);
            this.lblLAAccumulateAttributeNames.Name = "lblLAAccumulateAttributeNames";
            this.lblLAAccumulateAttributeNames.Size = new System.Drawing.Size(120, 16);
            this.lblLAAccumulateAttributeNames.TabIndex = 120;
            this.lblLAAccumulateAttributeNames.Text = "Accumulate Attributes";
            // 
            // chklstLAAccumulateAttributeNames
            // 
            this.chklstLAAccumulateAttributeNames.CheckOnClick = true;
            this.chklstLAAccumulateAttributeNames.Location = new System.Drawing.Point(11, 296);
            this.chklstLAAccumulateAttributeNames.Name = "chklstLAAccumulateAttributeNames";
            this.chklstLAAccumulateAttributeNames.ScrollAlwaysVisible = true;
            this.chklstLAAccumulateAttributeNames.Size = new System.Drawing.Size(192, 34);
            this.chklstLAAccumulateAttributeNames.TabIndex = 119;
            // 
            // lblLARestrictionAttributeNames
            // 
            this.lblLARestrictionAttributeNames.Location = new System.Drawing.Point(11, 192);
            this.lblLARestrictionAttributeNames.Name = "lblLARestrictionAttributeNames";
            this.lblLARestrictionAttributeNames.Size = new System.Drawing.Size(71, 15);
            this.lblLARestrictionAttributeNames.TabIndex = 118;
            this.lblLARestrictionAttributeNames.Text = "Restrictions";
            // 
            // chklstLARestrictionAttributeNames
            // 
            this.chklstLARestrictionAttributeNames.CheckOnClick = true;
            this.chklstLARestrictionAttributeNames.Location = new System.Drawing.Point(11, 210);
            this.chklstLARestrictionAttributeNames.Name = "chklstLARestrictionAttributeNames";
            this.chklstLARestrictionAttributeNames.ScrollAlwaysVisible = true;
            this.chklstLARestrictionAttributeNames.Size = new System.Drawing.Size(191, 34);
            this.chklstLARestrictionAttributeNames.TabIndex = 117;
            // 
            // cboLAOutputLines
            // 
            this.cboLAOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLAOutputLines.ItemHeight = 13;
            this.cboLAOutputLines.Items.AddRange(new object[] {
            "Straight Lines",
            "None"});
            this.cboLAOutputLines.Location = new System.Drawing.Point(142, 95);
            this.cboLAOutputLines.Name = "cboLAOutputLines";
            this.cboLAOutputLines.Size = new System.Drawing.Size(178, 21);
            this.cboLAOutputLines.TabIndex = 115;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 15);
            this.label11.TabIndex = 116;
            this.label11.Text = "Shape";
            // 
            // cboLATravelDirection
            // 
            this.cboLATravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLATravelDirection.ItemHeight = 13;
            this.cboLATravelDirection.Items.AddRange(new object[] {
            "Facility To Demand",
            "Demand To Facility"});
            this.cboLATravelDirection.Location = new System.Drawing.Point(142, 70);
            this.cboLATravelDirection.Name = "cboLATravelDirection";
            this.cboLATravelDirection.Size = new System.Drawing.Size(178, 21);
            this.cboLATravelDirection.TabIndex = 113;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(11, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 16);
            this.label12.TabIndex = 114;
            this.label12.Text = "Travel Direction";
            // 
            // lblCostAttribute
            // 
            this.lblCostAttribute.AutoSize = true;
            this.lblCostAttribute.Location = new System.Drawing.Point(8, 25);
            this.lblCostAttribute.Name = "lblCostAttribute";
            this.lblCostAttribute.Size = new System.Drawing.Size(70, 13);
            this.lblCostAttribute.TabIndex = 25;
            this.lblCostAttribute.Text = "Cost Attribute";
            // 
            // cboLAImpedance
            // 
            this.cboLAImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLAImpedance.FormattingEnabled = true;
            this.cboLAImpedance.Location = new System.Drawing.Point(142, 25);
            this.cboLAImpedance.Name = "cboLAImpedance";
            this.cboLAImpedance.Size = new System.Drawing.Size(176, 21);
            this.cboLAImpedance.TabIndex = 24;
            // 
            // tabAttributeParameters
            // 
            this.tabAttributeParameters.Controls.Add(this.btnReset);
            this.tabAttributeParameters.Controls.Add(this.attributeParameterGrid);
            this.tabAttributeParameters.Controls.Add(this.label14);
            this.tabAttributeParameters.Location = new System.Drawing.Point(4, 22);
            this.tabAttributeParameters.Name = "tabAttributeParameters";
            this.tabAttributeParameters.Size = new System.Drawing.Size(711, 473);
            this.tabAttributeParameters.TabIndex = 7;
            this.tabAttributeParameters.Text = "Attribute Parameters";
            this.tabAttributeParameters.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(638, 445);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(70, 21);
            this.btnReset.TabIndex = 28;
            this.btnReset.Text = "&Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // attributeParameterGrid
            // 
            this.attributeParameterGrid.AllowUserToAddRows = false;
            this.attributeParameterGrid.AllowUserToDeleteRows = false;
            this.attributeParameterGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.attributeParameterGrid.BackgroundColor = System.Drawing.Color.White;
            this.attributeParameterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.attributeParameterGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcAttribute,
            this.dgvcParameter,
            this.dgvcValue});
            this.attributeParameterGrid.Location = new System.Drawing.Point(15, 38);
            this.attributeParameterGrid.Name = "attributeParameterGrid";
            this.attributeParameterGrid.RowHeadersVisible = false;
            this.attributeParameterGrid.Size = new System.Drawing.Size(693, 401);
            this.attributeParameterGrid.TabIndex = 27;
            // 
            // dgvcAttribute
            // 
            this.dgvcAttribute.HeaderText = "Attribute";
            this.dgvcAttribute.Name = "dgvcAttribute";
            this.dgvcAttribute.ReadOnly = true;
            // 
            // dgvcParameter
            // 
            this.dgvcParameter.HeaderText = "Parameter";
            this.dgvcParameter.Name = "dgvcParameter";
            this.dgvcParameter.ReadOnly = true;
            // 
            // dgvcValue
            // 
            this.dgvcValue.HeaderText = "Value";
            this.dgvcValue.Name = "dgvcValue";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(267, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Specify the parameter values for the network attributes.";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(578, 524);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 21);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(657, 524);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmNALayerProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(739, 563);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabPropPages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNALayerProperties";
            this.ShowInTaskbar = false;
            this.Text = "Properties";
            this.tabPropPages.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabRoute.ResumeLayout(false);
            this.tabRoute.PerformLayout();
            this.tabClosestFacility.ResumeLayout(false);
            this.tabClosestFacility.PerformLayout();
            this.tabODCostMatrix.ResumeLayout(false);
            this.tabODCostMatrix.PerformLayout();
            this.tabServiceArea.ResumeLayout(false);
            this.tabServiceArea.PerformLayout();
            this.tabVRP.ResumeLayout(false);
            this.gbRestrictions.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.tabLocationAllocation.ResumeLayout(false);
            this.tabLocationAllocation.PerformLayout();
            this.grpLASettings.ResumeLayout(false);
            this.grpLASettings.PerformLayout();
            this.tabAttributeParameters.ResumeLayout(false);
            this.tabAttributeParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attributeParameterGrid)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		bool m_okClicked;
        INALayer3 m_naLayer;

        enum VARTYPE
        {
            VT_EMPTY = 0, // uninitialized (null)
            VT_NULL = 1, // System.DBNull.Value
            VT_I2 = 2, // short
            VT_I4 = 3, // int
            VT_R4 = 4, // float
            VT_R8 = 5, // double
            VT_DATE = 7, // DateTime
            VT_BSTR = 8, // string
            VT_BOOL = 11, // boolean
            VT_UNKNOWN = 13, // COM object
            VT_ARRAY = 0x2000 // array bitmask
        }

        enum AttributeParameterGridColumnType
        {
            ATTRIBUTE_NAME = 0,
            PARAMETER_NAME = 1,
            PARAMETER_VALUE = 2
        }

        Dictionary<string, double> m_restrictionParameterValues = new Dictionary<string, double> 
        { 
            {"Prohibit", -1},
            {"Avoid: High",5}, {"Avoid: Medium",2}, {"Avoid: Low",1.3},
            {"Prefer: Low",0.8}, {"Prefer: Medium",0.5}, {"Prefer: High",0.2}
        };

		public frmNALayerProperties()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);

            m_naLayer = null;
		}

		/// <summary>
		/// Called by clients to show the properties window and respond to changes made when OK is clicked.
		/// </summary>
		public bool ShowModal(INALayer3 naLayer)
		{
			m_okClicked = false;

            m_naLayer = naLayer;

			// Setup the window based on the current NALayer settings
            PopulateControls(m_naLayer);
			tabPropPages.SelectedIndex = 1;
            this.Text = ((ILayer)m_naLayer).Name + " Properties";

			this.ShowDialog();
			return m_okClicked;
		}

		/// <summary>
		/// Set controls based on the current NALayer settings
		/// This function takes the current NALayer and determines what type of solver it's pointing to
		/// and populates the corresponding controls and hides the tabs for the other solvers.
		/// </summary>
		private void PopulateControls(INALayer naLayer)
		{
            var layer = naLayer as ILayer;
			INAContext naContext = naLayer.Context;
			INetworkDataset networkDataset = naContext.NetworkDataset;
            var naLocator = naContext.Locator as INALocator2;

			INASolver naSolver = naContext.Solver;
            var naSolverSettings = naSolver as INASolverSettings2;
            var naTimeAwareSolverSettings = naSolver as INATimeAwareSolverSettings;

            var routeSolver = naSolver as INARouteSolver2;
            var cfSolver = naSolver as INAClosestFacilitySolver;
            var odSolver = naSolver as INAODCostMatrixSolver;
            var saSolver = naSolver as INAServiceAreaSolver2;
            var vrpSolver = naSolver as INAVRPSolver;
            var laSolver = naSolver as INALocationAllocationSolver;

			// Populate general Layer controls
			txtLayerName.Text = layer.Name;
			txtMaxSearchTolerance.Text = naLocator.MaxSnapTolerance.ToString();
			cboMaxSearchToleranceUnits.SelectedIndex = Convert.ToInt32(naLocator.SnapToleranceUnits);

            PopulateAttributeParameterControl(networkDataset);

			// Populate controls for the particular solver

			if (routeSolver != null)  // ROUTE LAYER
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabVRP);
				tabPropPages.TabPages.Remove(tabLocationAllocation);

				// INARouteSolver2
				chkRouteFindBestSequence.Checked = routeSolver.FindBestSequence;
				chkRoutePreserveFirstStop.Checked = routeSolver.PreserveFirstStop;
				chkRoutePreserveLastStop.Checked = routeSolver.PreserveLastStop;
				chkRouteUseTimeWindows.Checked = routeSolver.UseTimeWindows;
				chkRouteUseStartTime.Checked = routeSolver.UseStartTime;
				txtRouteStartTime.Text = routeSolver.StartTime.ToShortTimeString();
				cboRouteOutputLines.SelectedIndex = System.Convert.ToInt32(routeSolver.OutputLines);

				// INASolverSettings
				PopulateImpedanceNameControl(cboRouteImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName);
				chkRouteUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0);
				chkRouteUseHierarchy.Checked = (chkRouteUseHierarchy.Enabled && naSolverSettings.UseHierarchy);
				chkRouteIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations;
				cboRouteRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns);
				PopulateAttributeControl(chklstRouteAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost);
				PopulateAttributeControl(chklstRouteRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);
			}
			else if (cfSolver != null)  // CLOSEST FACILITY LAYER
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabVRP);
				tabPropPages.TabPages.Remove(tabLocationAllocation);

				// INAClosestFacilitySolver
				txtCFDefaultCutoff.Text = GetStringFromObject(cfSolver.DefaultCutoff);
				txtCFDefaultTargetFacilityCount.Text = cfSolver.DefaultTargetFacilityCount.ToString();
				cboCFTravelDirection.SelectedIndex = Convert.ToInt32(cfSolver.TravelDirection);
				cboCFOutputLines.SelectedIndex = Convert.ToInt32(cfSolver.OutputLines);

				// INASolverSettings
				PopulateImpedanceNameControl(cboCFImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName);
				chkCFUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0);
				chkCFUseHierarchy.Checked = (chkCFUseHierarchy.Enabled && naSolverSettings.UseHierarchy);
				chkCFIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations;
				cboCFRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns);
				PopulateAttributeControl(chklstCFAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost);
				PopulateAttributeControl(chklstCFRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);

                // INATimeAwareSolverSettings
                cboCFTimeUsage.SelectedIndex = System.Convert.ToInt32(naTimeAwareSolverSettings.TimeOfDayUsage);
                txtCFUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString(@"HH:mm:ss MM/dd/yyyy");
            }
			else if (odSolver != null) // OD COST MATRIX LAYER
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabVRP);
				tabPropPages.TabPages.Remove(tabLocationAllocation);

				// INAODCostMatrixSolver
				txtODDefaultCutoff.Text = GetStringFromObject(odSolver.DefaultCutoff);
				txtODDefaultTargetDestinationCount.Text = GetStringFromObject(odSolver.DefaultTargetDestinationCount);
				cboODOutputLines.SelectedIndex = Convert.ToInt32(odSolver.OutputLines);

				// INASolverSettings
				PopulateImpedanceNameControl(cboODImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName);
				chkODUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0);
				chkODUseHierarchy.Checked = (chkODUseHierarchy.Enabled && naSolverSettings.UseHierarchy);
				chkODIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations;
				cboODRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns);
				PopulateAttributeControl(chklstODAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost);
				PopulateAttributeControl(chklstODRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);

                // INATimeAwareSolverSettings
                if (naTimeAwareSolverSettings.TimeOfDayUsage == esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime)
                    chkODUseTime.Checked = true;
                txtODUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString(@"HH:mm:ss MM/dd/yyyy");
            }
			else if (saSolver != null)  //SERVICE AREA SOLVER
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabVRP);
				tabPropPages.TabPages.Remove(tabLocationAllocation);

				// INAServiceAreaSolver2
				txtSADefaultBreaks.Text = "";
				for (int iBreak = 0; iBreak < saSolver.DefaultBreaks.Count; iBreak++)
					txtSADefaultBreaks.Text = txtSADefaultBreaks.Text + " " + saSolver.DefaultBreaks.get_Element(iBreak).ToString();
				cboSATravelDirection.SelectedIndex = Convert.ToInt32(saSolver.TravelDirection);

				cboSAOutputPolygons.SelectedIndex = -1;
				cboSAOutputPolygons.SelectedIndex = Convert.ToInt32(saSolver.OutputPolygons);
				chkSAOverlapPolygons.Checked = saSolver.OverlapPolygons;
				chkSASplitPolygonsAtBreaks.Checked = saSolver.SplitPolygonsAtBreaks;
				chkSAMergeSimilarPolygonRanges.Checked = saSolver.MergeSimilarPolygonRanges;
				chkSATrimOuterPolygon.Checked = saSolver.TrimOuterPolygon;
				txtSATrimPolygonDistance.Text = saSolver.TrimPolygonDistance.ToString();
				cboSATrimPolygonDistanceUnits.SelectedIndex = Convert.ToInt32(saSolver.TrimPolygonDistanceUnits);

				cboSAOutputLines.SelectedIndex = -1;
				cboSAOutputLines.SelectedIndex = Convert.ToInt32(saSolver.OutputLines);
				chkSAOverlapLines.Checked = saSolver.OverlapLines;
				chkSASplitLinesAtBreaks.Checked = saSolver.SplitLinesAtBreaks;
				chkSAIncludeSourceInformationOnLines.Checked = saSolver.IncludeSourceInformationOnLines;

				// INASolverSettings
				PopulateImpedanceNameControl(cboSAImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName);
				chkSAIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations;
				cboSARestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns);
				PopulateAttributeControl(chklstSAAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost);
				PopulateAttributeControl(chklstSARestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);

                // INATimeAwareSolverSettings
                if (naTimeAwareSolverSettings.TimeOfDayUsage == esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime)
                    chkSAUseTime.Checked = true;
                txtSAUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString(@"HH:mm:ss MM/dd/yyyy");
            }
			else if (vrpSolver != null) // VRP Solver
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabLocationAllocation);

				cboVRPOutputShapeType.SelectedIndex = Convert.ToInt32(vrpSolver.OutputLines);
				cboVRPAllowUTurns.SelectedIndex = Convert.ToInt32(naSolverSettings.RestrictUTurns);
				// VRP cannot have unknown units, so the index is offset by 1 from the solver field units
				cboVRPDistanceFieldUnits.SelectedIndex = Convert.ToInt32(vrpSolver.DistanceFieldUnits) - 1;
				cboVRPTransitTime.SelectedIndex = Convert.ToInt32(vrpSolver.ExcessTransitTimePenaltyFactor);
				cboVRPTimeWindow.SelectedIndex = Convert.ToInt32(vrpSolver.TimeWindowViolationPenaltyFactor);
				cboVRPTimeFieldUnits.SelectedIndex = Convert.ToInt32(vrpSolver.TimeFieldUnits - 20);

				txtVRPCapacityCount.Text = vrpSolver.CapacityCount.ToString();
				txtVRPDefaultDate.Text = vrpSolver.DefaultDate.ToShortDateString();

				chkVRPUseHierarchy.Checked = naSolverSettings.UseHierarchy;

				PopulateAttributeControl(chklstVRPRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);

				//populate the time attribute combo box
				cboVRPTimeAttribute.Items.Clear();

				for (int i = 0; i < networkDataset.AttributeCount; i++)
				{
					INetworkAttribute networkAttribute = networkDataset.get_Attribute(i);

					if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost &&
						networkAttribute.Units >= esriNetworkAttributeUnits.esriNAUSeconds)
						cboVRPTimeAttribute.Items.Add(networkAttribute.Name);
				}

				if (cboVRPTimeAttribute.Items.Count > 0)
					cboVRPTimeAttribute.Text = naSolverSettings.ImpedanceAttributeName;


				// for VRP, the AccumulateAttributeNames hold the length, and it can only hold one length.
				//  Loop through the network dataset attributes
				cboVRPDistanceAttribute.Items.Clear();
				cboVRPDistanceAttribute.SelectedIndex = cboVRPDistanceAttribute.Items.Add("");

				for (int i = 0; i < networkDataset.AttributeCount; i++)
				{
					INetworkAttribute networkAttribute = networkDataset.get_Attribute(i);
					if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost &&
						networkAttribute.Units < esriNetworkAttributeUnits.esriNAUSeconds)
					{
						string attributeName = networkAttribute.Name;

						int cboindex = cboVRPDistanceAttribute.Items.Add(networkAttribute.Name);

						// If the attribute is in the strArray, it should be the selected one
						for (int j = 0; j < naSolverSettings.AccumulateAttributeNames.Count; j++)
							if (naSolverSettings.AccumulateAttributeNames.get_Element(j) == attributeName)
								cboVRPDistanceAttribute.SelectedIndex = cboindex;
					}
				}
			}
			else if (laSolver != null)  // Location-Allocation LAYER
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabVRP);

				// INALocationAllocationSolver
				txtLACutOff.Text = GetStringFromObject(laSolver.DefaultCutoff);
				txtLAFacilitiesToLocate.Text = laSolver.NumberFacilitiesToLocate.ToString();
				txtLAImpParameter.Text = laSolver.TransformationParameter.ToString();
				txtLATargetMarketShare.Text = laSolver.TargetMarketSharePercentage.ToString();

				cboLAImpTransformation.SelectedIndex = Convert.ToInt32(laSolver.ImpedanceTransformation);
				cboLAProblemType.SelectedIndex = Convert.ToInt32(laSolver.ProblemType);
				cboLAOutputLines.SelectedIndex = Convert.ToInt32(laSolver.OutputLines);
				cboLATravelDirection.SelectedIndex = Convert.ToInt32(laSolver.TravelDirection);

				//// INASolverSettings
				PopulateImpedanceNameControl(cboLAImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName);
				PopulateAttributeControl(chklstLAAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost);
				PopulateAttributeControl(chklstLARestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction);
				chkLAUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0);
				chkLAUseHierarchy.Checked = (chkCFUseHierarchy.Enabled && naSolverSettings.UseHierarchy);
				chkLAIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations;

                // INATimeAwareSolverSettings
                if (naTimeAwareSolverSettings.TimeOfDayUsage == esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime)
                    chkLAUseTime.Checked = true;
                txtLAUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString(@"HH:mm:ss MM/dd/yyyy");
            }
			else  // Unknown type of layer
			{
				// Remove unnecessary tabs
				tabPropPages.TabPages.Remove(tabRoute);
				tabPropPages.TabPages.Remove(tabClosestFacility);
				tabPropPages.TabPages.Remove(tabODCostMatrix);
				tabPropPages.TabPages.Remove(tabServiceArea);
				tabPropPages.TabPages.Remove(tabVRP);
				tabPropPages.TabPages.Remove(tabLocationAllocation);
                tabPropPages.TabPages.Remove(tabAttributeParameters);
            }
		}

        /// <summary>
        /// Interrogate the layer's network dataset attributes to populate a list of attribute parameters
        /// </summary>
        private void PopulateAttributeParameterControl(INetworkDataset networkDataset)
        {
            var solverSettings = m_naLayer.Context.Solver as INASolverSettings2;

            // Track if there are attribute parameters, to decide if the attribute parameter tab should be displayed
            bool hasAttributeParameters = false;

            // Iterate over all of the network attributes to search for parameters
            for (int attrIndex = 0; attrIndex < networkDataset.AttributeCount; attrIndex++)
            {
                var networkAttribute = networkDataset.get_Attribute(attrIndex) as INetworkAttribute2;
                string attributeName = networkAttribute.Name;

                // Iterate over all of the parameters, to find their values
                for (int paramIndex = 0; paramIndex < networkAttribute.Parameters.Count; paramIndex++)
                {
                    hasAttributeParameters = true;

                    // Find the current attribute parameter value for this layer
                    var attributeParameter = networkAttribute.Parameters.get_Element(paramIndex) as INetworkAttributeParameter2;
                    object paramValue = solverSettings.get_AttributeParameterValue(attributeName, attributeParameter.Name);

                    int rowID = attributeParameterGrid.Rows.Add();
                    attributeParameterGrid[(int)AttributeParameterGridColumnType.ATTRIBUTE_NAME, rowID].Value = networkAttribute.Name;
                    attributeParameterGrid[(int)AttributeParameterGridColumnType.PARAMETER_NAME, rowID].Value = attributeParameter.Name;

                    UpdateAttributeParameterValueCell(rowID, paramValue, (VARTYPE)attributeParameter.VarType, attributeParameter.ParameterUsageType);
                }
            }

            // Don't display the attribute parameters tab, if there are no attribute parameters
            if (!hasAttributeParameters)
                tabPropPages.TabPages.Remove(tabAttributeParameters);
        }

        private void UpdateAttributeParameterValueCell(int rowID, object paramValue, VARTYPE paramVarType, esriNetworkAttributeParameterUsageType paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral)
        {
            string cellText = ConvertAttributeParameterValueToString(paramValue, paramVarType, paramUsageType);

            // Set up the combo box choices for restriction attribute parameters
            if (paramUsageType == esriNetworkAttributeParameterUsageType.esriNAPUTRestriction)
                attributeParameterGrid[(int)AttributeParameterGridColumnType.PARAMETER_VALUE, rowID] = CreateRestrictionParameterCell(paramValue, cellText, rowID);

            attributeParameterGrid[(int)AttributeParameterGridColumnType.PARAMETER_VALUE, rowID].Value = cellText;
        }

        private DataGridViewComboBoxCell CreateStandardRestrictionParameterCell(int rowID)
        {
            DataGridViewComboBoxCell cbcRestriction = new DataGridViewComboBoxCell();
            cbcRestriction.Items.AddRange(m_restrictionParameterValues.Keys.ToArray());

            cbcRestriction.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            return cbcRestriction;
        }

        private DataGridViewComboBoxCell CreateRestrictionParameterCell(object paramValue, string cellText, int rowID)
        {
            DataGridViewComboBoxCell comboBoxCell = CreateStandardRestrictionParameterCell(rowID);

            bool isCustomRestrictionParamValue = !m_restrictionParameterValues.ContainsValue((double)paramValue);
            if (isCustomRestrictionParamValue)
                comboBoxCell.Items.Add(cellText);
            
            return comboBoxCell;
        }

        private string ConvertAttributeParameterValueToString(object paramValue, VARTYPE paramVarType, esriNetworkAttributeParameterUsageType paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral)
        {
            string paramValueString = "";

            // Use bitwise arithmetic to determine if this parameter is an array.
            VARTYPE vtBase = (VARTYPE)((int)paramVarType & ~(int)VARTYPE.VT_ARRAY);
            bool isArrayType = (vtBase != paramVarType); 

            // Null and DBNull should be represented as an empty string
            if (!System.DBNull.Value.Equals(paramValue) && paramValue != null)
            {
                // For restriction attribute parameters, try to match the parameter double value with its associated
                //  text representation
                bool isStandardRestrictionParamValue = (paramUsageType == esriNetworkAttributeParameterUsageType.esriNAPUTRestriction &&
                                                        m_restrictionParameterValues.ContainsValue((double)paramValue));
                if (isStandardRestrictionParamValue)
                {
                    if (isStandardRestrictionParamValue)
                    {
                        // Assign celltext to a key name matching the paramValue
                        IEnumerable<string> matchingKeys = from KeyValuePair<string, double> pair in m_restrictionParameterValues
                                                           where (((double)paramValue).Equals(pair.Value))
                                                           select pair.Key;

                        paramValueString = matchingKeys.First();
                    }
                }

                // For attribute parameters that are array types, determine the type of array, 
                //   then convert the array to a string for display purposes.
                else if (isArrayType)
                {
                    switch (vtBase)
                    {
                        case VARTYPE.VT_I2:
                            paramValueString = ConvertGenericArrayToString((short[])paramValue);
                            break;
                        case VARTYPE.VT_I4:
                            paramValueString = ConvertGenericArrayToString((int[])paramValue);
                            break;
                        case VARTYPE.VT_R4:
                            paramValueString = ConvertGenericArrayToString((float[])paramValue);
                            break;
                        case VARTYPE.VT_R8:
                            paramValueString = ConvertGenericArrayToString((double[])paramValue);
                            break;
                        case VARTYPE.VT_DATE:
                            paramValueString = ConvertGenericArrayToString((DateTime[])paramValue);
                            break;
                        case VARTYPE.VT_BSTR:
                            paramValueString = ConvertGenericArrayToString((string[])paramValue);
                            break;
                        case VARTYPE.VT_BOOL:
                            paramValueString = ConvertGenericArrayToString((bool[])paramValue);
                            break;
                        default:
                            throw new Exception("Unexpected array base type");
                    }
                }
                else
                    paramValueString = paramValue.ToString();
            }

            return paramValueString;
        }

        /// <summary>
        /// Take generic arrays and convert them to a string
        /// </summary>
        private static string ConvertGenericArrayToString<T>(T[] values)
        {
            string[] sValues = System.Array.ConvertAll(values, p => p.ToString());
            return String.Join(",", sValues);
        }

        /// <summary>
        /// Take string values and convert them to generic arrays
        /// </summary>
        private static T[] ConvertStringToGenericArray<T>(string cellValue)
        {
            List<T> list = new List<T>();

            string[] values = cellValue.Split(',');
            foreach (string value in values)
                list.Add((T)Convert.ChangeType(value, typeof(T)));

            return list.ToArray();
        }

		/// <summary>
		/// Updates the NALayer based on the current controls.
		/// This will update the solver settings for the solver referenced by the NALayer.
		/// </summary>
		private void UpdateNALayer(INALayer naLayer)
		{
			ILayer layer = naLayer as ILayer;
			INAContext naContext = naLayer.Context;
			INetworkDataset networkDataset = naContext.NetworkDataset;
			var naLocator = naContext.Locator as INALocator2;

            INASolver naSolver = naContext.Solver;
			var naSolverSettings = naSolver as INASolverSettings2;
            var naTimeAwareSolverSettings = naSolver as INATimeAwareSolverSettings;

            var routeSolver = naSolver as INARouteSolver2;
			var cfSolver = naSolver as INAClosestFacilitySolver;
			var odSolver = naSolver as INAODCostMatrixSolver;
			var saSolver = naSolver as INAServiceAreaSolver2;
			var vrpSolver = naSolver as INAVRPSolver;
			var laSolver = naSolver as INALocationAllocationSolver;

			// Set Layer properties
			layer.Name = txtLayerName.Text;
			naLocator.MaxSnapTolerance = Convert.ToDouble(txtMaxSearchTolerance.Text);
			naLocator.SnapToleranceUnits = (esriUnits)cboMaxSearchToleranceUnits.SelectedIndex;

            SetAttributeParameters(networkDataset);

			// Set Solver properties
			if (routeSolver != null)  // ROUTE LAYER
			{
				// INARouteSolver
				routeSolver.FindBestSequence = chkRouteFindBestSequence.Checked;
				routeSolver.PreserveFirstStop = chkRoutePreserveFirstStop.Checked;
				routeSolver.PreserveLastStop = chkRoutePreserveLastStop.Checked;
				routeSolver.UseTimeWindows = chkRouteUseTimeWindows.Checked;
				routeSolver.UseStartTime = chkRouteUseStartTime.Checked;
                try 
                {
				    routeSolver.StartTime = System.Convert.ToDateTime(txtRouteStartTime.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
				routeSolver.OutputLines = (esriNAOutputLineType)cboRouteOutputLines.SelectedIndex;

				// INASolverSettings
				naSolverSettings.ImpedanceAttributeName = cboRouteImpedance.Text;
				naSolverSettings.UseHierarchy = chkRouteUseHierarchy.Checked;
				naSolverSettings.IgnoreInvalidLocations = chkRouteIgnoreInvalidLocations.Checked;
				naSolverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboRouteRestrictUTurns.SelectedIndex;
				naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstRouteAccumulateAttributeNames);
				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstRouteRestrictionAttributeNames);
			}

			else if (cfSolver != null)  // CLOSEST FACILITY LAYER
			{
				if (txtCFDefaultCutoff.Text.Length == 0)
					cfSolver.DefaultCutoff = null;
				else
					cfSolver.DefaultCutoff = Convert.ToDouble(txtCFDefaultCutoff.Text);

				if (txtCFDefaultTargetFacilityCount.Text.Length == 0)
					cfSolver.DefaultTargetFacilityCount = 1;
				else
					cfSolver.DefaultTargetFacilityCount = Convert.ToInt32(txtCFDefaultTargetFacilityCount.Text);

				cfSolver.TravelDirection = (esriNATravelDirection)cboCFTravelDirection.SelectedIndex;
				cfSolver.OutputLines = (esriNAOutputLineType)cboCFOutputLines.SelectedIndex;

				// INASolverSettings
				naSolverSettings.ImpedanceAttributeName = cboCFImpedance.Text;
				naSolverSettings.UseHierarchy = chkCFUseHierarchy.Checked;
				naSolverSettings.IgnoreInvalidLocations = chkCFIgnoreInvalidLocations.Checked;
				naSolverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboCFRestrictUTurns.SelectedIndex;
				naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstCFAccumulateAttributeNames);
				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstCFRestrictionAttributeNames);

                // INATimeAwareSolverSettings
                try
                {
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtCFUseTime.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
                naTimeAwareSolverSettings.TimeOfDayUsage = (esriNATimeOfDayUsage)cboCFTimeUsage.SelectedIndex;
			}

			else if (odSolver != null)  // OD COST MATRIX LAYER
			{
				if (txtODDefaultCutoff.Text.Length == 0)
					odSolver.DefaultCutoff = null;
				else
					odSolver.DefaultCutoff = Convert.ToDouble(txtODDefaultCutoff.Text);

				if (txtODDefaultTargetDestinationCount.Text.Length == 0)
					odSolver.DefaultTargetDestinationCount = null;
				else
					odSolver.DefaultTargetDestinationCount = Convert.ToInt32(txtODDefaultTargetDestinationCount.Text);

				odSolver.OutputLines = (esriNAOutputLineType)cboODOutputLines.SelectedIndex;

				// INASolverSettings
				naSolverSettings.ImpedanceAttributeName = cboODImpedance.Text;
				naSolverSettings.UseHierarchy = chkODUseHierarchy.Checked;
				naSolverSettings.IgnoreInvalidLocations = chkODIgnoreInvalidLocations.Checked;
				naSolverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboODRestrictUTurns.SelectedIndex;
				naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstODAccumulateAttributeNames);
				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstODRestrictionAttributeNames);

                // INATimeAwareSolverSettings
                try 
                {
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtODUseTime.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
                if (chkODUseTime.Checked)
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime;
            }

			else if (saSolver != null)  // SERVICE AREA SOLVER
			{
				IDoubleArray defaultBreaks = saSolver.DefaultBreaks;
				defaultBreaks.RemoveAll();
				string breaks = txtSADefaultBreaks.Text.Trim();
				breaks.Replace("  ", " ");
				string[] values = breaks.Split(' ');
				for (int iBreak = values.GetLowerBound(0); iBreak <= values.GetUpperBound(0); iBreak++)
					defaultBreaks.Add(System.Convert.ToDouble(values.GetValue(iBreak)));
				saSolver.DefaultBreaks = defaultBreaks;
				saSolver.TravelDirection = (esriNATravelDirection)cboSATravelDirection.SelectedIndex;

				saSolver.OutputPolygons = (esriNAOutputPolygonType)cboSAOutputPolygons.SelectedIndex;
				saSolver.OverlapPolygons = chkSAOverlapPolygons.Checked;
				saSolver.SplitPolygonsAtBreaks = chkSASplitPolygonsAtBreaks.Checked;
				saSolver.MergeSimilarPolygonRanges = chkSAMergeSimilarPolygonRanges.Checked;
				saSolver.TrimOuterPolygon = chkSATrimOuterPolygon.Checked;
				saSolver.TrimPolygonDistance = Convert.ToDouble(this.txtSATrimPolygonDistance.Text);
				saSolver.TrimPolygonDistanceUnits = (esriUnits)cboSATrimPolygonDistanceUnits.SelectedIndex;

				if (cboSAOutputLines.SelectedIndex == 0)
					saSolver.OutputLines = (esriNAOutputLineType)cboSAOutputLines.SelectedIndex;
				else // Does not support Straight lines, so not in combobox, up by one to account for this
					saSolver.OutputLines = (esriNAOutputLineType)(cboSAOutputLines.SelectedIndex + 1);

				saSolver.OverlapLines = chkSAOverlapLines.Checked;
				saSolver.SplitLinesAtBreaks = chkSASplitLinesAtBreaks.Checked;
				saSolver.IncludeSourceInformationOnLines = this.chkSAIncludeSourceInformationOnLines.Checked;

				// INASolverSettings
				naSolverSettings.ImpedanceAttributeName = cboSAImpedance.Text;
				naSolverSettings.IgnoreInvalidLocations = chkSAIgnoreInvalidLocations.Checked;
				naSolverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboSARestrictUTurns.SelectedIndex;
				naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstSAAccumulateAttributeNames);
				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstSARestrictionAttributeNames);

                // INATimeAwareSolverSettings
                try 
                {
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtSAUseTime.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
                if (chkSAUseTime.Checked)
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime;
            }
			else if (vrpSolver != null)
			{
				naSolverSettings.ImpedanceAttributeName = cboVRPTimeAttribute.Text;
				naSolverSettings.AccumulateAttributeNames.RemoveAll();
				IStringArray strArray = naSolverSettings.AccumulateAttributeNames;
				strArray.RemoveAll();
				strArray.Add(cboVRPDistanceAttribute.Text);
				naSolverSettings.AccumulateAttributeNames = strArray;

				vrpSolver.CapacityCount = Convert.ToInt32(txtVRPCapacityCount.Text);
                try 
                {
				    vrpSolver.DefaultDate = Convert.ToDateTime(txtVRPDefaultDate.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
				vrpSolver.TimeFieldUnits = ((esriNetworkAttributeUnits)cboVRPTimeFieldUnits.SelectedIndex) + 20;

				// there cannot be unknown units for a VRP, so the index is offset by 1
				vrpSolver.DistanceFieldUnits = (esriNetworkAttributeUnits)cboVRPDistanceFieldUnits.SelectedIndex + 1;
				naSolverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboVRPAllowUTurns.SelectedIndex;
				vrpSolver.OutputLines = (esriNAOutputLineType)cboVRPOutputShapeType.SelectedIndex;
				vrpSolver.TimeWindowViolationPenaltyFactor = cboVRPTimeWindow.SelectedIndex;
				vrpSolver.ExcessTransitTimePenaltyFactor = cboVRPTransitTime.SelectedIndex;

				naSolverSettings.UseHierarchy = chkVRPUseHierarchy.Checked;

				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstVRPRestrictionAttributeNames);
			}
			else if (laSolver != null)  // Location-Allocation LAYER
			{
				if (txtLACutOff.Text.Length == 0)
					laSolver.DefaultCutoff = null;
				else if (Convert.ToDouble(txtLACutOff.Text) == 0.0)
					laSolver.DefaultCutoff = null;
				else
					laSolver.DefaultCutoff = Convert.ToDouble(txtLACutOff.Text);

				if (txtLAFacilitiesToLocate.Text.Length == 0)
					laSolver.NumberFacilitiesToLocate = 1;
				else
					laSolver.NumberFacilitiesToLocate = Convert.ToInt32(txtLAFacilitiesToLocate.Text);

				laSolver.ProblemType = (esriNALocationAllocationProblemType)cboLAProblemType.SelectedIndex;
				laSolver.ImpedanceTransformation = (esriNAImpedanceTransformationType)cboLAImpTransformation.SelectedIndex;
				laSolver.TransformationParameter = Convert.ToDouble(txtLAImpParameter.Text);
				laSolver.TargetMarketSharePercentage = Convert.ToDouble(txtLATargetMarketShare.Text);
				laSolver.TravelDirection = (esriNATravelDirection)cboLATravelDirection.SelectedIndex;
				laSolver.OutputLines = (esriNAOutputLineType)cboLAOutputLines.SelectedIndex;

				//// INASolverSettings
				naSolverSettings.ImpedanceAttributeName = cboLAImpedance.Text;
				naSolverSettings.UseHierarchy = chkLAUseHierarchy.Checked;
				naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstLAAccumulateAttributeNames);
				naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstLARestrictionAttributeNames);
				naSolverSettings.IgnoreInvalidLocations = chkLAIgnoreInvalidLocations.Checked;

                // INATimeAwareSolverSettings
                try 
                {
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtLAUseTime.Text);
                }
                catch (Exception)
                {
                    throw new Exception(@"Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.");
                }
                if (chkLAUseTime.Checked)
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime;
            }
		}

        /// <summary>
        /// Populate the attribute parameter values based on the data grid rows.
        /// </summary>
        private void SetAttributeParameters(INetworkDataset networkDataset)
        {
            var solverSettings = m_naLayer.Context.Solver as INASolverSettings2;

            // The parameter values will be updated for every row in the data grid view
            for (int rowIndex = 0; rowIndex < attributeParameterGrid.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = attributeParameterGrid.Rows[rowIndex];

                // Use the first cell value to find the appropriate network attribute
                var netAttribute = networkDataset.get_AttributeByName(row.Cells[(int)AttributeParameterGridColumnType.ATTRIBUTE_NAME].Value.ToString()) as INetworkAttribute3;
                string attributeName = netAttribute.Name;

                for (int paramIndex = 0; paramIndex < netAttribute.Parameters.Count; paramIndex++)
                {
                    var parameter = netAttribute.Parameters.get_Element(paramIndex) as INetworkAttributeParameter2;
                    string paramName = parameter.Name;
                    try
                    {
                        // Get the base type for the parameter.  For example, if the type is a double array,
                        //  then the base type is double.
                        VARTYPE vt = (VARTYPE)parameter.VarType;
                        VARTYPE vtBase = (VARTYPE)((int)vt & ~(int)VARTYPE.VT_ARRAY);

                        // Determine if the parameter is an array
                        bool isArrayType = (vtBase != vt);

                        // Use the second cell value to find the appropriate parameter
                        if (parameter.Name == row.Cells[(int)AttributeParameterGridColumnType.PARAMETER_NAME].Value.ToString())
                        {
                            object paramValue = System.DBNull.Value;
                            
                            object cellValue = row.Cells[(int)AttributeParameterGridColumnType.PARAMETER_VALUE].Value;                            
                            if (!System.DBNull.Value.Equals(cellValue) && cellValue != null)
                              paramValue = ConvertStringToAttributeParameterValue(cellValue.ToString(), (VARTYPE)parameter.VarType, parameter.ParameterUsageType);
                            
                            solverSettings.set_AttributeParameterValue(attributeName, paramName, paramValue);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Invalid attribute parameter value.\nAttribute: " + attributeName + "\nParameter: " + paramName + "\nError Message: " + e.Message);
                    }
                }
            }
        }

        private object ConvertStringToAttributeParameterValue(string paramValueText, VARTYPE paramVarType, esriNetworkAttributeParameterUsageType paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral)
        {
            object paramValue = System.DBNull.Value; // Regardless of the VarType, the parameter value can be DBNull

            // Use bitwise arithmetic to determine if this parameter is an array.
            VARTYPE vtBase = (VARTYPE)((int)paramVarType & ~(int)VARTYPE.VT_ARRAY);
            bool isArrayType = (vtBase != paramVarType); 

            if (paramValueText != "")
            {
                // Restriction parameters are specially handled, due to the conversion between displayed text values
                //  and stored double values
                bool isRestrictionParm = (paramUsageType == esriNetworkAttributeParameterUsageType.esriNAPUTRestriction);

                // For restriction parameters that have text values in the list, use the associated double values
                if (isRestrictionParm && m_restrictionParameterValues.ContainsKey(paramValueText))
                    paramValue = m_restrictionParameterValues[paramValueText];

                // For attribute parameters that are array types, determine the type of array, 
                //   then convert the string to the appropriate array type.
                else if (isArrayType)
                {
                    switch (vtBase)
                    {
                        case VARTYPE.VT_I2:
                            paramValue = ConvertStringToGenericArray<short>(paramValueText);
                            break;
                        case VARTYPE.VT_I4:
                            paramValue = ConvertStringToGenericArray<int>(paramValueText);
                            break;
                        case VARTYPE.VT_R4:
                            paramValue = ConvertStringToGenericArray<float>(paramValueText);
                            break;
                        case VARTYPE.VT_R8:
                            paramValue = ConvertStringToGenericArray<double>(paramValueText);
                            break;
                        case VARTYPE.VT_DATE:
                            paramValue = ConvertStringToGenericArray<DateTime>(paramValueText);
                            break;
                        case VARTYPE.VT_BSTR:
                            paramValue = ConvertStringToGenericArray<string>(paramValueText);
                            break;
                        case VARTYPE.VT_BOOL:
                            paramValue = ConvertStringToGenericArray<bool>(paramValueText);
                            break;
                        default:
                            throw new Exception("Unexpected array base type");
                    }
                }
                else // Simple type
                {
                    // Conversion for simple types is handled automatically, if the string can be converted to the VARTYPE
                    paramValue = paramValueText;
                }
            }
            return paramValue;
        }

		/// <summary>
		/// Update the Impedance control based on the network dataset cost attributes
		/// </summary>
		private void PopulateImpedanceNameControl(ComboBox cboImpedance, INetworkDataset networkDataset, string impedanceName)
		{
			cboImpedance.Items.Clear();

			for (int i = 0; i < networkDataset.AttributeCount; i++)
			{
				INetworkAttribute networkAttribute = networkDataset.get_Attribute(i);
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
					cboImpedance.Items.Add(networkAttribute.Name);
			}

			if (cboImpedance.Items.Count > 0)
				cboImpedance.Text = impedanceName;
		}

		/// <summary>
		/// Update the CheckedListBox control based on the network dataset attributes (checking the ones currently chosen by the solver)
		/// </summary>
		private void PopulateAttributeControl(CheckedListBox chklstBox, INetworkDataset networkDataset, IStringArray strArray, esriNetworkAttributeUsageType usageType)
		{
			chklstBox.Items.Clear();

			//  Loop through the network dataset attributes
			for (int i = 0; i < networkDataset.AttributeCount; i++)
			{
				var networkAttribute = networkDataset.get_Attribute(i) as INetworkAttribute2;
				if (networkAttribute.UsageType == usageType)
				{
					string attributeName = networkAttribute.Name;
					CheckState checkState = CheckState.Unchecked;

					// If the attribute is in the strArray, it should be checked
					for (int j = 0; j < strArray.Count; j++)
						if (strArray.get_Element(j) == attributeName)
							checkState = CheckState.Checked;

					// Add the attribute to the control
					chklstBox.Items.Add(attributeName, checkState);

				}
			}
		}

		/// <summary>
		/// Returns the attribute names checked.
		/// </summary>
		private IStringArray GetCheckedAttributeNamesFromControl(CheckedListBox chklstBox)
		{
			IStringArray attributeNames = new StrArrayClass();

			for (int i = 0; i < chklstBox.CheckedItems.Count; i++)
				attributeNames.Add(chklstBox.Items[chklstBox.CheckedIndices[i]].ToString());

			return attributeNames;
		}

		/// <summary>
		/// Encapsulates returning an empty string if the object is NULL.
		/// </summary>
		private string GetStringFromObject(object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		private void chkRouteUseStartTime_CheckedChanged(object sender, EventArgs e)
		{
			txtRouteStartTime.Enabled = chkRouteUseStartTime.Checked;
		}

		private void chkRouteFindBestSequence_CheckedChanged(object sender, EventArgs e)
		{
			chkRoutePreserveFirstStop.Enabled = chkRouteFindBestSequence.Checked;
			chkRoutePreserveLastStop.Enabled = chkRouteFindBestSequence.Checked;
		}

		// Enable/Disable SA Polygon controls if not generating polygons
		private void cboSAOutputPolygons_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool bOutputPolygons = (cboSAOutputPolygons.SelectedIndex > 0);
			chkSAOverlapPolygons.Enabled = bOutputPolygons;
			chkSASplitPolygonsAtBreaks.Enabled = bOutputPolygons;
			chkSAMergeSimilarPolygonRanges.Enabled = bOutputPolygons;
			chkSATrimOuterPolygon.Enabled = bOutputPolygons;
			txtSATrimPolygonDistance.Enabled = bOutputPolygons;
			cboSATrimPolygonDistanceUnits.Enabled = bOutputPolygons;
		}

		// Enable/Disable SA Line controls if not generating lines
		private void cboSAOutputLines_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool bOutputLines = (cboSAOutputLines.SelectedIndex > 0);
			chkSAOverlapLines.Enabled = bOutputLines;
			chkSASplitLinesAtBreaks.Enabled = bOutputLines;
			chkSAIncludeSourceInformationOnLines.Enabled = bOutputLines;
		}

		private void cboLAProblemType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((cboLAProblemType.SelectedIndex == 5) || (cboLAProblemType.SelectedIndex == 2))
				txtLAFacilitiesToLocate.Enabled = false;
			else
				txtLAFacilitiesToLocate.Enabled = true;

			if (cboLAProblemType.SelectedIndex == 5)
				txtLATargetMarketShare.Enabled = true;
			else
				txtLATargetMarketShare.Enabled = false;
		}

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            m_okClicked = true;

            try
            {
                // Get the NAContext and NetworkDataset
                INAContext naContext = m_naLayer.Context;
                INetworkDataset networkDataset = naContext.NetworkDataset;

                UpdateNALayer(m_naLayer);

                // Update the Context so it can respond to changes made to the solver settings
                IGPMessages gpMessages = new GPMessagesClass();
                IDENetworkDataset deNetworkDataset = ((IDatasetComponent)networkDataset).DataElement as IDENetworkDataset;
                naContext.Solver.UpdateContext(naContext, deNetworkDataset, gpMessages);

                // Only close the form if the update happens successfully
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update the layer. " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            m_okClicked = false;
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            INetworkDataset networkDataset = m_naLayer.Context.NetworkDataset;
            var solverSettings = m_naLayer.Context.Solver as INASolverSettings2;

            // The parameter values will be reset for every row in the data grid view
            for (int rowID = 0; rowID < attributeParameterGrid.Rows.Count; rowID++)
            {
                DataGridViewRow row = attributeParameterGrid.Rows[rowID];

                // Use the first cell value to find the appropriate network attribute
                var netAttribute = networkDataset.get_AttributeByName(row.Cells[(int)AttributeParameterGridColumnType.ATTRIBUTE_NAME].Value.ToString()) as INetworkAttribute3;
                IArray attributeParameters = netAttribute.Parameters;
                string attributeName = netAttribute.Name;

                // Check every parameter to find the matching one
                for (int paramIndex = 0; paramIndex < netAttribute.Parameters.Count; paramIndex++)
                {
                    var attributeParameter = attributeParameters.get_Element(paramIndex) as INetworkAttributeParameter2;
                    if (attributeParameter.Name == row.Cells[(int)AttributeParameterGridColumnType.PARAMETER_NAME].Value.ToString())
                    {
                        solverSettings.set_AttributeParameterValue(attributeName, attributeParameter.Name, attributeParameter.DefaultValue);
                        UpdateAttributeParameterValueCell(rowID, attributeParameter.DefaultValue, (VARTYPE)attributeParameter.VarType, attributeParameter.ParameterUsageType);
                    }
                }
            }
        }
	}
}
