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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.TrackingAnalyst
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Display

Public Class AddTemporalLayerButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button
  Private m_bInitialized As Boolean = False

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    setupTrackingEnv()
    'Open the year 2000 hurricanes shapefile
    Dim featureClass As IFeatureClass = openTemporalData()

    'Create and add a Temporal Layer to the map
    AddTemporalLayer(featureClass, "EVENTID", "DATE_TIME")

    'Turn on Dynamic Display
    EnableDynamicDisplay()

    My.ArcMap.Application.CurrentTool = Nothing
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub

  'Initialize the Tracking Environment, you only need to do this once
  Private Sub setupTrackingEnv()
    'get the map container
    Dim mapObj As Object = My.ArcMap.Application

    If (Not m_bInitialized) AndAlso Not mapObj Is Nothing Then
      Dim extentionManager As IExtensionManager = New ExtensionManagerClass()

      Dim uid As UID = New UIDClass()
      uid.Value = "esriTrackingAnalyst.TrackingEngineUtil"

      CType(extentionManager, IExtensionManagerAdmin).AddExtension(uid, mapObj)

      Dim trackingEnv As ITrackingEnvironment3 = New TrackingEnvironmentClass()
      trackingEnv.Initialize(mapObj)
      trackingEnv.EnableTemporalDisplayManagement = True
      m_bInitialized = True
    End If
  End Sub

  ''' <summary>
  ''' Turns Dynamic Display On.
  ''' </summary>
  Private Sub EnableDynamicDisplay()
    Dim dynamicMap As IDynamicMap = TryCast(My.ArcMap.Document.FocusMap, IDynamicMap)

    If Not dynamicMap Is Nothing Then
      dynamicMap.DynamicMapEnabled = True
    End If

  End Sub

  ''' <summary>
  ''' Opens a feature class from a shapefile stored on disk.
  ''' </summary>
  ''' <returns>The opened feature class</returns>
  Private Function openTemporalData() As IFeatureClass

    'set the path to the featureclass
    Dim path As String = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\Time\ProjectData.gdb")
    If (Not System.IO.Directory.Exists(path)) Then
      MessageBox.Show("Cannot find hurricane data:" & Constants.vbLf & path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End If

    Dim wsFactory As IWorkspaceFactory2 = New FileGDBWorkspaceFactoryClass()
    Dim workspace As IWorkspace = wsFactory.OpenFromFile(path, 0)
    Dim featureClass As IFeatureClass = (CType(workspace, IFeatureWorkspace)).OpenFeatureClass("atlantic_hurricanes_2000")

    Return featureClass
  End Function

  ''' <summary>
  ''' Creates a Temporal Layer using the specified feature class and add it to the map.
  ''' </summary>
  ''' <param name="featureClass">The feature class to use for the temporal layer.</param>
  ''' <param name="eventFieldName">Indicates the feature class column that identifies or groups temporal observations with time series.</param>
  ''' <param name="temporalFieldName">Identifies the temporal field, which must be a field type whose data can be converted to a date value.</param>
  Private Sub AddTemporalLayer(ByVal featureClass As IFeatureClass, ByVal eventFieldName As String, ByVal temporalFieldName As String)
    Dim temporalFeatureLayer As ITemporalLayer = New TemporalFeatureLayerClass()
    Dim featureLayer As IFeatureLayer2 = TryCast(temporalFeatureLayer, IFeatureLayer2)
    Dim layer As ILayer = TryCast(temporalFeatureLayer, ILayer)
    Dim temporalRenderer As ITemporalRenderer = New CoTrackSymbologyRendererClass()
    Dim temporalRenderer2 As ITemporalRenderer2 = CType(temporalRenderer, ITemporalRenderer2)
    Dim featureRenderer As IFeatureRenderer = TryCast(temporalRenderer, IFeatureRenderer)
    Dim trackRenderer As ITrackSymbologyRenderer = TryCast(temporalRenderer, ITrackSymbologyRenderer)

    If Not featureLayer Is Nothing Then
      featureLayer.FeatureClass = featureClass
    End If

    If Not featureRenderer Is Nothing Then
      temporalRenderer.TemporalObjectColumnName = eventFieldName
      temporalRenderer.TemporalFieldName = temporalFieldName
      temporalFeatureLayer.Renderer = featureRenderer
    End If

    If Not trackRenderer Is Nothing Then
      'Create green color value
      Dim rgbColor As IRgbColor = New RgbColorClass()
      rgbColor.RGB = &HFF00

      'Create simple thin green line 
      Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
      simpleLineSymbol.Color = CType(rgbColor, IColor)
      simpleLineSymbol.Width = 1.0

      'Create simple renderer using line symbol
      Dim simpleRenderer As ISimpleRenderer = New SimpleRendererClass()
      simpleRenderer.Symbol = CType(simpleLineSymbol, ISymbol)

      'Apply line renderer as track symbol and enable track rendering
      trackRenderer.TrackSymbologyRenderer = CType(simpleRenderer, IFeatureRenderer)
      trackRenderer.ShowTrackSymbologyLegendGroup = True
      temporalRenderer2.TrackRendererEnabled = True
    End If

    If Not layer Is Nothing Then
      My.ArcMap.Document.FocusMap.AddLayer(layer)
    End If
  End Sub

End Class
