Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Timers

Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display

  Public NotInheritable Class MyDynamicLayer : Inherits BaseDynamicLayer
	Public m_bOnce As Boolean = True
	Private m_myGlyph As IDynamicGlyph = Nothing
	Private m_dynamicSymbolProps As IDynamicSymbolProperties2 = Nothing
	Private m_point As IPoint = Nothing
	Private m_stepX As Double = 0
	Private m_stepY As Double = 0
	Private m_updateTimer As Timer = Nothing

	Public Sub New()
		MyBase.New()
	  MyBase.m_sName = "My Dynamic layer"
	  m_updateTimer = New Timer(15)
	  m_updateTimer.Enabled = False
	  AddHandler m_updateTimer.Elapsed, AddressOf OnTimerElapsed
	End Sub

	Public Overrides Sub DrawDynamicLayer(ByVal DynamicDrawPhase As esriDynamicDrawPhase, ByVal Display As IDisplay, ByVal DynamicDisplay As IDynamicDisplay)
	  If DynamicDrawPhase <> esriDynamicDrawPhase.esriDDPImmediate Then
			Return
	  End If

	  If (Not m_bValid) OrElse (Not m_visible) Then
			Return
	  End If

	  Dim visibleExtent As IEnvelope = Display.DisplayTransformation.FittedBounds

	  If m_bOnce Then
			Dim dynamicGlyphFactory As IDynamicGlyphFactory = DynamicDisplay.DynamicGlyphFactory
			m_dynamicSymbolProps = TryCast(DynamicDisplay, IDynamicSymbolProperties2)

			Dim markerSymbol As ICharacterMarkerSymbol = New CharacterMarkerSymbolClass()
            markerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(New Font("ESRI Default Marker", 25.0F, FontStyle.Bold))
			markerSymbol.Size = 25.0
			' set the symbol color to white
            markerSymbol.Color = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor)
			markerSymbol.CharacterIndex = 92

			' create the dynamic glyph
			m_myGlyph = dynamicGlyphFactory.CreateDynamicGlyph(CType(markerSymbol, ISymbol))

			Dim r As Random = New Random()
			Dim X As Double = visibleExtent.XMin + r.NextDouble() * visibleExtent.Width
			Dim Y As Double = visibleExtent.YMin + r.NextDouble() * visibleExtent.Height
			m_point = New PointClass()
			m_point.PutCoords(X, Y)

			m_stepX = visibleExtent.Width / 250
			m_stepY = visibleExtent.Height / 250

			' start the update timer
			m_updateTimer.Enabled = True

			m_bOnce = False
	  End If

	  ' draw the marker
	  m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_myGlyph
	  m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 0.0f, 0.0f, 1.0f)
	  m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f)
	  DynamicDisplay.DrawMarker(m_point)

	  ' update the point location for the next draw cycle
	  m_point.X += m_stepX
	  m_point.Y += m_stepY

	  ' make sure that the point fall within the visible extent
	  If m_point.X > visibleExtent.XMax Then
			m_stepX = -Math.Abs(m_stepX)
	  End If
	  If m_point.X < visibleExtent.XMin Then
			m_stepX = Math.Abs(m_stepX)
	  End If
	  If m_point.Y > visibleExtent.YMax Then
			m_stepY = -Math.Abs(m_stepY)
	  End If
	  If m_point.Y < visibleExtent.YMin Then
			m_stepY = Math.Abs(m_stepY)
	  End If

	  ' set the dirty flag to false since drawing is done.
	  MyBase.m_bIsImmediateDirty = False
	End Sub

	Private Sub OnTimerElapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
	  ' set the dirty flag to true in order to assure the next drawing cycle
	  MyBase.m_bIsImmediateDirty = True
	End Sub
  End Class

