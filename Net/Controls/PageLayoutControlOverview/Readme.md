## Use a PageLayoutControl as an overview window

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample application contains two PageLayoutControls and shows how to use one to view the visible extent of the other.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  12/13/2018
ArcObjects SDK:        10.7
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Click the Load button and browse to a map document to load into the PageLayoutControl.   
1. Click to zoom in and right-click to pan the main PageLayoutControl. View the visible extent on the other PageLayoutControl.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The Browse dialog box allows you to search and select map documents that are validated and loaded into the main PageLayoutControl, triggering the OnPageLayoutReplaced event. The OnPageLayoutReplaced event determines the MxPath property of the main PageLayoutControl and uses it to load the same document into the second PageLayoutControl with the LoadMxFile method.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The OnExtentUpdated event is triggered when you zoom in on the main PageLayoutControl. The event uses the AddElement method to add an element displaying the visible extent of the main PageLayoutControl on the second PageLayoutControl. Any previous elements on the second PageLayoutControl are found using the FindElementByName method and are deleted from the GraphicsContainer. The Refresh method is used to refresh the graphics to reflect the changes. </div>  


#### See Also  
[PageLayoutControl class](http://desktop.arcgis.com/search/?q=PageLayoutControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IPageLayoutControl interface](http://desktop.arcgis.com/search/?q=IPageLayoutControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


