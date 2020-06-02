## Layer property page and property sheet

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">A property sheet is a dialog box that contains one or many property pages. Each property page on the property sheet contains controls to view and allow users to interact with objects by changing the values of their properties without writing code. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to implement and register a custom layer property page (LayerVisibilityPage class) for ArcGIS Desktop in .NET. This property page toggles visibility of the target layer. It also includes a custom command (SimpleLayerPropertiesCmd class) showing how to display a property sheet with a selective number of property pages. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
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
#### Implement and register a custom layer property page   
1. Open and compile the sample project in Visual Studio. Set up a debug application if needed.  
1. Open ArcMap, ArcScene, or ArcGlobe. The custom layer property page works in all of these applications since it is registered in the appropriate layer property page component categories.  
1. Add a layer to the application.  
1. Display the layer context menu by right-clicking the layer and selecting Properties. The Properties dialog box appears.  
1. Activate the custom property page by clicking the Layer Visibility (C#) or Layer Visibility (VB.NET) tab.  
1. The visibility is controlled by two radio buttons on the page. Toggle visibility to enable the Apply button in the dialog box.   
1. Click the Apply or OK button to commit the change. The TOC and display are refreshed to reflect the change you made with the custom property page.  
1. Close the Properties dialog box.  

#### Add the custom command (SimpleLayerPropertiesCmd) that displays the simplified layer property sheet  
1. Click the Customize menu, and click Customize Mode. The Customize dialog box appears.   
1. Click the Toolbars tab and select Context Menus.  
1. Expand and scroll down to Feature Layer Context Menu, and click to expand the menu.  
1. Click the Commands tab, and drag and drop the Simple Layer Properties (C#) or Simple Layer Properties (VB.NET) command from the .NET Samples category onto the Feature Layer Context Menu.   
1. Close the dialog box.  
1. Click the Simple Layer Properties command you just added. The layer context menu displays. The simplified Properties dialog box only shows the Layer Visibility and Symbology pages.  
1. If necessary, clean up to remove the custom command by resetting the Feature Layer Context Menu in the Customize dialog box.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


