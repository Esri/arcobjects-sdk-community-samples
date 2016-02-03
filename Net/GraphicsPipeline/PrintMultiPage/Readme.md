##Print multiple pages

###Purpose  
This sample shows how to programmatically print multiple pages from a map document—whose map page size is larger than the printer's paper size—using the IPrinterMPage interface. A sample MXD file is included that can be used to test the sample. The IPrinterMPage interface used in this sample is intended for use by those who are customizing or extending the printing pipeline and need fine-grained control over the steps of the output process. In cases where you simply need to print a page layout to tiles or to print data driven pages, use the PrintAndExport class, which internally handles multipage printing. For more information on typical programmatic printing scenarios, see Print active view.  


###Usage
1. Start Visual Studio and open the solution file.  
1. Click the Build menu and click Build Solution to compile and register the project.  
1. Start ArcMap and open the sample MXD file. You can also use your own MXD file if it's set up to tile to multiple pages.  
1. Click the Customize menu and click Customize Mode. The Customize dialog box opens.  
1. Click the Commands tab and select the Developer Samples category.  
1. Drag and drop the PrintMultiPage command onto an active toolbar.  
1. Click close on the Customize dialog box.  
1. Click the PrintMultiPage tool to print multiple pages. By default the sample is set at two pages to prevent paper waste.  







####See Also  
[Sample: Print active view](../../../Net/GraphicsPipeline/PrintActiveView)  
[Output](http://desktopdev.arcgis.com/search/?q=Output&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| Engine Developer Kit | Engine |  


