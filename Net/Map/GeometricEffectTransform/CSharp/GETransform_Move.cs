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
    [Guid("E1CCDB35-18F7-4e67-B8B5-3579DB6BC10A")]
    public class GETransform_Move : IGeometricEffect, IGraphicAttributes, IPersistVariant, IEditInteraction
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

        double m_dOffsetX;
        double m_dOffsetY;
        bool m_bDone;
        IGeometry m_pGeom;
        ITransform2D m_pTransform;
        IClone m_pCloneGeom;
        IGeometry m_pGeomCopy;

        public GETransform_Move()
        {
            m_dOffsetX = 0.1;
            m_dOffsetY = -0.1;
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
                m_pTransform.Move(m_dOffsetX, m_dOffsetY);
                m_bDone = true;
                return m_pGeomCopy;
            }

        }

        void IGeometricEffect.Reset(IGeometry Geometry)
        {
            m_pGeom = Geometry;
            m_pGeomCopy = null;
            m_bDone = false;
        }

        esriGeometryType IGeometricEffect.get_OutputType(esriGeometryType inputType)
        {
            return inputType;
        }

        #endregion

        #region IGraphicAttributes Members

        string IGraphicAttributes.ClassName
        {
            get { return "Transform Move"; }
        }

        int IGraphicAttributes.GraphicAttributeCount
        {
            get { return 2; }
        }

        int IGraphicAttributes.get_ID(int attrIndex)
        {
            if ((attrIndex >= 0 & attrIndex < 2))
            {
                return attrIndex;
            }
            return -1;

        }

        int IGraphicAttributes.get_IDByName(string Name)
        {
            if ((Name == "X Offset Move"))
            {
                return 0;
            }
            if (Name == "Y Offset Move")
            {
                return 1;
            }
            return -1;
        }

        string IGraphicAttributes.get_Name(int attrId)
        {
            if ((attrId == 0))
            {
                return "X Offset Move";
            }
            if (attrId == 1)
            {
                return "Y Offset Move";
            }
            return " ";

        }

        IGraphicAttributeType IGraphicAttributes.get_Type(int attrId)
        {
            if (attrId == 0)
            {
                return new ESRI.ArcGIS.Display.GraphicAttributeSizeType();
            }
            if (attrId == 1)
            {
                return new ESRI.ArcGIS.Display.GraphicAttributeSizeType();
            }
            return null;
        }

        object IGraphicAttributes.get_Value(int attrId)
        {
            if (attrId == 0)
            {
                return m_dOffsetX;
            }
            if (attrId == 1)
            {
                return m_dOffsetY;
            }
            return 0;
        }

        void IGraphicAttributes.set_Value(int attrId, object val)
        {
            if (attrId == 0)
            {
                m_dOffsetX = (double)val;
            }
            if (attrId == 1)
            {
                m_dOffsetY = (double)val;
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
                pUID.Value = "{E1CCDB35-18F7-4e67-B8B5-3579DB6BC10A}";
                return pUID;
            }
        }

        void IPersistVariant.Load(IVariantStream Stream)
        {
            int version;
            version = (int)Stream.Read();
            m_dOffsetX = (double)Stream.Read();
            m_dOffsetY = (double)Stream.Read();
        }

        void IPersistVariant.Save(IVariantStream Stream)
        {
            int version;
            version = 1;
            Stream.Write(version);
            Stream.Write(m_dOffsetX);
            Stream.Write(m_dOffsetY);
        }

        #endregion

        #region IEditInteraction Members

        void IEditInteraction.ModifyAttributes(object editParams, object attrArray)
        {
            IMoveInteraction pMove;
            pMove = editParams as IMoveInteraction;
            short[] a = (short[])attrArray;
            if (!(pMove == null))
            {
                if (Convert.ToBoolean(a[0]))
                {
                    m_dOffsetX = m_dOffsetX + pMove.OffsetX;
                }
                if (Convert.ToBoolean(a[1]))
                {
                    m_dOffsetY = m_dOffsetY + pMove.OffsetY;
                }
            }
        }

        bool IEditInteraction.get_IsEditableAttribute(object editParams, int attrIndex)
        {
            IMoveInteraction pMove;
            pMove = editParams as IMoveInteraction;
            if (!(pMove == null))
            {
                if ((attrIndex == 0 | attrIndex == 1))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
