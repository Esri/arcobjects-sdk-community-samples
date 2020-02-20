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
Imports Microsoft.VisualBasic
Imports System
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.NetworkAnalyst
Imports System.Windows.Forms
Imports System.Collections.Generic

' This form allows users to load locations from another point feature layer into the selected NALayer and active category.
Namespace NAEngine
	Public Class frmLoadLocations : Inherits System.Windows.Forms.Form
		Private lblInputData As System.Windows.Forms.Label
		Private chkUseSelection As System.Windows.Forms.CheckBox
		Private WithEvents btnCancel As System.Windows.Forms.Button
		Private WithEvents btnOK As System.Windows.Forms.Button
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		Private WithEvents cboInputData As System.Windows.Forms.ComboBox

		Private m_okClicked As Boolean
		Private m_listDisplayTable As System.Collections.IList

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
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
			Me.cboInputData = New System.Windows.Forms.ComboBox()
			Me.lblInputData = New System.Windows.Forms.Label()
			Me.chkUseSelection = New System.Windows.Forms.CheckBox()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.btnOK = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' cboInputData
			' 
			Me.cboInputData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cboInputData.Location = New System.Drawing.Point(82, 9)
			Me.cboInputData.Name = "cboInputData"
			Me.cboInputData.Size = New System.Drawing.Size(381, 21)
			Me.cboInputData.TabIndex = 0
'			Me.cboInputData.SelectedIndexChanged += New System.EventHandler(Me.cboInputData_SelectedIndexChanged);
			' 
			' lblInputData
			' 
			Me.lblInputData.Location = New System.Drawing.Point(12, 12)
			Me.lblInputData.Name = "lblInputData"
			Me.lblInputData.Size = New System.Drawing.Size(64, 24)
			Me.lblInputData.TabIndex = 1
			Me.lblInputData.Text = "Input Data"
			' 
			' chkUseSelection
			' 
			Me.chkUseSelection.Location = New System.Drawing.Point(15, 39)
			Me.chkUseSelection.Name = "chkUseSelection"
			Me.chkUseSelection.Size = New System.Drawing.Size(419, 16)
			Me.chkUseSelection.TabIndex = 2
			Me.chkUseSelection.Text = "Use Selection"
			' 
			' btnCancel
			' 
			Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnCancel.Location = New System.Drawing.Point(351, 61)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(112, 32)
			Me.btnCancel.TabIndex = 6
			Me.btnCancel.Text = "&Cancel"
'			Me.btnCancel.Click += New System.EventHandler(Me.btnCancel_Click);
			' 
			' btnOK
			' 
			Me.btnOK.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnOK.Location = New System.Drawing.Point(223, 61)
			Me.btnOK.Name = "btnOK"
			Me.btnOK.Size = New System.Drawing.Size(112, 32)
			Me.btnOK.TabIndex = 5
			Me.btnOK.Text = "&OK"
