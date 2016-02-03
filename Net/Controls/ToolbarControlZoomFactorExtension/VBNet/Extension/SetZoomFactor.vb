Option Explicit On 

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(SetZoomFactor.ClassId, SetZoomFactor.InterfaceId, SetZoomFactor.EventsId)> _
Public NotInheritable Class SetZoomFactor
    Inherits BaseCommand
    Private m_Hookhelper As IHookHelper

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "0f65b096-8d64-4f03-a69d-0f976c32e6cf"
    Public Const InterfaceId As String = "83de6bce-2cf7-46e6-af2c-e396a694a34c"
    Public Const EventsId As String = "d1125967-20bc-4e55-9fdd-de5d831637f3"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction()> _
    Public Shared Sub Reg(ByVal regKey As String)
        ControlsCommands.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub Unreg(ByVal regKey As String)
        ControlsCommands.Unregister(regKey)
    End Sub
#End Region

    Public Sub New()
        MyBase.New()

        'Create an IHookHelper object
        m_Hookhelper = New HookHelperClass

        'Set the command properties
        MyBase.m_caption = "Set Variable Zoom"
        MyBase.m_message = "Set Variable Zoom"
        MyBase.m_toolTip = "Set Variable Zoom"
        MyBase.m_category = "ZoomExtension Sample(VB.NET)"
        MyBase.m_name = "ZoomExtension Sample(VB.NET)_Set Variable Zoom"
        MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomOut).Assembly.GetManifestResourceStream(GetType(ZoomOut), "zoomfactor.bmp"))

    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            'Get the extension manager
            Dim pExtensionManager As IExtensionManager = New ExtensionManagerClass

            'Get the extension from the extension manager
            Dim pExtension As IExtension
            pExtension = pExtensionManager.FindExtension("Zoom Factor Extension")

            'Get the state of the extension
            Dim pExtensionConfig As IExtensionConfig
            pExtensionConfig = pExtension
            If (Not pExtensionConfig Is Nothing) Then
                If (pExtensionConfig.State = esriExtensionState.esriESEnabled) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    Public Overrides Sub OnClick()

        'Get the extension manager
        Dim pExtensionManager As IExtensionManager
        pExtensionManager = New ExtensionManagerClass
        'Get the extension from the extension manager
        Dim pExtension As IExtension
        pExtension = pExtensionManager.FindExtension("Zoom Factor Extension")

        'Get the zoom extension interface
        Dim pZoomExtension As IZoomExtension
        pZoomExtension = pExtension

        'Get a zoom factor from the user
        Dim zoomString As String
        zoomString = InputBox("Enter Zoom Factor", "ZoomExtension Sample", CStr(pZoomExtension.ZoomFactor))

        'Set the zoom factor
        If IsNumeric(zoomString) Then pZoomExtension.ZoomFactor = CDbl(zoomString)

    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        'Not implemented
    End Sub
End Class