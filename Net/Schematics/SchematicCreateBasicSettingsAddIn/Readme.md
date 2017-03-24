## Utility wizard for basic schematic datasets configuration

  <div xmlns="http://www.w3.org/1999/xhtml">This sample, developed as an add-in ArcCatalog extension, is a Utility wizard that can be used to create a schematic dataset in a geodatabase with basic settings on a diagram template that uses geometric network data as the input for generating schematic diagrams. It greatly simplifies the schematic dataset's primary configuration steps for simple cases. It allows the following:</div>

*   Configuration of some basic properties for the Standard builder that will be used to generate the schematic diagrams (optional).
*   Specification of the input geometric network data from which the diagrams will be generated (required). This results in the creation of the associated schematic feature classes in the schematic dataset.
*   Specification of node reduction by priority rules to systematically reduce any orphan nodes and nodes with two connections when those nodes are based on specific schematic feature classes (optional).
*   Specification of the directions for a Hierarchical-Smart Tree layout algorithm to be automatically executed at the diagram generation (optional).
*   Creation of specific schematic attributes associated with fields in the geometric network feature classes (optional).  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Building the component  
1. Start Visual Studio.  
1. Open the solution and build the project.  

#### Adding the custom Create Basic Schematic Settings button to any ArcCatalog toolbar  
1. Start ArcCatalog.  
1. Click Customize and click Customize Mode.  
1. Click the Commands tab, and click Schematic Extras (VBNet) or Schematic Extras (CSharp) in the Categories list.  
1. Drag-and-drop the Create Basic Schematic Settings command onto any toolbar.  

#### Testing the Create Basic Schematic Settings wizard  
1. In the Catalog tree, either select the geodatabase in which you want to create the new schematic dataset with basic settings or select the schematic dataset in which you want to configure a new diagram template.  
1. Click the Create Basic Schematic Settings (VBNet) or Create Basic Schematic Settings (C#) button. The Dataset and Template Name dialog box appears.  
1. If you started from a selected geodatabase, type a name for the new schematic dataset to create in the Dataset Name field. When starting from a schematic dataset, the Dataset Name field is already specified.  
1. Type a name for the diagram template in the Template Name field.  
1. Select Use digitized vertices if you want the schematic links in the diagrams based on the new diagram template to display with their initial vertices.  
1. Click Next. The Select a map document dialog box appears.  
1. Select the map that contains the geographic feature classes from which you want to generate the diagrams based on the new diagram template and click Add. The Select network feature classes to reduce dialog box appears.  
1. If you want a Node reduction by priority rule to be configured on your diagram template for one or several schematic feature classes, select the desired schematic feature class items.  
1. Click Next. The Advanced Options dialog box appears.  
1. In the Algorithm tab, if you want a Smart Tree schematic layout algorithm to be automatically executed at the end of each diagram generation, select the Apply Smart Tree algorithm check box, then configure the desired Direction and Root Feature Class using the drop-down lists.  
1. In the Attributes tab, if you want some schematic associated field attributes to be automatically created on your schematic feature classes, select the Create associated attribute check box. Select the desired schematic feature class on the left part of the form and, on the right part, select the check box related to each desired field item.  
1. Click Done. The process starts. A new schematic dataset containing a new diagram template with the settings you specified is created in the geodatabase.  
1. You can now open the map that was selected in the preceding Step 7, select some features, and generate a new diagram based on the new dataset and/or template.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


