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
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Framework

<ComClass(SimpleExtension.ClassId, SimpleExtension.InterfaceId, SimpleExtension.EventsId), _
 ProgId("DesktopExtensionsVB.SimpleExtension")> _
Public Class SimpleExtension
    Implements IExtension

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
        MxExtension.Register(regKey)
        GxExtensions.Register(regKey)
        SxExtensions.Register(regKey)
        GMxExtensions.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxExtension.Unregister(regKey)
        GxExtensions.Unregister(regKey)
        SxExtensions.Unregister(regKey)
        GMxExtensions.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "e5cf1fa5-8247-4c8f-a5e4-f4871e2be9fc"
    Public Const InterfaceId As String = "1498761f-0870-4a39-a870-454a36f4a8c0"
    Public Const EventsId As String = "c1a0c102-2f1c-4180-b800-888b08b67d2d"
#End Region

    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Name of extension. Do not exceed 31 characters
    ''' </summary>
    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.esriSystem.IExtension.Name
        Get
            Return "SimpleExtensionVB"
        End Get
    End Property

    Public Sub Shutdown() Implements ESRI.ArcGIS.esriSystem.IExtension.Shutdown
        'Clean up resources
        m_docEvents = Nothing
        m_application = Nothing
    End Sub

    Public Sub Startup(ByRef initializationData As Object) Implements ESRI.ArcGIS.esriSystem.IExtension.Startup
        m_application = CType(initializationData, IApplication)
        If m_application Is Nothing Then Return

        'Listening to document events
        SetUpDocumentEvent(m_application.Document)
    End Sub
#Region "Document Events"
    'Event member variable.
    Private m_docEvents As ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event

    'Wiring.
    Private Sub SetUpDocumentEvent(ByVal myDocument As ESRI.ArcGIS.Framework.IDocument)
        m_docEvents = CType(myDocument, ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event)
        AddHandler m_docEvents.NewDocument, AddressOf OnNewDocument
        AddHandler m_docEvents.OpenDocument, AddressOf OnOpenDocument
    End Sub

    'Event handler methods.
    Private Sub OnNewDocument()
        Debug.Print("New Document Event (VB.Net Sample)")
    End Sub

    Private Sub OnOpenDocument()
        Debug.Print("Open Document Event (VB.Net Sample)")
    End Sub
#End Region
End Class


