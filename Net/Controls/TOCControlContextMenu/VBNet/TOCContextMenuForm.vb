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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS

Public Class TOCContextMenuForm
    Inherits System.Windows.Forms.Form

    Private m_pTocControl As ITOCControl2
    Private m_pMapControl As IMapControl3
    Private m_pMenuMap As IToolbarMenu
    Private m_pMenuLayer As IToolbarMenu

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
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TOCContextMenuForm))
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.label2.Location = New System.Drawing.Point(240, 464)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(400, 16)
        Me.label2.TabIndex = 8
        Me.label2.Text = "2) Right click on a map or layer in the TOCControl to display a context menu"
        '
        'label1
        '
        Me.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.label1.Location = New System.Drawing.Point(8, 464)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(240, 23)
        Me.label1.TabIndex = 7
        Me.label1.Text = "1) Load a map document into the MapControl"
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(736, 28)
        Me.AxToolbarControl1.TabIndex = 9
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(192, 416)
        Me.AxTOCControl1.TabIndex = 10
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(208, 40)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(536, 416)
        Me.AxMapControl1.TabIndex = 11
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(520, 56)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 12
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(752, 486)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "Form1"
        Me.Text = "ContextMenu"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        m_pTocControl = AxTOCControl1.Object
        m_pMapControl = AxMapControl1.Object

        'Set buddy control
        m_pTocControl.SetBuddyControl(m_pMapControl)
        AxToolbarControl1.SetBuddyControl(m_pMapControl)

        'Add pre-defined control commands to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", -1, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddToolbarDef("esriControls.ControlsMapNavigationToolbar", 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Add custom commands to the map menu
        m_pMenuMap = New ToolbarMenuClass
        m_pMenuMap.AddItem(New ContextMenu.LayerVisibility, 1, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuMap.AddItem(New ContextMenu.LayerVisibility, 2, 1, False, esriCommandStyles.esriCommandStyleTextOnly)
        'Add pre-defined menu to the map menu as a sub menu 
        m_pMenuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 2, True)
        'Add custom commands to the map menu
        m_pMenuLayer = New ToolbarMenuClass
        m_pMenuLayer.AddItem(New ContextMenu.RemoveLayer, -1, 0, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.ScaleThresholds, 1, 1, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.ScaleThresholds, 2, 2, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.ScaleThresholds, 3, 3, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.LayerSelectable, 1, 4, True, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.LayerSelectable, 2, 5, False, esriCommandStyles.esriCommandStyleTextOnly)
        m_pMenuLayer.AddItem(New ContextMenu.ZoomToLayer, -1, 6, True, esriCommandStyles.esriCommandStyleTextOnly)

        'Set the hook of each menu
        m_pMenuLayer.SetHook(m_pMapControl)
        m_pMenuMap.SetHook(m_pMapControl)
    End Sub

    Private Sub AxTOCControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent) Handles AxTOCControl1.OnMouseDown
        If (e.button <> 2) Then Exit Sub

        Dim pItem As esriTOCControlItem
        Dim pMap As IMap = Nothing, pLayer As ILayer = Nothing
        Dim pOther As Object = Nothing, pIndex As Object = Nothing

        'Determine what kind of item is selected
        m_pTocControl.HitTest(e.x, e.y, pItem, CType(pMap, IBasicMap), pLayer, pOther, pIndex)

        'Ensure the item gets selected 
        If (pItem = esriTOCControlItem.esriTOCControlItemMap) Then
            m_pTocControl.SelectItem(pMap, Nothing)
        Else
            m_pTocControl.SelectItem(pLayer, Nothing)
        End If

        'Set the layer into the CustomProperty (this is used by the custom layer commands)			
        m_pMapControl.CustomProperty = pLayer

        'Popup the correct context menu
        If (pItem = esriTOCControlItem.esriTOCControlItemMap) Then m_pMenuMap.PopupMenu(e.x, e.y, m_pTocControl.hWnd)
        If (pItem = esriTOCControlItem.esriTOCControlItemLayer) Then m_pMenuLayer.PopupMenu(e.x, e.y, m_pTocControl.hWnd)
    End Sub
End Class
