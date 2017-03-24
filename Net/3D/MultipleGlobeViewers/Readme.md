## Multiple globe viewers

This add-in sample shows how multiple viewer windows can be opened in ArcGlobe and tied to the main viewer. With this capability, developers can have multiple views from the same camera position. In this sample, the user has the option to add a new viewer to ArcGlobe, then by using the tool, get the "observer top down" view in the new window for the current observer location in the main viewer. The display in the new viewer is dynamically updated as the user navigates around in the main window. In a similar manner, developers can add other options in addition to the observer top down view.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               3D
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
1. Open the solution in Visual Studio.  
1. Build the solution.  
1. Start ArcGlobe.  
1. On the Customize dialog box, choose the Add-In Controls category.  
1. On the Command Items pane, drag the MultipleGlobeViewers command onto a toolbar.  
1. Run the application.  
1. Add a new viewer using the Add New Viewer command on the ArcGlobe user interface (UI).  
1. Click the tool to open the form and select the viewer that you want to work with.  
1. Select the way you want to tie the new viewer to the main viewer; either TopDown or Synced up.  
1. Navigate on the main viewer and see how the secondary viewer is automatically updated.  
1. Note that the Top Down view works only in the surface mode. Therefore, if you choose the Top Down option while you are in the global navigation mode, until you change to local navigation mode, the secondary viewer is in sync with the main viewer.  
1. If the form is closed, the secondary viewers are no longer tied to the main viewer.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: 3D Analyst | ArcGIS Desktop Basic: 3D Analyst |  
| ArcGIS Desktop Standard: 3D Analyst | ArcGIS Desktop Standard: 3D Analyst |  
| ArcGIS Desktop Advanced: 3D Analyst | ArcGIS Desktop Advanced: 3D Analyst |  


