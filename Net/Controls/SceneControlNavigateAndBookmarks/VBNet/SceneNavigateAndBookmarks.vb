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
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Analyst3D
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
    Public WithEvents txtNewBookmarkName As System.Windows.Forms.TextBox
    Public WithEvents cmdCaptureBookmark As System.Windows.Forms.Button
    Public WithEvents cmdBrowse As System.Windows.Forms.Button
    Public WithEvents txtFileName As System.Windows.Forms.TextBox
    Public WithEvents chkRotate As System.Windows.Forms.CheckBox
    Public WithEvents chkNavigate As System.Windows.Forms.CheckBox
    Public WithEvents lstBookmarks As System.Windows.Forms.ListBox
    Public WithEvents cmdLoad As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Line3 As System.Windows.Forms.Label
    Public WithEvents Line2 As System.Windows.Forms.Label
    Public WithEvents Line1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxSceneControl1 As ESRI.ArcGIS.Controls.AxSceneControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.txtNewBookmarkName = New System.Windows.Forms.TextBox
        Me.cmdCaptureBookmark = New System.Windows.Forms.Button
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.chkRotate = New System.Windows.Forms.CheckBox
        Me.chkNavigate = New System.Windows.Forms.CheckBox
        Me.lstBookmarks = New System.Windows.Forms.ListBox
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Line3 = New System.Windows.Forms.Label
        Me.Line2 = New System.Windows.Forms.Label
        Me.Line1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxSceneControl1 = New ESRI.ArcGIS.Controls.AxSceneControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxSceneControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNewBookmarkName
        '
        Me.txtNewBookmarkName.AcceptsReturn = True
        Me.txtNewBookmarkName.AutoSize = False
        Me.txtNewBookmarkName.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewBookmarkName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewBookmarkName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewBookmarkName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewBookmarkName.Location = New System.Drawing.Point(152, 440)
        Me.txtNewBookmarkName.MaxLength = 0
        Me.txtNewBookmarkName.Name = "txtNewBookmarkName"
        Me.txtNewBookmarkName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewBookmarkName.Size = New System.Drawing.Size(121, 27)
        Me.txtNewBookmarkName.TabIndex = 13
        Me.txtNewBookmarkName.Text = "New Bookmark"
        '
        'cmdCaptureBookmark
        '
        Me.cmdCaptureBookmark.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCaptureBookmark.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCaptureBookmark.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCaptureBookmark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCaptureBookmark.Location = New System.Drawing.Point(8, 440)
        Me.cmdCaptureBookmark.Name = "cmdCaptureBookmark"
        Me.cmdCaptureBookmark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCaptureBookmark.Size = New System.Drawing.Size(137, 25)
        Me.cmdCaptureBookmark.TabIndex = 12
        Me.cmdCaptureBookmark.Text = "Capture Bookmark"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBrowse.Location = New System.Drawing.Point(528, 280)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBrowse.Size = New System.Drawing.Size(65, 25)
        Me.cmdBrowse.TabIndex = 7
        Me.cmdBrowse.Text = "Browse..."
        '
        'txtFileName
        '
        Me.txtFileName.AcceptsReturn = True
        Me.txtFileName.AutoSize = False
        Me.txtFileName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFileName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFileName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFileName.Location = New System.Drawing.Point(88, 280)
        Me.txtFileName.MaxLength = 0
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFileName.Size = New System.Drawing.Size(433, 25)
        Me.txtFileName.TabIndex = 6
        Me.txtFileName.Text = "Enter a path to a scene document to load into the SceneControl"
        '
        'chkRotate
        '
        Me.chkRotate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRotate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRotate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRotate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRotate.Location = New System.Drawing.Point(304, 416)
        Me.chkRotate.Name = "chkRotate"
        Me.chkRotate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRotate.Size = New System.Drawing.Size(121, 25)
        Me.chkRotate.TabIndex = 4
        Me.chkRotate.Text = "Rotate Gesture"
        '
        'chkNavigate
        '
        Me.chkNavigate.BackColor = System.Drawing.SystemColors.Control
        Me.chkNavigate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkNavigate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNavigate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkNavigate.Location = New System.Drawing.Point(304, 336)
        Me.chkNavigate.Name = "chkNavigate"
        Me.chkNavigate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkNavigate.Size = New System.Drawing.Size(113, 25)
        Me.chkNavigate.TabIndex = 3
        Me.chkNavigate.Text = "Navigate Mode"
        '
        'lstBookmarks
        '
        Me.lstBookmarks.BackColor = System.Drawing.SystemColors.Window
        Me.lstBookmarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstBookmarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBookmarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstBookmarks.ItemHeight = 14
        Me.lstBookmarks.Location = New System.Drawing.Point(8, 352)
        Me.lstBookmarks.Name = "lstBookmarks"
        Me.lstBookmarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstBookmarks.Size = New System.Drawing.Size(265, 74)
        Me.lstBookmarks.TabIndex = 2
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoad.Location = New System.Drawing.Point(8, 280)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoad.Size = New System.Drawing.Size(73, 25)
        Me.cmdLoad.TabIndex = 1
        Me.cmdLoad.Text = "Load"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(432, 400)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(169, 65)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Hold down left mouse button, move mouse left (or right) and keep mouse moving whi" & _
        "le releasing the left button. Press ESC to stop rotation."
        '
        'Line3
        '
        Me.Line3.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line3.Location = New System.Drawing.Point(8, 312)
        Me.Line3.Name = "Line3"
        Me.Line3.Size = New System.Drawing.Size(584, 1)
        Me.Line3.TabIndex = 14
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line2.Location = New System.Drawing.Point(288, 392)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(304, 1)
        Me.Line2.TabIndex = 15
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line1.Location = New System.Drawing.Point(288, 312)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(1, 152)
        Me.Line1.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(432, 344)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(169, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Middle mouse to pan"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(432, 360)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(169, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Right mouse to zoom in and out"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(432, 328)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(153, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Left mouse to rotate"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 328)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(265, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Bookmarks: Click on name below"
        '
        'AxSceneControl1
        '
        Me.AxSceneControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxSceneControl1.Name = "AxSceneControl1"
        Me.AxSceneControl1.OcxState = CType(resources.GetObject("AxSceneControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSceneControl1.Size = New System.Drawing.Size(592, 264)
        Me.AxSceneControl1.TabIndex = 17
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(384, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 18
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(606, 476)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxSceneControl1)
        Me.Controls.Add(Me.txtNewBookmarkName)
        Me.Controls.Add(Me.cmdCaptureBookmark)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.chkRotate)
        Me.Controls.Add(Me.chkNavigate)
        Me.Controls.Add(Me.lstBookmarks)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Line3)
        Me.Controls.Add(Me.Line2)
        Me.Controls.Add(Me.Line1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Form1"
        CType(Me.AxSceneControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    'Hold an array of the Scene Bookmarks
    Dim m_pBookmarks As IArray

    Private Sub cmdBrowse_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBrowse.Click

        OpenFileDialog1.Title = "Scene Documents"
        OpenFileDialog1.DefaultExt = ".sxd"
        OpenFileDialog1.Filter = "Scene Documents (*.sxd)|*.sxd|Scene Templates (*.sxt)|*.sxt"
        OpenFileDialog1.ShowDialog()
        txtFileName.Text = OpenFileDialog1.FileName

        'Try and load the filename
        cmdLoad_Click(cmdLoad, New System.EventArgs)

    End Sub

    Private Sub cmdCaptureBookmark_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCaptureBookmark.Click

        Dim pBookmark3d As IBookmark3D
        pBookmark3d = New Bookmark3DClass

        pBookmark3d.Name = txtNewBookmarkName.Text
        pBookmark3d.Capture(AxSceneControl1.Camera)
        Dim pBookmarks As ISceneBookmarks
        pBookmarks = AxSceneControl1.Scene
        pBookmarks.AddBookmark(pBookmark3d)

        UpdateBookmarks()

    End Sub

    Private Sub cmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoad.Click
        On Error GoTo errorHandler

        AxSceneControl1.LoadSxFile(txtFileName.Text)
        UpdateBookmarks()

        Exit Sub
errorHandler:
        MsgBox("Error occurred trying to load Scene Document: " & vbCrLf & Err.Description, MsgBoxStyle.Exclamation)
    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        chkNavigate.CheckState = AxSceneControl1.Navigate

    End Sub

    Private Sub lstBookmarks_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstBookmarks.SelectedIndexChanged

        'Get a bookmark corresponding to list and apply it to the SceneViewer
        Dim pBookmark As IBookmark3D
        pBookmark = m_pBookmarks.Element(lstBookmarks.SelectedIndex)

        'Switch to new bookmark location
        pBookmark.Apply(AxSceneControl1.SceneViewer, False, 0)

    End Sub

    Private Sub UpdateBookmarks()

        'Get bookmarks from Scene
        Dim pBookmarks As ISceneBookmarks
        pBookmarks = AxSceneControl1.Scene

        m_pBookmarks = Nothing
        m_pBookmarks = pBookmarks.Bookmarks
        lstBookmarks.Items.Clear()
        Dim haveBookmarks As Boolean
        haveBookmarks = False

        Dim pBookmark3d As IBookmark3D
        Dim i As Integer
        If (Not m_pBookmarks Is Nothing) Then
            'Add the bookmark names to the listbox in the same order as they are in the Scene Document
            For i = 0 To m_pBookmarks.Count - 1
                pBookmark3d = m_pBookmarks.Element(i)
                lstBookmarks.Items.Add(pBookmark3d.Name)
            Next
            haveBookmarks = m_pBookmarks.Count <> 0
            lstBookmarks.Enabled = True
        End If

        If (Not haveBookmarks) Then
            'No bookmarks available
            lstBookmarks.Items.Add("<No Bookmarks Available>")
            lstBookmarks.Enabled = False
        End If

    End Sub

    Private Sub chkNavigate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNavigate.Click

        'Enable navigation mode
        AxSceneControl1.Navigate = chkNavigate.CheckState

    End Sub

    Private Sub chkRotate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRotate.Click

        'Enable rotate gesture if checked
        AxSceneControl1.SceneViewer.GestureEnabled = chkRotate.CheckState

    End Sub

End Class