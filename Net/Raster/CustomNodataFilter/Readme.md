## Create a custom NoData pixel filter

This sample shows how to implement a custom raster pixel filter by extending IPixelFilter. As an example, a NoData filter is implemented that filters pixels as NoData if its value falls in a given range of values.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
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
1. Open the solution in Visual Studio.  
1. A test image (testimage.tif) is provided in this sample's folder if you installed the ArcObjects software development kit (SDK), and the output will be in the same folder by default.  
1. You can change the data path in the TestApp code to reference your dataset. You can also change the NoData range for your specific data.  
1. Build the solution.  
1. Run.  
1. Browse to the output raster dataset and compare the display with the original data. Notice the pixels that fell inside the given range of values do not appear on the screen. (Note: The output raster dataset is located in the Temp folder for the user's local settings.)  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


