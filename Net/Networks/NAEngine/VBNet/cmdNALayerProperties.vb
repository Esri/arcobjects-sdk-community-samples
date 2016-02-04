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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.NetworkAnalyst

' This command brings up the property pages for the NALayer.
Namespace NAEngine
	<Guid("04B67C95-96DD-4afe-AF62-942255ACBA71"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.NALayerProperties")> _
	Public NotInheritable Class cmdNALayerProperties : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
		Private m_mapControl As ESRI.ArcGIS.Controls.IMapControl3

		Public Sub New()
			MyBase.m_caption = "Properties..."
		End Sub

		Public Overrides Sub OnClick()
			If m_mapControl Is Nothing Then
				MessageBox.Show("Error: Map control is null for this command")
				Return
			End If

			' Get the NALayer that was right-clicked on in the table of contents
			' m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
            Dim naLayer As INALayer3 = TryCast(m_mapControl.CustomProperty, INALayer3)
			If naLayer Is Nothing Then
				MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control")
				Return
			End If

			' Show the Property Page form for the NALayer
            Dim props As frmNALayerProperties = New frmNALayerProperties()
			If props.ShowModal(naLayer) Then
				' Notify the ActiveView that the contents have changed so the TOC and NAWindow know to refresh themselves.
				m_mapControl.ActiveView.ContentsChanged()
			End If
		End Sub

		Public Overrides Sub OnCreate(ByVal hook As Object)
			' The hook for OnCreate is set as a MapControl in frmMain_Load
			m_mapControl = TryCast(hook, ESRI.ArcGIS.Controls.IMapControl3)
		End Sub
	End Class
End Namespace
