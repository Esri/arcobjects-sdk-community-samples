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
using ESRI.ArcGIS.ADF.CATIDs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;

namespace VBCSharpCultureSample
{
    [Guid("37259af4-14ce-4579-b0e4-6a264cd4a6ef")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("VBCSharpCultureSample.CultureMenu")]
    public class CultureMenu : ESRI.ArcGIS.SystemUI.IMenuDef
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

        #region "IMenuDef Implementations"
        public string Caption
        {
            get
            {
                //The Caption is set from the 'MenuCaption' string 
                //stored in the Resource File. The ResourceManager acquires the appropriate 
                //Resource file according to the UI Culture of the current thread
                //system.Windows.Forms.MessageBox.Show(My.Resources.Culture.

                ResourceManager rm = new ResourceManager("VBCSharpCultureSample.Resources", Assembly.GetExecutingAssembly());
                string m_Resource_str;
                m_Resource_str = (string)rm.GetString("MenuCaption"); 
                return (string)m_Resource_str;
           
            }
        }

        public void GetItemInfo(int pos, ESRI.ArcGIS.SystemUI.IItemDef itemDef)
        {
            //Adding the Items to the Menu
            switch (pos)
            {
                case 0 : 
                    // Set the item in the menu to the ClassID of multiItem above
                    itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomInFixedCommand";
                    itemDef.Group = false;
                    break;
                case 1 : 
                    //Set the item in the menu to the ClassID of multiItem above
                    itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomOutFixedCommand";
                    itemDef.Group = false;
                    break;
                case 2 : 
                    //Set the item in the menu to the ClassID of multiItem above
                    itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomWholePageCommand";
                    itemDef.Group = false;
                    break;
                case 3 :
                    //Set the item in the menu to the ClassID of multiItem above
                    itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomPageToLastExtentBackCommand";
                    itemDef.Group = false;
                    break;
                case 4 : 
                    //Set the item in the menu to the ClassID of multiItem above
                    itemDef.ID = "esriControlToolsPageLayout.ControlsPageZoomPageToLastExtentForwardCommand";
                    itemDef.Group = false;
                    break;
            }        
        }

        public int ItemCount
        {
            get
            {
                return 5;
            }
        }

        public string Name
        {
            get
            {
                return "CustomCommands_CultureMenu";
            }
        }
        #endregion

    }
}
