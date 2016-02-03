Imports ESRI.ArcGIS.Controls
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
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents chkOrientation As System.Windows.Forms.CheckBox
    Friend WithEvents AxGlobeControl1 As ESRI.ArcGIS.Controls.AxGlobeControl
    Friend WithEvents chkHiddenItems As System.Windows.Forms.CheckBox
    Friend WithEvents btnBackColor As System.Windows.Forms.Button
    Friend WithEvents btnFadeColor As System.Windows.Forms.Button
    Friend WithEvents chkFillDirection As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.chkOrientation = New System.Windows.Forms.CheckBox
        Me.AxGlobeControl1 = New ESRI.ArcGIS.Controls.AxGlobeControl
        Me.chkHiddenItems = New System.Windows.Forms.CheckBox
        Me.btnBackColor = New System.Windows.Forms.Button
        Me.btnFadeColor = New System.Windows.Forms.Button
        Me.chkFillDirection = New System.Windows.Forms.CheckBox
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(450, 28)
        Me.AxToolbarControl1.TabIndex = 1
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(365, 338)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 2
        '
        'chkOrientation
        '
        Me.chkOrientation.Location = New System.Drawing.Point(376, 40)
        Me.chkOrientation.Name = "chkOrientation"
        Me.chkOrientation.Size = New System.Drawing.Size(152, 16)
        Me.chkOrientation.TabIndex = 3
        Me.chkOrientation.Text = "Vertical Orientation"
        '
        'AxGlobeControl1
        '
        Me.AxGlobeControl1.Location = New System.Drawing.Point(40, 40)
        Me.AxGlobeControl1.Name = "AxGlobeControl1"
        Me.AxGlobeControl1.OcxState = CType(resources.GetObject("AxGlobeControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGlobeControl1.Size = New System.Drawing.Size(319, 330)
        Me.AxGlobeControl1.TabIndex = 4
        '
        'chkHiddenItems
        '
        Me.chkHiddenItems.Location = New System.Drawing.Point(376, 64)
        Me.chkHiddenItems.Name = "chkHiddenItems"
        Me.chkHiddenItems.Size = New System.Drawing.Size(152, 16)
        Me.chkHiddenItems.TabIndex = 5
        Me.chkHiddenItems.Text = "Show Hidden Items"
        '
        'btnBackColor
        '
        Me.btnBackColor.Location = New System.Drawing.Point(376, 96)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(112, 23)
        Me.btnBackColor.TabIndex = 6
        Me.btnBackColor.Text = "Back Color"
        '
        'btnFadeColor
        '
        Me.btnFadeColor.Location = New System.Drawing.Point(376, 128)
        Me.btnFadeColor.Name = "btnFadeColor"
        Me.btnFadeColor.Size = New System.Drawing.Size(112, 23)
        Me.btnFadeColor.TabIndex = 7
        Me.btnFadeColor.Text = "Fade Color"
        '
        'chkFillDirection
        '
        Me.chkFillDirection.Location = New System.Drawing.Point(376, 160)
        Me.chkFillDirection.Name = "chkFillDirection"
        Me.chkFillDirection.Size = New System.Drawing.Size(144, 24)
        Me.chkFillDirection.TabIndex = 8
        Me.chkFillDirection.Text = "Vertical Fill Direction"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 382)
        Me.Controls.Add(Me.chkFillDirection)
        Me.Controls.Add(Me.btnFadeColor)
        Me.Controls.Add(Me.btnBackColor)
        Me.Controls.Add(Me.chkHiddenItems)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxGlobeControl1)
        Me.Controls.Add(Me.chkOrientation)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Name = "Form1"
        Me.Text = "ToolbarControl Appearance"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxGlobeControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(AxGlobeControl1)

        'Add new items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsGlobeOpenDocCommand", -1, 0, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddToolbarDef("esriControls.ControlsGlobeGlobeToolbar", -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddToolbarDef("esriControls.ControlsGlobeRotateToolbar", -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

    End Sub

    Private Sub chkOrientation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOrientation.CheckedChanged
        If chkOrientation.Checked = True Then
            AxToolbarControl1.Orientation = esriToolbarOrientation.esriToolbarOrientationVertical
        Else
            AxToolbarControl1.Orientation = esriToolbarOrientation.esriToolbarOrientationHorizontal
        End If
    End Sub

    Private Sub chkHiddenItems_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHiddenItems.CheckedChanged
        If chkHiddenItems.Checked = True Then
            AxToolbarControl1.ShowHiddenItems = True
        Else
            AxToolbarControl1.ShowHiddenItems = False
        End If
    End Sub

    Private Sub btnBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackColor.Click

        'Create new ColorDialog control
        Dim colorDialog As New ColorDialog
        'Show the ColorDialog and exit if user cancelled
        If colorDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'Get color from ColorDialog
        Dim color As System.Drawing.Color
        color = colorDialog.Color

        'The ToolbarControl host wrapper expects a SystemDrawingColor. The 
        'ToolbarControl type library wrapper expects an OLE_Color. The OLE_Color
        'is made up as follows:(Red) + (Green * 256) + (Blue * 256 * 256)
        'Dim toolbarControl As IToolbarControl2
        'toolbarControl = AxToolbarControl1.Object
        'toolbarControl.BackColor = (color.R + (color.G * 256) + (color.B * 256 * 256))
        AxToolbarControl1.BackColor = color

        colorDialog = Nothing

    End Sub

    Private Sub btnFadeColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFadeColor.Click

        'Create new ColorDialog control
        Dim colorDialog As New ColorDialog
        'Show the ColorDialog and exit if user cancelled
        If colorDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'Get color from ColorDialog
        Dim color As System.Drawing.Color
        color = colorDialog.Color

        'The ToolbarControl host wrapper expects a SystemDrawingColor. The 
        'ToolbarControl type library wrapper expects an OLE_Color. The OLE_Color
        'is made up as follows:(Red) + (Green * 256) + (Blue * 256 * 256)
        'Dim toolbarControl As IToolbarControl2
        'toolbarControl = AxToolbarControl1.Object
        'toolbarControl.FadeColor = (color.R + (color.G * 256) + (color.B * 256 * 256))
        AxToolbarControl1.FadeColor = color

        colorDialog = Nothing
    End Sub

    Private Sub chkFillDirection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFillDirection.CheckedChanged
        If chkFillDirection.Checked = True Then
            AxToolbarControl1.FillDirection = esriToolbarFillDirection.esriToolbarFillVertical
        Else
            AxToolbarControl1.FillDirection = esriToolbarFillDirection.esriToolbarFillHorizontal
        End If
    End Sub
End Class
