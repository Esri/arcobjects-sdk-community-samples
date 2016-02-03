Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto

''' <summary>
''' Summary description for OpenNewMapDocument.
''' </summary>
<Guid("5bf50443-f852-47cd-9c96-984184b6cca6"), ClassInterface(ClassInterfaceType.None), ProgId("MapAndPageLayoutSynchApp.OpenNewMapDocument")> _
Public NotInheritable Class OpenNewMapDocument
  Inherits BaseCommand
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

  Private m_hookHelper As IHookHelper
  Private m_controlsSynchronizer As ControlsSynchronizer = Nothing
  Private m_sDocumentPath As String = String.Empty

  Public Sub New(ByVal controlsSynchronizer As ControlsSynchronizer)
    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Open Map Document"
    MyBase.m_message = "Open Map Document"
    MyBase.m_toolTip = "Open Map Document"
    MyBase.m_name = "DotNetSamplesOpenMapDocument"

    m_controlsSynchronizer = controlsSynchronizer

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
        'launch a new OpenFile dialog
        Dim dlg As OpenFileDialog = New OpenFileDialog()
        dlg.Filter = "Map Documents (*.mxd)|*.mxd"
        dlg.Multiselect = False
        dlg.Title = "Open Map Document"
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim docName As String = dlg.FileName

            Dim mapDoc As IMapDocument = New MapDocumentClass()
            If mapDoc.IsPresent(docName) AndAlso (Not mapDoc.IsPasswordProtected(docName)) Then
                mapDoc.Open(docName, String.Empty)

                ' set the first map as the active view
                Dim map As IMap = mapDoc.Map(0)
                mapDoc.SetActiveView(CType(map, IActiveView))

                m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout
                m_controlsSynchronizer.ReplaceMap(map)

                mapDoc.Close()

                m_sDocumentPath = docName
            End If
        End If
    End Sub

#End Region

  Public ReadOnly Property DocumentFileName() As String
    Get
      Return m_sDocumentPath
    End Get
  End Property
End Class
