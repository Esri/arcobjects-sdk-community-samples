Imports System.Drawing
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports TabbedInspectorVB2005.TabbedFeatureInspector

Namespace TabbedInspectorEngineApplication
  Partial Public Class EngineApplication
    Inherits Form
    Implements IApplicationServices

    Private message As String
        Private err As Boolean
        <STAThread()> _
        Shared Sub Main()
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
            Application.Run(New EngineApplication())
        End Sub
    Public Sub New()
      InitializeComponent()

      ' Store the application services interface instance in the toolbar's custom property, so
      ' commands can get to it to access the TOC and update the status message.
      AxToolbarControl1.CustomProperty = Me
    End Sub

    Public Sub work()
      status.Text = message
      status.BackColor = IIf(err, Color.Red, SystemColors.ButtonFace)
      status.ForeColor = IIf(err, Color.White, SystemColors.ControlText)
    End Sub

#Region "IApplicationServices implementation"

    Public Sub SetStatusMessage(ByVal msg As String, ByVal fail As Boolean) Implements IApplicationServices.SetStatusMessage
      message = msg
      err = fail
      Dim UpdateStatus As MethodInvoker = AddressOf work
      If (status.InvokeRequired) Then
        status.Invoke(UpdateStatus)
      Else
        UpdateStatus()
      End If
    End Sub

    Public Function GetLayerSelectedInTOC() As IFeatureLayer Implements IApplicationServices.GetLayerSelectedInTOC
      Dim itemType As esriTOCControlItem = esriTOCControlItem.esriTOCControlItemNone
      Dim unkIgnore As Object = Nothing
      Dim selectedLayer As ILayer = Nothing
      Dim dataIgnore As Object = Nothing
      Dim mapIgnore As IBasicMap = Nothing

      AxTOCControl1.GetSelectedItem(itemType, mapIgnore, selectedLayer, unkIgnore, dataIgnore)

      GetLayerSelectedInTOC = selectedLayer
    End Function
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents status As System.Windows.Forms.TextBox

#End Region

    Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EngineApplication))
            Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
            Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
            Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
            Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
            Me.status = New System.Windows.Forms.TextBox
            CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'AxToolbarControl1
            '
            Me.AxToolbarControl1.Location = New System.Drawing.Point(-1, -1)
            Me.AxToolbarControl1.MaximumSize = New System.Drawing.Size(1000, 1000)
            Me.AxToolbarControl1.Name = "AxToolbarControl1"
            Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.AxToolbarControl1.Size = New System.Drawing.Size(700, 28)
            Me.AxToolbarControl1.TabIndex = 0
            '
            'AxTOCControl1
            '
            Me.AxTOCControl1.Location = New System.Drawing.Point(-1, 33)
            Me.AxTOCControl1.Name = "AxTOCControl1"
            Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.AxTOCControl1.Size = New System.Drawing.Size(265, 394)
            Me.AxTOCControl1.TabIndex = 1
            '
            'AxMapControl1
            '
            Me.AxMapControl1.Location = New System.Drawing.Point(270, 33)
            Me.AxMapControl1.MaximumSize = New System.Drawing.Size(1000, 1000)
            Me.AxMapControl1.Name = "AxMapControl1"
            Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.AxMapControl1.Size = New System.Drawing.Size(429, 394)
            Me.AxMapControl1.TabIndex = 2
            '
            'AxLicenseControl1
            '
            Me.AxLicenseControl1.Enabled = True
            Me.AxLicenseControl1.Location = New System.Drawing.Point(105, 303)
            Me.AxLicenseControl1.Name = "AxLicenseControl1"
            Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
            Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
            Me.AxLicenseControl1.TabIndex = 3
            '
            'status
            '
            Me.status.Location = New System.Drawing.Point(-1, 433)
            Me.status.MaximumSize = New System.Drawing.Size(1000, 1000)
            Me.status.Name = "status"
            Me.status.Size = New System.Drawing.Size(700, 20)
            Me.status.TabIndex = 4
            '
            'EngineApplication
            '
            Me.ClientSize = New System.Drawing.Size(702, 455)
            Me.Controls.Add(Me.status)
            Me.Controls.Add(Me.AxLicenseControl1)
            Me.Controls.Add(Me.AxMapControl1)
            Me.Controls.Add(Me.AxTOCControl1)
            Me.Controls.Add(Me.AxToolbarControl1)
            Me.Name = "EngineApplication"
            Me.Text = "Engine Application"
            CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
  End Class
End Namespace
