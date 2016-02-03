##Update a legend format with SymbologyControl area and line patches

###Purpose  
This sample demonstrates using the SymbologyControl to display AreaPatch and LinePatch symbols that are used to update the format of a legend. The sample uses the SymbologyControl in conjunction with the PageLayoutControl, ToolbarControl, and the controls commands.  


###Usage
1. Open a map document.   
1. Add a legend to the PageLayout.   
1. Change the area patch and line patch style by selecting an item from the SymbologyControl.  
1. Delete the legend.   





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">A new Legend MapSurround is created and added to the GraphicsContainer of the PageLayout with a name of Legend using the IPageLayoutControl.AddElement method. The IPageLayoutControl.FindElementByName method is used to find the Legend element to delete it using the IGraphicsContainer.DeleteElement method, or update its formatting.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The LoadStyleFile method is used within the Form_Load event of the SymbolForm to add the contents of the ESRI.ServerStyle into the SymbologyControl. The StyleClass property is used to set whether AreaPatch or LinePatch symbols show in the SymbologyControl. Clicking an item shown in the SymbologyControl fires the OnItemSelectedEvent. The ILegend.Format.DefaultAreaPatch is set when the IStyleGalleryItem.Item property implements IAreaPatch, and the ILegend.Format.DefaultLinePatch is set when the IStyleGalleryItem.Item property implements ILinePatch. </div>  


####See Also  
[SymbologyControl Class](http://desktopdev.arcgis.com/search/?q=SymbologyControl%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyControl Interface](http://desktopdev.arcgis.com/search/?q=ISymbologyControl%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyStyleClass Interface](http://desktopdev.arcgis.com/search/?q=ISymbologyStyleClass%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


