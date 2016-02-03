Imports System.Data
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Threading
Imports System.Timers
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.EngineCore
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile

#Region "WeatherItemEventArgs class members"
Public NotInheritable Class WeatherItemEventArgs
	Inherits EventArgs
	Private m_iconId As Integer
	Private m_zipCode As Long
	Private m_x As Double
	Private m_y As Double
	Private m_iconWidth As Integer
	Private m_iconHeight As Integer
	Public Sub New(iconId As Integer, zipCode As Long, X As Double, Y As Double, iconWidth As Integer, iconHeight As Integer)
		m_iconId = iconId
		m_zipCode = zipCode
		m_x = X
		m_y = Y
		m_iconWidth = iconWidth
		m_iconHeight = iconHeight
	End Sub

	Public ReadOnly Property IconID() As Integer
		Get
			Return m_iconId
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
End Class
#End Region

'declare delegates for the event handling
Public Delegate Sub WeatherItemAdded(sender As Object, args As WeatherItemEventArgs)
Public Delegate Sub WeatherItemsUpdated(sender As Object, args As EventArgs)

Class RSSWeather
	Private NotInheritable Class InvokeHelper
		Inherits Control
		'delegate used to pass the invoked method to the main thread
		Public Delegate Sub RefreshWeatherItemHelper(weatherItemInfo As WeatherItemEventArgs)

		Private m_weather As RSSWeather = Nothing

		Public Sub New(rssWeather As RSSWeather)
			m_weather = rssWeather

			CreateHandle()
			CreateControl()
		End Sub

		Public Sub RefreshWeatherItem(weatherItemInfo As WeatherItemEventArgs)
			Try
				' Invoke the RefreshInternal through its delegate
				If Not Me.IsDisposed AndAlso Me.IsHandleCreated Then
					Invoke(New RefreshWeatherItemHelper(AddressOf RefreshWeatherItemInvoked), New Object() {weatherItemInfo})
				End If
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine(ex.Message)
			End Try
		End Sub

		Private Sub RefreshWeatherItemInvoked(weatherItemInfo As WeatherItemEventArgs)
			If m_weather IsNot Nothing Then
				m_weather.UpdateTracker(weatherItemInfo)
			End If
		End Sub
	End Class

	#Region "class members"

	Private m_timer As System.Timers.Timer = Nothing
	Private m_updateThread As Thread = Nothing
	Private m_iconFolder As String = String.Empty
	Private m_weatherItemTable As DataTable = Nothing
	Private m_symbolTable As DataTable = Nothing
	Private m_locations As DataTable = Nothing
	Private m_dataFolder As String = String.Empty
	Private m_installationFolder As String = String.Empty

	Private m_point As IPoint = Nothing
	Private m_SRWGS84 As ISpatialReference = Nothing
	Private m_mapOrGlobe As IBasicMap = Nothing
	Private m_textSymbol As ISimpleTextSymbol = Nothing
	Private m_graphicTracker As IGraphicTracker = Nothing
	Private m_invokeHelper As InvokeHelper = Nothing

	'weather items events
    Public Event OnWeatherItemAdded As WeatherItemAdded
	Public Event OnWeatherItemsUpdated As WeatherItemsUpdated

	#End Region


	Public Sub New()
		'get the directory for the cache. If it does not exist, create it.
		m_dataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RSSWeather")
		If Not System.IO.Directory.Exists(m_dataFolder) Then
			System.IO.Directory.CreateDirectory(m_dataFolder)
		End If
		m_iconFolder = m_dataFolder

		m_installationFolder = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
	End Sub

	Public Sub Init(mapOrGlobe As IBasicMap)
		System.Diagnostics.Trace.WriteLine("Init - Thread ID: " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString())

		If mapOrGlobe Is Nothing Then
			Return
		End If

		m_mapOrGlobe = mapOrGlobe

		Try
			'initialize the tables (main table as well as the symbols table)
			InitializeTables()

			'get the location list from a featureclass (US major cities) and synchronize it with the 
			'cached information in case it exists.
			If m_locations Is Nothing Then
				InitializeLocations()
			End If

			m_point = New PointClass()
			m_SRWGS84 = CreateGeographicSpatialReference()
			m_point.SpatialReference = m_SRWGS84

			m_textSymbol = TryCast(New TextSymbolClass(), ISimpleTextSymbol)
			m_textSymbol.Font = ToFontDisp(New Font("Tahoma", 10F, FontStyle.Bold))
			m_textSymbol.Size = 10.0
			m_textSymbol.Color = DirectCast(ToRGBColor(Color.FromArgb(0, 255, 0)), IColor)
			m_textSymbol.XOffset = 0.0
			m_textSymbol.YOffset = 16.0


			m_graphicTracker = New GraphicTrackerClass()
			m_graphicTracker.Initialize(TryCast(mapOrGlobe, Object))

			If m_weatherItemTable.Rows.Count > 0 Then
				PopulateGraphicTracker()
			End If

			m_invokeHelper = New InvokeHelper(Me)

            AddHandler Me.OnWeatherItemAdded, New WeatherItemAdded(AddressOf OnWeatherItemAddedEventHandler)

			'instantiate the timer for the weather update thread
			m_timer = New System.Timers.Timer(1000)
            AddHandler m_timer.Elapsed, New ElapsedEventHandler(AddressOf OnUpdateTimer)
			'enable the update timer
			m_timer.Enabled = True
		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
		End Try
	End Sub

	Public Sub Remove()
        RemoveHandler Me.OnWeatherItemAdded, New WeatherItemAdded(AddressOf OnWeatherItemAddedEventHandler)
		m_invokeHelper = Nothing
		m_timer.Enabled = False
		' wait for the update thread to exit
		m_updateThread.Join()
		m_graphicTracker.RemoveAll()
		m_graphicTracker = Nothing
	End Sub

	Public Sub UpdateTracker(weatherItemInfo As WeatherItemEventArgs)

		System.Diagnostics.Trace.WriteLine("UpdateTracker - Thread ID: " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString())
		If m_graphicTracker Is Nothing Then
			Throw New Exception("Graphic tracker is not initialized!")
		End If


		' 1. lock the m_weatherItemTable and get the record
		Dim row As DataRow
		SyncLock m_weatherItemTable
			row = m_weatherItemTable.Rows.Find(weatherItemInfo.ZipCode)
			If row Is Nothing Then
				Return
			End If

			' 2. get the symbol for the item
			Dim symbol As IGraphicTrackerSymbol = GetSymbol(weatherItemInfo.IconID, Convert.ToString(row(7)))
			If symbol Is Nothing Then
				Return
			End If

			Dim label As String = String.Format("{0}, {1}?F", Convert.ToString(row(2)), Convert.ToString(row(5)))

			' 3. check whether it has a tracker ID (not equals -1)
			Dim trackerID As Integer = Convert.ToInt32(row(15))
			'm_graphicTracker.SuspendUpdate = true;
			m_point.PutCoords(weatherItemInfo.mapX, weatherItemInfo.mapY)
			m_point.SpatialReference = m_SRWGS84
			m_point.Project(m_mapOrGlobe.SpatialReference)
			If trackerID = -1 Then
				' new tracker
				trackerID = m_graphicTracker.Add(TryCast(m_point, IGeometry), symbol)
				m_graphicTracker.SetTextSymbol(trackerID, m_textSymbol)


				row(15) = trackerID
			Else
				' existing tracker
				m_graphicTracker.MoveTo(trackerID, m_point.X, m_point.Y, 0)
				m_graphicTracker.SetSymbol(trackerID, symbol)
			End If

			m_graphicTracker.SetLabel(trackerID, label)


				'm_graphicTracker.SuspendUpdate = false;
			row.AcceptChanges()
		End SyncLock
	End Sub


	#Region "private utility methods"

	Private Sub PopulateGraphicTracker()
		m_graphicTracker.SuspendUpdate = True

		For Each row As DataRow In m_weatherItemTable.Rows
			Dim symbol As IGraphicTrackerSymbol = GetSymbol(Convert.ToInt32(row(8)), Convert.ToString(row(7)))
			If symbol Is Nothing Then
				Continue For
			End If

			Dim label As String = String.Format("{0}, {1}?F", Convert.ToString(row(2)), Convert.ToString(row(5)))

			m_point.PutCoords(Convert.ToDouble(row(4)), Convert.ToDouble(row(3)))
			Dim trackerID As Integer = m_graphicTracker.Add(TryCast(m_point, IGeometry), symbol)
			m_graphicTracker.SetTextSymbol(trackerID, m_textSymbol)
			m_graphicTracker.SetScaleMode(trackerID, esriGTScale.esriGTScaleAuto)
			m_graphicTracker.SetOrientationMode(trackerID, esriGTOrientation.esriGTOrientationAutomatic)
			m_graphicTracker.SetElevationMode(trackerID, esriGTElevation.esriGTElevationClampToGround)
			m_graphicTracker.SetLabel(trackerID, label)
			row(15) = trackerID
			row.AcceptChanges()
		Next

		m_graphicTracker.SuspendUpdate = False
	End Sub

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
	''' run the thread that does the update of the weather data
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub OnUpdateTimer(sender As Object, e As ElapsedEventArgs)
		m_timer.Interval = 2700000
		'(45 minutes)
		m_updateThread = New Thread(New ThreadStart(AddressOf ThreadProc))

		'run the update thread
		m_updateThread.Start()
	End Sub

	''' <summary>
	''' the main update thread for the data.
	''' </summary>
	''' <remarks>Since the information is coming from a web service which might
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
				'put the thread to sleep in order not to overwhelm the RSS website
				'System.Threading.Thread.Sleep(200);

				'get the zip code of the record (column #1)
				lZipCode = Convert.ToInt32(r(1))

				'make the request and update the item
				AddWeatherItem(lZipCode, 0.0, 0.0)
			Next

			'serialize the tables onto the local machine
			Dim ds As New DataSet()
			ds.Tables.Add(m_weatherItemTable)
			ds.WriteXml(System.IO.Path.Combine(m_dataFolder, "Weather.xml"))
			ds.Tables.Remove(m_weatherItemTable)
			ds.Dispose()
			GC.Collect()

			'fire an event to notify update of the weather items 
			RaiseEvent OnWeatherItemsUpdated(Me, New EventArgs())
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
	Private Function DownloadIcon(iconPath As String, width As Integer, height As Integer) As Bitmap
		'if the icon does not exist on the local machine, get it from RSS site
		Dim iconFileName As String = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) & ".png")
		width = 0
		height = 0
		Dim bitmap As Bitmap = Nothing
		If Not File.Exists(iconFileName) Then
			Using webClient As New System.Net.WebClient()
				'open a readable stream to download the bitmap
				Using stream As System.IO.Stream = webClient.OpenRead(iconPath)
					bitmap = New Bitmap(stream, True)

					'save the image as a bitmap in the icons folder
					bitmap.Save(iconFileName, ImageFormat.Png)

					'get the bitmap's dimensions
					width = bitmap.Width
					height = bitmap.Height
				End Using
			End Using
		Else
			'get the bitmap's dimensions
			If True Then
				bitmap = New Bitmap(iconFileName)
				width = bitmap.Width
				height = bitmap.Height
			End If
		End If

		Return bitmap
	End Function

	''' <summary>
	''' get the specified symbol from the symbols table.
	''' </summary>
	''' <param name="iconCode"></param>
	''' <param name="dbr"></param>
	''' <returns></returns>
	Private Function GetSymbol(iconCode As Integer, iconPath As String) As IGraphicTrackerSymbol
		Dim symbol As IGraphicTrackerSymbol
		Dim iconWidth As Integer, iconHeight As Integer
		Dim bitmap As Bitmap = Nothing

		'search for an existing symbol in the table
		Dim r As DataRow = m_symbolTable.Rows.Find(iconCode)
		If r Is Nothing Then
			'in case that the symbol does not exist in the table, create a new entry
			r = m_symbolTable.NewRow()
			r(1) = iconCode

			'Initialize the picture marker symbol
			symbol = InitializeSymbol(iconPath, iconWidth, iconHeight, bitmap)
			If symbol Is Nothing Then
				Return Nothing
			End If

			'update the symbol table
			SyncLock m_symbolTable
				r(2) = symbol
				r(3) = iconWidth
				r(4) = iconHeight
				r(5) = bitmap
				m_symbolTable.Rows.Add(r)
			End SyncLock
		Else
			If TypeOf r(2) Is DBNull Then
				'in case that the record exists but the symbol hasn't been initialized
				'Initialize the symbol
				symbol = InitializeSymbol(iconPath, iconWidth, iconHeight, bitmap)
				If symbol Is Nothing Then
					Return Nothing
				End If

				'update the symbol table
				SyncLock m_symbolTable
					r(2) = symbol
					r(5) = bitmap
					r.AcceptChanges()
				End SyncLock
			Else
				'the record exists in the table and the symbol has been initialized
				'get the symbol
				symbol = TryCast(r(2), IGraphicTrackerSymbol)
			End If
		End If

		'return the requested symbol
		Return symbol
	End Function

	''' <summary>
	''' Initialize a character marker symbol for a given bitmap path
	''' </summary>
	''' <param name="iconPath"></param>
	''' <param name="iconWidth"></param>
	''' <param name="iconHeight"></param>
	''' <returns></returns>
	Private Function InitializeSymbol(iconPath As String, iconWidth As Integer, iconHeight As Integer, bitmap As Bitmap) As IGraphicTrackerSymbol
		iconWidth = InlineAssignHelper(iconHeight, 0)
		bitmap = Nothing
		Try
			'make sure that the icon exit on dist or else download it
			DownloadIcon(iconPath, iconWidth, iconHeight)
			Dim iconFileName As String = System.IO.Path.Combine(m_iconFolder, System.IO.Path.GetFileNameWithoutExtension(iconPath) & ".png")
			If Not System.IO.File.Exists(iconFileName) Then
				Return Nothing
			End If

			Dim symbol As IGraphicTrackerSymbol = m_graphicTracker.CreateSymbolFromPath(iconFileName, iconFileName)

			Return symbol
		Catch
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Makes a request against RSS Weather service and add update the table
	''' </summary>
	''' <param name="zipCode"></param>
	''' <param name="Lat"></param>
	''' <param name="Lon"></param>
	Private Sub AddWeatherItem(zipCode As Long, Lat__1 As Double, Lon__2 As Double)
		Try
			Dim cityName As String
			Dim lat__3 As Double, lon__4 As Double
			Dim temp As Integer
			Dim condition As String
			Dim desc As String
			Dim iconPath As String
			Dim day As String
			Dim [date] As String
			Dim low As Integer
			Dim high As Integer
			Dim iconCode As Integer
			Dim iconWidth As Integer = 52
			'default values
			Dim iconHeight As Integer = 52
			Dim bitmap As Bitmap = Nothing

			Dim dbr As DataRow = m_weatherItemTable.Rows.Find(zipCode)
			If dbr IsNot Nothing Then
				' get the date 
				Dim updateDate As DateTime = Convert.ToDateTime(dbr(14))
				Dim ts As TimeSpan = DateTime.Now - updateDate

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
			If node Is Nothing Then
				Return
			End If

			'get the city name
			cityName = doc.DocumentElement.SelectSingleNode("/rss/channel/yweather:location/@city", xmlnsManager).InnerXml
			If Lat__1 = 0.0 AndAlso Lon__2 = 0.0 Then
				'in case that the caller did not specify a coordinate, get the default coordinate from the service
				lat__3 = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:lat", xmlnsManager).InnerXml)
				lon__4 = Convert.ToDouble(doc.DocumentElement.SelectSingleNode("/rss/channel/item/geo:long", xmlnsManager).InnerXml)
			Else
				lat__3 = Lat__1
				lon__4 = Lon__2
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
			Dim m As Match = Regex.Match(desc, regxQry)
			If m.Success Then
				iconPath = m.Value

				'add the icon ID to the symbology table
				Dim tr As DataRow = m_symbolTable.Rows.Find(iconCode)
				If tr Is Nothing Then
					'get the icon from the website
					bitmap = DownloadIcon(iconPath, iconWidth, iconHeight)

					'create a new record
					tr = m_symbolTable.NewRow()
					tr(1) = iconCode
					tr(3) = iconWidth
					tr(4) = iconHeight
					tr(5) = bitmap

					'update the symbol table. The initialization of the symbol cannot take place in here, since
					'this code gets executed on a background thread.
					SyncLock m_symbolTable
						m_symbolTable.Rows.Add(tr)
					End SyncLock
				Else
					'get the icon's dimensions from the table
					'get the icon's dimensions from the table
					iconWidth = Convert.ToInt32(tr(3))
					iconHeight = Convert.ToInt32(tr(4))
				End If
			Else
				iconPath = ""
			End If

			'test whether the record already exists in the table.
			If dbr Is Nothing Then
				'in case that the record does not exist
				'create a new record
				dbr = m_weatherItemTable.NewRow()

				If Not m_weatherItemTable.Columns(0).AutoIncrement Then
					dbr(0) = Convert.ToInt32(DateTime.Now.Millisecond)
				End If

				'add the item to the table
				SyncLock m_weatherItemTable
					dbr(1) = zipCode
					dbr(2) = cityName
					dbr(3) = lat__3
					dbr(4) = lon__4
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
					dbr(15) = -1

					m_weatherItemTable.Rows.Add(dbr)
				End SyncLock
			Else
				'in case that the record exists, just update it
				'update the record
				SyncLock m_weatherItemTable
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

			'fire an event to notify the user that the item has been updated
            If OnWeatherItemAddedEvent IsNot Nothing Then
                Dim weatherItemEventArgs As New WeatherItemEventArgs(iconCode, zipCode, lon__4, lat__3, iconWidth, iconHeight)
                RaiseEvent OnWeatherItemAdded(Me, weatherItemEventArgs)
            End If
		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine("AddWeatherItem: " & ex.Message)
		End Try
	End Sub

	Private Function ToRGBColor(color As System.Drawing.Color) As IRgbColor
		Dim rgbColor As IRgbColor = New RgbColorClass()
		rgbColor.Red = color.R
		rgbColor.Green = color.G
		rgbColor.Blue = color.B

		Return rgbColor
	End Function

	Private Function ToFontDisp(font As System.Drawing.Font) As stdole.IFontDisp
		Dim aFont As stdole.IFont
		aFont = New stdole.StdFontClass()
		aFont.Name = font.Name
		aFont.Size = CDec(font.Size)
		aFont.Bold = font.Bold
		aFont.Italic = font.Italic
		aFont.Strikethrough = font.Strikeout
		aFont.Underline = font.Underline

		Return TryCast(aFont, stdole.IFontDisp)
	End Function

	''' <summary>
	''' initialize the main table as well as the symbols table.
	''' The base class calls new on the table and adds a default ID field.
	''' </summary>
	Private Sub InitializeTables()
		Dim path As String = System.IO.Path.Combine(m_dataFolder, "Weather.xml")
		'In case that there is no existing cache on the local machine, create the table.
        If Not System.IO.File.Exists(path) Then

            'create the table the table	in addition to the default 'ID' and 'Geometry'	
            m_weatherItemTable = New DataTable("RECORDS")

            m_weatherItemTable.Columns.Add("ID", GetType(Long))
            '0
            m_weatherItemTable.Columns.Add("ZIPCODE", GetType(Long))
            '1
            m_weatherItemTable.Columns.Add("CITYNAME", GetType(String))
            '2
            m_weatherItemTable.Columns.Add("LAT", GetType(Double))
            '3
            m_weatherItemTable.Columns.Add("LON", GetType(Double))
            '4
            m_weatherItemTable.Columns.Add("TEMP", GetType(Integer))
            '5	
            m_weatherItemTable.Columns.Add("CONDITION", GetType(String))
            '6
            m_weatherItemTable.Columns.Add("ICONNAME", GetType(String))
            '7	
            m_weatherItemTable.Columns.Add("ICONID", GetType(Integer))
            '8 
            m_weatherItemTable.Columns.Add("DAY", GetType(String))
            '9	
            m_weatherItemTable.Columns.Add("DATE", GetType(String))
            '10
            m_weatherItemTable.Columns.Add("LOW", GetType(String))
            '11
            m_weatherItemTable.Columns.Add("HIGH", GetType(String))
            '12
            m_weatherItemTable.Columns.Add("SELECTED", GetType(Boolean))
            '13
            m_weatherItemTable.Columns.Add("UPDATEDATE", GetType(DateTime))
            '14	
            m_weatherItemTable.Columns.Add("TRACKERID", GetType(Long))
            '15

            'set the ID column to be auto increment
            m_weatherItemTable.Columns(0).AutoIncrement = True
            m_weatherItemTable.Columns(0).[ReadOnly] = True

            'the zipCode column must be the unique and nut allow null
            m_weatherItemTable.Columns(1).Unique = True

            ' set the ZIPCODE primary key for the table

            m_weatherItemTable.PrimaryKey = New DataColumn() {m_weatherItemTable.Columns("ZIPCODE")}
        Else
            'in case that the local cache exists, simply load the tables from the cache.
            Dim ds As New DataSet()
            ds.ReadXml(path)

            m_weatherItemTable = ds.Tables("RECORDS")

            If m_weatherItemTable Is Nothing Then
                Throw New Exception("Cannot find 'RECORDS' table")
            End If

            If 16 <> m_weatherItemTable.Columns.Count Then
                Throw New Exception("Table 'RECORDS' does not have all required columns")
            End If

            m_weatherItemTable.Columns(0).[ReadOnly] = True

            ' set the ZIPCODE primary key for the table
            m_weatherItemTable.PrimaryKey = New DataColumn() {m_weatherItemTable.Columns("ZIPCODE")}

            'synchronize the locations table
            For Each r As DataRow In m_weatherItemTable.Rows
                Try
                    'in case that the locations table does not exists, create and initialize it
                    If m_locations Is Nothing Then
                        InitializeLocations()
                    End If

                    'get the zipcode for the record
                    Dim zip As String = Convert.ToString(r(1))

                    'make sure that there is no existing record with that zipCode already in the 
                    'locations table.
                    Dim rows As DataRow() = m_locations.[Select]("ZIPCODE = " & zip)
                    If 0 = rows.Length Then
                        Dim rec As DataRow = m_locations.NewRow()
                        rec(1) = Convert.ToInt64(r(1))
                        'zip code 
                        rec(2) = Convert.ToString(r(2))
                        'city name
                        'add the new record to the locations table
                        SyncLock m_locations
                            m_locations.Rows.Add(rec)
                        End SyncLock
                    End If
                Catch ex As Exception
                    System.Diagnostics.Trace.WriteLine(ex.Message)
                End Try
            Next

            'dispose the DS
            ds.Tables.Remove(m_weatherItemTable)
            ds.Dispose()
            GC.Collect()
        End If

		'initialize the symbol map table
		m_symbolTable = New DataTable("Symbology")

		'add the columns to the table
		m_symbolTable.Columns.Add("ID", GetType(Integer))
		'0
		m_symbolTable.Columns.Add("ICONID", GetType(Integer))
		'1
		m_symbolTable.Columns.Add("SYMBOL", GetType(IGraphicTrackerSymbol))
		'2
		m_symbolTable.Columns.Add("SYMBOLWIDTH", GetType(Integer))
		'3
		m_symbolTable.Columns.Add("SYMBOLHEIGHT", GetType(Integer))
		'4
		m_symbolTable.Columns.Add("BITMAP", GetType(Bitmap))
		'5
		'set the ID column to be auto increment
		m_symbolTable.Columns(0).AutoIncrement = True
		m_symbolTable.Columns(0).[ReadOnly] = True

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
		m_locations.Columns(0).[ReadOnly] = True

		'set ZIPCODE as the primary key for the table
		m_locations.PrimaryKey = New DataColumn() {m_locations.Columns("ZIPCODE")}

		PopulateLocationsTable()
	End Sub

	''' <summary>
	''' Load the information from the MajorCities featureclass to the locations table
	''' </summary>
	Private Sub PopulateLocationsTable()
        Dim path As String = System.IO.Path.Combine(m_installationFolder & "\..", "DeveloperKit10.4\Samples\data\USZipCodeData\")

		'open the featureclass
		Dim wf As IWorkspaceFactory = TryCast(New ShapefileWorkspaceFactoryClass(), IWorkspaceFactory)
		Dim ws As IWorkspace = wf.OpenFromFile(path, 0)
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

			While feature IsNot Nothing
				Dim obj As Object = feature.get_Value(nameIndex)
				If obj Is Nothing Then
					Continue While
				End If
				cityName = Convert.ToString(obj)

				obj = feature.get_Value(zipIndex)
				If obj Is Nothing Then
					Continue While
				End If
				zip = Long.Parse(Convert.ToString(obj))
				If zip <= 0 Then
					Continue While
				End If

				'add the current location to the location table
				Dim r As DataRow = m_locations.Rows.Find(zip)
				If r Is Nothing Then
					r = m_locations.NewRow()
					r(1) = zip
					r(2) = cityName
					SyncLock m_locations
						m_locations.Rows.Add(r)
					End SyncLock
				End If

				feature = fCursor.NextFeature()

				index += 1
			End While

			'release the feature cursor
			Marshal.ReleaseComObject(fCursor)
		Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
		End Try
	End Sub


	''' <summary>
	''' weather ItemAdded event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="args"></param>
	''' <remarks>gets fired when an item is added to the table</remarks>
    Private Sub OnWeatherItemAddedEventHandler(ByVal sender As Object, ByVal args As WeatherItemEventArgs)
        ' use the invoke helper since this event gets fired on a different thread
        m_invokeHelper.RefreshWeatherItem(args)
    End Sub
	Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
		target = value
		Return value
	End Function
	#End Region
End Class
