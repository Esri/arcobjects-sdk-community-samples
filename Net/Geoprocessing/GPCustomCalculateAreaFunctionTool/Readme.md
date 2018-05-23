## Calculate area geoprocessing function tool

This sample is for developers who want to extend the geoprocessing framework by building new geoprocessing tools using ArcObjects. This sample explains how to build a geoprocessing function tool and describes the ArcObjects components necessary for building these tools. This sample function tool calculates the area of a polygon feature class.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Geoprocessing
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
1. Open the solution file, review the code, and compile the project. The function factory is automatically added to the Geoprocessor Function Factory component category.  
1. Register the DLL (in the Debug folder) with ESRIRegAsm utility.   
1. Create a custom toolbox in a folder or a geodatabase. ArcGIS Desktop and ArcGIS Engine developers can do this by using the IGPUtilities2.CreateToolboxFromFactory method.  
1. ArcGIS Desktop users also have the option to add the tool to a custom toolbox manually in ArcCatalog, ArcMap, or ArcGlobe. Right-click a custom toolbox, choose Add, click Tool, expand the AreaCalculation function factory, select the Calculate Area tool, and click OK. The tool is added to the custom toolbox.  







#### See Also  
[Custom geoprocessing function tools](http://desktop.arcgis.com/search/?q=Custom%20geoprocessing%20function%20tools&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Building a custom geoprocessing function tool](http://desktop.arcgis.com/search/?q=Building%20a%20custom%20geoprocessing%20function%20tool&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create a toolbox from a function factory](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20toolbox%20from%20a%20function%20factory&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


