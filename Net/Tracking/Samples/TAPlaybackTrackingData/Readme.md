## Play back tracking data

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates using the ArcGIS Tracking Analyst extension to play back tracking data. The tracking data can be any type of a feature class that has time stamp information (such as date and time). In this sample, you learn how to initialize the Tracking Analyst extension and the Tracking Analyst tracking environment. This sample covers the following:</div>

*   Opening the tracking data feature class
*   Creating the renderers used by the temporal layer to display temporal data
*   Loading the Tracking Analyst extension
*   Initializing the tracking environment
*   Assigning temporal operators to the tracking environment to display temporal data
*   Incrementing a temporal operator
*   Changing the tracking environment temporal perspective time stamp   


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
1. Open the solution in Visual Studio.  
1. Build the solution to make the play back tracking data Add-in's dynamic-link library (DLL) file.  
1. Add the Add-in's button to a toolbar in ArcMap.  
1. Click the Playback Tracking data button to start the play back of the hurricane data. The data gets updated every one-half second and increases by six hour increments.   







#### See Also  
[How to connect to ArcGIS Tracking Server](http://desktop.arcgis.com/search/?q=How%20to%20connect%20to%20ArcGIS%20Tracking%20Server&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Tracking Analyst | ArcGIS Desktop Basic: Tracking Analyst |  
| ArcGIS Desktop Standard: Tracking Analyst | ArcGIS Desktop Standard: Tracking Analyst |  
| ArcGIS Desktop Advanced: Tracking Analyst | ArcGIS Desktop Advanced: Tracking Analyst |  


