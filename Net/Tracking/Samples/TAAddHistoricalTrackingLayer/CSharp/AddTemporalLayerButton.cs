/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.TrackingAnalyst;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;

namespace TAAddHistoricalTrackingLayer
{
  public class AddTemporalLayerButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {

    private bool m_bInitialized = false;

    public AddTemporalLayerButton()
    {
    }

    protected override void OnClick()
    {
      setupTrackingEnv();
      //Open the year 2000 hurricanes shapefile
      IFeatureClass featureClass = openTemporalData();

      //Create and add a Temporal Layer to the map
      AddTemporalLayer(featureClass, "EVENTID", "Date_Time");

      //Turn on Dynamic Display
      EnableDynamicDisplay();

      ArcMap.Application.CurrentTool = null;
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    //Initialize the Tracking Environment, you only need to do this once
    private void setupTrackingEnv()
    {
      if (!m_bInitialized && ArcMap.Application != null)
      {
        IExtensionManager extentionManager = new ExtensionManagerClass();

        UID uid = new UIDClass();
        uid.Value = "esriTrackingAnalyst.TrackingEngineUtil";
        object mapRef = ArcMap.Application;

        ((IExtensionManagerAdmin)extentionManager).AddExtension(uid, ref mapRef);

        ITrackingEnvironment3 trackingEnv = new TrackingEnvironmentClass();
        try
        {
          trackingEnv.Initialize(ref mapRef);
        }
        catch (Exception ex)
        {
        }
        trackingEnv.EnableTemporalDisplayManagement = true;
     
        m_bInitialized = true;
      }
    }
        /// <summary>
        /// Turns Dynamic Display On.
        /// </summary>
        private void EnableDynamicDisplay()
        {
          
            IDynamicMap dynamicMap = ArcMap.Document.FocusMap as IDynamicMap;

            if (dynamicMap != null)
            {
                dynamicMap.DynamicMapEnabled = true;
            }

        }

    /// <summary>
    /// Opens a feature class from a shapefile stored on disk.
    /// </summary>
    /// <returns>The opened feature class</returns>
    private IFeatureClass openTemporalData()
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

        /// <summary>
        /// Creates a Temporal Layer using the specified feature class and add it to the map.
        /// </summary>
        /// <param name="featureClass">The feature class to use for the temporal layer.</param>
        /// <param name="eventFieldName">Indicates the feature class column that identifies or groups temporal observations with time series.</param>
        /// <param name="temporalFieldName">Identifies the temporal field, which must be a field type whose data can be converted to a date value.</param>
        private void AddTemporalLayer(IFeatureClass featureClass, string eventFieldName, string temporalFieldName)
        {
            ITemporalLayer temporalFeatureLayer = new TemporalFeatureLayerClass();
            IFeatureLayer2 featureLayer = temporalFeatureLayer as IFeatureLayer2;
            ILayer layer = temporalFeatureLayer as ILayer;
            ITemporalRenderer temporalRenderer = new CoTrackSymbologyRendererClass();
			ITemporalRenderer2 temporalRenderer2 = (ITemporalRenderer2)temporalRenderer;
            IFeatureRenderer featureRenderer = temporalRenderer as IFeatureRenderer;
			ITrackSymbologyRenderer trackRenderer = temporalRenderer as ITrackSymbologyRenderer;

            if (featureLayer != null)
            {
                featureLayer.FeatureClass = featureClass;
            }

            if (featureRenderer != null)
            {
                temporalRenderer.TemporalObjectColumnName = eventFieldName;
                temporalRenderer.TemporalFieldName = temporalFieldName;
                temporalFeatureLayer.Renderer = featureRenderer;
            }

			if (trackRenderer != null)
			{
				//Create green color value
				IRgbColor rgbColor = new RgbColorClass();
				rgbColor.RGB = 0x00FF00;

				//Create simple thin green line 
				ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
				simpleLineSymbol.Color = (IColor)rgbColor;
				simpleLineSymbol.Width = 1.0;

				//Create simple renderer using line symbol
				ISimpleRenderer simpleRenderer = new SimpleRendererClass();
				simpleRenderer.Symbol = (ISymbol)simpleLineSymbol;

				//Apply line renderer as track symbol and enable track rendering
				trackRenderer.TrackSymbologyRenderer = (IFeatureRenderer)simpleRenderer;
				trackRenderer.ShowTrackSymbologyLegendGroup = true;
				temporalRenderer2.TrackRendererEnabled = true;
			}

            if (layer != null)
            {
              ArcMap.Document.FocusMap.AddLayer(layer);
            }
        }
    }
}

