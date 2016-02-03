<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoutingForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RoutingForm))
		Me.m_btnFindRoute = New System.Windows.Forms.Button
		Me.m_btnClose = New System.Windows.Forms.Button
		Me.Label1 = New System.Windows.Forms.Label
		Me.m_txtRoutingService = New System.Windows.Forms.TextBox
		Me.m_dlgRoutingSrvc = New System.Windows.Forms.OpenFileDialog
		Me.m_btnRoutingService = New System.Windows.Forms.Button
		Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.m_btnAddressLocator = New System.Windows.Forms.Button
		Me.m_txtAddressLocator = New System.Windows.Forms.TextBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.m_txtStartAddress = New System.Windows.Forms.TextBox
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.m_txtStartCity = New System.Windows.Forms.TextBox
		Me.m_txtStartState = New System.Windows.Forms.TextBox
		Me.m_txtStartCode = New System.Windows.Forms.TextBox
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.m_txtFinishAddress = New System.Windows.Forms.TextBox
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.m_txtFinishCity = New System.Windows.Forms.TextBox
		Me.m_txtFinishState = New System.Windows.Forms.TextBox
		Me.m_txtFinishCode = New System.Windows.Forms.TextBox
		Me.GroupBox3 = New System.Windows.Forms.GroupBox
		Me.m_cmbDistanceUnit = New System.Windows.Forms.ComboBox
		Me.Label14 = New System.Windows.Forms.Label
		Me.m_trackUseRoad = New System.Windows.Forms.TrackBar
		Me.Label12 = New System.Windows.Forms.Label
		Me.m_btnRestrictions = New System.Windows.Forms.Button
		Me.m_rbtnQuickest = New System.Windows.Forms.RadioButton
		Me.Label11 = New System.Windows.Forms.Label
		Me.m_rbtnShortest = New System.Windows.Forms.RadioButton
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label15 = New System.Windows.Forms.Label
		Me.m_txtBarriers = New System.Windows.Forms.TextBox
		Me.m_btnBarriersOpen = New System.Windows.Forms.Button
		Me.m_btnShowDirections = New System.Windows.Forms.Button
		Me.m_btnBarriersClear = New System.Windows.Forms.Button
		Me.GroupBox4 = New System.Windows.Forms.GroupBox
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		CType(Me.m_trackUseRoad, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'm_btnFindRoute
		'
		Me.m_btnFindRoute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnFindRoute.Enabled = False
		Me.m_btnFindRoute.Location = New System.Drawing.Point(96, 488)
		Me.m_btnFindRoute.Name = "m_btnFindRoute"
		Me.m_btnFindRoute.Size = New System.Drawing.Size(104, 23)
		Me.m_btnFindRoute.TabIndex = 14
		Me.m_btnFindRoute.Text = "Find Route"
		'
		'm_btnClose
		'
		Me.m_btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnClose.Location = New System.Drawing.Point(320, 488)
		Me.m_btnClose.Name = "m_btnClose"
		Me.m_btnClose.Size = New System.Drawing.Size(75, 23)
		Me.m_btnClose.TabIndex = 16
		Me.m_btnClose.Text = "Close"
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(8, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(100, 23)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Routing Service:"
		'
		'm_txtRoutingService
		'
		Me.m_txtRoutingService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtRoutingService.Location = New System.Drawing.Point(112, 8)
		Me.m_txtRoutingService.Name = "m_txtRoutingService"
		Me.m_txtRoutingService.ReadOnly = True
		Me.m_txtRoutingService.Size = New System.Drawing.Size(248, 20)
		Me.m_txtRoutingService.TabIndex = 1
		'
		'm_dlgRoutingSrvc
		'
		Me.m_dlgRoutingSrvc.Filter = "Routing Service Files (*.rs)|*.rs"
		Me.m_dlgRoutingSrvc.RestoreDirectory = True
		Me.m_dlgRoutingSrvc.Title = "Choose Routing Service"
		'
		'm_btnRoutingService
		'
		Me.m_btnRoutingService.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnRoutingService.ImageIndex = 0
		Me.m_btnRoutingService.ImageList = Me.ImageList1
		Me.m_btnRoutingService.Location = New System.Drawing.Point(368, 8)
		Me.m_btnRoutingService.Name = "m_btnRoutingService"
		Me.m_btnRoutingService.Size = New System.Drawing.Size(24, 23)
		Me.m_btnRoutingService.TabIndex = 2
		'
		'ImageList1
		'
		Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
		Me.ImageList1.Images.SetKeyName(0, "")
		'
		'm_btnAddressLocator
		'
		Me.m_btnAddressLocator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnAddressLocator.ImageIndex = 0
		Me.m_btnAddressLocator.ImageList = Me.ImageList1
		Me.m_btnAddressLocator.Location = New System.Drawing.Point(368, 37)
		Me.m_btnAddressLocator.Name = "m_btnAddressLocator"
		Me.m_btnAddressLocator.Size = New System.Drawing.Size(24, 23)
		Me.m_btnAddressLocator.TabIndex = 5
		'
		'm_txtAddressLocator
		'
		Me.m_txtAddressLocator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtAddressLocator.Location = New System.Drawing.Point(112, 40)
		Me.m_txtAddressLocator.Name = "m_txtAddressLocator"
		Me.m_txtAddressLocator.ReadOnly = True
		Me.m_txtAddressLocator.Size = New System.Drawing.Size(248, 20)
		Me.m_txtAddressLocator.TabIndex = 4
		'
		'Label2
		'
		Me.Label2.Location = New System.Drawing.Point(8, 40)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(100, 23)
		Me.Label2.TabIndex = 3
		Me.Label2.Text = "Address Locator:"
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.m_txtStartAddress)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.m_txtStartCity)
		Me.GroupBox1.Controls.Add(Me.m_txtStartState)
		Me.GroupBox1.Controls.Add(Me.m_txtStartCode)
		Me.GroupBox1.Location = New System.Drawing.Point(8, 64)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(386, 128)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Start:"
		'
		'm_txtStartAddress
		'
		Me.m_txtStartAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtStartAddress.Location = New System.Drawing.Point(136, 24)
		Me.m_txtStartAddress.Name = "m_txtStartAddress"
		Me.m_txtStartAddress.Size = New System.Drawing.Size(240, 20)
		Me.m_txtStartAddress.TabIndex = 1
		Me.m_txtStartAddress.Text = "2001 ARDEN WAY"
		'
		'Label6
		'
		Me.Label6.Location = New System.Drawing.Point(8, 96)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(100, 23)
		Me.Label6.TabIndex = 6
		Me.Label6.Text = "Postal Code:"
		'
		'Label5
		'
		Me.Label5.Location = New System.Drawing.Point(8, 72)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(100, 23)
		Me.Label5.TabIndex = 4
		Me.Label5.Text = "State or Province:"
		'
		'Label4
		'
		Me.Label4.Location = New System.Drawing.Point(8, 48)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(100, 23)
		Me.Label4.TabIndex = 2
		Me.Label4.Text = "City:"
		'
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(8, 24)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(128, 23)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Address or Intersection:"
		'
		'm_txtStartCity
		'
		Me.m_txtStartCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtStartCity.Location = New System.Drawing.Point(136, 48)
		Me.m_txtStartCity.Name = "m_txtStartCity"
		Me.m_txtStartCity.Size = New System.Drawing.Size(240, 20)
		Me.m_txtStartCity.TabIndex = 3
		Me.m_txtStartCity.Text = "Sacramento"
		'
		'm_txtStartState
		'
		Me.m_txtStartState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtStartState.Location = New System.Drawing.Point(136, 72)
		Me.m_txtStartState.Name = "m_txtStartState"
		Me.m_txtStartState.Size = New System.Drawing.Size(240, 20)
		Me.m_txtStartState.TabIndex = 5
		Me.m_txtStartState.Text = "CA"
		'
		'm_txtStartCode
		'
		Me.m_txtStartCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtStartCode.Location = New System.Drawing.Point(136, 96)
		Me.m_txtStartCode.Name = "m_txtStartCode"
		Me.m_txtStartCode.Size = New System.Drawing.Size(240, 20)
		Me.m_txtStartCode.TabIndex = 7
		'
		'GroupBox2
		'
		Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox2.Controls.Add(Me.m_txtFinishAddress)
		Me.GroupBox2.Controls.Add(Me.Label7)
		Me.GroupBox2.Controls.Add(Me.Label8)
		Me.GroupBox2.Controls.Add(Me.Label9)
		Me.GroupBox2.Controls.Add(Me.Label10)
		Me.GroupBox2.Controls.Add(Me.m_txtFinishCity)
		Me.GroupBox2.Controls.Add(Me.m_txtFinishState)
		Me.GroupBox2.Controls.Add(Me.m_txtFinishCode)
		Me.GroupBox2.Location = New System.Drawing.Point(8, 200)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(386, 128)
		Me.GroupBox2.TabIndex = 7
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Finish:"
		'
		'm_txtFinishAddress
		'
		Me.m_txtFinishAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtFinishAddress.Location = New System.Drawing.Point(136, 24)
		Me.m_txtFinishAddress.Name = "m_txtFinishAddress"
		Me.m_txtFinishAddress.Size = New System.Drawing.Size(240, 20)
		Me.m_txtFinishAddress.TabIndex = 1
		Me.m_txtFinishAddress.Text = "4760 MACK RD"
		'
		'Label7
		'
		Me.Label7.Location = New System.Drawing.Point(8, 96)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(100, 23)
		Me.Label7.TabIndex = 6
		Me.Label7.Text = "Postal Code:"
		'
		'Label8
		'
		Me.Label8.Location = New System.Drawing.Point(8, 72)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(100, 23)
		Me.Label8.TabIndex = 4
		Me.Label8.Text = "State or Province:"
		'
		'Label9
		'
		Me.Label9.Location = New System.Drawing.Point(8, 48)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(100, 23)
		Me.Label9.TabIndex = 2
		Me.Label9.Text = "City:"
		'
		'Label10
		'
		Me.Label10.Location = New System.Drawing.Point(8, 24)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(128, 23)
		Me.Label10.TabIndex = 0
		Me.Label10.Text = "Address or Intersection:"
		'
		'm_txtFinishCity
		'
		Me.m_txtFinishCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtFinishCity.Location = New System.Drawing.Point(136, 48)
		Me.m_txtFinishCity.Name = "m_txtFinishCity"
		Me.m_txtFinishCity.Size = New System.Drawing.Size(240, 20)
		Me.m_txtFinishCity.TabIndex = 3
		Me.m_txtFinishCity.Text = "Sacramento"
		'
		'm_txtFinishState
		'
		Me.m_txtFinishState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtFinishState.Location = New System.Drawing.Point(136, 72)
		Me.m_txtFinishState.Name = "m_txtFinishState"
		Me.m_txtFinishState.Size = New System.Drawing.Size(240, 20)
		Me.m_txtFinishState.TabIndex = 5
		Me.m_txtFinishState.Text = "CA"
		'
		'm_txtFinishCode
		'
		Me.m_txtFinishCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtFinishCode.Location = New System.Drawing.Point(136, 96)
		Me.m_txtFinishCode.Name = "m_txtFinishCode"
		Me.m_txtFinishCode.Size = New System.Drawing.Size(240, 20)
		Me.m_txtFinishCode.TabIndex = 7
		'
		'GroupBox3
		'
		Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox3.Controls.Add(Me.m_rbtnShortest)
		Me.GroupBox3.Controls.Add(Me.m_rbtnQuickest)
		Me.GroupBox3.Controls.Add(Me.m_cmbDistanceUnit)
		Me.GroupBox3.Controls.Add(Me.Label14)
		Me.GroupBox3.Controls.Add(Me.m_trackUseRoad)
		Me.GroupBox3.Controls.Add(Me.Label12)
		Me.GroupBox3.Controls.Add(Me.m_btnRestrictions)
		Me.GroupBox3.Controls.Add(Me.Label11)
		Me.GroupBox3.Controls.Add(Me.Label13)
		Me.GroupBox3.Location = New System.Drawing.Point(8, 336)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(386, 100)
		Me.GroupBox3.TabIndex = 8
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Options:"
		'
		'm_cmbDistanceUnit
		'
		Me.m_cmbDistanceUnit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_cmbDistanceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.m_cmbDistanceUnit.Items.AddRange(New Object() {"Feet", "Yards", "Miles", "Meters", "Kilometers"})
		Me.m_cmbDistanceUnit.Location = New System.Drawing.Point(264, 16)
		Me.m_cmbDistanceUnit.Name = "m_cmbDistanceUnit"
		Me.m_cmbDistanceUnit.Size = New System.Drawing.Size(114, 21)
		Me.m_cmbDistanceUnit.TabIndex = 3
		'
		'Label14
		'
		Me.Label14.Location = New System.Drawing.Point(176, 16)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(88, 23)
		Me.Label14.TabIndex = 2
		Me.Label14.Text = "Distance Units:"
		'
		'm_trackUseRoad
		'
		Me.m_trackUseRoad.AutoSize = False
		Me.m_trackUseRoad.Location = New System.Drawing.Point(96, 62)
		Me.m_trackUseRoad.Maximum = 100
		Me.m_trackUseRoad.Name = "m_trackUseRoad"
		Me.m_trackUseRoad.Size = New System.Drawing.Size(104, 32)
		Me.m_trackUseRoad.TabIndex = 5
		Me.m_trackUseRoad.TickFrequency = 10
		Me.m_trackUseRoad.Value = 50
		'
		'Label12
		'
		Me.Label12.Location = New System.Drawing.Point(8, 64)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(96, 23)
		Me.Label12.TabIndex = 4
		Me.Label12.Text = "Use Local Roads"
		'
		'm_btnRestrictions
		'
		Me.m_btnRestrictions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnRestrictions.Enabled = False
		Me.m_btnRestrictions.Location = New System.Drawing.Point(282, 64)
		Me.m_btnRestrictions.Name = "m_btnRestrictions"
		Me.m_btnRestrictions.Size = New System.Drawing.Size(96, 23)
		Me.m_btnRestrictions.TabIndex = 7
		Me.m_btnRestrictions.Text = "Restrictions"
		'
		'm_rbtnQuickest
		'
		Me.m_rbtnQuickest.Checked = True
		Me.m_rbtnQuickest.Location = New System.Drawing.Point(46, 13)
		Me.m_rbtnQuickest.Name = "m_rbtnQuickest"
		Me.m_rbtnQuickest.Size = New System.Drawing.Size(104, 24)
		Me.m_rbtnQuickest.TabIndex = 0
		Me.m_rbtnQuickest.TabStop = True
		Me.m_rbtnQuickest.Text = "Quickest Route"
		'
		'Label11
		'
		Me.Label11.Location = New System.Drawing.Point(8, 16)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(32, 23)
		Me.Label11.TabIndex = 0
		Me.Label11.Text = "Find:"
		'
		'm_rbtnShortest
		'
		Me.m_rbtnShortest.Location = New System.Drawing.Point(46, 37)
		Me.m_rbtnShortest.Name = "m_rbtnShortest"
		Me.m_rbtnShortest.Size = New System.Drawing.Size(104, 24)
		Me.m_rbtnShortest.TabIndex = 1
		Me.m_rbtnShortest.TabStop = True
		Me.m_rbtnShortest.Text = "Shortest Route"
		'
		'Label13
		'
		Me.Label13.Location = New System.Drawing.Point(200, 64)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(96, 23)
		Me.Label13.TabIndex = 6
		Me.Label13.Text = "Use Highways"
		'
		'Label15
		'
		Me.Label15.Location = New System.Drawing.Point(8, 448)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(100, 23)
		Me.Label15.TabIndex = 9
		Me.Label15.Text = "Barriers Table:"
		'
		'm_txtBarriers
		'
		Me.m_txtBarriers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_txtBarriers.Location = New System.Drawing.Point(112, 448)
		Me.m_txtBarriers.Name = "m_txtBarriers"
		Me.m_txtBarriers.ReadOnly = True
		Me.m_txtBarriers.Size = New System.Drawing.Size(168, 20)
		Me.m_txtBarriers.TabIndex = 10
		'
		'm_btnBarriersOpen
		'
		Me.m_btnBarriersOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnBarriersOpen.Enabled = False
		Me.m_btnBarriersOpen.ImageIndex = 0
		Me.m_btnBarriersOpen.ImageList = Me.ImageList1
		Me.m_btnBarriersOpen.Location = New System.Drawing.Point(288, 448)
		Me.m_btnBarriersOpen.Name = "m_btnBarriersOpen"
		Me.m_btnBarriersOpen.Size = New System.Drawing.Size(24, 23)
		Me.m_btnBarriersOpen.TabIndex = 11
		'
		'm_btnShowDirections
		'
		Me.m_btnShowDirections.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnShowDirections.Enabled = False
		Me.m_btnShowDirections.Location = New System.Drawing.Point(208, 488)
		Me.m_btnShowDirections.Name = "m_btnShowDirections"
		Me.m_btnShowDirections.Size = New System.Drawing.Size(104, 23)
		Me.m_btnShowDirections.TabIndex = 15
		Me.m_btnShowDirections.Text = "Show Directions"
		'
		'm_btnBarriersClear
		'
		Me.m_btnBarriersClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.m_btnBarriersClear.Location = New System.Drawing.Point(320, 448)
		Me.m_btnBarriersClear.Name = "m_btnBarriersClear"
		Me.m_btnBarriersClear.Size = New System.Drawing.Size(75, 23)
		Me.m_btnBarriersClear.TabIndex = 12
		Me.m_btnBarriersClear.Text = "Clear"
		'
		'GroupBox4
		'
		Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox4.Location = New System.Drawing.Point(8, 480)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(384, 2)
		Me.GroupBox4.TabIndex = 13
		Me.GroupBox4.TabStop = False
		'
		'RoutingForm
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
		Me.ClientSize = New System.Drawing.Size(402, 522)
		Me.Controls.Add(Me.GroupBox4)
		Me.Controls.Add(Me.m_btnBarriersClear)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.m_btnRoutingService)
		Me.Controls.Add(Me.m_txtRoutingService)
		Me.Controls.Add(Me.m_txtAddressLocator)
		Me.Controls.Add(Me.m_txtBarriers)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.m_btnClose)
		Me.Controls.Add(Me.m_btnFindRoute)
		Me.Controls.Add(Me.m_btnAddressLocator)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.Label15)
		Me.Controls.Add(Me.m_btnBarriersOpen)
		Me.Controls.Add(Me.m_btnShowDirections)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "RoutingForm"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Routing Sample"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.GroupBox3.ResumeLayout(False)
		CType(Me.m_trackUseRoad, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents m_ctrlPages As System.Windows.Forms.TabControl
	Friend WithEvents m_btnFindRoute As System.Windows.Forms.Button
	Friend WithEvents m_ctrlPageStops As System.Windows.Forms.TabPage
	Friend WithEvents m_ctrlPageBarriers As System.Windows.Forms.TabPage
	Friend WithEvents m_ctrlPageDD As System.Windows.Forms.TabPage
	Friend WithEvents m_ctrlPageOptions As System.Windows.Forms.TabPage
	Friend WithEvents m_imgList As System.Windows.Forms.ImageList
	Friend WithEvents m_btnStopAdd As System.Windows.Forms.Button
	Friend WithEvents m_btnStopsRemoveAll As System.Windows.Forms.Button
	Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents m_btnStopRemove As System.Windows.Forms.Button
	Friend WithEvents m_btnStopUp As System.Windows.Forms.Button
	Friend WithEvents m_btnStopDown As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
	Friend m_dlgRoutingSrvc As System.Windows.Forms.OpenFileDialog
	Friend WithEvents Label15 As System.Windows.Forms.Label
	Friend WithEvents Label14 As System.Windows.Forms.Label
	Friend WithEvents Label13 As System.Windows.Forms.Label
	Friend WithEvents Label12 As System.Windows.Forms.Label
	Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
	Friend WithEvents m_btnClose As System.Windows.Forms.Button
	Friend WithEvents m_txtRoutingService As System.Windows.Forms.TextBox
	Friend WithEvents m_btnRoutingService As System.Windows.Forms.Button
	Friend WithEvents m_btnAddressLocator As System.Windows.Forms.Button
	Friend WithEvents m_txtAddressLocator As System.Windows.Forms.TextBox
	Friend WithEvents m_txtStartAddress As System.Windows.Forms.TextBox
	Friend WithEvents m_txtStartCity As System.Windows.Forms.TextBox
	Friend WithEvents m_txtStartState As System.Windows.Forms.TextBox
	Friend WithEvents m_txtStartCode As System.Windows.Forms.TextBox
	Friend WithEvents m_txtFinishAddress As System.Windows.Forms.TextBox
	Friend WithEvents m_txtFinishCity As System.Windows.Forms.TextBox
	Friend WithEvents m_txtFinishState As System.Windows.Forms.TextBox
	Friend WithEvents m_txtFinishCode As System.Windows.Forms.TextBox
	Friend WithEvents m_cmbDistanceUnit As System.Windows.Forms.ComboBox
	Friend WithEvents m_trackUseRoad As System.Windows.Forms.TrackBar
	Friend WithEvents m_btnRestrictions As System.Windows.Forms.Button
	Friend WithEvents m_rbtnQuickest As System.Windows.Forms.RadioButton
	Friend WithEvents m_rbtnShortest As System.Windows.Forms.RadioButton
	Friend WithEvents m_txtBarriers As System.Windows.Forms.TextBox
	Friend WithEvents m_btnBarriersOpen As System.Windows.Forms.Button
	Friend WithEvents m_btnShowDirections As System.Windows.Forms.Button
	Friend WithEvents m_btnBarriersClear As System.Windows.Forms.Button

End Class

