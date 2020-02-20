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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.esriSystem;

namespace TabbedFeatureInspector
{
  /// <summary>
  /// Summary description for AddEXTCLSID.
  /// </summary>
  [Guid("ffa4371f-59ca-4545-99db-8580a7445b12")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TabbedFeatureInspector.AddEXTCLSID")]
  public sealed class AddEXTCLSID : BaseCommand
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
    public AddEXTCLSID()
    {

      base.m_category = "Developer Samples"; 
      base.m_caption = "Add EXTCLSID";  
      base.m_message = "This command adds the GUID of the project to the EXTCLSID";  
      base.m_toolTip = "Adds EXTCLSID to feature class";  
      base.m_name = "TabbedFeatureInspector_AddEXTCLSID";   

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

      m_application = hook as IApplication;

      //Disable if it is not ArcMap
      if (hook is IMxApplication)
        base.m_enabled = true;
      else
        base.m_enabled = false;     
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {
        IGxDialog gxDialog = new GxDialogClass();
        IGxObjectFilter mdbFilter = new GxFilterPGDBFeatureClasses();
        gxDialog.ObjectFilter = mdbFilter;
        object temp = "C:\\";
        gxDialog.set_StartingLocation(ref temp);
        gxDialog.Title = "Pick the feature class you want to add the ext clsid to";
        gxDialog.RememberLocation = true;
        IEnumGxObject pEnumGx;
      
        if (!gxDialog.DoModalOpen(0, out pEnumGx))
          return;
     
        IGxObject gdbObj = pEnumGx.Next();
        //Make sure there was only one GxObject in the enum.
        if (pEnumGx.Next() != null) 
          return;
        //Get the Name for the internal object that this GxObject represents.
        IName fcName  = gdbObj.InternalObjectName;
        //Opens the object referred to by this name
        IFeatureClass featClass = (IFeatureClass)fcName.Open();
        //Procedure to add the class id to the feature class internally.
        IClassSchemaEdit_Example(featClass);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error: Could open the feature class. Original error: " + ex.Message);
        }
      }
    
    public void IClassSchemaEdit_Example(IObjectClass objectClass) 
    {        
      //This function shows how you can use the IClassSchemaEdit   
      //interface to alter the COM class extension for an object class.    
      //cast for the IClassSchemaEdit      
      IClassSchemaEdit classSchemaEdit = (IClassSchemaEdit)objectClass;
        //set and exclusive lock on the class     
      ISchemaLock schemaLock = (ISchemaLock)objectClass;  
      schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
        ESRI.ArcGIS.esriSystem.UID classUID = new ESRI.ArcGIS.esriSystem.UIDClass();
      //GUID for the C# project.
        classUID.Value = "{65a43962-8cc0-49c0-bfa3-015d0ff8350e}";
        classSchemaEdit.AlterClassExtensionCLSID(classUID, null);    
      //release the exclusive lock     
      schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);  
    }
    #endregion
  }
}
