## Schematic diagram auto refresh

  <div xmlns="http://www.w3.org/1999/xhtml">
    <div>
      <span class="PropertyValue">This sample is an ArcMap add-in that shows how to create a custom button that turns the automatic refresh feature on and off in a schematic diagram.</span>
    </div>
  </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
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
#### Building the component  
1. Open the solution file and build the project.  

#### Configuring schematic attributes to be evaluated at On Start Editing  
1. Start ArcCatalog and edit a schematic dataset with a diagram template configured to work from a geometric network.  
1. Check the On Start Editing evaluation mode for any Associated Field schematic attribute that is used to label or symbolize the schematic features in the schematic diagrams.  
1. Save the schematic dataset and exit the Schematic Dataset Editor.  

#### Adding the custom AutoRefresh button to the Schematic Editor toolbar  
1. Start ArcMap with a new empty map.  
1. Click Customize and click Customize Mode.  
1. Click the Commands tab and click AutoRefresh (VBNet) or AutoRefresh (C#) in the Categories list.  
1. Drag and drop the AutoRefresh command onto the Schematic Editor toolbar.  

#### Testing auto refresh in the schematic diagram  
1. In the Layers data frame, add the geometric network data with which your diagram template works.  
1. Open a schematic diagram containing schematic features that were affected by the changes made during the On Start Editing evaluation, or generate a schematic diagram.  
1. Click Schematic Editor and click Start Editing Diagram on the Schematic Editor toolbar.  
1. Click the AutoRefresh (VBNet) or AutoRefresh (C#) button. The Schematic Auto Refresh Properties dialog box opens.  
1. Check the Auto Refresh On option, configure the time interval parameter values, then close the dialog box.  
1. In the active schematic diagram, select a schematic feature impacted by the changes made during the On Start Editing evaluation.  
1. Click Propagate Schematic Selection To Map on the Schematic toolbar. The associated feature is automatically selected in the Layers data frame.  
1. Activate this data frame and zoom in on the selected geographic information system (GIS) feature.  
1. On the Editor toolbar, click Editor and click Start Editing.  
1. Change some attribute values for the selected GIS feature.  
1. Activate the data frame containing the schematic diagram. After the specified time interval, all schematic attributes configured during the On Start Editing evaluation have been automatically re-evaluated. They reflect the changes you made to the associated GIS feature attributes. If these schematic attributes are used to label or symbolize the schematic feature, the impact is visual; the symbol or the label has been automatically changed.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Schematics | ArcGIS Desktop Basic: Schematics |  
| ArcGIS Desktop Standard: Schematics | ArcGIS Desktop Standard: Schematics |  
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


