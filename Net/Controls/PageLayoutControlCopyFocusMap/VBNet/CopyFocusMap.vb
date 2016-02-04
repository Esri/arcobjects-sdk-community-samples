'Copyright 2016 Esri

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
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
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
    Public WithEvents cmdZoomPage As System.Windows.Forms.Button
    Public WithEvents cboMaps As System.Windows.Forms.ComboBox
    Public WithEvents txbPath As System.Windows.Forms.TextBox
    Public WithEvents cmdLoad As System.Windows.Forms.Button
    Public WithEvents cmdFullExtent As System.Windows.Forms.Button
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxMapControl1 As ESRI.ArcGIS.Controls.AxMapControl
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdZoomPage = New System.Windows.Forms.Button
        Me.cboMaps = New System.Windows.Forms.ComboBox
        Me.txbPath = New System.Windows.Forms.TextBox
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxMapControl1 = New ESRI.ArcGIS.Controls.AxMapControl
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdZoomPage
        '
        Me.cmdZoomPage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdZoomPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdZoomPage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdZoomPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdZoomPage.Location = New System.Drawing.Point(8, 360)
        Me.cmdZoomPage.Name = "cmdZoomPage"
        Me.cmdZoomPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdZoomPage.Size = New System.Drawing.Size(97, 25)
        Me.cmdZoomPage.TabIndex = 9
        Me.cmdZoomPage.Text = "Zoom to Page"
        Me.cmdZoomPage.UseVisualStyleBackColor = False
        '
        'cboMaps
        '
        Me.cboMaps.BackColor = System.Drawing.SystemColors.Window
        Me.cboMaps.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMaps.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMaps.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMaps.Location = New System.Drawing.Point(72, 48)
        Me.cboMaps.Name = "cboMaps"
        Me.cboMaps.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMaps.Size = New System.Drawing.Size(161, 22)
        Me.cboMaps.TabIndex = 7
        '
        'txbPath
        '
        Me.txbPath.AcceptsReturn = True
        Me.txbPath.BackColor = System.Drawing.SystemColors.Window
        Me.txbPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txbPath.Enabled = False
        Me.txbPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txbPath.Location = New System.Drawing.Point(136, 11)
        Me.txbPath.MaxLength = 0
        Me.txbPath.Name = "txbPath"
        Me.txbPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txbPath.Size = New System.Drawing.Size(369, 20)
        Me.txbPath.TabIndex = 6
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoad.Location = New System.Drawing.Point(8, 8)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoad.Size = New System.Drawing.Size(121, 25)
        Me.cmdLoad.TabIndex = 5
        Me.cmdLoad.Text = "Load Map Document"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFullExtent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullExtent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullExtent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullExtent.Location = New System.Drawing.Point(352, 360)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullExtent.Size = New System.Drawing.Size(153, 25)
        Me.cmdFullExtent.TabIndex = 2
        Me.cmdFullExtent.Text = "Zoom to Full Data Extent"
        Me.cmdFullExtent.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(65, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Focus Map:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(120, 360)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(257, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Right mouse button to pan page or data."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(120, 376)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(249, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Left mouse button to zoom in on page or data."
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(152, 96)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 12
        '
        'AxMapControl1
        '
        Me.AxMapControl1.Location = New System.Drawing.Point(239, 48)
        Me.AxMapControl1.Name = "AxMapControl1"
        Me.AxMapControl1.OcxState = CType(resources.GetObject("AxMapControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMapControl1.Size = New System.Drawing.Size(266, 306)
        Me.AxMapControl1.TabIndex = 13
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(8, 76)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(225, 278)
        Me.AxPageLayoutControl1.TabIndex = 14
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(515, 402)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxMapControl1)
        Me.Controls.Add(Me.cmdZoomPage)
        Me.Controls.Add(Me.cboMaps)
        Me.Controls.Add(Me.txbPath)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Update MapControl's Map with PageLayoutControl's Focus Map "
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMapControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Dim m_bUpdateFocusMap As Boolean
    Dim m_bReplacedPageLayout As Boolean

    Private Sub cmdFullExtent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFullExtent.Click

        m_bUpdateFocusMap = True

        'Zoom to the full extent of the data in the map
        AxMapControl1.Extent = AxMapControl1.FullExtent

    End Sub

    Private Sub cmdZoomPage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdZoomPage.Click

        'Zoom to the whole page
        AxPageLayoutControl1.ZoomToWholePage()

    End Sub

    Private Sub cmdLoad_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoad.Click

        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Browse Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If sFilePath = "" Then Exit Sub

        'If valid map document
        If AxPageLayoutControl1.CheckMxFile(sFilePath) Then
            'Set mouse pointers
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
            AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
            'Reset controls
            AxMapControl1.ActiveView.Clear()
            AxMapControl1.ActiveView.GraphicsContainer.DeleteAllElements()
            AxMapControl1.Refresh()
            cboMaps.Items.Clear()
            txbPath.Text = sFilePath
            'Load map document
            AxPageLayoutControl1.LoadMxFile(sFilePath)
            'Set mouse pointers
            AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
            AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
        Else
            MsgBox(sFilePath & " is not a valid ArcMap document")
            Exit Sub
        End If

    End Sub

    Public Sub ListMaps()

        'Get IGraphicsContainer interface
        Dim pGraphicsContainer As IGraphicsContainer
        pGraphicsContainer = AxPageLayoutControl1.GraphicsContainer
        pGraphicsContainer.Reset()

        'Query Interface for IElement interface
        Dim pElement As IElement
        pElement = pGraphicsContainer.Next

        Dim index As Short
        index = 0
        'Loop through the elements
        Dim pMapFrame As IMapFrame
        Dim pElementProperties As IElementProperties
        Dim sMapName As String
        Do While Not pElement Is Nothing

            'Query interface for IMapFrame interface
            If TypeOf pElement Is IMapFrame Then
                pMapFrame = pElement

                'Query interface for IElementProperties interface
                pElementProperties = pElement

                'Get the name of the Map in the MapFrame
                sMapName = pMapFrame.Map.Name

                'Set the name of the MapFrame to the Map's name
                pElementProperties.Name = sMapName
                'Add the map name to the ComboBox
                cboMaps.Items.Insert(index, pMapFrame.Map.Name)

                'If the Map is the FocusMap select the MapName in the ComboBox
                If sMapName = AxPageLayoutControl1.ActiveView.FocusMap.Name Then
                    cboMaps.SelectedIndex = index
                End If

                index = index + 1
            End If
            pElement = pGraphicsContainer.Next
        Loop

    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        m_bUpdateFocusMap = False
        m_bReplacedPageLayout = False
        AxMapControl1.ShowScrollbars = False

    End Sub

    Public Sub CopyAndOverwriteMap()

        'Get IObjectCopy interface
        Dim pObjectCopy As IObjectCopy
        pObjectCopy = New ObjectCopyClass

        'Get IUnknown interface (map to copy)
        Dim pToCopyMap As Object
        pToCopyMap = AxPageLayoutControl1.ActiveView.FocusMap

        'Each Map contained within the PageLayout encapsulated by the 
        'PageLayoutControl, resides within a separate MapFrame, and therefore 
        'have their IMap::IsFramed property set to True. A Map contained within the 
        'MapControl does not reside within a MapFrame. As such before 
        'overwriting the MapControl's map, the IMap::IsFramed property must be set 
        'to False. Failure to do this will lead to corrupted map documents saved 
        'containing the contents of the MapControl.        
        Dim pMap As IMap
        pMap = pToCopyMap
        pMap.IsFramed = False

        'Get IUnknown interface (copied map)
        Dim pCopiedMap As Object
        pCopiedMap = pObjectCopy.Copy(pToCopyMap)

        'Get IUnknown interface (map to overwrite)
        Dim pToOverwriteMap As Object
        pToOverwriteMap = AxMapControl1.Map

        'Overwrite the MapControl's map
        pObjectCopy.Overwrite(pCopiedMap, pToOverwriteMap)

        SetMapExtent()

    End Sub

    Private Sub SetMapExtent()

        'Get IActiveView interface
        Dim pActiveView As IActiveView
        pActiveView = AxPageLayoutControl1.ActiveView.FocusMap

        'Set the control's extent
        AxMapControl1.Extent = pActiveView.Extent
        'Refresh the display
        AxMapControl1.Refresh()

    End Sub

    Private Sub AxPageLayoutControl1_OnFocusMapChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxPageLayoutControl1.OnFocusMapChanged
        CopyAndOverwriteMap()
    End Sub

    Private Sub cboMaps_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMaps.SelectedIndexChanged
        Dim pPageLayoutControl As IPageLayoutControl
        pPageLayoutControl = AxPageLayoutControl1.Object
        'Get IMapFrame interface
        Dim pElement As IMapFrame
        pElement = pPageLayoutControl.FindElementByName(cboMaps.Text)

        'Set the FocusMap
        AxPageLayoutControl1.ActiveView.FocusMap = pElement.Map
    End Sub

    Private Sub AxPageLayoutControl1_OnAfterScreenDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnAfterScreenDrawEvent) Handles AxPageLayoutControl1.OnAfterScreenDraw
        'Set mouse pointers
        AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
        AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault

        If m_bReplacedPageLayout = False Then Exit Sub

        CopyAndOverwriteMap()
        m_bReplacedPageLayout = False
    End Sub

    Private Sub AxPageLayoutControl1_OnBeforeScreenDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnBeforeScreenDrawEvent) Handles AxPageLayoutControl1.OnBeforeScreenDraw
        'Set mouse pointers
        AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
        AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
    End Sub

    Private Sub AxMapControl1_OnBeforeScreenDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnBeforeScreenDrawEvent) Handles AxMapControl1.OnBeforeScreenDraw
        'Set mouse pointers
        AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
        AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass
    End Sub

    Private Sub AxMapControl1_OnAfterScreenDraw(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent) Handles AxMapControl1.OnAfterScreenDraw
        'Set mouse pointers
        AxPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault
        AxMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault

        If m_bUpdateFocusMap = False Then Exit Sub

        'Get IActiveView interface
        Dim pActiveView As IActiveView
        pActiveView = AxPageLayoutControl1.ActiveView.FocusMap

        'Get IDisplayTransformation interface
        Dim pDisplayTransformation As IDisplayTransformation
        pDisplayTransformation = pActiveView.ScreenDisplay.DisplayTransformation

        'Set the visible extent of the focus map
        pDisplayTransformation.VisibleBounds = AxMapControl1.Extent
        'Refresh the focus map
        pActiveView.Refresh()

        m_bUpdateFocusMap = False
    End Sub

    Private Sub AxMapControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles AxMapControl1.OnMouseDown
        m_bUpdateFocusMap = True

        If e.button = 1 Then
            AxMapControl1.Extent = AxMapControl1.TrackRectangle
        ElseIf e.button = 2 Then
            AxMapControl1.Pan()
        End If
    End Sub

    Private Sub AxPageLayoutControl1_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent) Handles AxPageLayoutControl1.OnMouseDown
        If e.button = 1 Then
            AxPageLayoutControl1.Extent = AxPageLayoutControl1.TrackRectangle
        ElseIf e.button = 2 Then
            AxPageLayoutControl1.Pan()
        End If
    End Sub

    Private Sub AxPageLayoutControl1_OnPageLayoutReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnPageLayoutReplacedEvent) Handles AxPageLayoutControl1.OnPageLayoutReplaced
        m_bReplacedPageLayout = True
        ListMaps()
    End Sub

End Class