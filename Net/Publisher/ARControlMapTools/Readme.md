##ArcReaderControl map tools

###Purpose  
This sample demonstrates how to set the IARControl.CurrentTool property to the MapZoomIn, MapZoomOut, or MapPan tool to navigate around the map view or any of the data frames when the current view is a PageLayout.  The OnAfterScreenDraw event uses the IARMap.CanUndoExtent and CanRedoExtent properties to determine whether the IARMap.UndoExtent and RedoExtent methods can be used to undo or redo extent changes. The IARMap.SetExtent method is set to the result of the IARMap.GetFullExtent method to zoom to the full extent of the data in the map.   


###Usage
1. Compile and run the sample from the provided code.   
1. Enter a valid file path of a Published Map File (PMF) from ArcMap and load.   
1. Click the Map button to display the data view.   
1. Use the tools to navigate around the focus map.   
1. Click the Layout button to display the layout view.  
1. Use the tools to navigate the data in any of the data frames on the page layout.   









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Publisher | ArcReader |  
| ArcGIS for Desktop Standard: Publisher | Engine |  
| ArcGIS for Desktop Advanced: Publisher | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


