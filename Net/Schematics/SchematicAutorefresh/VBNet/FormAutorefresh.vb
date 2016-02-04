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
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.SchematicUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto

Public Class FormAutorefresh

	Private m_schematicInMemoryDiagram As ISchematicInMemoryDiagram = Nothing
  Private m_application As ESRI.ArcGIS.Framework.IApplication = Nothing

	Private m_minute As Integer = 0
	Private m_second As Integer = 4

	Public Sub FormAutoRefresh()
		InitializeComponent()
	End Sub

	Public Sub InitializeMinute()

		For i As Integer = 0 To 9
			Me.IntervalMinute.Items.Add(Me.IntervalMinute.FormatString.Insert(0, i.ToString()))
		Next

		Me.IntervalMinute.SelectedIndex = m_minute
	End Sub

	Public Sub InitializeSecond()

		For i As Integer = 0 To 60
			Me.IntervalSecond.Items.Add(Me.IntervalSecond.FormatString.Insert(0, i.ToString()))
		Next

		Me.IntervalSecond.SelectedIndex = m_second
	End Sub

	Public Function GetMinute() As Integer
		Return IntervalMinute.SelectedIndex
	End Function

	Public Function GetSecond() As Integer
		Return IntervalSecond.SelectedIndex
	End Function

	Public Sub SetAutoOn(ByVal val As Boolean)
		AutoOn.Checked = val
	End Sub

	Public Sub SetAutoOff(ByVal val As Boolean)
		AutoOff.Checked = val
	End Sub


	Public Function GetAutoOn() As Boolean
		Return AutoOn.Checked
	End Function

	Public Function GetAutoOff() As Boolean
		Return AutoOff.Checked
	End Function

	Private Sub IntervalMinute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		InitializeTimer()
	End Sub

	Private Sub IntervalSecond_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		InitializeTimer()
	End Sub

	Private Sub InitializeTimer()
		'Obtains or to define the time by milliseconds
		'<summary>
		'Obtains or to define the time by milliseconds
		'</summary>
		If Me.IntervalMinute.SelectedIndex <> -1 AndAlso Me.IntervalSecond.SelectedIndex <> -1 Then
			Dim time As Integer = (Me.IntervalMinute.SelectedIndex * 60000) + (Me.IntervalSecond.SelectedIndex * 1000)
			If time > 0 Then
				timerAutoRefresh.Interval = time
			Else
				timerAutoRefresh.Stop()
			End If
		End If
	End Sub

	Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
		If Me.AutoOn.Checked Then
			InitializeTimer()
			If timerAutoRefresh.Interval > 0 Then
				timerAutoRefresh.Start()
			Else
				timerAutoRefresh.Stop()
			End If
		End If

		Me.Hide()
	End Sub

	Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
		Me.timerAutoRefresh.Stop()
		Me.IntervalSecond.SelectedIndex = m_second
		Me.IntervalMinute.SelectedIndex = m_minute
		Me.Hide()
	End Sub

	Private Sub FormAutorefresh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Me.IntervalSecond.SelectedIndex = m_second
		Me.IntervalMinute.SelectedIndex = m_minute
	End Sub

	Public Sub SetSchematicInmemoryDiagram(ByVal SchMemoryDiag As ISchematicInMemoryDiagram)
		m_schematicInMemoryDiagram = SchMemoryDiag
	End Sub

	Public Sub Appli(ByVal value As ESRI.ArcGIS.Framework.IApplication)
		m_application = value
	End Sub

	Private Sub RefreshViewerWindows()
		'refresh viewer window
		Dim applicationWindows As IApplicationWindows
		applicationWindows = TryCast(m_application, IApplicationWindows)

		Dim setWindows As ISet
		setWindows = applicationWindows.DataWindows
		If setWindows IsNot Nothing Then
			setWindows.Reset()
			Dim dataWindow As IMapInsetWindow
			dataWindow = TryCast(setWindows.Next(), IMapInsetWindow)
			While dataWindow IsNot Nothing
				dataWindow.Refresh()
				dataWindow = TryCast(setWindows.Next(), IMapInsetWindow)
			End While
		End If
	End Sub

	Private Sub timerAutoRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerAutoRefresh.Tick
		If m_schematicInMemoryDiagram IsNot Nothing AndAlso Me.AutoOn.Checked Then
			timerAutoRefresh.Stop()
			m_schematicInMemoryDiagram.Refresh()
			Dim layer As ILayer
      Dim doc As ESRI.ArcGIS.Framework.IDocument
			Dim mxDoc As IMxDocument
			Dim maps As IMaps
			Dim enumLayers As IEnumLayer
			Dim map As IMap
			Dim schematicLayer As ISchematicLayer = Nothing

			doc = m_application.Document
			mxDoc = TryCast(doc, IMxDocument)

			If mxDoc Is Nothing Then
				Exit Sub
			End If

			maps = mxDoc.Maps
			For i As Integer = 0 To maps.Count - 1
				map = maps.Item(i)
				enumLayers = map.Layers(Nothing, True)
				enumLayers.Reset()
				layer = enumLayers.Next()
				Do While (Not layer Is Nothing)
					Dim sText As String = layer.Name
					Try
						If TypeOf layer Is SchematicLayer Then
							schematicLayer = TryCast(layer, ISchematicLayer)
							If schematicLayer.SchematicInMemoryDiagram IsNot Nothing Then
								If schematicLayer.SchematicInMemoryDiagram Is m_schematicInMemoryDiagram Then
									Exit Do
								End If
							ElseIf sText = m_schematicInMemoryDiagram.Name Then
								Exit Do
							End If
						End If
					Finally
						layer = Nothing
					End Try

					schematicLayer = Nothing
					layer = enumLayers.Next()
				Loop

				If schematicLayer IsNot Nothing Then
					Dim actiView As IActiveView
					actiView = TryCast(map, IActiveView)
					actiView.Refresh()
				End If
			Next

			RefreshViewerWindows()
			timerAutoRefresh.Start()
		End If

	End Sub
End Class
