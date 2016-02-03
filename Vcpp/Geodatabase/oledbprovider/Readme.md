##OGIS OLE DB Provider

###Purpose  
This example demonstrates how to create a read-only OLE DB provider which meets all the requirements for ArcGIS, that is it can identify, query, and retrieve spatial data. This sample provider can read data from an ArcGIS personal geodatabase or Microsoft® Access .mdb file using standard ArcObjects. The Sample Provider is compliant with the OpenGIS (OGIS) OLE-COM Simple Features Specification. This means that the provider is spatially enabled, i.e., it can discover, query and retrieve spatial features. You can find complete details of the Microsoft OLE DB provider technology and of the OGIS specification at www.microsoft.com/data and www.opengis.org, respectively.    


###Usage
1. Open and build the solution to compile and register the DLL (you may have to modify the path to the ESRI type libraries to correspond to your ArcGIS install directory).   
1. Make an OLE DB connection from ArcGIS using the SampleProvider.               Double click on the               Add OLE DB connection object in either ArcCatalog (TOC panel) or ArcMap (Add Data dialog).                This will call the Data Link Properties dialog -                on the Provider tab select the 'SampleProv OLE DB Provider'.           
1. Click on 'Next >>' to bring up the 'Connection' tab and enter the path to and name of a personal               geodatabase or Microsoft® Access .mdb file in the Data Source field.               Click on 'Next >>' to move to the 'Advanced' tab and click on 'OK'.                A new OLE DB connection should appear in the TOC panel or Add Data Dialog.                                         
1. Test the provider by browsing the data.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
| ArcGIS for Desktop Basic | ArcGIS for Desktop Basic |  
| ArcGIS for Desktop Standard | ArcGIS for Desktop Standard |  
| ArcGIS for Desktop Advanced | ArcGIS for Desktop Advanced |  


