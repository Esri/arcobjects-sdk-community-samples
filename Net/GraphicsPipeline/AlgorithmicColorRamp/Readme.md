## Use an AlgorithmicColorRamp to color a ClassBreaksRenderer

This sample demonstrates a tool for producing a user-specified AlgorithmicColorRamp, which is used to color the symbols on a ClassBreaksRenderer on a feature layer.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Graphics Pipeline
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
1. Open the Visual Studio solution file and build the project.  
1. Start ArcMap and create a map.   
1. Open the Customize dialog box. On the Toolbars tab, select the Context Menus option. The Context Menus toolbar appears.   
1. Using the Context Menus toolbar, click Context Menus and click Feature Layer Context Menu. The Feature Layer context menu appears.  
1. On the Commands tab on the Customize dialog box, select the new AlgorithmicColorRamp command from the Developer Samples category. Drag this command onto the Feature Layer context menu that was displayed in the previous step. Close the Customize dialog box.   
1. Add a feature layer (containing polygons, lines, or points) to the map document.  
1. Render your layer with a ClassBreaksRenderer. Right-click the layer and click Properties, then Symbology. Choose the Quantities, GraduatedColors option and add values from any suitable Value field.   
1. Change the colors on the renderer by right-clicking the feature layer in the table of contents (TOC). The dialog box that appears allows you to create an AlgorithmicColorRamp based on a FromColor, a ToColor, and an Algorithm.  
1. Click OK to recolor your feature layer renderer according to your selections.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


