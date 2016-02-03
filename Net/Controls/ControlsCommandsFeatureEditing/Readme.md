##Feature editing with the control commands

###Purpose  
This sample demonstrates feature editing using the control commands in conjunction with the ToolbarControl and MapControl. The AddItem method is used within the Form_Load event to add new feature editing commands to the ToolbarControl with their Style set. A Sketch menu and Vertex menu are added to ToolbarMenu's using the AddItem method. Within the MapControl's OnMouseDown event, the PopupMenu method is used to display the appropriate ToolbarMenu when the right mouse button has been clicked. The editing properties used by the feature editing commands in the ToolbarControl and the ToolbarMenu are determined by the IEditProperties and IEditProperties2 interface members implemented by the EngineEditor object. The EditProperties and EditProperties2 forms are used to manage and validate changes to these properties by the end user. Note: If you are editing features from a shapefile or a personal geodatabase, the minimum license required to initialize this application is an ArcGIS Engine or ArcGIS for Desktop Basic license. If you are editing features from an enterprise geodatabase, the minimum license required to initialize this application is an ArcGIS Engine Enterprise GeoDatabase or ArcGIS for Desktop Standard license. Before running this application, check the product licenses in the LicenseControl property pages accordingly.   


###Usage
1. Browse to a map document to load into the MapControl.   
1. Use the commands on the ToolbarControl to sketch new features, update existing features, update parts of a sketch, or update attributes.   
1. Right-click the MapControl to access the Sketch context menu.   
1. Click the Option buttons to change the edit properties.  







####See Also  
[EngineEditor class](http://desktop.arcgis.com/search/?q=EngineEditor%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineEditProperties interface](http://desktop.arcgis.com/search/?q=IEngineEditProperties%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IEngineEditProperties2 interface](http://desktop.arcgis.com/search/?q=IEngineEditProperties2%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


