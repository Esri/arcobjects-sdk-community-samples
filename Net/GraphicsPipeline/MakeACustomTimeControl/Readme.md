##Make a custom time slider

###Purpose  
This sample demonstrates how the map's current time can be controlled using a custom control dialog box. The sample includes code showing how to convert the time values from Windows date and time controls into ArcGIS Time objects. It also shows how you can add and subtract times using the TimeExtent and TimeDuration classes.  


###Usage
1. Open the Visual Studio solution file and build the project. This will create a .dll and a type library (.tlb) file in the \bin folder. The .dll gets registered with the ESRI Add-Ins component category.   
1. Start ArcMap and load the TimeAwareHurricanes.mxd map in the folder <Your ArcGIS Developer Kit Install directory>/Samples/data/Time.  
1. Open the Customize dialog box. On the Commands tab, select the Add-In Controls. The Add-In Controls appear, including the Custom Time Control.   
1. Drag the Custom Time Control button onto one of the toolbars and close the Customize dialog box.  
1. Double-click atlantic_hurricanes_2000 to open the layer's properties.   
1. Click the Time tab. Make sure the Enable Time on this layer check box is selected.  
1. Click OK to close the properties panel.   
1. Click the Custom Time Control button.  
1. Note that only a portion of the hurricane data shows on the map. Drag the slider in the Custom Time Control back and forth to interactively change the time. Try using the date picker in the Custom Time Control to go to a specific date.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


