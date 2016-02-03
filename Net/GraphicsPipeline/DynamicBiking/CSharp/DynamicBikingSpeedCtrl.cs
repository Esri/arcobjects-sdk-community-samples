using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DynamicBiking
{
	public partial class DynamicBikingSpeedCtrl : UserControl
	{
		private DynamicBikingCmd m_dynamicBikingCmd = null;
		
		public DynamicBikingSpeedCtrl()
		{
			InitializeComponent();
		}

		public void SetDynamicBikingCmd(DynamicBikingCmd dynamicBikingCmd)
		{
			m_dynamicBikingCmd = dynamicBikingCmd;
		}

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			if (m_dynamicBikingCmd != null)
			{
				m_dynamicBikingCmd.PlaybackSpeed = trackBar1.Value;
				toolTip1.ToolTipTitle = Convert.ToString(trackBar1.Value);
			}
		}
	}
}
