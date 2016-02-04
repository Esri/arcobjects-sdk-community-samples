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
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RecentFilesCommandsCS
{
    /// <summary>
    /// Summary description for CommandRecentFiles.
    /// </summary>
    [Guid("fbfdb761-c720-42a8-81d8-0447eb17ff0d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RecentFilesCommandsCS.CommandRecentFiles")]
    public sealed class CommandRecentFiles : BaseCommand
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
        public CommandRecentFiles()
        {
            base.m_category = ".NET Samples";
            base.m_caption = "Open Recent Files Dialog (C#)";
            base.m_message = "Select to open document from the recent files list";
            base.m_toolTip = "Recently opened files";
            base.m_name = "CSNETSamples_RecentFiles";

            try
            {
                base.m_bitmap = new Bitmap(GetType(), "OpenDocument.png");
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
            m_application = hook as IApplication;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            string[] recentFilePaths = RecentFilesRegistryHelper.GetRecentFiles(m_application);
            if (recentFilePaths.Length > 0)
            {
                //Populate the form with the files
                FormRecentFiles recentFileForm = new FormRecentFiles();
                recentFileForm.PopulateFileList(recentFilePaths);

                //Set up parent window for modal dialog using Application's hWnd
                NativeWindow parentWindow = new NativeWindow();
                parentWindow.AssignHandle(new IntPtr(m_application.hWnd));

                //Show dialog and open file if necessary
                if (recentFileForm.ShowDialog(parentWindow) == DialogResult.OK)
                {
                    string path = recentFileForm.lstFiles.SelectedItem.ToString();
                    if (System.IO.File.Exists(path))
                    {
                        m_application.OpenDocument(path);  
                    }
                    else
                    {
                        MessageBox.Show(string.Format("'{0}' cannot be found", path), "File doesn't exist");
                    }
                }
            }
        }

        #endregion
    }
}
