using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS;


namespace InkSketchCommit
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpbInkSketch;
		private System.Windows.Forms.GroupBox gpbReport;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.RadioButton radManual;
		private System.Windows.Forms.RadioButton radAutoGraphic;
		private System.Windows.Forms.RadioButton radAutoText;
		private System.Windows.Forms.Label lbl1sec;
		private System.Windows.Forms.Label lbl10sec;
		private System.Windows.Forms.Label lblAutoComplete;
		private System.Windows.Forms.Label lblCollectingStatus;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxNumber;
		/// <summary>
		/// Required designer variable.
		/// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TrackBar tbrAutoComplete;
		private IMap m_pMap;
		private ESRI.ArcGIS.Controls.IEngineInkEnvironment m_EngineInkEnvironment;
		private IEngineInkEnvironmentEvents_OnStartEventHandler m_startInkE;
		private IEngineInkEnvironmentEvents_OnStopEventHandler m_stopInkE;
        private IEngineInkEnvironmentEvents_OnGestureEventHandler m_gestureInkE;
        private TableLayoutPanel tableLayoutPanel1;
        private AxToolbarControl axToolbarControl1;
        private AxMapControl axMapControl1;
        private AxLicenseControl axLicenseControl1;

		///Tablet PC system metric value used by GetSystemMetrics to identify whether the application
		///is running on a Tablet PC.

		private const int SM_TABLETPC = 86;
		/// <summary>
		/// The GetSystemMetrics function retrieves system metrics and system configuration settings.
		/// </summary>
		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gpbInkSketch = new System.Windows.Forms.GroupBox();
            this.lblAutoComplete = new System.Windows.Forms.Label();
            this.lbl10sec = new System.Windows.Forms.Label();
            this.lbl1sec = new System.Windows.Forms.Label();
            this.tbrAutoComplete = new System.Windows.Forms.TrackBar();
            this.radAutoText = new System.Windows.Forms.RadioButton();
            this.radAutoGraphic = new System.Windows.Forms.RadioButton();
            this.radManual = new System.Windows.Forms.RadioButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.gpbReport = new System.Windows.Forms.GroupBox();
            this.tbxNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCollectingStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.gpbInkSketch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAutoComplete)).BeginInit();
            this.gpbReport.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbInkSketch
            // 
            this.gpbInkSketch.Controls.Add(this.lblAutoComplete);
            this.gpbInkSketch.Controls.Add(this.lbl10sec);
            this.gpbInkSketch.Controls.Add(this.lbl1sec);
            this.gpbInkSketch.Controls.Add(this.tbrAutoComplete);
            this.gpbInkSketch.Controls.Add(this.radAutoText);
            this.gpbInkSketch.Controls.Add(this.radAutoGraphic);
            this.gpbInkSketch.Controls.Add(this.radManual);
            this.gpbInkSketch.Controls.Add(this.lblInfo);
            this.gpbInkSketch.Location = new System.Drawing.Point(433, 51);
            this.gpbInkSketch.Name = "gpbInkSketch";
            this.gpbInkSketch.Size = new System.Drawing.Size(296, 352);
            this.gpbInkSketch.TabIndex = 3;
            this.gpbInkSketch.TabStop = false;
            this.gpbInkSketch.Text = "Ink Sketch Commit Options";
            // 
            // lblAutoComplete
            // 
            this.lblAutoComplete.Location = new System.Drawing.Point(24, 240);
            this.lblAutoComplete.Name = "lblAutoComplete";
            this.lblAutoComplete.Size = new System.Drawing.Size(263, 23);
            this.lblAutoComplete.TabIndex = 7;
            this.lblAutoComplete.Text = "Automatically Commit the Ink Sketch after:";
            // 
            // lbl10sec
            // 
            this.lbl10sec.Location = new System.Drawing.Point(216, 303);
            this.lbl10sec.Name = "lbl10sec";
            this.lbl10sec.Size = new System.Drawing.Size(64, 24);
            this.lbl10sec.TabIndex = 6;
            this.lbl10sec.Text = "(10 sec)";
            // 
            // lbl1sec
            // 
            this.lbl1sec.Location = new System.Drawing.Point(8, 303);
            this.lbl1sec.Name = "lbl1sec";
            this.lbl1sec.Size = new System.Drawing.Size(65, 24);
            this.lbl1sec.TabIndex = 5;
            this.lbl1sec.Text = "(1 sec)";
            // 
            // tbrAutoComplete
            // 
            this.tbrAutoComplete.Location = new System.Drawing.Point(8, 264);
            this.tbrAutoComplete.Minimum = 1;
            this.tbrAutoComplete.Name = "tbrAutoComplete";
            this.tbrAutoComplete.Size = new System.Drawing.Size(264, 56);
            this.tbrAutoComplete.TabIndex = 4;
            this.tbrAutoComplete.Value = 1;
            this.tbrAutoComplete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbrAutoComplete_MouseUp);
            // 
            // radAutoText
            // 
            this.radAutoText.Location = new System.Drawing.Point(16, 167);
            this.radAutoText.Name = "radAutoText";
            this.radAutoText.Size = new System.Drawing.Size(272, 54);
            this.radAutoText.TabIndex = 3;
            this.radAutoText.Text = "Automatically Committed and Recognized as Text (Tablet PC only)";
            this.radAutoText.CheckedChanged += new System.EventHandler(this.radAutoText_CheckedChanged);
            // 
            // radAutoGraphic
            // 
            this.radAutoGraphic.Location = new System.Drawing.Point(16, 144);
            this.radAutoGraphic.Name = "radAutoGraphic";
            this.radAutoGraphic.Size = new System.Drawing.Size(271, 24);
            this.radAutoGraphic.TabIndex = 2;
            this.radAutoGraphic.Text = "Automatically Committed to Graphic";
            this.radAutoGraphic.CheckedChanged += new System.EventHandler(this.radAutoGraphic_CheckedChanged);
            // 
            // radManual
            // 
            this.radManual.Location = new System.Drawing.Point(16, 112);
            this.radManual.Name = "radManual";
            this.radManual.Size = new System.Drawing.Size(160, 24);
            this.radManual.TabIndex = 1;
            this.radManual.Text = "Manually Committed";
            this.radManual.CheckedChanged += new System.EventHandler(this.radManual_CheckedChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(16, 40);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(264, 56);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Ink sketches can be committed manually or automatically. Click on the buttons belo" +
                "w to change the commit method.";
            // 
            // gpbReport
            // 
            this.gpbReport.Controls.Add(this.tbxNumber);
            this.gpbReport.Controls.Add(this.label1);
            this.gpbReport.Controls.Add(this.lblCollectingStatus);
            this.gpbReport.Location = new System.Drawing.Point(433, 409);
            this.gpbReport.Name = "gpbReport";
            this.gpbReport.Size = new System.Drawing.Size(296, 112);
            this.gpbReport.TabIndex = 4;
            this.gpbReport.TabStop = false;
            this.gpbReport.Text = "Sketch Report";
            // 
            // tbxNumber
            // 
            this.tbxNumber.BackColor = System.Drawing.SystemColors.Control;
            this.tbxNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxNumber.Location = new System.Drawing.Point(176, 32);
            this.tbxNumber.Name = "tbxNumber";
            this.tbxNumber.Size = new System.Drawing.Size(100, 15);
            this.tbxNumber.TabIndex = 2;
            this.tbxNumber.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Ink Sketches = ";
            // 
            // lblCollectingStatus
            // 
            this.lblCollectingStatus.Location = new System.Drawing.Point(8, 80);
            this.lblCollectingStatus.Name = "lblCollectingStatus";
            this.lblCollectingStatus.Size = new System.Drawing.Size(272, 16);
            this.lblCollectingStatus.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.46906F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.53094F));
            this.tableLayoutPanel1.Controls.Add(this.gpbInkSketch, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.axMapControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gpbReport, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.axLicenseControl1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.88406F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.11594F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 527);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // axToolbarControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.axToolbarControl1, 2);
            this.axToolbarControl1.Location = new System.Drawing.Point(3, 3);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(729, 28);
            this.axToolbarControl1.TabIndex = 5;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(3, 51);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(423, 352);
            this.axMapControl1.TabIndex = 6;
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(3, 409);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(767, 540);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(775, 580);
            this.MinimumSize = new System.Drawing.Size(775, 580);
            this.Name = "Form1";
            this.Text = "Ink Sketch Commit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gpbInkSketch.ResumeLayout(false);
            this.gpbInkSketch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrAutoComplete)).EndInit();
            this.gpbReport.ResumeLayout(false);
            this.gpbReport.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
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

            Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
	        //Set buddy control 
			axToolbarControl1.SetBuddyControl(axMapControl1);

			//Add items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsInkToolbar", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

			//Set the EngineInkEnviroment Singleton
			m_EngineInkEnvironment = new EngineInkEnvironmentClass();

			//Set the Ink Tool commit type to be manual
			m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeManual;

			//Set the Form Controls 
			tbrAutoComplete.Enabled = false;
			tbrAutoComplete.Minimum = 1;
			tbrAutoComplete.Maximum = 10;
			tbrAutoComplete.TickFrequency = 1;
			tbrAutoComplete.TickStyle = TickStyle.BottomRight;
			lblAutoComplete.Enabled = false;
			lbl1sec.Enabled = false;
			lbl10sec.Enabled = false;
			lblCollectingStatus.Text = "Not Collecting Ink";
			tbxNumber.Text = "0";
			radManual.Checked = true;

	        //The radAutoText Radio button is only available on a Tablet PC.
		    //Converting ink to text requires a Recognizer which can only 
			//run on Windows XP Tablet PC Edition.		

			if (IsRunningOnTabletPC())
				{
				radAutoText.Enabled = true;
				}
			else
				{
				radAutoText.Enabled = false;
				}

			//Set the EngineInkEnvironment OnStart events 
			m_startInkE = new IEngineInkEnvironmentEvents_OnStartEventHandler(OnStartInk);
			((IEngineInkEnvironmentEvents_Event)m_EngineInkEnvironment).OnStart += m_startInkE;

			//Set the EngineInkEnvironment OnStop events 
			m_stopInkE = new IEngineInkEnvironmentEvents_OnStopEventHandler(OnStopInk);
			((IEngineInkEnvironmentEvents_Event)m_EngineInkEnvironment).OnStop += m_stopInkE;

			//Set the EngineInkEnvironment OnGesture events 
			m_gestureInkE = new IEngineInkEnvironmentEvents_OnGestureEventHandler(OnGestureInk);
			((IEngineInkEnvironmentEvents_Event)m_EngineInkEnvironment).OnGesture += m_gestureInkE;
		}

		private void OnStartInk()
		{
            //Report to the user the mode of the Ink Collector
            lblCollectingStatus.Text = "Collecting Ink Sketch";
		}

		private void OnStopInk()
		{
            //Report to the user the mode of the Ink Collector
            lblCollectingStatus.Text = "Not Collecting Ink Sketch";
		}

        private void OnGestureInk(esriEngineInkGesture p_gesture, object p_point)
		{
            //Report to the user that a Gesture has been made
            lblCollectingStatus.Text = "Gesture Made Sketch";
		}

		private bool IsRunningOnTabletPC()
		{
			// Check to see if the application is running on a Tablet PC
			// MSDN Help GetSystemMetrics(86) 
			// Nonzero if the current operating system is the Windows XP Tablet PC edition,
			// 0 (zero) if not.

			if (Win32.GetSystemMetrics(SM_TABLETPC) != 0)
				return true;
			else
				return false;
		}



		private void radManual_CheckedChanged(object sender, System.EventArgs e)
		{
			//Manually committed ink sketch
			if (radManual.Checked)
			{
				tbrAutoComplete.Enabled = false;
				lblAutoComplete.Enabled = false;
				lbl1sec.Enabled = false;
				lbl10sec.Enabled = false;
				m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeManual;
			}
		}

		private void radAutoGraphic_CheckedChanged(object sender, System.EventArgs e)
		{
			//Automatically commit and save as ink graphic
			if (radAutoGraphic.Checked)
			{
				tbrAutoComplete.Enabled = true;
				lblAutoComplete.Enabled = true;
				lbl1sec.Enabled = true;
				lbl10sec.Enabled = true;
				m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeAutoGraphic;
				m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value;

			}
		}

		private void radAutoText_CheckedChanged(object sender, System.EventArgs e)
		{
            //Automatically commit and recognize as ink text
			//This is only available on a Tablet PC
			if (radAutoText.Checked)
			{            
				tbrAutoComplete.Enabled = true;
				lblAutoComplete.Enabled = true;
				lbl1sec.Enabled = true;
				lbl10sec.Enabled = true;
				m_EngineInkEnvironment.ToolCommitType = esriEngineInkToolCommitType.esriEngineInkToolCommitTypeAutoText;
				m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value;
			}
		}

        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            //Report to the user the number of Ink Sketches that are present

            IElement pElement;
            IGraphicsContainer pContainer;
            int i = 0;

            m_pMap = axMapControl1.Map;
            pContainer = (IGraphicsContainer)m_pMap;
            pContainer.Reset();
            pElement = pContainer.Next();

            while (pElement != null)
            {
                if (pElement is InkGraphic)
                    i = i + 1;
                pElement = pContainer.Next();
            }
            tbxNumber.Text = i.ToString();	
        }

        private void tbrAutoComplete_MouseUp(object sender, MouseEventArgs e)
        {
            //Set the ToolCommitDelay using the value of the TrackBar
            m_EngineInkEnvironment.ToolCommitDelay = tbrAutoComplete.Value;
        }

	}

	public class Win32 
	{
		[DllImport("user32.dll",EntryPoint="GetSystemMetrics")]
		public static extern int GetSystemMetrics(int abc);
	}
}
