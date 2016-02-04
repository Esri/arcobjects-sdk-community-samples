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
Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Imports System.Collections.Specialized
Imports Route_WebService.WebService

Namespace Route_WebService
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Route_WebServiceClass : Inherits System.Windows.Forms.Form
#Region "Window Controls Declaration"

		Private pictureBox As System.Windows.Forms.PictureBox
		Private fraINASolverSettings As System.Windows.Forms.GroupBox
		Private cboUturnPolicy As System.Windows.Forms.ComboBox
		Private label11 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private chklstAccumulateAttributes As System.Windows.Forms.CheckedListBox
		Private label5 As System.Windows.Forms.Label
		Private chklstRestrictions As System.Windows.Forms.CheckedListBox
		Private cboImpedance As System.Windows.Forms.ComboBox
		Private label4 As System.Windows.Forms.Label
		Private chkIgnoreInvalidLocations As System.Windows.Forms.CheckBox
		Private WithEvents cboNALayers As System.Windows.Forms.ComboBox
		Private label10 As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private chkReturnMap As System.Windows.Forms.CheckBox
		Private WithEvents cmdSolve As System.Windows.Forms.Button
		Private cboSnapToleranceUnits As System.Windows.Forms.ComboBox
		Private txtMaxSnapTolerance As System.Windows.Forms.TextBox
		Private txtSnapTolerance As System.Windows.Forms.TextBox
		Private components As System.ComponentModel.IContainer
		Private label20 As System.Windows.Forms.Label
		Private label21 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private chkPreserveFirst As System.Windows.Forms.CheckBox
		Private WithEvents chkBestOrder As System.Windows.Forms.CheckBox
		Private cboRouteDirectionsLengthUnits As System.Windows.Forms.ComboBox
		Private chkReturnRoutes As System.Windows.Forms.CheckBox
		Private chkReturnRouteGeometries As System.Windows.Forms.CheckBox
		Private chkReturnStops As System.Windows.Forms.CheckBox
		Private chkReturnDirections As System.Windows.Forms.CheckBox
		Private cboRouteOutputLines As System.Windows.Forms.ComboBox
		Private chkUseTimeWindows As System.Windows.Forms.CheckBox
		Private chkPreserveLast As System.Windows.Forms.CheckBox
		Private cboRouteDirectionsTimeAttribute As System.Windows.Forms.ComboBox
		Private fraINARouteSolver As System.Windows.Forms.GroupBox
		Private fraINAServerRouteParams As System.Windows.Forms.GroupBox
		Private tabCtrlOutput As System.Windows.Forms.TabControl
		Private tabReturnStopsFeatures As System.Windows.Forms.TabPage
		Private tabReturnBarrierFeatures As System.Windows.Forms.TabPage
		Private tabReturnDirections As System.Windows.Forms.TabPage
		Private tabReturnRouteFeatures As System.Windows.Forms.TabPage
		Private dataGridRouteFeatures As System.Windows.Forms.DataGrid
		Private dataGridStopFeatures As System.Windows.Forms.DataGrid
		Private dataGridBarrierFeatures As System.Windows.Forms.DataGrid
		Private tabReturnMap As System.Windows.Forms.TabPage
		Private tabReturnRouteGeometry As System.Windows.Forms.TabPage
		Private treeViewRouteGeometry As System.Windows.Forms.TreeView
		Private treeViewDirections As System.Windows.Forms.TreeView
		Private checkReturnBarriers As System.Windows.Forms.CheckBox
		Private chkUseHierarchy As System.Windows.Forms.CheckBox
		Private WithEvents chkUseStartTime As System.Windows.Forms.CheckBox
		Private txtStartTime As System.Windows.Forms.TextBox
		Private fraINAServerSolverParams As System.Windows.Forms.GroupBox

		Private m_naServer As SanFrancisco_NAServer

