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
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Resources
Imports System.Reflection

Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Display


<ComClass(CustomVertexCommands.ClassId, CustomVertexCommands.InterfaceId, CustomVertexCommands.EventsId), _
 ProgId("VertexCommands_VB.CustomVertexCommands")> _
Public NotInheritable Class CustomVertexCommands
    Inherits BaseTool
    Implements ICommandSubType


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "3131C3B2-CA12-4c3b-9698-123AB30557E9"
    Public Const InterfaceId As String = "B697F1A4-5DF1-477f-B9C8-B4A047AAF284"
    Public Const EventsId As String = "D77B1014-2E86-4176-B6F2-64873F3CDF0C"
#End Region

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
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "private members"
    Private m_hookHelper As IHookHelper
    Private m_engineEditor As IEngineEditor
    Private m_editLayer As IEngineEditLayers
    Private m_lSubType As Long

    Private m_InsertVertexCursor As System.Windows.Forms.Cursor
    Private m_DeleteVertexCursor As System.Windows.Forms.Cursor
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New()

        'load the cursors
        Try
            m_InsertVertexCursor = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream("VertexCommands_VB.InsertVertexCursor.cur"))
            m_DeleteVertexCursor = New System.Windows.Forms.Cursor(Me.GetType().Assembly.GetManifestResourceStream("VertexCommands_VB.DeleteVertexCursor.cur"))

        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Cursor")
        End Try

    End Sub
#End Region

#Region "class overrides"

    Public Overrides Sub OnClick()
        'Find the Modify Feature task and set it as the current task
        Dim editTask As IEngineEditTask = m_engineEditor.GetTaskByUniqueName("ControlToolsEditing_ModifyFeatureTask")
        m_engineEditor.CurrentTask = editTask
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        If (m_hookHelper Is Nothing) Then m_hookHelper = New HookHelperClass

        If Not hook Is Nothing Then
            m_hookHelper.Hook = hook
            m_engineEditor = New EngineEditorClass() 'this class is a singleton
            m_editLayer = CType(m_engineEditor, IEngineEditLayers)
        End If

    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            'check whether Editing 
            If (m_engineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing) Then
                Return False
            End If

            'check for appropriate geometry types
            Dim geomType As esriGeometryType = m_editLayer.TargetLayer.FeatureClass.ShapeType
            If Not (geomType = esriGeometryType.esriGeometryPolyline Or geomType = esriGeometryType.esriGeometryPolygon) Then
                Return False
            End If

            'check that only one feature is currently selected
            Dim featureSelection As IFeatureSelection = CType(m_editLayer.TargetLayer, IFeatureSelection)
            Dim selectionSet As ISelectionSet = featureSelection.SelectionSet
            If selectionSet.Count <> 1 Then
                Return False
            End If

            'conditions have been met so enable the tools
            Return True

        End Get
    End Property

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        Try

            Dim editSketch As IEngineEditSketch = CType(m_engineEditor, IEngineEditSketch)
            Dim editShape As IGeometry = editSketch.Geometry

            'location clicked as a point object
            Dim clickedPt As IPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

            'local variables used in the HitTest
            Dim hitShape As IHitTest = CType(editShape, IHitTest)
            Dim hitPoint As IPoint = New PointClass()
            Dim hitDistance As Double = 0
            Dim hitPartIndex As Integer = 0
            Dim hitSegmentIndex As Integer = 0
            Dim bRightSide As Boolean = False
            Dim hitPartType As esriGeometryHitPartType = esriGeometryHitPartType.esriGeometryPartNone

            'the searchRadius is the maximum distance away, in map units, from the shape that will be used
            'for the test - change to an appropriate value.
            Dim searchRadius As Double = 1
            Select Case m_lSubType
                Case 1
                    hitPartType = esriGeometryHitPartType.esriGeometryPartBoundary
                Case 2
                    hitPartType = esriGeometryHitPartType.esriGeometryPartVertex
            End Select

            hitShape.HitTest(clickedPt, searchRadius, hitPartType, hitPoint, hitDistance, hitPartIndex, hitSegmentIndex, bRightSide)

            'check whether the HitTest was successful (i.e within the search radius)
            If hitPoint.IsEmpty = False Then
                Dim sketchOp As IEngineSketchOperation = New EngineSketchOperationClass()
                sketchOp.Start(m_engineEditor)

                'Get the PointCollection for a specific path or ring by hitPartIndex to handle multi-part features
                Dim geomeTryCol As IGeometryCollection = CType(editShape, IGeometryCollection)
                Dim pathOrRingPointCollection As IPointCollection = geomeTryCol.Geometry(hitPartIndex)
                Dim missing As Object = Type.Missing
                Dim hitSegmentIndexObject As Object = New Object()
                hitSegmentIndexObject = hitSegmentIndex
                Dim partIndexObject As Object = New Object()
                partIndexObject = hitPartIndex
                Dim opType As esriEngineSketchOperationType = esriEngineSketchOperationType.esriEngineSketchOperationGeneral

                Select Case m_lSubType
                    Case 1  'Insert Vertex 
                        'add new vertex to the path or ring PointCollection
                        pathOrRingPointCollection.AddPoint(clickedPt, missing, hitSegmentIndexObject)
                        sketchOp.SetMenuString("Insert Vertex (Custom)")
                        opType = esriEngineSketchOperationType.esriEngineSketchOperationVertexAdded
                    Case 2  'Delete Vertex.
                        'delete a vertex from the path or ring PointCollection
                        pathOrRingPointCollection.RemovePoints(hitSegmentIndex, 1)
                        sketchOp.SetMenuString("Delete Vertex (Custom)")
                        opType = esriEngineSketchOperationType.esriEngineSketchOperationVertexDeleted
                End Select

                'remove the old PointCollection from the GeometryCollection and replace with the new one
                geomeTryCol.RemoveGeometries(hitPartIndex, 1)
                geomeTryCol.AddGeometry(pathOrRingPointCollection, partIndexObject, missing)

                sketchOp.Finish(Nothing, opType, clickedPt)

            End If

        Catch ex As Exception

            System.Diagnostics.Trace.WriteLine(ex.Message, "Unexpected Error")
        End Try


    End Sub
