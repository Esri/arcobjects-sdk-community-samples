'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS

Public Class CADViewForm
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

        Application.Run(New CADViewForm())
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
    Public WithEvents CmdFullExtent As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CADViewForm))
        Me.CmdFullExtent = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdFullExtent
        '
        Me.CmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.CmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdFullExtent.Location = New System.Drawing.Point(8, 352)
        Me.CmdFullExtent.Name = "CmdFullExtent"
        Me.CmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdFullExtent.Size = New System.Drawing.Size(97, 33)
        Me.CmdFullExtent.TabIndex = 0
        Me.CmdFullExtent.Text = "Full Extent"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(112, 368)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(401, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Right mouse button to pan."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(112, 352)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(401, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Left mouse button to drag a rectangle to zoom in."
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(544, 336)
        Me.AxMapControl1.TabIndex = 4
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(344, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 5
        '
        'CADViewForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(564, 393)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.CmdFullExtent)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "CADViewForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "MapControl CAD Viewer"
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub CmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdFullExtent.Click

        'Get the MapContol's full extent and set the current extent to this
        Dim pEnv As IEnvelope
        pEnv = AxMapControl1.FullExtent
        AxMapControl1.Extent = pEnv

    End Sub

    Private Sub CADViewForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Set passed file to variable
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.Length = 1 Then
            MessageBox.Show("No filename passed", "CAD Fileviewer")
            Me.Close()
            Exit Sub
        End If

        Dim strWorkspacePath As String, strFileName As String
        strWorkspacePath = System.IO.Path.GetDirectoryName(arguments(1))
        strFileName = System.IO.Path.GetFileName(arguments(1))

        'Add passed file to MapControl
        Dim pCadDrawingDataset As ICadDrawingDataset
        pCadDrawingDataset = GetCadDataset(strWorkspacePath, strFileName)
        If pCadDrawingDataset Is Nothing Then Exit Sub
        Dim pCadLayer As ICadLayer
        pCadLayer = New CadLayerClass
        pCadLayer.CadDrawingDataset = pCadDrawingDataset
        pCadLayer.Name = strFileName
        AxMapControl1.AddLayer(pCadLayer)

    End Sub

    Private Function GetCadDataset(ByRef strCadWorkspacePath As String, ByRef strCadFileName As String) As ICadDrawingDataset

        Dim pName As IName
        Dim pCadDatasetName As IDatasetName
        Dim pWorkspaceName As IWorkspaceName

        'Create a WorkspaceName object
        pWorkspaceName = New WorkspaceNameClass
        pWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory"
        pWorkspaceName.PathName = strCadWorkspacePath

        'Create a CadDrawingName object
        pCadDatasetName = New CadDrawingNameClass
        pCadDatasetName.Name = strCadFileName
        pCadDatasetName.WorkspaceName = pWorkspaceName

        'Open the CAD drawing
        pName = pCadDatasetName 'QI
        GetCadDataset = pName.Open

    End Function

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        ' Check which button has been pressed by the user
        Dim pEnv As IEnvelope
        If e.button = 1 Then
            ' Left button - Track a Rectangle and use this to set the MapControl's extent
            pEnv = AxMapControl1.TrackRectangle
            AxMapControl1.Extent = pEnv

        Else
            'Left or middle button - Pan
            AxMapControl1.Pan()
        End If
    End Sub
End Class