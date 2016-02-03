##Create a custom raster type from the ground up for DMCII data

###Purpose  
This sample shows how to implement a custom raster type from the ground up to provide support for DMCII data. Also provided is an optional test application to create a geodatabase and a mosaic dataset, and add data using the custom type. The main interface to be implemented is the IRasterBuilder interface along with secondary interfaces such as IRasterBuilderInit (which provides access to the parent MD), IPersistVariant (which implements persistence), IRasterBuilderInit2 and IRasterBuilder2 (new interfaces added at 10.2). The interface IRasterFactory also needs to be implemented for the raster type to show up in the list of raster types in the Add Rasters GP tool. The factory is responsible for creating the raster type object and setting some properties on it. It also enables the use of the aster product.  


###Usage
####Setting up the custom function  
1. Start Visual Studio and open the solution.  
1. Build the DMCIIRasterType and TestDMCIIRasterType projects.  
1. The raster type can now be used like any of the built-in raster types in the system.  

####Using the custom raster type in desktop  
1. Start ArcMap.  
1. Create a mosaic dataset.  
1. Run the Add Rasters To Mosaic Dataset tool.  
1. Select the mosaic dataset created as source.  
1. Select the DMCII Raster Type from the list of raster types.  
1. To adjust raster type specific properties, click the Raster Type Properties button to the right of the Raster Type Selection drop-down list. The Raster Type properties page opens with options to pick the specific product to add (All, L1R, or L1T) and the template to apply (Raw or Stretch).   
1. Change settings as desired and click OK.  
1. Use the directory picker, or drag and drop the directory containing the DMCII data into the table showing input workspaces.  
1. Click OK.  
1. The tool adds the DMCII data to the mosaic dataset using the custom raster type.  

####Using the custom raster type test application  
1. Start Visual Studio and open the solution file.  
1. Open the TestDMCIIRasterType project.  
1. Edit the strings in the Setup MD Test Parameters region to specify the parameters that control what geodatabase to create, where to create the geodatabase and mosaic dataset, the names of the geodatabase and the mosaic dataset, what folder to add data from, and so on.  
1. Compile and run the program.  
1. The program console displays detailed messaging regarding the steps it performs and whether it has succeeded or not. It will also display any errors that occur.  
1. Load the created mosaic dataset into ArcMap or ArcCatalog.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| Engine Developer Kit | Engine |  


