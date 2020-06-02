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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.DataManagementTools

Public Partial Class RunGPForm
	Inherits Form
  Private _gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor = Nothing

	''' <summary>
	''' A Dictionary contains the layers loaded into the application. The Keys are named appropriately.
	''' </summary>
	Private _layersDict As New Dictionary(Of String, IFeatureLayer)()

	''' <summary>
	''' A List of FeatureLayer objects each of which represents result layers that are added to the Map.
	''' In this example only one Layer is added to the List.
	''' </summary>
	Private _resultsList As New List(Of IFeatureLayer)()

	''' <summary>
	''' A Queue of GPProcess objects each of which represents a geoprocessing tool to be executed asynchronously.
	''' </summary>
	Private _myGPToolsToExecute As New Queue(Of IGPProcess)()


	''' <summary>
	''' Initializes a new instance of the RunGPForm class which represents the ArcGIS Engine application.
	''' The constructor is used to perform setup: the ListViewControl is configured, a new Geoprocessor
	''' object is created, the output result directory is specified and event handlers created in order
	''' to listen to geoprocessing events. A helper method called SetupMap is called to create, add and
	''' symbolize the layers used by the application.
	''' </summary>
	Public Sub New()
		InitializeComponent()

		'Set up ListView control
		listView1.Columns.Add("Event", 200, HorizontalAlignment.Left)
		listView1.Columns.Add("Message", 1000, HorizontalAlignment.Left)
		'The image list component contains the icons used in the ListView control
		listView1.SmallImageList = imageList1

		'Create a Geoprocessor object and associated event handlers which will be fired during tool execution
    _gp = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()
		'All results will be written to the users TEMP folder
		Dim outputDir As String = System.Environment.GetEnvironmentVariable("TEMP")
		_gp.SetEnvironmentValue("workspace", outputDir)
		_gp.OverwriteOutput = True
		AddHandler _gp.ToolExecuted, New EventHandler(Of ToolExecutedEventArgs)(AddressOf _gp_ToolExecuted)
		AddHandler _gp.ProgressChanged, New EventHandler(Of ESRI.ArcGIS.Geoprocessor.ProgressChangedEventArgs)(AddressOf _gp_ProgressChanged)
		AddHandler _gp.MessagesCreated, New EventHandler(Of MessagesCreatedEventArgs)(AddressOf _gp_MessagesCreated)
		AddHandler _gp.ToolExecuting, New EventHandler(Of ToolExecutingEventArgs)(AddressOf _gp_ToolExecuting)

		'Add layers to the map, select a city, zoom in on it
		SetupMap()
	End Sub

	''' <summary>
	''' Handles the click event of the Button and runs the geoprocessing tasks asynchronously.
	''' </summary>
  Private Sub btnRunGP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRunGP.Click
    Try
      '#Region "tidy up any previous gp runs"

      'Clear the ListView control
      listView1.Items.Clear()

      'Remove any result layers present in the map
      Dim mapLayers As IMapLayers = TryCast(axMapControl1.Map, IMapLayers)
      For Each resultLayer As IFeatureLayer In _resultsList
        mapLayers.DeleteLayer(resultLayer)
      Next

      axTOCControl1.Update()

      'Empty the results layer list
      _resultsList.Clear()

      'make sure that my GP tool queue is empty
      _myGPToolsToExecute.Clear()

      '#End Region

      'Buffer the selected cities by the specified distance
      Dim bufferTool As New ESRI.ArcGIS.AnalysisTools.Buffer()
      bufferTool.in_features = _layersDict("Cities")
      bufferTool.buffer_distance_or_field = txtBufferDistance.Text & " Miles"
      bufferTool.out_feature_class = "city_buffer.shp"

      'Clip the zip codes layer with the result of the buffer tool
      Dim clipTool As New ESRI.ArcGIS.AnalysisTools.Clip()
      clipTool.in_features = _layersDict("ZipCodes")
      clipTool.clip_features = bufferTool.out_feature_class
      clipTool.out_feature_class = "city_buffer_clip.shp"

      'To run multiple GP tools asynchronously, all tool inputs must exist before ExecuteAsync is called.
      'The output from the first buffer operation is used as an input to the second clip
      'operation. To deal with this restriction a Queue is created and all tools added to it. The first tool
      'is executed from this method, whereas the second is executed from the ToolExecuted event. This approach
      'is scalable - any additional geoprocessing tools added to this queue will also be executed in turn.
      _myGPToolsToExecute.Enqueue(bufferTool)
      _myGPToolsToExecute.Enqueue(clipTool)
      _gp.ExecuteAsync(_myGPToolsToExecute.Dequeue())
    Catch ex As Exception
      listView1.Items.Add(New ListViewItem(New String(1) {"N/A", ex.Message}, "error"))
    End Try
  End Sub

	''' <summary>
	''' Handles the ProgressChanged event.
	''' </summary>
	Private Sub _gp_ProgressChanged(sender As Object, e As ESRI.ArcGIS.Geoprocessor.ProgressChangedEventArgs)
		Dim gpResult As IGeoProcessorResult2 = DirectCast(e.GPResult, IGeoProcessorResult2)

		If e.ProgressChangedType = ProgressChangedType.Message Then
			listView1.Items.Add(New ListViewItem(New String(1) {"ProgressChanged", e.Message}, "information"))
		End If
	End Sub

	''' <summary>
	''' Handles the ToolExecuting event. All tools start by firing this event.
	''' </summary>
	Private Sub _gp_ToolExecuting(sender As Object, e As ToolExecutingEventArgs)
		Dim gpResult As IGeoProcessorResult2 = DirectCast(e.GPResult, IGeoProcessorResult2)
		listView1.Items.Add(New ListViewItem(New String(1) {"ToolExecuting", gpResult.Process.Tool.Name & " " & gpResult.Status.ToString()}, "information"))
	End Sub

	''' <summary>
	''' Handles the ToolExecuted event. All tools end by firing this event. If the tool executed successfully  
	''' the next tool in the queue is removed and submitted for execution.  If unsuccessful, geoprocessing is terminated,
	''' an error message is written to the ListViewControl and the is queue cleared. After the final tool has executed
	''' the result layer is added to the Map.
	''' </summary>
	Private Sub _gp_ToolExecuted(sender As Object, e As ToolExecutedEventArgs)
		Dim gpResult As IGeoProcessorResult2 = DirectCast(e.GPResult, IGeoProcessorResult2)

		Try
			'The first GP tool has completed, if it was successful process the others
			If gpResult.Status = esriJobStatus.esriJobSucceeded Then
				listView1.Items.Add(New ListViewItem(New String(1) {"ToolExecuted", gpResult.Process.Tool.Name}, "success"))

				'Execute next tool in the queue
				If _myGPToolsToExecute.Count > 0 Then
					_gp.ExecuteAsync(_myGPToolsToExecute.Dequeue())
				Else
					'If last tool has executed add the output layer to the map
					Dim resultFClass As IFeatureClass = TryCast(_gp.Open(gpResult.ReturnValue), IFeatureClass)
					Dim resultLayer As IFeatureLayer = New FeatureLayerClass()
					resultLayer.FeatureClass = resultFClass
					resultLayer.Name = resultFClass.AliasName

					'Add the result to the map
					axMapControl1.AddLayer(DirectCast(resultLayer, ILayer), 2)
					axTOCControl1.Update()

					'add the result layer to the List of result layers
					_resultsList.Add(resultLayer)
				End If
			'If the GP process failed, do not try to process any more tools in the queue
			ElseIf gpResult.Status = esriJobStatus.esriJobFailed Then
				'The actual GP error message will be output by the MessagesCreated event handler
				listView1.Items.Add(New ListViewItem(New String(1) {"ToolExecuted", gpResult.Process.Tool.Name & " failed, any remaining processes will not be executed."}, "error"))
				'Empty the queue
				_myGPToolsToExecute.Clear()
			End If
		Catch ex As Exception
			listView1.Items.Add(New ListViewItem(New String(1) {"ToolExecuted", ex.Message}, "error"))
		End Try
	End Sub

	''' <summary>
	''' Handles the MessagesCreated event.
	''' </summary>
	Private Sub _gp_MessagesCreated(sender As Object, e As MessagesCreatedEventArgs)

		Dim gpMsgs As IGPMessages = e.GPMessages

		If gpMsgs.Count > 0 Then
			For count As Integer = 0 To gpMsgs.Count - 1
				Dim msg As IGPMessage = gpMsgs.GetMessage(count)
				Dim imageToShow As String = "information"

				Select Case msg.Type
					Case esriGPMessageType.esriGPMessageTypeAbort
            imageToShow = "warning"
						Exit Select
          Case esriGPMessageType.esriGPMessageTypeEmpty
            imageToShow = "information"
            Exit Select
					Case esriGPMessageType.esriGPMessageTypeError
						imageToShow = "error"
						Exit Select
					Case esriGPMessageType.esriGPMessageTypeGDBError
            imageToShow = "error"
						Exit Select
					Case esriGPMessageType.esriGPMessageTypeInformative
						imageToShow = "information"
						Exit Select
          Case esriGPMessageType.esriGPMessageTypeProcessDefinition
            imageToShow = "information"
            Exit Select
          Case esriGPMessageType.esriGPMessageTypeProcessStart
            imageToShow = "information"
            Exit Select
					Case esriGPMessageType.esriGPMessageTypeProcessStop
						imageToShow = "information"
						Exit Select
					Case esriGPMessageType.esriGPMessageTypeWarning
						imageToShow = "warning"
						Exit Select
					Case Else
						Exit Select
				End Select

				listView1.Items.Add(New ListViewItem(New String(1) {"MessagesCreated", msg.Description}, imageToShow))
			Next
		End If
	End Sub

	#Region "Helper methods for setting up map and layers and validating buffer distance input"

	''' <summary>
	''' Loads and symbolizes data used by the application.  Selects a city and zooms to it
	''' </summary>
	Private Sub SetupMap()
		Try
			'Relative path to the sample data from EXE location
			Dim dirPath As String = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\USZipCodeData")

      'Create the cities layer
			Dim cities As IFeatureClass = TryCast(_gp.Open(dirPath & "\ZipCode_Boundaries_US_Major_Cities.shp"), IFeatureClass)
			Dim citiesLayer As IFeatureLayer = New FeatureLayerClass()
			citiesLayer.FeatureClass = cities
			citiesLayer.Name = "Major Cities"
			
      'Create the zip code boundaries layer
			Dim zipBndrys As IFeatureClass = TryCast(_gp.Open(dirPath & "\US_ZipCodes.shp"), IFeatureClass)
			Dim zipBndrysLayer As IFeatureLayer = New FeatureLayerClass()
			zipBndrysLayer.FeatureClass = zipBndrys
			zipBndrysLayer.Name = "Zip Code boundaries"
			
      'Create the highways layer
			dirPath = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\USAMajorHighways")
			Dim highways As IFeatureClass = TryCast(_gp.Open(dirPath & "\usa_major_highways.shp"), IFeatureClass)
			Dim highwaysLayer As IFeatureLayer = New FeatureLayerClass()
			highwaysLayer.FeatureClass = highways
			highwaysLayer.Name = "Highways"
			
      '***** Important code *********
      'Add the layers to a dictionary. Layers can then easily be returned by their 'key'
      _layersDict.Add("ZipCodes", zipBndrysLayer)
      _layersDict.Add("Highways", highwaysLayer)
      _layersDict.Add("Cities", citiesLayer)

      'Symbolize and set additional properties for each layer
      'Setup and symbolize the cities layer
      citiesLayer.Selectable = True
      citiesLayer.ShowTips = True
      Dim markerSym As ISimpleMarkerSymbol = CreateSimpleMarkerSymbol(CreateRGBColor(0, 92, 230), esriSimpleMarkerStyle.esriSMSCircle)
      markerSym.Size = 9
      Dim simpleRend As ISimpleRenderer = New SimpleRendererClass()
      simpleRend.Symbol = DirectCast(markerSym, ISymbol)
      DirectCast(citiesLayer, IGeoFeatureLayer).Renderer = DirectCast(simpleRend, IFeatureRenderer)

      'Setup and symbolize the zip boundaries layer
      zipBndrysLayer.Selectable = False
      Dim fillSym As ISimpleFillSymbol = CreateSimpleFillSymbol(CreateRGBColor(0, 0, 0), esriSimpleFillStyle.esriSFSHollow, CreateRGBColor(204, 204, 204), esriSimpleLineStyle.esriSLSSolid, 0.5)
      Dim simpleRend2 As ISimpleRenderer = New SimpleRendererClass()
      simpleRend2.Symbol = DirectCast(fillSym, ISymbol)
      DirectCast(zipBndrysLayer, IGeoFeatureLayer).Renderer = DirectCast(simpleRend2, IFeatureRenderer)

      'Setup and symbolize the highways layer
      highwaysLayer.Selectable = False
      Dim lineSym As ISimpleLineSymbol = CreateSimpleLineSymbol(CreateRGBColor(250, 52, 17), 3.4, esriSimpleLineStyle.esriSLSSolid)
      Dim simpleRend3 As ISimpleRenderer = New SimpleRendererClass()
      simpleRend3.Symbol = DirectCast(lineSym, ISymbol)
      DirectCast(highwaysLayer, IGeoFeatureLayer).Renderer = DirectCast(simpleRend3, IFeatureRenderer)

			'Add the layers to the Map
			For Each layer As IFeatureLayer In _layersDict.Values
				axMapControl1.AddLayer(DirectCast(layer, ILayer))
			Next

      'Select and zoom in on Los Angeles city
			Dim qf As IQueryFilter = New QueryFilterClass()
			qf.WhereClause = "NAME='Los Angeles'"
			Dim citiesLayerSelection As IFeatureSelection = DirectCast(citiesLayer, IFeatureSelection)
			citiesLayerSelection.SelectFeatures(qf, esriSelectionResultEnum.esriSelectionResultNew, True)
			Dim laFeature As IFeature = cities.GetFeature(citiesLayerSelection.SelectionSet.IDs.[Next]())
			Dim env As IEnvelope = laFeature.Shape.Envelope
			env.Expand(0.5, 0.5, False)
			axMapControl1.Extent = env
			axMapControl1.Refresh()
			axTOCControl1.Update()

			'Enable GP analysis button
			btnRunGP.Enabled = True
		Catch ex As Exception
			MessageBox.Show("There was an error loading the data used by this sample: " & ex.Message)
		End Try
	End Sub


	#Region "Create Simple Fill Symbol"
	' ArcGIS Snippet Title:
	' Create Simple Fill Symbol
	' 
	' Long Description:
	' Create a simple fill symbol by specifying a color, outline color and fill style.
	' 
	' Add the following references to the project:
	' ESRI.ArcGIS.Display
	' ESRI.ArcGIS.System
	' 
	' Intended ArcGIS Products for this snippet:
	' ArcGIS Desktop (Standard, Advanced, Basic)
	' ArcGIS Engine
	' ArcGIS Server
	' 
	' Applicable ArcGIS Product Versions:
	' 9.2
	' 9.3
	' 9.3.1
	' 10.0
	' 
	' Required ArcGIS Extensions:
	' (NONE)
	' 
	' Notes:
	' This snippet is intended to be inserted at the base level of a Class.
	' It is not intended to be nested within an existing Method.
	' 

	'''<summary>Create a simple fill symbol by specifying a color, outline color and fill style.</summary>
	'''  
	'''<param name="fillColor">An IRGBColor interface. The color for the inside of the fill symbol.</param>
	'''<param name="fillStyle">An esriSimpleLineStyle enumeration for the inside fill symbol. Example: esriSFSSolid.</param>
	'''<param name="borderColor">An IRGBColor interface. The color for the outside line border of the fill symbol.</param>
	'''<param name="borderStyle">An esriSimpleLineStyle enumeration for the outside line border. Example: esriSLSSolid.</param>
	'''<param name="borderWidth">A System.Double that is the width of the outside line border in points. Example: 2</param>
	'''   
	'''<returns>An ISimpleFillSymbol interface.</returns>
	'''  
	'''<remarks></remarks>
	Public Function CreateSimpleFillSymbol(fillColor As ESRI.ArcGIS.Display.IRgbColor, fillStyle As ESRI.ArcGIS.Display.esriSimpleFillStyle, borderColor As ESRI.ArcGIS.Display.IRgbColor, borderStyle As ESRI.ArcGIS.Display.esriSimpleLineStyle, borderWidth As System.Double) As ESRI.ArcGIS.Display.ISimpleFillSymbol

		Dim simpleLineSymbol As ESRI.ArcGIS.Display.ISimpleLineSymbol = New ESRI.ArcGIS.Display.SimpleLineSymbolClass()
		simpleLineSymbol.Width = borderWidth
		simpleLineSymbol.Color = borderColor
		simpleLineSymbol.Style = borderStyle

		Dim simpleFillSymbol As ESRI.ArcGIS.Display.ISimpleFillSymbol = New ESRI.ArcGIS.Display.SimpleFillSymbolClass()
		simpleFillSymbol.Outline = simpleLineSymbol
		simpleFillSymbol.Style = fillStyle
		simpleFillSymbol.Color = fillColor

		Return simpleFillSymbol
	End Function
	#End Region


	#Region "Create Simple Line Symbol"
	' ArcGIS Snippet Title:
	' Create Simple Line Symbol
	' 
	' Long Description:
	' Create a simple line symbol by specifying a color, width and line style.
	' 
	' Add the following references to the project:
	' ESRI.ArcGIS.Display
	' ESRI.ArcGIS.System
	' 
	' Intended ArcGIS Products for this snippet:
	' ArcGIS Desktop (Standard, Advanced, Basic)
	' ArcGIS Engine
	' ArcGIS Server
	' 
	' Applicable ArcGIS Product Versions:
	' 9.2
	' 9.3
	' 9.3.1
	' 10.0
	' 
	' Required ArcGIS Extensions:
	' (NONE)
	' 
	' Notes:
	' This snippet is intended to be inserted at the base level of a Class.
	' It is not intended to be nested within an existing Method.
	' 

	'''<summary>Create a simple line symbol by specifying a color, width and line style.</summary>
	'''  
	'''<param name="rgbColor">An IRGBColor interface.</param>
	'''<param name="inWidth">A System.Double that is the width of the line symbol in points. Example: 2</param>
	'''<param name="inStyle">An esriSimpleLineStyle enumeration. Example: esriSLSSolid.</param>
	'''   
	'''<returns>An ISimpleLineSymbol interface.</returns>
	'''  
	'''<remarks></remarks>
	Public Function CreateSimpleLineSymbol(rgbColor As ESRI.ArcGIS.Display.IRgbColor, inWidth As System.Double, inStyle As ESRI.ArcGIS.Display.esriSimpleLineStyle) As ESRI.ArcGIS.Display.ISimpleLineSymbol
		If rgbColor Is Nothing Then
			Return Nothing
		End If

		Dim simpleLineSymbol As ESRI.ArcGIS.Display.ISimpleLineSymbol = New ESRI.ArcGIS.Display.SimpleLineSymbolClass()
		simpleLineSymbol.Style = inStyle
		simpleLineSymbol.Color = rgbColor
		simpleLineSymbol.Width = inWidth

		Return simpleLineSymbol
	End Function
	#End Region


	#Region "Create Simple Marker Symbol"
	' ArcGIS Snippet Title:
	' Create Simple Marker Symbol
	' 
	' Long Description:
	' Create a simple marker symbol by specifying and input color and marker style.
	' 
	' Add the following references to the project:
	' ESRI.ArcGIS.Display
	' ESRI.ArcGIS.System
	' 
	' Intended ArcGIS Products for this snippet:
	' ArcGIS Desktop (Standard, Advanced, Basic)
	' ArcGIS Engine
	' ArcGIS Server
	' 
	' Applicable ArcGIS Product Versions:
	' 9.2
	' 9.3
	' 9.3.1
	' 10.0
	' 
	' Required ArcGIS Extensions:
	' (NONE)
	' 
	' Notes:
	' This snippet is intended to be inserted at the base level of a Class.
	' It is not intended to be nested within an existing Method.
	' 

	'''<summary>Create a simple marker symbol by specifying and input color and marker style.</summary>
	'''  
	'''<param name="rgbColor">An IRGBColor interface.</param>
	'''<param name="inputStyle">An esriSimpleMarkerStyle enumeration. Example: esriSMSCircle.</param>
	'''   
	'''<returns>An ISimpleMarkerSymbol interface.</returns>
	'''  
	'''<remarks></remarks>
	Public Function CreateSimpleMarkerSymbol(rgbColor As ESRI.ArcGIS.Display.IRgbColor, inputStyle As ESRI.ArcGIS.Display.esriSimpleMarkerStyle) As ESRI.ArcGIS.Display.ISimpleMarkerSymbol

		Dim simpleMarkerSymbol As ESRI.ArcGIS.Display.ISimpleMarkerSymbol = New ESRI.ArcGIS.Display.SimpleMarkerSymbolClass()
		simpleMarkerSymbol.Color = rgbColor
		simpleMarkerSymbol.Style = inputStyle

		Return simpleMarkerSymbol
	End Function
	#End Region


	#Region "Create RGBColor"
	' ArcGIS Snippet Title:
	' Create RGBColor
	' 
	' Long Description:
	' Generate an RgbColor by specifying the amount of Red, Green and Blue.
	' 
	' Add the following references to the project:
	' ESRI.ArcGIS.Display
	' ESRI.ArcGIS.System
	' 
	' Intended ArcGIS Products for this snippet:
	' ArcGIS Desktop (Standard, Advanced, Basic)
	' ArcGIS Engine
	' ArcGIS Server
	' 
	' Applicable ArcGIS Product Versions:
	' 9.2
	' 9.3
	' 9.3.1
	' 10.0
	' 
	' Required ArcGIS Extensions:
	' (NONE)
	' 
	' Notes:
	' This snippet is intended to be inserted at the base level of a Class.
	' It is not intended to be nested within an existing Method.
	' 

	'''<summary>Generate an RgbColor by specifying the amount of Red, Green and Blue.</summary>
	''' 
	'''<param name="myRed">A byte (0 to 255) used to represent the Red color. Example: 0</param>
	'''<param name="myGreen">A byte (0 to 255) used to represent the Green color. Example: 255</param>
	'''<param name="myBlue">A byte (0 to 255) used to represent the Blue color. Example: 123</param>
	'''  
	'''<returns>An IRgbColor interface</returns>
	'''  
	'''<remarks></remarks>
	Public Function CreateRGBColor(myRed As System.Byte, myGreen As System.Byte, myBlue As System.Byte) As ESRI.ArcGIS.Display.IRgbColor
		Dim rgbColor As ESRI.ArcGIS.Display.IRgbColor = New ESRI.ArcGIS.Display.RgbColorClass()
		rgbColor.Red = myRed
		rgbColor.Green = myGreen
		rgbColor.Blue = myBlue
		rgbColor.UseWindowsDithering = True
		Return rgbColor
	End Function
	#End Region

  Private Sub txtBufferDistance_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBufferDistance.TextChanged

    Dim txtToCheck As String = txtBufferDistance.Text

    If ((IsDecimal(txtToCheck)) Or (IsInteger(txtToCheck))) AndAlso (txtToCheck <> "0") Then
      btnRunGP.Enabled = True
    Else
      btnRunGP.Enabled = False
    End If
  End Sub

	Private Function IsDecimal(theValue As String) As Boolean
		Try
			Convert.ToDouble(theValue)
			Return True
		Catch
			Return False
		End Try
	End Function
	'IsDecimal
	Private Function IsInteger(theValue As String) As Boolean
		Try
			Convert.ToInt32(theValue)
			Return True
		Catch
			Return False
		End Try
	End Function
	'IsInteger

	#End Region

End Class
