##RSS weather layer

###Purpose  
This sample illustrates a real-life scenario for creating a new layer to consume a Web service and display the information in a map.  This sample shows implementation of the following:Simple editing capabilitiesSelection by attribute and locationPersistenceIdentify  


###Usage
1. Start Visual Studio, open the solution file, and build the project.  
1. Open a MapControl application or ArcMap.  
1. From the toolbars list, select the RSS Weather layer toolbar to add to the application.  
1. Click Load Layer to connect to the RSS weather service.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">To use the layer without using the existing AddRSSWeatherLayer command, you will have to manage the refreshes of the layer. You can do that by listening to the OnWeatherItemAdded and OnWeatherItemsUpdated events or by using a timer to refresh the layer.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The following items are covered in this sample:</div>  


####See Also  
[Writing multithreaded ArcObjects code](http://desktop.arcgis.com/search/?q=Writing%20multithreaded%20ArcObjects%20code&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[How to wire ArcObjects .NET events](http://desktop.arcgis.com/search/?q=How%20to%20wire%20ArcObjects%20.NET%20events&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | Engine |  
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


