##Layer access

###Purpose  
This sample demonstrates two things: How to handle REST, SOAP, and binary requests.How to implement layer level controls and conditionally give access to certain layers based on an external permissions file. As part of the handling of SOAP and binary requests, this samples includes an SOISupport utility class to show how to call into the ArcGIS Server default operation handlers. This sample class can be used in your own SOIs where you need to handle SOAP and binary requests. Note that this sample is not comprehensive in what operations are intercepted. Developers should use this as the basis for learning how to implement security related functionality, but all production code should be carefully vetted to ensure that all appropriate operations are handled and no information is inadvertently leaked or made accessible. This sample is dependent on a file named permission.json to be present in the directory containing the arcgisoutput server directory. For example, in a default Windows installation this is C:\arcgisserver. See Deploying extensions to learn how to deploy the extension to your server.  


###Usage
1. Deploy LayerAccessSOI.soe to your server (or your own modified version if you have changed the path for the permission.json file).  
1. Enable the SOI on a map service.  
1. Disable caching of layer resources in the ArcGIS Server REST handler.  
1. Consume the service using different users.  
1. Note how certain layers are only visible when the request is authenticated and matches the permissions defined in permission.json.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
|  |  |  


