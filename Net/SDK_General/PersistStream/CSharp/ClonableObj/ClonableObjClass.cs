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
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.IO;

namespace ClonableObj
{
  [Guid(ClonableObjClass.GUID)]
  [ProgId("ClonableObj.ClonableObjClass")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  public sealed class ClonableObjClass : ESRI.ArcGIS.esriSystem.IClone, ESRI.ArcGIS.esriSystem.IPersistStream
  {
    public const string GUID = "C97C7707-3A8B-4933-953E-8AF605851FCC";

    //class members
    private int m_version = 1;
    private ISpatialReference m_spatialRef = null;
    private IPoint m_point = null;
    private string m_name = string.Empty;
    private ArrayList m_arr = null;
    private Guid m_ID;

    public ClonableObjClass()
    {
      m_ID = Guid.NewGuid();

      m_spatialRef = new UnknownCoordinateSystemClass();
    }

    #region public properties
    public string Name
    {
      get { return m_name; }
      set { m_name = value; }
    }

    public int Version
    {
      get { return m_version; }
    }

    public ISpatialReference SpatialReference
    {
      get { return m_spatialRef; }
      set { m_spatialRef = value; }
    }

    public Guid ID
    {
      get { return m_ID;  }
    }

    public IPoint Point
    {
      get { return m_point; }
      set { m_point = value; }
    }

    public ArrayList ManagedArray
    {
      get { return m_arr; }
      set { m_arr = value; }
    }
    #endregion

    #region IClone Members

    public void Assign(IClone src)
    {
      //1. make sure that src is pointing to a valid object
      if (null == src)
      {
        throw new COMException("Invalid object.");
      }

      //2. make sure that the type of src is of type 'ClonableObjClass'
      if (!(src is ClonableObjClass))
      {
        throw new COMException("Bad object type.");
      }

      //3. assign the properties of src to the current instance
      ClonableObjClass srcClonable = (ClonableObjClass)src;
      m_name = srcClonable.Name;
      m_version = srcClonable.Version;
      
      //it is not possible to copy guids...
      //m_ID = srcClonable.ID;

      //don't clone the spatial reference, since in this case we want both object to 
      //reference the same spatial reference (for example like features in a featureclass 
      //which share the same spatial reference)
      m_spatialRef = srcClonable.SpatialReference;

      //clone the point. Use deep cloning 
      if (null == srcClonable.Point)
        m_point = null;
      else
      {
        IObjectCopy objectCopy = new ObjectCopyClass();
        object obj = objectCopy.Copy((object)srcClonable.Point);
        m_point = (IPoint)obj;
      }

      m_arr = (ArrayList)srcClonable.ManagedArray.Clone();
    }

    public IClone Clone()
    {
      //ClonableObjClass obj = new ClonableObjClass();
      //obj.Assign(this);

      //use the C++ way to clone the object. Write it into a memory stream
      IObjectCopy objectCopy = new ObjectCopyClass();
      object obj = objectCopy.Copy((object)this);

      return (IClone)obj;
    }

    public bool IsEqual(IClone other)
    {
      //1. make sure that the 'other' object is pointing to a valid object
      if (null == other)
        throw new COMException("Invalid object.");

      //2. verify the type of 'other'
      if (!(other is ClonableObjClass))
        throw new COMException("Bad object type.");

      ClonableObjClass otherClonable = (ClonableObjClass)other;

      //test that all ot the object's properties are the same.
      //please note the usage of IsEqual when using ArcObjects components that
      //supports cloning
      if (otherClonable.Version == m_version &&
        otherClonable.Name == m_name &&
        otherClonable.ID == m_ID &&
        otherClonable.ManagedArray == m_arr &&
        ((IClone)otherClonable.SpatialReference).IsEqual((IClone)m_spatialRef) &&
        ((IClone)otherClonable.Point).IsEqual((IClone)m_point))

        return true;

      return false;      
    }

    public bool IsIdentical(IClone other)
    {
      //1. make sure that the 'other' object is pointing to a valid object
      if (null == other)
        throw new COMException("Invalid object.");

      //2. verify the type of 'other'
      if (!(other is ClonableObjClass))
        throw new COMException("Bad object type.");

      //3. test if the other is the 'this'
      if ((ClonableObjClass)other == this)
        return true;

      return false;
    }

    #endregion

    #region IPersistStream Members

    public void GetClassID(out Guid pClassID)
    {
      pClassID = new Guid(ClonableObjClass.GUID);
    }

    public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
    {
      pcbSize.QuadPart = 0;
    }

    public void IsDirty()
    {
      return;
    }

    public void Load(IStream pStm)
    {
      System.Runtime.InteropServices.ComTypes.IStream stream = (System.Runtime.InteropServices.ComTypes.IStream)pStm;

      //load the information from the stream
      object obj = null;
      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_version = Convert.ToInt32(obj);

      obj = PeristStream.PeristStreamHelper.Load(stream);
      byte[] arr = (byte[])obj;
      m_ID = new Guid(arr);

      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_name = Convert.ToString(obj);

      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_spatialRef = obj as ISpatialReference;

      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_ID = (Guid)obj;

      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_point = obj as IPoint;

      obj = PeristStream.PeristStreamHelper.Load(stream);
      m_arr = obj as ArrayList;
    }

    public void Save(IStream pStm, int fClearDirty)
    {
      System.Runtime.InteropServices.ComTypes.IStream stream = (System.Runtime.InteropServices.ComTypes.IStream)pStm;

      //save the different objects to the stream
      PeristStream.PeristStreamHelper.Save(stream, m_version);
      PeristStream.PeristStreamHelper.Save(stream, m_ID.ToByteArray());
      PeristStream.PeristStreamHelper.Save(stream, m_name);
      PeristStream.PeristStreamHelper.Save(stream, m_spatialRef);

      //save the guid
      PeristStream.PeristStreamHelper.Save(stream, m_ID);

      //save the point to the stream
      if (null == m_point)
        m_point = new PointClass();

      PeristStream.PeristStreamHelper.Save(stream, m_point);

      if (null == m_arr)
        m_arr = new ArrayList();

      PeristStream.PeristStreamHelper.Save(stream, m_arr);
    }

    #endregion
  }
}
