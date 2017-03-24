## Update a frame's background, border, and shadow using the SymbologyControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates using the SymbologyControl to update frame properties. The sample uses the SymbologyControl in conjunction with the PageLayoutControl, ToolbarControl, and the controls commands.</div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Controls
Organization:          Esri, http://www.esri.com
Date:                  3/24/2017
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
1. Load a map document into the PageLayoutControl.   
1. Change the page properties using the combo box and the items shown in the SymbologyControl.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The LoadStyleFile method is used within the Form_Load event to add the contents of the ESRI.ServerStyle into the SymbologyControl. A combo box is populated with background, border, and shadow items. The SelectedIndexChanged event of the combo box is used to set the type of items displayed in the SymbologyControl using the StyleClass property.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Clicking an item shown in the SymbologyControl fires the OnItemSelectedEvent. The IStyleGalleryItem.Item property is used to determine whether the item implements IBackground, IBorder, or IShadow, then the IFrameProperties, Background, Border, or Shadow property is set. The frame properties of the MapFrame containing the FocusMap are updated.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Loading a map document into the PageLayoutControl fires the OnPageLayoutReplaced event. If the frame properties of the MapFrame containing the FocusMap are set, a new ServerStyleGalleryItem is created with the name set to "myStyle" and the item set to the Background, Border, or Shadow. The ServerStyleGalleryItem is added to the appropriate StyleClass using the ISymbologyStyleClass.AddItem method.</div>  


#### See Also  
[SymbologyControl Class](http://desktop.arcgis.com/search/?q=SymbologyControl%20Class&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyControl Interface](http://desktop.arcgis.com/search/?q=ISymbologyControl%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  
[ISymbologyStyleClass Interface](http://desktop.arcgis.com/search/?q=ISymbologyStyleClass%20Interface&p=0&language=en&product=arcobjects-sdk-dotnet&version=&n=15&collection=help)  


---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| Engine Developer Kit | Engine |  
|  | ArcGIS Desktop Basic |  
|  | ArcGIS Desktop Standard |  
|  | ArcGIS Desktop Advanced |  


