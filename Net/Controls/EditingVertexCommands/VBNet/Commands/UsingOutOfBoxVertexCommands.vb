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


<ComClass(UsingOutOfBoxVertexCommands.ClassId, UsingOutOfBoxVertexCommands.InterfaceId, UsingOutOfBoxVertexCommands.EventsId), _
 ProgId("VertexCommands_VB.UsingOutOfBoxVertexCommands")> _
Public NotInheritable Class UsingOutOfBoxVertexCommands
    Inherits BaseTool
    Implements ICommandSubType


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "E7083AC5-E3FD-41d4-AA83-E6CA65E5C5E0"
    Public Const InterfaceId As String = "3913FFBF-EAF0-453e-89D5-104B072C6138"
    Public Const EventsId As String = "4359BAD5-9645-4c60-A570-46E5084E24F9"
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

            Return True

        End Get
    End Property

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        Try
            'set the x,y location which will be used by the out-of-the-box commands         
            Dim editsketch As IEngineEditSketch = m_engineEditor
            editsketch.SetEditLocation(X, Y)

            Dim t As Type = Nothing
            Dim o As Object = Nothing

            Select Case (m_lSubType)

                Case 1 'Insert Vertex using out-of-the-box command 

                    t = Type.GetTypeFromProgID("esriControls.ControlsEditingVertexInsertCommand.1")
                    o = Activator.CreateInstance(t)
                    Dim insertVertexCommand As ICommand = o

                    If Not insertVertexCommand Is Nothing Then
                        insertVertexCommand.OnCreate(m_hookHelper.Hook)
                        insertVertexCommand.OnClick()
                    End If


                Case 2 'Delete Vertex using out-of-the-box command

                    t = Type.GetTypeFromProgID("esriControls.ControlsEditingVertexDeleteCommand.1")
                    o = Activator.CreateInstance(t)
                    Dim deleteVertexCommand As ICommand = o

                    If Not deleteVertexCommand Is Nothing Then
                        deleteVertexCommand.OnCreate(m_hookHelper.Hook)
                        deleteVertexCommand.OnClick()
                    End If

            End Select

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

                Case 1 'Insert Vertex

                    MyBase.m_caption = rm.GetString("OOBInsertVertex_CommandCaption")
                    MyBase.m_message = rm.GetString("OOBInsertVertex_CommandMessage")
                    MyBase.m_toolTip = rm.GetString("OOBInsertVertex_CommandToolTip")
                    MyBase.m_name = "VertexCommands_VB_UsingOutOfBoxInsertVertex"
                    MyBase.m_cursor = m_InsertVertexCursor

                    Try
                        MyBase.m_bitmap = rm.GetObject("OOBInsertVertex")
                    Catch ex As Exception
                        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
                    End Try


                Case 2 'Delete vertex

                    MyBase.m_caption = rm.GetString("OOBDeleteVertex_CommandCaption")
                    MyBase.m_message = rm.GetString("OOBDeleteVertex_CommandMessage")
                    MyBase.m_toolTip = rm.GetString("OOBDeleteVertex_CommandToolTip")
                    MyBase.m_name = "VertexCommands_VB_UsingOutOfBoxDeleteVertex"
                    MyBase.m_cursor = m_DeleteVertexCursor

                    Try
                        MyBase.m_bitmap = rm.GetObject("OOBDeleteVertex")
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
