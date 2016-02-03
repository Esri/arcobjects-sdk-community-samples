Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ClonableObject

Public NotInheritable Class TestClass
#Region "class constructor (empty constructor)"
  Public Sub New()

  End Sub
#End Region

  Public Sub Test()
    Console.WriteLine("Creating an instance of the clonable object.")
    Dim cloneable As ClonableObjClass = New ClonableObjClass()

    Console.WriteLine("Assigning properties")
    cloneable.SpatialReference = CreateGeographicSpatialReference()
    cloneable.Name = "test 1"
    cloneable.Point = New PointClass()
    cloneable.Point.PutCoords(35.02, 31.4)

    cloneable.ManagedArray = New ArrayList()
    cloneable.ManagedArray.Add("One")
    cloneable.ManagedArray.Add("Two")
    cloneable.ManagedArray.Add("Three")

    Console.WriteLine("New object's properties")
    Console.WriteLine("-----------------------")
    Console.WriteLine("Name: " & cloneable.Name)
    Console.WriteLine("ID: " & cloneable.ID.ToString())
    Console.WriteLine("Version: " & cloneable.Version.ToString())
    Console.WriteLine("Array values: ")
    For Each obj As Object In cloneable.ManagedArray
      Console.WriteLine(CStr(obj))
    Next obj
    Console.WriteLine("Spatial Reference parameters:")
    Console.WriteLine("Name: " & cloneable.SpatialReference.Name)
    Dim gcs As IGeographicCoordinateSystem = CType(cloneable.SpatialReference, IGeographicCoordinateSystem)
    Console.WriteLine("Alias: " & gcs.Alias)
    Console.WriteLine("Datum: " & gcs.Datum.Name)
    Console.WriteLine("")


    Console.WriteLine("Cloning the object...")
    Dim clonee As ClonableObjClass = TryCast(cloneable.Clone(), ClonableObjClass)
    Console.WriteLine("")
    Console.WriteLine("Cloned object's properties:")
    Console.WriteLine("---------------------------")
    Console.WriteLine("Name: " & clonee.Name)
    Console.WriteLine("ID: " & clonee.ID.ToString())
    Console.WriteLine("Version: " & clonee.Version.ToString())
    Console.WriteLine("Array values: ")
    For Each obj As Object In clonee.ManagedArray
      Console.WriteLine(CStr(obj))
    Next obj
    Console.WriteLine("Spatial Reference parameters:")
    Console.WriteLine("Name: " & clonee.SpatialReference.Name)
    gcs = CType(clonee.SpatialReference, IGeographicCoordinateSystem)
    Console.WriteLine("Alias: " & gcs.Alias)
    Console.WriteLine("Datum: " & gcs.Datum.Name)
    Console.WriteLine("")

  End Sub

  ''' <summary>
  ''' create a crazy coordinate system
  ''' </summary>
  ''' <returns></returns>
  Private Function CreateGeographicSpatialReference() As ISpatialReference
    Dim spatialRefFatcory As ISpatialReferenceFactory = New SpatialReferenceEnvironmentClass()
    Dim geoCoordSys As IGeographicCoordinateSystem
    geoCoordSys = spatialRefFatcory.CreateGeographicCoordinateSystem(CInt(Fix(esriSRGeoCSType.esriSRGeoCS_WGS1984)))

    'assign a user defined datum to the SR (just for the test)
    Dim datum As IDatum = New DatumClass()
    Dim datumEdit As IDatumEdit = CType(datum, IDatumEdit)
    Dim spheroid As ISpheroid = spatialRefFatcory.CreateSpheroid(CInt(Fix(esriSRSpheroidType.esriSRSpheroid_Clarke1880)))
    datumEdit.DefineEx("MyDatum", "My Datum", "MDTM", "", spheroid)

    Dim primeMeridian As IPrimeMeridian = spatialRefFatcory.CreatePrimeMeridian(CInt(Fix(esriSRPrimeMType.esriSRPrimeM_Greenwich)))
    Dim angularUnits As IAngularUnit = TryCast(spatialRefFatcory.CreateUnit(CInt(Fix(esriSRUnitType.esriSRUnit_Degree))), IAngularUnit)
    Dim geoCoordSysEdit As IGeographicCoordinateSystemEdit = CType(geoCoordSys, IGeographicCoordinateSystemEdit)
    geoCoordSysEdit.DefineEx("MyGeoCoordSys", "MyGeoCoordSys", "MGCS", "", "", datum, primeMeridian, angularUnits)

    geoCoordSys.SetFalseOriginAndUnits(-180.0, -180.0, 5000000.0)
    geoCoordSys.SetZFalseOriginAndUnits(0.0, 100000.0)
    geoCoordSys.SetMFalseOriginAndUnits(0.0, 100000.0)

    Return TryCast(geoCoordSys, ISpatialReference)
  End Function

End Class
