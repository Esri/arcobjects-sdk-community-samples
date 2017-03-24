## Creating a toolbar of globe tools

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This add-in sample shows how to write ArcObjects code to build a toolbar of tools that work in ArcGlobe, which allows you to interactively create three-dimensional (3D) points, rasterized polylines and polygons, and two-dimensional (2D) text by clicking the globe surface. This sample also shows how to work with globe graphic containers, text elements, polygon elements, polyline elements, and point elements, and how to programmatically adjust various display settings. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               3D
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
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
#### Running the sample from ArcGlobe  
1. Start ArcGlobe.  
1. Click Customize, click Customize Mode, then click the Toolbars tab.  
1. Select GraphicsToolbar on the Toolbars pane.  
1. Close the Customize dialog box.  
1. To use the Point tool, click the globe surface to digitize a 3D sphere.  
1. To use the Polyline tool, click the globe surface to digitize the polyline vertices, and double-click to end the polyline.  
1. To use the Polygon tool, click the globe surface to digitize the polygon vertices and double-click to close the polygon.  
1. To use the Text tool, click the globe surface to specify where the text should be centered, type text on the dialog box that appears, and press Enter.  
1. Selecting a custom color via the Color tool before using any of the previous tools, causes the color of the digitized geometry or text to change accordingly.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  | ArcGIS Desktop Basic: 3D Analyst |  
|  | ArcGIS Desktop Standard: 3D Analyst |  
|  | ArcGIS Desktop Advanced: 3D Analyst |  


