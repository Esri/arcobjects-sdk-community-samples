## Add map surrounds using the SymbologyControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates using the SymbologyControl to display north arrow, scale bar, and scale text symbols, which are used by custom commands when adding MapSurround objects to the GraphicsContainer of the PageLayout. The sample uses the SymbologyControl in conjunction with the PageLayoutControl, TOCControl, ToolbarControl, and the controls commands.</div>  


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
1. Load a map document into the PageLayoutControl.   
1. Use the palette to add map surrounds to the page.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">In the Form_Load event of the SymbolForm, the LoadStyleFile method is used to add the contents of the ESRI.ServerStyle into the SymbologyControl, and the StyleClass property is used to display MarkerNorthArrow, Scalebar, and ScaleText symbols.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Clicking an item displayed in the SymbologyControl fires the OnItemSelectedEvent. The ITool.OnMouseUp events of the custom commands are used to add MapSurroundFrame objects to the GraphicsContainer with the IMapSurroundFrame.MapSurround property set to the IStyleGalleryItem.Item property. </div>  


#### See Also  
[SymbologyControl class](http://desktop.arcgis.com/search/?q=SymbologyControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyControl interface](http://desktop.arcgis.com/search/?q=ISymbologyControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyStyleClass interface](http://desktop.arcgis.com/search/?q=ISymbologyStyleClass%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


