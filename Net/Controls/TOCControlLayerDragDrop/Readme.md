## Enable layer drag and drop in the TOCControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to drag and drop layers in the TOCControl. The sample uses the TOCControl in conjunction with the PageLayoutControl and the controls commands. The ITOCControl2.EnableLayerDragDrop property is used to enable layer dragging and dropping. Once enabled, layers can be dragged and dropped as follows:</div>

*   On a map 
*   On, to, and from a group layer
*   Between maps
*   Between maps in different TOCControls
*   In ArcMap 
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">To copy a layer, instead of moving within a map, press the Ctrl key while dragging and dropping. To move a layer, instead of copying it between maps, press the Ctrl key while dragging and dropping. </div>  


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
1. Load a map document into the PageLayoutControl and enable layer drag and drop.   
1. Re-order the layers in the TOCControl.   
1. Right-click the PageLayoutControl and drag a rectangle to create a MapFrame.   
1. Copy a layer into the new map by dragging and dropping a layer.   
1. Move a layer into the new map by dragging and dropping a layer while pressing the Ctrl key.   







#### See Also  
[TOCControl class](http://desktop.arcgis.com/search/?q=TOCControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ITOCControl2 interface](http://desktop.arcgis.com/search/?q=ITOCControl2%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


