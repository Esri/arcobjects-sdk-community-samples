Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

  ''' <summary>
  ''' this command adds a new graphics layer to the map and makes it the active graphics layer
  ''' </summary>
<Guid("ba7007d0-545a-49a8-87a1-b42e5f2d2262"), ClassInterface(ClassInterfaceType.None), ProgId("GraphicsLayerToolControl.NewGraphicsLayerCmd")> _
Public NotInheritable Class NewGraphicsLayerCmd : Inherits BaseCommand
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisible(False)> _
  Private Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)

    '
    ' TODO: Add any COM registration code here
    '
  End Sub

  <ComUnregisterFunction(), ComVisible(False)> _
  Private Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)

    '
    ' TODO: Add any COM unregistration code here
    '
  End Sub

#Region "ArcGIS Component Category Registrar generated code"
  ''' <summary>
  ''' Required method for ArcGIS Component Category registration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Register(regKey)
    MxCommands.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsCommands.Unregister(regKey)
    MxCommands.Unregister(regKey)
  End Sub

#End Region
#End Region

#Region "class members"
  Private m_hookHelper As IHookHelper = Nothing
#End Region

#Region "class constructor"
  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "New Graphics Layer"
    MyBase.m_message = "Add new Graphics layer"
    MyBase.m_toolTip = "Add new Graphics layer"
    MyBase.m_name = "ToolControlSample_NewGraphicsLayerCmd"

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub
#End Region

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If hook Is Nothing Then
            Return
        End If

        If m_hookHelper Is Nothing Then
            m_hookHelper = New HookHelperClass()
        End If

        m_hookHelper.Hook = hook
    End Sub

    ''' <summary>
    ''' Occurs when this command is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        'get the map
        Dim map As IMap = m_hookHelper.FocusMap

        'count the graphics layers (will be used in order to name the new layer)
        Dim graphicsLayerCoount As Integer = 0
        Dim i As Integer = 0
        Do While i < map.LayerCount
            If TypeOf map.Layer(i) Is IGraphicsLayer Then
                graphicsLayerCoount += 1
            End If
            i += 1
        Loop

        'create a new graphics layer
        Dim graphicsLayer As IGraphicsLayer = New CompositeGraphicsLayerClass()
        'name the new layer
        CType(graphicsLayer, ILayer).Name = "Graphics Layer " & graphicsLayerCoount.ToString()
        'make the new graphics layer the active graphics layer
        map.ActiveGraphicsLayer = CType(graphicsLayer, ILayer)
        'add the new layer to the map
        map.AddLayer(CType(graphicsLayer, ILayer))
    End Sub

#End Region
End Class
