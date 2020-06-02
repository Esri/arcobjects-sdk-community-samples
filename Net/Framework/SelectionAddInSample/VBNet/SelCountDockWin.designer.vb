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
Namespace SelectionSample
  Partial Public Class SelCountDockWin
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	  If disposing AndAlso (components IsNot Nothing) Then
		components.Dispose()
	  End If
	  MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
	  Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(SelCountDockWin))
	  Me.listView1 = New System.Windows.Forms.ListView()
	  Me.LayerName = New System.Windows.Forms.ColumnHeader()
	  Me.FeatureCount = New System.Windows.Forms.ColumnHeader()
	  Me.label1 = New System.Windows.Forms.Label()
	  Me.SuspendLayout()
	  ' 
	  ' listView1
	  ' 
	  Me.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
	  Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.LayerName, Me.FeatureCount})
	  resources.ApplyResources(Me.listView1, "listView1")
	  Me.listView1.Items.AddRange(New System.Windows.Forms.ListViewItem() { (CType(resources.GetObject("listView1.Items"), System.Windows.Forms.ListViewItem)), (CType(resources.GetObject("listView1.Items1"), System.Windows.Forms.ListViewItem))})
	  Me.listView1.Name = "listView1"
	  Me.listView1.UseCompatibleStateImageBehavior = False
	  ' 
	  ' LayerName
	  ' 
	  resources.ApplyResources(Me.LayerName, "LayerName")
	  ' 
	  ' FeatureCount
	  ' 
	  resources.ApplyResources(Me.FeatureCount, "FeatureCount")
	  ' 
	  ' label1
	  ' 
	  resources.ApplyResources(Me.label1, "label1")
	  Me.label1.Name = "label1"
	  ' 
	  ' SelCountDockWin
	  ' 
	  Me.Controls.Add(Me.label1)
	  Me.Controls.Add(Me.listView1)
	  resources.ApplyResources(Me, "$this")
	  Me.Name = "SelCountDockWin"
	  Me.ResumeLayout(False)
	  Me.PerformLayout()

	End Sub

	#End Region

	Friend listView1 As System.Windows.Forms.ListView
	Private LayerName As System.Windows.Forms.ColumnHeader
	Private FeatureCount As System.Windows.Forms.ColumnHeader
	Private label1 As System.Windows.Forms.Label

  End Class
End Namespace
