##Tabbed feature inspector

###Purpose  
This sample demonstrates how to create a custom object inspector. A custom object inspectors allows you to control the information that is returned to the user when selected features are inspected. The custom inspector calculates and reports the area and perimeter of selected polygons, the length and coordinates of selected lines, and the coordinates of selected points.  The custom information is shown in the second panel of a tabbed control. The first panel of the control shows the standard information for the feature.  The selected features must be from an object that supports both IFeatureClass and IClassSchemaEdit (see the Additional information section).  


###Usage
1. Open the solution and set EngineApplication as the startup project.  
1. Build the solution and run the application.  
1. Add point, polyline, and/or polygon feature class data to the map.  
1. Select a feature layer and use the Attach/Detach Tabbed Inspector Extension command.  
1. Confirm that the status bar at the bottom of the EngineApplication displays the Tabbed Inspector Extension as successfully attached to the selected feature class.  
1. Start editing, then choose one or more features from the selected feature class and open the Attribute dialog box.  
1. Ensure that a feature is selected in the Attribute dialog box tree. The Custom tab of the Attribute dialog box shows the output from the custom object inspector.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The inspected features must be from an object that supports both IFeatureClass and IClassSchemaEdit. The Tabbed Feature Inspector is attached to a feature class using the feature class's IClassSchemaEdit interface. Classes that do not implement this interface, such as a shapefile class, cannot be used with this sample. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">In addition, a feature class can have only one IFeatureClassExtension at a time, so some feature classes<font face="Verdana">—</font>such as annotation feature classes<font face="Verdana">—</font>cannot be used with this sample.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample can be adapted for use with an ObjectClass that is not displayed in the table of contents but that has a feature class extension.</div>  




---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


