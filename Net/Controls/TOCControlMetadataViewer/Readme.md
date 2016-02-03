##TOCControl metadata viewer

###Purpose  
This sample describes how to use the TOCControl to access a layer on the PageLayoutControl to show its metadata within a Web Browser control. The metadata Extensible Markup Language (XML) is transformed using the selected style sheet provided by the sample.  


###Usage
1. Load a map document into the PageLayoutControl.   
1. Select a style sheet or type the style sheet's file path.   
1. Right-click a layer on the TOCControl to show its metadata.   





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">A browse dialog box allows you to search and select map documents (*.mxd) that are validated and loaded into the PageLayoutControl using the CheckMxFile and LoadMxFile methods. The ITOCControl.HitTest method is used in the ITOCControlEvents.OnMouseDown event when right-clicked to determine if a layer has been clicked.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">If the layer is an IDataLayer, the metadata is retrieved using the IMetadata.Metadata property. The IXmlPropertySet2.SaveAsFile method is used to create a temporary metadata.htm file in the same folder as the sample code. The metadata.htm file contains the metadata XML transformed using the selected style sheet. The Web Browser control is used to show this file.</div>  


####See Also  
[TOCControl class](http://desktop.arcgis.com/search/?q=TOCControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ITOCControl interface](http://desktop.arcgis.com/search/?q=ITOCControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


