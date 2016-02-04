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
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports System.Windows.Forms

Public Class CustomTimeSliderButton
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Private m_myLayerTimeExtent As ITimeExtent = Nothing
  Private m_myLayerIntervalUnits As ESRI.ArcGIS.esriSystem.esriTimeUnits
  Private m_myLayerInterval As Double = 0
  Private m_sliderDlg As TimeSliderDialog = Nothing


  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    Dim pMxDoc As IMxDocument = My.ArcMap.Document
    If pMxDoc.SelectedLayer Is Nothing Then
      MessageBox.Show("There is no layer selected.  First select a time-aware layer.")
      Return
    End If

    Dim pFLyr As IFeatureLayer = TryCast(pMxDoc.SelectedLayer, IFeatureLayer)
    Dim pTimeData As ITimeData = TryCast(pFLyr, ITimeData)
    If (Not pTimeData.SupportsTime) Then
      MessageBox.Show("Select a time-aware layer first.")
      Return
    End If
    m_myLayerTimeExtent = pTimeData.GetFullTimeExtent()

    Dim pTimeDataDisplayProperties As ITimeDataDisplay = TryCast(pFLyr, ITimeDataDisplay)
    m_myLayerIntervalUnits = pTimeDataDisplayProperties.TimeIntervalUnits
    m_myLayerInterval = pTimeDataDisplayProperties.TimeInterval

    m_sliderDlg = New TimeSliderDialog(Me)
    m_sliderDlg.Show()
  End Sub
  Public Function GetTimeExtent() As ITimeExtent
    Return m_myLayerTimeExtent
  End Function

  Private Sub Multiply(ByVal duration As ITimeDuration, ByVal factor As Double)
    duration.SetFromTicks(CLng(Fix(duration.QueryTicks() * factor)))
  End Sub

  Public Sub UpdateCurrentTime(ByVal progress As Double)
    My.ArcMap.Application.StatusBar.Message(0) = "progress = " & progress & ", interval = " & m_myLayerInterval

    'Calculate how far into the layer to jump
    Dim offsetToNewCurrentTime As ITimeDuration = m_myLayerTimeExtent.QueryTimeDuration

    offsetToNewCurrentTime.Scale(progress)

    Dim pMxDoc As IMxDocument = My.ArcMap.Document
    Dim pMap As IMap = pMxDoc.FocusMap
    Dim pActiveView As IActiveView = TryCast(pMap, IActiveView)
    Dim pScreenDisplay As IScreenDisplay = pActiveView.ScreenDisplay
    Dim pTimeDisplay As ITimeDisplay = TryCast(pScreenDisplay, ITimeDisplay)

    Dim endTime As ITime = m_myLayerTimeExtent.StartTime
    Dim endTimeDuration As ITimeOffsetOperator
    endTimeDuration = TryCast(endTime, ITimeOffsetOperator)
    endTimeDuration.AddDuration(offsetToNewCurrentTime)

    Dim startTime As ITime = CType((CType(endTime, IClone)).Clone(), ITime)
    Dim startTimeOffsetOp As ITimeOffsetOperator
    startTimeOffsetOp = TryCast(startTime, ITimeOffsetOperator)

    Select Case m_myLayerIntervalUnits
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsYears
        startTimeOffsetOp.AddYears(-1 * m_myLayerInterval, False, True)
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsMonths
        startTimeOffsetOp.AddMonths(-1 * m_myLayerInterval, False, True)
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsDays
        startTimeOffsetOp.AddDays(-1 * m_myLayerInterval)
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsHours
        startTimeOffsetOp.AddHours(-1 * m_myLayerInterval)
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsMinutes
        startTimeOffsetOp.AddMinutes(-1 * m_myLayerInterval)
      Case ESRI.ArcGIS.esriSystem.esriTimeUnits.esriTimeUnitsSeconds
        startTimeOffsetOp.AddSeconds(-1 * m_myLayerInterval)
    End Select

    Dim pTimeExt As ITimeExtent = New TimeExtentClass()
    pTimeExt.SetExtent(startTime, endTime)
    pTimeDisplay.TimeValue = TryCast(pTimeExt, ITimeValue)
    pActiveView.Refresh()

  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = True
  End Sub
End Class
