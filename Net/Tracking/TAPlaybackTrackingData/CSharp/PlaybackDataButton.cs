using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.IO;
using ESRI.ArcGIS.TrackingAnalyst;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ADF.Connection.Local;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.GeoDatabaseExtensions;

namespace TAPlaybackTrackingData
{

  public class PlaybackDataButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    private System.Timers.Timer m_timer = null;
    private ITemporalLayer m_temporalLayer = null;
    private bool m_bIsConnected = false;
    private IActiveView m_activeView = null;
    private ITrackingEnvironment3 m_temporalEnv = null;
    private ITemporalOperator3 m_baseTime = null;
    bool m_bIsFirst = true;
    private ITemporalOperator3 m_tempOpIncrement = null;
    private IDocumentEvents_Event m_docEvent = null;

    public PlaybackDataButton()
    {
      m_timer = new System.Timers.Timer(500);
      m_timer.Enabled = false;
      m_timer.Elapsed += new ElapsedEventHandler(OnTimer);
    }

    protected override void OnClick()
    {
      if (!m_bIsConnected)
      {
        try
        {
          //open the shapefile with the recorded data
          IFeatureClass featureClass = openPlaybackData();
          if (null == featureClass)
            return;

          //get the map container
          object mapObj = ArcMap.Application;
          
          //load the Tracking Analyst extension
          ITrackingEnvironment3 trackingEnv = setupTrackingEnv(ref mapObj);
          //set the mode to historic, since you need to do playback
          trackingEnv.DefaultTemporalReference.TemporalMode = enumTemporalMode.enumHistoric;
          //set the units of the temporal period to days
          trackingEnv.DefaultTemporalReference.TemporalPeriodUnits = enumTemporalUnits.enumDays;
          //set the update mode to manual so that it will be controlled by the application
          trackingEnv.DisplayManager.ManualUpdate = true;
          //set the temporal perspective to Aug 03 2000 7PM.
          trackingEnv.DefaultTemporalReference.TemporalPerspective = "8/3/2000 7:0:00 PM";

          // Stop using the map's time to allow the layer to draw based on it's TemporalPerspective
          ITimeData timeData = m_temporalLayer as ITimeData;
          timeData.UseTime = false;

          //create a temporal operator that will serve as a base time for the tracking environment
          ITemporalOperator3 temporalOpBaseTime = new TemporalOperatorClass() as ITemporalOperator3;
          //set the base time to 6PM, Aug 3 2000
          temporalOpBaseTime.SetDateTime(2000, 8, 3, 18, 0, 0, 0);

          //create the renderer for the temporal layer
          ITemporalRenderer temporalRenderer = setRenderer(featureClass, "DATE_TIME", "EVENTID");

          //create the temporal layer for the playback data
          m_temporalLayer = new TemporalFeatureLayerClass();
          //assign the featureclass for the layer
          ((IFeatureLayer)m_temporalLayer).FeatureClass = featureClass;
          //set the base time to initialize the time window of the layer
          m_temporalLayer.RelativeTimeOperator = (ITemporalOperator)temporalOpBaseTime;
          //set the renderer for the temporal layer 
          m_temporalLayer.Renderer = temporalRenderer as IFeatureRenderer;
          //set the flag in order to display the track of previous locations
          m_temporalLayer.DisplayOnlyLastKnownEvent = false;
          //initialize labels for the event name
          setupLayerLabels(m_temporalLayer, "EVENTID");

          m_activeView = ArcMap.Document.ActiveView;
          m_temporalEnv = trackingEnv;
          m_baseTime = temporalOpBaseTime;

          //add the temporal layer to the map
          ArcMap.Document.FocusMap.AddLayer((ILayer)m_temporalLayer);

          //enable the timer
          m_timer.Enabled = true;
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.WriteLine(ex.Message);
        }
      }
      else
      {
        //disable the timer
        m_timer.Enabled = false;

        if (null == m_temporalLayer)
          return;
        //remove the layer
        ArcMap.Document.FocusMap.DeleteLayer((ILayer)m_temporalLayer);
        m_temporalLayer = null;
      }
      m_bIsConnected = !m_bIsConnected;

      m_docEvent = ArcMap.Document as IDocumentEvents_Event;
      m_docEvent.CloseDocument += new IDocumentEvents_CloseDocumentEventHandler(docEvent_CloseDocument);

      ArcMap.Application.CurrentTool = null;
    }

    void docEvent_CloseDocument()
    {
      m_timer.Enabled = false;
    }

    private void setupLayerLabels(ITemporalLayer trackingLayer, string labelField)
    {
      //cast TrackingLayerLabels from the temporal layer
      ITrackingLayerLabels layerLabels = (ITrackingLayerLabels)trackingLayer;

      //set the labels properties
      layerLabels.LabelFieldName = labelField;
      layerLabels.LabelFeatures = true;

      // create text symbol
      ITextSymbol textSymbol = new TextSymbolClass();
      textSymbol.Color = (IColor)Converter.ToRGBColor(Color.Red);
      textSymbol.Size = 15;
      textSymbol.Font = Converter.ToStdFont(new Font(new FontFamily("Arial"), 15.0f, FontStyle.Regular));
      textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
      textSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;

      layerLabels.TextSymbol = textSymbol;
    }

