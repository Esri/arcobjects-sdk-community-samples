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
