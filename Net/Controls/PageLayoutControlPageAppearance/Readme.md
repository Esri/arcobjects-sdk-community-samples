##Modify the appearance of the PageLayoutControl's page

###Purpose  
This sample demonstrates how to change the border, background, color, and shadow effects of the PageLayoutControl's page using one, some, or all of the following interfaces: IPage, IFrameProperties, and IPropertySupport.  


###Usage
1. Browse and select a map document to load into the PageLayoutControl.    
1. Change the page border, background, color, and shadow properties by double-clicking a symbol.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The Browse dialog box allows you to search and select map documents that are validated and loaded into the PageLayoutControl using the CheckMxFile and LoadMxFile methods. The visible extent of the PageLayout is set with the ZoomToWholePage method and the TrackRectangle and Pan methods in the OnMouseDown event to zoom in and pan the display.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The LoadStyleFile method and StyleClass property are used in the Form_Load event to display the contents of a SymbologyStyleClass (Borders, Backgrounds, Colors, and Shadows) from the ESRI.ServerStyle file into each of the SymbologyControls. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">When you double-click a symbol in a SymbologyControl, the GetSelectedItem method is used to get the selected symbol, which is applied to the PageLayoutControl's page in one of the following ways: </div>  


####See Also  
[PageLayoutControl class](http://desktop.arcgis.com/search/?q=PageLayoutControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IPageLayoutControl interface](http://desktop.arcgis.com/search/?q=IPageLayoutControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[SymbologyControl class](http://desktop.arcgis.com/search/?q=SymbologyControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyControl interface](http://desktop.arcgis.com/search/?q=ISymbologyControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


