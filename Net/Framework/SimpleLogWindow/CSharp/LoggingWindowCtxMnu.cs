using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Framework;

namespace SimpleLogWindowCS
{
    /// <summary>
    /// Summary description for LoggingWindowCtxMnu.
    /// </summary>
    [Guid("c6238198-5a2a-4fe8-bff0-e2f574f6a6cf")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SimpleLogWindowCS.LoggingWindowCtxMnu")]
    public sealed class LoggingWindowCtxMnu : BaseMenu, IShortcutMenu
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
            MxCommandBars.Register(regKey);
            GxCommandBars.Register(regKey);
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
            GxCommandBars.Unregister(regKey);
            GMxCommandBars.Unregister(regKey);
            SxCommandBars.Unregister(regKey);

        }

        #endregion
        #endregion

        public LoggingWindowCtxMnu()
        {
            AddItem("{b5820a63-e3d4-42a1-91c5-d90eacc3985b}"); //Clear log command
            //BeginGroup(); //Separator
            AddItem("{21532172-bc21-43eb-a2ad-bb6c333eff5e}"); //Delete log by line multiItem
        }

        public override string Caption
        {
            get
            {
                return "Logging Window Context Menu (C#)";
            }
        }
        public override string Name
        {
            get
            {
                return "CSNETSamples_LoggingWindowCtxMnu";
            }
        }
    }
}