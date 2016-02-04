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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.GlobeCore;
using System;
using System.Windows.Forms;
using System.Security.Permissions;
using ESRI.ArcGIS;


namespace GlobeCntrlAnimation
{
	public class Form1 : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label lblStatus;
		public System.Windows.Forms.GroupBox FrmAnim;
		public System.Windows.Forms.Button CmdPlay;
		public System.Windows.Forms.TextBox txtDuration;
		public System.Windows.Forms.TextBox TxtFrequency;
		public System.Windows.Forms.RadioButton OptDuration;
		public System.Windows.Forms.RadioButton OptIteration;
		public System.Windows.Forms.Button CmdLoad;
		public System.Windows.Forms.Label Label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.ComponentModel.Container components = null;
		private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
		private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
		private ESRI.ArcGIS.Controls.AxGlobeControl axGlobeControl1;
		private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
		private string m_AnimFilePath;

		public Form1()
		{
			InitializeComponent();
		}


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.lblStatus = new System.Windows.Forms.Label();
			this.FrmAnim = new System.Windows.Forms.GroupBox();
			this.CmdPlay = new System.Windows.Forms.Button();
			this.txtDuration = new System.Windows.Forms.TextBox();
			this.TxtFrequency = new System.Windows.Forms.TextBox();
			this.OptDuration = new System.Windows.Forms.RadioButton();
			this.OptIteration = new System.Windows.Forms.RadioButton();
			this.CmdLoad = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
			this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
			this.axGlobeControl1 = new ESRI.ArcGIS.Controls.AxGlobeControl();
			this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
			this.FrmAnim.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
			this.lblStatus.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblStatus.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblStatus.Location = new System.Drawing.Point(8, 376);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblStatus.Size = new System.Drawing.Size(129, 54);
			this.lblStatus.TabIndex = 13;
			// 
			// FrmAnim
			// 
			this.FrmAnim.BackColor = System.Drawing.SystemColors.Control;
			this.FrmAnim.Controls.Add(this.CmdPlay);
			this.FrmAnim.Controls.Add(this.txtDuration);
			this.FrmAnim.Controls.Add(this.TxtFrequency);
			this.FrmAnim.Controls.Add(this.OptDuration);
			this.FrmAnim.Controls.Add(this.OptIteration);
			this.FrmAnim.Controls.Add(this.CmdLoad);
			this.FrmAnim.Controls.Add(this.Label1);
			this.FrmAnim.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FrmAnim.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FrmAnim.Location = new System.Drawing.Point(144, 376);
			this.FrmAnim.Name = "FrmAnim";
			this.FrmAnim.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.FrmAnim.Size = new System.Drawing.Size(461, 55);
			this.FrmAnim.TabIndex = 12;
			this.FrmAnim.TabStop = false;
			// 
			// CmdPlay
			// 
			this.CmdPlay.BackColor = System.Drawing.SystemColors.Control;
			this.CmdPlay.Cursor = System.Windows.Forms.Cursors.Default;
			this.CmdPlay.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CmdPlay.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CmdPlay.Location = new System.Drawing.Point(266, 10);
			this.CmdPlay.Name = "CmdPlay";
			this.CmdPlay.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CmdPlay.Size = new System.Drawing.Size(81, 38);
			this.CmdPlay.TabIndex = 9;
			this.CmdPlay.Text = "Play Animation";
			this.CmdPlay.Click += new System.EventHandler(this.CmdPlay_Click);
			// 
			// txtDuration
			// 
			this.txtDuration.AcceptsReturn = true;
			this.txtDuration.AutoSize = false;
			this.txtDuration.BackColor = System.Drawing.SystemColors.Window;
			this.txtDuration.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDuration.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDuration.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDuration.Location = new System.Drawing.Point(220, 13);
			this.txtDuration.MaxLength = 0;
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDuration.Size = new System.Drawing.Size(40, 19);
			this.txtDuration.TabIndex = 8;
			this.txtDuration.Text = "10";
			// 
			// TxtFrequency
			// 
			this.TxtFrequency.AcceptsReturn = true;
			this.TxtFrequency.AutoSize = false;
			this.TxtFrequency.BackColor = System.Drawing.SystemColors.Window;
			this.TxtFrequency.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.TxtFrequency.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TxtFrequency.ForeColor = System.Drawing.SystemColors.WindowText;
			this.TxtFrequency.Location = new System.Drawing.Point(400, 13);
			this.TxtFrequency.MaxLength = 0;
			this.TxtFrequency.Name = "TxtFrequency";
			this.TxtFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.TxtFrequency.Size = new System.Drawing.Size(40, 19);
			this.TxtFrequency.TabIndex = 7;
			this.TxtFrequency.Text = "2";
			// 
			// OptDuration
			// 
			this.OptDuration.BackColor = System.Drawing.SystemColors.Control;
			this.OptDuration.Checked = true;
			this.OptDuration.Cursor = System.Windows.Forms.Cursors.Default;
			this.OptDuration.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OptDuration.ForeColor = System.Drawing.SystemColors.ControlText;
			this.OptDuration.Location = new System.Drawing.Point(122, 8);
			this.OptDuration.Name = "OptDuration";
			this.OptDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.OptDuration.Size = new System.Drawing.Size(98, 17);
			this.OptDuration.TabIndex = 6;
			this.OptDuration.TabStop = true;
			this.OptDuration.Text = "Duration (secs)";
			this.OptDuration.CheckedChanged += new System.EventHandler(this.OptDuration_CheckedChanged);
			// 
			// OptIteration
			// 
			this.OptIteration.BackColor = System.Drawing.SystemColors.Control;
			this.OptIteration.Cursor = System.Windows.Forms.Cursors.Default;
			this.OptIteration.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OptIteration.ForeColor = System.Drawing.SystemColors.ControlText;
			this.OptIteration.Location = new System.Drawing.Point(122, 22);
			this.OptIteration.Name = "OptIteration";
			this.OptIteration.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.OptIteration.Size = new System.Drawing.Size(94, 19);
			this.OptIteration.TabIndex = 5;
			this.OptIteration.TabStop = true;
			this.OptIteration.Text = "No. Iterations";
			this.OptIteration.CheckedChanged += new System.EventHandler(this.OptIteration_CheckedChanged);
			// 
			// CmdLoad
			// 
			this.CmdLoad.BackColor = System.Drawing.SystemColors.Control;
			this.CmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
			this.CmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CmdLoad.Location = new System.Drawing.Point(9, 10);
			this.CmdLoad.Name = "CmdLoad";
			this.CmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CmdLoad.Size = new System.Drawing.Size(94, 38);
			this.CmdLoad.TabIndex = 4;
			this.CmdLoad.Text = "Load Animation  File (*.aga)";
			this.CmdLoad.Click += new System.EventHandler(this.CmdLoad_Click);
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(354, 14);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(36, 17);
			this.Label1.TabIndex = 10;
			this.Label1.Text = "Cycles:";
			// 
			// axToolbarControl1
			// 
			this.axToolbarControl1.Location = new System.Drawing.Point(8, 8);
			this.axToolbarControl1.Name = "axToolbarControl1";
			this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
			this.axToolbarControl1.Size = new System.Drawing.Size(592, 28);
			this.axToolbarControl1.TabIndex = 14;
			this.axToolbarControl1.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axToolbarControl1_OnItemClick);
			// 
			// axTOCControl1
			// 
			this.axTOCControl1.Location = new System.Drawing.Point(8, 40);
			this.axTOCControl1.Name = "axTOCControl1";
			this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
			this.axTOCControl1.Size = new System.Drawing.Size(136, 328);
			this.axTOCControl1.TabIndex = 15;
			// 
			// axGlobeControl1
			// 
			this.axGlobeControl1.Location = new System.Drawing.Point(144, 40);
			this.axGlobeControl1.Name = "axGlobeControl1";
			this.axGlobeControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGlobeControl1.OcxState")));
			this.axGlobeControl1.Size = new System.Drawing.Size(456, 328);
			this.axGlobeControl1.TabIndex = 16;
			// 
			// axLicenseControl1
			// 
			this.axLicenseControl1.Enabled = true;
			this.axLicenseControl1.Location = new System.Drawing.Point(384, 56);
			this.axLicenseControl1.Name = "axLicenseControl1";
			this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
			this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
			this.axLicenseControl1.TabIndex = 17;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 438);
			this.Controls.Add(this.axLicenseControl1);
			this.Controls.Add(this.axGlobeControl1);
			this.Controls.Add(this.axTOCControl1);
			this.Controls.Add(this.axToolbarControl1);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.FrmAnim);
			this.Name = "Form1";
			this.Text = "GlobeControlAnimation";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.FrmAnim.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axGlobeControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
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
			//check and load if the animation file is present...
			m_AnimFilePath = @"..\..\..\..\..\Data\ArcGlobeAnimation\AnimationSample.aga";
			if (System.IO.File.Exists(m_AnimFilePath))
			{
				//Load the sample animation file into the animation file into the doc...
				IGlobe globe = axGlobeControl1.Globe;
				IBasicScene basicScene = (IBasicScene) globe;
				basicScene.LoadAnimation(m_AnimFilePath);
			}
			else
			{
				//Disable Animation Player controls...
				OptDuration.Enabled = false;
				OptIteration.Enabled = false;
				txtDuration.Enabled = false;
				TxtFrequency.Enabled = false;
				TxtFrequency.Enabled = false;
				CmdPlay.Enabled = false;
			}
			Icon = null;
		}


		private void OptIteration_CheckedChanged(object sender, System.EventArgs e)
		{
			if (OptIteration.Checked == true)
			{
				//Set Default cycle and iteration..
				txtDuration.Text = "500";
				TxtFrequency.Text = "2";
			}
		}

		private void OptDuration_CheckedChanged(object sender, System.EventArgs e)
		{
			if (OptDuration.Checked == true)
			{
				//Set Default cycle and iteration..
				txtDuration.Text = "10"; 
				TxtFrequency.Text = "2"; 
			}
		}

		private void CmdPlay_Click(object sender, System.EventArgs e)
		{
			if (OptDuration.Checked == true)
			{
				double duration = Convert.ToInt32(txtDuration.Text);
				int numCycle = Convert.ToInt32(TxtFrequency.Text);
				//play the animation via duration
				PlayAnimation(duration, numCycle);
			}
			else
			{
				int iteration = Convert.ToInt32(txtDuration.Text);
				int	cycles = Convert.ToInt32(TxtFrequency.Text);
				//play animation via iteration...
				PlayAnimationFast(cycles, iteration);
			}
		}

		private void CmdLoad_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Title = "Open ArcGlobe Animation File";
			openFileDialog1.Filter = "ArcGlobe Animation Files (*.aga)|*.aga";
			//set ArcGlobeAnimaton path folder as default path...
			if (m_AnimFilePath != "")
			{
				openFileDialog1.InitialDirectory = m_AnimFilePath;
			}
			else
			{
				openFileDialog1.InitialDirectory = Application.StartupPath;
			}
			openFileDialog1.ShowDialog();

			//if the user selected an animation file
			string animFilePath = openFileDialog1.FileName;
			if (animFilePath == "") return; 

			//sAnimFilePath
	        IGlobe globe = axGlobeControl1.Globe;
		    IBasicScene basicScene = (IBasicScene) globe;
			basicScene.LoadAnimation(animFilePath);

			//if loading of the animation succeeded, enable player controls...
			//Enable Animation Player controls...
			OptDuration.Enabled = true;
			OptIteration.Enabled = true;
			txtDuration.Enabled = true;
			TxtFrequency.Enabled = true;
			TxtFrequency.Enabled = true;
			CmdPlay.Enabled = true;
		}

		private int Greatest(ref int[] array) 
		{
			//Function to find the largest count of keyframes (in any one of the Animation tracks)
			int max=0;
			int length = array.Length;
			for (int i=0; i<length; i++)
			{
				if (max == 0)
				{
					max = array[i];
				}
				else if (array[i] > max)
				{
					max = array[i];
				}
			}
			return max;
		}

		private void PlayAnimation(double duration, int numCycles)
		{
			IGlobe globe = axGlobeControl1.Globe;
			IAnimationTracks tracks = (IAnimationTracks) globe;
			IViewers3D viewers3D = globe.GlobeDisplay;

			//exit if document doesn't contain animation..
			 string sError;
			if (tracks.TrackCount == 0)
			{
				sError = m_AnimFilePath;
				if (sError == "")
				{
					sError = "To get a Sample animation file, Developer Kit Samples need to be installed!";
					System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." + 0x000A  + sError);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." + 0x000A  + "Load " + m_AnimFilePath + @"\AnimationSample.aga for sample.");
				}
				return;
			}
			
			DateTime startTime;
			TimeSpan timeSpan;
			int j;
			double elapsedTime;

			for (int i=1; i<=numCycles; i++)
			{
				startTime = DateTime.Now;
				j = 0;
				do
				{
					timeSpan = (DateTime.Now).Subtract(startTime);
					elapsedTime = timeSpan.TotalSeconds;
					if (elapsedTime > duration) elapsedTime = duration;
					tracks.ApplyTracks(null, elapsedTime, duration);
					viewers3D.RefreshViewers();
					j = j + 1;
				}
				while (elapsedTime < duration);
			}
		}


		private void PlayAnimationFast(int cycles, int iteration)
		{
			IGlobe globe = axGlobeControl1.Globe;
			IGlobeDisplay globeDisplay = globe.GlobeDisplay;
			Scene scene = (Scene) globeDisplay.Scene;
			IAnimationTracks sceneTracks = (IAnimationTracks) scene;

			IArray trackCamArray = new ArrayClass();
			IArray trackGlbArray = new ArrayClass();
			IArray trackLyrArray = new ArrayClass();

			string sError;
			if (sceneTracks.TrackCount == 0)
			{
				sError = m_AnimFilePath;
				if (sError == "")
				{
					sError = "To get a Sample animation file, Developer Kit Samples need to be installed!";
					System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." + 0x000A  + sError);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("The current document doesn't contain animation file." + 0x000A  + "Load " + m_AnimFilePath + @"\AnimationSample.aga for sample.");
				}
				return;
			}
			
			IAnimationTrack track;
			IAnimationTrack trackLayer;
			IAnimationTrack trackGlobe = null;
			IAnimationType animType;
			IAnimationType animLayer;
			IAnimationType animGlobeCam = null;
			IKeyframe kFGlbCam;
			IKeyframe kFGlbLayer;
			int k;
			int[] count = new int[1000];

			//get each track from the scene and store tracks of the same kind in an Array
			for (int i=0; i<=sceneTracks.TrackCount-1;i++)
			{
				track = (IAnimationTrack) sceneTracks.Tracks.get_Element(i);
				k = i;
				animType = track.AnimationType;

				if (animType.CLSID.Value.ToString() == "{7CCBA704-3933-4D7A-8E89-4DFEE88AA937}")
				{
					//GlobeLayer
					trackLayer = new AnimationTrackClass();
					trackLayer = track;
					trackLayer.AnimationType = animType;
					kFGlbLayer = new GlobeLayerKeyframeClass();
					animLayer = animType;
					//Store the keyframe count of each track in an array
					count[i] = trackLayer.KeyframeCount;
					trackLyrArray.Add(trackLayer);
				}
				else if (animType.CLSID.Value.ToString() == "{D4565495-E2F9-4D89-A8A7-D0B69FD7A424}")
				{
					//Globe Camera type
					trackGlobe = new AnimationTrackClass();
					trackGlobe = track;
					trackGlobe.AnimationType = animType;
					kFGlbCam = new GlobeCameraKeyframeClass();
					animGlobeCam = animType;
					//Store the keyframe count of each track in an array
					count[i] = trackGlobe.KeyframeCount;
					trackGlbArray.Add(trackGlobe);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("Animation Type " + animType.Name + " Not Supported. Check if the animation File is Valid!");
					return;
				}
			}

			int larger = Greatest(ref count);
			//if nothing gets passed by the argument it takes the max no of keyframes
			if (iteration == 0) iteration = larger;

			IAnimationTrack trackCamera;
			IAnimationType animCam = null;
			IKeyframe kFBkmark;
			double time = 0;
			int keyFrameLayerCount; int keyFrameCameraCount; int keyFrameCount;

			for (int i=1; i<=cycles; i++) //no of cycles...
			{
				for (int start=0; start<=iteration; start++) //no of iterations...
				{
					for (int j=0; j<=trackCamArray.Count-1; j++)
					{
						trackCamera = (IAnimationTrack) trackCamArray.get_Element(j);
						if (trackCamera != null)
						{
							if (time >= trackCamera.BeginTime)
							{
								keyFrameCameraCount = trackGlobe.KeyframeCount;
								kFBkmark = trackCamera.get_Keyframe(keyFrameCameraCount - keyFrameCameraCount);
								//reset object
								animCam.ResetObject(scene, kFBkmark);
								//interpolate by using track
								trackCamera.InterpolateObjectProperties(scene, time);
								keyFrameCameraCount = keyFrameCameraCount - 1;
							}
						}
					}

					for (k=0; k<=trackGlbArray.Count-1;k++)
					{
						trackGlobe = (IAnimationTrack) trackGlbArray.get_Element(k);
						if (trackGlobe != null)
						{
							if (time >= trackGlobe.BeginTime)
							{
								keyFrameCount = trackGlobe.KeyframeCount;
								kFGlbCam = trackGlobe.get_Keyframe(trackGlobe.KeyframeCount - keyFrameCount);
								//reset object
								animGlobeCam.ResetObject(scene, kFGlbCam);
								//interpolate by using track
								trackGlobe.InterpolateObjectProperties(scene, time);
								keyFrameCount = keyFrameCount - 1;
							}
						}
					}

					for (int t=0; t<=trackLyrArray.Count-1; t++)
					{
						trackLayer = (IAnimationTrack) trackLyrArray.get_Element(t);
						if (trackLayer != null)
						{
							if (time >= trackLayer.BeginTime)
							{
								keyFrameLayerCount = trackLayer.KeyframeCount;
								kFGlbLayer = trackLayer.get_Keyframe(trackLayer.KeyframeCount - keyFrameLayerCount);
								//interpolate by using track
								trackLayer.InterpolateObjectProperties(scene, time);
								keyFrameLayerCount = keyFrameLayerCount - 1;
							}
						}
					}

					//reset interpolation Point
					time = start / iteration;
					//refresh the globeviewer(s)
					globeDisplay.RefreshViewers();
				}
			}
		}

		private string routin_ReadRegistry(string sKey) 
		{
			//Open the subkey for reading
			Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sKey,true);
			if (rk == null) return ""; 
			// Get the data from a specified item in the key.
			return (string) rk.GetValue("InstallDir");
		}

		private void axToolbarControl1_OnItemClick(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent e)
		{
			//Display help when fly tool and walk tools are selected...
			//check if key intercept is enabled,if not enable it.
			if (axGlobeControl1.KeyIntercept != 1) axGlobeControl1.KeyIntercept = 1;
			
			UID uID = axToolbarControl1.GetItem(e.index).UID;
			//uid for fly tool={2C327C42-8CA9-4FC3-8C7B-F6290680FABB}
			//uid for walk tool={56C3BDD4-C51A-4574-8954-D3E1F5F54E57}

			if (uID.Value.ToString() == "{2C327C42-8CA9-4FC3-8C7B-F6290680FABB}")
			{
				//fly...
				lblStatus.Text = "Use arrow up or arrow left keys to decelerate and arrow up or arrow left keys to accelerate.";
			}
			else if (uID.Value.ToString() == "{56C3BDD4-C51A-4574-8954-D3E1F5F54E57}")
			{
				//walk...
				lblStatus.Text = "Use arrow up or down keys to change elevation and the arrow left or right keys to fine-tune travel speed.";
			}
			else
			{
				lblStatus.Text = "";
			}
		}

	}
}
