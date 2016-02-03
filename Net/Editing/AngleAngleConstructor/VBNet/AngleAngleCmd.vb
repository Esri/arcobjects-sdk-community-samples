Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.esriSystem

<ComClass(AngleAngleCmd.ClassId, AngleAngleCmd.InterfaceId, AngleAngleCmd.EventsId), _
 ProgId("AngleAngleVB.AngleAngleCmd")> _
Public NotInheritable Class AngleAngleCmd
  Inherits BaseCommand

#Region "COM GUIDs"
  ' These  GUIDs provide the COM identity for this class 
  ' and its COM interfaces. If you change them, existing 
  ' clients will no longer be able to access the class.
  Public Const ClassId As String = "c68043b2-8808-436f-8205-b439877fde82"
  Public Const InterfaceId As String = "aba3e56c-aff8-4c0b-9395-f768f8bfe967"
  Public Const EventsId As String = "ea3e8ad6-065c-4fbe-8014-eac72267c227"
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
  Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Register(regKey)

  End Sub
  Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
    Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
    MxCommands.Unregister(regKey)

  End Sub

#End Region
#End Region


  Private m_application As IApplication
  Private m_editor As IEditor3
  Private m_edSketch As IEditSketch3

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()
    MyBase.m_category = "Developer Samples"  'localizable text 
    MyBase.m_caption = "Angle-Angle Segment Constructor (VB)"   'localizable text 
    MyBase.m_message = "Adds a point to the edit sketch based on intersection."   'localizable text 
    MyBase.m_toolTip = "Angle-Angle Shape Constructor" 'localizable text 
    MyBase.m_name = "ShapeConstructor_AngleAngle"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

    Try
      Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
      MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try

  End Sub


  Public Overrides Sub OnCreate(ByVal hook As Object)
    If Not hook Is Nothing Then
      m_application = CType(hook, IApplication)

      'Disable if it is not ArcMap
      If TypeOf hook Is IMxApplication Then
        MyBase.m_enabled = True
      Else
        MyBase.m_enabled = False
      End If
    End If

    'get the editor
    Dim editorUid As UID = New UID
    editorUid.Value = "esriEditor.Editor"
    m_editor = m_application.FindExtensionByCLSID(editorUid)
  End Sub

  Public Overrides Sub OnClick()
    'Create the constructor, pass the editor and set as current constructor
    m_edSketch = m_editor
    Dim aac As AngleAngleCstr = New AngleAngleCstr()
    aac.Initialize(m_editor)
    m_edSketch.ShapeConstructor = aac
  End Sub

  Public Overrides ReadOnly Property Enabled() As Boolean
    Get
      Return m_editor.EditState = esriEditState.esriStateEditing
    End Get
  End Property

  Public Overrides ReadOnly Property Checked() As Boolean
    Get
      'Check the command/button if we are the current constructor
      Dim ptemp As IPersist = m_edSketch.ShapeConstructor
      Dim pg As Guid
      ptemp.GetClassID(pg)
      Return (pg.ToString() = "cdcbb1bf-a87d-4927-8e75-9babe1979f90")
    End Get
  End Property
End Class



