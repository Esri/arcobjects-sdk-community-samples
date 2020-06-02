## ToolbarControl MDI application

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates creating a Multiple Document Interface (MDI) application using the ToolbarControl, MapControl, and the control commands.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Click File and click New to create a document window containing a MapControl, then click Close to exit the currently selected document window.   
1. Click Window and click Cascade, Tile Horizontally, or Tile Vertically to arrange the document windows in the application.  
1. Use the commands and tools on the ToolbarControl to load a map document and navigate around the data in the currently selected document window.   
1. Click a document window to select it or select it from the Window menu.   
1. Click Exit under the File menu to close the application.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The "parent" form containing the ToolbarControl has the IsMdiContainer property set to true for it to support "child" document windows. The parent form also contains a MainMenu component with a File menu for creating and closing child document windows, and a Windows menu for arranging the child document windows.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The child form contains a MapControl. When a document is loaded into the MapControl, the child form's caption is set to the IMapControl3.DocumentFileName property in the OnMapReplaced event. This caption is used by the Windows menu on the parent form to show a list of all the child document windows that are currently open.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">For the commands and tools on the ToolbarControl to work with multiple MapControls in the child document windows, the SetBuddyControl method is used within the parent form's MdiChildActivate event to set the ToolbarControl buddy to the MapControl in the active child form.</div>  


#### See Also  
[ToolbarControl Class](http://desktop.arcgis.com/search/?q=ToolbarControl%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl Interface](http://desktop.arcgis.com/search/?q=IToolbarControl%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


