Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Analyst3D

Namespace GlobeGraphicsToolbar
	Public Class GeographicCoordinates
		Private _longitude As Double
		Private _latitude As Double
		Private _altitudeInKilometers As Double

		Public Sub New(ByVal globe As IGlobe, ByVal screenX As Integer, ByVal screenY As Integer)
			GetGeographicCoordinates(globe, screenX, screenY, _longitude, _latitude, _altitudeInKilometers)
		End Sub

		Private Sub GetGeographicCoordinates(ByVal globe As IGlobe, ByVal screenX As Integer, ByVal screenY As Integer, ByRef longitude As Double, ByRef latitude As Double, ByRef altitudeInKilometers As Double)
			Dim globeDisplay As IGlobeDisplay = globe.GlobeDisplay

			Dim sceneViewer As ISceneViewer = globeDisplay.ActiveViewer

			Dim camera As ICamera = globeDisplay.ActiveViewer.Camera

			Dim globeViewUtil As IGlobeViewUtil = TryCast(camera, IGlobeViewUtil)

			globeViewUtil.WindowToGeographic(globeDisplay, sceneViewer, screenX, screenY, True, longitude, latitude, altitudeInKilometers)
		End Sub

		Public ReadOnly Property Longitude() As Double
			Get
				Return _longitude
			End Get
		End Property

		Public ReadOnly Property Latitude() As Double
			Get
				Return _latitude
			End Get
		End Property

		Public ReadOnly Property AltitudeInKilometers() As Double
			Get
				Return _altitudeInKilometers
			End Get
		End Property
	End Class
End Namespace