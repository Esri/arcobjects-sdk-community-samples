Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS
Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)

        'Release COM objects 
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents cmdDraw As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdChangeArea As System.Windows.Forms.Button
    Friend WithEvents cmdChangeLine As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.cmdDraw = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdChangeArea = New System.Windows.Forms.Button
        Me.cmdChangeLine = New System.Windows.Forms.Button
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(0, 0)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(265, 28)
        Me.AxToolbarControl1.TabIndex = 0
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(0, 48)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(760, 472)
        Me.AxPageLayoutControl1.TabIndex = 1
        '
        'cmdDraw
        '
        Me.cmdDraw.Location = New System.Drawing.Point(304, 8)
        Me.cmdDraw.Name = "cmdDraw"
        Me.cmdDraw.Size = New System.Drawing.Size(96, 23)
        Me.cmdDraw.TabIndex = 2
        Me.cmdDraw.Text = "Add Legend"
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(408, 8)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(96, 23)
        Me.cmdDelete.TabIndex = 3
        Me.cmdDelete.Text = "Delete Legend"
        '
        'cmdChangeArea
        '
        Me.cmdChangeArea.Location = New System.Drawing.Point(512, 8)
        Me.cmdChangeArea.Name = "cmdChangeArea"
        Me.cmdChangeArea.Size = New System.Drawing.Size(112, 23)
        Me.cmdChangeArea.TabIndex = 4
        Me.cmdChangeArea.Text = "Change Area Patch"
        '
        'cmdChangeLine
        '
        Me.cmdChangeLine.Location = New System.Drawing.Point(632, 8)
        Me.cmdChangeLine.Name = "cmdChangeLine"
        Me.cmdChangeLine.Size = New System.Drawing.Size(120, 23)
        Me.cmdChangeLine.TabIndex = 5
        Me.cmdChangeLine.Text = "AreaLinePatches"
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(440, 200)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(760, 518)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.cmdChangeLine)
        Me.Controls.Add(Me.cmdChangeArea)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdDraw)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Name = "Form1"
        Me.Text = "AreaLinePatches"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(Me.AxPageLayoutControl1)

        'Add ToolbarControl items
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool")

        'disable buttons for draw legend, change area/line patches, delete legend
        cmdDraw.Enabled = False
        cmdDelete.Enabled = False
        cmdChangeArea.Enabled = False
        cmdChangeLine.Enabled = False

    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced

        'When a document gets loaded into the PageLayoutControl enable the draw legend button
        cmdDraw.Enabled = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDraw.Click

        'Get the GraphicsContainer
        Dim graphicsContainer As IGraphicsContainer
        graphicsContainer = AxPageLayoutControl1.GraphicsContainer

        'Get the MapFrame
        Dim mapFrame As IMapFrame
        mapFrame = graphicsContainer.FindFrame(AxPageLayoutControl1.ActiveView.FocusMap)
        If mapFrame Is Nothing Then Exit Sub

        'Create a legend
        Dim uID As UID = New UIDClass
        uID.Value = "esriCarto.Legend"

        'Create a MapSurroundFrame from the MapFrame
        Dim mapSurroundFrame As IMapSurroundFrame
        mapSurroundFrame = mapFrame.CreateSurroundFrame(uID, Nothing)
        If mapSurroundFrame Is Nothing Then Return
        If mapSurroundFrame.MapSurround Is Nothing Then Return
        'Set the name 
        mapSurroundFrame.MapSurround.Name = "Legend"

        'Envelope for the legend
        Dim envelope As IEnvelope = New EnvelopeClass
        envelope.PutCoords(1, 1, 3.4, 2.4)

        'Set the geometry of the MapSurroundFrame 
        Dim element As IElement
        element = mapSurroundFrame
        element.Geometry = envelope

        'Add the legend to the PageLayout
        AxPageLayoutControl1.AddElement(element, Type.Missing, Type.Missing, "Legend", 0)

        'Refresh the PageLayoutControl
        AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

        'disable/enable buttons
        cmdDraw.Enabled = False
        cmdDelete.Enabled = True
        cmdChangeArea.Enabled = True
        cmdChangeLine.Enabled = True

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        'Find the legend 
        Dim element As IElement
        element = AxPageLayoutControl1.FindElementByName("Legend", 1)

        If Not element Is Nothing Then
            'Delete the legend
            Dim graphicsContainer As IGraphicsContainer
            graphicsContainer = AxPageLayoutControl1.GraphicsContainer
            graphicsContainer.DeleteElement(element)
            'Refresh the display
            AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

            'enable/disable buttons
            cmdDraw.Enabled = True
            cmdDelete.Enabled = False
            cmdChangeArea.Enabled = False
            cmdChangeLine.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeArea.Click

        'create the form with the SymbologyControl
        Dim symbolForm As New Form2

        'Get the IStyleGalleryItem that has been selected in the SymbologyControl
        Dim styleGalleryItem As IStyleGalleryItem
        styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassAreaPatches)

        'release the form
        symbolForm.Dispose()
        If styleGalleryItem Is Nothing Then Exit Sub

        'Find the legend
        Dim element As IElement
        element = AxPageLayoutControl1.FindElementByName("Legend", 1)
        If element Is Nothing Then Exit Sub

        'Get the IMapSurroundFrame
        Dim mapSurroundFrame As IMapSurroundFrame
        mapSurroundFrame = element
        If mapSurroundFrame Is Nothing Then Exit Sub

        'If a legend exists change the default area patch
        Dim legend As ILegend
        legend = mapSurroundFrame.MapSurround
        legend.Format.DefaultAreaPatch = styleGalleryItem.Item

        'Update the legend
        legend.Refresh()
        'Refresh the display
        AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeLine.Click

        'create the form with the SymbologyControl
        Dim symbolForm As New Form2

        'Get the IStyleGalleryItem that has been selected in the SymbologyControl
        Dim styleGalleryItem As IStyleGalleryItem
        styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassLinePatches)

        'release the form
        symbolForm.Dispose()
        If styleGalleryItem Is Nothing Then Exit Sub

        'Find the legend
        Dim element As IElement
        element = AxPageLayoutControl1.FindElementByName("Legend", 1)
        If element Is Nothing Then Exit Sub

        'Get the IMapSurroundFrame
        Dim mapSurroundFrame As IMapSurroundFrame
        mapSurroundFrame = element
        If mapSurroundFrame Is Nothing Then Exit Sub

        'If a legend exists change the default area patch
        Dim legend As ILegend
        legend = mapSurroundFrame.MapSurround
        legend.Format.DefaultLinePatch = styleGalleryItem.Item

        'Update the legend
        legend.Refresh()
        'Refresh the display
        AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

End Class
