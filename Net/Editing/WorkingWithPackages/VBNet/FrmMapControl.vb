Option Explicit On
Option Strict On


Imports Microsoft.Win32
Imports System.IO
Imports System.Net.Sockets

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto


Public Class FrmMapControl
  Private packageLocation As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ArcGIS\Packages"
  Private webMapLocation As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ArcGIS\Web Maps"
    Private myLayerPackageName As String = "Layer Package"
  Private myMapPackageName As String = "Events"
  Private myWebMapName As String = "USA Topo Maps"
  Private layerFiles() As FileInfo

  Private Enum PackageType
    LayerPackage
    MapPackage
  End Enum


  Private Sub btnLoadlpk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadlpk.Click
    Try
      Dim layerFile As ILayerFile = New LayerFileClass()
      ' Test to see if we can connect to ArcGIS.com
      If IsConnected() Then
        ' If so, open the Layer Package from ArcGIS.com, this will get the most recent data.
        ' for the package.  If there is no change, the data will not get re-downloaded, 
        ' just use what is stored on disk.
        layerFile.Open(txtLayerPackage.Text)
      Else
        ' If we cannot connect to ArcGIS.com use what was previously downloaded.
        If DoesPackageExist(PackageType.LayerPackage) Then
          For Each layerpackage As FileInfo In layerFiles
            ' Layer packages can have multiple layers included in them.  However,
            ' the LayerFile and MapDocument classes will only get the first one.
            ' Here the sample is using an array so we can get all the layer files
            ' and not worry about the name.
            layerFile.Open(layerpackage.FullName)
          Next
        End If
      End If
      Dim layer As ILayer = layerFile.Layer
      axMapControl1.AddLayer(layer)
    Catch ex As Exception
      MessageBox.Show("Failed to open Layer Package!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Sub btnLoadmpk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadmpk.Click
    Try
      Dim mapDocument As IMapDocument = New MapDocument()
      If IsConnected() Then
        mapDocument.Open(txtMapPackage.Text, "")
      Else
        If DoesPackageExist(PackageType.MapPackage) Then
          mapDocument.Open(packageLocation, "")
        End If
      End If
      axMapControl1.Map = mapDocument.Map(0)
    Catch ex As Exception
      MessageBox.Show("Failed to open Map Package!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Sub btnWebMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWebMap.Click
    If Not IsConnected() Then
      ' Since Web Maps require and internet connection anyways
      ' just fail if there isn't one.
      MessageBox.Show("Cannot Connect to ArcGIS.com!", "Failed to open Web Map.", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return
    End If
    Try
      Dim mapDocument As IMapDocument = New MapDocument()
      Dim webMapMxd As String = webMapLocation + "\" + myWebMapName + "\" + myWebMapName + ".mxd"
      If File.Exists(webMapMxd) Then
        mapDocument.Open(webMapMxd, "")
      Else
        mapDocument.Open(txtWebMap.Text, "")
      End If
      axMapControl1.Map = mapDocument.Map(0)
    Catch ex As Exception
      MessageBox.Show("Failed to open Web Map!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Function DoesPackageExist(ByVal packageType As PackageType) As Boolean
    ' Packages and Web Maps are handled by the FileHandler in Common Files\ArcGIS\bin
    ' The default package location can be controled be the user.
    Dim regKey As RegistryKey = Registry.CurrentUser
    Dim subKey As RegistryKey = regKey.OpenSubKey("Software\ESRI\ArcGIS File Handler\Settings")
    Dim regOption As Integer = CType(subKey.GetValue("PackageLocationOption", 0), Integer)
    If regOption = 1 Then
      ' Change the package location if different otherwise use the default.
      Dim fileHandlerUserSetting As String = subKey.GetValue("PackageLocation", "").ToString()
      If Not fileHandlerUserSetting = "" Then
        packageLocation = fileHandlerUserSetting
      End If
    End If

    Select Case packageType
      Case FrmMapControl.PackageType.LayerPackage
        ' Layer Packages can have two different directories, depending on if the 
        ' package contains a single layer or multiple layers.
        Dim directoryInfo As DirectoryInfo
        Dim layerPackageLocation As String = packageLocation + "\" + myLayerPackageName.Replace(" ", "_")
        If Directory.Exists(layerPackageLocation) Then
          directoryInfo = New DirectoryInfo(layerPackageLocation)
          layerFiles = directoryInfo.GetFiles("*.lyr")
        Else
          layerPackageLocation = layerPackageLocation + ".lpk"
          If Directory.Exists(layerPackageLocation) Then
            directoryInfo = New DirectoryInfo(layerPackageLocation)
            layerFiles = directoryInfo.GetFiles("*.lyr")
          End If
        End If
        If (layerFiles.Length > 0) Then
          packageLocation = layerPackageLocation
          Return True
        End If
      Case FrmMapControl.PackageType.MapPackage
        packageLocation = packageLocation + "\" + myMapPackageName.Replace(" ", "_") + "\v10\" + myMapPackageName + ".mxd"
        Return File.Exists(packageLocation)
      Case Else
        Return False
    End Select
    Return False
  End Function

  Private Function IsConnected() As Boolean
    Try
      ' Test to see if we can connect to ArcGIS.com
      Dim tcpClient As TcpClient = New TcpClient("www.arcgis.com", 80)
      tcpClient.Close()
      Return True
    Catch ex As Exception
      Return False
    End Try
  End Function
End Class
