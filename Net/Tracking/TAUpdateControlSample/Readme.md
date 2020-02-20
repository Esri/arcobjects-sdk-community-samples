## Using the ITAControlUpdate interface

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates using the ITAUpdateControl interface of the Tracking Analyst Display Manager. This interface is used to control the refresh rates and thresholds when working with real-time temporal data on a map.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The settings for ITAUpdateControl apply to all tracking layers on the map. It is not possible to have different update strategies for multiple tracking layers. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Tracking
Organization:          Esri, http://www.esri.com
Date:                  12/13/2018
ArcObjects SDK:        10.7
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and build the .NET solution.  
1. Add the command to ArcMap or an existing MapControl application.  
1. Open a document that has at least one real-time temporal layer or add a real-time layer to your display.  
1. Click the command to show a dialog box to use ITAUpdateControl.  
1. Update settings to observe how the display is affected.  
1. Click Apply to save the settings.  
1. Click Refresh to manually refresh the map using the selected refresh method.  
1. Click the Help button on the TAUpdateControl settings dialog box to see a description of all the settings.  







#### See Also  
[Sample: Adding a real-time feed to ArcMap](../../../Net/Tracking/Samples/TAAddRealTimeTemporalLayer)  
[Updating the purge rule on a real-time temporal layer](http://desktop.arcgis.com/search/?q=Updating%20the%20purge%20rule%20on%20a%20real-time%20temporal%20layer&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine: Tracking Analyst |  
| ArcGIS Desktop Basic: Tracking Analyst | ArcGIS Desktop Basic: Tracking Analyst |  
| ArcGIS Desktop Standard: Tracking Analyst | ArcGIS Desktop Standard: Tracking Analyst |  
| ArcGIS Desktop Advanced: Tracking Analyst | ArcGIS Desktop Advanced: Tracking Analyst |  