    private IFeatureClass openPlaybackData()
    {
      //set the path to the featureclass
      string path = @"..\..\..\..\..\data\Time\ProjectData.gdb";
      if (!System.IO.Directory.Exists(path))
      {
        MessageBox.Show("Cannot find hurricane data:\n" + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }

      IWorkspaceFactory2 wsFactory = new FileGDBWorkspaceFactoryClass();
      IWorkspace workspace = wsFactory.OpenFromFile( path, 0 );
      IFeatureClass featureClass = ((IFeatureWorkspace)workspace).OpenFeatureClass( "atlantic_hurricanes_2000" );

      return featureClass;
    }

    private ITrackingEnvironment3 setupTrackingEnv(ref object mapObj)
    {
      IExtensionManager extentionManager = new ExtensionManagerClass();

      UID uid = new UIDClass();
      uid.Value = "esriTrackingAnalyst.TrackingEngineUtil";

      ((IExtensionManagerAdmin)extentionManager).AddExtension(uid, ref mapObj);

      ITrackingEnvironment3 trackingEnv = new TrackingEnvironmentClass();
      try
      {
        trackingEnv.Initialize(ref mapObj);
      }
      catch (Exception ex)
      {
      }
      trackingEnv.EnableTemporalDisplayManagement = true;
      return trackingEnv;
    }

    private IUniqueValueRenderer CreateUniqueValueRenderer(IFeatureClass featureClass, string fieldName)
    {
      IRgbColor color = new RgbColorClass();
      color.Red = 255;
      color.Blue = 0;
      color.Green = 0;

      ICharacterMarkerSymbol charMarkersymbol = new CharacterMarkerSymbolClass();
      charMarkersymbol.Font = Converter.ToStdFont(new Font(new FontFamily("ESRI Default Marker"), 12.0f, FontStyle.Regular));
      charMarkersymbol.CharacterIndex = 96;
      charMarkersymbol.Size = 12.0;
      charMarkersymbol.Color = (IColor)color;


      IRandomColorRamp randomColorRamp = new RandomColorRampClass();
      randomColorRamp.MinSaturation = 20;
      randomColorRamp.MaxSaturation = 40;
      randomColorRamp.MaxValue = 85;
      randomColorRamp.MaxValue = 100;
      randomColorRamp.StartHue = 75;
      randomColorRamp.EndHue = 190;
      randomColorRamp.UseSeed = true;
      randomColorRamp.Seed = 45;

      IUniqueValueRenderer uniqueRenderer = new UniqueValueRendererClass();
      uniqueRenderer.FieldCount = 1;
      uniqueRenderer.set_Field(0, fieldName);
      uniqueRenderer.DefaultSymbol = (ISymbol)charMarkersymbol;
      uniqueRenderer.UseDefaultSymbol = true;



      Random rand = new Random();
      bool bValFound = false;
      IFeatureCursor featureCursor = featureClass.Search(null, true);
      IFeature feature = null;
      string val = string.Empty;
      int fieldID = featureClass.FindField(fieldName);
      if (-1 == fieldID)
        return uniqueRenderer;

      while ((feature = featureCursor.NextFeature()) != null)
      {
        bValFound = false;
        val = Convert.ToString(feature.get_Value(fieldID));
        for (int i = 0; i < uniqueRenderer.ValueCount - 1; i++)
        {
          if (uniqueRenderer.get_Value(i) == val)
            bValFound = true;
        }

        if (!bValFound)//need to add the value to the renderer
        {
          color.Red = rand.Next(255);
          color.Blue = rand.Next(255);
          color.Green = rand.Next(255);

          charMarkersymbol = new CharacterMarkerSymbolClass();
          charMarkersymbol.Font = Converter.ToStdFont(new Font(new FontFamily("ESRI Default Marker"), 10.0f, FontStyle.Regular));
          charMarkersymbol.CharacterIndex = rand.Next(40, 118);
          charMarkersymbol.Size = 20.0;
          charMarkersymbol.Color = (IColor)color;

          //add the value to the renderer
          uniqueRenderer.AddValue(val, "name", (ISymbol)charMarkersymbol);
        }
      }

      //release the featurecursor
      ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor);

      return uniqueRenderer;
    }

