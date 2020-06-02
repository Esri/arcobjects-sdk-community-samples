## Multivariate renderer

  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample consists of a multivariate feature renderer that can represent multiple, independent data distributions on a single layer. The multivariate renderer is a powerful way to represent the relationship between two or more data distributions when multiple variables exist for the same geographic features (points, lines, or areas). Each data variable is assigned a unique graphical visual variable (for example, the size, shape, and orientation) and the multivariate symbolization is built up from two or more of these. The implementation of this is that the multivariate renderer maintains references to several univariate ESRI feature renderers (SimpleRenderer, UniqueValueRenderer, ClassBreaksRenderer, or ProportionalSymbolRenderer). </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">For example, population and housing data might be available for city point data. You can assign a ClassBreaksRenderer to encode both of these variables and thus, choose to use symbol color (to be specific, its saturation and value components) to encode a variable for percent housing vacancy, and use symbol size in points to represent a variable for total population. You could add a third variable to your symbolization, making it trivariate, if you have a categorical variable, perhaps that indicates the region where the city is located. This could be symbolized using the symbol shape or the hue component of color.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">This sample also includes a multivariate feature renderer property page that allows the software user to build and assign a multivariate feature renderer to a layer in ArcMap. The design of both the renderer and property page takes advantage of the existing support in ArcMap for univariate feature rendering. To set up multivariate symbolization for a layer, first make several copies of the layer and set up each univariate symbolization, then use the multivariate renderer property page to create multivariate symbolization based on the univariate symbolizations. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">The multivariate renderer is designed as a "super" feature renderer that manages a collection of multiple univariate feature renderers. The custom multivariate renderer keeps track of each constituent renderer and, on IFeatureRenderer.Draw, draws the multivariate symbology for each feature by combining symbology from each univariate renderer. Both the custom property page and custom renderer use Bertin's graphical as a fundamental design principle. By using the property page, you can choose to encode a single variable using size, color, shape and pattern, or orientation. Size is useful for representing quantitative difference. The multivariate renderer uses a ClassBreaksRenderer for this.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">You can separate color into hue, saturation, and value. Hue is what most people think of when they think of color and is good for showing categorical difference. Saturation is the richness of the color, and value can be thought of as lightness and darkness. Both are good for showing quantitative difference and they are combined together for the purposes of this renderer.</div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Shape and pattern are combined together as a single encoded graphical visual variable, which is useful for symbolizing categorical difference between features. </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53"> </div>
  <div xmlns="http://www.w3.org/1999/xhtml" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-02-10T23:25:53">Orientation (rotation) of a symbol can be useful for symbolizing categorical difference or quantitative difference if the variable being represented has some true directionality (for example, predominate wind direction measured at point features). </div>  


<!-- TODO: Fill this section below with metadata about this sample-->
```
Language:              C#, VB
Subject:               Graphics Pipeline
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
1. Open the sample.  
1. Compile the sample to create its .dll.  
1. Start ArcMap.  
1. Add a dataset to ArcMap that you want to draw with the multivariate renderer.   
1. Copy and paste the layer so that you have enough copies of the layer in your map to assign each univariate symbolization, as well as for the multivariate symbolization. Note: It might be useful to rename these layers so you can remember how each is symbolized.  
1. Set up each univariate symbolization using the Symbology tab on the Layer Properties dialog box. Do this, so that each applicable univariate symbolization is assigned to a separate layer in your map. Each layer points to the same underlying data. The extra layers are only used to set up the multivariate symbolization or to make changes. The layers can be disabled in the table of contents (TOC) after you have set up your multivariate symbolization.   
1. For the layer you want to symbolize with the multivariate renderer, click the Symbology tab on the Layer Properties dialog box, and choose Multivariate Renderer to show the custom renderer property page that you will use to set up your renderer.   
1. Select the check boxes to enable rendering for the graphical visual variable that you want to use. For example, if you want to use shape and size, select the Shape/Pattern and Size check boxes.   
1. For each check box that is selected, use the drop-down lists to select the layer whose univariate symbolization will be used for that graphical visual variable. For example, for Shape/Pattern, select the layer symbolized with a UniqueValueRenderer with point symbols that are different shapes, line symbols with a different pattern, or fill symbols with a different fill pattern. For Size, select the layer symbolized with ClassBreaksRenderer or ProportionalSymbolRenderer that symbolizes features with different sized symbols.   
1. For orientation, a second dialog box appears where you can pick the field whose values determine the orientation of the multivariate symbolization.   
1. When complete, click OK or Apply to add the multivariate renderer to your layer.  









---------------------------------

#### Licensing  
| Development licensing | Deployment licensing | 
| ------------- | ------------- | 
| ArcGIS Desktop Basic | ArcGIS Desktop Basic |  
| ArcGIS Desktop Standard | ArcGIS Desktop Standard |  
| ArcGIS Desktop Advanced | ArcGIS Desktop Advanced |  


