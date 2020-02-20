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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF;

namespace Events
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class EditorEventsDialog : UserControl
    {
        internal TabPage m_selectTab;
        internal TabPage m_listenTab;
        internal TabControl m_TabControl;
        internal static EditorEventsDialog e_dockWinForm;
        EventListener eventListener;
        private ESRI.ArcGIS.Editor.IEditor m_editor;

        public EditorEventsDialog(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
            e_dockWinForm = this;
            //get a reference to the editor
            UID uid = new UIDClass();
            uid.Value = "esriEditor.Editor";
            m_editor = ArcMap.Application.FindExtensionByCLSID(uid) as ESRI.ArcGIS.Editor.IEditor;

            m_TabControl = e_dockWinForm.tabControl1;
            System.Collections.IEnumerator e = m_TabControl.TabPages.GetEnumerator();
            e.MoveNext();
            m_listenTab = e.Current as TabPage;
            e.MoveNext();
            m_selectTab = e.Current as TabPage;

            CheckedListBox editEventList = m_selectTab.GetNextControl(m_selectTab, true) as CheckedListBox;
            editEventList.ItemCheck += new ItemCheckEventHandler(editEventList_ItemCheck);

            ListBox listEvent = m_listenTab.GetNextControl(m_listenTab, true) as ListBox;
            listEvent.MouseDown += new MouseEventHandler(listEvent_MouseDown);

            eventListener = new EventListener(m_editor);

            eventListener.Changed += new ChangedEventHandler(eventListener_Changed);

            //populate the editor events
            editEventList.Items.AddRange(Enum.GetNames(typeof(EventListener.EditorEvent)));
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }
      
        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private EditorEventsDialog m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new EditorEventsDialog(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }
        }

        void listEvent_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.lstEditorEvents.Items.Clear();
                this.lstEditorEvents.Refresh();
            }
        }

        void eventListener_Changed(object sender, EditorEventArgs e)
        {
            ((ListBox)m_listenTab.GetNextControl(m_listenTab, true)).Items.Add(e.eventType.ToString());
        }

        void editEventList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            // start or stop listening for event based on checked state
            eventListener.ListenToEvents((Events.EventListener.EditorEvent)e.Index, e.NewValue == CheckState.Checked);
        }
    }
}
