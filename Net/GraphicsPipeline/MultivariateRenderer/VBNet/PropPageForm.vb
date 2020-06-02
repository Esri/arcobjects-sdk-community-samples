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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Public Class PropPageForm
    Inherits System.Windows.Forms.Form

    Private Enum eRendererType
        eShapePattern
        eColor
        eSize
    End Enum

    Private m_PageIsDirty As Boolean = False
    Private m_pSite As IComPropertyPageSite
    Private m_pMap As IMap
    Private m_pCurrentLayer As IGeoFeatureLayer
    Private m_pRend As IFeatureRenderer

    Private m_pShapePatternRendList() As IFeatureRenderer
    Private m_pColorRendList() As IFeatureRenderer
    Private m_pSizeRendList() As IFeatureRenderer

    Private m_eColorCombinationMethod As EColorCombinationType
    Private m_pShapePatternRend As IFeatureRenderer
    Private m_pColorRend1 As IFeatureRenderer
    Private m_pColorRend2 As IFeatureRenderer
    Private m_pSizeRend As IFeatureRenderer


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
    Friend WithEvents cboShapePattern As System.Windows.Forms.ComboBox
    Friend WithEvents cboHue As System.Windows.Forms.ComboBox
    Friend WithEvents chkShapePattern As System.Windows.Forms.CheckBox
    Friend WithEvents chkColor As System.Windows.Forms.CheckBox
    Friend WithEvents radComponents As System.Windows.Forms.RadioButton
    Friend WithEvents lblHue As System.Windows.Forms.Label
    Friend WithEvents lblPrimaryColor As System.Windows.Forms.Label
    Friend WithEvents radCombination As System.Windows.Forms.RadioButton
    Friend WithEvents cboPrimaryColor As System.Windows.Forms.ComboBox
    Friend WithEvents cboSatValue As System.Windows.Forms.ComboBox
    Friend WithEvents lblSatValue As System.Windows.Forms.Label
    Friend WithEvents lblSecondaryColor As System.Windows.Forms.Label
    Friend WithEvents cboSecondaryColor As System.Windows.Forms.ComboBox
    Friend WithEvents chkSize As System.Windows.Forms.CheckBox
    Friend WithEvents cboSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkRotation As System.Windows.Forms.CheckBox
    Friend WithEvents butRotation As System.Windows.Forms.Button
    Friend WithEvents cboSize1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cboShapePattern = New System.Windows.Forms.ComboBox
        Me.cboHue = New System.Windows.Forms.ComboBox
        Me.chkShapePattern = New System.Windows.Forms.CheckBox
        Me.chkColor = New System.Windows.Forms.CheckBox
        Me.radComponents = New System.Windows.Forms.RadioButton
        Me.lblHue = New System.Windows.Forms.Label
        Me.lblPrimaryColor = New System.Windows.Forms.Label
        Me.radCombination = New System.Windows.Forms.RadioButton
        Me.cboPrimaryColor = New System.Windows.Forms.ComboBox
        Me.cboSatValue = New System.Windows.Forms.ComboBox
        Me.lblSatValue = New System.Windows.Forms.Label
        Me.lblSecondaryColor = New System.Windows.Forms.Label
        Me.cboSecondaryColor = New System.Windows.Forms.ComboBox
        Me.chkSize = New System.Windows.Forms.CheckBox
        Me.chkRotation = New System.Windows.Forms.CheckBox
        Me.butRotation = New System.Windows.Forms.Button
        Me.cboSize1 = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cboShapePattern
        '
        Me.cboShapePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShapePattern.Enabled = False
        Me.cboShapePattern.Location = New System.Drawing.Point(224, 8)
        Me.cboShapePattern.Name = "cboShapePattern"
        Me.cboShapePattern.Size = New System.Drawing.Size(192, 21)
        Me.cboShapePattern.TabIndex = 4
        '
        'cboHue
        '
        Me.cboHue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHue.Enabled = False
        Me.cboHue.Location = New System.Drawing.Point(224, 48)
        Me.cboHue.Name = "cboHue"
        Me.cboHue.Size = New System.Drawing.Size(192, 21)
        Me.cboHue.TabIndex = 5
        '
        'chkShapePattern
        '
        Me.chkShapePattern.Location = New System.Drawing.Point(8, 8)
        Me.chkShapePattern.Name = "chkShapePattern"
        Me.chkShapePattern.Size = New System.Drawing.Size(152, 24)
        Me.chkShapePattern.TabIndex = 6
        Me.chkShapePattern.Text = "Shape/Pattern"
        '
        'chkColor
        '
        Me.chkColor.Location = New System.Drawing.Point(8, 32)
        Me.chkColor.Name = "chkColor"
        Me.chkColor.Size = New System.Drawing.Size(152, 24)
        Me.chkColor.TabIndex = 7
        Me.chkColor.Text = "Color"
        '
        'radComponents
        '
        Me.radComponents.Enabled = False
        Me.radComponents.Location = New System.Drawing.Point(24, 56)
        Me.radComponents.Name = "radComponents"
        Me.radComponents.Size = New System.Drawing.Size(128, 24)
        Me.radComponents.TabIndex = 8
        Me.radComponents.Text = "Color Components"
        '
        'lblHue
        '
        Me.lblHue.Enabled = False
        Me.lblHue.Location = New System.Drawing.Point(136, 48)
        Me.lblHue.Name = "lblHue"
        Me.lblHue.Size = New System.Drawing.Size(88, 24)
        Me.lblHue.TabIndex = 9
        Me.lblHue.Text = "Hue"
        '
        'lblPrimaryColor
        '
        Me.lblPrimaryColor.Enabled = False
        Me.lblPrimaryColor.Location = New System.Drawing.Point(136, 104)
        Me.lblPrimaryColor.Name = "lblPrimaryColor"
        Me.lblPrimaryColor.Size = New System.Drawing.Size(88, 24)
        Me.lblPrimaryColor.TabIndex = 12
        Me.lblPrimaryColor.Text = "Color 1"
        '
        'radCombination
        '
        Me.radCombination.Enabled = False
        Me.radCombination.Location = New System.Drawing.Point(24, 112)
        Me.radCombination.Name = "radCombination"
        Me.radCombination.Size = New System.Drawing.Size(128, 24)
        Me.radCombination.TabIndex = 11
        Me.radCombination.Text = "Color Combination"
        '
        'cboPrimaryColor
        '
        Me.cboPrimaryColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrimaryColor.Enabled = False
        Me.cboPrimaryColor.Location = New System.Drawing.Point(224, 104)
        Me.cboPrimaryColor.Name = "cboPrimaryColor"
        Me.cboPrimaryColor.Size = New System.Drawing.Size(192, 21)
        Me.cboPrimaryColor.TabIndex = 10
        '
        'cboSatValue
        '
        Me.cboSatValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSatValue.Enabled = False
        Me.cboSatValue.Location = New System.Drawing.Point(224, 72)
        Me.cboSatValue.Name = "cboSatValue"
        Me.cboSatValue.Size = New System.Drawing.Size(192, 21)
        Me.cboSatValue.TabIndex = 13
        '
        'lblSatValue
        '
        Me.lblSatValue.Enabled = False
        Me.lblSatValue.Location = New System.Drawing.Point(136, 72)
        Me.lblSatValue.Name = "lblSatValue"
        Me.lblSatValue.Size = New System.Drawing.Size(88, 24)
        Me.lblSatValue.TabIndex = 14
        Me.lblSatValue.Text = "Saturation/Value"
        '
        'lblSecondaryColor
        '
        Me.lblSecondaryColor.Enabled = False
        Me.lblSecondaryColor.Location = New System.Drawing.Point(136, 128)
        Me.lblSecondaryColor.Name = "lblSecondaryColor"
        Me.lblSecondaryColor.Size = New System.Drawing.Size(88, 24)
        Me.lblSecondaryColor.TabIndex = 16
        Me.lblSecondaryColor.Text = "Color 2"
        '
        'cboSecondaryColor
        '
        Me.cboSecondaryColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSecondaryColor.Enabled = False
        Me.cboSecondaryColor.Location = New System.Drawing.Point(224, 128)
        Me.cboSecondaryColor.Name = "cboSecondaryColor"
        Me.cboSecondaryColor.Size = New System.Drawing.Size(192, 21)
        Me.cboSecondaryColor.TabIndex = 15
        '
        'chkSize
        '
        Me.chkSize.Location = New System.Drawing.Point(8, 160)
        Me.chkSize.Name = "chkSize"
        Me.chkSize.Size = New System.Drawing.Size(152, 24)
        Me.chkSize.TabIndex = 18
        Me.chkSize.Text = "Size"
        '
        'chkRotation
        '
        Me.chkRotation.Location = New System.Drawing.Point(8, 192)
        Me.chkRotation.Name = "chkRotation"
        Me.chkRotation.Size = New System.Drawing.Size(152, 24)
        Me.chkRotation.TabIndex = 19
        Me.chkRotation.Text = "Rotation"
        '
        'butRotation
        '
        Me.butRotation.Enabled = False
        Me.butRotation.Location = New System.Drawing.Point(224, 192)
        Me.butRotation.Name = "butRotation"
        Me.butRotation.Size = New System.Drawing.Size(192, 24)
        Me.butRotation.TabIndex = 21
        Me.butRotation.Text = "Properties"
        '
        'cboSize1
        '
        Me.cboSize1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSize1.Enabled = False
        Me.cboSize1.Location = New System.Drawing.Point(224, 160)
        Me.cboSize1.Name = "cboSize1"
        Me.cboSize1.Size = New System.Drawing.Size(192, 21)
        Me.cboSize1.TabIndex = 23
        '
        'PropPageForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(424, 285)
        Me.Controls.Add(Me.cboSize1)
        Me.Controls.Add(Me.butRotation)
        Me.Controls.Add(Me.chkRotation)
        Me.Controls.Add(Me.chkSize)
        Me.Controls.Add(Me.lblSecondaryColor)
        Me.Controls.Add(Me.cboSecondaryColor)
        Me.Controls.Add(Me.lblSatValue)
        Me.Controls.Add(Me.cboSatValue)
        Me.Controls.Add(Me.lblPrimaryColor)
        Me.Controls.Add(Me.radCombination)
        Me.Controls.Add(Me.cboPrimaryColor)
        Me.Controls.Add(Me.lblHue)
        Me.Controls.Add(Me.radComponents)
        Me.Controls.Add(Me.chkColor)
        Me.Controls.Add(Me.chkShapePattern)
        Me.Controls.Add(Me.cboHue)
        Me.Controls.Add(Me.cboShapePattern)
        Me.Name = "PropPageForm"
        Me.Text = "ColorPropPageForm"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public ReadOnly Property IsDirty() As Boolean
        Get
            Return m_PageIsDirty
        End Get
    End Property

    Public WriteOnly Property PageSite() As IComPropertyPageSite
        Set(ByVal Value As IComPropertyPageSite)
            m_pSite = Value
        End Set
    End Property

    Public Sub InitControls(ByVal pMultiRend As IMultivariateRenderer, ByVal pMap As IMap, ByVal pGeoLayer As IGeoFeatureLayer)

        m_eColorCombinationMethod = pMultiRend.ColorCombinationMethod
        m_pShapePatternRend = pMultiRend.ShapePatternRend
        m_pColorRend1 = pMultiRend.ColorRend1
        m_pColorRend2 = pMultiRend.ColorRend2
        m_pSizeRend = pMultiRend.SizeRend

        If Not m_pShapePatternRend Is Nothing Then
            chkShapePattern.CheckState = Windows.Forms.CheckState.Checked
            cboShapePattern.Enabled = True
        End If

        If m_eColorCombinationMethod = EColorCombinationType.enuComponents Then
            radComponents.Checked = True
            radCombination.Checked = False
            UpdateColorComb()

        Else
            'disabled
            'radComponents.Checked = False
            'radCombination.Checked = True
            radComponents.Checked = True
            radCombination.Checked = False
            'added to disable color combination
            m_eColorCombinationMethod = EColorCombinationType.enuComponents
            UpdateColorComb()

        End If

        If Not m_pColorRend1 Is Nothing Then
            chkColor.CheckState = Windows.Forms.CheckState.Checked
            radComponents.Enabled = True
            'disabled
            'radCombination.Enabled = True
            radCombination.Enabled = False

        End If
        If Not m_pSizeRend Is Nothing Then
            chkSize.CheckState = Windows.Forms.CheckState.Checked
            cboSize1.Enabled = True
        End If

        Dim pRotRend As IRotationRenderer
        pRotRend = pMultiRend
        If pRotRend.RotationField <> "" Then
            chkRotation.CheckState = Windows.Forms.CheckState.Checked
            butRotation.Enabled = True
        End If

        'Dim pTransRend As ITransparencyRenderer
        'pTransRend = pMultiRend
        'If pTransRend.TransparencyField <> "" Then
        'chkTransparency.CheckState = Windows.Forms.CheckState.Checked
        'butTransparency.Enabled = True
        'End If

        m_pMap = pMap
        m_pCurrentLayer = pGeoLayer
        m_pRend = pMultiRend         ' we need this object to support the root transparency dialogs

        m_PageIsDirty = False
    End Sub

    Public Sub InitRenderer(ByVal pMultiRend As IMultivariateRenderer)
        ' copy properties from the form to the renderer

        If chkShapePattern.CheckState = Windows.Forms.CheckState.Checked Then
            pMultiRend.ShapePatternRend = m_pShapePatternRend
        Else
            pMultiRend.ShapePatternRend = Nothing
        End If

        If chkColor.CheckState = Windows.Forms.CheckState.Checked Then
            pMultiRend.ColorRend1 = m_pColorRend1
            pMultiRend.ColorRend2 = m_pColorRend2
            pMultiRend.ColorCombinationMethod = m_eColorCombinationMethod
        Else
            pMultiRend.ColorRend1 = Nothing
            pMultiRend.ColorRend2 = Nothing
            pMultiRend.ColorCombinationMethod = EColorCombinationType.enuCIELabMatrix ' default (?)
        End If

        If chkSize.CheckState = Windows.Forms.CheckState.Checked Then
            pMultiRend.SizeRend = m_pSizeRend
        Else
            pMultiRend.SizeRend = Nothing
        End If

        Dim pRotRend As IRotationRenderer
        Dim pFormRotRend As IRotationRenderer
        pRotRend = pMultiRend
        If chkRotation.CheckState = Windows.Forms.CheckState.Checked Then
            pFormRotRend = m_pRend
            pRotRend.RotationField = pFormRotRend.RotationField
            pRotRend.RotationType = pFormRotRend.RotationType
        Else
            pRotRend.RotationField = ""
            pRotRend.RotationType = esriSymbolRotationType.esriRotateSymbolArithmetic ' default (?)
        End If

        'Dim pTransRend As ITransparencyRenderer
        'Dim pFormTransRend As ITransparencyRenderer
        'pTransRend = pMultiRend
        'If chkTransparency.CheckState = Windows.Forms.CheckState.Checked Then
        '    pFormTransRend = m_pRend
        '    pTransRend.TransparencyField = pFormTransRend.TransparencyField
        'Else
        '    pTransRend.TransparencyField = ""
        'End If


    End Sub

    Private Sub PropPageForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' intialize form controls from data members

        Dim pGeoLayers As IEnumLayer
        Dim pUID As New UID
        pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
        pGeoLayers = m_pMap.Layers(pUID, True)
        pGeoLayers.Reset()
        Dim pGeoLayer As IGeoFeatureLayer
        Dim pFeatRend As IFeatureRenderer
        Dim iColor As Integer = 0
        Dim iShapePattern As Integer = 0
        Dim iSize As Integer = 0

        pGeoLayer = pGeoLayers.Next
        Dim sColor1 As String = ""
        Dim sColor2 As String = ""
        Dim sShapePattern As String = ""
        Dim sSize As String = ""

        Do While Not pGeoLayer Is Nothing
            ' to keep things simple, filter for layers with same feat class geometry (point, line, poly) as current layer
            If (pGeoLayer.FeatureClass.ShapeType) = (m_pCurrentLayer.FeatureClass.ShapeType) Then
                ' filter out the current layer
                If (Not pGeoLayer Is m_pCurrentLayer) Then
                    pFeatRend = pGeoLayer.Renderer

                    ' filter for only layers currently assigned a renderer that is valid for each renderer type (shape, color, size)
                    If RendererIsValidForType(pFeatRend, eRendererType.eColor) Then
                        iColor = iColor + 1
                        ReDim Preserve m_pColorRendList(iColor)
                        m_pColorRendList(iColor - 1) = pFeatRend

                        cboHue.Items.Add(pGeoLayer.Name)
                        cboSatValue.Items.Add(pGeoLayer.Name)
                        cboPrimaryColor.Items.Add(pGeoLayer.Name)
                        cboSecondaryColor.Items.Add(pGeoLayer.Name)

                        If CompareRenderers(pGeoLayer.Renderer, m_pColorRend1) Then sColor1 = pGeoLayer.Name
                        If CompareRenderers(pGeoLayer.Renderer, m_pColorRend2) Then sColor2 = pGeoLayer.Name
                    End If

                    If RendererIsValidForType(pFeatRend, eRendererType.eShapePattern) Then
                        iShapePattern = iShapePattern + 1
                        ReDim Preserve m_pShapePatternRendList(iShapePattern)
                        m_pShapePatternRendList(iShapePattern - 1) = pFeatRend

                        cboShapePattern.Items.Add(pGeoLayer.Name)

                        'If pGeoLayer.Renderer Is m_pShapePatternRend Then sShapePattern = pGeoLayer.Name
                        If CompareRenderers(pGeoLayer.Renderer, m_pShapePatternRend) Then sShapePattern = pGeoLayer.Name
                    End If

                    If RendererIsValidForType(pFeatRend, eRendererType.eSize) Then
                        iSize = iSize + 1
                        ReDim Preserve m_pSizeRendList(iSize)
                        m_pSizeRendList(iSize - 1) = pFeatRend

                        cboSize1.Items.Add(pGeoLayer.Name)

                        'If pGeoLayer.Renderer Is m_pSizeRend Then sSize = pGeoLayer.Name
                        If CompareRenderers(pGeoLayer.Renderer, m_pSizeRend) Then sSize = pGeoLayer.Name
                    End If

                End If
            End If
            pGeoLayer = pGeoLayers.Next
        Loop

        ' select correct items in combos
        cboShapePattern().Text = sShapePattern
        If radComponents.Checked Then
            cboHue.Text() = sColor1
            cboSatValue.Text() = sColor2
        Else
            cboPrimaryColor.Text() = sColor1
            cboSecondaryColor.Text() = sColor2
        End If
        'TESTING:

        cboSize1.Text() = sSize
        'TESTING:
        cboShapePattern.Text() = sShapePattern
        ' disable if there aren't any layers in the map of the correct type
        If iShapePattern <= 0 Then cboShapePattern.Enabled = False

        If iColor <= 0 Then
            If radComponents.Checked Then
                cboHue.Enabled = False
                cboSatValue.Enabled = False
            Else
                cboPrimaryColor.Enabled = False
                cboSecondaryColor.Enabled = False
            End If
        End If

        If iSize <= 0 Then cboSize1.Enabled = False
    End Sub

    Private Sub cboShapePattern_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShapePattern.SelectedIndexChanged
        m_pShapePatternRend = m_pShapePatternRendList(cboShapePattern.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub cboHue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboHue.SelectedIndexChanged
        m_pColorRend1 = m_pColorRendList(cboHue.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub cboSatValue_selectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSatValue.SelectedIndexChanged
        m_pColorRend2 = m_pColorRendList(cboSatValue.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub cboPrimaryColor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrimaryColor.SelectedIndexChanged
        m_pColorRend1 = m_pColorRendList(cboPrimaryColor.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub cboSecondaryColor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSecondaryColor.SelectedIndexChanged
        m_pColorRend2 = m_pColorRendList(cboSecondaryColor.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub cboSize1_selectedindexchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSize1.SelectedIndexChanged
        m_pSizeRend = m_pSizeRendList(cboSize1.SelectedIndex)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub radComponents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radComponents.CheckedChanged
        UpdateColorComb()
    End Sub

    Private Sub radCombination_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radCombination.CheckedChanged
        UpdateColorComb()
    End Sub

    Private Sub UpdateColorComb()

        If radComponents.Checked Then
            m_eColorCombinationMethod = EColorCombinationType.enuComponents

            cboHue.Enabled = True
            cboSatValue.Enabled = True
            cboPrimaryColor.Enabled = False
            cboSecondaryColor.Enabled = False
        Else
            m_eColorCombinationMethod = EColorCombinationType.enuCIELabMatrix

            cboHue.Enabled = False
            cboSatValue.Enabled = False
            cboPrimaryColor.Enabled = True
            cboSecondaryColor.Enabled = True
        End If

        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True

    End Sub

    Private Function RendererIsValidForType(ByVal pFeatRend As IFeatureRenderer, ByVal eMultiRendType As eRendererType) As Boolean
        ' indicates whether or not pFeatRend is valid for the eMultiRendType for the current layer
        ' e.g. if pFeatRend is an IProportionalSymbolRenderer, then it's valid for eMultiRendType = eSize 

        Dim pLegendInfo As ILegendInfo

        If eMultiRendType = eRendererType.eShapePattern Then
            Return TypeOf pFeatRend Is IUniqueValueRenderer
        ElseIf eMultiRendType = eRendererType.eColor Then
            pLegendInfo = pFeatRend
            Return (TypeOf pFeatRend Is IUniqueValueRenderer) Or (TypeOf pFeatRend Is IClassBreaksRenderer And Not pLegendInfo.SymbolsAreGraduated)
        Else ' size
            pLegendInfo = pFeatRend
            Return (TypeOf pFeatRend Is IClassBreaksRenderer And pLegendInfo.SymbolsAreGraduated) Or (TypeOf pFeatRend Is IProportionalSymbolRenderer)
        End If

    End Function

    Private Sub butRotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRotation.Click
        Dim pRendUIDlg2 As IRendererUIDialog2
        pRendUIDlg2 = New MarkerRotationDialog
        pRendUIDlg2.FeatureLayer = m_pCurrentLayer
        pRendUIDlg2.Renderer = m_pRend
        Dim pMyForm As SecondaryForm
        pMyForm = New SecondaryForm
        pRendUIDlg2.DoModal(pMyForm.Handle.ToInt32)

    End Sub

    Private Sub chkShapePattern_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShapePattern.CheckedChanged
        cboShapePattern.Enabled = (chkShapePattern.CheckState = Windows.Forms.CheckState.Checked)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub chkColor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkColor.CheckedChanged
        'combination is disabled
        'radComponents.Enabled = (chkColor.CheckState = Windows.Forms.CheckState.Checked)
        'radCombination.Enabled = (chkColor.CheckState = Windows.Forms.CheckState.Checked)
        radComponents.Enabled = (chkColor.CheckState = Windows.Forms.CheckState.Checked)
        radCombination.Enabled = False
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    Private Sub chkSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSize.CheckedChanged
        cboSize1.Enabled = (chkSize.CheckState = Windows.Forms.CheckState.Checked)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub


    Private Sub chkRotation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRotation.CheckedChanged
        butRotation.Enabled = (chkRotation.CheckState = Windows.Forms.CheckState.Checked)
        If Not m_pSite Is Nothing Then m_pSite.PageChanged()
        m_PageIsDirty = True
    End Sub

    'Private Sub chkTransparency_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '   butTransparency.Enabled = (chkTransparency.CheckState = Windows.Forms.CheckState.Checked)
    '  If Not m_pSite Is Nothing Then m_pSite.PageChanged()
    ' m_PageIsDirty = True
    'End Sub

    Private Sub radComponents_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radComponents.EnabledChanged

        cboHue.Enabled = radComponents.Enabled And radComponents.Checked
        cboSatValue.Enabled = radComponents.Enabled And radComponents.Checked
    End Sub

    Private Sub radCombination_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radCombination.EnabledChanged
        'disabled
        'cboPrimaryColor.Enabled = radCombination.Enabled And radCombination.Checked
        'cboSecondaryColor.Enabled = radCombination.Enabled And radCombination.Checked
        cboPrimaryColor.Enabled = False
        cboSecondaryColor.Enabled = False
    End Sub

    Private Sub cboHue_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboHue.EnabledChanged

        lblHue.Enabled = cboHue.Enabled
    End Sub

    Private Sub cboSatValue_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSatValue.EnabledChanged

        lblSatValue.Enabled = cboSatValue.Enabled
    End Sub

    Private Sub cboPrimaryColor_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPrimaryColor.EnabledChanged

        lblPrimaryColor.Enabled = cboPrimaryColor.Enabled
    End Sub

    Private Sub cboSecondaryColor_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSecondaryColor.EnabledChanged

        lblSecondaryColor.Enabled = cboSecondaryColor.Enabled
    End Sub

    Private Function CompareRenderers(ByVal pRend As IFeatureRenderer, ByVal pCheckRend As IFeatureRenderer) As Boolean

        If TypeOf pRend Is IClassBreaksRenderer Then
            ' type
            If Not TypeOf pCheckRend Is IClassBreaksRenderer Then Return False

            Dim pCBRend As IClassBreaksRenderer
            pCBRend = pRend
            Dim pCBCheckRend As IClassBreaksRenderer
            pCBCheckRend = pCheckRend

            ' break count
            If pCBRend.BreakCount <> pCBCheckRend.BreakCount Then Return False

            ' field
            If pCBRend.Field <> pCBCheckRend.Field Then Return False

        End If

        Return True

    End Function
End Class