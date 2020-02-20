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
Imports System.IO
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS
Public Class Form1
    Inherits System.Windows.Forms.Form

    Private m_pTocControl As ITOCControl
    Private m_sTempFile As String
    Private m_sTempDir As String
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Private m_pLayer As ILayer

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.Engine) Then
            If Not RuntimeManager.Bind(ProductCode.Desktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit or other indication in the application
            End If
        End If
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
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents cboStyleSheets As System.Windows.Forms.ComboBox
    Friend WithEvents buttonLoad As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AxPageLayoutControl1 As ESRI.ArcGIS.Controls.AxPageLayoutControl
    Friend WithEvents AxLicenseControl1 As ESRI.ArcGIS.Controls.AxLicenseControl
    Friend WithEvents AxTOCControl1 As ESRI.ArcGIS.Controls.AxTOCControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.cboStyleSheets = New System.Windows.Forms.ComboBox
        Me.buttonLoad = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxPageLayoutControl1 = New ESRI.ArcGIS.Controls.AxPageLayoutControl
        Me.AxLicenseControl1 = New ESRI.ArcGIS.Controls.AxLicenseControl
        Me.AxTOCControl1 = New ESRI.ArcGIS.Controls.AxTOCControl
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.label3.Location = New System.Drawing.Point(192, 40)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(336, 16)
        Me.label3.TabIndex = 15
        Me.label3.Text = "3) Right click a layer on the TOCControl to display its metadata"
        '
        'label2
        '
        Me.label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.label2.Location = New System.Drawing.Point(192, 24)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(296, 16)
        Me.label2.TabIndex = 14
        Me.label2.Text = "2) Select a style sheet or enter the file path to style sheet"
        '
        'label1
        '
        Me.label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.label1.Location = New System.Drawing.Point(192, 8)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(296, 16)
        Me.label1.TabIndex = 13
        Me.label1.Text = "1) Load a map document into the PageLayoutControl"
        '
        'cboStyleSheets
        '
        Me.cboStyleSheets.Location = New System.Drawing.Point(547, 35)
        Me.cboStyleSheets.Name = "cboStyleSheets"
        Me.cboStyleSheets.Size = New System.Drawing.Size(329, 21)
        Me.cboStyleSheets.TabIndex = 12
        '
        'buttonLoad
        '
        Me.buttonLoad.Location = New System.Drawing.Point(8, 8)
        Me.buttonLoad.Name = "buttonLoad"
        Me.buttonLoad.Size = New System.Drawing.Size(176, 48)
        Me.buttonLoad.TabIndex = 11
        Me.buttonLoad.Text = "Load Document"
        '
        'AxPageLayoutControl1
        '
        Me.AxPageLayoutControl1.Location = New System.Drawing.Point(225, 64)
        Me.AxPageLayoutControl1.Name = "AxPageLayoutControl1"
        Me.AxPageLayoutControl1.OcxState = CType(resources.GetObject("AxPageLayoutControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPageLayoutControl1.Size = New System.Drawing.Size(316, 426)
        Me.AxPageLayoutControl1.TabIndex = 20
        '
        'AxLicenseControl1
        '
        Me.AxLicenseControl1.Enabled = True
        Me.AxLicenseControl1.Location = New System.Drawing.Point(128, 88)
        Me.AxLicenseControl1.Name = "AxLicenseControl1"
        Me.AxLicenseControl1.OcxState = CType(resources.GetObject("AxLicenseControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxLicenseControl1.Size = New System.Drawing.Size(32, 32)
        Me.AxLicenseControl1.TabIndex = 21
        '
        'AxTOCControl1
        '
        Me.AxTOCControl1.Location = New System.Drawing.Point(12, 64)
        Me.AxTOCControl1.Name = "AxTOCControl1"
        Me.AxTOCControl1.OcxState = CType(resources.GetObject("AxTOCControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxTOCControl1.Size = New System.Drawing.Size(207, 426)
        Me.AxTOCControl1.TabIndex = 22
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(547, 64)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(329, 426)
        Me.WebBrowser1.TabIndex = 23
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(888, 502)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.AxLicenseControl1)
        Me.Controls.Add(Me.AxTOCControl1)
        Me.Controls.Add(Me.AxPageLayoutControl1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.cboStyleSheets)
        Me.Controls.Add(Me.buttonLoad)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form1"
        Me.Text = "MetadataViewer"
        CType(Me.AxPageLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxLicenseControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxTOCControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub buttonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonLoad.Click
        'Open a file dialog for selecting map documents
        OpenFileDialog1.Title = "Select Map Document"
        OpenFileDialog1.Filter = "Map Documents (*.mxd)|*.mxd"
        OpenFileDialog1.ShowDialog()

        'Exit if no map document is selected
        Dim sFilePath As String
        sFilePath = OpenFileDialog1.FileName
        If (sFilePath = "") Then Exit Sub

        'Load the specified mxd
        If (AxPageLayoutControl1.CheckMxFile(sFilePath) = False) Then
            System.Windows.Forms.MessageBox.Show("This document cannot be loaded!")
            Exit Sub
        End If
        AxPageLayoutControl1.LoadMxFile(sFilePath, "")

        'Set the current directory to the that of the executable
        Directory.SetCurrentDirectory(m_sTempDir)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        m_pTocControl = AxTOCControl1.Object

        'Set buddy control
        m_pTocControl.SetBuddyControl(AxPageLayoutControl1)

        'Get the directory of the executable
        m_sTempDir = System.Reflection.Assembly.GetExecutingAssembly().Location
        m_sTempDir = Path.GetDirectoryName(m_sTempDir)
        'The location to save the temporary metadata
        m_sTempFile = m_sTempDir + "metadata.htm"

        'Add style sheets to the combo box
        cboStyleSheets.Items.Insert(0, "Brief.xsl")
        cboStyleSheets.Items.Insert(1, "Attributes.xsl")
        cboStyleSheets.Items.Insert(1, "DataDictionTable.xsl")
        cboStyleSheets.Items.Insert(1, "DataDictionPage.xsl")
        cboStyleSheets.SelectedIndex = 0
    End Sub

    Private Sub ShowMetadata(ByVal pLayer As ILayer)

        If (TypeOf pLayer Is IDataLayer) Then

            'Check style sheet exists
            If (File.Exists(cboStyleSheets.Text) = False) Then
                System.Windows.Forms.MessageBox.Show("The selected style sheet does not exist!", "Missing Style Sheet")
                Exit Sub
            End If

            'QI for IDataLayer
            Dim pDataLayer As IDataLayer
            pDataLayer = pLayer
            'Get the metadata
            Dim pMetadata As IMetadata
            pMetadata = pDataLayer.DataSourceName
            'Get the xml property set from the metadata
            Dim pXML As IXmlPropertySet2
            pXML = pMetadata.Metadata

            'Save the xml to a temporary file and transforms it using the selected style sheet
            pXML.SaveAsFile(cboStyleSheets.Text, "", False, m_sTempFile)

            'Navigate the web browser to the temporary file
            WebBrowser1.Navigate(m_sTempFile)
        Else
            System.Windows.Forms.MessageBox.Show("Metadata shown for IDataLayer objects only", "IDataLayer objects only")
        End If

    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

    End Sub

    Private Sub cboStyleSheets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStyleSheets.SelectedIndexChanged
        'Show the metadata for the layer
        If (m_pLayer Is Nothing) Then Exit Sub
        ShowMetadata(m_pLayer)
    End Sub

    Private Sub AxTOCControl1_OnMouseDown(ByVal sender As System.Object, ByVal e As ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent) Handles AxTOCControl1.OnMouseDown
        'Exit not a right mouse click
        If (e.button <> 2) Then Exit Sub

        Dim pItem As esriTOCControlItem
        Dim pMap As Map = Nothing
        Dim pOther As Object = Nothing, pIndex As Object = Nothing

        'Determine what kind of item has been clicked on
        m_pTocControl.HitTest(e.x, e.y, pItem, CType(pMap, IBasicMap), m_pLayer, pOther, pIndex)

        'Show the metadata for the layer
        If (m_pLayer Is Nothing) Then Exit Sub
        ShowMetadata(m_pLayer)
    End Sub
End Class
