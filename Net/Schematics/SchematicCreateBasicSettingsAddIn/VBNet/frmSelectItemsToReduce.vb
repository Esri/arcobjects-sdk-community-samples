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

Partial Public Class frmSelectItemsToReduce
	Public Event cancelFormEvent As EventHandler
	Public itemList As String = ""
	Public Event doneFormEvent As EventHandler(Of ReduceEvents)

	Public Sub New()
		InitializeComponent()
		AddHandler Me.Load, New EventHandler(AddressOf frmSelectItemsToReduce_Load)
	End Sub

	Sub frmSelectItemsToReduce_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim myItems() As String
		Dim splitter() As Char = {";"}

		myItems = itemList.Split(splitter)

		For Each s As String In myItems

			If (s.Length > 0) Then
				Me.chkListBox.Items.Add(s)
			End If
		Next
	End Sub

	Private Sub btnDone_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim selectedItems As CheckedListBox.CheckedItemCollection = Me.chkListBox.CheckedItems
		Dim items(Me.chkListBox.CheckedItems.Count - 1) As String
		If (selectedItems.Count > 0) Then
			'do something
			For i As Integer = 0 To selectedItems.Count - 1
				items(i) = selectedItems(i).ToString()
			Next
		End If

		'raise event back to controller
		Dim reduce As ReduceEvents = New ReduceEvents(items)
		RaiseEvent doneFormEvent(sender, reduce)
	End Sub

	Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		RaiseEvent cancelFormEvent(sender, e)
	End Sub
End Class
