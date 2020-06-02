## Create a custom raster function

  <div xmlns="http://www.w3.org/1999/xhtml">This sample shows how to create a custom raster function, its corresponding arguments object and the user interface (UI) for the function by implementing the IRasterFunction and IRasterFunctionArguments interfaces along with additional interfaces. The implemented function is a watermark function that uses GDI+ to read a small watermark image and blend it with the raster to which it is applied.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The Function UI can be used to view or modify the parameters of the function. The function editor can be used to add or remove it from a function raster dataset or mosaic dataset. The optional test application provided can be used to apply the custom function to a raster dataset or a mosaic dataset, create a Raster Function template from the custom function, serialize the function template to Extensible Markup Language (XML), and deserialize it back from an XML file. </div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">Note: To serialize a function to and from an XML file, you need to update the XmlSupport.dat file, which is described in this sample topic.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Raster
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
#### Setting up the custom function  
1. Start Visual Studio and open the solution.  
1. Build the WatermarkFunction and WatermarkFunctionUI projects.  
1. The function can now be used like any of the built-in functions in the system.  

#### Updating XmlSupport.dat to be able to serialize functions to and from xml  
1. Open the XmlSupportSubsetWM.dat file in the TestWatermarkFunction folder. Copy its contents.  
1. Open <Program Files>\ArcGIS\<Desktop Directory>\bin\XmlSupport.dat. This is an XML file containing types that can be serialized to and from an XML file. Create a backup of the file.  
1. Paste the contents copied in Step 1 inside the main node of the XML file along with the other type entries. Make sure it's between type nodes and not inside one.  
1. Save the XMLSupport.dat file.  

#### Using the custom function in desktop  
1. Start ArcMap.  
1. Open the raster dataset on which the function is to be applied.  
1. Open the Image Analysis window.  
1. Select the raster dataset on which the function is to be applied in the topmost section.  
1. In ArcGIS 10.0, under the Processing section, click the Clip button, open the properties of the newly created layer, then navigate to the functions tab. The function editor opens. Right-click the Clip function and click Remove.  
1. In ArcGIS 10.2 and after, under the Processing section click the fx button. The function editor opens.  
1. Right-click the dataset, open the Insert menu, and click the NDVI Custom function. This opens the Watermark Function UI page, which allows you to set parameters.  
1. Input the location of the watermark, the path to the watermark image, and the blend percentage.  
1. Click OK.  
1. In ArcGIS 10.0, the layer now represents the output of the custom function applied to the raster dataset.  
1. In ArcGIS 10.2 and after, a new layer is created that represents the output of the custom function applied to the raster dataset.  

#### Using the custom function test application  
1. Start Visual Studio and open the solution file.  
1. Open the TestWatermarkFunction project.  
1. Set the flags to add the Watermark function on top of a raster dataset and a mosaic dataset, to serialize a raster function template containing the custom function to an XML file, or to deserialize an XML file back to a function template containing the custom function.  
1. Edit the strings in the Specify inputs region to specify the directory and name of the input dataset, the Geodatabase and name of the mosaic dataset, the output directory, the name of the output dataset (including the .afr extension), and the full path to the XML file to write out or read from if needed.   
1. Set the parameters for the Watermark Custom function (that is, the location of the watermark, the path to the watermark image and the blending percentage).  
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


