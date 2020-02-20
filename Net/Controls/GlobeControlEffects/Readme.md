## Effects in the GlobeControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates how to manipulate heads-up display (HUD), north arrow, observer location (latitude, longitude, and altitude), enabling, setting sun, and ambient light effects. This sample shows how to do the following:</div>

*   Use the IGlobeViewer interface NorthArrowEnabled and HUDEnabled properties to enable north arrow and heads-up display.
*   Use the IGlobeDisplayRendering interface GetSunColor, SetSunColor, GetSunPosition, and SetSunPosition methods and AmbientLight property to get and set the sun color, sun position, and ambient light.
*   Use the IGlobeCamera.GetObserverLatLonAlt method to get observer latitude, longitude, and altitude.
*   Listen to an AfterDraw event of IGlobeDisplayEvents.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. If no default document is loaded, open a globe document.  
1. Select the Enable Sun check box to enable and disable the sun effect and click the Set Sun Color button to set the desired color of the sun.   
1. Select the HUD check boxes to enable and disable the built-in heads-up display; the Alternate HUD area shows how to make a heads-up display.   



![Screen shot of the HUD options used in this sample.](images/pic1.png)  
Screen shot of the HUD options used in this sample.  




#### See Also  
[GlobeControl class](http://desktop.arcgis.com/search/?q=GlobeControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IGlobeControl interface](http://desktop.arcgis.com/search/?q=IGlobeControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine: 3D Analyst |  
|  | ArcGIS Desktop Basic: 3D Analyst |  
|  | ArcGIS Desktop Standard: 3D Analyst |  
|  | ArcGIS Desktop Advanced: 3D Analyst |  


