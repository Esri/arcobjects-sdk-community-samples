using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;

namespace AnimationDeveloperSamples
{
    [Guid("965a7a4e-6371-427a-b8f7-ca433c262dc8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AnimationDeveloperSamples.MapLayerEffectsKeyframe")]
    public class MapLayerEffectsKeyframe:IAGKeyframe,IAGKeyframeUI
    {
        private ILongArray activeProps;
        private IAGAnimationType animType;
        private string name;
        private bool bObjectsNeedRefresh;
        private short brightness;
        private short contrast;
        private double timeStamp;

        #region constructor
        public MapLayerEffectsKeyframe()
        {
            activeProps = new LongArrayClass();
            activeProps.Add(0);
            activeProps.Add(1);
            animType = new AnimationTypeLayerEffects();
            name = "";
            bObjectsNeedRefresh = false;
            brightness = 0;
            contrast = 0;
            timeStamp = 0;
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
            SetBrightness((ILayer)pObject, brightness);
            SetContrast((ILayer)pObject, contrast);
            pContainer.RefreshObject(pObject);
            return;
        }

        public void CaptureProperties(IAGAnimationContainer pContainer, object pObject)
        {
            contrast = GetContrast((ILayer)pObject);
            brightness = GetBrightness((ILayer)pObject);
        }

        public void Interpolate(IAGAnimationTrack pTrack, IAGAnimationContainer pContainer,
            object pObject, int propertyIndex, double time, IAGKeyframe pNextKeyframe,
            IAGKeyframe pPrevKeyframe, IAGKeyframe pAfterNextKeyframe)
        {
            if (time < TimeStamp || time > pNextKeyframe.TimeStamp)
                return;

            double timeFactor;
            timeFactor = (time - TimeStamp) / (pNextKeyframe.TimeStamp - TimeStamp); //ignoring pPrevKeyframe and pAfterNextKeyframe
            if (propertyIndex == 0) //interpolate brightness
            {
                short brightnessInterpolated;
                short brightnessStart;
                short brightnessEnd;
                brightnessStart = brightness;
                brightnessEnd = System.Convert.ToInt16(pNextKeyframe.get_PropertyValue(0));
                brightnessInterpolated = System.Convert.ToInt16(timeFactor * (brightnessEnd - brightnessStart) + brightnessStart);
                SetBrightness((ILayer)pObject, brightnessInterpolated);
                bObjectsNeedRefresh = true;
            }
            else //interpolate contrast
            {
                short contrastInterpolated;
                short contrastStart;
                short contrastEnd;
                contrastStart = contrast;
                contrastEnd = System.Convert.ToInt16(pNextKeyframe.get_PropertyValue(1));
                contrastInterpolated = System.Convert.ToInt16(timeFactor * (contrastEnd - contrastStart) + contrastStart);
                SetContrast((ILayer)pObject, contrastInterpolated);
                bObjectsNeedRefresh = true;
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
                    int i=0;
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
                return brightness;
            else if (propIndex == 1)
                return contrast;
            else
                return null;
        }

        public void set_PropertyValue(int propIndex, object pValue)
        {
            if (propIndex == 0)
                brightness = Convert.ToInt16(pValue);
            else if (propIndex == 1)
                contrast = Convert.ToInt16(pValue);
            else
                return;
        }

        public void RefreshObject(IAGAnimationTrack pTrack,
            IAGAnimationContainer pContainer, object pObject)
        {
            bObjectsNeedRefresh = false;
            pContainer.RefreshObject(pObject);
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
                        text = System.Convert.ToString(brightness);
                    }
                    break;
                case 1:
                    text = System.Convert.ToString(contrast);
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
                        brightness = System.Convert.ToInt16(text);
                    }
                    break;
                case 1:
                    {
                        contrast = System.Convert.ToInt16(text);
                    }
                    break;
            }

            return;
        }
        #endregion

        #region private methods
        private short GetBrightness(ILayer layer)
        {
            ILayerEffects layerEffects = (ILayerEffects)layer;
            return layerEffects.Brightness;
        }

        private void SetBrightness(ILayer layer, short brtness)
        {
            ILayerEffects layerEffects = (ILayerEffects)layer;
            layerEffects.Brightness = brtness;
            return;
        }

        private short GetContrast(ILayer layer)
        {
            ILayerEffects layerEffects = (ILayerEffects)layer;
            return layerEffects.Contrast;
        }

        private void SetContrast(ILayer layer, short ctr)
        {
            ILayerEffects layerEffects = (ILayerEffects)layer;
            layerEffects.Contrast = ctr;
            return;
        }
        #endregion
    }
}
