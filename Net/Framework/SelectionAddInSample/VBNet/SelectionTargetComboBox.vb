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
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Desktop.AddIns

Imports My

Namespace SelectionSample
  Public Class SelectionTargetComboBox
	  Inherits ESRI.ArcGIS.Desktop.AddIns.ComboBox
	Private Shared s_comboBox As SelectionTargetComboBox
	Private m_selAllCookie As Integer

	Public Sub New()
	  m_selAllCookie = -1
	  s_comboBox = Me
	End Sub

	Friend Shared Function GetSelectionComboBox() As SelectionTargetComboBox
	  Return s_comboBox
	End Function

	Friend Sub AddItem(ByVal itemName As String, ByVal layer As IFeatureLayer)
	  If s_comboBox.items.Count = 0 Then
		m_selAllCookie = s_comboBox.Add("Select All")
		s_comboBox.Select(m_selAllCookie)
	  End If

	  ' Add each item to combo box.
	  Dim cookie As Integer = s_comboBox.Add(itemName, layer)
	End Sub

	Friend Sub ClearAll()
	  m_selAllCookie = -1
	  s_comboBox.Clear()
	End Sub

	Protected Overrides Sub OnUpdate()
	  Me.Enabled = SelectionExtension.IsExtensionEnabled()
	End Sub

	Protected Overrides Sub OnSelChange(ByVal cookie As Integer)
	  If cookie = -1 Then
		Return
	  End If

	  For Each item As ComboBox.Item In Me.items
		' All feature layers are selectable if "Select All" is selected;
		' otherwise, only the selected layer is selectable.
		Dim fl As IFeatureLayer = TryCast(item.Tag, IFeatureLayer)
		If fl Is Nothing Then
		  Continue For
		End If

		If cookie = item.Cookie Then
		  fl.Selectable = True
		  Continue For
		End If

		fl.Selectable = If((cookie = m_selAllCookie), True, False)
	  Next item

	  ' Fire ContentsChanged event to cause TOC to refresh with new selected layers.
	  ArcMap.Document.ActiveView.ContentsChanged()


	End Sub
  End Class

End Namespace
