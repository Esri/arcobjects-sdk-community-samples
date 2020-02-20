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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.CATIDs;

namespace ACME.GIS.SampleExt
{
  [Guid("A7198661-605D-4683-A282-221C154450A1")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("ACME.MainMenu")]
  public class AcmeMenu : IMenuDef, IRootLevelMenu
  {
    #region IMenuDef Members

    public string Caption
    {
      get
      {
        return "ACME";
      }
    }

    public void GetItemInfo(int pos, IItemDef itemDef)
    {
      // Add some commands to the menu (don't really exist for simplicity sake)
      switch (pos)
      {
        case 0:
          itemDef.ID = "ACME.SomeCmd";
          itemDef.Group = false;
          break;
        case 1:
          itemDef.ID = "ACME.SomeCmd2";
          itemDef.Group = true;
          break;
      }
    }

    public int ItemCount
    {
      get { return 2; }
    }

    public string Name
    {
      get { return "ACME Main Menu"; }
    }

    #endregion
  }
}
