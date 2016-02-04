'Copyright 2016 Esri

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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI

  Public Partial Class CacheManagerDlg : Inherits Form : Implements IDisposable
	#Region "dialog class members"
	Public Structure LayerCacheInfo
	  Public name As String
	  Public folderName As String
	  Public folderPath As String
	  Public format As String
	  Public alwaysDrawCoarsestLevel As Boolean
	  Public strictOnDemand As Boolean
	  Public progressiveDrawingLevels As Integer
	  Public progressiveFetchingLevels As Integer
	  Public detailsThreshold As Double
	  Public maxCacheScale As Double

	  ' override ToString method in order to show only the layer name in the combo itself
	  Public Overrides Function ToString() As String
			Return name
	  End Function
	End Structure

	Private m_hookHelper As IHookHelper = Nothing
	Private m_layerCacheInfos As Dictionary(Of String, LayerCacheInfo) = New Dictionary(Of String, LayerCacheInfo)()
	#End Region

	#Region "dialog constructor"
	Public Sub New(ByVal hookHelper As IHookHelper)
	  m_hookHelper = hookHelper

	  InitializeComponent()
	End Sub
	#End Region

	#Region "private methods"

	Private Sub CacheManagerDlg_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
	  InitializeForm()
	End Sub

	Private Sub cboLayerNames_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboLayerNames.SelectedIndexChanged
	  ' get the layers from the map
	  If m_hookHelper.FocusMap.LayerCount = 0 Then
			Return
	  End If

	  Dim map As IMap = m_hookHelper.FocusMap

	  Dim dynamicCacheLayerManager As IDynamicCacheLayerManager = New DynamicCacheLayerManagerClass()

	  ' get all of the non-dynamic layers
	  Dim layer As ILayer
	  Dim i As Integer = 0
	  Do While i < map.LayerCount
			layer = map.Layer(i)
			If TypeOf layer Is IDynamicLayer Then
				Continue Do
			End If

			dynamicCacheLayerManager.Init(map, layer)
			Dim layerInfo As LayerCacheInfo = CType(cboLayerNames.SelectedItem, LayerCacheInfo)
			If dynamicCacheLayerManager.FolderName = layerInfo.folderName Then
				' populate the form
				lblCacheFolderName.Text = dynamicCacheLayerManager.FolderName
				lblCacheFolderPath.Text = dynamicCacheLayerManager.FolderPath
				If dynamicCacheLayerManager.Format.ToUpper().IndexOf("PNG") > -1 Then
					rdoPNG.Checked = True
				Else
					rdoJPEG.Checked = True
				End If

				chkAlwaysDrawCoarsestLevel.Checked = dynamicCacheLayerManager.AlwaysDrawCoarsestLevel
				numDetaildThreshold.Value = Convert.ToDecimal(dynamicCacheLayerManager.DetailsThreshold)
				chkStrictOnDemandMode.Checked = dynamicCacheLayerManager.StrictOnDemandMode
				numProgressiveDrawingLevels.Value = dynamicCacheLayerManager.ProgressiveDrawingLevels
				numProgressiveFetchingLevels.Value = dynamicCacheLayerManager.ProgressiveFetchingLevels
				numMaxCacheScale.Value = Convert.ToDecimal(dynamicCacheLayerManager.MaxCacheScale)
				Return
			End If
		  i += 1
	  Loop
	End Sub

	Private Sub btnFolderPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFolderPath.Click
	  Dim dlg As FolderBrowserDialog = New FolderBrowserDialog()
	  dlg.ShowNewFolderButton = True
	  dlg.Description = "Cache Folder"

	  If cboLayerNames.SelectedIndex <> -1 Then
			Dim layerInfo As LayerCacheInfo = CType(cboLayerNames.SelectedItem, LayerCacheInfo)
			dlg.SelectedPath = layerInfo.folderPath
	  End If

	  If System.Windows.Forms.DialogResult.OK = dlg.ShowDialog() Then
			If System.IO.Directory.Exists(dlg.SelectedPath) Then
				lblCacheFolderPath.Text = dlg.SelectedPath
			End If
	  End If
	End Sub

	Private Sub btnRestoreDefaults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestoreDefaults.Click
	  ' get the selected item
	  If cboLayerNames.SelectedIndex = -1 Then
			Return
	  End If

	  Dim layerInfo As LayerCacheInfo = CType(cboLayerNames.SelectedItem, LayerCacheInfo)
	  If m_layerCacheInfos.ContainsKey(layerInfo.folderName) Then
			layerInfo = m_layerCacheInfos(layerInfo.folderName)
			' populate the form
			lblCacheFolderName.Text = layerInfo.folderName
			lblCacheFolderPath.Text = layerInfo.folderPath
			If layerInfo.format.ToUpper().IndexOf("PNG") > -1 Then
				rdoPNG.Checked = True
			Else
				rdoJPEG.Checked = True
			End If

			chkAlwaysDrawCoarsestLevel.Checked = layerInfo.alwaysDrawCoarsestLevel
			chkStrictOnDemandMode.Checked = layerInfo.strictOnDemand
			numDetaildThreshold.Value = Convert.ToDecimal(layerInfo.detailsThreshold)
			numProgressiveDrawingLevels.Value = layerInfo.progressiveDrawingLevels
			numProgressiveFetchingLevels.Value = layerInfo.progressiveFetchingLevels
			numMaxCacheScale.Value = Convert.ToDecimal(layerInfo.maxCacheScale)
	  End If


	End Sub

	Private Sub btnDismiss_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDismiss.Click
	  Close()
	End Sub

	Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
	  UpdateLayer()
	End Sub

	Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
	  UpdateLayer()
	  Close()
	End Sub

	Private Sub GetCacheDefaultsProps()
	  m_layerCacheInfos.Clear()

	  If m_hookHelper.FocusMap.LayerCount = 0 Then
			Return
	  End If

	  Dim map As IMap = m_hookHelper.FocusMap

	  Dim dynamicCacheLayerManager As IDynamicCacheLayerManager = New DynamicCacheLayerManagerClass()

	  ' get all of the non-dynamic layers
	  Dim layer As ILayer
	  Dim i As Integer = 0
	  Do While i < map.LayerCount
			layer = map.Layer(i)
			If TypeOf layer Is IDynamicLayer Then
				Continue Do
			End If

			dynamicCacheLayerManager.Init(map, layer)
			Dim layerCacheInfo As LayerCacheInfo
			layerCacheInfo.name = layer.Name
			layerCacheInfo.folderName = dynamicCacheLayerManager.FolderName
			layerCacheInfo.folderPath = dynamicCacheLayerManager.FolderPath
			layerCacheInfo.format = dynamicCacheLayerManager.Format
			layerCacheInfo.detailsThreshold = dynamicCacheLayerManager.DetailsThreshold
			layerCacheInfo.alwaysDrawCoarsestLevel = dynamicCacheLayerManager.AlwaysDrawCoarsestLevel
			layerCacheInfo.progressiveDrawingLevels = dynamicCacheLayerManager.ProgressiveDrawingLevels
			layerCacheInfo.progressiveFetchingLevels = dynamicCacheLayerManager.ProgressiveFetchingLevels
			layerCacheInfo.strictOnDemand = dynamicCacheLayerManager.StrictOnDemandMode
			layerCacheInfo.maxCacheScale = dynamicCacheLayerManager.MaxCacheScale

			m_layerCacheInfos.Add(layerCacheInfo.folderName, layerCacheInfo)
		  i += 1
	  Loop
	End Sub

	Private Sub InitializeForm()
	  GetCacheDefaultsProps()

	  If m_layerCacheInfos.Count = 0 Then
			' clear the form
			cboLayerNames.Items.Clear()
			cboLayerNames.SelectedIndex = -1

			lblCacheFolderPath.Text = String.Empty
			btnFolderPath.Enabled = False
			numProgressiveDrawingLevels.Value = 0
			numProgressiveFetchingLevels.Value = 0
			numMaxCacheScale.Value = 0

			groupDrawingProps.Enabled = False
			btnOK.Enabled = False
			btnApply.Enabled = False

			Return
	  End If

	  groupDrawingProps.Enabled = True
	  btnFolderPath.Enabled = True
	  btnOK.Enabled = True
	  btnApply.Enabled = True

	  Dim selectedLayerName As String = String.Empty
	  Dim layerInfo As LayerCacheInfo
	  layerInfo.name = String.Empty
	  layerInfo.folderPath = String.Empty
	  layerInfo.folderName = String.Empty
	  layerInfo.format = String.Empty
	  layerInfo.alwaysDrawCoarsestLevel = False
	  layerInfo.detailsThreshold = 0
	  layerInfo.strictOnDemand = False
	  layerInfo.progressiveDrawingLevels = 0
	  layerInfo.progressiveFetchingLevels = 0
	  layerInfo.maxCacheScale = 0

	  Dim selectedIndex As Integer = cboLayerNames.SelectedIndex
	  If selectedIndex <> -1 Then
			selectedLayerName = (CType(cboLayerNames.SelectedItem, LayerCacheInfo)).folderName
			If m_layerCacheInfos.ContainsKey(selectedLayerName) Then
				layerInfo = m_layerCacheInfos(selectedLayerName)
			End If
	  End If

	  ' populate the combo
	  cboLayerNames.Items.Clear()
	  Dim index As Integer = 0
	  Dim bFirst As Boolean = True
	  For Each layerCacheInfoPair As KeyValuePair(Of String, LayerCacheInfo) In m_layerCacheInfos
			cboLayerNames.Items.Add(layerCacheInfoPair.Value)

			If bFirst AndAlso layerInfo.name = String.Empty Then
				layerInfo = layerCacheInfoPair.Value
				cboLayerNames.SelectedIndex = 0
				bFirst = False
			End If

			If selectedLayerName = layerCacheInfoPair.Key Then
				' make it the selected item
				cboLayerNames.SelectedIndex = index
			End If
			index += 1
	  Next layerCacheInfoPair

	  ' populate the form
	  lblCacheFolderName.Text = layerInfo.folderName
	  lblCacheFolderPath.Text = layerInfo.folderPath
	  If layerInfo.format.ToUpper().IndexOf("PNG") > -1 Then
			rdoPNG.Checked = True
	  Else
			rdoJPEG.Checked = True
	  End If

	  chkAlwaysDrawCoarsestLevel.Checked = layerInfo.alwaysDrawCoarsestLevel
	  chkStrictOnDemandMode.Checked = layerInfo.strictOnDemand
	  numDetaildThreshold.Value = Convert.ToDecimal(layerInfo.detailsThreshold)
	  numProgressiveDrawingLevels.Value = layerInfo.progressiveDrawingLevels
	  numProgressiveFetchingLevels.Value = layerInfo.progressiveFetchingLevels
	  numMaxCacheScale.Value = Convert.ToDecimal(layerInfo.maxCacheScale)
	End Sub

	Private Sub UpdateLayer()
	  ' get the selected layer from the map
	  If m_hookHelper.FocusMap.LayerCount = 0 Then
			Return
	  End If

	  If cboLayerNames.SelectedIndex = -1 Then
			Return
	  End If

	  Dim map As IMap = m_hookHelper.FocusMap

	  Dim dynamicCacheLayerManager As IDynamicCacheLayerManager = New DynamicCacheLayerManagerClass()

	  ' get all of the non-dynamic layers
	  Dim layer As ILayer
	  Dim i As Integer = 0
	  Do While i < map.LayerCount
			layer = map.Layer(i)
		If TypeOf layer Is IDynamicLayer Then
		  Continue Do
		End If

		dynamicCacheLayerManager.Init(map, layer)
		Dim layerInfo As LayerCacheInfo = CType(cboLayerNames.SelectedItem, LayerCacheInfo)
		If dynamicCacheLayerManager.FolderName = layerInfo.folderName Then
		  dynamicCacheLayerManager.FolderPath = lblCacheFolderPath.Text
		  If rdoPNG.Checked Then
			  dynamicCacheLayerManager.Format = ("PNG32")
		  Else
			  dynamicCacheLayerManager.Format = ("JPEG32")
		  End If
		  dynamicCacheLayerManager.StrictOnDemandMode = chkStrictOnDemandMode.Checked
		  dynamicCacheLayerManager.AlwaysDrawCoarsestLevel = chkAlwaysDrawCoarsestLevel.Checked
		  dynamicCacheLayerManager.DetailsThreshold = Convert.ToDouble(numDetaildThreshold.Value)
		  dynamicCacheLayerManager.ProgressiveDrawingLevels = Convert.ToInt32(numProgressiveDrawingLevels.Value)
		  dynamicCacheLayerManager.ProgressiveFetchingLevels = Convert.ToInt32(numProgressiveFetchingLevels.Value)
		  dynamicCacheLayerManager.MaxCacheScale = Convert.ToDouble(numMaxCacheScale.Value)
		End If
		  i += 1
	  Loop
	End Sub

	#End Region
  End Class
