## Printing with the PageLayoutControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates changing the PageLayoutControl's page properties and printing the page to the system default printer. The Browse dialog box allows you to search and select map documents, which are validated and loaded into the PageLayoutControl using the CheckMxFile and LoadMxFile methods. Whenever the FormID, Orientation, or PrinterToPageMapping properties of the page are changed, the number of pages to be printed is updated using the PrinterPageCount property. Before the PageLayout is sent to the printer using the Print method, the orientation of the printer's paper is aligned to that of the page's paper. </div>  


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
1. Browse and select a map document to load into the PageLayoutControl.   
1. Change the page size, orientation, and printer to the page mapping properties.   
1. Before printing the page layout, specify the page range to be printed and whether there is to be any overlap between pages.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Changing the page orientation or size can result in the map frame shrinking in relation to the page. This is dependant on the IPage.StretchGraphicsWithPage property. </div>  


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


