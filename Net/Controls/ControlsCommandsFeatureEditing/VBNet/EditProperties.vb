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
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports System.Windows.Forms
Public Class EditProperties
    Inherits System.Windows.Forms.Form

    Private m_engineEditProperties As New EngineEditorClass
    Private bSketchColor As Boolean
    Private R As Integer, B As Integer, G As Integer

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
    Friend WithEvents chkStretch As System.Windows.Forms.CheckBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents txtTolerance As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtStreamCount As System.Windows.Forms.TextBox
    Friend WithEvents lblStream As System.Windows.Forms.Label
    Friend WithEvents txtPrecision As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSketchColor As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtSketchWidth As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.chkStretch = New System.Windows.Forms.CheckBox
        Me.label4 = New System.Windows.Forms.Label
        Me.txtTolerance = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.txtStreamCount = New System.Windows.Forms.TextBox
        Me.lblStream = New System.Windows.Forms.Label
        Me.txtPrecision = New System.Windows.Forms.TextBox
        Me.label8 = New System.Windows.Forms.Label
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSketchColor = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.txtSketchWidth = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkStretch
        '
        Me.chkStretch.Location = New System.Drawing.Point(112, 96)
        Me.chkStretch.Name = "chkStretch"
        Me.chkStretch.Size = New System.Drawing.Size(16, 32)
        Me.chkStretch.TabIndex = 20
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 104)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(104, 16)
        Me.label4.TabIndex = 15
        Me.label4.Text = "Stretch Geometry:"
        '
        'txtTolerance
        '
        Me.txtTolerance.Location = New System.Drawing.Point(112, 72)
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(56, 20)
        Me.txtTolerance.TabIndex = 19
        Me.txtTolerance.Text = ""
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 72)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(104, 16)
        Me.label1.TabIndex = 13
        Me.label1.Text = "Stream Tolerance:"
        '
        'txtStreamCount
        '
        Me.txtStreamCount.Location = New System.Drawing.Point(112, 40)
        Me.txtStreamCount.Name = "txtStreamCount"
        Me.txtStreamCount.Size = New System.Drawing.Size(56, 20)
        Me.txtStreamCount.TabIndex = 18
        Me.txtStreamCount.Text = ""
        '
        'lblStream
        '
        Me.lblStream.Location = New System.Drawing.Point(8, 40)
        Me.lblStream.Name = "lblStream"
        Me.lblStream.Size = New System.Drawing.Size(104, 16)
        Me.lblStream.TabIndex = 14
        Me.lblStream.Text = "Stream Count:"
        '
        'txtPrecision
        '
        Me.txtPrecision.Location = New System.Drawing.Point(112, 8)
        Me.txtPrecision.Name = "txtPrecision"
        Me.txtPrecision.Size = New System.Drawing.Size(56, 20)
        Me.txtPrecision.TabIndex = 17
        Me.txtPrecision.Text = ""
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(8, 8)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(104, 16)
        Me.label8.TabIndex = 16
        Me.label8.Text = "Report Precision:"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.btnSketchColor)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.txtSketchWidth)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Location = New System.Drawing.Point(8, 136)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(160, 96)
        Me.groupBox1.TabIndex = 12
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Sketch Symbol"
        '
        'btnSketchColor
        '
        Me.btnSketchColor.Location = New System.Drawing.Point(72, 56)
        Me.btnSketchColor.Name = "btnSketchColor"
        Me.btnSketchColor.Size = New System.Drawing.Size(64, 24)
        Me.btnSketchColor.TabIndex = 3
        Me.btnSketchColor.Text = "Pick Color"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(16, 64)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(48, 24)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Color:"
        '
        'txtSketchWidth
        '
        Me.txtSketchWidth.Location = New System.Drawing.Point(72, 24)
        Me.txtSketchWidth.Name = "txtSketchWidth"
        Me.txtSketchWidth.Size = New System.Drawing.Size(64, 20)
        Me.txtSketchWidth.TabIndex = 1
        Me.txtSketchWidth.Text = ""
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(16, 24)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(48, 24)
        Me.label2.TabIndex = 0
        Me.label2.Text = "Width:"
        '
        'EditProperties
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(176, 238)
        Me.Controls.Add(Me.chkStretch)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.txtTolerance)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtStreamCount)
        Me.Controls.Add(Me.lblStream)
        Me.Controls.Add(Me.txtPrecision)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.groupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EditProperties"
        Me.Text = "Edit Properties"
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub EditProperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Populate form with current IEngineProperties values 
        txtPrecision.Text = m_engineEditProperties.ReportPrecision.ToString()
        txtSketchWidth.Text = m_engineEditProperties.SketchSymbol.Width.ToString()
        txtStreamCount.Text = m_engineEditProperties.StreamGroupingCount.ToString()
        txtTolerance.Text = m_engineEditProperties.StreamTolerance.ToString()

        If (m_engineEditProperties.StretchGeometry) Then
            chkStretch.Checked = True
        Else
            chkStretch.Checked = False
        End If
        txtPrecision.Focus()
    End Sub

    Private Sub EditProperties_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'Update precision property
        If (txtPrecision.Text <> "") Then
            m_engineEditProperties.ReportPrecision = Convert.ToInt32(txtPrecision.Text)
        End If

        'Update stream grouping count
        If (txtStreamCount.Text <> "") Then
            m_engineEditProperties.StreamGroupingCount = Convert.ToInt32(txtStreamCount.Text)
        End If

        'Update stream tolerance
        If (txtTolerance.Text <> "") Then
            m_engineEditProperties.StreamTolerance = Convert.ToInt32(txtTolerance.Text)
        End If

        'Update stretch geometry property
        If (chkStretch.Checked) Then
            m_engineEditProperties.StretchGeometry = True
        Else
            m_engineEditProperties.StretchGeometry = False
        End If

        'Update sketch symbol property
        If (bSketchColor Or txtSketchWidth.Text <> "") Then

            Dim lineSymbol As ILineSymbol
            lineSymbol = m_engineEditProperties.SketchSymbol
            If (bSketchColor) Then
                dim color as new RgbColorClass()
                color.Red = R
                color.Blue = B
                color.Green = G
                lineSymbol.Color = color
            End If

            If (txtSketchWidth.Text <> "") Then
                lineSymbol.Width = Convert.ToInt32(txtSketchWidth.Text)
            End If

            m_engineEditProperties.SketchSymbol = lineSymbol
        End If
    End Sub

    Private Sub btnSketchColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSketchColor.Click
        'Create a new color dialog
        Dim colorDialog As New ColorDialog
        'Prevent the user from selecting a custom color
        colorDialog.AllowFullOpen = False
        'Allows the user to obtain help (default is false)
        colorDialog.ShowHelp = True

        If (colorDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            R = Convert.ToInt32(colorDialog.Color.R)
            B = Convert.ToInt32(colorDialog.Color.B)
            G = Convert.ToInt32(colorDialog.Color.G)
            bSketchColor = True
        End If
    End Sub

    Private Sub txtSketchWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSketchWidth.TextChanged
        'Validate sketch width
        Try
            If (txtSketchWidth.Text <> "") Then
                Convert.ToInt32(txtSketchWidth.Text)
            End If
        Catch
            MessageBox.Show("Sketch width should be a numeric value", "Error sketch width")
            txtSketchWidth.Text = ""
            txtSketchWidth.Focus()
        End Try
    End Sub

    Private Sub txtPrecision_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecision.TextChanged
        'Validate precision
        Try
            If (txtPrecision.Text <> "") Then
                Convert.ToInt32(txtPrecision.Text)
            End If
        Catch
            MessageBox.Show("Precision should be a numeric value", "Error precision")
            txtPrecision.Text = ""
            txtPrecision.Focus()
        End Try
    End Sub

    Private Sub txtStreamCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStreamCount.TextChanged
        'Validate tolerance
        Try
            If (txtStreamCount.Text <> "") Then
                Convert.ToInt32(txtStreamCount.Text)
            End If
        Catch
            MessageBox.Show("Stream count should be a numeric value", "Error Stream Count")
            txtStreamCount.Text = ""
            txtStreamCount.Focus()
        End Try
    End Sub

    Private Sub txtTolerance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTolerance.TextChanged
        'Validate tolerance
        Try
            If (txtTolerance.Text <> "") Then
                Convert.ToInt32(txtTolerance.Text)
            End If
        Catch
            MessageBox.Show("Stream Tolerance should be a numeric value", "Error Stream Tolerance")
            txtTolerance.Text = ""
            txtTolerance.Focus()
        End Try
    End Sub
End Class
