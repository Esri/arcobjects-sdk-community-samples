## Timestamper class extension

This sample provides an example of a geodatabase class extension that implements the optional IObjectClassEvents interface. By listening to object creation and modification events from the base class, this extension automatically maintains creation and modification time stamps for each object in the class, along with the name of the user who created or last modified the object. Along with this class extension, this sample contains two ancillary classes<font face="Verdana" xmlns="http://www.w3.org/1999/xhtml">—</font>a custom class description and a custom property page. The custom class description allows time-stamped feature classes to be created from ArcCatalog and the property page allows users to configure the class extension from ArcCatalog.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Geodatabase
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open the solution in Visual Studio.  
1. Ensure ArcCatalog is closed. Build the solution's class library in Visual Studio (this registers the class extension, description, and property page with ArcGIS).  
1. Start ArcCatalog. When creating a feature class in a geodatabase, Timestamped Class should show as a type of class.  
1. After creating a time-stamped feature class, view the feature class's properties. There should be a Timestamp Settings tab that allows the extension to be configured.  







#### See Also  
[Creating class extensions](http://desktop.arcgis.com/search/?q=Creating%20class%20extensions&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create property pages and property sheets](http://desktop.arcgis.com/search/?q=How%20to%20create%20property%20pages%20and%20property%20sheets&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


