using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace ESRIWebSitesCS
{
    /// <summary>
    /// Summary description for dockable window toggle command
    /// </summary>
    [Guid("6398a074-30c8-4eb8-b78d-bdde54292fd3")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ESRIWebSitesCS.CommandESRIWebsitesWindowCommand")]
    public sealed class ESRIWebsitesWindowCommand : BaseCommand
    {
        private IApplication m_application;
        private IDockableWindow m_dockableWindow;

        private const string DockableWindowGuid = "{ef536813-36f3-4a1b-8bdc-055f8e445330}";

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
            GxCommands.Register(regKey);
            SxCommands.Register(regKey);
            GMxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            GxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);
            GMxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        public ESRIWebsitesWindowCommand()
        {
            base.m_category = ".NET Samples";
            base.m_caption = "Toggle ESRI Resources Window (C#)";
            base.m_message = "Command toggles ESRI Resources window (C#)";
            base.m_toolTip = "Toggle ESRI resources dockable window";  
            base.m_name = "CSNETTemplate_ESRIWebsitesWindowCommand";  

            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
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
            if (hook != null)
                m_application = hook as IApplication;

            if (m_application != null)
            {
                SetupDockableWindow();
                base.m_enabled = m_dockableWindow != null;
            }
            else
                base.m_enabled = false;
        }

        /// <summary>
        /// Toggle visibility of dockable window and show the visible state by its checked property
        /// </summary>
        public override void OnClick()
        {
            if (m_dockableWindow == null)
                return;

            if (m_dockableWindow.IsVisible())
                m_dockableWindow.Show(false);
            else
                m_dockableWindow.Show(true);

            base.m_checked = m_dockableWindow.IsVisible();
        }

        public override bool Checked
        {
            get
            {
                return m_dockableWindow != null && m_dockableWindow.IsVisible();
            }
        }
        #endregion

        private void SetupDockableWindow()
        {
            if (m_dockableWindow == null)
            {
                IDockableWindowManager dockWindowManager = m_application as IDockableWindowManager;
                if (dockWindowManager != null)
                {
                    UID windowID = new UIDClass();
                    windowID.Value = DockableWindowGuid;
                    m_dockableWindow = dockWindowManager.GetDockableWindow(windowID);
                }
            }
        }
    }
}
