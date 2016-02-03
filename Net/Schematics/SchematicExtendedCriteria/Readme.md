##Implementing extended criteria for some predefined schematic rules

###Purpose  
This sample shows how to implement custom extended criteria that are intended for use with the following schematic rules:Node Reduction By PriorityFeature RemovalExpand LinksCollapse Related ElementsThis sample will help you become familiar with the ISchematicNodeReductionExtended, ISchematicCollapseRelatedElementsExtended, ISchematicFeatureRemovalExtended, and ISchematicExpandLinksByAttributeExtented interfaces, and it will help you understand how these interfaces must be implemented to use a custom criterion to achieve the following:Reduce nodes during a Node Reduction By Priority rule executionRemove schematic features during a Feature Removal Links ruleExpand schematic links during an Expand Links rule executionCollapse schematic features during a Collapse Related Elements rule executionAny criterion can be combined with the other options specified for the rule to define the final set of schematic features that will be impacted during the rule execution. These extended criteria have been developed to work with data stored in the ExtendedCriteriaSamples sample geodatabase containing cable links and plant nodes. The PlantOnCableDiameter and PlantWithoutEquipment extended criteria are implemented for the Node Reduction By Priority rule as follows:PlantOnCableDiameter works on the set of incident cable links related to a node candidate for being reduced. If all incident cable diameters are 8, the candidate node is reduced.PlantWithoutEquipment works directly on the candidate plant nodes that can be reduced. The candidate node is reduced only when it does not have any associated record in a specific database table.The FeatureRemovalExt extended criterion is implemented for the Feature Removal rule; it can be used to remove cable schematic links that have particular identifiers. The ExpandLinksExt extended criterion is implemented for the Expand Links rule; it is used to expands cable schematic links according to particular attribute values on their plant origin nodes. The CollapseRelatedElts extended criterion is a template class you must customize to use an extended criterion for Collapse Related Elements rules.   


###Usage
####Building the component  
1. Start Visual Studio, open the solution file, and build the project.  

####Copying the sample database in a working folder  
1. Navigate to <ArcGIS DeveloperKit install location>\Samples\data\Schematics  
1. Copy the ExtendedCriteriaSamples.gdb geodatabase in a folder for which you have full rights.  

