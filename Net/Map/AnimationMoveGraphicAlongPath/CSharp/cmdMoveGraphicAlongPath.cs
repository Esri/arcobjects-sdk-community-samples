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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;

namespace AnimationDeveloperSamples
{
    [Guid("96EA1F27-4394-4997-ADC0-065792082D1D")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AnimationDeveloperSamples.cmdMoveGraphicAlongPath")]
    public sealed class cmdMoveGraphicAlongPath : BaseCommand
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

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IAnimationExtension animExt;

        public cmdMoveGraphicAlongPath()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Animation Developer Samples"; //localizable text
            base.m_caption = "Move Graphic along Path...";  //localizable text 
            base.m_message = "Move graphic along a selected line graphic or line feature";  //localizable text
            base.m_toolTip = "Move graphic along path";  //localizable text
            base.m_name = "AnimationDeveloperSamples_cmdMoveGraphicAlongPath";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            string[] res = GetType().Assembly.GetManifestResourceNames();
            if (res.GetLength(0) > 0)
            {
                base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(), "cmdMoveGraphicAlongPath.bmp"));
            }
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

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            if (hook is IApplication)
            {
                IApplication app = (IApplication)hook;
                UID pUID = new UIDClass();
                pUID.Value = "esriAnimation.AnimationExtension";
                animExt = (IAnimationExtension)app.FindExtensionByCLSID(pUID);
            }
        }

        public override void OnClick()
        {
            frmCreateGraphicTrackOptions optionsForm = new frmCreateGraphicTrackOptions();
            IGeometry selectedPath = GetSelectedLineFeature();
            IElement lineElement = (IElement)GetSelectedLineElement();
            IElement selectedElement = GetSelectedPointElement();
            optionsForm.lineFeature = selectedPath;
            optionsForm.lineGraphic = (ILineElement)lineElement;
            optionsForm.pointGraphic = selectedElement;
            optionsForm.AnimationExtension = animExt;
            optionsForm.RefreshPathSourceOptions();
            optionsForm.ShowDialog();
        }

        public override bool Enabled
        {
            get
            {
                if (m_hookHelper != null)
                {
                    IGeometry selectedPath = GetSelectedLineFeature();
                    IElement lineElement = (IElement)GetSelectedLineElement();
                    IElement selectedElement = GetSelectedPointElement();

                    if (selectedElement != null && (lineElement != null || selectedPath != null))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
        }

        #endregion

        #region private methods
        private IGeometry GetSelectedLineFeature()
        {
            IMap pMap = m_hookHelper.FocusMap;
            IEnumFeature enumFeature = (IEnumFeature)pMap.FeatureSelection;
            IGeometry selectedPath = null;

            IFeature pFeat = enumFeature.Next();
            while (pFeat != null)
            {
                if (pFeat.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    selectedPath = (IGeometry)pFeat.Shape;
                    break;
                }
                pFeat = enumFeature.Next();
            }
            return selectedPath;
        }

        private IElement GetSelectedPointElement()
        {
            IMap activeFrame = m_hookHelper.FocusMap;
            IGraphicsContainerSelect graphicsSel = activeFrame as IGraphicsContainerSelect;
            IElement selectedElement = null;
            IAGAnimationType graphicAnimationType = new AnimationTypeMapGraphic();
            if (graphicsSel.ElementSelectionCount > 0)
            {
                IEnumElement enumElem = graphicsSel.SelectedElements;
                selectedElement = enumElem.Next();
                while (selectedElement != null)
                {
                    if (graphicAnimationType.get_AppliesToObject(selectedElement))
                        break;
                    selectedElement = enumElem.Next();
                }
            }
            else
            {
                selectedElement = null;
            }
            return selectedElement;
        }

        private ILineElement GetSelectedLineElement()
        {
            IMap activeFrame = m_hookHelper.FocusMap;
            IGraphicsContainerSelect graphicsSel = activeFrame as IGraphicsContainerSelect;
            IElement selectedElement = null;
            ILineElement lineElement;
            if (graphicsSel.ElementSelectionCount > 0)
            {
                IEnumElement enumElem = graphicsSel.SelectedElements;
                selectedElement = enumElem.Next();
                while (selectedElement != null)
                {
                    if (selectedElement is ILineElement)
                        break;
                    selectedElement = enumElem.Next();
                }
            }
            lineElement = (ILineElement)selectedElement;
            return lineElement;
        }
        #endregion
    }
}
