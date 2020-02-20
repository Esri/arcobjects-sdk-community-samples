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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.NetworkAnalyst

' This command deletes all the network locations and analysis results from the selected NALayer.
Namespace NAEngine
	<Guid("773CCD44-C46A-42eb-A1B2-E00C7B765783"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.ClearAnalysisLayer")> _
	Public NotInheritable Class cmdClearAnalysisLayer : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
		Private m_mapControl As IMapControl3

		Public Sub New()
			MyBase.m_caption = "Clear Analysis Layer"
		End Sub

		Public Overrides Sub OnClick()

			If m_mapControl Is Nothing Then
				MessageBox.Show("Error: Map control is null for this command")
				Return
			End If

			' Get the NALayer and corresponding NAContext of the layer that
			' was right-clicked on in the table of contents
			' m_MapControl.CustomProperty was set in frmMain.axTOCControl1_OnMouseDown
			Dim naLayer As INALayer = TryCast(m_mapControl.CustomProperty, INALayer)
			If naLayer Is Nothing Then
				MessageBox.Show("Error: NALayer was not set as the CustomProperty of the map control")
				Return
			End If

            Dim naEnv As IEngineNetworkAnalystEnvironment = CommonFunctions.GetTheEngineNetworkAnalystEnvironment()
			If naEnv Is Nothing OrElse naEnv.NAWindow Is Nothing Then
				MessageBox.Show("Error: EngineNetworkAnalystEnvironment is not properly configured")
				Return
			End If

			' Set the active Analysis layer
			Dim naWindow As IEngineNAWindow = naEnv.NAWindow
			If Not naWindow.ActiveAnalysis Is naLayer Then
				naWindow.ActiveAnalysis = naLayer
			End If

			' Remember what the current category is
			Dim originalCategory As IEngineNAWindowCategory = naWindow.ActiveCategory

			' Loop through deleting all the items from all the categories
			Dim naClasses As INamedSet = naLayer.Context.NAClasses
            Dim naHelper As IEngineNetworkAnalystHelper = TryCast(naEnv, IEngineNetworkAnalystHelper)
			Dim i As Integer = 0
			Do While i < naClasses.Count
                Dim category As IEngineNAWindowCategory = naWindow.CategoryByNAClassName(naClasses.Name(i))
				naWindow.ActiveCategory = category
				naHelper.DeleteAllNetworkLocations()
				i += 1
			Loop

			'Reset to the original category
			naWindow.ActiveCategory = originalCategory

			' Redraw the map
			m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography, naLayer, m_mapControl.Extent)
		End Sub

		Public Overrides Sub OnCreate(ByVal hook As Object)
			' The "hook" was set as a MapControl in formMain_Load
			m_mapControl = TryCast(hook, IMapControl3)
		End Sub
	End Class
End Namespace
