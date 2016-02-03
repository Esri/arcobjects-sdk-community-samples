Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem

Public Class Form2
    Inherits System.Windows.Forms.Form

    Private m_classBreaksRenderer As IClassBreaksRenderer
    Private m_styleGalleryItem As IStyleGalleryItem
    Private m_featureLayer As IFeatureLayer

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form2))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(320, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 120)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Feature Layer"
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(16, 56)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(216, 21)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.Text = "ComboBox1"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numeric Fields:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.AxSymbologyControl1)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(296, 320)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Symbology"
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.ContainingControl = Me
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(8, 16)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(280, 296)
        Me.AxSymbologyControl1.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(320, 144)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(248, 136)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Break"
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = False
        Me.TextBox3.Location = New System.Drawing.Point(128, 104)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.TabIndex = 8
        Me.TextBox3.Text = ""
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(128, 64)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.TabIndex = 7
        Me.TextBox2.Text = ""
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(128, 24)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.TabIndex = 6
        Me.TextBox1.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Max Value:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Min Value:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Number of Classes:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(416, 296)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "OK"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(496, 296)
        Me.Button3.Name = "Button3"
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Cancel"
        '
        'Form2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(584, 334)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form2"
        Me.Text = "Class Breaks Renderer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Get the ArcGIS install location
        Dim sInstall As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path

        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle")

        'Set the style class
        AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps

        'Select the color ramp item
        AxSymbologyControl1.GetStyleClass(AxSymbologyControl1.StyleClass).SelectItem(0)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        'Create a new ClassBreaksRenderer and set properties
        m_classBreaksRenderer = New ClassBreaksRenderer
        m_classBreaksRenderer.Field = ComboBox1.SelectedItem
        m_classBreaksRenderer.BreakCount = Convert.ToInt32(TextBox1.Text)
        m_classBreaksRenderer.MinimumBreak = Convert.ToDouble(TextBox2.Text)

        'Calculate the class interval by a simple mean value
        Dim interval As Double
        interval = (Convert.ToDouble(TextBox3.Text) - m_classBreaksRenderer.MinimumBreak) / m_classBreaksRenderer.BreakCount

        'Get the color ramp
        Dim colorRamp As IColorRamp
        colorRamp = m_styleGalleryItem.Item
        'Set the size of the color ramp and recreate it
        colorRamp.Size = Convert.ToInt32(TextBox1.Text)
        colorRamp.CreateRamp(False)

        Dim i As Integer, currentBreak As Double
        Dim simpleFillSymbol As ISimpleFillSymbol

        'Get the enumeration of colors from the color ramp
        Dim enumColors As IEnumColors = colorRamp.Colors
        enumColors.Reset()
        currentBreak = m_classBreaksRenderer.MinimumBreak

        'Loop through each class break
        For i = 0 To m_classBreaksRenderer.BreakCount - 1
            'Set class break
            m_classBreaksRenderer.Break(i) = currentBreak
            'Create simple fill symbol and set color
            simpleFillSymbol = New SimpleFillSymbolClass
            simpleFillSymbol.Color = enumColors.Next()
            'Add symbol to renderer
            m_classBreaksRenderer.Symbol(i) = simpleFillSymbol
            currentBreak += interval
        Next i

        'Hide the form
        Me.Hide()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Public Function GetClassBreaksRenderer(ByVal featureLayer As IFeatureLayer) As IClassBreaksRenderer

        m_featureLayer = featureLayer

        ComboBox1.Items.Clear()

        'Add numeric fields names to the combobox
        Dim fields As IFields, i As Integer
        fields = m_featureLayer.FeatureClass.Fields
        For i = 0 To fields.FieldCount - 1
            If fields.Field(i).Type = esriFieldType.esriFieldTypeDouble Or _
                fields.Field(i).Type = esriFieldType.esriFieldTypeInteger Or _
                fields.Field(i).Type = esriFieldType.esriFieldTypeSingle Or _
                fields.Field(i).Type = esriFieldType.esriFieldTypeSmallInteger Then
                ComboBox1.Items.Add(fields.Field(i).Name)
            End If
        Next
        ComboBox1.SelectedIndex = 0

        'Show form modally and wait for user input
        Me.ShowDialog()

        'return the ClassBreaksRenderer
        Return m_classBreaksRenderer

    End Function

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave

        Dim colorRamp As IColorRamp = m_styleGalleryItem.Item

        'Ensure is numeric
        If IsNumeric(TextBox1.Text) = False Then
            System.Windows.Forms.MessageBox.Show("Must be a numeric!")
            TextBox1.Text = "10"
            Exit Sub
        ElseIf Convert.ToInt32(TextBox1.Text) <= 0 Then
            'Ensure is not zero
            System.Windows.Forms.MessageBox.Show("Must be greater than 0!")
            TextBox1.Text = "10"
            Exit Sub
        ElseIf Convert.ToInt32(TextBox1.Text) > colorRamp.Size Then
            'Ensure does not exceed number of colors in color ramp
            System.Windows.Forms.MessageBox.Show("Must be less than " & colorRamp.Size & "!")
            TextBox1.Text = "10"
            Exit Sub
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        'Find the selected field in the feature layer
        Dim featureClass As IFeatureClass = m_featureLayer.FeatureClass
        Dim field As IField = featureClass.Fields.Field(featureClass.FindField(ComboBox1.Text))

        'Get a feature cursor
        Dim cursor As ICursor
        cursor = m_featureLayer.Search(Nothing, False)

        'Create a DataStatistics object and initialize properties
        Dim dataStatistics As IDataStatistics = New DataStatisticsClass
        dataStatistics.Field = field.Name
        dataStatistics.Cursor = cursor

        'Get the result statistics
        Dim statisticsResults As IStatisticsResults
        statisticsResults = dataStatistics.Statistics

        'Set the values min and max values
        TextBox2.Text = statisticsResults.Minimum
        TextBox3.Text = statisticsResults.Maximum
        TextBox1.Text = 10

    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected
        'Get the selected item
        m_styleGalleryItem = e.styleGalleryItem
    End Sub
End Class
