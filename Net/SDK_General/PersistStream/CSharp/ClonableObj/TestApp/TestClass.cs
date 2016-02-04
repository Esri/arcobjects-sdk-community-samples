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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ClonableObj;

namespace TestApp
{
  public sealed class TestClass
  {
    #region class constructor (empty constructor)
    public TestClass()
    {

    }
    #endregion

    public void Test()
    {
      Console.WriteLine("Creating an instance of the clonable object.");
      ClonableObjClass cloneable = new ClonableObjClass();

      Console.WriteLine("Assigning properties");
      cloneable.SpatialReference = CreateGeographicSpatialReference();
      cloneable.Name = "test 1";
      cloneable.Point = new PointClass();
      cloneable.Point.PutCoords(35.02, 31.4);

      cloneable.ManagedArray = new ArrayList();
      cloneable.ManagedArray.Add("One");
      cloneable.ManagedArray.Add("Two");
      cloneable.ManagedArray.Add("Three");

      Console.WriteLine("New object's properties");
      Console.WriteLine("-----------------------");
      Console.WriteLine("Name: " + cloneable.Name);
      Console.WriteLine("ID: " + cloneable.ID.ToString());
      Console.WriteLine("Version: " + cloneable.Version.ToString());
      Console.WriteLine("Array values: ");
      foreach (object obj in cloneable.ManagedArray)
      {
        Console.WriteLine((string)obj);
      }
      Console.WriteLine("Spatial Reference parameters:");
      Console.WriteLine("Name: " + cloneable.SpatialReference.Name);
      IGeographicCoordinateSystem gcs = (IGeographicCoordinateSystem)cloneable.SpatialReference;
      Console.WriteLine("Alias: " + gcs.Alias);
      Console.WriteLine("Datum: " + gcs.Datum.Name);
      Console.WriteLine("");


      Console.WriteLine("Cloning the object...");
      ClonableObjClass clonee = cloneable.Clone() as ClonableObjClass;
      Console.WriteLine("");
      Console.WriteLine("Cloned object's properties:");
      Console.WriteLine("---------------------------");
      Console.WriteLine("Name: " + clonee.Name);
      Console.WriteLine("ID: " + clonee.ID.ToString());
      Console.WriteLine("Version: " + clonee.Version.ToString());
      Console.WriteLine("Array values: ");
      foreach (object obj in clonee.ManagedArray)
      {
        Console.WriteLine((string)obj);
      }
      Console.WriteLine("Spatial Reference parameters:");
      Console.WriteLine("Name: " + clonee.SpatialReference.Name);
      gcs = (IGeographicCoordinateSystem)clonee.SpatialReference;
      Console.WriteLine("Alias: " + gcs.Alias);
      Console.WriteLine("Datum: " + gcs.Datum.Name);
      Console.WriteLine("");

    }

    /// <summary>
    /// create a crazy coordinate system
    /// </summary>
    /// <returns></returns>
    private ISpatialReference CreateGeographicSpatialReference()
    {
      ISpatialReferenceFactory spatialRefFatcory = new SpatialReferenceEnvironmentClass();
      IGeographicCoordinateSystem geoCoordSys;
      geoCoordSys = spatialRefFatcory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);

      //assign a user defined datum to the SR (just for the test)
      IDatum datum = new DatumClass();
      IDatumEdit datumEdit = (IDatumEdit)datum;
      ISpheroid spheroid = spatialRefFatcory.CreateSpheroid((int)esriSRSpheroidType.esriSRSpheroid_Clarke1880);
      datumEdit.DefineEx("MyDatum", "My Datum", "MDTM", "", spheroid);

      IPrimeMeridian primeMeridian = spatialRefFatcory.CreatePrimeMeridian((int)esriSRPrimeMType.esriSRPrimeM_Greenwich);
      IAngularUnit angularUnits = spatialRefFatcory.CreateUnit((int)esriSRUnitType.esriSRUnit_Degree) as IAngularUnit;
      IGeographicCoordinateSystemEdit geoCoordSysEdit = (IGeographicCoordinateSystemEdit)geoCoordSys;
      geoCoordSysEdit.DefineEx("MyGeoCoordSys", "MyGeoCoordSys", "MGCS", "", "", datum, primeMeridian, angularUnits);

      geoCoordSys.SetFalseOriginAndUnits(-180.0, -180.0, 5000000.0);
      geoCoordSys.SetZFalseOriginAndUnits(0.0, 100000.0);
      geoCoordSys.SetMFalseOriginAndUnits(0.0, 100000.0);

      return geoCoordSys as ISpatialReference;
    }

  }
}
