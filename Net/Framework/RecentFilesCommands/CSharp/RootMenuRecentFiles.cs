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
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Framework;

namespace RecentFilesCommandsCS
{
    /// <summary>
    /// Summary description for RootMenuRecentFiles.
    /// </summary>
    [Guid("4e0552c8-ee58-4dda-b6be-c4eb6b9dd690")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RecentFilesCommandsCS.RootMenuRecentFiles")]
    public sealed class RootMenuRecentFiles : BaseMenu, IRootLevelMenu
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
            MxCommandBars.Register(regKey);
            GMxCommandBars.Register(regKey);
            SxCommandBars.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
            GMxCommandBars.Unregister(regKey);
            SxCommandBars.Unregister(regKey);

        }

        #endregion
        #endregion

        public RootMenuRecentFiles()
        {
            AddItem("RecentFilesCommandsCS.CommandRecentFiles");
            BeginGroup();
            AddItem("{8e5372ed-a185-4b52-9a2a-76f359107c58}");
        }

        public override string Caption
        {
            get
            {
                return "Recent Files (C#)";
            }
        }

        public override string Name
        {
            get
            {
                return "CSNETSamples_RecentFilesMenu";
            }
        }
    }
}