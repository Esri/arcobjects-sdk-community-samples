## ArcReaderControl document properties

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates how to use the IARControl.ShowARWindow method to display the FileProperties modal window when the Browse dialog box is used to select a document to load into the control. The DataFrame and Layer properties modal windows are displayed by right-clicking a map or layer in the table of contents (TOC) to display the context menu and selecting Properties. The IARControl.TOCVisible property is used to toggle the visibility of the TOC if the loaded document was published with permission to do so. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">When the TOC is invisible, the DataFrame and Layer properties cannot be displayed manually. Instead, the IARPageLayout.ARMap property is used to iterate each map and obtain its Description, DistanceUnits, Name, and SpatialReferenceName properties. The IARMap.ARLayer property is used to iterate each layer in the map and obtain its Description, Name, MaximumScale, and MinimumScale properties. These properties are concatenated into a string, which is displayed in a RichTextBox.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Publisher
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
1. Start Visual Studio, open the solution file, and build the project.   
1. Run the application.  
1. Browse to a Published Map File (PMF) from ArcMap to load.  
1. Display the file properties.   
1. Right-click a map in the TOC to display the data frame properties.  
1. Right-click a layer in the TOC to display the layer properties.   
1. Hide the TOC.   
1. Display the map and layer properties.   









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Publisher | ArcReader |  
| ArcGIS Desktop Standard: Publisher | Engine |  
| ArcGIS Desktop Advanced: Publisher | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


