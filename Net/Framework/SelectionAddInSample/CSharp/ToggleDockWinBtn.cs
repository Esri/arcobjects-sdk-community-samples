using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace SelectionSample
{
  public class ToggleDockWinBtn : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public ToggleDockWinBtn()
    {
     
    }

    protected override void OnClick()
    {
      IDockableWindow dockWindow = SelectionExtension.GetSelectionCountWindow();

      if (dockWindow == null)
        return;
      
      dockWindow.Show(!dockWindow.IsVisible());
    }

    protected override void OnUpdate()
    {
      this.Enabled = SelectionExtension.IsExtensionEnabled();
    }
  }

}
