namespace ExtentView_CS
{
    partial class FrmExtentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExtentView));
            this.AxMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.AxMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // AxMapControl1
            // 
            this.AxMapControl1.Location = new System.Drawing.Point(7, 9);
            this.AxMapControl1.Name = "AxMapControl1";
            this.AxMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxMapControl1.OcxState")));
            this.AxMapControl1.Size = new System.Drawing.Size(372, 328);
            this.AxMapControl1.TabIndex = 0;
            // 
            // FrmExtentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 341);
            this.Controls.Add(this.AxMapControl1);
            this.Name = "FrmExtentView";
            this.Text = "FrmExtentView";
            ((System.ComponentModel.ISupportInitialize)(this.AxMapControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public ESRI.ArcGIS.Controls.AxMapControl AxMapControl1;
    }
}