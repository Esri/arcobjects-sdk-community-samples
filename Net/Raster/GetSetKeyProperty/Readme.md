## Get and set key properties on a mosaic dataset

  <div xmlns="http://www.w3.org/1999/xhtml">
    <div>This sample shows how to open a mosaic dataset and goes through each row in the attribute table, then checks whether the dataset in the row has any band information (band properties) associated with it. If the item has no band information, band properties for the first three bands in the item (if the item has three or more bands) are inserted. The mosaic dataset product definition is changed to natural color red, green, and blue (RGB) so that ArcGIS can use the band names of the mosaic dataset. This sample also shows how to set a key property on the mosaic dataset.</div>
    <div> </div>
    <div>This sample has the following functions:</div>

*   Functions to get/set any key property for a dataset
*   Functions to get/set any band property for a dataset
*   Functions to get all the properties and all the band properties 
*   <div>for a dataset</div>

  </div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution file.  
1. Edit the strings in the Input database and Mosaic Dataset region to specify the parameters that specify the database from which to open the mosaic dataset, and the name of the mosaic dataset to open. Alternatively, these parameters can also be specified as command line arguments.  
1. Compile and run the program.  
1. If the program succeeds, the console shows a Success message. If an error occurs, it shows on the console window.  
1. Load the changed mosaic dataset into ArcMap or ArcCatalog.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


