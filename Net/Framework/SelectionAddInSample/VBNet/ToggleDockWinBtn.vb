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
