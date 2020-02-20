'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geoprocessing
Imports GeoprocessorEventHelper

Namespace TestListner
  Friend Class GPEventListner
        ' This sample console app demonstrates listening to GP events as they happen

        Shared Sub Main(ByVal args As String())

            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)

            Dim gpEventHandler As GPMessageEventHandler = New GPMessageEventHandler()

            'get an instance of the geoprocessor
            Dim GP As IGeoProcessor2 = New ESRI.ArcGIS.Geoprocessing.GeoProcessor()
            'Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()
            'register the event helper in order to be able to listen to GP events
            GP.RegisterGeoProcessorEvents(gpEventHandler)

            'wire the GP events
            AddHandler gpEventHandler.GPMessage, AddressOf OnGPMessage
            AddHandler gpEventHandler.GPPreToolExecute, AddressOf OnGPPreToolExecute
            AddHandler gpEventHandler.GPToolboxChanged, AddressOf OnGPToolboxChanged
            AddHandler gpEventHandler.GPPostToolExecute, AddressOf OnGPPostToolExecute

            'instruct the geoprocessing engine to overwrite existing datasets
            GP.OverwriteOutput = True

            Dim parameters As IVariantArray = New VarArray()
            parameters.Add(System.IO.Path.GetTempPath())
            parameters.Add("NewShapefile.shp")

            ''create instance of the 'create random points' tool. Write the output to the machine's temp directory
            'Dim createFeatureclassTool As CreateFeatureclass = New CreateFeatureclass(System.IO.Path.GetTempPath(), "RandomPoints.shp")
            'execute the tool
            'GP.Execute(createFeatureclassTool, Nothing)
            GP.Execute("CreateFeatureclass_management", parameters, Nothing)

            'unwire the GP events
            RemoveHandler gpEventHandler.GPMessage, AddressOf OnGPMessage
            RemoveHandler gpEventHandler.GPPreToolExecute, AddressOf OnGPPreToolExecute
            RemoveHandler gpEventHandler.GPToolboxChanged, AddressOf OnGPToolboxChanged
            RemoveHandler gpEventHandler.GPPostToolExecute, AddressOf OnGPPostToolExecute

            'unregister the event helper
            GP.UnRegisterGeoProcessorEvents(gpEventHandler)

            System.Diagnostics.Trace.WriteLine("Done")
        End Sub

	Private Shared Sub OnGPPostToolExecute(ByVal sender As Object, ByVal e As GPPostToolExecuteEventArgs)
	  System.Diagnostics.Trace.WriteLine(e.Result.ToString())
	End Sub

	Private Shared Sub OnGPToolboxChanged(ByVal sender As Object, ByVal e As EventArgs)
	  System.Diagnostics.Trace.WriteLine("OnGPToolboxChanged")
	End Sub

	Private Shared Sub OnGPPreToolExecute(ByVal sender As Object, ByVal e As GPPreToolExecuteEventArgs)
	  System.Diagnostics.Trace.WriteLine(e.Description)
	End Sub

	Private Shared Sub OnGPMessage(ByVal sender As Object, ByVal e As GPMessageEventArgs)
	  System.Diagnostics.Trace.WriteLine(e.Message)
	End Sub
  End Class
End Namespace
