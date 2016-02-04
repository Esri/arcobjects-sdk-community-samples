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
using System.Windows.Forms;
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

namespace AnimationDeveloperSamples
{
    [Guid("B2263D65-7FAF-4118-A255-5B0D47E453EE")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AnimationDeveloperSamples.MapGraphicKeyframe")]
    public class MapGraphicKeyframe:IAGKeyframe, IAGKeyframeUI
    {
        private ILongArray activeProps;
        private IAGAnimationType animType;
        private string name;
        private bool bObjectsNeedRefresh;
        private IPoint Position;
        private double Rotation;
        private double timeStamp;

        #region constructor
        public MapGraphicKeyframe()
        {
            activeProps = new LongArrayClass();
            activeProps.Add(0);
            activeProps.Add(1);
            animType = new AnimationTypeMapGraphic();
            name = "";
            bObjectsNeedRefresh = false;
            Position = new PointClass();
            Rotation = 0.0;
            timeStamp = 0.0;
        }
        #endregion

        #region IAGKeyframe members
        public ILongArray ActiveProperties 
        {
            get {
                return activeProps;
            }
            set {
                activeProps = value;
            }
        }

        public IAGAnimationType AnimationType 
        {
            get {
                return animType;
            }            
        }

        public void Apply(IAGAnimationTrack pTrack, IAGAnimationContainer pContainer, object pObject)
        {
            IElement elem = (IElement) pObject;

            UpdateGraphicObject(elem,pContainer,pTrack,Position,Rotation);

            return;
        }

        public void CaptureProperties(IAGAnimationContainer pContainer, object pObject)
        {
            IElement elem = pObject as IElement;
            IActiveView view = pContainer.CurrentView as IActiveView;
            IGraphicsContainerSelect graphicsConSel = view as IGraphicsContainerSelect;
            IDisplay disp = view.ScreenDisplay as IDisplay;
            IEnvelope elemEnv = GetElementBound(elem,pContainer);

            if (elem.Geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                Position = elem.Geometry as IPoint;
            }
            else
            {
                IEnvelope elementEnvelope = elem.Geometry.Envelope;
                IArea elementArea = elementEnvelope as IArea;
                Position = elementArea.Centroid;
            }

            IElementProperties elemProps = (IElementProperties)elem;
            if(elemProps.CustomProperty!=null)
                Rotation = (double)elemProps.CustomProperty;
        }

        public void Interpolate(IAGAnimationTrack pTrack, IAGAnimationContainer pContainer,
            object pObject, int propertyIndex, double time, IAGKeyframe pNextKeyframe,
            IAGKeyframe pPrevKeyframe, IAGKeyframe pAfterNextKeyframe)
        {
            if (time < TimeStamp || time > pNextKeyframe.TimeStamp)
                return;

            IElement elem = (IElement)pObject;
            IPoint new_pos = new PointClass();
           
            if (propertyIndex == 0)
            {
                double x1;
                double y1;
                IPoint nextPosition = (IPoint)pNextKeyframe.get_PropertyValue(0);

                double timeFactor;
                timeFactor = (time - TimeStamp) / (pNextKeyframe.TimeStamp - TimeStamp); //ignoring pPrevKeyframe and pAfterNextKeyframe

                x1 = Position.X * (1 - timeFactor) + nextPosition.X * timeFactor;
                y1 = Position.Y * (1 - timeFactor) + nextPosition.Y * timeFactor;
                
                new_pos.PutCoords(x1, y1);

                if (!(elem is ILineElement))
                {
                    MoveElement(elem, new_pos, pContainer);
                    TracePath(elem, new_pos, pContainer, pTrack, this);
                    bObjectsNeedRefresh = true;
                }
            }
            if (propertyIndex == 1)
            {
                //this property only applies to the point graphic 
                if (!(elem is ILineElement))
                {
                    RotateElement(elem, Rotation, pContainer);
                    bObjectsNeedRefresh = true;
                }
            }

            return;
        }

        public bool get_IsActiveProperty(int propIndex)
        {
            bool bIsActive = false;
            int count = activeProps.Count;

            for (int i = 0; i < count; i++)
            {
                long temp = activeProps.get_Element(i);
                if (temp == propIndex)
                    bIsActive = true;
            }
            return bIsActive;
        }

        public void set_IsActiveProperty(int propIndex, bool pbIsActiveProp)
        {
            if (pbIsActiveProp)
            {
                if (get_IsActiveProperty(propIndex) == false)
                {
                    activeProps.Add(propIndex);
                }
            }
            else
            {
                if (get_IsActiveProperty(propIndex) == true)
                {
                    int i = 0;
                    int count = activeProps.Count;
                    for (i = 0; i < count; i++)
                    {
                        long temp = activeProps.get_Element(i);
                        if (temp == propIndex)
                            break;
                    }

                    activeProps.Remove(i);
                }
            }          
        }
        public string Name 
        { 
            get {
                return name;
            }

            set {
                name = value;
            }        
        }
        public bool ObjectNeedsRefresh 
        {
            get {
                return bObjectsNeedRefresh;
            }        
        }
        
        public object get_PropertyValue(int propIndex)
        {
            if (propIndex == 0)
                return (object)Position;
            else if (propIndex == 1)
                return (object)Rotation;
            else
                return null;
        }

        public void set_PropertyValue(int propIndex, object pValue)
        {
            if (propIndex == 0)
                Position = (IPoint)pValue;
            else if (propIndex == 1)
                Rotation = (Double)pValue;
            else
                return;
        }

        public void RefreshObject(IAGAnimationTrack pTrack,
            IAGAnimationContainer pContainer, object pObject)
        {         
            RefreshGraphicObject((IElement)pObject, pContainer);
        }
        public double TimeStamp
        { 
            get {
                return timeStamp;
            }

            set {
                timeStamp = value;
            }
        }
        #endregion

        #region IAGKeyframeUI members
        public string GetText(int propIndex, int columnIndex)
        {
            string text;
            text = null;
            switch (propIndex)
            {
                case 0:
                    {
                        if (columnIndex == 0)
                        {
                            text = System.Convert.ToString(Position.X);
                        }
                        if (columnIndex == 1)
                        {
                            text = System.Convert.ToString(Position.Y);
                        }
                    }
                    break;
                case 1:
                    text = System.Convert.ToString(Rotation);
                    break;
            }
            return text;
        }

        public void SetText(int propIndex, int columnIndex, string text)
        {
            switch (propIndex)
            {
                case 0:
                    {
                        if (columnIndex == 0)
                        {
                            Position.X = System.Convert.ToDouble(text);
                        }
                        if (columnIndex == 1)
                        {
                            Position.Y = System.Convert.ToDouble(text);
                        }
                    }
                    break;
                case 1:
                    {
                        Rotation = System.Convert.ToDouble(text);
                    }
                    break;
            }

            return;
        }
        #endregion 

        #region private methods
        private void RotateElement(IElement elem, double new_angle, IAGAnimationContainer pContainer)
        {
            ITransform2D transform2D = elem as ITransform2D;
            IPoint rotateOrigin;

            if (elem.Geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                rotateOrigin = elem.Geometry as IPoint;
            }
            else
            {
                IEnvelope elementEnvelope = elem.Geometry.Envelope;
                IArea elementArea = elementEnvelope as IArea;
                rotateOrigin = elementArea.Centroid;
            }

            AddPropertySet(elem);

            IElementProperties prop = (IElementProperties)elem; //record the old properties
            IPropertySet propSet;
            propSet = (IPropertySet)prop.CustomProperty;
            double old_angle;
            old_angle = (double)propSet.GetProperty("Angle");

            propSet.SetProperty("Angle", new_angle); //update old angle

            transform2D.Rotate(rotateOrigin, new_angle-old_angle);
        }

        private void TracePath(IElement elem, IPoint new_pos, IAGAnimationContainer pContainer, IAGAnimationTrack pTrack, IAGKeyframe pKeyframe)
        {
            IAGAnimationTrackExtensions trackExtensions = (IAGAnimationTrackExtensions)(pTrack);
            IMapGraphicTrackExtension graphicTrackExtension;
            if (trackExtensions.ExtensionCount == 0) //if there is no extension, add one
            {
                graphicTrackExtension = new MapGraphicTrackExtension();
                trackExtensions.AddExtension(graphicTrackExtension);
            }
            else
            {
                graphicTrackExtension = (IMapGraphicTrackExtension)trackExtensions.get_Extension(0);
            }

            ILineElement path = graphicTrackExtension.TraceElement;

            bool showTrace = graphicTrackExtension.ShowTrace;
            if (!showTrace)
            {
                if (CheckGraphicExistance((IElement)path, pContainer))
                {
                    RemoveGraphicFromDisplay((IElement)path, pContainer);
                }
                return;
            }

            //Add the path to the graphic container
            if (!CheckGraphicExistance((IElement)path, pContainer))
            {
                AddGraphicToDisplay((IElement)path, pContainer);
            }

            RecreateLineGeometry((IElement)path, pTrack, pKeyframe, new_pos);
        }

        private void AddGraphicToDisplay(IElement elem, IAGAnimationContainer animContainer)
        {
            IActiveView view = animContainer.CurrentView as IActiveView;
            IGraphicsContainer graphicsContainer = view as IGraphicsContainer;
            graphicsContainer.AddElement(elem, 0);            
            elem.Activate(view.ScreenDisplay);
        }

        private void RemoveGraphicFromDisplay(IElement elem, IAGAnimationContainer animContainer)
        {
            IActiveView view = animContainer.CurrentView as IActiveView;
            IGraphicsContainer graphicsContainer = view as IGraphicsContainer;
            graphicsContainer.DeleteElement(elem);
        }

        private bool CheckGraphicExistance(IElement elem, IAGAnimationContainer animContainer)
        {
            IActiveView view = animContainer.CurrentView as IActiveView;
            IGraphicsContainer graphicsContainer = view as IGraphicsContainer;
            graphicsContainer.Reset();

            bool exists = false;
            IElement temp = graphicsContainer.Next();
            while (temp != null)
            {
                if (temp == elem)
                {
                    exists = true;
                    break;
                }
                temp = graphicsContainer.Next();
            }
            return exists;
        }

        private void MoveElement(IElement elem, IPoint new_pos, IAGAnimationContainer pContainer)
        {
            ITransform2D transform2D = elem as ITransform2D;
            IPoint origin;

            if (elem.Geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                origin = elem.Geometry as IPoint;
            }
            else
            {
                IEnvelope elementEnvelope = elem.Geometry.Envelope;
                IArea elementArea = elementEnvelope as IArea;
                origin = elementArea.Centroid;
            }

            AddPropertySet(elem);

            IElementProperties prop = (IElementProperties)elem; //record the old properties
            IPropertySet propSet;
            propSet = (IPropertySet)prop.CustomProperty;
            IEnvelope oldEnv = GetElementBound(elem, pContainer);
            propSet.SetProperty("Envelope", oldEnv);

            transform2D.Move(new_pos.X - origin.X, new_pos.Y - origin.Y);
        }

        private void RecreateLineGeometry(IElement elem, IAGAnimationTrack pTrack, IAGKeyframe pKeyframe, IPoint new_pos)
        {
            IGeometry newGeometry = new PolylineClass();
            IPointCollection newPointCol = (IPointCollection)newGeometry;

            IAGAnimationTrackKeyframes trackKeyframes = (IAGAnimationTrackKeyframes)pTrack;
            int tCount = trackKeyframes.KeyframeCount;
            object missing = Type.Missing;
            for (int i = 0; i < tCount; i++)
            {
                IAGKeyframe tempKeyframe = trackKeyframes.get_Keyframe(i);
                newPointCol.AddPoint((IPoint)tempKeyframe.get_PropertyValue(0), ref missing, ref missing);
                if ((IPoint)tempKeyframe.get_PropertyValue(0) == (IPoint)pKeyframe.get_PropertyValue(0))
                    break;
            }
            if (new_pos != null)
                newPointCol.AddPoint(new_pos, ref missing, ref missing);

            elem.Geometry = newGeometry;
        }

        private IEnvelope GetElementBound(IElement elem, IAGAnimationContainer pContainer)
        {
            IActiveView view = pContainer.CurrentView as IActiveView;
            IGraphicsContainerSelect graphicsContainerSelect = view as IGraphicsContainerSelect;
            IDisplay disp = view.ScreenDisplay as IDisplay;
            
            IEnvelope elementEnvelope = new EnvelopeClass();
            elem.QueryBounds(disp, elementEnvelope);

            if (graphicsContainerSelect.ElementSelected(elem))
            {
                elementEnvelope = elem.SelectionTracker.get_Bounds(disp);
            }

            return elementEnvelope;
        }

        private void UpdateGraphicObject(IElement elem, IAGAnimationContainer pContainer, IAGAnimationTrack pTrack, IPoint new_pos, double new_angle)
        {
            if (elem == null||new_pos==null)
                return; //invalidate parameter

            MoveElement(elem, new_pos, pContainer);
            RotateElement(elem, new_angle, pContainer);

            return;
        }

        private void AddPropertySet(IElement elem)
        {
            if (elem == null)
                return; //invalidate parameter

            IElementProperties prop = (IElementProperties)elem; //record the old properties
            IPropertySet propSet;
            if (prop.CustomProperty == null)
            {
                propSet = new PropertySetClass();
                propSet.SetProperty("Envelope", null);
                propSet.SetProperty("Angle", 0.0);
                prop.CustomProperty = propSet;
            }
        }

        private void RefreshGraphicObject(IElement elem, IAGAnimationContainer pContainer)
        {
            IActiveView view = pContainer.CurrentView as IActiveView;
            IEnvelope elemEnv = GetElementBound(elem, pContainer);

            IEnvelope oldEnv = new EnvelopeClass();
            IElementProperties2 elemProps2 = (IElementProperties2) elem;
            oldEnv = (IEnvelope) elemProps2.CustomProperty;

            IViewRefresh animRefresh = (IViewRefresh)view;

            if (oldEnv != null)
            {
                elemEnv.Union(oldEnv);
            }

            animRefresh.AnimationRefresh(esriViewDrawPhase.esriViewGraphics, elem, elemEnv);
        }
        #endregion 
    }
}
