Option Strict Off
Option Explicit On
Friend Class NavigationMenu
	Implements ESRI.ArcGIS.SystemUI.IMenuDef
		
	Private ReadOnly Property IMenuDef_Caption() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Caption
		Get
			Return "Navigation"
		End Get
	End Property
	
	Private ReadOnly Property IMenuDef_ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IMenuDef.ItemCount
		Get
			Return 5
		End Get
	End Property
	
	Private ReadOnly Property IMenuDef_Name() As String Implements ESRI.ArcGIS.SystemUI.IMenuDef.Name
		Get
			Return "Navigation"
		End Get
	End Property
	
	Private Sub IMenuDef_GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IMenuDef.GetItemInfo
		Select Case pos 'Commands for the menu - the object browser lists these commands
			Case 0
                itemDef.ID = "esriControls.ControlsMapZoomInFixedCommand"
			Case 1
                itemDef.ID = "esriControls.ControlsMapZoomOutFixedCommand"
			Case 2
                itemDef.ID = "esriControls.ControlsMapFullExtentCommand"
				itemDef.Group = True
			Case 3
                itemDef.ID = "esriControls.ControlsMapZoomToLastExtentBackCommand"
			Case 4
                itemDef.ID = "esriControls.ControlsMapZoomToLastExtentForwardCommand"
		End Select
	End Sub
End Class