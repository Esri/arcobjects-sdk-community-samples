/*

   Copyright 2016 Esri

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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace SelectCommands
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("b737395c-7d7f-4cef-b38e-63b13498b079")]
    public sealed class ClearFeatureSelection : BaseCommand
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
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion
        private IHookHelper m_HookHelper = new HookHelperClass();

        public ClearFeatureSelection()
        {
            //Create an IHookHelper object
            m_HookHelper = new HookHelperClass();

            //Set the tool properties
            base.m_caption = "Clear Feature Selection";
            base.m_category = "Sample_Select(C#)";
            base.m_name = "Sample_Select(C#)_Clear Feature Selection";
            base.m_message = "Clear Current Feature Selection";
            base.m_toolTip = "Clear Feature Selection";
            base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "ClearSelection.bmp"));
        }

        public override void OnCreate(object hook)
        {
            m_HookHelper.Hook = hook;
        }

        public override bool Enabled
        {
            get
            {
                if (m_HookHelper.FocusMap == null) return false;
                return (m_HookHelper.FocusMap.SelectionCount > 0);
            }
        }

        public override void OnClick()
        {
            //Clear selection
            m_HookHelper.FocusMap.ClearSelection();

            //Get the IActiveView of the FocusMap
            IActiveView activeView = (IActiveView)m_HookHelper.FocusMap;

            //Get the visible extent of the display
            IEnvelope bounds = activeView.ScreenDisplay.DisplayTransformation.FittedBounds;

            //Refresh the visible extent of the display
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, bounds);
        }

    }
}