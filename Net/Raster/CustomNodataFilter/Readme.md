##Create a custom NoData pixel filter

###Purpose  
This sample shows how to implement a custom raster pixel filter by extending IPixelFilter. As an example, a NoData filter is implemented that filters pixels as NoData if its value falls in a given range of values.  


###Usage
1. Open the solution in Visual Studio.  
1. A test image (testimage.tif) is provided in this sample's folder if you installed the ArcObjects software development kit (SDK), and the output will be in the same folder by default.  
1. You can change the data path in the TestApp code to reference your dataset. You can also change the NoData range for your specific data.  
1. Build the solution.  
1. Run.  
1. Browse to the output raster dataset and compare the display with the original data. Notice the pixels that fell inside the given range of values do not appear on the screen. (Note: The output raster dataset is located in the Temp folder for the user's local settings.)  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine |  


