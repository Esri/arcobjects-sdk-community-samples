## Synchronized MapControl and PageLayoutControl application

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates synchronization of a MapControl and a PageLayoutControl in one application. This means that, as in ArcMap, you can switch between the two views and maintain the scale and extent, as well as add graphics to the Map view and see them in the PageLayout view.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">For simplicity, this sample only handles one data frame; it selects the first data frame from a map document.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio, open the solution, and build the project.  
1. Press F5 or double-click the resulting executable to run the application.  
1. Use the Open Map Document command to load a new map document to the application.  
1. Zoom in and add graphic elements using the Graphics toolbar at the bottom on the application window.  
1. Click the Layout tab to switch to the Layout view. The PageLayout is centered around the same point as the MapControl, and the graphic elements from the Map view are visible in the Layout view.  
1. Use the Pan tool in the PageLayout view, then switch back to the Map view. Once again, the map is centered around the same point as the Layout view.  







#### See Also  
[How to create a MapControl application](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20MapControl%20application&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Building a map viewing application using the ArcGIS Engine controls](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Building%20a%20map%20viewing%20application%20using%20the%20ArcGIS%20Engine%20controls&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Load a map document into the PageLayoutControl](../../../Net/Controls/PageLayoutControlLoadMapDocument)  
[Sample: Printing with the PageLayoutControl](../../../Net/Controls/PageLayoutControlPrinting)  
[Working with map elements](http://desktop.arcgis.com/search/?q=Working%20with%20map%20elements&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to drop data onto the MapControl](http://desktop.arcgis.com/search/?q=How%20to%20drop%20data%20onto%20the%20MapControl&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


