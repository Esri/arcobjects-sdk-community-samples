## Create a Raster Function template

This topic shows how to create a Raster Function template, which defines a series of raster functions applied to two images resulting in one Function Raster dataset as the output. It accepts as input, the Panchromatic and Multispectral rasters of a QuickBird Basic dataset, and applies a series of raster functions to each, resulting in a pan-sharpened raster dataset as the output.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution file.  
1. Edit the strings in the "Specify input directory and dataset name" region to specify the directory and name of the input datasets, and the full path to the output dataset including the .afr extension.  
1. Compile and run the program.  
1. If the program succeeds, the console shows a "Success" message. If an error occurs, it shows on the console window.  
1. Load the created output .afr file into ArcMap or ArcCatalog, which reflects the processing results.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


