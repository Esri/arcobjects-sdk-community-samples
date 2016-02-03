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
  [Guid("AFC53D59-FB35-4531-9B91-DFB36512A784")]
  [ComVisible(true)]
  [ProgId("RSSWeatherLayer.RSSLayerProps2")]
  [ClassInterface(ClassInterfaceType.None)]
  public partial class RSSLayerProps2 : PropertyPage
  {
    private int m_symbolSize;

    public RSSLayerProps2()
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

      int symbolSize;
      int.TryParse(txtSymbolSize.Text, out symbolSize);
      if (0 != symbolSize && m_symbolSize != symbolSize)
        layer.SymbolSize = symbolSize;

    }

    protected override void OnPageActivate(IntPtr hWndParent, Rectangle Rect, bool bModal)
    {
      base.OnPageActivate(hWndParent, Rect, bModal);

      PropertySheet propSheet = Objects[0] as PropertySheet;

      RSSWeatherLayerClass layer = propSheet.RSSWatherLayer;
      if (null == layer)
        return;

      txtSymbolSize.Text = layer.SymbolSize.ToString();

      m_symbolSize = layer.SymbolSize;
    }

    private void txtSymbolSize_TextChanged(object sender, EventArgs e)
    {
      if (!IsPageActivating)
        PageIsDirty = true;
    }
  }
}