    private IUniqueValueRenderer CreateTrackUniqueValueRenderer(IFeatureClass featureClass, string fieldName)
    {
      IRgbColor color = new RgbColorClass();
      color.Red = 0;
      color.Blue = 0;
      color.Green = 255;

      ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
      simpleLineSymbol.Color = (IColor)color;
      simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
      simpleLineSymbol.Width = 1.0;

      IUniqueValueRenderer uniqueRenderer = new UniqueValueRendererClass();
      uniqueRenderer.FieldCount = 1;
      uniqueRenderer.set_Field(0, fieldName);
      uniqueRenderer.DefaultSymbol = (ISymbol)simpleLineSymbol;
      uniqueRenderer.UseDefaultSymbol = true;

      Random rand = new Random();
      bool bValFound = false;
      IFeatureCursor featureCursor = featureClass.Search(null, true);
      IFeature feature = null;
      string val = string.Empty;
      int fieldID = featureClass.FindField(fieldName);
      if (-1 == fieldID)
        return uniqueRenderer;

      while ((feature = featureCursor.NextFeature()) != null)
      {
        bValFound = false;
        val = Convert.ToString(feature.get_Value(fieldID));
        for (int i = 0; i < uniqueRenderer.ValueCount - 1; i++)
        {
          if (uniqueRenderer.get_Value(i) == val)
            bValFound = true;
        }

        if (!bValFound)//need to add the value to the renderer
        {
          color.Red = rand.Next(255);
          color.Blue = rand.Next(255);
          color.Green = rand.Next(255);

          simpleLineSymbol = new SimpleLineSymbolClass();
          simpleLineSymbol.Color = (IColor)color;
          simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
          simpleLineSymbol.Width = 1.0;

          //add the value to the renderer
          uniqueRenderer.AddValue(val, "name", (ISymbol)simpleLineSymbol);
        }
      }

      //release the featurecursor
      ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor);

      return uniqueRenderer;
    }
    
    private ITemporalRenderer setRenderer(IFeatureClass featureClass, string temporalField, string eventName)
    {
      CoTrackSymbologyRenderer trackingRenderer = new CoTrackSymbologyRendererClass();
      ITemporalRenderer temporalRenderer = (ITemporalRenderer)trackingRenderer;
      temporalRenderer.TemporalFieldName = temporalField;
      temporalRenderer.TemporalObjectColumnName = eventName;
      temporalRenderer.TimeSymbologyMethod = enumTemporalSymbolizationMethod.enumColor;

      //this is a desktop only code which requires assemblies CartoUI and Framework
      //IRendererPropertyPage rendererPropPage = new UniqueValuePropertyPageClass();

      //enable the most current renderer
      IUniqueValueRenderer uniqueValrenderer = CreateUniqueValueRenderer(featureClass, eventName);
      if (null != uniqueValrenderer)
      {
        ((ITemporalRenderer2)temporalRenderer).MostCurrentRenderer = (IFeatureRenderer)uniqueValrenderer;
        ((ITemporalRenderer2)temporalRenderer).MostCurrentRendererEnabled = true;

        //this is a desktop only code which requires assemblies CartoUI and Framework
        //((ITemporalRenderer2)temporalRenderer).PropPageMostCurrentRenderer = rendererPropPage.ClassID;
      }

      //set the track renderer
      uniqueValrenderer = CreateTrackUniqueValueRenderer(featureClass, eventName);
      if (null != uniqueValrenderer)
      {
        ITrackSymbologyRenderer trackSymbolenderer = trackingRenderer as ITrackSymbologyRenderer;
        trackSymbolenderer.TrackSymbologyRenderer = (IFeatureRenderer)uniqueValrenderer;
        ((ITemporalRenderer2)temporalRenderer).TrackRendererEnabled = true;
        ((ITemporalRenderer2)temporalRenderer).SmoothTracks = true;

        //this is a desktop only code which requires assemblies CartoUI and Framework
        //((ITemporalRenderer2)temporalRenderer).PropPageTrackRenderer = rendererPropPage.ClassID;
      }

      return temporalRenderer;
    }    

    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    private void OnTimer(object sender, ElapsedEventArgs e)
    {
      //increment the time-stamp of the tracking environment in order to do the playback
      OnIncrement();
    }

    private void OnIncrement()
    {
      if (m_bIsFirst)
      {
        //create the temporal increment object
        m_tempOpIncrement = new TemporalOperatorClass();
        m_tempOpIncrement.SetInterval(6.0, enumTemporalOperatorUnits.enumTemporalOperatorHours);

        m_bIsFirst = false;
      }

      if (null == m_baseTime)
        return;

      //increment the base time to match the 'current' time
      m_baseTime.Add((ITemporalOperator)m_tempOpIncrement);

      string date = m_baseTime.get_AsString("%c");
      System.Diagnostics.Trace.WriteLine(date);

      //increment the timestamp
      m_temporalEnv.DefaultTemporalReference.TemporalPerspective = (object)date;

      //refresh the display
      m_activeView.Refresh();
      // For better performance, the following line can be used instead of the one above to
      // do a partial refresh of the screen instead of refreshing the whole display.
      //m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_temporalLayer, new EnvelopeClass());
    }
  }

}
