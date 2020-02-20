## Mosaic raster datasets to a file raster format

This sample shows how to create a file raster dataset or a personal geodatabase raster mosaic from all rasters stored in a folder and its subfolders. This approach is different from creating a geodatabase raster dataset mosaic due to a different implementation mechanism for handling a file mosaic and a geodatabase (file geodatabase and ArcSDE) raster mosaic.  


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
1. Open the CreateFileRasterMosaic solution in Visual Studio.  
1. Review the source code and substitute the local variables with your data location.  
1. Compile and run.  
1. Check the output raster file. It will be a mosaic of all the raster datasets in the input folder and its subfolders.  







#### See Also  
[Sample: Mosaic raster datasets to a geodatabase raster dataset](../../../Net/Raster/CreateGDBRasterDatasetMosaic)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


