## Graphics layers ToolControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates creating a ToolControl by implementing the IToolControl interface. The ToolControl hosts a drop-down list that displays the names of the graphics layers in the map. The end user can select and activate a graphics layer from the ToolControl. This sample also includes a command that creates and adds a new graphics layer to a map. A ToolbarDef hosts both the ToolControl and the new graphics layer command. The following techniques are covered by this sample:</div>

*   Implementing IToolControl
*   Wiring IActiveViewEvents
*   Creating graphics layer
*   Getting and setting the active graphics layer
*   Implementing IToolbarDef   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Graphics Pipeline
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio and open the solution.  
1. Build the solution.  
1. Open a MapControl or PageLayoutControl application (or ArcMap).  
1. From the ToolbarControl Customize dialog box, double-click the Graphics Layers toolset to add the commands to the ToolbarControl.  
1. Use the graphics layers list to select the active graphics layer and use the New Graphics Layer command to add new graphics layers to the map.  







#### See Also  
[How to wire ArcObjects .NET events](http://desktop.arcgis.com/search/?q=How%20to%20wire%20ArcObjects%20.NET%20events&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolControl interface](http://desktop.arcgis.com/search/?q=IToolControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


