##Calculate area geoprocessing function tool

###Purpose  
This sample is for developers who want to extend the geoprocessing framework by building new geoprocessing tools using ArcObjects. This sample explains how to build a geoprocessing function tool and describes the ArcObjects components necessary for building these tools. This sample function tool calculates the area of a polygon feature class.  


###Usage
1. Open the solution file, review the code, and compile the project. The function factory is automatically added to the Geoprocessor Function Factory component category.  
1. Register the DLL (in the Debug folder) with ESRIRegAsm utility.   
1. Create a custom toolbox in a folder or a geodatabase. ArcGIS for Desktop and ArcGIS Engine developers can do this by using the IGPUtilities2.CreateToolboxFromFactory method.  
1. ArcGIS for Desktop users also have the option to add the tool to a custom toolbox manually in ArcCatalog, ArcMap, or ArcGlobe. Right-click a custom toolbox, choose Add, click Tool, expand the AreaCalculation function factory, select the Calculate Area tool, and click OK. The tool is added to the custom toolbox.  







####See Also  
[Custom geoprocessing function tools](http://desktopdev.arcgis.com/search/?q=Custom%20geoprocessing%20function%20tools&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Building a custom geoprocessing function tool](http://desktopdev.arcgis.com/search/?q=Building%20a%20custom%20geoprocessing%20function%20tool&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create a toolbox from a function factory](http://desktopdev.arcgis.com/search/?q=How%20to%20create%20a%20toolbox%20from%20a%20function%20factory&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine |  


