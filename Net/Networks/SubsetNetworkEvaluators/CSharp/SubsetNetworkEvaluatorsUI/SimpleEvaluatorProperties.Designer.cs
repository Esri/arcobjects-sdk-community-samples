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
namespace SubsetNetworkEvaluatorsUI
{
	partial class SimpleEvaluatorProperties
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
			this.lblEvaluatorDescription = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblEvaluatorDescription
			// 
			this.lblEvaluatorDescription.AutoSize = true;
			this.lblEvaluatorDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblEvaluatorDescription.Location = new System.Drawing.Point(0, 0);
			this.lblEvaluatorDescription.Name = "lblEvaluatorDescription";
			this.lblEvaluatorDescription.Size = new System.Drawing.Size(93, 13);
			this.lblEvaluatorDescription.TabIndex = 0;
			this.lblEvaluatorDescription.Text = "Custom Evaluator:";
			// 
			// SimpleEvaluatorProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(673, 323);
			this.Controls.Add(this.lblEvaluatorDescription);
			this.Name = "SimpleEvaluatorProperties";
			this.Text = "Parameterized Evaluator Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblEvaluatorDescription;
	}
}