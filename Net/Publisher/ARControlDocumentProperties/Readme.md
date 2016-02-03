##ArcReaderControl document properties

###Purpose  
This sample demonstrates how to use the IARControl.ShowARWindow method to display the FileProperties modal window when the Browse dialog box is used to select a document to load into the control. The DataFrame and Layer properties modal windows are displayed by right-clicking a map or layer in the table of contents (TOC) to display the context menu and selecting Properties. The IARControl.TOCVisible property is used to toggle the visibility of the TOC if the loaded document was published with permission to do so.  When the TOC is invisible, the DataFrame and Layer properties cannot be displayed manually. Instead, the IARPageLayout.ARMap property is used to iterate each map and obtain its Description, DistanceUnits, Name, and SpatialReferenceName properties. The IARMap.ARLayer property is used to iterate each layer in the map and obtain its Description, Name, MaximumScale, and MinimumScale properties. These properties are concatenated into a string, which is displayed in a RichTextBox.  


###Usage
1. Start Visual Studio, open the solution file, and build the project.   
1. Run the application.  
1. Browse to a Published Map File (PMF) from ArcMap to load.  
1. Display the file properties.   
1. Right-click a map in the TOC to display the data frame properties.  
1. Right-click a layer in the TOC to display the layer properties.   
1. Hide the TOC.   
1. Display the map and layer properties.   









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Publisher | ArcReader |  
| ArcGIS for Desktop Standard: Publisher | Engine |  
| ArcGIS for Desktop Advanced: Publisher | ArcGIS for Desktop Basic |  
|  | ArcGIS for Desktop Standard |  
|  | ArcGIS for Desktop Advanced |  


