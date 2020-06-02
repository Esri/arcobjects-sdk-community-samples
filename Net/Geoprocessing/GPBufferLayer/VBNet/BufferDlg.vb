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
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.AnalysisTools

Partial Public Class BufferDlg : Inherits Form
  'in order to scroll the messages textbox to the bottom we must import this Win32 call
  <DllImport("user32.dll")> _
  Private Shared Function PostMessage(ByVal wnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
  End Function

  Private m_hookHelper As IHookHelper = Nothing
  Private Const WM_VSCROLL As UInteger = &H115
  Private Const SB_BOTTOM As UInteger = 7

  Public Sub New(ByVal hookHelper As IHookHelper)
    InitializeComponent()

    m_hookHelper = hookHelper
  End Sub

  Private Sub bufferDlg_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    If Nothing Is m_hookHelper OrElse Nothing Is m_hookHelper.Hook OrElse 0 = m_hookHelper.FocusMap.LayerCount Then
      Return
    End If

    'load all the feature layers in the map to the layers combo
    Dim layers As IEnumLayer = GetLayers()
    layers.Reset()
    Dim layer As ILayer = layers.Next()
    Do While Not layer Is Nothing
      cboLayers.Items.Add(layer.Name)
      layer = layers.Next()
    Loop
    'select the first layer
    If cboLayers.Items.Count > 0 Then
      cboLayers.SelectedIndex = 0
    End If

    Dim tempDir As String = System.IO.Path.GetTempPath()
    txtOutputPath.Text = System.IO.Path.Combine(tempDir, (CStr(cboLayers.SelectedItem) & "_buffer.shp"))

    'set the default units of the buffer
    Dim units As Integer = Convert.ToInt32(m_hookHelper.FocusMap.MapUnits)
    cboUnits.SelectedIndex = units
  End Sub

  Private Sub btnOutputLayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOutputLayer.Click
    'set the output layer
    Dim saveDlg As SaveFileDialog = New SaveFileDialog()
    saveDlg.CheckPathExists = True
    saveDlg.Filter = "Shapefile (*.shp)|*.shp"
    saveDlg.OverwritePrompt = True
    saveDlg.Title = "Output Layer"
    saveDlg.RestoreDirectory = True
    saveDlg.FileName = CStr(cboLayers.SelectedItem) & "_buffer.shp"

    Dim dr As DialogResult = saveDlg.ShowDialog()
    If dr = System.Windows.Forms.DialogResult.OK Then
      txtOutputPath.Text = saveDlg.FileName
    End If
  End Sub

  Private Sub btnBuffer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuffer.Click
    'make sure that all parameters are okay
    Dim bufferDistance As Double
    Double.TryParse(txtBufferDistance.Text, bufferDistance)
    If 0.0 = bufferDistance Then
      MessageBox.Show("Bad buffer distance!")
      Return
    End If

    If (Not System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtOutputPath.Text))) OrElse ".shp" <> System.IO.Path.GetExtension(txtOutputPath.Text) Then
      MessageBox.Show("Bad output filename!")
      Return
    End If

    If m_hookHelper.FocusMap.LayerCount = 0 Then
      Return
    End If

    'get the layer from the map
    Dim layer As IFeatureLayer = GetFeatureLayer(CStr(cboLayers.SelectedItem))
    If Nothing Is layer Then
      txtMessages.Text &= "Layer " & CStr(cboLayers.SelectedItem) & "cannot be found!" & Constants.vbCrLf
      Return
    End If

    'scroll the textbox to the bottom
    ScrollToBottom()
    'add message to the messages box
    txtMessages.Text &= "Buffering layer: " & layer.Name & Constants.vbCrLf

    txtMessages.Text += Constants.vbCrLf & "Get the geoprocessor. This might take a few seconds..." & Constants.vbCrLf
    txtMessages.Update()
    'get an instance of the geoprocessor
    Dim gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()
    gp.OverwriteOutput = True
    txtMessages.Text &= "Buffering..." & Constants.vbCrLf
    txtMessages.Update()

    'create a new instance of a buffer tool
    Dim buffer As ESRI.ArcGIS.AnalysisTools.Buffer = New ESRI.ArcGIS.AnalysisTools.Buffer(layer, txtOutputPath.Text, CStr(bufferDistance) & " " & CStr(cboUnits.SelectedItem))
    'execute the buffer tool (very easy :-))
    Dim results As IGeoProcessorResult = CType(gp.Execute(buffer, Nothing), IGeoProcessorResult)
    If results.Status <> esriJobStatus.esriJobSucceeded Then
      txtMessages.Text &= "Failed to buffer layer: " & layer.Name & Constants.vbCrLf
    End If
    txtMessages.Text += ReturnMessages(gp)
    'scroll the textbox to the bottom
    ScrollToBottom()

    txtMessages.Text += Constants.vbCrLf & "Done." & Constants.vbCrLf
    txtMessages.Text &= "-----------------------------------------------------------------------------------------" & Constants.vbCrLf
    'scroll the textbox to the bottom
    ScrollToBottom()

  End Sub

  Private Function ReturnMessages(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor) As String
    Dim sb As StringBuilder = New StringBuilder()
    If gp.MessageCount > 0 Then
      Dim Count As Integer = 0
      Do While Count <= gp.MessageCount - 1
        System.Diagnostics.Trace.WriteLine(gp.GetMessage(Count))
        sb.AppendFormat("{0}" & Constants.vbLf, gp.GetMessage(Count))
        Count += 1
      Loop
    End If
    Return sb.ToString()
  End Function

  Private Function GetFeatureLayer(ByVal layerName As String) As IFeatureLayer
    'get the layers from the maps
    Dim layers As IEnumLayer = GetLayers()
    layers.Reset()

    Dim layer As ILayer = layers.Next()
    Do While Not layer Is Nothing
      If layer.Name = layerName Then
        Return TryCast(layer, IFeatureLayer)
      End If
      layer = layers.Next()
    Loop

    Return Nothing
  End Function

  Private Function GetLayers() As IEnumLayer
    Dim uid As UID = New UIDClass()
    uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
    Dim layers As IEnumLayer = m_hookHelper.FocusMap.Layers(uid, True)

    Return layers
  End Function

  Private Sub ScrollToBottom()
    PostMessage(CType(txtMessages.Handle, IntPtr), WM_VSCROLL, CType(SB_BOTTOM, IntPtr), CType(IntPtr.Zero, IntPtr))
  End Sub
  Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
    Me.Close()
  End Sub
End Class