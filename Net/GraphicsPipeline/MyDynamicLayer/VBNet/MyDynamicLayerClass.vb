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
Imports System.Data
Imports System.Drawing
Imports System.Timers
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF

  ''' <summary>
  ''' This layer implements a DynamicLayer based on the DynamicLayerBase base-class
  ''' </summary>
  <Guid("27FB44EB-0426-415f-BFA3-D1581056C0C4"), ComVisible(True), ClassInterface(ClassInterfaceType.None), ProgId("MyDynamicLayerClass")> _
  Public NotInheritable Class MyDynamicLayerClass : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseDynamicLayer
	
	#Region "class members"
	Private m_point As IPoint = Nothing
	Private m_bDynamicGlyphsCreated As Boolean = False
	Private m_bOnce As Boolean = True
	Private m_markerGlyphs As IDynamicGlyph() = Nothing
	Private m_textGlyph As IDynamicGlyph = Nothing
	Private m_updateTimer As Timer = Nothing
	Private m_extentMaxX As Double = 1000.0
	Private m_extentMaxY As Double = 1000.0
	Private m_extentMinX As Double = 0.0
	Private m_extentMinY As Double = 0.0
	Private m_dynamicGlyphFactory As IDynamicGlyphFactory2 = Nothing
	Private m_dynamicSymbolProps As IDynamicSymbolProperties = Nothing
	Private m_dynamicCompoundMarker As IDynamicCompoundMarker = Nothing
	Private m_table As DataTable = Nothing
	Private Const m_nNumOfItems As Integer = 100
	#End Region

	#Region "class constructor"
	Public Sub New()
		MyBase.New()
			m_table = New DataTable ("rows")
			m_table.Columns.Add("OID", GetType(Integer)) '0
	  m_table.Columns.Add("X", GetType(Double)) '1
	  m_table.Columns.Add("Y", GetType(Double)) '2
	  m_table.Columns.Add("STEPX", GetType(Double)) '3
	  m_table.Columns.Add("STEPY", GetType(Double)) '4
	  m_table.Columns.Add("HEADING", GetType(Double)) '5
	  m_table.Columns.Add("TYPE", GetType(Integer)) '6

	  'set the ID column to be AutoIncremented
	  m_table.Columns(0).AutoIncrement = True

	  m_point = New PointClass()

	  'set an array to store the glyphs used to symbolize the tracked object
	  m_markerGlyphs = New IDynamicGlyph(2){}

	  'set the update timer for the layer
	  m_updateTimer = New Timer(50)
	  m_updateTimer.Enabled = False
	  AddHandler m_updateTimer.Elapsed, AddressOf OnLayerUpdateEvent

	End Sub
	#End Region

#Region "overridden methods"
	''' <summary>
	''' The dynamic layer draw method
	''' </summary>
	''' <param name="DynamicDrawPhase">the current drawphase of the dynamic drawing</param>
	''' <param name="Display">the ActiveView's display</param>
	''' <param name="DynamicDisplay">the ActiveView's dynamic display</param>
	Public Overrides Sub DrawDynamicLayer(ByVal DynamicDrawPhase As ESRI.ArcGIS.Display.esriDynamicDrawPhase, ByVal Display As ESRI.ArcGIS.Display.IDisplay, ByVal DynamicDisplay As ESRI.ArcGIS.Display.IDynamicDisplay)
	  Try
			'make sure that the display is valid as well as that the layer is visible
			If Nothing Is DynamicDisplay OrElse Nothing Is Display OrElse (Not Me.m_visible) Then
				Return
			End If

			'make sure that the current drawphase is immediate. In this sample there is no use of the
			'compiled drawPhase. Use the esriDDPCompiled drawPhase in order to draw semi-static items (items
			'which have update rate lower than the display update rate).
			If DynamicDrawPhase <> esriDynamicDrawPhase.esriDDPImmediate Then
				Return
			End If

			If m_bOnce Then
				'cast the DynamicDisplay into DynamicGlyphFactory
				m_dynamicGlyphFactory = TryCast(DynamicDisplay.DynamicGlyphFactory, IDynamicGlyphFactory2)
				'cast the DynamicDisplay into DynamicSymbolProperties
				m_dynamicSymbolProps = TryCast(DynamicDisplay, IDynamicSymbolProperties)
				'cast the compound marker symbol
				m_dynamicCompoundMarker = TryCast(DynamicDisplay, IDynamicCompoundMarker)

				IntializeLayerData (Display.DisplayTransformation)
				GetLayerExtent()

				m_bOnce = False
			End If

			'get the display fitted bounds
			m_extentMaxX = Display.DisplayTransformation.FittedBounds.XMax
			m_extentMaxY = Display.DisplayTransformation.FittedBounds.YMax
			m_extentMinX = Display.DisplayTransformation.FittedBounds.XMin
			m_extentMinY = Display.DisplayTransformation.FittedBounds.YMin

			'create the dynamic symbols for the layer
			If (Not m_bDynamicGlyphsCreated) Then
				Me.CreateDynamicSymbols(m_dynamicGlyphFactory)
				m_bDynamicGlyphsCreated = True
			End If

			Dim X, Y, heading As Double
			Dim type As Integer
			'iterate through the layers' items
			For Each r As DataRow In m_table.Rows
				If TypeOf r(1) Is DBNull OrElse TypeOf r(2) Is DBNull Then
					Continue For
				End If

				'get the item's coordinate, heading and type
				X = Convert.ToDouble(r(1))
				Y = Convert.ToDouble(r(2))
				heading = Convert.ToDouble(r(5))
				type = Convert.ToInt32(r(6))

				'assign the items' coordinate to the cached point
				m_point.PutCoords(X, Y)

				'set the symbol's properties
				Select Case type
				Case 0
					'set the heading of the current symbols' text
					m_dynamicSymbolProps.Heading(esriDynamicSymbolType.esriDSymbolText)= 0.0f
					m_dynamicSymbolProps.Heading(esriDynamicSymbolType.esriDSymbolMarker)= 0.0f

					'set the symbol alignment so that it will align with the screen
					m_dynamicSymbolProps.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker)=esriDynamicSymbolRotationAlignment.esriDSRAScreen

					'set the text alignment so that it will also align with the screen
					m_dynamicSymbolProps.RotationAlignment(esriDynamicSymbolType.esriDSymbolText)= esriDynamicSymbolRotationAlignment.esriDSRAScreen

					'scale the item
					m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 0.8f, 0.8f)
					'set the items' color (blue)
					m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0f, 0.0f, 1.0f, 1.0f) ' Blue
					'assign the item's glyph to the dynamic-symbol
					m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_markerGlyphs(0)
					'set the labels text glyph
					m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolText)= m_textGlyph
					'set the color of the text
					m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolText, 1.0f, 1.0f, 0.0f, 1.0f) ' Yellow

                        'draw the item as a compound marker. This means that you do not have to draw the items and their
					'accompanying labels separately, and thus allow you to write less code as well as get better
					'performance.  
					'"TOP",
					'"BOTTOM",
					m_dynamicCompoundMarker.DrawCompoundMarker4 (m_point, "Item " & Convert.ToString(r(0)), heading.ToString("###.##"), m_point.X.ToString("###.#####"), m_point.Y.ToString("###.#####"))
				Case 1
					'set the heading of the current symbol
					m_dynamicSymbolProps.Heading(esriDynamicSymbolType.esriDSymbolMarker)= CSng(heading)

				 'set the symbol alignment so that it will align with towards the symbol heading
					m_dynamicSymbolProps.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker)= esriDynamicSymbolRotationAlignment.esriDSRANorth

					m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f)
					m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0f, 1.0f, 0.6f, 1.0f) ' GREEN
					m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_markerGlyphs(1)

					'draw the current location
					DynamicDisplay.DrawMarker(m_point)
				Case 2
					'set the heading of the current symbol
					m_dynamicSymbolProps.Heading(esriDynamicSymbolType.esriDSymbolMarker)= CSng(heading)

					'set the symbol alignment so that it will align with towards the symbol heading
					m_dynamicSymbolProps.RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker)= esriDynamicSymbolRotationAlignment.esriDSRANorth

					m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.1f, 1.1f)
					m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f) ' WHITE
					m_dynamicSymbolProps.DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker)= m_markerGlyphs(2)

					'draw the current location
					DynamicDisplay.DrawMarker(m_point)
				End Select
			Next r

			' by setting immediate flag to false, we signal the dynamic display that the layer is current.
			MyBase.m_bIsImmediateDirty = False

	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
	  End Try
	End Sub

	''' <summary>
	''' Returns the UID (ProgID or CLSID)
	''' </summary>
	Public Overrides ReadOnly Property ID() As UID
	  Get
			m_uid.Value = "MyDynamicLayer.MyDynamicLayerClass"
			Return m_uid
	  End Get
	End Property
	#End Region

	#Region "public methods"
	Public Sub Connect()
	  m_updateTimer.Enabled = True
	End Sub

	Public Sub Disconnect()
	  m_updateTimer.Enabled = False
	End Sub

		Public Function NewItem() As DataRow
			If m_table Is Nothing Then
				Return Nothing
			Else
				Return m_table.NewRow ()
			End If
		End Function

		Public Sub AddItem(ByVal row As DataRow)
			If row Is Nothing Then
				Return
			Else
				m_table.Rows.Add (row)
			End If
		End Sub
	#End Region

	#Region "private utility methods"
	''' <summary>
	''' create the layer's glyphs used to set the symbol of the dynamic-layer items
	''' </summary>
	''' <param name="pDynamicGlyphFactory"></param>
	Private Sub CreateDynamicSymbols(ByVal pDynamicGlyphFactory As IDynamicGlyphFactory2)
	  Try
			'set the background color
			Dim backgroundColor As IColor 
            backgroundColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255))

			' Create Character Marker Symbols glyph
			' --------------------------------------
			Dim characterMarkerSymbol As ICharacterMarkerSymbol = New CharacterMarkerSymbolClass()
			characterMarkerSymbol.Color = TryCast(backgroundColor, IColor)
            characterMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(New Font("ESRI Environmental & Icons", 32))
			characterMarkerSymbol.Size = 40
			characterMarkerSymbol.Angle = 0
			characterMarkerSymbol.CharacterIndex = 36

			'create the glyph from the marker symbol
			m_markerGlyphs(0) = pDynamicGlyphFactory.CreateDynamicGlyph(TryCast(characterMarkerSymbol, ISymbol))

			characterMarkerSymbol.Size = 32
			characterMarkerSymbol.CharacterIndex = 224

			'create the glyph from the marker symbol
			m_markerGlyphs(1) = pDynamicGlyphFactory.CreateDynamicGlyph(TryCast(characterMarkerSymbol, ISymbol))

			' Create the glyph from embedded bitmap
			' -----------------------------------
		  ' Sets the transparency color
		  Dim transparenyColor As IColor
            transparenyColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255))
		
			Dim bitmap as new Bitmap(me.GetType(), "B2.bmp")
		  m_markerGlyphs(2) = pDynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker,bitmap.GetHbitmap.ToInt32(),False,transparenyColor)

			' Create a glyph for the labels text, use the first 'internal' text glyph 
			' ------------------------------------------------------------------------
			m_textGlyph = pDynamicGlyphFactory.DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1)

	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
	  End Try
	End Sub

	''' <summary>
	''' timer elapsed event handler, used to update the layer
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks>This layer has synthetic data, and therefore need the timer in order
	''' to update the layers' items.</remarks>
	Private Sub OnLayerUpdateEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
	  Try
			Dim X, Y, stepX, stepY, heading As Double

		'iterate through the layers' records
			For Each r As DataRow In m_table.Rows
				If TypeOf r(1) Is DBNull OrElse TypeOf r(2) Is DBNull Then
					Continue For
				End If

				'get the current item location and the item's steps
				X = Convert.ToDouble(r(1))
				Y = Convert.ToDouble(r(2))
				stepX = Convert.ToDouble(r(3))
				stepY = Convert.ToDouble(r(4))

				'increment the item's location
				X += stepX
				Y += stepY

				'test that the item's location is within the fitted bounds
				If X > m_extentMaxX Then
					stepX = -Math.Abs(stepX)
				End If
				If X < m_extentMinX Then
					stepX = Math.Abs(stepX)
				End If
				If Y > m_extentMaxY Then
					stepY = -Math.Abs(stepY)
				End If
				If Y < m_extentMinY Then
					stepY = Math.Abs(stepY)
				End If
				'calculate the item's heading
				heading = (360.0 + 90.0 - Math.Atan2(stepY, stepX) * 180 / Math.PI) Mod 360.0

				'update the item's record
				r(1) = X
				r(2) = Y
				r(3) = stepX
				r(4) = stepY
				r(5) = heading
				SyncLock m_table
				r.AcceptChanges()
				End SyncLock

					'set the dirty flag to true in order to let the DynamicDisplay that the layer needs redraw.
					MyBase.m_bIsImmediateDirty = True
			Next r
	  Catch ex As Exception
			System.Diagnostics.Trace.WriteLine(ex.Message)
	  End Try
	End Sub

	''' <summary>
	''' Calculates the layer extent
	''' </summary>
	Private Sub GetLayerExtent()
	  If Nothing Is m_table Then
			Return
	  End If

	  Dim env As IEnvelope = New EnvelopeClass()
		env.SpatialReference = MyBase.m_spatialRef
	  Dim point As IPoint = New PointClass()
		For Each r As DataRow In m_table.Rows
			If TypeOf r(1) Is DBNull OrElse TypeOf r(2) Is DBNull Then
				Continue For
			End If

			point.Y = Convert.ToDouble(r(3))
			point.X = Convert.ToDouble(r(4))

			env.Union(point.Envelope)
		Next r

	  MyBase.m_extent = env
	End Sub

		''' <summary>
		''' Initialize the synthetic data of the layer
		''' </summary>
		Private Sub IntializeLayerData(ByVal displayTransformation As IDisplayTransformation)
			Try

				'get the map's fitted bounds
				Dim extent As IEnvelope = displayTransformation.FittedBounds

				'calculate the step for which will be used to increment the items
				Dim XStep As Double = extent.Width / 5000.0
				Dim YStep As Double = extent.Height / 5000.0

				Dim rnd As Random = New Random ()
				Dim stepX, stepY As Double

				'generate the items
				For i As Integer = 0 To m_nNumOfItems - 1
					'calculate the step for each item
					stepX = XStep * rnd.NextDouble ()
					stepY = YStep * rnd.NextDouble ()

					'create new record
					Dim r As DataRow = NewItem ()
					'set the item's coordinate
					r(1) = extent.XMin + rnd.NextDouble () * (extent.XMax - extent.XMin)
					r(2) = extent.YMin + rnd.NextDouble () * (extent.YMax - extent.YMin)
					'set the item's steps
					r(3) = stepX
					r(4) = stepY
					'calculate the heading
					r(5) = (360.0 + 90.0 - Math.Atan2 (stepY, stepX) * 180 / Math.PI) Mod 360.0

					'add a type ID in order to define the symbol for the item
					Select Case i Mod 3
						Case 0
							r(6) = 0
						Case 1
							r(6) = 1
						Case 2
							r(6) = 2
					End Select

					'add the new item record to the table
					AddItem (r)
				Next i
			Catch ex As Exception
				System.Diagnostics.Trace.WriteLine (ex.Message)
			End Try
		End Sub
	#End Region

  End Class

