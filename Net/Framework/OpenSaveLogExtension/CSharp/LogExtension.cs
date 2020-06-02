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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace OpenSaveLogExtensionCS
{
    /// <summary>
    /// Simple extension that logs message when document is opened and saved
    /// </summary>
    [Guid("7d868fd7-f986-4347-bc93-b79751e12def")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("OpenSaveLogExtensionCS.LogExtension")]
    public class LogExtension : IExtension, IPersistVariant
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
            GMxExtensions.Register(regKey);
            SxExtensions.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxExtension.Unregister(regKey);
            GMxExtensions.Unregister(regKey);
            SxExtensions.Unregister(regKey);

        }

        #endregion
        #endregion
        private IApplication m_application;

        #region "Add Event Wiring for Open Documents"
        // Event member variables
        private IDocumentEvents_Event m_docEvents;

        // Wiring
        private void SetUpDocumentEvent(IDocument myDocument)
        {
            m_docEvents = myDocument as IDocumentEvents_Event;
            m_docEvents.OpenDocument += new IDocumentEvents_OpenDocumentEventHandler(OnOpenDocument); 

            //Optional, new and close document events
            m_docEvents.NewDocument += new IDocumentEvents_NewDocumentEventHandler(OnNewDocument);
            m_docEvents.CloseDocument += new IDocumentEvents_CloseDocumentEventHandler(OnCloseDocument);
        }

        void OnOpenDocument()
        {
            System.Diagnostics.Debug.WriteLine("Open Document", "Sample Extension (C#)");
            string logText = "Document '" + m_application.Document.Title + "'"
                            + " opened by " + Environment.UserName
                            + " at " + DateTime.Now.ToLongTimeString();

            LogMessage(logText);
        }

        void OnCloseDocument()
        {
            System.Diagnostics.Debug.WriteLine("Close Document", "Sample Extension (C#)");
        }
        void OnNewDocument()
        {
            System.Diagnostics.Debug.WriteLine("New Document", "Sample Extension (C#)");
        }
        #endregion

        #region "IExtension Implementations"
        public string Name
        {
            get
            {
                return "OpenSaveLogExtensionCS";
            }
        }

        public void Shutdown()
        {
            m_docEvents = null;
            m_application = null;
        }

        public void Startup(ref object initializationData)
        {
            m_application = initializationData as IApplication;
            SetUpDocumentEvent(m_application.Document);
        }
        #endregion

        #region "IPersistVariant Implementations"
        public UID ID
        {
            get
            {
                UID extUID = new UIDClass();
                extUID.Value = GetType().GUID.ToString("B");
                return extUID;
            }
        }

        public void Load(IVariantStream Stream)
        {
            Marshal.ReleaseComObject(Stream);
        }

        public void Save(IVariantStream Stream)
        {
            System.Diagnostics.Debug.WriteLine("Save Document", "Sample Extension (C#)");
            LogMessage("Document '" + m_application.Document.Title + "'"
                            + " saved by " + Environment.UserName
                            + " at " + DateTime.Now.ToLongTimeString());

            Marshal.ReleaseComObject(Stream);
        }
        #endregion

        private void LogMessage(string message)
        {
            m_application.StatusBar.set_Message(0, message);
        }
    }
}
