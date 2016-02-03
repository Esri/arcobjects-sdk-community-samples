Imports ESRI.ArcGIS.Carto

Public Interface IMultivariateRenderer

    Property ShapePatternRend() As IFeatureRenderer
    Property ColorRend1() As IFeatureRenderer
    Property ColorRend2() As IFeatureRenderer
    Property ColorCombinationMethod() As EColorCombinationType
    Property SizeRend() As IFeatureRenderer
    Sub CreateLegend()
End Interface