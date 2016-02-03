##Use an AlgorithmicColorRamp to color a ClassBreaksRenderer

###Purpose  
This sample demonstrates a tool for producing a user-specified AlgorithmicColorRamp, which is used to color the symbols on a ClassBreaksRenderer on a feature layer.   


###Usage
1. Open the Visual Studio solution file and build the project.  
1. Start ArcMap and create a map.   
1. Open the Customize dialog box. On the Toolbars tab, select the Context Menus option. The Context Menus toolbar appears.   
1. Using the Context Menus toolbar, click Context Menus and click Feature Layer Context Menu. The Feature Layer context menu appears.  
1. On the Commands tab on the Customize dialog box, select the new AlgorithmicColorRamp command from the Developer Samples category. Drag this command onto the Feature Layer context menu that was displayed in the previous step. Close the Customize dialog box.   
1. Add a feature layer (containing polygons, lines, or points) to the map document.  
1. Render your layer with a ClassBreaksRenderer. Right-click the layer and click Properties, then Symbology. Choose the Quantities, GraduatedColors option and add values from any suitable Value field.   
1. Change the colors on the renderer by right-clicking the feature layer in the table of contents (TOC). The dialog box that appears allows you to create an AlgorithmicColorRamp based on a FromColor, a ToColor, and an Algorithm.  
1. Click OK to recolor your feature layer renderer according to your selections.   









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


