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
Imports System
Imports System.Drawing
Imports System.Timers
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.Connection.Local
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.GeoDatabaseExtensions
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.TrackingAnalyst

Public Class PlaybackDataButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button
  Private m_timer As System.Timers.Timer = Nothing
  Private m_temporalLayer As ITemporalLayer = Nothing
  Private m_bIsConnected As Boolean = False

  Private m_tempOpIncrement As ITemporalOperator3 = Nothing
  Private m_baseTime As ITemporalOperator3 = Nothing
  Private m_temporalEnv As ITrackingEnvironment3 = Nothing
  Private m_bIsFirst As Boolean = True


  Public Sub New()

    m_timer = New System.Timers.Timer(500)
    m_timer.Enabled = False
    AddHandler m_timer.Elapsed, AddressOf OnTimer

  End Sub

  Protected Overrides Sub OnClick()
    If (Not m_bIsConnected) Then
      Try
        'open the shapefile with the recorded data
        Dim featureClass As IFeatureClass = openPlaybackData()
        If Nothing Is featureClass Then
          Return
        End If

        'get the map container
        Dim mapObj As Object = My.ArcMap.Application

        'load the Tracking Analyst extension
        m_temporalEnv = setupTrackingEnv(mapObj)
        'set the mode to historic, since you need to do playback
        m_temporalEnv.DefaultTemporalReference.TemporalMode = enumTemporalMode.enumHistoric
        'set the units of the temporal period to days
        m_temporalEnv.DefaultTemporalReference.TemporalPeriodUnits = enumTemporalUnits.enumDays
        'set the update mode to manual so that it will be controlled by the application
        m_temporalEnv.DisplayManager.ManualUpdate = True
        'set the temporal perspective to Aug 03 2000 7PM.
        m_temporalEnv.DefaultTemporalReference.TemporalPerspective = "8/3/2000 7:0:00 PM"

        ' Stop using the map's time to allow the layer to draw based on it's TemporalPerspective
        CType(m_temporalLayer, ITimeData).UseTime = False

        'create a temporal operator that will serve as a base time for the tracking environment
        m_baseTime = New TemporalOperatorClass()
        'set the base time to 6PM, Aug 3 2000
        m_baseTime.SetDateTime(2000, 8, 3, 18, 0, 0, 0)

        'create the renderer for the temporal layer
        Dim temporalRenderer As ITemporalRenderer = setRenderer(featureClass, "DATE_TIME", "EVENTID")

        'create the temporal layer for the playback data
        m_temporalLayer = New TemporalFeatureLayerClass()
        'assign the featureclass for the layer
        CType(m_temporalLayer, IFeatureLayer).FeatureClass = featureClass
        'set the base time to initialize the time window of the layer
        m_temporalLayer.RelativeTimeOperator = CType(m_baseTime, ITemporalOperator)
        'set the renderer for the temporal layer 
        m_temporalLayer.Renderer = TryCast(temporalRenderer, IFeatureRenderer)
        'set the flag in order to display the track of previous locations
        m_temporalLayer.DisplayOnlyLastKnownEvent = False
                'initialize labels for the event name
        setupLayerLabels(m_temporalLayer, "EVENTID")

        'add the temporal layer to the map
        My.ArcMap.Document.FocusMap.AddLayer(CType(m_temporalLayer, ILayer))

        'enable the timer
        m_timer.Enabled = True
      Catch ex As Exception
        System.Diagnostics.Trace.WriteLine(ex.Message)
      End Try
    Else
      'disable the timer
      m_timer.Enabled = False

      If Nothing Is m_temporalLayer Then
        Return
      End If
            'remove the layer
      My.ArcMap.Document.FocusMap.DeleteLayer(CType(m_temporalLayer, ILayer))
      m_temporalLayer = Nothing
    End If
    m_bIsConnected = Not m_bIsConnected

    My.ArcMap.Application.CurrentTool = Nothing
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub

  Private Sub OnTimer(ByVal sender As Object, ByVal e As ElapsedEventArgs)
    OnIncrement()
  End Sub


  Private Sub OnIncrement()
    If m_bIsFirst Then
      'create the temporal increment object
      m_tempOpIncrement = New TemporalOperatorClass()
      m_tempOpIncrement.SetInterval(6.0, enumTemporalOperatorUnits.enumTemporalOperatorHours)

      m_bIsFirst = False
    End If

    If Nothing Is m_baseTime Then
      Return
    End If

    'increment the base time to match the 'current' time
    m_baseTime.Add(CType(m_tempOpIncrement, ITemporalOperator))

    Dim d As String = m_baseTime.AsString("%c")
    System.Diagnostics.Trace.WriteLine(d)

    'increment the timestamp
    m_temporalEnv.DefaultTemporalReference.TemporalPerspective = d

    'refresh the display
    My.ArcMap.Document.ActiveView.Refresh()
    'For improved performance, the line above can be replaces with the line above this comment.  The
    'lower line of code will only refresh the part of the screen that has changed.
    'My.ArcMap.Document.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_temporalLayer, Nothing)
  End Sub


  ''' <summary>
  ''' create the renderer used to draw the tracks
  ''' </summary>
  ''' <param name="featureClass"></param>
  ''' <param name="fieldName"></param>
  ''' <returns></returns>
  Private Function CreateTrackUniqueValueRenderer(ByVal featureClass As IFeatureClass, ByVal fieldName As String) As IUniqueValueRenderer
    Dim color As IRgbColor = New RgbColorClass()
    color.Red = 0
    color.Blue = 0
    color.Green = 255

    Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
    simpleLineSymbol.Color = CType(color, IColor)
    simpleLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
    simpleLineSymbol.Width = 1.0

    Dim uniqueRenderer As IUniqueValueRenderer = New UniqueValueRendererClass()
    uniqueRenderer.FieldCount = 1
    uniqueRenderer.Field(0) = fieldName
    uniqueRenderer.DefaultSymbol = CType(simpleLineSymbol, ISymbol)
    uniqueRenderer.UseDefaultSymbol = True

    Dim rand As Random = New Random()
    Dim bValFound As Boolean = False
    Dim featureCursor As IFeatureCursor = featureClass.Search(Nothing, True)
    Dim feature As IFeature = Nothing
    Dim val As String = String.Empty
    Dim fieldID As Integer = featureClass.FindField(fieldName)
    If -1 = fieldID Then
      Return uniqueRenderer
    End If

    feature = featureCursor.NextFeature()
    Do While Not feature Is Nothing
      bValFound = False
      val = Convert.ToString(feature.Value(fieldID))
      Dim i As Integer = 0
      Do While i < uniqueRenderer.ValueCount - 1
        If uniqueRenderer.Value(i) = val Then
          bValFound = True
        End If
        i += 1
      Loop

      If (Not bValFound) Then 'need to add the value to the renderer
        color.Red = rand.Next(255)
        color.Blue = rand.Next(255)
        color.Green = rand.Next(255)

        simpleLineSymbol = New SimpleLineSymbolClass()
        simpleLineSymbol.Color = CType(color, IColor)
        simpleLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid
        simpleLineSymbol.Width = 1.0

        'add the value to the renderer
        uniqueRenderer.AddValue(val, "EVENTID", CType(simpleLineSymbol, ISymbol))
      End If

      feature = featureCursor.NextFeature()
    Loop

    'release the featurecursor
    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor)
    'ESRI.ArcGIS.ADF.BaseClasses.ComReleaser.ReleaseCOMObject(featureCursor)

    Return uniqueRenderer

  End Function

  Private Function CreateUniqueValueRenderer(ByVal featureClass As IFeatureClass, ByVal fieldName As String) As IUniqueValueRenderer
    Dim color As IRgbColor = New RgbColorClass()
    color.Red = 255
    color.Blue = 0
    color.Green = 0

    Dim charMarkersymbol As ICharacterMarkerSymbol = New CharacterMarkerSymbolClass()
    charMarkersymbol.Font = Converter.ToStdFont(New Font(New FontFamily("ESRI Default Marker"), 12.0F, FontStyle.Regular))
    charMarkersymbol.CharacterIndex = 96
    charMarkersymbol.Size = 12.0
    charMarkersymbol.Color = CType(color, IColor)


    Dim randomColorRamp As IRandomColorRamp = New RandomColorRampClass()
    randomColorRamp.MinSaturation = 20
    randomColorRamp.MaxSaturation = 40
    randomColorRamp.MaxValue = 85
    randomColorRamp.MaxValue = 100
    randomColorRamp.StartHue = 75
    randomColorRamp.EndHue = 190
    randomColorRamp.UseSeed = True
    randomColorRamp.Seed = 45

    Dim uniqueRenderer As IUniqueValueRenderer = New UniqueValueRendererClass()
    uniqueRenderer.FieldCount = 1
    uniqueRenderer.Field(0) = fieldName
    uniqueRenderer.DefaultSymbol = CType(charMarkersymbol, ISymbol)
    uniqueRenderer.UseDefaultSymbol = True



    Dim rand As Random = New Random()
    Dim bValFound As Boolean = False
    Dim featureCursor As IFeatureCursor = featureClass.Search(Nothing, True)
    Dim feature As IFeature = Nothing
    Dim val As String = String.Empty
    Dim fieldID As Integer = featureClass.FindField(fieldName)
    If -1 = fieldID Then
      Return uniqueRenderer
    End If

    feature = featureCursor.NextFeature()
    Do While Not feature Is Nothing
      bValFound = False
      val = Convert.ToString(feature.Value(fieldID))
      Dim i As Integer = 0
      Do While i < uniqueRenderer.ValueCount - 1
        If uniqueRenderer.Value(i) = val Then
          bValFound = True
        End If
        i += 1
      Loop

      If (Not bValFound) Then 'need to add the value to the renderer
        color.Red = rand.Next(255)
        color.Blue = rand.Next(255)
        color.Green = rand.Next(255)

        charMarkersymbol = New CharacterMarkerSymbolClass()
        charMarkersymbol.Font = Converter.ToStdFont(New Font(New FontFamily("ESRI Default Marker"), 10.0F, FontStyle.Regular))
        charMarkersymbol.CharacterIndex = rand.Next(40, 118)
        charMarkersymbol.Size = 20.0
        charMarkersymbol.Color = CType(color, IColor)

        'add the value to the renderer
        uniqueRenderer.AddValue(val, "name", CType(charMarkersymbol, ISymbol))
      End If

      feature = featureCursor.NextFeature()
    Loop

    'release the featurecursor
    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor)

    Return uniqueRenderer
  End Function

  Private Sub setupLayerLabels(ByVal trackingLayer As ITemporalLayer, ByVal labelField As String)
    'cast TrackingLayerLabels from the temporal layer
    Dim layerLabels As ITrackingLayerLabels = CType(trackingLayer, ITrackingLayerLabels)

    'set the labels properties
    layerLabels.LabelFieldName = labelField
    layerLabels.LabelFeatures = True

    ' create text symbol
    Dim textSymbol As ITextSymbol = New TextSymbolClass()
    textSymbol.Color = CType(Converter.ToRGBColor(Color.Red), IColor)
    'textSymbol.Color = (IColor)color;
    textSymbol.Size = 15
    textSymbol.Font = Converter.ToStdFont(New Font(New FontFamily("Arial"), 15.0F, FontStyle.Regular))
    textSymbol.HorizontalAlignment = ESRI.ArcGIS.Display.esriTextHorizontalAlignment.esriTHARight
    textSymbol.VerticalAlignment = ESRI.ArcGIS.Display.esriTextVerticalAlignment.esriTVABaseline

    layerLabels.TextSymbol = textSymbol
  End Sub

  Private Function setupTrackingEnv(ByRef mapObj As Object) As ITrackingEnvironment3
    Dim extentionManager As IExtensionManager = New ExtensionManagerClass()

    Dim uid As UID = New UIDClass()
    uid.Value = "esriTrackingAnalyst.TrackingEngineUtil"

    CType(extentionManager, IExtensionManagerAdmin).AddExtension(uid, mapObj)

    Dim trackingEnv As ITrackingEnvironment3 = New TrackingEnvironmentClass()
    trackingEnv.Initialize(mapObj)
    trackingEnv.EnableTemporalDisplayManagement = True
    Return trackingEnv
  End Function

  Private Function openPlaybackData() As IFeatureClass
        'set the path to the featureclass in the sample data
    Dim path As String = "..\..\..\..\..\data\Time\ProjectData.gdb"
    If (Not System.IO.Directory.Exists(path)) Then
      MessageBox.Show("Cannot find hurricane data:" & Constants.vbLf & path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return Nothing
    End If

    Dim wsFactory As IWorkspaceFactory2 = New FileGDBWorkspaceFactoryClass()
    Dim workspace As IWorkspace = wsFactory.OpenFromFile(path, 0)
    Dim featureClass As IFeatureClass = (CType(workspace, IFeatureWorkspace)).OpenFeatureClass("atlantic_hurricanes_2000")

    Return featureClass
  End Function

  Private Function setRenderer(ByVal featureClass As IFeatureClass, ByVal temporalField As String, ByVal eventName As String) As ITemporalRenderer
    Dim trackingRenderer As CoTrackSymbologyRenderer = New CoTrackSymbologyRendererClass()
    Dim temporalRenderer As ITemporalRenderer = CType(trackingRenderer, ITemporalRenderer)
    temporalRenderer.TemporalFieldName = temporalField
    temporalRenderer.TemporalObjectColumnName = eventName
    temporalRenderer.TimeSymbologyMethod = enumTemporalSymbolizationMethod.enumColor

    'this is a desktop only code which requires assemblies CartoUI and Framework
    'IRendererPropertyPage rendererPropPage = new UniqueValuePropertyPageClass();

    'enable the most current renderer
    Dim uniqueValrenderer As IUniqueValueRenderer = CreateUniqueValueRenderer(featureClass, eventName)
    If Not Nothing Is uniqueValrenderer Then
      CType(temporalRenderer, ITemporalRenderer2).MostCurrentRenderer = CType(uniqueValrenderer, IFeatureRenderer)
      CType(temporalRenderer, ITemporalRenderer2).MostCurrentRendererEnabled = True

      'this is a desktop only code which requires assemblies CartoUI and Framework
      '((ITemporalRenderer2)temporalRenderer).PropPageMostCurrentRenderer = rendererPropPage.ClassID;
    End If

    'set the track renderer
    uniqueValrenderer = CreateTrackUniqueValueRenderer(featureClass, eventName)
    If Not Nothing Is uniqueValrenderer Then
      Dim trackSymbolenderer As ITrackSymbologyRenderer = TryCast(trackingRenderer, ITrackSymbologyRenderer)
      trackSymbolenderer.TrackSymbologyRenderer = CType(uniqueValrenderer, IFeatureRenderer)
      CType(temporalRenderer, ITemporalRenderer2).TrackRendererEnabled = True
      CType(temporalRenderer, ITemporalRenderer2).SmoothTracks = True

      'this is a desktop only code which requires assemblies CartoUI and Framework
      '((ITemporalRenderer2)temporalRenderer).PropPageTrackRenderer = rendererPropPage.ClassID;
    End If

    Return temporalRenderer
  End Function

End Class
