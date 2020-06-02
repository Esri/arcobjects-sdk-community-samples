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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem

<ComClass(ReshapePolylineEditTask.ClassId, ReshapePolylineEditTask.InterfaceId, ReshapePolylineEditTask.EventsId), _
 ProgId("ReshapePolylineEditTask_VB.ReshapePolylineEditTask")> _
Public Class ReshapePolylineEditTask

    Implements ESRI.ArcGIS.Controls.IEngineEditTask


#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)
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
    Public Const ClassId As String = "3ee19d65-bcb9-4823-995a-a2779d664332"
    Public Const InterfaceId As String = "2f536850-a5b7-48b7-a3e6-337dd5d3d9ec"
    Public Const EventsId As String = "c83dddf2-f231-4f7d-96b0-630d06e49f97"
#End Region

#Region "Private Members"
    Private m_engineEditor As IEngineEditor
    Private m_editSketch As IEngineEditSketch
    Private m_editLayer As IEngineEditLayers
    Private m_ActiveViewEventsAfterDraw As IActiveViewEvents_AfterDrawEventHandler
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub Activate(ByVal editor As ESRI.ArcGIS.Controls.IEngineEditor, ByVal oldTask As ESRI.ArcGIS.Controls.IEngineEditTask) Implements ESRI.ArcGIS.Controls.IEngineEditTask.Activate

        If (editor Is Nothing) Then
            Return
        End If

        m_engineEditor = editor
        m_editSketch = TryCast(m_engineEditor, IEngineEditSketch)
        m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline
        m_editLayer = TryCast(m_editSketch, IEngineEditLayers)

        'Listen to engine editor events
        AddHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnTargetLayerChanged, AddressOf OnTargetLayerChanged
        AddHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnSelectionChanged, AddressOf OnSelectionChanged
        AddHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged

    End Sub

    Public Sub Deactivate() Implements ESRI.ArcGIS.Controls.IEngineEditTask.Deactivate

        m_editSketch.RefreshSketch()

        'Stop listening for engine editor events.
        RemoveHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnTargetLayerChanged, AddressOf OnTargetLayerChanged
        RemoveHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnSelectionChanged, AddressOf OnSelectionChanged
        RemoveHandler (CType(m_engineEditor, IEngineEditEvents_Event)).OnCurrentTaskChanged, AddressOf OnCurrentTaskChanged

        'Release object references.
        m_engineEditor = Nothing
        m_editSketch = Nothing
        m_editLayer = Nothing

    End Sub

    Public ReadOnly Property GroupName() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.GroupName
        Get
            'This property allows groups to be created/used in the EngineEditTaskToolControl treeview.
            'If an empty string is supplied the task will be appear in an "Other Tasks" group. 
            'In this example the Reshape Feature_VB task will appear in the existing Modify Tasks group.
            Return "Modify Tasks"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.Name
        Get
            Return "Reshape Polyline_VB"
        End Get
    End Property

    Public Sub OnDeleteSketch() Implements ESRI.ArcGIS.Controls.IEngineEditTask.OnDeleteSketch

    End Sub

    Public Sub OnFinishSketch() Implements ESRI.ArcGIS.Controls.IEngineEditTask.OnFinishSketch

        'get reference to the FeatureLayer being edited
        Dim featureLayer As IFeatureLayer = CType(m_editLayer.TargetLayer, IFeatureLayer)
        'get reference to the sketch geometry
        Dim reshapeGeom As IGeometry = m_editSketch.Geometry

        If (reshapeGeom.IsEmpty = False) Then

            'get the currently selected feature  
            Dim featureSelection As IFeatureSelection = featureLayer
            Dim selectionSet As ISelectionSet = featureSelection.SelectionSet
            Dim featureCursor As IFeatureCursor = Nothing

            selectionSet.Search(Nothing, False, featureCursor)

            'the enabled property has already checked that only 1 feature is selected
            Dim feature As IFeature = featureCursor.NextFeature()

            'Take a copy of geometry for the selected feature
            Dim editShape As IGeometry = feature.ShapeCopy

            'create a path from the editsketch geometry
            Dim reshapePath As IPointCollection = New PathClass()
            reshapePath.AddPointCollection(reshapeGeom)

            'reshape the selected feature
            Dim polyline As IPolyline = editShape
            polyline.Reshape(reshapePath)

            Try

                m_engineEditor.StartOperation()
                feature.Shape = editShape
                feature.Store()
                m_engineEditor.StopOperation("Reshape Feature")

            Catch ex As Exception

                m_engineEditor.AbortOperation()
                System.Diagnostics.Trace.WriteLine(ex.Message, "Edit Reshape Feature Failed")

            End Try

        End If

        'refresh the display 
        Dim activeView As IActiveView = CType(m_engineEditor.Map, IActiveView)
        activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, featureLayer, activeView.Extent)

    End Sub

    Public ReadOnly Property UniqueName() As String Implements ESRI.ArcGIS.Controls.IEngineEditTask.UniqueName
        Get
            Return "ReshapePolylineEditTask_Reshape Polyline_VB"
        End Get
    End Property

#Region "Event Handlers"

    Private Sub OnTargetLayerChanged()
        PerformSketchToolEnabledChecks()
    End Sub

    Private Sub OnSelectionChanged()
        PerformSketchToolEnabledChecks()
    End Sub

    Private Sub OnCurrentTaskChanged()
        If (m_engineEditor.CurrentTask.Name = "Reshape Polyline_VB") Then
            PerformSketchToolEnabledChecks()
        End If

    End Sub

#End Region

#Region "Private Methods"
    Private Sub PerformSketchToolEnabledChecks()

        If m_editLayer Is Nothing Then
            Return
        End If

        'Only enable the sketch tool if there is a polyline target layer.
        If m_editLayer.TargetLayer.FeatureClass.ShapeType <> esriGeometryType.esriGeometryPolyline Then
            m_editSketch.GeometryType = esriGeometryType.esriGeometryNull
            Return
        End If

        'check that only one feature in the target layer is currently selected
        Dim featureSelection As IFeatureSelection = CType(m_editLayer.TargetLayer, IFeatureSelection)
        Dim selectionSet As ISelectionSet = featureSelection.SelectionSet

        If (selectionSet.Count <> 1) Then
            m_editSketch.GeometryType = esriGeometryType.esriGeometryNull
            Return
        End If


        m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline

    End Sub
#End Region

End Class


