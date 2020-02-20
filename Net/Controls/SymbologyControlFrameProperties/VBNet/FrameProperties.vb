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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS
Public Class Form1
    Inherits System.Windows.Forms.Form

    <STAThread()> _
Shared Sub Main()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
        Application.Run(New Form1())
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(416, 400)
        Me.AxPageLayoutControl1.TabIndex = 0
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(416, 28)
        Me.AxToolbarControl1.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.Location = New System.Drawing.Point(432, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(264, 21)
        Me.ComboBox1.TabIndex = 4
        Me.ComboBox1.Text = "ComboBox1"
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(432, 40)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(265, 265)
        Me.AxSymbologyControl1.TabIndex = 5
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(336, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(706, 446)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form1"
        Me.Text = "Setting Frame Properties"
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set the buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsSaveAsDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)

        'Get the ArcGIS install location by opening the subkey for reading
        Dim sInstall As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(sInstall & "\\Styles\\ESRI.ServerStyle")

        'Add style classes to the combo box
        ComboBox1.Items.Add("Backgrounds")
        ComboBox1.Items.Add("Borders")
        ComboBox1.Items.Add("Shadows")
        ComboBox1.SelectedIndex = 0

        'Update each style class. This forces item to be loaded into each style class.
        'When the contents of a server style file are loaded into the SymbologyControl 
        'items are 'demand loaded'. This is done to increase performance and means 
        'items are only loaded into a SymbologyStyleClass when it is the current StyleClass.
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBackgrounds).Update()
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBorders).Update()
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassShadows).Update()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        'Set the SymbologyControl style class
        If ComboBox1.SelectedItem = "Backgrounds" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds
        ElseIf ComboBox1.SelectedItem = "Borders" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders
        ElseIf ComboBox1.SelectedItem = "Shadows" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassShadows
        End If

    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected

        Dim styleGalleryItem As IStyleGalleryItem = e.styleGalleryItem

        'Get the frame containing the focus map
        Dim frameProperties As IFrameProperties
        frameProperties = AxPageLayoutControl1.GraphicsContainer.FindFrame(AxPageLayoutControl1.ActiveView.FocusMap)

        If TypeOf styleGalleryItem.Item Is IBackground Then
            'Set the frame's background
            frameProperties.Background = styleGalleryItem.Item
        ElseIf TypeOf styleGalleryItem.Item Is IBorder Then
            'Set the frame's border
            frameProperties.Border = styleGalleryItem.Item
        ElseIf TypeOf styleGalleryItem.Item Is IShadow Then
            'Set the frame's shadow
            frameProperties.Shadow = styleGalleryItem.Item
        End If

        'Refresh the PageLayoutControl
        AxPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewBackground, Nothing, Nothing)

    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced

        'Get the frame containing the focus map
        Dim frameProperties As IFrameProperties
        frameProperties = AxPageLayoutControl1.GraphicsContainer.FindFrame(AxPageLayoutControl1.ActiveView.FocusMap)

        'Create a new ServerStyleGalleryItem with its name set
        Dim styleGalleryItem As IStyleGalleryItem = New ServerStyleGalleryItemClass
        styleGalleryItem.Name = "myStyle"

        Dim styleClass As ISymbologyStyleClass

        'Get the background style class and remove any custom style
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBackgrounds)
        If styleClass.GetItem(0).Name = "myStyle" Then styleClass.RemoveItem(0)
        If Not frameProperties.Background Is Nothing Then
            'Set the background into the style gallery item
            styleGalleryItem.Item = frameProperties.Background
            'Add the item to the style class
            styleClass.AddItem(styleGalleryItem, 0)
        End If

        'Get the border style class and remove any custom style
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassBorders)
        If styleClass.GetItem(0).Name = "myStyle" Then styleClass.RemoveItem(0)
        If Not frameProperties.Border Is Nothing Then
            'Set the border into the style gallery item
            styleGalleryItem.Item = frameProperties.Border
            'Add the item to the style class
            styleClass.AddItem(styleGalleryItem, 0)
        End If

        'Get the shadow style class and remove any custom style
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassShadows)
        If styleClass.GetItem(0).Name = "myStyle" Then styleClass.RemoveItem(0)
        If Not frameProperties.Shadow Is Nothing Then
            'Set the border into the style gallery item
            styleGalleryItem.Item = frameProperties.Shadow
            'Add the item to the style class
            styleClass.AddItem(styleGalleryItem, 0)
        End If

    End Sub

End Class
