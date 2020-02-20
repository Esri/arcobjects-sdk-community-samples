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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS




Public Class MapViewer
    'The popup menu
    Private m_pToolbarMenu As IToolbarMenu
    'The envelope drawn on the MapControl
    Private m_pEnvelope As IEnvelope
    'The symbol used to draw the envelope on the MapControl
    Private m_pFillSymbol As ISimpleFillSymbol
    'The PageLayoutControl's focus map events 
    Private m_pTransformEvents As ITransformEvents_Event
    Private visBoundsUpdatedE As ITransformEvents_VisibleBoundsUpdatedEventHandler
    'The CustomizeDialog used by the ToolbarControl
    Private m_pCustomizeDialog As ICustomizeDialog
    'The CustomizeDialog start event
    Private startDialogE As ICustomizeDialogEvents_OnStartDialogEventHandler
    'The CustomizeDialog close event 
    Private closeDialogE As ICustomizeDialogEvents_OnCloseDialogEventHandler

    <STAThread()> _
    Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New MapViewer())
    End Sub

    Private Sub MapViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Create the customize dialog for the ToolbarControl
        CreateCustomizeDialog()

        'Create symbol used on the MapControl
        CreateOverviewSymbol()

        'Set label editing to manual
        AxTOCControl1.LabelEdit = esriTOCControlEdit.esriTOCControlManual

        'Get file name used to persist the ToolbarControl 
        Dim filePath As String
        filePath = System.Reflection.Assembly.GetExecutingAssembly.Location.Replace("Controls.exe", "") & "\PersistedItems.txt"
        If System.IO.File.Exists(filePath) Then
            LoadToolbarControlItems(filePath)
        Else
            'Add generic commands
            AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsAddDataCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            'Add page layout navigation commands
            AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsPagePanTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            'Add map navigation commands
            AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentBackCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapZoomToLastExtentForwardCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            'Add map inquiry commands
            AxToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapFindCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            AxToolbarControl1.AddItem("esriControls.ControlsMapMeasureTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
            'Add custom AddDateTool
            AxToolbarControl1.AddItem("Commands.AddDateTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

            'Create a new ToolbarPalette
            Dim pToolbarPalette As IToolbarPalette = New ToolbarPaletteClass()
            'Add commands and tools to the ToolbarPalette
            pToolbarPalette.AddItem("esriControls.ControlsNewMarkerTool", -1, -1)
            pToolbarPalette.AddItem("esriControls.ControlsNewLineTool", -1, -1)
            pToolbarPalette.AddItem("esriControls.ControlsNewCircleTool", -1, -1)
            pToolbarPalette.AddItem("esriControls.ControlsNewEllipseTool", -1, -1)
            pToolbarPalette.AddItem("esriControls.ControlsNewRectangleTool", -1, -1)
            pToolbarPalette.AddItem("esriControls.ControlsNewPolygonTool", -1, -1)
            'Add the ToolbarPalette to the ToolbarControl
            AxToolbarControl1.AddItem(pToolbarPalette, 0, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        End If

        'Create a new ToolbarMenu
        m_pToolbarMenu = New ToolbarMenuClass()
        'Share the ToolbarControl's command pool
        m_pToolbarMenu.CommandPool = AxToolbarControl1.CommandPool
        'Set the hook to the PageLayoutControl
        m_pToolbarMenu.SetHook(AxPageLayoutControl1)
        'Add commands to the ToolbarMenu
        m_pToolbarMenu.AddItem("esriControls.ControlsPageZoomInFixedCommand", -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
        m_pToolbarMenu.AddItem("esriControls.ControlsPageZoomOutFixedCommand", -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
        m_pToolbarMenu.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)
        m_pToolbarMenu.AddItem("esriControls.ControlsPageZoomPageToLastExtentBackCommand", -1, -1, True, esriCommandStyles.esriCommandStyleIconAndText)
        m_pToolbarMenu.AddItem("esriControls.ControlsPageZoomPageToLastExtentForwardCommand", -1, -1, False, esriCommandStyles.esriCommandStyleIconAndText)

        'Set buddy controls
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)

        'Load a pre-authored map document into the PageLayoutControl using relative paths
        Dim sFileName As String = System.IO.Path.Combine (Environment.SpecialFolder.MyDocuments, "ArcGIS\data\GulfOfStLawrence\Gulf_of_St._Lawrence.mxd")
        If AxPageLayoutControl1.CheckMxFile(sFileName) Then
            AxPageLayoutControl1.LoadMxFile(sFileName)
        End If

    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced
        'Get the IActiveView of the focus map in the PageLayoutControl
        Dim activeView As IActiveView = AxPageLayoutControl1.ActiveView.FocusMap
        'Trap the ITranformEvents of the PageLayoutCntrol's focus map 
        visBoundsUpdatedE = New ITransformEvents_VisibleBoundsUpdatedEventHandler(AddressOf OnVisibleBoundsUpdated)
        'Start listening to the transform events interface
        m_pTransformEvents = activeView.ScreenDisplay.DisplayTransformation
        'Start listening to the VisibleBoundsUpdated method on ITransformEvents interface
        AddHandler (m_pTransformEvents.VisibleBoundsUpdated), visBoundsUpdatedE
        'Get the extent of the focus map
        m_pEnvelope = activeView.Extent

        'Load the same pre-authored map document into the MapControl
        AxMapControl1.LoadMxFile(AxPageLayoutControl1.DocumentFilename)
        'Set the extent of the MapControl to the full extent of the data
        AxMapControl1.Extent = AxMapControl1.FullExtent
    End Sub

    Private Sub MapViewer_ResizeBegin(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeBegin
        'Suppress data redraw and draw bitmap instead
        AxMapControl1.SuppressResizeDrawing(True, 0)
        AxPageLayoutControl1.SuppressResizeDrawing(True, 0)
    End Sub

    Private Sub MapViewer_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        'Stop bitmap draw and draw data
        AxMapControl1.SuppressResizeDrawing(False, 0)
        AxPageLayoutControl1.SuppressResizeDrawing(False, 0)
    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown
        'Popup the ToolbarMenu
        If e.button = 2 Then
            m_pToolbarMenu.PopupMenu(e.x, e.y, AxPageLayoutControl1.hWnd)
        End If
    End Sub

    Private Sub AxTOCControl1_OnEndLabelEdit(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ITOCControlEvents_OnEndLabelEditEvent) Handles AxTOCControl1.OnEndLabelEdit
        'If the new label is an empty string then prevent the edit
        If e.newLabel.Trim = "" Then e.canEdit = False
    End Sub

    Private Sub CreateOverviewSymbol()
        'Get the IRGBColor interface
        Dim pColor As IRgbColor
        pColor = New RgbColorClass
        'Set the color properties
        pColor.RGB = RGB(255, 0, 0)
        'Get the ILine symbol interface
        Dim pOutline As ILineSymbol
        pOutline = New SimpleLineSymbolClass
        'Set the line symbol properties
        pOutline.Width = 1.5
        pOutline.Color = pColor
        'Get the ISimpleFillSymbol interface
        m_pFillSymbol = New SimpleFillSymbolClass
        'Set the fill symbol properties
        m_pFillSymbol.Outline = pOutline
        m_pFillSymbol.Style = esriSimpleFillStyle.esriSFSHollow
    End Sub

    Private Sub OnVisibleBoundsUpdated(ByVal sender As IDisplayTransformation, ByVal sizeChanged As Boolean)
        'Set the extent to the new visible extent
        m_pEnvelope = sender.VisibleBounds
        'Refresh the MapControl's foreground phase
        AxMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, Nothing, Nothing)
    End Sub

    Private Sub AxMapControl1_OnAfterDraw(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterDrawEvent) Handles AxMapControl1.OnAfterDraw
        If m_pEnvelope Is Nothing Then Exit Sub

        'If the foreground phase has drawn
        Dim pViewDrawPhase As esriViewDrawPhase
        pViewDrawPhase = e.viewDrawPhase
        If pViewDrawPhase = esriViewDrawPhase.esriViewForeground Then
            'Draw the shape on the MapControl
            AxMapControl1.DrawShape(m_pEnvelope, m_pFillSymbol)
        End If
    End Sub

    Private Sub CreateCustomizeDialog()
        'Create new customize dialog 
        m_pCustomizeDialog = New CustomizeDialogClass()
        'Set the title
        m_pCustomizeDialog.DialogTitle = "Customize ToolbarControl Items"
        'Show the 'Add from File' button
        m_pCustomizeDialog.ShowAddFromFile = True
        'Set the ToolbarControl that new items will be added to
        m_pCustomizeDialog.SetDoubleClickDestination(AxToolbarControl1)

        'Set the customize dialog events 
        startDialogE = New ICustomizeDialogEvents_OnStartDialogEventHandler(AddressOf OnStartDialog)
        AddHandler CType(m_pCustomizeDialog, ICustomizeDialogEvents_Event).OnStartDialog, startDialogE
        closeDialogE = New ICustomizeDialogEvents_OnCloseDialogEventHandler(AddressOf OnCloseDialog)
        AddHandler CType(m_pCustomizeDialog, ICustomizeDialogEvents_Event).OnCloseDialog, closeDialogE
    End Sub

    Private Sub chkCustomize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomize.CheckedChanged
        'Show or hide the customize dialog
        If chkCustomize.Checked = False Then
            m_pCustomizeDialog.CloseDialog()
        Else
            m_pCustomizeDialog.StartDialog(AxToolbarControl1.hWnd)
        End If
    End Sub

    Private Sub OnStartDialog()
        AxToolbarControl1.Customize = True
    End Sub

    Private Sub OnCloseDialog()
        AxToolbarControl1.Customize = False
        chkCustomize.Checked = False
    End Sub

    Private Sub SaveToolbarControlItems(ByVal filePath)
        'Create a MemoryBlobStream
        Dim blobStream As IBlobStream
        blobStream = New MemoryBlobStreamClass
        'Get the IStream interface
        Dim stream As IStream
        stream = blobStream

        'Save the ToolbarControl into the stream
        AxToolbarControl1.SaveItems(stream)
        'Save the stream to a file
        blobStream.SaveToFile(filePath)
    End Sub

    Private Sub LoadToolbarControlItems(ByVal filePath)
        'Create a MemoryBlobStream
        Dim blobStream As IBlobStream
        blobStream = New MemoryBlobStreamClass
        'Get the IStream interface
        Dim stream As IStream
        stream = blobStream

        'Load the stream from the file
        blobStream.LoadFromFile(filePath)
        'Load the stream into the ToolbarControl
        AxToolbarControl1.LoadItems(stream)
    End Sub

    Private Sub MapViewer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Get file name to persist the ToolbarControl 
        Dim filePath As String
        filePath = System.Reflection.Assembly.GetExecutingAssembly.Location.Replace("Controls.exe", "") & "\PersistedItems.txt"
        'Persist ToolbarControl contents
        SaveToolbarControlItems(filePath)
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        'Exit if there is no system default printer
        If AxPageLayoutControl1.Printer Is Nothing Then
            MessageBox.Show("Unable to print!", "No default printer")
            Exit Sub
        End If

        'Set printer papers orientation to that of the Page
        AxPageLayoutControl1.Printer.Paper.Orientation = AxPageLayoutControl1.Page.Orientation
        'Scale to the page
        AxPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale
        'Send the pagelayout to the printer
        AxPageLayoutControl1.PrintPageLayout()
    End Sub

End Class
