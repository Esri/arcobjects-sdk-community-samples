'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.TrackingAnalyst

Public Class AddTrackingServerLayerButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button
  Private Const TS_SERVER_NAME As String = "hound" 'computer name of the Tracking Server
  Private Const TS_SERVICE_NAME As String = "SanDiegoTaxis" 'name of the Tracking Service to open

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

    'load the Tracking Analyst extension
    Dim trackingEnv As ITrackingEnvironment3 = setupTrackingEnv(My.ArcMap.Application)

    'Create the temporal layer and add it to the display
    Dim temporalLayer As ILayer = CreateTemporalLayer()

    My.ArcMap.Document.FocusMap.AddLayer(temporalLayer)

    My.ArcMap.Application.CurrentTool = Nothing
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub

  'Create a new temporal layer with default symbology
  Private Function CreateTemporalLayer() As ILayer
    Dim featureLayer As IFeatureLayer = New TemporalFeatureLayerClass()
    Dim featureClass As IFeatureClass = OpenTrackingServerConnection()

    featureLayer.FeatureClass = featureClass

    Return TryCast(featureLayer, ILayer)
  End Function


  Private Function OpenTrackingServerConnection() As IFeatureClass
    Dim amsWorkspaceFactory As IWorkspaceFactory = New AMSWorkspaceFactory()

    Dim connectionProperties As IPropertySet = CreateTrackingServerConnectionProperties()
    Dim amsWorkspace As IAMSWorkspace = TryCast(amsWorkspaceFactory.Open(connectionProperties, 0), IAMSWorkspace)
    Dim featureClass As IFeatureClass = amsWorkspace.OpenFeatureClass(TS_SERVICE_NAME)

    Return featureClass
  End Function

  'Create connection property set for Tracking Server
  Private Function CreateTrackingServerConnectionProperties() As IPropertySet
    Dim connectionProperties As IPropertySet = New PropertySetClass()

    connectionProperties.SetProperty("SERVERNAME", TS_SERVER_NAME)
    connectionProperties.SetProperty("AMS_CONNECTION_NAME", "Sample TS Connection")
    'This is the standard AMS connection editor, this would only be different if you wrote your own connector
    connectionProperties.SetProperty("AMS_CONNECTOR_EDITOR", "{1C6BA545-2F59-11D5-B7E2-00010265ADC5}")
    'This is the standard AMS connector, this would only be different if you wrote your own connector
    connectionProperties.SetProperty("AMS_CONNECTOR", "{F6FC70F5-5778-11D6-B841-00010265ADC5}")
    connectionProperties.SetProperty("AMS_USER_NAME", "")
    connectionProperties.SetProperty("TMS_USER_PWD", "")

    Return connectionProperties
  End Function

  'Initialize the Tracking Environment, you only need to do this once
  Private Function setupTrackingEnv(ByRef mapObj As Object) As ITrackingEnvironment3
    Dim extentionManager As IExtensionManager = New ExtensionManagerClass()

    Dim uid As UID = New UIDClass()
    uid.Value = "esriTrackingAnalyst.TrackingEngineUtil"

    CType(extentionManager, IExtensionManagerAdmin).AddExtension(uid, mapObj)

    Dim trackingEnv As ITrackingEnvironment3 = New TrackingEnvironmentClass()
    trackingEnv.Initialize(mapObj)
    trackingEnv.EnableTemporalDisplayManagement = True
    Return trackingEnv
  End Function

End Class
