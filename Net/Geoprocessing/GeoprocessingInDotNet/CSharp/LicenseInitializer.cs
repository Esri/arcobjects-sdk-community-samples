using System;
using ESRI.ArcGIS;

namespace GeoprocessingInDotNet2008
{
  internal partial class LicenseInitializer
  {
    public LicenseInitializer()
    {
      ResolveBindingEvent += new EventHandler(BindingArcGISRuntime);
      
      // TODO: Uncomment if implicit runtime binding (pre-10 version) is allowed
      // AllowImplicitRuntimeBinding = true;
    }

    void BindingArcGISRuntime(object sender, EventArgs e)
    {
       // TODO: Add ESRI.ArcGIS.RuntimeManager code to load target ArcGIS runtime here
    }
  }
}