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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace Events
{
    public class EditorEventsDialogCommand : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private IDockableWindow m_dockableWindow;

        public EditorEventsDialogCommand()
        { 
           // SetupDockableWindow();
            UID windowID = new UIDClass();
            windowID.Value = @"ESRI_Employee_Events_EditorEventsDialog";
            m_dockableWindow = ArcMap.DockableWindowManager.GetDockableWindow(windowID);
        }

        #region Overriden Class Methods
        protected override void OnClick()
        {
            if (m_dockableWindow == null)
                return;

            m_dockableWindow.Show(!m_dockableWindow.IsVisible());
            Checked = m_dockableWindow.IsVisible();
        }

        protected override void OnUpdate()
        {
            this.Enabled = (m_dockableWindow != null);
            Checked = m_dockableWindow != null && m_dockableWindow.IsVisible();
        }
        #endregion
    }
}
