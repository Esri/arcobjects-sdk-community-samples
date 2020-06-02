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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;

namespace SimpleLogWindowCS
{
    /// <summary>
    /// Summary description for ClearLoggingCommand.
    /// </summary>
    [Guid("b5820a63-e3d4-42a1-91c5-d90eacc3985b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SimpleLogWindowCS.ClearLoggingCommand")]
    public sealed class ClearLoggingCommand : BaseCommand
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
            GxCommands.Register(regKey);
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
            GxCommands.Unregister(regKey);
            GMxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        public ClearLoggingCommand()
        {
            base.m_category = ".NET Samples";
            base.m_caption = "Clear Log (C#)";
            base.m_message = "Clear items in logging dockable window";
            base.m_toolTip = "Clear log";
            base.m_name = "CSNETSamples_ClearLogCommand";
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
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            //This command is designed to be on a context menu displayed when the 
            //logging window is right-clicked. Get the context item of the application
            IDocument doc = m_application.Document;
            object contextItem = null;
            if (doc is IBasicDocument)
            {
                contextItem = ((IBasicDocument)doc).ContextItem;
            }

            IDockableWindow dockWin = null;
            UID logWindowID = new UIDClass();
            logWindowID.Value = "{600cb3c8-e9d8-4c20-b2c7-f97082b10f92}";

            if (contextItem != null && contextItem is IDockableWindow)
            {
                dockWin = (IDockableWindow)contextItem;
            }
            else //In the case of ArcCatalog or the command has been placed outside the designated context menu
            {
                //Get the dockable window directly
                IDockableWindowManager dockWindowManager = (IDockableWindowManager)m_application;
                dockWin = dockWindowManager.GetDockableWindow(logWindowID);
            }

            //Clear list items in the dockable window
            if (dockWin != null && dockWin.ID.Compare(logWindowID))
            {
                System.Windows.Forms.ListBox containedBox = dockWin.UserData as System.Windows.Forms.ListBox;
                containedBox.Items.Clear();
            }

        }

        #endregion
    }
}
