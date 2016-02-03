##Add a custom bookmarks MultiItem to the ToolbarControl

###Purpose  
This sample demonstrates creating a MultiItem and adding it to a ToolbarMenu. The MultiItem creates a menu item for each spatial bookmark that exists in the bookmarks collection of the focus map. The sample uses the ToolbarControl in conjunction with the MapControl and Control commands.   


###Usage
1. Browse to a map document to load into the MapControl.   
1. Navigate around the data using the commands on the ToolbarControl.   
1. Zoom to any bookmark listed on the spatial bookmark menu.  
1. Create a new bookmark based on the current extent of the focus map.   





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">An IMultiItem object is created from a MultiItem defined in the same application. The MultiItem is defined in a class implementing the IMenuDef interface. The IMultiItem.OnPopup function determines the number of menu items to be created and returns the number of bookmarks in the focus maps bookmark collection. The IMultiItem.ItemCaption property sets each menu item caption to ISpatialBookmark.Name and the IMultiItem.OnItemClick event zooms to the bookmark with the ISpatialBookmark.ZoomTo method. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">A new ToolbarMenu is created and the IToolbarMenu2.AddMultiItem method is used to add the spatial bookmarks MultiItem to the ToolbarMenu. A create bookmarks custom command defined in the application is also added to the ToolbarMenu using the IToolbarMenu2.AddItem method. The IToolbarControl.AddItem method is used in the Form_Load event to add the ToolbarMenu and some control commands to the ToolbarControl with their style set. </div>  


####See Also  
[ToolbarControl class](http://desktopdev.arcgis.com/search/?q=ToolbarControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl interface](http://desktopdev.arcgis.com/search/?q=IToolbarControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarMenu2 interface](http://desktopdev.arcgis.com/search/?q=IToolbarMenu2%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IMultiItem interface](http://desktopdev.arcgis.com/search/?q=IMultiItem%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create dynamic menu commands using a MultiItem](http://desktopdev.arcgis.com/search/?q=How%20to%20create%20dynamic%20menu%20commands%20using%20a%20MultiItem&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


