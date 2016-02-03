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
