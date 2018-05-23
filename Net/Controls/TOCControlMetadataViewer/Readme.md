## TOCControl metadata viewer

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample describes how to use the TOCControl to access a layer on the PageLayoutControl to show its metadata within a Web Browser control. The metadata Extensible Markup Language (XML) is transformed using the selected style sheet provided by the sample.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
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
1. Load a map document into the PageLayoutControl.   
1. Select a style sheet or type the style sheet's file path.   
1. Right-click a layer on the TOCControl to show its metadata.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">A browse dialog box allows you to search and select map documents (*.mxd) that are validated and loaded into the PageLayoutControl using the CheckMxFile and LoadMxFile methods. The ITOCControl.HitTest method is used in the ITOCControlEvents.OnMouseDown event when right-clicked to determine if a layer has been clicked.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">If the layer is an IDataLayer, the metadata is retrieved using the IMetadata.Metadata property. The IXmlPropertySet2.SaveAsFile method is used to create a temporary metadata.htm file in the same folder as the sample code. The metadata.htm file contains the metadata XML transformed using the selected style sheet. The Web Browser control is used to show this file.</div>  


#### See Also  
[TOCControl class](http://desktop.arcgis.com/search/?q=TOCControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ITOCControl interface](http://desktop.arcgis.com/search/?q=ITOCControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


