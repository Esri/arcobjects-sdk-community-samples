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
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem

  ''' <summary>
  ''' The IdentifyDlg is used by the Identify object to display the identify results
  ''' </summary>
	Public Class IdentifyDlg : Inherits System.Windows.Forms.Form
	Private listView1 As System.Windows.Forms.ListView
	Private columnField As System.Windows.Forms.ColumnHeader
	Private columnValue As System.Windows.Forms.ColumnHeader
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
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
			Me.listView1 = New System.Windows.Forms.ListView
			Me.columnField = New System.Windows.Forms.ColumnHeader
			Me.columnValue = New System.Windows.Forms.ColumnHeader
			Me.SuspendLayout()
			'
			'listView1
			'
			Me.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnField, Me.columnValue})
			Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.listView1.Location = New System.Drawing.Point(0, 0)
			Me.listView1.Name = "listView1"
			Me.listView1.Size = New System.Drawing.Size(314, 272)
			Me.listView1.TabIndex = 0
			Me.listView1.UseCompatibleStateImageBehavior = False
			Me.listView1.View = System.Windows.Forms.View.Details
			'
			'columnField
			'
			Me.columnField.Text = "Field"
			Me.columnField.Width = 100
			'
			'columnValue
			'
			Me.columnValue.Width = 200
			'
			'IdentifyDlg
			'
			Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
			Me.ClientSize = New System.Drawing.Size(314, 272)
			Me.Controls.Add(Me.listView1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Name = "IdentifyDlg"
			Me.ShowInTaskbar = False
			Me.Text = "Identify Dialog"
			Me.ResumeLayout(False)

		End Sub
		#End Region

	''' <summary>
	''' Populates the dialog's listview in order to display the identify results
	''' </summary>
	''' <param name="propSet"></param>
	''' <remarks>The identify results are passed by the layer by the IdentifyObject through a PropertySet</remarks>
	Public Sub SetProperties(ByVal propSet As IPropertySet)
	  If Nothing Is propSet Then
		Return
	  End If

        'The listView gets pairs of items since it has two columns for fields and value

	  Dim id As String = Convert.ToString(propSet.GetProperty("ID"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"ID", id}))

	  Dim zipCode As String = Convert.ToString(propSet.GetProperty("ZIPCODE"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"ZIPCODE", zipCode}))

	  Dim cityName As String = Convert.ToString(propSet.GetProperty("CITYNAME"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"CITYNAME", cityName}))

	  Dim latitude As String = Convert.ToString(propSet.GetProperty("LAT"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"LATITUDE", latitude}))

	  Dim longitude As String = Convert.ToString(propSet.GetProperty("LON"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"LONGITUDE", longitude}))

	  Dim temperature As String = Convert.ToString(propSet.GetProperty("TEMPERATURE"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"TEMPERATURE", temperature}))

	  Dim description As String = Convert.ToString(propSet.GetProperty("CONDITION"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"DESCRIPTION", description}))

	  Dim day As String = Convert.ToString(propSet.GetProperty("DAY"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"DAY", day}))

	  Dim [date] As String = Convert.ToString(propSet.GetProperty("DATE"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"DATE", [date]}))

	  Dim low As String = Convert.ToString(propSet.GetProperty("LOW"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"LOW", low}))

	  Dim high As String = Convert.ToString(propSet.GetProperty("HIGH"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"HIGH", high}))

	  Dim updated As String = Convert.ToDateTime(propSet.GetProperty("UPDATED")).ToLongTimeString()
	  listView1.Items.Add(New ListViewItem(New String(1) {"UPDATED", updated}))

	  Dim icon As String = Convert.ToString(propSet.GetProperty("ICONNAME"))
	  listView1.Items.Add(New ListViewItem(New String(1) {"ICON", icon}))
	End Sub
	End Class

