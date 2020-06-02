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
Imports ESRI.ArcGIS.DataSourcesFile

Public Class DirectionsForm
	Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()

	End Sub

#End Region

#Region "Public methods"

	' Clears Directions text
	Public Sub Init()
		m_txtDirections.Text = ""
	End Sub

	' Fills text box
	Public Sub Init(ByVal objDirections As ISMDirections)
		Dim nCount As Integer = objDirections.Count

		Dim strText As String

		' Totals
		strText = objDirections.TotalsText + vbCrLf + vbCrLf

		' Add text for each Direction
		For i As Integer = 0 To nCount - 1
			Dim objItem As SMDirItem
			objItem = objDirections.Item(i)

			' Direction text
			strText = strText + objItem.Text + vbCrLf

			' Drive text (length, time)
			If objItem.DriveText.Length > 0 Then _
				strText = strText + "	" + objItem.DriveText + vbCrLf

			strText = strText + vbCrLf
		Next

		' Set control text
		m_txtDirections.Text = strText

		' deselect if was be selected
		m_txtDirections.Select(0, 0)
	End Sub

#End Region

End Class

