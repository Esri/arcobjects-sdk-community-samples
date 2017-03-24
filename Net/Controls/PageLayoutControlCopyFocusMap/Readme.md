## Copy the PageLayoutControl focus map and overwrite the MapControl map

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates how to set the PageLayoutControl's FocusMap and use the IObjectCopy object to overwrite the MapControl's map object with a copy of the PageLayoutControl's FocusMap. The map object must be copied because the MapControl and PageLayoutControl are unable to share the same ActiveView at the same time.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Load a map document into the PageLayoutControl.   
1. Use the left mouse button to zoom in and the right mouse button to pan both the page and the map data.   
1. Change the PageLayoutControl's FocusMap.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The Browse dialog box allows you to search and select map documents, which are validated and loaded into the PageLayoutControl using the CheckMxFile and LoadMxFile methods. This triggers the OnPageLayoutReplaced event, which loops through the elements in the PageLayoutControl's GraphicsContainer finding MapFrame elements. The name of each MapFrame is set to the name of the map in the MapFrame using the IElementProperties interface. A combo box is populated with all of the map names to allow you to change the FocusMap. The FindElementByName method is used to find the MapFrame containing the map that is to become the FocusMap. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The PageLayoutControl's OnFocusMapChanged and OnAfterScreenDraw (when a new map document has been loaded into the PageLayoutControl) events use the IObjectCopy Copy and Overwrite methods to overwrite the MapControl's map with a copy of the PageLayoutControl's FocusMap. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Each map contained within the PageLayout, encapsulated by the PageLayoutControl, resides within a separate MapFrame, and therefore has its IMap.IsFramed property set to true. A map contained within the MapControl does not reside within a MapFrame. As such, before overwriting the MapControl's map, the IMap.IsFramed property must be set to false. (Failure to do this will lead to corrupted map documents containing the contents of the MapControl being saved).</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The PageLayoutControl and MapControl OnMouseDown events use the TrackRectangle and Pan methods to zoom in and pan around either the PageLayoutControl's page or the MapControl's map. If the MapControl's display is redrawn due to a change in VisibleExtent, the OnAfterScreenDraw event is used to set the extent of the PageLayoutControl's FocusMap equal to that of the MapControl using the IDisplayTransformation interface. </div>  


#### See Also  
[PageLayoutControl class](http://desktop.arcgis.com/search/?q=PageLayoutControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IPageLayoutControl interface](http://desktop.arcgis.com/search/?q=IPageLayoutControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IObjectCopy interface](http://desktop.arcgis.com/search/?q=IObjectCopy%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


