Imports ESRI.ArcGIS.Analyst3D
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(TargetCenter.ClassId, TargetCenter.InterfaceId, TargetCenter.EventsId)> _
Public NotInheritable Class TargetCenter
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "5A168A30-31A4-4971-9595-59B648BD2783"
    Public Const InterfaceId As String = "D6F30DFD-6010-4526-927A-D62FADE076D0"
    Public Const EventsId As String = "89FE92A8-76B0-4FF1-925E-3E535982329B"
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
        ControlsCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_pCursor As System.Windows.Forms.Cursor
    Private m_pSceneHookHelper As ISceneHookHelper

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_SceneControl(VB.NET)"
        MyBase.m_caption = "Target Center"
        MyBase.m_toolTip = "Center on Target"
        MyBase.m_name = "Sample_SceneControl(VB.NET)/TargetCenter"
        MyBase.m_message = "Center view at selected target"

        'Load resources
        Dim res() As String = GetType(TargetCenter).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(TargetCenter).Assembly.GetManifestResourceStream("SceneToolsVB.TargetCenter.bmp"))
        End If
        m_pCursor = New System.Windows.Forms.Cursor(GetType(TargetCenter).Assembly.GetManifestResourceStream("SceneToolsVB.targetcenter.cur"))
        m_pSceneHookHelper = New SceneHookHelperClass
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pSceneHookHelper.Hook = hook
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If (m_pSceneHookHelper.Scene Is Nothing) Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Cursor() As Integer
        Get
            Return m_pCursor.Handle.ToInt32()
        End Get
    End Property

    Public Overrides Function Deactivate() As Boolean
        Return True
    End Function

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        'Get the scene graph
        Dim pSceneGraph As ISceneGraph = CType(m_pSceneHookHelper.SceneGraph, ISceneGraph)

        Dim pNewTgt As IPoint = Nothing
        Dim pOwner As Object = Nothing, pObject As Object = Nothing

        'Translate screen coordinates into a 3D point
        pSceneGraph.Locate(pSceneGraph.ActiveViewer, X, Y, esriScenePickMode.esriScenePickAll, True, pNewTgt, pOwner, pObject)

        If (pNewTgt Is Nothing) Then
            Return
        End If

        'Get the scene viewer's camera
        Dim pCamera As ICamera = CType(m_pSceneHookHelper.Camera, ICamera)

        'If orthographic (2D) view
        If (pCamera.ProjectionType = esri3DProjectionType.esriOrthoProjection) Then
            'Set the camera's new target
            pCamera.Target = pNewTgt

            'Redraw the scene viewer
            pSceneGraph.ActiveViewer.Redraw(True)
        Else
            'Get the camera's old target and observer
            Dim pOldTgt, pOldObs As IPoint
            pOldTgt = pCamera.Target
            pOldObs = pCamera.Observer

            'Set the camera's new target and get the new observer
            pCamera.Target = pNewTgt
            pCamera.PolarUpdate(1, 0, 0, True)

            Dim pNewObs As IPoint = CType(pCamera.Observer, IPoint)

            'Get the duration in seconds of last redraw
            'and the average number of frames per second
            Dim dlastFrameDuration, dMeanFrameRate As Double
            pSceneGraph.GetDrawingTimeInfo(dlastFrameDuration, dMeanFrameRate)

            If (dlastFrameDuration < 0.01) Then
                dlastFrameDuration = 0.01
            End If

            Dim iSteps As Integer
            iSteps = 2 / dlastFrameDuration

            If (iSteps < 1) Then
                iSteps = 1
            End If

            If (iSteps > 60) Then
                iSteps = 60
            End If

            Dim dxObs, dyObs, dzObs As Double
            Dim dxTgt, dyTgt, dzTgt As Double

            dxObs = (pNewObs.X - pOldObs.X) / iSteps
            dyObs = (pNewObs.Y - pOldObs.Y) / iSteps
            dzObs = (pNewObs.Z - pOldObs.Z) / iSteps

            dxTgt = (pNewTgt.X - pOldTgt.X) / iSteps
            dyTgt = (pNewTgt.Y - pOldTgt.Y) / iSteps
            dzTgt = (pNewTgt.Z - pOldTgt.Z) / iSteps

            'Loop through each step moving the camera's observer and target from the
            'old positions to the new positions, refreshing the scene viewer each time
            Dim i As Integer

            For i = 0 To iSteps
                pNewObs.X = pOldObs.X + (i * dxObs)
                pNewObs.Y = pOldObs.Y + (i * dyObs)
                pNewObs.Z = pOldObs.Z + (i * dzObs)

                pNewTgt.X = pOldTgt.X + (i * dxTgt)
                pNewTgt.Y = pOldTgt.Y + (i * dyTgt)
                pNewTgt.Z = pOldTgt.Z + (i * dzTgt)

                pCamera.Observer = pNewObs
                pCamera.Target = pNewTgt
                pSceneGraph.ActiveViewer.Redraw(True)
            Next i

        End If

    End Sub
End Class


