## Create a Function Raster dataset

This topic shows how to create a Function Raster dataset from a raster dataset. It opens a raster dataset, applies an Arithmetic Raster function onto it (which adds a constant value of 128 to each band for each pixel of the raster dataset), then saves the created Function Raster dataset to the specified file.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution file.  
1. Edit the strings in the "Specify input directory and dataset name" region to specify the directory and name of the input dataset, and the full path to the output dataset including the .afr extension.  
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


