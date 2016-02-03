##Dynamic heads up display

###Purpose  
This sample demonstrates a combination of the Dynamic Display application programming interface (API) drawing and pure OpenGL drawings in the same tool. The sample shows the map's azimuth as a heads up display (HUD), which updates automatically when the map rotates. When you are rotating the map, the numbers showing in the HUD are different than the numbers reported by the rotation tool, because the reported map rotation is the mathematical angle, while the HUD shows the angle from the north (the map's azimuth).   


###Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the DynamicDisplayHUD class library.  
1. Use this command in an application with a MapControl and a ToolbarControl, such as the MapControlApplication template included with the Visual Studio integration.  
1. Add both commands to the ToolbarControl. The commands can be found in the .NET Samples category with the names, Dynamic HUD and Toggle dynamic mode.  
1. Run the application, add, then click the Toggle dynamic mode command to switch into dynamic mode.   
1. Click Dynamic HUD to add the HUD to the map.  
1. Use the rotation tool to rotate the map (the HUD changes while the map rotates).  







####See Also  
[Dynamic display](http://desktop.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display compass](../../../Net/GraphicsPipeline/DynamicDisplayCompass)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic logo](../../../Net/GraphicsPipeline/DynamicLogo)  
[How to create a dynamic glyph from a marker symbol](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


