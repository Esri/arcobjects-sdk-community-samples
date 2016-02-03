
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace MultivariateRenderers
{
	public class SecondaryForm : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public SecondaryForm() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.Text = "SecondaryForm";
		}

	#endregion

	}

} //end of root namespace