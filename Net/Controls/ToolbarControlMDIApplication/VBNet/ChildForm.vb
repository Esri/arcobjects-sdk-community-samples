Option Explicit On 
Imports ESRI.ArcGIS.Controls

Public Class ChildForm
    Inherits System.Windows.Forms.Form

    Private Const WM_ENTERSIZEMOVE As Integer = &H231   'For resizing
    Private Const WM_EXITSIZEMOVE As Integer = &H232    'For resizing

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ChildForm))
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxMapControl1.Location = New System.Drawing.Point(0, 0)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(384, 280)
        Me.AxMapControl1.TabIndex = 0
        '
        'ChildForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(384, 278)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Name = "ChildForm"
        Me.Text = "MDIChild"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ChildForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Suppress drawing while resizing
        Me.SetStyle(ControlStyles.EnableNotifyMessage, True)

        AxMapControl1.BorderStyle = esriControlsBorderStyle.esriNoBorder
    End Sub

    Protected Overrides Sub OnNotifyMessage(ByVal m As System.Windows.Forms.Message)
        'This method of suppressing resize drawing works by examining the windows messages 
        'sent to the form. When a form starts resizing, windows sends the WM_ENTERSIZEMOVE 
        'Windows(message). At this point we suppress drawing to the MapControl and 
        'PageLayoutControl and draw using a "stretchy bitmap". When windows sends the 
        'WM_EXITSIZEMOVE the form is released from resizing and we resume with a full 
        'redraw at the new extent.

        'Note in DotNet forms we can not simply use the second parameter in a Form_Load 
        'event to automatically detect when a form is resized as follows:
        'AxPageLayoutControl1.SuppressResizeDrawing(False, Me.Handle.ToInt32)
        'This results in a System.NullException when the form closes (after layers have been 
        'loaded). This is a limitation caused by .Net's particular implementation of its 
        'windows message pump which conflicts with "windows subclassing" used to watch the
        'forms window.

        If (m.Msg = WM_ENTERSIZEMOVE) Then
            AxMapControl1.SuppressResizeDrawing(True, 0)
        ElseIf (m.Msg = WM_EXITSIZEMOVE) Then
            AxMapControl1.SuppressResizeDrawing(False, 0)
        End If

    End Sub

    Private Sub AxMapControl1_OnMapReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent) Handles AxMapControl1.OnMapReplaced
        'Set the forms text
        Me.Text = "MDIChild (" + AxMapControl1.DocumentFilename + ")"
    End Sub

End Class
