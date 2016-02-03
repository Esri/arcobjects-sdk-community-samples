##Share a command pool between ToolbarControls

###Purpose  
This sample demonstrates what happens when multiple ToolbarControls share the same CommandPool and use the ToolbarControl in conjunction with the MapControl and control commands.  


###Usage
1. Browse to a map document to load into the MapControl.   
1. Click the ZoomIn, ZoomOut, or Pan tool (only one tool is depressed).   
1. Share the same CommandPool between both ToolbarControls (the UsageCount changes).   
1. Click the ZoomIn, ZoomOut, or Pan tool (the same tool on both ToolbarControls are depressed).  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The AddItem method is used within the Form_Load event to add four new items to ToolbarControl1 and ToolbarControl2. Three of the items that are added to ToolbarControl1 are also added to ToolbarControl2. By default, each ToolbarControl has its own CommandPool to which these items' commands are added. One instance of each command in the CommandPool of ToolbarControl1 is used by ToolbarControl1; consequently, the ICommandPool.UsageCount of each command is one. This is the same for ToolbarControl2.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">By selecting the check box, ToolbarControl1 and ToolbarControl2 share the CommandPool of ToolbarControl1. If any command used by ToolbarControl2 is in the CommandPool, the ICommand.UsageCount of that command increments by one. If any command used by ToolbarControl2 is not in the CommandPool, the command is added to the CommandPool with an ICommandPool.UsageCount of one.</div>  


####See Also  
[ToolbarControl Class](http://desktopdev.arcgis.com/search/?q=ToolbarControl%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl Interface](http://desktopdev.arcgis.com/search/?q=IToolbarControl%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ICommandPool Interface](http://desktopdev.arcgis.com/search/?q=ICommandPool%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


