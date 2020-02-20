## Move a graphic along a path in ArcMap

This sample implements the move-object-along-path functionality for ArcMap animation. It allows you to move a point graphic element or a text element along a selected line feature or a selected line graphic. A new animation type (Map Graphic animation type) and its corresponding keyframe (Map Graphic keyframe) are implemented to support moving a graphic along the path. It also implements an ArcMap command for creating a move-object-along-path animation. The purpose of this sample is to show how to create a custom animation type in ArcMap and how to implement an ArcMap command to import the animation tracks. To create a custom animation type, implement a new keyframe class and a new animation type class.   


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Map
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
#### Add and use the move-graphic-along-path functionality in ArcMap  
1. Compile the sample project in Visual Studio.  
1. Start ArcMap and click Customize, Toolbars, and Animation. The Animation toolbar appears.  
1. Click Customize, then Customize Mode. The Customize dialog box appears.  
1. On the Commands tab, locate the Animation Developer Samples category.  
1. Drag the Move Graphic along Path command from the Animation Developer Samples category to the Animation drop-down on the Animation toolbar. Close the Customize dialog box.  
1. Add a line feature layer on to the table of contents (TOC) in ArcMap; or alternatively, draw a line graphic element on the ArcMap display.  
1. Draw a point graphic element (or a text element) on the display and symbolize it as necessary.  
1. Select the point graphic element (or the text element) and a line feature (or line graphic). The Move Graphic along Path command is enabled.  
1. On the Animation drop-down, click the Move Graphic along Path command.  
1. Accept all default settings and click the Import button on the Move Graphic along Path dialog box to create a track.  
1. Play the animation and the point graphic element (or text element) moves along the selected path.  





#### Additional information  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">If more then one line feature (or line graphic) is selected, only the first one is used as the path of the movement.</div>  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">You can elect to show the trace of the moving graphic element by selecting the Trace path option on the Move Graphic along Path dialog box when you create the track. </div>  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">You can change the Trace path option after a track is created by opening the Animation Manager and selecting the track on the Tracks tab. Click Properties, then click the Graphic Track Properties tab. The Trace path option is available on this tab.</div>  
<div style="PADDING-RIGHT: 0in; MARGIN-TOP: 0in; PADDING-LEFT: 0in; MARGIN-BOTTOM: 0pt" xmlns="http://www.w3.org/1999/xhtml">
  <span>You can change the symbology for the point graphic element (or text element) and the line graphic representing the trace on the ArcMap display window.</span>
</div>  




---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


