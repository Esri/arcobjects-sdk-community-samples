Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display

Public Class frmSymbol


    Inherits System.Windows.Forms.Form
    Private m_styleGalleryItem As IStyleGalleryItem
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Change cursor
        Me.Cursor = Cursors.WaitCursor
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
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSymbol))
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(7, 7)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(320, 308)
        Me.AxSymbologyControl1.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(333, 263)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(333, 291)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 24)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'frmSymbol
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 322)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSymbol"
        Me.Text = "SymbolForm"
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub SymbolForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Get the ArcGIS install location
        Dim sInstall As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path

        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle")

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_styleGalleryItem = Nothing
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Hide()
    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected
        'Get the selected item
        m_styleGalleryItem = e.styleGalleryItem
    End Sub

    Public Function GetItem(ByRef styleClass As esriSymbologyStyleClass) As IStyleGalleryItem

        m_styleGalleryItem = Nothing

        'Get and set the style class
        AxSymbologyControl1.StyleClass = styleClass

        'Change cursor
        Me.Cursor = Cursors.Default

        'Show the modal form
        Me.ShowDialog()

        Return m_styleGalleryItem
    End Function

End Class
