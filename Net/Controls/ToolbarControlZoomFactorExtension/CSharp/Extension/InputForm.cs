using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ZoomFactorExtensionCSharp
{
	public class InputForm : System.Windows.Forms.Form
	{
		#region Controls Declaration
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		#endregion
		
		internal bool bCanceled = false;
		private System.ComponentModel.Container components = null;
		
		public static InputFormResult ShowModal(System.Windows.Forms.IWin32Window owner, string caption, string title, string defaultText)
		{
			InputForm theForm = new InputForm();
			theForm.InitializeComponent();
			theForm.lblCaption.Text = caption;
			theForm.Text = title;
			theForm.txtInput.Text = defaultText;
			theForm.ShowDialog(owner);
		
			return GetResult(theForm);
		}	

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		private static InputFormResult GetResult(InputForm currForm)
		{
			if (currForm.bCanceled)
			{
				return new InputFormResult(DialogResult.Cancel);
			}
			else
			{
				return new InputFormResult(DialogResult.OK, currForm.txtInput.Text);
			}
		}
		private static void SizeCaption(InputForm currForm, string caption)
		{
			// Work out how big the text will be.
			SizeF layoutSize = new SizeF (currForm.lblCaption.Width, SystemInformation.WorkingArea.Height);
			System.Drawing.Graphics grph = System.Drawing.Graphics.FromHwnd(currForm.Handle);
			SizeF stringSize = grph.MeasureString(caption, currForm.lblCaption.Font, layoutSize);

            // Resize the caption label and other controls appropriately.
			if ( ((int) stringSize.Height) > currForm.lblCaption.Height)
			{
				currForm.SuspendLayout();
				currForm.lblCaption.Height = ((int) stringSize.Height) + 4;
				currForm.txtInput.Top = currForm.lblCaption.Bottom + 12;
				currForm.btnCancel.Top = currForm.txtInput.Bottom + 8;
				currForm.btnOK.Top = currForm.btnCancel.Top;
				currForm.Height = currForm.btnOK.Bottom + (30);
				currForm.ResumeLayout(true);				
			}
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtInput = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(8, 68);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(336, 20);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			// 
			// lblCaption
			// 
			this.lblCaption.Location = new System.Drawing.Point(12, 16);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(328, 36);
			this.lblCaption.TabIndex = 1;
			this.lblCaption.Text = "label1";
			this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picIcon
			// 
			this.picIcon.Location = new System.Drawing.Point(276, 16);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(40, 36);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picIcon.TabIndex = 2;
			this.picIcon.TabStop = false;
			this.picIcon.Visible = false;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(276, 100);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 24);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(212, 100);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// InputForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 129);
			this.ControlBox = false;
			this.Controls.Add(this.lblCaption);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.picIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "InputForm";
			this.Text = "InputForm";
			this.Load += new System.EventHandler(this.InputForm_Load);
			this.ResumeLayout(false);

		}
		#endregion
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			bCanceled = true;
			this.Close();
		}
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			bCanceled = false;
			this.Close();
		}

		private void InputForm_Load(object sender, System.EventArgs e)
		{
		
		}
	}
	public class InputFormResult
	{
		DialogResult m_result = DialogResult.None;
		string m_input = "";

		public InputFormResult(DialogResult res)
		{
			m_result = res;
		}
		public InputFormResult(DialogResult res, string userInput)
		{
			m_result = res;
			m_input = userInput;
		}
		public DialogResult Result
		{
			get
			{
				return m_result;
			}
		}
		public string InputString
		{
			get
			{
				return m_input;
			}
		}	
	}
}
