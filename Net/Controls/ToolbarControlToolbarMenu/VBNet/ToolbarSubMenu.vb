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
Friend Class ToolbarSubMenu
	Implements ESRI.ArcGIS.SystemUI.IMenuDef
	
	Private ReadOnly Property IMenuDef_Caption() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Caption
		Get
			Return "SubMenu"
		End Get
	End Property
	
	Private ReadOnly Property IMenuDef_ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IMenuDef.ItemCount
		Get
			Return 8
		End Get
	End Property
	
	Private ReadOnly Property IMenuDef_Name() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Name
		Get
			Return "SubMenu"
		End Get
	End Property
	
	Private Sub IMenuDef_GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IMenuDef.GetItemInfo
		Select Case pos 'Commands for the menu - the object browser lists these commands
			Case 0
                itemDef.ID = "esriControls.ControlsMapUpCommand"
			Case 1
                itemDef.ID = "esriControls.ControlsMapDownCommand"
			Case 2
                itemDef.ID = "esriControls.ControlsMapLeftCommand"
			Case 3
                itemDef.ID = "esriControls.ControlsMapRightCommand"
			Case 4
                itemDef.ID = "esriControls.ControlsMapPageUpCommand"
				itemDef.Group = True
			Case 5
                itemDef.ID = "esriControls.ControlsMapPageDownCommand"
			Case 6
                itemDef.ID = "esriControls.ControlsMapPageLeftCommand"
			Case 7
                itemDef.ID = "esriControls.ControlsMapPageRightCommand"
		End Select
	End Sub
End Class