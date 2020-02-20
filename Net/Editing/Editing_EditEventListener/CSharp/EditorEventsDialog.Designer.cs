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
namespace Events
{
    partial class EditorEventsDialog
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblEditorEvents = new System.Windows.Forms.Label();
            this.lstEditorEvents = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSelectEvent = new System.Windows.Forms.CheckedListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(300, 158);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblEditorEvents);
            this.tabPage1.Controls.Add(this.lstEditorEvents);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(292, 132);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Listen to Events";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblEditorEvents
            // 
            this.lblEditorEvents.AutoSize = true;
            this.lblEditorEvents.Location = new System.Drawing.Point(3, -21);
            this.lblEditorEvents.Name = "lblEditorEvents";
            this.lblEditorEvents.Size = new System.Drawing.Size(162, 13);
            this.lblEditorEvents.TabIndex = 7;
            this.lblEditorEvents.Text = "Editor Events (Most Recent First)";
            // 
            // lstEditorEvents
            // 
            this.lstEditorEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEditorEvents.FormattingEnabled = true;
            this.lstEditorEvents.Location = new System.Drawing.Point(3, 3);
            this.lstEditorEvents.Name = "lstEditorEvents";
            this.lstEditorEvents.ScrollAlwaysVisible = true;
            this.lstEditorEvents.Size = new System.Drawing.Size(286, 121);
            this.lstEditorEvents.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSelectEvent);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(292, 132);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Select Events";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSelectEvent
            // 
            this.chkSelectEvent.CheckOnClick = true;
            this.chkSelectEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSelectEvent.FormattingEnabled = true;
            this.chkSelectEvent.Location = new System.Drawing.Point(3, 3);
            this.chkSelectEvent.Name = "chkSelectEvent";
            this.chkSelectEvent.Size = new System.Drawing.Size(286, 124);
            this.chkSelectEvent.TabIndex = 0;
            // 
            // EditorEventsDialog
            // 
            this.Controls.Add(this.tabControl1);
            this.Name = "EditorEventsDialog";
            this.Size = new System.Drawing.Size(300, 158);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblEditorEvents;
        public System.Windows.Forms.ListBox lstEditorEvents;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckedListBox chkSelectEvent;

    }
}
