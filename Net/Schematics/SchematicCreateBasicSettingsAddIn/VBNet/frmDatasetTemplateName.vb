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
' Copyright 2010 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
'
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at &ltyour ArcGIS install location&gt/DeveloperKit10.0/userestrictions.txt.

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcCatalog
Imports ESRI.ArcGIS.Catalog

Partial Public Class frmDatasetTemplateName
	Public blnNewDataset As Boolean = False
	Public Event cancelFormEvent As EventHandler
	Public Event nextFormEvent As EventHandler(Of NameEvents)

	Public Sub New()
		InitializeComponent()
		AddHandler Me.btnNext.Click, AddressOf btnNext_Click
		AddHandler Me.txtDatasetName.TextChanged, AddressOf txtDatasetName_TextChanged
		AddHandler Me.txtTemplateName.TextChanged, AddressOf txtTemplateName_TextChanged
	End Sub

	Private Sub frmDatasetTemplateName_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

		If (blnNewDataset = False) Then

			txtDatasetName.Enabled = False
			txtDatasetName.Text = ArcCatalog.ThisApplication.SelectedObject.Name
		End If
	End Sub

	'Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
	'	'RaiseEvent cancelFormEvent(sender, e)
	'End Sub

	Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim evts As NameEvents = New NameEvents(blnNewDataset, txtDatasetName.Text, txtTemplateName.Text, chkVertices.Checked)
		RaiseEvent nextFormEvent(sender, evts)
	End Sub

	Private Sub txtDatasetName_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
		If ((txtDatasetName.Text.Length > 0) AndAlso (txtTemplateName.Text.Length > 0)) Then
			btnNext.Enabled = True
		Else
			btnNext.Enabled = False
		End If
	End Sub

	Private Sub txtTemplateName_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
		If ((txtDatasetName.Text.Length > 0) AndAlso (txtTemplateName.Text.Length > 0)) Then
			btnNext.Enabled = True
		Else
			btnNext.Enabled = False
		End If
	End Sub

End Class
