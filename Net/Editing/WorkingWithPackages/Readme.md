## Working with packages in ArcGIS Engine

This sample demonstrates how to open map and layer packages from [ArcGIS.com](http://www.arcgis.com/home/). As an ArcGIS Engine developer, you can deploy your application and leverage ArcGIS.com to deploy your data and map documents. Data generally updates more frequently than the application and ArcGIS.com can be used to easily push updates to the end user. This sample application shows how to work with ArcGIS.com layer packages, map packages, and Web maps (it first attempts to make a connection to ArcGIS.com). If it can connect, it directly consumes the package. If it cannot make a connection, it searches for the associated layer files and map documents in the known user location where the results of the packages are stored.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Editing
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
1. Build and run the solution.  
1. You can use the default uniform resource locator (URL) in the text box or specify your own by following the instructions in the "Accessing packages from ArcGIS.com" section in the "Working with packages" topic in the "See Also" section at the bottom of this sample.  
1. Click each button to download and open the package.  
1. When you have downloaded the package, the sample works disconnected as it accesses the downloaded data on disk. However, this will not work for Web maps because they require a connection to view the data.  







#### See Also  
[Working with packages](http://desktop.arcgis.com/search/?q=Working%20with%20packages&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ArcGIS.com](http://desktop.arcgis.com/search/?q=ArcGIS.com&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


