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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework

Namespace SelectionSample
  Public Class ToggleDockWinBtn
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

	Public Sub New()

	End Sub

	Protected Overrides Sub OnClick()
      Dim dockWindow As ESRI.ArcGIS.Framework.IDockableWindow
      dockWindow = SelectionExtension.GetSelectionCountWindow()

	  If dockWindow Is Nothing Then
		Return
	  End If

	  dockWindow.Show((Not dockWindow.IsVisible()))
	End Sub

	Protected Overrides Sub OnUpdate()
	  Me.Enabled = SelectionExtension.IsExtensionEnabled()
	End Sub
  End Class

End Namespace
