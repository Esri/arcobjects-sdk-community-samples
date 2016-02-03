##Draw text on a MapControl

###Purpose  
This draw text sample application demonstrates the splined text rendering capabilities of ArcObjects. When you click the map to track a line, the MapControl method DrawText is used to place the text within the text box along the tracked line. At least two clicks are required for the line and text to appear on the MapControl.   


###Usage
1. Type the text to annotate the map.   
1. Use the left mouse button to trace a line on the map, and use the right mouse button to zoom in.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The application uses the AddLayerFromFile method to load the world continents sample data, which is then symbolized. The OnClick event either uses the TrackRectangle method to zoom in (if the right or middle mouse button has been used) or adds the new point to the line and updates the display through the Refresh method. The Refresh method is used to draw the esriViewForeground, thereby removing any previously drawn annotation and triggering the MapControl's OnAfterDraw event. The OnAfterDraw event uses the DrawShape and DrawText methods to draw the line and text onto the MapControl in response to the refresh of the esriViewForeground phase. </div>  


####See Also  
[MapControl class](http://desktopdev.arcgis.com/search/?q=MapControl%20class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[IMapControl2 interface](http://desktopdev.arcgis.com/search/?q=IMapControl2%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


