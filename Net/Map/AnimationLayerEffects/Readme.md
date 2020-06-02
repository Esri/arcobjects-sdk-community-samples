## Layer effects animation in ArcMap

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows how to create custom animations in ArcGIS Desktop and implement a layer effects animation type for ArcMap. It allows you to animate through layer brightness and contrast. This animation only works with raster layers that support contrast and brightness as layer properties. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">As shown by the sample code, to create a custom animation type in ArcMap, ArcScene, or ArcGlobe, implement a new keyframe class and a new animation type class. Register the animation type class in the Esri Map Animation Types component category (or Esri 3D Animation Types for ArcScene and Esri 3D Animation Types for Globe or ArcGlobe).</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Map
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
#### Add layer effects animation functionality to ArcMap  
1. Compile the sample project in Visual Studio.  
1. Start ArcMap and add a raster layer to the table of contents (TOC).  
1. Click Customize, Toolbars, and Animation to open the Animation toolbar.  
1. You can now create layer effects animation in ArcMap in a similar way as you create a map layer animation (first use the Create Animation Keyframe dialog box to create a layer effects track with two keyframes, then use the Animation Manager to set the keyframe properties).  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


