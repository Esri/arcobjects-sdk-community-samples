## Temporal statistics

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates the following:</div>

*   Initializing the Tracking Analyst in an ArcGIS Engine application
*   Retrieving all Tracking Analyst temporal layers on the map
*   Using ITemporalWorkspaceStatistics to retrieve metrics about all tracking connections open on the map
*   Using ITemporalFeatureClassStatistics to retrieve metrics on specific tracking services  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Tracking
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open the .NET solution file and build the project.  
1. Run the TemporalStatistics application.  
1. Use the Open Document command on the Toolbar control to browse to a pre-authored map document to load into the MapControl. The pre-authored map document should include one or many tracking services that shows.   
1. Use the Tracking Services combo box to select the statistics shown.  
1. Click the Refresh button to apply any configuration changes to the statistics display, such as, the sampling size or refresh rate.  







#### See Also  
[Sample: Adding a real-time feed to ArcMap](../../../Net/Tracking/Samples/TAAddRealTimeTemporalLayer)  
[Updating the purge rule on a real-time temporal layer](http://desktop.arcgis.com/search/?q=Updating%20the%20purge%20rule%20on%20a%20real-time%20temporal%20layer&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine: Tracking Analyst |  
|  | ArcGIS Desktop Advanced: Tracking Analyst |  
|  | ArcGIS Desktop Standard: Tracking Analyst |  
|  | ArcGIS Desktop Basic: Tracking Analyst |  


