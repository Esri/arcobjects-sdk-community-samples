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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports System.Collections.Generic

' This form allows users to change the NALayer/NAContext/NASolver properties

Namespace NAEngine
	''' <summary>
	''' Summary description for frmNALayerProperties.
	''' </summary>
	Public Class frmNALayerProperties
		Inherits System.Windows.Forms.Form
		#Region "Windows Form Designer generated code (defining controls)"
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private tabPropPages As TabControl
		Private tabGeneral As TabPage
		Private tabRoute As TabPage
		Private tabServiceArea As TabPage
		Private tabClosestFacility As TabPage
		Private tabODCostMatrix As TabPage
		Private tabVRP As TabPage
		Private WithEvents btnOK As Button
		Private WithEvents btnCancel As Button
		Private chkRouteIgnoreInvalidLocations As CheckBox
		Private cboRouteRestrictUTurns As ComboBox
		Private lblRouteRestrictUTurns As Label
		Private lblRouteAccumulateAttributeNames As Label
		Private chklstRouteAccumulateAttributeNames As CheckedListBox
		Private lblRouteRestrictionAttributeNames As Label
		Private chklstRouteRestrictionAttributeNames As CheckedListBox
		Private cboRouteImpedance As ComboBox
		Private lblRouteImpedance As Label
		Private chkRouteUseHierarchy As CheckBox
		Private labelRouteOutputLines As Label
		Private cboRouteOutputLines As ComboBox
		Private chkRouteUseTimeWindows As CheckBox
		Private chkRoutePreserveLastStop As CheckBox
		Private chkRoutePreserveFirstStop As CheckBox
		Private WithEvents chkRouteFindBestSequence As CheckBox
		Private WithEvents chkRouteUseStartTime As CheckBox
		Private txtRouteStartTime As TextBox
		Private txtLayerName As TextBox
		Private lblLayerName As Label
		Private components As System.ComponentModel.Container = Nothing
		Private cboCFOutputLines As ComboBox
		Private lblCFOutputLines As Label
		Private cboCFTravelDirection As ComboBox
		Private lblCFTravelDirection As Label
		Private txtCFDefaultTargetFacilityCount As TextBox
		Private lblCFDefaultTargetFacilityCount As Label
		Private txtCFDefaultCutoff As TextBox
		Private lblCFDefaultCutoff As Label
		Private chkCFIgnoreInvalidLocations As CheckBox
		Private cboCFRestrictUTurns As ComboBox
		Private lblCFRestrictUTurns As Label
		Private lblCFAccumulateAttributeNames As Label
		Private chklstCFAccumulateAttributeNames As CheckedListBox
		Private lblCFRestrictionAttributeNames As Label
		Private chklstCFRestrictionAttributeNames As CheckedListBox
		Private cboCFImpedance As ComboBox
		Private lblCFImpedance As Label
		Private chkCFUseHierarchy As CheckBox
		Private chkODIgnoreInvalidLocations As CheckBox
		Private cboODRestrictUTurns As ComboBox
		Private lblODRestrictUTurns As Label
		Private lblODAccumulateAttributeNames As Label
		Private chklstODAccumulateAttributeNames As CheckedListBox
		Private lblODRestrictionAttributeNames As Label
		Private chklstODRestrictionAttributeNames As CheckedListBox
		Private cboODImpedance As ComboBox
		Private lblODImpedance As Label
		Private chkODUseHierarchy As CheckBox
		Private cboODOutputLines As ComboBox
		Private lblODOutputLines As Label
		Private txtODDefaultTargetDestinationCount As TextBox
		Private lblODDefaultTargetDestinationCount As Label
		Private txtODDefaultCutoff As TextBox
		Private lblODDefaultCutoff As Label
		Private txtSADefaultBreaks As TextBox
		Private lblSADefaultBreaks As Label
		Private cboSAImpedance As ComboBox
		Private lblSAImpedance As Label
		Private lblSAOutputPolygons As Label
		Private WithEvents cboSAOutputPolygons As ComboBox
		Private lblSAOutputLines As Label
		Private WithEvents cboSAOutputLines As ComboBox
		Private chkSAMergeSimilarPolygonRanges As CheckBox
		Private chkSAIgnoreInvalidLocations As CheckBox
		Private cboSARestrictUTurns As ComboBox
		Private lblSARestrictUTurns As Label
		Private lblSAAccumulateAttributeNames As Label
		Private chklstSAAccumulateAttributeNames As CheckedListBox
		Private lblSARestrictionAttributeNames As Label
		Private chklstSARestrictionAttributeNames As CheckedListBox
		Private chkSAOverlapLines As CheckBox
		Private chkSASplitPolygonsAtBreaks As CheckBox
		Private chkSAOverlapPolygons As CheckBox
		Private chkSASplitLinesAtBreaks As CheckBox
		Private cboSATrimPolygonDistanceUnits As ComboBox
		Private txtSATrimPolygonDistance As TextBox
		Private chkSATrimOuterPolygon As CheckBox
		Private chkSAIncludeSourceInformationOnLines As CheckBox
		Private cboSATravelDirection As ComboBox
		Private lblSATravelDirection As Label
		Private lblMaxSearchTolerance As Label
		Private cboMaxSearchToleranceUnits As ComboBox
		Private gbSettings As GroupBox
		Private chkVRPUseHierarchy As CheckBox
		Private cboVRPOutputShapeType As ComboBox
		Private cboVRPAllowUTurns As ComboBox
		Private cboVRPTimeFieldUnits As ComboBox
		Private txtVRPCapacityCount As TextBox
		Private txtVRPDefaultDate As TextBox
		Private cboVRPDistanceAttribute As ComboBox
		Private cboVRPTimeAttribute As ComboBox
		Private label7 As Label
		Private label6 As Label
		Private label5 As Label
		Private label4 As Label
		Private label3 As Label
		Private label2 As Label
		Private label1 As Label
		Private lblTimeAttribute As Label
		Private gbRestrictions As GroupBox
		Private chklstVRPRestrictionAttributeNames As CheckedListBox
		Private cboVRPTimeWindow As ComboBox
		Private label10 As Label
		Private label9 As Label
		Private cboVRPTransitTime As ComboBox
		Private cboVRPDistanceFieldUnits As ComboBox
		Private tabLocationAllocation As TabPage
		Private lblTargetMarketShare As Label
		Private txtLATargetMarketShare As TextBox
		Private cboLAImpTransformation As ComboBox
		Private lblImpParameter As Label
		Private txtLAImpParameter As TextBox
		Private lblImpTransformation As Label
		Private lblCostAttribute As Label
		Private cboLAImpedance As ComboBox
		Private lblProblemType As Label
		Private WithEvents cboLAProblemType As ComboBox
		Private lblCutOff As Label
		Private txtLACutOff As TextBox
		Private lblNumFacilities As Label
		Private txtLAFacilitiesToLocate As TextBox
		Private cboLAOutputLines As ComboBox
		Private label11 As Label
		Private cboLATravelDirection As ComboBox
		Private label12 As Label
		Private lblLAAccumulateAttributeNames As Label
		Private chklstLAAccumulateAttributeNames As CheckedListBox
		Private lblLARestrictionAttributeNames As Label
		Private chklstLARestrictionAttributeNames As CheckedListBox
		Private chkLAUseHierarchy As CheckBox
		Private grpLASettings As GroupBox
        Private chkLAIgnoreInvalidLocations As CheckBox
        Private WithEvents label13 As System.Windows.Forms.Label
        Private WithEvents label8 As System.Windows.Forms.Label
        Private WithEvents cboCFTimeUsage As System.Windows.Forms.ComboBox
        Private WithEvents txtCFUseTime As System.Windows.Forms.TextBox
        Private WithEvents chkODUseTime As System.Windows.Forms.CheckBox
        Private WithEvents txtODUseTime As System.Windows.Forms.TextBox
        Private WithEvents chkSAUseTime As System.Windows.Forms.CheckBox
        Private WithEvents txtSAUseTime As System.Windows.Forms.TextBox
        Private WithEvents chkLAUseTime As System.Windows.Forms.CheckBox
        Private WithEvents txtLAUseTime As System.Windows.Forms.TextBox
        Friend WithEvents tabAttributeParameters As System.Windows.Forms.TabPage
        Private WithEvents attributeParameterGrid As System.Windows.Forms.DataGridView
        Private WithEvents dgvcAttribute As System.Windows.Forms.DataGridViewTextBoxColumn
        Private WithEvents dgvcParameter As System.Windows.Forms.DataGridViewTextBoxColumn
        Private WithEvents dgvcValue As System.Windows.Forms.DataGridViewTextBoxColumn
        Private WithEvents label14 As System.Windows.Forms.Label
        Private WithEvents btnReset As System.Windows.Forms.Button
        Private txtMaxSearchTolerance As TextBox

#End Region

#Region "Windows Form Designer generated code (InitializeComponent)"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.tabPropPages = New System.Windows.Forms.TabControl()
            Me.tabGeneral = New System.Windows.Forms.TabPage()
            Me.lblMaxSearchTolerance = New System.Windows.Forms.Label()
            Me.cboMaxSearchToleranceUnits = New System.Windows.Forms.ComboBox()
            Me.txtMaxSearchTolerance = New System.Windows.Forms.TextBox()
            Me.txtLayerName = New System.Windows.Forms.TextBox()
            Me.lblLayerName = New System.Windows.Forms.Label()
            Me.tabRoute = New System.Windows.Forms.TabPage()
            Me.labelRouteOutputLines = New System.Windows.Forms.Label()
            Me.cboRouteOutputLines = New System.Windows.Forms.ComboBox()
            Me.chkRouteUseTimeWindows = New System.Windows.Forms.CheckBox()
            Me.chkRoutePreserveLastStop = New System.Windows.Forms.CheckBox()
            Me.chkRoutePreserveFirstStop = New System.Windows.Forms.CheckBox()
            Me.chkRouteFindBestSequence = New System.Windows.Forms.CheckBox()
            Me.chkRouteUseStartTime = New System.Windows.Forms.CheckBox()
            Me.txtRouteStartTime = New System.Windows.Forms.TextBox()
            Me.chkRouteIgnoreInvalidLocations = New System.Windows.Forms.CheckBox()
            Me.cboRouteRestrictUTurns = New System.Windows.Forms.ComboBox()
            Me.lblRouteRestrictUTurns = New System.Windows.Forms.Label()
            Me.lblRouteAccumulateAttributeNames = New System.Windows.Forms.Label()
            Me.chklstRouteAccumulateAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblRouteRestrictionAttributeNames = New System.Windows.Forms.Label()
            Me.chklstRouteRestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.cboRouteImpedance = New System.Windows.Forms.ComboBox()
            Me.lblRouteImpedance = New System.Windows.Forms.Label()
            Me.chkRouteUseHierarchy = New System.Windows.Forms.CheckBox()
            Me.tabClosestFacility = New System.Windows.Forms.TabPage()
            Me.label13 = New System.Windows.Forms.Label()
            Me.label8 = New System.Windows.Forms.Label()
            Me.cboCFTimeUsage = New System.Windows.Forms.ComboBox()
            Me.txtCFUseTime = New System.Windows.Forms.TextBox()
            Me.chkCFIgnoreInvalidLocations = New System.Windows.Forms.CheckBox()
            Me.cboCFRestrictUTurns = New System.Windows.Forms.ComboBox()
            Me.lblCFRestrictUTurns = New System.Windows.Forms.Label()
            Me.lblCFAccumulateAttributeNames = New System.Windows.Forms.Label()
            Me.chklstCFAccumulateAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblCFRestrictionAttributeNames = New System.Windows.Forms.Label()
            Me.chklstCFRestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.cboCFImpedance = New System.Windows.Forms.ComboBox()
            Me.lblCFImpedance = New System.Windows.Forms.Label()
            Me.chkCFUseHierarchy = New System.Windows.Forms.CheckBox()
            Me.cboCFOutputLines = New System.Windows.Forms.ComboBox()
            Me.lblCFOutputLines = New System.Windows.Forms.Label()
            Me.cboCFTravelDirection = New System.Windows.Forms.ComboBox()
            Me.lblCFTravelDirection = New System.Windows.Forms.Label()
            Me.txtCFDefaultTargetFacilityCount = New System.Windows.Forms.TextBox()
            Me.lblCFDefaultTargetFacilityCount = New System.Windows.Forms.Label()
            Me.txtCFDefaultCutoff = New System.Windows.Forms.TextBox()
            Me.lblCFDefaultCutoff = New System.Windows.Forms.Label()
            Me.tabODCostMatrix = New System.Windows.Forms.TabPage()
            Me.chkODUseTime = New System.Windows.Forms.CheckBox()
            Me.txtODUseTime = New System.Windows.Forms.TextBox()
            Me.chkODIgnoreInvalidLocations = New System.Windows.Forms.CheckBox()
            Me.cboODRestrictUTurns = New System.Windows.Forms.ComboBox()
            Me.lblODRestrictUTurns = New System.Windows.Forms.Label()
            Me.lblODAccumulateAttributeNames = New System.Windows.Forms.Label()
            Me.chklstODAccumulateAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblODRestrictionAttributeNames = New System.Windows.Forms.Label()
            Me.chklstODRestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.cboODImpedance = New System.Windows.Forms.ComboBox()
            Me.lblODImpedance = New System.Windows.Forms.Label()
            Me.chkODUseHierarchy = New System.Windows.Forms.CheckBox()
            Me.cboODOutputLines = New System.Windows.Forms.ComboBox()
            Me.lblODOutputLines = New System.Windows.Forms.Label()
            Me.txtODDefaultTargetDestinationCount = New System.Windows.Forms.TextBox()
            Me.lblODDefaultTargetDestinationCount = New System.Windows.Forms.Label()
            Me.txtODDefaultCutoff = New System.Windows.Forms.TextBox()
            Me.lblODDefaultCutoff = New System.Windows.Forms.Label()
            Me.tabServiceArea = New System.Windows.Forms.TabPage()
            Me.chkSAUseTime = New System.Windows.Forms.CheckBox()
            Me.txtSAUseTime = New System.Windows.Forms.TextBox()
            Me.cboSATrimPolygonDistanceUnits = New System.Windows.Forms.ComboBox()
            Me.txtSATrimPolygonDistance = New System.Windows.Forms.TextBox()
            Me.chkSATrimOuterPolygon = New System.Windows.Forms.CheckBox()
            Me.chkSAIncludeSourceInformationOnLines = New System.Windows.Forms.CheckBox()
            Me.cboSATravelDirection = New System.Windows.Forms.ComboBox()
            Me.lblSATravelDirection = New System.Windows.Forms.Label()
            Me.chkSASplitPolygonsAtBreaks = New System.Windows.Forms.CheckBox()
            Me.chkSAOverlapPolygons = New System.Windows.Forms.CheckBox()
            Me.chkSASplitLinesAtBreaks = New System.Windows.Forms.CheckBox()
            Me.chkSAOverlapLines = New System.Windows.Forms.CheckBox()
            Me.chkSAIgnoreInvalidLocations = New System.Windows.Forms.CheckBox()
            Me.cboSARestrictUTurns = New System.Windows.Forms.ComboBox()
            Me.lblSARestrictUTurns = New System.Windows.Forms.Label()
            Me.lblSAAccumulateAttributeNames = New System.Windows.Forms.Label()
            Me.chklstSAAccumulateAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblSARestrictionAttributeNames = New System.Windows.Forms.Label()
            Me.chklstSARestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblSAOutputPolygons = New System.Windows.Forms.Label()
            Me.cboSAOutputPolygons = New System.Windows.Forms.ComboBox()
            Me.lblSAOutputLines = New System.Windows.Forms.Label()
            Me.cboSAOutputLines = New System.Windows.Forms.ComboBox()
            Me.chkSAMergeSimilarPolygonRanges = New System.Windows.Forms.CheckBox()
            Me.txtSADefaultBreaks = New System.Windows.Forms.TextBox()
            Me.lblSADefaultBreaks = New System.Windows.Forms.Label()
            Me.cboSAImpedance = New System.Windows.Forms.ComboBox()
            Me.lblSAImpedance = New System.Windows.Forms.Label()
            Me.tabVRP = New System.Windows.Forms.TabPage()
            Me.gbRestrictions = New System.Windows.Forms.GroupBox()
            Me.chklstVRPRestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.gbSettings = New System.Windows.Forms.GroupBox()
            Me.cboVRPDistanceFieldUnits = New System.Windows.Forms.ComboBox()
            Me.cboVRPTransitTime = New System.Windows.Forms.ComboBox()
            Me.cboVRPTimeWindow = New System.Windows.Forms.ComboBox()
            Me.label10 = New System.Windows.Forms.Label()
            Me.label9 = New System.Windows.Forms.Label()
            Me.chkVRPUseHierarchy = New System.Windows.Forms.CheckBox()
            Me.cboVRPOutputShapeType = New System.Windows.Forms.ComboBox()
            Me.cboVRPAllowUTurns = New System.Windows.Forms.ComboBox()
            Me.cboVRPTimeFieldUnits = New System.Windows.Forms.ComboBox()
            Me.txtVRPCapacityCount = New System.Windows.Forms.TextBox()
            Me.txtVRPDefaultDate = New System.Windows.Forms.TextBox()
            Me.cboVRPDistanceAttribute = New System.Windows.Forms.ComboBox()
            Me.cboVRPTimeAttribute = New System.Windows.Forms.ComboBox()
            Me.label7 = New System.Windows.Forms.Label()
            Me.label6 = New System.Windows.Forms.Label()
            Me.label5 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.label3 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.lblTimeAttribute = New System.Windows.Forms.Label()
            Me.tabLocationAllocation = New System.Windows.Forms.TabPage()
            Me.chkLAUseTime = New System.Windows.Forms.CheckBox()
            Me.txtLAUseTime = New System.Windows.Forms.TextBox()
            Me.chkLAIgnoreInvalidLocations = New System.Windows.Forms.CheckBox()
            Me.grpLASettings = New System.Windows.Forms.GroupBox()
            Me.lblTargetMarketShare = New System.Windows.Forms.Label()
            Me.txtLATargetMarketShare = New System.Windows.Forms.TextBox()
            Me.cboLAImpTransformation = New System.Windows.Forms.ComboBox()
            Me.lblImpParameter = New System.Windows.Forms.Label()
            Me.txtLAImpParameter = New System.Windows.Forms.TextBox()
            Me.lblImpTransformation = New System.Windows.Forms.Label()
            Me.lblProblemType = New System.Windows.Forms.Label()
            Me.cboLAProblemType = New System.Windows.Forms.ComboBox()
            Me.lblCutOff = New System.Windows.Forms.Label()
            Me.txtLACutOff = New System.Windows.Forms.TextBox()
            Me.lblNumFacilities = New System.Windows.Forms.Label()
            Me.txtLAFacilitiesToLocate = New System.Windows.Forms.TextBox()
            Me.chkLAUseHierarchy = New System.Windows.Forms.CheckBox()
            Me.lblLAAccumulateAttributeNames = New System.Windows.Forms.Label()
            Me.chklstLAAccumulateAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.lblLARestrictionAttributeNames = New System.Windows.Forms.Label()
            Me.chklstLARestrictionAttributeNames = New System.Windows.Forms.CheckedListBox()
            Me.cboLAOutputLines = New System.Windows.Forms.ComboBox()
            Me.label11 = New System.Windows.Forms.Label()
            Me.cboLATravelDirection = New System.Windows.Forms.ComboBox()
            Me.label12 = New System.Windows.Forms.Label()
            Me.lblCostAttribute = New System.Windows.Forms.Label()
            Me.cboLAImpedance = New System.Windows.Forms.ComboBox()
            Me.tabAttributeParameters = New System.Windows.Forms.TabPage()
            Me.btnReset = New System.Windows.Forms.Button()
            Me.attributeParameterGrid = New System.Windows.Forms.DataGridView()
            Me.dgvcAttribute = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.dgvcParameter = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.dgvcValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.label14 = New System.Windows.Forms.Label()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.tabPropPages.SuspendLayout()
            Me.tabGeneral.SuspendLayout()
            Me.tabRoute.SuspendLayout()
            Me.tabClosestFacility.SuspendLayout()
            Me.tabODCostMatrix.SuspendLayout()
            Me.tabServiceArea.SuspendLayout()
            Me.tabVRP.SuspendLayout()
            Me.gbRestrictions.SuspendLayout()
            Me.gbSettings.SuspendLayout()
            Me.tabLocationAllocation.SuspendLayout()
            Me.grpLASettings.SuspendLayout()
            Me.tabAttributeParameters.SuspendLayout()
            CType(Me.attributeParameterGrid, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'tabPropPages
            '
            Me.tabPropPages.Controls.Add(Me.tabGeneral)
            Me.tabPropPages.Controls.Add(Me.tabRoute)
            Me.tabPropPages.Controls.Add(Me.tabClosestFacility)
            Me.tabPropPages.Controls.Add(Me.tabODCostMatrix)
            Me.tabPropPages.Controls.Add(Me.tabServiceArea)
            Me.tabPropPages.Controls.Add(Me.tabVRP)
            Me.tabPropPages.Controls.Add(Me.tabLocationAllocation)
            Me.tabPropPages.Controls.Add(Me.tabAttributeParameters)
            Me.tabPropPages.Location = New System.Drawing.Point(8, 8)
            Me.tabPropPages.Name = "tabPropPages"
            Me.tabPropPages.SelectedIndex = 0
            Me.tabPropPages.Size = New System.Drawing.Size(720, 499)
            Me.tabPropPages.TabIndex = 0
            '
            'tabGeneral
            '
            Me.tabGeneral.Controls.Add(Me.lblMaxSearchTolerance)
            Me.tabGeneral.Controls.Add(Me.cboMaxSearchToleranceUnits)
            Me.tabGeneral.Controls.Add(Me.txtMaxSearchTolerance)
            Me.tabGeneral.Controls.Add(Me.txtLayerName)
            Me.tabGeneral.Controls.Add(Me.lblLayerName)
            Me.tabGeneral.Location = New System.Drawing.Point(4, 22)
            Me.tabGeneral.Name = "tabGeneral"
            Me.tabGeneral.Size = New System.Drawing.Size(712, 473)
            Me.tabGeneral.TabIndex = 0
            Me.tabGeneral.Text = "General"
            Me.tabGeneral.UseVisualStyleBackColor = True
            '
            'lblMaxSearchTolerance
            '
            Me.lblMaxSearchTolerance.Location = New System.Drawing.Point(24, 64)
            Me.lblMaxSearchTolerance.Name = "lblMaxSearchTolerance"
            Me.lblMaxSearchTolerance.Size = New System.Drawing.Size(100, 24)
            Me.lblMaxSearchTolerance.TabIndex = 123
            Me.lblMaxSearchTolerance.Text = "Search Tolerance"
            '
            'cboMaxSearchToleranceUnits
            '
            Me.cboMaxSearchToleranceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboMaxSearchToleranceUnits.ItemHeight = 13
            Me.cboMaxSearchToleranceUnits.Items.AddRange(New Object() {"Unknown Units", "Inches", "Points", "Feet", "Yards", "Miles", "Nautical Miles", "Millimeters", "Centimeters", "Meters", "Kilometers", "DecimalDegrees", "Decimeters"})
            Me.cboMaxSearchToleranceUnits.Location = New System.Drawing.Point(258, 61)
            Me.cboMaxSearchToleranceUnits.Name = "cboMaxSearchToleranceUnits"
            Me.cboMaxSearchToleranceUnits.Size = New System.Drawing.Size(130, 21)
            Me.cboMaxSearchToleranceUnits.TabIndex = 122
            '
            'txtMaxSearchTolerance
            '
            Me.txtMaxSearchTolerance.Location = New System.Drawing.Point(130, 62)
            Me.txtMaxSearchTolerance.Name = "txtMaxSearchTolerance"
            Me.txtMaxSearchTolerance.Size = New System.Drawing.Size(122, 20)
            Me.txtMaxSearchTolerance.TabIndex = 121
            '
            'txtLayerName
            '
            Me.txtLayerName.Location = New System.Drawing.Point(130, 32)
            Me.txtLayerName.Name = "txtLayerName"
            Me.txtLayerName.Size = New System.Drawing.Size(258, 20)
            Me.txtLayerName.TabIndex = 1
            '
            'lblLayerName
            '
            Me.lblLayerName.Location = New System.Drawing.Point(24, 35)
            Me.lblLayerName.Name = "lblLayerName"
            Me.lblLayerName.Size = New System.Drawing.Size(88, 24)
            Me.lblLayerName.TabIndex = 0
            Me.lblLayerName.Text = "Layer Name"
            '
            'tabRoute
            '
            Me.tabRoute.Controls.Add(Me.labelRouteOutputLines)
            Me.tabRoute.Controls.Add(Me.cboRouteOutputLines)
            Me.tabRoute.Controls.Add(Me.chkRouteUseTimeWindows)
            Me.tabRoute.Controls.Add(Me.chkRoutePreserveLastStop)
            Me.tabRoute.Controls.Add(Me.chkRoutePreserveFirstStop)
            Me.tabRoute.Controls.Add(Me.chkRouteFindBestSequence)
            Me.tabRoute.Controls.Add(Me.chkRouteUseStartTime)
            Me.tabRoute.Controls.Add(Me.txtRouteStartTime)
            Me.tabRoute.Controls.Add(Me.chkRouteIgnoreInvalidLocations)
            Me.tabRoute.Controls.Add(Me.cboRouteRestrictUTurns)
            Me.tabRoute.Controls.Add(Me.lblRouteRestrictUTurns)
            Me.tabRoute.Controls.Add(Me.lblRouteAccumulateAttributeNames)
            Me.tabRoute.Controls.Add(Me.chklstRouteAccumulateAttributeNames)
            Me.tabRoute.Controls.Add(Me.lblRouteRestrictionAttributeNames)
            Me.tabRoute.Controls.Add(Me.chklstRouteRestrictionAttributeNames)
            Me.tabRoute.Controls.Add(Me.cboRouteImpedance)
            Me.tabRoute.Controls.Add(Me.lblRouteImpedance)
            Me.tabRoute.Controls.Add(Me.chkRouteUseHierarchy)
            Me.tabRoute.Location = New System.Drawing.Point(4, 22)
            Me.tabRoute.Name = "tabRoute"
            Me.tabRoute.Size = New System.Drawing.Size(712, 473)
            Me.tabRoute.TabIndex = 1
            Me.tabRoute.Text = "Route"
            Me.tabRoute.UseVisualStyleBackColor = True
            '
            'labelRouteOutputLines
            '
            Me.labelRouteOutputLines.Location = New System.Drawing.Point(20, 209)
            Me.labelRouteOutputLines.Name = "labelRouteOutputLines"
            Me.labelRouteOutputLines.Size = New System.Drawing.Size(40, 16)
            Me.labelRouteOutputLines.TabIndex = 96
            Me.labelRouteOutputLines.Text = "Shape"
            '
            'cboRouteOutputLines
            '
            Me.cboRouteOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRouteOutputLines.ItemHeight = 13
            Me.cboRouteOutputLines.Items.AddRange(New Object() {"No Lines", "Straight Lines", "True Shape", "True Shape With Measures"})
            Me.cboRouteOutputLines.Location = New System.Drawing.Point(148, 204)
            Me.cboRouteOutputLines.Name = "cboRouteOutputLines"
            Me.cboRouteOutputLines.Size = New System.Drawing.Size(200, 21)
            Me.cboRouteOutputLines.TabIndex = 95
            '
            'chkRouteUseTimeWindows
            '
            Me.chkRouteUseTimeWindows.Location = New System.Drawing.Point(20, 76)
            Me.chkRouteUseTimeWindows.Name = "chkRouteUseTimeWindows"
            Me.chkRouteUseTimeWindows.Size = New System.Drawing.Size(128, 16)
            Me.chkRouteUseTimeWindows.TabIndex = 92
            Me.chkRouteUseTimeWindows.Text = "Use Time Windows"
            '
            'chkRoutePreserveLastStop
            '
            Me.chkRoutePreserveLastStop.Location = New System.Drawing.Point(39, 151)
            Me.chkRoutePreserveLastStop.Name = "chkRoutePreserveLastStop"
            Me.chkRoutePreserveLastStop.Size = New System.Drawing.Size(331, 23)
            Me.chkRoutePreserveLastStop.TabIndex = 91
            Me.chkRoutePreserveLastStop.Text = "Preserve Last Stop"
            '
            'chkRoutePreserveFirstStop
            '
            Me.chkRoutePreserveFirstStop.Location = New System.Drawing.Point(39, 123)
            Me.chkRoutePreserveFirstStop.Name = "chkRoutePreserveFirstStop"
            Me.chkRoutePreserveFirstStop.Size = New System.Drawing.Size(331, 28)
            Me.chkRoutePreserveFirstStop.TabIndex = 90
            Me.chkRoutePreserveFirstStop.Text = "Preserve First Stop"
            '
            'chkRouteFindBestSequence
            '
            Me.chkRouteFindBestSequence.Checked = True
            Me.chkRouteFindBestSequence.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkRouteFindBestSequence.Location = New System.Drawing.Point(20, 98)
            Me.chkRouteFindBestSequence.Name = "chkRouteFindBestSequence"
            Me.chkRouteFindBestSequence.Size = New System.Drawing.Size(336, 32)
            Me.chkRouteFindBestSequence.TabIndex = 89
            Me.chkRouteFindBestSequence.Text = "Find Best Sequence"
            '
            'chkRouteUseStartTime
            '
            Me.chkRouteUseStartTime.Checked = True
            Me.chkRouteUseStartTime.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkRouteUseStartTime.Location = New System.Drawing.Point(20, 54)
            Me.chkRouteUseStartTime.Name = "chkRouteUseStartTime"
            Me.chkRouteUseStartTime.Size = New System.Drawing.Size(104, 16)
            Me.chkRouteUseStartTime.TabIndex = 93
            Me.chkRouteUseStartTime.Text = "Use Start Time"
            '
            'txtRouteStartTime
            '
            Me.txtRouteStartTime.Location = New System.Drawing.Point(151, 50)
            Me.txtRouteStartTime.Name = "txtRouteStartTime"
            Me.txtRouteStartTime.Size = New System.Drawing.Size(200, 20)
            Me.txtRouteStartTime.TabIndex = 94
            '
            'chkRouteIgnoreInvalidLocations
            '
            Me.chkRouteIgnoreInvalidLocations.Location = New System.Drawing.Point(20, 252)
            Me.chkRouteIgnoreInvalidLocations.Name = "chkRouteIgnoreInvalidLocations"
            Me.chkRouteIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 29)
            Me.chkRouteIgnoreInvalidLocations.TabIndex = 81
            Me.chkRouteIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
            '
            'cboRouteRestrictUTurns
            '
            Me.cboRouteRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRouteRestrictUTurns.ItemHeight = 13
            Me.cboRouteRestrictUTurns.Items.AddRange(New Object() {"No U-Turns", "Allow U-Turns", "Only At Dead Ends"})
            Me.cboRouteRestrictUTurns.Location = New System.Drawing.Point(148, 177)
            Me.cboRouteRestrictUTurns.Name = "cboRouteRestrictUTurns"
            Me.cboRouteRestrictUTurns.Size = New System.Drawing.Size(200, 21)
            Me.cboRouteRestrictUTurns.TabIndex = 80
            '
            'lblRouteRestrictUTurns
            '
            Me.lblRouteRestrictUTurns.Location = New System.Drawing.Point(20, 182)
            Me.lblRouteRestrictUTurns.Name = "lblRouteRestrictUTurns"
            Me.lblRouteRestrictUTurns.Size = New System.Drawing.Size(88, 16)
            Me.lblRouteRestrictUTurns.TabIndex = 88
            Me.lblRouteRestrictUTurns.Text = "UTurn Policy"
            '
            'lblRouteAccumulateAttributeNames
            '
            Me.lblRouteAccumulateAttributeNames.Location = New System.Drawing.Point(236, 284)
            Me.lblRouteAccumulateAttributeNames.Name = "lblRouteAccumulateAttributeNames"
            Me.lblRouteAccumulateAttributeNames.Size = New System.Drawing.Size(120, 16)
            Me.lblRouteAccumulateAttributeNames.TabIndex = 87
            Me.lblRouteAccumulateAttributeNames.Text = "Accumulate Attributes"
            '
            'chklstRouteAccumulateAttributeNames
            '
            Me.chklstRouteAccumulateAttributeNames.CheckOnClick = True
            Me.chklstRouteAccumulateAttributeNames.Location = New System.Drawing.Point(236, 300)
            Me.chklstRouteAccumulateAttributeNames.Name = "chklstRouteAccumulateAttributeNames"
            Me.chklstRouteAccumulateAttributeNames.ScrollAlwaysVisible = True
            Me.chklstRouteAccumulateAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstRouteAccumulateAttributeNames.TabIndex = 84
            '
            'lblRouteRestrictionAttributeNames
            '
            Me.lblRouteRestrictionAttributeNames.Location = New System.Drawing.Point(20, 284)
            Me.lblRouteRestrictionAttributeNames.Name = "lblRouteRestrictionAttributeNames"
            Me.lblRouteRestrictionAttributeNames.Size = New System.Drawing.Size(72, 16)
            Me.lblRouteRestrictionAttributeNames.TabIndex = 86
            Me.lblRouteRestrictionAttributeNames.Text = "Restrictions"
            '
            'chklstRouteRestrictionAttributeNames
            '
            Me.chklstRouteRestrictionAttributeNames.CheckOnClick = True
            Me.chklstRouteRestrictionAttributeNames.Location = New System.Drawing.Point(20, 300)
            Me.chklstRouteRestrictionAttributeNames.Name = "chklstRouteRestrictionAttributeNames"
            Me.chklstRouteRestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstRouteRestrictionAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstRouteRestrictionAttributeNames.TabIndex = 83
            '
            'cboRouteImpedance
            '
            Me.cboRouteImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboRouteImpedance.ItemHeight = 13
            Me.cboRouteImpedance.Location = New System.Drawing.Point(151, 23)
            Me.cboRouteImpedance.Name = "cboRouteImpedance"
            Me.cboRouteImpedance.Size = New System.Drawing.Size(200, 21)
            Me.cboRouteImpedance.TabIndex = 79
            '
            'lblRouteImpedance
            '
            Me.lblRouteImpedance.Location = New System.Drawing.Point(20, 28)
            Me.lblRouteImpedance.Name = "lblRouteImpedance"
            Me.lblRouteImpedance.Size = New System.Drawing.Size(64, 16)
            Me.lblRouteImpedance.TabIndex = 85
            Me.lblRouteImpedance.Text = "Impedance"
            '
            'chkRouteUseHierarchy
            '
            Me.chkRouteUseHierarchy.Location = New System.Drawing.Point(20, 228)
            Me.chkRouteUseHierarchy.Name = "chkRouteUseHierarchy"
            Me.chkRouteUseHierarchy.Size = New System.Drawing.Size(96, 26)
            Me.chkRouteUseHierarchy.TabIndex = 82
            Me.chkRouteUseHierarchy.Text = "Use Hierarchy"
            '
            'tabClosestFacility
            '
            Me.tabClosestFacility.Controls.Add(Me.label13)
            Me.tabClosestFacility.Controls.Add(Me.label8)
            Me.tabClosestFacility.Controls.Add(Me.cboCFTimeUsage)
            Me.tabClosestFacility.Controls.Add(Me.txtCFUseTime)
            Me.tabClosestFacility.Controls.Add(Me.chkCFIgnoreInvalidLocations)
            Me.tabClosestFacility.Controls.Add(Me.cboCFRestrictUTurns)
            Me.tabClosestFacility.Controls.Add(Me.lblCFRestrictUTurns)
            Me.tabClosestFacility.Controls.Add(Me.lblCFAccumulateAttributeNames)
            Me.tabClosestFacility.Controls.Add(Me.chklstCFAccumulateAttributeNames)
            Me.tabClosestFacility.Controls.Add(Me.lblCFRestrictionAttributeNames)
            Me.tabClosestFacility.Controls.Add(Me.chklstCFRestrictionAttributeNames)
            Me.tabClosestFacility.Controls.Add(Me.cboCFImpedance)
            Me.tabClosestFacility.Controls.Add(Me.lblCFImpedance)
            Me.tabClosestFacility.Controls.Add(Me.chkCFUseHierarchy)
            Me.tabClosestFacility.Controls.Add(Me.cboCFOutputLines)
            Me.tabClosestFacility.Controls.Add(Me.lblCFOutputLines)
            Me.tabClosestFacility.Controls.Add(Me.cboCFTravelDirection)
            Me.tabClosestFacility.Controls.Add(Me.lblCFTravelDirection)
            Me.tabClosestFacility.Controls.Add(Me.txtCFDefaultTargetFacilityCount)
            Me.tabClosestFacility.Controls.Add(Me.lblCFDefaultTargetFacilityCount)
            Me.tabClosestFacility.Controls.Add(Me.txtCFDefaultCutoff)
            Me.tabClosestFacility.Controls.Add(Me.lblCFDefaultCutoff)
            Me.tabClosestFacility.Location = New System.Drawing.Point(4, 22)
            Me.tabClosestFacility.Name = "tabClosestFacility"
            Me.tabClosestFacility.Size = New System.Drawing.Size(712, 473)
            Me.tabClosestFacility.TabIndex = 3
            Me.tabClosestFacility.Text = "Closest Facility"
            Me.tabClosestFacility.UseVisualStyleBackColor = True
            '
            'label13
            '
            Me.label13.Location = New System.Drawing.Point(20, 77)
            Me.label13.Name = "label13"
            Me.label13.Size = New System.Drawing.Size(114, 16)
            Me.label13.TabIndex = 121
            Me.label13.Text = "Time"
            '
            'label8
            '
            Me.label8.Location = New System.Drawing.Point(20, 51)
            Me.label8.Name = "label8"
            Me.label8.Size = New System.Drawing.Size(114, 16)
            Me.label8.TabIndex = 120
            Me.label8.Text = "Time Usage"
            '
            'cboCFTimeUsage
            '
            Me.cboCFTimeUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCFTimeUsage.ItemHeight = 13
            Me.cboCFTimeUsage.Items.AddRange(New Object() {"Not used", "End time", "Start time"})
            Me.cboCFTimeUsage.Location = New System.Drawing.Point(151, 48)
            Me.cboCFTimeUsage.Name = "cboCFTimeUsage"
            Me.cboCFTimeUsage.Size = New System.Drawing.Size(200, 21)
            Me.cboCFTimeUsage.TabIndex = 119
            '
            'txtCFUseTime
            '
            Me.txtCFUseTime.Location = New System.Drawing.Point(151, 74)
            Me.txtCFUseTime.Name = "txtCFUseTime"
            Me.txtCFUseTime.Size = New System.Drawing.Size(200, 20)
            Me.txtCFUseTime.TabIndex = 118
            '
            'chkCFIgnoreInvalidLocations
            '
            Me.chkCFIgnoreInvalidLocations.Location = New System.Drawing.Point(20, 269)
            Me.chkCFIgnoreInvalidLocations.Name = "chkCFIgnoreInvalidLocations"
            Me.chkCFIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 29)
            Me.chkCFIgnoreInvalidLocations.TabIndex = 105
            Me.chkCFIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
            '
            'cboCFRestrictUTurns
            '
            Me.cboCFRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCFRestrictUTurns.ItemHeight = 13
            Me.cboCFRestrictUTurns.Items.AddRange(New Object() {"No U-Turns", "Allow U-Turns", "Only At Dead Ends"})
            Me.cboCFRestrictUTurns.Location = New System.Drawing.Point(151, 178)
            Me.cboCFRestrictUTurns.Name = "cboCFRestrictUTurns"
            Me.cboCFRestrictUTurns.Size = New System.Drawing.Size(200, 21)
            Me.cboCFRestrictUTurns.TabIndex = 104
            '
            'lblCFRestrictUTurns
            '
            Me.lblCFRestrictUTurns.Location = New System.Drawing.Point(20, 183)
            Me.lblCFRestrictUTurns.Name = "lblCFRestrictUTurns"
            Me.lblCFRestrictUTurns.Size = New System.Drawing.Size(88, 16)
            Me.lblCFRestrictUTurns.TabIndex = 112
            Me.lblCFRestrictUTurns.Text = "UTurn Policy"
            '
            'lblCFAccumulateAttributeNames
            '
            Me.lblCFAccumulateAttributeNames.Location = New System.Drawing.Point(236, 301)
            Me.lblCFAccumulateAttributeNames.Name = "lblCFAccumulateAttributeNames"
            Me.lblCFAccumulateAttributeNames.Size = New System.Drawing.Size(120, 16)
            Me.lblCFAccumulateAttributeNames.TabIndex = 111
            Me.lblCFAccumulateAttributeNames.Text = "Accumulate Attributes"
            '
            'chklstCFAccumulateAttributeNames
            '
            Me.chklstCFAccumulateAttributeNames.CheckOnClick = True
            Me.chklstCFAccumulateAttributeNames.Location = New System.Drawing.Point(236, 317)
            Me.chklstCFAccumulateAttributeNames.Name = "chklstCFAccumulateAttributeNames"
            Me.chklstCFAccumulateAttributeNames.ScrollAlwaysVisible = True
            Me.chklstCFAccumulateAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstCFAccumulateAttributeNames.TabIndex = 108
            '
            'lblCFRestrictionAttributeNames
            '
            Me.lblCFRestrictionAttributeNames.Location = New System.Drawing.Point(20, 301)
            Me.lblCFRestrictionAttributeNames.Name = "lblCFRestrictionAttributeNames"
            Me.lblCFRestrictionAttributeNames.Size = New System.Drawing.Size(72, 16)
            Me.lblCFRestrictionAttributeNames.TabIndex = 110
            Me.lblCFRestrictionAttributeNames.Text = "Restrictions"
            '
            'chklstCFRestrictionAttributeNames
            '
            Me.chklstCFRestrictionAttributeNames.CheckOnClick = True
            Me.chklstCFRestrictionAttributeNames.Location = New System.Drawing.Point(20, 317)
            Me.chklstCFRestrictionAttributeNames.Name = "chklstCFRestrictionAttributeNames"
            Me.chklstCFRestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstCFRestrictionAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstCFRestrictionAttributeNames.TabIndex = 107
            '
            'cboCFImpedance
            '
            Me.cboCFImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCFImpedance.ItemHeight = 13
            Me.cboCFImpedance.Location = New System.Drawing.Point(151, 23)
            Me.cboCFImpedance.Name = "cboCFImpedance"
            Me.cboCFImpedance.Size = New System.Drawing.Size(200, 21)
            Me.cboCFImpedance.TabIndex = 103
            '
            'lblCFImpedance
            '
            Me.lblCFImpedance.Location = New System.Drawing.Point(20, 28)
            Me.lblCFImpedance.Name = "lblCFImpedance"
            Me.lblCFImpedance.Size = New System.Drawing.Size(64, 16)
            Me.lblCFImpedance.TabIndex = 109
            Me.lblCFImpedance.Text = "Impedance"
            '
            'chkCFUseHierarchy
            '
            Me.chkCFUseHierarchy.Location = New System.Drawing.Point(20, 237)
            Me.chkCFUseHierarchy.Name = "chkCFUseHierarchy"
            Me.chkCFUseHierarchy.Size = New System.Drawing.Size(96, 26)
            Me.chkCFUseHierarchy.TabIndex = 106
            Me.chkCFUseHierarchy.Text = "Use Hierarchy"
            '
            'cboCFOutputLines
            '
            Me.cboCFOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCFOutputLines.ItemHeight = 13
            Me.cboCFOutputLines.Items.AddRange(New Object() {"No Lines", "Straight Lines", "True Shape", "True Shape With Measures"})
            Me.cboCFOutputLines.Location = New System.Drawing.Point(151, 205)
            Me.cboCFOutputLines.Name = "cboCFOutputLines"
            Me.cboCFOutputLines.Size = New System.Drawing.Size(200, 21)
            Me.cboCFOutputLines.TabIndex = 101
            '
            'lblCFOutputLines
            '
            Me.lblCFOutputLines.Location = New System.Drawing.Point(20, 210)
            Me.lblCFOutputLines.Name = "lblCFOutputLines"
            Me.lblCFOutputLines.Size = New System.Drawing.Size(114, 16)
            Me.lblCFOutputLines.TabIndex = 102
            Me.lblCFOutputLines.Text = "Shape"
            '
            'cboCFTravelDirection
            '
            Me.cboCFTravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCFTravelDirection.ItemHeight = 13
            Me.cboCFTravelDirection.Items.AddRange(New Object() {"From Facility", "To Facility"})
            Me.cboCFTravelDirection.Location = New System.Drawing.Point(151, 151)
            Me.cboCFTravelDirection.Name = "cboCFTravelDirection"
            Me.cboCFTravelDirection.Size = New System.Drawing.Size(200, 21)
            Me.cboCFTravelDirection.TabIndex = 99
            '
            'lblCFTravelDirection
            '
            Me.lblCFTravelDirection.Location = New System.Drawing.Point(20, 156)
            Me.lblCFTravelDirection.Name = "lblCFTravelDirection"
            Me.lblCFTravelDirection.Size = New System.Drawing.Size(114, 16)
            Me.lblCFTravelDirection.TabIndex = 100
            Me.lblCFTravelDirection.Text = "Travel Direction"
            '
            'txtCFDefaultTargetFacilityCount
            '
            Me.txtCFDefaultTargetFacilityCount.Location = New System.Drawing.Point(151, 125)
            Me.txtCFDefaultTargetFacilityCount.Name = "txtCFDefaultTargetFacilityCount"
            Me.txtCFDefaultTargetFacilityCount.Size = New System.Drawing.Size(200, 20)
            Me.txtCFDefaultTargetFacilityCount.TabIndex = 98
            '
            'lblCFDefaultTargetFacilityCount
            '
            Me.lblCFDefaultTargetFacilityCount.Location = New System.Drawing.Point(20, 129)
            Me.lblCFDefaultTargetFacilityCount.Name = "lblCFDefaultTargetFacilityCount"
            Me.lblCFDefaultTargetFacilityCount.Size = New System.Drawing.Size(114, 16)
            Me.lblCFDefaultTargetFacilityCount.TabIndex = 97
            Me.lblCFDefaultTargetFacilityCount.Text = "Number of Facilities"
            '
            'txtCFDefaultCutoff
            '
            Me.txtCFDefaultCutoff.Location = New System.Drawing.Point(151, 99)
            Me.txtCFDefaultCutoff.Name = "txtCFDefaultCutoff"
            Me.txtCFDefaultCutoff.Size = New System.Drawing.Size(200, 20)
            Me.txtCFDefaultCutoff.TabIndex = 96
            '
            'lblCFDefaultCutoff
            '
            Me.lblCFDefaultCutoff.Location = New System.Drawing.Point(20, 103)
            Me.lblCFDefaultCutoff.Name = "lblCFDefaultCutoff"
            Me.lblCFDefaultCutoff.Size = New System.Drawing.Size(114, 16)
            Me.lblCFDefaultCutoff.TabIndex = 95
            Me.lblCFDefaultCutoff.Text = "Default Cutoff"
            '
            'tabODCostMatrix
            '
            Me.tabODCostMatrix.Controls.Add(Me.chkODUseTime)
            Me.tabODCostMatrix.Controls.Add(Me.txtODUseTime)
            Me.tabODCostMatrix.Controls.Add(Me.chkODIgnoreInvalidLocations)
            Me.tabODCostMatrix.Controls.Add(Me.cboODRestrictUTurns)
            Me.tabODCostMatrix.Controls.Add(Me.lblODRestrictUTurns)
            Me.tabODCostMatrix.Controls.Add(Me.lblODAccumulateAttributeNames)
            Me.tabODCostMatrix.Controls.Add(Me.chklstODAccumulateAttributeNames)
            Me.tabODCostMatrix.Controls.Add(Me.lblODRestrictionAttributeNames)
            Me.tabODCostMatrix.Controls.Add(Me.chklstODRestrictionAttributeNames)
            Me.tabODCostMatrix.Controls.Add(Me.cboODImpedance)
            Me.tabODCostMatrix.Controls.Add(Me.lblODImpedance)
            Me.tabODCostMatrix.Controls.Add(Me.chkODUseHierarchy)
            Me.tabODCostMatrix.Controls.Add(Me.cboODOutputLines)
            Me.tabODCostMatrix.Controls.Add(Me.lblODOutputLines)
            Me.tabODCostMatrix.Controls.Add(Me.txtODDefaultTargetDestinationCount)
            Me.tabODCostMatrix.Controls.Add(Me.lblODDefaultTargetDestinationCount)
            Me.tabODCostMatrix.Controls.Add(Me.txtODDefaultCutoff)
            Me.tabODCostMatrix.Controls.Add(Me.lblODDefaultCutoff)
            Me.tabODCostMatrix.Location = New System.Drawing.Point(4, 22)
            Me.tabODCostMatrix.Name = "tabODCostMatrix"
            Me.tabODCostMatrix.Size = New System.Drawing.Size(712, 473)
            Me.tabODCostMatrix.TabIndex = 4
            Me.tabODCostMatrix.Text = "Origin-Destination Cost Matrix"
            Me.tabODCostMatrix.UseVisualStyleBackColor = True
            '
            'chkODUseTime
            '
            Me.chkODUseTime.Location = New System.Drawing.Point(20, 50)
            Me.chkODUseTime.Name = "chkODUseTime"
            Me.chkODUseTime.Size = New System.Drawing.Size(104, 16)
            Me.chkODUseTime.TabIndex = 133
            Me.chkODUseTime.Text = "Use Time"
            '
            'txtODUseTime
            '
            Me.txtODUseTime.Location = New System.Drawing.Point(151, 46)
            Me.txtODUseTime.Name = "txtODUseTime"
            Me.txtODUseTime.Size = New System.Drawing.Size(200, 20)
            Me.txtODUseTime.TabIndex = 134
            '
            'chkODIgnoreInvalidLocations
            '
            Me.chkODIgnoreInvalidLocations.Location = New System.Drawing.Point(20, 216)
            Me.chkODIgnoreInvalidLocations.Name = "chkODIgnoreInvalidLocations"
            Me.chkODIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 29)
            Me.chkODIgnoreInvalidLocations.TabIndex = 123
            Me.chkODIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
            '
            'cboODRestrictUTurns
            '
            Me.cboODRestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboODRestrictUTurns.ItemHeight = 13
            Me.cboODRestrictUTurns.Items.AddRange(New Object() {"No U-Turns", "Allow U-Turns", "Only At Dead Ends"})
            Me.cboODRestrictUTurns.Location = New System.Drawing.Point(151, 125)
            Me.cboODRestrictUTurns.Name = "cboODRestrictUTurns"
            Me.cboODRestrictUTurns.Size = New System.Drawing.Size(200, 21)
            Me.cboODRestrictUTurns.TabIndex = 122
            '
            'lblODRestrictUTurns
            '
            Me.lblODRestrictUTurns.Location = New System.Drawing.Point(20, 130)
            Me.lblODRestrictUTurns.Name = "lblODRestrictUTurns"
            Me.lblODRestrictUTurns.Size = New System.Drawing.Size(88, 16)
            Me.lblODRestrictUTurns.TabIndex = 130
            Me.lblODRestrictUTurns.Text = "UTurn Policy"
            '
            'lblODAccumulateAttributeNames
            '
            Me.lblODAccumulateAttributeNames.Location = New System.Drawing.Point(236, 248)
            Me.lblODAccumulateAttributeNames.Name = "lblODAccumulateAttributeNames"
            Me.lblODAccumulateAttributeNames.Size = New System.Drawing.Size(120, 16)
            Me.lblODAccumulateAttributeNames.TabIndex = 129
            Me.lblODAccumulateAttributeNames.Text = "Accumulate Attributes"
            '
            'chklstODAccumulateAttributeNames
            '
            Me.chklstODAccumulateAttributeNames.CheckOnClick = True
            Me.chklstODAccumulateAttributeNames.Location = New System.Drawing.Point(236, 264)
            Me.chklstODAccumulateAttributeNames.Name = "chklstODAccumulateAttributeNames"
            Me.chklstODAccumulateAttributeNames.ScrollAlwaysVisible = True
            Me.chklstODAccumulateAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstODAccumulateAttributeNames.TabIndex = 126
            '
            'lblODRestrictionAttributeNames
            '
            Me.lblODRestrictionAttributeNames.Location = New System.Drawing.Point(20, 248)
            Me.lblODRestrictionAttributeNames.Name = "lblODRestrictionAttributeNames"
            Me.lblODRestrictionAttributeNames.Size = New System.Drawing.Size(72, 16)
            Me.lblODRestrictionAttributeNames.TabIndex = 128
            Me.lblODRestrictionAttributeNames.Text = "Restrictions"
            '
            'chklstODRestrictionAttributeNames
            '
            Me.chklstODRestrictionAttributeNames.CheckOnClick = True
            Me.chklstODRestrictionAttributeNames.Location = New System.Drawing.Point(20, 264)
            Me.chklstODRestrictionAttributeNames.Name = "chklstODRestrictionAttributeNames"
            Me.chklstODRestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstODRestrictionAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstODRestrictionAttributeNames.TabIndex = 125
            '
            'cboODImpedance
            '
            Me.cboODImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboODImpedance.ItemHeight = 13
            Me.cboODImpedance.Location = New System.Drawing.Point(151, 23)
            Me.cboODImpedance.Name = "cboODImpedance"
            Me.cboODImpedance.Size = New System.Drawing.Size(200, 21)
            Me.cboODImpedance.TabIndex = 121
            '
            'lblODImpedance
            '
            Me.lblODImpedance.Location = New System.Drawing.Point(20, 28)
            Me.lblODImpedance.Name = "lblODImpedance"
            Me.lblODImpedance.Size = New System.Drawing.Size(64, 16)
            Me.lblODImpedance.TabIndex = 127
            Me.lblODImpedance.Text = "Impedance"
            '
            'chkODUseHierarchy
            '
            Me.chkODUseHierarchy.Location = New System.Drawing.Point(20, 184)
            Me.chkODUseHierarchy.Name = "chkODUseHierarchy"
            Me.chkODUseHierarchy.Size = New System.Drawing.Size(96, 26)
            Me.chkODUseHierarchy.TabIndex = 124
            Me.chkODUseHierarchy.Text = "Use Hierarchy"
            '
            'cboODOutputLines
            '
            Me.cboODOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboODOutputLines.ItemHeight = 13
            Me.cboODOutputLines.Items.AddRange(New Object() {"No Lines", "Straight Lines"})
            Me.cboODOutputLines.Location = New System.Drawing.Point(151, 152)
            Me.cboODOutputLines.Name = "cboODOutputLines"
            Me.cboODOutputLines.Size = New System.Drawing.Size(200, 21)
            Me.cboODOutputLines.TabIndex = 119
            '
            'lblODOutputLines
            '
            Me.lblODOutputLines.Location = New System.Drawing.Point(20, 157)
            Me.lblODOutputLines.Name = "lblODOutputLines"
            Me.lblODOutputLines.Size = New System.Drawing.Size(114, 16)
            Me.lblODOutputLines.TabIndex = 120
            Me.lblODOutputLines.Text = "Shape"
            '
            'txtODDefaultTargetDestinationCount
            '
            Me.txtODDefaultTargetDestinationCount.Location = New System.Drawing.Point(151, 96)
            Me.txtODDefaultTargetDestinationCount.Name = "txtODDefaultTargetDestinationCount"
            Me.txtODDefaultTargetDestinationCount.Size = New System.Drawing.Size(200, 20)
            Me.txtODDefaultTargetDestinationCount.TabIndex = 116
            '
            'lblODDefaultTargetDestinationCount
            '
            Me.lblODDefaultTargetDestinationCount.Location = New System.Drawing.Point(20, 100)
            Me.lblODDefaultTargetDestinationCount.Name = "lblODDefaultTargetDestinationCount"
            Me.lblODDefaultTargetDestinationCount.Size = New System.Drawing.Size(125, 16)
            Me.lblODDefaultTargetDestinationCount.TabIndex = 115
            Me.lblODDefaultTargetDestinationCount.Text = "Number of Destinations"
            '
            'txtODDefaultCutoff
            '
            Me.txtODDefaultCutoff.Location = New System.Drawing.Point(151, 70)
            Me.txtODDefaultCutoff.Name = "txtODDefaultCutoff"
            Me.txtODDefaultCutoff.Size = New System.Drawing.Size(200, 20)
            Me.txtODDefaultCutoff.TabIndex = 114
            '
            'lblODDefaultCutoff
            '
            Me.lblODDefaultCutoff.Location = New System.Drawing.Point(20, 74)
            Me.lblODDefaultCutoff.Name = "lblODDefaultCutoff"
            Me.lblODDefaultCutoff.Size = New System.Drawing.Size(114, 16)
            Me.lblODDefaultCutoff.TabIndex = 113
            Me.lblODDefaultCutoff.Text = "Default Cutoff"
            '
            'tabServiceArea
            '
            Me.tabServiceArea.Controls.Add(Me.chkSAUseTime)
            Me.tabServiceArea.Controls.Add(Me.txtSAUseTime)
            Me.tabServiceArea.Controls.Add(Me.cboSATrimPolygonDistanceUnits)
            Me.tabServiceArea.Controls.Add(Me.txtSATrimPolygonDistance)
            Me.tabServiceArea.Controls.Add(Me.chkSATrimOuterPolygon)
            Me.tabServiceArea.Controls.Add(Me.chkSAIncludeSourceInformationOnLines)
            Me.tabServiceArea.Controls.Add(Me.cboSATravelDirection)
            Me.tabServiceArea.Controls.Add(Me.lblSATravelDirection)
            Me.tabServiceArea.Controls.Add(Me.chkSASplitPolygonsAtBreaks)
            Me.tabServiceArea.Controls.Add(Me.chkSAOverlapPolygons)
            Me.tabServiceArea.Controls.Add(Me.chkSASplitLinesAtBreaks)
            Me.tabServiceArea.Controls.Add(Me.chkSAOverlapLines)
            Me.tabServiceArea.Controls.Add(Me.chkSAIgnoreInvalidLocations)
            Me.tabServiceArea.Controls.Add(Me.cboSARestrictUTurns)
            Me.tabServiceArea.Controls.Add(Me.lblSARestrictUTurns)
            Me.tabServiceArea.Controls.Add(Me.lblSAAccumulateAttributeNames)
            Me.tabServiceArea.Controls.Add(Me.chklstSAAccumulateAttributeNames)
            Me.tabServiceArea.Controls.Add(Me.lblSARestrictionAttributeNames)
            Me.tabServiceArea.Controls.Add(Me.chklstSARestrictionAttributeNames)
            Me.tabServiceArea.Controls.Add(Me.lblSAOutputPolygons)
            Me.tabServiceArea.Controls.Add(Me.cboSAOutputPolygons)
            Me.tabServiceArea.Controls.Add(Me.lblSAOutputLines)
            Me.tabServiceArea.Controls.Add(Me.cboSAOutputLines)
            Me.tabServiceArea.Controls.Add(Me.chkSAMergeSimilarPolygonRanges)
            Me.tabServiceArea.Controls.Add(Me.txtSADefaultBreaks)
            Me.tabServiceArea.Controls.Add(Me.lblSADefaultBreaks)
            Me.tabServiceArea.Controls.Add(Me.cboSAImpedance)
            Me.tabServiceArea.Controls.Add(Me.lblSAImpedance)
            Me.tabServiceArea.Location = New System.Drawing.Point(4, 22)
            Me.tabServiceArea.Name = "tabServiceArea"
            Me.tabServiceArea.Size = New System.Drawing.Size(712, 473)
            Me.tabServiceArea.TabIndex = 2
            Me.tabServiceArea.Text = "Service Area"
            Me.tabServiceArea.UseVisualStyleBackColor = True
            '
            'chkSAUseTime
            '
            Me.chkSAUseTime.Location = New System.Drawing.Point(20, 51)
            Me.chkSAUseTime.Name = "chkSAUseTime"
            Me.chkSAUseTime.Size = New System.Drawing.Size(104, 16)
            Me.chkSAUseTime.TabIndex = 135
            Me.chkSAUseTime.Text = "Use Time"
            '
            'txtSAUseTime
            '
            Me.txtSAUseTime.Location = New System.Drawing.Point(151, 47)
            Me.txtSAUseTime.Name = "txtSAUseTime"
            Me.txtSAUseTime.Size = New System.Drawing.Size(200, 20)
            Me.txtSAUseTime.TabIndex = 136
            '
            'cboSATrimPolygonDistanceUnits
            '
            Me.cboSATrimPolygonDistanceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSATrimPolygonDistanceUnits.ItemHeight = 13
            Me.cboSATrimPolygonDistanceUnits.Items.AddRange(New Object() {"Unknown Units", "Inches", "Points", "Feet", "Yards", "Miles", "Nautical Miles", "Millimeters", "Centimeters", "Meters", "Kilometers", "DecimalDegrees", "Decimeters"})
            Me.cboSATrimPolygonDistanceUnits.Location = New System.Drawing.Point(241, 173)
            Me.cboSATrimPolygonDistanceUnits.Name = "cboSATrimPolygonDistanceUnits"
            Me.cboSATrimPolygonDistanceUnits.Size = New System.Drawing.Size(110, 21)
            Me.cboSATrimPolygonDistanceUnits.TabIndex = 120
            '
            'txtSATrimPolygonDistance
            '
            Me.txtSATrimPolygonDistance.Location = New System.Drawing.Point(169, 174)
            Me.txtSATrimPolygonDistance.Name = "txtSATrimPolygonDistance"
            Me.txtSATrimPolygonDistance.Size = New System.Drawing.Size(66, 20)
            Me.txtSATrimPolygonDistance.TabIndex = 119
            '
            'chkSATrimOuterPolygon
            '
            Me.chkSATrimOuterPolygon.Location = New System.Drawing.Point(41, 174)
            Me.chkSATrimOuterPolygon.Name = "chkSATrimOuterPolygon"
            Me.chkSATrimOuterPolygon.Size = New System.Drawing.Size(122, 22)
            Me.chkSATrimOuterPolygon.TabIndex = 118
            Me.chkSATrimOuterPolygon.Text = "Trim Outer Polygon"
            '
            'chkSAIncludeSourceInformationOnLines
            '
            Me.chkSAIncludeSourceInformationOnLines.Location = New System.Drawing.Point(329, 229)
            Me.chkSAIncludeSourceInformationOnLines.Name = "chkSAIncludeSourceInformationOnLines"
            Me.chkSAIncludeSourceInformationOnLines.Size = New System.Drawing.Size(215, 22)
            Me.chkSAIncludeSourceInformationOnLines.TabIndex = 117
            Me.chkSAIncludeSourceInformationOnLines.Text = "Include Source Information On Lines"
            '
            'cboSATravelDirection
            '
            Me.cboSATravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSATravelDirection.ItemHeight = 13
            Me.cboSATravelDirection.Items.AddRange(New Object() {"From Facility", "To Facility"})
            Me.cboSATravelDirection.Location = New System.Drawing.Point(151, 95)
            Me.cboSATravelDirection.Name = "cboSATravelDirection"
            Me.cboSATravelDirection.Size = New System.Drawing.Size(200, 21)
            Me.cboSATravelDirection.TabIndex = 115
            '
            'lblSATravelDirection
            '
            Me.lblSATravelDirection.Location = New System.Drawing.Point(20, 100)
            Me.lblSATravelDirection.Name = "lblSATravelDirection"
            Me.lblSATravelDirection.Size = New System.Drawing.Size(114, 16)
            Me.lblSATravelDirection.TabIndex = 116
            Me.lblSATravelDirection.Text = "Travel Direction"
            '
            'chkSASplitPolygonsAtBreaks
            '
            Me.chkSASplitPolygonsAtBreaks.Location = New System.Drawing.Point(169, 150)
            Me.chkSASplitPolygonsAtBreaks.Name = "chkSASplitPolygonsAtBreaks"
            Me.chkSASplitPolygonsAtBreaks.Size = New System.Drawing.Size(154, 22)
            Me.chkSASplitPolygonsAtBreaks.TabIndex = 114
            Me.chkSASplitPolygonsAtBreaks.Text = "Split Polygons At Breaks"
            '
            'chkSAOverlapPolygons
            '
            Me.chkSAOverlapPolygons.Location = New System.Drawing.Point(41, 149)
            Me.chkSAOverlapPolygons.Name = "chkSAOverlapPolygons"
            Me.chkSAOverlapPolygons.Size = New System.Drawing.Size(122, 22)
            Me.chkSAOverlapPolygons.TabIndex = 113
            Me.chkSAOverlapPolygons.Text = "Overlap Polygons"
            '
            'chkSASplitLinesAtBreaks
            '
            Me.chkSASplitLinesAtBreaks.Location = New System.Drawing.Point(169, 229)
            Me.chkSASplitLinesAtBreaks.Name = "chkSASplitLinesAtBreaks"
            Me.chkSASplitLinesAtBreaks.Size = New System.Drawing.Size(154, 22)
            Me.chkSASplitLinesAtBreaks.TabIndex = 112
            Me.chkSASplitLinesAtBreaks.Text = "Split Lines At Breaks"
            '
            'chkSAOverlapLines
            '
            Me.chkSAOverlapLines.Location = New System.Drawing.Point(41, 229)
            Me.chkSAOverlapLines.Name = "chkSAOverlapLines"
            Me.chkSAOverlapLines.Size = New System.Drawing.Size(122, 22)
            Me.chkSAOverlapLines.TabIndex = 111
            Me.chkSAOverlapLines.Text = "Overlap Lines"
            '
            'chkSAIgnoreInvalidLocations
            '
            Me.chkSAIgnoreInvalidLocations.Location = New System.Drawing.Point(23, 283)
            Me.chkSAIgnoreInvalidLocations.Name = "chkSAIgnoreInvalidLocations"
            Me.chkSAIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 29)
            Me.chkSAIgnoreInvalidLocations.TabIndex = 105
            Me.chkSAIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
            '
            'cboSARestrictUTurns
            '
            Me.cboSARestrictUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSARestrictUTurns.ItemHeight = 13
            Me.cboSARestrictUTurns.Items.AddRange(New Object() {"No U-Turns", "Allow U-Turns", "Only At Dead Ends"})
            Me.cboSARestrictUTurns.Location = New System.Drawing.Point(151, 259)
            Me.cboSARestrictUTurns.Name = "cboSARestrictUTurns"
            Me.cboSARestrictUTurns.Size = New System.Drawing.Size(200, 21)
            Me.cboSARestrictUTurns.TabIndex = 104
            '
            'lblSARestrictUTurns
            '
            Me.lblSARestrictUTurns.Location = New System.Drawing.Point(20, 264)
            Me.lblSARestrictUTurns.Name = "lblSARestrictUTurns"
            Me.lblSARestrictUTurns.Size = New System.Drawing.Size(88, 16)
            Me.lblSARestrictUTurns.TabIndex = 110
            Me.lblSARestrictUTurns.Text = "UTurn Policy"
            '
            'lblSAAccumulateAttributeNames
            '
            Me.lblSAAccumulateAttributeNames.Location = New System.Drawing.Point(238, 314)
            Me.lblSAAccumulateAttributeNames.Name = "lblSAAccumulateAttributeNames"
            Me.lblSAAccumulateAttributeNames.Size = New System.Drawing.Size(120, 16)
            Me.lblSAAccumulateAttributeNames.TabIndex = 109
            Me.lblSAAccumulateAttributeNames.Text = "Accumulate Attributes"
            '
            'chklstSAAccumulateAttributeNames
            '
            Me.chklstSAAccumulateAttributeNames.CheckOnClick = True
            Me.chklstSAAccumulateAttributeNames.Location = New System.Drawing.Point(238, 330)
            Me.chklstSAAccumulateAttributeNames.Name = "chklstSAAccumulateAttributeNames"
            Me.chklstSAAccumulateAttributeNames.ScrollAlwaysVisible = True
            Me.chklstSAAccumulateAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstSAAccumulateAttributeNames.TabIndex = 107
            '
            'lblSARestrictionAttributeNames
            '
            Me.lblSARestrictionAttributeNames.Location = New System.Drawing.Point(22, 314)
            Me.lblSARestrictionAttributeNames.Name = "lblSARestrictionAttributeNames"
            Me.lblSARestrictionAttributeNames.Size = New System.Drawing.Size(72, 16)
            Me.lblSARestrictionAttributeNames.TabIndex = 108
            Me.lblSARestrictionAttributeNames.Text = "Restrictions"
            '
            'chklstSARestrictionAttributeNames
            '
            Me.chklstSARestrictionAttributeNames.CheckOnClick = True
            Me.chklstSARestrictionAttributeNames.Location = New System.Drawing.Point(22, 330)
            Me.chklstSARestrictionAttributeNames.Name = "chklstSARestrictionAttributeNames"
            Me.chklstSARestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstSARestrictionAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstSARestrictionAttributeNames.TabIndex = 106
            '
            'lblSAOutputPolygons
            '
            Me.lblSAOutputPolygons.Location = New System.Drawing.Point(20, 127)
            Me.lblSAOutputPolygons.Name = "lblSAOutputPolygons"
            Me.lblSAOutputPolygons.Size = New System.Drawing.Size(122, 16)
            Me.lblSAOutputPolygons.TabIndex = 103
            Me.lblSAOutputPolygons.Text = "Output Polygons"
            '
            'cboSAOutputPolygons
            '
            Me.cboSAOutputPolygons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSAOutputPolygons.ItemHeight = 13
            Me.cboSAOutputPolygons.Items.AddRange(New Object() {"No Polygons", "Simplified Polygons", "Detailed Polygons"})
            Me.cboSAOutputPolygons.Location = New System.Drawing.Point(151, 122)
            Me.cboSAOutputPolygons.Name = "cboSAOutputPolygons"
            Me.cboSAOutputPolygons.Size = New System.Drawing.Size(200, 21)
            Me.cboSAOutputPolygons.TabIndex = 102
            '
            'lblSAOutputLines
            '
            Me.lblSAOutputLines.Location = New System.Drawing.Point(20, 207)
            Me.lblSAOutputLines.Name = "lblSAOutputLines"
            Me.lblSAOutputLines.Size = New System.Drawing.Size(122, 16)
            Me.lblSAOutputLines.TabIndex = 101
            Me.lblSAOutputLines.Text = "Output Lines"
            '
            'cboSAOutputLines
            '
            Me.cboSAOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSAOutputLines.ItemHeight = 13
            Me.cboSAOutputLines.Items.AddRange(New Object() {"No Lines", "True Shape", "True Shape With Measures"})
            Me.cboSAOutputLines.Location = New System.Drawing.Point(151, 202)
            Me.cboSAOutputLines.Name = "cboSAOutputLines"
            Me.cboSAOutputLines.Size = New System.Drawing.Size(200, 21)
            Me.cboSAOutputLines.TabIndex = 100
            '
            'chkSAMergeSimilarPolygonRanges
            '
            Me.chkSAMergeSimilarPolygonRanges.Location = New System.Drawing.Point(329, 149)
            Me.chkSAMergeSimilarPolygonRanges.Name = "chkSAMergeSimilarPolygonRanges"
            Me.chkSAMergeSimilarPolygonRanges.Size = New System.Drawing.Size(192, 22)
            Me.chkSAMergeSimilarPolygonRanges.TabIndex = 99
            Me.chkSAMergeSimilarPolygonRanges.Text = "Merge Similar Polygon Ranges"
            '
            'txtSADefaultBreaks
            '
            Me.txtSADefaultBreaks.Location = New System.Drawing.Point(151, 69)
            Me.txtSADefaultBreaks.Name = "txtSADefaultBreaks"
            Me.txtSADefaultBreaks.Size = New System.Drawing.Size(200, 20)
            Me.txtSADefaultBreaks.TabIndex = 98
            '
            'lblSADefaultBreaks
            '
            Me.lblSADefaultBreaks.Location = New System.Drawing.Point(20, 73)
            Me.lblSADefaultBreaks.Name = "lblSADefaultBreaks"
            Me.lblSADefaultBreaks.Size = New System.Drawing.Size(114, 16)
            Me.lblSADefaultBreaks.TabIndex = 97
            Me.lblSADefaultBreaks.Text = "Default Breaks"
            '
            'cboSAImpedance
            '
            Me.cboSAImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboSAImpedance.ItemHeight = 13
            Me.cboSAImpedance.Location = New System.Drawing.Point(151, 23)
            Me.cboSAImpedance.Name = "cboSAImpedance"
            Me.cboSAImpedance.Size = New System.Drawing.Size(200, 21)
            Me.cboSAImpedance.TabIndex = 86
            '
            'lblSAImpedance
            '
            Me.lblSAImpedance.Location = New System.Drawing.Point(20, 28)
            Me.lblSAImpedance.Name = "lblSAImpedance"
            Me.lblSAImpedance.Size = New System.Drawing.Size(64, 16)
            Me.lblSAImpedance.TabIndex = 87
            Me.lblSAImpedance.Text = "Impedance"
            '
            'tabVRP
            '
            Me.tabVRP.Controls.Add(Me.gbRestrictions)
            Me.tabVRP.Controls.Add(Me.gbSettings)
            Me.tabVRP.Location = New System.Drawing.Point(4, 22)
            Me.tabVRP.Name = "tabVRP"
            Me.tabVRP.Size = New System.Drawing.Size(712, 473)
            Me.tabVRP.TabIndex = 5
            Me.tabVRP.Text = "VRP"
            Me.tabVRP.UseVisualStyleBackColor = True
            '
            'gbRestrictions
            '
            Me.gbRestrictions.Controls.Add(Me.chklstVRPRestrictionAttributeNames)
            Me.gbRestrictions.Location = New System.Drawing.Point(349, 3)
            Me.gbRestrictions.Name = "gbRestrictions"
            Me.gbRestrictions.Size = New System.Drawing.Size(206, 90)
            Me.gbRestrictions.TabIndex = 1
            Me.gbRestrictions.TabStop = False
            Me.gbRestrictions.Text = "Restrictions"
            '
            'chklstVRPRestrictionAttributeNames
            '
            Me.chklstVRPRestrictionAttributeNames.CheckOnClick = True
            Me.chklstVRPRestrictionAttributeNames.Location = New System.Drawing.Point(6, 14)
            Me.chklstVRPRestrictionAttributeNames.Name = "chklstVRPRestrictionAttributeNames"
            Me.chklstVRPRestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstVRPRestrictionAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstVRPRestrictionAttributeNames.TabIndex = 109
            '
            'gbSettings
            '
            Me.gbSettings.Controls.Add(Me.cboVRPDistanceFieldUnits)
            Me.gbSettings.Controls.Add(Me.cboVRPTransitTime)
            Me.gbSettings.Controls.Add(Me.cboVRPTimeWindow)
            Me.gbSettings.Controls.Add(Me.label10)
            Me.gbSettings.Controls.Add(Me.label9)
            Me.gbSettings.Controls.Add(Me.chkVRPUseHierarchy)
            Me.gbSettings.Controls.Add(Me.cboVRPOutputShapeType)
            Me.gbSettings.Controls.Add(Me.cboVRPAllowUTurns)
            Me.gbSettings.Controls.Add(Me.cboVRPTimeFieldUnits)
            Me.gbSettings.Controls.Add(Me.txtVRPCapacityCount)
            Me.gbSettings.Controls.Add(Me.txtVRPDefaultDate)
            Me.gbSettings.Controls.Add(Me.cboVRPDistanceAttribute)
            Me.gbSettings.Controls.Add(Me.cboVRPTimeAttribute)
            Me.gbSettings.Controls.Add(Me.label7)
            Me.gbSettings.Controls.Add(Me.label6)
            Me.gbSettings.Controls.Add(Me.label5)
            Me.gbSettings.Controls.Add(Me.label4)
            Me.gbSettings.Controls.Add(Me.label3)
            Me.gbSettings.Controls.Add(Me.label2)
            Me.gbSettings.Controls.Add(Me.label1)
            Me.gbSettings.Controls.Add(Me.lblTimeAttribute)
            Me.gbSettings.Location = New System.Drawing.Point(3, 3)
            Me.gbSettings.Name = "gbSettings"
            Me.gbSettings.Size = New System.Drawing.Size(340, 321)
            Me.gbSettings.TabIndex = 0
            Me.gbSettings.TabStop = False
            Me.gbSettings.Text = "Settings"
            '
            'cboVRPDistanceFieldUnits
            '
            Me.cboVRPDistanceFieldUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPDistanceFieldUnits.ItemHeight = 13
            Me.cboVRPDistanceFieldUnits.Items.AddRange(New Object() {"Inches", "Points", "Feet", "Yards", "Miles", "Nautical Miles", "Millimeters", "Centimeters", "Meters", "Kilometers", "DecimalDegrees", "Decimeters"})
            Me.cboVRPDistanceFieldUnits.Location = New System.Drawing.Point(189, 151)
            Me.cboVRPDistanceFieldUnits.Name = "cboVRPDistanceFieldUnits"
            Me.cboVRPDistanceFieldUnits.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPDistanceFieldUnits.TabIndex = 123
            '
            'cboVRPTransitTime
            '
            Me.cboVRPTransitTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPTransitTime.FormattingEnabled = True
            Me.cboVRPTransitTime.Items.AddRange(New Object() {"High", "Medium", "Low"})
            Me.cboVRPTransitTime.Location = New System.Drawing.Point(189, 265)
            Me.cboVRPTransitTime.Name = "cboVRPTransitTime"
            Me.cboVRPTransitTime.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPTransitTime.TabIndex = 20
            '
            'cboVRPTimeWindow
            '
            Me.cboVRPTimeWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPTimeWindow.FormattingEnabled = True
            Me.cboVRPTimeWindow.Items.AddRange(New Object() {"High", "Medium", "Low"})
            Me.cboVRPTimeWindow.Location = New System.Drawing.Point(189, 238)
            Me.cboVRPTimeWindow.Name = "cboVRPTimeWindow"
            Me.cboVRPTimeWindow.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPTimeWindow.TabIndex = 19
            '
            'label10
            '
            Me.label10.AutoSize = True
            Me.label10.Location = New System.Drawing.Point(8, 268)
            Me.label10.Name = "label10"
            Me.label10.Size = New System.Drawing.Size(161, 13)
            Me.label10.TabIndex = 18
            Me.label10.Text = "Excess Transit Time Importance:"
            '
            'label9
            '
            Me.label9.AutoSize = True
            Me.label9.Location = New System.Drawing.Point(9, 241)
            Me.label9.Name = "label9"
            Me.label9.Size = New System.Drawing.Size(174, 13)
            Me.label9.TabIndex = 17
            Me.label9.Text = "Time Window Violation Importance:"
            '
            'chkVRPUseHierarchy
            '
            Me.chkVRPUseHierarchy.AutoSize = True
            Me.chkVRPUseHierarchy.Location = New System.Drawing.Point(12, 294)
            Me.chkVRPUseHierarchy.Name = "chkVRPUseHierarchy"
            Me.chkVRPUseHierarchy.Size = New System.Drawing.Size(93, 17)
            Me.chkVRPUseHierarchy.TabIndex = 16
            Me.chkVRPUseHierarchy.Text = "Use Hierarchy"
            Me.chkVRPUseHierarchy.UseVisualStyleBackColor = True
            '
            'cboVRPOutputShapeType
            '
            Me.cboVRPOutputShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPOutputShapeType.FormattingEnabled = True
            Me.cboVRPOutputShapeType.Items.AddRange(New Object() {"None", "Straight Line", "True Shape", "True Shape with Measure"})
            Me.cboVRPOutputShapeType.Location = New System.Drawing.Point(189, 208)
            Me.cboVRPOutputShapeType.Name = "cboVRPOutputShapeType"
            Me.cboVRPOutputShapeType.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPOutputShapeType.TabIndex = 15
            '
            'cboVRPAllowUTurns
            '
            Me.cboVRPAllowUTurns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPAllowUTurns.FormattingEnabled = True
            Me.cboVRPAllowUTurns.Items.AddRange(New Object() {"No U-Turns", "Allow U-Turns", "Only At Dead Ends"})
            Me.cboVRPAllowUTurns.Location = New System.Drawing.Point(189, 180)
            Me.cboVRPAllowUTurns.Name = "cboVRPAllowUTurns"
            Me.cboVRPAllowUTurns.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPAllowUTurns.TabIndex = 14
            '
            'cboVRPTimeFieldUnits
            '
            Me.cboVRPTimeFieldUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPTimeFieldUnits.FormattingEnabled = True
            Me.cboVRPTimeFieldUnits.Items.AddRange(New Object() {"Seconds", "Minutes", "Hours", "Days"})
            Me.cboVRPTimeFieldUnits.Location = New System.Drawing.Point(189, 124)
            Me.cboVRPTimeFieldUnits.Name = "cboVRPTimeFieldUnits"
            Me.cboVRPTimeFieldUnits.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPTimeFieldUnits.TabIndex = 12
            '
            'txtVRPCapacityCount
            '
            Me.txtVRPCapacityCount.Location = New System.Drawing.Point(189, 97)
            Me.txtVRPCapacityCount.Name = "txtVRPCapacityCount"
            Me.txtVRPCapacityCount.Size = New System.Drawing.Size(136, 20)
            Me.txtVRPCapacityCount.TabIndex = 11
            '
            'txtVRPDefaultDate
            '
            Me.txtVRPDefaultDate.Location = New System.Drawing.Point(189, 70)
            Me.txtVRPDefaultDate.Name = "txtVRPDefaultDate"
            Me.txtVRPDefaultDate.Size = New System.Drawing.Size(136, 20)
            Me.txtVRPDefaultDate.TabIndex = 10
            '
            'cboVRPDistanceAttribute
            '
            Me.cboVRPDistanceAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPDistanceAttribute.FormattingEnabled = True
            Me.cboVRPDistanceAttribute.Items.AddRange(New Object() {"", "Meters (Meters)"})
            Me.cboVRPDistanceAttribute.Location = New System.Drawing.Point(189, 42)
            Me.cboVRPDistanceAttribute.Name = "cboVRPDistanceAttribute"
            Me.cboVRPDistanceAttribute.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPDistanceAttribute.TabIndex = 9
            '
            'cboVRPTimeAttribute
            '
            Me.cboVRPTimeAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboVRPTimeAttribute.FormattingEnabled = True
            Me.cboVRPTimeAttribute.Location = New System.Drawing.Point(189, 14)
            Me.cboVRPTimeAttribute.Name = "cboVRPTimeAttribute"
            Me.cboVRPTimeAttribute.Size = New System.Drawing.Size(136, 21)
            Me.cboVRPTimeAttribute.TabIndex = 8
            '
            'label7
            '
            Me.label7.AutoSize = True
            Me.label7.Location = New System.Drawing.Point(9, 50)
            Me.label7.Name = "label7"
            Me.label7.Size = New System.Drawing.Size(94, 13)
            Me.label7.TabIndex = 7
            Me.label7.Text = "Distance Attribute:"
            '
            'label6
            '
            Me.label6.AutoSize = True
            Me.label6.Location = New System.Drawing.Point(9, 78)
            Me.label6.Name = "label6"
            Me.label6.Size = New System.Drawing.Size(70, 13)
            Me.label6.TabIndex = 6
            Me.label6.Text = "Default Date:"
            '
            'label5
            '
            Me.label5.AutoSize = True
            Me.label5.Location = New System.Drawing.Point(9, 105)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(82, 13)
            Me.label5.TabIndex = 5
            Me.label5.Text = "Capacity Count:"
            '
            'label4
            '
            Me.label4.AutoSize = True
            Me.label4.Location = New System.Drawing.Point(9, 132)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(85, 13)
            Me.label4.TabIndex = 4
            Me.label4.Text = "Time Field Units:"
            '
            'label3
            '
            Me.label3.AutoSize = True
            Me.label3.Location = New System.Drawing.Point(8, 160)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(104, 13)
            Me.label3.TabIndex = 3
            Me.label3.Text = "Distance Field Units:"
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(9, 188)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(74, 13)
            Me.label2.TabIndex = 2
            Me.label2.Text = "U-Turn Policy:"
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(9, 216)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(103, 13)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Output Shape Type:"
            '
            'lblTimeAttribute
            '
            Me.lblTimeAttribute.AutoSize = True
            Me.lblTimeAttribute.Location = New System.Drawing.Point(9, 22)
            Me.lblTimeAttribute.Name = "lblTimeAttribute"
            Me.lblTimeAttribute.Size = New System.Drawing.Size(75, 13)
            Me.lblTimeAttribute.TabIndex = 0
            Me.lblTimeAttribute.Text = "Time Attribute:"
            '
            'tabLocationAllocation
            '
            Me.tabLocationAllocation.Controls.Add(Me.chkLAUseTime)
            Me.tabLocationAllocation.Controls.Add(Me.txtLAUseTime)
            Me.tabLocationAllocation.Controls.Add(Me.chkLAIgnoreInvalidLocations)
            Me.tabLocationAllocation.Controls.Add(Me.grpLASettings)
            Me.tabLocationAllocation.Controls.Add(Me.chkLAUseHierarchy)
            Me.tabLocationAllocation.Controls.Add(Me.lblLAAccumulateAttributeNames)
            Me.tabLocationAllocation.Controls.Add(Me.chklstLAAccumulateAttributeNames)
            Me.tabLocationAllocation.Controls.Add(Me.lblLARestrictionAttributeNames)
            Me.tabLocationAllocation.Controls.Add(Me.chklstLARestrictionAttributeNames)
            Me.tabLocationAllocation.Controls.Add(Me.cboLAOutputLines)
            Me.tabLocationAllocation.Controls.Add(Me.label11)
            Me.tabLocationAllocation.Controls.Add(Me.cboLATravelDirection)
            Me.tabLocationAllocation.Controls.Add(Me.label12)
            Me.tabLocationAllocation.Controls.Add(Me.lblCostAttribute)
            Me.tabLocationAllocation.Controls.Add(Me.cboLAImpedance)
            Me.tabLocationAllocation.Location = New System.Drawing.Point(4, 22)
            Me.tabLocationAllocation.Name = "tabLocationAllocation"
            Me.tabLocationAllocation.Padding = New System.Windows.Forms.Padding(3)
            Me.tabLocationAllocation.Size = New System.Drawing.Size(712, 473)
            Me.tabLocationAllocation.TabIndex = 6
            Me.tabLocationAllocation.Text = "Location-Allocation"
            Me.tabLocationAllocation.UseVisualStyleBackColor = True
            '
            'chkLAUseTime
            '
            Me.chkLAUseTime.Location = New System.Drawing.Point(11, 52)
            Me.chkLAUseTime.Name = "chkLAUseTime"
            Me.chkLAUseTime.Size = New System.Drawing.Size(104, 16)
            Me.chkLAUseTime.TabIndex = 135
            Me.chkLAUseTime.Text = "Use Time"
            '
            'txtLAUseTime
            '
            Me.txtLAUseTime.Location = New System.Drawing.Point(142, 48)
            Me.txtLAUseTime.Name = "txtLAUseTime"
            Me.txtLAUseTime.Size = New System.Drawing.Size(179, 20)
            Me.txtLAUseTime.TabIndex = 136
            '
            'chkLAIgnoreInvalidLocations
            '
            Me.chkLAIgnoreInvalidLocations.Location = New System.Drawing.Point(13, 149)
            Me.chkLAIgnoreInvalidLocations.Name = "chkLAIgnoreInvalidLocations"
            Me.chkLAIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 29)
            Me.chkLAIgnoreInvalidLocations.TabIndex = 123
            Me.chkLAIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
            '
            'grpLASettings
            '
            Me.grpLASettings.Controls.Add(Me.lblTargetMarketShare)
            Me.grpLASettings.Controls.Add(Me.txtLATargetMarketShare)
            Me.grpLASettings.Controls.Add(Me.cboLAImpTransformation)
            Me.grpLASettings.Controls.Add(Me.lblImpParameter)
            Me.grpLASettings.Controls.Add(Me.txtLAImpParameter)
            Me.grpLASettings.Controls.Add(Me.lblImpTransformation)
            Me.grpLASettings.Controls.Add(Me.lblProblemType)
            Me.grpLASettings.Controls.Add(Me.cboLAProblemType)
            Me.grpLASettings.Controls.Add(Me.lblCutOff)
            Me.grpLASettings.Controls.Add(Me.txtLACutOff)
            Me.grpLASettings.Controls.Add(Me.lblNumFacilities)
            Me.grpLASettings.Controls.Add(Me.txtLAFacilitiesToLocate)
            Me.grpLASettings.Location = New System.Drawing.Point(230, 129)
            Me.grpLASettings.Name = "grpLASettings"
            Me.grpLASettings.Size = New System.Drawing.Size(342, 241)
            Me.grpLASettings.TabIndex = 122
            Me.grpLASettings.TabStop = False
            Me.grpLASettings.Text = "Advanced Settings"
            '
            'lblTargetMarketShare
            '
            Me.lblTargetMarketShare.AccessibleDescription = "grpLA"
            Me.lblTargetMarketShare.AutoSize = True
            Me.lblTargetMarketShare.Location = New System.Drawing.Point(20, 205)
            Me.lblTargetMarketShare.Name = "lblTargetMarketShare"
            Me.lblTargetMarketShare.Size = New System.Drawing.Size(122, 13)
            Me.lblTargetMarketShare.TabIndex = 31
            Me.lblTargetMarketShare.Text = "Target Market Share (%)"
            '
            'txtLATargetMarketShare
            '
            Me.txtLATargetMarketShare.AccessibleDescription = "grpLA"
            Me.txtLATargetMarketShare.Location = New System.Drawing.Point(199, 201)
            Me.txtLATargetMarketShare.Name = "txtLATargetMarketShare"
            Me.txtLATargetMarketShare.Size = New System.Drawing.Size(129, 20)
            Me.txtLATargetMarketShare.TabIndex = 30
            Me.txtLATargetMarketShare.Text = "10.0"
            '
            'cboLAImpTransformation
            '
            Me.cboLAImpTransformation.AccessibleDescription = "grpLA"
            Me.cboLAImpTransformation.FormattingEnabled = True
            Me.cboLAImpTransformation.Items.AddRange(New Object() {"Linear", "Power", "Exponential"})
            Me.cboLAImpTransformation.Location = New System.Drawing.Point(202, 135)
            Me.cboLAImpTransformation.Name = "cboLAImpTransformation"
            Me.cboLAImpTransformation.Size = New System.Drawing.Size(128, 21)
            Me.cboLAImpTransformation.TabIndex = 29
            Me.cboLAImpTransformation.Text = "Linear"
            '
            'lblImpParameter
            '
            Me.lblImpParameter.AccessibleDescription = "grpLA"
            Me.lblImpParameter.AutoSize = True
            Me.lblImpParameter.Location = New System.Drawing.Point(19, 171)
            Me.lblImpParameter.Name = "lblImpParameter"
            Me.lblImpParameter.Size = New System.Drawing.Size(111, 13)
            Me.lblImpParameter.TabIndex = 28
            Me.lblImpParameter.Text = "Impedance Parameter"
            '
            'txtLAImpParameter
            '
            Me.txtLAImpParameter.AccessibleDescription = "grpLA"
            Me.txtLAImpParameter.Location = New System.Drawing.Point(200, 166)
            Me.txtLAImpParameter.Name = "txtLAImpParameter"
            Me.txtLAImpParameter.Size = New System.Drawing.Size(129, 20)
            Me.txtLAImpParameter.TabIndex = 27
            Me.txtLAImpParameter.Text = "1.0"
            '
            'lblImpTransformation
            '
            Me.lblImpTransformation.AccessibleDescription = "grpLA"
            Me.lblImpTransformation.AutoSize = True
            Me.lblImpTransformation.Location = New System.Drawing.Point(19, 135)
            Me.lblImpTransformation.Name = "lblImpTransformation"
            Me.lblImpTransformation.Size = New System.Drawing.Size(133, 13)
            Me.lblImpTransformation.TabIndex = 26
            Me.lblImpTransformation.Text = "Impedance Transformation"
            '
            'lblProblemType
            '
            Me.lblProblemType.AccessibleDescription = "grpLA"
            Me.lblProblemType.AutoSize = True
            Me.lblProblemType.Location = New System.Drawing.Point(19, 30)
            Me.lblProblemType.Name = "lblProblemType"
            Me.lblProblemType.Size = New System.Drawing.Size(72, 13)
            Me.lblProblemType.TabIndex = 23
            Me.lblProblemType.Text = "Problem Type"
            '
            'cboLAProblemType
            '
            Me.cboLAProblemType.AccessibleDescription = "grpLA"
            Me.cboLAProblemType.FormattingEnabled = True
            Me.cboLAProblemType.Items.AddRange(New Object() {"Minimize Impedance", "Maximize Coverage", "Minimize Facilities", "Maximize Attendance", "Maximize Market Share", "Target Market Share"})
            Me.cboLAProblemType.Location = New System.Drawing.Point(202, 24)
            Me.cboLAProblemType.Name = "cboLAProblemType"
            Me.cboLAProblemType.Size = New System.Drawing.Size(128, 21)
            Me.cboLAProblemType.TabIndex = 22
            Me.cboLAProblemType.Text = "Minimize Impedance"
            '
            'lblCutOff
            '
            Me.lblCutOff.AccessibleDescription = "grpLA"
            Me.lblCutOff.AutoSize = True
            Me.lblCutOff.Location = New System.Drawing.Point(20, 98)
            Me.lblCutOff.Name = "lblCutOff"
            Me.lblCutOff.Size = New System.Drawing.Size(91, 13)
            Me.lblCutOff.TabIndex = 21
            Me.lblCutOff.Text = "Impedance Cutoff"
            '
            'txtLACutOff
            '
            Me.txtLACutOff.AccessibleDescription = "grpLA"
            Me.txtLACutOff.Location = New System.Drawing.Point(202, 98)
            Me.txtLACutOff.Name = "txtLACutOff"
            Me.txtLACutOff.Size = New System.Drawing.Size(129, 20)
            Me.txtLACutOff.TabIndex = 20
            Me.txtLACutOff.Text = "<None>"
            '
            'lblNumFacilities
            '
            Me.lblNumFacilities.AccessibleDescription = "grpLA"
            Me.lblNumFacilities.AutoSize = True
            Me.lblNumFacilities.Location = New System.Drawing.Point(20, 63)
            Me.lblNumFacilities.Name = "lblNumFacilities"
            Me.lblNumFacilities.Size = New System.Drawing.Size(102, 13)
            Me.lblNumFacilities.TabIndex = 19
            Me.lblNumFacilities.Text = "Facilities To Choose"
            '
            'txtLAFacilitiesToLocate
            '
            Me.txtLAFacilitiesToLocate.AccessibleDescription = "grpLA"
            Me.txtLAFacilitiesToLocate.Location = New System.Drawing.Point(202, 63)
            Me.txtLAFacilitiesToLocate.Name = "txtLAFacilitiesToLocate"
            Me.txtLAFacilitiesToLocate.Size = New System.Drawing.Size(130, 20)
            Me.txtLAFacilitiesToLocate.TabIndex = 18
            Me.txtLAFacilitiesToLocate.Text = "1"
            '
            'chkLAUseHierarchy
            '
            Me.chkLAUseHierarchy.AutoSize = True
            Me.chkLAUseHierarchy.Location = New System.Drawing.Point(13, 120)
            Me.chkLAUseHierarchy.Name = "chkLAUseHierarchy"
            Me.chkLAUseHierarchy.Size = New System.Drawing.Size(93, 17)
            Me.chkLAUseHierarchy.TabIndex = 121
            Me.chkLAUseHierarchy.Text = "Use Hierarchy"
            Me.chkLAUseHierarchy.UseVisualStyleBackColor = True
            '
            'lblLAAccumulateAttributeNames
            '
            Me.lblLAAccumulateAttributeNames.Location = New System.Drawing.Point(11, 280)
            Me.lblLAAccumulateAttributeNames.Name = "lblLAAccumulateAttributeNames"
            Me.lblLAAccumulateAttributeNames.Size = New System.Drawing.Size(120, 16)
            Me.lblLAAccumulateAttributeNames.TabIndex = 120
            Me.lblLAAccumulateAttributeNames.Text = "Accumulate Attributes"
            '
            'chklstLAAccumulateAttributeNames
            '
            Me.chklstLAAccumulateAttributeNames.CheckOnClick = True
            Me.chklstLAAccumulateAttributeNames.Location = New System.Drawing.Point(11, 296)
            Me.chklstLAAccumulateAttributeNames.Name = "chklstLAAccumulateAttributeNames"
            Me.chklstLAAccumulateAttributeNames.ScrollAlwaysVisible = True
            Me.chklstLAAccumulateAttributeNames.Size = New System.Drawing.Size(192, 34)
            Me.chklstLAAccumulateAttributeNames.TabIndex = 119
            '
            'lblLARestrictionAttributeNames
            '
            Me.lblLARestrictionAttributeNames.Location = New System.Drawing.Point(11, 192)
            Me.lblLARestrictionAttributeNames.Name = "lblLARestrictionAttributeNames"
            Me.lblLARestrictionAttributeNames.Size = New System.Drawing.Size(71, 15)
            Me.lblLARestrictionAttributeNames.TabIndex = 118
            Me.lblLARestrictionAttributeNames.Text = "Restrictions"
            '
            'chklstLARestrictionAttributeNames
            '
            Me.chklstLARestrictionAttributeNames.CheckOnClick = True
            Me.chklstLARestrictionAttributeNames.Location = New System.Drawing.Point(11, 210)
            Me.chklstLARestrictionAttributeNames.Name = "chklstLARestrictionAttributeNames"
            Me.chklstLARestrictionAttributeNames.ScrollAlwaysVisible = True
            Me.chklstLARestrictionAttributeNames.Size = New System.Drawing.Size(191, 34)
            Me.chklstLARestrictionAttributeNames.TabIndex = 117
            '
            'cboLAOutputLines
            '
            Me.cboLAOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboLAOutputLines.ItemHeight = 13
            Me.cboLAOutputLines.Items.AddRange(New Object() {"Straight Lines", "None"})
            Me.cboLAOutputLines.Location = New System.Drawing.Point(142, 93)
            Me.cboLAOutputLines.Name = "cboLAOutputLines"
            Me.cboLAOutputLines.Size = New System.Drawing.Size(178, 21)
            Me.cboLAOutputLines.TabIndex = 115
            '
            'label11
            '
            Me.label11.Location = New System.Drawing.Point(11, 98)
            Me.label11.Name = "label11"
            Me.label11.Size = New System.Drawing.Size(114, 15)
            Me.label11.TabIndex = 116
            Me.label11.Text = "Shape"
            '
            'cboLATravelDirection
            '
            Me.cboLATravelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboLATravelDirection.ItemHeight = 13
            Me.cboLATravelDirection.Items.AddRange(New Object() {"Facility To Demand", "Demand To Facility"})
            Me.cboLATravelDirection.Location = New System.Drawing.Point(142, 69)
            Me.cboLATravelDirection.Name = "cboLATravelDirection"
            Me.cboLATravelDirection.Size = New System.Drawing.Size(178, 21)
            Me.cboLATravelDirection.TabIndex = 113
            '
            'label12
            '
            Me.label12.Location = New System.Drawing.Point(11, 74)
            Me.label12.Name = "label12"
            Me.label12.Size = New System.Drawing.Size(114, 16)
            Me.label12.TabIndex = 114
            Me.label12.Text = "Travel Direction"
            '
            'lblCostAttribute
            '
            Me.lblCostAttribute.AutoSize = True
            Me.lblCostAttribute.Location = New System.Drawing.Point(8, 25)
            Me.lblCostAttribute.Name = "lblCostAttribute"
            Me.lblCostAttribute.Size = New System.Drawing.Size(70, 13)
            Me.lblCostAttribute.TabIndex = 25
            Me.lblCostAttribute.Text = "Cost Attribute"
            '
            'cboLAImpedance
            '
            Me.cboLAImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboLAImpedance.FormattingEnabled = True
            Me.cboLAImpedance.Location = New System.Drawing.Point(142, 25)
            Me.cboLAImpedance.Name = "cboLAImpedance"
            Me.cboLAImpedance.Size = New System.Drawing.Size(176, 21)
            Me.cboLAImpedance.TabIndex = 24
            '
            'tabAttributeParameters
            '
            Me.tabAttributeParameters.Controls.Add(Me.btnReset)
            Me.tabAttributeParameters.Controls.Add(Me.attributeParameterGrid)
            Me.tabAttributeParameters.Controls.Add(Me.label14)
            Me.tabAttributeParameters.Location = New System.Drawing.Point(4, 22)
            Me.tabAttributeParameters.Name = "tabAttributeParameters"
            Me.tabAttributeParameters.Padding = New System.Windows.Forms.Padding(3)
            Me.tabAttributeParameters.Size = New System.Drawing.Size(712, 473)
            Me.tabAttributeParameters.TabIndex = 7
            Me.tabAttributeParameters.Text = "Attribute Parameters"
            Me.tabAttributeParameters.UseVisualStyleBackColor = True
            '
            'btnReset
            '
            Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnReset.Location = New System.Drawing.Point(636, 446)
            Me.btnReset.Name = "btnReset"
            Me.btnReset.Size = New System.Drawing.Size(70, 21)
            Me.btnReset.TabIndex = 29
            Me.btnReset.Text = "&Reset"
            '
            'attributeParameterGrid
            '
            Me.attributeParameterGrid.AllowUserToAddRows = False
            Me.attributeParameterGrid.AllowUserToDeleteRows = False
            Me.attributeParameterGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.attributeParameterGrid.BackgroundColor = System.Drawing.Color.White
            Me.attributeParameterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.attributeParameterGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvcAttribute, Me.dgvcParameter, Me.dgvcValue})
            Me.attributeParameterGrid.Location = New System.Drawing.Point(13, 36)
            Me.attributeParameterGrid.Name = "attributeParameterGrid"
            Me.attributeParameterGrid.RowHeadersVisible = False
            Me.attributeParameterGrid.Size = New System.Drawing.Size(693, 404)
            Me.attributeParameterGrid.TabIndex = 28
            '
            'dgvcAttribute
            '
            Me.dgvcAttribute.HeaderText = "Attribute"
            Me.dgvcAttribute.Name = "dgvcAttribute"
            Me.dgvcAttribute.ReadOnly = True
            '
            'dgvcParameter
            '
            Me.dgvcParameter.HeaderText = "Parameter"
            Me.dgvcParameter.Name = "dgvcParameter"
            Me.dgvcParameter.ReadOnly = True
            '
            'dgvcValue
            '
            Me.dgvcValue.HeaderText = "Value"
            Me.dgvcValue.Name = "dgvcValue"
            '
            'label14
            '
            Me.label14.AutoSize = True
            Me.label14.Location = New System.Drawing.Point(6, 13)
            Me.label14.Name = "label14"
            Me.label14.Size = New System.Drawing.Size(267, 13)
            Me.label14.TabIndex = 27
            Me.label14.Text = "Specify the parameter values for the network attributes."
            '
            'btnOK
            '
            Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnOK.Location = New System.Drawing.Point(598, 523)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(58, 22)
            Me.btnOK.TabIndex = 1
            Me.btnOK.Text = "&OK"
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnCancel.Location = New System.Drawing.Point(666, 523)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(62, 22)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "&Cancel"
            '
            'frmNALayerProperties
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(739, 563)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.tabPropPages)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmNALayerProperties"
            Me.ShowInTaskbar = False
            Me.Text = "Properties"
            Me.tabPropPages.ResumeLayout(False)
            Me.tabGeneral.ResumeLayout(False)
            Me.tabGeneral.PerformLayout()
            Me.tabRoute.ResumeLayout(False)
            Me.tabRoute.PerformLayout()
            Me.tabClosestFacility.ResumeLayout(False)
            Me.tabClosestFacility.PerformLayout()
            Me.tabODCostMatrix.ResumeLayout(False)
            Me.tabODCostMatrix.PerformLayout()
            Me.tabServiceArea.ResumeLayout(False)
            Me.tabServiceArea.PerformLayout()
            Me.tabVRP.ResumeLayout(False)
            Me.gbRestrictions.ResumeLayout(False)
            Me.gbSettings.ResumeLayout(False)
            Me.gbSettings.PerformLayout()
            Me.tabLocationAllocation.ResumeLayout(False)
            Me.tabLocationAllocation.PerformLayout()
            Me.grpLASettings.ResumeLayout(False)
            Me.grpLASettings.PerformLayout()
            Me.tabAttributeParameters.ResumeLayout(False)
            Me.tabAttributeParameters.PerformLayout()
            CType(Me.attributeParameterGrid, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Private m_okClicked As Boolean
        Private m_naLayer As INALayer3

        Private Enum VARTYPE
            VT_EMPTY = 0 ' uninitialized (null)
            VT_NULL = 1 ' System.DBNull.Value
            VT_I2 = 2 ' short
            VT_I4 = 3 ' int
            VT_R4 = 4 ' float
            VT_R8 = 5 ' double
            VT_DATE = 7 ' DateTime
            VT_BSTR = 8 ' string
            VT_BOOL = 11 ' boolean
            VT_UNKNOWN = 13 ' COM object
            VT_ARRAY = 8192 ' array bitmask
        End Enum

        Private Enum AttributeParameterGridColumnType
            ATTRIBUTE_NAME = 0
            PARAMETER_NAME = 1
            PARAMETER_VALUE = 2
        End Enum

        Private m_restrictionParameterValues As Dictionary(Of String, Double) = New Dictionary(Of String, Double) From
        {
            {"Prohibit", -1},
            {"Avoid: High", 5}, {"Avoid: Medium", 2}, {"Avoid: Low", 1.3},
            {"Prefer: Low", 0.8}, {"Prefer: Medium", 0.5}, {"Prefer: High", 0.2}
        }

        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
            m_naLayer = Nothing
        End Sub

        ''' <summary>
        ''' Called by clients to show the properties window and respond to changes made when OK is clicked.
        ''' </summary>
        Public Function ShowModal(ByVal naLayer As INALayer3) As Boolean
            m_okClicked = False

            If (naLayer Is Nothing) Then
                MessageBox.Show("Null NALayer")
            Else
                m_naLayer = naLayer
            End If

            ' Setup the window based on the current NALayer settings
            PopulateControls(naLayer)
            tabPropPages.SelectedIndex = 1
            Me.Text = (CType(naLayer, ILayer)).Name & " Properties"

            Me.ShowDialog()
            Return m_okClicked
        End Function

        ''' <summary>
        ''' Set controls based on the current NALayer settings
        ''' This function takes the current NALayer and determines what type of solver it's pointing to
        ''' and populates the corresponding controls and hides the tabs for the other solvers.
        ''' </summary>
        Private Sub PopulateControls(ByVal naLayer As INALayer)
            Dim layer As ILayer = TryCast(naLayer, ILayer)
            Dim naContext As INAContext = naLayer.Context
            Dim networkDataset As INetworkDataset = naContext.NetworkDataset
            Dim naLocator As INALocator2 = TryCast(naContext.Locator, INALocator2)

            Dim naSolver As INASolver = naContext.Solver
            Dim naSolverSettings As INASolverSettings = TryCast(naSolver, INASolverSettings2)
            Dim naTimeAwareSolverSettings As INATimeAwareSolverSettings = TryCast(naSolver, INATimeAwareSolverSettings)

            Dim routeSolver As INARouteSolver2 = TryCast(naSolver, INARouteSolver2)
            Dim cfSolver As INAClosestFacilitySolver = TryCast(naSolver, INAClosestFacilitySolver)
            Dim odSolver As INAODCostMatrixSolver = TryCast(naSolver, INAODCostMatrixSolver)
            Dim saSolver As INAServiceAreaSolver2 = TryCast(naSolver, INAServiceAreaSolver2)
            Dim vrpSolver As INAVRPSolver = TryCast(naSolver, INAVRPSolver)
            Dim laSolver As INALocationAllocationSolver = TryCast(naSolver, INALocationAllocationSolver)

            ' Populate general Layer controls
            txtLayerName.Text = layer.Name
            txtMaxSearchTolerance.Text = naLocator.MaxSnapTolerance.ToString()
            cboMaxSearchToleranceUnits.SelectedIndex = Convert.ToInt32(naLocator.SnapToleranceUnits)

            PopulateAttributeParameterControl(networkDataset)

            ' Populate controls for the particular solver

            If routeSolver IsNot Nothing Then ' ROUTE LAYER
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabVRP)
                tabPropPages.TabPages.Remove(tabLocationAllocation)

                ' INARouteSolver2
                chkRouteFindBestSequence.Checked = routeSolver.FindBestSequence
                chkRoutePreserveFirstStop.Checked = routeSolver.PreserveFirstStop
                chkRoutePreserveLastStop.Checked = routeSolver.PreserveLastStop
                chkRouteUseTimeWindows.Checked = routeSolver.UseTimeWindows
                chkRouteUseStartTime.Checked = routeSolver.UseStartTime
                txtRouteStartTime.Text = routeSolver.StartTime.ToShortTimeString()
                cboRouteOutputLines.SelectedIndex = System.Convert.ToInt32(routeSolver.OutputLines)

                ' INASolverSettings
                PopulateImpedanceNameControl(cboRouteImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName)
                chkRouteUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0)
                chkRouteUseHierarchy.Checked = (chkRouteUseHierarchy.Enabled AndAlso naSolverSettings.UseHierarchy)
                chkRouteIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations
                cboRouteRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns)
                PopulateAttributeControl(chklstRouteAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost)
                PopulateAttributeControl(chklstRouteRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)
            ElseIf cfSolver IsNot Nothing Then ' CLOSEST FACILITY LAYER
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabVRP)
                tabPropPages.TabPages.Remove(tabLocationAllocation)

                ' INAClosestFacilitySolver
                txtCFDefaultCutoff.Text = GetStringFromObject(cfSolver.DefaultCutoff)
                txtCFDefaultTargetFacilityCount.Text = cfSolver.DefaultTargetFacilityCount.ToString()
                cboCFTravelDirection.SelectedIndex = Convert.ToInt32(cfSolver.TravelDirection)
                cboCFOutputLines.SelectedIndex = Convert.ToInt32(cfSolver.OutputLines)

                ' INASolverSettings
                PopulateImpedanceNameControl(cboCFImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName)
                chkCFUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0)
                chkCFUseHierarchy.Checked = (chkCFUseHierarchy.Enabled AndAlso naSolverSettings.UseHierarchy)
                chkCFIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations
                cboCFRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns)
                PopulateAttributeControl(chklstCFAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost)
                PopulateAttributeControl(chklstCFRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)

                ' INATimeAwareSolverSettings
                cboCFTimeUsage.SelectedIndex = System.Convert.ToInt32(naTimeAwareSolverSettings.TimeOfDayUsage)
                txtCFUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString("HH:mm:ss MM/dd/yyyy")

            ElseIf odSolver IsNot Nothing Then ' OD COST MATRIX LAYER
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabVRP)
                tabPropPages.TabPages.Remove(tabLocationAllocation)

                ' INAODCostMatrixSolver
                txtODDefaultCutoff.Text = GetStringFromObject(odSolver.DefaultCutoff)
                txtODDefaultTargetDestinationCount.Text = GetStringFromObject(odSolver.DefaultTargetDestinationCount)
                cboODOutputLines.SelectedIndex = Convert.ToInt32(odSolver.OutputLines)

                ' INASolverSettings
                PopulateImpedanceNameControl(cboODImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName)
                chkODUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0)
                chkODUseHierarchy.Checked = (chkODUseHierarchy.Enabled AndAlso naSolverSettings.UseHierarchy)
                chkODIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations
                cboODRestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns)
                PopulateAttributeControl(chklstODAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost)
                PopulateAttributeControl(chklstODRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)

                ' INATimeAwareSolverSettings
                If (naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime) Then
                    chkODUseTime.Checked = True
                End If
                txtODUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString("HH:mm:ss MM/dd/yyyy")

            ElseIf saSolver IsNot Nothing Then 'SERVICE AREA SOLVER
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabVRP)
                tabPropPages.TabPages.Remove(tabLocationAllocation)

                ' INAServiceAreaSolver2
                txtSADefaultBreaks.Text = ""
                For iBreak As Integer = 0 To saSolver.DefaultBreaks.Count - 1
                    txtSADefaultBreaks.Text = txtSADefaultBreaks.Text & " " & saSolver.DefaultBreaks.Element(iBreak).ToString()
                Next iBreak
                cboSATravelDirection.SelectedIndex = Convert.ToInt32(saSolver.TravelDirection)

                cboSAOutputPolygons.SelectedIndex = -1
                cboSAOutputPolygons.SelectedIndex = Convert.ToInt32(saSolver.OutputPolygons)
                chkSAOverlapPolygons.Checked = saSolver.OverlapPolygons
                chkSASplitPolygonsAtBreaks.Checked = saSolver.SplitPolygonsAtBreaks
                chkSAMergeSimilarPolygonRanges.Checked = saSolver.MergeSimilarPolygonRanges
                chkSATrimOuterPolygon.Checked = saSolver.TrimOuterPolygon
                txtSATrimPolygonDistance.Text = saSolver.TrimPolygonDistance.ToString()
                cboSATrimPolygonDistanceUnits.SelectedIndex = Convert.ToInt32(saSolver.TrimPolygonDistanceUnits)

                cboSAOutputLines.SelectedIndex = -1
                cboSAOutputLines.SelectedIndex = Convert.ToInt32(saSolver.OutputLines)
                chkSAOverlapLines.Checked = saSolver.OverlapLines
                chkSASplitLinesAtBreaks.Checked = saSolver.SplitLinesAtBreaks
                chkSAIncludeSourceInformationOnLines.Checked = saSolver.IncludeSourceInformationOnLines

                ' INASolverSettings
                PopulateImpedanceNameControl(cboSAImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName)
                chkSAIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations
                cboSARestrictUTurns.SelectedIndex = System.Convert.ToInt32(naSolverSettings.RestrictUTurns)
                PopulateAttributeControl(chklstSAAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost)
                PopulateAttributeControl(chklstSARestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)

                ' INATimeAwareSolverSettings
                If (naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime) Then
                    chkSAUseTime.Checked = True
                End If
                txtSAUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString("HH:mm:ss MM/dd/yyyy")

            ElseIf vrpSolver IsNot Nothing Then ' VRP Solver
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabLocationAllocation)

                cboVRPOutputShapeType.SelectedIndex = Convert.ToInt32(vrpSolver.OutputLines)
                cboVRPAllowUTurns.SelectedIndex = Convert.ToInt32(naSolverSettings.RestrictUTurns)
                ' VRP cannot have unknown units, so the index is offset by 1 from the solver field units
                cboVRPDistanceFieldUnits.SelectedIndex = Convert.ToInt32(vrpSolver.DistanceFieldUnits) - 1
                cboVRPTransitTime.SelectedIndex = Convert.ToInt32(vrpSolver.ExcessTransitTimePenaltyFactor)
                cboVRPTimeWindow.SelectedIndex = Convert.ToInt32(vrpSolver.TimeWindowViolationPenaltyFactor)
                cboVRPTimeFieldUnits.SelectedIndex = Convert.ToInt32(vrpSolver.TimeFieldUnits - 20)

                txtVRPCapacityCount.Text = vrpSolver.CapacityCount.ToString()
                txtVRPDefaultDate.Text = vrpSolver.DefaultDate.ToShortDateString()

                chkVRPUseHierarchy.Checked = naSolverSettings.UseHierarchy

                PopulateAttributeControl(chklstVRPRestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)

                'populate the time attribute combo box
                cboVRPTimeAttribute.Items.Clear()

                For i As Integer = 0 To networkDataset.AttributeCount - 1
                    Dim networkAttribute As INetworkAttribute = networkDataset.Attribute(i)

                    If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost AndAlso networkAttribute.Units >= esriNetworkAttributeUnits.esriNAUSeconds Then
                        cboVRPTimeAttribute.Items.Add(networkAttribute.Name)
                    End If
                Next i

                If cboVRPTimeAttribute.Items.Count > 0 Then
                    cboVRPTimeAttribute.Text = naSolverSettings.ImpedanceAttributeName
                End If


                ' for VRP, the AccumulateAttributeNames hold the length, and it can only hold one length.
                '  Loop through the network dataset attributes
                cboVRPDistanceAttribute.Items.Clear()
                cboVRPDistanceAttribute.SelectedIndex = cboVRPDistanceAttribute.Items.Add("")

                For i As Integer = 0 To networkDataset.AttributeCount - 1
                    Dim networkAttribute As INetworkAttribute = networkDataset.Attribute(i)
                    If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost AndAlso networkAttribute.Units < esriNetworkAttributeUnits.esriNAUSeconds Then
                        Dim attributeName As String = networkAttribute.Name

                        Dim cboindex As Integer = cboVRPDistanceAttribute.Items.Add(networkAttribute.Name)

                        ' If the attribute is in the strArray, it should be the selected one
                        For j As Integer = 0 To naSolverSettings.AccumulateAttributeNames.Count - 1
                            If naSolverSettings.AccumulateAttributeNames.Element(j) = attributeName Then
                                cboVRPDistanceAttribute.SelectedIndex = cboindex
                            End If
                        Next j
                    End If
                Next i
            ElseIf laSolver IsNot Nothing Then ' Location-Allocation LAYER
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabVRP)

                ' INALocationAllocationSolver
                txtLACutOff.Text = GetStringFromObject(laSolver.DefaultCutoff)
                txtLAFacilitiesToLocate.Text = laSolver.NumberFacilitiesToLocate.ToString()
                txtLAImpParameter.Text = laSolver.TransformationParameter.ToString()
                txtLATargetMarketShare.Text = laSolver.TargetMarketSharePercentage.ToString()

                cboLAImpTransformation.SelectedIndex = Convert.ToInt32(laSolver.ImpedanceTransformation)
                cboLAProblemType.SelectedIndex = Convert.ToInt32(laSolver.ProblemType)
                cboLAOutputLines.SelectedIndex = Convert.ToInt32(laSolver.OutputLines)
                cboLATravelDirection.SelectedIndex = Convert.ToInt32(laSolver.TravelDirection)

                '// INASolverSettings
                PopulateImpedanceNameControl(cboLAImpedance, networkDataset, naSolverSettings.ImpedanceAttributeName)
                PopulateAttributeControl(chklstLAAccumulateAttributeNames, networkDataset, naSolverSettings.AccumulateAttributeNames, esriNetworkAttributeUsageType.esriNAUTCost)
                PopulateAttributeControl(chklstLARestrictionAttributeNames, networkDataset, naSolverSettings.RestrictionAttributeNames, esriNetworkAttributeUsageType.esriNAUTRestriction)
                chkLAUseHierarchy.Enabled = (naSolverSettings.HierarchyAttributeName.Length > 0)
                chkLAUseHierarchy.Checked = (chkCFUseHierarchy.Enabled AndAlso naSolverSettings.UseHierarchy)
                chkLAIgnoreInvalidLocations.Checked = naSolverSettings.IgnoreInvalidLocations

                ' INATimeAwareSolverSettings
                If (naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime) Then
                    chkLAUseTime.Checked = True
                End If
                txtLAUseTime.Text = naTimeAwareSolverSettings.TimeOfDay.ToString("HH:mm:ss MM/dd/yyyy")
            Else ' Unknown type of layer
                ' Remove unnecessary tabs
                tabPropPages.TabPages.Remove(tabRoute)
                tabPropPages.TabPages.Remove(tabClosestFacility)
                tabPropPages.TabPages.Remove(tabODCostMatrix)
                tabPropPages.TabPages.Remove(tabServiceArea)
                tabPropPages.TabPages.Remove(tabVRP)
                tabPropPages.TabPages.Remove(tabLocationAllocation)
            End If
        End Sub

        ''' <summary>
        ''' Interrogate the network dataset attributes to populate a list of attribute parameters
        ''' </summary>
        Private Sub PopulateAttributeParameterControl(ByVal networkDataset As INetworkDataset)
            Dim solverSettings As INASolverSettings2 = TryCast(m_naLayer.Context.Solver, INASolverSettings2)

            ' Track if there are attribute parameters, to decide if the attribute parameter tab should be displayed
            Dim hasAttributeParameters As Boolean = False

            ' Iterate over all of the network attributes to search for parameters
            For attrIndex As Integer = 0 To networkDataset.AttributeCount - 1
                Dim networkAttribute As INetworkAttribute3 = TryCast(networkDataset.Attribute(attrIndex), INetworkAttribute3)
                Dim attributeName As String = networkAttribute.Name

                ' Iterate over all of the parameters, to find their values
                For paramIndex As Integer = 0 To networkAttribute.Parameters.Count - 1
                    hasAttributeParameters = True

                    ' Find the current attribute parameter value for this layer
                    Dim attributeParameter As INetworkAttributeParameter2 = TryCast(networkAttribute.Parameters.Element(paramIndex), INetworkAttributeParameter2)
                    Dim paramValue As Object = solverSettings.AttributeParameterValue(attributeName, attributeParameter.Name)

                    Dim rowID As Integer = attributeParameterGrid.Rows.Add()
                    attributeParameterGrid(CInt(AttributeParameterGridColumnType.ATTRIBUTE_NAME), rowID).Value = networkAttribute.Name
                    attributeParameterGrid(CInt(AttributeParameterGridColumnType.PARAMETER_NAME), rowID).Value = attributeParameter.Name

                    UpdateAttributeParameterValueCell(rowID, paramValue, DirectCast(attributeParameter.VarType, VARTYPE), attributeParameter.ParameterUsageType)
                Next
            Next

            ' Don't display the attribute parameters tab, if there are no attribute parameters
            If Not hasAttributeParameters Then
                tabPropPages.TabPages.Remove(tabAttributeParameters)
            End If
        End Sub

        Private Sub UpdateAttributeParameterValueCell(ByVal rowID As Integer, ByVal paramValue As Object, ByVal paramVarType As VARTYPE, Optional ByVal paramUsageType As esriNetworkAttributeParameterUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral)
            Dim cellText As String = ConvertAttributeParameterValueToString(paramValue, paramVarType, paramUsageType)

            ' Set up the combo box choices for restriction attribute parameters
            If paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTRestriction Then
                attributeParameterGrid(CInt(AttributeParameterGridColumnType.PARAMETER_VALUE), rowID) = CreateRestrictionParameterCell(paramValue, cellText, rowID)
            End If

            attributeParameterGrid(CInt(AttributeParameterGridColumnType.PARAMETER_VALUE), rowID).Value = cellText
        End Sub

        Private Function CreateStandardRestrictionParameterCell(ByVal rowID As Integer) As DataGridViewComboBoxCell
            Dim cbcRestriction As New DataGridViewComboBoxCell()
            cbcRestriction.Items.AddRange(m_restrictionParameterValues.Keys.ToArray())

            cbcRestriction.DisplayStyle = DataGridViewComboBoxDisplayStyle.[Nothing]
            Return cbcRestriction
        End Function

        Private Function CreateRestrictionParameterCell(ByVal paramValue As Object, ByVal cellText As String, ByVal rowID As Integer) As DataGridViewComboBoxCell
            Dim comboBoxCell As DataGridViewComboBoxCell = CreateStandardRestrictionParameterCell(rowID)

            Dim isCustomRestrictionParamValue As Boolean = Not m_restrictionParameterValues.ContainsValue(CDbl(paramValue))
            If isCustomRestrictionParamValue Then
                comboBoxCell.Items.Add(cellText)
            End If

            Return comboBoxCell
        End Function

        Private Function ConvertAttributeParameterValueToString(ByVal paramValue As Object, ByVal paramVarType As VARTYPE, Optional ByVal paramUsageType As esriNetworkAttributeParameterUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral) As String
            Dim paramValueString As String = ""

            ' Use bitwise arithmetic to determine if this parameter is an array.
            Dim vtBase As VARTYPE = DirectCast(CInt(paramVarType) And Not CInt(VARTYPE.VT_ARRAY), VARTYPE)
            Dim isArrayType As Boolean = (vtBase <> paramVarType)

            ' Null and DBNull should be represented as an empty string
            If Not System.DBNull.Value.Equals(paramValue) AndAlso paramValue IsNot Nothing Then
                ' For restriction attribute parameters, try to match the parameter double value with its associated
                '  text representation
                Dim isStandardRestrictionParamValue As Boolean = (paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTRestriction AndAlso m_restrictionParameterValues.ContainsValue(CDbl(paramValue)))
                If isStandardRestrictionParamValue Then
                    If isStandardRestrictionParamValue Then
                        ' Assign celltext to a key name matching the paramValue
                        Dim matchingKeys As IEnumerable(Of String) = From pair In m_restrictionParameterValues _
                                             Where (CDbl(paramValue).Equals(pair.Value)) _
                                             Select pair.Key

                        paramValueString = matchingKeys.First()
                    End If

                    ' For attribute parameters that are array types, determine the type of array, 
                    '   then convert the array to a string for display purposes.
                ElseIf isArrayType Then
                    Select Case vtBase
                        Case VARTYPE.VT_I2
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, Short()))
                            Exit Select
                        Case VARTYPE.VT_I4
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, Integer()))
                            Exit Select
                        Case VARTYPE.VT_R4
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, Single()))
                            Exit Select
                        Case VARTYPE.VT_R8
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, Double()))
                            Exit Select
                        Case VARTYPE.VT_DATE
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, DateTime()))
                            Exit Select
                        Case VARTYPE.VT_BSTR
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, String()))
                            Exit Select
                        Case VARTYPE.VT_BOOL
                            paramValueString = ConvertGenericArrayToString(DirectCast(paramValue, Boolean()))
                            Exit Select
                        Case Else
                            Throw New Exception("Unexpected array base type")
                    End Select
                Else
                    paramValueString = paramValue.ToString()
                End If
            End If

            Return paramValueString
        End Function

        ''' <summary>
        ''' Take generic arrays and convert them to a string
        ''' </summary>
        Private Shared Function ConvertGenericArrayToString(Of T)(ByVal values As T()) As String
            Dim sValues As String() = System.Array.ConvertAll(values, Function(p) p.ToString())
            Return [String].Join(",", sValues)
        End Function

        ''' <summary>
        ''' Take string values and convert them to generic arrays
        ''' </summary>
        Private Shared Function ConvertStringToGenericArray(Of T)(ByVal cellValue As String) As T()
            Dim list As New List(Of T)()

            Dim values As String() = cellValue.Split(","c)
            For Each value As String In values
                list.Add(DirectCast(Convert.ChangeType(value, GetType(T)), T))
            Next

            Return list.ToArray()
        End Function

        ''' <summary>
        ''' Updates the NALayer based on the current controls.
        ''' This will update the solver settings for the solver referenced by the NALayer.
        ''' </summary>
        Private Sub UpdateNALayer(ByVal naLayer As INALayer)
            Dim layer As ILayer = TryCast(naLayer, ILayer)
            Dim naContext As INAContext = naLayer.Context
            Dim networkDataset As INetworkDataset = naContext.NetworkDataset
            Dim naLocator As INALocator2 = TryCast(naContext.Locator, INALocator2)

            Dim naSolver As INASolver = naContext.Solver
            Dim naSolverSettings As INASolverSettings = TryCast(naSolver, INASolverSettings2)
            Dim naTimeAwareSolverSettings As INATimeAwareSolverSettings = TryCast(naSolver, INATimeAwareSolverSettings)

            Dim routeSolver As INARouteSolver2 = TryCast(naSolver, INARouteSolver2)
            Dim cfSolver As INAClosestFacilitySolver = TryCast(naSolver, INAClosestFacilitySolver)
            Dim odSolver As INAODCostMatrixSolver = TryCast(naSolver, INAODCostMatrixSolver)
            Dim saSolver As INAServiceAreaSolver2 = TryCast(naSolver, INAServiceAreaSolver2)
            Dim vrpSolver As INAVRPSolver = TryCast(naSolver, INAVRPSolver)
            Dim laSolver As INALocationAllocationSolver = TryCast(naSolver, INALocationAllocationSolver)

            ' Set Layer properties
            layer.Name = txtLayerName.Text
            naLocator.MaxSnapTolerance = Convert.ToDouble(txtMaxSearchTolerance.Text)
            naLocator.SnapToleranceUnits = CType(cboMaxSearchToleranceUnits.SelectedIndex, esriUnits)

            SetAttributeParameters(networkDataset)

            ' Set Solver properties
            If routeSolver IsNot Nothing Then ' ROUTE LAYER
                ' INARouteSolver
                routeSolver.FindBestSequence = chkRouteFindBestSequence.Checked
                routeSolver.PreserveFirstStop = chkRoutePreserveFirstStop.Checked
                routeSolver.PreserveLastStop = chkRoutePreserveLastStop.Checked
                routeSolver.UseTimeWindows = chkRouteUseTimeWindows.Checked
                routeSolver.UseStartTime = chkRouteUseStartTime.Checked
                Try
                    routeSolver.StartTime = System.Convert.ToDateTime(txtRouteStartTime.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                routeSolver.OutputLines = CType(cboRouteOutputLines.SelectedIndex, esriNAOutputLineType)

                ' INASolverSettings
                naSolverSettings.ImpedanceAttributeName = cboRouteImpedance.Text
                naSolverSettings.UseHierarchy = chkRouteUseHierarchy.Checked
                naSolverSettings.IgnoreInvalidLocations = chkRouteIgnoreInvalidLocations.Checked
                naSolverSettings.RestrictUTurns = CType(cboRouteRestrictUTurns.SelectedIndex, esriNetworkForwardStarBacktrack)
                naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstRouteAccumulateAttributeNames)
                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstRouteRestrictionAttributeNames)

            ElseIf cfSolver IsNot Nothing Then ' CLOSEST FACILITY LAYER
                If txtCFDefaultCutoff.Text.Length = 0 Then
                    cfSolver.DefaultCutoff = Nothing
                Else
                    cfSolver.DefaultCutoff = Convert.ToDouble(txtCFDefaultCutoff.Text)
                End If

                If txtCFDefaultTargetFacilityCount.Text.Length = 0 Then
                    cfSolver.DefaultTargetFacilityCount = 1
                Else
                    cfSolver.DefaultTargetFacilityCount = Convert.ToInt32(txtCFDefaultTargetFacilityCount.Text)
                End If

                cfSolver.TravelDirection = CType(cboCFTravelDirection.SelectedIndex, esriNATravelDirection)
                cfSolver.OutputLines = CType(cboCFOutputLines.SelectedIndex, esriNAOutputLineType)

                ' INASolverSettings
                naSolverSettings.ImpedanceAttributeName = cboCFImpedance.Text
                naSolverSettings.UseHierarchy = chkCFUseHierarchy.Checked
                naSolverSettings.IgnoreInvalidLocations = chkCFIgnoreInvalidLocations.Checked
                naSolverSettings.RestrictUTurns = CType(cboCFRestrictUTurns.SelectedIndex, esriNetworkForwardStarBacktrack)
                naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstCFAccumulateAttributeNames)
                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstCFRestrictionAttributeNames)

                ' INATimeAwareSolverSettings
                Try
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtCFUseTime.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                naTimeAwareSolverSettings.TimeOfDayUsage = CType(cboCFTimeUsage.SelectedIndex, esriNATimeOfDayUsage)

            ElseIf odSolver IsNot Nothing Then ' OD COST MATRIX LAYER
                If txtODDefaultCutoff.Text.Length = 0 Then
                    odSolver.DefaultCutoff = Nothing
                Else
                    odSolver.DefaultCutoff = Convert.ToDouble(txtODDefaultCutoff.Text)
                End If

                If txtODDefaultTargetDestinationCount.Text.Length = 0 Then
                    odSolver.DefaultTargetDestinationCount = Nothing
                Else
                    odSolver.DefaultTargetDestinationCount = Convert.ToInt32(txtODDefaultTargetDestinationCount.Text)
                End If

                odSolver.OutputLines = CType(cboODOutputLines.SelectedIndex, esriNAOutputLineType)

                ' INASolverSettings
                naSolverSettings.ImpedanceAttributeName = cboODImpedance.Text
                naSolverSettings.UseHierarchy = chkODUseHierarchy.Checked
                naSolverSettings.IgnoreInvalidLocations = chkODIgnoreInvalidLocations.Checked
                naSolverSettings.RestrictUTurns = CType(cboODRestrictUTurns.SelectedIndex, esriNetworkForwardStarBacktrack)
                naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstODAccumulateAttributeNames)
                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstODRestrictionAttributeNames)

                ' INATimeAwareSolverSettings
                Try
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtODUseTime.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                If (chkODUseTime.Checked) Then
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime
                End If

            ElseIf saSolver IsNot Nothing Then ' SERVICE AREA SOLVER
                Dim defaultBreaks As IDoubleArray = saSolver.DefaultBreaks
                defaultBreaks.RemoveAll()
                Dim breaks As String = txtSADefaultBreaks.Text.Trim()
                breaks.Replace("  ", " ")
                Dim values() As String = breaks.Split(" "c)
                For iBreak As Integer = values.GetLowerBound(0) To values.GetUpperBound(0)
                    defaultBreaks.Add(System.Convert.ToDouble(values.GetValue(iBreak)))
                Next iBreak
                saSolver.DefaultBreaks = defaultBreaks
                saSolver.TravelDirection = CType(cboSATravelDirection.SelectedIndex, esriNATravelDirection)

                saSolver.OutputPolygons = CType(cboSAOutputPolygons.SelectedIndex, esriNAOutputPolygonType)
                saSolver.OverlapPolygons = chkSAOverlapPolygons.Checked
                saSolver.SplitPolygonsAtBreaks = chkSASplitPolygonsAtBreaks.Checked
                saSolver.MergeSimilarPolygonRanges = chkSAMergeSimilarPolygonRanges.Checked
                saSolver.TrimOuterPolygon = chkSATrimOuterPolygon.Checked
                saSolver.TrimPolygonDistance = Convert.ToDouble(Me.txtSATrimPolygonDistance.Text)
                saSolver.TrimPolygonDistanceUnits = CType(cboSATrimPolygonDistanceUnits.SelectedIndex, esriUnits)

                If cboSAOutputLines.SelectedIndex = 0 Then
                    saSolver.OutputLines = CType(cboSAOutputLines.SelectedIndex, esriNAOutputLineType)
                Else ' Does not support Straight lines, so not in combobox, up by one to account for this
                    saSolver.OutputLines = CType(cboSAOutputLines.SelectedIndex + 1, esriNAOutputLineType)
                End If

                saSolver.OverlapLines = chkSAOverlapLines.Checked
                saSolver.SplitLinesAtBreaks = chkSASplitLinesAtBreaks.Checked
                saSolver.IncludeSourceInformationOnLines = Me.chkSAIncludeSourceInformationOnLines.Checked

                ' INASolverSettings
                naSolverSettings.ImpedanceAttributeName = cboSAImpedance.Text
                naSolverSettings.IgnoreInvalidLocations = chkSAIgnoreInvalidLocations.Checked
                naSolverSettings.RestrictUTurns = CType(cboSARestrictUTurns.SelectedIndex, esriNetworkForwardStarBacktrack)
                naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstSAAccumulateAttributeNames)
                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstSARestrictionAttributeNames)

                ' INATimeAwareSolverSettings
                Try
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtSAUseTime.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                If (chkSAUseTime.Checked) Then
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime
                End If
            ElseIf vrpSolver IsNot Nothing Then
                naSolverSettings.ImpedanceAttributeName = cboVRPTimeAttribute.Text
                naSolverSettings.AccumulateAttributeNames.RemoveAll()
                Dim strArray As IStringArray = naSolverSettings.AccumulateAttributeNames
                strArray.RemoveAll()
                strArray.Add(cboVRPDistanceAttribute.Text)
                naSolverSettings.AccumulateAttributeNames = strArray

                vrpSolver.CapacityCount = Convert.ToInt32(txtVRPCapacityCount.Text)
                Try
                    vrpSolver.DefaultDate = Convert.ToDateTime(txtVRPDefaultDate.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                vrpSolver.TimeFieldUnits = CType((cboVRPTimeFieldUnits.SelectedIndex + 20), esriNetworkAttributeUnits)

                ' there cannot be unknown units for a VRP, so the index is offset by 1
                vrpSolver.DistanceFieldUnits = CType((cboVRPDistanceFieldUnits.SelectedIndex + 1), esriNetworkAttributeUnits)
                naSolverSettings.RestrictUTurns = CType(cboVRPAllowUTurns.SelectedIndex, esriNetworkForwardStarBacktrack)
                vrpSolver.OutputLines = CType(cboVRPOutputShapeType.SelectedIndex, esriNAOutputLineType)
                vrpSolver.TimeWindowViolationPenaltyFactor = cboVRPTimeWindow.SelectedIndex
                vrpSolver.ExcessTransitTimePenaltyFactor = cboVRPTransitTime.SelectedIndex

                naSolverSettings.UseHierarchy = chkVRPUseHierarchy.Checked

                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstVRPRestrictionAttributeNames)
            ElseIf laSolver IsNot Nothing Then ' Location-Allocation LAYER
                If txtLACutOff.Text.Length = 0 Then
                    laSolver.DefaultCutoff = Nothing
                ElseIf Convert.ToDouble(txtLACutOff.Text) = 0.0 Then
                    laSolver.DefaultCutoff = Nothing
                Else
                    laSolver.DefaultCutoff = Convert.ToDouble(txtLACutOff.Text)
                End If

                If txtLAFacilitiesToLocate.Text.Length = 0 Then
                    laSolver.NumberFacilitiesToLocate = 1
                Else
                    laSolver.NumberFacilitiesToLocate = Convert.ToInt32(txtLAFacilitiesToLocate.Text)
                End If

                laSolver.ProblemType = CType(cboLAProblemType.SelectedIndex, esriNALocationAllocationProblemType)
                laSolver.ImpedanceTransformation = CType(cboLAImpTransformation.SelectedIndex, esriNAImpedanceTransformationType)
                laSolver.TransformationParameter = Convert.ToDouble(txtLAImpParameter.Text)
                laSolver.TargetMarketSharePercentage = Convert.ToDouble(txtLATargetMarketShare.Text)
                laSolver.TravelDirection = CType(cboLATravelDirection.SelectedIndex, esriNATravelDirection)
                laSolver.OutputLines = CType(cboLAOutputLines.SelectedIndex, esriNAOutputLineType)

                '// INASolverSettings
                naSolverSettings.ImpedanceAttributeName = cboLAImpedance.Text
                naSolverSettings.UseHierarchy = chkLAUseHierarchy.Checked
                naSolverSettings.AccumulateAttributeNames = GetCheckedAttributeNamesFromControl(chklstLAAccumulateAttributeNames)
                naSolverSettings.RestrictionAttributeNames = GetCheckedAttributeNamesFromControl(chklstLARestrictionAttributeNames)
                naSolverSettings.IgnoreInvalidLocations = chkLAIgnoreInvalidLocations.Checked

                ' INATimeAwareSolverSettings
                Try
                    naTimeAwareSolverSettings.TimeOfDay = DateTime.Parse(txtLAUseTime.Text)
                Catch e As Exception
                    MessageBox.Show("Invalid Time specified.  Use the format HH:mm:ss MM/dd/yyyy.")
                End Try
                If (chkLAUseTime.Checked) Then
                    naTimeAwareSolverSettings.TimeOfDayUsage = esriNATimeOfDayUsage.esriNATimeOfDayUseAsStartTime
                End If
            End If
        End Sub

        ''' <summary>
        ''' Populate the attribute parameter values based on the data grid rows.
        ''' </summary>
        Private Sub SetAttributeParameters(ByVal networkDataset As INetworkDataset)
            Dim solverSettings As INASolverSettings2 = TryCast(m_naLayer.Context.Solver, INASolverSettings2)

            ' The parameter values will be updated for every row in the data grid view
            For rowIndex As Integer = 0 To attributeParameterGrid.Rows.Count - 1
                Dim row As DataGridViewRow = attributeParameterGrid.Rows(rowIndex)

                ' Use the first cell value to find the appropriate network attribute
                Dim netAttribute As INetworkAttribute3 = TryCast(networkDataset.AttributeByName(row.Cells(CInt(AttributeParameterGridColumnType.ATTRIBUTE_NAME)).Value.ToString()), INetworkAttribute3)
                Dim attributeName As String = netAttribute.Name

                For paramIndex As Integer = 0 To netAttribute.Parameters.Count - 1
                    Dim parameter As INetworkAttributeParameter2 = TryCast(netAttribute.Parameters.Element(paramIndex), INetworkAttributeParameter2)
                    Dim paramName As String = parameter.Name
                    Try
                        ' Get the base type for the parameter.  For example, if the type is a double array,
                        '  then the base type is double.
                        Dim vt As VARTYPE = DirectCast(parameter.VarType, VARTYPE)
                        Dim vtBase As VARTYPE = DirectCast(CInt(vt) And Not CInt(VARTYPE.VT_ARRAY), VARTYPE)

                        ' Determine if the parameter is an array
                        Dim isArrayType As Boolean = (vtBase <> vt)

                        ' Use the second cell value to find the appropriate parameter
                        If parameter.Name = row.Cells(CInt(AttributeParameterGridColumnType.PARAMETER_NAME)).Value.ToString() Then
                            Dim paramValue As Object = System.DBNull.Value

                            Dim cellValue As Object = row.Cells(CInt(AttributeParameterGridColumnType.PARAMETER_VALUE)).Value
                            If Not System.DBNull.Value.Equals(cellValue) AndAlso cellValue IsNot Nothing Then
                                paramValue = ConvertStringToAttributeParameterValue(cellValue.ToString(), DirectCast(parameter.VarType, VARTYPE), parameter.ParameterUsageType)
                            End If

                            solverSettings.AttributeParameterValue(attributeName, paramName) = paramValue
                        End If
                    Catch e As Exception
                        Throw New Exception(("Invalid attribute parameter value." & vbLf & "Attribute: " & attributeName & vbLf & "Parameter: " & paramName & vbLf & "Error Message: ") + e.Message)
                    End Try
                Next
            Next
        End Sub

        Private Function ConvertStringToAttributeParameterValue(ByVal paramValueText As String, ByVal paramVarType As VARTYPE, Optional ByVal paramUsageType As esriNetworkAttributeParameterUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTGeneral) As Object
            Dim paramValue As Object = System.DBNull.Value
            ' Regardless of the VarType, the parameter value can be DBNull
            ' Use bitwise arithmetic to determine if this parameter is an array.
            Dim vtBase As VARTYPE = DirectCast(CInt(paramVarType) And Not CInt(VARTYPE.VT_ARRAY), VARTYPE)
            Dim isArrayType As Boolean = (vtBase <> paramVarType)

            If paramValueText <> "" Then
                ' Restriction parameters are specially handled, due to the conversion between displayed text values
                '  and stored double values
                Dim isRestrictionParm As Boolean = (paramUsageType = esriNetworkAttributeParameterUsageType.esriNAPUTRestriction)

                ' For restriction parameters that have text values in the list, use the associated double values
                If isRestrictionParm AndAlso m_restrictionParameterValues.ContainsKey(paramValueText) Then
                    paramValue = m_restrictionParameterValues(paramValueText)

                    ' For attribute parameters that are array types, determine the type of array, 
                    '   then convert the string to the appropriate array type.
                ElseIf isArrayType Then
                    Select Case vtBase
                        Case VARTYPE.VT_I2
                            paramValue = ConvertStringToGenericArray(Of Short)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_I4
                            paramValue = ConvertStringToGenericArray(Of Integer)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_R4
                            paramValue = ConvertStringToGenericArray(Of Single)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_R8
                            paramValue = ConvertStringToGenericArray(Of Double)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_DATE
                            paramValue = ConvertStringToGenericArray(Of DateTime)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_BSTR
                            paramValue = ConvertStringToGenericArray(Of String)(paramValueText)
                            Exit Select
                        Case VARTYPE.VT_BOOL
                            paramValue = ConvertStringToGenericArray(Of Boolean)(paramValueText)
                            Exit Select
                        Case Else
                            Throw New Exception("Unexpected array base type")
                    End Select
                Else
                    ' Simple type
                    ' Conversion for simple types is handled automatically, if the string can be converted to the VARTYPE
                    paramValue = paramValueText
                End If
            End If
            Return paramValue
        End Function

        ''' <summary>
        ''' Update the Impedance control based on the network dataset cost attributes
        ''' </summary>
        Private Sub PopulateImpedanceNameControl(ByVal cboImpedance As ComboBox, ByVal networkDataset As INetworkDataset, ByVal impedanceName As String)
            cboImpedance.Items.Clear()

            For i As Integer = 0 To networkDataset.AttributeCount - 1
                Dim networkAttribute As INetworkAttribute = networkDataset.Attribute(i)
                If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
                    cboImpedance.Items.Add(networkAttribute.Name)
                End If
            Next i

            If cboImpedance.Items.Count > 0 Then
                cboImpedance.Text = impedanceName
            End If
        End Sub

        ''' <summary>
        ''' Update the CheckedListBox control based on the network dataset attributes (checking the ones currently chosen by the solver)
        ''' </summary>
        Private Sub PopulateAttributeControl(ByVal chklstBox As CheckedListBox, ByVal networkDataset As INetworkDataset, ByVal strArray As IStringArray, ByVal usageType As esriNetworkAttributeUsageType)
            chklstBox.Items.Clear()

            '  Loop through the newtork dataset attributes
            For i As Integer = 0 To networkDataset.AttributeCount - 1
                Dim networkAttribute As INetworkAttribute = networkDataset.Attribute(i)
                If networkAttribute.UsageType = usageType Then
                    Dim attributeName As String = networkAttribute.Name
                    Dim checkState As CheckState = checkState.Unchecked

                    ' If the attribute is in the strArray, it should be checked
                    For j As Integer = 0 To strArray.Count - 1
                        If strArray.Element(j) = attributeName Then
                            checkState = checkState.Checked
                        End If
                    Next j

                    ' Add the attribute to the control
                    chklstBox.Items.Add(attributeName, checkState)
                End If
            Next i
        End Sub

        ''' <summary>
        ''' Returns the attribute names checked.
        ''' </summary>
        Private Function GetCheckedAttributeNamesFromControl(ByVal chklstBox As CheckedListBox) As IStringArray
            Dim attributeNames As IStringArray = New StrArrayClass()

            For i As Integer = 0 To chklstBox.CheckedItems.Count - 1
                attributeNames.Add(chklstBox.Items(chklstBox.CheckedIndices(i)).ToString())
            Next i

            Return attributeNames
        End Function

        ''' <summary>
        ''' Encapsulates returning an empty string if the object is NULL.
        ''' </summary>
        Private Function GetStringFromObject(ByVal value As Object) As String
            If value Is Nothing Then
                Return ""
            Else
                Return value.ToString()
            End If
        End Function

        Private Sub chkRouteUseStartTime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRouteUseStartTime.CheckedChanged
            txtRouteStartTime.Enabled = chkRouteUseStartTime.Checked
        End Sub

        Private Sub chkRouteFindBestSequence_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRouteFindBestSequence.CheckedChanged
            chkRoutePreserveFirstStop.Enabled = chkRouteFindBestSequence.Checked
            chkRoutePreserveLastStop.Enabled = chkRouteFindBestSequence.Checked
        End Sub

        ' Enable/Disable SA Polygon controls if not generating polygons
        Private Sub cboSAOutputPolygons_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSAOutputPolygons.SelectedIndexChanged
            Dim bOutputPolygons As Boolean = (cboSAOutputPolygons.SelectedIndex > 0)
            chkSAOverlapPolygons.Enabled = bOutputPolygons
            chkSASplitPolygonsAtBreaks.Enabled = bOutputPolygons
            chkSAMergeSimilarPolygonRanges.Enabled = bOutputPolygons
            chkSATrimOuterPolygon.Enabled = bOutputPolygons
            txtSATrimPolygonDistance.Enabled = bOutputPolygons
            cboSATrimPolygonDistanceUnits.Enabled = bOutputPolygons
        End Sub

        ' Enable/Disable SA Line controls if not generating lines
        Private Sub cboSAOutputLines_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSAOutputLines.SelectedIndexChanged
            Dim bOutputLines As Boolean = (cboSAOutputLines.SelectedIndex > 0)
            chkSAOverlapLines.Enabled = bOutputLines
            chkSASplitLinesAtBreaks.Enabled = bOutputLines
            chkSAIncludeSourceInformationOnLines.Enabled = bOutputLines
        End Sub

        Private Sub cboLAProblemType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboLAProblemType.SelectedIndexChanged
            If (cboLAProblemType.SelectedIndex = 5) OrElse (cboLAProblemType.SelectedIndex = 2) Then
                txtLAFacilitiesToLocate.Enabled = False
            Else
                txtLAFacilitiesToLocate.Enabled = True
            End If

            If cboLAProblemType.SelectedIndex = 5 Then
                txtLATargetMarketShare.Enabled = True
            Else
                txtLATargetMarketShare.Enabled = False
            End If
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            m_okClicked = True

            Try
                ' Get the NAContext and NetworkDataset
                Dim naContext As INAContext = m_naLayer.Context
                Dim networkDataset As INetworkDataset = naContext.NetworkDataset

                ' Update the layer properties based on the items chosen
                UpdateNALayer(m_naLayer)

                ' Update the Context so it can respond to changes made to the solver settings
                Dim gpMessages As IGPMessages = New GPMessagesClass()
                Dim deNetworkDataset As IDENetworkDataset = TryCast((CType(networkDataset, IDatasetComponent)).DataElement, IDENetworkDataset)
                naContext.Solver.UpdateContext(naContext, deNetworkDataset, gpMessages)

                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Failed to update the layer. " + ex.Message)
            End Try
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            m_okClicked = False
            Me.Close()
        End Sub

        Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
            Dim networkDataset As INetworkDataset = m_naLayer.Context.NetworkDataset
            Dim solverSettings As INASolverSettings2 = TryCast(m_naLayer.Context.Solver, INASolverSettings2)

            ' The parameter values will be reset for every row in the data grid view
            For rowID As Integer = 0 To attributeParameterGrid.Rows.Count - 1
                Dim row As DataGridViewRow = attributeParameterGrid.Rows(rowID)

                ' Use the first cell value to find the appropriate network attribute
                Dim netAttribute As INetworkAttribute3 = TryCast(networkDataset.AttributeByName(row.Cells(CInt(AttributeParameterGridColumnType.ATTRIBUTE_NAME)).Value.ToString()), INetworkAttribute3)
                Dim attributeParameters As IArray = netAttribute.Parameters
                Dim attributeName As String = netAttribute.Name

                ' Check every parameter to find the matching one
                For paramIndex As Integer = 0 To netAttribute.Parameters.Count - 1
                    Dim attributeParameter As INetworkAttributeParameter2 = TryCast(attributeParameters.Element(paramIndex), INetworkAttributeParameter2)
                    If attributeParameter.Name = row.Cells(CInt(AttributeParameterGridColumnType.PARAMETER_NAME)).Value.ToString() Then
                        solverSettings.AttributeParameterValue(attributeName, attributeParameter.Name) = attributeParameter.DefaultValue
                        UpdateAttributeParameterValueCell(rowID, attributeParameter.DefaultValue, DirectCast(attributeParameter.VarType, VARTYPE), attributeParameter.ParameterUsageType)
                    End If
                Next
            Next
        End Sub
    End Class
End Namespace
