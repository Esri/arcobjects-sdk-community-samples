## Simple logging dockable window with a custom context menu

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to create a custom dockable window for use in ArcGIS Desktop applications. A dockable window is a window that can exist in a floating state or be attached to the main application window. The table of contents in ArcMap is an example of a dockable window.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Use the IDockableWindowDef interface to define a custom dockable window to be created by the application. This interface allows you to set properties and define the child control window. The class you create is the definition of the window, not the actual dockable window object you interact with in the application. The application uses the definition of the dockable window in your class to create the actual dockable window, accessed by the IDockableWindow interface. You can query properties of the dockable window, such as caption, name, and visibility, as well as user-defined data.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The dockable window in this sample contains a list box for message logging, and the list box is exposed to other components via the IDockableWindowDef.UserData property. Other components can access the list box by the IDockableWindow.UserData property. A command that toggles the visibility of the dockable window is also included.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample also demonstrates different ways to provide a custom context menu that manipulates message items in the list box of the custom dockable window. You can either use the pure .NET Windows Forms ContextMenuStrip class or an ArcGIS framework context menu to implement the context menu. In general, the ContextMenuStrip class is less complicated to implement since the context menu code can be contained in the same class as the dockable window. However, it has a slightly different look and feel than the standard ArcGIS context menu and cannot be interactively customized with the Customize dialog box. </div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open and compile the sample project in Visual Studio. If needed, set up a debug application.  
1. Open ArcCatalog, ArcMap, ArcScene, or ArcGlobe. Drag and drop the Show/Hide Log Window (VB .NET) or Show/Hide Log Window (C#) toggle command from the Customize dialog box under the .NET Samples category onto the application.  
1. Click the command to show the dockable window.   
1. Dock the window at various locations on the application window.   
1. Float the dockable window.  
1. Insert text into the list box by typing in the text box and press Enter.  
1. In the Context Menu Option panel at the bottom of the dockable window, select the Pure .NET (Windows Forms) radio button and right-click the list box. A context menu from the Windows Forms ContextMenuStrip class is displayed.   
1. Switch to the predefined (ArcObjects) option and right-click the list box again. A standard ArcGIS Desktop context menu is displayed.   
1. This predefined context menu can be customized interactively in the Customize dialog box. Click the Toolbars tab, check Context Menu and expand to show Logging Window Context Menu (VB .NET) or Logging Window Context Menu (C#). Reverse the order of the commands. After closing the dialog box, right-click the list box again to see your interactive change.  
1. Switch to the Dynamic (ArcObjects) option and right-click the list box. It displays a similar ArcGIS Desktop context menu that is created dynamically at run time that is not customizable.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


