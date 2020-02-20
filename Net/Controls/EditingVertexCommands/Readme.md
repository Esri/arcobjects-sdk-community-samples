## Custom vertex editing commands

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample provides four tools to insert or delete vertices from an edit sketch and shows the following approaches.</div>

*   Using fine grained ArcObjects
*   Using containment to call out-of-the-box commands
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">It also shows how to create sketch operations. All the tools can be used in conjunction with the out-of-the-box ArcGIS Engine editing commands. The sample assumes previous experience in creating custom tools and commands. </div>  


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
1. Start editing.  
1. Zoom in on a polyline or polygon feature.  
1. Select a feature with the edit tool.  
1. Try out the vertex tools. To delete a vertex, select one of the Delete Vertex tools and click an existing vertex. To insert a vertex, select one of the Insert Vertex tools and click a selected feature.   
1. Right-click and finish the sketch.  
1. Stop editing and save edits to persist changes.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The CustomVertexCommands class and the UsingOutOfBoxVertexCommands class each contain the following tools for vertex management:</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The InsertVertex tool creates a vertex at the clicked location while DeleteVertex deletes the closest vertex. Both classes have the following in common:</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The CustomVertexCommands class uses fine grained ArcObjects to perform the vertex operations. The OnMouseUp event is used to transform the clicked location from screen coordinates to map coordinates using the IDisplayTransformation.ToMapPoint method. </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The IHitTest.HitTest method is then used on the edit sketch geometry to identify the part of the geometry closest to the click location. A search tolerance is used to determine whether the mouse click is too far from the selected feature to be valid. InsertVertex identifies the segment index of the nearest boundary, which is then used with the IPointCollection.AddPoint method to create a vertex. DeleteVertex identifies the index of the nearest vertex, then deletes it by calling IPointCollection.DeletePoints.  </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The UsingOutOfBoxVertexCommands class contains the following out-of-the-box ControlsCommands:</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The location of the vertex operation is set by calling IEditSketch.SetEditLocation. The commands are instantiated by using System.Activator.CreateInstance, initialized with OnCreate by passing in the hook, and executed with OnClick.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The approach taken in the CustomVertexCommands class provides greater flexibility programmatically, whereas the UsingOutOfBoxVertexCommands class is more concise with less code to maintain. </div>  


#### See Also  
[EngineEditorClass](http://desktop.arcgis.com/search/?q=EngineEditorClass&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


