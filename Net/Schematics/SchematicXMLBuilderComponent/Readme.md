## Implementing an XML builder external component

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates the implementation of ISchematicXmlGenerate and ISchematicXmlUpdate to automatically create the input Extensible Markup Language (XML) data to generate and update diagrams based on the XML builder. The creation of the input XML data is based on the features in the GISForXMLSample geodatabase. This component can be used to generate and update XML builder diagrams from features contained in this geodatabase when they are currently selected on a map document. </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Building the component  
1. Start Visual Studio, open the solution file, and build the component.  

#### Copying the sample database in a working folder  
1. Navigate to <ArcGIS DeveloperKit install location>\Samples\data\Schematics  
1. Copy both the GISForXMLSample.gdb geodatabase and the GISForXMLSample.mxd file in a folder for which you have full rights.  

#### Configuring the schematic dataset  
1. Start ArcCatalog.  
1. Browse to the GISForXMLSample geodatabase you have just copied.  
1. Right-click the GISForXMLSample_Schematic schematic dataset contained in this geodatabase and click Edit. The Schematic Dataset Editor opens.  
1. Click the XMLDiagrams diagram template in the Dataset Editor tree.  
1. On the Properties tab on the right, click the Schematic Builder Properties button. The Schematic Builder Properties dialog box opens.  
1. Type MyExtXMLComponentCS.XMLDocImpl or MyExtXMLComponentVB.XMLDocImpl (depending on the language you're using) in the Generate and Update fields.  
1. Check the Initialize link vertices and Automatic schematic feature class creation options.  
1. Click OK.  
1. Click Save and exit the Schematic Dataset Editor.  

#### Testing the XML builder diagram generate and update functions  
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







#### See Also  
[ISchematicXMLGenerate interface](http://desktop.arcgis.com/search/?q=ISchematicXMLGenerate%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicXMLUpdate interface](http://desktop.arcgis.com/search/?q=ISchematicXMLUpdate%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Schematics | ArcGIS Desktop Basic: Schematics |  
| ArcGIS Desktop Standard: Schematics | ArcGIS Desktop Standard: Schematics |  
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


