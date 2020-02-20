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
Imports ESRI.ArcGIS.PublisherControls
Imports ESRI.ArcGIS

Public Class Form1
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.ArcReader) Then
            If Not RuntimeManager.Bind(ProductCode.EngineOrDesktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit 
            End If
        End If

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
    Public WithEvents lblRecords As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdQuery As System.Windows.Forms.Button
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents cmdLoad As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Public WithEvents optTool2 As System.Windows.Forms.RadioButton
    Public WithEvents optTool1 As System.Windows.Forms.RadioButton
    Public WithEvents optTool0 As System.Windows.Forms.RadioButton
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents cmdFeature3 As System.Windows.Forms.Button
    Public WithEvents cmdFeature2 As System.Windows.Forms.Button
    Public WithEvents cmdFeature1 As System.Windows.Forms.Button
    Public WithEvents cmdFeature0 As System.Windows.Forms.Button
    Public WithEvents cmdFeatureSet1 As System.Windows.Forms.Button
    Public WithEvents cmdFeatureSet0 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AxArcReaderControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderControl

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdFeatureSet1 = New System.Windows.Forms.Button
        Me.cmdFeature3 = New System.Windows.Forms.Button
        Me.cmdFeature2 = New System.Windows.Forms.Button
        Me.cmdFeature1 = New System.Windows.Forms.Button
        Me.cmdFeature0 = New System.Windows.Forms.Button
        Me.cmdFeatureSet0 = New System.Windows.Forms.Button
        Me.lblRecords = New System.Windows.Forms.Label
        Me.cmdQuery = New System.Windows.Forms.Button
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.optTool2 = New System.Windows.Forms.RadioButton
        Me.optTool1 = New System.Windows.Forms.RadioButton
        Me.optTool0 = New System.Windows.Forms.RadioButton
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxArcReaderControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderControl
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Frame1.SuspendLayout()
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.DataGridView1)
        Me.Frame1.Controls.Add(Me.cmdFeatureSet1)
        Me.Frame1.Controls.Add(Me.cmdFeature3)
        Me.Frame1.Controls.Add(Me.cmdFeature2)
        Me.Frame1.Controls.Add(Me.cmdFeature1)
        Me.Frame1.Controls.Add(Me.cmdFeature0)
        Me.Frame1.Controls.Add(Me.cmdFeatureSet0)
        Me.Frame1.Controls.Add(Me.lblRecords)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(456, 128)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(241, 369)
        Me.Frame1.TabIndex = 6
        Me.Frame1.TabStop = False
        '
        'cmdFeatureSet1
        '
        Me.cmdFeatureSet1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeatureSet1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeatureSet1.Enabled = False
        Me.cmdFeatureSet1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeatureSet1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeatureSet1.Location = New System.Drawing.Point(120, 16)
        Me.cmdFeatureSet1.Name = "cmdFeatureSet1"
        Me.cmdFeatureSet1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeatureSet1.Size = New System.Drawing.Size(113, 25)
        Me.cmdFeatureSet1.TabIndex = 14
        Me.cmdFeatureSet1.Text = "<< Previous Feature"
        Me.cmdFeatureSet1.UseVisualStyleBackColor = False
        '
        'cmdFeature3
        '
        Me.cmdFeature3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeature3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeature3.Enabled = False
        Me.cmdFeature3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeature3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeature3.Location = New System.Drawing.Point(176, 328)
        Me.cmdFeature3.Name = "cmdFeature3"
        Me.cmdFeature3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeature3.Size = New System.Drawing.Size(57, 33)
        Me.cmdFeature3.TabIndex = 11
        Me.cmdFeature3.Text = "Flicker"
        Me.cmdFeature3.UseVisualStyleBackColor = False
        '
        'cmdFeature2
        '
        Me.cmdFeature2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeature2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeature2.Enabled = False
        Me.cmdFeature2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeature2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeature2.Location = New System.Drawing.Point(120, 328)
        Me.cmdFeature2.Name = "cmdFeature2"
        Me.cmdFeature2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeature2.Size = New System.Drawing.Size(57, 33)
        Me.cmdFeature2.TabIndex = 10
        Me.cmdFeature2.Text = "Flash"
        Me.cmdFeature2.UseVisualStyleBackColor = False
        '
        'cmdFeature1
        '
        Me.cmdFeature1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeature1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeature1.Enabled = False
        Me.cmdFeature1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeature1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeature1.Location = New System.Drawing.Point(64, 328)
        Me.cmdFeature1.Name = "cmdFeature1"
        Me.cmdFeature1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeature1.Size = New System.Drawing.Size(57, 33)
        Me.cmdFeature1.TabIndex = 9
        Me.cmdFeature1.Text = "CenterAt"
        Me.cmdFeature1.UseVisualStyleBackColor = False
        '
        'cmdFeature0
        '
        Me.cmdFeature0.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeature0.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeature0.Enabled = False
        Me.cmdFeature0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeature0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeature0.Location = New System.Drawing.Point(8, 328)
        Me.cmdFeature0.Name = "cmdFeature0"
        Me.cmdFeature0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeature0.Size = New System.Drawing.Size(57, 33)
        Me.cmdFeature0.TabIndex = 8
        Me.cmdFeature0.Text = "ZoomTo"
        Me.cmdFeature0.UseVisualStyleBackColor = False
        '
        'cmdFeatureSet0
        '
        Me.cmdFeatureSet0.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFeatureSet0.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFeatureSet0.Enabled = False
        Me.cmdFeatureSet0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFeatureSet0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFeatureSet0.Location = New System.Drawing.Point(8, 16)
        Me.cmdFeatureSet0.Name = "cmdFeatureSet0"
        Me.cmdFeatureSet0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFeatureSet0.Size = New System.Drawing.Size(113, 25)
        Me.cmdFeatureSet0.TabIndex = 7
        Me.cmdFeatureSet0.Text = "Next Feature >>"
        Me.cmdFeatureSet0.UseVisualStyleBackColor = False
        '
        'lblRecords
        '
        Me.lblRecords.BackColor = System.Drawing.SystemColors.Control
        Me.lblRecords.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRecords.Enabled = False
        Me.lblRecords.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRecords.Location = New System.Drawing.Point(8, 48)
        Me.lblRecords.Name = "lblRecords"
        Me.lblRecords.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRecords.Size = New System.Drawing.Size(225, 17)
        Me.lblRecords.TabIndex = 13
        Me.lblRecords.Text = "0 of 0 features"
        '
        'cmdQuery
        '
        Me.cmdQuery.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQuery.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdQuery.Enabled = False
        Me.cmdQuery.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQuery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuery.Location = New System.Drawing.Point(368, 8)
        Me.cmdQuery.Name = "cmdQuery"
        Me.cmdQuery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQuery.Size = New System.Drawing.Size(73, 49)
        Me.cmdQuery.TabIndex = 5
        Me.cmdQuery.Text = "Spatial Query"
        Me.cmdQuery.UseVisualStyleBackColor = False
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Enabled = False
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdFullExtent.Location = New System.Drawing.Point(296, 8)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(73, 49)
        Me.cmdFullExtent.TabIndex = 4
        Me.cmdFullExtent.Text = "FullExtent"
        Me.cmdFullExtent.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdFullExtent.UseVisualStyleBackColor = False
        '
        'optTool2
        '
        Me.optTool2.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool2.BackColor = System.Drawing.SystemColors.Control
        Me.optTool2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool2.Enabled = False
        Me.optTool2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool2.Location = New System.Drawing.Point(224, 8)
        Me.optTool2.Name = "optTool2"
        Me.optTool2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool2.Size = New System.Drawing.Size(73, 49)
        Me.optTool2.TabIndex = 3
        Me.optTool2.TabStop = True
        Me.optTool2.Text = "Pan"
        Me.optTool2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.optTool2.UseVisualStyleBackColor = False
        '
        'optTool1
        '
        Me.optTool1.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool1.BackColor = System.Drawing.SystemColors.Control
        Me.optTool1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool1.Enabled = False
        Me.optTool1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool1.Location = New System.Drawing.Point(152, 8)
        Me.optTool1.Name = "optTool1"
        Me.optTool1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool1.Size = New System.Drawing.Size(73, 49)
        Me.optTool1.TabIndex = 2
        Me.optTool1.TabStop = True
        Me.optTool1.Text = "ZoomOut"
        Me.optTool1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.optTool1.UseVisualStyleBackColor = False
        '
        'optTool0
        '
        Me.optTool0.Appearance = System.Windows.Forms.Appearance.Button
        Me.optTool0.BackColor = System.Drawing.SystemColors.Control
        Me.optTool0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTool0.Enabled = False
        Me.optTool0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTool0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTool0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.optTool0.Location = New System.Drawing.Point(80, 8)
        Me.optTool0.Name = "optTool0"
        Me.optTool0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTool0.Size = New System.Drawing.Size(73, 49)
        Me.optTool0.TabIndex = 1
        Me.optTool0.TabStop = True
        Me.optTool0.Text = "ZoomIn"
        Me.optTool0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.optTool0.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLoad.Location = New System.Drawing.Point(8, 8)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoad.Size = New System.Drawing.Size(73, 49)
        Me.cmdLoad.TabIndex = 0
        Me.cmdLoad.Text = "Open"
        Me.cmdLoad.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(456, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(249, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "1) Browse to a PMF to load."
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(456, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(249, 17)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "2) Navigate to some features of interest. "
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(456, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(225, 56)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "4) Loop through the features to display field values and use the buttons to ident" & _
            "ify each feature on the map."
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label6.Location = New System.Drawing.Point(456, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(249, 30)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "3) Query the focus map for all visible features within the current map extent. "
        '
        'AxArcReaderControl1
        '
        Me.AxArcReaderControl1.Location = New System.Drawing.Point(8, 64)
        Me.AxArcReaderControl1.Name = "AxArcReaderControl1"
        Me.AxArcReaderControl1.Size = New System.Drawing.Size(432, 424)
        Me.AxArcReaderControl1.TabIndex = 19
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(6, 68)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(229, 254)
        Me.DataGridView1.TabIndex = 15
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 5
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(708, 504)
        Me.Controls.Add(Me.AxArcReaderControl1)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdQuery)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.optTool2)
        Me.Controls.Add(Me.optTool1)
        Me.Controls.Add(Me.optTool0)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Query"
        Me.Frame1.ResumeLayout(False)
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private m_pFeature As ARFeature
    Private m_pFeatureSet As ARFeatureSet
    Private m_bQueryFeatures As Boolean
    Private m_bQueryValues As Boolean
    Private m_lRecord As Integer

    Private Sub cmdFeature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFeature0.Click, cmdFeature1.Click, cmdFeature2.Click, cmdFeature3.Click

        Dim senderName As String
        senderName = sender.Name
        'Navigate or show the selected feature
        Select Case senderName
            Case "cmdFeature0"
                m_pFeature.ZoomTo()
            Case "cmdFeature1"
                m_pFeature.CenterAt()
            Case "cmdFeature2"
                m_pFeature.Flash()
            Case "cmdFeature3"
                m_pFeature.Flicker()
        End Select

    End Sub

    Private Sub cmdFeatureSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFeatureSet0.Click, cmdFeatureSet1.Click

        Dim senderName As String
        senderName = sender.Name

        Select Case senderName
            Case "cmdFeatureSet0"
                'Next record
                m_lRecord = m_lRecord + 1
            Case "cmdFeatureSet1"
                'Previous record
                m_lRecord = m_lRecord - 1
        End Select

        'Get the next/previous feature
        m_pFeature = m_pFeatureSet.ARFeature(m_lRecord)
        'Display attribute values
        UpdateValueDisplay()

    End Sub

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        Dim dXmax, dXmin, dYmin, dYmax As Double
        'Get the coordinates of data's full extent
        AxArcReaderControl1.ARPageLayout.FocusARMap.GetFullExtent(dXmin, dYmin, dXmax, dYmax)
        'Set the extent of the focus map
        AxArcReaderControl1.ARPageLayout.FocusARMap.SetExtent(dXmin, dYmin, dXmax, dYmax)
        'Refresh the display
        AxArcReaderControl1.ARPageLayout.FocusARMap.Refresh()

    End Sub

    Private Sub cmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoad.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Select Published Map Document"
        OpenFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Load the specified pmf
        If AxArcReaderControl1.CheckDocument(sFilePath) = True Then
            AxArcReaderControl1.LoadDocument(sFilePath)
        Else
            MsgBox("This document cannot be loaded!")
            Exit Sub
        End If

        'Determine whether permission to search layers and query field values
        m_bQueryFeatures = AxArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryFeatures)
        m_bQueryValues = AxArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryValues)

        'Set current tool
        optTool0.Checked = True

    End Sub

    Private Sub cmdQuery_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdQuery.Click

        'Determine whether permission to search layers
        If m_bQueryFeatures = False Then
            MsgBox("You do not have permission to search for features!")
            Exit Sub
        End If

        'Get IARQueryDef interface
        Dim pSearchDef As ArcReaderSearchDef
        pSearchDef = New ArcReaderSearchDef
        'Set the spatial searching to intersects
        pSearchDef.SpatialRelationship = esriARSpatialRelationship.esriARSpatialRelationshipIntersects

        'Get the coordinates of the current extent
        Dim dXmax, dXmin, dYmin, dYmax As Double
        AxArcReaderControl1.ARPageLayout.FocusARMap.GetExtent(dXmin, dYmin, dXmax, dYmax)
        'Set the envelope coordinates as the search shape
        pSearchDef.SetEnvelopeShape(dXmin, dYmin, dXmax, dYmax)

        'Get IARFeatureSet interface
        m_pFeatureSet = AxArcReaderControl1.ARPageLayout.FocusARMap.QueryARFeatures(pSearchDef)
        'Reset the featureset
        m_pFeatureSet.Reset()
        'Get the IARFeature interface
        m_pFeature = m_pFeatureSet.Next
        'Display attribute values
        m_lRecord = 0
        UpdateValueDisplay()

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        'Load command button images from the resource editor
        Dim pBitmap As System.Drawing.Bitmap
        pBitmap = New System.Drawing.Bitmap(GetType(Form1).Assembly.GetManifestResourceStream(GetType(Form1), "browse.bmp"))
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdLoad.Image = pBitmap
        pBitmap = New System.Drawing.Bitmap(GetType(Form1).Assembly.GetManifestResourceStream(GetType(Form1), "ZoomIn.bmp"))
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool0.Image = pBitmap
        pBitmap = New System.Drawing.Bitmap(GetType(Form1).Assembly.GetManifestResourceStream(GetType(Form1), "ZoomOut.bmp"))
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool1.Image = pBitmap
        pBitmap = New System.Drawing.Bitmap(GetType(Form1).Assembly.GetManifestResourceStream(GetType(Form1), "Pan.bmp"))
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        optTool2.Image = pBitmap
        pBitmap = New System.Drawing.Bitmap(GetType(Form1).Assembly.GetManifestResourceStream(GetType(Form1), "FullExtent.bmp"))
        pBitmap.MakeTransparent(System.Drawing.Color.Teal)
        cmdFullExtent.Image = pBitmap

    End Sub
    Private Sub MixedControlsCurrentTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTool0.Click, optTool1.Click, optTool2.Click

        Dim senderName As String
        senderName = sender.Name

        Select Case senderName
            'Set current tool
            Case "optTool0"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn
            Case "optTool1"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut
            Case "optTool2"
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan
        End Select

    End Sub

    Public Sub UpdateValueDisplay()

        Dim i As Short
        Dim iRow As Short
        Dim sFieldValue As String
        Dim pGraphics As Graphics = Me.CreateGraphics()

        DataGridView1.Rows.Clear()

        'For each field that isn't the 'Shape' field
        For i = 0 To m_pFeature.FieldCount - 1
            If m_pFeature.FieldType(i) <> esriARFieldType.esriARFieldTypeGeometry And (m_pFeature.FieldType(i) <> esriARFieldType.esriARFieldTypeRaster) And (m_pFeature.FieldType(i) <> esriARFieldType.esriARFieldTypeBlob) Then

                'Display field names
                Dim dataGridViewRow As DataGridViewRow
                dataGridViewRow = New DataGridViewRow()
                dataGridViewRow.HeaderCell.Value = m_pFeature.FieldAliasName(i)

                'Display field values
                If m_bQueryValues = True Then
                    sFieldValue = m_pFeature.ValueAsString(CInt(i))
                Else
                    sFieldValue = "No Permission"
                End If
                Dim cell As DataGridViewCell = New DataGridViewTextBoxCell()
                cell.Value = sFieldValue
                dataGridViewRow.Cells.Add(cell)
                DataGridView1.Rows.Add(dataGridViewRow)

                iRow = iRow + 1
            End If
        Next i


        'Enabled/disbale controls
        Dim bEnabled As Boolean
        If m_pFeatureSet.ARFeatureCount = 0 Then
            bEnabled = False
            cmdFeatureSet0.Enabled = False
            cmdFeatureSet1.Enabled = False
            lblRecords.Text = m_lRecord & " of " & m_pFeatureSet.ARFeatureCount
        ElseIf m_pFeatureSet.ARFeatureCount = 1 Then
            bEnabled = True
            cmdFeatureSet0.Enabled = False
            cmdFeatureSet1.Enabled = False
            lblRecords.Text = m_lRecord + 1 & " of " & m_pFeatureSet.ARFeatureCount
        Else
            bEnabled = True
            If m_lRecord = 0 Then cmdFeatureSet1.Enabled = False Else cmdFeatureSet1.Enabled = True
            If m_lRecord + 1 = m_pFeatureSet.ARFeatureCount Then cmdFeatureSet0.Enabled = False Else cmdFeatureSet0.Enabled = True
            lblRecords.Text = m_lRecord + 1 & " of " & m_pFeatureSet.ARFeatureCount
        End If
        cmdFeature0.Enabled = bEnabled
        cmdFeature1.Enabled = bEnabled
        cmdFeature2.Enabled = bEnabled
        cmdFeature3.Enabled = bEnabled

        'Clean up the Graphics object
        pGraphics.Dispose()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()
    End Sub

    Private Sub AxArcReaderControl1_OnCurrentViewChanged(ByVal sender As Object, ByVal e As ESRI.ArcGIS.PublisherControls.IARControlEvents_OnCurrentViewChangedEvent) Handles AxArcReaderControl1.OnCurrentViewChanged
        Dim bEnabled As Boolean

        'Set the current tool
        If AxArcReaderControl1.CurrentViewType = esriARViewType.esriARViewTypeNone Then
            bEnabled = False
        ElseIf AxArcReaderControl1.CurrentViewType = esriARViewType.esriARViewTypePageLayout Then
            bEnabled = False
            If AxArcReaderControl1.CurrentARTool <> esriARTool.esriARToolNoneSelected Then
                AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolNoneSelected
            End If
        Else
            bEnabled = True
            If AxArcReaderControl1.CurrentARTool <> esriARTool.esriARToolMapZoomIn Then
                optTool0.Checked = True
            End If
        End If

        'Enable/disbale controls
        cmdQuery.Enabled = bEnabled
        optTool0.Enabled = bEnabled
        optTool1.Enabled = bEnabled
        optTool2.Enabled = bEnabled
        cmdFullExtent.Enabled = bEnabled
    End Sub
End Class