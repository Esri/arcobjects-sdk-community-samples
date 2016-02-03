using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS;

namespace SceneNavigateAndBookmarks
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SceneNavigateAndBookmarks : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txtNewBookmarkName;
		public System.Windows.Forms.Button cmdCaptureBookmark;
		public System.Windows.Forms.ListBox lstBookmarks;
		public System.Windows.Forms.CheckBox chkRotate;
		public System.Windows.Forms.CheckBox chkNavigate;
		public System.Windows.Forms.Label Label5;
		public System.Windows.Forms.Label Label4;
		public System.Windows.Forms.Label Label3;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Line3;
		public System.Windows.Forms.Label Line2;
		public System.Windows.Forms.Label Line1;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Button cmdBrowse;
		public System.Windows.Forms.TextBox txtFileName;
		public System.Windows.Forms.Button cmdLoad;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private IArray m_Bookmarks = new ArrayClass();
		private ESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SceneNavigateAndBookmarks()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SceneNavigateAndBookmarks));
			this.txtNewBookmarkName = new System.Windows.Forms.TextBox();
			this.cmdCaptureBookmark = new System.Windows.Forms.Button();
			this.lstBookmarks = new System.Windows.Forms.ListBox();
			this.chkRotate = new System.Windows.Forms.CheckBox();
			this.chkNavigate = new System.Windows.Forms.CheckBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Line3 = new System.Windows.Forms.Label();
			this.Line2 = new System.Windows.Forms.Label();
			this.Line1 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.cmdLoad = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.axSceneControl1 = new ESRI.ArcGIS.Controls.AxSceneControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// txtNewBookmarkName
			// 
			this.txtNewBookmarkName.AcceptsReturn = true;
			this.txtNewBookmarkName.AutoSize = false;
			this.txtNewBookmarkName.BackColor = System.Drawing.SystemColors.Window;
			this.txtNewBookmarkName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtNewBookmarkName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtNewBookmarkName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtNewBookmarkName.Location = new System.Drawing.Point(152, 480);
			this.txtNewBookmarkName.MaxLength = 0;
			this.txtNewBookmarkName.Name = "txtNewBookmarkName";
			this.txtNewBookmarkName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtNewBookmarkName.Size = new System.Drawing.Size(121, 27);
			this.txtNewBookmarkName.TabIndex = 16;
			this.txtNewBookmarkName.Text = "New Bookmark";
			// 
			// cmdCaptureBookmark
			// 
			this.cmdCaptureBookmark.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCaptureBookmark.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdCaptureBookmark.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdCaptureBookmark.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdCaptureBookmark.Location = new System.Drawing.Point(8, 480);
			this.cmdCaptureBookmark.Name = "cmdCaptureBookmark";
			this.cmdCaptureBookmark.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdCaptureBookmark.Size = new System.Drawing.Size(137, 25);
			this.cmdCaptureBookmark.TabIndex = 15;
			this.cmdCaptureBookmark.Text = "Capture Bookmark";
			this.cmdCaptureBookmark.Click += new System.EventHandler(this.cmdCaptureBookmark_Click);
			// 
			// lstBookmarks
			// 
			this.lstBookmarks.BackColor = System.Drawing.SystemColors.Window;
			this.lstBookmarks.Cursor = System.Windows.Forms.Cursors.Default;
			this.lstBookmarks.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstBookmarks.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstBookmarks.ItemHeight = 14;
			this.lstBookmarks.Location = new System.Drawing.Point(8, 392);
			this.lstBookmarks.Name = "lstBookmarks";
			this.lstBookmarks.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstBookmarks.Size = new System.Drawing.Size(265, 74);
			this.lstBookmarks.TabIndex = 14;
			this.lstBookmarks.SelectedIndexChanged += new System.EventHandler(this.lstBookmarks_SelectedIndexChanged);
			// 
			// chkRotate
			// 
			this.chkRotate.BackColor = System.Drawing.SystemColors.Control;
			this.chkRotate.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkRotate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkRotate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkRotate.Location = new System.Drawing.Point(296, 464);
			this.chkRotate.Name = "chkRotate";
			this.chkRotate.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkRotate.Size = new System.Drawing.Size(121, 25);
			this.chkRotate.TabIndex = 18;
			this.chkRotate.Text = "Rotate Gesture";
			this.chkRotate.CheckedChanged += new System.EventHandler(this.chkRotate_CheckedChanged);
			// 
			// chkNavigate
			// 
			this.chkNavigate.BackColor = System.Drawing.SystemColors.Control;
			this.chkNavigate.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkNavigate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkNavigate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkNavigate.Location = new System.Drawing.Point(296, 384);
			this.chkNavigate.Name = "chkNavigate";
			this.chkNavigate.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.chkNavigate.Size = new System.Drawing.Size(113, 25);
			this.chkNavigate.TabIndex = 17;
			this.chkNavigate.Text = "Navigate Mode";
			this.chkNavigate.CheckedChanged += new System.EventHandler(this.chkNavigate_CheckedChanged);
			// 
			// Label5
			// 
			this.Label5.BackColor = System.Drawing.SystemColors.Control;
			this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label5.Location = new System.Drawing.Point(424, 448);
			this.Label5.Name = "Label5";
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.Size = new System.Drawing.Size(169, 65);
			this.Label5.TabIndex = 22;
			this.Label5.Text = "Hold down left mouse button, move mouse left (or right) and keep mouse moving whi" +
				"le releasing the left button. Press ESC to stop rotation.";
			// 
			// Label4
			// 
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label4.Location = new System.Drawing.Point(424, 392);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(169, 17);
			this.Label4.TabIndex = 21;
			this.Label4.Text = "Middle mouse to pan";
			// 
			// Label3
			// 
			this.Label3.BackColor = System.Drawing.SystemColors.Control;
			this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label3.Location = new System.Drawing.Point(424, 408);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(169, 17);
			this.Label3.TabIndex = 20;
			this.Label3.Text = "Right mouse to zoom in and out";
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(424, 376);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(153, 17);
			this.Label2.TabIndex = 19;
			this.Label2.Text = "Left mouse to rotate";
			// 
			// Line3
			// 
			this.Line3.BackColor = System.Drawing.SystemColors.WindowText;
			this.Line3.Location = new System.Drawing.Point(8, 360);
			this.Line3.Name = "Line3";
			this.Line3.Size = new System.Drawing.Size(584, 1);
			this.Line3.TabIndex = 23;
			// 
			// Line2
			// 
			this.Line2.BackColor = System.Drawing.SystemColors.WindowText;
			this.Line2.Location = new System.Drawing.Point(288, 440);
			this.Line2.Name = "Line2";
			this.Line2.Size = new System.Drawing.Size(304, 1);
			this.Line2.TabIndex = 24;
			// 
			// Line1
			// 
			this.Line1.BackColor = System.Drawing.SystemColors.WindowText;
			this.Line1.Location = new System.Drawing.Point(288, 360);
			this.Line1.Name = "Line1";
			this.Line1.Size = new System.Drawing.Size(1, 152);
			this.Line1.TabIndex = 25;
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(8, 368);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(265, 17);
			this.Label1.TabIndex = 26;
			this.Label1.Text = "Bookmarks: Click on name below";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdBrowse.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdBrowse.Location = new System.Drawing.Point(528, 328);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdBrowse.Size = new System.Drawing.Size(65, 25);
			this.cmdBrowse.TabIndex = 29;
			this.cmdBrowse.Text = "Browse...";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// txtFileName
			// 
			this.txtFileName.AcceptsReturn = true;
			this.txtFileName.AutoSize = false;
			this.txtFileName.BackColor = System.Drawing.SystemColors.Window;
			this.txtFileName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtFileName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtFileName.Location = new System.Drawing.Point(88, 328);
			this.txtFileName.MaxLength = 0;
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtFileName.Size = new System.Drawing.Size(433, 25);
			this.txtFileName.TabIndex = 28;
			this.txtFileName.Text = "Enter a path to a scene document to load into the SceneControl";
			// 
			// cmdLoad
			// 
			this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
			this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdLoad.Location = new System.Drawing.Point(8, 328);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdLoad.Size = new System.Drawing.Size(73, 25);
			this.cmdLoad.TabIndex = 27;
			this.cmdLoad.Text = "Load";
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// axSceneControl1
			// 
			this.axSceneControl1.Location = new System.Drawing.Point(8, 8);
			this.axSceneControl1.Name = "axSceneControl1";
			this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
			this.axSceneControl1.Size = new System.Drawing.Size(584, 312);
			this.axSceneControl1.TabIndex = 30;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(376, 24);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(200, 50);
			this.axLicenseControl1.TabIndex = 31;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 526);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axSceneControl1);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.cmdLoad);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Line3);
			this.Controls.Add(this.Line2);
			this.Controls.Add(this.Line1);
			this.Controls.Add(this.chkRotate);
			this.Controls.Add(this.chkNavigate);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.txtNewBookmarkName);
			this.Controls.Add(this.cmdCaptureBookmark);
			this.Controls.Add(this.lstBookmarks);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
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
            Application.Run(new SceneNavigateAndBookmarks());
        }

		private void Form1_Load(object sender, System.EventArgs e)
		{
			chkNavigate.Checked = axSceneControl1.Navigate;
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			// Load the specified document
			if (axSceneControl1.CheckSxFile(txtFileName.Text) == true) 
			{
				axSceneControl1.LoadSxFile(txtFileName.Text);
			}
			else 
			{
				System.Windows.Forms.MessageBox.Show("This document cannot be loaded!");
				return;
			}
			UpdateBookmarks();
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			//Open a file dialog for selecting map documents
			openFileDialog1.Title = "Scene Documents";
			openFileDialog1.DefaultExt = ".sxd";
			openFileDialog1.Filter = "Scene Documents (*.sxd)|*.sxd|Scene Templates (*.sxt)|*.sxt";
			openFileDialog1.ShowDialog();

			txtFileName.Text = openFileDialog1.FileName;

			//Try and load the filename
			cmdLoad_Click(cmdLoad, new System.EventArgs());
		}

		private void cmdCaptureBookmark_Click(object sender, System.EventArgs e)
		{
			IBookmark3D bookmark3d = new Bookmark3DClass(); 
			bookmark3d.Name = txtNewBookmarkName.Text;
			bookmark3d.Capture(axSceneControl1.Camera);
			ISceneBookmarks bookmarks = (ISceneBookmarks) axSceneControl1.Scene;
			bookmarks.AddBookmark(bookmark3d);

			UpdateBookmarks();
		}

		private void lstBookmarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Get a bookmark corresponding to list and apply it to the SceneViewer
			IBookmark3D bookmark = (IBookmark3D) m_Bookmarks.get_Element(lstBookmarks.SelectedIndex);
			//Switch to new bookmark location
			bookmark.Apply(axSceneControl1.SceneViewer, false, 0);
		}

		private void UpdateBookmarks()
		{
			//Get bookmarks from Scene
			ISceneBookmarks bookmarks = (ISceneBookmarks) axSceneControl1.Scene;
			m_Bookmarks = null;
			m_Bookmarks = bookmarks.Bookmarks;
			lstBookmarks.Items.Clear();
			bool haveBookmarks = false;

			IBookmark3D bookmark3d;
			if (m_Bookmarks != null)
			{
				//Add the bookmark names to the listbox in the same order as they are in the Scene Document
				for (int i = 0; i <= m_Bookmarks.Count - 1; i++)
				{
					bookmark3d = (IBookmark3D) m_Bookmarks.get_Element(i);
					lstBookmarks.Items.Add(bookmark3d.Name);
				}
				haveBookmarks = m_Bookmarks.Count != 0;
				lstBookmarks.Enabled = true;
			}

			if (haveBookmarks == false) 
			{
				//No bookmarks available
				lstBookmarks.Items.Add("<No Bookmarks Available>");
				lstBookmarks.Enabled = false;
			}
		}

		private void chkNavigate_CheckedChanged(object sender, System.EventArgs e)
		{
			//Enable navigation mode
			axSceneControl1.Navigate = (bool) chkNavigate.Checked;
		}

		private void chkRotate_CheckedChanged(object sender, System.EventArgs e)
		{
			//Enable rotate gesture if checked
			axSceneControl1.SceneViewer.GestureEnabled = chkRotate.Checked;
		}

	}
}
