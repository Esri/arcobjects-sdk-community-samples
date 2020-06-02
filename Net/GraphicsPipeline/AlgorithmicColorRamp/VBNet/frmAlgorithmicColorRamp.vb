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
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System
Imports System.Collections
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.Compatibility

Friend Class frmAlgorithmicColorRamp
	Inherits System.Windows.Forms.Form
	

	' This form allows a user to set properties determining the constraints of a
	' AlgoritmicColorRamp, which is then used to populate an existing ClassBreaksRenderer
	' on an existing FeatureLayer.
	'
	'
	' The m_lngClasses variable is set by the calling function, to indicate the
	' number of random colors required by the classbreaksrenderer.
	'
	Public m_lngClasses As Integer
	'
	' The m_enumNewColors variable holds the colors to be returned to the calling function.
	'
    Public m_enumNewColors As IEnumColors
    '
    ' The m_lngColors variable holds the index of the last color displayed in the array.
    '
    Private m_lngColors As Integer

    Private Buttons(2) As System.Windows.Forms.Button
    Private LabelsIndex(10) As System.Windows.Forms.Label
    Private TextBoxColors(10) As System.Windows.Forms.TextBox

    Private Sub cmbAlgorithm_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbAlgorithm.SelectedIndexChanged
        If cmbAlgorithm.SelectedIndex > -1 Then
            Call UpdateRamp()
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        '
        ' User pressed Cancel, so we set the colors enumeration to nothing and
        ' unload the form.
        '
        m_enumNewColors = Nothing
        Me.Close()
    End Sub

    Private Sub cmdEnumColorsNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnumColorsNext.Click
        '
        ' Increase the indicator variable m_lngColors by 10, so we can display the
        ' next ten colors to the user.
        '
        If Not m_enumNewColors Is Nothing Then
            m_lngColors = m_lngColors + 10
            Call HideColors()
            Call LooRGBColors()
        End If
    End Sub

    Private Sub cmdEnumColorsFirst_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnumColorsFirst.Click
        '
        ' Reset the indicator variable to zero.
        '
        If Not m_enumNewColors Is Nothing Then
            m_lngColors = 0
            Call HideColors()
            Call LooRGBColors()
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        '
        ' Check we have a colors enumeration.
        '
        If m_enumNewColors Is Nothing Then
            MsgBox("You have not created a new color ramp." & "Your layer symbology will be unchanged.", MsgBoxStyle.Information, "No Ramp Created")
        Else
            Me.Hide()
        End If
    End Sub

    Private Sub frmAlgorithmicColorRamp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '
        ' Initialize the controls.
        '
        Call SetupControls()
    End Sub

    Private Sub UpdateRamp()
        '
        ' Create a new AlgorithmicColorRamp object, and get it's
        ' IAlgorithmicColorRamp interface.
        '
        Dim AlgortihmicColorRamp As IAlgorithmicColorRamp
        AlgortihmicColorRamp = New ESRI.ArcGIS.Display.AlgorithmicColorRamp
        '
        ' Set the size of the color ramp ot the number of classes
        ' to be renderered.
        '
        AlgortihmicColorRamp.Size = m_lngClasses
        '
        ' Set the color ramps properties.
        '
        Dim RGBColor As IRgbColor
        With AlgortihmicColorRamp
            RGBColor = New RgbColor
            RGBColor.RGB = System.Drawing.ColorTranslator.ToOle(txtStartColor.BackColor)
            .FromColor = RGBColor
            RGBColor.RGB = System.Drawing.ColorTranslator.ToOle(txtEndColor.BackColor)
            .ToColor = RGBColor
            .Algorithm = cmbAlgorithm.SelectedIndex
        End With

        Dim boolRamp As Boolean
        If AlgortihmicColorRamp.Size > 0 Then

            boolRamp = True
            AlgortihmicColorRamp.CreateRamp(boolRamp)
            If boolRamp Then
                m_enumNewColors = AlgortihmicColorRamp.Colors
                m_enumNewColors.Reset()
                cmdOK.Enabled = True
                '
                ' Check if we should be showing the colors.
                '
                If chkShowColors.CheckState = System.Windows.Forms.CheckState.Checked Then
                    '
                    ' Populate the Colors textbox array and it's labels.
                    '
                    m_lngColors = 0
                    Call ShowColorsArray()
                End If
            End If
        End If
    End Sub

    Private Sub SetupControls()
        LabelsIndex(0) = Label1
        LabelsIndex(1) = Label2
        LabelsIndex(2) = Label3
        LabelsIndex(3) = Label4
        LabelsIndex(4) = Label5
        LabelsIndex(5) = Label6
        LabelsIndex(6) = Label7
        LabelsIndex(7) = Label8
        LabelsIndex(8) = Label9
        LabelsIndex(9) = Label10

        TextBoxColors(0) = TextBox1
        TextBoxColors(1) = TextBox2
        TextBoxColors(2) = TextBox3
        TextBoxColors(3) = TextBox4
        TextBoxColors(4) = TextBox5
        TextBoxColors(5) = TextBox6
        TextBoxColors(6) = TextBox7
        TextBoxColors(7) = TextBox8
        TextBoxColors(8) = TextBox9
        TextBoxColors(9) = TextBox10

        Call HideColors()

        txtStartColor.Text = ""
        txtEndColor.Text = ""
        txtStartColor.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFS)
        txtEndColor.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFS)
        'MsgBox("Before ", MsgBoxStyle.Information, "SetupControls ")

        Buttons(0) = Button1
        Buttons(1) = Button2

        'MsgBox("After  ", MsgBoxStyle.Information, "SetupControls")

        cmbAlgorithm.Items.Clear()
        cmbAlgorithm.Items.Insert(0, "0 - esriHSVAlgorithm")
        cmbAlgorithm.Items.Insert(1, "1 - esriCIELabAlgorithm")
        cmbAlgorithm.Items.Insert(2, "2 - esriLabLChAlgorithm")
        cmbAlgorithm.SelectedIndex = 0

        cmdOK.Enabled = False
        chkShowColors.CheckState = System.Windows.Forms.CheckState.Unchecked

        Call UpdateRamp()
        Call chkShowColors_CheckStateChanged(chkShowColors, New System.EventArgs())
    End Sub

    Private Sub ShowColorsArray()
        If m_enumNewColors Is Nothing Then
            Exit Sub
        Else
            '
            ' Iterate and show all colors in the ColorRamp.
            '
            Call HideColors()
            LooRGBColors() 'm_lngColors
        End If
    End Sub

    Private Sub LooRGBColors()
        '
        ' Move to the required Color to show. We only have space to show ten colors
        ' at a time on the form. So when we wish to show the next ten colors,
        ' (colors 11-20, 21-30 etc) we iterate the colors enumeration appropriately.
        '
        Dim lngMoveNext As Integer
        m_enumNewColors.Reset()
        Do While lngMoveNext < m_lngColors
            m_enumNewColors.Next()
            lngMoveNext = lngMoveNext + 1
        Loop
        '
        ' Show colors in textboxes as necessary.
        '
        Dim colNew As IColor
        Dim lngCount As Integer
        For lngCount = 0 To 9
            'commented the control array txtColor - OLD
            'With txtColor(lngCount)
            '    colNew = m_enumNewColors.Next
            '    '
            '    ' If getting the next color returns nothing, we have got to
            '    ' the end of the colors enumeration.
            '    '
            '    If colNew Is Nothing Then
            '        Exit For
            '    End If
            '    .BackColor = System.Drawing.ColorTranslator.FromOle(colNew.RGB)
            '    .Visible = True
            'End With
            With TextBoxColors(lngCount)
                colNew = m_enumNewColors.Next
                '
                ' If getting the next color returns nothing, we have got to
                ' the end of the colors enumeration.
                '
                If colNew Is Nothing Then
                    Exit For
                End If
                .BackColor = System.Drawing.ColorTranslator.FromOle(colNew.RGB)
                .Visible = True
            End With
            'Commented the control array lblIndex - OLD
            'With lblIndex(lngCount)
            '    .Text = CStr(lngCount + m_lngColors)
            '    .Visible = True
            'End With
            LabelsIndex(lngCount).Text = CStr(lngCount + m_lngColors)
            LabelsIndex(lngCount).Visible = True

        Next lngCount
    End Sub
	
	Private Sub HideColors()
		'
		' Hide all Color textboxes.
		'
		Dim i As Short
		For i = 0 To 9
            'txtColor(i).Visible = False '- OLD control array
            TextBoxColors(i).Visible = False
            'lblIndex(i).Visible = False '- OLD control array
            LabelsIndex(i).Visible = False
		Next i
	End Sub
	
	Private Sub frmAlgorithmicColorRamp_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		If m_enumNewColors Is Nothing Then
			'
			' User has exited the Form without setting a new ColorRamp.
			'
			MsgBox("You have not created a new ColorRamp." & vbNewLine & "Your symbology will not be changed.", MsgBoxStyle.Information, "ColorRamp not set")
		End If
		eventArgs.Cancel = Cancel
	End Sub

    Private Sub chkShowColors_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowColors.Click
        If chkShowColors.CheckState = System.Windows.Forms.CheckState.Checked Then
            Me.Width = VB6.TwipsToPixelsX(3705)
            Call ShowColorsArray()
        Else
            Me.Width = VB6.TwipsToPixelsX(2355)
        End If

    End Sub

    Private Sub chkShowColors_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowColors.CheckStateChanged
        '
        ' Show and hide the colors array.
        '
        If chkShowColors.CheckState = System.Windows.Forms.CheckState.Checked Then
            Me.Width = Microsoft.VisualBasic.Compatibility.VB6.TwipsToPixelsX(3705)
            Call ShowColorsArray()
        Else
            Me.Width = VB6.TwipsToPixelsX(2355)
        End If
    End Sub

    Private Sub frmAlgorithmicColorRamp_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button2.Click
        Dim ColorSelector As IColorSelector
        If Buttons(0) Is sender Then
            ' Create color selector object.
            ColorSelector = New ColorSelector
            ' Open the selector dialog.
            If ColorSelector.DoModal(Me.Handle.ToInt32) Then
                '
                ' A Color was selected (if the above method returned false,
                ' this indicates that the user pressed Cancel).
                txtStartColor.BackColor = System.Drawing.ColorTranslator.FromOle(ColorSelector.Color.RGB)
            End If
            Call UpdateRamp()

        Else
            ColorSelector = New ColorSelector
            If ColorSelector.DoModal(Me.Handle.ToInt32) Then
                txtEndColor.BackColor = System.Drawing.ColorTranslator.FromOle(ColorSelector.Color.RGB)
            End If
            Call UpdateRamp()
        End If
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub _txtColor_0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class