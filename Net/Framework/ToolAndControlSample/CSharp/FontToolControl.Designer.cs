namespace ToolAndControlSampleCS
{
    partial class FontToolControl
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
            if (m_hBitmap.ToInt32() != 0)
            {
                DeleteObject(m_hBitmap);
                m_hBitmap = System.IntPtr.Zero;
            }
            if (m_ifc != null)
            {
                m_ifc.Dispose();
                m_ifc = null;
            }

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
            this.cboFont = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboFont
            // 
            this.cboFont.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFont.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboFont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFont.FormattingEnabled = true;
            this.cboFont.Location = new System.Drawing.Point(0, 0);
            this.cboFont.Name = "cboFont";
            this.cboFont.Size = new System.Drawing.Size(200, 21);
            this.cboFont.TabIndex = 1;
            this.cboFont.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboFont_DrawItem);
            this.cboFont.DropDownClosed += new System.EventHandler(this.cboFont_DropDownClosed);
            // 
            // FontToolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboFont);
            this.Name = "FontToolControl";
            this.Size = new System.Drawing.Size(200, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFont;
    }
}
