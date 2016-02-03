using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections.Specialized;
using Route_WebService.WebService;

namespace Route_WebService
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Route_WebServiceClass : System.Windows.Forms.Form
	{
		#region Window Controls Declaration

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.GroupBox fraINASolverSettings;
		private System.Windows.Forms.ComboBox cboUturnPolicy;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckedListBox chklstAccumulateAttributes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckedListBox chklstRestrictions;
		private System.Windows.Forms.ComboBox cboImpedance;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkIgnoreInvalidLocations;
		private System.Windows.Forms.ComboBox cboNALayers;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkReturnMap;
		private System.Windows.Forms.Button cmdSolve;
		private System.Windows.Forms.ComboBox cboSnapToleranceUnits;
		private System.Windows.Forms.TextBox txtMaxSnapTolerance;
		private System.Windows.Forms.TextBox txtSnapTolerance;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.CheckBox chkPreserveFirst;
		private System.Windows.Forms.CheckBox chkBestOrder;
		private System.Windows.Forms.ComboBox cboRouteDirectionsLengthUnits;
		private System.Windows.Forms.CheckBox chkReturnRoutes;
		private System.Windows.Forms.CheckBox chkReturnRouteGeometries;
		private System.Windows.Forms.CheckBox chkReturnStops;
		private System.Windows.Forms.CheckBox chkReturnDirections;
		private System.Windows.Forms.ComboBox cboRouteOutputLines;
		private System.Windows.Forms.CheckBox chkUseTimeWindows;
		private System.Windows.Forms.CheckBox chkPreserveLast;
		private System.Windows.Forms.ComboBox cboRouteDirectionsTimeAttribute;
		private System.Windows.Forms.GroupBox fraINARouteSolver;
		private System.Windows.Forms.GroupBox fraINAServerRouteParams;
		private System.Windows.Forms.TabControl tabCtrlOutput;
		private System.Windows.Forms.TabPage tabReturnStopsFeatures;
		private System.Windows.Forms.TabPage tabReturnBarrierFeatures;
		private System.Windows.Forms.TabPage tabReturnDirections;
		private System.Windows.Forms.TabPage tabReturnRouteFeatures;
		private System.Windows.Forms.DataGrid dataGridRouteFeatures;
		private System.Windows.Forms.DataGrid dataGridStopFeatures;
		private System.Windows.Forms.DataGrid dataGridBarrierFeatures;
		private System.Windows.Forms.TabPage tabReturnMap;
		private System.Windows.Forms.TabPage tabReturnRouteGeometry;
		private System.Windows.Forms.TreeView treeViewRouteGeometry;
		private System.Windows.Forms.TreeView treeViewDirections;
		private System.Windows.Forms.CheckBox checkReturnBarriers;
		private System.Windows.Forms.CheckBox chkUseHierarchy;
		private System.Windows.Forms.CheckBox chkUseStartTime;
		private System.Windows.Forms.TextBox txtStartTime;
		private System.Windows.Forms.GroupBox fraINAServerSolverParams;

		private SanFrancisco_NAServer m_naServer;

		#endregion

		public Route_WebServiceClass()
		{
			//
			// Required for Windows Form Designer support
			//
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
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdSolve = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.fraINASolverSettings = new System.Windows.Forms.GroupBox();
			this.chkIgnoreInvalidLocations = new System.Windows.Forms.CheckBox();
			this.cboUturnPolicy = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.chklstAccumulateAttributes = new System.Windows.Forms.CheckedListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chklstRestrictions = new System.Windows.Forms.CheckedListBox();
			this.cboImpedance = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkUseHierarchy = new System.Windows.Forms.CheckBox();
			this.fraINAServerSolverParams = new System.Windows.Forms.GroupBox();
			this.chkReturnMap = new System.Windows.Forms.CheckBox();
			this.cboSnapToleranceUnits = new System.Windows.Forms.ComboBox();
			this.cboNALayers = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtMaxSnapTolerance = new System.Windows.Forms.TextBox();
			this.txtSnapTolerance = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.fraINARouteSolver = new System.Windows.Forms.GroupBox();
			this.label20 = new System.Windows.Forms.Label();
			this.cboRouteOutputLines = new System.Windows.Forms.ComboBox();
			this.chkUseTimeWindows = new System.Windows.Forms.CheckBox();
			this.chkPreserveLast = new System.Windows.Forms.CheckBox();
			this.chkPreserveFirst = new System.Windows.Forms.CheckBox();
			this.chkBestOrder = new System.Windows.Forms.CheckBox();
			this.chkUseStartTime = new System.Windows.Forms.CheckBox();
			this.txtStartTime = new System.Windows.Forms.TextBox();
			this.fraINAServerRouteParams = new System.Windows.Forms.GroupBox();
			this.checkReturnBarriers = new System.Windows.Forms.CheckBox();
			this.cboRouteDirectionsTimeAttribute = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			this.cboRouteDirectionsLengthUnits = new System.Windows.Forms.ComboBox();
			this.label22 = new System.Windows.Forms.Label();
			this.chkReturnRoutes = new System.Windows.Forms.CheckBox();
			this.chkReturnRouteGeometries = new System.Windows.Forms.CheckBox();
			this.chkReturnStops = new System.Windows.Forms.CheckBox();
			this.chkReturnDirections = new System.Windows.Forms.CheckBox();
			this.tabCtrlOutput = new System.Windows.Forms.TabControl();
			this.tabReturnMap = new System.Windows.Forms.TabPage();
			this.tabReturnRouteFeatures = new System.Windows.Forms.TabPage();
			this.dataGridRouteFeatures = new System.Windows.Forms.DataGrid();
			this.tabReturnBarrierFeatures = new System.Windows.Forms.TabPage();
			this.dataGridBarrierFeatures = new System.Windows.Forms.DataGrid();
			this.tabReturnStopsFeatures = new System.Windows.Forms.TabPage();
			this.dataGridStopFeatures = new System.Windows.Forms.DataGrid();
			this.tabReturnDirections = new System.Windows.Forms.TabPage();
			this.treeViewDirections = new System.Windows.Forms.TreeView();
			this.tabReturnRouteGeometry = new System.Windows.Forms.TabPage();
			this.treeViewRouteGeometry = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.fraINASolverSettings.SuspendLayout();
			this.fraINAServerSolverParams.SuspendLayout();
			this.fraINARouteSolver.SuspendLayout();
			this.fraINAServerRouteParams.SuspendLayout();
			this.tabCtrlOutput.SuspendLayout();
			this.tabReturnMap.SuspendLayout();
			this.tabReturnRouteFeatures.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridRouteFeatures)).BeginInit();
			this.tabReturnBarrierFeatures.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridBarrierFeatures)).BeginInit();
			this.tabReturnStopsFeatures.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridStopFeatures)).BeginInit();
			this.tabReturnDirections.SuspendLayout();
			this.tabReturnRouteGeometry.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdSolve
			// 
			this.cmdSolve.Location = new System.Drawing.Point(232, 504);
			this.cmdSolve.Name = "cmdSolve";
			this.cmdSolve.Size = new System.Drawing.Size(200, 32);
			this.cmdSolve.TabIndex = 29;
			this.cmdSolve.Text = "Find Route";
			this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.White;
			this.pictureBox.Location = new System.Drawing.Point(8, 8);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(448, 456);
			this.pictureBox.TabIndex = 20;
			this.pictureBox.TabStop = false;
			// 
			// fraINASolverSettings
			// 
			this.fraINASolverSettings.Controls.Add(this.chkIgnoreInvalidLocations);
			this.fraINASolverSettings.Controls.Add(this.cboUturnPolicy);
			this.fraINASolverSettings.Controls.Add(this.label11);
			this.fraINASolverSettings.Controls.Add(this.label7);
			this.fraINASolverSettings.Controls.Add(this.chklstAccumulateAttributes);
			this.fraINASolverSettings.Controls.Add(this.label5);
			this.fraINASolverSettings.Controls.Add(this.chklstRestrictions);
			this.fraINASolverSettings.Controls.Add(this.cboImpedance);
			this.fraINASolverSettings.Controls.Add(this.label4);
			this.fraINASolverSettings.Controls.Add(this.chkUseHierarchy);
			this.fraINASolverSettings.Enabled = false;
			this.fraINASolverSettings.Location = new System.Drawing.Point(8, 112);
			this.fraINASolverSettings.Name = "fraINASolverSettings";
			this.fraINASolverSettings.Size = new System.Drawing.Size(424, 192);
			this.fraINASolverSettings.TabIndex = 70;
			this.fraINASolverSettings.TabStop = false;
			this.fraINASolverSettings.Text = "INASolverSettings";
			// 
			// chkIgnoreInvalidLocations
			// 
			this.chkIgnoreInvalidLocations.Location = new System.Drawing.Point(11, 80);
			this.chkIgnoreInvalidLocations.Name = "chkIgnoreInvalidLocations";
			this.chkIgnoreInvalidLocations.Size = new System.Drawing.Size(144, 21);
			this.chkIgnoreInvalidLocations.TabIndex = 10;
			this.chkIgnoreInvalidLocations.Text = "Ignore Invalid Locations";
			// 
			// cboUturnPolicy
			// 
			this.cboUturnPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUturnPolicy.Location = new System.Drawing.Point(128, 48);
			this.cboUturnPolicy.Name = "cboUturnPolicy";
			this.cboUturnPolicy.Size = new System.Drawing.Size(280, 21);
			this.cboUturnPolicy.TabIndex = 9;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(11, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(88, 16);
			this.label11.TabIndex = 73;
			this.label11.Text = "Allow U-Turns";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(216, 104);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 16);
			this.label7.TabIndex = 72;
			this.label7.Text = "Accumulate Attributes";
			// 
			// chklstAccumulateAttributes
			// 
			this.chklstAccumulateAttributes.CheckOnClick = true;
			this.chklstAccumulateAttributes.Location = new System.Drawing.Point(216, 120);
			this.chklstAccumulateAttributes.Name = "chklstAccumulateAttributes";
			this.chklstAccumulateAttributes.ScrollAlwaysVisible = true;
			this.chklstAccumulateAttributes.Size = new System.Drawing.Size(192, 64);
			this.chklstAccumulateAttributes.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 16);
			this.label5.TabIndex = 70;
			this.label5.Text = "Restrictions";
			// 
			// chklstRestrictions
			// 
			this.chklstRestrictions.CheckOnClick = true;
			this.chklstRestrictions.Location = new System.Drawing.Point(11, 120);
			this.chklstRestrictions.Name = "chklstRestrictions";
			this.chklstRestrictions.ScrollAlwaysVisible = true;
			this.chklstRestrictions.Size = new System.Drawing.Size(192, 64);
			this.chklstRestrictions.TabIndex = 12;
			// 
			// cboImpedance
			// 
			this.cboImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboImpedance.Location = new System.Drawing.Point(128, 24);
			this.cboImpedance.Name = "cboImpedance";
			this.cboImpedance.Size = new System.Drawing.Size(280, 21);
			this.cboImpedance.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 67;
			this.label4.Text = "Impedance";
			// 
			// chkUseHierarchy
			// 
			this.chkUseHierarchy.Checked = true;
			this.chkUseHierarchy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseHierarchy.Location = new System.Drawing.Point(216, 80);
			this.chkUseHierarchy.Name = "chkUseHierarchy";
			this.chkUseHierarchy.Size = new System.Drawing.Size(96, 21);
			this.chkUseHierarchy.TabIndex = 11;
			this.chkUseHierarchy.Text = "Use Hierarchy";
			// 
			// fraINAServerSolverParams
			// 
			this.fraINAServerSolverParams.Controls.Add(this.chkReturnMap);
			this.fraINAServerSolverParams.Controls.Add(this.cboSnapToleranceUnits);
			this.fraINAServerSolverParams.Controls.Add(this.cboNALayers);
			this.fraINAServerSolverParams.Controls.Add(this.label10);
			this.fraINAServerSolverParams.Controls.Add(this.label8);
			this.fraINAServerSolverParams.Controls.Add(this.txtMaxSnapTolerance);
			this.fraINAServerSolverParams.Controls.Add(this.txtSnapTolerance);
			this.fraINAServerSolverParams.Controls.Add(this.label9);
			this.fraINAServerSolverParams.Enabled = false;
			this.fraINAServerSolverParams.Location = new System.Drawing.Point(8, 8);
			this.fraINAServerSolverParams.Name = "fraINAServerSolverParams";
			this.fraINAServerSolverParams.Size = new System.Drawing.Size(424, 96);
			this.fraINAServerSolverParams.TabIndex = 71;
			this.fraINAServerSolverParams.TabStop = false;
			this.fraINAServerSolverParams.Text = "INAServerSolverParams";
			// 
			// chkReturnMap
			// 
			this.chkReturnMap.Checked = true;
			this.chkReturnMap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnMap.Location = new System.Drawing.Point(8, 72);
			this.chkReturnMap.Name = "chkReturnMap";
			this.chkReturnMap.Size = new System.Drawing.Size(96, 16);
			this.chkReturnMap.TabIndex = 7;
			this.chkReturnMap.Text = "Return Map";
			// 
			// cboSnapToleranceUnits
			// 
			this.cboSnapToleranceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSnapToleranceUnits.Location = new System.Drawing.Point(240, 48);
			this.cboSnapToleranceUnits.Name = "cboSnapToleranceUnits";
			this.cboSnapToleranceUnits.Size = new System.Drawing.Size(168, 21);
			this.cboSnapToleranceUnits.TabIndex = 6;
			// 
			// cboNALayers
			// 
			this.cboNALayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboNALayers.Location = new System.Drawing.Point(128, 24);
			this.cboNALayers.Name = "cboNALayers";
			this.cboNALayers.Size = new System.Drawing.Size(280, 21);
			this.cboNALayers.TabIndex = 3;
			this.cboNALayers.SelectedIndexChanged += new System.EventHandler(this.cboNALayers_SelectedIndexChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(168, 56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(16, 16);
			this.label10.TabIndex = 72;
			this.label10.Text = "to";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 16);
			this.label8.TabIndex = 71;
			this.label8.Text = "NALayer Name";
			// 
			// txtMaxSnapTolerance
			// 
			this.txtMaxSnapTolerance.Location = new System.Drawing.Point(192, 48);
			this.txtMaxSnapTolerance.Name = "txtMaxSnapTolerance";
			this.txtMaxSnapTolerance.Size = new System.Drawing.Size(40, 20);
			this.txtMaxSnapTolerance.TabIndex = 5;
			this.txtMaxSnapTolerance.Text = "50";
			// 
			// txtSnapTolerance
			// 
			this.txtSnapTolerance.Location = new System.Drawing.Point(128, 48);
			this.txtSnapTolerance.Name = "txtSnapTolerance";
			this.txtSnapTolerance.Size = new System.Drawing.Size(32, 20);
			this.txtSnapTolerance.TabIndex = 4;
			this.txtSnapTolerance.Text = "2";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104, 16);
			this.label9.TabIndex = 68;
			this.label9.Text = "Search Tolerance";
			// 
			// fraINARouteSolver
			// 
			this.fraINARouteSolver.Controls.Add(this.label20);
			this.fraINARouteSolver.Controls.Add(this.cboRouteOutputLines);
			this.fraINARouteSolver.Controls.Add(this.chkUseTimeWindows);
			this.fraINARouteSolver.Controls.Add(this.chkPreserveLast);
			this.fraINARouteSolver.Controls.Add(this.chkPreserveFirst);
			this.fraINARouteSolver.Controls.Add(this.chkBestOrder);
			this.fraINARouteSolver.Controls.Add(this.chkUseStartTime);
			this.fraINARouteSolver.Controls.Add(this.txtStartTime);
			this.fraINARouteSolver.Enabled = false;
			this.fraINARouteSolver.Location = new System.Drawing.Point(232, 312);
			this.fraINARouteSolver.Name = "fraINARouteSolver";
			this.fraINARouteSolver.Size = new System.Drawing.Size(200, 184);
			this.fraINARouteSolver.TabIndex = 76;
			this.fraINARouteSolver.TabStop = false;
			this.fraINARouteSolver.Text = "INARouteSolver";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 136);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(40, 16);
			this.label20.TabIndex = 53;
			this.label20.Text = "Shape";
			// 
			// cboRouteOutputLines
			// 
			this.cboRouteOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRouteOutputLines.ItemHeight = 13;
			this.cboRouteOutputLines.Location = new System.Drawing.Point(8, 152);
			this.cboRouteOutputLines.Name = "cboRouteOutputLines";
			this.cboRouteOutputLines.Size = new System.Drawing.Size(184, 21);
			this.cboRouteOutputLines.TabIndex = 28;
			// 
			// chkUseTimeWindows
			// 
			this.chkUseTimeWindows.Location = new System.Drawing.Point(8, 80);
			this.chkUseTimeWindows.Name = "chkUseTimeWindows";
			this.chkUseTimeWindows.Size = new System.Drawing.Size(128, 16);
			this.chkUseTimeWindows.TabIndex = 25;
			this.chkUseTimeWindows.Text = "Use Time Windows";
			// 
			// chkPreserveLast
			// 
			this.chkPreserveLast.Location = new System.Drawing.Point(24, 56);
			this.chkPreserveLast.Name = "chkPreserveLast";
			this.chkPreserveLast.Size = new System.Drawing.Size(96, 16);
			this.chkPreserveLast.TabIndex = 24;
			this.chkPreserveLast.Text = "PreserveLast";
			// 
			// chkPreserveFirst
			// 
			this.chkPreserveFirst.Location = new System.Drawing.Point(24, 40);
			this.chkPreserveFirst.Name = "chkPreserveFirst";
			this.chkPreserveFirst.Size = new System.Drawing.Size(104, 16);
			this.chkPreserveFirst.TabIndex = 23;
			this.chkPreserveFirst.Text = "Preserve First";
			// 
			// chkBestOrder
			// 
			this.chkBestOrder.Location = new System.Drawing.Point(8, 24);
			this.chkBestOrder.Name = "chkBestOrder";
			this.chkBestOrder.Size = new System.Drawing.Size(112, 16);
			this.chkBestOrder.TabIndex = 22;
			this.chkBestOrder.Text = "Find Best Order";
			this.chkBestOrder.CheckedChanged += new System.EventHandler(this.chkBestOrder_CheckedChanged);
			// 
			// chkUseStartTime
			// 
			this.chkUseStartTime.Location = new System.Drawing.Point(8, 96);
			this.chkUseStartTime.Name = "chkUseStartTime";
			this.chkUseStartTime.Size = new System.Drawing.Size(104, 16);
			this.chkUseStartTime.TabIndex = 26;
			this.chkUseStartTime.Text = "Use Start Time";
			this.chkUseStartTime.CheckedChanged += new System.EventHandler(this.chkUseStartTime_CheckedChanged);
			// 
			// txtStartTime
			// 
			this.txtStartTime.Enabled = false;
			this.txtStartTime.Location = new System.Drawing.Point(24, 112);
			this.txtStartTime.Name = "txtStartTime";
			this.txtStartTime.Size = new System.Drawing.Size(168, 20);
			this.txtStartTime.TabIndex = 27;
			// 
			// fraINAServerRouteParams
			// 
			this.fraINAServerRouteParams.Controls.Add(this.checkReturnBarriers);
			this.fraINAServerRouteParams.Controls.Add(this.cboRouteDirectionsTimeAttribute);
			this.fraINAServerRouteParams.Controls.Add(this.label21);
			this.fraINAServerRouteParams.Controls.Add(this.cboRouteDirectionsLengthUnits);
			this.fraINAServerRouteParams.Controls.Add(this.label22);
			this.fraINAServerRouteParams.Controls.Add(this.chkReturnRoutes);
			this.fraINAServerRouteParams.Controls.Add(this.chkReturnRouteGeometries);
			this.fraINAServerRouteParams.Controls.Add(this.chkReturnStops);
			this.fraINAServerRouteParams.Controls.Add(this.chkReturnDirections);
			this.fraINAServerRouteParams.Enabled = false;
			this.fraINAServerRouteParams.Location = new System.Drawing.Point(8, 312);
			this.fraINAServerRouteParams.Name = "fraINAServerRouteParams";
			this.fraINAServerRouteParams.Size = new System.Drawing.Size(216, 184);
			this.fraINAServerRouteParams.TabIndex = 75;
			this.fraINAServerRouteParams.TabStop = false;
			this.fraINAServerRouteParams.Text = "INAServerRouteParams";
			// 
			// checkReturnBarriers
			// 
			this.checkReturnBarriers.Checked = true;
			this.checkReturnBarriers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkReturnBarriers.Location = new System.Drawing.Point(8, 56);
			this.checkReturnBarriers.Name = "checkReturnBarriers";
			this.checkReturnBarriers.Size = new System.Drawing.Size(136, 16);
			this.checkReturnBarriers.TabIndex = 17;
			this.checkReturnBarriers.Text = "Returns Barriers";
			// 
			// cboRouteDirectionsTimeAttribute
			// 
			this.cboRouteDirectionsTimeAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRouteDirectionsTimeAttribute.ItemHeight = 13;
			this.cboRouteDirectionsTimeAttribute.Location = new System.Drawing.Point(56, 144);
			this.cboRouteDirectionsTimeAttribute.Name = "cboRouteDirectionsTimeAttribute";
			this.cboRouteDirectionsTimeAttribute.Size = new System.Drawing.Size(152, 21);
			this.cboRouteDirectionsTimeAttribute.TabIndex = 21;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 152);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(48, 16);
			this.label21.TabIndex = 52;
			this.label21.Text = "Dir Time";
			// 
			// cboRouteDirectionsLengthUnits
			// 
			this.cboRouteDirectionsLengthUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRouteDirectionsLengthUnits.ItemHeight = 13;
			this.cboRouteDirectionsLengthUnits.Location = new System.Drawing.Point(56, 120);
			this.cboRouteDirectionsLengthUnits.Name = "cboRouteDirectionsLengthUnits";
			this.cboRouteDirectionsLengthUnits.Size = new System.Drawing.Size(152, 21);
			this.cboRouteDirectionsLengthUnits.TabIndex = 20;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 128);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(56, 16);
			this.label22.TabIndex = 50;
			this.label22.Text = "Dir Units";
			// 
			// chkReturnRoutes
			// 
			this.chkReturnRoutes.Checked = true;
			this.chkReturnRoutes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnRoutes.Location = new System.Drawing.Point(8, 40);
			this.chkReturnRoutes.Name = "chkReturnRoutes";
			this.chkReturnRoutes.Size = new System.Drawing.Size(96, 16);
			this.chkReturnRoutes.TabIndex = 16;
			this.chkReturnRoutes.Text = "Return Routes";
			// 
			// chkReturnRouteGeometries
			// 
			this.chkReturnRouteGeometries.Checked = true;
			this.chkReturnRouteGeometries.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnRouteGeometries.Location = new System.Drawing.Point(8, 24);
			this.chkReturnRouteGeometries.Name = "chkReturnRouteGeometries";
			this.chkReturnRouteGeometries.Size = new System.Drawing.Size(152, 16);
			this.chkReturnRouteGeometries.TabIndex = 15;
			this.chkReturnRouteGeometries.Text = "Return Route Geometries";
			// 
			// chkReturnStops
			// 
			this.chkReturnStops.Checked = true;
			this.chkReturnStops.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnStops.Location = new System.Drawing.Point(8, 72);
			this.chkReturnStops.Name = "chkReturnStops";
			this.chkReturnStops.Size = new System.Drawing.Size(96, 16);
			this.chkReturnStops.TabIndex = 18;
			this.chkReturnStops.Text = "Return Stops";
			// 
			// chkReturnDirections
			// 
			this.chkReturnDirections.Checked = true;
			this.chkReturnDirections.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReturnDirections.Location = new System.Drawing.Point(8, 88);
			this.chkReturnDirections.Name = "chkReturnDirections";
			this.chkReturnDirections.Size = new System.Drawing.Size(160, 16);
			this.chkReturnDirections.TabIndex = 19;
			this.chkReturnDirections.Text = "Generate Directions";
			// 
			// tabCtrlOutput
			// 
			this.tabCtrlOutput.Controls.Add(this.tabReturnMap);
			this.tabCtrlOutput.Controls.Add(this.tabReturnRouteFeatures);
			this.tabCtrlOutput.Controls.Add(this.tabReturnBarrierFeatures);
			this.tabCtrlOutput.Controls.Add(this.tabReturnStopsFeatures);
			this.tabCtrlOutput.Controls.Add(this.tabReturnDirections);
			this.tabCtrlOutput.Controls.Add(this.tabReturnRouteGeometry);
			this.tabCtrlOutput.Enabled = false;
			this.tabCtrlOutput.Location = new System.Drawing.Point(440, 16);
			this.tabCtrlOutput.Name = "tabCtrlOutput";
			this.tabCtrlOutput.SelectedIndex = 0;
			this.tabCtrlOutput.Size = new System.Drawing.Size(472, 496);
			this.tabCtrlOutput.TabIndex = 30;
			// 
			// tabReturnMap
			// 
			this.tabReturnMap.Controls.Add(this.pictureBox);
			this.tabReturnMap.Location = new System.Drawing.Point(4, 22);
			this.tabReturnMap.Name = "tabReturnMap";
			this.tabReturnMap.Size = new System.Drawing.Size(464, 470);
			this.tabReturnMap.TabIndex = 0;
			this.tabReturnMap.Text = "Map";
			// 
			// tabReturnRouteFeatures
			// 
			this.tabReturnRouteFeatures.Controls.Add(this.dataGridRouteFeatures);
			this.tabReturnRouteFeatures.Location = new System.Drawing.Point(4, 22);
			this.tabReturnRouteFeatures.Name = "tabReturnRouteFeatures";
			this.tabReturnRouteFeatures.Size = new System.Drawing.Size(464, 470);
			this.tabReturnRouteFeatures.TabIndex = 4;
			this.tabReturnRouteFeatures.Text = "Route Features";
			// 
			// dataGridRouteFeatures
			// 
			this.dataGridRouteFeatures.DataMember = "";
			this.dataGridRouteFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridRouteFeatures.Location = new System.Drawing.Point(8, 8);
			this.dataGridRouteFeatures.Name = "dataGridRouteFeatures";
			this.dataGridRouteFeatures.Size = new System.Drawing.Size(448, 456);
			this.dataGridRouteFeatures.TabIndex = 0;
			// 
			// tabReturnBarrierFeatures
			// 
			this.tabReturnBarrierFeatures.Controls.Add(this.dataGridBarrierFeatures);
			this.tabReturnBarrierFeatures.Location = new System.Drawing.Point(4, 22);
			this.tabReturnBarrierFeatures.Name = "tabReturnBarrierFeatures";
			this.tabReturnBarrierFeatures.Size = new System.Drawing.Size(464, 470);
			this.tabReturnBarrierFeatures.TabIndex = 3;
			this.tabReturnBarrierFeatures.Text = "Barrier Features";
			// 
			// dataGridBarrierFeatures
			// 
			this.dataGridBarrierFeatures.DataMember = "";
			this.dataGridBarrierFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridBarrierFeatures.Location = new System.Drawing.Point(8, 8);
			this.dataGridBarrierFeatures.Name = "dataGridBarrierFeatures";
			this.dataGridBarrierFeatures.Size = new System.Drawing.Size(448, 456);
			this.dataGridBarrierFeatures.TabIndex = 0;
			// 
			// tabReturnStopsFeatures
			// 
			this.tabReturnStopsFeatures.Controls.Add(this.dataGridStopFeatures);
			this.tabReturnStopsFeatures.Location = new System.Drawing.Point(4, 22);
			this.tabReturnStopsFeatures.Name = "tabReturnStopsFeatures";
			this.tabReturnStopsFeatures.Size = new System.Drawing.Size(464, 470);
			this.tabReturnStopsFeatures.TabIndex = 2;
			this.tabReturnStopsFeatures.Text = "Stop Features";
			// 
			// dataGridStopFeatures
			// 
			this.dataGridStopFeatures.DataMember = "";
			this.dataGridStopFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridStopFeatures.Location = new System.Drawing.Point(8, 8);
			this.dataGridStopFeatures.Name = "dataGridStopFeatures";
			this.dataGridStopFeatures.Size = new System.Drawing.Size(448, 456);
			this.dataGridStopFeatures.TabIndex = 0;
			// 
			// tabReturnDirections
			// 
			this.tabReturnDirections.Controls.Add(this.treeViewDirections);
			this.tabReturnDirections.Location = new System.Drawing.Point(4, 22);
			this.tabReturnDirections.Name = "tabReturnDirections";
			this.tabReturnDirections.Size = new System.Drawing.Size(464, 470);
			this.tabReturnDirections.TabIndex = 1;
			this.tabReturnDirections.Text = "Directions";
			// 
			// treeViewDirections
			// 
			this.treeViewDirections.Location = new System.Drawing.Point(8, 8);
			this.treeViewDirections.Name = "treeViewDirections";
			this.treeViewDirections.Size = new System.Drawing.Size(448, 456);
			this.treeViewDirections.TabIndex = 69;
			// 
			// tabReturnRouteGeometry
			// 
			this.tabReturnRouteGeometry.Controls.Add(this.treeViewRouteGeometry);
			this.tabReturnRouteGeometry.Location = new System.Drawing.Point(4, 22);
			this.tabReturnRouteGeometry.Name = "tabReturnRouteGeometry";
			this.tabReturnRouteGeometry.Size = new System.Drawing.Size(464, 470);
			this.tabReturnRouteGeometry.TabIndex = 5;
			this.tabReturnRouteGeometry.Text = "Route Geometry";
			// 
			// treeViewRouteGeometry
			// 
			this.treeViewRouteGeometry.Location = new System.Drawing.Point(8, 8);
			this.treeViewRouteGeometry.Name = "treeViewRouteGeometry";
			this.treeViewRouteGeometry.Size = new System.Drawing.Size(448, 456);
			this.treeViewRouteGeometry.TabIndex = 1;
			// 
			// Route_WebServiceClass
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(936, 550);
			this.Controls.Add(this.tabCtrlOutput);
			this.Controls.Add(this.fraINARouteSolver);
			this.Controls.Add(this.fraINAServerRouteParams);
			this.Controls.Add(this.fraINAServerSolverParams);
			this.Controls.Add(this.fraINASolverSettings);
			this.Controls.Add(this.cmdSolve);
			this.Name = "Route_WebServiceClass";
			this.Text = "NAServer - Route Web Service";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.fraINASolverSettings.ResumeLayout(false);
			this.fraINAServerSolverParams.ResumeLayout(false);
			this.fraINAServerSolverParams.PerformLayout();
			this.fraINARouteSolver.ResumeLayout(false);
			this.fraINARouteSolver.PerformLayout();
			this.fraINAServerRouteParams.ResumeLayout(false);
			this.tabCtrlOutput.ResumeLayout(false);
			this.tabReturnMap.ResumeLayout(false);
			this.tabReturnRouteFeatures.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridRouteFeatures)).EndInit();
			this.tabReturnBarrierFeatures.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridBarrierFeatures)).EndInit();
			this.tabReturnStopsFeatures.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridStopFeatures)).EndInit();
			this.tabReturnDirections.ResumeLayout(false);
			this.tabReturnRouteGeometry.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Route_WebServiceClass());
		}

		/// <summary>
		/// This function
		///     - sets the server and solver parameters
		///     - populates the stops NALocations
		///     - gets and displays the server results (map, directions, etc.)
		/// </summary>
		private void cmdSolve_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				// Get SolverParams
				NAServerSolverParams solverParams = m_naServer.GetSolverParameters(cboNALayers.Text) as NAServerSolverParams;

				// Set Solver params
				SetNASolverSettings(solverParams);
				SetSolverSpecificInterface(solverParams);
				SetServerSolverParams(solverParams);

				// Load Locations
				LoadLocations(solverParams);

				//Solve the Route
				NAServerSolverResults solverResults;
				solverResults = m_naServer.Solve(solverParams);

				//Get NAServer results in the tab controls
				OutputResults(solverParams, solverResults);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// This function
		///     - gets all route network analysis layers
		///     - selects the first route network analysis layer
		///     - sets all controls for this route network analysis layer
		/// </summary>
		private void GetNetworkAnalysisLayers()
		{

			this.Cursor = Cursors.WaitCursor;

			try
			{
				// Enable Frame
				fraINAServerSolverParams.Enabled = true;

				//Get Route NA layer names
				cboNALayers.Items.Clear();
				string[] naLayers = m_naServer.GetNALayerNames(esriNAServerLayerType.esriNAServerRouteLayer);
				for (int i = 0; i < naLayers.Length; i++)
				{
					cboNALayers.Items.Add(naLayers[i]);
				}

				// Select the first NA Layer name
				if (cboNALayers.Items.Count > 0)
					cboNALayers.SelectedIndex = 0;
				else
					MessageBox.Show("There is no Network Analyst layer associated with this MapServer object!", "NAServer - Route Sample", System.Windows.Forms.MessageBoxButtons.OK);

			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// This function sets all controls for the selected route network analysis layer
		/// </summary>
		private void cboNALayers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string naLayerName = cboNALayers.Text;

			// Enable Solve Button
			cmdSolve.Enabled = (naLayerName.Length == 0);

			if (naLayerName.Length == 0)
				return;

			this.Cursor = Cursors.WaitCursor;

			try
			{
				NAServerSolverParams solverParams = m_naServer.GetSolverParameters(naLayerName);
				NAServerNetworkDescription networkDescription = m_naServer.GetNetworkDescription(naLayerName);

				// Setup Default Properties
				GetNASolverSettings(networkDescription, solverParams);
				GetSolverSpecificInterface(solverParams);
				GetServerSolverParams(networkDescription, solverParams);

				// Make frames Enable
				MakeFramesEnabled(solverParams);

				// Hide Tabs
				HideTabs();

				cmdSolve.Enabled = true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}

			this.Cursor = Cursors.Default;

		}

		/// <summary>
		/// Get Default NASolverSettings controls (Cost Attributes, Restrictions Attributes. etc.)
		/// </summary>
		private void GetNASolverSettings(NAServerNetworkDescription networkDescription, NAServerSolverParams solverSettings)
		{
			int ImpedanceIndex = 0;

			//Get Attributes
			cboImpedance.Items.Clear();
			chklstAccumulateAttributes.Items.Clear();
			chklstRestrictions.Items.Clear();
			cboUturnPolicy.SelectedIndex = -1;

			NAServerNetworkAttribute[] attributes = networkDescription.NetworkAttributes;
			string[] accumulateAttributeNames = solverSettings.AccumulateAttributeNames;
			string[] restrictionAttributeNames = solverSettings.RestrictionAttributeNames;

			for (int i = 0; i < attributes.Length; i++)
			{
				NAServerNetworkAttribute networkAttribute = attributes[i];
				string networkAttributeName = networkAttribute.Name;
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					chklstAccumulateAttributes.Items.Add(networkAttributeName, IsStringInStringArray(networkAttributeName, accumulateAttributeNames));

					int index = cboImpedance.Items.Add(networkAttributeName + " (" + networkAttribute.Units.ToString().Substring(7) + ")");
					if (networkAttributeName == solverSettings.ImpedanceAttributeName)
						ImpedanceIndex = index;
				}

				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTRestriction)
				{
					chklstRestrictions.Items.Add(networkAttributeName, IsStringInStringArray(networkAttributeName, restrictionAttributeNames));
				}
			}

			if (cboImpedance.Items.Count > 0)
				cboImpedance.SelectedIndex = ImpedanceIndex;

			chkUseHierarchy.Checked = solverSettings.UseHierarchy;
			chkUseHierarchy.Enabled = solverSettings.HierarchyAttributeName.Length > 0;
			chkIgnoreInvalidLocations.Checked = solverSettings.IgnoreInvalidLocations;
			cboUturnPolicy.SelectedIndex = System.Convert.ToInt32(solverSettings.RestrictUTurns);
		}

		private bool IsStringInStringArray(string inputString, string[] stringArray)
		{
			int numInArray = stringArray.Length;
			for (int i = 0; i < numInArray; i++)
			{
				if (inputString.Equals(stringArray[i]))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Set Default route solver controls  (BestOrder, UseTimeWindows, UseStartTime, etc.)
		/// </summary>
		private void GetSolverSpecificInterface(NAServerSolverParams solverParams)
		{
			NAServerRouteParams routeSolver = solverParams as NAServerRouteParams;
			if (routeSolver != null)
			{
				chkBestOrder.Checked = routeSolver.FindBestSequence;
				chkPreserveFirst.Checked = routeSolver.PreserveFirstStop;
				chkPreserveLast.Checked = routeSolver.PreserveLastStop;
				if (chkBestOrder.Checked == true)
				{
					this.chkPreserveFirst.Enabled = true;
					this.chkPreserveLast.Enabled = true;
				}
				else
				{
					this.chkPreserveFirst.Enabled = false;
					this.chkPreserveLast.Enabled = false;
				}
				chkUseTimeWindows.Checked = routeSolver.UseTimeWindows;
				chkUseStartTime.Checked = routeSolver.UseStartTime;
				if (chkUseStartTime.Checked)
				{
					txtStartTime.Enabled = true;
					txtStartTime.Text = routeSolver.StartTime.ToString();
				}
				else
				{
					txtStartTime.Enabled = false;
					txtStartTime.Text = System.DateTime.Now.ToString();
				}
				cboRouteOutputLines.SelectedIndex = System.Convert.ToInt32(routeSolver.OutputLines);
			}
		}

		/// <summary>
		/// Get ServerSolverParams controls (ReturnMap, SnapTolerance, etc.)
		/// </summary>
		private void GetServerSolverParams(NAServerNetworkDescription networkDescription, NAServerSolverParams solverParams)
		{
			chkReturnMap.Checked = true;
			txtSnapTolerance.Text = solverParams.SnapTolerance.ToString();
			txtMaxSnapTolerance.Text = solverParams.MaxSnapTolerance.ToString();
			cboSnapToleranceUnits.SelectedIndex = Convert.ToInt32(solverParams.SnapToleranceUnits);

			//Set Route Defaults
			chkReturnRouteGeometries.Checked = false;
			chkReturnRoutes.Checked = true;
			chkReturnStops.Checked = true;
			chkReturnDirections.Checked = true;
			checkReturnBarriers.Checked = true;

			//Set Directions Defaults
			cboRouteDirectionsTimeAttribute.Items.Clear();
			NAServerNetworkAttribute[] attributes = networkDescription.NetworkAttributes;
			for (int i = 0; i < attributes.Length; i++)
			{
				NAServerNetworkAttribute networkAttribute = attributes[i];
				if (networkAttribute.UsageType == esriNetworkAttributeUsageType.esriNAUTCost)
				{
					if (String.Compare(networkAttribute.Units.ToString(), "esriNAUMinutes") == 0)
					{
						cboRouteDirectionsTimeAttribute.Items.Add(networkAttribute.Name);
					}
				}
			}

			// Set the default direction settings
			NAServerRouteParams routeParams = solverParams as NAServerRouteParams;
			if (routeParams != null)
			{
				cboRouteDirectionsLengthUnits.Text = routeParams.DirectionsLengthUnits.ToString().Substring(7);
				//Select the first time attribute
				if (cboRouteDirectionsTimeAttribute.Items.Count > 0)
					cboRouteDirectionsTimeAttribute.Text = routeParams.DirectionsTimeAttributeName;
			}
		}

		/// <summary>
		/// Set general solver settings  (Impedance, Restrictions, Accumulates, etc.)
		/// </summary>
		private void SetNASolverSettings(NAServerSolverParams solverSettings)
		{
			solverSettings.ImpedanceAttributeName = ExtractImpedanceName(cboImpedance.Text);

			string[] restrictionAttributes = new string[chklstRestrictions.CheckedItems.Count];
			for (int i = 0; i < chklstRestrictions.CheckedItems.Count; i++)
				restrictionAttributes[i] = chklstRestrictions.Items[chklstRestrictions.CheckedIndices[i]].ToString();
			solverSettings.RestrictionAttributeNames = restrictionAttributes;

			string[] accumulateAttributes = new string[chklstAccumulateAttributes.CheckedItems.Count];
			for (int i = 0; i < chklstAccumulateAttributes.CheckedItems.Count; i++)
				accumulateAttributes[i] = chklstAccumulateAttributes.Items[chklstAccumulateAttributes.CheckedIndices[i]].ToString();
			solverSettings.AccumulateAttributeNames = accumulateAttributes;

			solverSettings.RestrictUTurns = (esriNetworkForwardStarBacktrack)cboUturnPolicy.SelectedIndex;
			solverSettings.IgnoreInvalidLocations = chkIgnoreInvalidLocations.Checked;
			solverSettings.UseHierarchy = chkUseHierarchy.Checked;
		}

		/// <summary>
		/// Set specific solver settings  (FindBestSequence, UseTimeWindows, etc.)      
		/// </summary>  
		private void SetSolverSpecificInterface(NAServerSolverParams solverParams)
		{
			NAServerRouteParams routeSolver = solverParams as NAServerRouteParams;
			if (routeSolver != null)
			{
				routeSolver.FindBestSequence = chkBestOrder.Checked;
				routeSolver.PreserveFirstStop = chkPreserveFirst.Checked;
				routeSolver.PreserveLastStop = chkPreserveLast.Checked;
				routeSolver.UseTimeWindows = chkUseTimeWindows.Checked;
				routeSolver.OutputLines = (esriNAOutputLineType)cboRouteOutputLines.SelectedIndex;

				routeSolver.UseStartTime = chkUseStartTime.Checked;
				if (routeSolver.UseStartTime == true)
					routeSolver.StartTime = System.Convert.ToDateTime(txtStartTime.Text.ToString());
			}
		}

		/// <summary>
		/// Set server solver parameters  (ReturnMap, SnapTolerance, etc.)
		/// </summary>
		private void SetServerSolverParams(NAServerSolverParams solverParams)
		{
			solverParams.ReturnMap = chkReturnMap.Checked;
			solverParams.SnapTolerance = Convert.ToDouble(txtSnapTolerance.Text);
			solverParams.MaxSnapTolerance = Convert.ToDouble(txtMaxSnapTolerance.Text);
			solverParams.SnapToleranceUnits = (esriUnits)cboSnapToleranceUnits.SelectedIndex;
			solverParams.ImageDescription.ImageDisplay.ImageWidth = pictureBox.Width;
			solverParams.ImageDescription.ImageDisplay.ImageHeight = pictureBox.Height;

			//			// This code shows how to specify the output spatial reference in order to get the map
			//			// in a different spatial reference than the Network Dataset
			//			ESRISpatialReferenceGEN sr = pServerContext.CreateObject("esriGeometry.GeographicCoordinateSystem") as IESRISpatialReferenceGEN;
			//			int read;
			//			sr.ImportFromESRISpatialReference("GEOGCS[GCS_North_American_1983,DATUM[D_North_American_1983,SPHEROID[GRS_1980,6378137.0,298.257222101]],PRIMEM[Greenwich,0.0],UNIT[Degree,0.0174532925199433]]", out read);
			//			solverParams.OutputSpatialReference = sr as SpatialReference;

			NAServerRouteParams routeParams = solverParams as NAServerRouteParams;
			if (routeParams != null)
			{
				routeParams.ReturnRouteGeometries = chkReturnRouteGeometries.Checked;
				routeParams.ReturnRoutes = chkReturnRoutes.Checked;
				routeParams.ReturnStops = chkReturnStops.Checked;
				routeParams.ReturnBarriers = checkReturnBarriers.Checked;
				routeParams.ReturnDirections = chkReturnDirections.Checked;
				routeParams.DirectionsLengthUnits = GetstringToesriUnits(cboRouteDirectionsLengthUnits.Text);
				routeParams.DirectionsTimeAttributeName = cboRouteDirectionsTimeAttribute.Text;
			}
		}

		/// <summary>
		/// Make frames Enabled
		/// </summary> 
		private void MakeFramesEnabled(NAServerSolverParams solverParams)
		{
			fraINARouteSolver.Enabled = ((solverParams as NAServerRouteParams) != null);
			fraINAServerRouteParams.Enabled = ((solverParams as NAServerRouteParams) != null);
			fraINASolverSettings.Enabled = ((solverParams as NAServerRouteParams) != null);
		}

		/// <summary>
		/// Load form
		/// </summary> 
		private void Form1_Load(object sender, System.EventArgs e)
		{
			try
			{
				cboUturnPolicy.Items.Add("Nowhere");
				cboUturnPolicy.Items.Add("Everywhere");
				cboUturnPolicy.Items.Add("Only at Dead Ends");

				cboSnapToleranceUnits.Items.Add("Unknown Units");
				cboSnapToleranceUnits.Items.Add("Inches");
				cboSnapToleranceUnits.Items.Add("Points");
				cboSnapToleranceUnits.Items.Add("Feet");
				cboSnapToleranceUnits.Items.Add("Yards");
				cboSnapToleranceUnits.Items.Add("Miles");
				cboSnapToleranceUnits.Items.Add("Nautical Miles");
				cboSnapToleranceUnits.Items.Add("Millimeters");
				cboSnapToleranceUnits.Items.Add("Centimeters");
				cboSnapToleranceUnits.Items.Add("Meters");
				cboSnapToleranceUnits.Items.Add("Kilometers");
				cboSnapToleranceUnits.Items.Add("DecimalDegrees");
				cboSnapToleranceUnits.Items.Add("Decimeters");

				// Route
				cboRouteOutputLines.Items.Add("None");
				cboRouteOutputLines.Items.Add("Straight Line");
				cboRouteOutputLines.Items.Add("True Shape");
				cboRouteOutputLines.Items.Add("True Shape With Ms");

				cboRouteDirectionsLengthUnits.Items.Add("Feet");
				cboRouteDirectionsLengthUnits.Items.Add("Yards");
				cboRouteDirectionsLengthUnits.Items.Add("Miles");
				cboRouteDirectionsLengthUnits.Items.Add("Meters");
				cboRouteDirectionsLengthUnits.Items.Add("Kilometers");

				ConnectToWebService();
				GetNetworkAnalysisLayers();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "An error has occurred");
			}
		}

		/// <summary>
		/// Get NAServer Object from the web service
		/// </summary>
		private void ConnectToWebService()
		{
			m_naServer = null;

			// Get NAServer
			m_naServer = new SanFrancisco_NAServer();
			if (m_naServer != null)
				return;

			throw (new System.Exception("Could not find the web service."));

		}

		/// <summary>
		/// This function shows how to populate stop locations using two different options:
		///     1) From Record Set using a Point Feature Class - Uncommented
		///     2) From an Array of PropertySets - Commented out
		/// Uncomment the option, you would like to use
		/// </summary>
		private void LoadLocations(NAServerSolverParams solverParams)
		{
			// Set first point
			PropertySet[] propSets = new PropertySet[2];

			propSets[0] = CreateLocationPropertySet("Stop 1", "-122.49024904900", "37.74811940430", null);
			propSets[1] = CreateLocationPropertySet("Stop 2", "-122.43083365400", "37.75396354490", null);

			NAServerRouteParams routeParams = solverParams as NAServerRouteParams;
			NAServerPropertySets stopsPropSets = new NAServerPropertySets();
			stopsPropSets.PropertySets = propSets;

			routeParams.Stops = stopsPropSets;
		}

		/// <summary>
		/// Create a property set for NALocation
		/// </summary> 
		private PropertySet CreateLocationPropertySet(string name, string X, string Y, SpatialReference sr)
		{
			PropertySet propSet = new PropertySet();
			propSet.PropertyArray = CreateLocationPropertyArray(name, X, Y, sr);
			return propSet;
		}

		/// <summary>
		/// Create a property location array
		/// </summary> 
		private PropertySetProperty[] CreateLocationPropertyArray(string name, string X, string Y, SpatialReference sr)
		{
			PropertySetProperty[] propSetProperty;
			if (sr == null)
				propSetProperty = new PropertySetProperty[3];
			else
				propSetProperty = new PropertySetProperty[4];

			propSetProperty[0] = new PropertySetProperty();
			propSetProperty[0].Key = "NAME";
			propSetProperty[0].Value = name;

			propSetProperty[1] = new PropertySetProperty();
			propSetProperty[1].Key = "X";
			propSetProperty[1].Value = X;

			propSetProperty[2] = new PropertySetProperty();
			propSetProperty[2].Key = "Y";
			propSetProperty[2].Value = Y;

			if (sr != null)
			{
				propSetProperty[3] = new PropertySetProperty();
				propSetProperty[3].Key = "SpatialReference";
				propSetProperty[3].Value = sr;
			}

			return propSetProperty;
		}

		/// <summary>
		/// Output Results Messages, Map, Route Geometries, Directions
		/// </summary>
		private void OutputResults(NAServerSolverParams solverParams, NAServerSolverResults solverResults)
		{
			string messagesSolverResults = "";

			// Output Solve messages
			GPMessages gpMessages = solverResults.SolveMessages;
			GPMessage[] arrGPMessage = gpMessages.GPMessages1;
			if (arrGPMessage != null)
			{
				for (int i = 0; i < arrGPMessage.GetLength(0); i++)
				{
					GPMessage gpMessage = arrGPMessage[i];
					messagesSolverResults += "\n" + gpMessage.MessageDesc;
				}
			}

			// Uncomment the following section to output the total impedance of each route in a MessageBox

			//NAServerRouteResults routeSolverResults = solverResults as NAServerRouteResults;
			//if (routeSolverResults != null)
			//{
			//    for (int i = 0; i < routeSolverResults.TotalImpedances.GetLength(0); i++)
			//    {
			//        messagesSolverResults += "\nTotal Impedance for Route[" + (i + 1) + "] = ";
			//        messagesSolverResults += routeSolverResults.TotalImpedances[i].ToString("f");
			//        messagesSolverResults += " " + ExtractImpedanceUnits(cboImpedance.Text).ToLower();
			//    }
			//}

			// Show a message box displaying both solver messages and total_impedance per route
			if (messagesSolverResults.Length > 0)
				MessageBox.Show(messagesSolverResults, "NAServer Route Results");

			//Output Map
			pictureBox.Image = null;
			if (solverParams.ReturnMap)
			{
				pictureBox.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(solverResults.MapImage.ImageData));
			}
			pictureBox.Refresh();

			if (((solverParams as NAServerRouteParams) != null) && ((solverResults as NAServerRouteResults) != null))
				OutputRouteResults(solverParams as NAServerRouteParams, solverResults as NAServerRouteResults);
		}

		/// <summary>
		/// Output Route Results according to the NAServerRouteParams
		/// </summary>
		private void OutputRouteResults(NAServerRouteParams solverParams, NAServerRouteResults solverResults)
		{
			// Return Directions if generated
			if (solverParams.ReturnDirections)
			{
				AddTabAndControl(this.tabReturnDirections, this.treeViewDirections);
				OutputDirections(solverResults.Directions);
			}
			else
				this.tabCtrlOutput.TabPages.Remove(this.tabReturnDirections);

			// Return Route Geometries is generated
			if (solverParams.ReturnRouteGeometries)
			{
				AddTabAndControl(this.tabReturnRouteGeometry, this.treeViewRouteGeometry);
				OutputPolylines(solverResults.RouteGeometries);
			}
			else
				this.tabCtrlOutput.TabPages.Remove(this.tabReturnRouteGeometry);

			// Return Route Features as RecordSet
			if (solverParams.ReturnRoutes)
			{
				AddTabAndControl(this.tabReturnRouteFeatures, this.dataGridRouteFeatures);
				OutputRecSetToDataGrid(solverResults.Routes, dataGridRouteFeatures);
			}
			else
				this.tabCtrlOutput.TabPages.Remove(this.tabReturnRouteFeatures);

			// Return Stop Features as RecordSet
			if (solverParams.ReturnStops)
			{
				AddTabAndControl(this.tabReturnStopsFeatures, this.dataGridStopFeatures);
				OutputRecSetToDataGrid(solverResults.Stops, dataGridStopFeatures);
			}
			else
				this.tabCtrlOutput.TabPages.Remove(this.tabReturnStopsFeatures);

			// Return Barrier Features as RecordSet
			if (solverParams.ReturnBarriers)
			{
				AddTabAndControl(this.tabReturnBarrierFeatures, this.dataGridBarrierFeatures);
				OutputRecSetToDataGrid(solverResults.Barriers, dataGridBarrierFeatures);
			}
			else
				this.tabCtrlOutput.TabPages.Remove(this.tabReturnBarrierFeatures);

			// Make TabControlOutput enable
			tabCtrlOutput.Enabled = true;
		}

		/// <summary>
		/// Output Route Geometries (Polylines) in a TreeView control
		/// </summary> 
		private void OutputPolylines(Polyline[] polylines)
		{
			if (polylines == null)
			{
				treeViewRouteGeometry.Nodes.Clear();
				TreeNode newNode = new TreeNode("Route Geometry not generated");
				treeViewRouteGeometry.Nodes.Add(newNode);
				return;
			}

			// Suppress repainting the TreeView until all the objects have been created.
			treeViewRouteGeometry.BeginUpdate();

			// Clear the TreeView each time the method is called.
			treeViewRouteGeometry.Nodes.Clear();

			for (int i = 0; i < polylines.GetLength(0); i++)
			{
				PolylineN polylineN = polylines[i] as PolylineN;

				if (polylineN != null)
				{
					TreeNode newNode = new TreeNode("Polyline Length for Route [" + (i + 1) + "] = " + polylineN.PathArray.GetLength(0).ToString());
					treeViewRouteGeometry.Nodes.Add(newNode);

					for (int j = 0; j < polylineN.PathArray.GetLength(0); j++)
					{
						WebService.Point[] points = polylineN.PathArray[j].PointArray;

						for (int k = 0; k < points.GetLength(0); k++)
						{
							PointN point = points[k] as PointN;
							if (point != null)
								treeViewRouteGeometry.Nodes[i].Nodes.Add(new TreeNode("Point [" + (k + 1) + "]: " + point.X + "," + point.Y));
						}
					}
				}
			}

			// Check if Route Geometry has been generated
			if (polylines.Length == 0)
			{
				TreeNode newNode = new TreeNode("Route Geometry not generated");
				treeViewRouteGeometry.Nodes.Add(newNode);
			}
			// Begin repainting the TreeView.
			treeViewRouteGeometry.ExpandAll();
			treeViewRouteGeometry.EndUpdate();
		}

		/// <summary>
		/// Output Directions if a TreeView control
		/// </summary> 
		private void OutputDirections(NAStreetDirections[] serverDirections)
		{
			if (serverDirections == null)
			{
				treeViewDirections.Nodes.Clear();
				TreeNode newNode = new TreeNode("Directions not generated");
				treeViewDirections.Nodes.Add(newNode);
				return;
			}

			// Suppress repainting the TreeView until all the objects have been created.
			treeViewDirections.BeginUpdate();

			// Clear the TreeView each time the method is called.
			treeViewDirections.Nodes.Clear();

			for (int i = serverDirections.GetLowerBound(0); i <= serverDirections.GetUpperBound(0); i++)
			{
				// get Directions from the ith route
				NAStreetDirections directions;
				directions = serverDirections[i];

				// get Summary (Total Distance and Time)
				NAStreetDirection direction = directions.Summary;
				string totallength = null, totaltime = null;
				string[] SummaryStrings = direction.Strings;
				for (int k = SummaryStrings.GetLowerBound(0); k < SummaryStrings.GetUpperBound(0); k++)
				{
					if (direction.StringTypes[k] == esriDirectionsStringType.esriDSTLength)
						totallength = SummaryStrings[k];
					if (direction.StringTypes[k] == esriDirectionsStringType.esriDSTTime)
						totaltime = SummaryStrings[k];
				}

				// Add a Top a Node with the Route number and Total Distance and Total Time
				TreeNode newNode = new TreeNode("Directions for Route [" + (i + 1) + "] - Total Distance: " + totallength + " Total Time: " + totaltime);
				treeViewDirections.Nodes.Add(newNode);

				// Then add a node for each step-by-step directions
				NAStreetDirection[] StreetDirections = directions.Directions;
				for (int directionIndex = StreetDirections.GetLowerBound(0); directionIndex <= StreetDirections.GetUpperBound(0); directionIndex++)
				{
					NAStreetDirection streetDirection = StreetDirections[directionIndex];
					string[] StringStreetDirection = streetDirection.Strings;
					for (int stringIndex = StringStreetDirection.GetLowerBound(0); stringIndex <= StringStreetDirection.GetUpperBound(0); stringIndex++)
					{
						if (streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTGeneral ||
							streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTDepart ||
							streetDirection.StringTypes[stringIndex] == esriDirectionsStringType.esriDSTArrive)
							treeViewDirections.Nodes[i].Nodes.Add(new TreeNode(StringStreetDirection[stringIndex]));
					}
				}
			}

			// Check if Directions have been generated
			if (serverDirections.Length == 0)
			{
				TreeNode newNode = new TreeNode("Directions not generated");
				treeViewDirections.Nodes.Add(newNode);
			}

			// Begin repainting the TreeView.
			treeViewDirections.ExpandAll();
			treeViewDirections.EndUpdate();
		}

		/// <summary>
		/// Generic function to output RecordSet (Stops, Barriers, or Routes) in a DataDrid Control
		/// </summary> 
		private void OutputRecSetToDataGrid(RecordSet recSet, System.Windows.Forms.DataGrid outDataGrid)
		{

			Fields fields = recSet.Fields;
			Field[] fldArray = fields.FieldArray;

			DataSet dataSet = new DataSet("dataSet");
			DataTable dataTable = new DataTable("Results");
			dataSet.Tables.Add(dataTable);

			DataColumn dataColumn = null;
			for (int f = 0; f < fldArray.Length; f++)
			{
				dataColumn = new DataColumn(fldArray[f].Name);
				dataTable.Columns.Add(dataColumn);
			}

			// Populate DataGrid rows by RecordSet
			DataRow newDataRow;
			Record[] records = recSet.Records;
			Record record = null;
			object[] values = null;
			for (int j = 0; j < records.Length; j++)
			{
				newDataRow = dataTable.NewRow();
				record = records[j];
				values = record.Values;
				for (int l = 0; l < values.Length; l++)
				{
					if (values[l] != null)
						newDataRow[l] = values[l].ToString();
				}
				dataTable.Rows.Add(newDataRow);
			}

			outDataGrid.SetDataBinding(dataSet, "Results");
			outDataGrid.Visible = true;
		}

		/// <summary>
		/// Enable the startTime control
		/// </summary> 
		private void chkUseStartTime_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkUseStartTime.Checked == true)
				txtStartTime.Enabled = true;
			else
				txtStartTime.Enabled = false;

		}

		/// <summary>
		/// Helper function to convert a string unit (e.g Miles) to an esriNetworkAnalystUnits (e.g esriNAUMiles)
		/// </summary>         
		private esriNetworkAttributeUnits GetstringToesriUnits(string stresriUnits)
		{
			switch (stresriUnits.ToLower())
			{
				case "inches": return esriNetworkAttributeUnits.esriNAUInches;
				case "feet": return esriNetworkAttributeUnits.esriNAUFeet;
				case "yards": return esriNetworkAttributeUnits.esriNAUYards;
				case "miles": return esriNetworkAttributeUnits.esriNAUMiles;
				case "nautical miles": return esriNetworkAttributeUnits.esriNAUNauticalMiles;
				case "millimeters": return esriNetworkAttributeUnits.esriNAUMillimeters;
				case "centimeters": return esriNetworkAttributeUnits.esriNAUCentimeters;
				case "meters": return esriNetworkAttributeUnits.esriNAUMeters;
				case "kilometers": return esriNetworkAttributeUnits.esriNAUKilometers;
				case "decimal degrees": return esriNetworkAttributeUnits.esriNAUDecimalDegrees;
				case "decimeters": return esriNetworkAttributeUnits.esriNAUDecimeters;
				case "seconds": return esriNetworkAttributeUnits.esriNAUSeconds;
				case "minutes": return esriNetworkAttributeUnits.esriNAUMinutes;
				case "hours": return esriNetworkAttributeUnits.esriNAUHours;
				case "days": return esriNetworkAttributeUnits.esriNAUDays;
				case "unknown": return esriNetworkAttributeUnits.esriNAUUnknown;
				default: return esriNetworkAttributeUnits.esriNAUUnknown;
			}
		}

		/// <summary>
		/// Helper function to extract the impedance name in a string like "impedancename (impedanceunits)"
		/// </summary> 
		private string ExtractImpedanceName(string impedanceUnits)
		{
			int firstIndex = impedanceUnits.LastIndexOf(" ");
			int lastIndex = impedanceUnits.Length;
			return impedanceUnits.Remove(firstIndex, (lastIndex - firstIndex));
		}

		/// <summary>
		/// Helper function to extract the impedance unit in a string like "impedancename (impedanceunits)"
		/// </summary> 
		private string ExtractImpedanceUnits(string impedanceUnits)
		{
			int firstIndex = impedanceUnits.LastIndexOf("(") + 1;
			int lastIndex = impedanceUnits.LastIndexOf(")");
			return impedanceUnits.Substring(firstIndex, (lastIndex - firstIndex));
		}

		/// <summary>
		/// Enable chkPreserveFirst and chkPreserveFirst controls
		/// </summary> 
		private void chkBestOrder_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkBestOrder.Checked == true)
			{
				this.chkPreserveFirst.Enabled = true;
				this.chkPreserveLast.Enabled = true;
			}
			else
			{
				this.chkPreserveFirst.Enabled = false;
				this.chkPreserveLast.Enabled = false;
			}
		}

		/// <summary>
		/// Hide tabs
		/// </summary> 
		private void HideTabs()
		{
			// remove tabs initially - There are added when the result is returned.			
			this.tabCtrlOutput.TabPages.Remove(this.tabReturnDirections);
			this.tabCtrlOutput.TabPages.Remove(this.tabReturnStopsFeatures);
			this.tabCtrlOutput.TabPages.Remove(this.tabReturnBarrierFeatures);
			this.tabCtrlOutput.TabPages.Remove(this.tabReturnRouteGeometry);
			this.tabCtrlOutput.TabPages.Remove(this.tabReturnRouteFeatures);
		}

		/// <summary>
		//// Add a tab page and a tab control
		/// </summary> 
		private void AddTabAndControl(TabPage tabPage, System.Windows.Forms.Control controlToAdd)
		{
			if (!tabCtrlOutput.TabPages.Contains(tabPage))
			{
				tabCtrlOutput.TabPages.Add(tabPage);
				tabPage.Controls.Add(controlToAdd);
			}
		}
	}
}