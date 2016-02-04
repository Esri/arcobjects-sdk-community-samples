'Copyright 2016 Esri

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
Imports System.Collections
Imports System.Data
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.Threading
Imports System.Timers
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Microsoft.Win32
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.DataSourcesFile

  #Region "WeatherItemEventArgs class members"
  Public NotInheritable Class WeatherItemEventArgs : Inherits EventArgs
		Private m_id As Integer
		Private m_zipCode As Long
		Private m_x As Double
		Private m_y As Double
		Private m_iconWidth As Integer
		Private m_iconHeight As Integer

	Public Sub New(ByVal newID As Integer, ByVal newZipCode As Long, ByVal X As Double, ByVal Y As Double, ByVal newIconWidth As Integer, ByVal newIconHeight As Integer)
			m_id = newID
			m_zipCode = newZipCode
			m_x = X
			m_y = Y
			m_iconWidth = newIconWidth
			m_iconHeight = newIconHeight
	End Sub

		Public ReadOnly Property ID() As Integer
			Get
				Return m_id
			End Get
		End Property
		Public ReadOnly Property ZipCode() As Long
			Get
				Return m_zipCode
			End Get
		End Property
		Public ReadOnly Property mapY() As Double
			Get
				Return m_y
			End Get
		End Property
		Public ReadOnly Property mapX() As Double
			Get
				Return m_x
			End Get
		End Property
		Public ReadOnly Property IconWidth() As Integer
			Get
				Return m_iconWidth
			End Get
		End Property
		Public ReadOnly Property IconHeight() As Integer
			Get
				Return m_iconHeight
			End Get
		End Property
  End Class
  #End Region

	'declare delegates for the event handling
	Public Delegate Sub WeatherItemAdded(ByVal sender As Object, ByVal args As WeatherItemEventArgs)
	Public Delegate Sub WeatherItemsUpdated(ByVal sender As Object, ByVal args As EventArgs)


  ''' <summary>
  ''' RSSWeatherLayerClass is a custom layer for ArcMap/MapControl. It inherits CustomLayerBase
  ''' which implements the relevant interfaces required by the Map.
  ''' This sample is a comprehensive sample of a real life scenario for creating a new layer in 
  ''' order to consume a web service and display the information in a map.
  ''' In this sample you can find implementation of simple editing capabilities, selection by 
  ''' attribute and by location, persistence and identify.
  ''' </summary>
  <Guid("3460FB55-4326-4d28-9F96-D62211B0C754"), ClassInterface(ClassInterfaceType.None), ComVisible(True), ProgId("RSSWeatherLayerClass")> _
  Public NotInheritable Class RSSWeatherLayerClass : Inherits BaseDynamicLayer : Implements IIdentify
	#Region "class members"

	Private m_timer As System.Timers.Timer = Nothing
	Private m_updateThread As Thread = Nothing
	Private m_iconFolder As String = String.Empty
	Private m_table As DataTable = Nothing
	Private m_symbolTable As DataTable = Nothing
	Private m_locations As DataTable = Nothing
	Private m_selectionSymbol As ISymbol = Nothing
	Private m_display As IDisplay = Nothing
	Private m_dataFolder As String = String.Empty
	Private m_layerSRFactoryCode As Integer = 0
	Private m_symbolSize As Integer = 32

	Private m_point As IPoint = Nothing
	Private m_llPnt As IPoint = Nothing
	Private m_urPnt As IPoint = Nothing
	Private m_env As IEnvelope = Nothing

	' dynamic display members
	Private m_dynamicGlyphFactory As IDynamicGlyphFactory2 = Nothing
	Private m_dynamicSymbolProperties As IDynamicSymbolProperties2 = Nothing
	Private m_dynamicCompoundMarker As IDynamicCompoundMarker2 = Nothing
	Private m_textGlyph As IDynamicGlyph = Nothing
	Private m_selectionGlyph As IDynamicGlyph = Nothing
	Private m_bDDOnce As Boolean = True

	'weather items events
	Public Event OnWeatherItemAdded As WeatherItemAdded
	Public Event OnWeatherItemsUpdated As WeatherItemsUpdated
	#End Region

	#Region "Constructor"
	''' <summary>
	''' The class has only default CTor.
	''' </summary>
	Public Sub New()
		MyBase.New()
	  Try
			'set the layer's name
			MyBase.m_sName = "RSS Weather Layer"
			'ask the Map to create a separate cache for the layer
			MyBase.m_IsCached = False

			' the underlying data for this layer is always in WGS1984 geographical coordinate system
			m_spatialRef = CreateGeographicSpatialReference()
			m_layerSRFactoryCode = m_spatialRef.FactoryCode

			'get the directory for the layer's cache. If it does not exist, create it.
			m_dataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RSSWeatherLayer")
			If (Not System.IO.Directory.Exists(m_dataFolder)) Then
				System.IO.Directory.CreateDirectory(m_dataFolder)
			End If
			m_iconFolder = m_dataFolder

			'instantiate the timer for the weather update
			m_timer = New System.Timers.Timer(1000)
			m_timer.Enabled = False
			AddHandler m_timer.Elapsed, AddressOf OnUpdateTimer

			'initialize the layer's tables (main table as well as the symbols table)
			InitializeTables()

			'get the location list from a featureclass (US major cities) and synchronize it with the 
			'cached information in case it exists.
			If Nothing Is m_locations Then
				InitializeLocations()
			End If

			'initialize the selection symbol used to highlight selected weather items
			InitializeSelectionSymbol()

			m_point = New PointClass()
			m_llPnt = New PointClass()
			m_urPnt = New PointClass()
			m_env = New EnvelopeClass()

			'connect to the RSS service
			Connect()
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message)
			End Try
	End Sub

	Protected Overrides Sub Finalize()
	  Disconnect()
	End Sub
	#End Region

#Region "Overridden methods"

    ''' <summary>
    ''' Draws the layer to the specified display for the given draw phase. 
    ''' </summary>
    ''' <param name="drawPhase"></param>
    ''' <param name="Display"></param>
    ''' <param name="trackCancel"></param>
    ''' <remarks>the draw method is set as an abstract method and therefore must be overridden</remarks>
    Public Overrides Sub Draw(ByVal drawPhase As esriDrawPhase, ByVal Display As IDisplay, ByVal trackCancel As ITrackCancel)
        If drawPhase <> esriDrawPhase.esriDPGeography Then
            Return
        End If
        If Display Is Nothing Then
            Return
        End If
        If m_table Is Nothing OrElse m_symbolTable Is Nothing Then
            Return
        End If

        m_display = Display

        Dim envelope As IEnvelope = TryCast(Display.DisplayTransformation.FittedBounds, IEnvelope)

        Dim lat, lon As Double
        Dim iconCode As Integer
        Dim selected As Boolean
        Dim symbol As ISymbol = Nothing

        'loop through the rows. Draw each row that has a shape
        For Each row As DataRow In m_table.Rows
            'get the Lat/Lon of the item
            lat = Convert.ToDouble(row(3))
            lon = Convert.ToDouble(row(4))
            'get the icon ID
            iconCode = Convert.ToInt32(row(8))

            'get the selection state of the item
            selected = Convert.ToBoolean(row(13))

            If lon >= envelope.XMin AndAlso lon <= envelope.XMax AndAlso lat >= envelope.YMin AndAlso lat <= envelope.YMax Then
                'search for the symbol in the symbology table
                symbol = GetSymbol(iconCode, row)
                If Nothing Is symbol Then
                    Continue For
                End If

                m_point.X = lon
                m_point.Y = lat
                m_point.SpatialReference = m_spatialRef

                'reproject the point to the DataFrame's spatial reference
                If Not Nothing Is m_mapSpatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode Then
                    m_point.Project(m_mapSpatialRef)
                End If

                Display.SetSymbol(symbol)
                Display.DrawPoint(m_point)

                If selected Then
                    Display.SetSymbol(m_selectionSymbol)
                    Display.DrawPoint(m_point)
                End If
            End If
        Next row
    End Sub

    ''' <summary>
    ''' Draw the layer while in dynamic mode
    ''' </summary>
    ''' <param name="DynamicDrawPhase"></param>
    ''' <param name="Display"></param>
    ''' <param name="DynamicDisplay"></param>
    Public Overrides Sub DrawDynamicLayer(ByVal DynamicDrawPhase As esriDynamicDrawPhase, ByVal Display As IDisplay, ByVal DynamicDisplay As IDynamicDisplay)
        If DynamicDrawPhase <> esriDynamicDrawPhase.esriDDPCompiled Then
            Return
        End If

        If (Not m_bValid) OrElse (Not m_visible) Then
            Return
        End If

        If m_bDDOnce Then
            m_dynamicGlyphFactory = TryCast(DynamicDisplay.DynamicGlyphFactory, IDynamicGlyphFactory2)
            m_dynamicSymbolProperties = TryCast(DynamicDisplay, IDynamicSymbolProperties2)
            m_dynamicCompoundMarker = TryCast(DynamicDisplay, IDynamicCompoundMarker2)

            m_textGlyph = m_dynamicGlyphFactory.DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1)

            ' create glyph for the selection symbol
            If m_selectionSymbol Is Nothing Then
                InitializeSelectionSymbol()
            End If

            m_selectionGlyph = m_dynamicGlyphFactory.CreateDynamicGlyph(m_selectionSymbol)

            m_bDDOnce = False
        End If

        m_display = Display

        Dim lat, lon As Double
        Dim iconCode As Integer
        Dim iconWidth As Integer = 0
        Dim selected As Boolean
        Dim dynamicGlyph As IDynamicGlyph = Nothing
        Dim symbolSized As Single
        Dim citiName As String = String.Empty
        Dim temperature As String = String.Empty

        'loop through the rows. Draw each row that has a shape
        For Each row As DataRow In m_table.Rows
            'get the Lat/Lon of the item
            lat = Convert.ToDouble(row(3))
            lon = Convert.ToDouble(row(4))
            'get the icon ID
            iconCode = Convert.ToInt32(row(8))

            ' get citiname and temperature
            citiName = Convert.ToString(row(2))
            temperature = String.Format("{0} F", row(5))

            'get the selection state of the item
            selected = Convert.ToBoolean(row(13))

            'search for the symbol in the symbology table
            dynamicGlyph = GetDynamicGlyph(m_dynamicGlyphFactory, iconCode, row, iconWidth)
            If Nothing Is dynamicGlyph Then
                Continue For
            End If

            m_point.X = lon
            m_point.Y = lat
            m_point.SpatialReference = m_spatialRef

            'reproject the point to the DataFrame's spatial reference
            If Not Nothing Is m_spatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode Then
                m_point.Project(m_mapSpatialRef)
            End If

            symbolSized = 1.35F * CSng(m_symbolSize / CDbl(iconWidth))

            ' draw the weather item

            ' 1. set the whether symbol properties
            m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = dynamicGlyph
            m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker) = esriDynamicSymbolRotationAlignment.esriDSRAScreen
            m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolMarker) = 0.0F
            m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0F, 1.0F, 1.0F, 1.0F)
            m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolMarker, symbolSized, symbolSized)
            m_dynamicSymbolProperties.Smooth(esriDynamicSymbolType.esriDSymbolMarker) = False

            ' 2. set the text properties
            m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText) = m_textGlyph
            m_dynamicSymbolProperties.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker) = esriDynamicSymbolRotationAlignment.esriDSRAScreen
            m_dynamicSymbolProperties.Heading(esriDynamicSymbolType.esriDSymbolText) = 0.0F
            m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolText, 0.0F, 0.85F, 0.0F, 1.0F)
            m_dynamicSymbolProperties.SetScale(esriDynamicSymbolType.esriDSymbolText, 1.0F, 1.0F)
            m_dynamicSymbolProperties.Smooth(esriDynamicSymbolType.esriDSymbolText) = False
            m_dynamicSymbolProperties.TextBoxUseDynamicFillSymbol = False
            m_dynamicSymbolProperties.TextBoxHorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter
            m_dynamicSymbolProperties.TextRightToLeft = False

            ' draw both the icon and the text as a compound marker
            m_dynamicCompoundMarker.DrawCompoundMarker2(m_point, temperature, citiName)

            If selected Then ' draw the selected symbol
                m_dynamicSymbolProperties.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0F, 1.0F, 1.0F, 1.0F)
                m_dynamicSymbolProperties.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker) = m_selectionGlyph
                DynamicDisplay.DrawMarker(m_point)
            End If
        Next row

        MyBase.m_bIsCompiledDirty = False
    End Sub

    ''' <summary>
    ''' The spatial reference of the underlying data.
    ''' </summary>
    Public Overrides ReadOnly Property SpatialReference() As ISpatialReference
        Get
            If Nothing Is m_spatialRef Then
                m_spatialRef = CreateGeographicSpatialReference()
            End If
            Return m_spatialRef
        End Get
    End Property

    ''' <summary>
    ''' The ID of the object. 
    ''' </summary>
    Public Overrides ReadOnly Property ID() As ESRI.ArcGIS.esriSystem.UID
        Get
            Dim uid As UID = New UIDClass()
            uid.Value = "RSSWeatherLayerClass"

            Return uid
        End Get
    End Property

    ''' <summary>
    ''' The default area of interest for the layer. Returns the spatial-referenced extent of the layer. 
    ''' </summary>
    Public Overrides ReadOnly Property AreaOfInterest() As IEnvelope
        Get
            Return Me.Extent
        End Get
    End Property

    ''' <summary>
    ''' The layer's extent which is a union of the extents of all the items of the layer 
    ''' </summary>
    ''' <remarks>In case where the DataFram's spatial reference is different than the underlying
    ''' data's spatial reference the envelope must be projected</remarks>
    Public Overrides ReadOnly Property Extent() As IEnvelope
        Get
            m_extent = GetLayerExtent()
            If Nothing Is m_extent Then
                Return Nothing
            End If

            Dim env As IEnvelope = TryCast((CType(m_extent, IClone)).Clone(), IEnvelope)
            If Not Nothing Is m_mapSpatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode Then
                env.Project(m_mapSpatialRef)
            End If

            Return env
        End Get
    End Property

    ''' <summary>
    ''' Map tip text at the specified mouse location.
    ''' </summary>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <param name="Tolerance"></param>
    ''' <returns></returns>
    Public Overrides Function get_TipText(ByVal X As Double, ByVal Y As Double, ByVal Tolerance As Double) As String
        Dim envelope As IEnvelope = New EnvelopeClass()
        envelope.PutCoords(X - Tolerance, Y - Tolerance, X + Tolerance, Y + Tolerance)

        'reproject the envelope to the datasource coordinate system
        If Not Nothing Is m_mapSpatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode Then
            envelope.SpatialReference = m_spatialRef
            envelope.Project(m_mapSpatialRef)
        End If

        Dim xmin, ymin, xmax, ymax As Double
        envelope.QueryCoords(xmin, ymin, xmax, ymax)

        'select all the records within the given extent
        Dim qry As String = "LON >= " & xmin.ToString() & " AND LON <= " & xmax.ToString() & " AND Lat >= " & ymin.ToString() & " AND LAT <= " & ymax.ToString()
        Dim rows As DataRow() = m_table.Select(qry)
        If 0 = rows.Length Then
            Return String.Empty
        End If

        Dim r As DataRow = rows(0)
        Dim zipCode As String = Convert.ToString(r(1))
        Dim cityName As String = Convert.ToString(r(2))
        Dim temperature As String = Convert.ToString(r(5))

        Return cityName & ", " & zipCode & ", " & temperature & "F"
    End Function

