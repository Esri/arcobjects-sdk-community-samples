## Edit event listener

The add-in in this sample provides the ability to understand when various edit events are fired in response to changes in the editing environment or user actions. Using this sample can help you determine which events are appropriate to listen for in various situations, and understand what causes an event to fire.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Editing
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Start Visual Studio, open the solution file, and build the project.  
1. Start ArcMap, click the Customize menu, and click Add-In Manager. The Add-In Manager dialog box opens.  
1. Using the Browse for Add-In command, browse to the location of the add-in and select the .esriAddIn file located in the projects bin\Debug folder.   
1. On the Customize dialog box, drag the Edit Events command from the Add-In Controls category to a toolbar.  
1. Add one or more editable data sources to ArcMap.  
1. Click the Edit Events command. The Edit Events window opens.  
1. Click the Select Events tab. See the following screen shot on the left.  
1. Select one or more edit events in the list for which you want to receive notification, then click the Listen to Events tab.  
1. Perform one or more actions in the editor. Events that fire as a result of the action are listed on the Listen to Events tab. See the following screen shot on the right.  
1. Right-click in the list to clear it.  



![Screen shot showing the Select Events and Listen to Events tabs.](images/pic1.png)  
Screen shot showing the Select Events and Listen to Events tabs.  




#### See Also  
[Working with editing events](http://desktop.arcgis.com/search/?q=Working%20with%20editing%20events&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


