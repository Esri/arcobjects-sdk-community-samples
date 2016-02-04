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

<ComClass(ArcEditorOnlyExtension.ClassId, ArcEditorOnlyExtension.InterfaceId, ArcEditorOnlyExtension.EventsId), _
 ProgId("DesktopExtensionsVB.ArcEditorOnlyExtension")> _
Public Class ArcEditorOnlyExtension
    Implements IExtension
    Implements IExtensionConfig

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
    Public Const ClassId As String = "4be1be36-8bf4-404f-bd27-373436396d2a"
    Public Const InterfaceId As String = "1413fac5-b844-4ad5-8020-80c3e8cdc10c"
    Public Const EventsId As String = "c3c7f727-198f-4978-ae57-b11297cf16bc"
#End Region

    Private m_application As IApplication
    Private m_enableState As esriExtensionState

    Private Const RequiredProductCode As esriLicenseProductCode = esriLicenseProductCode.esriLicenseProductCodeStandard

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Determine extension state
    ''' </summary>
    Private Function StateCheck(ByVal requestEnable As Boolean) As esriExtensionState
        'Turn on or off extension directly 
        If requestEnable Then
            'Check if the correct product is licensed
            Dim aoInitTestProduct As IAoInitialize = New AoInitializeClass()
            Dim prodCode As esriLicenseProductCode = aoInitTestProduct.InitializedProduct()
            If prodCode = RequiredProductCode Then _
                Return esriExtensionState.esriESEnabled

            Return esriExtensionState.esriESUnavailable
        Else
            Return esriExtensionState.esriESDisabled
        End If
    End Function

#Region "IExtension Members"
    ''' <summary>
    ''' Name of extension. Do not exceed 31 characters
    ''' </summary>
    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.esriSystem.IExtension.Name
        Get
            Return "ArcEditorOnlyExtension_VB"
        End Get
    End Property

    Public Sub Shutdown() Implements ESRI.ArcGIS.esriSystem.IExtension.Shutdown
        m_application = Nothing
    End Sub

    Public Sub Startup(ByRef initializationData As Object) Implements ESRI.ArcGIS.esriSystem.IExtension.Startup
        m_application = CType(initializationData, IApplication)
        If m_application Is Nothing Then Return

    End Sub
#End Region

#Region "IExtensionConfig Members"
    Public ReadOnly Property Description() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.Description
        Get
      Return "Standard Only Extension (VB.Net Sample)\r\n" + _
                "Copyright � ESRI 2006\r\n\r\n" + _
                "Only available with an Standard product license."
        End Get
    End Property

    ''' <summary>
    ''' Friendly name shown in the Extensions dialog
    ''' </summary>
    Public ReadOnly Property ProductName() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.ProductName
        Get
      Return "Standard Extension (VB.Net)"
        End Get
    End Property

    Public Property State() As ESRI.ArcGIS.esriSystem.esriExtensionState Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.State
        Get
            Return m_enableState
        End Get
        Set(ByVal value As ESRI.ArcGIS.esriSystem.esriExtensionState)
            If m_enableState <> 0 And value = m_enableState Then Exit Property

            'Check if ok to enable or disable extension
            Dim requestState As esriExtensionState = value
            If requestState = esriExtensionState.esriESEnabled Then
                'Cannot enable if it's already in unavailable state
                If m_enableState = esriExtensionState.esriESUnavailable Then
                    Throw New COMException("Not running the appropriate product license.")
                End If

                'Determine if state can be changed
                Dim checkState As esriExtensionState = StateCheck(True)
                m_enableState = checkState

                If m_enableState = esriExtensionState.esriESUnavailable Then
                    Throw New COMException("Not running the appropriate product license.")
                End If

            ElseIf requestState = esriExtensionState.esriESDisabled Then
                'Determine if state can be changed
                Dim checkState As esriExtensionState = StateCheck(False)
                If (m_enableState <> checkState) Then m_enableState = checkState
            End If
        End Set
    End Property
#End Region


End Class