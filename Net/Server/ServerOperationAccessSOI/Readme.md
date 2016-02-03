##Server operation access

###Purpose  
This sample illustrates how to filter access to individual operations available on the map server. Three operations – "find", "identify", and "export" – are checked against the incoming request’s user’s role. See Deploying extensions to learn how to deploy the extension to your server.  


###Usage
1. Define at least two new custom roles in you ArcGIS Server site called "gold" and "platinum".  
1. Associate one or more users to those two roles, as well as other roles.  
1. Deploy NetOperationAccessSOI.soe to your server.  
1. Attach the SOI to a map service of your choice.  
1. Optionally, secure the service.  
1. Observe how the find, identify, and export operations are only available when the request is authenticated and the authenticated user is in either the gold or platinum roles.  









---------------------------------

####Licensing  
| Development licensing | Deployment licensing | 
| :------------- | :------------- | 
|  |  |  


