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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Text
Imports System.Collections

Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs

<ComClass(CutPolygonsWithoutSelectionEditTask.ClassId, CutPolygonsWithoutSelectionEditTask.InterfaceId, CutPolygonsWithoutSelectionEditTask.EventsId), _
 ProgId("CutPolygonsWithoutSelection_VB.CutPolygonsWithoutSelectionEditTask")> _
Public Class CutPolygonsWithoutSelectionEditTask
  Implements ESRI.ArcGIS.Controls.IEngineEditTask
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    'Add any COM registration code after the ArcGISCategoryRegistration() call

  End Sub

  <ComUnregisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    EngineEditTasks.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    EngineEditTasks.Unregister(regKey)

  End Sub

#End Region
#End Region


#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "3d0e1daf-d468-4137-8955-1956cbc8da28"
  Public Const InterfaceId As String = "4cec67ad-3d4b-427f-8de8-84566d54fd27"
  Public Const EventsId As String = "3df03882-7c31-48df-af15-042a0541124a"
#End Region

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
  End Sub
#Region "Private Members"
  Private m_engineEditor As IEngineEditor
  Private m_editSketch As IEngineEditSketch
  Private m_editLayer As IEngineEditLayers
#End Region

  Public Sub Activate(ByVal editor As ESRI.ArcGIS.Controls.IEngineEditor, ByVal oldTask As ESRI.ArcGIS.Controls.IEngineEditTask) Implements ESRI.ArcGIS.Controls.IEngineEditTask.Activate
    If editor Is Nothing Then
      Return
    End If

    'Initialize class member variables. 
    m_engineEditor = editor
    m_editSketch = DirectCast(editor, IEngineEditSketch)
    m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline
    m_editLayer = DirectCast(m_editSketch, IEngineEditLayers)

    'Wire editor events. 
    AddHandler CType(m_editSketch, IEngineEditEvents_Event).OnTargetLayerChanged, AddressOf OnTargetLayerChanged
    AddHandler CType(m_editSketch, IEngineEditEvents_Event).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged

  End Sub

  Public Sub Deactivate() Implements ESRI.ArcGIS.Controls.IEngineEditTask.Deactivate
    'Stop listening for editor events. 
    RemoveHandler CType(m_engineEditor, IEngineEditEvents_Event).OnTargetLayerChanged, AddressOf OnTargetLayerChanged
    RemoveHandler CType(m_engineEditor, IEngineEditEvents_Event).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged

    'Release object references. 
    m_engineEditor = Nothing
    m_editSketch = Nothing
    m_editLayer = Nothing

  End Sub

  Public ReadOnly Property GroupName() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.GroupName
    Get
      Return "Modify Tasks"
    End Get
  End Property

  Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.Name
    Get
      Return "Cut Polygons Without Selection (VB)"
    End Get
  End Property

  Public Sub OnDeleteSketch() Implements ESRI.ArcGIS.Controls.IEngineEditTask.OnDeleteSketch

  End Sub

  Public Sub OnFinishSketch() Implements ESRI.ArcGIS.Controls.IEngineEditTask.OnFinishSketch
    If m_editSketch Is Nothing Then
      Return
    End If

    Dim hasCutPolygons As Boolean = False

    'Change the cursor to be hourglass shape. 
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

    Try
      'Get the geometry that performs the cut from the edit sketch. 
      Dim cutGeometry As IGeometry = m_editSketch.Geometry

      'The sketch geometry is simplified to deal with a multi-part sketch as well 
      'as the case where the sketch loops back over itself. 
      Dim topoOperator As ITopologicalOperator2 = DirectCast(cutGeometry, ITopologicalOperator2)
      topoOperator.IsKnownSimple_2 = False
      topoOperator.Simplify()

      'Create the spatial filter to search for features in the target feature class. 
      'The spatial relationship we care about is whether the interior of the line 
            'intersects the interior of the polygon. 
      Dim spatialFilter As ISpatialFilter = New SpatialFilterClass()
      spatialFilter.Geometry = m_editSketch.Geometry
      spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects

      'Find the polygon features that cross the sketch. 
      Dim featureClass As IFeatureClass = m_editLayer.TargetLayer.FeatureClass
      Dim featureCursor As IFeatureCursor = featureClass.Search(spatialFilter, False)

      'Only do work if there are features that intersect the edit sketch. 
      Dim origFeature As IFeature = featureCursor.NextFeature()

      If origFeature IsNot Nothing Then
        'Check the first feature to see if it is ZAware and if it needs to make the 
        'cut geometry ZAware. 
        Dim zAware As IZAware = DirectCast(origFeature.Shape, IZAware)
        If zAware.ZAware Then
          zAware = DirectCast(cutGeometry, IZAware)
          zAware.ZAware = True
        End If

        Dim comErrors As ArrayList = New ArrayList()

        'Start an edit operation so we can have undo/redo. 
        m_engineEditor.StartOperation()

        'Cycle through the features, cutting with the sketch. 
        While origFeature IsNot Nothing

          Try
            'Split the feature. Use the IFeatureEdit::Split method which ensures 
            'the attributes are correctly dealt with. 
            Dim featureEdit As IFeatureEdit = DirectCast(origFeature, IFeatureEdit)
            'Set to hold the new features that are created by the Split. 
            Dim newFeaturesSet As ISet = featureEdit.Split(cutGeometry)

            'New features have been created. 
            If newFeaturesSet IsNot Nothing Then
              newFeaturesSet.Reset()
              hasCutPolygons = True
            End If

          Catch comExc As COMException
            comErrors.Add(String.Format("OID: {0}, Error: {1} , {2}", origFeature.OID.ToString(), comExc.ErrorCode, comExc.Message))
          Finally
            'Continue to work on the next feature if it fails to split the current one. 
            origFeature = featureCursor.NextFeature()
          End Try

        End While

        'If any polygons were cut, refresh the display and stop the edit operation. 
        If hasCutPolygons Then
          'Clear the map's selection. 
          m_engineEditor.Map.ClearSelection()

                    'Refresh the display including modified layer and any previously selected component. 
          Dim activeView As IActiveView = DirectCast(m_engineEditor.Map, IActiveView)
          activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography Or esriViewDrawPhase.esriViewGeoSelection, Nothing, activeView.Extent)

          'Complete the edit operation. 
          m_engineEditor.StopOperation("Cut Polygons Without Selection")
        Else
          m_engineEditor.AbortOperation()
        End If

        'report any errors that have arisen while splitting features 
        If comErrors.Count > 0 Then
          Dim stringBuilder As New StringBuilder("The following features could not be split: " & Chr(10) & "", 200)
          For Each comError As String In comErrors
            stringBuilder.AppendLine(comError)
          Next

          MessageBox.Show(stringBuilder.ToString(), "Cut Errors")
        End If

      End If

    Catch e As Exception
      System.Windows.Forms.MessageBox.Show("Unable to perform the cut task." & Chr(10) & "" + e.ToString())
      'In the event of an error, abort the operation. 
      m_engineEditor.AbortOperation()
    Finally
      'Change the cursor shape to default. 
      System.Windows.Forms.Cursor.Current = Cursors.[Default]
    End Try

  End Sub

  Public ReadOnly Property UniqueName() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.UniqueName
    Get
      Return "CutPolygonsWithoutSelection_VB_CutPolygonsWithoutSelection_VB"
    End Get
  End Property

  'This method is not expected to be fired since we have unregistered from the event in Deactivate
  Public Sub OnCurrentTaskChanged()
    UpdateScketchToolStatus()
  End Sub

  Public Sub OnTargetLayerChanged()
    UpdateScketchToolStatus()
  End Sub
#Region "IEngineEditTask private methods"
  Private Sub UpdateScketchToolStatus()
    If m_editLayer Is Nothing Then
      Return
    End If

    'Only enable the sketch tool if there is a polygon target layer. 
    If m_editLayer.TargetLayer.FeatureClass.ShapeType <> esriGeometryType.esriGeometryPolygon Then
      m_editSketch.GeometryType = esriGeometryType.esriGeometryNull
    Else
      m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline
      'Set the edit sketch geometry type to be esriGeometryPolyline. 
    End If
  End Sub
#End Region

End Class


