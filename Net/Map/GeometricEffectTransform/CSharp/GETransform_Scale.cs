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
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;

namespace GETransformCSharp
{
    [Guid("4E927ADF-0368-4d6e-8473-86022A719E13")]
    public class GETransform_Scale: IGraphicAttributes, IGeometricEffect, IPersistVariant, IEditInteraction
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            ArcGISCategoryRegistration(registerType);
        }
        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            ArcGISCategoryUnregistration(registerType);
        }
        #endregion

        #region Component Category Registration
        static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = String.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GeometricEffect.Register(regKey);
        }
        static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = String.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GeometricEffect.Unregister(regKey);
        }
        #endregion

        double m_dFactorX;
        double m_dFactorY;
        bool m_bDone;
        IGeometry m_pGeom;
        ITransform2D m_pTransform;
        IClone m_pCloneGeom;
        IGeometry m_pGeomCopy;
        IPoint m_pCenterPoint;

        public GETransform_Scale()
        {
            m_dFactorX = 1; 
            m_dFactorY = 1; 
            m_pCenterPoint = new Point();
        }
        #region IGraphicAttributes Members

        string IGraphicAttributes.ClassName
        {
            get {return "Transform Scale";}
        }

        int IGraphicAttributes.GraphicAttributeCount
        {
            get { return 2; }
        }

        int IGraphicAttributes.get_ID(int attrIndex)
        {
            if (attrIndex < 2)
            {
                return attrIndex; 
            }
            return -1;
        }

        int IGraphicAttributes.get_IDByName(string Name)
        {
            if (Name == "X Transform Scale") { 
             return 0; 
            } 
            if (Name == "Y Transform Scale") { 
             return 1; 
            }
            return -1;
        }

        string IGraphicAttributes.get_Name(int attrId)
        {
            if (attrId == 0) {return "X Transform Scale";}
            if (attrId == 1) {return "Y Transform Scale";}
            return "";
        }

        IGraphicAttributeType IGraphicAttributes.get_Type(int attrId)
        {
            if (attrId == 0)
            {
                return new GraphicAttributeSizeType();
            }
            if (attrId == 1)
            {
                return new GraphicAttributeSizeType();
            }
            return null;
        }

        object IGraphicAttributes.get_Value(int attrId)
        {
            if (attrId == 0)
            {
                return m_dFactorX;
            }
            if (attrId == 1)
            {
                return m_dFactorY;
            }
            return null;
        }

        void IGraphicAttributes.set_Value(int attrId, object val)
        {
            if (attrId == 0)
            {
                m_dFactorX = (double)val;
            }
            if (attrId == 1)
            {
                m_dFactorY = (double)val;
            }
        }

        #endregion

        #region IGeometricEffect Members

        IGeometry IGeometricEffect.NextGeometry()
        {
            if (m_bDone)
            {
                return null;
            }
            else
            {
                m_pCloneGeom = (IClone)m_pGeom;
                m_pGeomCopy = (IGeometry)m_pCloneGeom.Clone();
                m_pTransform = (ITransform2D)m_pGeomCopy;
                m_pTransform.Scale(m_pCenterPoint, m_dFactorX, m_dFactorY);
                m_bDone = true;
                return m_pGeomCopy;                
            }
        }

        void IGeometricEffect.Reset(IGeometry Geometry)
        {
            m_pGeom = Geometry;
            m_pGeomCopy = null;
            double dXCenter;
            double dYCenter;
            dXCenter = (m_pGeom.Envelope.XMin + m_pGeom.Envelope.XMax) / 2;
            dYCenter = (m_pGeom.Envelope.YMin + m_pGeom.Envelope.YMax) / 2;
            m_pCenterPoint.PutCoords(dXCenter, dYCenter);
            m_pCenterPoint.SpatialReference = m_pGeom.SpatialReference;
            m_bDone = false;
        }

        esriGeometryType IGeometricEffect.get_OutputType(esriGeometryType inputType)
        {
            if (inputType == esriGeometryType.esriGeometryPolygon)
            {
                return inputType;
            }
            if (inputType == esriGeometryType.esriGeometryPolyline)
            {
                return inputType;
            }
            return esriGeometryType.esriGeometryNull;
        }

        #endregion

        #region IPersistVariant Members

        UID IPersistVariant.ID
        {
            get
            {
                UID pUID;
                pUID = new UID();
                pUID.Value = "{4E927ADF-0368-4d6e-8473-86022A719E13}";
                return pUID;
            }
        }

        void IPersistVariant.Load(IVariantStream Stream)
        {
            int version; 
            version = (int)Stream.Read(); 
            m_dFactorX = (double)Stream.Read(); 
            m_dFactorY = (double)Stream.Read();
        }

        void IPersistVariant.Save(IVariantStream Stream)
        {
            int version;
            version = 1;
            Stream.Write(version);
            Stream.Write(m_dFactorX);
            Stream.Write(m_dFactorY);
        }

        #endregion

        #region IEditInteraction Members

        void IEditInteraction.ModifyAttributes(object editParams, object attrArray)
        {
            IResizeInteraction pResize;
            pResize = editParams as IResizeInteraction;
            short[] a = (short[])attrArray;
            if (!(pResize == null))
            {
                if (Convert.ToBoolean(a[0]))
                {
                    m_dFactorX = m_dFactorX * pResize.RatioX;
                }
                if (Convert.ToBoolean(a[1]))
                {
                    m_dFactorY = m_dFactorY * pResize.RatioY;
                }
            }

        }

        bool IEditInteraction.get_IsEditableAttribute(object editParams, int attrIndex)
        {
            IResizeInteraction pResize;
            pResize = editParams as IResizeInteraction;
            if (!(pResize == null))
            {
                if (attrIndex == 0 | attrIndex == 1)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