####Configuring a Node Reduction By Priority rule using the Reduce if connected cable diameters are 8 custom reduce extended criteria  
1. Start ArcCatalog, browse to the copied ExtendedCriteriaSamples geodatabase, right-click the ExtendedCriteriaSamples_Schematic schematic dataset, then click Edit.  
1. From the Schematic Dataset Editor tree, click the DiagramsFromSampleFeatureDataset diagram template.  
1. Click the Rules tab.  
1. If there are rule items on the Rules tab, uncheck their related Active box.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select Node Reduction By Priority from the Type drop-down list .  
1. Click Rule Properties on the Rules tab toolbar. The Node Reduction By Priority rule properties page displays.  
1. Type a description for the newly created rule; for example, type Reduce plants whose connected cables diameters are 8.  
1. Select plants for the Select node element class to reduce.  
1. Keep the default reduction options, then check Use extended criteria.  
1. The two custom reduction extended criteria that were automatically registered when the solution was built are available from the drop-down list under the Use extended criteria check box. Click Reduce if connected cable diameters are 8 (VBNet) or Reduce if connected cable diameters are 8 (C#) from the drop-down list and click OK.  
1. Save and exit the Schematic Dataset Editor.  
1. Start ArcMap with an empty map.  
1. Click Open Schematic Diagrams on the Schematic toolbar and open the TestReduction_Diameter schematic diagram stored in the ExtendedCriteriaSamples_Schematic schematic dataset in the copied ExtendedCriteriaSamples.gdb geodatabase.  
1. Click Update Diagram on the Schematic toolbar. The Update Diagram dialog box opens.   
1. Keep the Synchronize against original selection/trace/query option checked and click OK. At the end of the update process, all plants' connecting links whose diameters are 8 are reduced.  

####Configuring a Node Reduction By Priority rule using the Reduce plants without equipments custom reduce extended criteria  
1. Start ArcCatalog, browse to the copied ExtendedCriteriaSamples geodatabase, right-click the ExtendedCriteriaSamples_Schematic schematic dataset, then click Edit.  
1. From the Schematic Dataset Editor tree, click the DiagramsFromSampleFeatureDataset diagram template.  
1. Click the Rules tab.  
1. If there are rule items on the Rules tab, uncheck their related Active box.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select Node Reduction By Priority from the Type drop-down list .  
1. Click Rule Properties on the Rules tab toolbar. The Node Reduction By Priority rule properties page displays.  
1. Type a description for the newly created rule; for example, type Reduce plants without equipments.  
1. Select plants for the Select node element class to reduce.  
1. Keep the default reduction options, then check Use extended criteria.  
1. The two custom reduction extended criteria that were automatically registered when the solution was built are available from the drop-down list under the Use extended criteria check box. Click Reduce plant without equipments (VBNet) or Reduce plant without equipments (C#) from the drop-down list and click OK.  
1. Save and exit the Schematic Dataset Editor.  
1. Start ArcMap with an empty map.  
1. Click Open Schematic Diagrams on the Schematic toolbar and open the TestReduction_Equipments schematic diagram stored in the ExtendedCriteriaSamples_Schematic schematic dataset in the copied ExtendedCriteriaSamples.gdb geodatabase.  
1. Click Update Diagram on the Schematic toolbar. The Update Diagram dialog box opens.   
1. Keep the Synchronize against original selection/trace/query option checked and click OK. At the end of the update process, the plants without equipments are reduced.  

####Configuring a Feature Removal rule using the Remove cables with particular ID custom reduce extended criteria  
1. Start ArcCatalog, browse to the copied ExtendedCriteriaSamples geodatabase, right-click the ExtendedCriteriaSamples_Schematic schematic dataset, then click Edit.  
1. From the Schematic Dataset Editor tree, click the DiagramsFromSampleFeatureDataset diagram template.  
1. Click the Rules tab.  
1. If there are rule items on the Rules tab, uncheck their related Active box.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select Feature Removal from the Type drop-down list .  
1. Click Rule Properties on the Rules tab toolbar. The Feature Removal rule properties page displays.  
1. Type a description for the newly created rule; for example, type Remove cables with particular ID.  
1. Select cables for the Select the schematic feature class to remove.  
1. Keep the default removal options, then check Use extended criteria.  
1. Click Remove cables with particular ID (VBNet) or Remove cables with particular ID (C#) from the drop-down list under this check box, and click OK.  
1. Save and exit the Schematic Dataset Editor.  
1. Start ArcMap with an empty map.  
1. Click Open Schematic Diagrams on the Schematic toolbar and open the TestRemoval_ParticularCables schematic diagram stored in the ExtendedCriteriaSamples_Schematic schematic dataset in the copied ExtendedCriteriaSamples.gdb geodatabase.  
1. Click Update Diagram on the Schematic toolbar. The Update Diagram dialog box opens.   
1. Keep the Synchronize against original selection/trace/query option checked and click OK. At the end of the update process, two cables are removed.  

####Configuring an Expand Links rule using the Use origin plant's MaxOutLines value custom reduce extended criteria  
1. Start ArcCatalog, browse to the copied ExtendedCriteriaSamples geodatabase, right-click the ExtendedCriteriaSamples_Schematic schematic dataset, then click Edit.  
1. From the Schematic Dataset Editor tree, click the DiagramsFromSampleFeatureDataset diagram template.  
1. Click the Rules tab.  
1. If there are rule items on the Rules tab, uncheck their related Active box.  
1. Click Add Rule on the Rules tab toolbar.  
1. Select Expand Links from the Type drop-down list.  
1. Click Rule Properties on the Rules tab toolbar. The Expand Links rule properties page displays.  
1. Type a description for the newly created rule; for example, type Expand cables based on its origin plant's MaxOutLines value.  
1. Select cables for the Select the link schematic feature class to expand.  
1. Check Use extended criteria on the Select value source section.  
1. Choose Use origin plant's MaxOutLines value (VBNet) or Use origin plant's MaxOutLines value (C#) from the drop-down list under this check box, and click OK.  
1. Check the Integer value option on the Value format section.  
1. Save and exit the Schematic Dataset Editor.  
1. Start ArcMap with an empty map.  
1. Click Open Schematic Diagrams on the Schematic toolbar and open the TestExpandLinks_FromOriginPlant schematic diagram stored in the ExtendedCriteriaSamples_Schematic schematic dataset in the copied ExtendedCriteriaSamples.gdb geodatabase.  
1. Click Update Diagram on the Schematic toolbar. The Update Diagram dialog box opens.   
1. Keep the Synchronize against original selection/trace/query option checked and click OK.  
1. Click Start Editing Diagram on the Schematic Editor toolbar.  
1. Choose Separate Overlapping Links in the Layout Task drop-down list.  
1. Click Apply Layout Task. Several links now connect the same plant nodes. The number of links between two plant nodes correspond to the MaxOutLines value on the initial cable's plant origin node.  







####See Also  
[ISchematicCollapseRelatedElementsExtended interface](http://desktopdev.arcgis.com/search/?q=ISchematicCollapseRelatedElementsExtended%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicNodeReductionExtended interface](http://desktopdev.arcgis.com/search/?q=ISchematicNodeReductionExtended%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicFeatureRemovalExtended interface](http://desktopdev.arcgis.com/search/?q=ISchematicFeatureRemovalExtended%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISchematicExpandLinksByAttributeExtended interface](http://desktopdev.arcgis.com/search/?q=ISchematicExpandLinksByAttributeExtended%20interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic: Schematics | ArcGIS for Desktop Basic: Schematics |  
| ArcGIS for Desktop Standard: Schematics | ArcGIS for Desktop Standard: Schematics |  
| ArcGIS for Desktop Advanced: Schematics | ArcGIS for Desktop Advanced: Schematics |  