#End Region

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If Not components Is Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.cmdSolve = New System.Windows.Forms.Button
			Me.pictureBox = New System.Windows.Forms.PictureBox
			Me.fraINASolverSettings = New System.Windows.Forms.GroupBox
			Me.chkIgnoreInvalidLocations = New System.Windows.Forms.CheckBox
			Me.cboUturnPolicy = New System.Windows.Forms.ComboBox
			Me.label11 = New System.Windows.Forms.Label
			Me.label7 = New System.Windows.Forms.Label
			Me.chklstAccumulateAttributes = New System.Windows.Forms.CheckedListBox
			Me.label5 = New System.Windows.Forms.Label
			Me.chklstRestrictions = New System.Windows.Forms.CheckedListBox
			Me.cboImpedance = New System.Windows.Forms.ComboBox
			Me.label4 = New System.Windows.Forms.Label
			Me.chkUseHierarchy = New System.Windows.Forms.CheckBox
			Me.fraINAServerSolverParams = New System.Windows.Forms.GroupBox
			Me.chkReturnMap = New System.Windows.Forms.CheckBox
			Me.cboSnapToleranceUnits = New System.Windows.Forms.ComboBox
			Me.cboNALayers = New System.Windows.Forms.ComboBox
			Me.label10 = New System.Windows.Forms.Label
			Me.label8 = New System.Windows.Forms.Label
			Me.txtMaxSnapTolerance = New System.Windows.Forms.TextBox
			Me.txtSnapTolerance = New System.Windows.Forms.TextBox
			Me.label9 = New System.Windows.Forms.Label
			Me.fraINARouteSolver = New System.Windows.Forms.GroupBox
			Me.label20 = New System.Windows.Forms.Label
			Me.cboRouteOutputLines = New System.Windows.Forms.ComboBox
			Me.chkUseTimeWindows = New System.Windows.Forms.CheckBox
			Me.chkPreserveLast = New System.Windows.Forms.CheckBox
			Me.chkPreserveFirst = New System.Windows.Forms.CheckBox
			Me.chkBestOrder = New System.Windows.Forms.CheckBox
			Me.chkUseStartTime = New System.Windows.Forms.CheckBox
			Me.txtStartTime = New System.Windows.Forms.TextBox
			Me.fraINAServerRouteParams = New System.Windows.Forms.GroupBox
			Me.checkReturnBarriers = New System.Windows.Forms.CheckBox
			Me.cboRouteDirectionsTimeAttribute = New System.Windows.Forms.ComboBox
			Me.label21 = New System.Windows.Forms.Label
			Me.cboRouteDirectionsLengthUnits = New System.Windows.Forms.ComboBox
			Me.label22 = New System.Windows.Forms.Label
			Me.chkReturnRoutes = New System.Windows.Forms.CheckBox
			Me.chkReturnRouteGeometries = New System.Windows.Forms.CheckBox
			Me.chkReturnStops = New System.Windows.Forms.CheckBox
			Me.chkReturnDirections = New System.Windows.Forms.CheckBox
			Me.tabCtrlOutput = New System.Windows.Forms.TabControl
			Me.tabReturnMap = New System.Windows.Forms.TabPage
			Me.tabReturnRouteFeatures = New System.Windows.Forms.TabPage
			Me.dataGridRouteFeatures = New System.Windows.Forms.DataGrid
			Me.tabReturnBarrierFeatures = New System.Windows.Forms.TabPage
			Me.dataGridBarrierFeatures = New System.Windows.Forms.DataGrid
			Me.tabReturnStopsFeatures = New System.Windows.Forms.TabPage
			Me.dataGridStopFeatures = New System.Windows.Forms.DataGrid
			Me.tabReturnDirections = New System.Windows.Forms.TabPage
			Me.treeViewDirections = New System.Windows.Forms.TreeView
			Me.tabReturnRouteGeometry = New System.Windows.Forms.TabPage
			Me.treeViewRouteGeometry = New System.Windows.Forms.TreeView
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.fraINASolverSettings.SuspendLayout()
			Me.fraINAServerSolverParams.SuspendLayout()
			Me.fraINARouteSolver.SuspendLayout()
			Me.fraINAServerRouteParams.SuspendLayout()
			Me.tabCtrlOutput.SuspendLayout()
			Me.tabReturnMap.SuspendLayout()
			Me.tabReturnRouteFeatures.SuspendLayout()
			CType(Me.dataGridRouteFeatures, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.tabReturnBarrierFeatures.SuspendLayout()
			CType(Me.dataGridBarrierFeatures, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.tabReturnStopsFeatures.SuspendLayout()
			CType(Me.dataGridStopFeatures, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.tabReturnDirections.SuspendLayout()
			Me.tabReturnRouteGeometry.SuspendLayout()
			Me.SuspendLayout()
			'
			'cmdSolve
			'
			Me.cmdSolve.Location = New System.Drawing.Point(232, 504)
			Me.cmdSolve.Name = "cmdSolve"
			Me.cmdSolve.Size = New System.Drawing.Size(200, 32)
			Me.cmdSolve.TabIndex = 29
			Me.cmdSolve.Text = "Find Route"
			'
			'pictureBox
			'
			Me.pictureBox.BackColor = System.Drawing.Color.White
			Me.pictureBox.Location = New System.Drawing.Point(8, 8)
			Me.pictureBox.Name = "pictureBox"
			Me.pictureBox.Size = New System.Drawing.Size(448, 456)
			Me.pictureBox.TabIndex = 20
			Me.pictureBox.TabStop = False
			'
			'fraINASolverSettings
			'
			Me.fraINASolverSettings.Controls.Add(Me.chkIgnoreInvalidLocations)
			Me.fraINASolverSettings.Controls.Add(Me.cboUturnPolicy)
			Me.fraINASolverSettings.Controls.Add(Me.label11)
			Me.fraINASolverSettings.Controls.Add(Me.label7)
			Me.fraINASolverSettings.Controls.Add(Me.chklstAccumulateAttributes)
			Me.fraINASolverSettings.Controls.Add(Me.label5)
			Me.fraINASolverSettings.Controls.Add(Me.chklstRestrictions)
			Me.fraINASolverSettings.Controls.Add(Me.cboImpedance)
			Me.fraINASolverSettings.Controls.Add(Me.label4)
			Me.fraINASolverSettings.Controls.Add(Me.chkUseHierarchy)
			Me.fraINASolverSettings.Enabled = False
			Me.fraINASolverSettings.Location = New System.Drawing.Point(8, 112)
			Me.fraINASolverSettings.Name = "fraINASolverSettings"
			Me.fraINASolverSettings.Size = New System.Drawing.Size(424, 192)
			Me.fraINASolverSettings.TabIndex = 70
			Me.fraINASolverSettings.TabStop = False
			Me.fraINASolverSettings.Text = "INASolverSettings"
			'
			'chkIgnoreInvalidLocations
			'
			Me.chkIgnoreInvalidLocations.Location = New System.Drawing.Point(24, 80)
			Me.chkIgnoreInvalidLocations.Name = "chkIgnoreInvalidLocations"
			Me.chkIgnoreInvalidLocations.Size = New System.Drawing.Size(144, 21)
			Me.chkIgnoreInvalidLocations.TabIndex = 10
			Me.chkIgnoreInvalidLocations.Text = "Ignore Invalid Locations"
			'
			'cboUturnPolicy
			'
			Me.cboUturnPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboUturnPolicy.Location = New System.Drawing.Point(128, 48)
			Me.cboUturnPolicy.Name = "cboUturnPolicy"
			Me.cboUturnPolicy.Size = New System.Drawing.Size(280, 21)
			Me.cboUturnPolicy.TabIndex = 9
			'
			'label11
			'
			Me.label11.Location = New System.Drawing.Point(16, 56)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(88, 16)
			Me.label11.TabIndex = 73
			Me.label11.Text = "Allow U-Turns"
			'
			'label7
			'
			Me.label7.Location = New System.Drawing.Point(216, 104)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(120, 16)
			Me.label7.TabIndex = 72
			Me.label7.Text = "Accumulate Attributes"
			'
			'chklstAccumulateAttributes
			'
			Me.chklstAccumulateAttributes.CheckOnClick = True
			Me.chklstAccumulateAttributes.Location = New System.Drawing.Point(216, 120)
			Me.chklstAccumulateAttributes.Name = "chklstAccumulateAttributes"
			Me.chklstAccumulateAttributes.ScrollAlwaysVisible = True
			Me.chklstAccumulateAttributes.Size = New System.Drawing.Size(192, 64)
			Me.chklstAccumulateAttributes.TabIndex = 13
			'
			'label5
			'
			Me.label5.Location = New System.Drawing.Point(16, 104)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(72, 16)
			Me.label5.TabIndex = 70
			Me.label5.Text = "Restrictions"
			'
			'chklstRestrictions
			'
			Me.chklstRestrictions.CheckOnClick = True
			Me.chklstRestrictions.Location = New System.Drawing.Point(16, 120)
			Me.chklstRestrictions.Name = "chklstRestrictions"
			Me.chklstRestrictions.ScrollAlwaysVisible = True
			Me.chklstRestrictions.Size = New System.Drawing.Size(192, 64)
			Me.chklstRestrictions.TabIndex = 12
			'
			'cboImpedance
			'
			Me.cboImpedance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboImpedance.Location = New System.Drawing.Point(128, 24)
			Me.cboImpedance.Name = "cboImpedance"
			Me.cboImpedance.Size = New System.Drawing.Size(280, 21)
			Me.cboImpedance.TabIndex = 8
			'
			'label4
			'
			Me.label4.Location = New System.Drawing.Point(16, 24)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(64, 16)
			Me.label4.TabIndex = 67
			Me.label4.Text = "Impedance"
			'
			'chkUseHierarchy
			'
			Me.chkUseHierarchy.Checked = True
			Me.chkUseHierarchy.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkUseHierarchy.Location = New System.Drawing.Point(216, 80)
			Me.chkUseHierarchy.Name = "chkUseHierarchy"
			Me.chkUseHierarchy.Size = New System.Drawing.Size(96, 21)
			Me.chkUseHierarchy.TabIndex = 11
			Me.chkUseHierarchy.Text = "Use Hierarchy"
			'
			'fraINAServerSolverParams
			'
			Me.fraINAServerSolverParams.Controls.Add(Me.chkReturnMap)
			Me.fraINAServerSolverParams.Controls.Add(Me.cboSnapToleranceUnits)
			Me.fraINAServerSolverParams.Controls.Add(Me.cboNALayers)
			Me.fraINAServerSolverParams.Controls.Add(Me.label10)
			Me.fraINAServerSolverParams.Controls.Add(Me.label8)
			Me.fraINAServerSolverParams.Controls.Add(Me.txtMaxSnapTolerance)
			Me.fraINAServerSolverParams.Controls.Add(Me.txtSnapTolerance)
			Me.fraINAServerSolverParams.Controls.Add(Me.label9)
			Me.fraINAServerSolverParams.Enabled = False
			Me.fraINAServerSolverParams.Location = New System.Drawing.Point(8, 8)
			Me.fraINAServerSolverParams.Name = "fraINAServerSolverParams"
			Me.fraINAServerSolverParams.Size = New System.Drawing.Size(424, 96)
			Me.fraINAServerSolverParams.TabIndex = 71
			Me.fraINAServerSolverParams.TabStop = False
			Me.fraINAServerSolverParams.Text = "INAServerSolverParams"
			'
			'chkReturnMap
			'
			Me.chkReturnMap.Checked = True
			Me.chkReturnMap.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnMap.Location = New System.Drawing.Point(8, 72)
			Me.chkReturnMap.Name = "chkReturnMap"
			Me.chkReturnMap.Size = New System.Drawing.Size(96, 16)
			Me.chkReturnMap.TabIndex = 7
			Me.chkReturnMap.Text = "Return Map"
			'
			'cboSnapToleranceUnits
			'
			Me.cboSnapToleranceUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboSnapToleranceUnits.Location = New System.Drawing.Point(240, 48)
			Me.cboSnapToleranceUnits.Name = "cboSnapToleranceUnits"
			Me.cboSnapToleranceUnits.Size = New System.Drawing.Size(168, 21)
			Me.cboSnapToleranceUnits.TabIndex = 6
			'
			'cboNALayers
			'
			Me.cboNALayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboNALayers.Location = New System.Drawing.Point(128, 24)
			Me.cboNALayers.Name = "cboNALayers"
			Me.cboNALayers.Size = New System.Drawing.Size(280, 21)
			Me.cboNALayers.TabIndex = 3
			'
			'label10
			'
			Me.label10.Location = New System.Drawing.Point(168, 56)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(16, 16)
			Me.label10.TabIndex = 72
			Me.label10.Text = "to"
			'
			'label8
			'
			Me.label8.Location = New System.Drawing.Point(8, 24)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(128, 16)
			Me.label8.TabIndex = 71
			Me.label8.Text = "NALayer Name"
			'
			'txtMaxSnapTolerance
			'
			Me.txtMaxSnapTolerance.Location = New System.Drawing.Point(192, 48)
			Me.txtMaxSnapTolerance.Name = "txtMaxSnapTolerance"
			Me.txtMaxSnapTolerance.Size = New System.Drawing.Size(40, 20)
			Me.txtMaxSnapTolerance.TabIndex = 5
			Me.txtMaxSnapTolerance.Text = "50"
			'
			'txtSnapTolerance
			'
			Me.txtSnapTolerance.Location = New System.Drawing.Point(128, 48)
			Me.txtSnapTolerance.Name = "txtSnapTolerance"
			Me.txtSnapTolerance.Size = New System.Drawing.Size(32, 20)
			Me.txtSnapTolerance.TabIndex = 4
			Me.txtSnapTolerance.Text = "2"
			'
			'label9
			'
			Me.label9.Location = New System.Drawing.Point(8, 48)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(104, 16)
			Me.label9.TabIndex = 68
			Me.label9.Text = "Search Tolerance"
			'
			'fraINARouteSolver
			'
			Me.fraINARouteSolver.Controls.Add(Me.label20)
			Me.fraINARouteSolver.Controls.Add(Me.cboRouteOutputLines)
			Me.fraINARouteSolver.Controls.Add(Me.chkUseTimeWindows)
			Me.fraINARouteSolver.Controls.Add(Me.chkPreserveLast)
			Me.fraINARouteSolver.Controls.Add(Me.chkPreserveFirst)
			Me.fraINARouteSolver.Controls.Add(Me.chkBestOrder)
			Me.fraINARouteSolver.Controls.Add(Me.chkUseStartTime)
			Me.fraINARouteSolver.Controls.Add(Me.txtStartTime)
			Me.fraINARouteSolver.Enabled = False
			Me.fraINARouteSolver.Location = New System.Drawing.Point(232, 312)
			Me.fraINARouteSolver.Name = "fraINARouteSolver"
			Me.fraINARouteSolver.Size = New System.Drawing.Size(200, 184)
			Me.fraINARouteSolver.TabIndex = 76
			Me.fraINARouteSolver.TabStop = False
			Me.fraINARouteSolver.Text = "INARouteSolver"
			'
			'label20
			'
			Me.label20.Location = New System.Drawing.Point(8, 136)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(40, 16)
			Me.label20.TabIndex = 53
			Me.label20.Text = "Shape"
			'
			'cboRouteOutputLines
			'
			Me.cboRouteOutputLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboRouteOutputLines.ItemHeight = 13
			Me.cboRouteOutputLines.Location = New System.Drawing.Point(8, 152)
			Me.cboRouteOutputLines.Name = "cboRouteOutputLines"
			Me.cboRouteOutputLines.Size = New System.Drawing.Size(184, 21)
			Me.cboRouteOutputLines.TabIndex = 28
			'
			'chkUseTimeWindows
			'
			Me.chkUseTimeWindows.Location = New System.Drawing.Point(8, 80)
			Me.chkUseTimeWindows.Name = "chkUseTimeWindows"
			Me.chkUseTimeWindows.Size = New System.Drawing.Size(128, 16)
			Me.chkUseTimeWindows.TabIndex = 25
			Me.chkUseTimeWindows.Text = "Use Time Windows"
			'
			'chkPreserveLast
			'
			Me.chkPreserveLast.Location = New System.Drawing.Point(24, 56)
			Me.chkPreserveLast.Name = "chkPreserveLast"
			Me.chkPreserveLast.Size = New System.Drawing.Size(96, 16)
			Me.chkPreserveLast.TabIndex = 24
			Me.chkPreserveLast.Text = "PreserveLast"
			'
			'chkPreserveFirst
			'
			Me.chkPreserveFirst.Location = New System.Drawing.Point(24, 40)
			Me.chkPreserveFirst.Name = "chkPreserveFirst"
			Me.chkPreserveFirst.Size = New System.Drawing.Size(104, 16)
			Me.chkPreserveFirst.TabIndex = 23
			Me.chkPreserveFirst.Text = "Preserve First"
			'
			'chkBestOrder
			'
			Me.chkBestOrder.Location = New System.Drawing.Point(8, 24)
			Me.chkBestOrder.Name = "chkBestOrder"
			Me.chkBestOrder.Size = New System.Drawing.Size(112, 16)
			Me.chkBestOrder.TabIndex = 22
			Me.chkBestOrder.Text = "Find Best Order"
			'
			'chkUseStartTime
			'
			Me.chkUseStartTime.Location = New System.Drawing.Point(8, 96)
			Me.chkUseStartTime.Name = "chkUseStartTime"
			Me.chkUseStartTime.Size = New System.Drawing.Size(104, 16)
			Me.chkUseStartTime.TabIndex = 26
			Me.chkUseStartTime.Text = "Use Start Time"
			'
			'txtStartTime
			'
			Me.txtStartTime.Enabled = False
			Me.txtStartTime.Location = New System.Drawing.Point(24, 112)
			Me.txtStartTime.Name = "txtStartTime"
			Me.txtStartTime.Size = New System.Drawing.Size(168, 20)
			Me.txtStartTime.TabIndex = 27
			'
			'fraINAServerRouteParams
			'
			Me.fraINAServerRouteParams.Controls.Add(Me.checkReturnBarriers)
			Me.fraINAServerRouteParams.Controls.Add(Me.cboRouteDirectionsTimeAttribute)
			Me.fraINAServerRouteParams.Controls.Add(Me.label21)
			Me.fraINAServerRouteParams.Controls.Add(Me.cboRouteDirectionsLengthUnits)
			Me.fraINAServerRouteParams.Controls.Add(Me.label22)
			Me.fraINAServerRouteParams.Controls.Add(Me.chkReturnRoutes)
			Me.fraINAServerRouteParams.Controls.Add(Me.chkReturnRouteGeometries)
			Me.fraINAServerRouteParams.Controls.Add(Me.chkReturnStops)
			Me.fraINAServerRouteParams.Controls.Add(Me.chkReturnDirections)
			Me.fraINAServerRouteParams.Enabled = False
			Me.fraINAServerRouteParams.Location = New System.Drawing.Point(8, 312)
			Me.fraINAServerRouteParams.Name = "fraINAServerRouteParams"
			Me.fraINAServerRouteParams.Size = New System.Drawing.Size(216, 184)
			Me.fraINAServerRouteParams.TabIndex = 75
			Me.fraINAServerRouteParams.TabStop = False
			Me.fraINAServerRouteParams.Text = "INAServerRouteParams"
			'
			'checkReturnBarriers
			'
			Me.checkReturnBarriers.Checked = True
			Me.checkReturnBarriers.CheckState = System.Windows.Forms.CheckState.Checked
			Me.checkReturnBarriers.Location = New System.Drawing.Point(8, 56)
			Me.checkReturnBarriers.Name = "checkReturnBarriers"
			Me.checkReturnBarriers.Size = New System.Drawing.Size(136, 16)
			Me.checkReturnBarriers.TabIndex = 17
			Me.checkReturnBarriers.Text = "Returns Barriers"
			'
			'cboRouteDirectionsTimeAttribute
			'
			Me.cboRouteDirectionsTimeAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboRouteDirectionsTimeAttribute.ItemHeight = 13
			Me.cboRouteDirectionsTimeAttribute.Location = New System.Drawing.Point(56, 144)
			Me.cboRouteDirectionsTimeAttribute.Name = "cboRouteDirectionsTimeAttribute"
			Me.cboRouteDirectionsTimeAttribute.Size = New System.Drawing.Size(152, 21)
			Me.cboRouteDirectionsTimeAttribute.TabIndex = 21
			'
			'label21
			'
			Me.label21.Location = New System.Drawing.Point(8, 152)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(48, 16)
			Me.label21.TabIndex = 52
			Me.label21.Text = "Dir Time"
			'
			'cboRouteDirectionsLengthUnits
			'
			Me.cboRouteDirectionsLengthUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboRouteDirectionsLengthUnits.ItemHeight = 13
			Me.cboRouteDirectionsLengthUnits.Location = New System.Drawing.Point(56, 120)
			Me.cboRouteDirectionsLengthUnits.Name = "cboRouteDirectionsLengthUnits"
			Me.cboRouteDirectionsLengthUnits.Size = New System.Drawing.Size(152, 21)
			Me.cboRouteDirectionsLengthUnits.TabIndex = 20
			'
			'label22
			'
			Me.label22.Location = New System.Drawing.Point(8, 128)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(56, 16)
			Me.label22.TabIndex = 50
			Me.label22.Text = "Dir Units"
			'
			'chkReturnRoutes
			'
			Me.chkReturnRoutes.Checked = True
			Me.chkReturnRoutes.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnRoutes.Location = New System.Drawing.Point(8, 40)
			Me.chkReturnRoutes.Name = "chkReturnRoutes"
			Me.chkReturnRoutes.Size = New System.Drawing.Size(96, 16)
			Me.chkReturnRoutes.TabIndex = 16
			Me.chkReturnRoutes.Text = "Return Routes"
			'
			'chkReturnRouteGeometries
			'
			Me.chkReturnRouteGeometries.Checked = True
			Me.chkReturnRouteGeometries.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnRouteGeometries.Location = New System.Drawing.Point(8, 24)
			Me.chkReturnRouteGeometries.Name = "chkReturnRouteGeometries"
			Me.chkReturnRouteGeometries.Size = New System.Drawing.Size(152, 16)
			Me.chkReturnRouteGeometries.TabIndex = 15
			Me.chkReturnRouteGeometries.Text = "Return Route Geometries"
			'
			'chkReturnStops
			'
			Me.chkReturnStops.Checked = True
			Me.chkReturnStops.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnStops.Location = New System.Drawing.Point(8, 72)
			Me.chkReturnStops.Name = "chkReturnStops"
			Me.chkReturnStops.Size = New System.Drawing.Size(96, 16)
			Me.chkReturnStops.TabIndex = 18
			Me.chkReturnStops.Text = "Return Stops"
			'
			'chkReturnDirections
			'
			Me.chkReturnDirections.Checked = True
			Me.chkReturnDirections.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnDirections.Location = New System.Drawing.Point(8, 88)
			Me.chkReturnDirections.Name = "chkReturnDirections"
			Me.chkReturnDirections.Size = New System.Drawing.Size(160, 16)
			Me.chkReturnDirections.TabIndex = 19
			Me.chkReturnDirections.Text = "Generate Directions"
			'
			'tabCtrlOutput
			'
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnMap)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnRouteFeatures)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnBarrierFeatures)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnStopsFeatures)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnDirections)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnRouteGeometry)
			Me.tabCtrlOutput.Enabled = False
			Me.tabCtrlOutput.Location = New System.Drawing.Point(440, 16)
			Me.tabCtrlOutput.Name = "tabCtrlOutput"
			Me.tabCtrlOutput.SelectedIndex = 0
			Me.tabCtrlOutput.Size = New System.Drawing.Size(472, 496)
			Me.tabCtrlOutput.TabIndex = 30
			'
			'tabReturnMap
			'
			Me.tabReturnMap.Controls.Add(Me.pictureBox)
			Me.tabReturnMap.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnMap.Name = "tabReturnMap"
			Me.tabReturnMap.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnMap.TabIndex = 0
			Me.tabReturnMap.Text = "Map"
			'
			'tabReturnRouteFeatures
			'
			Me.tabReturnRouteFeatures.Controls.Add(Me.dataGridRouteFeatures)
			Me.tabReturnRouteFeatures.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnRouteFeatures.Name = "tabReturnRouteFeatures"
			Me.tabReturnRouteFeatures.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnRouteFeatures.TabIndex = 4
			Me.tabReturnRouteFeatures.Text = "Route Features"
			'
			'dataGridRouteFeatures
			'
			Me.dataGridRouteFeatures.DataMember = ""
			Me.dataGridRouteFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText
			Me.dataGridRouteFeatures.Location = New System.Drawing.Point(8, 8)
			Me.dataGridRouteFeatures.Name = "dataGridRouteFeatures"
			Me.dataGridRouteFeatures.Size = New System.Drawing.Size(448, 456)
			Me.dataGridRouteFeatures.TabIndex = 0
			'
			'tabReturnBarrierFeatures
			'
			Me.tabReturnBarrierFeatures.Controls.Add(Me.dataGridBarrierFeatures)
			Me.tabReturnBarrierFeatures.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnBarrierFeatures.Name = "tabReturnBarrierFeatures"
			Me.tabReturnBarrierFeatures.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnBarrierFeatures.TabIndex = 3
			Me.tabReturnBarrierFeatures.Text = "Barrier Features"
			'
			'dataGridBarrierFeatures
			'
			Me.dataGridBarrierFeatures.DataMember = ""
			Me.dataGridBarrierFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText
			Me.dataGridBarrierFeatures.Location = New System.Drawing.Point(8, 8)
			Me.dataGridBarrierFeatures.Name = "dataGridBarrierFeatures"
			Me.dataGridBarrierFeatures.Size = New System.Drawing.Size(448, 456)
			Me.dataGridBarrierFeatures.TabIndex = 0
			'
			'tabReturnStopsFeatures
			'
			Me.tabReturnStopsFeatures.Controls.Add(Me.dataGridStopFeatures)
			Me.tabReturnStopsFeatures.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnStopsFeatures.Name = "tabReturnStopsFeatures"
			Me.tabReturnStopsFeatures.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnStopsFeatures.TabIndex = 2
			Me.tabReturnStopsFeatures.Text = "Stop Features"
			'
			'dataGridStopFeatures
			'
			Me.dataGridStopFeatures.DataMember = ""
			Me.dataGridStopFeatures.HeaderForeColor = System.Drawing.SystemColors.ControlText
			Me.dataGridStopFeatures.Location = New System.Drawing.Point(8, 8)
			Me.dataGridStopFeatures.Name = "dataGridStopFeatures"
			Me.dataGridStopFeatures.Size = New System.Drawing.Size(448, 456)
			Me.dataGridStopFeatures.TabIndex = 0
			'
			'tabReturnDirections
			'
			Me.tabReturnDirections.Controls.Add(Me.treeViewDirections)
			Me.tabReturnDirections.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnDirections.Name = "tabReturnDirections"
			Me.tabReturnDirections.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnDirections.TabIndex = 1
			Me.tabReturnDirections.Text = "Directions"
			'
			'treeViewDirections
			'
			Me.treeViewDirections.Location = New System.Drawing.Point(8, 8)
			Me.treeViewDirections.Name = "treeViewDirections"
			Me.treeViewDirections.Size = New System.Drawing.Size(448, 456)
			Me.treeViewDirections.TabIndex = 69
			'
			'tabReturnRouteGeometry
			'
			Me.tabReturnRouteGeometry.Controls.Add(Me.treeViewRouteGeometry)
			Me.tabReturnRouteGeometry.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnRouteGeometry.Name = "tabReturnRouteGeometry"
			Me.tabReturnRouteGeometry.Size = New System.Drawing.Size(464, 470)
			Me.tabReturnRouteGeometry.TabIndex = 5
			Me.tabReturnRouteGeometry.Text = "Route Geometry"
			'
			'treeViewRouteGeometry
			'
			Me.treeViewRouteGeometry.Location = New System.Drawing.Point(8, 8)
			Me.treeViewRouteGeometry.Name = "treeViewRouteGeometry"
			Me.treeViewRouteGeometry.Size = New System.Drawing.Size(448, 456)
			Me.treeViewRouteGeometry.TabIndex = 1
			'
			'Route_WebServiceClass
			'
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(936, 550)
			Me.Controls.Add(Me.tabCtrlOutput)
			Me.Controls.Add(Me.fraINARouteSolver)
			Me.Controls.Add(Me.fraINAServerRouteParams)
			Me.Controls.Add(Me.fraINAServerSolverParams)
			Me.Controls.Add(Me.fraINASolverSettings)
			Me.Controls.Add(Me.cmdSolve)
			Me.Name = "Route_WebServiceClass"
			Me.Text = "NAServer - Route Web Service"
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
			Me.fraINASolverSettings.ResumeLayout(False)
			Me.fraINAServerSolverParams.ResumeLayout(False)
			Me.fraINAServerSolverParams.PerformLayout()
			Me.fraINARouteSolver.ResumeLayout(False)
			Me.fraINARouteSolver.PerformLayout()
			Me.fraINAServerRouteParams.ResumeLayout(False)
			Me.tabCtrlOutput.ResumeLayout(False)
			Me.tabReturnMap.ResumeLayout(False)
			Me.tabReturnRouteFeatures.ResumeLayout(False)
			CType(Me.dataGridRouteFeatures, System.ComponentModel.ISupportInitialize).EndInit()
			Me.tabReturnBarrierFeatures.ResumeLayout(False)
			CType(Me.dataGridBarrierFeatures, System.ComponentModel.ISupportInitialize).EndInit()
			Me.tabReturnStopsFeatures.ResumeLayout(False)
			CType(Me.dataGridStopFeatures, System.ComponentModel.ISupportInitialize).EndInit()
			Me.tabReturnDirections.ResumeLayout(False)
			Me.tabReturnRouteGeometry.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub
