namespace EditingUsingCustomForm
{
    partial class EditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.cmdModify = new System.Windows.Forms.Button();
            this.cmdReshape = new System.Windows.Forms.Button();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.axModifyToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axReshapeToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axBlankToolBar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axUndoRedoToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axCreateToolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axModifyToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axReshapeToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBlankToolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axUndoRedoToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCreateToolbar)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdModify
            // 
            this.cmdModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModify.Location = new System.Drawing.Point(8, 44);
            this.cmdModify.Name = "cmdModify";
            this.cmdModify.Size = new System.Drawing.Size(110, 32);
            this.cmdModify.TabIndex = 1;
            this.cmdModify.Text = "Modify";
            this.cmdModify.UseVisualStyleBackColor = true;
            this.cmdModify.Click += new System.EventHandler(this.cmdModify_Click);
            // 
            // cmdReshape
            // 
            this.cmdReshape.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReshape.Location = new System.Drawing.Point(8, 83);
            this.cmdReshape.Name = "cmdReshape";
            this.cmdReshape.Size = new System.Drawing.Size(110, 30);
            this.cmdReshape.TabIndex = 2;
            this.cmdReshape.Text = "Reshape";
            this.cmdReshape.UseVisualStyleBackColor = true;
            this.cmdReshape.Click += new System.EventHandler(this.cmdReshape_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreate.Location = new System.Drawing.Point(8, 6);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(110, 31);
            this.cmdCreate.TabIndex = 4;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.txtInfo);
            this.panel1.Controls.Add(this.cmdEdit);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.cmdCreate);
            this.panel1.Controls.Add(this.cmdReshape);
            this.panel1.Controls.Add(this.cmdModify);
            this.panel1.Location = new System.Drawing.Point(2, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 160);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(124, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "label1";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(124, 38);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(95, 30);
            this.flowLayoutPanel2.TabIndex = 14;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Location = new System.Drawing.Point(124, 67);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(99, 59);
            this.txtInfo.TabIndex = 13;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEdit.Location = new System.Drawing.Point(8, 120);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(110, 32);
            this.cmdEdit.TabIndex = 8;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(124, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(95, 33);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // axModifyToolbar
            // 
            this.axModifyToolbar.Location = new System.Drawing.Point(290, 93);
            this.axModifyToolbar.Name = "axModifyToolbar";
            this.axModifyToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axModifyToolbar.OcxState")));
            this.axModifyToolbar.Size = new System.Drawing.Size(221, 28);
            this.axModifyToolbar.TabIndex = 6;
            this.axModifyToolbar.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axModifyToolbar_OnItemClick);
            // 
            // axReshapeToolbar
            // 
            this.axReshapeToolbar.Location = new System.Drawing.Point(289, 183);
            this.axReshapeToolbar.Name = "axReshapeToolbar";
            this.axReshapeToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axReshapeToolbar.OcxState")));
            this.axReshapeToolbar.Size = new System.Drawing.Size(222, 28);
            this.axReshapeToolbar.TabIndex = 7;
            this.axReshapeToolbar.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axReshapeToolbar_OnItemClick);
            // 
            // axBlankToolBar
            // 
            this.axBlankToolBar.Location = new System.Drawing.Point(290, 55);
            this.axBlankToolBar.Name = "axBlankToolBar";
            this.axBlankToolBar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBlankToolBar.OcxState")));
            this.axBlankToolBar.Size = new System.Drawing.Size(221, 28);
            this.axBlankToolBar.TabIndex = 9;
            // 
            // axUndoRedoToolbar
            // 
            this.axUndoRedoToolbar.Location = new System.Drawing.Point(290, 127);
            this.axUndoRedoToolbar.Name = "axUndoRedoToolbar";
            this.axUndoRedoToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axUndoRedoToolbar.OcxState")));
            this.axUndoRedoToolbar.Size = new System.Drawing.Size(221, 28);
            this.axUndoRedoToolbar.TabIndex = 10;
            // 
            // axCreateToolbar
            // 
            this.axCreateToolbar.Location = new System.Drawing.Point(290, 18);
            this.axCreateToolbar.Name = "axCreateToolbar";
            this.axCreateToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCreateToolbar.OcxState")));
            this.axCreateToolbar.Size = new System.Drawing.Size(221, 28);
            this.axCreateToolbar.TabIndex = 12;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(532, 265);
            this.Controls.Add(this.axCreateToolbar);
            this.Controls.Add(this.axUndoRedoToolbar);
            this.Controls.Add(this.axBlankToolBar);
            this.Controls.Add(this.axReshapeToolbar);
            this.Controls.Add(this.axModifyToolbar);
            this.Controls.Add(this.panel1);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorForm_FormClosing);
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axModifyToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axReshapeToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBlankToolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axUndoRedoToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCreateToolbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdModify;
        private System.Windows.Forms.Button cmdReshape;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axModifyToolbar;
        private ESRI.ArcGIS.Controls.AxToolbarControl axReshapeToolbar;
        private ESRI.ArcGIS.Controls.AxToolbarControl axBlankToolBar;
        private System.Windows.Forms.TextBox txtInfo;
        private ESRI.ArcGIS.Controls.AxToolbarControl axUndoRedoToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axCreateToolbar;
    }
}