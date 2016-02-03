using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace MyDynamicDisplayApp
{
  /// <summary>
  /// Summary description for CreateNewDocument.
  /// </summary>
  public class CreateNewDocument : BaseCommand
  {
    private IHookHelper m_hookHelper = null;

    //constructor
    public CreateNewDocument()
    {
      //update the base properties
      base.m_category = ".NET Samples";
      base.m_caption = "NewDocument";
      base.m_message = "Create a new map";
      base.m_toolTip = "Create a new map";
      base.m_name = "DotNetTemplate_NewDocumentCommand";
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();

      m_hookHelper.Hook = hook;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      IMapControl3 mapControl = null;

      //get the MapControl from the hook in case the container is a ToolbatControl
      if (m_hookHelper.Hook is IToolbarControl)
      {
        mapControl = (IMapControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;
      }
      //In case the container is MapControl
      else if (m_hookHelper.Hook is IMapControl3)
      {
        mapControl = (IMapControl3)m_hookHelper.Hook;
      }
      else
      {
        MessageBox.Show("Active control must be MapControl!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      //allow the user to save the current document
      DialogResult res = MessageBox.Show("Would you like to save the current document?", "AoView", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (res == DialogResult.Yes)
      {
        //launch the save command (why work hard!?)
        ICommand command = new ControlsSaveAsDocCommandClass();
        command.OnCreate(m_hookHelper.Hook);
        command.OnClick();
      }

      //create a new Map
      IMap map = new MapClass();
      map.Name = "Map";

      //assign the new map to the MapControl
      mapControl.DocumentFilename = string.Empty;
      mapControl.Map = map;
    }

    #endregion
  }
}
