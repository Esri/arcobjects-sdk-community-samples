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