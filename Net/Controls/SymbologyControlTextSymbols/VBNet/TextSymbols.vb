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
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display

Public Class Form1
  Inherits System.Windows.Forms.Form

    Private m_textSymbol As ITextSymbol

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)

        'Release COM objects 
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

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
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.Button1 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(184, 28)
        Me.AxToolbarControl1.TabIndex = 0
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(0, 56)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(672, 360)
        Me.AxPageLayoutControl1.TabIndex = 1
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(56, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(200, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Select Text Symbol"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(336, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(256, 20)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.Text = "TextElement with selected TextSymbol"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Location = New System.Drawing.Point(600, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(64, 21)
        Me.ComboBox1.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(336, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "2) Right click on the display to add a text element"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(197, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(296, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "1) Select a text symbol "
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(672, 414)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Form1"
        Me.Text = "Change Text Symbol"
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set buddy control
        AxToolbarControl1.SetBuddyControl(Me.AxPageLayoutControl1)

        'Add ToolbarControl items
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool")
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand")
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool")

        'Add values for the text size to the combo box
        ComboBox1.Items.Add("8pt")
        ComboBox1.Items.Add("10pt")
        ComboBox1.Items.Add("12pt")
        ComboBox1.Items.Add("14pt")
        ComboBox1.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Create a new SymbolForm
        Dim symbolForm As Form2 = New Form2

        'Get the IStyleGalleryItem that has been selected in the SymbologyControl
        Dim styleGalleryItem As IStyleGalleryItem
        styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassTextSymbols)
        If styleGalleryItem Is Nothing Then Exit Sub

        'Set the TextSymbol
        m_textSymbol = CType(styleGalleryItem.Item, ITextSymbol)

        'Release the SymbolForm
        symbolForm.Dispose()

    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown

        'Check if the right button of the mouse was clicked
        If e.button <> 2 Then Exit Sub
        'Ensure a label style has been selected
        If m_textSymbol Is Nothing Then Exit Sub

        'Create a point and set its coordinates
        Dim point As IPoint = New PointClass
        point.X = e.pageX
        point.Y = e.pageY

        'Create a text element
        Dim textElement As ITextElement = New TextElementClass
        textElement.Text = TextBox1.Text

        'Set the size of the text
        If ComboBox1.SelectedItem = ("8pt") Then
            m_textSymbol.Size = 8.0
        ElseIf ComboBox1.SelectedItem = ("10pt") Then
            m_textSymbol.Size = 10.0
        elseIf ComboBox1.SelectedItem = ("12pt") Then
            m_textSymbol.Size = 12.0
        ElseIf ComboBox1.SelectedItem = ("14pt") Then
            m_textSymbol.Size = 14.0
        End If

        'Set the TextElement symbol to that of the selected text symbol
        textElement.Symbol = m_textSymbol
        textElement.ScaleText = True

        'QI to IElment
        Dim element As IElement
        element = textElement
        'Set the TextElement's geometry
        element.Geometry = point

        'Add the element to the GraphicsContainer
        AxPageLayoutControl1.ActiveView.GraphicsContainer.AddElement(element, 0)
        'Refresh the PageLayout
        AxPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, Nothing)

    End Sub

End Class
