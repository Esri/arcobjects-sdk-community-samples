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
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.Diagnostics;

namespace TabbedFeatureInspector
{
  /// <summary>
  /// A command that attaches/detaches the 'tabbed inspector' extension class from 
  /// the feature class selected in the table of contents.
  /// In order to work correctly, the hosting application must implement and pass an 
  /// instance of IApplicationServices in the CustomProperty of its toolbar control.
  /// </summary>
  [Guid("14BAA8DD-677E-425b-B5CC-26F18B41D5B3")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TabbedFeatureInspectorCS.AttachTabbedInspectorExtensionCommand")]
  public sealed class AttachTabbedInspectorExtensionCommand : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction]
    [ComVisible(false)]
    public static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction]
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

    IHookHelper m_hookHelper;
    IApplicationServices m_appServices;

    public AttachTabbedInspectorExtensionCommand()
    {
      m_category = "Developer Samples";
      m_caption = "Attach/Detach Tabbed Inspector Extension CS";
      m_message = "This command attaches or detaches the Tabbed Inspector class extension from the selected feature class.";
      m_toolTip = "This command attaches or detaches the Tabbed Inspector class extension from the selected feature class.";
      m_name = "TabbedInspector_AttachDetachExtension_CS";
    }

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      m_hookHelper = new HookHelperClass();
      m_hookHelper.Hook = hook;
      m_appServices = null;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      try
      {
        GetApplicationServices();

        IFeatureLayer fl = m_appServices.GetLayerSelectedInTOC();

        if (fl != null)
          AlterClassExtension(fl.FeatureClass);
        else
          m_appServices.SetStatusMessage("Couldn't attach the 'custom inspector' extension. No feature layer was selected in the Table of Contents.", true);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: Could open the feature class. Original error: " + ex.Message);
      }
    }

    /// <summary>
    /// Obtains the IApplicationServices interface instance implemented by the hosting application.
    /// This is needed so the command can determine the selected layer, and update the application's status message.
    /// </summary>
    void GetApplicationServices()
    {
      if (m_appServices == null)
      {
        IToolbarControl2 toolbarControl = m_hookHelper.Hook as IToolbarControl2;
        if (toolbarControl == null)
          throw new ApplicationException(
            "Command appears to be running in an unexpected environment. Its hookHelper ought to be a toolbar control.");

        m_appServices = toolbarControl.CustomProperty as IApplicationServices;
        if (m_appServices == null)
          throw new ApplicationException(
            "Command appears to be running in an unexpected environment. The toolbar custom property ought to be an instance of IApplicationServices.");
      }
    }

    /// <summary>
    /// Perform the work contained in the delegate inside an exclusive schema lock.
    /// </summary>
    /// <param name="fc">The feature class whose schema is to be exclusively locked.</param>
    /// <param name="work">The work to be performed.</param>
    static void DoInSchemaLock(IFeatureClass fc, MethodInvoker work)
    {
      ISchemaLock schemaLock = (ISchemaLock)fc;
      try
      {
        // Exclusively lock the class schema.
        schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
        
        // Do the work inside the schema lock
        work();
      }
      finally
      {
        // Release the exclusive lock on the featureclass' schema.
        schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
      }
    }

    /// <summary>
    /// This method attaches or detaches the "TabbedInspector" class extension to/from the specified
    /// feature class. If the featureclass already has an extension class, and it isn't the 'TabbedInspector' extension,
    /// the method does not modify the class extension.
    /// </summary>
    /// <param name="fc">The feature class to be altered.</param>
    /// <returns>Whether the operation succeeded (successful detach or attach).</returns>
    bool AlterClassExtension(IFeatureClass fc)
    {
      // Attempt to get access to schema-editing functionality on the feature class
      IClassSchemaEdit classSchemaEdit = fc as IClassSchemaEdit;
      if (classSchemaEdit == null)
      {
        m_appServices.SetStatusMessage("The selected feature class doesn't support attaching an extension class.", true);
        return false;
      }

      // Create a UID object holding the TabbedInspector's CLSID
      UID classUID = new UIDClass();
      classUID.Value = "{" + TabbedInspector.TabbedInspectorCLSID + "}";

      // Do the schema update within a schema lock.
      bool succeeded = false;
      DoInSchemaLock(fc, delegate
        {

          // Does the feature class already have an extension class associated with it?
          if (fc.EXTCLSID != null)
          {
            // The featureclass already has an extension attached.
            if (fc.EXTCLSID.Value.Equals(classUID.Value))
            {
              // The extension is the TabbedInspector extension. Detach it.
              classSchemaEdit.AlterClassExtensionCLSID(null, null);

              m_appServices.SetStatusMessage(
                string.Format("The 'custom inspector' extension class was detached from {0}.", fc.AliasName), false);
              succeeded = true;
            }
            else
            {
              //Don't mess with featureclasses that have some other existing extension class associated with them.
              m_appServices.SetStatusMessage(
                string.Format("{0} already has another extension class attached to it. No change was made.", fc.AliasName), true);
              succeeded = false;
            }
          }
          else
          {
            // There is no extension attached to the featureclass. Attach the TabbedInspector extension.
            classSchemaEdit.AlterClassExtensionCLSID(classUID, null);
            m_appServices.SetStatusMessage(
              string.Format("The 'custom inspector' extension class was attached to {0}.", fc.AliasName), false);
            succeeded = true;
          }
        });

      
      return succeeded;
    }
  }
}
