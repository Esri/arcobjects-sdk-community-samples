## Visualizing the camera path while animating

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This add-in sample allows you to generate and visualize the trace back path the camera follows in a GlobeCamera animation. The path is created using three-dimensional (3D) graphic elements. The sample allows the placement of graphics (at the camera positions) as the number of graphic elements created per second or by setting the maximum number of elements created between keyframe positions. This is useful to visualize the camera's location at a time duration without playing or previewing the animation<font face="Verdana">—f</font>or example, to generate the flight path of an animation you created by recording the navigation (using the Navigate tool, Fly tool, and so on). Also, the GlobeCamera keyframes have a different color than the intermediate camera positions. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">There is also the option to generate graphics for the "Camera to target" direction at each of the camera positions. By visualizing this information, users can make changes to their keyframe attributes resulting in better and fine tuned animations.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               3D
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
1. Start Visual Studio, open the solution, and build the project.  
1. Open the ArcGlobe application in design mode.  
1. Click the Customize menu and click Customize Mode. The Customize dialog box opens.   
1. Choose the Add-In Controls category.  
1. Drag the Visualize Camera Path command from the Command Items pane onto a toolbar.  
1. Run the application and create an animation.  
1. Click the command.  
1. Make your choices on the form and play the animation or generate the camera path.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: 3D Analyst | ArcGIS Desktop Basic: 3D Analyst |  
| ArcGIS Desktop Standard: 3D Analyst | ArcGIS Desktop Standard: 3D Analyst |  
| ArcGIS Desktop Advanced: 3D Analyst | ArcGIS Desktop Advanced: 3D Analyst |  


