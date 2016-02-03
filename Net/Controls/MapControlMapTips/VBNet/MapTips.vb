Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form

    <STAThread()> _
Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Application shutting down. Unable to bind to ArcGIS Engine runtime.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New Form1())
    End Sub
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
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
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents chkShowTips As System.Windows.Forms.CheckBox
    Public WithEvents cmdLoadData As System.Windows.Forms.Button
    Public WithEvents cboDataField As System.Windows.Forms.ComboBox
    Public WithEvents cboDataLayer As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents chkTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.chkShowTips = New System.Windows.Forms.CheckBox
        Me.cmdLoadData = New System.Windows.Forms.Button
        Me.cboDataField = New System.Windows.Forms.ComboBox
        Me.cboDataLayer = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.chkTransparent = New System.Windows.Forms.CheckBox
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.Location = New System.Drawing.Point(223, 400)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(113, 25)
        Me.cmdFullExtent.TabIndex = 7
        Me.cmdFullExtent.Text = "Zoom to Full Extent"
        Me.cmdFullExtent.UseVisualStyleBackColor = False
        '
        'chkShowTips
        '
        Me.chkShowTips.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowTips.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowTips.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowTips.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowTips.Location = New System.Drawing.Point(8, 40)
        Me.chkShowTips.Name = "chkShowTips"
        Me.chkShowTips.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowTips.Size = New System.Drawing.Size(129, 25)
        Me.chkShowTips.TabIndex = 4
        Me.chkShowTips.Text = "Show Map Tips"
        Me.chkShowTips.UseVisualStyleBackColor = False
        '
        'cmdLoadData
        '
        Me.cmdLoadData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadData.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadData.Location = New System.Drawing.Point(8, 8)
        Me.cmdLoadData.Name = "cmdLoadData"
        Me.cmdLoadData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadData.Size = New System.Drawing.Size(113, 25)
        Me.cmdLoadData.TabIndex = 3
        Me.cmdLoadData.Text = "Load Document..."
        Me.cmdLoadData.UseVisualStyleBackColor = False
        '
        'cboDataField
        '
        Me.cboDataField.BackColor = System.Drawing.SystemColors.Window
        Me.cboDataField.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDataField.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDataField.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDataField.Location = New System.Drawing.Point(176, 40)
        Me.cboDataField.Name = "cboDataField"
        Me.cboDataField.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDataField.Size = New System.Drawing.Size(161, 22)
        Me.cboDataField.TabIndex = 2
        '
        'cboDataLayer
        '
        Me.cboDataLayer.BackColor = System.Drawing.SystemColors.Window
        Me.cboDataLayer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDataLayer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDataLayer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDataLayer.Location = New System.Drawing.Point(176, 16)
        Me.cboDataLayer.Name = "cboDataLayer"
        Me.cboDataLayer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDataLayer.Size = New System.Drawing.Size(161, 22)
        Me.cboDataLayer.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(7, 400)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(209, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Left mouse button to zoomin, right to pan"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(136, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(33, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Fields:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(136, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Layers:"
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 89)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(328, 296)
        Me.AxMapControl1.TabIndex = 9
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(304, 68)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 10
        '
        'chkTransparent
        '
        Me.chkTransparent.AutoSize = True
        Me.chkTransparent.Location = New System.Drawing.Point(8, 65)
        Me.chkTransparent.Name = "chkTransparent"
        Me.chkTransparent.Size = New System.Drawing.Size(108, 18)
        Me.chkTransparent.TabIndex = 11
        Me.chkTransparent.Text = "Transparent Tips"
        Me.chkTransparent.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(350, 433)
        Me.Controls.Add(Me.chkTransparent)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.chkShowTips)
        Me.Controls.Add(Me.cmdLoadData)
        Me.Controls.Add(Me.cboDataField)
        Me.Controls.Add(Me.cboDataLayer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Map Tips"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private Sub cboDataField_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDataField.SelectedIndexChanged

        'Get IFeatureLayer interface
        Dim pFeatureLayer As IFeatureLayer
        pFeatureLayer = AxMapControl1.get_Layer(cboDataLayer.SelectedIndex)
        'Query interface for IlayerFields
        Dim pLayerFields As ILayerFields
        pLayerFields = pFeatureLayer

        Dim i As Integer
        Dim pField As IField
        'Loop through the fields
        For i = 0 To pLayerFields.FieldCount - 1
            'Get IField interface
            pField = pLayerFields.Field(i)
            'If the field name is the name selected in the control
            If pField.Name = cboDataField.Text Then
                'Set the field as the display field
                pFeatureLayer.DisplayField = pField.Name
                Exit For
            End If
        Next i

    End Sub

    Private Sub cboDataLayer_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDataLayer.SelectedIndexChanged

        'Disable field combo if feature layer is not selected and exit
        If Not TypeOf AxMapControl1.get_Layer(cboDataLayer.SelectedIndex) Is IFeatureLayer Then
            cboDataField.Items.Clear()
            cboDataField.Enabled = False
            Exit Sub
        End If

        'Get IFeatureLayer interface
        Dim pFeatureLayer As IFeatureLayer
        pFeatureLayer = AxMapControl1.get_Layer(cboDataLayer.SelectedIndex)
        'Query interface for ILayerFields
        Dim pLayerFields As ILayerFields
        pLayerFields = pFeatureLayer

        Dim i As Integer
        Dim j As Integer
        j = 0
        Dim pField As IField
        cboDataField.Items.Clear()
        cboDataField.Enabled = True
        'Loop through the fields
        For i = 0 To pLayerFields.FieldCount - 1
            'Get IField interface
            pField = pLayerFields.Field(i)
            'If the field is not the shape field
            If pField.Type <> esriFieldType.esriFieldTypeGeometry Then
                'Add field name to the control
                cboDataField.Items.Insert(j, pField.Name)
                'If the field name is the display field
                If pField.Name = pFeatureLayer.DisplayField Then
                    'Select the field name in the control
                    cboDataField.SelectedIndex = j
                End If
                j = j + 1
            End If
        Next i

        ShowLayerTips()

    End Sub

    Private Sub chkShowTips_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowTips.CheckStateChanged
        ShowLayerTips()
    End Sub

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click
        'Zoom to full extent of data
        AxMapControl1.Extent = AxMapControl1.FullExtent
    End Sub

    Private Sub cmdLoadData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadData.Click

        OpenFileDialog1.Title = "Browse Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Validate and load map document
        If AxMapControl1.CheckMxFile(sFilePath) Then
            AxMapControl1.LoadMxFile(sFilePath)
            'Enabled MapControl
            AxMapControl1.Enabled = True
        Else
            MsgBox(sFilePath & " is not a valid ArcMap document")
            Exit Sub
        End If

        'Add the layer names to combo
        cboDataLayer.Items.Clear()
        Dim i As Short
        For i = 0 To AxMapControl1.LayerCount - 1
            cboDataLayer.Items.Insert(i, AxMapControl1.get_Layer(i).Name)
        Next i

        'Select first layer in control
        cboDataLayer.SelectedIndex = 0
        'Enable controls if disabled
        If chkShowTips.Enabled = False Then chkShowTips.Enabled = True
        If chkTransparent.Enabled = False Then chkTransparent.Enabled = True
        If cboDataLayer.Enabled = False Then cboDataLayer.Enabled = True
        If cboDataField.Enabled = False Then cboDataField.Enabled = True

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Disable controls
        chkShowTips.Enabled = False
        chkTransparent.Enabled = False
        cboDataLayer.Enabled = False
        cboDataField.Enabled = False
    End Sub

    Private Sub ShowLayerTips()

        Dim i As Integer
        Dim pLayer As ILayer

        'Loop through the maps layers
        For i = 0 To AxMapControl1.LayerCount - 1
            'Get ILayer interface
            pLayer = AxMapControl1.get_Layer(i)
            'If is the layer selected in the control
            If cboDataLayer.SelectedIndex = i Then
                'If want to show map tips
                If chkShowTips.CheckState = 1 Then
                    pLayer.ShowTips = True
                Else
                    pLayer.ShowTips = False
                End If
            Else
                pLayer.ShowTips = False
            End If
        Next i
    End Sub

    Private Overloads Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        'If left mouse button zoom in
        If e.button = 1 Then
            AxMapControl1.Extent = AxMapControl1.TrackRectangle
            'If right mouse button pan
        ElseIf e.button = 2 Then
            AxMapControl1.Pan()
        End If
    End Sub

    Private Sub chkShowTips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowTips.CheckedChanged
        If chkShowTips.CheckState = CheckState.Checked Then
            AxMapControl1.ShowMapTips = True
        Else
            AxMapControl1.ShowMapTips = False
        End If
    End Sub

    Private Sub chkTransparent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTransparent.CheckedChanged
        If chkTransparent.CheckState = CheckState.Checked Then
            AxMapControl1.TipStyle = esriTipStyle.esriTipStyleTransparent
        Else
            AxMapControl1.TipStyle = esriTipStyle.esriTipStyleSolid
        End If
    End Sub
End Class