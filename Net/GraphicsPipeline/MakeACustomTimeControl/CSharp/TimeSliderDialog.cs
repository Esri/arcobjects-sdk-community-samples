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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace MakeACustomTimeControl2008
{
  public partial class TimeSliderDialog : Form
  {
    private CustomTimeSliderButton m_parent = null;

    public TimeSliderDialog(CustomTimeSliderButton parent)
    {
      InitializeComponent();
      m_parent = parent;
      ITimeExtent timeExtent = m_parent.GetTimeExtent();
      m_datePicker.MinDate = new DateTime(timeExtent.StartTime.QueryTicks());
      m_datePicker.MaxDate = new DateTime(timeExtent.EndTime.QueryTicks());
      m_datePicker.Value = m_datePicker.MinDate;
    }

    private void TimeSlider_ValueChanged(object sender, EventArgs e)
    {
      m_parent.UpdateCurrentTime(0.01 * (double)(m_timeSlider.Value));

    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
      long ticks = m_datePicker.Value.Ticks;
      long minTicks = m_datePicker.MinDate.Ticks;
      long maxTicks = m_datePicker.MaxDate.Ticks;
      double progress = ((double)(ticks - minTicks)) / ((double)(maxTicks - minTicks));
      m_parent.UpdateCurrentTime(progress);

    }
  }
}
