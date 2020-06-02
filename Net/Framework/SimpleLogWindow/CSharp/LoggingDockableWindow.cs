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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;

namespace SimpleLogWindowCS
{
    /// <summary>
    /// Simple dockable window with a listbox control which can be used by 
    /// other custom components to log messages. The listcontrol can be accessed 
    /// via the UserData property.
    /// </summary>
    [Guid("600cb3c8-e9d8-4c20-b2c7-f97082b10f92")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SimpleLogWindowCS.LoggingDockableWindow")]
    public partial class LoggingDockableWindow : UserControl, IDockableWindowDef
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
            MxDockableWindows.Register(regKey);
            GxDockableWindows.Register(regKey);
            GMxDockableWindows.Register(regKey);
            SxDockableWindows.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Unregister(regKey);
            GxDockableWindows.Unregister(regKey);
            GMxDockableWindows.Unregister(regKey);
            SxDockableWindows.Unregister(regKey);

        }

        #endregion
        #endregion
        
        private IApplication m_application;

        public LoggingDockableWindow()
        {
            InitializeComponent();
        }

        #region IDockableWindowDef Members

        string IDockableWindowDef.Caption
        {
            get
            {
                return "Simple Logging (C#)";
            }
        }

        int IDockableWindowDef.ChildHWND
        {
            get { return this.Handle.ToInt32(); }
        }

        string IDockableWindowDef.Name
        {
            get
            {
                return "SimpleLoggingDockableWindowCS";
            }
        }

        void IDockableWindowDef.OnCreate(object hook)
        {
            m_application = hook as IApplication;
        }

        void IDockableWindowDef.OnDestroy()
        {
            m_application = null;
        }

        object IDockableWindowDef.UserData
        {
            get
            {
                return lstPendingMessage;
            }
        }

        #endregion

        private void radCtxMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (radDotNetCtx.Checked)
            {
                lstPendingMessage.ContextMenuStrip = ctxMenuStrip;
                lstPendingMessage.MouseUp -= new MouseEventHandler(lstPendingMessage_MouseUp);
            }
            else
            {
                if (lstPendingMessage.ContextMenuStrip != null)
                {
                    lstPendingMessage.ContextMenuStrip = null;
                    lstPendingMessage.MouseUp += new MouseEventHandler(lstPendingMessage_MouseUp);
                }
            }
        }

        #region Handling private pure .Net context menu strip
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstPendingMessage.Items.Clear();
        }

        private void ctxMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //Delete first
            ctxMenuStrip.Items.Clear();

            //Add items
            ctxMenuStrip.Items.Add(clearToolStripMenuItem);
            ctxMenuStrip.Items.Add(toolStripSeparator1);
            
            for (int i = 0; i < lstPendingMessage.Items.Count; i++ )
            {
                string formatMessage = lstPendingMessage.Items[i].ToString();
                if (formatMessage.Length > 25) //Trim display string
                {
                    formatMessage = formatMessage.Substring(0, 11) + "..." + formatMessage.Substring(formatMessage.Length - 11);
                }
                ctxMenuStrip.Items.Add(string.Format("Delete line {0}: {1}", i + 1, formatMessage));
            }           
        }

        private void ctxMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != clearToolStripMenuItem)
            {
                //Get "index" to remove the line
                int lineNum = ctxMenuStrip.Items.IndexOf(e.ClickedItem);
                lstPendingMessage.Items.RemoveAt(lineNum);
            }
        }
        #endregion

        private void lstPendingMessage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lstPendingMessage.ContextMenuStrip == null)
            {
                //Before showing the context menu, set ContextItem to be the actual dockable window 
                //which represents this definition class in the framework
                IDocument doc = m_application.Document;
                IDockableWindowManager windowManager = m_application as IDockableWindowManager;
                UID dockwinID = new UIDClass();
                dockwinID.Value = this.GetType().GUID.ToString("B");
                IDockableWindow frameworkWindow = windowManager.GetDockableWindow(dockwinID);
                if (doc is IBasicDocument) //ArcMap, ArcScene and ArcGlobe
                {
                    ((IBasicDocument)doc).ContextItem = frameworkWindow;
                }

                //Get the context menu to show
                ICommandBars documentBars = m_application.Document.CommandBars;
                ICommandBar ctxMenu = null;
                if (radDynamic.Checked) //Create context menu dynamically
                {
                    //Disadvantage(s): 
                    //1. ICommandBars.Create will set document to dirty
                    //2. Cannot insert separator
                    ctxMenu = documentBars.Create("DockableWindowCtxTemp", esriCmdBarType.esriCmdBarTypeShortcutMenu); //This sets document flag to dirty :(

                    //Add commands to context menu
                    UID cmdID = new UIDClass();
                    object idx = Type.Missing;

                    cmdID.Value = "{b5820a63-e3d4-42a1-91c5-d90eacc3985b}"; //ClearLoggingCommand
                    ctxMenu.Add(cmdID, ref idx);

                    cmdID.Value = "{21532172-bc21-43eb-a2ad-bb6c333eff5e}"; //LogLineMultiItemCmd
                    ctxMenu.Add(cmdID, ref idx);
                }
                else    //Use predefined context menu
                {
                    UID menuID = new UIDClass();
                    menuID.Value = "{c6238198-5a2a-4fe8-bff0-e2f574f6a6cf}"; //LoggingWindowCtxMnu
                    ICommandItem locateMenu = documentBars.Find(menuID, false, false);
                    if (locateMenu != null)
                        ctxMenu = locateMenu as ICommandBar;
                }

                //Pop up context menu at screen location
                Point scrnPoint = lstPendingMessage.PointToScreen(e.Location); 
                if (ctxMenu != null)
                    ctxMenu.Popup(scrnPoint.X, scrnPoint.Y);
            }
        }

        /// <summary>
        /// Enter text into listbox by listening to the Enter key. KeyPress won't be fired for enter key
        /// so listening to key up event instead
        /// </summary>
        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (!string.IsNullOrEmpty(txtInput.Text.Trim()))
                {
                    lstPendingMessage.Items.Add(txtInput.Text);
                    txtInput.Clear();
                    e.Handled = true;
                }
            }
        }
    }
}
