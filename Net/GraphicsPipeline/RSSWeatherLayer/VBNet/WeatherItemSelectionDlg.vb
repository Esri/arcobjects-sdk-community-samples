Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Threading
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem

  ''' <summary>
  ''' Select by city name dialog
  ''' </summary>
  ''' <remarks>Allows users to select items according to city names.</remarks>
	Public Class WeatherItemSelectionDlg : Inherits System.Windows.Forms.Form
	Private grpWeatherItems As System.Windows.Forms.GroupBox
	Private WithEvents lstWeatherItemNames As System.Windows.Forms.ListBox
	Private WithEvents btnRefreshList As System.Windows.Forms.Button
	Private lblSelect As System.Windows.Forms.Label
	Private WithEvents txtSelect As System.Windows.Forms.TextBox
	Private chkNewSelection As System.Windows.Forms.CheckBox
	Private WithEvents btnSelect As System.Windows.Forms.Button
	Private WithEvents btnDismiss As System.Windows.Forms.Button
	Private progressBar1 As System.Windows.Forms.ProgressBar
	Private contextMenu1 As System.Windows.Forms.ContextMenu
	Private WithEvents menuZoomTo As System.Windows.Forms.MenuItem

	'Class members
	Private m_activeView As IActiveView = Nothing
	Private m_weatherLayer As RSSWeatherLayerClass = Nothing
	Private m_cityNames As String() = Nothing
	Private m_weatherItemsTable As DataTable = Nothing


	' This delegate enables asynchronous calls for setting
	' the text property on a TextBox control.
	Private Delegate Sub IncrementProgressBarCallback()
	Private Delegate Sub AddListItmCallback(ByVal item As String)
	Private Delegate Sub ShowProgressBarCallBack()
	Private Delegate Sub HideProgressBarCallBack()


		''' <summary>
		''' Required designer variable.
		''' </summary>
	Private components As System.ComponentModel.Container = Nothing

	''' <summary>
	''' class constructor
	''' </summary>
	''' <param name="weatherLayer"></param>
	Public Sub New(ByVal weatherLayer As RSSWeatherLayerClass, ByVal activeView As IActiveView)

		InitializeComponent()

		'get the layer
		m_weatherLayer = weatherLayer
		m_activeView = activeView

		'get the list of all citynames for all items in the layer
		m_cityNames = m_weatherLayer.GetCityNames()

		'create a table to host the citynames
		m_weatherItemsTable = New DataTable("CityNames")
		m_weatherItemsTable.Columns.Add("CITYNAME", GetType(String))

		'populate the listbox and build a table containing the items
		PopulateList()
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
		Me.grpWeatherItems = New System.Windows.Forms.GroupBox()
		Me.progressBar1 = New System.Windows.Forms.ProgressBar()
		Me.txtSelect = New System.Windows.Forms.TextBox()
		Me.lblSelect = New System.Windows.Forms.Label()
		Me.btnRefreshList = New System.Windows.Forms.Button()
		Me.lstWeatherItemNames = New System.Windows.Forms.ListBox()
		Me.contextMenu1 = New System.Windows.Forms.ContextMenu()
		Me.menuZoomTo = New System.Windows.Forms.MenuItem()
		Me.chkNewSelection = New System.Windows.Forms.CheckBox()
		Me.btnSelect = New System.Windows.Forms.Button()
		Me.btnDismiss = New System.Windows.Forms.Button()
		Me.grpWeatherItems.SuspendLayout()
		Me.SuspendLayout()
		' 
		' grpWeatherItems
		' 
		Me.grpWeatherItems.Controls.Add(Me.progressBar1)
		Me.grpWeatherItems.Controls.Add(Me.txtSelect)
		Me.grpWeatherItems.Controls.Add(Me.lblSelect)
		Me.grpWeatherItems.Controls.Add(Me.btnRefreshList)
		Me.grpWeatherItems.Controls.Add(Me.lstWeatherItemNames)
		Me.grpWeatherItems.Location = New System.Drawing.Point(4, 8)
		Me.grpWeatherItems.Name = "grpWeatherItems"
		Me.grpWeatherItems.Size = New System.Drawing.Size(200, 328)
		Me.grpWeatherItems.TabIndex = 0
		Me.grpWeatherItems.TabStop = False
		' 
		' progressBar1
		' 
		Me.progressBar1.Location = New System.Drawing.Point(8, 256)
		Me.progressBar1.Name = "progressBar1"
		Me.progressBar1.Size = New System.Drawing.Size(184, 23)
		Me.progressBar1.Step = 1
		Me.progressBar1.TabIndex = 4
		' 
		' txtSelect
		' 
		Me.txtSelect.Location = New System.Drawing.Point(92, 296)
		Me.txtSelect.Name = "txtSelect"
		Me.txtSelect.TabIndex = 3
		Me.txtSelect.Text = ""
		'	  Me.txtSelect.TextChanged += New System.EventHandler(Me.txtSelect_TextChanged);
		' 
		' lblSelect
		' 
		Me.lblSelect.Location = New System.Drawing.Point(8, 300)
		Me.lblSelect.Name = "lblSelect"
		Me.lblSelect.Size = New System.Drawing.Size(52, 16)
		Me.lblSelect.TabIndex = 2
		Me.lblSelect.Text = "Select"
		' 
		' btnRefreshList
		' 
		Me.btnRefreshList.Location = New System.Drawing.Point(64, 256)
		Me.btnRefreshList.Name = "btnRefreshList"
		Me.btnRefreshList.TabIndex = 1
		Me.btnRefreshList.Text = "Refresh List"
		'	  Me.btnRefreshList.Click += New System.EventHandler(Me.btnRefreshList_Click);
		' 
		' lstWeatherItemNames
		' 
		Me.lstWeatherItemNames.ContextMenu = Me.contextMenu1
		Me.lstWeatherItemNames.Location = New System.Drawing.Point(8, 16)
		Me.lstWeatherItemNames.Name = "lstWeatherItemNames"
		Me.lstWeatherItemNames.Size = New System.Drawing.Size(184, 225)
		Me.lstWeatherItemNames.Sorted = True
		Me.lstWeatherItemNames.TabIndex = 0
		'	  Me.lstWeatherItemNames.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.lstWeatherItemNames_MouseDown);
		'	  Me.lstWeatherItemNames.DoubleClick += New System.EventHandler(Me.lstWeatherItemNames_DoubleClick);
		' 
		' contextMenu1
		' 
		Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuZoomTo})
		' 
		' menuZoomTo
		' 
		Me.menuZoomTo.Index = 0
		Me.menuZoomTo.Text = "Zoom To"
		'	  Me.menuZoomTo.Click += New System.EventHandler(Me.menuZoomTo_Click);
		' 
		' chkNewSelection
		' 
		Me.chkNewSelection.Checked = True
		Me.chkNewSelection.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkNewSelection.Location = New System.Drawing.Point(12, 344)
		Me.chkNewSelection.Name = "chkNewSelection"
		Me.chkNewSelection.TabIndex = 1
		Me.chkNewSelection.Text = "New Selection"
		' 
		' btnSelect
		' 
		Me.btnSelect.Location = New System.Drawing.Point(8, 380)
		Me.btnSelect.Name = "btnSelect"
		Me.btnSelect.TabIndex = 2
		Me.btnSelect.Text = "Select"
		'	  Me.btnSelect.Click += New System.EventHandler(Me.btnSelect_Click);
		' 
		' btnDismiss
		' 
		Me.btnDismiss.Location = New System.Drawing.Point(124, 384)
		Me.btnDismiss.Name = "btnDismiss"
		Me.btnDismiss.TabIndex = 3
		Me.btnDismiss.Text = "Dismiss"
		'	  Me.btnDismiss.Click += New System.EventHandler(Me.btnDismiss_Click);
		' 
		' WeatherItemSelectionDlg
		' 
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.ClientSize = New System.Drawing.Size(206, 416)
		Me.Controls.Add(Me.btnDismiss)
		Me.Controls.Add(Me.btnSelect)
		Me.Controls.Add(Me.chkNewSelection)
		Me.Controls.Add(Me.grpWeatherItems)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "WeatherItemSelectionDlg"
		Me.ShowInTaskbar = False
		Me.Text = "Select weather item"
		Me.TopMost = True
		'	  Me.Load += New System.EventHandler(Me.WeatherItemSelectionDlg_Load);
		'	  Me.VisibleChanged += New System.EventHandler(Me.WeatherItemSelectionDlg_VisibleChanged);
		Me.grpWeatherItems.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
#End Region

	''' <summary>
	''' Event handler for Form::Load
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub WeatherItemSelectionDlg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		txtSelect.Text = ""
		'btnRefreshList.Visible  = true;
		'progressBar1.Visible    = false;
		lstWeatherItemNames.ClearSelected()
	End Sub

	''' <summary>
	''' dialog visible change event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub WeatherItemSelectionDlg_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
		If Me.Visible Then
			txtSelect.Text = ""
			lstWeatherItemNames.ClearSelected()
		End If
	End Sub

	''' <summary>
	''' listbox's event handler for mousedown
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub lstWeatherItemNames_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstWeatherItemNames.MouseDown
		'if right click then select a record
		If e.Button = MouseButtons.Right Then
			'given the point, return the item index
			Dim pt As Point = New Point(e.X, e.Y)
			Dim index As Integer = lstWeatherItemNames.IndexFromPoint(pt)
			If index > 0 Then
				'select the item pointed by the index
				lstWeatherItemNames.ClearSelected()
				lstWeatherItemNames.SelectedIndex = index
				lstWeatherItemNames.Refresh()
			End If
		End If
	End Sub

	''' <summary>
	''' listbox's double-click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub lstWeatherItemNames_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWeatherItemNames.DoubleClick
		'set the dialog results to OK
		Me.DialogResult = System.Windows.Forms.DialogResult.OK

		'select the items which the user double-clicked
		SelectWeatherItems()
	End Sub

	''' <summary>
    ''' refresh button click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnRefreshList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click
		'clear all the items on the list
		m_weatherItemsTable.Rows.Clear()

		'get an up-to-date list of citynames
		m_cityNames = m_weatherLayer.GetCityNames()

		'add the citynames to the listbox
		PopulateList()
	End Sub


	''' <summary>
	''' selection textbox text change event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub txtSelect_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSelect.TextChanged
		'clear the items of the listbox
		lstWeatherItemNames.Items.Clear()

		'spawn a thread to populate the list with items that match the selection criteria
		Dim t As Thread = New Thread(AddressOf PopulateSubListProc)
		t.Start()
	End Sub

	''' <summary>
	''' Select button click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
		'set the dialog results to OK
		Me.DialogResult = System.Windows.Forms.DialogResult.OK

		'select all the weather items with are selected in the listbox
		SelectWeatherItems()
	End Sub


	''' <summary>
	''' dismiss button event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnDismiss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDismiss.Click
		'set the dialog results to OK
		Me.DialogResult = DialogResult.No

		'hide the dialog
		Me.Hide()
	End Sub

	''' <summary>
	''' Populate the listbox with the citynames
	''' </summary>
	Private Sub PopulateList()
		'spawn the population thread (it populate both the listbox and the DataTable)
		Dim t As Thread = New Thread(AddressOf PopulateWeatherItemsTableProc)
		t.Start()

		Return
	End Sub

	''' <summary>
	''' Populate the listbox with the layer's list of cityNames
	''' </summary>
	Private Sub PopulateWeatherItemsTableProc()
		'hide the refresh button and show the progressbar
		ShowProgressBar()

		'iterate through the citynames
		For Each s As String In m_cityNames
			'create new record
			Dim r As DataRow = m_weatherItemsTable.NewRow()

			'add the cityname to the record
			r(0) = s

			'add the record to the table
			SyncLock m_weatherItemsTable
				m_weatherItemsTable.Rows.Add(r)
			End SyncLock

			'add the cityName to the listbox
			AddListItemString(s)

			'set the progress of the progressbar
			IncrementProgressBar()
		Next s

		'hide the progressbar and show the refresh button
		HideProgressBar()
	End Sub

	''' <summary>
	''' Make Thread-Safe Calls to Windows Forms Controls
	''' </summary>
	''' <param name="item"></param>
	Private Sub AddListItemString(ByVal item As String)
	  ' InvokeRequired required compares the thread ID of the
	  ' calling thread to the thread ID of the creating thread.
	  ' If these threads are different, it returns true.
	  If Me.lstWeatherItemNames.InvokeRequired Then
		'call itself on the main thread
		Dim d As AddListItmCallback = New AddListItmCallback(AddressOf AddListItemString)
		Me.Invoke(d, New Object() { item })
	  Else
		'guaranteed to run on the main UI thread
		Me.lstWeatherItemNames.Items.Add(item)
	  End If
	End Sub

	''' <summary>
	''' show the progressbar and hide the refresh button
	''' </summary>
	Private Sub ShowProgressBar()
	  'test whether Invoke is required (was this call made on a different thread than the main thread?)
	  If Me.lstWeatherItemNames.InvokeRequired Then
		'call itself on the main thread
		Dim d As ShowProgressBarCallBack = New ShowProgressBarCallBack(AddressOf ShowProgressBar)
		Me.Invoke(d)
	  Else
		'clear all the rows from the table
		m_weatherItemsTable.Rows.Clear()

		'clear all the items of the listbox
		lstWeatherItemNames.Items.Clear()

		'set the progressbar properties
		Dim count As Integer = m_cityNames.Length
		progressBar1.Maximum = count
		progressBar1.Value = 0
		btnRefreshList.Visible = False
		progressBar1.Visible = True
		Me.UpdateStyles()
	  End If
	End Sub

	''' <summary>
	''' hide the progressbar and show the refresh button
	''' </summary>
	Private Sub HideProgressBar()
	  'test whether Invoke is required (was this call made on a different thread than the main thread?)
	  If Me.progressBar1.InvokeRequired Then
			'call itself on the main thread
			Dim d As ShowProgressBarCallBack = New ShowProgressBarCallBack(AddressOf HideProgressBar)
			Me.Invoke(d)
	  Else
		'set the visibility
		btnRefreshList.Visible = True
		progressBar1.Visible = False
	  End If
	End Sub

	''' <summary>
	''' increments the progressbar of the refresh list button
	''' </summary>
	Private Sub IncrementProgressBar()
	  ' InvokeRequired required compares the thread ID of the
	  ' calling thread to the thread ID of the creating thread.
	  ' If these threads are different, it returns true.
	  If Me.progressBar1.InvokeRequired Then
			'call itself on the main thread
			Dim d As IncrementProgressBarCallback = New IncrementProgressBarCallback(AddressOf IncrementProgressBar)
			Me.Invoke(d)
	  Else
		Me.progressBar1.Increment(1)
	  End If

	End Sub

	''' <summary>
	''' select a weather items given selected items from the listbox
	''' </summary>
	Private Sub SelectWeatherItems()
		'get the selected list from the listbox
		Dim newSelection As Boolean = Me.chkNewSelection.Checked

		'in case of a new selection, unselect all items first
		If newSelection Then
			m_weatherLayer.UnselectAll()
		End If

		Dim propSet As IPropertySet = Nothing
		Dim o As Object
		Dim zipCode As Long
		'iterate through the selected items of the listbox
		For Each index As Integer In lstWeatherItemNames.SelectedIndices
			'get the weatheritem properties according to the zipCode of the item in the listbox
			propSet = m_weatherLayer.GetWeatherItem(Convert.ToString(lstWeatherItemNames.Items(index)))
			If Nothing Is propSet Then
				Continue For
			End If

			o = propSet.GetProperty("ZIPCODE")
			If Nothing Is o Then
				Continue For
			End If

			zipCode = Convert.ToInt64(o)

			'select the item in the weather layer
			m_weatherLayer.Select(zipCode, False)
		Next index

		'refresh the display
		m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, m_weatherLayer, m_activeView.Extent)
		m_activeView.ScreenDisplay.UpdateWindow()
	End Sub


	''' <summary>
	''' Populate the listbox according to a selection criteria
	''' </summary>
	Private Sub PopulateSubListProc()
		'get the selection criteria
		Dim exp As String = txtSelect.Text

        'in case that the user did not specify a criteria, populate the entire citiname list
		If exp = "" Then
			PopulateWeatherItemsTableProc()
			Return
		End If

		'set query
		exp = "CITYNAME LIKE '" & exp & "%'"

		'do the criteria selection against the table
		Dim rows As DataRow() = m_weatherItemsTable.Select(exp)

		'iterate through the selectionset
		For Each r As DataRow In rows
			'add the cityName to the listbox
			AddListItemString(Convert.ToString(r(0)))
		Next r
	End Sub

	''' <summary>
	''' ZoomTo menu click event handler
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub menuZoomTo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuZoomTo.Click
	  If Nothing Is m_weatherLayer Then
			Return
	  End If

	  'get the selected item
	  Dim cityName As String = Convert.ToString(lstWeatherItemNames.SelectedItem)

	  'ask the layer to zoom to that cityname
	  m_weatherLayer.ZoomTo(cityName)
	End Sub
	End Class
