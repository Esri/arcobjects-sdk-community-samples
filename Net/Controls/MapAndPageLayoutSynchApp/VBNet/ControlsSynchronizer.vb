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
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.IO
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI

''' <summary>
''' This class is used to synchronize a given PageLayoutControl and a MapControl.
''' When initialized, the user must pass the reference of these control to the class, bind
''' the control together by calling 'BindControls' which in turn sets a joined Map referenced
''' by both control; and set all the buddy controls joined between these two controls.
''' When alternating between the MapControl and PageLayoutControl, you should activate the visible control 
''' and deactivate the other by calling ActivateXXX.
''' This class is limited to a situation where the controls are not simultaneously visible. 
''' </summary>
Public Class ControlsSynchronizer
#Region "class members"
  Private m_mapControl As IMapControl3 = Nothing
  Private m_pageLayoutControl As IPageLayoutControl2 = Nothing
  Private m_mapActiveTool As ITool = Nothing
  Private m_pageLayoutActiveTool As ITool = Nothing
  Private m_IsMapCtrlactive As Boolean = True

  Private m_frameworkControls As ArrayList = Nothing
#End Region

#Region "constructor"

  ''' <summary>
  ''' default constructor
  ''' </summary>
  Public Sub New()
    'initialize the underlying ArrayList
    m_frameworkControls = New ArrayList()
  End Sub

  ''' <summary>
  ''' class constructor
  ''' </summary>
  ''' <param name="mapControl"></param>
  ''' <param name="pageLayoutControl"></param>
  Public Sub New(ByVal mapControl As IMapControl3, ByVal pageLayoutControl As IPageLayoutControl2)
    Me.New()
    'assign the class members
    m_mapControl = mapControl
    m_pageLayoutControl = pageLayoutControl
  End Sub
#End Region

#Region "properties"
  ''' <summary>
  ''' Gets or sets the MapControl
  ''' </summary>
  Public Property MapControl() As IMapControl3
    Get
      Return m_mapControl
    End Get
    Set(ByVal value As IMapControl3)
      m_mapControl = Value
    End Set
  End Property

  ''' <summary>
  ''' Gets or sets the PageLayoutControl
  ''' </summary>
  Public Property PageLayoutControl() As IPageLayoutControl2
    Get
      Return m_pageLayoutControl
    End Get
    Set(ByVal value As IPageLayoutControl2)
      m_pageLayoutControl = Value
    End Set
  End Property

  ''' <summary>
  ''' Get an indication of the type of the currently active view
  ''' </summary>
  Public ReadOnly Property ActiveViewType() As String
    Get
      If m_IsMapCtrlactive Then
        Return "MapControl"
      Else
        Return "PageLayoutControl"
      End If
    End Get
  End Property

  ''' <summary>
  ''' get the active control
  ''' </summary>
  Public ReadOnly Property ActiveControl() As Object
    Get
      If m_mapControl Is Nothing OrElse m_pageLayoutControl Is Nothing Then
        Throw New Exception("ControlsSynchronizer::ActiveControl:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
      End If

      If m_IsMapCtrlactive Then
        Return m_mapControl.Object
      Else
        Return m_pageLayoutControl.Object
      End If
    End Get
  End Property
#End Region

#Region "Methods"
  ''' <summary>
  ''' Activate the MapControl and deactivate the PagleLayoutControl
  ''' </summary>
  Public Sub ActivateMap()
    Try
      If m_pageLayoutControl Is Nothing OrElse m_mapControl Is Nothing Then
        Throw New Exception("ControlsSynchronizer::ActivateMap:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
      End If

      'cache the current tool of the PageLayoutControl
      If Not m_pageLayoutControl.CurrentTool Is Nothing Then
        m_pageLayoutActiveTool = m_pageLayoutControl.CurrentTool
      End If

      'deactivate the PagleLayout
      m_pageLayoutControl.ActiveView.Deactivate()

      'activate the MapControl
      m_mapControl.ActiveView.Activate(m_mapControl.hWnd)

      'assign the last active tool that has been used on the MapControl back as the active tool
      If Not m_mapActiveTool Is Nothing Then
        m_mapControl.CurrentTool = m_mapActiveTool
      End If

      m_IsMapCtrlactive = True

      'on each of the framework controls, set the Buddy control to the MapControl
      Me.SetBuddies(m_mapControl.Object)
    Catch ex As Exception
      Throw New Exception(String.Format("ControlsSynchronizer::ActivateMap:" & Constants.vbCrLf & "{0}", ex.Message))
    End Try
  End Sub

  ''' <summary>
  ''' Activate the PagleLayoutControl and deactivate the MapCotrol
  ''' </summary>
  Public Sub ActivatePageLayout()
    Try
      If m_pageLayoutControl Is Nothing OrElse m_mapControl Is Nothing Then
        Throw New Exception("ControlsSynchronizer::ActivatePageLayout:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
      End If

      'cache the current tool of the MapControl
      If Not m_mapControl.CurrentTool Is Nothing Then
        m_mapActiveTool = m_mapControl.CurrentTool
      End If

      'deactivate the MapControl
      m_mapControl.ActiveView.Deactivate()

      'activate the PageLayoutControl
      m_pageLayoutControl.ActiveView.Activate(m_pageLayoutControl.hWnd)

      'assign the last active tool that has been used on the PageLayoutControl back as the active tool
      If Not m_pageLayoutActiveTool Is Nothing Then
        m_pageLayoutControl.CurrentTool = m_pageLayoutActiveTool
      End If

      m_IsMapCtrlactive = False

      'on each of the framework controls, set the Buddy control to the PageLayoutControl
      Me.SetBuddies(m_pageLayoutControl.Object)
    Catch ex As Exception
      Throw New Exception(String.Format("ControlsSynchronizer::ActivatePageLayout:" & Constants.vbCrLf & "{0}", ex.Message))
    End Try
  End Sub

  ''' <summary>
  ''' given a new map, replaces the PageLayoutControl and the MapControl's focus map
  ''' </summary>
  ''' <param name="newMap"></param>
  Public Sub ReplaceMap(ByVal newMap As IMap)
    If newMap Is Nothing Then
      Throw New Exception("ControlsSynchronizer::ReplaceMap:" & Constants.vbCrLf & "New map for replacement is not initialized!")
    End If

    If m_pageLayoutControl Is Nothing OrElse m_mapControl Is Nothing Then
      Throw New Exception("ControlsSynchronizer::ReplaceMap:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
    End If

    'create a new instance of IMaps collection which is needed by the PageLayout
    Dim maps As IMaps = New Maps()
    'add the new map to the Maps collection
    maps.Add(newMap)

    Dim bIsMapActive As Boolean = m_IsMapCtrlactive

    'call replace map on the PageLayout in order to replace the focus map
    'we must call ActivatePageLayout, since it is the control we call 'ReplaceMaps'
    Me.ActivatePageLayout()
    m_pageLayoutControl.PageLayout.ReplaceMaps(maps)

    'assign the new map to the MapControl
    m_mapControl.Map = newMap

    'reset the active tools
    m_pageLayoutActiveTool = Nothing
    m_mapActiveTool = Nothing

    'make sure that the last active control is activated
    If bIsMapActive Then
      Me.ActivateMap()
      m_mapControl.ActiveView.Refresh()
    Else
      Me.ActivatePageLayout()
      m_pageLayoutControl.ActiveView.Refresh()
    End If
  End Sub

  ''' <summary>
  ''' bind the MapControl and PageLayoutControl together by assigning a new joint focus map
  ''' </summary>
  ''' <param name="mapControl"></param>
  ''' <param name="pageLayoutControl"></param>
  ''' <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
  Public Sub BindControls(ByVal mapControl As IMapControl3, ByVal pageLayoutControl As IPageLayoutControl2, ByVal activateMapFirst As Boolean)
    If mapControl Is Nothing OrElse pageLayoutControl Is Nothing Then
      Throw New Exception("ControlsSynchronizer::BindControls:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
    End If

    m_mapControl = Me.MapControl
    m_pageLayoutControl = pageLayoutControl

    Me.BindControls(activateMapFirst)
  End Sub

  ''' <summary>
  ''' bind the MapControl and PageLayoutControl together by assigning a new joint focus map 
  ''' </summary>
  ''' <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
  Public Sub BindControls(ByVal activateMapFirst As Boolean)
    If m_pageLayoutControl Is Nothing OrElse m_mapControl Is Nothing Then
      Throw New Exception("ControlsSynchronizer::BindControls:" & Constants.vbCrLf & "Either MapControl or PageLayoutControl are not initialized!")
    End If

    'create a new instance of IMap
    Dim newMap As IMap = New MapClass()
    newMap.Name = "Map"

    'create a new instance of IMaps collection which is needed by the PageLayout
    Dim maps As IMaps = New Maps()
    'add the new Map instance to the Maps collection
    maps.Add(newMap)

    'call replace map on the PageLayout in order to replace the focus map
    m_pageLayoutControl.PageLayout.ReplaceMaps(maps)
    'assign the new map to the MapControl
    m_mapControl.Map = newMap

    'reset the active tools
    m_pageLayoutActiveTool = Nothing
    m_mapActiveTool = Nothing

    'make sure that the last active control is activated
    If activateMapFirst Then
      Me.ActivateMap()
    Else
      Me.ActivatePageLayout()
    End If
  End Sub

  ''' <summary>
  '''by passing the application's toolbars and TOC to the synchronization class, it saves you the
  '''management of the buddy control each time the active control changes. This method ads the framework
  '''control to an array; once the active control changes, the class iterates through the array and 
    '''calls SetBuddyControl on each of the stored framework control.
  ''' </summary>
  ''' <param name="control"></param>
  Public Sub AddFrameworkControl(ByVal control As Object)
    If control Is Nothing Then
      Throw New Exception("ControlsSynchronizer::AddFrameworkControl:" & Constants.vbCrLf & "Added control is not initialized!")
    End If

    m_frameworkControls.Add(control)
  End Sub

  ''' <summary>
  ''' Remove a framework control from the managed list of controls
  ''' </summary>
  ''' <param name="control"></param>
  Public Sub RemoveFrameworkControl(ByVal control As Object)
    If control Is Nothing Then
      Throw New Exception("ControlsSynchronizer::RemoveFrameworkControl:" & Constants.vbCrLf & "Control to be removed is not initialized!")
    End If

    m_frameworkControls.Remove(control)
  End Sub

  ''' <summary>
  ''' Remove a framework control from the managed list of controls by specifying its index in the list
  ''' </summary>
  ''' <param name="index"></param>
  Public Sub RemoveFrameworkControlAt(ByVal index As Integer)
    If m_frameworkControls.Count < index Then
      Throw New Exception("ControlsSynchronizer::RemoveFrameworkControlAt:" & Constants.vbCrLf & "Index is out of range!")
    End If

    m_frameworkControls.RemoveAt(index)
  End Sub

  ''' <summary>
  ''' when the active control changes, the class iterates through the array of the framework controls
    '''  and calls SetBuddyControl on each of the controls.
  ''' </summary>
  ''' <param name="buddy">the active control</param>
  Private Sub SetBuddies(ByVal buddy As Object)
    Try
      If buddy Is Nothing Then
        Throw New Exception("ControlsSynchronizer::SetBuddies:" & Constants.vbCrLf & "Target Buddy Control is not initialized!")
      End If

      For Each obj As Object In m_frameworkControls
        If TypeOf obj Is IToolbarControl Then
          CType(obj, IToolbarControl).SetBuddyControl(buddy)
        ElseIf TypeOf obj Is ITOCControl Then
          CType(obj, ITOCControl).SetBuddyControl(buddy)
        End If
      Next obj
    Catch ex As Exception
      Throw New Exception(String.Format("ControlsSynchronizer::SetBuddies:" & Constants.vbCrLf & "{0}", ex.Message))
    End Try
  End Sub
#End Region
End Class
