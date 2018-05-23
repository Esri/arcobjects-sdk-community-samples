## Retrieve a color ramp from the SymbologyControl

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample demonstrates how to retrieve a color ramp from the SymbologyControl and use it within a ClassBreaksRenderer. The sample uses the SymbologyControl in conjunction with the PageLayoutControl, ToolbarControl, and the controls commands.</div>  


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
* [What's new](http://desktop.arcgis.com/en/arcobjects/latest/net/webframe.htm#91cabc68-2271-400a-8ff9-c7fb25108546.htm)  
* [Download the ArcObjects SDK for .Net from MyEsri.com](https://my.esri.com/)  

### Usage
1. Open a map document and select a polygon feature layer.    
1. Select a color ramp and field, then set the number of classes.   





#### Additional information  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">When a document is loaded into the PageLayoutControl, the OnPageLayoutReplaced event is fired and used to populate a combo box with a list of polygon feature classes implementing IGeoFeatureLayer. The layer selected in the combo box is rendered with a ClassBreaksRenderer using the IGeoFeatureLayer.Renderer property.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Within the Form_Load event of the SymbolForm, the LoadStyleFile method is used to add the contents of the ESRI.ServerStyle into the SymbologyControl, and the StyleClass property is used to show color ramps. A combo box is populated with the names of the numeric fields within the FeatureClass. The minimum and maximum values within selected fields are shown using the IStatisticsResults.Maximum and IStatisticsResults.Minimum properties.</div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>  
<div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Clicking an item in the SymbologyControl passes the selected IStyleGalleryItem object to the OnItemSelected event. A ColorRamp is returned from the IStyleGalleryItem.Item property. The IColorRamp.Size and IClassBreaksRenderer.BreakCount properties are set to the number of classes specified by the end user. For each break in the ClassBreaksRenderer, the IClassBreaksRenderer.Symbol is set with a SimpleFillSymbol.Color specified by the next color in the ColorRamp.</div>  


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


