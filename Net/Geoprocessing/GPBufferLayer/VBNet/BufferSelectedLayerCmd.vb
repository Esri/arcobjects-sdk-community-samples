Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Text
'using System.Windows.Forms;
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls

  ''' <summary>
  ''' Summary description for BufferSelectedLayerCmd.
  ''' </summary>
<Guid("7dc0aa20-efe4-4714-9110-7f3c57bf00aa"), ClassInterface(ClassInterfaceType.None), ProgId("GpBufferLayer.BufferSelectedLayerCmd")> _
Public NotInheritable Class BufferSelectedLayerCmd : Inherits BaseCommand

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

  Private m_hookHelper As IHookHelper

  Public Sub New()
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Buffer selected layer"
    MyBase.m_message = "Buffer selected layer"
    MyBase.m_toolTip = "Buffer selected layer"
    MyBase.m_name = "GpBufferLayer_BufferSelectedLayerCmd"

    Try
      Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try
  End Sub

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
        If Nothing Is m_hookHelper Then
            Return
        End If

        If m_hookHelper.FocusMap.LayerCount > 0 Then
            Dim bufferDlg As BufferDlg = New BufferDlg(m_hookHelper)
            bufferDlg.Show()
        End If
    End Sub

#End Region
End Class
