##View CAD data in a MapControl

###Purpose  
This sample provides a stand-alone application containing a MapControl that can be used to view computer-aided design (CAD) data. When compiled, the application can be associated with the most common CAD file name extensions (.dxf, .dwg, and .dng) so that this will be the default application for opening files of these types. When opened, the application allows the user to view the data including the ability to zoom and pan.   


###Usage
1. Edit the CADFiles.REG file, replacing the two instances of the path to the sample executable to the actual path of the sample executable on your machine; for example, C:\Program Files\ArcGIS\DeveloperKit<version number>\Samples\ArcObjectsNET\MapControlCADViewer\CSharp\CADView.exe.   
1. Double-click this .reg file to merge its contents into the registry. Note that merging this data into the registry may overwrite existing file associations for these file types.   
1. Select any CAD file (.dxf, .dwg, or .dgn)—for example, in Windows Explorer—and right-click the file.   
1. Choose View. The application opens to display the CAD file you selected inside the MapControl.   
1. Zoom in on the CAD data by dragging a rectangle using the left mouse button and pan around using the right mouse button.   







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


