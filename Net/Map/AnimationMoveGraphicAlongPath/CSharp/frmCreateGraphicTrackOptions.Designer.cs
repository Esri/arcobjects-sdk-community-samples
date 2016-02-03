namespace AnimationDeveloperSamples
{
    partial class frmCreateGraphicTrackOptions
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBoxTrackName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxOverwriteTrack = new System.Windows.Forms.CheckBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxReverseOrder = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTracePath = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonLineGraphic = new System.Windows.Forms.RadioButton();
            this.radioButtonLineFeature = new System.Windows.Forms.RadioButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Simplification factor:";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(118, 114);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(174, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TabStop = false;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // textBoxTrackName
            // 
            this.textBoxTrackName.Location = new System.Drawing.Point(83, 173);
            this.textBoxTrackName.Name = "textBoxTrackName";
            this.textBoxTrackName.Size = new System.Drawing.Size(184, 20);
            this.textBoxTrackName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Track name:";
            // 
            // checkBoxOverwriteTrack
            // 
            this.checkBoxOverwriteTrack.AutoSize = true;
            this.checkBoxOverwriteTrack.Location = new System.Drawing.Point(13, 201);
            this.checkBoxOverwriteTrack.Name = "checkBoxOverwriteTrack";
            this.checkBoxOverwriteTrack.Size = new System.Drawing.Size(187, 17);
            this.checkBoxOverwriteTrack.TabIndex = 6;
            this.checkBoxOverwriteTrack.Text = "Overwrite tracks of the same name";
            this.checkBoxOverwriteTrack.UseVisualStyleBackColor = true;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(142, 230);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 7;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(223, 230);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxReverseOrder
            // 
            this.checkBoxReverseOrder.AutoSize = true;
            this.checkBoxReverseOrder.Location = new System.Drawing.Point(6, 65);
            this.checkBoxReverseOrder.Name = "checkBoxReverseOrder";
            this.checkBoxReverseOrder.Size = new System.Drawing.Size(128, 17);
            this.checkBoxReverseOrder.TabIndex = 9;
            this.checkBoxReverseOrder.Text = "Apply in reverse order";
            this.checkBoxReverseOrder.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTracePath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioButtonLineGraphic);
            this.groupBox1.Controls.Add(this.radioButtonLineFeature);
            this.groupBox1.Controls.Add(this.checkBoxReverseOrder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 162);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Path source";
            // 
            // checkBoxTracePath
            // 
            this.checkBoxTracePath.AutoSize = true;
            this.checkBoxTracePath.Location = new System.Drawing.Point(6, 88);
            this.checkBoxTracePath.Name = "checkBoxTracePath";
            this.checkBoxTracePath.Size = new System.Drawing.Size(78, 17);
            this.checkBoxTracePath.TabIndex = 14;
            this.checkBoxTracePath.Text = "Trace path";
            this.checkBoxTracePath.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "High";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Low";
            // 
            // radioButtonLineGraphic
            // 
            this.radioButtonLineGraphic.AutoSize = true;
            this.radioButtonLineGraphic.Enabled = false;
            this.radioButtonLineGraphic.Location = new System.Drawing.Point(7, 42);
            this.radioButtonLineGraphic.Name = "radioButtonLineGraphic";
            this.helpProvider1.SetShowHelp(this.radioButtonLineGraphic, true);
            this.radioButtonLineGraphic.Size = new System.Drawing.Size(124, 17);
            this.radioButtonLineGraphic.TabIndex = 11;
            this.radioButtonLineGraphic.TabStop = true;
            this.radioButtonLineGraphic.Text = "Selected line graphic";
            this.radioButtonLineGraphic.UseVisualStyleBackColor = true;
            // 
            // radioButtonLineFeature
            // 
            this.radioButtonLineFeature.AutoSize = true;
            this.radioButtonLineFeature.Enabled = false;
            this.radioButtonLineFeature.Location = new System.Drawing.Point(7, 19);
            this.radioButtonLineFeature.Name = "radioButtonLineFeature";
            this.helpProvider1.SetShowHelp(this.radioButtonLineFeature, true);
            this.radioButtonLineFeature.Size = new System.Drawing.Size(122, 17);
            this.radioButtonLineFeature.TabIndex = 10;
            this.radioButtonLineFeature.TabStop = true;
            this.radioButtonLineFeature.Text = "Selected line feature";
            this.radioButtonLineFeature.UseVisualStyleBackColor = true;
            // 
            // frmCreateGraphicTrackOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 265);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.checkBoxOverwriteTrack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTrackName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateGraphicTrackOptions";
            this.Text = "Move Graphic along Path ";
            this.Load += new System.EventHandler(this.frmCreateGraphicTrackOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBoxTrackName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxOverwriteTrack;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxReverseOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonLineGraphic;
        private System.Windows.Forms.RadioButton radioButtonLineFeature;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxTracePath;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}