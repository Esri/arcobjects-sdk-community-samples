##Allow run time customization of the ToolbarControl

###Purpose  
This sample demonstrates customizing the ToolbarControl at run time. The ToolbarControl is used in conjunction with the MapControl and the control commands. The AddItem method is used to add new items to the ToolbarControl with their style set. The ICustomizeDialogEvents.OnStartDialog event is used to put the ToolbarControl into customize mode, and the ICustomizeDialogEvents.OnCloseDialog event is used to take the ToolbarControl out of customize mode.  


###Usage
1. Select the Customize check box to enable customization of the ToolbarControl and to open the Customize dialog box.  
1. To move an item, click it, then drag and drop it to the location indicated by the black vertical bar.   
1. To delete an item, click it, then drag it off the ToolbarControl or right-click the item and click Delete from the Customize menu.   
1. To change the group, group spacing, or style of an item, right-click the item to display the Customize menu.  
1. To add a command, menu, and palette items, double-click them in the Customize dialog box or drag and drop them from the Customize dialog box onto the ToolbarControl.  







####See Also  
[ToolbarControl class](http://desktop.arcgis.com/search/?q=ToolbarControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl interface](http://desktop.arcgis.com/search/?q=IToolbarControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[CustomizeDialog class](http://desktop.arcgis.com/search/?q=CustomizeDialog%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ICustomizeDialog interface](http://desktop.arcgis.com/search/?q=ICustomizeDialog%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ICustomizeDialogEvents interface](http://desktop.arcgis.com/search/?q=ICustomizeDialogEvents%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


