Option Explicit On 

Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(ZoomIn.ClassId, ZoomIn.InterfaceId, ZoomIn.EventsId)> _
Public NotInheritable Class ZoomIn
    Inherits BaseCommand
    Private m_Hookhelper As IHookHelper

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "dfb082ac-6cd5-4a62-800d-ec8f75443172"
    Public Const InterfaceId As String = "7331345f-eb11-436a-a614-2eaf7462a1f3"
    Public Const EventsId As String = "19ae4591-45a0-410a-b3e7-de38463a20e2"
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
        MyBase.m_caption = "Variable Zoom In"
        MyBase.m_message = "Variable Zoom In"
        MyBase.m_toolTip = "Variable Zoom In"
        MyBase.m_category = "ZoomExtension Sample(VB.NET)"
        MyBase.m_name = "ZoomExtension Sample(VB.NET)_Variable Zoom In"
        MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(ZoomIn).Assembly.GetManifestResourceStream(GetType(ZoomIn), "zoominfxd.bmp"))

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

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_Hookhelper.Hook = hook
    End Sub

    Public Overrides Sub OnClick()
        'Get the current extent of the active view
        Dim pActiveView As IActiveView
        pActiveView = m_Hookhelper.ActiveView
        Dim pEnvelope As IEnvelope
        pEnvelope = pActiveView.Extent

        'Get the extension manager
        Dim pExtensionManager As ExtensionManager
        pExtensionManager = New ExtensionManagerClass
        'Get the extension from the extension manager
        Dim pExtension As IExtension
        pExtension = pExtensionManager.FindExtension("Zoom Factor Extension")

        'Get the zoom factor from the extension
        Dim ZoomFactor As Double
        ZoomFactor = 1.1
        Dim pZoomExtension As IZoomExtension
        If (Not pExtension Is Nothing) Then
            pZoomExtension = pExtension
            ZoomFactor = pZoomExtension.ZoomFactor
        Else
            System.Windows.Forms.MessageBox.Show("The extension cannot be found!")
        End If

        'Update the current extent of the active view
        pEnvelope.Expand((Convert.ToDouble(1.0) / ZoomFactor), (Convert.ToDouble(1.0) / ZoomFactor), True)
        pActiveView.Extent = pEnvelope
        pActiveView.Refresh()
    End Sub
End Class
