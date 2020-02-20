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

namespace AroundPoint
{
    //[ClassInterface(ClassInterfaceType.None)]
    [Guid("CBE279B8-B27C-4102-ACB5-E35D883F7D79")]
    public class AroundPointCls : IMarkerPlacement, IGraphicAttributes, IPersistVariant
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
            MarkerPlacement.Register(regKey);
        }
        static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = String.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MarkerPlacement.Unregister(regKey);
        }
        #endregion

        public double m_Radius ;
        public int m_Number;
        public double m_X0 ;
        public double m_Y0;
        public int m_Iter ;
        public IAffineTransformation2D m_pT;

        public AroundPointCls()
        {
             m_Radius = 10;
             m_Number = 6;
             m_pT =new AffineTransformation2DClass();
        }
        
        #region IGraphicAttributes Members

        string IGraphicAttributes.ClassName
        {
            get
            {
                return "Around_point";
            }

        }

        int IGraphicAttributes.GraphicAttributeCount
        {
            get
            {
                return 2;
            }

        }

        int IGraphicAttributes.get_ID(int attrIndex)
        {
            if ((attrIndex >= 0) && (attrIndex < 2))
                return attrIndex;
            else return -1;
        }

        int IGraphicAttributes.get_IDByName(string Name)
        {
            if (Name == "Radius")
                return 0;
            if (Name == "Number")
                return 1;
            else return -1;
        }

        string IGraphicAttributes.get_Name(int attrId)
        {
            if (attrId == 0)
                return "Radius";
            if (attrId == 1)
                return "Number";
            else return " ";
        }

        IGraphicAttributeType IGraphicAttributes.get_Type(int attrId)
        {
            if (attrId == 0)
                return new GraphicAttributeSizeType();

            if (attrId == 1)
                return new GraphicAttributeIntegerType();
            else return null;

        }

        object IGraphicAttributes.get_Value(int attrId)
        {
            if (attrId == 0)
            {
                return m_Radius;
            }
            if (attrId == 1)
            {
                return m_Number;
            }
            else return null;
        }

        void IGraphicAttributes.set_Value(int attrId, object val)
        {

            if ((attrId == 0) && ((double)val > 0))
                m_Radius = (double)val;
            else if ((attrId == 1) && ((int)val > 0))
                m_Number = (int)val;
        }

        #endregion

        #region IPersistVariant Members

        UID IPersistVariant.ID
        {
            get
            {
                UID pUID;
                pUID = new UID();
                pUID.Value = "{CBE279B8-B27C-4102-ACB5-E35D883F7D79}"; //"SineWaveGE.SineWave";
                return pUID;
            }
        }

        void IPersistVariant.Load(IVariantStream Stream)
        {

            int version;
            version = (int)Stream.Read();
            m_Radius = (double)Stream.Read();
            m_Number = (int)Stream.Read();
        }

        void IPersistVariant.Save(IVariantStream Stream)
        {
            int version;
            version = 1;
            Stream.Write(version);
            Stream.Write(m_Radius);
            Stream.Write(m_Number);
        }

        #endregion
        
        #region IMarkerPlacement Members

        IAffineTransformation2D IMarkerPlacement.NextTransformation()
        {
            if (m_Iter != m_Number)
            {
                double angle;
                angle = m_Iter * 2 * 3.141592 / m_Number;
                double x, y;
                x = m_X0 + m_Radius * Math.Cos (angle);
                y = m_Y0 + m_Radius * Math.Sin(angle);
                m_pT.Reset();
                m_pT.Rotate (angle);
                m_pT.Move ( x, y);                
                m_Iter = m_Iter + 1;
                return m_pT;
            }
            else 
            return null;
        }

        void IMarkerPlacement.Reset(IGeometry geom)
        {
            m_Iter = -1;
            IPoint pP;
            pP = (IPoint) geom;
            if (pP == null)
            {
            }
            else
            {
                pP.QueryCoords(out m_X0, out m_Y0);
                m_Iter = 0;
            }
        }

        bool IMarkerPlacement.get_AcceptGeometryType(esriGeometryType inputType)
        {
            if (inputType == esriGeometryType.esriGeometryPoint)
                return true;
            else
                return false;
        }

        #endregion
    }
}
