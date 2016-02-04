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
namespace ESRIWebSitesCS
{
    partial class ESRIWebsitesWindow
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.cboURLs = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(340, 164);
            this.webBrowser1.TabIndex = 1;
            // 
            // cboURLs
            // 
            this.cboURLs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cboURLs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLs.FormattingEnabled = true;
            this.cboURLs.Location = new System.Drawing.Point(0, 164);
            this.cboURLs.Name = "cboURLs";
            this.cboURLs.Size = new System.Drawing.Size(340, 21);
            this.cboURLs.TabIndex = 2;
            this.cboURLs.SelectedIndexChanged += new System.EventHandler(this.cboURLs_SelectedIndexChanged);
            // 
            // ESRIWebsitesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.cboURLs);
            this.Name = "ESRIWebsitesWindow";
            this.Size = new System.Drawing.Size(340, 185);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox cboURLs;

    }
}
