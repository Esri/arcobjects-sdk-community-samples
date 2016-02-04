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
Imports System.Runtime.InteropServices


  <Guid("AFC53D59-FB35-4531-9B91-DFB36512A784"), ComVisible(True), ProgId("RSSLayerProps2"), ClassInterface(ClassInterfaceType.None)> _
  Public Partial Class RSSLayerProps2 : Inherits PropertyPage
	Private m_symbolSize As Integer

	Public Sub New()
	  InitializeComponent()
	End Sub

	Protected Overrides Sub OnPageApply()
	  MyBase.OnPageApply()

	  Dim propSheet As PropertySheet = TryCast(Objects(0), PropertySheet)

	  Dim layer As RSSWeatherLayerClass = propSheet.RSSWatherLayer
			If Nothing Is layer Then
			Return
	  End If

	  Dim symbolSize As Integer
	  Integer.TryParse(txtSymbolSize.Text, symbolSize)
	  If 0 <> symbolSize AndAlso m_symbolSize <> symbolSize Then
			layer.SymbolSize = symbolSize
	  End If

	End Sub

	Protected Overrides Sub OnPageActivate(ByVal hWndParent As IntPtr, ByVal Rect As Rectangle, ByVal bModal As Boolean)
	  MyBase.OnPageActivate(hWndParent, Rect, bModal)

	  Dim propSheet As PropertySheet = TryCast(Objects(0), PropertySheet)

	  Dim layer As RSSWeatherLayerClass = propSheet.RSSWatherLayer
			If Nothing Is layer Then
			Return
	  End If

	  txtSymbolSize.Text = layer.SymbolSize.ToString()

	  m_symbolSize = layer.SymbolSize
	End Sub

	Private Sub txtSymbolSize_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSymbolSize.TextChanged
	  If (Not IsPageActivating) Then
			PageIsDirty = True
	  End If
	End Sub
  End Class