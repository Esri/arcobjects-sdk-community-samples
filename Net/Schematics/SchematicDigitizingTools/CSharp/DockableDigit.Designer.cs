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
namespace DigitTool
{
    partial class DigitDockableWindow
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
					this.components = new System.ComponentModel.Container();
					this.lblMode = new System.Windows.Forms.Label();
					this.btnOKPanel1 = new System.Windows.Forms.Button();
					this.btnOKPanel2 = new System.Windows.Forms.Button();
					this.Label1 = new System.Windows.Forms.Label();
					this.cboNodeType = new System.Windows.Forms.ComboBox();
					this.cboLinkType = new System.Windows.Forms.ComboBox();
					this.lblNodeLabel = new System.Windows.Forms.Label();
					this.Splitter = new System.Windows.Forms.SplitContainer();
					this.lblLinkLabel = new System.Windows.Forms.Label();
					this.ErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
					this.Splitter.Panel1.SuspendLayout();
					this.Splitter.Panel2.SuspendLayout();
					this.Splitter.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).BeginInit();
					this.SuspendLayout();
					// 
					// lblMode
					// 
					this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.lblMode.AutoSize = true;
					this.lblMode.Location = new System.Drawing.Point(104, 554);
					this.lblMode.Name = "lblMode";
					this.lblMode.Size = new System.Drawing.Size(33, 13);
					this.lblMode.TabIndex = 22;
					this.lblMode.Text = "None";
					// 
					// btnOKPanel1
					// 
					this.btnOKPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.ErrorProvider1.SetError(this.btnOKPanel1, "Invalid data entry");
					this.btnOKPanel1.Location = new System.Drawing.Point(214, 200);
					this.btnOKPanel1.Name = "btnOKPanel1";
					this.btnOKPanel1.Size = new System.Drawing.Size(44, 24);
					this.btnOKPanel1.TabIndex = 7;
					this.btnOKPanel1.Text = "OK";
					this.btnOKPanel1.UseVisualStyleBackColor = true;
					this.btnOKPanel1.Visible = false;
					this.btnOKPanel1.Click += new System.EventHandler(this.btnOKPanel1_Click);
					// 
					// btnOKPanel2
					// 
					this.btnOKPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.btnOKPanel2.Location = new System.Drawing.Point(225, 238);
					this.btnOKPanel2.Name = "btnOKPanel2";
					this.btnOKPanel2.Size = new System.Drawing.Size(44, 24);
					this.btnOKPanel2.TabIndex = 9;
					this.btnOKPanel2.Text = "OK";
					this.btnOKPanel2.UseVisualStyleBackColor = true;
					this.btnOKPanel2.Visible = false;
					this.btnOKPanel2.Click += new System.EventHandler(this.btnOKPanel2_Click);
					// 
					// Label1
					// 
					this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.Label1.AutoSize = true;
					this.Label1.Location = new System.Drawing.Point(24, 554);
					this.Label1.Name = "Label1";
					this.Label1.Size = new System.Drawing.Size(74, 13);
					this.Label1.TabIndex = 21;
					this.Label1.Text = "Current Mode:";
					// 
					// cboNodeType
					// 
					this.cboNodeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.cboNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					this.cboNodeType.FormattingEnabled = true;
					this.cboNodeType.Location = new System.Drawing.Point(63, 5);
					this.cboNodeType.Name = "cboNodeType";
					this.cboNodeType.Size = new System.Drawing.Size(211, 21);
					this.cboNodeType.TabIndex = 6;
					this.cboNodeType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
					// 
					// cboLinkType
					// 
					this.cboLinkType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.cboLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
					this.cboLinkType.Enabled = false;
					this.cboLinkType.FormattingEnabled = true;
					this.cboLinkType.Location = new System.Drawing.Point(63, 3);
					this.cboLinkType.Name = "cboLinkType";
					this.cboLinkType.Size = new System.Drawing.Size(211, 21);
					this.cboLinkType.TabIndex = 8;
					this.cboLinkType.SelectedIndexChanged += new System.EventHandler(this.cboLinkType_SelectedIndexChanged);
					this.cboLinkType.Click += new System.EventHandler(this.cboLinkType_SelectedIndexChanged);
					// 
					// lblNodeLabel
					// 
					this.lblNodeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.lblNodeLabel.AutoSize = true;
					this.lblNodeLabel.Location = new System.Drawing.Point(3, 8);
					this.lblNodeLabel.Name = "lblNodeLabel";
					this.lblNodeLabel.Size = new System.Drawing.Size(60, 13);
					this.lblNodeLabel.TabIndex = 5;
					this.lblNodeLabel.Text = "Node Type";
					// 
					// Splitter
					// 
					this.Splitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.Splitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					this.Splitter.Location = new System.Drawing.Point(27, 28);
					this.Splitter.Name = "Splitter";
					this.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
					// 
					// Splitter.Panel1
					// 
					this.Splitter.Panel1.AutoScroll = true;
					this.Splitter.Panel1.Controls.Add(this.btnOKPanel1);
					this.Splitter.Panel1.Controls.Add(this.cboNodeType);
					this.Splitter.Panel1.Controls.Add(this.lblNodeLabel);
					this.Splitter.Panel1.Click += new System.EventHandler(this.Splitter_Panel1_Click);
					// 
					// Splitter.Panel2
					// 
					this.Splitter.Panel2.AutoScroll = true;
					this.Splitter.Panel2.Controls.Add(this.btnOKPanel2);
					this.Splitter.Panel2.Controls.Add(this.cboLinkType);
					this.Splitter.Panel2.Controls.Add(this.lblLinkLabel);
					this.Splitter.Panel2.Click += new System.EventHandler(this.Splitter_Panel2_Click);
					this.Splitter.Size = new System.Drawing.Size(278, 518);
					this.Splitter.SplitterDistance = 239;
					this.Splitter.TabIndex = 20;
					// 
					// lblLinkLabel
					// 
					this.lblLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.lblLinkLabel.AutoSize = true;
					this.lblLinkLabel.Enabled = false;
					this.lblLinkLabel.Location = new System.Drawing.Point(3, 6);
					this.lblLinkLabel.Name = "lblLinkLabel";
					this.lblLinkLabel.Size = new System.Drawing.Size(54, 13);
					this.lblLinkLabel.TabIndex = 7;
					this.lblLinkLabel.Text = "Link Type";
					// 
					// ErrorProvider1
					// 
					this.ErrorProvider1.ContainerControl = this;
					this.ErrorProvider1.RightToLeft = true;
					// 
					// DigitDockableWindow
					// 
					this.Controls.Add(this.lblMode);
					this.Controls.Add(this.Label1);
					this.Controls.Add(this.Splitter);
					this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
					this.MaximumSize = new System.Drawing.Size(1000, 1000);
					this.MinimumSize = new System.Drawing.Size(300, 597);
					this.Name = "DigitDockableWindow";
					this.Size = new System.Drawing.Size(331, 595);
					this.Tag = "ESRI_AddInDockableWindow_Digit";
					this.VisibleChanged += new System.EventHandler(this.WindowVisibleChange);
					this.Resize += new System.EventHandler(this.DigitWindow_Resize);
					this.Splitter.Panel1.ResumeLayout(false);
					this.Splitter.Panel1.PerformLayout();
					this.Splitter.Panel2.ResumeLayout(false);
					this.Splitter.Panel2.PerformLayout();
					this.Splitter.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).EndInit();
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblMode;
        internal System.Windows.Forms.Button btnOKPanel1;
        internal System.Windows.Forms.ErrorProvider ErrorProvider1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.SplitContainer Splitter;
        internal System.Windows.Forms.ComboBox cboNodeType;
        internal System.Windows.Forms.Label lblNodeLabel;
        internal System.Windows.Forms.Button btnOKPanel2;
        internal System.Windows.Forms.ComboBox cboLinkType;
        internal System.Windows.Forms.Label lblLinkLabel;

    }
}
