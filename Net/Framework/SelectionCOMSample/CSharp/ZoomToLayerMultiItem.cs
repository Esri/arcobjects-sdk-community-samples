/*

   Copyright 2016 Esri

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
using ESRI.ArcGIS.SystemUI;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace SelectionCOMSample
{
  [Guid("B8147F77-BE16-4a43-A2F1-E6E030BD579E")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SelectionCOMSample.ZoomToLayerMultiItem")]
  public sealed class ZoomToLayerMultiItem : IMultiItem, IMultiItemEx
  {
    private IMap m_map;
    private IMxDocument m_doc;

    #region IMultiItem Members

    public string Caption
    {
      get { return "Selection MultiItem C#"; }
    }

    public int HelpContextID
    {
      get { return 0; }
    }

    public string HelpFile
    {
      get { return ""; }
    }

    public string Message
    {
      get { return "Select layer to zoom to its full extent."; }
    }

    public string Name
    {
      get { return "ESRI_SelectionCOMSample_MultiItem"; }
    }

    public void OnItemClick(int index)
    {
      ESRI.ArcGIS.Carto.ILayer layer = m_map.get_Layer(index);
      ESRI.ArcGIS.Geometry.IEnvelope env = layer.AreaOfInterest;
      m_doc.ActiveView.Extent = env;
      m_doc.ActiveView.Refresh();
    }

    public int OnPopup(object Hook)
    {
      IApplication app = Hook as IApplication;
      if (app == null)
        return 0;

      m_doc = app.Document as IMxDocument;
      m_map = m_doc.FocusMap;

      return m_map.LayerCount;
    }

    public int get_ItemBitmap(int index)
    {
      return 0;
    }

    public string get_ItemCaption(int index)
    {
      ILayer layer = m_map.get_Layer(index);
      if (layer != null)
        return layer.Name;
      else
        return "";
    }

    public bool get_ItemChecked(int index)
    {
      return false;
    }

    public bool get_ItemEnabled(int index)
    {
      ILayer layer = m_map.get_Layer(index);
      if (layer != null)
        return layer.Visible;
      else
        return false;
    }

    #endregion

    #region IMultiItemEx Members

    public int get_ItemHelpContextID(int index)
    {
      return 0;
    }

    public string get_ItemHelpFile(int index)
    {
      return "";
    }

    public string get_ItemMessage(int index)
    {
      ILayer layer = m_map.get_Layer(index);
      if (layer != null)
        return layer.Name;
      else
        return "";
    }

    #endregion
  }
}