#End Region

	#Region "public methods"

	''' <summary>
	''' connects to RSS weather service
	''' </summary>
		Public Sub Connect()
	  'enable the update timer
			m_timer.Enabled = True

			MyBase.m_bIsCompiledDirty = True
		End Sub

	''' <summary>
	''' disconnects from RSS weather service
	''' </summary>
		Public Sub Disconnect()
	  'disable the update timer
	  m_timer.Enabled = False

			Try
		'abort the update thread in case that it is alive
				If m_updateThread.IsAlive Then
					m_updateThread.Abort()
				End If
			Catch
				System.Diagnostics.Trace.WriteLine("WeatherLayer update thread has been terminated")
			End Try
		End Sub

	''' <summary>
	''' select a weather item by its zipCode
	''' </summary>
	''' <param name="zipCode"></param>
	''' <param name="newSelection"></param>
		Public Sub [Select](ByVal zipCode As Long, ByVal newSelection As Boolean)
			If Nothing Is m_table Then
				Return
			End If

			If newSelection Then
				UnselectAll()
			End If

			Dim rows As DataRow() = m_table.Select("ZIPCODE = " & zipCode.ToString())
			If rows.Length = 0 Then
				Return
			End If

			Dim rec As DataRow = rows(0)
			SyncLock m_table
				'13 is the selection column ID
				rec(13) = True
				rec.AcceptChanges()
			End SyncLock
			MyBase.m_bIsCompiledDirty = True
		End Sub

	''' <summary>
	''' unselect all weather items
	''' </summary>
		Public Sub UnselectAll()
			If Nothing Is m_table Then
				Return
			End If

			'unselect all the currently selected items
			SyncLock m_table
				For Each r As DataRow In m_table.Rows
					'13 is the selection column ID
					r(13) = False
				Next r
				m_table.AcceptChanges()
			End SyncLock

			MyBase.m_bIsCompiledDirty = True
		End Sub

	''' <summary>
	''' Run the update thread
	''' </summary>
	''' <remarks>calling this method to frequently might end up in blockage of RSS service.
	''' The service will interpret the excessive calls as an offence and thus would block the service for a while.</remarks>
		Public Sub Refresh()
			Try
				m_updateThread = New Thread(AddressOf ThreadProc)

				'run the update thread
				m_updateThread.Start()
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message)
			End Try
		End Sub

	''' <summary>
	''' add a new item given only a zipcode (will use the default location given by the service)
	''' should the item exists, it will get updated
	''' </summary>
	''' <param name="zipCode"></param>
	''' <returns></returns>
		Public Function AddItem(ByVal zipCode As Long) As Boolean
			Return AddItem(zipCode, 0.0,0.0)
		End Function

	''' <summary>
	''' adds a new item given a zipcode and a coordinate.
	''' Should the item already exists, it will get updated and will move to the new coordinate.
	''' </summary>
	''' <param name="zipCode"></param>
	''' <param name="lat"></param>
	''' <param name="lon"></param>
	''' <returns></returns>
		Public Function AddItem(ByVal zipCode As Long, ByVal lat As Double, ByVal lon As Double) As Boolean
			If Nothing Is m_table Then
				Return False
			End If

			Dim r As DataRow = m_table.Rows.Find(zipCode)
			If Not Nothing Is r Then 'if the record with this zipCode already exists
				'in case that the record exists and the input coordinates are not valid 
				If lat = 0.0 AndAlso lon = 0.0 Then
					Return False
				Else 'update the location according to the new coordinate
					SyncLock m_table
						r(3) = lat
						r(4) = lon

						r.AcceptChanges()
					End SyncLock
				End If
			Else
				'add new zip code to the locations list
				Dim rec As DataRow = m_locations.NewRow()
				SyncLock m_locations
					rec(1) = zipCode
					m_locations.Rows.Add(rec)
				End SyncLock

				'need to connect to the service and get the info
				AddWeatherItem(zipCode, lat, lon)
			End If

			Return True
		End Function

	''' <summary>
	''' delete an item from the dataset
	''' </summary>
	''' <param name="zipCode"></param>
	''' <returns></returns>
		Public Function DeleteItem(ByVal zipCode As Long) As Boolean
			If Nothing Is m_table Then
				Return False
			End If

			Try
				Dim r As DataRow = m_table.Rows.Find(zipCode)
				If Not Nothing Is r Then 'if the record with this zipCode already exists
					SyncLock m_table
						r.Delete()
					End SyncLock
					MyBase.m_bIsCompiledDirty = True
					Return True
				End If
				MyBase.m_bIsCompiledDirty = True
				Return False
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message)
				Return False
			End Try
		End Function

	''' <summary>
	''' get a weather item given a city name.
	''' </summary>
	''' <param name="cityName"></param>
	''' <returns></returns>
	''' <remarks>a city might have more than one zipCode and therefore this method will
	''' return the first zipcOde found for the specified city name.</remarks>
		Public Function GetWeatherItem(ByVal cityName As String) As IPropertySet
			If Nothing Is m_table Then
				Return Nothing
			End If

			Dim rows As DataRow() = m_table.Select("CITYNAME = '" & cityName & "'")
			If rows.Length = 0 Then
				Return Nothing
			End If

			Dim zipCode As Long = Convert.ToInt64(rows(0)(1))
			Return GetWeatherItem(zipCode)
		End Function

	''' <summary>
	''' This method searches for the record of the given zipcode and retunes the information as a PropertySet.
	''' </summary>
	''' <param name="zipCode"></param>
	''' <returns>a PropertySet encapsulating the weather information for the given weather item.</returns>
		Public Function GetWeatherItem(ByVal zipCode As Long) As IPropertySet
			Dim r As DataRow = m_table.Rows.Find(zipCode)
			If Nothing Is r Then
				Return Nothing
			End If

			Dim propSet As IPropertySet = New PropertySetClass()
			propSet.SetProperty("ID", r(0))
			propSet.SetProperty("ZIPCODE", r(1))
			propSet.SetProperty("CITYNAME", r(2))
			propSet.SetProperty("LAT", r(3))
			propSet.SetProperty("LON", r(4))
			propSet.SetProperty("TEMPERATURE", r(5))
			propSet.SetProperty("CONDITION", r(6))
			propSet.SetProperty("ICONNAME", r(7))
			propSet.SetProperty("ICONID", r(8))
			propSet.SetProperty("DAY", r(9))
			propSet.SetProperty("DATE", r(10))
			propSet.SetProperty("LOW", r(11))
			propSet.SetProperty("HIGH", r(12))
			propSet.SetProperty("UPDATEDATE", r(14))

			Return propSet
		End Function

	''' <summary>
	''' get a list of all citynames currently in the dataset.
	''' </summary>
	''' <returns></returns>
	''' <remarks>Please note that since the unique ID is zipCode, it is possible
	''' to have a city name appearing more than once.</remarks>
		Public Function GetCityNames() As String()
			If Nothing Is m_table OrElse 0 = m_table.Rows.Count Then
				Return Nothing
			End If

			Dim cityNames As String() = New String(m_table.Rows.Count - 1){}
			Dim i As Integer=0
			Do While i<m_table.Rows.Count
				'column #2 stores the cityName
				cityNames(i) = Convert.ToString(m_table.Rows(i)(2))
				i += 1
			Loop

			Return cityNames
		End Function

	''' <summary>
	''' Zoom to a weather item according to its city name
	''' </summary>
	''' <param name="cityName"></param>
	Public Sub ZoomTo(ByVal cityName As String)
	  If Nothing Is m_table Then
			Return
	  End If

	  Dim rows As DataRow() = m_table.Select("CITYNAME = '" & cityName & "'")
	  If rows.Length = 0 Then
			Return
	  End If

	  Dim zipCode As Long = Convert.ToInt64(rows(0)(1))
	  ZoomTo(zipCode)
	End Sub

	''' <summary>
	''' Zoom to weather item according to its zipcode
	''' </summary>
	''' <param name="zipCode"></param>
	Public Sub ZoomTo(ByVal zipCode As Long)
	  If Nothing Is m_table OrElse Nothing Is m_symbolTable Then
			Return
	  End If

	  If Nothing Is m_display Then
			Return
	  End If

	  'get the record for the requested zipCode
	  Dim r As DataRow = m_table.Rows.Find(zipCode)
	  If Nothing Is r Then
			Return
	  End If

	  'get the coordinate of the zipCode
	  Dim lat As Double = Convert.ToDouble(r(3))
	  Dim lon As Double = Convert.ToDouble(r(4))

	  Dim point As IPoint = New PointClass()
	  point.X = lon
	  point.Y = lat
	  point.SpatialReference = m_spatialRef

		If Not Nothing Is m_mapSpatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode Then
			point.Project(m_mapSpatialRef)
		End If

	  Dim iconCode As Integer = Convert.ToInt32(r(8))
        'find the appropriate symbol record
	  Dim rec As DataRow = m_symbolTable.Rows.Find(iconCode)
	  If rec Is Nothing Then
			Return
	  End If

	  'get the icon's dimensions
	  Dim iconWidth As Integer = Convert.ToInt32(rec(3))
	  Dim iconHeight As Integer = Convert.ToInt32(rec(4))

	  Dim displayTransformation As IDisplayTransformation = (CType(m_display, IScreenDisplay)).DisplayTransformation

	  'Convert the icon coordinate into screen coordinate
	  Dim windowX, windowY As Integer
	  displayTransformation.FromMapPoint(point,windowX, windowY)

	  'get the upper left coord
	  Dim ulx, uly As Integer
		ulx = Convert.ToInt32(windowX - iconWidth / 2)
		uly = Convert.ToInt32(windowY - iconHeight / 2)
	  Dim ulPnt As IPoint = displayTransformation.ToMapPoint(ulx, uly)

	  'get the lower right coord
	  Dim lrx, lry As Integer
		lrx = Convert.ToInt32(windowX + iconWidth / 2)
		lry = Convert.ToInt32(windowY + iconHeight / 2)
	  Dim lrPnt As IPoint = displayTransformation.ToMapPoint(lrx, lry)

	  'construct the new extent
	  Dim envelope As IEnvelope = New EnvelopeClass()
	  envelope.PutCoords(ulPnt.X, lrPnt.Y, lrPnt.X, ulPnt.Y)
	  envelope.Expand(2,2,False)

	  'set the new extent and refresh the display
	  displayTransformation.VisibleBounds = envelope

		MyBase.m_bIsCompiledDirty = True

	  CType(m_display, IScreenDisplay).Invalidate(Nothing, True, CShort(esriScreenCache.esriAllScreenCaches))
	  CType(m_display, IScreenDisplay).UpdateWindow()
	End Sub

	Private Sub SetSymbolSize(ByVal newSize As Integer)
	  If newSize <= 0 Then
			MessageBox.Show("Size is not allowed.")
			Return
	  End If

	  m_symbolSize = newSize

	  If Nothing Is m_symbolTable OrElse 0 = m_symbolTable.Rows.Count Then
			Return
	  End If

	  Dim pictureMarkerSymbol As IPictureMarkerSymbol = Nothing

	  SyncLock m_symbolTable
		For Each r As DataRow In m_symbolTable.Rows
		  pictureMarkerSymbol = TryCast(r(2), IPictureMarkerSymbol)
		  If Nothing Is pictureMarkerSymbol Then
				Continue For
		  End If

		  pictureMarkerSymbol.Size = newSize
		  r(2) = pictureMarkerSymbol
		  r.AcceptChanges()
		Next r
	  End SyncLock

		MyBase.m_bIsCompiledDirty = True

	  CType(m_display, IScreenDisplay).Invalidate(Nothing, True, CShort(esriScreenCache.esriAllScreenCaches))
	  CType(m_display, IScreenDisplay).UpdateWindow()
	End Sub

	Public Property SymbolSize() As Integer
	  Set
		  SetSymbolSize(Value)
	  End Set
	  Get
		  Return m_symbolSize
	  End Get
	End Property

		#End Region

		#Region "private utility methods"

	''' <summary>
	''' create a WGS1984 geographic coordinate system.
	''' In this case, the underlying data provided by the service is in WGS1984.
	''' </summary>
	''' <returns></returns>
		Private Function CreateGeographicSpatialReference() As ISpatialReference
			Dim spatialRefFatcory As ISpatialReferenceFactory = New SpatialReferenceEnvironmentClass()
			Dim geoCoordSys As IGeographicCoordinateSystem
			geoCoordSys = spatialRefFatcory.CreateGeographicCoordinateSystem(CInt(esriSRGeoCSType.esriSRGeoCS_WGS1984))
			geoCoordSys.SetFalseOriginAndUnits(-180.0, -180.0, 5000000.0)
			geoCoordSys.SetZFalseOriginAndUnits(0.0, 100000.0)
			geoCoordSys.SetMFalseOriginAndUnits(0.0, 100000.0)

			Return TryCast(geoCoordSys, ISpatialReference)
		End Function

	''' <summary>
	''' get the overall extent of the items in the layer
	''' </summary>
	''' <returns></returns>
		Private Function GetLayerExtent() As IEnvelope
	  'iterate through all the items in the layers DB and get the bounding envelope
			Dim env As IEnvelope = New EnvelopeClass()
			env.SpatialReference = m_spatialRef
			Dim point As IPoint = New PointClass()
			point.SpatialReference = m_spatialRef
			Dim symbolCode As Integer = 0
			Dim newSymbolSize As Double = 0
			For Each r As DataRow In m_table.Rows
				If TypeOf r(3) Is DBNull OrElse TypeOf r(4) Is DBNull Then
					Continue For
				End If

				point.Y = Convert.ToDouble(r(3))
				point.X = Convert.ToDouble(r(4))

				' need to get the symbol size in meters in order to add it to the total layer extent
				If Not m_display Is Nothing Then
					symbolCode = Convert.ToInt32(r(8))
					newSymbolSize = Math.Max(GetSymbolSize(m_display, symbolCode), newSymbolSize)
				End If


				env.Union(point.Envelope)
			Next r

	  ' Expand the envelope in order to include the size of the symbol
			env.Expand(newSymbolSize, newSymbolSize, False)

	  'return the layer's extent in the data underlying coordinate system
			Return env
		End Function

	''' <summary>
	''' initialize the main table used by the layer as well as the symbols table.
    ''' The base class calls new on the table and adds a default ID field.
	''' </summary>
		Private Sub InitializeTables()
			Dim path As String = System.IO.Path.Combine(m_dataFolder, "Weather.xml")
			'In case that there is no existing cache on the local machine, create the table.
			If (Not System.IO.File.Exists(path)) Then

				'create the table the table	in addition to the default 'ID' and 'Geometry'	
				m_table = New DataTable("RECORDS")

				m_table.Columns.Add("ID", GetType(Long)) '0
				m_table.Columns.Add("ZIPCODE", GetType(Long)) '1
				m_table.Columns.Add("CITYNAME", GetType(String)) '2
				m_table.Columns.Add("LAT", GetType(Double)) '3
				m_table.Columns.Add("LON", GetType(Double)) '4
				m_table.Columns.Add("TEMP", GetType(Integer)) '5
				m_table.Columns.Add("CONDITION", GetType(String)) '6
				m_table.Columns.Add("ICONNAME", GetType(String)) '7
				m_table.Columns.Add("ICONID", GetType(Integer)) '8
				m_table.Columns.Add("DAY", GetType(String)) '9
				m_table.Columns.Add("DATE", GetType(String)) '10
				m_table.Columns.Add("LOW", GetType(String)) '11
				m_table.Columns.Add("HIGH", GetType(String)) '12
				m_table.Columns.Add("SELECTED", GetType(Boolean)) '13
				m_table.Columns.Add("UPDATEDATE", GetType(DateTime)) '14


				'set the ID column to be auto increment
				m_table.Columns(0).AutoIncrement = True
				m_table.Columns(0).ReadOnly = True

				'the zipCode column must be the unique and nut allow null
				m_table.Columns(1).Unique = True

				' set the ZIPCODE primary key for the table
				m_table.PrimaryKey = New DataColumn() {m_table.Columns("ZIPCODE")}

	  Else 'in case that the local cache exists, simply load the tables from the cache.
				Dim ds As DataSet = New DataSet()
				ds.ReadXml(path)

				m_table = ds.Tables("RECORDS")

			If Nothing Is m_table Then
				Throw New Exception("Cannot find 'RECORDS' table")
			End If

			If 15 <> m_table.Columns.Count Then
				Throw New Exception("Table 'RECORDS' does not have all required columns")
			End If

			m_table.Columns(0).ReadOnly = True

			' set the ZIPCODE primary key for the table
				m_table.PrimaryKey = New DataColumn() {m_table.Columns("ZIPCODE")}

				'synchronize the locations table
				For Each r As DataRow In m_table.Rows
					Try
			'in case that the locations table does not exists, create and initialize it
						If Nothing Is m_locations Then
							InitializeLocations()
						End If

			'get the zipcode for the record
						Dim zip As String = Convert.ToString(r(1))

					'make sure that there is no existing record with that zipCode already in the 
					'locations table.
					Dim rows As DataRow() = m_locations.Select("ZIPCODE = " & zip)
						If 0 = rows.Length Then
							Dim rec As DataRow = m_locations.NewRow()
							rec(1) = Convert.ToInt64(r(1)) 'zip code
							rec(2) = Convert.ToString(r(2)) 'city name

			  'add the new record to the locations table
			  SyncLock m_locations
								m_locations.Rows.Add(rec)
			  End SyncLock
						End If
					Catch ex As Exception
						System.Diagnostics.Trace.WriteLine(ex.Message)
					End Try
				Next r

            'dispose the DS
			ds.Tables.Remove(m_table)
			ds.Dispose()
			GC.Collect()
	  End If

			'initialize the symbol map table
			m_symbolTable = New DataTable("Symbology")

			'add the columns to the table
			m_symbolTable.Columns.Add("ID", GetType(Integer)) '0
			m_symbolTable.Columns.Add("ICONID", GetType(Integer)) '1
			m_symbolTable.Columns.Add("SYMBOL", GetType(ISymbol)) '2
			m_symbolTable.Columns.Add("SYMBOLWIDTH", GetType(Integer)) '3
			m_symbolTable.Columns.Add("SYMBOLHEIGHT", GetType(Integer)) '4
			m_symbolTable.Columns.Add("DYNAMICGLYPH", GetType(IDynamicGlyph)) '5
			m_symbolTable.Columns.Add("BITMAP", GetType(Bitmap)) '6

			'set the ID column to be auto increment
			m_symbolTable.Columns(0).AutoIncrement = True
			m_symbolTable.Columns(0).ReadOnly = True

			m_symbolTable.Columns(1).AllowDBNull = False
			m_symbolTable.Columns(1).Unique = True

			'set ICONID as the primary key for the table
			m_symbolTable.PrimaryKey = New DataColumn() {m_symbolTable.Columns("ICONID")}
		End Sub

	''' <summary>
	''' Initialize the location table. Gets the location from a featureclass
	''' </summary>
	Private Sub InitializeLocations()
        'create a new instance of the location table
		m_locations = New DataTable()

		'add fields to the table
		m_locations.Columns.Add("ID", GetType(Integer))
		m_locations.Columns.Add("ZIPCODE", GetType(Long))
		m_locations.Columns.Add("CITYNAME", GetType(String))

		m_locations.Columns(0).AutoIncrement = True
		m_locations.Columns(0).ReadOnly = True

		'set ZIPCODE as the primary key for the table
		m_locations.PrimaryKey = New DataColumn() {m_locations.Columns("ZIPCODE")}

		'spawn a thread to populate the locations table
		Dim t As Thread = New Thread(AddressOf PopulateLocationsTableProc)
		t.Start()

		System.Threading.Thread.Sleep(1000)
	End Sub

	''' <summary>
	''' Load the information from the MajorCities featureclass to the locations table
	''' </summary>
	Private Sub PopulateLocationsTableProc()
	  'get the ArcGIS path from the registry
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\ESRI\ArcObjectsSdk10.4")
	  Dim path As String = Convert.ToString(key.GetValue("InstallDir"))

        If (Not System.IO.File.Exists(System.IO.Path.Combine(path, "Samples\Data\USZipCodeData\ZipCode_Boundaries_US_Major_Cities.shp"))) Then
            MessageBox.Show("Cannot find file ZipCode_Boundaries_US_Major_Cities.shp!")
            Return
        End If

	  'open the featureclass
		Dim wf As IWorkspaceFactory = New ShapefileWorkspaceFactoryClass()
        Dim ws As IWorkspace = wf.OpenFromFile(System.IO.Path.Combine(path, "Samples\Data\USZipCodeData"), 0)
	  Dim fw As IFeatureWorkspace = TryCast(ws, IFeatureWorkspace)
	  Dim featureClass As IFeatureClass = fw.OpenFeatureClass("ZipCode_Boundaries_US_Major_Cities")
	  'map the name and zip fields
	  Dim zipIndex As Integer = featureClass.FindField("ZIP")
	  Dim nameIndex As Integer = featureClass.FindField("NAME")
	  Dim cityName As String
	  Dim zip As Long

	  Try
			'iterate through the features and add the information to the table
			Dim fCursor As IFeatureCursor = Nothing
			fCursor = featureClass.Search(Nothing, True)
			Dim feature As IFeature = fCursor.NextFeature()
			Dim index As Integer = 0

			Do While Not Nothing Is feature
				Dim obj As Object = feature.Value(nameIndex)
				If obj Is Nothing Then
					Continue Do
				End If
				cityName = Convert.ToString(obj)

				obj = feature.Value(zipIndex)
				If obj Is Nothing Then
					Continue Do
				End If
				zip = Long.Parse(Convert.ToString(obj))
				If zip <= 0 Then
					Continue Do
				End If

				'add the current location to the location table
				Dim r As DataRow = m_locations.Rows.Find(zip)
				If Nothing Is r Then
					r = m_locations.NewRow()
					r(1) = zip
					r(2) = cityName
					SyncLock m_locations
						m_locations.Rows.Add(r)
					End SyncLock
				End If

				feature = fCursor.NextFeature()

				index += 1
			Loop

		'release the feature cursor
		Marshal.ReleaseComObject(fCursor)
	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
	  End Try
	End Sub

		''' <summary>
		''' Initialize the symbol that would use to highlight selected items
		''' </summary>
	Private Sub InitializeSelectionSymbol()
		'use a character marker symbol:
		Dim chMrkSym As ICharacterMarkerSymbol
		chMrkSym = New CharacterMarkerSymbolClass()

	  'Set the selection color (yellow)
		Dim color As IRgbColor
		color = New RgbColorClass()
		color.Red = 0
		color.Green = 255
		color.Blue = 255

	  'set the font
		Dim aFont As stdole.IFont
		aFont = New stdole.StdFontClass()
		aFont.Name = "ESRI Default Marker"
	  aFont.Size = m_symbolSize
		aFont.Bold = True

	  'char #41 is just a rectangle
		chMrkSym.CharacterIndex = 41
		chMrkSym.Color = TryCast(color, IColor)
		chMrkSym.Font = TryCast(aFont, stdole.IFontDisp)
	  chMrkSym.Size = m_symbolSize

		m_selectionSymbol = TryCast(chMrkSym, ISymbol)
	End Sub
	''' <summary>
	''' run the thread that does the update of the weather data
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub OnUpdateTimer(ByVal sender As Object, ByVal e As ElapsedEventArgs)
		m_timer.Interval = 2700000 '(45 minutes)
		m_updateThread = New Thread(AddressOf ThreadProc)

		'run the update thread
		m_updateThread.Start()
	End Sub

	''' <summary>
	''' the main update thread for the layer.
	''' </summary>
	''' <remarks>Since the layer gets the weather information from a web service which might
    ''' take a while to respond, it is not logical to let the application hang while waiting
    ''' for response. Therefore, running the request on a different thread frees the application to 
	''' continue working while waiting for a response. 
	''' Please note that in this case, synchronization of shared resources must be addressed,
	''' otherwise you might end up getting unexpected results.</remarks>
	Private Sub ThreadProc()
		Try

			Dim lZipCode As Long
			'iterate through all the records in the main table and update it against 
			'the information from the website.
			For Each r As DataRow In m_locations.Rows
				'put the thread to sleep in order not to overwhelm yahoo web site might
				System.Threading.Thread.Sleep(200)

				'get the zip code of the record (column #1)
				lZipCode = Convert.ToInt32(r(1))

				'make the request and update the item
				AddWeatherItem(lZipCode, 0.0, 0.0)
			Next r

			'serialize the tables onto the local machine
			Dim ds As DataSet = New DataSet()
			ds.Tables.Add(m_table)
			ds.WriteXml(System.IO.Path.Combine(m_dataFolder, "Weather.xml"))
			ds.Tables.Remove(m_table)
			ds.Dispose()
			GC.Collect()

			MyBase.m_bIsCompiledDirty = True

			'fire an event to notify update of the weatheritems 
			If Not OnWeatherItemsUpdatedEvent Is Nothing Then
				RaiseEvent OnWeatherItemsUpdated(Me, New EventArgs())
			End If
		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
		End Try
	End Sub

		''' <summary>
    ''' given a bitmap url, saves it on the local machine and returns its size
		''' </summary>
		''' <param name="iconPath"></param>
		''' <param name="width"></param>
		''' <param name="height"></param>
	Private Function DownloadIcon(ByVal iconPath As String, <System.Runtime.InteropServices.Out()> ByRef width As Integer, <System.Runtime.InteropServices.Out()> ByRef height As Integer) As Bitmap
		'if the icon does not exist on the local machine, get it from RSS site
		Dim iconFileName As String = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) & ".bmp")
		width = 0
		height = 0
		Dim bitmap As Bitmap = Nothing
		If (Not File.Exists(iconFileName)) Then
			Using webClient As System.Net.WebClient = New System.Net.WebClient()
				'open a readable stream to download the bitmap
				Using stream As System.IO.Stream = webClient.OpenRead(iconPath)
					bitmap = New Bitmap(stream, True)

					'save the image as a bitmap in the icons folder
					bitmap.Save(iconFileName, ImageFormat.Bmp)

					'get the bitmap's dimensions
					width = bitmap.Width
					height = bitmap.Height
				End Using
			End Using
		Else
			'get the bitmap's dimensions
			bitmap = New Bitmap(iconFileName)
			width = bitmap.Width
			height = bitmap.Height
		End If

		Return bitmap
	End Function

	''' <summary>
	''' get the specified symbol from the symbols table.
	''' </summary>
	''' <param name="iconCode"></param>
	''' <param name="dbr"></param>
	''' <returns></returns>
	Private Function GetSymbol(ByVal iconCode As Integer, ByVal dbr As DataRow) As ISymbol
		Dim symbol As ISymbol = Nothing
		Dim iconPath As String
		Dim iconWidth, iconHeight As Integer
		Dim bitmap As Bitmap = Nothing

		'search for an existing symbol in the table
		Dim r As DataRow = m_symbolTable.Rows.Find(iconCode)
		If r Is Nothing Then 'in case that the symbol does not exist in the table, create a new entry
			r = m_symbolTable.NewRow()
			r(1) = iconCode

			iconPath = Convert.ToString(dbr(7))
			'Initialize the picture marker symbol
			symbol = InitializeSymbol(iconPath, iconWidth, iconHeight, bitmap)
			If Nothing Is symbol Then
				Return Nothing
			End If

			'update the symbol table
			SyncLock m_symbolTable
				r(2) = symbol
				r(3) = iconWidth
				r(4) = iconHeight
				r(6) = bitmap
				m_symbolTable.Rows.Add(r)
			End SyncLock
		Else
			If TypeOf r(2) Is DBNull Then	'in case that the record exists but the symbol hasn't been initialized
				iconPath = Convert.ToString(dbr(7))
				'Initialize the picture marker symbol
				symbol = InitializeSymbol(iconPath, iconWidth, iconHeight, bitmap)
				If Nothing Is symbol Then
					Return Nothing
				End If

				'update the symbol table
				SyncLock m_symbolTable
					r(2) = symbol
					r(6) = bitmap
					r.AcceptChanges()
				End SyncLock
			Else 'the record exists in the table and the symbol has been initialized
				'get the symbol
				symbol = TryCast(r(2), ISymbol)
			End If
		End If

		'return the requested symbol
		Return symbol
	End Function

	Private Function GetDynamicGlyph(ByVal dynamicGlyphFactory As IDynamicGlyphFactory2, ByVal iconCode As Integer, ByVal dbr As DataRow, <System.Runtime.InteropServices.Out()> ByRef originalIconSize As Integer) As IDynamicGlyph
		originalIconSize = 0

		If dynamicGlyphFactory Is Nothing Then
			Return Nothing
		End If

		Dim iconPath As String
		Dim iconWidth, iconHeight As Integer
		Dim bitmap As Bitmap = Nothing
		Dim dynamicGlyph As IDynamicGlyph = Nothing

		'search for an existing symbol in the table
		Dim r As DataRow = m_symbolTable.Rows.Find(iconCode)
		If r Is Nothing Then
			iconPath = Convert.ToString(dbr(7))
			bitmap = DownloadIcon(iconPath, iconWidth, iconHeight)
			If Not bitmap Is Nothing Then
				originalIconSize = iconWidth

                dynamicGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor))


				'update the symbol table
				r = m_symbolTable.NewRow()
				SyncLock m_symbolTable
					r(1) = iconCode
					r(3) = iconWidth
					r(4) = iconHeight
					r(5) = dynamicGlyph
					r(6) = bitmap
					m_symbolTable.Rows.Add(r)
				End SyncLock
			End If
		Else
			If TypeOf r(5) Is DBNull Then
				If TypeOf r(6) Is DBNull Then
					iconPath = Convert.ToString(dbr(7))
					bitmap = DownloadIcon(iconPath, iconWidth, iconHeight)
					If bitmap Is Nothing Then
						Return Nothing
					End If

					originalIconSize = iconWidth

					SyncLock m_symbolTable
						r(3) = iconWidth
						r(4) = iconHeight
						r(6) = bitmap
					End SyncLock
				Else
					originalIconSize = Convert.ToInt32(r(3))
					bitmap = CType(r(6), Bitmap)
				End If
                dynamicGlyph = dynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap().ToInt32(), False, CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)), IColor))

				SyncLock m_symbolTable
					r(5) = dynamicGlyph
				End SyncLock
			Else
				originalIconSize = Convert.ToInt32(r(3))
				dynamicGlyph = CType(r(5), IDynamicGlyph)
			End If
		End If

		Return dynamicGlyph
	End Function


		''' <summary>
	''' Initialize a character marker symbol for a given bitmap path
		''' </summary>
		''' <param name="iconPath"></param>
		''' <param name="iconWidth"></param>
		''' <param name="iconHeight"></param>
		''' <returns></returns>
	Private Function InitializeSymbol(ByVal iconPath As String, <System.Runtime.InteropServices.Out()> ByRef iconWidth As Integer, <System.Runtime.InteropServices.Out()> ByRef iconHeight As Integer, <System.Runtime.InteropServices.Out()> ByRef bitmap As Bitmap) As ISymbol
		iconWidth = 0
		iconHeight = 0
		bitmap = Nothing
		Try
			'make sure that the icon exit on dist or else download it
			bitmap = DownloadIcon(iconPath, iconWidth, iconHeight)
			Dim iconFileName As String = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) & ".bmp")
			If (Not System.IO.File.Exists(iconFileName)) Then
				Return Nothing
			End If

			'initialize the transparent color
			Dim rgbColor As IRgbColor = New RgbColorClass()
			rgbColor.Red = 255
			rgbColor.Blue = 255
			rgbColor.Green = 255

			'instantiate the marker symbol and set its properties
			Dim pictureMarkerSymbol As IPictureMarkerSymbol = New PictureMarkerSymbolClass()
			pictureMarkerSymbol.CreateMarkerSymbolFromFile(ESRI.ArcGIS.Display.esriIPictureType.esriIPictureBitmap, iconFileName)
			pictureMarkerSymbol.Angle = 0
			pictureMarkerSymbol.Size = m_symbolSize
			pictureMarkerSymbol.XOffset = 0
			pictureMarkerSymbol.YOffset = 0
			pictureMarkerSymbol.BitmapTransparencyColor = TryCast(rgbColor, IColor)

			'return the symbol
			Return CType(pictureMarkerSymbol, ISymbol)
		Catch
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Makes a request against RSS Weather service and add update the layer's table
	''' </summary>
	''' <param name="zipCode"></param>
	''' <param name="Lat"></param>
	''' <param name="Lon"></param>
		Private Sub AddWeatherItem(ByVal zipCode As Long, ByVal Lat As Double, ByVal Lon As Double)
			Try
				Dim cityName As String
				Dim newLat, newLon As Double
				Dim temp As Integer
				Dim condition As String
				Dim desc As String
				Dim iconPath As String
				Dim day As String
				Dim [date] As String
				Dim low As Integer
				Dim high As Integer
				Dim iconCode As Integer
				Dim iconWidth As Integer = 52 'default values
				Dim iconHeight As Integer = 52
				Dim bitmap As Bitmap = Nothing

				Dim dbr As DataRow = m_table.Rows.Find(zipCode)
				If Not dbr Is Nothing Then
					' get the date 
					Dim updateDate As DateTime = Convert.ToDateTime(dbr(14))
					Dim ts As TimeSpan = DateTime.Now.Subtract(updateDate)

					' if the item had been updated in the past 15 minutes, simply bail out.
					If ts.TotalMinutes < 15 Then
						Return
					End If
				End If

			'the base URL for the service
				Dim url As String = "http://xml.weather.yahoo.com/forecastrss?p="
			'the RegEx used to extract the icon path from the HTML tag
				Dim regxQry As String = "(http://(\"")?(.*?\.gif))"
				Dim reader As XmlTextReader = Nothing
				Dim doc As XmlDocument
				Dim node As XmlNode

				Try	
				'make the request and get the result back into XmlReader
					reader = New XmlTextReader(url & zipCode.ToString())
				Catch ex As Exception
					System.Diagnostics.Trace.WriteLine(ex.Message)
					Return
				End Try

			'load the XmlReader to an xml doc
				doc = New XmlDocument()
				doc.Load(reader)

			'set an XmlNamespaceManager since we have to make explicit namespace searches
			Dim xmlnsManager As XmlNamespaceManager = New System.Xml.XmlNamespaceManager(doc.NameTable)
			'Add the namespaces used in the xml doc to the XmlNamespaceManager.
			xmlnsManager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0")
			xmlnsManager.AddNamespace("geo", "http://www.w3.org/2003/01/geo/wgs84_pos#")

			'make sure that the node exists
			node = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager)
			If Nothing Is node Then
				Return
			End If

			'get the cityname
			cityName = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager).InnerXml
			If Lat = 0.0 AndAlso Lon = 0.0 Then
                'in case that the caller did not specify a coordinate, get the default coordinate from the service
				newLat = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:lat", xmlnsManager).InnerXml)
				newLon = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:long", xmlnsManager).InnerXml)
			Else
				newLat = Lat
				newLon = Lon
			End If

			'extract the rest of the information from the RSS response
				condition = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@text", xmlnsManager).InnerXml
				iconCode = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@code", xmlnsManager).InnerXml)
				temp = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:condition/@temp", xmlnsManager).InnerXml)
				desc = doc.DocumentElement.SelectSingleNode("/rss/channel/item/description").InnerXml
				day = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@day", xmlnsManager).InnerXml
				[date] = doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@date", xmlnsManager).InnerXml
				low = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@low", xmlnsManager).InnerXml)
				high = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("/rss/channel/item/yweather:forecast/@high", xmlnsManager).InnerXml)


				'use regex in order to extract the icon name from the html script
				Dim m As Match = Regex.Match(desc,regxQry)
				If m.Success Then
					iconPath = m.Value

					'add the icon ID to the symbology table
					Dim tr As DataRow = m_symbolTable.Rows.Find(iconCode)
					If Nothing Is tr Then
						'get the icon from the website
						bitmap = DownloadIcon(iconPath, iconWidth, iconHeight)

                    'create a new record
						tr = m_symbolTable.NewRow()
						tr(1) = iconCode
						tr(3) = iconWidth
						tr(4) = iconHeight
						tr(6) = bitmap

				'update the symbol table. The initialization of the symbol cannot take place in here, since
                    'this code gets executed on a background thread.
						SyncLock m_symbolTable
							m_symbolTable.Rows.Add(tr)
						End SyncLock
					Else 'get the icon's dimensions from the table
                    'get the icon's dimensions from the table
						iconWidth = Convert.ToInt32(tr(3))
						iconHeight = Convert.ToInt32(tr(4))
					End If
				Else
					iconPath = ""
				End If

			'test whether the record already exists in the layer's table.
            If Nothing Is dbr Then 'in case that the record does not exist
                'create a new record
                dbr = m_table.NewRow()

                If (Not m_table.Columns(0).AutoIncrement) Then
                    dbr(0) = Convert.ToInt32(DateTime.Now.Millisecond)
                End If

                'add the item to the table
                SyncLock m_table
                    dbr(1) = zipCode
                    dbr(2) = cityName
                    dbr(3) = newLat
                    dbr(4) = newLon
                    dbr(5) = temp
                    dbr(6) = condition
                    dbr(7) = iconPath
                    dbr(8) = iconCode
                    dbr(9) = day
                    dbr(10) = [date]
                    dbr(11) = low
                    dbr(12) = high
                    dbr(13) = False
                    dbr(14) = DateTime.Now

                    m_table.Rows.Add(dbr)
                End SyncLock
            Else 'in case that the record exists, just update it
                'update the record
                SyncLock m_table
                    dbr(5) = temp
                    dbr(6) = condition
                    dbr(7) = iconPath
                    dbr(8) = iconCode
                    dbr(9) = day
                    dbr(10) = [date]
                    dbr(11) = low
                    dbr(12) = high
                    dbr(14) = DateTime.Now

                    dbr.AcceptChanges()
                End SyncLock
            End If

				MyBase.m_bIsCompiledDirty = True

				'fire an event to notify the user that the item has been updated
		If Not OnWeatherItemAddedEvent Is Nothing Then
		  Dim weatherItemEventArgs As WeatherItemEventArgs = New WeatherItemEventArgs(Convert.ToInt32(dbr(0)), zipCode, newLon, newLat, iconWidth, iconHeight)
		  RaiseEvent OnWeatherItemAdded(Me, weatherItemEventArgs)
		End If

		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine("AddWeatherItem: " & ex.Message)
		End Try
	End Sub
	#End Region

	#Region "IIdentify Members"

	''' <summary>
	''' Identifying all the weather items falling within the given envelope
	''' </summary>
	''' <param name="pGeom"></param>
	''' <returns></returns>
	Public Function Identify(ByVal pGeom As IGeometry) As IArray Implements IIdentify.Identify
	  Dim intersectEnv As IEnvelope = New EnvelopeClass()
	  Dim inEnv As IEnvelope
	  Dim array As IArray = New ArrayClass()

        'get the envelope from the geometry 
	  If pGeom.GeometryType = esriGeometryType.esriGeometryEnvelope Then
			inEnv = pGeom.Envelope
	  Else
			inEnv = TryCast(pGeom, IEnvelope)
	  End If

	  'reproject the envelope to the source coordsys
	  'this would allow to search directly on the Lat/Lon columns
			If Not Nothing Is m_spatialRef AndAlso m_mapSpatialRef.FactoryCode <> m_layerSRFactoryCode AndAlso Not Nothing Is inEnv.SpatialReference Then
				inEnv.Project(MyBase.m_spatialRef)
			End If

	  'expand the envelope so that it'll cover the symbol
	  inEnv.Expand(4,4,True)

	  Dim xmin, ymin, xmax, ymax As Double
	  inEnv.QueryCoords(xmin, ymin, xmax, ymax)

	  'select all the records within the given extent
	  Dim qry As String = "LON >= " & xmin.ToString() & " AND LON <= " & xmax.ToString() & " AND Lat >= " & ymin.ToString() & " AND LAT <= " & ymax.ToString()
	  Dim rows As DataRow() = m_table.Select(qry)
	  If 0 = rows.Length Then
			Return array
	  End If

	  Dim zipCode As Long
	  Dim propSet As IPropertySet = Nothing
	  Dim idObj As IIdentifyObj = Nothing
	  Dim idObject As IIdentifyObject = Nothing
	  Dim bIdentify As Boolean = False

	  For Each r As DataRow In rows
		'get the zipCode
		zipCode = Convert.ToInt64(r("ZIPCODE"))

		'get the properties of the given item in order to pass it to the identify object
		propSet = Me.GetWeatherItem(zipCode)
		If Not Nothing Is propSet Then
		  'instantiate the identify object and add it to the array
		  idObj = New RSSWeatherIdentifyObject()
		  'test whether the layer can be identified
		  bIdentify = idObj.CanIdentify(CType(Me, ILayer))
		  If bIdentify Then
				idObject = TryCast(idObj, IIdentifyObject)
				idObject.PropertySet = propSet
				array.Add(idObj)
		  End If
		End If
	  Next r

	  'return the array with the identify objects
	  Return array
	End Function

		Private Function GetSymbolSize(ByVal display As IDisplay, ByVal symbolCode As Integer) As Double
			If display Is Nothing Then
				Return 0
			End If

			Dim newSymbolSize As Double = 0
			Dim symbolSizePixels As Double = 0
			Dim r As DataRow = m_symbolTable.Rows.Find(symbolCode)
			If Not r Is Nothing Then
				symbolSizePixels = Convert.ToDouble(m_symbolSize)

            ' convert the symbol size from pixels to map units
				Dim transform As ITransformation = TryCast(display.DisplayTransformation, ITransformation)
				If transform Is Nothing Then
					Return 0
				End If

				Dim symbolDimensions As Double() = New Double(1){}
				symbolDimensions(0) = CDbl(symbolSizePixels)
				symbolDimensions(1) = CDbl(symbolSizePixels)

				Dim symbolDimensionsMap As Double() = New Double(1){}

				transform.TransformMeasuresFF(esriTransformDirection.esriTransformReverse, 1, symbolDimensionsMap(0), symbolDimensions(0))
				newSymbolSize = symbolDimensionsMap(0)
			End If

			Return newSymbolSize
		End Function

	#End Region
  End Class

