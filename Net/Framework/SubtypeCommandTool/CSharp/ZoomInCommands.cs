using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace CommandSubtypeCS
{
    /// <summary>
    /// Summary description for ZoomInCommands.
    /// </summary>
    [Guid("b280f977-a3fe-40f1-9826-522a025e5923")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CommandSubtypeCS.ZoomInCommands")]
    public sealed class ZoomInCommands : BaseCommand, ICommandSubType
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
            MxCommands.Register(regKey);
            GMxCommands.Register(regKey);
            SxCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            GMxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IApplication m_application;
        private int m_subtype;
        private ICommandItem m_xoomCommand;
        public ZoomInCommands()
        {
            //Set up common properties
            base.m_category = ".NET Samples";
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            //Get the appropriate zoom in command based on application
            m_application = hook as IApplication;
            base.m_enabled = false;

            if (m_application != null)
            {
                UID cmdUID = new UIDClass();
                switch (m_application.Name) //or test by casting the appropriate application interface
                {
                    case "ArcMap":
                        cmdUID.Value = "esriArcMapUI.ZoomInFixedCommand";
                        break;
                    case "ArcGlobe":
                        cmdUID.Value = "esriArcGlobe.GMxNarrowFOVCommand";
                        break;
                    case "ArcScene":
                        cmdUID.Value = "esriArcScene.SxNarrowFOVCommand";
                        break;
                }

                ICommandBars docBars = m_application.Document.CommandBars;
                m_xoomCommand = docBars.Find(cmdUID, false, false);

                //Enable only when the delegate command is available
                base.m_enabled = m_xoomCommand != null;
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            for (int i = 0; i < m_subtype; i++)
                m_xoomCommand.Execute();
        }

        #endregion

        #region ICommandSubType Members

        public int GetCount()
        {
            return 3;
        }

        public void SetSubType(int SubType)
        {
            //Called by framework to indicate the subtype command the application is referring to
            //Subtype starts from 1
            m_subtype = SubType;

            //Set up bitmap, caption, tooltip etc.
            if (base.Bitmap == 0)
            {
                switch (m_subtype)
                {
                    case 1:
                        base.m_bitmap = Properties.Resources.ZoomOnce;
                        break;
                    case 2:
                        base.m_bitmap = Properties.Resources.ZoomTwice;
                        break;
                    case 3:
                        base.m_bitmap = Properties.Resources.ZoomThrice;
                        break;
                }
            }
            base.m_caption = string.Format("Fixed zoom in x{0} (C#)", m_subtype.ToString());
            base.m_name = string.Format("CSNETSamples_SubTypeCommand{0}", m_subtype);
            base.m_message = string.Format("Executing fixed zoom in {0} time(s)", m_subtype.ToString());
            base.m_toolTip = string.Format("Fixed Zoom in x{0}", m_subtype);
        }

        #endregion
    }
}
