##Publish an image service and set configurations

###Purpose  
This sample shows how to programmatically publish an image service to ArcGIS 10.2 for Server. It illustrates how to connect to a server admin endpoint, create an image service and set parameters and capabilities based on data source type (mosaic dataset, raster dataset, raster layer), and enable Web Coverage Service (WCS) and Web Map Service (WMS). Additional functionalities include starting, stopping, and deleting an image service.  


###Usage
####How to use  
1. Start Visual Studio, open the solution file, and compile the program.  
1. Run isconfig from the command line environment. To run this utility, both the client and ArcGIS Server need to have access to the data source. See the Additional information section below.  
1. This sample only populates commonly used properties in this configuration, you may need to alter some of them based on your service requirements.  
1. Caching and REST metadata/thumbnail/iteminfo are not handled in this sample. Caching can be created after service creating through caching geoprocessing tools, and REST metadata/thumbnail/iteminfo can be created by making calls to ArcGIS Server admin endpoint.  





####Additional information  
<div xmlns="http://www.w3.org/1999/xhtml"> </div>  




---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| Engine Developer Kit | Engine |  


