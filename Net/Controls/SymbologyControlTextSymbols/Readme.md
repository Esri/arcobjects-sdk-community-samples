##Control text symbols using the SymbologyControl

###Purpose  
This sample demonstrates using the SymbologyControl to display text symbols that control the style of text elements. The sample uses the SymbologyControl in conjunction with the PageLayoutControl, ToolbarControl, and the controls commands.  


###Usage
1. Select TextSymbol from the SymbologyControl.   
1. Add the text and select the text font size.   
1. Right-click the PageLayoutControl to add a text element.   





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The LoadStyleFile method is used in the Form_Load event of the SymbolForm to add the contents of the ESRI.ServerStyle into the SymbologyControl. The StyleClass property is used to display text symbols in the SymbologyControl. When an item is selected by the end user from the SymbologyControl, a ServerStyleGalleryItem is returned.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The IPageLayoutControlEvents.OnMouseDown event is used to add a TextElement to the GraphicsContainer of the PageLayout when right-clicked. The ITextElement.Symbol is set to the IStyleGalleryItem.Item property.</div>  


####See Also  
[SymbologyControl class](http://desktop.arcgis.com/search/?q=SymbologyControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyControl interface](http://desktop.arcgis.com/search/?q=ISymbologyControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyStyleClass interface](http://desktop.arcgis.com/search/?q=ISymbologyStyleClass%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


