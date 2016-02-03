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