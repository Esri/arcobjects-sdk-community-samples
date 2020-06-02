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
Friend Class CreateMenuSchematicEditor
	Implements ESRI.ArcGIS.SystemUI.IMenuDef

	Private ReadOnly Property IMenuDef_Caption() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Caption
		Get
			Return "SchematicEditor"
		End Get
	End Property

	Private ReadOnly Property IMenuDef_ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IMenuDef.ItemCount
		Get
			Return 5 'There are 5 items on this menu
		End Get
	End Property

	Private ReadOnly Property IMenuDef_Name() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Name
		Get
			Return "SchematicEditor"
		End Get
	End Property

	Private Sub IMenuDef_GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IMenuDef.GetItemInfo
		Select Case pos	'Commands for the menu - the object browser lists these commands
			Case 0
				itemDef.ID = "esriControls.ControlsSchematicStartEditCommand"
			Case 1
				itemDef.ID = "esriControls.ControlsSchematicStopEditCommand"
			Case 2
				itemDef.ID = "esriControls.ControlsSchematicSaveEditsCommand"
				itemDef.Group = True
			Case 3
				itemDef.ID = "esriControls.ControlsSchematicRemoveLinkPointsCommand"
				itemDef.Group = True
			Case 4
				itemDef.ID = "esriControls.ControlsSchematicSquareLinksCommand"
		End Select
	End Sub
End Class