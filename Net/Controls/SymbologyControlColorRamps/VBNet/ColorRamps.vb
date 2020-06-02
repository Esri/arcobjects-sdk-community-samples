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
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
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
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(200, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(472, 376)
        Me.AxPageLayoutControl1.TabIndex = 0
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(408, 28)
        Me.AxToolbarControl1.TabIndex = 1
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(184, 376)
        Me.AxTOCControl1.TabIndex = 2
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(184, 136)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 3
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.Location = New System.Drawing.Point(424, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 4
        Me.ComboBox1.Text = "ComboBox1"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(552, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Change Color Ramp"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(680, 422)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Name = "Form1"
        Me.Text = "Color Ramps"
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)
        AxTOCControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add ToolbarControl items
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomInTool")
        AxToolbarControl1.AddItem("esriControls.ControlsMapZoomOutTool")
        AxToolbarControl1.AddItem("esriControls.ControlsMapPanTool")
        AxToolbarControl1.AddItem("esriControls.ControlsMapFullExtentCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsMapIdentifyTool")

        'Disable controls
        Button1.Enabled = False
        ComboBox1.Enabled = False

    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced

        'Clear the combo box
        ComboBox1.Items.Clear()

        'Get IGeoFeatureLayers CLSID
        Dim uid As UID = New UIDClass
        uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
        'Get IGeoFeatureLayers from the focus map
        Dim layers As IEnumLayer
        layers = AxPageLayoutControl1.ActiveView.FocusMap.Layers(uid, True)
        If layers Is Nothing Then Exit Sub

        'Reset enumeration and loop through layers
        layers.Reset()
        Dim geoFeatureLayer As IGeoFeatureLayer = layers.Next()
        While Not geoFeatureLayer Is Nothing
            'If layer contains polygon features add to combo box
            If geoFeatureLayer.FeatureClass.ShapeType = esriGeometryType.esriGeometryPolygon Then
                If Not TypeOf geoFeatureLayer Is IGroupLayer Then
                    ComboBox1.Items.Add(geoFeatureLayer.Name)
                End If
            End If
            geoFeatureLayer = layers.Next()
        End While
        ComboBox1.SelectedIndex = 0

        'Enable controls
        Button1.Enabled = True
        ComboBox1.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Get the layer selected in the combo box
        Dim i As Integer, map As IMap, geofeaturelayer As IGeoFeatureLayer = Nothing
        map = AxPageLayoutControl1.ActiveView.FocusMap
        For i = 0 To map.LayerCount - 1
            If map.Layer(i).Name = ComboBox1.SelectedItem Then
                geofeaturelayer = map.Layer(i)
                Exit For
            End If
        Next i
        If geofeaturelayer Is Nothing Then Exit Sub

        'Create ClassBreaks form 
        Dim classBreaksForm As Form2 = New Form2

        'Get a ClassBreakRenderer that uses the selected ColorRamp
        Dim classBreaksRenderer As IClassBreaksRenderer
        classBreaksRenderer = classBreaksForm.GetClassBreaksRenderer(geofeaturelayer)
        If classBreaksRenderer Is Nothing Then Exit Sub

        'Set the new renderer 
        geofeaturelayer.Renderer = classBreaksRenderer

        'Trigger contents changed event for TOCControl
        AxPageLayoutControl1.ActiveView.ContentsChanged()
        'Refresh the display 
        AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, geofeaturelayer, Nothing)

        'Dispose of the form
        classBreaksForm.Dispose()

    End Sub

End Class
