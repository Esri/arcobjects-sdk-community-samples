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
using System.Runtime.InteropServices;


namespace VisualizeCameraPath
{
	/// <summary>
	/// Summary description for VisualizeCameraPathForm.
	/// </summary>
	public class VisualizeCameraPathForm : System.Windows.Forms.Form
	{		
		#region Member Variables
		
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label3;
		public System.Windows.Forms.Button playButton;
		public System.Windows.Forms.Button generatePathButton;
		public System.Windows.Forms.Button stopButton;
		public System.Windows.Forms.ListBox animTracksListBox;
		public System.Windows.Forms.CheckBox generateCamPathCheckBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		public System.Windows.Forms.RadioButton ptsPerSecRadioButton;
		public System.Windows.Forms.RadioButton ptsBtwnKframeRadioButton;
		public System.Windows.Forms.TextBox numPtsPerSecTextBox;
		public System.Windows.Forms.TextBox ptsBtwnKframeTextBox;
		public System.Windows.Forms.TextBox animDurationTextBox;
		public System.Windows.Forms.CheckBox camToTargetDirectionCheckBox;
		public System.Windows.Forms.ListBox symbolTypeListBox;
		private System.ComponentModel.IContainer components = null;
		#endregion
		
		#region Constructor/Dispose

		public VisualizeCameraPathForm()
		{
			InitializeComponent();
			//load symbol types
			loadSymbolTypes();			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.generateCamPathCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.animDurationTextBox = new System.Windows.Forms.TextBox();
			this.animTracksListBox = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.stopButton = new System.Windows.Forms.Button();
			this.playButton = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.generatePathButton = new System.Windows.Forms.Button();
			this.ptsPerSecRadioButton = new System.Windows.Forms.RadioButton();
			this.numPtsPerSecTextBox = new System.Windows.Forms.TextBox();
			this.ptsBtwnKframeRadioButton = new System.Windows.Forms.RadioButton();
			this.ptsBtwnKframeTextBox = new System.Windows.Forms.TextBox();
			this.camToTargetDirectionCheckBox = new System.Windows.Forms.CheckBox();
			this.symbolTypeListBox = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select Camera Track:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.generateCamPathCheckBox);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 344);
			this.panel1.TabIndex = 1;
			// 
			// generateCamPathCheckBox
			// 
			this.generateCamPathCheckBox.Location = new System.Drawing.Point(16, 136);
			this.generateCamPathCheckBox.Name = "generateCamPathCheckBox";
			this.generateCamPathCheckBox.Size = new System.Drawing.Size(176, 16);
			this.generateCamPathCheckBox.TabIndex = 3;
			this.generateCamPathCheckBox.Text = "Generate Camera path";
			this.generateCamPathCheckBox.CheckedChanged += new System.EventHandler(this.generateCamPathCheckBox_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.animDurationTextBox);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.animTracksListBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.stopButton);
			this.groupBox2.Controls.Add(this.playButton);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(312, 120);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			// 
			// animDurationTextBox
			// 
			this.animDurationTextBox.Location = new System.Drawing.Point(168, 56);
			this.animDurationTextBox.Name = "animDurationTextBox";
			this.animDurationTextBox.Size = new System.Drawing.Size(136, 20);
			this.animDurationTextBox.TabIndex = 6;
			this.animDurationTextBox.Text = "10";
			// 
			// animTracksListBox
			// 
			this.animTracksListBox.Location = new System.Drawing.Point(168, 16);
			this.animTracksListBox.Name = "animTracksListBox";
			this.animTracksListBox.Size = new System.Drawing.Size(136, 30);
			this.animTracksListBox.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Animation Duration (sec):";
			// 
			// stopButton
			// 
			this.stopButton.Enabled = false;
			this.stopButton.Location = new System.Drawing.Point(168, 88);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(88, 23);
			this.stopButton.TabIndex = 7;
			this.stopButton.Text = "Stop";
			// 
			// playButton
			// 
			this.playButton.Location = new System.Drawing.Point(48, 88);
			this.playButton.Name = "playButton";
			this.playButton.Size = new System.Drawing.Size(88, 23);
			this.playButton.TabIndex = 2;
			this.playButton.Text = "Play";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.generatePathButton);
			this.groupBox3.Controls.Add(this.ptsPerSecRadioButton);
			this.groupBox3.Controls.Add(this.numPtsPerSecTextBox);
			this.groupBox3.Controls.Add(this.ptsBtwnKframeRadioButton);
			this.groupBox3.Controls.Add(this.ptsBtwnKframeTextBox);
			this.groupBox3.Controls.Add(this.camToTargetDirectionCheckBox);
			this.groupBox3.Controls.Add(this.symbolTypeListBox);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Enabled = false;
			this.groupBox3.Location = new System.Drawing.Point(8, 160);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(312, 176);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Camera path options";
			// 
			// generatePathButton
			// 
			this.generatePathButton.Enabled = false;
			this.generatePathButton.Location = new System.Drawing.Point(112, 144);
			this.generatePathButton.Name = "generatePathButton";
			this.generatePathButton.Size = new System.Drawing.Size(88, 23);
			this.generatePathButton.TabIndex = 8;
			this.generatePathButton.Text = "Generate Path";
			// 
			// ptsPerSecRadioButton
			// 
			this.ptsPerSecRadioButton.Checked = true;
			this.ptsPerSecRadioButton.Location = new System.Drawing.Point(16, 24);
			this.ptsPerSecRadioButton.Name = "ptsPerSecRadioButton";
			this.ptsPerSecRadioButton.Size = new System.Drawing.Size(176, 16);
			this.ptsPerSecRadioButton.TabIndex = 7;
			this.ptsPerSecRadioButton.TabStop = true;
			this.ptsPerSecRadioButton.Text = "Points per second :";
			// 
			// numPtsPerSecTextBox
			// 
			this.numPtsPerSecTextBox.Location = new System.Drawing.Point(232, 16);
			this.numPtsPerSecTextBox.Name = "numPtsPerSecTextBox";
			this.numPtsPerSecTextBox.Size = new System.Drawing.Size(72, 20);
			this.numPtsPerSecTextBox.TabIndex = 6;
			this.numPtsPerSecTextBox.Text = "";
			// 
			// ptsBtwnKframeRadioButton
			// 
			this.ptsBtwnKframeRadioButton.Location = new System.Drawing.Point(16, 48);
			this.ptsBtwnKframeRadioButton.Name = "ptsBtwnKframeRadioButton";
			this.ptsBtwnKframeRadioButton.Size = new System.Drawing.Size(208, 16);
			this.ptsBtwnKframeRadioButton.TabIndex = 8;
			this.ptsBtwnKframeRadioButton.Text = "Points between keyframe positions :";
			// 
			// ptsBtwnKframeTextBox
			// 
			this.ptsBtwnKframeTextBox.Location = new System.Drawing.Point(232, 48);
			this.ptsBtwnKframeTextBox.Name = "ptsBtwnKframeTextBox";
			this.ptsBtwnKframeTextBox.Size = new System.Drawing.Size(72, 20);
			this.ptsBtwnKframeTextBox.TabIndex = 9;
			this.ptsBtwnKframeTextBox.Text = "";
			// 
			// camToTargetDirectionCheckBox
			// 
			this.camToTargetDirectionCheckBox.Location = new System.Drawing.Point(16, 120);
			this.camToTargetDirectionCheckBox.Name = "camToTargetDirectionCheckBox";
			this.camToTargetDirectionCheckBox.Size = new System.Drawing.Size(160, 16);
			this.camToTargetDirectionCheckBox.TabIndex = 4;
			this.camToTargetDirectionCheckBox.Text = "Camera to Target direction";
			// 
			// symbolTypeListBox
			// 
			this.symbolTypeListBox.Items.AddRange(new object[] {
																   "Cone",
																   "Sphere",
																   "Tetrahedron",
																   "Diamond",
																   "Cylinder",
																   "Cube"});
			this.symbolTypeListBox.Location = new System.Drawing.Point(168, 80);
			this.symbolTypeListBox.Name = "symbolTypeListBox";
			this.symbolTypeListBox.Size = new System.Drawing.Size(136, 30);
			this.symbolTypeListBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Select Symbol Type:";
			// 
			// VisualizeCameraPathForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 342);
			this.Controls.Add(this.panel1);
			this.Name = "VisualizeCameraPathForm";
			this.Text = "Trace Camera Path";
			this.TopMost = true;
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Custom Functions/Event Handlers

		private void generateCamPathCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if(generateCamPathCheckBox.Checked==true) groupBox3.Enabled=true;
			else if(generateCamPathCheckBox.Checked==false) groupBox3.Enabled=false;
		}

		public void loadSymbolTypes()
		{
			//first clear collection and then load
			symbolTypeListBox.Items.Clear();
			symbolTypeListBox.Items.Add("Cone");
			symbolTypeListBox.Items.Add("Sphere");
			symbolTypeListBox.Items.Add("Tetrahedron");
			symbolTypeListBox.Items.Add("Diamond");
			symbolTypeListBox.Items.Add("Cylinder");
			symbolTypeListBox.Items.Add("Cube");
		}

		#endregion
		
	}
}
