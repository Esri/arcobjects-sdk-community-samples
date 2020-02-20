## Validate and edit features using a server object extension 

  <div xmlns="http://www.w3.org/1999/xhtml">The purpose of this sample is to demonstrate how to edit features in layers exposed by a map service, via a server object extension (SOE).</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml">The EditFeatures SOE has one property called layerId, which indicates the layer chosen for editing. This sample SOE also has one subresource called layers and two operations called editFeature and addNewFeatures. The administrator that deploys this SOE and enables it on a map service can chose which layer to open for editing using the layerId property. Editing will be restricted to this specific layer only.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#
Subject:               Server
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
#### Using this sample  
1. Deploy the EditFeaturesRESTSOE.soe file to the server.   
1. Enable the SOE on a map service of your choice. Ensure that the map service has at least one feature layer.   
1. Confirm that the SOE's layerId property has a value that matches a feature layer's layer id. Click Save and Restart.  
1. Open the Services Directory and access the http://<server name>:6080/arcgis/rest/services page.  
1. Click the map service on which you enabled your SOE.  
1. Scroll down and click EditFeaturesRESTSOE listed in the Supported Extensions section. If your REST SOE isn't listed here, log in to the Services Directory as an administrator and clear the REST cache. Repeat steps 5 and 6 as necessary.  
1. The EditFeaturesRESTSOE web page displays root resource details, such as name and description, along with a description of how to use the SOE, the layer id chosen for editing, and the layer schema that the SOE can accept, based on layers in the map service on which the SOE is enabled. This page also displays Child Resources and Supported Operations sections.  
1. Click the layers subresource. It displays information about all feature layers in JSON format.   
1. Click the editFeature operation. Provide the featureId and featureJSON. Click editFeature (POST). To determine the valid feature JSON for the selected layer, see the acceptable layer schema section on the EditFeaturesRESTSOE page.  
1. Navigate to the map service page in the Services Directory, select the layer enabled for editing, and execute a Query operation to verify that the feature corresponding to featureId mentioned above has indeed been modified.  
1. Navigate back to the SOE page and click the addNewFeature operation. This operation takes in feature JSON and creates a feature in the layer's feature class.  
1. Provide valid JSON and click addNewFeature (POST).  
1. Run the Query operation again on the layer to verify the addition of the new feature.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
|  |  |  


