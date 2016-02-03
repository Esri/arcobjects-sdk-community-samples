
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using ESRI.ArcGIS.Carto;

namespace MultivariateRenderers
{
	public interface IMultivariateRenderer
	{

		IFeatureRenderer ShapePatternRend {get; set;}
		IFeatureRenderer ColorRend1 {get; set;}
		IFeatureRenderer ColorRend2 {get; set;}
		EColorCombinationType ColorCombinationMethod {get; set;}
		IFeatureRenderer SizeRend {get; set;}
		void CreateLegend();
	}
} //end of root namespace