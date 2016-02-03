namespace DynamicBiking
{
	partial class DynamicBikingSpeedCtrl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.lblMin = new System.Windows.Forms.Label();
			this.lblMaximum = new System.Windows.Forms.Label();
			this.lblSpeed = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(110, 4);
			this.trackBar1.Maximum = 20;
			this.trackBar1.Minimum = 1;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(145, 45);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.TickFrequency = 2;
			this.toolTip1.SetToolTip(this.trackBar1, "Playback speed");
			this.trackBar1.Value = 10;
			this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
			// 
			// lblMin
			// 
			this.lblMin.AutoSize = true;
			this.lblMin.Location = new System.Drawing.Point(95, 14);
			this.lblMin.Name = "lblMin";
			this.lblMin.Size = new System.Drawing.Size(20, 13);
			this.lblMin.TabIndex = 1;
			this.lblMin.Text = "X1";
			// 
			// lblMaximum
			// 
			this.lblMaximum.AutoSize = true;
			this.lblMaximum.Location = new System.Drawing.Point(250, 14);
			this.lblMaximum.Name = "lblMaximum";
			this.lblMaximum.Size = new System.Drawing.Size(26, 13);
			this.lblMaximum.TabIndex = 2;
			this.lblMaximum.Text = "X20";
			// 
			// lblSpeed
			// 
			this.lblSpeed.AutoSize = true;
			this.lblSpeed.Location = new System.Drawing.Point(2, 13);
			this.lblSpeed.Name = "lblSpeed";
			this.lblSpeed.Size = new System.Drawing.Size(85, 13);
			this.lblSpeed.TabIndex = 3;
			this.lblSpeed.Text = "playback speed:";
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 500;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ToolTipTitle = "Playback speed";
			// 
			// DynamicBikingSpeedCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblSpeed);
			this.Controls.Add(this.lblMaximum);
			this.Controls.Add(this.lblMin);
			this.Controls.Add(this.trackBar1);
			this.Name = "DynamicBikingSpeedCtrl";
			this.Size = new System.Drawing.Size(280, 41);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label lblMin;
		private System.Windows.Forms.Label lblMaximum;
		private System.Windows.Forms.Label lblSpeed;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
