Imports ESRI.ArcGIS.Controls
Imports System.Windows
Imports System.Drawing

Partial Public Class Window1

	Private mapControl As AxMapControl
	Private toolbarControl As AxToolbarControl
	Private tocControl As AxTOCControl

	Public Sub New()

		' This call is required by the Windows Form Designer.
		InitializeComponent()

		CreateEngineControls()
	End Sub

	Private Sub CreateEngineControls()

		'set Engine controls to the child of each hosts 
		mapControl = New AxMapControl()
		mapHost.Child = mapControl

		toolbarControl = New AxToolbarControl()
		toolbarHost.Child = toolbarControl

		tocControl = New AxTOCControl()
		tocHost.Child = tocControl
	End Sub

	Private Sub LoadMap()

		'Buddy up controls
		tocControl.SetBuddyControl(mapControl)
		toolbarControl.SetBuddyControl(mapControl)

		'add command and tools to the toolbar
		toolbarControl.AddItem("esriControls.ControlsOpenDocCommand")
		toolbarControl.AddItem("esriControls.ControlsAddDataCommand")
		toolbarControl.AddItem("esriControls.ControlsSaveAsDocCommand")
		toolbarControl.AddItem("esriControls.ControlsMapNavigationToolbar")
		toolbarControl.AddItem("esriControls.ControlsMapIdentifyTool")

		'set controls' properties
		toolbarControl.BackColor = Color.FromArgb(245, 245, 220)

		'wire up events
		AddHandler mapControl.OnMouseMove, AddressOf mapControl_OnMouseMove

	End Sub

	Private Sub mapControl_OnMouseMove(ByVal sender As Object, ByVal e As IMapControlEvents2_OnMouseMoveEvent)
		textBlock1.Text = " X,Y on Map: " + String.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), mapControl.MapUnits.ToString().Substring(4))
	End Sub

	Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
		LoadMap()
	End Sub

	Private Sub Window_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
		ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
	End Sub

End Class
