## Create a custom default raster catalog renderer

This sample shows how to customize the default raster catalog renderer. This sample sets two available renderers<font face="Verdana" xmlns="http://www.w3.org/1999/xhtml">—the </font>RGB and stretch renderers<font face="Verdana" xmlns="http://www.w3.org/1999/xhtml">—</font>then displays the single band raster datasets with a defined stretch renderer and multiple band raster datasets with a defined RGB renderer.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
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
1. Start Visual Studio, open the solution file, and compile the program. A dynamic-link library (DLL) file is generated and registered in the ESRI RasterCatalogRendererPickers component category.  
1. Run category.exe from the ArcGIS bin directory to confirm that the custom DLL was added to that category.  
1. Start ArcMap and add a raster catalog with multiple bands (> 4) or single band raster datasets to test.  
1. To return to the ArcGIS built-in default renderer for raster catalogs, use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin folder to unregister the DLL.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit |  |  


