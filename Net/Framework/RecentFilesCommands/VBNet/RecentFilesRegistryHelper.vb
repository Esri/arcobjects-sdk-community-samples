Imports System.Collections.Generic
Imports Microsoft.Win32

Module RecentFilesRegistryHelper
  Const RecentFileRegistryKeyPath As String = "Software\ESRI\Desktop{0}\{1}\Recent File List"

  ''' <summary>
  ''' Helper function to process recent file lists stored in the registry
  ''' </summary>
  Public Function GetRecentFiles(ByVal app As ESRI.ArcGIS.Framework.IApplication) As String()
    Dim recentFilePaths As List(Of String) = New List(Of String)

    'Read the registry to get the recent file list
    Dim version As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Version
    Dim openKey As String = String.Format(RecentFileRegistryKeyPath, version, app.Name)
    Dim recentListKey As RegistryKey = Registry.CurrentUser.OpenSubKey(openKey)
    If Not recentListKey Is Nothing Then
      Dim listNames As String() = recentListKey.GetValueNames()
      For Each name As String In listNames
        Dim fileName As String = recentListKey.GetValue(name, String.Empty).ToString()
        If Not String.IsNullOrEmpty(fileName) Then
          recentFilePaths.Add(fileName)
        End If
      Next
    End If

    Return recentFilePaths.ToArray()
  End Function

End Module
