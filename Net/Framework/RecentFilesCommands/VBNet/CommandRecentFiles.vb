Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms

<ComClass(CommandRecentFiles.ClassId, CommandRecentFiles.InterfaceId, CommandRecentFiles.EventsId), _
 ProgId("RecentFilesCommandsVB.CommandRecentFiles")> _
Public NotInheritable Class CommandRecentFiles
    Inherits BaseCommand
#Region "COM Registration Function(s)"
  <ComRegisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub RegisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryRegistration(registerType)
  End Sub

  <ComUnregisterFunction(), ComVisibleAttribute(False)> _
  Public Shared Sub UnregisterFunction(ByVal registerType As Type)
    ' Required for ArcGIS Component Category Registrar support
    ArcGISCategoryUnregistration(registerType)
  End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GMxCommands.Register(regKey)
        MxCommands.Register(regKey)
        SxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        GMxCommands.Unregister(regKey)
        MxCommands.Unregister(regKey)
        SxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "4d523e63-0133-4058-b6bb-771daab01ed4"
    Public Const InterfaceId As String = "6a1dee8e-84c4-4c6d-baeb-c734d630fde2"
    Public Const EventsId As String = "7bc1c97f-82bb-4836-9477-97a28ccbb5fd"
#End Region

  Private m_application As IApplication

  ' A creatable COM class must have a Public Sub New() 
  ' with no parameters, otherwise, the class will not be 
  ' registered in the COM registry and cannot be created 
  ' via CreateObject.
  Public Sub New()
    MyBase.New()

    MyBase.m_category = ".NET Samples"
    MyBase.m_caption = "Open Recent Files Dialog (VB)"
    MyBase.m_message = "Select to open document from the recent files list"
    MyBase.m_toolTip = "Recently opened files"
    MyBase.m_name = "VBNETSamples_RecentFiles"

    Try
      MyBase.m_bitmap = New Bitmap(Me.GetType(), "OpenDocument.png")
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
    End Try


  End Sub


  Public Overrides Sub OnCreate(ByVal hook As Object)
    m_application = CType(hook, IApplication)
  End Sub

  Public Overrides Sub OnClick()
    Dim recentFilePaths As String() = RecentFilesRegistryHelper.GetRecentFiles(m_application)

    If recentFilePaths.Length > 0 Then
      'Populate the form with the files
      Dim recentFileForm As FormRecentFiles = New FormRecentFiles()
      recentFileForm.PopulateFileList(recentFilePaths)

      'Set up parent window for modal dialog using Application's hWnd
      Dim parentWindow As New NativeWindow
      parentWindow.AssignHandle(New IntPtr(m_application.hWnd))

      'Show dialog and open file if necessary
      If recentFileForm.ShowDialog(parentWindow) = DialogResult.OK Then
        Dim path As String = recentFileForm.lstFiles.SelectedItem.ToString()
        If System.IO.File.Exists(path) Then
          m_application.OpenDocument(path)
        Else
          MsgBox(String.Format("'{0}' cannot be found", path), , "File doesn't exist")
        End If
      End If
    End If
  End Sub
End Class



