using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

namespace DesktopExtensionsCS
{
    [Guid("e53757bc-a6ed-4283-8e53-9c7f242c5c8a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DesktopExtensionsCS.SimpleExtension")]
    public class SimpleExtension : IExtension
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
            MxExtension.Register(regKey);
            SxExtensions.Register(regKey);
            GxExtensions.Register(regKey);
            GMxExtensions.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxExtension.Unregister(regKey);
            SxExtensions.Unregister(regKey);
            GxExtensions.Unregister(regKey);
            GMxExtensions.Unregister(regKey);

        }

        #endregion
        #endregion
        private IApplication m_application;

        #region IExtension Members

        /// <summary>
        /// Name of extension. Do not exceed 31 characters
        /// </summary>
        public string Name { get { return "SimpleExtensionCS"; } }

        public void Shutdown()
        {
            //Clean up resources
            m_docEvents = null;
            m_application = null;
        }

        public void Startup(ref object initializationData)
        {
            m_application = initializationData as IApplication;
            if (m_application == null)
                return;

            //Listening to document events
            SetUpDocumentEvent(m_application.Document);
        }

        #endregion

        #region Document events
        //Event member variable.
        private ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event m_docEvents = null;

        //Wiring.
        private void SetUpDocumentEvent(ESRI.ArcGIS.Framework.IDocument myDocument)
        {
            m_docEvents = myDocument as ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event;
            m_docEvents.NewDocument += new ESRI.ArcGIS.ArcMapUI.IDocumentEvents_NewDocumentEventHandler(OnNewDocument);
            m_docEvents.OpenDocument += new ESRI.ArcGIS.ArcMapUI.IDocumentEvents_OpenDocumentEventHandler(OnOpenDocument);
        }

        //Event handler methods.
        void OnNewDocument()
        {
            System.Diagnostics.Debug.WriteLine("New document event (C# Sample)");
        }

        void OnOpenDocument()
        {
            System.Diagnostics.Debug.WriteLine("Open document event (C# Sample)");
        }
        #endregion

    }
}
