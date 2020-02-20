## Implementing a schematic digitizing tool

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows how to implement a tool that can be used in ArcMap to digitize schematic features in schematic diagrams. This sample includes a configuration file to work with a specific type of diagram (a diagram in the DigitizingSample schematic dataset). The Extensible Markup Language (XML) configuration file can be modified to work with other schematic datasets as well. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample also shows how to use the Schematic library to create schematic features and how to use an add-in dockable window.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Schematics
Organization:          Esri, http://www.esri.com
Date:                  12/13/2018
ArcObjects SDK:        10.7
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Building the project  
1. Start Visual Studio, open the solution file, and build the project.  

#### Adding the new tool to the ArcMap toolbar and opening the sample schematic diagram  
1. Navigate to <ArcGIS DeveloperKit install location>\Samples\data\Schematics, and copy both the GenericDigitizing.gdb geodatabase and DigitizeProperties.xml file in a folder for which you have full rights.  
1. Start ArcMap and open a new empty map.  
1. Click Customize, and click Customize Mode. The Customize dialog box opens.  
1. Click the Commands tab, select the Schematic Samples category, and drag the DigitTool tool onto the Schematic Editor toolbar.  
1. Click Close on the Customize dialog box.  
1. Click Open Schematic Diagrams on the Schematic toolbar and browse to and select the Crime1 schematic diagram contained in the DigitizingSample schematic dataset in the GenericDigitizing sample geodatabase you copied at step#1.  
1. Click Start Editing Diagram on the Schematic Editor drop-down menu.  
1. Click the schematic digitizing tool (DigitTool). The Schematic Digitize dockable window opens. This window is organized in two sections: the first section concerns the digitizing of schematic nodes, the second section concerns the digitizing of schematic links.  

#### Digitizing schematic nodes  
1. On the Schematic Digitize dialog box's first section, click the Node Type drop-down list  
1. Choose the type of node you want to digitize in the active diagram; for this example, choose Person.  
1. Fill in the Name text box; for example, type PersonA. The name is a mandatory parameter that needs to be specified before digitizing the related node in the active schematic diagram.  
1. Click anywhere in the background of the schematic diagram. The new PersonA schematic node appears at the clicked location.  

#### Digitizing schematic links  
1. On the Schematic Digitize dialog box's second section, click the Link Type drop-down list.  
1. Choose the type of link you want to digitize in the active diagram; for this example, choose Relation.   
1. Using the Relation Type drop-down list, specify the type of relation you want to create. This is a mandatory parameter that needs to be specified before going further. For this example, choose person-person-friend.  
1. Click two persons nodes in the active diagram. A link connecting the two clicked nodes is created.  

