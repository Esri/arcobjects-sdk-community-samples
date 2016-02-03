Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.GlobeCore
Imports ESRI.ArcGIS.Analyst3D

Namespace SetSunPosition
	Public Class SunPositionTool
		Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
		#Region "Member Variables"

		Private m_globe As IGlobe = Nothing
		Private m_globeCamera As IGlobeCamera = Nothing
		Private m_globeViewUtil As IGlobeViewUtil = Nothing
		Private m_globeDisplay As IGlobeDisplay3 = Nothing
		Private m_globeDisplayRendering As IGlobeDisplayRendering = Nothing
		Private m_sceneViewer As ISceneViewer = Nothing
		Private m_bDrawPoint As Boolean = False

		#End Region

		Public Sub New()
			'get the different members
			m_globe = ArcGlobe.Globe
			m_globeDisplay = TryCast(m_globe.GlobeDisplay, IGlobeDisplay3)
			m_globeDisplayRendering = TryCast(m_globeDisplay, IGlobeDisplayRendering)
			m_globeCamera = TryCast(m_globeDisplay.ActiveViewer.Camera, IGlobeCamera)
			m_globeViewUtil = TryCast(m_globeCamera, IGlobeViewUtil)
			m_sceneViewer = m_globeDisplay.ActiveViewer
		End Sub

		#Region "Tool overrides"

		Protected Overrides Sub OnUpdate()
			Enabled = ArcGlobe.Application IsNot Nothing
		End Sub

		Protected Overrides Sub OnActivate()
			'Enable the light source
			m_globeDisplayRendering.IsSunEnabled = True
			'set an ambient light
			m_globeDisplayRendering.AmbientLight = 0.1f
			m_globeDisplayRendering.SunContrast = 30
		End Sub

		Protected Overrides Sub OnMouseDown(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			m_bDrawPoint = True

			'move the light-source according to the mouse coordinate
			Dim lat, lon, alt As Double
			alt = 0
			lon = alt
			lat = lon

			m_globeViewUtil.WindowToGeographic(m_globeDisplay, m_sceneViewer, arg.X, arg.Y, False, lon, lat, alt)

			m_globeDisplayRendering.SetSunPosition(lat, lon)

			'Refresh the display so that the AfterDraw will get called
			m_sceneViewer.Redraw(False)
		End Sub

		Protected Overrides Sub OnMouseMove(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)

		End Sub

		Protected Overrides Sub OnMouseUp(ByVal arg As ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs)
			m_bDrawPoint = False
		End Sub

		Protected Overrides Function OnDeactivate() As Boolean
			'disable the light source
			m_globeDisplayRendering.IsSunEnabled = False
			Return MyBase.OnDeactivate()
		End Function

		#End Region

	End Class

End Namespace
