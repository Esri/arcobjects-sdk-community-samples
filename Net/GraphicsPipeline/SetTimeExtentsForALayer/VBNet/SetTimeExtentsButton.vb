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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ArcMapUI


Public Class SetTimeExtentsButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    Dim pMxDoc As IMxDocument = My.ArcMap.Document
    Dim pMap As IMap = pMxDoc.FocusMap
    Dim sampeMapFileName As String = "BasicHurricanes.mxd"

    If pMap.LayerCount < 1 Then
      MessageBox.Show("Before running this sample, load the associated file '" & sampeMapFileName & "'")
      Return
    End If

    If pMap.Layer(0).Name <> "atlantic_hurricanes_2000" Then
      MessageBox.Show("Before running this sample, load the associated file '" & sampeMapFileName & "'")
      Return
    End If

    Dim pTZFac As ITimeZoneFactory = New TimeZoneFactoryClass()
        'making the first layer of the focused map time-aware
    Dim pFLyr As IFeatureLayer = TryCast(pMap.Layer(0), IFeatureLayer)
    Dim pTimeData As ITimeData = TryCast(pFLyr, ITimeData)

    If pTimeData.SupportsTime Then
      pTimeData.UseTime = True
      Dim pTimeDataDef As ITimeTableDefinition = TryCast(pFLyr, ITimeTableDefinition)
      pTimeDataDef.StartTimeFieldName = "Date_Time"
      pTimeDataDef.TimeReference = pTZFac.CreateTimeReferenceFromWindowsID(pTZFac.QueryLocalTimeZoneWindowsID)
      Dim pTimeAnimProp As ITimeDataDisplay = TryCast(pFLyr, ITimeDataDisplay)
      pTimeAnimProp.TimeIntervalUnits = ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsHours
      pTimeAnimProp.TimeInterval = 12.0
    End If

    '
    Dim pActiveView As IActiveView = TryCast(pMap, IActiveView)
    Dim pScreenDisplay As IScreenDisplay = pActiveView.ScreenDisplay
    Dim pTimeDisplay As ITimeDisplay = TryCast(pScreenDisplay, ITimeDisplay)
    pTimeDisplay.TimeReference = pTZFac.CreateTimeReferenceFromWindowsID(pTZFac.QueryLocalTimeZoneWindowsID)

    Dim pStartTime As ITime = New TimeClass()
    pStartTime.Year = 2000
    pStartTime.Month = 9
    pStartTime.Day = 25
    Dim pEndTime As ITime = New TimeClass()
    pEndTime.Year = 2000
    pEndTime.Month = 9
    pEndTime.Day = 30

    Dim pTimeExt As ITimeExtent = New TimeExtentClass()
    pTimeExt.StartTime = pStartTime
    pTimeExt.EndTime = pEndTime
    pTimeDisplay.TimeValue = TryCast(pTimeExt, ITimeValue)

    pActiveView.ContentsChanged()
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = True
  End Sub
End Class
