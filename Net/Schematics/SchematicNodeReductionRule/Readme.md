## Reducing schematic nodes and computing a cumulative attribute via a schematic rule

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Several schematic node reduction rules are provided with this version to simplify schematic diagrams content by removing some specific schematic nodes while preserving the topology. You can also develop your custom schematic node reduction rule by implementing the IComPropertyPage interface to create the rule property page, and the new ISchematicRule and ISchematicRuleDesign interfaces. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
    <span style="FONT-SIZE: 10pt">
      ****
    </span> </div>
  <div style="LINE-HEIGHT: 12pt; MARGIN-TOP: 0in; PADDING-RIGHT: 0in; MARGIN-BOTTOM: 0pt; FONT-SIZE: 10pt" xmlns="http://www.w3.org/1999/xhtml">
    <span style="FONT-SIZE: 10pt">
      **Caution:** If you plan to implement a custom schematic rule to be executed via a client-server application, the solution must implement two projects; one for the custom rule property page and the other for the custom rule execution itself. The component dedicated to the custom rule execution will have to be registered with the ArcGIS server to be used on any client-server application.</span>
  </div>
  <div style="LINE-HEIGHT: 12pt; MARGIN-TOP: 0in; PADDING-RIGHT: 0in; MARGIN-BOTTOM: 0pt; FONT-SIZE: 10pt" xmlns="http://www.w3.org/1999/xhtml">
    <span style="FONT-SIZE: 10pt"></span> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample details a customized schematic node reduction rule implementation. The goal of this rule is as follows:</div>

*   Reducing schematic nodes with two connections, implemented by a specific schematic feature class. The rule works on nodes that connect only two links.
*   Preserving the initial topology by creating super spans based on a given schematic link feature class. A super span link is created to replace the two "reduced" links that initially connect a reduced node.
*   Cumulating values of a given attribute brought by the two "reduced" links on the super span attribute.  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
Organization:          Esri, http://www.esri.com
Date:                  10/17/2019
ArcObjects SDK:        10.8
Visual Studio:         2017, 2019
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open the solution file and build the project. Note that this step automatically registers the components.  
1. Start ArcCatalog.  
1. Browse to any geodatabase that contains a schematic dataset.  
1. Right-click this schematic dataset entry and select Edit. The Schematic Dataset Editor starts.  
1. Select any diagram template entry that works with the Standard builder or create a diagram template that works with the Standard builder.  
1. Click the Rules tab.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select the new custom node reduction rule from the Type drop-down list (that is, select Node Reduction Rule C# or Node Reduction Rule VBNet).  
1. Click Rule Properties on the Rules tab toolbar. The custom rule properties page appears.  
1. Select the node schematic feature class (that implements the nodes reduced during the rule execution) in the Select the schematic class to reduce drop-down list.  
1. In the Target superspan link section, select the link schematic feature class (that automatically implements the schematic links created to preserve the topology during the rule execution) in the Select the schematic class drop-down list .  
1. Add the name of the attribute that needs to be computed during the rule execution in the Cumulative attribute name text box. This attribute must exist with the same name on the schematic feature classes related to the links that connect the reduced nodes (that is, it must exist for each "reduced" link) and on the superspan link schematic feature class.  
1. Select the Keep vertices check box if you want to report the original path of the reduced links to the superspan link.  
1. Click Save to save the schematic dataset's new parameters.  
1. Start ArcMap and generate or update a schematic diagram from the diagram template (which the definition has just been modified). The custom rule will be executed.  







#### See Also  
[ISchematicRule interface](http://desktop.arcgis.com/search/?q=ISchematicRule%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicRuleDesign interface](http://desktop.arcgis.com/search/?q=ISchematicRuleDesign%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Schematics | ArcGIS Desktop Basic: Schematics |  
| ArcGIS Desktop Standard: Schematics | ArcGIS Desktop Standard: Schematics |  
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


