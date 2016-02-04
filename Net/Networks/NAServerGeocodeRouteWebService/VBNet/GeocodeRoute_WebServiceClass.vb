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
Imports GeocodeRoute_WebService.WebService

Namespace Route_WebService
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Route_WebServiceClass : Inherits System.Windows.Forms.Form
#Region "Window Controls Declaration"

		Private pictureBox As System.Windows.Forms.PictureBox
		Private cboNALayers As System.Windows.Forms.ComboBox
		Private label8 As System.Windows.Forms.Label
		Private chkReturnMap As System.Windows.Forms.CheckBox
		Private WithEvents cmdSolve As System.Windows.Forms.Button
		Private components As System.ComponentModel.IContainer
		Private chkReturnDirections As System.Windows.Forms.CheckBox
		Private tabCtrlOutput As System.Windows.Forms.TabControl
		Private tabReturnDirections As System.Windows.Forms.TabPage
		Private tabReturnMap As System.Windows.Forms.TabPage
		Private treeViewDirections As System.Windows.Forms.TreeView
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private txtStartStreetAddress As System.Windows.Forms.TextBox
		Private lblStartCity As System.Windows.Forms.Label
		Private txtStartCity As System.Windows.Forms.TextBox
		Private lblStartZipCode As System.Windows.Forms.Label
		Private txtStartZipCode As System.Windows.Forms.TextBox
		Private lblStartStreetAddress As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtStartState As System.Windows.Forms.TextBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private label1 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private txtEndState As System.Windows.Forms.TextBox
		Private txtEndZipCode As System.Windows.Forms.TextBox
		Private txtEndCity As System.Windows.Forms.TextBox
		Private txtEndStreetAddress As System.Windows.Forms.TextBox

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
			Me.cmdSolve = New System.Windows.Forms.Button()
			Me.pictureBox = New System.Windows.Forms.PictureBox()
			Me.chkReturnMap = New System.Windows.Forms.CheckBox()
			Me.cboNALayers = New System.Windows.Forms.ComboBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.chkReturnDirections = New System.Windows.Forms.CheckBox()
			Me.tabCtrlOutput = New System.Windows.Forms.TabControl()
			Me.tabReturnMap = New System.Windows.Forms.TabPage()
			Me.tabReturnDirections = New System.Windows.Forms.TabPage()
			Me.treeViewDirections = New System.Windows.Forms.TreeView()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.txtEndState = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtEndZipCode = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.txtEndCity = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.txtEndStreetAddress = New System.Windows.Forms.TextBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.txtStartState = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txtStartZipCode = New System.Windows.Forms.TextBox()
			Me.lblStartZipCode = New System.Windows.Forms.Label()
			Me.txtStartCity = New System.Windows.Forms.TextBox()
			Me.lblStartCity = New System.Windows.Forms.Label()
			Me.txtStartStreetAddress = New System.Windows.Forms.TextBox()
			Me.lblStartStreetAddress = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.tabCtrlOutput.SuspendLayout()
			Me.tabReturnMap.SuspendLayout()
			Me.tabReturnDirections.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.SuspendLayout()
			' 
			' cmdSolve
			' 
			Me.cmdSolve.Location = New System.Drawing.Point(120, 384)
			Me.cmdSolve.Name = "cmdSolve"
			Me.cmdSolve.Size = New System.Drawing.Size(200, 32)
			Me.cmdSolve.TabIndex = 29
			Me.cmdSolve.Text = "Find Route"
			'			Me.cmdSolve.Click += New System.EventHandler(Me.cmdSolve_Click);
			' 
			' pictureBox
			' 
			Me.pictureBox.BackColor = System.Drawing.Color.White
			Me.pictureBox.Location = New System.Drawing.Point(8, 16)
			Me.pictureBox.Name = "pictureBox"
			Me.pictureBox.Size = New System.Drawing.Size(448, 360)
			Me.pictureBox.TabIndex = 20
			Me.pictureBox.TabStop = False
			' 
			' chkReturnMap
			' 
			Me.chkReturnMap.Checked = True
			Me.chkReturnMap.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnMap.Location = New System.Drawing.Point(8, 64)
			Me.chkReturnMap.Name = "chkReturnMap"
			Me.chkReturnMap.Size = New System.Drawing.Size(96, 16)
			Me.chkReturnMap.TabIndex = 7
			Me.chkReturnMap.Text = "Return Map"
			' 
			' cboNALayers
			' 
			Me.cboNALayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboNALayers.Location = New System.Drawing.Point(112, 24)
			Me.cboNALayers.Name = "cboNALayers"
			Me.cboNALayers.Size = New System.Drawing.Size(248, 21)
			Me.cboNALayers.TabIndex = 3
			' 
			' label8
			' 
			Me.label8.Location = New System.Drawing.Point(8, 24)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(88, 16)
			Me.label8.TabIndex = 71
			Me.label8.Text = "NALayer Name"
			' 
			' chkReturnDirections
			' 
			Me.chkReturnDirections.Checked = True
			Me.chkReturnDirections.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chkReturnDirections.Location = New System.Drawing.Point(160, 64)
			Me.chkReturnDirections.Name = "chkReturnDirections"
			Me.chkReturnDirections.Size = New System.Drawing.Size(160, 16)
			Me.chkReturnDirections.TabIndex = 19
			Me.chkReturnDirections.Text = "Generate Directions"
			' 
			' tabCtrlOutput
			' 
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnMap)
			Me.tabCtrlOutput.Controls.Add(Me.tabReturnDirections)
			Me.tabCtrlOutput.Enabled = False
			Me.tabCtrlOutput.Location = New System.Drawing.Point(440, 32)
			Me.tabCtrlOutput.Name = "tabCtrlOutput"
			Me.tabCtrlOutput.SelectedIndex = 0
			Me.tabCtrlOutput.Size = New System.Drawing.Size(472, 408)
			Me.tabCtrlOutput.TabIndex = 30
			' 
			' tabReturnMap
			' 
			Me.tabReturnMap.Controls.Add(Me.pictureBox)
			Me.tabReturnMap.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnMap.Name = "tabReturnMap"
			Me.tabReturnMap.Size = New System.Drawing.Size(464, 382)
			Me.tabReturnMap.TabIndex = 0
			Me.tabReturnMap.Text = "Map"
			' 
			' tabReturnDirections
			' 
			Me.tabReturnDirections.Controls.Add(Me.treeViewDirections)
			Me.tabReturnDirections.Location = New System.Drawing.Point(4, 22)
			Me.tabReturnDirections.Name = "tabReturnDirections"
			Me.tabReturnDirections.Size = New System.Drawing.Size(464, 382)
			Me.tabReturnDirections.TabIndex = 1
			Me.tabReturnDirections.Text = "Directions"
			' 
			' treeViewDirections
			' 
			Me.treeViewDirections.Location = New System.Drawing.Point(0, 8)
			Me.treeViewDirections.Name = "treeViewDirections"
			Me.treeViewDirections.Size = New System.Drawing.Size(448, 368)
			Me.treeViewDirections.TabIndex = 69
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.txtEndState)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.txtEndZipCode)
			Me.groupBox1.Controls.Add(Me.label3)
			Me.groupBox1.Controls.Add(Me.txtEndCity)
			Me.groupBox1.Controls.Add(Me.label4)
			Me.groupBox1.Controls.Add(Me.txtEndStreetAddress)
			Me.groupBox1.Controls.Add(Me.label5)
			Me.groupBox1.Controls.Add(Me.groupBox3)
			Me.groupBox1.Controls.Add(Me.txtStartState)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Controls.Add(Me.txtStartZipCode)
			Me.groupBox1.Controls.Add(Me.lblStartZipCode)
			Me.groupBox1.Controls.Add(Me.txtStartCity)
			Me.groupBox1.Controls.Add(Me.lblStartCity)
			Me.groupBox1.Controls.Add(Me.txtStartStreetAddress)
			Me.groupBox1.Controls.Add(Me.lblStartStreetAddress)
			Me.groupBox1.Controls.Add(Me.groupBox2)
			Me.groupBox1.Location = New System.Drawing.Point(24, 24)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(408, 232)
			Me.groupBox1.TabIndex = 72
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Enter addresses to get map and directions"
			' 
			' txtEndState
			' 
			Me.txtEndState.Location = New System.Drawing.Point(216, 184)
			Me.txtEndState.Name = "txtEndState"
			Me.txtEndState.Size = New System.Drawing.Size(72, 20)
			Me.txtEndState.TabIndex = 15
			Me.txtEndState.Text = "California"
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(176, 184)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(32, 16)
			Me.label1.TabIndex = 14
			Me.label1.Text = "State"
			' 
			' txtEndZipCode
			' 
			Me.txtEndZipCode.Location = New System.Drawing.Point(344, 184)
			Me.txtEndZipCode.Name = "txtEndZipCode"
			Me.txtEndZipCode.Size = New System.Drawing.Size(48, 20)
			Me.txtEndZipCode.TabIndex = 17
			Me.txtEndZipCode.Text = "94123"
			' 
			' label3
			' 
			Me.label3.Location = New System.Drawing.Point(296, 184)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(56, 16)
			Me.label3.TabIndex = 16
			Me.label3.Text = "Zip Code"
			' 
			' txtEndCity
			' 
			Me.txtEndCity.Location = New System.Drawing.Point(56, 184)
			Me.txtEndCity.Name = "txtEndCity"
			Me.txtEndCity.Size = New System.Drawing.Size(112, 20)
			Me.txtEndCity.TabIndex = 13
			Me.txtEndCity.Text = "San Francisco"
			' 
			' label4
			' 
			Me.label4.Location = New System.Drawing.Point(24, 184)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(64, 16)
			Me.label4.TabIndex = 12
			Me.label4.Text = "City"
			' 
			' txtEndStreetAddress
			' 
			Me.txtEndStreetAddress.Location = New System.Drawing.Point(104, 152)
			Me.txtEndStreetAddress.Name = "txtEndStreetAddress"
			Me.txtEndStreetAddress.Size = New System.Drawing.Size(288, 20)
			Me.txtEndStreetAddress.TabIndex = 11
			Me.txtEndStreetAddress.Text = "171 Capra Way"
			' 
			' label5
			' 
			Me.label5.Location = New System.Drawing.Point(24, 152)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(96, 16)
			Me.label5.TabIndex = 10
			Me.label5.Text = "Street Address"
			' 
			' groupBox3
			' 
			Me.groupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.groupBox3.Location = New System.Drawing.Point(8, 128)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(392, 88)
			Me.groupBox3.TabIndex = 18
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Arriving at:"
			' 
			' txtStartState
			' 
			Me.txtStartState.Location = New System.Drawing.Point(216, 88)
			Me.txtStartState.Name = "txtStartState"
			Me.txtStartState.Size = New System.Drawing.Size(72, 20)
			Me.txtStartState.TabIndex = 6
			Me.txtStartState.Text = "California"
			' 
			' label2
			' 
			Me.label2.Location = New System.Drawing.Point(176, 90)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(32, 16)
			Me.label2.TabIndex = 5
			Me.label2.Text = "State"
			' 
			' txtStartZipCode
			' 
			Me.txtStartZipCode.Location = New System.Drawing.Point(344, 88)
			Me.txtStartZipCode.Name = "txtStartZipCode"
			Me.txtStartZipCode.Size = New System.Drawing.Size(48, 20)
			Me.txtStartZipCode.TabIndex = 8
			Me.txtStartZipCode.Text = "94110"
			' 
			' lblStartZipCode
			' 
			Me.lblStartZipCode.Location = New System.Drawing.Point(296, 90)
			Me.lblStartZipCode.Name = "lblStartZipCode"
			Me.lblStartZipCode.Size = New System.Drawing.Size(56, 16)
			Me.lblStartZipCode.TabIndex = 7
			Me.lblStartZipCode.Text = "Zip Code"
			' 
			' txtStartCity
			' 
			Me.txtStartCity.Location = New System.Drawing.Point(56, 88)
			Me.txtStartCity.Name = "txtStartCity"
			Me.txtStartCity.Size = New System.Drawing.Size(112, 20)
			Me.txtStartCity.TabIndex = 4
			Me.txtStartCity.Text = "San Francisco"
			' 
			' lblStartCity
			' 
			Me.lblStartCity.Location = New System.Drawing.Point(24, 90)
			Me.lblStartCity.Name = "lblStartCity"
			Me.lblStartCity.Size = New System.Drawing.Size(64, 16)
			Me.lblStartCity.TabIndex = 3
			Me.lblStartCity.Text = "City"
			' 
			' txtStartStreetAddress
			' 
			Me.txtStartStreetAddress.Location = New System.Drawing.Point(104, 56)
			Me.txtStartStreetAddress.Name = "txtStartStreetAddress"
			Me.txtStartStreetAddress.Size = New System.Drawing.Size(288, 20)
			Me.txtStartStreetAddress.TabIndex = 2
			Me.txtStartStreetAddress.Text = "113 Cleridge Street"
			' 
			' lblStartStreetAddress
			' 
			Me.lblStartStreetAddress.Location = New System.Drawing.Point(24, 58)
			Me.lblStartStreetAddress.Name = "lblStartStreetAddress"
			Me.lblStartStreetAddress.Size = New System.Drawing.Size(96, 16)
			Me.lblStartStreetAddress.TabIndex = 1
			Me.lblStartStreetAddress.Text = "Street Address"
			' 
			' groupBox2
			' 
			Me.groupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.groupBox2.Location = New System.Drawing.Point(8, 32)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(392, 88)
			Me.groupBox2.TabIndex = 9
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "Starting from:"
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.label8)
			Me.groupBox4.Controls.Add(Me.cboNALayers)
			Me.groupBox4.Controls.Add(Me.chkReturnDirections)
			Me.groupBox4.Controls.Add(Me.chkReturnMap)
			Me.groupBox4.Location = New System.Drawing.Point(24, 272)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(408, 88)
			Me.groupBox4.TabIndex = 72
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Solve Parameters"
			' 
			' Route_WebServiceClass
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(936, 454)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.tabCtrlOutput)
			Me.Controls.Add(Me.cmdSolve)
			Me.Controls.Add(Me.groupBox4)
			Me.Name = "Route_WebServiceClass"
			Me.Text = "NAServer - Geocode Route Web Service"
			'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
			Me.tabCtrlOutput.ResumeLayout(False)
			Me.tabReturnMap.ResumeLayout(False)
			Me.tabReturnDirections.ResumeLayout(False)
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
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
		'''     - gets and displays the server results (map, directions)
		''' </summary>
		Private Sub cmdSolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSolve.Click
			Me.Cursor = Cursors.WaitCursor

			Try
				' Get SolverParams
				Dim solverParams As NAServerSolverParams = TryCast(m_naServer.GetSolverParameters(cboNALayers.Text), NAServerSolverParams)

				' Set Solver params
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
		''' Set server solver paramaters  (ReturnMap, SnapTolerance, etc.)
		''' </summary>
		Private Sub SetServerSolverParams(ByVal solverParams As NAServerSolverParams)
			solverParams.ReturnMap = chkReturnMap.Checked
			solverParams.ImageDescription.ImageDisplay.ImageWidth = pictureBox.Width
			solverParams.ImageDescription.ImageDisplay.ImageHeight = pictureBox.Height


			Dim routeParams As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			If Not routeParams Is Nothing Then
				routeParams.ReturnDirections = chkReturnDirections.Checked
			End If
		End Sub

		''' <summary>
		''' Load form
		''' </summary> 
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Try
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

			Throw (New System.Exception("Could not find the web service."))

		End Sub

		''' <summary>
		''' This function shows how to populate stop locations using an array of PropertySets
		''' </summary>
		Private Sub LoadLocations(ByVal solverParams As NAServerSolverParams)
			' Geocode Addresses
			Dim propSets As PropertySet() = New PropertySet(1) {}
			propSets(0) = GeocodeAddress(txtStartStreetAddress.Text, txtStartCity.Text, txtStartState.Text, txtStartZipCode.Text)
			propSets(1) = GeocodeAddress(txtEndStreetAddress.Text, txtEndCity.Text, txtEndState.Text, txtEndZipCode.Text)

			Dim StopsPropSets As NAServerPropertySets = New NAServerPropertySets()
			StopsPropSets.PropertySets = propSets

			Dim routeParams As NAServerRouteParams = TryCast(solverParams, NAServerRouteParams)
			routeParams.Stops = StopsPropSets
		End Sub

		''' <summary>
		''' Geocode an address based on the street name, city, state, and zip code
		''' Throws and exception and returns null if the address was unmatched.
		''' </summary> 
		Private Function GeocodeAddress(ByVal StreetAddress As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String) As PropertySet
			Dim propSet As PropertySet = Nothing

			Try
				Dim gc As SanFranciscoLocator_GeocodeServer = New SanFranciscoLocator_GeocodeServer()
				Dim pAddressProperties As PropertySet = New PropertySet()

				Dim pAddressFields As Fields
				Dim pField As Field

				Dim propSetProperty As PropertySetProperty() = New PropertySetProperty(3) {}
				pAddressFields = gc.GetAddressFields()
				Dim i As Integer = 0
				Do While i < pAddressFields.FieldArray.GetLength(0)
					pField = pAddressFields.FieldArray(i)

					If pField.Name.ToUpper() = "STREET" Then
						propSetProperty(0) = TryCast(CreatePropertySetProperty(pField.AliasName, StreetAddress), PropertySetProperty)
					End If

					If pField.Name.ToUpper() = "CITY" Then
						propSetProperty(1) = TryCast(CreatePropertySetProperty(pField.AliasName, City), PropertySetProperty)
					End If

					If pField.Name.ToUpper() = "STATE" Then
						propSetProperty(2) = TryCast(CreatePropertySetProperty(pField.AliasName, State), PropertySetProperty)
					End If

					If pField.Name.ToUpper() = "ZIP" Then
						propSetProperty(3) = TryCast(CreatePropertySetProperty(pField.AliasName, ZipCode), PropertySetProperty)
					End If

					i += 1
				Loop

				pAddressProperties.PropertyArray = propSetProperty

				' find the matching address	
				propSet = gc.GeocodeAddress(pAddressProperties, Nothing)
			Catch exception As Exception
				MessageBox.Show(exception.Message, "An error has occurred")
			End Try

			' Throw and error if the geocoded address is "Unmatched"
			If (Not propSet Is Nothing) AndAlso (propSet.PropertyArray(1).Value.ToString() = "U") Then
				Throw (New System.Exception("Could not geocode [" & StreetAddress & "]"))
			End If

			Return propSet
		End Function

		''' <summary>
		''' CreatePropertySetProperty
		''' </summary> 
		Private Function CreatePropertySetProperty(ByVal key As String, ByVal value As Object) As PropertySetProperty
			Dim propSetProperty As PropertySetProperty = New PropertySetProperty()
			propSetProperty.Key = key
			propSetProperty.Value = value
			Return propSetProperty
		End Function

		''' <summary>
		''' Output Results map, Directions
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

			' Output the total impedance of each route
			Dim routeSolverResults As NAServerRouteResults = TryCast(solverResults, NAServerRouteResults)

			'Output Map
			pictureBox.Image = Nothing
			If solverParams.ReturnMap Then
				pictureBox.Image = System.Drawing.Image.FromStream(New System.IO.MemoryStream(solverResults.MapImage.ImageData))
			End If
			pictureBox.Refresh()

			If Not routeSolverResults Is Nothing Then
				OutputDirections(routeSolverResults.Directions)	' Return Directions if generated
			End If

			tabCtrlOutput.Enabled = True
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
	End Class

End Namespace