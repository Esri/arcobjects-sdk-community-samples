## Add a custom menu created in .NET to ArcGIS Desktop

This sample demonstrates how to create a base or root-level menu with submenu functionality. The root-level menu is created by inheriting the BaseMenu class from the ESRI.ArcGIS.ADF.Local assembly's ESRI.ArcGIS.ADF namespace and by implementing the [ESRI.ArcGIS.Framework.IRootLevelMenu](http://esriFramework/IRootLevelMenu.htm) interface. The submenu functionality is created by using existing ArcGIS functionality that is accessed by the class identifier (CLSID) and ProgID in ArcMap. Additionally, a custom command is created and is accessed as a submenu. This sample contains all of the code demonstrated in the [Adding a custom menu created in .NET to ArcGIS Desktop](http://e685b331-303d-47a0-ae4f-22a41ab3566b) walkthrough.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
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
#### Add and use menu and submenu commands in ArcMap  
1. Open and compile the solution in Visual Studio.  
1. Start ArcMap.  
1. Click the Customize menu and click Customize Mode. The Customize dialog box appears.   
1. Click the Commands tab, and in the Categories area, scroll down and click [Menus].   
1. In the Commands area, click My_Menu and drag it onto the ArcMap graphical user interface (GUI) in the menu bar area.   
1. Click Close. You can now access the functionality on your custom menu and submenu items. See the following screen shots:  



![Screen shot of the Customize dialog box.](images/pic1.png)  
Screen shot of the Customize dialog box.  
![Screen shot of the My_Menu command in ArcMap.](images/pic1.png)  
Screen shot of the My_Menu command in ArcMap.  




#### See Also  
[Walkthrough: Adding a custom menu created in .NET to ArcGIS Desktop](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Adding%20a%20custom%20menu%20created%20in%20.NET%20to%20ArcGIS%20Desktop&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Creating toolbars and menus](http://desktop.arcgis.com/search/?q=Creating%20toolbars%20and%20menus&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Creating commands and tools](http://desktop.arcgis.com/search/?q=Creating%20commands%20and%20tools&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


