using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;

namespace Core
{
  /// <summary>
  /// Launches the SnapEditorForm
  /// </summary>
  [Guid("F83AE05F-3E88-428A-B2E2-C6428081C61A")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("SnapCommands_CS.SnapSettingsCommand")]
  public sealed class SnapSettingsCommand : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    public static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);      
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    public static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    SnapEditor snapEditor;

    public SnapSettingsCommand()
    {
    }
     

    #region ICommand Members

    public override void OnClick()
    {
      //The snap editor form requires an edit session (e.g. in order to read the target layer and to set the snap tips)
      IEngineEditor editor = new EngineEditorClass();
      if (editor.EditState != esriEngineEditState.esriEngineStateEditing)
      {
        System.Windows.Forms.MessageBox.Show("Please start an edit session");
        return;
      }

      //Show the snap settings form
      if (snapEditor == null || snapEditor.IsDisposed)
      {        
        snapEditor = new SnapEditor();          
      }
      snapEditor.Show();
      snapEditor.BringToFront();
    }

    public override string Message
    {
      get
      {
        return "SnapSettingsCommand";
      }
    }

    public override int Bitmap
    {
      get
      {
        return 0;
      }
    }

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;         
    }

    public override string Caption
    {
      get
      {
        return "SnapSettingsCommandCS";
      }
    }

    public override string Tooltip
    {
      get
      {
        return "SnapSettingsCommand";
      }
    }

    public override int HelpContextID
    {
      get
      {
        return 0;
      }
    }

    public override string Name
    {
      get
      {
        return "SnapCommands_CS_SnapSettingsCommand";
      }
    }

    public override bool Checked
    {
      get
      {
        return false;
      }
    }

    public override bool Enabled
    {
      get
      {
        return true;
      }
    }

    public override string HelpFile
    {
      get
      {
        return null;
      }
    }

    public override string Category
    {
      get
      {
        return "Developer Samples";
      }
    }
    #endregion
  }
}