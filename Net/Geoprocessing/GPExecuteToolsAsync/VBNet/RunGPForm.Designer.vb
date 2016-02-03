Partial Class RunGPForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overloads Overrides Sub Dispose(disposing As Boolean)
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RunGPForm))
    Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.panel1 = New System.Windows.Forms.Panel
    Me.textBox7 = New System.Windows.Forms.TextBox
    Me.textBox6 = New System.Windows.Forms.TextBox
    Me.textBox5 = New System.Windows.Forms.TextBox
    Me.txtBufferDistance = New System.Windows.Forms.TextBox
    Me.textBox4 = New System.Windows.Forms.TextBox
    Me.textBox3 = New System.Windows.Forms.TextBox
    Me.textBox2 = New System.Windows.Forms.TextBox
    Me.textBox1 = New System.Windows.Forms.TextBox
    Me.axLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
    Me.btnRunGP = New System.Windows.Forms.Button
    Me.listView1 = New System.Windows.Forms.ListView
    Me.axMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
    Me.axToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
    Me.panel2 = New System.Windows.Forms.Panel
    Me.axTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
    Me.tableLayoutPanel1.SuspendLayout()
    Me.panel1.SuspendLayout()
    CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panel2.SuspendLayout()
    CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'imageList1
    '
    Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.imageList1.Images.SetKeyName(0, "error")
    Me.imageList1.Images.SetKeyName(1, "information")
    Me.imageList1.Images.SetKeyName(2, "warning")
    Me.imageList1.Images.SetKeyName(3, "success")
    '
    'tableLayoutPanel1
    '
    Me.tableLayoutPanel1.BackColor = System.Drawing.Color.White
    Me.tableLayoutPanel1.ColumnCount = 2
    Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.70563!))
    Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.29437!))
    Me.tableLayoutPanel1.Controls.Add(Me.panel1, 0, 0)
    Me.tableLayoutPanel1.Controls.Add(Me.listView1, 1, 2)
    Me.tableLayoutPanel1.Controls.Add(Me.axMapControl1, 1, 1)
    Me.tableLayoutPanel1.Controls.Add(Me.axToolbarControl1, 1, 0)
    Me.tableLayoutPanel1.Controls.Add(Me.panel2, 0, 2)
    Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
    Me.tableLayoutPanel1.RowCount = 3
    Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.042253!))
    Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.95775!))
    Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 211.0!))
    Me.tableLayoutPanel1.Size = New System.Drawing.Size(693, 558)
    Me.tableLayoutPanel1.TabIndex = 7
    '
    'panel1
    '
    Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.panel1.Controls.Add(Me.textBox7)
    Me.panel1.Controls.Add(Me.textBox6)
    Me.panel1.Controls.Add(Me.textBox5)
    Me.panel1.Controls.Add(Me.txtBufferDistance)
    Me.panel1.Controls.Add(Me.textBox4)
    Me.panel1.Controls.Add(Me.textBox3)
    Me.panel1.Controls.Add(Me.textBox2)
    Me.panel1.Controls.Add(Me.textBox1)
    Me.panel1.Controls.Add(Me.axLicenseControl1)
    Me.panel1.Controls.Add(Me.btnRunGP)
    Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.panel1.Location = New System.Drawing.Point(3, 3)
    Me.panel1.Name = "panel1"
    Me.tableLayoutPanel1.SetRowSpan(Me.panel1, 2)
    Me.panel1.Size = New System.Drawing.Size(186, 340)
    Me.panel1.TabIndex = 0
    '
    'textBox7
    '
    Me.textBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox7.Location = New System.Drawing.Point(3, 276)
    Me.textBox7.Multiline = True
    Me.textBox7.Name = "textBox7"
    Me.textBox7.Size = New System.Drawing.Size(180, 61)
    Me.textBox7.TabIndex = 15
    Me.textBox7.Text = "4. Test that the application stays responsive while the tools are executing, e.g." & _
        " try to Pan and Zoom the map."
    '
    'textBox6
    '
    Me.textBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox6.Location = New System.Drawing.Point(3, 201)
    Me.textBox6.Multiline = True
    Me.textBox6.Name = "textBox6"
    Me.textBox6.Size = New System.Drawing.Size(180, 33)
    Me.textBox6.TabIndex = 14
    Me.textBox6.Text = "3. Click Run to perform the analysis:"
    '
    'textBox5
    '
    Me.textBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox5.Location = New System.Drawing.Point(69, 180)
    Me.textBox5.Multiline = True
    Me.textBox5.Name = "textBox5"
    Me.textBox5.Size = New System.Drawing.Size(114, 13)
    Me.textBox5.TabIndex = 13
    Me.textBox5.Text = "Miles"
    '
    'txtBufferDistance
    '
    Me.txtBufferDistance.BackColor = System.Drawing.Color.White
    Me.txtBufferDistance.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtBufferDistance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtBufferDistance.Location = New System.Drawing.Point(17, 180)
    Me.txtBufferDistance.Name = "txtBufferDistance"
    Me.txtBufferDistance.Size = New System.Drawing.Size(46, 13)
    Me.txtBufferDistance.TabIndex = 12
    Me.txtBufferDistance.Text = "30"
    '
    'textBox4
    '
    Me.textBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox4.Location = New System.Drawing.Point(3, 158)
    Me.textBox4.Multiline = True
    Me.textBox4.Name = "textBox4"
    Me.textBox4.Size = New System.Drawing.Size(180, 25)
    Me.textBox4.TabIndex = 11
    Me.textBox4.Text = "2. Enter a buffer distance:"
    '
    'textBox3
    '
    Me.textBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox3.Location = New System.Drawing.Point(3, 112)
    Me.textBox3.Multiline = True
    Me.textBox3.Name = "textBox3"
    Me.textBox3.Size = New System.Drawing.Size(180, 51)
    Me.textBox3.TabIndex = 10
    Me.textBox3.Text = "1. Use the currently selected city, or use the Select Features tool to select som" & _
        "e other cities"
    '
    'textBox2
    '
    Me.textBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox2.Location = New System.Drawing.Point(3, 95)
    Me.textBox2.Multiline = True
    Me.textBox2.Name = "textBox2"
    Me.textBox2.Size = New System.Drawing.Size(180, 25)
    Me.textBox2.TabIndex = 9
    Me.textBox2.Text = "Steps:"
    '
    'textBox1
    '
    Me.textBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.textBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBox1.Location = New System.Drawing.Point(3, 3)
    Me.textBox1.Multiline = True
    Me.textBox1.Name = "textBox1"
    Me.textBox1.Size = New System.Drawing.Size(180, 92)
    Me.textBox1.TabIndex = 8
        Me.textBox1.Text = "This sample demonstrates a two stage geoprocessing analysis. The selected cities" & _
        " are buffered and the output layer is used to clip the zip codes layer. The resu" & _
        "lt layer is then added to the map."
    '
    'axLicenseControl1
    '
    Me.axLicenseControl1.Enabled = True
    Me.axLicenseControl1.Location = New System.Drawing.Point(130, 238)
    Me.axLicenseControl1.Name = "axLicenseControl1"
    Me.axLicenseControl1.OcxState = CType(resources.GetObject("axLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.axLicenseControl1.Size = New System.Drawing.Size(32, 32)
    Me.axLicenseControl1.TabIndex = 6
    '
    'btnRunGP
    '
    Me.btnRunGP.Enabled = False
    Me.btnRunGP.Location = New System.Drawing.Point(17, 236)
    Me.btnRunGP.Name = "btnRunGP"
    Me.btnRunGP.Size = New System.Drawing.Size(75, 23)
    Me.btnRunGP.TabIndex = 5
    Me.btnRunGP.Text = "Run"
    Me.btnRunGP.UseVisualStyleBackColor = True
    '
    'listView1
    '
    Me.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.listView1.Location = New System.Drawing.Point(195, 349)
    Me.listView1.Name = "listView1"
    Me.listView1.Size = New System.Drawing.Size(495, 206)
    Me.listView1.SmallImageList = Me.imageList1
    Me.listView1.TabIndex = 1
    Me.listView1.UseCompatibleStateImageBehavior = False
    Me.listView1.View = System.Windows.Forms.View.Details
    '
    'axMapControl1
    '
    Me.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.axMapControl1.Location = New System.Drawing.Point(195, 27)
    Me.axMapControl1.Name = "axMapControl1"
    Me.axMapControl1.OcxState = CType(resources.GetObject("axMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.axMapControl1.Size = New System.Drawing.Size(495, 316)
    Me.axMapControl1.TabIndex = 2
    '
    'axToolbarControl1
    '
    Me.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.axToolbarControl1.Location = New System.Drawing.Point(195, 3)
    Me.axToolbarControl1.Name = "axToolbarControl1"
    Me.axToolbarControl1.OcxState = CType(resources.GetObject("axToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.axToolbarControl1.Size = New System.Drawing.Size(495, 28)
    Me.axToolbarControl1.TabIndex = 3
    '
    'panel2
    '
    Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.panel2.Controls.Add(Me.axTOCControl1)
    Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.panel2.Location = New System.Drawing.Point(3, 349)
    Me.panel2.Name = "panel2"
    Me.panel2.Size = New System.Drawing.Size(186, 206)
    Me.panel2.TabIndex = 4
    '
    'axTOCControl1
    '
    Me.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.axTOCControl1.Location = New System.Drawing.Point(0, 0)
    Me.axTOCControl1.Name = "axTOCControl1"
    Me.axTOCControl1.OcxState = CType(resources.GetObject("axTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.axTOCControl1.Size = New System.Drawing.Size(186, 206)
    Me.axTOCControl1.TabIndex = 0
    '
    'RunGPForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(693, 558)
    Me.Controls.Add(Me.tableLayoutPanel1)
    Me.Name = "RunGPForm"
        Me.Text = "Run Geoprocessing Tools Asynchronously"
    Me.tableLayoutPanel1.ResumeLayout(False)
    Me.panel1.ResumeLayout(False)
    Me.panel1.PerformLayout()
    CType(Me.axLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.axMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.axToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panel2.ResumeLayout(False)
    CType(Me.axTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

	#End Region

	Private imageList1 As System.Windows.Forms.ImageList
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private panel1 As System.Windows.Forms.Panel
  Friend WithEvents btnRunGP As System.Windows.Forms.Button
	Private axLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
	Private listView1 As System.Windows.Forms.ListView
	Private axMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
	Private axToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
	Private textBox1 As System.Windows.Forms.TextBox
	Private panel2 As System.Windows.Forms.Panel
	Private axTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
	Private textBox2 As System.Windows.Forms.TextBox
	Private textBox5 As System.Windows.Forms.TextBox
  Friend WithEvents txtBufferDistance As System.Windows.Forms.TextBox
	Private textBox4 As System.Windows.Forms.TextBox
	Private textBox3 As System.Windows.Forms.TextBox
	Private textBox7 As System.Windows.Forms.TextBox
	Private textBox6 As System.Windows.Forms.TextBox
End Class

