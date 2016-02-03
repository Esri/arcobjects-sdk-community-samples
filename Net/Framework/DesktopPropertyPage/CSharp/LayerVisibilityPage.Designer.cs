namespace DesktopPropertyPageCS
{
    partial class LayerVisibilityPage
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
            this.radioButtonShow = new System.Windows.Forms.RadioButton();
            this.radioButtonHide = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonShow
            // 
            this.radioButtonShow.AutoSize = true;
            this.radioButtonShow.Location = new System.Drawing.Point(38, 37);
            this.radioButtonShow.Name = "radioButtonShow";
            this.radioButtonShow.Size = new System.Drawing.Size(55, 17);
            this.radioButtonShow.TabIndex = 0;
            this.radioButtonShow.TabStop = true;
            this.radioButtonShow.Text = "Visible";
            this.radioButtonShow.UseVisualStyleBackColor = true;
            this.radioButtonShow.CheckedChanged += new System.EventHandler(this.radioButtonShow_CheckedChanged);
            // 
            // radioButtonHide
            // 
            this.radioButtonHide.AutoSize = true;
            this.radioButtonHide.Location = new System.Drawing.Point(38, 72);
            this.radioButtonHide.Name = "radioButtonHide";
            this.radioButtonHide.Size = new System.Drawing.Size(63, 17);
            this.radioButtonHide.TabIndex = 1;
            this.radioButtonHide.TabStop = true;
            this.radioButtonHide.Text = "Invisible";
            this.radioButtonHide.UseVisualStyleBackColor = true;
            // 
            // LayerVisibilityPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonHide);
            this.Controls.Add(this.radioButtonShow);
            this.Name = "LayerVisibilityPage";
            this.Size = new System.Drawing.Size(305, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonShow;
        private System.Windows.Forms.RadioButton radioButtonHide;




    }
}
