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