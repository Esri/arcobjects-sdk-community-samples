##StreetMap routing

###Purpose  
This sample shows how to utilize the StreetMap routing application programming interface (API). It is an implementation of a simple dialog box allowing you to set from and to stop addresses along with other routing settings, such as calculate a route, display route geometry on the map, and display driving directions text.  


###Usage
1. Open the solution file in Visual Studio and build the project.  
1. Start ArcMap.  
1. Click the Customize menu, click Toolbars, and click Routing Sample to enable the Routing Sample toolbar.  
1. Click the button on the Routing Sample toolbar to open the Routing Sample dialog box.   
1. Browse to the location of a routing service file (.rs extension) and select it in the Routing Service field.  
1. Browse to the location of an address locator file and select it in the Address Locator field.   
1. Type the address of the starting location for driving directions in the Start section and the address of the ending location in the Finish section.  
1. In the Options section, specify the type of route, quickest or shortest, to be calculated. Specify the distance units and select the street type preference for routing using the slider bar; for example, moving the slider towards Use Local Roads causes the router to utilize non-highway roads as much as possible in the output driving directions.  
1. Optional. Click the Restrictions button to open the Restrictions dialog box. If the data referenced by the routing service file specified in step 5 contains restriction attributes, this dialog box will be populated with the available restriction parameters. Set the restrictions in this dialog box as desired.   
1. Optional. If a point feature class containing barrier features is available, browse to it in the Barriers Table field.  
1. Click the Find Route button to generate a list of driving directions and display a route graphic in ArcMap.  
1. Click the Show Directions button to open the Driving Directions dialog box, which contains the list of driving directions.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
|  |  |  


