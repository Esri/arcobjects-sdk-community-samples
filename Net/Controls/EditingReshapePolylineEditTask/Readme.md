## Custom reshape polyline edit task

This sample illustrates how to create a custom ArcGIS Engine edit task that can be used in conjunction with the out-of-the-box editing commands. The Reshape Polyline edit task allows the user to perform a reshape on the selected feature within an edit session.   


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
1. Build and run the sample.  
1. Start editing.  
1. Zoom in on a polyline feature that you want to reshape.  
1. Select this feature with the Edit tool.  
1. Select the Reshape Polyline edit task from the Edit Task Tool control.  
1. Using the Sketch tool, digitize a new line that intersects the selected feature in at least two places.  
1. Finish the sketch to perform the reshape.  
1. Stop editing and save edits to persist changes. See the following illustration:  



![Illustration of performing a reshape on the selected feature.](images/pic1.png)  
Illustration of performing a reshape on the selected feature.  


#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The edit task is created by implementing the IEngineEditTask interface. Compiling this sample registers the edit task in the ESRI Engine Edit Tasks component category, which is used to populate the ControlsEditingTaskToolControl at runtime. The position of the task within the ControlsEditingTaskToolControl list is controlled using the IEngineEditTask.GroupName property and the display name using the IEngineEditTask.Name property. The IEngineEditTask.Activate method is called when the end user selects the edit task in the ControlsEditingTaskToolControl. The Activate method is used to set up listeners to the following IEngineEditEvents:</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">These event listeners set the IEngineEditSketch.GeometryType to null if the following conditions are met, thereby, disabling the Sketch tool: </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The IEngineEditLayers.TargetLayer property on the EngineEditor singleton is used to return to the target layer selected in the ControlsEditingTargetToolControl. The IEngineEditTask.OnFinishSketch method is used to reshape the geometry of the selected feature using the IPolyline.Reshape method, passing in a path created from the digitized sketch as an argument. The task calls the IFeature.Store method within an edit operation to commit the changes.</div>  


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


