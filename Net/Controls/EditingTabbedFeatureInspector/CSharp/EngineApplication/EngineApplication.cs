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
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using TabbedFeatureInspector;

namespace TabbedInspectorEngineApplication
{
  public partial class EngineApplication : Form, IApplicationServices
  {
    public EngineApplication()
    {
      InitializeComponent();

      // Store the application services interface instance in the toolbar's custom property, so
      // commands can get to it to access the TOC and update the status message.
      toolbar.CustomProperty = this;
    }

    #region IApplicationServices implementation

    public void SetStatusMessage(string message, bool error)
    {
      MethodInvoker updateStatus = delegate
                                     {
                                       status.Text = message;
                                       status.BackColor = error ? Color.Red : SystemColors.ButtonFace;
                                       status.ForeColor = error ? Color.White : SystemColors.ControlText;
                                     };

      if (status.InvokeRequired)
        status.Invoke(updateStatus);
      else
        updateStatus();
    }

    public IFeatureLayer GetLayerSelectedInTOC()
    {
      esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
      object unkIgnore = null;
      ILayer selectedLayer = null;
      object dataIgnore = null;
      IBasicMap mapIgnore = null;

      tableOfContents.GetSelectedItem(ref itemType, ref mapIgnore, ref selectedLayer, ref unkIgnore, ref dataIgnore);

      return selectedLayer as IFeatureLayer;
    }

    #endregion

  }
}