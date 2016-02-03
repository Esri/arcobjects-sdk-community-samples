using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace VertexCommands_CS
{
    /// <summary>
    /// Summary description for VertexCommandsMenu.
    /// </summary>
    [Guid("327A27A2-BED2-4a96-8F3A-2F6B1F258E46")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("VertexCommands_CS.VertexCommandsMenu")]
    public sealed class VertexCommandsMenu : BaseMenu
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
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsMenus.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsMenus.Unregister(regKey);
        }

        #endregion
        #endregion

        #region class constructor

        public VertexCommandsMenu()
        {

            AddItem("VertexCommands_CS.CustomVertexCommands", 1);   //Custom Insert vertex
            AddItem("VertexCommands_CS.CustomVertexCommands", 2);   //Custom Delete vertex 
            AddItem("VertexCommands_CS.UsingOutOfBoxVertexCommands", 1);   //Out-of-Box Insert vertex
            AddItem("VertexCommands_CS.UsingOutOfBoxVertexCommands", 2);   //Out-of-Box Delete vertex 


        }
        #endregion

        #region overridden methods

        public override string Caption
        {
            get
            {
                return "Vertex Tools";
            }
        }
        public override string Name
        {
            get
            {
                return "VertexCommandsMenu";
            }
        }
        #endregion
    }
}