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
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS


Public Class Form1
    Inherits System.Windows.Forms.Form

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
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        'Release COM objects
        ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown()

        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents txbMxPath As System.Windows.Forms.TextBox
    Public WithEvents cmdLoadMxFile As System.Windows.Forms.Button
    Public WithEvents cmdZoomPage As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxPageLayoutControl2 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.txbMxPath = New System.Windows.Forms.TextBox
        Me.cmdLoadMxFile = New System.Windows.Forms.Button
        Me.cmdZoomPage = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxPageLayoutControl2 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txbMxPath
        '
        Me.txbMxPath.AcceptsReturn = True
        Me.txbMxPath.AutoSize = False
        Me.txbMxPath.BackColor = System.Drawing.SystemColors.Window
        Me.txbMxPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbMxPath.Enabled = False
        Me.txbMxPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbMxPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbMxPath.Location = New System.Drawing.Point(8, 8)
        Me.txbMxPath.MaxLength = 0
        Me.txbMxPath.Name = "txbMxPath"
        Me.txbMxPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbMxPath.Size = New System.Drawing.Size(249, 19)
        Me.txbMxPath.TabIndex = 4
        Me.txbMxPath.Text = ""
        '
        'cmdLoadMxFile
        '
        Me.cmdLoadMxFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoadMxFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoadMxFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadMxFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoadMxFile.Location = New System.Drawing.Point(264, 5)
        Me.cmdLoadMxFile.Name = "cmdLoadMxFile"
        Me.cmdLoadMxFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoadMxFile.Size = New System.Drawing.Size(113, 25)
        Me.cmdLoadMxFile.TabIndex = 3
        Me.cmdLoadMxFile.Text = "Load Mx File"
        '
        'cmdZoomPage
        '
        Me.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdZoomPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdZoomPage.Location = New System.Drawing.Point(264, 328)
        Me.cmdZoomPage.Name = "cmdZoomPage"
        Me.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdZoomPage.Size = New System.Drawing.Size(113, 25)
        Me.cmdZoomPage.TabIndex = 2
        Me.cmdZoomPage.Text = "Zoom To Page"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(264, 264)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(121, 57)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Use the left mouse button to drag a rectangle and  the right mouse button to pan." & _
        ""
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(8, 32)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(248, 320)
        Me.AxPageLayoutControl1.TabIndex = 6
        '
        'AxPageLayoutControl2
        '
        Me.AxPageLayoutControl2.Location = New System.Drawing.Point(264, 40)
        Me.AxPageLayoutControl2.Name = "AxPageLayoutControl2"
        Me.AxPageLayoutControl2.OcxState = CType(resources.GetObject("AxPageLayoutControl2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl2.Size = New System.Drawing.Size(120, 184)
        Me.AxPageLayoutControl2.TabIndex = 7
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(32, 48)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(200, 50)
        Me.AxLicenseControl1.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(387, 362)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxPageLayoutControl2)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.txbMxPath)
        Me.Controls.Add(Me.cmdLoadMxFile)
        Me.Controls.Add(Me.cmdZoomPage)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Overview"
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private m_pPageLayoutControl As IPageLayoutControl

    Private Sub cmdLoadMxFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoadMxFile.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Browse Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'Validate and load the Mx document
        If AxPageLayoutControl1.CheckMxFile(sFilePath) Then
            txbMxPath.Text = sFilePath
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
            AxPageLayoutControl1.LoadMxFile(sFilePath)
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
        Else
            MsgBox(sFilePath & " is not a valid ArcMap document")
        End If

    End Sub

    Private Sub cmdZoomPage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdZoomPage.Click

        'Zoom to the whole page
        AxPageLayoutControl1.ZoomToWholePage()

        'Get the IElement interface by finding an element by its name
        Dim pElement As IElement
        pElement = m_pPageLayoutControl.FindElementByName("ZoomExtent")
        If Not pElement Is Nothing Then
            'Delete the element
            m_pPageLayoutControl.GraphicsContainer.DeleteElement(pElement)
            'Refresh the graphics
            m_pPageLayoutControl.Refresh(esriViewDrawPhase.esriViewGraphics)
        End If

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        m_pPageLayoutControl = AxPageLayoutControl2.Object

        'Set PageLayoutControl properties
        AxPageLayoutControl1.Enabled = True
        m_pPageLayoutControl.Enabled = False
        AxPageLayoutControl1.Appearance = esriControlsAppearance.esri3D
        m_pPageLayoutControl.Appearance = esriControlsAppearance.esriFlat
        AxPageLayoutControl1.BorderStyle = esriControlsBorderStyle.esriBorder
        m_pPageLayoutControl.BorderStyle = esriControlsBorderStyle.esriNoBorder

    End Sub

    Private Sub AxPageLayoutControl1_OnExtentUpdated(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnExtentUpdatedEvent) Handles AxPageLayoutControl1.OnExtentUpdated
        'QI for IEnvelope
        Dim pEnvelope As IEnvelope
        pEnvelope = e.newEnvelope

        'Get the IElement interface by finding an element by its name
        Dim pElement As IElement
        pElement = m_pPageLayoutControl.FindElementByName("ZoomExtent")
        If Not pElement Is Nothing Then
            'Delete the graphic
            m_pPageLayoutControl.GraphicsContainer.DeleteElement(pElement)
        End If
        pElement = New RectangleElementClass

        'Get the IRGBColor interface
        Dim pColor As IRgbColor
        pColor = New RgbColorClass
        'Set the color properties
        pColor.RGB = 255
        pColor.Transparency = 255

        'Get the ILine symbol interface
        Dim pOutline As ILineSymbol
        pOutline = New SimpleLineSymbolClass
        'Set the line symbol properties
        pOutline.Width = 10
        pOutline.Color = pColor

        'Set the color properties
        pColor = New RgbColorClass
        pColor.RGB = 255
        pColor.Transparency = 0

        'Get the IFillSymbol properties
        Dim pFillSymbol As IFillSymbol
        pFillSymbol = New SimpleFillSymbolClass
        'Set the fill symbol properties
        pFillSymbol.Color = pColor
        pFillSymbol.Outline = pOutline

        'QI for IFillShapeElement interface through the IElement interface
        Dim pFillShapeElement As IFillShapeElement
        pFillShapeElement = pElement
        'Set the symbol property
        pFillShapeElement.Symbol = pFillSymbol

        'Add the element
        m_pPageLayoutControl.AddElement(pElement, e.newEnvelope, , "ZoomExtent")
        'Refresh the graphics
        m_pPageLayoutControl.Refresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown
        'Zoom in
        If e.button = 1 Then
            AxPageLayoutControl1.Extent = AxPageLayoutControl1.TrackRectangle
            'Pan
        ElseIf e.button = 2 Then
            AxPageLayoutControl1.Pan()
        End If
    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced
        'Get the file path
        Dim sFilePath As String
        sFilePath = txbMxPath.Text
        'Validate and load the Mx document
        If m_pPageLayoutControl.CheckMxFile(sFilePath) Then
            m_pPageLayoutControl.LoadMxFile((sFilePath))
        End If
    End Sub
End Class