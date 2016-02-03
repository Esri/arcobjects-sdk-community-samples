using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ADF.CATIDs;

namespace AnimationDeveloperSamples
{
    [Guid("E0DFDD9F-1C59-4FE8-89FA-71BE8D242A9C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AnimationDeveloperSamples.AnimationTypeMapGraphic")]
    public class AnimationTypeMapGraphic : IAGAnimationType, IAGAnimationTypeUI
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
        public AnimationTypeMapGraphic()
        {
            propName = new string[2];
            propName[0] = "Position";
            propName[1] = "Rotation";

            propType = new esriAnimationPropertyType[2];
            propType[0] = esriAnimationPropertyType.esriAnimationPropertyPoint;
            propType[1] = esriAnimationPropertyType.esriAnimationPropertyDouble;

            typeName = "Map Graphic";
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
            IArray array;
            array = get_ObjectArray(pContainer);
            IElement elem = (IElement)array.get_Element(objectID);

            return elem;
        }
        public int get_AnimationObjectID(IAGAnimationContainer pContainer, object pObject)
        {
            IArray array = get_ObjectArray(pContainer);
            int count = array.Count;
            int objectID = 0;
            for (int i = 0; i < count; i++)
            {
                IElement elem = (IElement)array.get_Element(i);
                if (elem == pObject)
                    break;
                objectID++;
            }

            return objectID;
        }
        public string get_AnimationObjectName(IAGAnimationContainer pContainer, object pObject)
        {
            string objectName;
            IElementProperties elemProps = (IElementProperties)pObject;
            objectName = elemProps.Name;

            return objectName;
        }
        public bool get_AppliesToObject(object pObject)
        {
            if ((pObject is IMarkerElement) || (pObject is IGroupElement) || (pObject is ITextElement))
                return true;
            else
                return false;
        }
        public UID CLSID
        {
            get
            {
                UID uid = new UIDClass();
                uid.Value = "{E0DFDD9F-1C59-4FE8-89FA-71BE8D242A9C}";
                return uid;
            }
        }
        public UID KeyframeCLSID
        {
            get
            {
                UID uid = new UIDClass();
                uid.Value = "{B2263D65-7FAF-4118-A255-5B0D47E453EE}";
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
            IGraphicsContainer graphicsContainer = view as IGraphicsContainer;
            graphicsContainer.Reset();

            IArray array = new ArrayClass();
            IElement elem = graphicsContainer.Next();

            while (elem != null)
            {
                if(get_AppliesToObject(elem))
                    array.Add(elem);
                elem = graphicsContainer.Next();
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
                return 2;
            else
                return 1;
        }
        public string get_ColumnName(int propIndex, int columnIndex)
        {
            if (propIndex == 0)
            {
                if (columnIndex == 0)
                    return "Position:X";
                else
                    return "Position:Y";
            }
            else if (propIndex == 1)
            {
                return "Rotation";
            }

            return null;
        }
        #endregion
    }
}
