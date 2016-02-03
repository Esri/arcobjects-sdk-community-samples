using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;


namespace EditPropertiesDialog
{

	public class Form1 : System.Windows.Forms.Form
	{
		private IToolbarMenu m_toolbarMenuSketch;
    private IToolbarMenu m_toolbarMenuVertex;
    private ICommandPool m_CommandPool;
    private ESRI.ArcGIS.Controls.IEngineEditor m_engineEditor;
		
    private System.Windows.Forms.Button btnOptions1;
		private System.Windows.Forms.Button btnOptions2;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    private TableLayoutPanel tableLayoutPanel1;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		}

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

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnOptions1 = new System.Windows.Forms.Button();
            this.btnOptions2 = new System.Windows.Forms.Button();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOptions1
            // 
            this.btnOptions1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions1.Location = new System.Drawing.Point(876, 3);
            this.btnOptions1.Name = "btnOptions1";
            this.btnOptions1.Size = new System.Drawing.Size(90, 23);
            this.btnOptions1.TabIndex = 5;
            this.btnOptions1.Text = "Edit Options1";
            this.btnOptions1.Click += new System.EventHandler(this.btnOptions1_Click);
            // 
            // btnOptions2
            // 
            this.btnOptions2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions2.Location = new System.Drawing.Point(876, 37);
            this.btnOptions2.Name = "btnOptions2";
            this.btnOptions2.Size = new System.Drawing.Size(90, 23);
            this.btnOptions2.TabIndex = 6;
            this.btnOptions2.Text = "Edit Options2";
            this.btnOptions2.Click += new System.EventHandler(this.btnOptions2_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axToolbarControl1.Location = new System.Drawing.Point(159, 37);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.Size = new System.Drawing.Size(704, 28);
            this.axToolbarControl1.TabIndex = 7;
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axToolbarControl2.Location = new System.Drawing.Point(159, 3);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.Size = new System.Drawing.Size(704, 28);
            this.axToolbarControl2.TabIndex = 8;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.axTOCControl1.Location = new System.Drawing.Point(3, 66);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.Size = new System.Drawing.Size(150, 433);
            this.axTOCControl1.TabIndex = 9;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl1.Location = new System.Drawing.Point(159, 66);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.Size = new System.Drawing.Size(704, 433);
            this.axMapControl1.TabIndex = 10;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(3, 3);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.02493F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.97507F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.Controls.Add(this.axMapControl1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.axTOCControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnOptions1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOptions2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.axLicenseControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.axToolbarControl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.axToolbarControl1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.42466F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.57534F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 438F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(969, 502);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1188, 599);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Feature Editing";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

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
			//Set buddy controls
			axTOCControl1.SetBuddyControl(axMapControl1);
			axToolbarControl1.SetBuddyControl(axMapControl1);
			axToolbarControl2.SetBuddyControl(axMapControl1);

      //Share command pools
      m_CommandPool = new CommandPoolClass();
      axToolbarControl1.CommandPool = m_CommandPool;
      axToolbarControl2.CommandPool = m_CommandPool;
		
			//Add items to the ToolbarControl
			axToolbarControl1.AddItem("esriControls.ControlsEditingEditorMenu",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);			
			axToolbarControl1.AddItem("esriControls.ControlsEditingEditTool",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsEditingSketchTool",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsUndoCommand",0,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsRedoCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl1.AddItem("esriControls.ControlsEditingTaskToolControl",0,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsEditingAttributeCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl1.AddItem("esriControls.ControlsEditingSketchPropertiesCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl1.AddItem("esriControls.ControlsEditingCutCommand", 0, -1, true, 0, esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl1.AddItem("esriControls.ControlsEditingPasteCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl1.AddItem("esriControls.ControlsEditingCopyCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl1.AddItem("esriControls.ControlsEditingClearCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

			axToolbarControl2.AddItem("esriControls.ControlsOpenDocCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl2.AddItem("esriControls.ControlsAddDataCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
      axToolbarControl2.AddItem("esriControls.ControlsSaveAsDocCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapZoomInTool",0,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapZoomOutTool",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapPanTool",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapFullExtentCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsFullScreenCommand",0,-1,true,0,esriCommandStyles.esriCommandStyleIconOnly);
			axToolbarControl2.AddItem("esriControls.ControlsMapIdentifyTool",0,-1,false,0,esriCommandStyles.esriCommandStyleIconOnly);

			//Create popup menus
			m_toolbarMenuSketch = new ToolbarMenuClass();
      m_toolbarMenuVertex = new ToolbarMenuClass();
      m_toolbarMenuSketch.AddItem("esriControls.ControlsEditingSketchContextMenu",0,0,false,esriCommandStyles.esriCommandStyleTextOnly);
      m_toolbarMenuVertex.AddItem("esriControls.ControlsEditingVertexContextMenu",0,0,false,esriCommandStyles.esriCommandStyleTextOnly);	

      //Create an operation stack for the undo and redo commands to use
      IOperationStack operationStack = new ControlsOperationStackClass();
      axToolbarControl1.OperationStack = operationStack;
      axToolbarControl2.OperationStack = operationStack;

      //Instantiate the EngineEditor singleton
      m_engineEditor = new EngineEditorClass();

      //Create each command on the ToolbarMenu so that the Accelerator Keys are recognized. 
      //Alternatively the user must popup the menu before using the Accelerator Keys
      long itemCount = m_toolbarMenuSketch.CommandPool.Count;
      for (int i = 0; i < itemCount; i++)
      {
          ICommand pCommand = m_toolbarMenuSketch.CommandPool.get_Command(i);
          pCommand.OnCreate(axMapControl1.Object);
      }

      //Share the commandpool with the ToolbarMenu
      m_toolbarMenuSketch.CommandPool = m_CommandPool;
      m_toolbarMenuVertex.CommandPool = m_CommandPool;
		}

		private void btnOptions1_Click(object sender, System.EventArgs e)
		{
			//Disable this window
			this.Enabled = false;
			
			EditProperties editOptions1 = new EditProperties();

			//Show the options1 dialog
			editOptions1.ShowDialog();

			//Enable this window
			this.Enabled = true;
		}

		private void btnOptions2_Click(object sender, System.EventArgs e)
		{
			//Disable this window
			this.Enabled = false;
			
			EditProperties2 editOptions2 = new EditProperties2();

			//Show the options2 dialog
			editOptions2.ShowDialog();

			//Enable this window
			this.Enabled = true;
		}

    private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {

            if (e.button == 2)
            {
                //Logic to determine which popup menu to show based on the current task and current tool

                IEngineEditTask currentTask = m_engineEditor.CurrentTask;

                switch (currentTask.UniqueName)
                {
                    case "ControlToolsEditing_CreateNewFeatureTask":
                        {
                            if (((ICommand)axToolbarControl1.CurrentTool).Name == "ControlToolsEditing_Sketch")
                            {
                                m_toolbarMenuSketch.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                            }
                            else if (((ICommand)axToolbarControl1.CurrentTool).Name == "ControlToolsEditing_Edit")
                            {
                                //SetEditLocation method must be called to enable commands
                                ((IEngineEditSketch)m_engineEditor).SetEditLocation(e.x, e.y);
                                m_toolbarMenuVertex.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                            }
                            break;

                        }
                    case "ControlToolsEditing_ModifyFeatureTask":
                        {
                            //SetEditLocation method must be called to enable commands
                            ((IEngineEditSketch)m_engineEditor).SetEditLocation(e.x, e.y);
                            m_toolbarMenuVertex.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                            break;
                        }
                }
            }
        }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      ICommandPool2 pPool2;
      pPool2 = m_toolbarMenuSketch.CommandPool as ICommandPool2;
      try
      {
        pPool2.TranslateAcceleratorKey((int)e.KeyCode);
      }
      catch (Exception)
      { }
    }
	}
}
