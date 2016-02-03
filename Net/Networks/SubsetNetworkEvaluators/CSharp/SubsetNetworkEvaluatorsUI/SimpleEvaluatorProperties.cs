using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SubsetNetworkEvaluatorsUI
{
	/// <summary>
	/// Simple informational properties dialog for displaying read only information
	/// about the chosen subset evaluator from the evaluators dialog in ArcCatalog.
	/// </summary>
	public partial class SimpleEvaluatorProperties : Form
	{
		public SimpleEvaluatorProperties(string description)
		{
			InitializeComponent();
			lblEvaluatorDescription.Text = description;
		}
	}
}