## Building a MapViewer application using the ArcGIS Engine controls

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">In this sample, the MapControl, PageLayoutControl, TOCControl, and ToolbarControl are used in conjunction with other ArcObjects and the control commands to create a Map Viewer application. This sample accompanies the [Building a map viewing application using the ArcGIS Engine controls](http://7bd52ed1-18ae-4aa7-8cde-e9eaed9537fe) walkthrough and demonstrates the following:</div>

*   Loading preauthored map documents into the PageLayoutControl and MapControl using the CheckMxFile and LoadMxFile methods.
*   Setting the ToolbarControl and TOCControl buddy control to be the PageLayoutControl using the SetBuddyControl method.
*   Resizing the MapControl and PageLayoutControl when the container form or dialog box is resized.
*   Adding navigation and inquiry commands to the ToolbarControl using the AddItem method.
*   Creating a pop-up menu from the commands that work with the PageLayoutControl and displays as a right-click context menu.
*   Creating a palette of tools from the commands and adding it to the ToolbarControl.
*   Managing the way map and layer labels are edited in the TOCControl using the LabelEdit property and the ITOCControlEvents.OnEndLabelEdit event.
*   Using the MapControl as an overview window by using the DrawShape method to highlight the current extent of data in the PageLayoutControl.
*   Creating a custom Add Date command that works with the PageLayoutControl, ToolbarControl, and MapControl.
*   Customizing the ToolbarControl with the Customize property and displaying the CustomizeDialog to the end user at runtime.
*   Persisting the ToolbarControl items using the SaveItems and LoadItems methods.
*   Printing the contents of the PageLayoutControl using the PrintPageLayout method.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open the .NET solution file and build the projects.  
1. Run the controls application.  
1. Use the Open Document command on the ToolbarControl to browse to a preauthored map document to load into the PageLayoutControl.  
1. Use the commands and tools on the ToolbarControl to navigate around the map document. Right-click the PageLayoutControl to display the pop-up menu. Use the palette on the ToolbarControl to draw graphic elements on the PageLayoutControl.  
1. Use the custom Add Date tool to add an element to the PageLayoutControl containing the current date.  
1. Click a map or layer label in the TOCControl, then click it again to edit the label.  
1. Customize the ToolbarControl by adding new commands, menus, or palettes from the CustomizeDialog. To alter the appearance of an existing item on the ToolbarControl, right-click the item to display the context menu. Stop running the application, then restart the application to see that the customizations you made were persisted.  
1. Print the contents of the PageLayoutControl by selecting Print from the File menu.  







#### See Also  
[Walkthrough: Building a map viewing application using the ArcGIS Engine controls](http://desktop.arcgis.com/search/?q=Walkthrough%3A%20Building%20a%20map%20viewing%20application%20using%20the%20ArcGIS%20Engine%20controls&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


