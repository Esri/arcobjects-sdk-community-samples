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
using System.Runtime.InteropServices;

namespace RSSWeatherLayer
{
  [Guid("12D0AE46-D542-43f7-8A53-5B7FCA4AA111")]
  [ComVisible(true)]
  [ProgId("RSSWeatherLayer.RSSLayerProps")]
  [ClassInterface(ClassInterfaceType.None)]
  public partial class RSSLayerProps : PropertyPage
  {
    public RSSLayerProps()
    {
      InitializeComponent();
    }

    protected override void OnPageApply()
    {
      base.OnPageApply();

      PropertySheet propSheet = Objects[0] as PropertySheet;

      RSSWeatherLayerClass layer = propSheet.RSSWatherLayer;
      if (null == layer)
        return;

    }

    protected override void OnPageActivate(IntPtr hWndParent, Rectangle Rect, bool bModal)
    {
      base.OnPageActivate(hWndParent, Rect, bModal);

      PropertySheet propSheet = Objects[0] as PropertySheet;

      RSSWeatherLayerClass layer = propSheet.RSSWatherLayer;
      if (null == layer)
        return;

      //get the cityNames from the layer
      string[] cityNames = layer.GetCityNames();

      //clear the listbox
			listBoxCityNames.Items.Clear();
      listBoxCityNames.Items.AddRange(cityNames);
    }
  }
}