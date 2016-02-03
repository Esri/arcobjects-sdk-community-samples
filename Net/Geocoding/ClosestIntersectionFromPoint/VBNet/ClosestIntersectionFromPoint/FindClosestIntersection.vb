Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Location
Imports ESRI.ArcGIS.esriSystem

Namespace ClosestIntersectionFromPoint
	Friend Class FindClosestIntersection
		Private Shared m_License As IAoInitialize = Nothing

		' X and Y values can either be passed in at the command line or from the command prompt after starting
		Shared Sub Main(ByVal args() As String)
			' Initialize the license
			getLicense()

			' See if the address point was passed in at the command line and if not, enter the values now
			Dim X, Y As Double
			If args Is Nothing OrElse args.Length = 0 Then
				Console.WriteLine("Enter a X value: ")
				X = Double.Parse(Console.ReadLine())
				Console.WriteLine("Enter a Y value: ")
				Y = Double.Parse(Console.ReadLine())
			Else
				X = Double.Parse(args(0))
				Y = Double.Parse(args(1))
			End If

			' Call the reverseGeocode method
			ReverseGeocodeIntersection(X, Y)

			Console.WriteLine("Press any key to exit...")
			Console.ReadLine()

			' Return the license
			returnLicense()
		End Sub

		''' <summary>
		''' The main functionality to use Intersection Reverse Geocoding 
		''' </summary>
		''' <param name="X"></param>
		''' <param name="Y"></param>
		Private Shared Sub ReverseGeocodeIntersection(ByVal X As Double, ByVal Y As Double)
      ' Get a locator from the locator Workspace
      Dim obj As System.Object = Activator.CreateInstance(Type.GetTypeFromProgID("esriLocation.LocatorManager"))
			Dim locatorManager As ILocatorManager2 = TryCast(obj, ILocatorManager2)
      Dim locatorWorkspace As ILocatorWorkspace = locatorManager.GetLocatorWorkspaceFromPath("C:\California_fdb.gdb")
			Dim locator As ILocator = locatorWorkspace.GetLocator("CaliforniaTest")
			Dim reverseGeocoding As IReverseGeocoding = TryCast(locator, IReverseGeocoding)

			' Get the spatial reference from the locator
			Dim addressGeocoding As IAddressGeocoding = TryCast(locator, IAddressGeocoding)
			Dim matchFields As IFields = addressGeocoding.MatchFields
			Dim shapeField As IField = matchFields.Field(matchFields.FindField("Shape"))
			Dim spatialReference As ISpatialReference = shapeField.GeometryDef.SpatialReference

			' Set up the point from the X and Y values
			Dim point As IPoint = New PointClass()
			point.SpatialReference = spatialReference
			point.X = X
			point.Y = Y

			' Set the search tolerance for reverse geocoding
			Dim reverseGeocodingProperties As IReverseGeocodingProperties = TryCast(reverseGeocoding, IReverseGeocodingProperties)
			reverseGeocodingProperties.SearchDistance = 2
			reverseGeocodingProperties.SearchDistanceUnits = esriUnits.esriKilometers

			' Determine if the locator supports intersection geocoding.
			' intersectionGeocoding will be null if it is not supported.
			Dim intersectionGeocoding As IIntersectionGeocoding = TryCast(locator, IIntersectionGeocoding)
			If intersectionGeocoding Is Nothing Then
				Console.WriteLine("You must use a locator that supports intersections.  Use a locator that was built off of one" & "of the US Streets Locator styles.")
			Else
				' Find the intersection that is nearest to the Point
				Dim addressProperties As IPropertySet = reverseGeocoding.ReverseGeocode(point, True)

				' Print the intersection properties
				Dim addressInputs As IAddressInputs = TryCast(reverseGeocoding, IAddressInputs)
				Dim addressFields As IFields = addressInputs.AddressFields
				For i As Integer = 0 To addressFields.FieldCount - 1
					Dim addressField As IField = addressFields.Field(i)
                    Console.WriteLine(addressField.AliasName & ": " & addressProperties.GetProperty(addressField.Name).ToString())
				Next i
			End If
		End Sub

		#Region "Helper Methods"

		Private Shared Sub getLicense()
			If (Not ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)) Then
				Throw New Exception("Could not set version. ")
			End If
			m_License = New AoInitializeClass()
			m_License.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)
		End Sub

		Private Shared Sub returnLicense()
			m_License.Shutdown()
		End Sub

		#End Region
	End Class
End Namespace
