## Allow run time customization of the ToolbarControl

This sample demonstrates customizing the ToolbarControl at run time. The ToolbarControl is used in conjunction with the MapControl and the control commands. The AddItem method is used to add new items to the ToolbarControl with their style set. The ICustomizeDialogEvents.OnStartDialog event is used to put the ToolbarControl into customize mode, and the ICustomizeDialogEvents.OnCloseDialog event is used to take the ToolbarControl out of customize mode.  


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
1. Select the Customize check box to enable customization of the ToolbarControl and to open the Customize dialog box.  
1. To move an item, click it, then drag and drop it to the location indicated by the black vertical bar.   
1. To delete an item, click it, then drag it off the ToolbarControl or right-click the item and click Delete from the Customize menu.   
1. To change the group, group spacing, or style of an item, right-click the item to display the Customize menu.  
1. To add a command, menu, and palette items, double-click them in the Customize dialog box or drag and drop them from the Customize dialog box onto the ToolbarControl.  







#### See Also  
[ToolbarControl class](http://desktop.arcgis.com/search/?q=ToolbarControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl interface](http://desktop.arcgis.com/search/?q=IToolbarControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[CustomizeDialog class](http://desktop.arcgis.com/search/?q=CustomizeDialog%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ICustomizeDialog interface](http://desktop.arcgis.com/search/?q=ICustomizeDialog%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ICustomizeDialogEvents interface](http://desktop.arcgis.com/search/?q=ICustomizeDialogEvents%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


