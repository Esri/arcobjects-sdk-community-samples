using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Editor;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AngleAngle
{
  [Guid("05b14e90-8b55-11dd-ad8b-0800200c9a66")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("AngleAngle.AngleAngleCmd")]

  public sealed class AngleAngleCmd : BaseCommand
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

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private IApplication m_application;
    private IEditor3 m_editor;
    private IEditSketch3 m_edSketch;
    

    public AngleAngleCmd()
    {
      m_category = "Developer Samples";
      m_caption = "Angle-Angle Shape Constructor (C#)";
      m_message = "Adds a point to the edit sketch based on intersection.";
      m_name = "ShapeConstructor_AngleAngle";
      m_toolTip = "Angle-Angle";
      m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("AngleAngle.AngleAngle.bmp"));
    }

    #region Overriden Class Methods
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      m_application = hook as IApplication;

      //get the editor
      UID editorUid = new UID();
      editorUid.Value = "esriEditor.Editor";
      m_editor = m_application.FindExtensionByCLSID(editorUid) as IEditor3;
    }

    public override void OnClick()
    {
      // Create the constructor, pass the editor and set as current constructor
      m_edSketch = m_editor as IEditSketch3;
      AngleAngleCstr aac = new AngleAngleCstr();
      aac.Initialize(m_editor);
      m_edSketch.ShapeConstructor = aac;
    }

    public override bool Enabled
    {
      // Enable the command if we are editing
      get {return (m_editor.EditState == esriEditState.esriStateEditing);}
    }

    public override bool Checked
    {
      get
      {
        // Check the command/button if we are the current constructor
        IPersist ptemp = m_edSketch.ShapeConstructor as IPersist;
        Guid pg;
        ptemp.GetClassID(out pg);
        return (pg.ToString() == "edb83080-999d-11de-8a39-0800200c9a66");
      }
    }

    #endregion
  }
}
