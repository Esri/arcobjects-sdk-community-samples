## Create an NDVI custom raster function

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows how to create a custom raster function, its corresponding arguments object and the user interface (UI) for the function by implementing the IRasterFunction and IRasterFunctionArguments interfaces along with additional interfaces. The implemented function is a function that calculates the Normalized Differential Vegetation index (NDVI) for an image given the indexes of the Near Infrared and Red bands.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The Function UI can be used to view or modify the parameters of the function. The function editor can be used to add or remove it from a function raster dataset or mosaic dataset. The optional test application provided can be used to apply the custom function to a raster dataset or a mosaic dataset, create a Raster Function template from the custom function, serialize the function template to Extensible Markup Language (XML), and deserialize it back from an XML file. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">Note: To be able to serialize a function to and from an XML file, you need to update the XmlSupport.dat file, which is described in this sample topic.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Setting up the custom function  
1. Start Visual Studio and open the solution.  
1. Build the NDVICustomFunction and NDVICustomFunctionUI projects.  
1. The function can now be used like any of the built-in functions in the system.  

#### Updating XmlSupport.dat to serialize functions to and from XML  
1. Open the XmlSupportSubsetNC.dat file in the TestNDVICustomFunction folder. Copy its contents.  
1. Open <Program Files>\ArcGIS\<Desktop Directory>\bin\XmlSupport.dat. This is an XML file containing types that can be serialized to and from an XML file. Create a backup of the file.  
1. Paste the contents copied in Step 1 inside the main node of the XML along with the other type entries. Make sure it's between type nodes and not inside one.  
1. Save the XMLSupport.dat file.  

#### Using the custom function in desktop  
1. Start ArcMap.  
1. Open the raster dataset on which the function is to be applied.  
1. Open the Image Analysis window.  
1. Select the raster dataset on which the function is to be applied in the topmost section.  
1. In ArcGIS 10.0, under the Processing section, click the Clip button, open the properties of the newly created layer, then navigate to the functions tab. The function editor opens. Right-click the Clip function and click Remove.  
1. In ArcGIS 10.2 and after, under the Processing section, click the fx button. The function editor opens.  
1. Right-click the dataset and open the Insert menu, then click the NDVI Custom function. This opens the NDVI Function UI page, which allows you to set the parameter.  
1. Input the band indexes for the NIR and Red bands separated by a space.  
1. Click OK.  
1. In ArcGIS 10.0, the layer now represents the output of the custom function applied to the raster dataset.  
1. In ArcGIS 10.2 and after, a new layer is created that represents the output of the custom function applied to the raster dataset.  

#### Using the custom function test application  
1. Start Visual Studio and open the solution file.  
1. Open the TestNDVICustomFunction project.  
1. Set the flags to add the NDVI Custom function on top of a raster dataset and a mosaic dataset, to serialize a Raster Function template containing the custom function to an XML file, or to deserialize an XML file back to a function template containing the custom function.  
1. Edit the strings in the Specify inputs region to specify the directory and name of the input dataset, the Geodatabase and name of the mosaic dataset, the output directory, the name of the output dataset (including the .afr extension), and the full path to the XML file to write out or read from if needed.   
1. Set the parameters for the NDVI Custom function (that is, indexes of the NIR and Red band delimited by spaces).  
1. Compile and run the program. If the program succeeds, the console shows a Success message. If an error occurs, it shows on the console window.  
1. If adding on top of a raster dataset, load the created output .afr file into an ArcMap or ArcCatalog dataset. An image that represents the output of the custom function applied to the raster dataset appears.  
1. If adding on top of a mosaic dataset, load the mosaic dataset to which the custom function was added in ArcMap or ArcCatalog. An image that represents the output of the custom function applied to the raster dataset appears.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  
| Engine Developer Kit | Engine |  


