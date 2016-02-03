##Custom selection extension

###Purpose  
This sample demonstrates how to create a complete add-in. The sample is based around a selection theme but its purpose is to demonstrate multiple add-in components working together. This add-in contains an extension, a dockview, a toolbar with several buttons and tools, and a menu. All components are disabled until the extension is selected on the Extensions dialog box. The extension does not load until it needs to (it delay loads). Similarly, the dockview only gets created once the Toggle DockView button is clicked.  This sample has also been localized to Chinese(PRC).  


###Usage
1. In Visual Studio, click the Build menu and click Build Solution.  
1. Load some data in ArcMap.  
1. Click the Customize menu, click Toolbars, then choose the Selection Add-In toolbar. (See the second screen shot for the Selection AddIn toolbar.)  
1. Click the Customize menu and click Extensions. Select the Selection Sample Extension check box on the Extensions dialog box. (See the first screen shot.)  
1. Make a selection using one of the tools.  
1. Optionally, use the combo box to choose target selection layers.  
1. Optionally, click the Zoom to layer menu to zoom to a selected layer.  
1. The dockable window will report selected features count. (See the second screen shot for the highlighted dockable window.)  
1. Use the toggle button to toggle the dockable window. The button is highlighted in a yellow box on the second screen shot.  
1. To see the localized version, change the format to Chinese(PRC) on the Regional and Language Options dialog box (accessed from the Control Panel).  



![Screen shot of the Extensions dialog box.](images/pic1.png)  
Screen shot of the Extensions dialog box.  
![Screen shot of ArcMap showing the custom toolbar and dockable window.](images/pic1.png)  
Screen shot of ArcMap showing the custom toolbar and dockable window.  






---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


