'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase

Partial Public Class OpenSimplePointDlg
  Inherits Form
#Region "class members"
    Private m_hookHelper As IHookHelper = Nothing
    Private m_workspace As IWorkspace = Nothing
#End Region

#Region "class constructor"
  Public Sub New(ByVal hookHelper As IHookHelper)
    If Nothing Is hookHelper Then
      Throw New Exception("Hook helper is not initialize")
    End If

    InitializeComponent()

    m_hookHelper = hookHelper
  End Sub
#End Region

#Region "UI event handlers"
  Private Sub btnOpenDataSource_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenDataSource.Click
    m_workspace = OpenPlugInWorkspace()

    ListFeatureClasses()
  End Sub

  Private Sub lstDeatureClasses_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstDeatureClasses.DoubleClick
    Me.Hide()
    OpenDataset()
    Me.Close()
  End Sub

  Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
    Me.Hide()
    OpenDataset()
    Me.Close()
  End Sub

  Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
    Me.Close()
  End Sub
#End Region

#Region "private methods"
  Private Function GetFileName() As String
    Dim dlg As OpenFileDialog = New OpenFileDialog()
    dlg.Filter = "Simple Point (*.csp)|*.csp"
    dlg.Title = "Open Simple Point file"
    dlg.RestoreDirectory = True
    dlg.CheckPathExists = True
    dlg.CheckFileExists = True
    dlg.Multiselect = False

    Dim dr As DialogResult = dlg.ShowDialog()
    If System.Windows.Forms.DialogResult.OK = dr Then
      Return dlg.FileName
    End If

    Return String.Empty
  End Function

  Private Function OpenPlugInWorkspace() As IWorkspace
    Try
      Dim path As String = GetFileName()
      If String.Empty = path Then
        Return Nothing
      End If

      'update the path textbox
      txtPath.Text = path

      'get the type using the ProgID
      Dim t As Type = Type.GetTypeFromProgID("esriGeoDatabase.SimplePointPluginWorkspaceFactory")
      'Use activator in order to create an instance of the workspace factory
      Dim workspaceFactory As IWorkspaceFactory = TryCast(Activator.CreateInstance(t), IWorkspaceFactory)

      'open the workspace
      Return workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(path), 0)
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
      Return Nothing
    End Try
  End Function

  Private Sub ListFeatureClasses()
    lstDeatureClasses.Items.Clear()

    If Nothing Is m_workspace Then
      Return
    End If

    Dim datasetNames As IEnumDatasetName = m_workspace.DatasetNames(esriDatasetType.esriDTAny)
    datasetNames.Reset()
    Dim dsName As IDatasetName

    dsName = datasetNames.Next()
    Do While Not dsName Is Nothing
      lstDeatureClasses.Items.Add(dsName.Name)
      dsName = datasetNames.Next()
    Loop

        'select the first dataset on the list
    If lstDeatureClasses.Items.Count > 0 Then
      lstDeatureClasses.SelectedIndex = 0
      lstDeatureClasses.Refresh()
    End If
  End Sub

  Private Sub OpenDataset()
    Try
      If Nothing Is m_hookHelper OrElse Nothing Is m_workspace Then
        Return
      End If

      If String.Empty Is CStr(lstDeatureClasses.SelectedItem) Then
        Return
      End If

      'get the selected item from the listbox
      Dim dataset As String = CStr(lstDeatureClasses.SelectedItem)

      'cast the workspace into a feature workspace
      Dim featureWorkspace As IFeatureWorkspace = TryCast(m_workspace, IFeatureWorkspace)
      If Nothing Is featureWorkspace Then
        Return
      End If

      'get a featureclass from the workspace
      Dim featureClass As IFeatureClass = featureWorkspace.OpenFeatureClass(dataset)

      'create a new feature layer and add it to the map
      Dim featureLayer As IFeatureLayer = New FeatureLayerClass()
      featureLayer.Name = featureClass.AliasName
      featureLayer.FeatureClass = featureClass
      m_hookHelper.FocusMap.AddLayer(CType(featureLayer, ILayer))
    Catch ex As Exception
      System.Diagnostics.Trace.WriteLine(ex.Message)
    End Try
  End Sub
#End Region
End Class