#End Region

#Region "ICommandSubType implementation"
    Public Function GetCount() As Integer Implements ESRI.ArcGIS.SystemUI.ICommandSubType.GetCount
        Try
            Return 2
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message())
        End Try
    End Function

    Public Sub SetSubType(ByVal SubType As Integer) Implements ESRI.ArcGIS.SystemUI.ICommandSubType.SetSubType
        Try

            m_lSubType = SubType

            'set a common Command category for all subtypes
            MyBase.m_category = "Vertex Cmds (VB)"

            Dim rm As ResourceManager = New ResourceManager("VertexCommands_VB.ResourceFile", Assembly.GetExecutingAssembly())

            Select Case (m_lSubType)

                Case 1 'Insert Vertex using the out-of-the-box ControlsEditingSketchInsertPointCommand command

                    MyBase.m_caption = rm.GetString("InsertVertex_CommandCaption")
                    MyBase.m_message = rm.GetString("InsertVertex_CommandMessage")
                    MyBase.m_toolTip = rm.GetString("InsertVertex_CommandToolTip")
                    MyBase.m_name = "VertexCommands_VB_InsertVertexOnShape"
                    MyBase.m_cursor = m_InsertVertexCursor

                    Try
                        MyBase.m_bitmap = rm.GetObject("InsertVertex")
                    Catch ex As Exception
                        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
                    End Try

                Case 2 'Delete vertex at clicked location using the out-of-the-box ControlsEditingSketchDeletePointCommand

                    MyBase.m_caption = rm.GetString("DeleteVertex_CommandCaption")
                    MyBase.m_message = rm.GetString("DeleteVertex_CommandMessage")
                    MyBase.m_toolTip = rm.GetString("DeleteVertex_CommandToolTip")
                    MyBase.m_name = "VertexCommands_VB_DeleteVertexAtClickPoint"
                    MyBase.m_cursor = m_DeleteVertexCursor

                    Try
                        MyBase.m_bitmap = rm.GetObject("DeleteVertex")
                    Catch ex As Exception
                        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
                    End Try

            End Select

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message())
        End Try

    End Sub

#End Region

End Class
