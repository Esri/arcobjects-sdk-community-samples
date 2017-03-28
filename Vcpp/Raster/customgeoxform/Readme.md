## Create a custom geodata transform

  <div xmlns="http://www.w3.org/1999/xhtml">
    <div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt">
      <span>
        <font face="Verdana">ArcGIS supports many geodata transforms such as RPCXFrom and PolynomialXForm for transforming pixel coordinates to map coordinates and vice versa. This sample shows how to create a custom geodata transform.</font>
      </span>
    </div>
  </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              VC++
Subject:               Raster
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
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
1. Compile and build the solution and generate CustomXForm.dll.  
1. Register CustomXForm.dll.  
1. To transform a raster using SimpleXForm, modify the GeodataXForm on a geodataset. It will then be persisted to the geodataset, as shown in the code example in the Additional information section below.   
1. To transform a raster using RMCXForm, download and unzip the Custom GDAL Driver sample.  
1. If you're using an existing GDAL driver, follow these steps: a) Compile rmcdataset.cpp from stage2 to create gdal_RMC.dll; b) Copy the .dll file to the GDAL plugins directory; c) Register the format with ArcGIS (using the New Format Properties dialog box or by editing RasterFormats.dat); and d) Copy RasterXforms.dat to the ArcGIS bin directory (or modify the existing file if present).  
1. If you're using a new GDAL driver (using GDAL XML Metadata), follow these steps: a) Compile rmcdataset.cpp from stage3 to create gdal_RMC.dll; b) Copy the .dll file to the GDAL plugins directory; c) Register the format with ArcGIS if not done previously; and d) Edit RasterXforms.dat to remove the RMCXform entry (it's not needed in this case).  
1. Ensure rmcdata\rmcS.rmc has the correct file paths to the data files.  
1. Start ArcMap, navigate to rmcdata, and open rmcS.rmc.  





#### Additional information  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">
  <font face="Verdana">To create a custom geodata transform, you will create a GeodataXForm class by implementing the IGeodataXform, IPersistStream, and IClone interfaces.</font>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">
  <font face="Verdana">The C++ sample solution implements two custom GeodataXforms, SimpleXform and RMCXform, which are hosted in a dynamic-link library (DLL), CustomXform.dll. The two custom geodata transforms are used in different ways as follows:</font>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Sub ApplySimpleXForm(pRasterDataset As IRasterDataset)</div>  
<div xmlns="http://www.w3.org/1999/xhtml">    ' This procedure creates a SimpleXForm and persists it to the raster dataset. </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    'Create simplexform. </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Dim pSimpleXForm As ISimpleXForm </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Set pSimpleXForm = New SimpleXForm</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    'Persist the xform to the input raster dataset. </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Dim pGeoDatasetEdit As IGeoDatasetSchemaEdit2 </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Set pGeoDatasetEdit = pRasterDataset </div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Dim pSimpleXForm As ISimpleXForm </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    Set pSimpleXForm = New SimpleXForm </div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml">    pGeoDatasetEdit.AlterGeodataXform pSimpleXForm</div>  
<div xmlns="http://www.w3.org/1999/xhtml">End Sub</div>  




---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


