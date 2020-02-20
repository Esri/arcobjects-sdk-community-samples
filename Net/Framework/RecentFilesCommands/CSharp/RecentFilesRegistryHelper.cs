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
using ESRI.ArcGIS.Framework;
using Microsoft.Win32;

namespace RecentFilesCommandsCS
{
    /// <summary>
    /// Helper class to process recent file lists stored in the registry
    /// </summary>
    class RecentFilesRegistryHelper
    {
        const string RecentFileRegistryKeyPath = @"Software\ESRI\Desktop{0}\{1}\Recent File List";
        public static string[] GetRecentFiles(IApplication app)
        {
            List<string> recentFilePaths = new List<string>();

            //Read the registry to get the recent file list
            string version = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Version;
            string openKey = string.Format(RecentFileRegistryKeyPath, version, app.Name);
            RegistryKey recentListKey = Registry.CurrentUser.OpenSubKey(openKey);
            if (recentListKey != null)
            {
                string[] listNames = recentListKey.GetValueNames();
                foreach (string name in listNames)
                {
                    string fileName = recentListKey.GetValue(name, string.Empty).ToString();
                    if (!string.IsNullOrEmpty(fileName))
                        recentFilePaths.Add(fileName);
                }
            }

            return recentFilePaths.ToArray();
        }
    }
}
