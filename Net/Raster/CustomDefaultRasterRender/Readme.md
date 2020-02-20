## Create a custom default raster renderer

This sample shows how to create a custom default raster renderer. This sample creates a dynamic-link library (DLL), which is registered to the component category, and changes the default behavior to add a 1-bit tagged image file format (TIFF) image.  


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
1. Start Visual Studio, open the solution file, and compile the program. A DLL is generated and registered in the ESRI RasterRendererMakers component category.  
1. Run category.exe from the ArcGIS bin directory to confirm that the custom DLL was added to that category.  
1. Start ArcMap and add a 1-bit TIFF image. The image displays as a unique value renderer with red color.  
1. To return to the ArcGIS built-in renderer for the 1-bit TIFF, use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin folder to unregister the DLL.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit |  |  


