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
namespace SimpleLogWindowCS
{
    partial class LoggingDockableWindow
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
            this.lstPendingMessage = new System.Windows.Forms.ListBox();
            this.ctxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxCtxMenu = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radDotNetCtx = new System.Windows.Forms.RadioButton();
            this.radFramework = new System.Windows.Forms.RadioButton();
            this.radDynamic = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.ctxMenuStrip.SuspendLayout();
            this.grpBoxCtxMenu.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPendingMessage
            // 
            this.lstPendingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPendingMessage.ContextMenuStrip = this.ctxMenuStrip;
            this.lstPendingMessage.HorizontalScrollbar = true;
            this.lstPendingMessage.IntegralHeight = false;
            this.lstPendingMessage.Location = new System.Drawing.Point(0, 22);
            this.lstPendingMessage.Name = "lstPendingMessage";
            this.lstPendingMessage.Size = new System.Drawing.Size(338, 78);
            this.lstPendingMessage.TabIndex = 0;
            // 
            // ctxMenuStrip
            // 
            this.ctxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.clearToolStripMenuItem});
            this.ctxMenuStrip.Name = "ctxMenuStrip";
            this.ctxMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuStrip.ShowItemToolTips = false;
            this.ctxMenuStrip.Size = new System.Drawing.Size(100, 32);
            this.ctxMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuStrip_Opening);
            this.ctxMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMenuStrip_ItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // grpBoxCtxMenu
            // 
            this.grpBoxCtxMenu.Controls.Add(this.flowLayoutPanel1);
            this.grpBoxCtxMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxCtxMenu.Location = new System.Drawing.Point(0, 0);
            this.grpBoxCtxMenu.Name = "grpBoxCtxMenu";
            this.grpBoxCtxMenu.Size = new System.Drawing.Size(338, 75);
            this.grpBoxCtxMenu.TabIndex = 2;
            this.grpBoxCtxMenu.TabStop = false;
            this.grpBoxCtxMenu.Text = "Context Menu Option";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.radDotNetCtx);
            this.flowLayoutPanel1.Controls.Add(this.radFramework);
            this.flowLayoutPanel1.Controls.Add(this.radDynamic);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(332, 56);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // radDotNetCtx
            // 
            this.radDotNetCtx.AutoSize = true;
            this.radDotNetCtx.Checked = true;
            this.radDotNetCtx.Location = new System.Drawing.Point(3, 3);
            this.radDotNetCtx.Name = "radDotNetCtx";
            this.radDotNetCtx.Size = new System.Drawing.Size(149, 17);
            this.radDotNetCtx.TabIndex = 0;
            this.radDotNetCtx.TabStop = true;
            this.radDotNetCtx.Text = "Pure .Net (Windows Form)";
            this.toolTip1.SetToolTip(this.radDotNetCtx, "Pure .Net Windows Form context menu (ContextMenuStrip Class)");
            this.radDotNetCtx.UseVisualStyleBackColor = true;
            this.radDotNetCtx.CheckedChanged += new System.EventHandler(this.radCtxMenu_CheckedChanged);
            // 
            // radFramework
            // 
            this.radFramework.AutoSize = true;
            this.radFramework.Location = new System.Drawing.Point(158, 3);
            this.radFramework.Name = "radFramework";
            this.radFramework.Size = new System.Drawing.Size(140, 17);
            this.radFramework.TabIndex = 1;
            this.radFramework.TabStop = true;
            this.radFramework.Text = "Pre-defined (ArcObjects)";
            this.toolTip1.SetToolTip(this.radFramework, "Use a predefined context menu registered in ArcGIS framework");
            this.radFramework.UseVisualStyleBackColor = true;
            this.radFramework.CheckedChanged += new System.EventHandler(this.radCtxMenu_CheckedChanged);
            // 
            // radDynamic
            // 
            this.radDynamic.AutoSize = true;
            this.radDynamic.Location = new System.Drawing.Point(3, 26);
            this.radDynamic.Name = "radDynamic";
            this.radDynamic.Size = new System.Drawing.Size(127, 17);
            this.radDynamic.TabIndex = 2;
            this.radDynamic.TabStop = true;
            this.radDynamic.Text = "Dynamic (ArcObjects)";
            this.toolTip1.SetToolTip(this.radDynamic, "Use a temporary context menu created at runtime");
            this.radDynamic.UseVisualStyleBackColor = true;
            this.radDynamic.CheckedChanged += new System.EventHandler(this.radCtxMenu_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblMsg);
            this.splitContainer1.Panel1.Controls.Add(this.txtInput);
            this.splitContainer1.Panel1.Controls.Add(this.lstPendingMessage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpBoxCtxMenu);
            this.splitContainer1.Size = new System.Drawing.Size(338, 179);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(1, 3);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(58, 13);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "Input Text:";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(82, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(256, 20);
            this.txtInput.TabIndex = 3;
            this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyUp);
            // 
            // LoggingDockableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LoggingDockableWindow";
            this.Size = new System.Drawing.Size(338, 179);
            this.ctxMenuStrip.ResumeLayout(false);
            this.grpBoxCtxMenu.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPendingMessage;
        private System.Windows.Forms.GroupBox grpBoxCtxMenu;
        private System.Windows.Forms.RadioButton radFramework;
        private System.Windows.Forms.RadioButton radDotNetCtx;
        private System.Windows.Forms.ContextMenuStrip ctxMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radDynamic;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Label lblMsg;
        internal System.Windows.Forms.TextBox txtInput;

    }
}
