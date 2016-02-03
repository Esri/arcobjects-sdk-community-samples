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