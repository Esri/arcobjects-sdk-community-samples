Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports System.Windows.Forms

Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto


  ''' <summary>
  ''' Summary description for TriangleElementTool.
  ''' </summary>
<Guid("2d0c353b-2a0e-4fdf-90ae-fc8e1314a989"), ClassInterface(ClassInterfaceType.None), ProgId("TriangleElement.TriangleElementTool")> _
Public NotInheritable Class TriangleElementTool
  Inherits BaseTool
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
    MxCommands.Register(regKey)
    ControlsCommands.Register(regKey)
  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Unregister(regKey)
    ControlsCommands.Unregister(regKey)
  End Sub

#End Region
#End Region

  Private m_hookHelper As IHookHelper = Nothing

  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Triangle Element"
    MyBase.m_message = "Add Triangle Element"
    MyBase.m_toolTip = "Add Triangle Element"
    MyBase.m_name = "TriangleElement_TriangleElementTool"
    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
      MyBase.m_cursor = New Cursor(Me.GetType(), Me.GetType().Name & ".cur")
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this tool is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        Try
            m_hookHelper = New HookHelperClass()
            m_hookHelper.Hook = hook
            If m_hookHelper.ActiveView Is Nothing Then
                m_hookHelper = Nothing
            End If
        Catch
            m_hookHelper = Nothing
        End Try

        If m_hookHelper Is Nothing Then
            MyBase.m_enabled = False
        Else
            MyBase.m_enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Occurs when this tool is clicked
    ''' </summary>
    Public Overrides Sub OnClick()
        ' TODO: Add TriangleElementTool.OnClick implementation
    End Sub

    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        Dim c As IColor = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Black), IColor)

        Dim lineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
        lineSymbol.Color = c
        lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid
        lineSymbol.Width = 2.0

        c = CType(ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Gold), IColor)
        Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
        simpleFillSymbol.Color = c
        simpleFillSymbol.Outline = CType(lineSymbol, ILineSymbol)
        simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross

        Dim triangleElement As ITriangleElement = New TriangleElementClass()
        triangleElement.Angle = 40.0
        triangleElement.Size = 25
        triangleElement.FillSymbol = simpleFillSymbol

        Dim point As IPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)

        Dim element As IElement = CType(triangleElement, IElement)
        element.Geometry = CType(point, IGeometry)

        Dim graphicsContainer As IGraphicsContainer = CType(m_hookHelper.FocusMap, IGraphicsContainer)
        graphicsContainer.AddElement(element, 0)
        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub

    Public Overrides Sub OnMouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO:  Add TriangleElementTool.OnMouseMove implementation
    End Sub

    Public Overrides Sub OnMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        ' TODO:  Add TriangleElementTool.OnMouseUp implementation
    End Sub
#End Region
End Class
