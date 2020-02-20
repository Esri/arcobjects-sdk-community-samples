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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display

Public Class TimeOffsetButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    Dim pMxDoc As IMxDocument = My.ArcMap.Document
    Dim pMap As IMap = pMxDoc.FocusMap
    Dim filename As String = "TimeAwareHurricanes.mxd"

    If pMap.LayerCount < 1 Then
      MessageBox.Show("Before running this sample, load the associated file '" & filename & "'")
      Return
    End If
    If pMap.Layer(0).Name <> "atlantic_hurricanes_2000" Then
      MessageBox.Show("Before running this sample, load the associated file '" & filename & "'")
      Return
    End If
    Dim selectedLayer As ILayer = My.ArcMap.Document.SelectedLayer
    If selectedLayer Is Nothing Then
      MessageBox.Show("There is no selected layer.  Select a time-aware layer")
      Return
    End If

    Dim pTZFac As ITimeZoneFactory = New TimeZoneFactoryClass()
    Dim pFLyr As IFeatureLayer = TryCast(selectedLayer, IFeatureLayer)
    Dim pTimeData As ITimeData = TryCast(pFLyr, ITimeData)

        'making the first layer of the focused map time-aware
    If pTimeData.SupportsTime Then
      pTimeData.UseTime = True
      Dim pTimeAnimProp As ITimeDataDisplay = TryCast(pFLyr, ITimeDataDisplay)
      pTimeAnimProp.TimeOffsetUnits = ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsYears
      pTimeAnimProp.TimeOffset = System.DateTime.Now.Year - 2000
    Else
      MessageBox.Show("Before running this sample, load the associated file '" & filename & "'")
      Return
    End If

    Dim pActiveView As IActiveView = TryCast(pMap, IActiveView)
    pActiveView.Refresh()
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = True
  End Sub
End Class
