##Automate ArcGIS for Desktop applications

###Purpose  
This sample shows how to automate ArcGIS for Desktop applications (ArcMap, ArcScene, and ArcGlobe) from stand-alone .NET applications. To programmatically start a new application session, you can use the new operator to create a new Document object (MxDocument, SxDocument, or GMxDocument). Once you successfully create the new Document object, a new application session is started.  The stand-alone application also allows you to add layers to the new session of the application. IObjectFactory is used to create the new layers inside the application's process space. In addition, it shows how to programmatically clean up and shut down the application properly, including how to handle the case when the application has been closed by the user manually (with AppROT event).   


###Usage
####Automating an ArcGIS for Desktop application  
1. Open the solution file in Visual Studio and compile the sample.  
1. Run DesktopAutomationCS.exe or DesktopAutomationVB.exe depending on the language version.  
1. Select ArcMap, ArcScene, or ArcGlobe from the application drop-down list and click Start to start a new application session. Notice the change in the state of the Start (becomes unavailable) and the Exit (becomes available) buttons.  
1. Type the path of a valid shapefile in the text box, and click Add Layer to add it to the new application session.  
1. Click Exit to shut down the application session. Notice the change in the state of the Start (becomes available) and the Exit (becomes unavailable) buttons.  

####Handling modal dialog boxes when exiting the application  
1. Start a new application session.  
1. Open a modal dialog box of the application; for example, click the Add Data button.  
1. With the Add Data dialog box displayed, click Exit to shut down the application session.  

####Handling the manual application shutdown in an automation session  
1. Start a new application session.  
1. Close the application manually by clicking File, then clicking Exit or click the Close (X) button on the title bar. Notice the change in the state of the Start (becomes available) and the Exit (becomes unavailable) buttons.  







####See Also  
[Automating ArcGIS for Desktop applications](http://desktopdev.arcgis.com/search/?q=Automating%20ArcGIS%20for%20Desktop%20applications&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