'			Me.btnOK.Click += New System.EventHandler(Me.btnOK_Click);
			' 
			' frmLoadLocations
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(479, 100)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOK)
			Me.Controls.Add(Me.chkUseSelection)
			Me.Controls.Add(Me.lblInputData)
			Me.Controls.Add(Me.cboInputData)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.Name = "frmLoadLocations"
			Me.ShowInTaskbar = False
			Me.Text = "Load Locations"
			Me.ResumeLayout(False)

		End Sub
		#End Region

		Public Function ShowModal(ByVal mapControl As IMapControl3, ByVal naEnv As IEngineNetworkAnalystEnvironment) As Boolean
			' Initialize variables
			m_okClicked = False
			m_listDisplayTable = New System.Collections.ArrayList()

            Dim activeCategory As IEngineNAWindowCategory2 = TryCast(naEnv.NAWindow.ActiveCategory, IEngineNAWindowCategory2)
			If activeCategory Is Nothing Then
				Return False
			End If

			Dim dataLayer As IDataLayer = activeCategory.DataLayer
			If dataLayer Is Nothing Then
				Return False
			End If

			' Set up the title of this dialog
			Dim dataLayerName As String = GetDataLayerName(dataLayer)
			If dataLayerName.Length = 0 Then
				Return False
			End If

			Me.Text = "Load Items into " & dataLayerName

			' Make sure the combo box lists only the appropriate possible input data layers
			PopulateInputDataComboBox(mapControl, dataLayer)

			'Select the first display table from the list
			If cboInputData.Items.Count > 0 Then
				cboInputData.SelectedIndex = 0
			End If

			' Show the window
			Me.ShowDialog()

			' If we selected a layer and clicked OK, load the locations
			If m_okClicked AndAlso (cboInputData.SelectedIndex >= 0) Then
				Try
					' Get a cursor on the source display table (either though the selection set or table)
					' Use IDisplayTable because it accounts for joins, querydefs, etc.
					' IDisplayTable is implemented by FeatureLayers and StandaloneTables.
					'
					Dim displayTable As IDisplayTable = TryCast(m_listDisplayTable(cboInputData.SelectedIndex), IDisplayTable)
                    Dim cursor As ICursor = Nothing
					If chkUseSelection.Checked Then
						Dim selSet As ISelectionSet
						selSet = displayTable.DisplaySelectionSet
						selSet.Search(Nothing, False, cursor)
					Else
						cursor = displayTable.SearchDisplayTable(Nothing, False)
					End If

					' Get the NAContext from the active analysis layer
					Dim naContext As INAContext = naEnv.NAWindow.ActiveAnalysis.Context

					' Get the dataset for the active NAClass  
					Dim naDataset As IDataset = TryCast(activeCategory.NAClass, IDataset)

					' Setup NAClassLoader and Load Locations
                    Dim naClassLoader As INAClassLoader2 = TryCast(New NAClassLoader(), INAClassLoader2)
					naClassLoader.Initialize(naContext, naDataset.Name, cursor)

					' Avoid loading network locations onto non-traversable portions of elements
					Dim locator As INALocator3 = TryCast(naContext.Locator, INALocator3)
					locator.ExcludeRestrictedElements = True
					locator.CacheRestrictedElements(naContext)

					Dim rowsIn As Integer = 0
					Dim rowsLocated As Integer = 0
					naClassLoader.Load(cursor, Nothing, rowsIn, rowsLocated)

					' Let the user know if some of the rows failed to locate
					If rowsIn <> rowsLocated Then
						MessageBox.Show("Out of " & rowsIn & " + rows, " & rowsLocated & " rows were located", "Loading locations", MessageBoxButtons.OK, MessageBoxIcon.Information)
					End If
				Catch e As Exception
					MessageBox.Show(e.Message, "Loading locations failure", MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try

				Return True
			End If

			Return False
		End Function

		Private Function GetDataLayerName(ByVal dataLayer As IDataLayer) As String
            Dim pFeatureLayer As IFeatureLayer = TryCast(dataLayer, IFeatureLayer)
            Dim pStandaloneTable As IStandaloneTable = TryCast(dataLayer, IStandaloneTable)
            Dim pLayer As ILayer = TryCast(dataLayer, ILayer)

			If Not pFeatureLayer Is Nothing AndAlso pLayer.Valid Then
				Return pLayer.Name
			ElseIf Not pStandaloneTable Is Nothing Then
				Return pStandaloneTable.Name
			End If

			Return ""
		End Function

		Private Function GetDataLayerGeometryType(ByVal dataLayer As IDataLayer) As esriGeometryType
            Dim pFeatureLayer As IFeatureLayer = TryCast(dataLayer, IFeatureLayer)
            Dim pLayer As ILayer = TryCast(dataLayer, ILayer)
			If Not pFeatureLayer Is Nothing AndAlso pLayer.Valid Then
				Dim pFeatureClass As IFeatureClass = pFeatureLayer.FeatureClass
				If Not pFeatureClass Is Nothing Then
					Return pFeatureClass.ShapeType
				End If
			End If

			Return esriGeometryType.esriGeometryNull
		End Function

		Private Sub PopulateInputDataComboBox(ByVal mapControl As IMapControl3, ByVal dataLayer As IDataLayer)
			Dim targetGeoType As esriGeometryType = GetDataLayerGeometryType(dataLayer)

			Dim sourceLayers As IEnumLayer = Nothing
			Dim sourceLayer As ILayer = Nothing
			Dim sourceDisplayTable As IDisplayTable = Nothing
			Dim searchInterfaceUID As UID = New UID()

			If targetGeoType <> esriGeometryType.esriGeometryNull Then
				' Only include layers that are of type IFeatureLayer
				searchInterfaceUID.Value = GetType(IFeatureLayer).GUID.ToString("B")
                sourceLayers = mapControl.Map.Layers(searchInterfaceUID, True)

				' iterate over all of the feature layers
				sourceLayer = sourceLayers.Next()
				Do While Not sourceLayer Is Nothing
					' Verify that the layer is a feature layer and a display table
					Dim sourceFeatureLayer As IFeatureLayer = TryCast(sourceLayer, IFeatureLayer)
					sourceDisplayTable = TryCast(sourceLayer, IDisplayTable)
					If (Not sourceFeatureLayer Is Nothing) AndAlso (Not sourceDisplayTable Is Nothing) Then
						' Make sure that the geometry of the feature layer matches the geometry
						'   of the class into which we are loading
						Dim sourceFeatureClass As IFeatureClass = sourceFeatureLayer.FeatureClass
						Dim sourceGeoType As esriGeometryType = sourceFeatureClass.ShapeType
						If (sourceGeoType = targetGeoType) OrElse (targetGeoType = esriGeometryType.esriGeometryPoint AndAlso sourceGeoType = esriGeometryType.esriGeometryMultipoint) Then
							' Add the layer name to the combobox and the layer to the list
							cboInputData.Items.Add(sourceLayer.Name)
							m_listDisplayTable.Add(sourceDisplayTable)
						End If
					End If

					sourceLayer = sourceLayers.Next()
				Loop
			' The layer being loaded into has no geometry type
			Else
				' Find all of the standalone table that are not part of an NALayer
				Dim sourceStandaloneTables As IStandaloneTableCollection = TryCast(mapControl.Map, IStandaloneTableCollection)
				Dim sourceStandaloneTable As IStandaloneTable = Nothing
				sourceDisplayTable = Nothing

				Dim count As Integer = 0
				If Not sourceStandaloneTables Is Nothing Then
					count = sourceStandaloneTables.StandaloneTableCount
				End If

				Dim i As Integer = 0
				Do While i < count
                    sourceStandaloneTable = sourceStandaloneTables.StandaloneTable(i)
					sourceDisplayTable = TryCast(sourceStandaloneTable, IDisplayTable)

					If (Not sourceStandaloneTable Is Nothing) AndAlso (Not sourceDisplayTable Is Nothing) Then
						' Add the table name to the combobox and the layer to the list
						cboInputData.Items.Add(sourceStandaloneTable.Name)
						m_listDisplayTable.Add(sourceDisplayTable)
					End If
					i += 1
				Loop

				' Find all of the standalone tables that are part of an NALayer
				searchInterfaceUID.Value = GetType(INALayer).GUID.ToString("B")
                sourceLayers = mapControl.Map.Layers(searchInterfaceUID, True)

				sourceLayer = sourceLayers.Next()
				Do While Not sourceLayer Is Nothing
					Dim sourceNALayer As INALayer = TryCast(sourceLayer, INALayer)
					If Not sourceNALayer Is Nothing Then
						sourceStandaloneTables = TryCast(sourceNALayer, IStandaloneTableCollection)
						sourceStandaloneTable = Nothing
						sourceDisplayTable = Nothing

						count = 0
						If Not sourceStandaloneTables Is Nothing Then
							count = sourceStandaloneTables.StandaloneTableCount
						End If

						i = 0
						Do While i < count
                            sourceStandaloneTable = sourceStandaloneTables.StandaloneTable(i)
							sourceDisplayTable = TryCast(sourceStandaloneTable, IDisplayTable)

							If (Not sourceStandaloneTable Is Nothing) AndAlso (Not sourceDisplayTable Is Nothing) Then
								' Add the table name to the combobox and the layer to the list
								cboInputData.Items.Add(sourceStandaloneTable.Name)
								m_listDisplayTable.Add(sourceDisplayTable)
							End If
							i += 1
						Loop
					End If

					sourceLayer = sourceLayers.Next()
				Loop
			End If
		End Sub

		Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
			m_okClicked = True
			Me.Close()
		End Sub

		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
			m_okClicked = False
			Me.Close()
		End Sub

		Private Sub cboInputData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInputData.SelectedIndexChanged
			' Set the chkUseSelectedFeatures control based on if anything is selected or not
			If cboInputData.SelectedIndex >= 0 Then
				Dim displayTable As IDisplayTable = TryCast(m_listDisplayTable(cboInputData.SelectedIndex), IDisplayTable)
				chkUseSelection.Checked = (displayTable.DisplaySelectionSet.Count > 0)
				chkUseSelection.Enabled = (displayTable.DisplaySelectionSet.Count > 0)
			End If
		End Sub
	End Class
End Namespace
