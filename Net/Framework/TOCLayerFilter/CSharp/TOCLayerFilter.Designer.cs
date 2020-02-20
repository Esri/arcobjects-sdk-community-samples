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
namespace TOCLayerFilterCS
{
    partial class TOCLayerFilter
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
            this.tvwLayer = new System.Windows.Forms.TreeView();
            this.contextMenuDummy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cboLayerType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tvwLayer
            // 
            this.tvwLayer.ContextMenuStrip = this.contextMenuDummy;
            this.tvwLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwLayer.Location = new System.Drawing.Point(0, 21);
            this.tvwLayer.Name = "tvwLayer";
            this.tvwLayer.Size = new System.Drawing.Size(224, 209);
            this.tvwLayer.TabIndex = 0;
            this.tvwLayer.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwLayer_NodeMouseClick);
            // 
            // contextMenuDummy
            // 
            this.contextMenuDummy.Name = "contextMenuDummy";
            this.contextMenuDummy.Size = new System.Drawing.Size(61, 4);
            // 
            // cboLayerType
            // 
            this.cboLayerType.ContextMenuStrip = this.contextMenuDummy;
            this.cboLayerType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboLayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayerType.FormattingEnabled = true;
            this.cboLayerType.Items.AddRange(new object[] {
            "All Layer Type",
            "Feature Layers",
            "Raster Layers",
            "Data Layers"});
            this.cboLayerType.Location = new System.Drawing.Point(0, 0);
            this.cboLayerType.Name = "cboLayerType";
            this.cboLayerType.Size = new System.Drawing.Size(224, 21);
            this.cboLayerType.TabIndex = 1;
            this.cboLayerType.SelectedIndexChanged += new System.EventHandler(this.cboLayerType_SelectedIndexChanged);
            // 
            // TOCLayerFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwLayer);
            this.Controls.Add(this.cboLayerType);
            this.Name = "TOCLayerFilter";
            this.Size = new System.Drawing.Size(224, 230);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvwLayer;
        private System.Windows.Forms.ComboBox cboLayerType;
        private System.Windows.Forms.ContextMenuStrip contextMenuDummy;
    }
}
