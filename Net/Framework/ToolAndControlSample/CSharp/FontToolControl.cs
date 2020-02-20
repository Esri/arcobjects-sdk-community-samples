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
using System.Drawing.Text;
using ESRI.ArcGIS.SystemUI;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace ToolAndControlSampleCS
{
    [Guid("F5248F40-385B-4a12-8092-1204A2692C94")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ToolAndControlSampleCS.FontToolControl")]
    public partial class FontToolControl : UserControl, ICommand, IToolControl
    {
        private IApplication m_application;
        private InstalledFontCollection m_ifc;
        private IntPtr m_hBitmap;
        private ICompletionNotify m_completeNotify;

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

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

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        public FontToolControl()
        {
            InitializeComponent();

            //Set up bitmap - Note clean up is done in Dispose method instead of destructor
            m_hBitmap = Properties.Resources.FontIcon.GetHbitmap(Color.Magenta);
        }

        /// <summary>
        /// Custom drawing when item is selected in the drop down to render
        /// the actual font
        /// </summary>
        private void cboFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            FontFamily ff = (FontFamily)cboFont.Items[e.Index];
            Font f;
            if (((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                && ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit))
            {
                //Determine font to render text
                if (ff.IsStyleAvailable(System.Drawing.FontStyle.Regular))
                    f = new Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Regular);
                else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Bold))
                    f = new Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Bold);
                else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Italic))
                    f = new Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Italic);
                else
                    f = new Font(ff.GetName(1), cboFont.Font.Size, System.Drawing.FontStyle.Underline);

                e.DrawFocusRectangle();
            }
            else
                f = cboFont.Font;

            // Draw the item 
            e.Graphics.DrawString(ff.Name, f, SystemBrushes.ControlText, e.Bounds.X, e.Bounds.Y);
        }
        private void cboFont_DropDownClosed(object sender, EventArgs e)
        {
            if (m_completeNotify != null)
                m_completeNotify.SetComplete();
        }
        
        #region ICommand Members

        int ICommand.Bitmap
        {
            get { return m_hBitmap.ToInt32(); }
        }

        string ICommand.Caption
        {
            get { return "Font Dropdown (C#)"; }
        }

        string ICommand.Category
        {
            get { return ".NET Samples"; }
        }

        bool ICommand.Checked
        {
            get { return false; }
        }

        bool ICommand.Enabled
        {
            get 
            { 
                return true;
            }
        }

        int ICommand.HelpContextID
        {
            get { return 0; }
        }

        string ICommand.HelpFile
        {
            get { return string.Empty; }
        }

        string ICommand.Message
        {
            get { return "Document Font dropdown list"; }
        }

        string ICommand.Name
        {
            get { return "CSNETSamples_FontToolControl"; }
        }

        void ICommand.OnClick()
        {

        }

        void ICommand.OnCreate(object hook)
        {
            //Set up data for the dropdown
            m_ifc = new InstalledFontCollection();
            cboFont.DataSource = m_ifc.Families;
            cboFont.ValueMember = "Name";

            m_application = hook as IApplication;

            //TODO: Uncomment the following lines if you want the control to sync with default document font
            //OnDocumentSession();
            //SetUpDocumentEvent(m_application.Document);
            //cboFont.SelectedValueChanged += new EventHandler(cboFont_SelectedValueChanged);
        }

        string ICommand.Tooltip
        {
            get { return "Font (C#)"; }
        }

        #endregion

        #region IToolControl Members

        bool IToolControl.OnDrop(esriCmdBarType barType)
        {
            OnDocumentSession(); //Initialize the font 
            return true;
        }

        void IToolControl.OnFocus(ICompletionNotify complete)
        {
            m_completeNotify = complete;

            //Can also do any last minute UI update here
        }

        int IToolControl.hWnd
        {
            get { return this.Handle.ToInt32(); }
        }

        #endregion

        #region Optional implementation to set document default font

        /// <summary>
        /// Optional, wire the cboFont's SelectedValueChanged event if you want
        /// to use this tool control to set the default text font of the document
        /// </summary>
        private void cboFont_SelectedValueChanged(object sender, EventArgs e)
        {
            //Change document default text font
            if (m_application != null)
            {
                IMxDocument mxDoc = m_application.Document as IMxDocument;
                if (!mxDoc.DefaultTextFont.Name.Equals(cboFont.SelectedValue.ToString()))
                {
                    FontFamily ff = (FontFamily)cboFont.SelectedItem;
                   
                    //Use the stdole to create the font
                    stdole.IFontDisp newFont = new stdole.StdFontClass() as stdole.IFontDisp;
                    newFont.Name = ff.GetName(1);
                    newFont.Size = mxDoc.DefaultTextFont.Size;

                    //Alternative: Create a .Net Font object then convert
                    //Font f = new Font(ff.GetName(1), (float)mxDoc.DefaultTextFont.Size);
                    //stdole.IFontDisp newFont = (stdole.IFontDisp)ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(f);

                    //Set other font properties
                    if (mxDoc.DefaultTextFont.Bold)
                        newFont.Bold = true;
                    if (mxDoc.DefaultTextFont.Italic)
                        newFont.Italic = true;
                    if (mxDoc.DefaultTextFont.Underline)
                        newFont.Underline = true;
                    if (mxDoc.DefaultTextFont.Strikethrough)
                        newFont.Strikethrough = true;

                    mxDoc.DefaultTextFont = newFont;
                    
                    //Set dirty flag with change
                    IDocumentDirty docDirty = mxDoc as IDocumentDirty;
                    docDirty.SetDirty();
                }
            }
        }

        #region Document event handling
        private IDocumentEvents_Event m_docEvents = null;   //Event member variable.

        private void SetUpDocumentEvent(IDocument myDocument)
        {
            m_docEvents = myDocument as IDocumentEvents_Event;
            m_docEvents.NewDocument += new IDocumentEvents_NewDocumentEventHandler(OnDocumentSession);
            m_docEvents.OpenDocument += new IDocumentEvents_OpenDocumentEventHandler(OnDocumentSession);
        }

        void OnDocumentSession()
        {
            //Get the default document font and update listbox
            IMxDocument mxDoc = m_application.Document as IMxDocument;
            string defaultFontName = mxDoc.DefaultTextFont.Name;
            cboFont.SelectedValue = defaultFontName;
        }
        #endregion
        #endregion
    }

   
}
