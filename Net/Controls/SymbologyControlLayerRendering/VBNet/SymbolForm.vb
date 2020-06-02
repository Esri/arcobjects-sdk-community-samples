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
Option Strict Off
Option Explicit On 
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Controls
Friend Class frmSymbol
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSymbol))
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(312, 216)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(96, 24)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(312, 248)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(96, 24)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(296, 265)
        Me.AxSymbologyControl1.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(312, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(104, 96)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'frmSymbol
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(418, 278)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Name = "frmSymbol"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Select Symbology..."
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Public m_styleGalleryItem As IStyleGalleryItem

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Get the ArcGIS install location
        Dim sInstall As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path

        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle")

    End Sub

    Public Function GetItem(ByRef styleClass As esriSymbologyStyleClass, ByVal symbol As ISymbol) As IStyleGalleryItem

        m_styleGalleryItem = Nothing

        'Get and set the style class
        AxSymbologyControl1.StyleClass = styleClass
        Dim symbologyStyleClass As ISymbologyStyleClass = AxSymbologyControl1.GetStyleClass(styleClass)

        'Create a new server style gallery item with its style set
        Dim styleGalleryItem As New ServerStyleGalleryItem
        styleGalleryItem.Item = symbol
        styleGalleryItem.Name = "mySymbol"

        'Add the item to the style class and select it
        symbologyStyleClass.AddItem(styleGalleryItem, 0)
        symbologyStyleClass.SelectItem(0)

        'Show the modal form
        Me.ShowDialog()

        Return m_styleGalleryItem
    End Function

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        m_styleGalleryItem = Nothing
        Me.Hide()
    End Sub

    Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Me.Hide()
    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected
        'Preview the selected item
        m_styleGalleryItem = e.styleGalleryItem
        PreviewImage()
    End Sub

    Private Sub PreviewImage()

        'Get and set the style class
        Dim symbologyStyleClass As ISymbologyStyleClass
        SymbologyStyleClass = AxSymbologyControl1.GetStyleClass(AxSymbologyControl1.StyleClass)

        'Preview an image of the symbol
        Dim picture As stdole.IPictureDisp
        picture = SymbologyStyleClass.PreviewItem(m_styleGalleryItem, PictureBox1.Width, PictureBox1.Height)
        Dim image As System.Drawing.Image
        image = System.Drawing.Image.FromHbitmap(New System.IntPtr(picture.Handle))
        PictureBox1.Image = image

    End Sub

End Class