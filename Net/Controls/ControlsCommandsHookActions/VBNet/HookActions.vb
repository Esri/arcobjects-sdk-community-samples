Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS

Public Class HookActions
    Private m_ToolbarMenu1 As IToolbarMenu
    Private m_ToolbarMenu2 As IToolbarMenu

    <STAThread()> _
   Shared Sub Main()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New HookActions())
    End Sub

    Private Sub HookActions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Add generic commands 
        AxToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        'Add map navigation commands
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add generic commands 
        AxToolbarControl2.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        'Add globe navigation commands
        AxToolbarControl2.AddItem("esriControls.ControlsGlobeZoomInOutTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsGlobePanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsGlobeFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl2.AddItem("esriControls.ControlsGlobeSelectFeaturesTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Create menu 
        m_ToolbarMenu1 = New ToolbarMenuClass()
        'Set hook and command pool
        m_ToolbarMenu1.SetHook(AxToolbarControl1)
        m_ToolbarMenu1.CommandPool = AxToolbarControl1.CommandPool
        'Add custom commands
        m_ToolbarMenu1.AddItem(New hookActionsPan, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu1.AddItem(New hookActionsZoom, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu1.AddItem(New hookActionsFlash, 0, -1, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu1.AddItem(New hookActionsGraphic, 0, -1, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu1.AddItem(New hookActionsLabel, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu1.AddItem(New hookActionsCallout, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)

        'Create menu 
        m_ToolbarMenu2 = New ToolbarMenuClass()
        'Set hook and command pool
        m_ToolbarMenu2.SetHook(AxToolbarControl2)
        m_ToolbarMenu2.CommandPool = AxToolbarControl2.CommandPool
        'Add custom commands
        m_ToolbarMenu2.AddItem(New hookActionsPan, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu2.AddItem(New hookActionsZoom, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu2.AddItem(New hookActionsFlash, 0, -1, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu2.AddItem(New hookActionsGraphic, 0, -1, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu2.AddItem(New hookActionsLabel, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_ToolbarMenu2.AddItem(New hookActionsCallout, 0, -1, False, esriCommandStyles.esriCommandStyleTextOnly)


    End Sub


    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'Popup menu
        If e.button = 2 Then
            m_ToolbarMenu1.PopupMenu(e.x, e.y, AxMapControl1.hWnd)
        End If
    End Sub

    Private Sub AxGlobeControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IGlobeControlEvents_OnMouseDownEvent) Handles AxGlobeControl1.OnMouseDown
        'Popup menu
        If e.button = 2 Then
            m_ToolbarMenu2.PopupMenu(e.x, e.y, AxGlobeControl1.hWnd)
        End If
    End Sub

    Private Sub HookActions_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
    End Sub
End Class
