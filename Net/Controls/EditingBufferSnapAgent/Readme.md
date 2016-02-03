##Buffer snap agent

###Purpose  
This sample demonstrates how to create a snapping agent based on a buffer around the points of the current editable point feature class. In this sample, a snapped vertex of a feature is located no closer than 1000 map units from the point that is closest to the cursor. For electrical datasets, it is common to want to snap primary lines to the boundary of the symbol representing a pole. Use this snapping agent with an appropriate buffer distance (based on the radius of the pole symbol).  


###Usage
1. Open and build the solution to register the BufferSnap class in the ESRI Engine Snap Agents component category.  
1. Run the solution.  
1. Start editing.  
1. Zoom in on some point features to a 1:30000 map scale.  
1. Open the Snapping Environment dialog box and turn on the Buffer Snap agent.  
1. Begin adding new features to snap to buffers (1000 map units) around the point features in the specified feature class.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| Engine Developer Kit | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  
|  | Engine |  


