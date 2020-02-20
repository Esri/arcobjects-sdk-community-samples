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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace HookActions
{
    [Guid("7DA9C483-ABDC-4e42-AA9F-530B33980D15")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("HookActions.hookActionsZoom")]
    public sealed class hookActionsZoom : BaseCommand
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
            GMxCommands.Register(regKey);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Unregister(regKey);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IGlobeHookHelper m_globeHookHelper = null;

        public hookActionsZoom()
        {
            base.m_category = "HookActions";
            base.m_caption = "Zoom selected features";
            base.m_message = "Zoom selected features";
            base.m_toolTip = "Zoom selected features";
            base.m_name = "HookActions_Zoom";
        }

        public override void OnCreate(object hook)
        {
            // Test the hook that calls this command and disable if nothing valid
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }
            if (m_hookHelper == null)
            {
                //Can be globe
                try
                {
                    m_globeHookHelper = new GlobeHookHelperClass();
                    m_globeHookHelper.Hook = hook;
                    if (m_globeHookHelper.ActiveViewer == null)
                    {
                        //Nothing valid!
                        m_globeHookHelper = null;
                    }
                }
                catch
                {
                    m_globeHookHelper = null;
                }
            }

            if (m_globeHookHelper == null && m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;
        }

        public override bool Enabled
        {
            get
            {
                IHookActions hookActions = null;
                IBasicMap basicMap = null;

                //Get basic map and set hook actions
                if (m_hookHelper != null)
                {
                    basicMap = m_hookHelper.FocusMap as IBasicMap;
                    hookActions = m_hookHelper as IHookActions;
                }
                else if (m_globeHookHelper != null)
                {
                    basicMap = m_globeHookHelper.Globe as IBasicMap;
                    hookActions = m_globeHookHelper as IHookActions;
                }

                //Disable if no features selected
                IEnumFeature enumFeature = basicMap.FeatureSelection as IEnumFeature;
                IFeature feature = enumFeature.Next();
                if (feature == null) return false;

                //Enable if action supported on first selected feature
                if (hookActions.get_ActionSupported(feature.Shape, esriHookActions.esriHookActionsZoom))
                    return true;
                else
                    return false;
            }
        }

        public override void OnClick()
        {
            IHookActions hookActions = null;
            IBasicMap basicMap = null;

            //Get basic map and set hook actions
            if (m_hookHelper != null)
            {
                basicMap = m_hookHelper.FocusMap as IBasicMap;
                hookActions = m_hookHelper as IHookActions;
            }

            else if (m_globeHookHelper != null)
            {
                basicMap = m_globeHookHelper.Globe as IBasicMap;
                hookActions = m_globeHookHelper as IHookActions;
            }

            //Get feature selection 
            ISelection selection = basicMap.FeatureSelection;
            //Get enumerator
            IEnumFeature enumFeature = selection as IEnumFeature;
            enumFeature.Reset();
            //Set first feature
            IFeature feature;
            feature = enumFeature.Next();

            //Loop though the features
            IArray array = new ESRI.ArcGIS.esriSystem.Array();
            while (feature != null)
            {
                //Add feature to array
                array.Add(feature.Shape);
                //Set next feature
                feature = enumFeature.Next();
            }

            //If the action is supported perform the action
            if (hookActions.get_ActionSupportedOnMultiple(array, esriHookActions.esriHookActionsZoom))
                hookActions.DoActionOnMultiple(array, esriHookActions.esriHookActionsZoom);
        }
    }
}


