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
using System.IO;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace CustomUIElements
{
  public class ZoomToLayerButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public ZoomToLayerButton()
    {
    }

    protected override void OnClick()
    {
      ZoomToActiveLayerInTOC();
    }

    protected override void OnUpdate()
    {
      this.Enabled = ArcMap.Application != null;
    }

    #region "Zoom to Active Layer in TOC"
    public void ZoomToActiveLayerInTOC()
    {
      IMxDocument mxDocument = ArcMap.Application.Document as IMxDocument;
      IActiveView activeView = mxDocument.ActiveView;

      // Get the TOC
      IContentsView IContentsView = mxDocument.CurrentContentsView;

      // Get the selected layer
      System.Object selectedItem = IContentsView.SelectedItem;
      if (!(selectedItem is ILayer))
      {
        return;
      }
      ILayer layer = selectedItem as ILayer;
      // Zoom to the extent of the layer and refresh the map
      activeView.Extent = layer.AreaOfInterest;
      activeView.Refresh();
    }
    #endregion
  }
}
