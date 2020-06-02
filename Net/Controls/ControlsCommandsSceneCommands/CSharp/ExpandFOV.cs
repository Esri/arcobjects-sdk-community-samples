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
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace sceneTools
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("90A4082D-8A9A-4858-BBFC-4DE3DF05905A")]

    public sealed class ExpandFOV : BaseCommand
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

        private ISceneHookHelper m_pSceneHookHelper;

        public ExpandFOV()
        {
            base.m_category = "Sample_SceneControl(C#)";
            base.m_caption = "Expand Field of View";
            base.m_toolTip = "Expand Field of View";
            base.m_name = "Sample_SceneControl(C#)/Expand Field of View";
            base.m_message = "Expand the Field of View";

            //Load resources
            string[] res = GetType().Assembly.GetManifestResourceNames();
            if (res.GetLength(0) > 0)
            {
                base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("sceneTools.expand.bmp"));
            }
            m_pSceneHookHelper = new SceneHookHelperClass();
        }
                
        public override bool Enabled
        {
            get
            {
                if (m_pSceneHookHelper.Hook == null || m_pSceneHookHelper.Scene == null)
                    return false;
                else
                {
                    ICamera pCamera = (ICamera)m_pSceneHookHelper.Camera;

                    //Disable if orthographic (2D) view
                    if (pCamera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
                        return false;
                    else
                        return true;
                }
            }
        }

        public override void OnCreate(object hook)
        {
            m_pSceneHookHelper.Hook = hook;
        }

        public override void OnClick()
        {
            //Get scene viewer's camera
            ICamera pCamera = (ICamera)m_pSceneHookHelper.Camera;

            //Widen the field of view
            double dAngle;
            dAngle = pCamera.ViewFieldAngle;
            pCamera.ViewFieldAngle = dAngle * 1.1;

            //Redraw the scene viewer
            ISceneViewer pSceneViewer = (ISceneViewer)m_pSceneHookHelper.ActiveViewer;
            pSceneViewer.Redraw(false);
        }
    }
}
