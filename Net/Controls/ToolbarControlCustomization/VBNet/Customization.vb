Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
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
    Public WithEvents chkCustomization As System.Windows.Forms.CheckBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.chkCustomization = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkCustomization
        '
        Me.chkCustomization.BackColor = System.Drawing.SystemColors.Control
        Me.chkCustomization.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCustomization.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomization.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCustomization.Location = New System.Drawing.Point(408, 72)
        Me.chkCustomization.Name = "chkCustomization"
        Me.chkCustomization.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCustomization.Size = New System.Drawing.Size(121, 17)
        Me.chkCustomization.TabIndex = 2
        Me.chkCustomization.Text = "Customize"
        Me.chkCustomization.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(408, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(185, 73)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "To delete an item, either select it with the left mouse button and drag it off th" & _
            "e ToolbarControl or select it with the right mouse button and choose delete from" & _
            " the customize menu."
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(408, 248)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(185, 57)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "To change the group, group spacing or style of an item, select it with the right " & _
            "mouse button to display the customize menu."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(408, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(185, 57)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To move an item, select it with the left mouse button and drag and drop it to the" & _
            " location indicated by the black vertical bar. "
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(584, 28)
        Me.AxToolbarControl1.TabIndex = 6
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(392, 416)
        Me.AxMapControl1.TabIndex = 7
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(559, 42)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(408, 315)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(185, 93)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "To add additional command, menu  and palette items either double click on them wi" & _
            "thin the customize dialog or drag and drop them from the customize dialog onto t" & _
            "he ToolbarControl."
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(603, 462)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.chkCustomization)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Customization"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

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

    Private m_pCustomizeDialog As ICustomizeDialog
    Private WithEvents m_pCustomizeDialogEvents As CustomizeDialog

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Set the Buddy
        AxToolbarControl1.SetBuddyControl(AxMapControl1)

        'Create UID's and add new items to the ToolBarControl
        Dim pUid As New ESRI.ArcGIS.esriSystem.UID
        pUid.Value = "esriControls.ControlsOpenDocCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        pUid.Value = "esriControls.ControlsMapZoomInTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapZoomOutTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapPanTool"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapFullExtentCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconAndText)
        pUid.Value = "esriControls.ControlsMapZoomToLastExtentBackCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, True, 20, esriCommandStyles.esriCommandStyleTextOnly)
        pUid.Value = "esriControls.ControlsMapZoomToLastExtentForwardCommand"
        AxToolbarControl1.AddItem(pUid, -1, -1, False, 0, esriCommandStyles.esriCommandStyleTextOnly)

        'Create a new customize dialog
        m_pCustomizeDialog = New CustomizeDialogClass
        m_pCustomizeDialogEvents = m_pCustomizeDialog
        m_pCustomizeDialog.SetDoubleClickDestination(AxToolbarControl1)
        chkCustomization.CheckState = CheckState.Unchecked
    End Sub

    Private Sub chkCustomization_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCustomization.Click
        'Show or hide the customize dialog
        If chkCustomization.Checked = False Then
            m_pCustomizeDialog.CloseDialog()
        Else
            m_pCustomizeDialog.StartDialog(AxMapControl1.hWnd)
        End If
    End Sub

    Private Sub m_pCustomizeDialogEvents_OnCloseDialog() Handles m_pCustomizeDialogEvents.OnCloseDialog
        'Take the ToolbarControl out of customize mode
        AxToolbarControl1.Customize = False
        chkCustomization.Checked = False
    End Sub

    Private Sub m_pCustomizeDialogEvents_OnStartDialog() Handles m_pCustomizeDialogEvents.OnStartDialog
        'Put the ToolbarControl into customize mode
        AxToolbarControl1.Customize = True
    End Sub
End Class