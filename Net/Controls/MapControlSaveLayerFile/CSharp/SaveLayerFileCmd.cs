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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace MapControlSaveLayerFile
{
  /// <summary>
  /// This command demonstrates saving of a layer file in an ArcGIS Engine application
  /// </summary>
  [Guid("294563e7-b475-43db-a2d1-a6b5f95c7113")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MapControlSaveLayerFile.SaveLayerFileCmd")]
  public sealed class SaveLayerFileCmd : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Register(regKey);
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper = null;
    public SaveLayerFileCmd()
    {
      base.m_category = ".NET Samples"; 
      base.m_caption = "Create Layer File";
      base.m_message = "Save the current layer as a layer file";
      base.m_toolTip = "Create As Layer File";
      base.m_name = "MapControlSaveLayerFile_SaveLayerFileCmd";  

      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      try
      {
        m_hookHelper = new HookHelperClass();
        m_hookHelper.Hook = hook;
        if (m_hookHelper.ActiveView == null)
          m_hookHelper = null;
      }
      catch
      {
        m_hookHelper = null;
      }

      if (m_hookHelper == null)
        base.m_enabled = false;
      else
        base.m_enabled = true;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      //need to get the layer from the custom-property of the map
      if (null == m_hookHelper)
        return;

      //get the mapControl hook
      object hook = null;
      if (m_hookHelper.Hook is IToolbarControl2)
      {
        hook = ((IToolbarControl2)m_hookHelper.Hook).Buddy;
      }
      else
      {
        hook = m_hookHelper.Hook;
      }

      //get the custom property from which is supposed to be the layer to be saved
      object customProperty = null;
      IMapControl3 mapControl = null;
      if (hook is IMapControl3)
      {
        mapControl = (IMapControl3)hook;
        customProperty = mapControl.CustomProperty;
      }
      else
        return;

      if (null == customProperty || !(customProperty is ILayer))
        return;

      //ask the user to set a name for the new layer file
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Layer File|*.lyr|All Files|*.*";
      saveFileDialog.Title = "Create Layer File";
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.FileName = System.IO.Path.Combine(saveFileDialog.InitialDirectory, ((ILayer)customProperty).Name + ".lyr");

      //get the layer name from the user
      DialogResult dr = saveFileDialog.ShowDialog();
      if (saveFileDialog.FileName != "" && dr == DialogResult.OK)
      {
        if (System.IO.File.Exists(saveFileDialog.FileName))
        {
          //try to delete the existing file
          System.IO.File.Delete(saveFileDialog.FileName);
        }

        //create a new LayerFile instance
        ILayerFile layerFile = new LayerFileClass();
        //create a new layer file
        layerFile.New(saveFileDialog.FileName);
        //attach the layer file with the actual layer
        layerFile.ReplaceContents((ILayer)customProperty);
        //save the layer file
        layerFile.Save();

        //ask the user whether he'd like to add the layer to the map
        if (DialogResult.Yes == MessageBox.Show("Would you like to add the layer to the map?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          mapControl.AddLayerFromFile(saveFileDialog.FileName, 0);
        }
      }
    }

    #endregion
  }
}
