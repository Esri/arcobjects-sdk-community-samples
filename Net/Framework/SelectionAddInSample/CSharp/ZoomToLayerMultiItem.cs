using System;
using System.Collections.Generic;
using System.Text;

namespace SelectionSample
{
  public class ZoomToLayerMultiItem : ESRI.ArcGIS.Desktop.AddIns.MultiItem
  {
    protected override void OnClick(Item item)
    {
      ESRI.ArcGIS.Carto.ILayer layer = item.Tag as ESRI.ArcGIS.Carto.ILayer;
      ESRI.ArcGIS.Geometry.IEnvelope env = layer.AreaOfInterest;
      ArcMap.Document.ActiveView.Extent = env;
      ArcMap.Document.ActiveView.Refresh();
    }

    protected override void OnPopup(ItemCollection items)
    {
      ESRI.ArcGIS.Carto.IMap map = ArcMap.Document.FocusMap;
      for (int i = 0; i < map.LayerCount; i++)
      {
        ESRI.ArcGIS.Carto.ILayer layer = map.get_Layer(i);
        Item item = new Item();
        item.Caption = layer.Name;
        item.Enabled = layer.Visible;
        item.Message = layer.Name;
        item.Tag = layer;
        items.Add(item);
      }
    }
  }
}
