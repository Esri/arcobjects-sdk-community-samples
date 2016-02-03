Imports ESRI.ArcGIS.PublisherControls
Imports ESRI.ArcGIS
Imports System.Collections


Public Class AttributeQuery
  Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

        'Load runtime 
        If Not RuntimeManager.Bind(ProductCode.ArcReader) Then
            If Not RuntimeManager.Bind(ProductCode.EngineOrDesktop) Then
                MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.")
                System.Environment.Exit(1) ' Force exit 
            End If
        End If

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
  Friend WithEvents grpBox As System.Windows.Forms.GroupBox
  Friend WithEvents lblValue As System.Windows.Forms.Label
  Friend WithEvents lblOperator As System.Windows.Forms.Label
  Friend WithEvents lblField As System.Windows.Forms.Label
  Friend WithEvents lblFieldType As System.Windows.Forms.Label
  Friend WithEvents lblLayerToQuery As System.Windows.Forms.Label
  Friend WithEvents cmdQuery As System.Windows.Forms.Button
  Friend WithEvents txtValue As System.Windows.Forms.TextBox
  Friend WithEvents cboLayers As System.Windows.Forms.ComboBox
  Friend WithEvents optString As System.Windows.Forms.RadioButton
  Friend WithEvents optNumber As System.Windows.Forms.RadioButton
  Friend WithEvents cboOperator As System.Windows.Forms.ComboBox
  Friend WithEvents cboFields As System.Windows.Forms.ComboBox
  Friend WithEvents cmdFullExtent As System.Windows.Forms.Button
  Friend WithEvents cmdFailCenterAt As System.Windows.Forms.Button
  Friend WithEvents cmdMeetCenterAt As System.Windows.Forms.Button
  Friend WithEvents cmdMeetZoomTo As System.Windows.Forms.Button
  Friend WithEvents lblFails As System.Windows.Forms.Label
  Friend WithEvents optPan As System.Windows.Forms.RadioButton
  Friend WithEvents optZoomOut As System.Windows.Forms.RadioButton
  Friend WithEvents optZoomIn As System.Windows.Forms.RadioButton
  Friend WithEvents cmdFailZoomTo As System.Windows.Forms.Button
  Friend WithEvents cmdMeetFlash As System.Windows.Forms.Button
  Friend WithEvents lblMeets As System.Windows.Forms.Label
  Friend WithEvents cmdOpen As System.Windows.Forms.Button
  Friend WithEvents cmdFailFlash As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
  Friend WithEvents AxArcReaderControl1 As ESRI.ArcGIS.PublisherControls.AxArcReaderControl
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AttributeQuery))
        Me.grpBox = New System.Windows.Forms.GroupBox
        Me.lblValue = New System.Windows.Forms.Label
        Me.lblOperator = New System.Windows.Forms.Label
        Me.lblField = New System.Windows.Forms.Label
        Me.lblFieldType = New System.Windows.Forms.Label
        Me.lblLayerToQuery = New System.Windows.Forms.Label
        Me.cmdQuery = New System.Windows.Forms.Button
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.cboLayers = New System.Windows.Forms.ComboBox
        Me.optString = New System.Windows.Forms.RadioButton
        Me.optNumber = New System.Windows.Forms.RadioButton
        Me.cboOperator = New System.Windows.Forms.ComboBox
        Me.cboFields = New System.Windows.Forms.ComboBox
        Me.cmdFullExtent = New System.Windows.Forms.Button
        Me.cmdFailCenterAt = New System.Windows.Forms.Button
        Me.cmdMeetCenterAt = New System.Windows.Forms.Button
        Me.cmdMeetZoomTo = New System.Windows.Forms.Button
        Me.lblFails = New System.Windows.Forms.Label
        Me.optPan = New System.Windows.Forms.RadioButton
        Me.optZoomOut = New System.Windows.Forms.RadioButton
        Me.optZoomIn = New System.Windows.Forms.RadioButton
        Me.cmdFailZoomTo = New System.Windows.Forms.Button
        Me.cmdMeetFlash = New System.Windows.Forms.Button
        Me.lblMeets = New System.Windows.Forms.Label
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.cmdFailFlash = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AxArcReaderControl1 = New ESRI.ArcGIS.PublisherControls.AxArcReaderControl
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.grpBox.SuspendLayout()
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBox
        '
        Me.grpBox.Controls.Add(Me.lblValue)
        Me.grpBox.Controls.Add(Me.lblOperator)
        Me.grpBox.Controls.Add(Me.lblField)
        Me.grpBox.Controls.Add(Me.lblFieldType)
        Me.grpBox.Controls.Add(Me.lblLayerToQuery)
        Me.grpBox.Controls.Add(Me.cmdQuery)
        Me.grpBox.Controls.Add(Me.txtValue)
        Me.grpBox.Controls.Add(Me.cboLayers)
        Me.grpBox.Controls.Add(Me.optString)
        Me.grpBox.Controls.Add(Me.optNumber)
        Me.grpBox.Controls.Add(Me.cboOperator)
        Me.grpBox.Controls.Add(Me.cboFields)
        Me.grpBox.Location = New System.Drawing.Point(568, 48)
        Me.grpBox.Name = "grpBox"
        Me.grpBox.Size = New System.Drawing.Size(144, 312)
        Me.grpBox.TabIndex = 34
        Me.grpBox.TabStop = False
        Me.grpBox.Text = "Query Criteria"
        '
        'lblValue
        '
        Me.lblValue.Location = New System.Drawing.Point(8, 224)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(120, 16)
        Me.lblValue.TabIndex = 31
        Me.lblValue.Text = "Value:"
        '
        'lblOperator
        '
        Me.lblOperator.Location = New System.Drawing.Point(8, 176)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(88, 16)
        Me.lblOperator.TabIndex = 30
        Me.lblOperator.Text = "Operator:"
        '
        'lblField
        '
        Me.lblField.Location = New System.Drawing.Point(8, 128)
        Me.lblField.Name = "lblField"
        Me.lblField.Size = New System.Drawing.Size(96, 16)
        Me.lblField.TabIndex = 29
        Me.lblField.Text = "Field to Query:"
        '
        'lblFieldType
        '
        Me.lblFieldType.Location = New System.Drawing.Point(8, 80)
        Me.lblFieldType.Name = "lblFieldType"
        Me.lblFieldType.Size = New System.Drawing.Size(104, 16)
        Me.lblFieldType.TabIndex = 28
        Me.lblFieldType.Text = "Field Type:"
        '
        'lblLayerToQuery
        '
        Me.lblLayerToQuery.Location = New System.Drawing.Point(8, 24)
        Me.lblLayerToQuery.Name = "lblLayerToQuery"
        Me.lblLayerToQuery.Size = New System.Drawing.Size(120, 16)
        Me.lblLayerToQuery.TabIndex = 27
        Me.lblLayerToQuery.Text = "Layer to Query:"
        '
        'cmdQuery
        '
        Me.cmdQuery.Location = New System.Drawing.Point(24, 272)
        Me.cmdQuery.Name = "cmdQuery"
        Me.cmdQuery.Size = New System.Drawing.Size(104, 32)
        Me.cmdQuery.TabIndex = 26
        Me.cmdQuery.Text = "Query"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(8, 240)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(120, 20)
        Me.txtValue.TabIndex = 25
        '
        'cboLayers
        '
        Me.cboLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLayers.Location = New System.Drawing.Point(8, 40)
        Me.cboLayers.Name = "cboLayers"
        Me.cboLayers.Size = New System.Drawing.Size(120, 21)
        Me.cboLayers.TabIndex = 24
        '
        'optString
        '
        Me.optString.Location = New System.Drawing.Point(80, 96)
        Me.optString.Name = "optString"
        Me.optString.Size = New System.Drawing.Size(56, 16)
        Me.optString.TabIndex = 23
        Me.optString.Text = "String"
        '
        'optNumber
        '
        Me.optNumber.Location = New System.Drawing.Point(8, 96)
        Me.optNumber.Name = "optNumber"
        Me.optNumber.Size = New System.Drawing.Size(64, 16)
        Me.optNumber.TabIndex = 22
        Me.optNumber.Text = "Number"
        '
        'cboOperator
        '
        Me.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOperator.Location = New System.Drawing.Point(8, 192)
        Me.cboOperator.Name = "cboOperator"
        Me.cboOperator.Size = New System.Drawing.Size(120, 21)
        Me.cboOperator.TabIndex = 21
        '
        'cboFields
        '
        Me.cboFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFields.Location = New System.Drawing.Point(8, 144)
        Me.cboFields.Name = "cboFields"
        Me.cboFields.Size = New System.Drawing.Size(120, 21)
        Me.cboFields.TabIndex = 20
        '
        'cmdFullExtent
        '
        Me.cmdFullExtent.Location = New System.Drawing.Point(232, 16)
        Me.cmdFullExtent.Name = "cmdFullExtent"
        Me.cmdFullExtent.Size = New System.Drawing.Size(72, 24)
        Me.cmdFullExtent.TabIndex = 33
        Me.cmdFullExtent.Text = "Full Extent"
        '
        'cmdFailCenterAt
        '
        Me.cmdFailCenterAt.Location = New System.Drawing.Point(488, 496)
        Me.cmdFailCenterAt.Name = "cmdFailCenterAt"
        Me.cmdFailCenterAt.Size = New System.Drawing.Size(112, 24)
        Me.cmdFailCenterAt.TabIndex = 27
        Me.cmdFailCenterAt.Text = "Center At"
        '
        'cmdMeetCenterAt
        '
        Me.cmdMeetCenterAt.Location = New System.Drawing.Point(136, 496)
        Me.cmdMeetCenterAt.Name = "cmdMeetCenterAt"
        Me.cmdMeetCenterAt.Size = New System.Drawing.Size(112, 24)
        Me.cmdMeetCenterAt.TabIndex = 24
        Me.cmdMeetCenterAt.Text = "Center At"
        '
        'cmdMeetZoomTo
        '
        Me.cmdMeetZoomTo.Location = New System.Drawing.Point(16, 496)
        Me.cmdMeetZoomTo.Name = "cmdMeetZoomTo"
        Me.cmdMeetZoomTo.Size = New System.Drawing.Size(112, 24)
        Me.cmdMeetZoomTo.TabIndex = 23
        Me.cmdMeetZoomTo.Text = "Zoom To"
        '
        'lblFails
        '
        Me.lblFails.Location = New System.Drawing.Point(376, 376)
        Me.lblFails.Name = "lblFails"
        Me.lblFails.Size = New System.Drawing.Size(336, 24)
        Me.lblFails.TabIndex = 36
        '
        'optPan
        '
        Me.optPan.Appearance = System.Windows.Forms.Appearance.Button
        Me.optPan.Location = New System.Drawing.Point(304, 16)
        Me.optPan.Name = "optPan"
        Me.optPan.Size = New System.Drawing.Size(72, 24)
        Me.optPan.TabIndex = 32
        Me.optPan.Text = "Pan"
        Me.optPan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optZoomOut
        '
        Me.optZoomOut.Appearance = System.Windows.Forms.Appearance.Button
        Me.optZoomOut.Location = New System.Drawing.Point(160, 16)
        Me.optZoomOut.Name = "optZoomOut"
        Me.optZoomOut.Size = New System.Drawing.Size(72, 24)
        Me.optZoomOut.TabIndex = 31
        Me.optZoomOut.Text = "Zoom Out"
        Me.optZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optZoomIn
        '
        Me.optZoomIn.Appearance = System.Windows.Forms.Appearance.Button
        Me.optZoomIn.Location = New System.Drawing.Point(88, 16)
        Me.optZoomIn.Name = "optZoomIn"
        Me.optZoomIn.Size = New System.Drawing.Size(72, 24)
        Me.optZoomIn.TabIndex = 30
        Me.optZoomIn.Text = "Zoom In"
        Me.optZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdFailZoomTo
        '
        Me.cmdFailZoomTo.Location = New System.Drawing.Point(368, 496)
        Me.cmdFailZoomTo.Name = "cmdFailZoomTo"
        Me.cmdFailZoomTo.Size = New System.Drawing.Size(112, 24)
        Me.cmdFailZoomTo.TabIndex = 26
        Me.cmdFailZoomTo.Text = "Zoom To"
        '
        'cmdMeetFlash
        '
        Me.cmdMeetFlash.Location = New System.Drawing.Point(256, 496)
        Me.cmdMeetFlash.Name = "cmdMeetFlash"
        Me.cmdMeetFlash.Size = New System.Drawing.Size(104, 24)
        Me.cmdMeetFlash.TabIndex = 25
        Me.cmdMeetFlash.Text = "Flash"
        '
        'lblMeets
        '
        Me.lblMeets.Location = New System.Drawing.Point(16, 376)
        Me.lblMeets.Name = "lblMeets"
        Me.lblMeets.Size = New System.Drawing.Size(344, 24)
        Me.lblMeets.TabIndex = 35
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(16, 16)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(72, 24)
        Me.cmdOpen.TabIndex = 29
        Me.cmdOpen.Text = "Open"
        '
        'cmdFailFlash
        '
        Me.cmdFailFlash.Location = New System.Drawing.Point(608, 496)
        Me.cmdFailFlash.Name = "cmdFailFlash"
        Me.cmdFailFlash.Size = New System.Drawing.Size(104, 24)
        Me.cmdFailFlash.TabIndex = 28
        Me.cmdFailFlash.Text = "Flash"
        '
        'AxArcReaderControl1
        '
        Me.AxArcReaderControl1.Location = New System.Drawing.Point(16, 48)
        Me.AxArcReaderControl1.Name = "AxArcReaderControl1"
        Me.AxArcReaderControl1.Size = New System.Drawing.Size(544, 312)
        Me.AxArcReaderControl1.TabIndex = 39
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(19, 403)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(341, 87)
        Me.DataGridView1.TabIndex = 40
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(368, 400)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(340, 90)
        Me.DataGridView2.TabIndex = 41
        '
        'AttributeQuery
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(720, 525)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.AxArcReaderControl1)
        Me.Controls.Add(Me.grpBox)
        Me.Controls.Add(Me.cmdFullExtent)
        Me.Controls.Add(Me.cmdFailCenterAt)
        Me.Controls.Add(Me.cmdMeetCenterAt)
        Me.Controls.Add(Me.cmdMeetZoomTo)
        Me.Controls.Add(Me.lblFails)
        Me.Controls.Add(Me.optPan)
        Me.Controls.Add(Me.optZoomOut)
        Me.Controls.Add(Me.optZoomIn)
        Me.Controls.Add(Me.cmdFailZoomTo)
        Me.Controls.Add(Me.cmdMeetFlash)
        Me.Controls.Add(Me.lblMeets)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.cmdFailFlash)
        Me.Name = "AttributeQuery"
        Me.Text = "AttributeQuery (LesserThan / GreaterThan) "
        Me.grpBox.ResumeLayout(False)
        Me.grpBox.PerformLayout()
        CType(Me.AxArcReaderControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

  Private m_pARFeatureSetMeets As ARFeatureSet
  Private m_pARFeatureSetFails As ARFeatureSet
  Private m_InverseOperators(1, 5)
  Private m_LayersIndex As Collections.Hashtable

  Private Sub AttributeQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'Disable search  & map tools
    EnableSearchTools(False)
    EnableMapTools(False)
    EnableMeetHighlightTools(False)
    EnableFailHighlightTools(False)

    'Populate Inverse Operators array
    PopulateInverseOperators()

    optNumber.Checked = True

  End Sub

  Private Sub MixedControls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optZoomIn.Click, optZoomOut.Click, optPan.Click

    Select Case sender.Name
      Case "optZoomIn"
        AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomIn
      Case "optZoomOut"
        AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapZoomOut
      Case "optPan"
        AxArcReaderControl1.CurrentARTool = esriARTool.esriARToolMapPan
    End Select

  End Sub
  Private Sub MixedDisplayResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMeetZoomTo.Click, cmdMeetCenterAt.Click, cmdMeetFlash.Click, cmdFailZoomTo.Click, cmdFailCenterAt.Click, cmdFailFlash.Click

    Select Case sender.Name
      Case "cmdMeetZoomTo"
        m_pARFeatureSetMeets.ZoomTo()
      Case "cmdMeetCenterAt"
        m_pARFeatureSetMeets.CenterAt()
      Case "cmdMeetFlash"
        m_pARFeatureSetMeets.Flash()
      Case "cmdFailZoomTo"
        m_pARFeatureSetFails.ZoomTo()
      Case "cmdFailCenterAt"
        m_pARFeatureSetFails.CenterAt()
      Case "cmdFailFlash"
        m_pARFeatureSetFails.Flash()
    End Select

  End Sub
  Private Sub DataType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNumber.Click, optString.Click

    Select Case sender.Name
      Case "optNumber"
        PopulateFields(False)
        PopulateOperators(False)
      Case "optString"
        PopulateFields(True)
        PopulateOperators(True)
    End Select

  End Sub
  Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

    'Open a file dialog for selecting map documents
    OpenFileDialog1.Title = "Select Published Map Document"
    OpenFileDialog1.Filter = "Published Map Documents (*.pmf)|*.pmf"
    OpenFileDialog1.ShowDialog()

    'Exit if no map document is selected
    Dim sFilePath As String
    sFilePath = OpenFileDialog1.FileName
    If sFilePath = "" Then Exit Sub

    'Load the specified pmf
    If AxArcReaderControl1.CheckDocument(sFilePath) = True Then
      AxArcReaderControl1.LoadDocument(sFilePath)
    Else
      MsgBox("This document cannot be loaded!")
      Exit Sub
    End If

    'Determine whether permission to search layers and query field values
    Dim bQueryFeatures As Boolean
    Dim bQueryValues As Boolean
    bQueryFeatures = AxArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryFeatures)
    bQueryValues = AxArcReaderControl1.HasDocumentPermission(esriARDocumentPermissions.esriARDocumentPermissionsQueryValues)

    If (bQueryFeatures = False) Or (bQueryValues = False) Then
      MsgBox("The selected Document does not have Query Permissions.", vbInformation)
      AxArcReaderControl1.UnloadDocument()
      Exit Sub
    End If

    'Disable search  & map tools
    cboLayers.Items.Clear()
    cboFields.Items.Clear()
    EnableSearchTools(False)
    EnableMeetHighlightTools(False)
    EnableFailHighlightTools(False)

    'Add map layers to combo and store in HashTable with combo index
    m_LayersIndex = New Hashtable
    ARPopulateComboWithMapLayers(cboLayers, m_LayersIndex)

    'Select first searchable layer
    Dim i As Integer
    For i = 0 To cboLayers.Items.Count - 1 Step i + 1
      Dim pARLayer As ARLayer = CType(m_LayersIndex(i), ARLayer)
      If (pARLayer.Searchable = True) Then
        cboLayers.SelectedIndex = i
        Exit For
      End If
    Next

    'Enable search & map tools
    EnableSearchTools(True)
    EnableMapTools(True)

  End Sub
  Private Sub ARPopulateComboWithMapLayers(ByVal Layers As ComboBox, ByVal LayersIndex As System.Collections.Hashtable)

    'In case cboLayers is already populated
    LayersIndex.Clear()
    Layers.Items.Clear()

    Dim pARLayer As ARLayer
    Dim pARGroupLayer As ARLayer

    ' Get the focus map
    Dim pARMap As ARMap = AxArcReaderControl1.ARPageLayout.FocusARMap

    ' Loop through each layer in the focus map
    Dim i As Integer
    For i = 0 To pARMap.ARLayerCount - 1 Step i + 1
      ' Get the layer name
      pARLayer = pARMap.ARLayer(i)
      ''Code was here
      If pARLayer.IsGroupLayer = True Then
        'If a GroupLayer add the ARChildLayers to the combo and HashTable
        Dim g As Integer
        For g = 0 To pARLayer.ARLayerCount - 1 Step g + 1
          pARGroupLayer = pARMap.ARLayer(i).ChildARLayer(g)
          Layers.Items.Add(pARGroupLayer.Name)
          LayersIndex.Add(Layers.Items.Count - 1, pARGroupLayer)
        Next
      ElseIf pARLayer.Searchable = True Then
        Layers.Items.Add(pARLayer.Name)
        LayersIndex.Add(Layers.Items.Count - 1, pARLayer)
      End If
    Next

  End Sub
  Private Sub PopulateFields(ByVal bIsStringField As Boolean)

    Try
      'Clear all items in fields combo
      cboFields.Items.Clear()

      Dim pARLayer As ARLayer
      pARLayer = CType(m_LayersIndex(cboLayers.SelectedIndex), ARLayer)

      Dim pARSearchDef As ArcReaderSearchDef
      pARSearchDef = New ArcReaderSearchDef

      Dim pFeatureCursor As ARFeatureCursor
      pFeatureCursor = pARLayer.SearchARFeatures(pARSearchDef)

      'Get the first feature in order to access the field names
      Dim pARFeature As ARFeature
      pARFeature = pFeatureCursor.NextARFeature

      'Loop through fields and add field names to combo
      Dim i As Integer
      i = 0

      Do Until i = pARFeature.FieldCount
        If bIsStringField = True Then
          If pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeString Then
            cboFields.Items.Add(pARFeature.FieldName(i))
          End If
        Else
          If (pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeDouble) Or _
          (pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeInteger) Or _
          (pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeSingle) Or _
          (pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeSmallInteger) Or _
          (pARFeature.FieldType(i) = esriARFieldType.esriARFieldTypeOID) Then
            cboFields.Items.Add(pARFeature.FieldName(i))
          End If
        End If
        i = i + 1
      Loop

      If cboFields.Items.Count <> 0 Then
        cboFields.SelectedIndex = 0
      End If
    Catch
      MsgBox("An error occurred populating the Field ComboBox.")
    End Try

  End Sub
  Private Sub PopulateOperators(ByVal bIsStringField As Boolean)

    'Clear any current values from combo
    cboOperator.Items.Clear()

    If bIsStringField = True Then
      cboOperator.Items.Insert(0, "=")
      cboOperator.Items.Insert(1, "<>")
    Else
      cboOperator.Items.Insert(0, "=")
      cboOperator.Items.Insert(1, "<>")
      cboOperator.Items.Insert(2, ">")
      cboOperator.Items.Insert(3, ">=")
      cboOperator.Items.Insert(4, "<=")
      cboOperator.Items.Insert(5, "<")
    End If

    cboOperator.SelectedIndex = 0

  End Sub
  Private Sub PopulateInverseOperators()

    'Populate the collection
    'It is used to inverse the operator used in the WhereClause
    'Array def is (Operator in List Box, Inverse Operator)
    m_InverseOperators(0, 0) = "="
    m_InverseOperators(1, 0) = "<>"

    m_InverseOperators(0, 1) = "<>"
    m_InverseOperators(1, 1) = "="

    m_InverseOperators(0, 2) = ">"
    m_InverseOperators(1, 2) = "<="

    m_InverseOperators(0, 3) = ">="
    m_InverseOperators(1, 3) = "<"

    m_InverseOperators(0, 4) = "<="
    m_InverseOperators(1, 4) = ">"

    m_InverseOperators(0, 5) = "<"
    m_InverseOperators(1, 5) = ">="

  End Sub
  Private Sub EnableSearchTools(ByVal EnabledState As Boolean)

    txtValue.Text = ""
    optNumber.Enabled = EnabledState
    optString.Enabled = EnabledState
    cboFields.Enabled = EnabledState
    cboOperator.Enabled = EnabledState
    txtValue.Enabled = EnabledState
    cmdQuery.Enabled = EnabledState

  End Sub
  Private Sub EnableMapTools(ByVal EnabledState As Boolean)

    optZoomIn.Enabled = EnabledState
    optZoomOut.Enabled = EnabledState
    optPan.Enabled = EnabledState
    cmdFullExtent.Enabled = EnabledState

  End Sub
  Private Sub EnableMeetHighlightTools(ByVal EnabledState As Boolean)

    cmdMeetFlash.Enabled = EnabledState
    cmdMeetZoomTo.Enabled = EnabledState
    cmdMeetCenterAt.Enabled = EnabledState

  End Sub
  Private Sub EnableFailHighlightTools(ByVal EnabledState As Boolean)

    cmdFailFlash.Enabled = EnabledState
    cmdFailZoomTo.Enabled = EnabledState
    cmdFailCenterAt.Enabled = EnabledState

  End Sub

  Private Sub cboLayers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLayers.SelectedIndexChanged

    Dim pARLayer As ARLayer = CType(m_LayersIndex(cboLayers.SelectedIndex), ARLayer)
    'Check if layer can be searched
    If (pARLayer.Searchable) Then
      EnableSearchTools(True)
      PopulateFields(optString.Checked)
      PopulateOperators(optString.Checked)
    Else
      MessageBox.Show("The Layer you have selected is not Searchable.")
      EnableSearchTools(False)
    End If

    'Clear Grids, Labels and disable display tools
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

    lblMeets.Text = ""
    lblFails.Text = ""
    EnableMeetHighlightTools(False)
    EnableFailHighlightTools(False)

  End Sub
  Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click

    'Set mouse cursor as this can take some time with large datasets
    Me.Cursor.Current = Cursors.WaitCursor

    'Check value has been entered in field combo
    If Trim(cboFields.Text) = "" Then
      MsgBox("No field has been selected.", vbInformation)
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

    'Check value has been entered in operator combo
    If Trim(cboOperator.Text) = "" Then
      MsgBox("No operator has been selected.", vbInformation)
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

    'Check value has been entered in value textbox
    If Trim(txtValue.Text) = "" Then
      MsgBox("You have not entered a threshold value.", vbInformation)
      txtValue.Focus()
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

    'Get layer to query
    Dim pARMap As ARMap
    pARMap = AxArcReaderControl1.ARPageLayout.FocusARMap

    Dim pARLayer As ARLayer = CType(m_LayersIndex(cboLayers.SelectedIndex), ARLayer)

    'Build the ARSearchDef
    Dim pARSearchDef As ArcReaderSearchDef
    pARSearchDef = New ArcReaderSearchDef

    'Build WhereClause that meets search criteria
    Dim sWhereClause As String
    'Remove quotes from WhereClause if search is numeric
    If optNumber.Checked = True Then
      sWhereClause = cboFields.Text + " " + cboOperator.Text + " " + txtValue.Text
    Else
      sWhereClause = cboFields.Text + " " + cboOperator.Text + " '" + txtValue.Text + "'"
    End If
    pARSearchDef.WhereClause = sWhereClause

    'Get ARFeatureSet that meets the search criteria
    m_pARFeatureSetMeets = pARLayer.QueryARFeatures(pARSearchDef)

    'Build WhereClause that fails search criteria
    'Remove quotes from WhereClause if search is numeric
    If optNumber.Checked = True Then
      sWhereClause = cboFields.Text + " " + m_InverseOperators(1, cboOperator.SelectedIndex) + " " + txtValue.Text
    Else
      sWhereClause = cboFields.Text + " " + m_InverseOperators(1, cboOperator.SelectedIndex) + " '" + txtValue.Text + "'"
    End If
    pARSearchDef.WhereClause = sWhereClause

    'Get ARFeatureSet that fails search criteria
    m_pARFeatureSetFails = pARLayer.QueryARFeatures(pARSearchDef)

    'Reset mouse cursor
    Me.Cursor = Cursors.Default

    'Populate the FlexGrid Controls with the ARFeatureSet's
        PopulateFlexGrids(DataGridView1, m_pARFeatureSetMeets)
        PopulateFlexGrids(DataGridView2, m_pARFeatureSetFails)

    If m_pARFeatureSetMeets.ARFeatureCount > 0 Then
      EnableMeetHighlightTools(True)
      lblMeets.Text = "Features MEETING the search criteria: " + m_pARFeatureSetMeets.ARFeatureCount.ToString()
    Else
      EnableMeetHighlightTools(False)
            DataGridView1.Rows.Clear()
      lblMeets.Text = "Features MEETING the search criteria: 0"
    End If
    If m_pARFeatureSetFails.ARFeatureCount > 0 Then
      EnableFailHighlightTools(True)
      lblFails.Text = "Features FAILING the search criteria: " + m_pARFeatureSetFails.ARFeatureCount.ToString()
    Else
      EnableFailHighlightTools(False)
            DataGridView2.Rows.Clear()
      lblMeets.Text = "Features FAILING the search criteria: 0"
    End If

  End Sub
    Private Sub PopulateFlexGrids(ByVal pDataGrid As DataGridView, ByVal pARFeatureSet As ARFeatureSet)

        'Get first feature in ARFeatureSet
        Dim pARFeature As ARFeature
        pARFeatureSet.Reset()
        pARFeature = pARFeatureSet.Next

        'Clear grids
        pDataGrid.Rows.Clear()

        'Exit if no features in set
        If pARFeature Is Nothing Then Exit Sub

        'Change cursor while grid populates
        Me.Cursor = Cursors.WaitCursor

        'Loop through and add field names  
        Dim i As Integer
        For i = 0 To pARFeature.FieldCount - 1
            pDataGrid.Columns.Add(pARFeature.FieldName(i), pARFeature.FieldName(i))
        Next i

        'add values 
        Dim values As Object() = Array.CreateInstance(GetType(String), pARFeature.FieldCount)

        'Populate Grid
        While (Not (pARFeature Is Nothing))
            For i = 0 To pARFeature.FieldCount - 1
                values(i) = ARFeatureValueAsString(pARFeature, i)
            Next i

            pDataGrid.Rows.Add(values)

            'Move to next Feature in the FeatureSet
            pARFeature = pARFeatureSet.Next()
        End While

        'Reset mouse cursor
        Me.Cursor = Cursors.Default

    End Sub
  Private Function ARFeatureValueAsString(ByVal pARFeature As ARFeature, ByVal pFieldNameIndex As Long) As String

    'If there is an issue accessing the value the function returns a string of asterisks
    'There are many reason Asterisks may be returned...
    'The return value cant be cast into a string e.g. a BLOB value
    'The return value is stored within a hidden field in the PMF
    'The return value is a Geometry Object

    On Error Resume Next
    ARFeatureValueAsString = "***"
    ARFeatureValueAsString = CStr(pARFeature.Value(pARFeature.FieldName(pFieldNameIndex)))

  End Function
  Private Sub txtValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtValue.TextChanged

    If (optNumber.Checked = True) Then
      If (IsNumeric(txtValue.Text) = False) Then
        txtValue.Clear()
      End If
    End If

  End Sub

  Private Sub cmdFullExtent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFullExtent.Click
    AxArcReaderControl1.ARPageLayout.FocusARMap.ZoomToFullExtent()
  End Sub
End Class
