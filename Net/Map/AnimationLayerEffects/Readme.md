##Layer effects animation in ArcMap

###Purpose  
This sample shows how to create custom animations in ArcGIS for Desktop and implement a layer effects animation type for ArcMap. It allows you to animate through layer brightness and contrast. This animation only works with raster layers that support contrast and brightness as layer properties.  As shown by the sample code, to create a custom animation type in ArcMap, ArcScene, or ArcGlobe, implement a new keyframe class and a new animation type class. Register the animation type class in the Esri Map Animation Types component category (or Esri 3D Animation Types for ArcScene and Esri 3D Animation Types for Globe or ArcGlobe).  


###Usage
####Add layer effects animation functionality to ArcMap  
1. Compile the sample project in Visual Studio.  
1. Start ArcMap and add a raster layer to the table of contents (TOC).  
1. Click Customize, Toolbars, and Animation to open the Animation toolbar.  
1. You can now create layer effects animation in ArcMap in a similar way as you create a map layer animation (first use the Create Animation Keyframe dialog box to create a layer effects track with two keyframes, then use the Animation Manager to set the keyframe properties).  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


