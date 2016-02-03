##Create a custom default raster catalog renderer

###Purpose  
This sample shows how to customize the default raster catalog renderer. This sample sets two available renderers—the RGB and stretch renderers—then displays the single band raster datasets with a defined stretch renderer and multiple band raster datasets with a defined RGB renderer.  


###Usage
1. Start Visual Studio, open the solution file, and compile the program. A dynamic-link library (DLL) file is generated and registered in the ESRI RasterCatalogRendererPickers component category.  
1. Run category.exe from the ArcGIS bin directory to confirm that the custom DLL was added to that category.  
1. Start ArcMap and add a raster catalog with multiple bands (> 4) or single band raster datasets to test.  
1. To return to the ArcGIS built-in default renderer for raster catalogs, use the esriregasm.exe utility from the Program Files\Common Files\ArcGIS\bin folder to unregister the DLL.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit |  |  


