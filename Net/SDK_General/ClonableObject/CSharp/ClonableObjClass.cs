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
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;

namespace ClonableObject
{
  /// <summary>
  /// This call demonstrated an implementation of a hybrid colnable
  /// class which has both .NET members as well as COM members.
  /// </summary>
  [Guid(ClonableObjClass.GUID)]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("ClonableObject.ClonableObjClass")]
  public sealed class ClonableObjClass : IClone
  {
    #region different types of class members (COM and .NET)
    public const string GUID = "0678ecf9-4066-4a61-94fb-45d6c4753826";

    private int m_version = 1;
    private ISpatialReference m_spatialRef = null;
    private IPoint m_point = null;
    private string m_name = string.Empty;
    private ArrayList m_arr = null;
    private Guid m_ID;
    #endregion

    #region class constructor
    public ClonableObjClass()
    {
      m_ID = Guid.NewGuid();

      m_spatialRef = new UnknownCoordinateSystemClass();
    }
    #endregion

    #region public class properties
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
      get { return m_ID; }
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

    /// <summary>
    /// Assigns the properties of src to the receiver.
    /// </summary>
    /// <param name="src"></param>
    public void Assign(IClone src)
    {
      //1. make sure that src is pointing to a valid object
      if (null == src)
      {
        throw new COMException("Invalid objact.");
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
      m_ID = new Guid(srcClonable.ID.ToString());

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

    /// <summary>
    /// Clones the receiver and assigns the result to clonee.
    /// <returns></returns>
    public IClone Clone()
    {
      //create a new instance of the object
      ClonableObjClass obj = new ClonableObjClass();
      //assign the properties of the new object with the current object's properties.
      //according to each 'Ref' property, the user need to decide whether to use deep cloning
      //or shallow cloning. 
      obj.Assign(this);

      return (IClone)obj;
    }

    /// <summary>
    /// Returns TRUE when the receiver and the other object have the same properties.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsEqual(IClone other)
    {
      //1. make sure that the 'other' object is pointing to a valid object
      if (null == other)
        throw new COMException("Invalid objact.");

      //2. verify the type of 'other'
      if (!(other is ClonableObjClass))
        throw new COMException("Bad object type.");

      ClonableObjClass otherClonable = (ClonableObjClass)other;

      //test that all ot the object's properties are the same.
      //please note the usage of IsEqual when using arcobjects components that
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
        throw new COMException("Invalid objact.");

      //2. verify the type of 'other'
      if (!(other is ClonableObjClass))
        throw new COMException("Bad object type.");

      //3. test if the other is the 'this'
      if ((ClonableObjClass)other == this)
        return true;

      return false;
    }

    #endregion
  }
}
