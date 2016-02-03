Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form
    Private m_graphicProperties As IGraphicProperties
    <STAThread()> _
Shared Sub Main()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If

        Application.Run(New Form1())
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
    Friend WithEvents AxSymbologyControl1 As ESRI.ArcGIS.Controls.AxSymbologyControl
    Friend WithEvents AxToolbarControl1 As ESRI.ArcGIS.Controls.AxToolbarControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.AxSymbologyControl1 = New ESRI.ArcGIS.Controls.AxSymbologyControl
        Me.AxToolbarControl1 = New ESRI.ArcGIS.Controls.AxToolbarControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxSymbologyControl1
        '
        Me.AxSymbologyControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxSymbologyControl1.Location = New System.Drawing.Point(408, 40)
        Me.AxSymbologyControl1.Name = "AxSymbologyControl1"
        Me.AxSymbologyControl1.OcxState = CType(resources.GetObject("AxSymbologyControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSymbologyControl1.Size = New System.Drawing.Size(265, 265)
        Me.AxSymbologyControl1.TabIndex = 0
        '
        'AxToolbarControl1
        '
        Me.AxToolbarControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxToolbarControl1.Location = New System.Drawing.Point(8, 8)
        Me.AxToolbarControl1.Name = "AxToolbarControl1"
        Me.AxToolbarControl1.OcxState = CType(resources.GetObject("AxToolbarControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxToolbarControl1.Size = New System.Drawing.Size(392, 28)
        Me.AxToolbarControl1.TabIndex = 1
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(8, 40)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(392, 384)
        Me.AxPageLayoutControl1.TabIndex = 2
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.Location = New System.Drawing.Point(408, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(264, 21)
        Me.ComboBox1.TabIndex = 3
        Me.ComboBox1.Text = "ComboBox1"
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(304, 24)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 4
        '
        'Form1
        '
        'Removed setting for AutoScaleBaseSize
        'Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)

        'Set AutoScaleDimensions & AutoScaleMode to allow AdjustBounds to scale controls correctly at 120 dpi
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font

        Me.ClientSize = New System.Drawing.Size(680, 430)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.AxToolbarControl1)
        Me.Controls.Add(Me.AxSymbologyControl1)
        Me.Name = "Form1"
        Me.Text = "Updating the Commands Environment"
        CType(Me.AxSymbologyControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxToolbarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Resize the controls so that they scale correctly at both 96 and 120 dpi
        AdjustBounds(Me.AxToolbarControl1)
        AdjustBounds(Me.AxLicenseControl1)
        AdjustBounds(Me.AxPageLayoutControl1)
        AdjustBounds(Me.AxSymbologyControl1)

        'Set the buddy control
        AxToolbarControl1.SetBuddyControl(AxPageLayoutControl1)

        'Add items to the ToolbarControl
        AxToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomInTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomOutTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsPageZoomWholePageCommand", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsSelectTool", -1, -1, True, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsNewMarkerTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsNewLineTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsNewFreeHandTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsNewRectangleTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)
        AxToolbarControl1.AddItem("esriControls.ControlsNewPolygonTool", -1, -1, False, 0, esriCommandStyles.esriCommandStyleIconOnly)

        'Get the ArcGIS install location by opening the subkey for reading
        Dim installationFolder As String = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path
        'Load the ESRI.ServerStyle file into the SymbologyControl
        AxSymbologyControl1.LoadStyleFile(installationFolder + "\\Styles\\ESRI.ServerStyle")


        'Add style classes to the combo box
        ComboBox1.Items.Add("Default Marker Symbol")
        ComboBox1.Items.Add("Default Line Symbol")
        ComboBox1.Items.Add("Default Fill Symbol")
        ComboBox1.Items.Add("Default Text Symbol")
        ComboBox1.SelectedIndex = 0

        'Update each style class. This forces item to be loaded into each style class.
        'When the contents of a server style file are loaded into the SymbologyControl 
        'items are 'demand loaded'. This is done to increase performance and means 
        'items are only loaded into a SymbologyStyleClass when it is the current StyleClass.
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassMarkerSymbols).Update()
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassLineSymbols).Update()
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassFillSymbols).Update()
        AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassTextSymbols).Update()

        'Get the CommandsEnvironment singleton
        m_graphicProperties = New CommandsEnvironmentClass

        'Create a new ServerStyleGalleryItem and set its name
        Dim styleGalleryItem As IStyleGalleryItem
        styleGalleryItem = New ServerStyleGalleryItemClass
        styleGalleryItem.Name = "myStyle"

        Dim styleClass As ISymbologyStyleClass

        'Get the marker symbol style class
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
        'Set the commands environment marker symbol into the item
        styleGalleryItem.Item = m_graphicProperties.MarkerSymbol
        'Add the item to the style class
        styleClass.AddItem(styleGalleryItem, 0)

        'Get the line symbol style class
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassLineSymbols)
        'Set the commands environment line symbol into the item
        styleGalleryItem.Item = m_graphicProperties.LineSymbol
        'Add the item to the style class
        styleClass.AddItem(styleGalleryItem, 0)

        'Get the fill symbol style class
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassFillSymbols)
        'Set the commands environment fill symbol into the item
        styleGalleryItem.Item = m_graphicProperties.FillSymbol
        'Add the item to the style class
        styleClass.AddItem(styleGalleryItem, 0)

        'Get the text symbol style class
        styleClass = AxSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassTextSymbols)
        'Set the commands environment text symbol into the item
        styleGalleryItem.Item = m_graphicProperties.TextSymbol
        'Add the item to the style class
        styleClass.AddItem(styleGalleryItem, 0)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        'Set the SymbologyControl style class
        If ComboBox1.SelectedItem = "Default Marker Symbol" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols
        ElseIf ComboBox1.SelectedItem = "Default Line Symbol" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols
        ElseIf ComboBox1.SelectedItem = "Default Fill Symbol" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols
        ElseIf ComboBox1.SelectedItem = "Default Text Symbol" Then
            AxSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassTextSymbols
        End If

    End Sub

    Private Sub AxSymbologyControl1_OnItemSelected(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent) Handles AxSymbologyControl1.OnItemSelected

        Dim styleGalleryItem As IStyleGalleryItem = e.styleGalleryItem

        If TypeOf styleGalleryItem.Item Is IMarkerSymbol Then
            'Set the default marker symbol
            m_graphicProperties.MarkerSymbol = styleGalleryItem.Item
        ElseIf TypeOf styleGalleryItem.Item Is ILineSymbol Then
            'Set the default line symbol
            m_graphicProperties.LineSymbol = styleGalleryItem.Item
        ElseIf TypeOf styleGalleryItem.Item Is IFillSymbol Then
            'Set the default fill symbol
            m_graphicProperties.FillSymbol = styleGalleryItem.Item
        ElseIf TypeOf styleGalleryItem.Item Is ITextSymbol Then
            'Set the default text symbol
            m_graphicProperties.TextSymbol = styleGalleryItem.Item
        End If

    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown

        If e.button <> 2 Then Exit Sub

        'Create a new point
        Dim pPoint As IPoint = New PointClass
        pPoint.PutCoords(e.pageX, e.pageY)

        'Create a new text element 
        Dim textElement As ITextElement = New TextElementClass
        'Set the text to display today's date
        textElement.Text = DateTime.Now.ToShortDateString

        'Add element to graphics container using the CommandsEnvironment default text symbol
        AxPageLayoutControl1.AddElement(textElement, pPoint, m_graphicProperties.TextSymbol, "", 0)
        'Refresh the graphics
        AxPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

    Private Sub AdjustBounds(ByVal controlToAdjust As AxHost)
        If Me.CurrentAutoScaleDimensions.Width <> 6.0! Then
            'Adjust location: ActiveX control doesn't do this by itself 
            controlToAdjust.Left = controlToAdjust.Left * Me.CurrentAutoScaleDimensions.Width / 6.0!
            'Undo the automatic resize... 
            controlToAdjust.Width = controlToAdjust.Width / DPIX() * 96
            '...and apply the resize we want 
            controlToAdjust.Width = controlToAdjust.Width * Me.CurrentAutoScaleDimensions.Width / 6.0!
        End If

        If Me.CurrentAutoScaleDimensions.Height <> 13.0! Then
            'Adjust location: ActiveX control doesn't do this by itself 
            controlToAdjust.Top = controlToAdjust.Top * Me.CurrentAutoScaleDimensions.Height / 13.0!
            'Undo the automatic resize... 
            controlToAdjust.Height = controlToAdjust.Height / DPIY() * 96
            '...and apply the resize we want 
            controlToAdjust.Height = controlToAdjust.Height * Me.CurrentAutoScaleDimensions.Height / 13.0!
        End If
    End Sub

    <Runtime.InteropServices.DllImport("Gdi32.dll")> _
    Private Shared Function GetDeviceCaps(ByVal hDC As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <Runtime.InteropServices.DllImport("Gdi32.dll")> _
    Private Shared Function CreateDC(ByVal lpszDriver As String, ByVal lpszDeviceName As String, ByVal lpszOutput As String, ByVal devMode As IntPtr) As IntPtr

    End Function

    Const LOGPIXELSX As Integer = 88
    Const LOGPIXELSY As Integer = 90

    Function DPIX() As Integer
        Return DPI(LOGPIXELSX)
    End Function

    Function DPIY() As Integer
        Return DPI(LOGPIXELSY)
    End Function

    Function DPI(ByVal logPixelOrientation As Integer) As Integer
        Dim displayPointer As IntPtr = CreateDC("DISPLAY", Nothing, Nothing, IntPtr.Zero)
        Return GetDeviceCaps(displayPointer, logPixelOrientation)
    End Function




End Class
