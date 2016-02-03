##Get and set key properties on a mosaic dataset

###Purpose  
This sample shows how to open a mosaic dataset and goes through each row in the attribute table, then checks whether the dataset in the row has any band information (band properties) associated with it. If the item has no band information, band properties for the first three bands in the item (if the item has three or more bands) are inserted. The mosaic dataset product definition is changed to natural color red, green, and blue (RGB) so that ArcGIS can use the band names of the mosaic dataset. This sample also shows how to set a key property on the mosaic dataset. This sample has the following functions:Functions to get/set any key property for a datasetFunctions to get/set any band property for a datasetFunctions to get all the properties and all the band properties for a dataset  


###Usage
1. Start Visual Studio and open the solution file.  
1. Edit the strings in the Input database and Mosaic Dataset region to specify the parameters that specify the database from which to open the mosaic dataset, and the name of the mosaic dataset to open. Alternatively, these parameters can also be specified as command line arguments.  
1. Compile and run the program.  
1. If the program succeeds, the console shows a Success message. If an error occurs, it shows on the console window.  
1. Load the changed mosaic dataset into ArcMap or ArcCatalog.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine |  


