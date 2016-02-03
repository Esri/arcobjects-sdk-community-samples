using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace MDIApplication
{
	public class ChildForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private const int WM_ENTERSIZEMOVE = 0x231;
		private ESRI.ArcGIS.Controls.AxMapControl axMapControl1; //For resizing
		private const int WM_EXITSIZEMOVE = 0x232;	//For resizing

		public ChildForm()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ChildForm));
			this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// axMapControl1
			// 
			this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.axMapControl1.Location = new System.Drawing.Point(0, 0);
			this.axMapControl1.Name = "axMapControl1";
			this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
			this.axMapControl1.Size = new System.Drawing.Size(376, 272);
			this.axMapControl1.TabIndex = 0;
			this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
			// 
			// ChildForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 270);
			this.Controls.Add(this.axMapControl1);
			this.Name = "ChildForm";
			this.ShowInTaskbar = false;
			this.Text = "MDIChild";
			this.Load += new System.EventHandler(this.ChildForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ChildForm_Load(object sender, System.EventArgs e)
		{
			//Suppress drawing while resizing
			this.SetStyle(ControlStyles.EnableNotifyMessage,true);
			axMapControl1.BorderStyle =  esriControlsBorderStyle.esriNoBorder;
		}

		protected override void OnNotifyMessage(System.Windows.Forms.Message m)
		{
			/*This method of suppressing resize drawing works by examining the windows messages 
			sent to the form. When a form starts resizing, windows sends the WM_ENTERSIZEMOVE 
			Windows(message). At this point we suppress drawing to the MapControl and 
			PageLayoutControl and draw using a "stretchy bitmap". When windows sends the 
			WM_EXITSIZEMOVE the form is released from resizing and we resume with a full 
			redraw at the new extent.

			Note in DotNet forms we can not simply use the second parameter in a Form_Load 
			event to automatically detect when a form is resized as follows:
			AxPageLayoutControl1.SuppressResizeDrawing(False, Me.Handle.ToInt32)
			This results in a System.NullException when the form closes (after layers have been 
			loaded). This is a limitation caused by .Net's particular implementation of its 
			windows message pump which conflicts with "windows subclassing" used to watch the
			forms window.*/

			base.OnNotifyMessage (m);
      
			if (m.Msg == WM_ENTERSIZEMOVE)
			{
				axMapControl1.SuppressResizeDrawing(true, 0);
			}
			else if (m.Msg == WM_EXITSIZEMOVE)
			{
				axMapControl1.SuppressResizeDrawing(false, 0);
			}
		}

		private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
		{
			//Set the forms text
			this.Text = "MDIChild (" + axMapControl1.DocumentFilename + ")";
		}

	}
}
