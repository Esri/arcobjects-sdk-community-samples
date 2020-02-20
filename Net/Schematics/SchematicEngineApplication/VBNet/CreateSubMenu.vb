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
Option Strict Off
Option Explicit On
Friend Class CreateSubMenuSchematic
	Implements ESRI.ArcGIS.SystemUI.IMenuDef

	Private ReadOnly Property IMenuDef_Caption() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Caption
		Get
			Return "Schematic Layer Properties"
		End Get
	End Property

	Private ReadOnly Property IMenuDef_ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IMenuDef.ItemCount
		Get
			Return 3 'There are 3 items on this submenu
		End Get
	End Property

	Private ReadOnly Property IMenuDef_Name() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Name
		Get
			Return "SchematicLayerProperties"
		End Get
	End Property

	Private Sub IMenuDef_GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IMenuDef.GetItemInfo
		Select Case pos	'Commands for the menu - the object browser lists these commands
			Case 0
				itemDef.ID = "esriControls.ControlsSchematicRestoreDefaultLayerPropertiesCommand"
			Case 1
				itemDef.ID = "esriControls.ControlsSchematicPropagateLayerPropertiesCommand"
			Case 2
				itemDef.ID = "esriControls.ControlsSchematicImportLayerPropertiesCommand"
		End Select
	End Sub
End Class