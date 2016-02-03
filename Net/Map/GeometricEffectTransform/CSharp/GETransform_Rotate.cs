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
    [Guid("3CFEE204-948E-43dd-AF2C-EB32FD0ADEF2")]
    public class GETransform_Rotate : IGeometricEffect, IGraphicAttributes, IPersistVariant, IEditInteraction
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

        double m_dAngle;
        bool m_bDone;
        IGeometry m_pGeom;
        ITransform2D m_pTransform;
        IClone m_pCloneGeom;
        IGeometry m_pGeomCopy;
        IPoint m_pCenterPoint;

        public GETransform_Rotate()
        {
            m_dAngle = 0; // default rotate angle is zero
            m_pCenterPoint = new Point();
        }
        

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
                m_pTransform.Rotate(m_pCenterPoint, m_dAngle);
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

        #region IGraphicAttributes Members

        string IGraphicAttributes.ClassName
        {
            get { return "Transform Rotate"; }
        }

        int IGraphicAttributes.GraphicAttributeCount
        {
            get { return 1; }
        }

        int IGraphicAttributes.get_ID(int attrIndex)
        {
            if (attrIndex >= 0 & attrIndex < 1)
            {
                return attrIndex;
            }
            return -1;
        }

        int IGraphicAttributes.get_IDByName(string Name)
        {
            if (Name == "Transform Angle")
            {
                return 0;
            }
            return -1;
        }

        string IGraphicAttributes.get_Name(int attrId)
        {
            if (attrId == 0)
                return "Transform Angle";
            return "";
        }

        IGraphicAttributeType IGraphicAttributes.get_Type(int attrId)
        {
            if (attrId == 0)
            {
                return new GraphicAttributeDoubleType();
            }
            if (attrId == 1)
            {
                return new GraphicAttributeDoubleType();
            }
            return null;
        }

        object IGraphicAttributes.get_Value(int attrId)
        {
            if (attrId == 0)
            {
                return m_dAngle * 360 / 3.141592653;
            }
            return 0;
        }

        void IGraphicAttributes.set_Value(int attrId, object val)
        {
            if (attrId == 0)
            {
                m_dAngle = (double)val / 360.0 * 3.141592653  ; 
            }
        }

        #endregion

        #region IPersistVariant Members

        UID IPersistVariant.ID
        {
            get
            {
                UID pUID;
                pUID = new UID();
                pUID.Value = "{3CFEE204-948E-43dd-AF2C-EB32FD0ADEF2}";
                return pUID;
            }
        }

        void IPersistVariant.Load(IVariantStream Stream)
        {
            int version ;
            version = (int)Stream.Read();
            m_dAngle = (double)Stream.Read();
        }

        void IPersistVariant.Save(IVariantStream Stream)
        {
            int version;
            version = 1;
            Stream.Write(version);
            Stream.Write(m_dAngle);
        }

        #endregion

        #region IEditInteraction Members

        void IEditInteraction.ModifyAttributes(object editParams, object attrArray)
        {
            IRotateInteraction pRotate;
            pRotate = editParams as IRotateInteraction;
            short[] a = (short[])attrArray;
            if (!(pRotate == null))
            {
                if (Convert.ToBoolean(a[0]))
                {
                    m_dAngle = pRotate.Angle;
                }
            }
        }

        bool IEditInteraction.get_IsEditableAttribute(object editParams, int attrIndex)
        {
            IRotateInteraction pRotate;
            pRotate = editParams as IRotateInteraction; 
            if (!(pRotate == null))
            {
                if (attrIndex == 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
