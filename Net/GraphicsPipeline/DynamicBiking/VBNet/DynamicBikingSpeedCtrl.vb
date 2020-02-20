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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms

	Public Partial Class DynamicBikingSpeedCtrl : Inherits UserControl
		Private m_dynamicBikingCmd As DynamicBikingCmd = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		Public Sub SetDynamicBikingCmd(ByVal dynamicBikingCmd As DynamicBikingCmd)
			m_dynamicBikingCmd = dynamicBikingCmd
		End Sub

		Private Sub trackBar1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trackBar1.ValueChanged
			If Not m_dynamicBikingCmd Is Nothing Then
				m_dynamicBikingCmd.PlaybackSpeed = trackBar1.Value
				toolTip1.ToolTipTitle = Convert.ToString(trackBar1.Value)
			End If
		End Sub
	End Class
