## Tabbed feature inspector

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates how to create a custom object inspector. Custom object inspectors allow you to control the information that's returned to the user when selected features are inspected. The custom inspector calculates and reports the area and perimeter for the selected polygons, length, and coordinates for selected lines and points. It also reports the total number of selected features. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The custom information is shown in the second panel of a tabbed control. The first panel of the control shows the standard information for the feature. A command button is also created for the extension classid value to be added to the feature class so it can recognize the extension.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Editing
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio, open the solution file, and build the project.   
1. Register the sample with ESRIRegAsm.exe.  
1. Start ArcMap and add the Editor toolbar.   
1. Click the Customize menu, click Customize Mode, and click the Commands tab.  
1. Under Categories, scroll down, and click Developer Samples.  
1. Under Commands, select the AddEXTCLSID command, and drag it to an available toolbar.   
1. Click the command, browse to the personal or file geodatabase feature class you want to use, and click Add.  
1. Load the feature class you selected with the AddEXTCLSID command onto the map.   
1. Start an edit session and use the Edit tool to select several features.  
1. Click the Attributes tool on the Editor toolbar. The Attributes dialog box opens. The custom inspector appears in the right-hand window of the Attributes dialog box whenever you inspect features belonging to the customized feature class. The default feature inspector shows for features belonging to any other feature class added to the map.  
1. Click a single feature ID (FID) in the tree view; its properties are reported.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


