'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Xml
Imports System.Xml.XPath
Imports System.Threading
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem

  ''' <summary>
  ''' Summary description for DynamicBikingCmd.
  ''' </summary>
  <Guid("f01054d2-0130-4124-8436-1bf2942bf2b6"), ClassInterface(ClassInterfaceType.None), ProgId("DynamicBikingCmd")> _
  Public NotInheritable Class DynamicBikingCmd : Inherits BaseCommand
	#Region "COM Registration Function(s)"
	<ComRegisterFunction(), ComVisible(False)> _
	Private Shared Sub RegisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryRegistration(registerType)

	  '
	  ' TODO: Add any COM registration code here
	  '
	End Sub

	<ComUnregisterFunction(), ComVisible(False)> _
	Private Shared Sub UnregisterFunction(ByVal registerType As Type)
	  ' Required for ArcGIS Component Category Registrar support
	  ArcGISCategoryUnregistration(registerType)

	  '
	  ' TODO: Add any COM unregistration code here
	  '
	End Sub

	#Region "ArcGIS Component Category Registrar generated code"
	''' <summary>
	''' Required method for ArcGIS Component Category registration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  ControlsCommands.Register(regKey)

	End Sub
	''' <summary>
	''' Required method for ArcGIS Component Category unregistration -
	''' Do not modify the contents of this method with the code editor.
	''' </summary>
	Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
	  Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
	  ControlsCommands.Unregister(regKey)

	End Sub

	#End Region
	#End Region

	#Region "class members"
	Private Enum GPSPlaybackFormat
	  HST = 0
	  GPX = 1
	  XML = 2
	End Enum
	Private m_playbackFormat As GPSPlaybackFormat = GPSPlaybackFormat.GPX
	Private m_hookHelper As IHookHelper
	Private m_dynamicMap As IDynamicMap = Nothing
	Private m_activeView As IActiveView = Nothing
	Private m_bConnected As Boolean = False

	Private m_gpsPosition As IPoint = Nothing
	Private m_additionalInfoPoint As IPoint = Nothing
	Private m_bikeRouteGeometry As IPointCollection4 = Nothing
	Private m_geometryBridge As IGeometryBridge2 = Nothing
	Private m_wksPoints As WKSPoint() = New WKSPoint(0){}
	Private m_wksPrevPosition As WKSPoint

	Private m_dynamicSymbolProperties As IDynamicSymbolProperties2 = Nothing
	Private m_dynamicCompoundMarker As IDynamicCompoundMarker2 = Nothing
	Private m_dynamicScreenDisplay As IDynamicScreenDisplay = Nothing
	Private m_bikeGlyph As IDynamicGlyph = Nothing
	Private m_bikeRouteGlyph As IDynamicGlyph = Nothing
	Private m_textGlyph As IDynamicGlyph = Nothing
	Private m_catGlyph As IDynamicGlyph = Nothing
	Private m_gpsGlyph As IDynamicGlyph = Nothing
	Private m_heartRateGlyph As IDynamicGlyph()
	Private m_gpsSymbolScale As Single = 1.0f
	Private m_heading As Double = 0
	Private m_heartRateString As String = String.Empty
	Private m_altitudeString As String = String.Empty
	Private m_speed As String = String.Empty
	Private m_bOnce As Boolean = True
	Private m_heartRateCounter As Integer = 0
	Private m_drawCycles As Integer = 0
	Public m_playbackSpeed As Integer = 10
	Private m_bTrackMode As Boolean = False
	Private nullString As String() = Nothing

	Private m_xmlPath As String = String.Empty
	' xml loader thread stuff
	Private m_dataLoaderThread As Thread = Nothing
	Private Shared m_autoEvent As AutoResetEvent = New AutoResetEvent(False)
	Private m_bikePositionCount As Integer = 0

	Private NotInheritable Class BikePositionInfo
	  Public Sub New()

	  End Sub

	  Public position As WKSPoint
	  Public time As DateTime
	  Public altitudeMeters As Double
	  Public heartRate As Integer
	  Public lapCount As Integer
	  Public lapAverageHeartRate As Integer
	  Public lapMaximumHeartRate As Integer
	  Public lapCalories As Integer
	  Public lapMaximumSpeed As Double
	  Public lapDistanceMeters As Double
	  Public course As Double
	  Public speed As Double
	  Public positionCount As Integer
	End Class

	Private m_bikePositionInfo As BikePositionInfo = Nothing

	Private Structure XmlDocTaksData
	  Public xmlDocPath As String
	End Structure

	#End Region

	#Region "class constructor"
	Public Sub New()
	  MyBase.m_category = ".NET Samples"
	  MyBase.m_caption = "Dynamic Biking"
	  MyBase.m_message = "Dynamic Biking"
	  MyBase.m_toolTip = "Dynamic Biking"
	  MyBase.m_name = "DynamicBikingCmd"

	  Try
			Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
			MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap.")
	  End Try
	End Sub

	Protected Overrides Sub Finalize()
	  If Not m_dataLoaderThread Is Nothing AndAlso m_dataLoaderThread.ThreadState = ThreadState.Running Then
			m_autoEvent.Set()
			m_dataLoaderThread.Join()
	  End If
	End Sub
	#End Region

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If hook Is Nothing Then
            Return
        End If

        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook

        m_activeView = m_hookHelper.ActiveView

        m_geometryBridge = New GeometryEnvironmentClass()

        m_wksPrevPosition.X = 0
        m_wksPrevPosition.Y = 0
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        m_dynamicMap = TryCast(m_hookHelper.FocusMap, IDynamicMap)
        If m_dynamicMap Is Nothing Then
            Return
        End If

        If (Not m_dynamicMap.DynamicMapEnabled) Then
            MessageBox.Show("Please enable dynamic mode and try again.")
            Return
        End If

        If (Not m_bConnected) Then
            m_xmlPath = GetPlaybackXmlPath()
            If m_xmlPath = String.Empty Then
                Return
            End If

            m_bikePositionInfo = New BikePositionInfo()
            m_bikePositionInfo.positionCount = m_bikePositionCount
            m_bikePositionInfo.altitudeMeters = 0
            m_bikePositionInfo.time = DateTime.Now
            m_bikePositionInfo.position.X = 0
            m_bikePositionInfo.position.Y = 0
            m_bikePositionInfo.heartRate = 0
            m_bikePositionInfo.lapCount = 0
            m_bikePositionInfo.lapAverageHeartRate = 0
            m_bikePositionInfo.lapMaximumHeartRate = 0
            m_bikePositionInfo.lapDistanceMeters = 0
            m_bikePositionInfo.lapMaximumSpeed = 0
            m_bikePositionInfo.lapCalories = 0

            m_gpsPosition = New PointClass()
            m_additionalInfoPoint = New PointClass()
            m_additionalInfoPoint.PutCoords(70, 90)
            m_bikeRouteGeometry = New PolylineClass()

            ' wire dynamic map events
            AddHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).AfterDynamicDraw, AddressOf OnAfterDynamicDraw
            AddHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).DynamicMapStarted, AddressOf OnDynamicMapStarted

            ' spin the thread that plays the data from the xml file
            m_dataLoaderThread = New Thread(New ParameterizedThreadStart(AddressOf XmlReaderTask))

            Dim taskData As XmlDocTaksData
            taskData.xmlDocPath = m_xmlPath
            m_dataLoaderThread.Start(taskData)
        Else
            ' unwire wire dynamic map events
            RemoveHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).AfterDynamicDraw, AddressOf OnAfterDynamicDraw
            RemoveHandler (CType(m_dynamicMap, IDynamicMapEvents_Event)).DynamicMapStarted, AddressOf OnDynamicMapStarted

            ' force the bike xml playback thread to quite
            m_autoEvent.Set()
            m_dataLoaderThread.Join()

            System.Diagnostics.Trace.WriteLine("Done!!!")
        End If

        m_bConnected = Not m_bConnected
    End Sub

    Public Overrides ReadOnly Property Checked() As Boolean
        Get
            Return m_bConnected
        End Get
    End Property

#End Region

	#Region "public properties"
	Public ReadOnly Property IsPlaying() As Boolean
	  Get
		  Return m_bConnected
	  End Get
	End Property

	Public Property TrackMode() As Boolean
	  Get
		  Return m_bTrackMode
	  End Get
	  Set
		  m_bTrackMode = Value
	  End Set
	End Property

	Public Property PlaybackSpeed() As Integer
	  Get
		  Return m_playbackSpeed
	  End Get
	  Set
		  m_playbackSpeed = Value
	  End Set
	End Property
	#End Region

	#Region "Private methods"
	Private Sub OnAfterDynamicDraw(ByVal DynamicMapDrawPhase As esriDynamicMapDrawPhase, ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)
	  
		if (DynamicMapDrawPhase <> esriDynamicMapDrawPhase.esriDMDPDynamicLayers) then
			return
		End if

		' initialize symbology for dynamic drawing
	  If m_bOnce Then
			' create the glyphs for the bike position as well as for the route
			Dim dynamicGlyphFactory As IDynamicGlyphFactory2 = TryCast(dynamicDisplay.DynamicGlyphFactory, IDynamicGlyphFactory2)
            Dim whiteTransparentColor As IColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor)

			Dim bitmap As Bitmap = New Bitmap(Me.GetType(), "bicycle-icon.bmp")
			m_bikeGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, whiteTransparentColor)

			bitmap = New Bitmap(Me.GetType(), "cat.bmp")
			m_catGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, whiteTransparentColor)

			bitmap = New Bitmap(Me.GetType(), "gps.png")
			m_gpsGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, whiteTransparentColor)

			Dim routeSymbol As ISymbol = CreateBikeRouteSymbol()
			m_bikeRouteGlyph = dynamicGlyphFactory.CreateDynamicGlyph(routeSymbol)

			' create the heart rate glyphs series
			CreateHeartRateAnimationGlyphs(dynamicGlyphFactory)

			' get the default internal text glyph
			m_textGlyph = dynamicGlyphFactory.DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1)

			' do one time casting
			m_dynamicSymbolProperties = TryCast(dynamicDisplay, IDynamicSymbolProperties2)
			m_dynamicCompoundMarker = TryCast(dynamicDisplay, IDynamicCompoundMarker2)
			m_dynamicScreenDisplay = TryCast(dynamicDisplay, IDynamicScreenDisplay)

			m_bOnce = False
	  End If

	  ' draw the trail
	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolLine)= m_bikeRouteGlyph
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolLine, 1.0f, 1.0f, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolLine, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.LineContinuePattern = True
	  dynamicDisplay.DrawPolyline(m_bikeRouteGeometry)

	  If m_playbackFormat = GPSPlaybackFormat.HST Then
			' adjust the bike lap additional info point to draw at the top left corner of the window
			m_additionalInfoPoint.Y = Display.DisplayTransformation.DeviceFrame().bottom - 70

            ' draw additional lap information
			DrawLapInfo(dynamicDisplay)

			' draw the heart-rate and altitude
			DrawHeartRateAnimation(dynamicDisplay, m_gpsPosition)

			' draw the current position as a marker glyph
			m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = m_bikeGlyph
			m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0F, 1.0F, 1.0F, 1.0F)
			m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.2F, 1.2F)
			m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker) = esriDynamicSymbolRotationAlignment.esriDSRANorth
			m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolMarker) = CSng(m_heading - 90)
			dynamicDisplay.DrawMarker(m_gpsPosition)
	  Else
				DrawGPSInfo(dynamicDisplay, m_gpsPosition)
	  End If

	End Sub

	Private Sub OnDynamicMapStarted(ByVal Display As IDisplay, ByVal dynamicDisplay As IDynamicDisplay)
	  SyncLock m_bikePositionInfo
		' update the bike position
		If m_bikePositionInfo.positionCount <> m_bikePositionCount Then
		  ' update the geometry
		  m_gpsPosition.PutCoords(m_bikePositionInfo.position.X, m_bikePositionInfo.position.Y)

		  ' check if needed to update the map extent
		  If m_bTrackMode Then
				Dim mapExtent As IEnvelope = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.FittedBounds
				mapExtent.CenterAt(m_gpsPosition)
				m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds = mapExtent
		  End If

		  ' update the bike trail
		  m_wksPoints(0) = m_bikePositionInfo.position
		  m_geometryBridge.AddWKSPoints(m_bikeRouteGeometry, m_wksPoints)

		  ' get the GPS altitude reading
		  m_altitudeString = String.Format("Altitude: {0} m", m_bikePositionInfo.altitudeMeters.ToString("####.#"))

		  If m_playbackFormat = GPSPlaybackFormat.HST Then
				' calculate the heading
				m_heading = Math.Atan2(m_bikePositionInfo.position.X - m_wksPrevPosition.X, m_bikePositionInfo.position.Y - m_wksPrevPosition.Y)
				m_heading *= (180 / Math.PI)
				If m_heading < 0 Then
					m_heading += 360
				End If

				m_heartRateString = String.Format("{0} BPM", m_bikePositionInfo.heartRate)
		  Else
				m_heading = m_bikePositionInfo.course
				m_speed = String.Format("Speed: {0} MPH", m_bikePositionInfo.speed.ToString("###.#"))
		  End If

		  m_wksPrevPosition.X = m_bikePositionInfo.position.X
		  m_wksPrevPosition.Y = m_bikePositionInfo.position.Y
		  m_bikePositionCount = m_bikePositionInfo.positionCount
		End If
	  End SyncLock

	  ' explicitly call refresh in order to make the dynamic display fire AfterDynamicDraw event 
	  m_hookHelper.ActiveView.Refresh()
	End Sub

	Private Sub XmlReaderTask(ByVal data As Object)
	  Dim bFirst As Boolean = True
	  Dim nextTime As DateTime = DateTime.Now
	  Dim currentTime As DateTime = DateTime.Now
	  Dim lat As Double = 0
	  Dim lon As Double = 0
	  Dim altitude As Double = 0
	  Dim course As Double = 0
	  Dim speed As Double = 0
	  Dim heartRate As Integer = 0
	  Dim trackPoint As XmlNode = Nothing
	  Dim nextTrackPointTimeNode As XmlNode = Nothing

	  Dim taskData As XmlDocTaksData = CType(data, XmlDocTaksData)

	  Dim bikeDataDoc As XmlDocument = New XmlDocument()
	  Dim xmlTextReader As XmlTextReader = New XmlTextReader(m_xmlPath)
	  bikeDataDoc.Load(xmlTextReader)

	  Dim rootElement As XmlElement = bikeDataDoc.DocumentElement

	  If m_playbackFormat = GPSPlaybackFormat.HST Then
			Dim laps As XmlNodeList = rootElement.GetElementsByTagName("Lap")
			For Each lap As XmlNode In laps
				' get lap average and maximum heart rate
				Dim averageHeartRate As XmlNode = (CType(lap, XmlElement)).GetElementsByTagName("AverageHeartRateBpm")(0)
				Dim maximumHeartRate As XmlNode = (CType(lap, XmlElement)).GetElementsByTagName("MaximumHeartRateBpm")(0)
				Dim calories As XmlNode = (CType(lap, XmlElement)).GetElementsByTagName("Calories")(0)
				Dim maxSpeed As XmlNode = (CType(lap, XmlElement)).GetElementsByTagName("MaximumSpeed")(0)
				Dim distance As XmlNode = (CType(lap, XmlElement)).GetElementsByTagName("DistanceMeters")(0)

				' update the position info
				SyncLock m_bikePositionInfo
					m_bikePositionInfo.lapCount += 1
					m_bikePositionInfo.lapAverageHeartRate = Convert.ToInt32(averageHeartRate.InnerText)
					m_bikePositionInfo.lapMaximumHeartRate = Convert.ToInt32(maximumHeartRate.InnerText)
					m_bikePositionInfo.lapCalories = Convert.ToInt32(calories.InnerText)
					m_bikePositionInfo.lapMaximumSpeed = Convert.ToDouble(maxSpeed.InnerText)
					m_bikePositionInfo.lapDistanceMeters = Convert.ToDouble(distance.InnerText)
				End SyncLock

				Dim tracks As XmlNodeList = (CType(lap, XmlElement)).GetElementsByTagName("Track")
				For Each track As XmlNode In tracks
					Dim trackpoints As XmlNodeList = (CType(track, XmlElement)).GetElementsByTagName("Trackpoint")
                    ' read each time one track point ahead in order to calculate the lag time between 
                    ' the current track point and the next one. This time will be used as the wait time
					' for the AutoResetEvent.
					For Each nextTrackPoint As XmlNode In trackpoints
						Dim bNextTime As Boolean = False
						Dim bTime As Boolean = False
						Dim bPosition As Boolean = False
						Dim bAltitude As Boolean = False
						Dim bHeartRate As Boolean = False

						' if this is the first node in the first track make it current
						If bFirst Then
							trackPoint = nextTrackPoint
							bFirst = False
							Continue For
						End If

						' get the time from the next point in order to calculate the lag time
						nextTrackPointTimeNode = (CType(nextTrackPoint, XmlElement)).GetElementsByTagName("Time")(0)
						If nextTrackPointTimeNode Is Nothing Then
							Continue For
						Else
							nextTime = Convert.ToDateTime(nextTrackPointTimeNode.InnerText)
							bNextTime = True
						End If

						If trackPoint.ChildNodes.Count = 4 Then
							For Each trackPointNode As XmlNode In trackPoint.ChildNodes

								If trackPointNode.Name = "Time" Then
									currentTime = Convert.ToDateTime(trackPointNode.InnerText)
									bTime = True
								ElseIf trackPointNode.Name = "Position" Then
									Dim latNode As XmlNode = trackPointNode("LatitudeDegrees")
									lat = Convert.ToDouble(latNode.InnerText)

									Dim lonNode As XmlNode = trackPointNode("LongitudeDegrees")
									lon = Convert.ToDouble(lonNode.InnerText)

									bPosition = True
								ElseIf trackPointNode.Name = "AltitudeMeters" Then
									altitude = Convert.ToDouble(trackPointNode.InnerText)
									bAltitude = True
								ElseIf trackPointNode.Name = "HeartRateBpm" Then
									heartRate = Convert.ToInt32(trackPointNode.InnerText)
									bHeartRate = True
								End If
							Next trackPointNode

							If bNextTime AndAlso bTime AndAlso bPosition AndAlso bAltitude AndAlso bHeartRate Then

								Dim ts As TimeSpan = nextTime.Subtract(currentTime)

								SyncLock m_bikePositionInfo
									m_bikePositionInfo.position.X = lon
									m_bikePositionInfo.position.Y = lat
									m_bikePositionInfo.altitudeMeters = altitude
									m_bikePositionInfo.heartRate = heartRate
									m_bikePositionInfo.time = currentTime
									m_bikePositionInfo.positionCount += 1
								End SyncLock

								' wait for the duration of the time span or bail out if the thread was signaled
								If m_autoEvent.WaitOne(CInt(ts.TotalMilliseconds / m_playbackSpeed), True) Then
									Return
								End If
							End If
						End If

						' make the next track point current
						trackPoint = nextTrackPoint
					Next nextTrackPoint
				Next track
			Next lap
	  Else ' GPX
			Dim trackpoints As XmlNodeList = bikeDataDoc.DocumentElement.GetElementsByTagName("trkpt")

            ' read each time one track point ahead in order to calculate the lag time between 
            ' the current track point and the next one. This time will be used as the wait time
			' for the AutoResetEvent.
			For Each nextTrackPoint As XmlNode In trackpoints
				' if this is the first node in the first track make it current
				If bFirst Then
					trackPoint = nextTrackPoint
					bFirst = False
					Continue For
				End If

				' get the time from the next point in order to calculate the lag time
				nextTrackPointTimeNode = (CType(nextTrackPoint, XmlElement)).GetElementsByTagName("time")(0)
				If nextTrackPointTimeNode Is Nothing Then
					Continue For
				Else
					nextTime = Convert.ToDateTime(nextTrackPointTimeNode.InnerText)
				End If

				lat = Convert.ToDouble(trackPoint.Attributes("lat").InnerText)
				lon = Convert.ToDouble(trackPoint.Attributes("lon").InnerText)

				For Each trackPointNode As XmlNode In trackPoint.ChildNodes
					If trackPointNode.Name = "time" Then
						currentTime = Convert.ToDateTime(trackPointNode.InnerText)
					ElseIf trackPointNode.Name = "ele" Then
						altitude = Convert.ToDouble(trackPointNode.InnerText)
					ElseIf trackPointNode.Name = "course" Then
						course = Convert.ToDouble(trackPointNode.InnerText)
					ElseIf trackPointNode.Name = "speed" Then
						speed = Convert.ToDouble(trackPointNode.InnerText)
					End If
				Next trackPointNode

				Dim ts As TimeSpan = nextTime.Subtract(currentTime)

				SyncLock m_bikePositionInfo
					m_bikePositionInfo.position.X = lon
					m_bikePositionInfo.position.Y = lat
					m_bikePositionInfo.altitudeMeters = altitude
					m_bikePositionInfo.time = currentTime
					m_bikePositionInfo.course = course
					m_bikePositionInfo.speed = speed
					m_bikePositionInfo.positionCount += 1
				End SyncLock

				' wait for the duration of the time span or bail out if the thread was signaled
				If m_autoEvent.WaitOne(CInt(ts.TotalMilliseconds / m_playbackSpeed), True) Then
					Return
				End If

				' make the next track point current
				trackPoint = nextTrackPoint
			Next nextTrackPoint
	  End If

	  ' close the reader when done
	  xmlTextReader.Close()
	End Sub

	Private Function CreateBikeRouteSymbol() As ISymbol
        Dim newColor As IColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 90, 250)), IColor)
	  Dim charMarkerSymbol As ICharacterMarkerSymbol = New CharacterMarkerSymbolClass()
	  charMarkerSymbol.Color = newColor
        charMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(New Font("ESRI Default Marker", 17.0F, FontStyle.Bold))
	  charMarkerSymbol.CharacterIndex = 189
	  charMarkerSymbol.Size = 17

	  Dim markerLineSymbol As IMarkerLineSymbol = New MarkerLineSymbolClass()
	  markerLineSymbol.Color = newColor
	  markerLineSymbol.Width = 17.0
	  markerLineSymbol.MarkerSymbol = CType(charMarkerSymbol, IMarkerSymbol)

	  ' Makes a new Cartographic Line symbol and sets its properties
	  Dim cartographicLineSymbol As ICartographicLineSymbol = TryCast(markerLineSymbol, ICartographicLineSymbol)

	  ' In order to set additional properties like offsets and dash patterns we must create an ILineProperties object
	  Dim lineProperties As ILineProperties = TryCast(cartographicLineSymbol, ILineProperties)
	  lineProperties.Offset = 0

	  ' Here's how to do a template for the pattern of marks and gaps
	  Dim hpe As Double() = New Double(3){}
	  hpe(0) = 0
	  hpe(1) = 39
	  hpe(2) = 1
	  hpe(3) = 0

	  Dim template As ITemplate = New TemplateClass()
	  template.Interval = 1
	  Dim i As Integer = 0
	  Do While i < hpe.Length
			template.AddPatternElement(hpe(i), hpe(i + 1))
		  i = i + 2
	  Loop
	  lineProperties.Template = template

	  ' Set the basic and cartographic line properties
	  cartographicLineSymbol.Color = newColor

        newColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(0, 220, 100)), IColor)

	  ' create a simple line
	  Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
	  simpleLineSymbol.Color = newColor
	  simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid
	  simpleLineSymbol.Width = 1.2

	  Dim multiLayerLineSymbol As IMultiLayerLineSymbol = New MultiLayerLineSymbolClass()
	  multiLayerLineSymbol.AddLayer(CType(cartographicLineSymbol, ILineSymbol))
	  multiLayerLineSymbol.AddLayer(CType(simpleLineSymbol, ILineSymbol))

	  Return TryCast(multiLayerLineSymbol, ISymbol)
	End Function

	Private Sub CreateHeartRateAnimationGlyphs(ByVal dynamicGlyphFactory As IDynamicGlyphFactory2)
        Dim whiteTransparentColor As IColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor)

	  m_heartRateGlyph = New IDynamicGlyph(4){}
	  Dim heartRateIcon As String
	  Dim heartIconBaseName As String = "valentine-heart"
	  Dim bitmap As Bitmap = Nothing
	  Dim imagesize As Integer = 16
	  For i As Integer = 0 To 4
			heartRateIcon = heartIconBaseName & imagesize & ".bmp"
			bitmap = New Bitmap(Me.GetType(), heartRateIcon)
			m_heartRateGlyph(i) = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, whiteTransparentColor)
			m_heartRateGlyph(i).SetAnchor(20.0F, -40.0F)
			imagesize += 2
	  Next i
	End Sub

	Private Sub DrawHeartRateAnimation(ByVal dynamicDisplay As IDynamicDisplay, ByVal bikePoint As IPoint)
	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_heartRateGlyph(m_heartRateCounter)
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker)= esriDynamicSymbolRotationAlignment.esriDSRAScreen
	  m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolMarker)= 0.0f
	  dynamicDisplay.DrawMarker(bikePoint)

	  m_textGlyph.SetAnchor(-35.0f, -50.0f)
	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText)= m_textGlyph
	  m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = True
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f)
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0f, 0.0f, 0.0f, 1.0f)
	  m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft
	  dynamicDisplay.DrawText(bikePoint, m_heartRateString)

	  m_textGlyph.SetAnchor(-20.0f, -30.0f)
	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText)= m_textGlyph
	  dynamicDisplay.DrawText(bikePoint, m_altitudeString)

	  If m_drawCycles Mod 5 = 0 Then
			m_heartRateCounter += 1

			If m_heartRateCounter > 4 Then
				m_heartRateCounter = 0
			End If
	  End If

	  m_drawCycles += 1
	  If m_drawCycles = 5 Then
			m_drawCycles = 0
	  End If
	End Sub

	Private Sub DrawGPSInfo(ByVal dynamicDisplay As IDynamicDisplay, ByVal gpsPosition As IPoint)

		' altitude is already available
		Dim course As String
		Dim speed As String

		SyncLock m_bikePositionInfo
			course = String.Format("Course {0} DEG", m_bikePositionInfo.course.ToString("###.##"))
			speed = String.Format("Speed {0} MPH", m_bikePositionInfo.speed.ToString("###.##"))
		End SyncLock

		Dim gpsInfo As String = String.Format("{0}" & Constants.vbLf & "{1}" & Constants.vbLf & "{2}", course, speed, m_altitudeString)

		m_textGlyph.SetAnchor(-35.0F, -47.0F)
		m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText) = m_textGlyph
		m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = True
		m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolText) = 0.0F
		m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0F, 0.8F, 0.0F, 1.0F)
		m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolText, 1.0F, 1.0F)
		m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0F, 0.0F, 0.0F, 0.6F)
		m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft
		dynamicDisplay.DrawText(m_gpsPosition, gpsInfo)

		m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = m_gpsGlyph
		m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0F, 1.0F, 1.0F, 1.0F)
		m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, m_gpsSymbolScale, m_gpsSymbolScale)
		m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker) = esriDynamicSymbolRotationAlignment.esriDSRANorth
		m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolMarker) = CSng(m_heading - 90)
		dynamicDisplay.DrawMarker(m_gpsPosition)

		If m_drawCycles Mod 5 = 0 Then
			' increment the symbol size
			m_gpsSymbolScale += 0.05F

			If m_gpsSymbolScale > 1.2F Then
				m_gpsSymbolScale = 0.8F
			End If
		End If

		m_drawCycles += 1
		If m_drawCycles = 5 Then
			m_drawCycles = 0
		End If
	End Sub

	Private Sub DrawLapInfo(ByVal dynamicDisplay As IDynamicDisplay)
	  Dim lapCount As String
	  Dim lapInfo As String
	  Dim lapHeartRateInfo As String

	  SyncLock m_bikePositionInfo
		lapCount = String.Format("Lap #{0}", m_bikePositionInfo.lapCount)
		lapInfo = String.Format("Lap information:" & Constants.vbLf & "Distance: {0}m" & Constants.vbLf & "Maximum speed - {1}" & Constants.vbLf & "Calories - {2}", m_bikePositionInfo.lapDistanceMeters.ToString("#####.#"), m_bikePositionInfo.lapMaximumSpeed.ToString("###.#"), m_bikePositionInfo.lapCalories)
		lapHeartRateInfo = String.Format("Lap heart rate info:" & Constants.vbLf & "Average - {0}" & Constants.vbLf & "Maximum - {1}", m_bikePositionInfo.lapAverageHeartRate, m_bikePositionInfo.lapMaximumHeartRate)
	  End SyncLock

	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_catGlyph
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f)
	  m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker)= esriDynamicSymbolRotationAlignment.esriDSRAScreen
	  m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolMarker)= 0.0f

	  m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = True
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0f, 0.8f, 0.0f, 1.0f)
	  m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolFill, 0.0f, 0.0f, 0.0f, 1.0f)
	  m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft
	  m_textGlyph.SetAnchor(0.0f, 0.0f)
	  m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText)= m_textGlyph
		
		Dim strLapInfo As String() = New String() { lapCount, lapInfo, lapHeartRateInfo }
		m_dynamicCompoundMarker.DrawScreenArrayMarker(m_additionalInfoPoint, nullString, nullString, strLapInfo, nullString, nullString)

	End Sub

	Private Function GetPlaybackXmlPath() As String
	  Dim ofd As OpenFileDialog = New OpenFileDialog()
	  ofd.CheckFileExists = True
	  ofd.Multiselect = False
			ofd.Filter = "HST files (*.hst)|*.hst|GPX files (*.gpx)|*.gpx|XML files (*.xml)|*.xml"
	  ofd.RestoreDirectory = True
	  ofd.Title = "Input biking log file"

	  Dim result As DialogResult = ofd.ShowDialog()
	  If result = System.Windows.Forms.DialogResult.OK Then
			Dim fileExtension As String = System.IO.Path.GetExtension(ofd.FileName).ToUpper()
			If fileExtension = ".GPX" Then
				m_playbackFormat = GPSPlaybackFormat.GPX
			ElseIf fileExtension = ".HST" Then
				m_playbackFormat = GPSPlaybackFormat.HST
			ElseIf fileExtension = ".XML" Then
				m_playbackFormat = GPSPlaybackFormat.XML
			End If

			Return ofd.FileName
	  End If
		Return String.Empty

	End Function
	#End Region
End Class
