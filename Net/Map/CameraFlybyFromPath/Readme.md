## Create camera flyby from path

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This add-in sample describes how to create a camera fly from path. The tool mimics the out-of-the-box functionality to create a camera fly from path available on the Animations toolbar. The tool creates a Globe Camera animation that moves the camera along a selected feature in a feature class.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The tool allows you to create three types of flybys using the path destination options. You can choose to create a simple flyby where both the observer and target move along the path. The second option allows you to create a camera fly where the observer moves along the path while looking at the current target. The third option allows you to move the target along the path while the observer remains fixed at one location. </div>  


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
1. Open the Visual Studio solution file.  
1. Build the solution.  
1. Start ArcGlobe.  
1. On the Customize dialog box, choose the Add-In Controls category.  
1. On the Command Items pane, drag the Camera flyby from path onto a toolbar.  
1. To move the globe camera along a path, add a line feature dataset in the globe session. Also, select a line feature using the "Select features" tool. The selected line is the one along which the globe camera will be animated.   
1. Choose the tool.  
1. Choose the options on the Camera Flyby From Path form.  
1. On running the tool, a Globe Camera track gets added to the globe.  
1. An animation is created, which can then be played by clicking Customize, Toolbars, and Animation to open the Animation Toolbar and then clicking on “Open Animation Controls” command  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: 3D Analyst | ArcGIS Desktop Basic: 3D Analyst |  
| ArcGIS Desktop Standard: 3D Analyst | ArcGIS Desktop Standard: 3D Analyst |  
| ArcGIS Desktop Advanced: 3D Analyst | ArcGIS Desktop Advanced: 3D Analyst |  


