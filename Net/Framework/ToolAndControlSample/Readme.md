## Custom feedback tool and font drop-down list tool control

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to implement a tool and a tool control.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The ToolSample class is an implementation of a tool that tracks mouse events with a circle display feedback in the map and page layout views, including the magnifier and viewer window. It also shows how to track key press events to change the color of the display feedback.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The FontToolControl class is an implementation of a tool control with a drop-down list of all installed fonts. It also shows how to write code to perform custom drawing with .NET Windows Form controls. When you select a font item from the list, the font is drawn. An advanced implementation is provided to allow the custom tool control to set the document default font similar to the out-of-the-box Font drop-down list on the Draw toolbar. For more information, see step 8 in this sample. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Framework
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and compile the sample project in Visual Studio. If necessary, set up ArcMap as the debug application.  
1. Start ArcMap, click the Customize menu, and click Customize Mode. The Customize dialog box opens.   
1. Select the .NET Samples category, then drag and drop the FeedBack tool (C#) or Feedback tool (VB .NET) for the sample tool on to a toolbar.  
1. Activate the tool and click the map to start the new circle feedback. Click again to finish the feedback.  
1. Press the Ctrl key to randomly change the color of the feedback. The Feedback tool also works in the magnifier or viewer window and on the page layout view.   
1. Drag and drop the sample Font drop-down (C#) and Font drop-down (VB.NET) tool control onto a toolbar from the Customize dialog box under the .NET Samples category.  
1. Click the Font drop-down list and scroll through the items. The selected item is drawn with the font.  
1. Optionally, to use this custom tool control in a similar way as the default Font drop-down list on the Draw toolbar, uncomment the code in the ICommand.OnCreate implementation and recompile. The control listens to document events and is updated according to the default document text font.  







#### See Also  
[How to create a command with a custom interface ToolControl](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20command%20with%20a%20custom%20interface%20ToolControl&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Creating commands and tools](http://desktop.arcgis.com/search/?q=Creating%20commands%20and%20tools&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: ArcScan | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


