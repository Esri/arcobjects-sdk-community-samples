/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessor;
using GeoprocessorEventHelper;

namespace TestListner
{
  class GPEventListner
  {
    /// <summary>
    /// This sample console app demonstrates listening to GP events as they happen
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);

      GPMessageEventHandler gpEventHandler = new GPMessageEventHandler();

      //get an instance of the geoprocessor
      Geoprocessor GP = new Geoprocessor();
      //register the event helper in order to be able to listen to GP events
      GP.RegisterGeoProcessorEvents(gpEventHandler);

      //wire the GP events
      gpEventHandler.GPMessage += new MessageEventHandler(OnGPMessage);
      gpEventHandler.GPPreToolExecute += new PreToolExecuteEventHandler(OnGPPreToolExecute);
      gpEventHandler.GPToolboxChanged += new ToolboxChangedEventHandler(OnGPToolboxChanged);
      gpEventHandler.GPPostToolExecute += new PostToolExecuteEventHandler(OnGPPostToolExecute);

      //instruct the geoprocessing engine to overwrite existing datasets
      GP.OverwriteOutput = true;

      //create instance of the 'create random points' tool. Write the output to the machine's temp directory
      CreateFeatureclass createFeatureClass = new CreateFeatureclass(System.IO.Path.GetTempPath(), "RandomPoints.shp");
      //execute the tool
      GP.Execute(createFeatureClass, null);

      //unwire the GP events
      gpEventHandler.GPMessage -= new MessageEventHandler(OnGPMessage);
      gpEventHandler.GPPreToolExecute -= new PreToolExecuteEventHandler(OnGPPreToolExecute);
      gpEventHandler.GPToolboxChanged -= new ToolboxChangedEventHandler(OnGPToolboxChanged);
      gpEventHandler.GPPostToolExecute -= new PostToolExecuteEventHandler(OnGPPostToolExecute);

      //unregister the event helper
      GP.UnRegisterGeoProcessorEvents(gpEventHandler);

      System.Diagnostics.Trace.WriteLine("Done");
    }

    static void OnGPPostToolExecute(object sender, GPPostToolExecuteEventArgs e)
    {
      System.Diagnostics.Trace.WriteLine(e.Result.ToString());
    }

    static void OnGPToolboxChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Trace.WriteLine("OnGPToolboxChanged");
    }

    static void OnGPPreToolExecute(object sender, GPPreToolExecuteEventArgs e)
    {
      System.Diagnostics.Trace.WriteLine(e.Description);
    }

    static void OnGPMessage(object sender, GPMessageEventArgs e)
    {
      System.Diagnostics.Trace.WriteLine(e.Message);
    }
  }
}