#### Stopping the digitizing operations  
1. Click Stop Editing Diagram on the Schematic Editor drop-down menu.   
1. Click Yes to save the edits. The newly digitized schematic features are saved in the schematic dataset.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">If you want to use this add-in tool to digitize schematic features in your own schematic database, you only need to configure the DigitizeProperties.xml file.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Navigate to &lt;ArcGIS DeveloperKit install location&gt;\Samples\data\Schematics, and copy the DigitizeProperties.xml file in the same folder your schematic database is.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Edit this copied DigitizeProperties.xml file with a text editor<font face="Calibri">.</font></div>  
<div xmlns="http://www.w3.org/1999/xhtml">REQUIRED—Set up the node and link schematic feature classes you want to digitize:</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Create a <strong><font face="Courier New">NodeFeature </font></strong>item for each node schematic feature class you want to digitize and type the exact name of the node schematic feature class in your schematic dataset for the <font face="Courier New"><strong>FeatureClassName</strong></font> parameter.<br />For example, if you want to digitize Valves schematic nodes, you must configure a NodeFeature item as follows:<br /><font face="Courier New">&lt;NodeFeature FeatureClassName="Valves"&gt;<br />&lt;/NodeFeature&gt;</font></div>  
<div xmlns="http://www.w3.org/1999/xhtml">Create a <font face="Courier New"><strong>LinkFeature</strong></font> item for each link schematic feature class you want to digitize and type the exact name of the link schematic feature class in your schematic dataset for the <font face="Courier New"><strong><font face="Courier New"><strong>FeatureClassName</strong></font></strong></font> parameter.<br />For example, if you want to digitize PrimaryLines schematic links, you must configure a LinkFeature item as follows:<br /><font face="Courier New"><font face="Courier New">&lt;LinkFeature FeatureClassName="PrimaryLines"&gt;<br />&lt;/LinkFeature&gt;</font></font></div>  
<div xmlns="http://www.w3.org/1999/xhtml">OPTIONAL—Set up the attributes you want to edit for each digitized schematic feature:</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Create a <strong><font face="Courier New">Field </font></strong>item for each attribute you want to be editable through the digitizing form.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Specify the <font face="Courier New"><strong>DisplayName </strong></font>parameter. This is the label that will display for this attribute on the digitizing form.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Specify the <font face="Courier New"><strong>DBColumnName </strong></font>parameter. This is name of the related field in the schematic feature class.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Specify the <font face="Courier New"><strong>Type </strong></font>parameter—<em><font face="Courier New">Text</font></em>, <font face="Courier New"><em>Date</em></font>, <font face="Courier New"><em>Combo </em></font>or <font face="Courier New"><em>MaskText</em></font>. This is the type of attribute values the user will edit.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">With a <em><font face="Courier New">Text</font></em> type, a regular text box will be automatically added to the digitizing form.<br />In this case, when <font face="Calibri"><font face="Verdana">configuring </font></font>the <strong><font face="Courier New">Length </font></strong>parameter, you can also limit the number of characters the user can type in the text box.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">With a <font face="Courier New"><em>Date</em></font> type, a drop down date pick list will be automatically added to the digitizing form.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">With a <font face="Courier New"><em>Combo</em></font> type, a combo box will be automatically added to the digitizing form.<br />In this case, you must also configure a list of <strong><font face="Courier New">Value</font></strong> parameters, for each possible value you want to display in the combo box.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">With a <font face="Courier New"><em>MaskText </em></font>type, a text box will be automatically added to the digitizing form to allow users typing a specific type of text value that will have to verify a specific format. For example, a field that will be used to have the user enter their social security number.<br />In this case, you must provide the expected format—<font face="Courier New"><strong>Mask</strong></font> parameter. For example, "###-##-###". See the Appendix section above for more details on Mask.</div>  
<div xmlns="http://www.w3.org/1999/xhtml">Specify when the attribute is mandatory or not—<font face="Courier New"><strong>Mandatory</strong></font> parameter, <font face="Courier New"><em>True</em></font>  or <em><font face="Courier New">False</font></em>. With Mandatory="True", it forces the user to specify the field value.</div>  
<div style="PADDING-LEFT: 100px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Here are sample parameters to allow the digitizing of nodes based on the Persons schematic feature class and fill some attribute fields for the digitized person:</div>  
<div style="PADDING-LEFT: 100px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;NodeFeature FeatureClassName="Persons" /&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Field DisplayName="Your Name" DBColumnName="NAME" Type="Text" Length="30" Mandatory="True"&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Field DisplayName="Date of Birth" DBColumnName="BIRTH_DATE" Type="Date" Mandatory="True"&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Field DisplayName="Sexe" DBColumnName="SEXE" Type="Combo"</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Value Default="True"&gt;"M"&lt;/Value&gt;</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Value&gt;"F"&lt;/Value&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;/Field&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Field DisplayName="Social Security Number" DBColumnName="SS_NUM" Type="MaskText" Mask=</font>
  <font face="Verdana"> "###-##-###"/&gt;</font>
</div>  
<div style="PADDING-LEFT: 100px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;/NodeFeature&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Relations&gt;</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Relation LinkType="Link1" FromType="Persons" ToType="Persons"&gt;</font>
</div>  
<div style="PADDING-LEFT: 250px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Value&gt;Friend&lt;/Value&gt;</font>
</div>  
<div style="PADDING-LEFT: 250px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Value&gt;Enemy&lt;/Value&gt;</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;/Relation&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;/Relations&gt;</font>
</div>  
<div style="PADDING-LEFT: 100px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Those three parameters must be configured at the end of the XML file:</div>  
<div style="PADDING-LEFT: 100px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;?xml version="1.0" encoding="utf-8" ?&gt;</font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;Properties&gt;</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">…</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">…</font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;MandatoryColor&gt;Yellow<font face="Courier New">&lt;/MandatoryColor&gt;</font></font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;FormName&gt;Digitize Inside Plants<font face="Courier New">&lt;/FormName&gt;</font></font>
</div>  
<div style="PADDING-LEFT: 200px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">&lt;AutoClearAfterCreate&gt;True<font face="Courier New">&lt;/AutoClearAfterCreate&gt;</font></font>
</div>  
<div style="PADDING-LEFT: 150px" xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <font face="Courier New">
    <font face="Courier New">&lt;/Properties&gt;</font>
  </font>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <hr tabIndex="-1" />
  <strong>Appendix—Mask Characters Description</strong>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">
  <div>
    <font size="2">Literal All other symbols are displayed as literals; that is, as themselves.</font>
  </div>
  <div> </div>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml">
  <font size="2">Literal All other symbols are displayed as literals; that is, as themselves.</font>
</div>  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  


#### See Also  
[ISchematicInMemoryFeatureClass](http://desktop.arcgis.com/search/?q=ISchematicInMemoryFeatureClass&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic: Schematics | ArcGIS Desktop Basic: Schematics |  
| ArcGIS Desktop Standard: Schematics | ArcGIS Desktop Standard: Schematics |  
| ArcGIS Desktop Advanced: Schematics | ArcGIS Desktop Advanced: Schematics |  


