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
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry

<ComClass(AddDateTool.ClassId, AddDateTool.InterfaceId, AddDateTool.EventsId)> _
Public NotInheritable Class AddDateTool
	Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "35c6e12a-b09c-4673-8ab8-85491f31cd16"
    Public Const InterfaceId As String = "9b0c240f-ddf2-4d47-95ca-97239447d765"
    Public Const EventsId As String = "837eac70-8f4a-4d7c-8d3c-64e605be9980"
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
        MxCommands.Register(regKey)
    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_hookHelper As IHookHelper

	' A creatable COM class must have a Public Sub New() 
	' with no parameters, otherwise, the class will not be 
	' registered in the COM registry and cannot be created 
	' via CreateObject.
	Public Sub New()
		MyBase.New()

        MyBase.m_category = "CustomMapViewer"
        MyBase.m_caption = "Add Date"
        MyBase.m_message = "Adds a date element to the active view"
        MyBase.m_toolTip = "Add Date"
        MyBase.m_name = "CustomMapViewer_AddDateTool"

        Try
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
            MyBase.m_cursor = New Cursor(Me.GetType(), Me.GetType().Name + ".cur")
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
	End Sub

	Public Overrides Sub OnCreate(ByVal hook As Object)
        If m_hookHelper Is Nothing Then m_hookHelper = New HookHelperClass

        If Not hook Is Nothing Then
            Try
                m_hookHelper.Hook = hook
                If m_hookHelper.ActiveView Is Nothing Then m_hookHelper = Nothing
            Catch
                m_hookHelper = Nothing
            End Try

            'Disable if hook fails
            If m_hookHelper Is Nothing Then
                MyBase.m_enabled = False
            Else
                MyBase.m_enabled = True
            End If

        End If
	End Sub

	Public Overrides Sub OnClick()
		'TODO: Add AddDateTool.OnClick implementation
	End Sub

	Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Get the active view
        Dim pActiveView As IActiveView = m_hookHelper.ActiveView

        'Create a new text element
        Dim pTextElement As ITextElement = New TextElementClass
        'Create a text symbol
        Dim pTextSymbol As ITextSymbol = New TextSymbolClass
        pTextSymbol.Size = 25

        'Set the text element properties
        pTextElement.Symbol = pTextSymbol
        pTextElement.Text = Date.Now.ToShortDateString

        'QI for IElement
        Dim pElement As IElement
        pElement = pTextElement
        'Create a point
        Dim pPoint As IPoint
        pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)
        'Set the elements geometry
        pElement.Geometry = pPoint

        'Add the element to the graphics container
        pActiveView.GraphicsContainer.AddElement(pTextElement, 0)
        'Refresh the graphics
        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub

	Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
		'TODO: Add AddDateTool.OnMouseMove implementation
	End Sub

	Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
		'TODO: Add AddDateTool.OnMouseUp implementation
	End Sub
End Class


