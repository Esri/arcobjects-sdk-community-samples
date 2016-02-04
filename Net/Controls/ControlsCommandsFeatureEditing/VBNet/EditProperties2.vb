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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls

Public Class EditProperties2
    Inherits System.Windows.Forms.Form

    Private m_engineEditProperties2 As New EngineEditorClass

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
    Friend WithEvents txtTolerance As System.Windows.Forms.TextBox
    Friend WithEvents chkGrid As System.Windows.Forms.CheckBox
    Friend WithEvents chkSnapTips As System.Windows.Forms.CheckBox
    Friend WithEvents txtFactor As System.Windows.Forms.TextBox
    Friend WithEvents cboUnits As System.Windows.Forms.ComboBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents txtPrecision As System.Windows.Forms.TextBox
    Friend WithEvents txtOffset As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtTolerance = New System.Windows.Forms.TextBox
        Me.chkGrid = New System.Windows.Forms.CheckBox
        Me.chkSnapTips = New System.Windows.Forms.CheckBox
        Me.txtFactor = New System.Windows.Forms.TextBox
        Me.cboUnits = New System.Windows.Forms.ComboBox
        Me.cboType = New System.Windows.Forms.ComboBox
        Me.txtPrecision = New System.Windows.Forms.TextBox
        Me.txtOffset = New System.Windows.Forms.TextBox
        Me.label8 = New System.Windows.Forms.Label
        Me.label7 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtTolerance
        '
        Me.txtTolerance.Location = New System.Drawing.Point(152, 201)
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(80, 20)
        Me.txtTolerance.TabIndex = 31
        Me.txtTolerance.Text = ""
        '
        'chkGrid
        '
        Me.chkGrid.Location = New System.Drawing.Point(152, 225)
        Me.chkGrid.Name = "chkGrid"
        Me.chkGrid.Size = New System.Drawing.Size(24, 32)
        Me.chkGrid.TabIndex = 30
        '
        'chkSnapTips
        '
        Me.chkSnapTips.Location = New System.Drawing.Point(152, 161)
        Me.chkSnapTips.Name = "chkSnapTips"
        Me.chkSnapTips.Size = New System.Drawing.Size(24, 32)
        Me.chkSnapTips.TabIndex = 29
        '
        'txtFactor
        '
        Me.txtFactor.Location = New System.Drawing.Point(152, 137)
        Me.txtFactor.Name = "txtFactor"
        Me.txtFactor.Size = New System.Drawing.Size(80, 20)
        Me.txtFactor.TabIndex = 28
        Me.txtFactor.Text = ""
        '
        'cboUnits
        '
        Me.cboUnits.Items.AddRange(New Object() {"Radians", "Decimal Degrees", "Degrees Minutes Seconds", "Gradians", "Gons"})
        Me.cboUnits.Location = New System.Drawing.Point(122, 105)
        Me.cboUnits.Name = "cboUnits"
        Me.cboUnits.Size = New System.Drawing.Size(136, 21)
        Me.cboUnits.TabIndex = 27
        '
        'cboType
        '
        Me.cboType.Items.AddRange(New Object() {"North Azimuth", "South Azimuth", "Polar", "Quadrant Bearing"})
        Me.cboType.Location = New System.Drawing.Point(122, 73)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(136, 21)
        Me.cboType.TabIndex = 26
        '
        'txtPrecision
        '
        Me.txtPrecision.Location = New System.Drawing.Point(152, 41)
        Me.txtPrecision.Name = "txtPrecision"
        Me.txtPrecision.Size = New System.Drawing.Size(80, 20)
        Me.txtPrecision.TabIndex = 25
        Me.txtPrecision.Text = ""
        '
        'txtOffset
        '
        Me.txtOffset.Location = New System.Drawing.Point(152, 9)
        Me.txtOffset.Name = "txtOffset"
        Me.txtOffset.Size = New System.Drawing.Size(80, 20)
        Me.txtOffset.TabIndex = 24
        Me.txtOffset.Text = ""
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(8, 233)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(120, 16)
        Me.label8.TabIndex = 23
        Me.label8.Text = "Use Ground to Grid:"
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(8, 201)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(136, 16)
        Me.label7.TabIndex = 22
        Me.label7.Text = "Sticky Move Tolerance:"
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(8, 169)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(88, 16)
        Me.label6.TabIndex = 21
        Me.label6.Text = "Snap Tips:"
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(8, 137)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(144, 16)
        Me.label5.TabIndex = 20
        Me.label5.Text = "Distance Correction Factor:"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 105)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(88, 16)
        Me.label4.TabIndex = 19
        Me.label4.Text = "Direction Units:"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(8, 73)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(88, 16)
        Me.label3.TabIndex = 18
        Me.label3.Text = "Direction Type:"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 41)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(120, 16)
        Me.label2.TabIndex = 17
        Me.label2.Text = "Angular Unit Precision:"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(136, 16)
        Me.label1.TabIndex = 16
        Me.label1.Text = "Angular Correction Offset:"
        '
        'EditProperties2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(264, 262)
        Me.Controls.Add(Me.txtTolerance)
        Me.Controls.Add(Me.chkGrid)
        Me.Controls.Add(Me.chkSnapTips)
        Me.Controls.Add(Me.txtFactor)
        Me.Controls.Add(Me.cboUnits)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.txtPrecision)
        Me.Controls.Add(Me.txtOffset)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EditProperties2"
        Me.Text = "Edit Properties 2"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub EditProperties2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Populate form with current IEngineProperties2 values 
        txtOffset.Text = m_engineEditProperties2.AngularCorrectionOffset.ToString()
        txtPrecision.Text = m_engineEditProperties2.AngularUnitPrecision.ToString()
        txtFactor.Text = m_engineEditProperties2.DistanceCorrectionFactor.ToString()
        txtTolerance.Text = m_engineEditProperties2.StickyMoveTolerance.ToString()

        If (m_engineEditProperties2.SnapTips) Then
            chkSnapTips.Checked = True
        Else
            chkSnapTips.Checked = False
        End If

        If (m_engineEditProperties2.UseGroundToGrid) Then
            chkGrid.Checked = True
        Else
            chkGrid.Checked = False
        End If

        'Select current direction type
        Dim type As esriEngineDirectionType
        type = m_engineEditProperties2.DirectionType

        Select Case (type.ToString())
            Case "esriEngineDTNorthAzimuth"
                cboType.SelectedItem = "North Azimuth"
                Exit Select
            Case "esriEngineDTSouthAzimuth"
                cboType.SelectedItem = "South Azimuth"
                Exit Select
            Case "esriEngineDTPolar"
                cboType.SelectedItem = "Polar"
                Exit Select
            Case "esriEngineDTQuadrantBearing"
                cboType.SelectedItem = "Quadrant Bearing"
                Exit Select
            Case Else
                Exit Select
        End Select


        'Select current direction units
        Dim units As esriEngineDirectionUnits
        units = m_engineEditProperties2.DirectionUnits
        Select Case (units.ToString())
            Case "esriEngineDURadians"
                cboUnits.SelectedItem = "Radians"
                Exit Select
            Case "esriEngineDUDecimalDegrees"
                cboUnits.SelectedItem = "Decimal Degrees"
                Exit Select
            Case "esriEngineDUDegreesMinutesSeconds"
                cboUnits.SelectedItem = "Degrees Minutes Seconds"
                Exit Select
            Case "esriEngineDUGradians"
                cboUnits.SelectedItem = "Gradians"
                Exit Select
            Case "esriEngineDUGons"
                cboUnits.SelectedItem = "Gons"
                Exit Select
            Case Else
                Exit Select
        End Select
    End Sub

    Private Sub EditProperties2_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'Update Offset property
        If (txtOffset.Text <> "") Then
            m_engineEditProperties2.AngularCorrectionOffset = Convert.ToInt32(txtOffset.Text)
        End If

        'Update Precision property
        If (txtPrecision.Text <> "") Then
            m_engineEditProperties2.AngularUnitPrecision = Convert.ToInt32(txtPrecision.Text)
        End If

        'Update Distance Correction Factor property
        If (txtFactor.Text <> "") Then
            m_engineEditProperties2.DistanceCorrectionFactor = Convert.ToInt32(txtFactor.Text)
        End If

        'Update Tolerance property
        If (txtTolerance.Text <> "") Then
            m_engineEditProperties2.StickyMoveTolerance = Convert.ToInt32(txtTolerance.Text)
        End If

        'Update Snap Tips property
        If (chkSnapTips.Checked) Then
            m_engineEditProperties2.SnapTips = True
        Else
            m_engineEditProperties2.SnapTips = False
        End If

        'Update Grid property
        If (chkGrid.Checked) Then
            m_engineEditProperties2.UseGroundToGrid = True
        Else
            m_engineEditProperties2.UseGroundToGrid = False
        End If

        'Set Direction Type property
        Dim type As String
        type = cboType.SelectedItem.ToString()
        Select Case (Type)
            Case "North Azimuth"
                m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTNorthAzimuth
                Exit Select
            Case "South Azimuth"
                m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTSouthAzimuth
                Exit Select
            Case "Polar"
                m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTPolar
                Exit Select
            Case "Quadrant Bearing"
                m_engineEditProperties2.DirectionType = esriEngineDirectionType.esriEngineDTQuadrantBearing
            Case Else
                Exit Select
        End Select

        'Set Direction Units property
        Dim units As String
        units = cboUnits.SelectedItem.ToString()
        Select Case (units)
        Case "Radians"
                m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDURadians
                Exit Select
            Case "Decimal Degrees"
                m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUDecimalDegrees
                Exit Select
            Case "Degrees Minutes Seconds"
                m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUDegreesMinutesSeconds
                Exit Select
            Case "Gradians"
                m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUGradians
                Exit Select
            Case "Gons"
                m_engineEditProperties2.DirectionUnits = esriEngineDirectionUnits.esriEngineDUGons
                Exit Select
            Case Else
                Exit Select
        End Select
    End Sub

    Private Sub txtOffset_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOffset.TextChanged
        'Validate offset
        Try
            If (txtOffset.Text <> "") Then
                Convert.ToInt32(txtOffset.Text)
            End If
        Catch
            MessageBox.Show("Correction offset should be a numeric value", "Correction Offset")
            txtOffset.Text = ""
            txtOffset.Focus()
        End Try
    End Sub

    Private Sub txtPrecision_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecision.TextChanged
        'Validate precision
        Try
            If (txtPrecision.Text <> "") Then
                Convert.ToInt32(txtPrecision.Text)
            End If
        Catch
            MessageBox.Show("Unit precision should be a numeric value", "Unit Precision")
            txtPrecision.Text = ""
            txtPrecision.Focus()
        End Try
    End Sub

    Private Sub txtFactor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFactor.TextChanged
        'Validate factor
        Try
            If (txtFactor.Text <> "") Then
                Convert.ToInt32(txtFactor.Text)
            End If
        Catch
            MessageBox.Show("Distance Correction Factor should be a numeric value", "Distance Correction Factor")
            txtFactor.Text = ""
            txtFactor.Focus()
        End Try
    End Sub

    Private Sub txtTolerance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTolerance.TextChanged
        'Validate tolerance
        Try
            If (txtTolerance.Text <> "") Then
                Convert.ToInt32(txtTolerance.Text)
            End If
        Catch
            MessageBox.Show("Sticky Move Tolerance should be a numeric value", "Sticky Move Tolerance")
            txtTolerance.Text = ""
            txtTolerance.Focus()
        End Try
    End Sub
End Class
