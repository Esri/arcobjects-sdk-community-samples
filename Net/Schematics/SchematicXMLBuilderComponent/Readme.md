##Implementing an XML builder external component

###Purpose  
This sample demonstrates the implementation of ISchematicXmlGenerate and ISchematicXmlUpdate to automatically create the input Extensible Markup Language (XML) data to generate and update diagrams based on the XML builder. The creation of the input XML data is based on the features in the GISForXMLSample geodatabase. This component can be used to generate and update XML builder diagrams from features contained in this geodatabase when they are currently selected on a map document.   


###Usage
####Building the component  
1. Start Visual Studio, open the solution file, and build the component.  

####Copying the sample database in a working folder  
1. Navigate to <ArcGIS DeveloperKit install location>\Samples\data\Schematics  
1. Copy both the GISForXMLSample.gdb geodatabase and the GISForXMLSample.mxd file in a folder for which you have full rights.  

####Configuring the schematic dataset  
1. Start ArcCatalog.  
1. Browse to the GISForXMLSample geodatabase you have just copied.  
1. Right-click the GISForXMLSample_Schematic schematic dataset contained in this geodatabase and click Edit. The Schematic Dataset Editor opens.  
1. Click the XMLDiagrams diagram template in the Dataset Editor tree.  
1. On the Properties tab on the right, click the Schematic Builder Properties button. The Schematic Builder Properties dialog box opens.  
1. Type MyExtXMLComponentCS.XMLDocImpl or MyExtXMLComponentVB.XMLDocImpl (depending on the language you're using) in the Generate and Update fields.  
1. Check the Initialize link vertices and Automatic schematic feature class creation options.  
1. Click OK.  
1. Click Save and exit the Schematic Dataset Editor.  

####Testing the XML builder diagram generate and update functions  
1. Start ArcMap and open the GISForXMLSample.mxd file from your working folder. This .mxd file references the ElectricDataset feature dataset stored in the copied GISForXMLSample geodatabase.  
1. Start the Schematics extension.  
1. In ArcMap, click Customize, click Toolbars, and choose the Schematic, Schematic Editor, and Schematic Network Analyst toolbars.  
1. Click the Select Features tool on the Tools toolbar and select a set of features on the active data frame.  
1. Click Generate New Schematic Diagram on the Schematic toolbar. The New Schematic Diagram dialog box opens.  
1. Ensure that the GISForXMLSample_Schematic schematic dataset is selected on the Schematic Dataset or Schematic Folder drop-down list and that the XMLDiagrams diagram template is selected on the Schematic Diagram Template drop-down list.  
1. Type a name for the diagram in the Schematic Diagram Name field, for example, XMLDiagramFromSelection1.  
1. Click OK. The XMLDiagramFromSelection1 diagram, based on the selected set of features, is generated and opens in a new data frame.  
1. Activate the Layers data frame containing the ElectricDataset features and select a new set of features.  
1. Click Schematic on the Schematic toolbar and click Update Diagram. The Update Diagram dialog box opens.  
1. Click OK. The active diagram is updated according to the features currently selected on the map.  
1. Select the Propagate Map Selection to Schematic command to confirm that the schematic features contained in the active diagram are correctly associated with the features from which they were created.  







####See Also  
[ISchematicXMLGenerate interface](http://desktopdev.arcgis.com/search/?q=ISchematicXMLGenerate%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicXMLUpdate interface](http://desktopdev.arcgis.com/search/?q=ISchematicXMLUpdate%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Advanced: Schematics |  


