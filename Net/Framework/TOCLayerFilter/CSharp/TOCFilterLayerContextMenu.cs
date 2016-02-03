using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Framework;

namespace TOCLayerFilterCS
{
    /// <summary>
    /// Summary description for TOCFilterLayerContextMenu.
    /// </summary>
    [Guid("30cb4a78-6eba-4f60-b52e-38bc02bacc89")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TOCLayerFilterCS.TOCFilterLayerContextMenu")]
    public sealed class TOCFilterLayerContextMenu : BaseMenu, IShortcutMenu
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
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public TOCFilterLayerContextMenu()
        {
            AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 7); //Layer Zoom
            AddItem("{BF64319A-9062-11D2-AE71-080009EC732A}", 4); //Table
            BeginGroup(); //Separator
            AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 8); //Save to file
            BeginGroup(); //Separator
            AddItem("{18DF94D9-0F8A-11D2-94B1-080009EEBECB}", 9); //Properties
        }

        public override string Caption
        {
            get
            {
                return "Custom TOC Layer Filter Menu (C#)";
            }
        }
        public override string Name
        {
            get
            {
                return "CSNETSamples_TOCFilterMenu";
            }
        }
    }
}