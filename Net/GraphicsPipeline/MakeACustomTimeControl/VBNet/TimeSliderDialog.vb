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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem

Partial Public Class TimeSliderDialog
  Inherits Form
  Private m_parent As CustomTimeSliderButton = Nothing

  Public Sub New(ByVal parent As CustomTimeSliderButton)
    InitializeComponent()
    m_parent = parent
    Dim timeExtent As ITimeExtent = m_parent.GetTimeExtent()

    m_datePicker.MinDate = New DateTime(timeExtent.StartTime.QueryTicks())
    m_datePicker.MaxDate = New DateTime(timeExtent.EndTime.QueryTicks())
    m_datePicker.Value = m_datePicker.MinDate

  End Sub

  Private Sub TimeSlider_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles m_timeSlider.ValueChanged
    m_parent.UpdateCurrentTime(0.01 * CDbl(m_timeSlider.Value))
  End Sub

  Private Sub DateTimePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles m_datePicker.ValueChanged
    Dim ticks As Long = m_datePicker.Value.Ticks
    Dim minTicks As Long = m_datePicker.MinDate.Ticks
    Dim maxTicks As Long = m_datePicker.MaxDate.Ticks
    Dim progress As Double = (CDbl(ticks - minTicks)) / (CDbl(maxTicks - minTicks))
    m_parent.UpdateCurrentTime(progress)
  End Sub
End Class
