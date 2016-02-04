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
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(LogExtension.ClassId, LogExtension.InterfaceId, LogExtension.EventsId), _
 ProgId("OpenSaveLogExtensionVB.LogExtension")> _
Public Class LogExtension
    Implements IExtension
    Implements IPersistVariant
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
        GMxExtensions.Register(regKey)
        MxExtension.Register(regKey)
        SxExtensions.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GMxExtensions.Unregister(regKey)
        MxExtension.Unregister(regKey)
        SxExtensions.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8545752a-d04c-4d92-84cd-65a5dd5f8de8"
    Public Const InterfaceId As String = "fdd2bdab-568d-490c-9ca3-916195a88ad2"
    Public Const EventsId As String = "07ba5ee7-1796-446f-947f-ad342153d3d4"
#End Region

    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

#Region "Add Event Wiring for Open Documents"
    'Event member variables
    Private m_docEvents As IDocumentEvents_Event

    'Wiring
    Private Sub SetUpDocumentEvent(ByVal myDocument As IDocument)
        m_docEvents = CType(myDocument, IDocumentEvents_Event)
        AddHandler m_docEvents.OpenDocument, AddressOf OnOpenDocument

        'Optional, new and close events
        AddHandler m_docEvents.NewDocument, AddressOf OnNewDocument
        AddHandler m_docEvents.CloseDocument, AddressOf OnCloseDocument
    End Sub

    Private Sub OnOpenDocument()
        Debug.WriteLine("Open document", "Sample Extension (VB.Net)")

        Dim logText As String = "Document '" + m_application.Document.Title + "'" _
                            + " opened by " + Environment.UserName _
                            + " at " + DateTime.Now.ToLongTimeString()
        LogMessage(logText)
    End Sub

    Private Sub OnNewDocument()
        Debug.WriteLine("New document", "Sample Extension (VB.Net)")
    End Sub

    Private Sub OnCloseDocument()
        Debug.WriteLine("Close document", "Sample Extension (VB.Net)")
    End Sub
#End Region

#Region "IExtension Implementations"
    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.esriSystem.IExtension.Name
        Get
            Return "OpenSaveLogExtensionVB"
        End Get
    End Property

    Public Sub Shutdown() Implements ESRI.ArcGIS.esriSystem.IExtension.Shutdown
        m_docEvents = Nothing
        m_application = Nothing
    End Sub

    Public Sub Startup(ByRef initializationData As Object) Implements ESRI.ArcGIS.esriSystem.IExtension.Startup
        m_application = TryCast(initializationData, IApplication)
        SetUpDocumentEvent(m_application.Document)
    End Sub
#End Region

#Region "IPersistVariant Implementations"
    Public ReadOnly Property ID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        Get
            Dim extUID As New UIDClass()
            extUID.Value = Me.GetType().GUID.ToString("B")
            Return extUID
        End Get
    End Property

    Public Sub Load(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Load
        Marshal.ReleaseComObject(Stream)
    End Sub

    Public Sub Save(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Save
        Debug.WriteLine("Save document", "Sample Extension (VB.Net)")
        LogMessage("Document '" + m_application.Document.Title + "'" _
                            + " saved by " + Environment.UserName _
                            + " at " + DateTime.Now.ToLongTimeString())
        Marshal.ReleaseComObject(Stream)
    End Sub
#End Region

    Private Sub LogMessage(ByVal message As String)
        m_application.StatusBar.Message(0) = message
    End Sub
End Class


