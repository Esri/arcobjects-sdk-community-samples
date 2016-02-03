Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesGDB

Public Class EditingForm

    Private m_engineEditor As IEngineSnapEnvironment


    Private Sub EditingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'set buddy controls
        AxTOCControl1.SetBuddyControl(AxMapControl1)
        AxEditorToolbar.SetBuddyControl(AxMapControl1)
        AxToolbarControl1.SetBuddyControl(AxMapControl1)

        'Add items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToolControl", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsMapMeasureTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add items to the custom editor toolbar          
        AxEditorToolbar.AddItem("esriControls.ControlsEditingEditorMenu", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingEditTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingSketchTool", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsUndoCommand", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsRedoCommand", 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingTaskToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxEditorToolbar.AddItem("esriControls.ControlsEditingTargetToolControl", 0, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'share the command pool
        AxToolbarControl1.CommandPool = AxEditorToolbar.CommandPool

        'Create an operation stack for the undo and redo commands to use
        Dim operationStack As IOperationStack = New ControlsOperationStackClass()
        AxEditorToolbar.OperationStack = operationStack

        'add some sample line data to the map
        Dim workspaceFactory As IWorkspaceFactory = New FileGDBWorkspaceFactory
        'relative file path to the sample data from EXE location
        Dim filePath As String = "..\..\..\..\data\AirportsAndGolf\Golf.gdb"
        Dim workspace As IFeatureWorkspace = CType(workspaceFactory.OpenFromFile(filePath, AxMapControl1.hWnd), IFeatureWorkspace)
        Dim featureLayer As IFeatureLayer = New FeatureLayerClass()

        featureLayer.Name = "Golf Courses"
        featureLayer.Visible = True
        featureLayer.FeatureClass = workspace.OpenFeatureClass("GOLF")

        AxMapControl1.Map.MapUnits = esriUnits.esriMeters
        AxMapControl1.Map.AddLayer(CType(featureLayer, ILayer))

        'set up the snapping environment
        m_engineEditor = New EngineEditorClass()
        m_engineEditor.SnapToleranceUnits = esriEngineSnapToleranceUnits.esriEngineSnapToleranceMapUnits
        m_engineEditor.SnapTolerance = 2000

    End Sub

    
End Class

