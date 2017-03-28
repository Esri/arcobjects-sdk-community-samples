## Set flow by digitized direction

The Set Flow by Digitized Direction sample is an ArcMap add-in that shows how to create a custom flow direction solver button for utility networks. This button sets the flow direction for each edge feature in the network along the direction in which the feature was digitized.    


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Geodatabase
Organization:          Esri, http://www.esri.com
Date:                  3/28/2017
ArcObjects SDK:        10.5
Visual Studio:         2013, 2015
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
#### Adding the button to ArcMap  
1. Open the .sln file in Visual Studio and compile it.  
1. Start ArcMap and make the Utility Network Analyst toolbar visible.  
1. Click the Customize menu and click Customize Mode. The Customize dialog box opens.  
1. Click the Commands tab.   
1. In the Categories list, click Add-in Developer Samples. Set Flow by Digitized Direction is listed under Commands.   
1. Drag the command button onto the Utility Network Analyst toolbar.  

#### Using the button to set the flow direction  
1. Add a geometric network to ArcMap.  
1. Start editing.  
1. Click the button to set the flow according to the digitized direction of the features.  
1. Stop editing and save your edits.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


