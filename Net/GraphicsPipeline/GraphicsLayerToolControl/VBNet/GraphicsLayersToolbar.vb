Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses

  ''' <summary>
  ''' Implements a toolbar that hosts the graphics ToolControl as well as the NewGraphicsLayer command
  ''' </summary>
<Guid("254d7ad1-eb3d-42ae-991e-638580792efd"), ClassInterface(ClassInterfaceType.None), ProgId("GraphicsLayerToolControl.GraphicsLayersToolbar")> _
Public NotInheritable Class GraphicsLayersToolbar : Inherits BaseToolbar
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
    ControlsToolbars.Register(regKey)
    MxCommandBars.Register(regKey)

  End Sub
  ''' <summary>
  ''' Required method for ArcGIS Component Category unregistration -
  ''' Do not modify the contents of this method with the code editor.
  ''' </summary>
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    ControlsToolbars.Unregister(regKey)
    MxCommandBars.Unregister(regKey)
  End Sub

#End Region
#End Region

#Region "class constructor"
  Public Sub New()
    AddItem("GraphicsLayerToolControl.GraphicsLayersListToolCtrl")
    AddItem("GraphicsLayerToolControl.NewGraphicsLayerCmd")
  End Sub
#End Region

  ''' <summary>
  ''' the caption of the toolbar
  ''' </summary>
  Public Overrides ReadOnly Property Caption() As String
    Get
      Return "Graphics Layers"
    End Get
  End Property

  'the internal name of the toolbar
  Public Overrides ReadOnly Property Name() As String
    Get
      Return "GraphicsLayersToolbar"
    End Get
  End Property
End Class