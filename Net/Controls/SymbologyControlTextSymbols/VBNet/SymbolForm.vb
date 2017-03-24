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
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS
Public Class Form2
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form2))
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(265, 265)
        Me.AxSymbologyControl1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Button1.Location = New System.Drawing.Point(120, 284)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "OK"
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Button2.Location = New System.Drawing.Point(200, 284)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancel"
        '
        'Form2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(282, 312)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form2"
        Me.Text = "Symbol Form"
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private m_styleGalleryItem As IStyleGalleryItem

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Get the selected item
        m_styleGalleryItem = AxSymbologyControl1.GetStyleClass(AxSymbologyControl1.StyleClass).GetSelectedItem()
        'Hide the form
        Me.Hide()

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Get the ArcGIS install location
        Dim sInstall As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Hide the form
        Me.Hide()
    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected
        'Enable ok button
        Button1.Enabled = True
    End Sub

    Public Function GetItem(ByVal styleClass As ESRI.ArcGIS.Controls.esriSymbologyStyleClass) As IStyleGalleryItem

        m_styleGalleryItem = Nothing

        'Disable ok button
        Button1.Enabled = False

        'Set the style class
        AxSymbologyControl1.StyleClass = styleClass
        'Unselect any selected item in the current style class
        AxSymbologyControl1.GetStyleClass(styleClass).UnselectItem()

        'Show the modal form
        Me.ShowDialog()

        'Return the label style that has been selected from the SymbologyControl
        Return m_styleGalleryItem

    End Function
  
End Class
