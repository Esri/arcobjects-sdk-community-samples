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
