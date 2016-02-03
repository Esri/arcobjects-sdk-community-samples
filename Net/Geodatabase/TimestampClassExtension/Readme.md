##Timestamper class extension

###Purpose  
This sample provides an example of a geodatabase class extension that implements the optional IObjectClassEvents interface. By listening to object creation and modification events from the base class, this extension automatically maintains creation and modification time stamps for each object in the class, along with the name of the user who created or last modified the object. Along with this class extension, this sample contains two ancillary classes—a custom class description and a custom property page. The custom class description allows time-stamped feature classes to be created from ArcCatalog and the property page allows users to configure the class extension from ArcCatalog.  


###Usage
1. Open the solution in Visual Studio.  
1. Ensure ArcCatalog is closed. Build the solution's class library in Visual Studio (this registers the class extension, description, and property page with ArcGIS).  
1. Start ArcCatalog. When creating a feature class in a geodatabase, Timestamped Class should show as a type of class.  
1. After creating a time-stamped feature class, view the feature class's properties. There should be a Timestamp Settings tab that allows the extension to be configured.  







####See Also  
[Creating class extensions](http://desktopdev.arcgis.com/search/?q=Creating%20class%20extensions&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create property pages and property sheets](http://desktopdev.arcgis.com/search/?q=How%20to%20create%20property%20pages%20and%20property%20sheets&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


