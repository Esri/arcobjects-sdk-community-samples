using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.TrackingAnalyst;

namespace TAAddRealTimeTemporalLayer
{
  public class AddTrackingServerLayerButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    private const string TS_SERVER_NAME = "hound"; //computer name of the Tracking Server
    private const string TS_SERVICE_NAME = "SanDiegoTaxis"; //name of the Tracking Service to open

    public AddTrackingServerLayerButton()
    {
    }

    protected override void OnClick()
    {
      //get the map container
      object mapObj = ArcMap.Application;
      
      //load the Tracking Analyst extension
      ITrackingEnvironment trackingEnv = setupTrackingEnv(ref mapObj);

      //Create the temporal layer and add it to the display
      ILayer temporalLayer = CreateTemporalLayer();

      ArcMap.Document.FocusMap.AddLayer(temporalLayer);
      
      ArcMap.Application.CurrentTool = null;
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    //Create a new temporal layer with default symbology
    private ILayer CreateTemporalLayer()
    {
      IFeatureLayer featureLayer = new TemporalFeatureLayerClass();
      IFeatureClass featureClass = OpenTrackingServerConnection();

      featureLayer.FeatureClass = featureClass;

      return featureLayer as ILayer;
    }

    private IFeatureClass OpenTrackingServerConnection()
    {
      IWorkspaceFactory amsWorkspaceFactory = new AMSWorkspaceFactory();

      IPropertySet connectionProperties = CreateTrackingServerConnectionProperties();
      IAMSWorkspace amsWorkspace = amsWorkspaceFactory.Open(connectionProperties, 0) as IAMSWorkspace;
      IFeatureClass featureClass = amsWorkspace.OpenFeatureClass(TS_SERVICE_NAME);

      return featureClass;
    }

    //Create connection property set for Tracking Server
    private IPropertySet CreateTrackingServerConnectionProperties()
    {
      IPropertySet connectionProperties = new PropertySetClass();

      connectionProperties.SetProperty("SERVERNAME", TS_SERVER_NAME);
      connectionProperties.SetProperty("AMS_CONNECTION_NAME", "Sample TS Connection");
      //This is the standard AMS connection editor, this would only be different if you wrote your own connector
      connectionProperties.SetProperty("AMS_CONNECTOR_EDITOR", "{1C6BA545-2F59-11D5-B7E2-00010265ADC5}");
      //This is the standard AMS connector, this would only be different if you wrote your own connector
      connectionProperties.SetProperty("AMS_CONNECTOR", "{F6FC70F5-5778-11D6-B841-00010265ADC5}");
      connectionProperties.SetProperty("AMS_USER_NAME", "");
      connectionProperties.SetProperty("TMS_USER_PWD", "");

      return connectionProperties;
    }

    //Initialize the Tracking Environment, you only need to do this once
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

  }

}