#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread()> _
		Shared Sub Main()
			Application.Run(New Route_WebServiceClass())
		End Sub

		''' <summary>
		''' This function
		'''     - sets the server and solver parameters
		'''     - populates the stops NALocations
		'''     - gets and displays the server results (map, directions, etc.)
		''' </summary>
		Private Sub cmdSolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSolve.Click
			Me.Cursor = Cursors.WaitCursor

			Try
				' Get SolverParams
				Dim solverParams As NAServerSolverParams = TryCast(m_naServer.GetSolverParameters(cboNALayers.Text), NAServerSolverParams)

				' Set Solver params
				SetNASolverSettings(solverParams)
				SetSolverSpecificInterface(solverParams)
				SetServerSolverParams(solverParams)

				' Load Locations
				LoadLocations(solverParams)

				'Solve the Route
				Dim solverResults As NAServerSolverResults
				solverResults = m_naServer.Solve(solverParams)

				'Get NAServer results in the tab controls
				OutputResults(solverParams, solverResults)
			Catch exception As Exception
				MessageBox.Show(exception.Message, "An error has occurred")
			End Try

			Me.Cursor = Cursors.Default
		End Sub

		''' <summary>
		''' This function
		'''     - gets all route network analysis layers
		'''     - selects the first route network analysis layer
		'''     - sets all controls for this route network analysis layer
		''' </summary>
		Private Sub GetNetworkAnalysisLayers()

			Me.Cursor = Cursors.WaitCursor

			Try
				' Enable Frame
				fraINAServerSolverParams.Enabled = True

				'Get Route NA layer names
				cboNALayers.Items.Clear()
				Dim naLayers As String() = m_naServer.GetNALayerNames(esriNAServerLayerType.esriNAServerRouteLayer)
				Dim i As Integer = 0
				Do While i < naLayers.Length
					cboNALayers.Items.Add(naLayers(i))
					i += 1
				Loop

				' Select the first NA Layer name
				If cboNALayers.Items.Count > 0 Then
					cboNALayers.SelectedIndex = 0
				Else
					MessageBox.Show("There is no Network Analyst layer associated with this MapServer object!", "NAServer - Route Sample", System.Windows.Forms.MessageBoxButtons.OK)
				End If

			Catch exception As Exception
				MessageBox.Show(exception.Message, "An error has occurred")
			End Try

			Me.Cursor = Cursors.Default
		End Sub

		''' <summary>
		''' This function sets all controls for the selected route network analysis layer
		''' </summary>
		Private Sub cboNALayers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNALayers.SelectedIndexChanged
			Dim naLayerName As String = cboNALayers.Text

			' Enable Solve Button
			cmdSolve.Enabled = (naLayerName.Length = 0)

			If naLayerName.Length = 0 Then
				Return
			End If

			Me.Cursor = Cursors.WaitCursor

			Try
				Dim solverParams As NAServerSolverParams = m_naServer.GetSolverParameters(naLayerName)
				Dim networkDescription As NAServerNetworkDescription = m_naServer.GetNetworkDescription(naLayerName)

				' Setup Default Properties
				GetNASolverSettings(networkDescription, solverParams)
				GetSolverSpecificInterface(solverParams)
				GetServerSolverParams(networkDescription, solverParams)

				' Make frames Enable
				MakeFramesEnabled(solverParams)

				' Hide Tabs
				HideTabs()

				cmdSolve.Enabled = True
			Catch exception As Exception
				MessageBox.Show(exception.Message, "An error has occurred")
			End Try

			Me.Cursor = Cursors.Default

		End Sub

		''' <summary>
		''' Get Default NASolverSettings controls (Cost Attributes, Restrictions Attributes. etc.)
		''' </summary>
		Private Sub GetNASolverSettings(ByVal networkDescription As NAServerNetworkDescription, ByVal solverSettings As NAServerSolverParams)
			Dim impedanceIndex As Integer = 0

			'Get Attributes
			cboImpedance.Items.Clear()
			chklstAccumulateAttributes.Items.Clear()
			chklstRestrictions.Items.Clear()
			cboUturnPolicy.SelectedIndex = -1

			Dim attributes() As NAServerNetworkAttribute = networkDescription.NetworkAttributes
			Dim accumulateAttributeNames() As String = solverSettings.AccumulateAttributeNames
			Dim restrictionAttributeNames() As String = solverSettings.RestrictionAttributeNames

			For i As Integer = 0 To attributes.Length - 1
				Dim networkAttribute As NAServerNetworkAttribute = attributes(i)
				Dim networkAttributeName As String = networkAttribute.Name
				If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
					chklstAccumulateAttributes.Items.Add(networkAttribute.Name, IsStringInStringArray(networkAttributeName, accumulateAttributeNames))

					Dim index As Integer = cboImpedance.Items.Add(networkAttribute.Name + " (" + networkAttribute.Units.ToString().Substring(7) + ")")
					If networkAttributeName = solverSettings.ImpedanceAttributeName Then
						impedanceIndex = index
					End If
				End If

				If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTRestriction Then
					chklstRestrictions.Items.Add(networkAttribute.Name, IsStringInStringArray(networkAttributeName, restrictionAttributeNames))
				End If
			Next

			If cboImpedance.Items.Count > 0 Then
				cboImpedance.SelectedIndex = impedanceIndex
			End If

			chkUseHierarchy.Checked = solverSettings.UseHierarchy
			chkUseHierarchy.Enabled = solverSettings.HierarchyAttributeName.Length > 0
			chkIgnoreInvalidLocations.Checked = solverSettings.IgnoreInvalidLocations
			cboUturnPolicy.SelectedIndex = System.Convert.ToInt32(solverSettings.RestrictUTurns)
		End Sub

		Private Function IsStringInStringArray(ByVal inputString As String, ByVal stringArray() As String) As Boolean
			Dim numInArray As Integer = stringArray.Length
			For i As Integer = 0 To numInArray - 1
				If inputString.Equals(stringArray(i)) Then
					Return True
				End If
			Next

			Return False
		End Function

		''' <summary>
		''' Set Default route solver controls  (BestOrder, UseTimeWindows, UseStartTime, etc.)
		''' </summary>
		Private Sub GetSolverSpecificInterface(ByVal solverParams As NAServerSolverParams)
			Dim routeSolver As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			If Not routeSolver Is Nothing Then
				chkBestOrder.Checked = routeSolver.FindBestSequence
				chkPreserveFirst.Checked = routeSolver.PreserveFirstStop
				chkPreserveLast.Checked = routeSolver.PreserveLastStop
				If chkBestOrder.Checked = True Then
					Me.chkPreserveFirst.Enabled = True
					Me.chkPreserveLast.Enabled = True
				Else
					Me.chkPreserveFirst.Enabled = False
					Me.chkPreserveLast.Enabled = False
				End If
				chkUseTimeWindows.Checked = routeSolver.UseTimeWindows
				chkUseStartTime.Checked = routeSolver.UseStartTime
				If chkUseStartTime.Checked Then
					txtStartTime.Enabled = True
					txtStartTime.Text = routeSolver.StartTime.ToString()
				Else
					txtStartTime.Enabled = False
					txtStartTime.Text = System.DateTime.Now.ToString()
				End If
				cboRouteOutputLines.SelectedIndex = System.Convert.ToInt32(routeSolver.OutputLines)
			End If
		End Sub

		''' <summary>
		''' Get ServerSolverParams controls (ReturnMap, SnapTolerance, etc.)
		''' </summary>
		Private Sub GetServerSolverParams(ByVal networkDescription As NAServerNetworkDescription, ByVal solverParams As NAServerSolverParams)
			chkReturnMap.Checked = True
			txtSnapTolerance.Text = solverParams.SnapTolerance.ToString()
			txtMaxSnapTolerance.Text = solverParams.MaxSnapTolerance.ToString()
			cboSnapToleranceUnits.SelectedIndex = Convert.ToInt32(solverParams.SnapToleranceUnits)

			'Set Route Defaults
            chkReturnRouteGeometries.Checked = False
			chkReturnRoutes.Checked = True
			chkReturnStops.Checked = True
			chkReturnDirections.Checked = True
			checkReturnBarriers.Checked = True

			'Set Directions Defaults
			cboRouteDirectionsTimeAttribute.Items.Clear()
			Dim attributes As NAServerNetworkAttribute() = networkDescription.NetworkAttributes
			Dim i As Integer = 0
			Do While i < attributes.Length
				Dim networkAttribute As NAServerNetworkAttribute = attributes(i)
				If networkAttribute.UsageType = esriNetworkAttributeUsageType.esriNAUTCost Then
					If String.Compare(networkAttribute.Units.ToString(), "esriNAUMinutes") = 0 Then
						cboRouteDirectionsTimeAttribute.Items.Add(networkAttribute.Name)
					End If
				End If
				i += 1
			Loop

			' Set the default direction settings
			Dim routeParams As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			If Not routeParams Is Nothing Then
				cboRouteDirectionsLengthUnits.Text = routeParams.DirectionsLengthUnits.ToString().Substring(7)
				'Select the first time attribute
				If cboRouteDirectionsTimeAttribute.Items.Count > 0 Then
					cboRouteDirectionsTimeAttribute.Text = routeParams.DirectionsTimeAttributeName
				End If
			End If
		End Sub

		''' <summary>
		''' Set general solver settings  (Impedance, Restrictions, Accumulates, etc.)
		''' </summary>
		Private Sub SetNASolverSettings(ByVal solverSettings As NAServerSolverParams)
			solverSettings.ImpedanceAttributeName = ExtractImpedanceName(cboImpedance.Text)

			Dim restrictionAttributes As String() = New String(chklstRestrictions.CheckedItems.Count - 1) {}
			Dim i As Integer = 0
			Do While i < chklstRestrictions.CheckedItems.Count
				restrictionAttributes(i) = chklstRestrictions.Items(chklstRestrictions.CheckedIndices(i)).ToString()
				i += 1
			Loop
			solverSettings.RestrictionAttributeNames = restrictionAttributes

			Dim accumulateAttributes As String() = New String(chklstAccumulateAttributes.CheckedItems.Count - 1) {}
			i = 0
			Do While i < chklstAccumulateAttributes.CheckedItems.Count
				accumulateAttributes(i) = chklstAccumulateAttributes.Items(chklstAccumulateAttributes.CheckedIndices(i)).ToString()
				i += 1
			Loop
			solverSettings.AccumulateAttributeNames = accumulateAttributes

			solverSettings.RestrictUTurns = CType(cboUturnPolicy.SelectedIndex, esriNetworkForwardStarBacktrack)
			solverSettings.IgnoreInvalidLocations = chkIgnoreInvalidLocations.Checked
			solverSettings.UseHierarchy = chkUseHierarchy.Checked
		End Sub

		''' <summary>
		''' Set specific solver settings  (FindBestSequence, UseTimeWindows, etc.)      
		''' </summary>  
		Private Sub SetSolverSpecificInterface(ByVal solverParams As NAServerSolverParams)
			Dim routeSolver As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			If Not routeSolver Is Nothing Then
				routeSolver.FindBestSequence = chkBestOrder.Checked
				routeSolver.PreserveFirstStop = chkPreserveFirst.Checked
				routeSolver.PreserveLastStop = chkPreserveLast.Checked
				routeSolver.UseTimeWindows = chkUseTimeWindows.Checked
				routeSolver.OutputLines = CType(cboRouteOutputLines.SelectedIndex, esriNAOutputLineType)

				routeSolver.UseStartTime = chkUseStartTime.Checked
				If routeSolver.UseStartTime = True Then
					routeSolver.StartTime = System.Convert.ToDateTime(txtStartTime.Text.ToString())
				End If
			End If
		End Sub

		''' <summary>
		''' Set server solver parameters  (ReturnMap, SnapTolerance, etc.)
		''' </summary>
		Private Sub SetServerSolverParams(ByVal solverParams As NAServerSolverParams)
			solverParams.ReturnMap = chkReturnMap.Checked
			solverParams.SnapTolerance = Convert.ToDouble(txtSnapTolerance.Text)
			solverParams.MaxSnapTolerance = Convert.ToDouble(txtMaxSnapTolerance.Text)
			solverParams.SnapToleranceUnits = CType(cboSnapToleranceUnits.SelectedIndex, esriUnits)
			solverParams.ImageDescription.ImageDisplay.ImageWidth = pictureBox.Width
			solverParams.ImageDescription.ImageDisplay.ImageHeight = pictureBox.Height

			'			// This code shows how to specify the output spatial reference in order to get the map
			'			// in a different spatial reference than the Network Dataset
			'			ESRISpatialReferenceGEN sr = pServerContext.CreateObject("esriGeometry.GeographicCoordinateSystem") as IESRISpatialReferenceGEN;
			'			int read;
			'			sr.ImportFromESRISpatialReference("GEOGCS[GCS_North_American_1983,DATUM[D_North_American_1983,SPHEROID[GRS_1980,6378137.0,298.257222101]],PRIMEM[Greenwich,0.0],UNIT[Degree,0.0174532925199433]]", out read);
			'			solverParams.OutputSpatialReference = sr as SpatialReference;

			Dim routeParams As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			If Not routeParams Is Nothing Then
				routeParams.ReturnRouteGeometries = chkReturnRouteGeometries.Checked
				routeParams.ReturnRoutes = chkReturnRoutes.Checked
				routeParams.ReturnStops = chkReturnStops.Checked
				routeParams.ReturnBarriers = checkReturnBarriers.Checked
				routeParams.ReturnDirections = chkReturnDirections.Checked
				routeParams.DirectionsLengthUnits = GetstringToesriUnits(cboRouteDirectionsLengthUnits.Text)
				routeParams.DirectionsTimeAttributeName = cboRouteDirectionsTimeAttribute.Text
			End If
		End Sub

		''' <summary>
		''' Make frames Enabled
		''' </summary> 
		Private Sub MakeFramesEnabled(ByVal solverParams As NAServerSolverParams)
			fraINARouteSolver.Enabled = (Not (TryCast(solverParams, NAServerRouteParams)) Is Nothing)
			fraINAServerRouteParams.Enabled = (Not (TryCast(solverParams, NAServerRouteParams)) Is Nothing)
			fraINASolverSettings.Enabled = (Not (TryCast(solverParams, NAServerRouteParams)) Is Nothing)
		End Sub

		''' <summary>
		''' Load form
		''' </summary> 
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Try
				cboUturnPolicy.Items.Add("Nowhere")
				cboUturnPolicy.Items.Add("Everywhere")
				cboUturnPolicy.Items.Add("Only at Dead Ends")

				cboSnapToleranceUnits.Items.Add("Unknown Units")
				cboSnapToleranceUnits.Items.Add("Inches")
				cboSnapToleranceUnits.Items.Add("Points")
				cboSnapToleranceUnits.Items.Add("Feet")
				cboSnapToleranceUnits.Items.Add("Yards")
				cboSnapToleranceUnits.Items.Add("Miles")
				cboSnapToleranceUnits.Items.Add("Nautical Miles")
				cboSnapToleranceUnits.Items.Add("Millimeters")
				cboSnapToleranceUnits.Items.Add("Centimeters")
				cboSnapToleranceUnits.Items.Add("Meters")
				cboSnapToleranceUnits.Items.Add("Kilometers")
				cboSnapToleranceUnits.Items.Add("DecimalDegrees")
				cboSnapToleranceUnits.Items.Add("Decimeters")

				' Route
				cboRouteOutputLines.Items.Add("None")
				cboRouteOutputLines.Items.Add("Straight Line")
				cboRouteOutputLines.Items.Add("True Shape")
				cboRouteOutputLines.Items.Add("True Shape With Ms")

				cboRouteDirectionsLengthUnits.Items.Add("Feet")
				cboRouteDirectionsLengthUnits.Items.Add("Yards")
				cboRouteDirectionsLengthUnits.Items.Add("Miles")
				cboRouteDirectionsLengthUnits.Items.Add("Meters")
				cboRouteDirectionsLengthUnits.Items.Add("Kilometers")

				ConnectToWebService()
				GetNetworkAnalysisLayers()
			Catch exception As Exception
				MessageBox.Show(exception.Message, "An error has occurred")
			End Try
		End Sub

		''' <summary>
		''' Get NAServer Object from the web service
		''' </summary>
		Private Sub ConnectToWebService()
			m_naServer = Nothing

			' Get NAServer
			m_naServer = New SanFrancisco_NAServer()
			If Not m_naServer Is Nothing Then
				Return
			End If

			Throw (New System.Exception("Could not find the web service"))

		End Sub

		''' <summary>
		''' This function shows how to populate stop locations using two different options:
		'''     1) From Record Set using a Point Feature Class - Uncommented
		'''     2) From an Array of PropertySets - Commented out
		''' Uncomment the option, you would like to use
		''' </summary>
		Private Sub LoadLocations(ByVal solverParams As NAServerSolverParams)
			' Set first point
			Dim propSets As PropertySet() = New PropertySet(1) {}

			propSets(0) = CreateLocationPropertySet("Stop 1", "-122.49024904900", "37.74811940430", Nothing)
			propSets(1) = CreateLocationPropertySet("Stop 2", "-122.43083365400", "37.75396354490", Nothing)

			Dim routeParams As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			Dim stopsPropSets As NAServerPropertySets = New NAServerPropertySets()
			stopsPropSets.PropertySets = propSets

			routeParams.Stops = stopsPropSets
		End Sub

		''' <summary>
		''' Create a property set for NALocation
		''' </summary> 
		Private Function CreateLocationPropertySet(ByVal name As String, ByVal X As String, ByVal Y As String, ByVal sr As SpatialReference) As PropertySet
			Dim propSet As PropertySet = New PropertySet()
			propSet.PropertyArray = CreateLocationPropertyArray(name, X, Y, sr)
			Return propSet
		End Function

		''' <summary>
		''' Create a property location array
		''' </summary> 
		Private Function CreateLocationPropertyArray(ByVal name As String, ByVal X As String, ByVal Y As String, ByVal sr As SpatialReference) As PropertySetProperty()
			Dim propSetProperty As PropertySetProperty()
			If sr Is Nothing Then
				propSetProperty = New PropertySetProperty(2) {}
			Else
				propSetProperty = New PropertySetProperty(3) {}
			End If

			propSetProperty(0) = New PropertySetProperty()
			propSetProperty(0).Key = "NAME"
			propSetProperty(0).Value = name

			propSetProperty(1) = New PropertySetProperty()
			propSetProperty(1).Key = "X"
			propSetProperty(1).Value = X

			propSetProperty(2) = New PropertySetProperty()
			propSetProperty(2).Key = "Y"
			propSetProperty(2).Value = Y

			If Not sr Is Nothing Then
				propSetProperty(3) = New PropertySetProperty()
				propSetProperty(3).Key = "SpatialReference"
				propSetProperty(3).Value = sr
			End If

			Return propSetProperty
		End Function

		''' <summary>
		''' Output Results Messages, Map, Route Geometries, Directions
		''' </summary>
		Private Sub OutputResults(ByVal solverParams As NAServerSolverParams, ByVal solverResults As NAServerSolverResults)
			Dim messagesSolverResults As String = ""

			' Output Solve messages
			Dim gpMessages As GPMessages = solverResults.SolveMessages
			Dim arrGPMessage As GPMessage() = gpMessages.GPMessages1
			If Not arrGPMessage Is Nothing Then
				Dim i As Integer = 0
				Do While i < arrGPMessage.GetLength(0)
					Dim gpMessage As GPMessage = arrGPMessage(i)
					messagesSolverResults &= Constants.vbLf + gpMessage.MessageDesc
					i += 1
				Loop
			End If

			' Uncomment the following section to output the total impedance of each route in a MessageBox

			'Dim routeSolverResults As NAServerRouteResults = TryCast(solverResults, NAServerRouteResults)
			'If Not routeSolverResults Is Nothing Then
			'	Dim i As Integer = 0
			'	Do While i < routeSolverResults.TotalImpedances.GetLength(0)
			'		messagesSolverResults &= Constants.vbLf & "Total Impedance for Route[" & (i + 1) & "] = "
			'		messagesSolverResults &= routeSolverResults.TotalImpedances(i).ToString("f")
			'		messagesSolverResults &= " " & ExtractImpedanceUnits(cboImpedance.Text).ToLower()
			'		i += 1
			'	Loop
			'End If

			' Show a message box displaying both solver messages and total_impedance per route
			If messagesSolverResults.Length > 0 Then
				MessageBox.Show(messagesSolverResults, "NAServer Route Results")
			End If

			'Output Map
			pictureBox.Image = Nothing
			If solverParams.ReturnMap Then
				pictureBox.Image = System.Drawing.Image.FromStream(New System.IO.MemoryStream(solverResults.MapImage.ImageData))
			End If
			pictureBox.Refresh()

			If (Not (TryCast(solverParams, NAServerRouteParams)) Is Nothing) AndAlso (Not (TryCast(solverResults, NAServerRouteResults)) Is Nothing) Then
				OutputRouteResults(TryCast(solverParams, NAServerRouteParams), TryCast(solverResults, NAServerRouteResults))
			End If
		End Sub

		''' <summary>
		''' Output Route Results according to the NAServerRouteParams
		''' </summary>
		Private Sub OutputRouteResults(ByVal solverParams As NAServerRouteParams, ByVal solverResults As NAServerRouteResults)
			' Return Directions if generated
			If solverParams.ReturnDirections Then
				AddTabAndControl(Me.tabReturnDirections, Me.treeViewDirections)
				OutputDirections(solverResults.Directions)
			Else
				Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnDirections)
			End If

			' Return Route Geometries is generated
			If solverParams.ReturnRouteGeometries Then
				AddTabAndControl(Me.tabReturnRouteGeometry, Me.treeViewRouteGeometry)
				OutputPolylines(solverResults.RouteGeometries)
			Else
				Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnRouteGeometry)
			End If

			' Return Route Features as RecordSet
			If solverParams.ReturnRoutes Then
				AddTabAndControl(Me.tabReturnRouteFeatures, Me.dataGridRouteFeatures)
                OutputRecSetToDataGrid(solverResults.Routes, Me.dataGridRouteFeatures)
			Else
				Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnRouteFeatures)
			End If

			' Return Stop Features as RecordSet
			If solverParams.ReturnStops Then
				AddTabAndControl(Me.tabReturnStopsFeatures, Me.dataGridStopFeatures)
				OutputRecSetToDataGrid(solverResults.Stops, dataGridStopFeatures)
			Else
				Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnStopsFeatures)
			End If

			' Return Barrier Features as RecordSet
			If solverParams.ReturnBarriers Then
				AddTabAndControl(Me.tabReturnBarrierFeatures, Me.dataGridBarrierFeatures)
				OutputRecSetToDataGrid(solverResults.Barriers, dataGridBarrierFeatures)
			Else
				Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnBarrierFeatures)
			End If

			' Make TabControlOutput enable
			tabCtrlOutput.Enabled = True
		End Sub

		''' <summary>
		''' Output Route Geometries (Polylines) in a TreeView control
		''' </summary> 
		Private Sub OutputPolylines(ByVal polylines As Polyline())
			If polylines Is Nothing Then
				treeViewRouteGeometry.Nodes.Clear()
				Dim newNode As TreeNode = New TreeNode("Route Geometry not generated")
				treeViewRouteGeometry.Nodes.Add(newNode)
				Return
			End If

			' Suppress repainting the TreeView until all the objects have been created.
			treeViewRouteGeometry.BeginUpdate()

			' Clear the TreeView each time the method is called.
			treeViewRouteGeometry.Nodes.Clear()

			Dim i As Integer = 0
			Do While i < polylines.GetLength(0)
				Dim polylineN As PolylineN = TryCast(polylines(i), PolylineN)

				If Not polylineN Is Nothing Then
					Dim newNode As TreeNode = New TreeNode("Polyline Length for Route [" & (i + 1) & "] = " & polylineN.PathArray.GetLength(0).ToString())
					treeViewRouteGeometry.Nodes.Add(newNode)

					Dim j As Integer = 0
					Do While j < polylineN.PathArray.GetLength(0)
						Dim points As WebService.Point() = polylineN.PathArray(j).PointArray

						Dim k As Integer = 0
						Do While k < points.GetLength(0)
							Dim point As PointN = TryCast(points(k), PointN)
							If Not point Is Nothing Then
								treeViewRouteGeometry.Nodes(i).Nodes.Add(New TreeNode("Point [" & (k + 1) & "]: " & point.X & "," & point.Y))
							End If
							k += 1
						Loop
						j += 1
					Loop
				End If
				i += 1
			Loop

			' Check if Route Geometry has been generated
			If polylines.Length = 0 Then
				Dim newNode As TreeNode = New TreeNode("Route Geometry not generated")
				treeViewRouteGeometry.Nodes.Add(newNode)
			End If
			' Begin repainting the TreeView.
			treeViewRouteGeometry.ExpandAll()
			treeViewRouteGeometry.EndUpdate()
		End Sub

		''' <summary>
		''' Output Directions if a TreeView control
		''' </summary> 
		Private Sub OutputDirections(ByVal serverDirections As NAStreetDirections())
			If serverDirections Is Nothing Then
				treeViewDirections.Nodes.Clear()
				Dim newNode As TreeNode = New TreeNode("Directions not generated")
				treeViewDirections.Nodes.Add(newNode)
				Return
			End If

			' Suppress repainting the TreeView until all the objects have been created.
			treeViewDirections.BeginUpdate()

			' Clear the TreeView each time the method is called.
			treeViewDirections.Nodes.Clear()

			Dim i As Integer = serverDirections.GetLowerBound(0)
			Do While i <= serverDirections.GetUpperBound(0)
				' get Directions from the ith route
				Dim directions As NAStreetDirections
				directions = serverDirections(i)

				' get Summary (Total Distance and Time)
				Dim direction As NAStreetDirection = directions.Summary
				Dim totallength As String = Nothing, totaltime As String = Nothing
				Dim SummaryStrings As String() = direction.Strings
				Dim k As Integer = SummaryStrings.GetLowerBound(0)
				Do While k < SummaryStrings.GetUpperBound(0)
					If direction.StringTypes(k) = esriDirectionsStringType.esriDSTLength Then
						totallength = SummaryStrings(k)
					End If
					If direction.StringTypes(k) = esriDirectionsStringType.esriDSTTime Then
						totaltime = SummaryStrings(k)
					End If
					k += 1
				Loop

				' Add a Top a Node with the Route number and Total Distance and Total Time
				Dim newNode As TreeNode = New TreeNode("Directions for Route [" & (i + 1) & "] - Total Distance: " & totallength & " Total Time: " & totaltime)
				treeViewDirections.Nodes.Add(newNode)

				' Then add a node for each step-by-step directions
				Dim StreetDirections As NAStreetDirection() = directions.Directions
				Dim directionIndex As Integer = StreetDirections.GetLowerBound(0)
				Do While directionIndex <= StreetDirections.GetUpperBound(0)
					Dim streetDirection As NAStreetDirection = StreetDirections(directionIndex)
					Dim StringStreetDirection As String() = streetDirection.Strings
					Dim stringIndex As Integer = StringStreetDirection.GetLowerBound(0)
					Do While stringIndex <= StringStreetDirection.GetUpperBound(0)
						If streetDirection.StringTypes(stringIndex) = esriDirectionsStringType.esriDSTGeneral OrElse streetDirection.StringTypes(stringIndex) = esriDirectionsStringType.esriDSTDepart OrElse streetDirection.StringTypes(stringIndex) = esriDirectionsStringType.esriDSTArrive Then
							treeViewDirections.Nodes(i).Nodes.Add(New TreeNode(StringStreetDirection(stringIndex)))
						End If
						stringIndex += 1
					Loop
					directionIndex += 1
				Loop
				i += 1
			Loop

			' Check if Directions have been generated
			If serverDirections.Length = 0 Then
				Dim newNode As TreeNode = New TreeNode("Directions not generated")
				treeViewDirections.Nodes.Add(newNode)
			End If

			' Begin repainting the TreeView.
			treeViewDirections.ExpandAll()
			treeViewDirections.EndUpdate()
		End Sub

		''' <summary>
		''' Generic function to output RecordSet (Stops, Barriers, or Routes) in a DataDrid Control
		''' </summary> 
		Private Sub OutputRecSetToDataGrid(ByVal recSet As RecordSet, ByVal outDataGrid As System.Windows.Forms.DataGrid)

			Dim fields As Fields = recSet.Fields
			Dim fldArray As Field() = fields.FieldArray

			Dim dataSet As DataSet = New DataSet("dataSet")
			Dim dataTable As DataTable = New DataTable("Results")
			dataSet.Tables.Add(dataTable)

			Dim dataColumn As DataColumn = Nothing
			Dim f As Integer = 0
			Do While f < fldArray.Length
				dataColumn = New DataColumn(fldArray(f).Name)
				dataTable.Columns.Add(dataColumn)
				f += 1
			Loop

			' Populate DataGrid rows by RecordSet
			Dim newDataRow As DataRow
			Dim records As Record() = recSet.Records
			Dim record As Record = Nothing
			Dim values As Object() = Nothing
			Dim j As Integer = 0
			Do While j < records.Length
				newDataRow = dataTable.NewRow()
				record = records(j)
				values = record.Values
				Dim l As Integer = 0
				Do While l < values.Length
					If Not values(l) Is Nothing Then
						newDataRow(l) = values(l).ToString()
					End If
					l += 1
				Loop
				dataTable.Rows.Add(newDataRow)
				j += 1
			Loop

			outDataGrid.SetDataBinding(dataSet, "Results")
			outDataGrid.Visible = True
		End Sub

		''' <summary>
		''' Enable the startTime control
		''' </summary> 
		Private Sub chkUseStartTime_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUseStartTime.CheckedChanged
			If chkUseStartTime.Checked = True Then
				txtStartTime.Enabled = True
			Else
				txtStartTime.Enabled = False
			End If

		End Sub

		''' <summary>
		''' Helper function to convert a string unit (e.g Miles) to an esriNetworkAnalystUnits (e.g esriNAUMiles)
		''' </summary>         
		Private Function GetstringToesriUnits(ByVal stresriUnits As String) As esriNetworkAttributeUnits
			Select Case stresriUnits.ToLower()
				Case "inches"
					Return esriNetworkAttributeUnits.esriNAUInches
				Case "feet"
					Return esriNetworkAttributeUnits.esriNAUFeet
				Case "yards"
					Return esriNetworkAttributeUnits.esriNAUYards
				Case "miles"
					Return esriNetworkAttributeUnits.esriNAUMiles
				Case "nautical miles"
					Return esriNetworkAttributeUnits.esriNAUNauticalMiles
				Case "millimeters"
					Return esriNetworkAttributeUnits.esriNAUMillimeters
				Case "centimeters"
					Return esriNetworkAttributeUnits.esriNAUCentimeters
				Case "meters"
					Return esriNetworkAttributeUnits.esriNAUMeters
				Case "kilometers"
					Return esriNetworkAttributeUnits.esriNAUKilometers
				Case "decimal degrees"
					Return esriNetworkAttributeUnits.esriNAUDecimalDegrees
				Case "decimeters"
					Return esriNetworkAttributeUnits.esriNAUDecimeters
				Case "seconds"
					Return esriNetworkAttributeUnits.esriNAUSeconds
				Case "minutes"
					Return esriNetworkAttributeUnits.esriNAUMinutes
				Case "hours"
					Return esriNetworkAttributeUnits.esriNAUHours
				Case "days"
					Return esriNetworkAttributeUnits.esriNAUDays
				Case "unknown"
					Return esriNetworkAttributeUnits.esriNAUUnknown
				Case Else
					Return esriNetworkAttributeUnits.esriNAUUnknown
			End Select
		End Function

		''' <summary>
		''' Helper function to extract the impedance name in a string like "impedancename (impedanceunits)"
		''' </summary> 
		Private Function ExtractImpedanceName(ByVal impedanceUnits As String) As String
			Dim firstIndex As Integer = impedanceUnits.LastIndexOf(" ")
			Dim lastIndex As Integer = impedanceUnits.Length
			Return impedanceUnits.Remove(firstIndex, (lastIndex - firstIndex))
		End Function

		''' <summary>
		''' Helper function to extract the impedance unit in a string like "impedancename (impedanceunits)"
		''' </summary> 
		Private Function ExtractImpedanceUnits(ByVal impedanceUnits As String) As String
			Dim firstIndex As Integer = impedanceUnits.LastIndexOf("(") + 1
			Dim lastIndex As Integer = impedanceUnits.LastIndexOf(")")
			Return impedanceUnits.Substring(firstIndex, (lastIndex - firstIndex))
		End Function

		''' <summary>
		''' Enable chkPreserveFirst and chkPreserveFirst controls
		''' </summary> 
		Private Sub chkBestOrder_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBestOrder.CheckedChanged
			If chkBestOrder.Checked = True Then
				Me.chkPreserveFirst.Enabled = True
				Me.chkPreserveLast.Enabled = True
			Else
				Me.chkPreserveFirst.Enabled = False
				Me.chkPreserveLast.Enabled = False
			End If
		End Sub

		''' <summary>
		''' Hide tabs
		''' </summary> 
		Private Sub HideTabs()
			' remove tabs initially - There are added when the result is returned.			
			Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnDirections)
			Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnStopsFeatures)
			Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnBarrierFeatures)
			Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnRouteGeometry)
			Me.tabCtrlOutput.TabPages.Remove(Me.tabReturnRouteFeatures)
		End Sub

		''' <summary>
		'// Add a tab page and a tab control
		''' </summary> 
		Private Sub AddTabAndControl(ByVal tabPage As TabPage, ByVal controlToAdd As System.Windows.Forms.Control)
			If (Not tabCtrlOutput.TabPages.Contains(tabPage)) Then
				tabCtrlOutput.TabPages.Add(tabPage)
				tabPage.Controls.Add(controlToAdd)
			End If
		End Sub
	End Class
End Namespace