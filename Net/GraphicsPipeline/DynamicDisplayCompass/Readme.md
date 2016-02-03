##Dynamic display compass

###Purpose  
This sample demonstrates drawing on the map in dynamic mode using an OpenGL application programming interface (API). A command is implemented that adds a compass object to the map, which rotates along together with the dynamic map. The following issues are covered in this sample: Wiring dynamic map events to listen to the Before or After DynamicDraw, which allows you to plug in your drawing to the map.Creation of OpenGL display lists.Mapping a bitmap into an OpenGL texture and binding the texture to an OpenGL geometry.Translating, scaling, and rotating OpenGL display lists.   


###Usage
1. Start Visual Studio and open the solution.  
1. Build the solution to make the Dynamic Display Compass command .dll file. Use this command in an application with a MapControl and a ToolbarControl, such as the MapControlApplication template included with the Visual Studio integration.  
1. Add the command to the ToolbarControl. The command can be found in the .NET Samples category with the name, Dynamic Display Compass.  
1. Run the application and click the Dynamic Display Compass command to add the compass object to the map. The map switches into dynamic mode and adds the compass to the map. The compass rotates with the map when rotated.  







####See Also  
[How dynamic display works](http://desktop.arcgis.com/search/?q=How%20dynamic%20display%20works&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Rendering dynamic map content](http://desktop.arcgis.com/search/?q=Rendering%20dynamic%20map%20content&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to create a dynamic glyph from a marker symbol](http://desktop.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to plug in dynamic drawing](http://desktop.arcgis.com/search/?q=How%20to%20plug%20in%20dynamic%20drawing&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Limitations for dynamic display](http://desktop.arcgis.com/search/?q=Limitations%20for%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display—tracking dynamic object](../../../Net/GraphicsPipeline/DynamicObjectTracking)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic heads up display](../../../Net/GraphicsPipeline/DynamicDisplayHUD)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


