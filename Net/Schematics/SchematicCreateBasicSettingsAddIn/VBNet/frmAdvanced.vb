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
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Schematic

Partial Public Class frmAdvanced

	Public Event doneFormEvent As EventHandler(Of AdvancedEvents)
    Public strLayers As String
    Public strNodeLayers As String
	Public m_myCol As NameValueCollection = New NameValueCollection()
	Public m_colFieldsToCreate As NameValueCollection = New NameValueCollection()

	Public Sub New()
		InitializeComponent()
		AddHandler Me.Load, New EventHandler(AddressOf frmAdvanced_Load)
	End Sub

	Sub frmAdvanced_Load(ByVal sender As Object, ByVal e As EventArgs)

		Dim myItems() As String
		Dim splitter() As Char = {";"}

		myItems = strLayers.Split(splitter)

		For Each s As String In myItems
			If (s.Length > 0) Then
                Me.tvFeatureClasses.Nodes.Add(s, s)
            End If
        Next

        myItems = strNodeLayers.Split(splitter)

        For Each s As String In myItems
            If (s.Length > 0) Then
                Me.cboRoot.Items.Add(s)
            End If
        Next
	End Sub

	Private Sub chkApplyAlgo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (chkApplyAlgo.Checked = True) Then
			cboDirection.Enabled = True
			cboRoot.Enabled = True
		Else
			cboDirection.Enabled = False
			cboDirection.Text = ""
			cboRoot.Enabled = False
			cboRoot.Text = ""
		End If
	End Sub

	Private Sub btnDone_Click(ByVal sender As Object, ByVal e As EventArgs)
		'raise event back to controller
		Dim dicAlgoParams As Dictionary(Of String, String) = New Dictionary(Of String, String)()
		Dim strAlgo As String = ""
		Dim strRoot As String = ""

		If (chkApplyAlgo.Checked = True) Then
			dicAlgoParams.Add("Direction", cboDirection.Text)
			strAlgo = "SmartTree"
			strRoot = cboRoot.Text
		End If
		Dim evts As AdvancedEvents = New AdvancedEvents(strAlgo, dicAlgoParams, strRoot, m_colFieldsToCreate)
		RaiseEvent doneFormEvent(sender, evts)
		m_myCol.Clear()
		m_colFieldsToCreate.Clear()
	End Sub

	Private Sub chkUseAttributes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (chkUseAttributes.Checked = True) Then
			tvFeatureClasses.Enabled = True
			chkFields.Enabled = True
		Else
			tvFeatureClasses.Enabled = False
			chkFields.Enabled = False
			chkFields.Items.Clear()
			m_colFieldsToCreate.Clear()
		End If
	End Sub

	Private Sub chkFields_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim strFields() As String = m_colFieldsToCreate.GetValues(tvFeatureClasses.SelectedNode.Name.ToString())

		If (strFields IsNot Nothing) Then
			'clear that key and start over
			m_colFieldsToCreate.Remove(tvFeatureClasses.SelectedNode.Name.ToString())
		End If

		If (chkFields.CheckedItems.Count > 0) Then

			For Each s As String In chkFields.CheckedItems
				m_colFieldsToCreate.Add(tvFeatureClasses.SelectedNode.Name.ToString(), s)
			Next
		End If
	End Sub

	Private Sub tvFeatureClasses_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs)
		Dim strFCName As String = e.Node.Text.ToString()
		'load chkfields
		chkFields.Items.Clear()
		Dim strFields() As String = m_myCol.GetValues(strFCName)

		For Each s As String In strFields
			chkFields.Items.Add(s)
		Next

		're-check the boxes if they already did check some
		If (m_colFieldsToCreate.Count > 0) Then

			Dim x As Integer = -1
			Dim strCheckedItems() As String = m_colFieldsToCreate.GetValues(tvFeatureClasses.SelectedNode.Name.ToString())
			If (strCheckedItems IsNot Nothing) Then
				For Each s As String In strCheckedItems
					x = chkFields.FindStringExact(s, -1)
					If (x <> -1) Then
						chkFields.SetItemChecked(x, True)
					End If
				Next
			End If
		End If
	End Sub

End Class
