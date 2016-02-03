##Simple point plug-in data source

###Purpose  
This sample implements a plug-in data source for a SimplePoint data format to provide direct read-only ArcGIS support for the format. The data in this sample follows a simple format. An American Standard Code for Information Interchange (ASCII) text file contains data for each new point on a new line. The first six characters are the x-coordinate, the next six characters contain the y-coordinate, and the trailing characters contain an attribute value. You can use the sample data from the software development kit (SDK) samples folder or create data according to the preceding specifications. For more information, see Creating a plug-in data source.   


###Usage
1. Open the solution using Visual Studio.  
1. Build the solution to make the SimplePointPlugin class library.  

####Using simple point with ArcGIS for Desktop  
1. Start ArcCatalog and ArcMap.  
1. In ArcCatalog, browse to the ArcGIS installation folder and locate the Developer Kit's Samples\Data\SimplePointData\ folder (you will see a Points dataset).  
1. In ArcCatalog's tree-view, expand the feature dataset to view the contained datasets.  
1. Drag the Points dataset onto ArcMap.  
1. In ArcMap, use the layer's properties to change the renderer to a "Unique values renderer." Use the values from the ColumnOne field.  
1. Examine the layer's attribute table, identify and apply the selection on the layer.  

####Using simple point with ArcGIS Engine  
1. Use this command in an application with a MapControl or PageLayoutControl, and a ToolbarControl.  
1. Add the command to the ToolbarControl. The command (Add SimplePoint layer) can be found in the .NET Samples category.  
1. Run the application.  
1. Click the Add SimplePoint layer command. The OpenSimplePointDlg dialog box appears.   
1. Click the Open Simple Point Data button to browse for Simple Point data datasets. Use Points in <ArcGIS Developer Kit Installation folder>\Samples\data\SimplePointData.  
1. From the datasets list, select the datasets to add to the map and click OK.  







####See Also  
[Plug-in data sources](http://desktop.arcgis.com/search/?q=Plug-in%20data%20sources&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Creating a plug-in data source](http://desktop.arcgis.com/search/?q=Creating%20a%20plug-in%20data%20source&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Implementing optional functionality on plug-in data sources](http://desktop.arcgis.com/search/?q=Implementing%20optional%20functionality%20on%20plug-in%20data%20sources&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Adding a plug-in data source programmatically](http://desktop.arcgis.com/search/?q=Adding%20a%20plug-in%20data%20source%20programmatically&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine |  


