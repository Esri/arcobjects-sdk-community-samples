using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;

namespace DesktopPropertyPageCS
{
    /// <summary>
    /// A command shows a simplified layer properties dialog
    /// </summary>
    /// <remarks>Drag and drop this command to the (feature) layer context menu
    /// in ArcMap, ArcScene or ArcGlobe</remarks>
    [Guid("63c663c0-779f-498e-85dd-41d57788bf65")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DesktopPropertyPageCS.SimpleLayerPropertiesCmd")]
    public sealed class SimpleLayerPropertiesCmd : BaseCommand
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
            GMxCommands.Register(regKey);
            SxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            GMxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        private string m_layerCategoryID = string.Empty;

        public SimpleLayerPropertiesCmd()
        {
            base.m_category = ".NET Samples";
            base.m_caption = "Simple Layer Properties... (C#)";
            base.m_message = "Display a simplified layer property sheet";
            base.m_toolTip = "Simplified layer property sheet";
            base.m_name = "CSNETSamples_SimpleLayerPropCommand";
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
            if (m_application != null)
            {
                switch (m_application.Name)
                {
                    case "ArcMap":
                        m_layerCategoryID = "{1476c782-6f57-11d2-a2c6-080009b6f22b}";
                        break;
                    case "ArcScene":
                        m_layerCategoryID = "{3f82c99b-1c5f-11d4-a381-00c04f6bc619}";
                        break;
                    case "ArcGlobe":
                        m_layerCategoryID = "{720e21dc-2199-11d6-b2b3-00508bcdde28}";
                        break;
                    default:
                        base.m_enabled = false;
                        break;
                }
            }
            else
                base.m_enabled = false;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            IComPropertySheet myPropertySheet = new ComPropertySheetClass();
            myPropertySheet.Title = "Simplified Layer Properties (C#)";
            myPropertySheet.HideHelpButton = true;

            //Add by component category - all pages registered in the layer property page
            //UID layerPropertyID = new UIDClass();
            //layerPropertyID.Value = m_layerCategoryID;
            //myPropertySheet.AddCategoryID(layerPropertyID);

            //Or add page by page - but have to call Applies yourself
            myPropertySheet.ClearCategoryIDs();
            myPropertySheet.AddCategoryID(new UIDClass()); //a dummy empty UID
            myPropertySheet.AddPage(new LayerVisibilityPage()); //my custom page
            myPropertySheet.AddPage(new ESRI.ArcGIS.CartoUI.LayerDrawingPropertyPageClass()); //feature layer symbology

            //Pass in layer, active view and the application
            ISet propertyObjects = new SetClass();
            IBasicDocument basicDocument = m_application.Document as IBasicDocument;

            propertyObjects.Add(basicDocument.ActiveView);
            propertyObjects.Add(basicDocument.SelectedLayer); //or check ContextItem is a layer?
            propertyObjects.Add(m_application); //optional?

            //Show the property sheet
            if (myPropertySheet.CanEdit(propertyObjects))
                myPropertySheet.EditProperties(propertyObjects, m_application.hWnd);
        }


        #endregion
    }
}
