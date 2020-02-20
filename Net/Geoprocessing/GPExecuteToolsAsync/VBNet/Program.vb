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
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

NotInheritable Class Program
	Private Sub New()
	End Sub

	''' <summary>
	''' The main entry point for the application.
	''' </summary>
	<STAThread> _
	Friend Shared Sub Main()
		If Not RuntimeManager.Bind(ProductCode.Engine) Then
			If Not RuntimeManager.Bind(ProductCode.Desktop) Then
				MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
				Return
			End If
		End If

		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New RunGPForm())
	End Sub
End Class
