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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;

namespace AnimationDeveloperSamples
{
    [Guid("52b48920-7eb0-4e3b-8be6-b3983e59e739")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AnimationDeveloperSamples.AnimationTypeLayerEffects")]
    public class AnimationTypeLayerEffects : IAGAnimationType, IAGAnimationTypeUI
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
            MapAnimationTypes.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MapAnimationTypes.Unregister(regKey);

        }

        #endregion
        #endregion
        private string[] propName;
        private esriAnimationPropertyType[] propType;
        private string typeName;

        #region constructor
        public AnimationTypeLayerEffects()
        {
            propName = new string[2];
            propName[0] = "Brightness";
            propName[1] = "Contrast";

            propType = new esriAnimationPropertyType[2];
            propType[0] = esriAnimationPropertyType.esriAnimationPropertyInt;
            propType[1] = esriAnimationPropertyType.esriAnimationPropertyInt;

            typeName = "Layer Effects";
        }
        #endregion

        #region IAGAnimationType members
        public esriAnimationClass AnimationClass
        {
            get
            {
                return esriAnimationClass.esriAnimationClassGeneric;
            }
        }
        public object get_AnimationObjectByID(IAGAnimationContainer pContainer, int objectID)
        {
            IArray objectArray = get_ObjectArray(pContainer);
            return (object)objectArray.get_Element(objectID);
        }
        public int get_AnimationObjectID(IAGAnimationContainer pContainer, object pObject)
        {
            IArray objectArray = get_ObjectArray(pContainer);
            int objCount = objectArray.Count;

            int i = 0;
            for (i = 0; i < objCount; i++)
            {
                if (pObject == objectArray.get_Element(i))
                    break;
            }
            return i;
        }
        public string get_AnimationObjectName(IAGAnimationContainer pContainer, object pObject)
        {
            ILayer layer = (ILayer)pObject;
            if (layer != null)
            {
                return layer.Name;
            }
            else
                return "";
        }
        public bool get_AppliesToObject(object pObject)
        {
            if (pObject is ILayer)
            {
                ILayerEffects layerEffects = (ILayerEffects)pObject;
                if (layerEffects.SupportsBrightnessChange && layerEffects.SupportsContrastChange)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public UID CLSID
        {
            get
            {
                UID uid = new UIDClass();
                uid.Value = "{52b48920-7eb0-4e3b-8be6-b3983e59e739}";
                return uid;
            }
        }
        public UID KeyframeCLSID
        {
            get
            {
                UID uid = new UIDClass();
                uid.Value = "{965a7a4e-6371-427a-b8f7-ca433c262dc8}";
                return uid;
            }
        }
        public string Name
        {
            get
            {
                return typeName;
            }
        }
        public IArray get_ObjectArray(IAGAnimationContainer pContainer)
        {
            IActiveView view = pContainer.CurrentView as IActiveView;
            IArray array = new ArrayClass();

            ILayer layer1;
            int layerCount = view.FocusMap.LayerCount;
            int i = 0;
            for (i = 0; i < layerCount; i++)
            {
                layer1 = view.FocusMap.get_Layer(i);
                if (get_AppliesToObject(layer1))
                {
                    array.Add(layer1);
                }
            }

            return array;
        }

        public int PropertyCount
        {
            get
            {
                return 2;
            }
        }

        public string get_PropertyName(int index)
        {
            if (index >= 0 && index < 2)
                return propName[index];
            else
                return null;
        }

        public esriAnimationPropertyType get_PropertyType(int index)
        {
            return propType[index];
        }

        public void ResetObject(IAGAnimationContainer pContainer, object pObject)
        {
            return;
        }

        public void UpdateTrackExtensions(IAGAnimationTrack pTrack)
        {
            return;
        }
        #endregion

        #region IAGAnimationTypeUI members
        public IStringArray get_ChoiceList(int propIndex, int columnIndex)
        {
            return null;
        }
        public int get_ColumnCount(int propIndex)
        {
            if (propIndex == 0)
                return 1;
            else
                return 1;
        }
        public string get_ColumnName(int propIndex, int columnIndex)
        {
            if (propIndex == 0)
            {
                return "Brightness";
            }
            else if (propIndex == 1)
            {
                return "Contrast";
            }

            return null;
        }
        #endregion
    }
}
