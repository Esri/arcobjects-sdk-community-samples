## Creating toolbar menus that work with the ToolbarControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates creating menu items, submenus, and pop-up menus using the ToolbarMenu object. The sample uses the ToolbarControl in conjunction with the MapControl and control commands.</div>  


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
1. Browse to a map document to load into the MapControl.   
1. Navigate around the data using the commands on the ToolbarControl.   
1. Use the command buttons to add a menu item to the ToolbarControl, and a submenu to the menu item.   
1. Right-click the display to show the Navigation menu.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The AddItem method is used within the Form_Load event to add new items to the ToolbarControl with their style set. An IMenuDef object is created from a menu defined within the same application. The menu is defined within a class implementing the IMenuDef interface and is made up of default ToolbarControl commands.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This menu is added to a ToolbarMenu using the AddItem method and the ToolbarMenu.Hook property is set to the ToolbarControl. A submenu is added to this menu using the IToolbarMenu.AddSubMenu method. The IToolbarMenu.PopupMenu method is used within the MapControl OnMouseDown event to display the menu when the right mouse button is used.</div>  


#### See Also  
[ToolbarControl class](http://desktop.arcgis.com/search/?q=ToolbarControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarControl interface](http://desktop.arcgis.com/search/?q=IToolbarControl%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IToolbarMenu interface](http://desktop.arcgis.com/search/?q=IToolbarMenu%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IMenuDef interface](http://desktop.arcgis.com/search/?q=IMenuDef%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


