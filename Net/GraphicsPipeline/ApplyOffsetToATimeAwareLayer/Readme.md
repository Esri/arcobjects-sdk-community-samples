##Applying a time offset to a time-aware feature layer

###Purpose  
This sample demonstrates how to apply an offset to a time-aware layer.  


###Usage
1. Open the Visual Studio solution file and build the project. This will create a .dll and a type library (.tlb) file in the \bin folder. The .dll gets registered with the ESRI Add-Ins component category.   
1. Start ArcMap and load the TimeAwareHurricanes.mxd map in the folder <Your ArcGIS Developer Kit Install directory>/Samples/data/Time.  
1. Open the Customize dialog box. On the Commands tab, select the Add-In Controls. The Add-In Controls appear, including the Time Offset button.   
1. Drag the Time Offset button onto one of the toolbars and close the Customize dialog box.  
1. Double-click atlantic_hurricanes_2000 to open the layer's properties.   
1. Click the Time tab. Note that the time offset is 0.  
1. Click Cancel to close the properties panel.  
1. Click the Time Offset button.  
1. Reopen the properties of the atlantic_hurricanes_2000 layer. Note that the time offset has changed.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


