##Synchronized MapControl and PageLayoutControl application

###Purpose  
This sample demonstrates synchronization of a MapControl and a PageLayoutControl in one application. This means that, as in ArcMap, you can switch between the two views and maintain the scale and extent, as well as add graphics to the Map view and see them in the PageLayout view. For simplicity, this sample only handles one data frame; it selects the first data frame from a map document.  


###Usage
1. Start Visual Studio, open the solution, and build the project.  
1. Press F5 or double-click the resulting executable to run the application.  
1. Use the Open Map Document command to load a new map document to the application.  
1. Zoom in and add graphic elements using the Graphics toolbar at the bottom on the application window.  
1. Click the Layout tab to switch to the Layout view. The PageLayout is centered around the same point as the MapControl, and the graphic elements from the Map view are visible in the Layout view.  
1. Use the Pan tool in the PageLayout view, then switch back to the Map view. Once again, the map is centered around the same point as the Layout view.  







####See Also  
[How to create a MapControl application](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20MapControl%20application&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Building a map viewing application using the ArcGIS Engine controls](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Building%20a%20map%20viewing%20application%20using%20the%20ArcGIS%20Engine%20controls&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Load a map document into the PageLayoutControl](../../../Net/Controls/PageLayoutControlLoadMapDocument)  
[Sample: Printing with the PageLayoutControl](../../../Net/Controls/PageLayoutControlPrinting)  
[Working with map elements](http://desktop.arcgis.com/search/?q=Working%20with%20map%20elements&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to drop data onto the MapControl](http://desktop.arcgis.com/search/?q=How%20to%20drop%20data%20onto%20the%20MapControl&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


