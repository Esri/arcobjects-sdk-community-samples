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
Option Strict Off
Option Explicit On

Public Class EnumCollapsedElts
	Implements ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature

	Private m_listElements As Generic.List(Of ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)
	Private m_maxElements As Integer
	Private m_currentIndex As Integer

	Public Sub New()
		m_listElements = New Generic.List(Of ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)
		m_maxElements = 100	' Default
	End Sub

	Protected Overrides Sub Finalize()
		m_listElements = Nothing
		MyBase.Finalize()
	End Sub

	Public Property MaxElements() As Integer
		Get
			Return m_maxElements
		End Get

		Set(ByVal value As Integer)
			m_maxElements = value
		End Set
	End Property

	Public Sub Initialize(ByRef relatedElements As ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature)
		On Error Resume Next

		m_currentIndex = 0
		m_listElements = New Generic.List(Of ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)

		relatedElements.Reset()
		Dim schemElement As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature = relatedElements.Next

		' add all Schematic feature to the list
		While (schemElement IsNot Nothing AndAlso m_listElements.Count < m_maxElements)
			m_listElements.Add(schemElement)
			schemElement = relatedElements.Next
		End While
	End Sub

#Region "IEnumSchematicElement Implementations"
	Private ReadOnly Property Count() As Integer _
	Implements ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature.Count

		Get
			Return m_listElements.Count
		End Get
	End Property

	Private Function [Next]() As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature _
 Implements ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature.Next

		If m_currentIndex < m_listElements.Count - 1 Then
			m_currentIndex = m_currentIndex + 1
			Return m_listElements(m_currentIndex - 1)
		Else
			Return Nothing
		End If
	End Function

	Private Sub Reset() _
	Implements ESRI.ArcGIS.Schematic.IEnumSchematicInMemoryFeature.Reset

		m_currentIndex = 0
	End Sub
#End Region

	Public Sub Add(ByVal value As ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)
		If m_listElements Is Nothing Then
			m_listElements = New Generic.List(Of ESRI.ArcGIS.Schematic.ISchematicInMemoryFeature)
		End If
		If m_listElements.Count < m_maxElements Then
			m_listElements.Add(value)
		End If
	End Sub


End Class