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
using ESRI.ArcGIS.Controls;

// This window shows the property pages for the ArcGIS Network Analyst extension environment.
namespace NAEngine
{
	public class frmNAProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox chkZoomToResultAfterSolve;
		private System.Windows.Forms.GroupBox grpMessages;
		private System.Windows.Forms.RadioButton rdoAllMessages;
		private System.Windows.Forms.RadioButton rdoErrorsAndWarnings;
		private System.Windows.Forms.RadioButton rdoNoMessages;
		private System.Windows.Forms.RadioButton rdoErrors;

		bool m_okClicked;

		public frmNAProperties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.chkZoomToResultAfterSolve = new System.Windows.Forms.CheckBox();
			this.grpMessages = new System.Windows.Forms.GroupBox();
			this.rdoNoMessages = new System.Windows.Forms.RadioButton();
			this.rdoErrors = new System.Windows.Forms.RadioButton();
			this.rdoErrorsAndWarnings = new System.Windows.Forms.RadioButton();
			this.rdoAllMessages = new System.Windows.Forms.RadioButton();
			this.grpMessages.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(240, 216);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 32);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(112, 216);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 32);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// chkZoomToResultAfterSolve
			// 
			this.chkZoomToResultAfterSolve.Location = new System.Drawing.Point(16, 24);
			this.chkZoomToResultAfterSolve.Name = "chkZoomToResultAfterSolve";
			this.chkZoomToResultAfterSolve.Size = new System.Drawing.Size(200, 24);
			this.chkZoomToResultAfterSolve.TabIndex = 5;
			this.chkZoomToResultAfterSolve.Text = "Zoom To Result After Solve";
			// 
			// grpMessages
			// 
			this.grpMessages.Controls.Add(this.rdoNoMessages);
			this.grpMessages.Controls.Add(this.rdoErrors);
			this.grpMessages.Controls.Add(this.rdoErrorsAndWarnings);
			this.grpMessages.Controls.Add(this.rdoAllMessages);
			this.grpMessages.Location = new System.Drawing.Point(16, 72);
			this.grpMessages.Name = "grpMessages";
			this.grpMessages.Size = new System.Drawing.Size(336, 120);
			this.grpMessages.TabIndex = 6;
			this.grpMessages.TabStop = false;
			this.grpMessages.Text = "Messages";
			// 
			// rdoNoMessages
			// 
			this.rdoNoMessages.Location = new System.Drawing.Point(16, 88);
			this.rdoNoMessages.Name = "rdoNoMessages";
			this.rdoNoMessages.Size = new System.Drawing.Size(304, 24);
			this.rdoNoMessages.TabIndex = 3;
			this.rdoNoMessages.Text = "No Messages";
			// 
			// rdoErrors
			// 
			this.rdoErrors.Location = new System.Drawing.Point(16, 64);
			this.rdoErrors.Name = "rdoErrors";
			this.rdoErrors.Size = new System.Drawing.Size(304, 24);
			this.rdoErrors.TabIndex = 2;
			this.rdoErrors.Text = "Errors";
			// 
			// rdoErrorsAndWarnings
			// 
			this.rdoErrorsAndWarnings.Location = new System.Drawing.Point(16, 40);
			this.rdoErrorsAndWarnings.Name = "rdoErrorsAndWarnings";
			this.rdoErrorsAndWarnings.Size = new System.Drawing.Size(304, 24);
			this.rdoErrorsAndWarnings.TabIndex = 1;
			this.rdoErrorsAndWarnings.Text = "Errors and Warnings";
			// 
			// rdoAllMessages
			// 
			this.rdoAllMessages.Location = new System.Drawing.Point(16, 16);
			this.rdoAllMessages.Name = "rdoAllMessages";
			this.rdoAllMessages.Size = new System.Drawing.Size(304, 24);
			this.rdoAllMessages.TabIndex = 0;
			this.rdoAllMessages.Text = "All Messages";
			// 
			// frmNAProperties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(370, 262);
			this.Controls.Add(this.grpMessages);
			this.Controls.Add(this.chkZoomToResultAfterSolve);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNAProperties";
			this.ShowInTaskbar = false;
			this.Text = "Network Analyst Properties";
			this.grpMessages.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void ShowModal()
		{
			m_okClicked = false;

			var naEnv = CommonFunctions.GetTheEngineNetworkAnalystEnvironment();
			if (naEnv == null)
			{
				System.Windows.Forms.MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured");
				return;
			}

			// Zoom to result after solve or not
			chkZoomToResultAfterSolve.Checked = naEnv.ZoomToResultAfterSolve;

			// Set the radio button based on the value in ShowAnalysisMessagesAfterSolve.
			// This is a bit property where multiple values are possible.  
			// Simplify it for the user so assume message types build on each other.  
			//  For example, if you want info, you probably want warnings and errors too
			//   No Messages = 0
			//   Errors = esriEngineNAMessageTypeError
			//   Errors and warnings = esriEngineNAMessageTypeError & esriEngineNAMessageTypeWarning
			//   All = esriEngineNAMessageTypeError & esriEngineNAMessageTypeWarning & esriEngineNAMessageTypeInformative
			if ((esriEngineNAMessageType)(naEnv.ShowAnalysisMessagesAfterSolve & (int)esriEngineNAMessageType.esriEngineNAMessageTypeInformative) == esriEngineNAMessageType.esriEngineNAMessageTypeInformative)
				rdoAllMessages.Checked = true;
			else if ((esriEngineNAMessageType)(naEnv.ShowAnalysisMessagesAfterSolve & (int)esriEngineNAMessageType.esriEngineNAMessageTypeWarning) == esriEngineNAMessageType.esriEngineNAMessageTypeWarning)
				rdoErrorsAndWarnings.Checked = true;
			else if ((esriEngineNAMessageType)(naEnv.ShowAnalysisMessagesAfterSolve & (int)esriEngineNAMessageType.esriEngineNAMessageTypeError) == esriEngineNAMessageType.esriEngineNAMessageTypeError)
				rdoErrors.Checked = true;
			else
				rdoNoMessages.Checked = true;

			this.ShowDialog();
			if (m_okClicked)
			{
				// Set ZoomToResultAfterSolve
				naEnv.ZoomToResultAfterSolve = chkZoomToResultAfterSolve.Checked;

				// Set ShowAnalysisMessagesAfterSolve
				// Use simplified version so higher severity errors also show lower severity "info" and "warnings"
				if (rdoAllMessages.Checked)
					naEnv.ShowAnalysisMessagesAfterSolve = (int)esriEngineNAMessageType.esriEngineNAMessageTypeInformative + (int)esriEngineNAMessageType.esriEngineNAMessageTypeWarning + (int)esriEngineNAMessageType.esriEngineNAMessageTypeError;
				else if (rdoErrorsAndWarnings.Checked)
					naEnv.ShowAnalysisMessagesAfterSolve = (int)esriEngineNAMessageType.esriEngineNAMessageTypeWarning + (int)esriEngineNAMessageType.esriEngineNAMessageTypeError;
				else if (rdoErrors.Checked)
					naEnv.ShowAnalysisMessagesAfterSolve = (int)esriEngineNAMessageType.esriEngineNAMessageTypeError;
				else
					naEnv.ShowAnalysisMessagesAfterSolve = 0;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			m_okClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_okClicked = false;
			this.Close();
		}
	}
}
