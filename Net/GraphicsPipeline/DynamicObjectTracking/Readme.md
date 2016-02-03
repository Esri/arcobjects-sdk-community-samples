##Dynamic display—tracking dynamic object

###Purpose  
This sample shows how to use the ArcGIS dynamic display to track a dynamic object and simulate a tracking mode where the map is centered and aligned according to the tracked object. The following items are discussed in this sample:Switching the display into dynamic mode
Updating the map visible extent and rotation according to information for the tracked object
Listening to the AfterDynamicDraw event to show the location of the tracked objectCreating dynamic glyphs from in-memory bitmaps
Changing different characters of the dynamic symbol The tracking data for this sample gets created synthetically out of an existing feature class. This process might take time before the sample starts running.   


###Usage
1. Start Visual Studio and open the solution file.  
1. Build the solution to make the Track Dynamic Object command .dll file. Use the command in an application with a MapControl and ToolbarControl, such as the MapControlApplication template included with the Visual Studio integration.  
1. Add this command to a ToolbarControl. The command (Track Dynamic Object) is located in the .NET samples category.  
1. Add the provided data to the MapControl. Make sure the file type is set to Shape files (*.shp).  
1. Run the application containing the MapControl and ToolbarControl, then zoom in an area on the roads. The application loads the navigation information from the target feature class and starts navigating (this procedure can take a few seconds).  
1. Click Track Dynamic Object to start tracking.  







####See Also  
[Dynamic display](http://desktopdev.arcgis.com/search/?q=Dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How dynamic display works](http://desktopdev.arcgis.com/search/?q=How%20dynamic%20display%20works&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Best practices for using dynamic display](http://desktopdev.arcgis.com/search/?q=Best%20practices%20for%20using%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Walkthrough: Getting started with dynamic display](http://desktopdev.arcgis.com/search/?q=Walkthrough%3A%20Getting%20started%20with%20dynamic%20display&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[Sample: Dynamic display layer](../../../Net/GraphicsPipeline/MyDynamicLayer)  
[Sample: Dynamic display compass](../../../Net/GraphicsPipeline/DynamicDisplayCompass)  
[How to create a dynamic glyph from a marker symbol](http://desktopdev.arcgis.com/search/?q=How%20to%20create%20a%20dynamic%20glyph%20from%20a%20marker%20symbol&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


