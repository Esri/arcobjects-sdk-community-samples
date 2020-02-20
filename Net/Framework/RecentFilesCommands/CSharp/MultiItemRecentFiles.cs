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
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using Microsoft.Win32;
using System.IO;

namespace RecentFilesCommandsCS
{
    [Guid("8e5372ed-a185-4b52-9a2a-76f359107c58")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RecentFilesCommandsCS.MultiItemRecentFiles")]
    public class MultiItemRecentFiles : IMultiItem
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
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
            GMxCommands.Unregister(regKey);
            MxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        private string[] m_filePaths = null;

        #region "IMultiItem Implementations"
        public string Caption
        {
            get
            {
                return "Recent Files MultiItem (C#)";
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
                return default(string);
            }
        }

        public string Name
        {
            get
            {
                return "CSNETSamples_RecentFilesMultiItem";
            }
        }

        public void OnItemClick(int index)
        {
            m_application.OpenDocument(m_filePaths[index]);
        }

        public int OnPopup(object hook)
        {
            //The incoming hook is the application, determine the number of
            //items by getting the recent files from the registry
            m_application = hook as IApplication;
            m_filePaths = RecentFilesRegistryHelper.GetRecentFiles(m_application);
            return m_filePaths.Length;
        }

        public int get_ItemBitmap(int index)
        {
            return default(int);
        }

        public string get_ItemCaption(int index)
        {
            //Caption of item is "#. <File path>"
            return string.Format("&{0}. {1}", index + 1, m_filePaths[index]);
        }

        public bool get_ItemChecked(int index)
        {
            return false;
        }

        public bool get_ItemEnabled(int index)
        {
            //check if file exists
            return File.Exists(m_filePaths[index]);
        }
        #endregion

    }
}
