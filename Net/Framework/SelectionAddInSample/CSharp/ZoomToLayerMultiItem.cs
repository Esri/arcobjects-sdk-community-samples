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
