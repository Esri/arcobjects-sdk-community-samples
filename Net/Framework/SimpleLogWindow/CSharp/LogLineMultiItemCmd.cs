/*

   Copyright 2016 Esri

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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;

namespace SimpleLogWindowCS
{
    [Guid("21532172-bc21-43eb-a2ad-bb6c333eff5e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SimpleLogWindowCS.LogLineMultiItemCmd")]
    public class LogLineMultiItemCmd : IMultiItem
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
            GxCommands.Register(regKey);
            GMxCommands.Register(regKey);
            MxCommands.Register(regKey);
            SxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxCommands.Unregister(regKey);
            GMxCommands.Unregister(regKey);
            MxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private System.Windows.Forms.ListBox m_targetListBox;

        #region "IMultiItem Implementations"
        public string Caption
        {
            get
            {
                return "Delete log by line (C#)";
            }
        }

        public int HelpContextID
        {
            get
            {
                return default(int);
            }
        }

        public string HelpFile
        {
            get
            {
                return default(string);
            }
        }

        public string Message
        {
            get
            {
                return "Delete a specific line in the simple log dockable window";
            }
        }

        public string Name
        {
            get
            {
                return "CSNETSamples_DeleteLogLineCommand";
            }
        }

        public void OnItemClick(int index)
        {
            if (index > -1)
                m_targetListBox.Items.RemoveAt(index);  //Delete the line
        }

        public int OnPopup(object hook)
        {
            IApplication app = hook as IApplication;

            //This command is designed to be on a context menu displayed when the 
            //logging window is right-clicked. Get the context item of the application
            IDocument doc = app.Document;
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
                IDockableWindowManager dockWindowManager = (IDockableWindowManager)app;
                dockWin = dockWindowManager.GetDockableWindow(logWindowID);
            }

             //Get list item count in the dockable window
            if (dockWin != null && dockWin.ID.Compare(logWindowID))
            {
                m_targetListBox = dockWin.UserData as System.Windows.Forms.ListBox;
                return m_targetListBox.Items.Count;
            }
            
            return 0; //failed or not applicable
        }

        public int get_ItemBitmap(int index)
        {
            return 0;
        }

        public string get_ItemCaption(int index)
        {
            if (index > -1)
            {
                string formatMessage = m_targetListBox.Items[index].ToString();
                if (formatMessage.Length > 25) //Trim display string
                {
                    formatMessage = formatMessage.Substring(0, 11) + "..." + formatMessage.Substring(formatMessage.Length - 11);
                }
                return string.Format("Delete line {0}: {1}", index + 1, formatMessage);
            }

            return "";
        }

        public bool get_ItemChecked(int index)
        {
            return false;
        }

        public bool get_ItemEnabled(int index)
        {
            return true;
        }
        #endregion

    }
}
