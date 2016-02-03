using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace CustomMenus
{
    /// <summary>
    /// Summary description for MyBaseMenu.
    /// </summary>
    [Guid("adb44f5a-b2e4-4c72-a2d4-b46a2c190f6e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomMenus.MyBaseMenu")]
    public sealed class MyBaseMenu : BaseMenu, ESRI.ArcGIS.Framework.IRootLevelMenu
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

        public MyBaseMenu()
        {
            //
            // TODO: Define your menu here by adding items
            //
            AddItem("esriArcMapUI.ZoomInFixedCommand");
            BeginGroup(); //Separator
            AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command

            BeginGroup(); //Add a separator.
            AddItem("CustomMenus.AddShapefile"); //Add custom functionality and add shapefile as a submenu.
        }

        public override string Caption
        {
          get
          {
            return "My_Menu";
          }
        }

        public override string Name
        {
            get
            {
                return "MyBaseMenu";
            }
        }
    }
}