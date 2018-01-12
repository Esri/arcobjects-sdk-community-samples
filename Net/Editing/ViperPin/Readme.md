## ViperPin tool

  <div xmlns="http://www.w3.org/1999/xhtml">This sample is a custom edit tool that creates parcel PIN values for parcel features. It is a new version of the SnakePin sample provided with ArcGIS 9.3.1 and earlier. The tool creates PIN values based on the intersection of an edit sketch and an underlying parcel layer. A base PIN value and an increment value can be added. This task works with any polygon data that has a numeric field to accept the PIN value. However, due to the numerous ways organizations represent parcel PINs, it is difficult to produce a generic tool that works for all; therefore, this tool serves as a sample only.</div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Compile and register the .dll.  
1. Start ArcMap and add some parcel data with a numeric field.  
1. Customize ArcMap and place the sample tool on a toolbar.  
1. Start editing and select a parcel feature template.  
1. Select the tool and sketch over some parcels.  
1. On the PIN dialog box, select the PIN field. Add a start PIN and increment.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


