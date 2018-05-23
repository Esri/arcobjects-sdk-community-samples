## Print multiple pages

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows how to programmatically print multiple pages from a map document<font face="Verdana">—</font>whose map page size is larger than the printer's paper size<font face="Verdana">—</font>using the IPrinterMPage interface. A sample MXD file is included that can be used to test the sample.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The IPrinterMPage interface used in this sample is intended for use by those who are customizing or extending the printing pipeline and need fine-grained control over the steps of the output process. In cases where you simply need to print a page layout to tiles or to print data driven pages, use the PrintAndExport class, which internally handles multipage printing. For more information on typical programmatic printing scenarios, see [Print active view](http://89f80789-6035-4c28-981f-501538fe43a1).</div>  


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
1. Start Visual Studio and open the solution file.  
1. Click the Build menu and click Build Solution to compile and register the project.  
1. Start ArcMap and open the sample MXD file. You can also use your own MXD file if it's set up to tile to multiple pages.  
1. Click the Customize menu and click Customize Mode. The Customize dialog box opens.  
1. Click the Commands tab and select the Developer Samples category.  
1. Drag and drop the PrintMultiPage command onto an active toolbar.  
1. Click close on the Customize dialog box.  
1. Click the PrintMultiPage tool to print multiple pages. By default the sample is set at two pages to prevent paper waste.  







#### See Also  
[Sample: Print active view](../../../Net/GraphicsPipeline/PrintActiveView)  
[Output](http://desktop.arcgis.com/search/?q=Output&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| Engine Developer Kit | Engine |  


