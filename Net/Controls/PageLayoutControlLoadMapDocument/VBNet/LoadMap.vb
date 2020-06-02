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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS


Public Class frmMain
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
        Application.Run(New frmMain())
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
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents cmdLoadMap As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.cmdLoadMap = New System.Windows.Forms.Button
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPath
        '
        Me.txtPath.Enabled = False
        Me.txtPath.Location = New System.Drawing.Point(8, 48)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(304, 20)
        Me.txtPath.TabIndex = 0
        '
        'cmdLoadMap
        '
        Me.cmdLoadMap.Location = New System.Drawing.Point(184, 8)
        Me.cmdLoadMap.Name = "cmdLoadMap"
        Me.cmdLoadMap.Size = New System.Drawing.Size(128, 32)
        Me.cmdLoadMap.TabIndex = 1
        Me.cmdLoadMap.Text = "Load Map Document"
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(8, 72)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(304, 304)
        Me.AxPageLayoutControl1.TabIndex = 2
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(8, 12)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 3
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(319, 382)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.cmdLoadMap)
        Me.Controls.Add(Me.txtPath)
        Me.Name = "frmMain"
        Me.Text = "Load Map Document"
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub cmdLoadMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadMap.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Browse Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd, *.mxt, *.pmf)|*.pmf; *.mxt; *.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String = OpenFileDialog1.FileName
        If sFilePath = "" Then
            Return
        End If

        Dim bPass As Boolean, bIsMapDoc As Boolean
        bPass = False
        bIsMapDoc = False
        Dim ipMapDoc As IMapDocument
        ipMapDoc = New MapDocumentClass

        'Check if the map document is password protected
        bPass = ipMapDoc.IsPasswordProtected(sFilePath)

        If (bPass) Then
            'Disable the main form
            Me.Enabled = False

            'Show the password dialog
            Dim Form2 As frmPassword = New frmPassword
            Form2.ShowDialog(Me)
            Dim check As Integer = Form2.Check

            'OK button pressed
            If (check = 1) Then
                Try
                    'Set a waiting cursor
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                    'Load the password protected map
                    AxPageLayoutControl1.LoadMxFile(sFilePath, Form2.Password)
                    txtPath.Text = sFilePath
                    Me.Enabled = True

                    'Set a default cursor
                    System.Windows.Forms.Cursor.Current = Cursors.Default
                Catch
                    Me.Enabled = True
                    MessageBox.Show("The Password was incorrect!")
                End Try
            Else
                Me.Enabled = True
            End If
        Else
            'Check whether the file is a map document
            bIsMapDoc = AxPageLayoutControl1.CheckMxFile(sFilePath)

            If (bIsMapDoc) Then
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                'Load the Mx document	
                AxPageLayoutControl1.LoadMxFile(sFilePath, Type.Missing)
                txtPath.Text = sFilePath
                'Set a default cursor
                System.Windows.Forms.Cursor.Current = Cursors.Default
            Else

                MessageBox.Show(sFilePath + " is not a valid ArcMap document")
                sFilePath = ""
            End If
        End If
    End Sub

End Class
