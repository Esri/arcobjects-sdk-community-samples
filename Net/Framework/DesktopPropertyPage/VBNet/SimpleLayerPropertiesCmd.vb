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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem

''' <summary>
''' A command shows a simplified layer properties dialog
''' </summary>
''' <remarks>Drag and drop this command to the (feature) layer context menu
''' in ArcMap, ArcScene or ArcGlobe</remarks>
<ComClass(SimpleLayerPropertiesCmd.ClassId, SimpleLayerPropertiesCmd.InterfaceId, SimpleLayerPropertiesCmd.EventsId), _
 ProgId("DesktopPropertyPageVB.SimpleLayerPropertiesCmd")> _
Public NotInheritable Class SimpleLayerPropertiesCmd
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6a44773a-b754-4dd9-bab2-e9129d49644d"
    Public Const InterfaceId As String = "6354e86c-fbf7-41a5-98e3-0874d6b134de"
    Public Const EventsId As String = "d252683d-fd58-495f-b187-97895dd35c22"
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
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)
        GMxCommands.Register(regKey)
        SxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)
        GMxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_application As IApplication
    Private m_layerCategoryID As String = String.Empty

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = ".NET Samples"
        MyBase.m_caption = "Simple Layer Properties... (VB.Net)"
        MyBase.m_message = "Display a simplified layer property sheet"
        MyBase.m_toolTip = "Simplified layer property sheet"
        MyBase.m_name = "VBNETSamples_SimpleLayerPropCommand"
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)
            If m_application IsNot Nothing Then
                Select Case (m_application.Name)
                    Case "ArcMap"
                        m_layerCategoryID = "{1476c782-6f57-11d2-a2c6-080009b6f22b}"
                    Case "ArcScene"
                        m_layerCategoryID = "{3f82c99b-1c5f-11d4-a381-00c04f6bc619}"
                    Case "ArcGlobe"
                        m_layerCategoryID = "{720e21dc-2199-11d6-b2b3-00508bcdde28}"
                    Case Else
                        MyBase.m_enabled = False
                End Select
            Else
                MyBase.m_enabled = False
            End If
        End If

    End Sub

    Public Overrides Sub OnClick()
        Dim myPropertySheet As IComPropertySheet = New ComPropertySheetClass()
        myPropertySheet.Title = "Simplified Layer Properties (VB.Net)"
        myPropertySheet.HideHelpButton = True

        'Add by component category - all pages registered in the layer property page
        'Dim layerPropertyID As New UIDClass()
        'layerPropertyID.Value = m_layerCategoryID
        'myPropertySheet.AddCategoryID(layerPropertyID)

        'Or add page by page - but have to call Applies yourself
        myPropertySheet.ClearCategoryIDs()
        myPropertySheet.AddCategoryID(New UIDClass()) 'a dummy empty UID
        myPropertySheet.AddPage(New LayerVisibilityPage()) 'my custom page
        myPropertySheet.AddPage(New ESRI.ArcGIS.CartoUI.LayerDrawingPropertyPageClass()) 'feature layer symbology

        'Pass in layer, active view and the application
        Dim propertyObjects As ISet = New SetClass()
        Dim basicDocument As IBasicDocument = DirectCast(m_application.Document, IBasicDocument)

        propertyObjects.Add(basicDocument.ActiveView)
        propertyObjects.Add(basicDocument.SelectedLayer)    'or check ContextItem is a layer?
        propertyObjects.Add(m_application)  'optional?

        'Show the property sheet
        If myPropertySheet.CanEdit(propertyObjects) Then
            myPropertySheet.EditProperties(propertyObjects, m_application.hWnd)
        End If

    End Sub
End Class



