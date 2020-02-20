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

' This command brings up the property pages for the ArcGIS Network Analyst extension environment.
Namespace NAEngine
	<Guid("7E98FE97-DA7A-4069-BC85-091D75B1AF65"), ClassInterface(ClassInterfaceType.None), ProgId("NAEngine.NAProperties")> _
	Public NotInheritable Class cmdNAProperties : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
		Public Sub New()
			MyBase.m_caption = "Properties..."
		End Sub

		Public Overrides Sub OnClick()
            ' Show the Property Page form for ArcGIS Network Analyst extension
            Dim props As frmNAProperties = New frmNAProperties()
			props.ShowModal()
		End Sub

		Public Overrides Sub OnCreate(ByVal hook As Object)
			' Since this ToolbarMenu item is on the ToolbarControl the Hook is initialized by the ToolbarControl.
            Dim toolbarControl As ESRI.ArcGIS.Controls.IToolbarControl = TryCast(hook, ESRI.ArcGIS.Controls.IToolbarControl)
		End Sub
	End Class
End Namespace
