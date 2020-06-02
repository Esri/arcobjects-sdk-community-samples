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
Imports System.Data
Imports System.Collections
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADF

''' <summary>
''' Implementation of interface IMaps which is eventually a collection of Maps
''' </summary>
Public Class Maps : Implements IMaps, IDisposable

  'class member - using internally an ArrayList to manage the Maps collection
  Private m_array As ArrayList = Nothing

#Region "class constructor"
  Public Sub New()
    m_array = New ArrayList()
  End Sub
#End Region

#Region "IDisposable Members"

  ''' <summary>
  ''' Dispose the collection
  ''' </summary>
  Public Sub Dispose() Implements IDisposable.Dispose
    If Not m_array Is Nothing Then
      m_array.Clear()
      m_array = Nothing
    End If
  End Sub

#End Region

#Region "IMaps Members"

  ''' <summary>
  ''' Add the given Map to the collection
  ''' </summary>
  ''' <param name="Map"></param>
  Public Sub Add(ByVal Map As ESRI.ArcGIS.Carto.IMap) Implements ESRI.ArcGIS.Carto.IMaps.Add
    If Map Is Nothing Then
      Throw New Exception("Maps::Add:" & Constants.vbCrLf & "New Map is mot initialized!")
    End If

    m_array.Add(Map)
  End Sub

  ''' <summary>
  ''' Get the number of Maps in the collection
  ''' </summary>
  Public ReadOnly Property Count() As Integer Implements ESRI.ArcGIS.Carto.IMaps.Count
    Get
      Return m_array.Count
    End Get
  End Property

  ''' <summary>
  ''' Create a new Map, add it to the collection and return it to the caller
  ''' </summary>
  ''' <returns></returns>
  Public Function Create() As ESRI.ArcGIS.Carto.IMap Implements ESRI.ArcGIS.Carto.IMaps.Create
    Dim newMap As IMap = New MapClass()
    m_array.Add(newMap)

    Return newMap
  End Function

  ''' <summary>
  ''' Return the Map at the given index
  ''' </summary>
  ''' <param name="Index"></param>
  ''' <returns></returns>
  Public ReadOnly Property Item(ByVal Index As Integer) As ESRI.ArcGIS.Carto.IMap Implements ESRI.ArcGIS.Carto.IMaps.Item
    Get
      If Index > m_array.Count OrElse Index < 0 Then
        Throw New Exception("Maps::Item:" & Constants.vbCrLf & "Index is out of range!")
      End If

      Return TryCast(m_array(Index), IMap)
    End Get
  End Property

  ''' <summary>
  ''' Remove the instance of the given Map
  ''' </summary>
  ''' <param name="Map"></param>
  Public Sub Remove(ByVal Map As ESRI.ArcGIS.Carto.IMap) Implements ESRI.ArcGIS.Carto.IMaps.Remove
    m_array.Remove(Map)
  End Sub

  ''' <summary>
  ''' Remove the Map at the given index
  ''' </summary>
  ''' <param name="Index"></param>
  Public Sub RemoveAt(ByVal Index As Integer) Implements ESRI.ArcGIS.Carto.IMaps.RemoveAt
    If Index > m_array.Count OrElse Index < 0 Then
      Throw New Exception("Maps::RemoveAt:" & Constants.vbCrLf & "Index is out of range!")
    End If

    m_array.RemoveAt(Index)
  End Sub

  ''' <summary>
  ''' Reset the Maps array
  ''' </summary>
  Public Sub Reset() Implements ESRI.ArcGIS.Carto.IMaps.Reset
    m_array.Clear()
  End Sub
#End Region
End Class