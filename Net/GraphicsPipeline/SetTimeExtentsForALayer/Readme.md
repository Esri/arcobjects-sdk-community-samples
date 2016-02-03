##Set the time extents for a layer then render the layer

###Purpose  
This sample shows how to take a normal feature layer and make it time aware, then set the temporal extents on the map.  


###Usage
1. Open the Visual Studio solution file and build the project. This will create a .dll and a type library (.tlb) file in the \bin folder. The .dll gets registered with the ESRI Add-Ins component category.   
1. Start ArcMap and load the BasicHurricanes.mxd map in the folder <Your ArcGIS Developer Kit Install directory>/Samples/data/Time.  
1. Open the Customize dialog box. On the Commands tab, select the Add-In Controls. The Add-In Controls appear, including the Set Time Extents button.   
1. Drag the Set Time Extents button onto one of the toolbars and close the Customize dialog box.  
1. Double-click atlantic_hurricanes_2000 to open the layer's properties.  
1. Click the Time tab. Note that the Enable time on this layer check box is not selected and none of the details on the time data are completed.  
1. Click OK to close the properties panel.  
1. Click the Set Time Extents button.  
1. Double-click atlantic_hurricanes_2000 to open the layer's properties.  
1. Note that the layer is now time-enabled. All of the details have been completed and the Enable time on this layer check box is selected.  
1. Click OK to close the properties panel.  
1. Click the Time slider button and try dragging the slider to a particular time on the timeline. Note that the map updates with the content appropriate for that point in time.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


