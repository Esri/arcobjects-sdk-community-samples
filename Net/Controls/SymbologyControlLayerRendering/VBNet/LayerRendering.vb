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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS
Friend Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
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
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(496, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(160, 33)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "1) Load a map document into the PageLayoutControl. "
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(496, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(152, 33)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "2) Right click on a feature layer to change its symbology"
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(648, 28)
        Me.AxToolbarControl1.TabIndex = 5
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(496, 112)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 7
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(152, 368)
        Me.AxTOCControl1.TabIndex = 8
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(168, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(320, 368)
        Me.AxPageLayoutControl1.TabIndex = 9
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(664, 414)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Layer Rendering"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add ToolbarControl items
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageFocusNextMapCommand", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageFocusPreviousMapCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

    End Sub

    Private Sub AxTOCControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent) Handles AxTOCControl1.OnMouseDown

        'Exit if not right mouse button
        If (e.button <> 2) Then Exit Sub

        Dim map As IMap = New MapClass
        Dim layer As ILayer = New FeatureLayerClass
        Dim other As Object = New Object
        Dim index As Object = New Object
        Dim item As esriTOCControlItem

        'Determine what kind of item has been clicked on
        AxTOCControl1.HitTest(e.x, e.y, item, map, layer, other, index)

        If layer Is Nothing Then Exit Sub
        If Not TypeOf layer Is IFeatureLayer Then Exit Sub

        'QI to IFeatureLayer and IGeoFeatuerLayer interface
        Dim featureLayer As IFeatureLayer = layer
        Dim geoFeatureLayer As IGeoFeatureLayer = featureLayer
        Dim simpleRenderer As ISimpleRenderer = geoFeatureLayer.Renderer

        'Create the form with the SymbologyControl
        Dim symbolForm As New frmSymbol

        'Get the IStyleGalleryItem
        Dim styleGalleryItem As IStyleGalleryItem = Nothing
        'Select SymbologyStyleClass based upon feature type
        Select Case featureLayer.FeatureClass.ShapeType
            Case esriGeometryType.esriGeometryPoint
                styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols, simpleRenderer.Symbol)
            Case esriGeometryType.esriGeometryPolyline
                styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassLineSymbols, simpleRenderer.Symbol)
            Case esriGeometryType.esriGeometryPolygon
                styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassFillSymbols, simpleRenderer.Symbol)
        End Select

        'Release the form
        symbolForm.Dispose()
        Me.Activate()
        If styleGalleryItem Is Nothing Then Exit Sub

        'Create a new renderer
        simpleRenderer = New SimpleRendererClass
        'Set its symbol from the styleGalleryItem
        simpleRenderer.Symbol = styleGalleryItem.Item
        'Set the renderer into the geoFeatureLayer
        geoFeatureLayer.Renderer = simpleRenderer

        'Fire contents changed event that the TOCControl listens to
        AxPageLayoutControl1.ActiveView.ContentsChanged()
        'Refresh the display
        AxPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGeography, Nothing, Nothing)

    End Sub
End Class