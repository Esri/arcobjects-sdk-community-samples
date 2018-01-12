## Bind a geodatabase table to a .NET control

  <div xmlns="http://www.w3.org/1999/xhtml">This sample demonstrates how to bind a geodatabase table and a .NET control. Additionally, it shows how editing and coded-value domains can be supported.</div>
  <div xmlns="http://www.w3.org/1999/xhtml"> </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  11/17/2017
ArcObjects SDK:        10.6
Visual Studio:         2015, 2017
.NET Target Framework: 4.5
```

### Resources

* [ArcObjects .NET API Reference online](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm)  
* [Sample Data Download](../../releases)  
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#05247c04-bfd9-4e36-ae09-bc6e833c3b14.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open the solution and build it.  
1. Run the sample to display the main window.  
1. The data in the table can be viewed by using the scroll bars and the current row can be changed by using the forward and back buttons on the navigation control at the bottom.  
1. Select the Use Coded Value Domain check box to show the domain value names for the Enabled column.  
1. All fields apart from geometry and Shape_Length can be edited by clicking the appropriate cell in the grid.  
1. The NAME field can also be edited by changing the text in the text box.  
1. Add a new row by typing values at the end of the grid or by clicking the Add Row button on the navigation control.  
1. Remove the current row by clicking the Delete button on the navigation control.  





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample shows the binding of an entire ITable to a Microsoft DataGridView control and also a single IField to a Microsoft TextBox control. The solution contains the following two projects: </div>  




---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


