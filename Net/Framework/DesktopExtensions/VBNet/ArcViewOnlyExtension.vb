Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.esriSystem
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Framework

<ComClass(ArcViewOnlyExtension.ClassId, ArcViewOnlyExtension.InterfaceId, ArcViewOnlyExtension.EventsId), _
 ProgId("DesktopExtensionsVB.ArcViewOnlyExtension")> _
Public Class ArcViewOnlyExtension
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
    Public Const ClassId As String = "9ef36120-9c61-4207-9884-601a19f25d49"
    Public Const InterfaceId As String = "347f8040-acb7-4d92-b371-880ed3964bd8"
    Public Const EventsId As String = "442b453d-64e9-4a2c-ad4e-141973880eb2"
#End Region

    Private m_application As IApplication
    Private m_enableState As esriExtensionState

    Private Const RequiredProductCode As esriLicenseProductCode = esriLicenseProductCode.esriLicenseProductCodeBasic

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
            Return "ArcViewOnlyExtension_VB"
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
            Return "Basic Only Extension (VB.Net Sample)\r\n" + _
                "Copyright © ESRI 2006\r\n\r\n" + _
                "Only available with an Basic product license."
        End Get
    End Property

    ''' <summary>
    ''' Friendly name shown in the Extensions dialog
    ''' </summary>
    Public ReadOnly Property ProductName() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.ProductName
        Get
            Return "Basic Extension (VB.Net)"
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


