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
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalystUI
Imports System.Text

Namespace ExportNAClass
  ''' <summary>
  ''' This sample command allows you export a text file version
  ''' of the active class in the Network Analyst window after 
  ''' completion of a successful solve.
  ''' </summary>
  ''' 
  <ClassInterface(ClassInterfaceType.None), Guid("7C12A530-759A-4B12-9241-2215403483E8"), ProgId("ExportNAClass.NAClassToTextfileCmd")> _
  Public NotInheritable Class NAClassToTextfileCmd : Inherits ESRI.ArcGIS.ADF.BaseClasses.BaseCommand : Implements INAWindowCommand
    Private Const DELIMITER As String = Constants.vbTab
    Private m_naExt As INetworkAnalystExtension

    ' set up the bitmap for the command icon
    <DllImport("gdi32.dll")> _
    Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function
    Private Shadows m_bitmap As System.Drawing.Bitmap
    Private m_hBitmap As IntPtr

    Public Sub New()

      ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop)

      ' set up the bitmap transparency
      Dim res As String() = Me.GetType().Assembly.GetManifestResourceNames()
      If res.GetLength(0) > 0 Then
        m_bitmap = New System.Drawing.Bitmap(Me.GetType().Assembly.GetManifestResourceStream(res(0)))
        If Not m_bitmap Is Nothing Then
          m_bitmap.MakeTransparent(m_bitmap.GetPixel(0, 0))
          m_hBitmap = m_bitmap.GetHbitmap()
        End If
      End If
    End Sub

    Protected Overrides Sub Finalize()
      If m_hBitmap.ToInt32() <> 0 Then
        DeleteObject(m_hBitmap)
      End If
    End Sub

#Region "Component Category Registration"
    <ComRegisterFunction()> _
    Private Shared Sub Reg(ByVal regKey As String)
      ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Register(regKey)
      ESRI.ArcGIS.ADF.CATIDs.MxCommands.Register(regKey)
      ' Register with NetworkAnalystWindowCategoryCommand to get the 
      ' command to show up when you right click on the class in the NAWindow
      ESRI.ArcGIS.ADF.CATIDs.NetworkAnalystWindowCategoryCommand.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    Private Shared Sub Unreg(ByVal regKey As String)
      ESRI.ArcGIS.ADF.CATIDs.ControlsCommands.Unregister(regKey)
      ESRI.ArcGIS.ADF.CATIDs.MxCommands.Unregister(regKey)
    End Sub
#End Region

#Region "NAWindow Interaction"
    Private Function GetActiveAnalysisLayer() As INALayer
      If Not m_naExt Is Nothing Then
        Return m_naExt.NAWindow.ActiveAnalysis
      Else
        Return Nothing
      End If
    End Function

    Private Function GetActiveCategory() As INAWindowCategory2
      ' Remove the next 2 lines for an engine only install
      If Not m_naExt Is Nothing Then
        Return TryCast(m_naExt.NAWindow.ActiveCategory, INAWindowCategory2)
      Else
        Return Nothing
      End If
    End Function
#End Region

#Region "Overridden BaseCommand Methods"
    Public Overrides Sub OnCreate(ByVal hook As Object)
      ' Try to get the network analyst extension from the desktop app's extensions
      Dim app As IApplication
      app = TryCast(hook, IApplication)
      If Not app Is Nothing Then
        m_naExt = TryCast(app.FindExtensionByName("Network Analyst"), INetworkAnalystExtension)
      End If
    End Sub

    ''' <summary>
    ''' This command will be enabled only for a NAClass
    ''' associated with a successful solve
    ''' </summary>
    Public Overrides ReadOnly Property Enabled() As Boolean
      Get
        ' there must be an active analysis layer
        Dim naLayer As INALayer = GetActiveAnalysisLayer()
        If Not naLayer Is Nothing Then
          ' the context must be valid
          Dim naContext As INAContext = naLayer.Context
          If Not naContext Is Nothing Then
            Return True
          End If
        End If
        Return False
      End Get
    End Property

    Public Overrides ReadOnly Property Message() As String
      Get
        Return "Export a network analysis class to a text file."
      End Get
    End Property

    Public Overrides ReadOnly Property Bitmap() As Integer
      Get
        Return m_hBitmap.ToInt32()
      End Get
    End Property

    Public Overrides ReadOnly Property Tooltip() As String
      Get
        Return "Export a network analysis class to a text file."
      End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
      Get
        Return "NAClassToTextFileCmd"
      End Get
    End Property

    Public Overrides ReadOnly Property Caption() As String
      Get
        Return "Export To text file..."
      End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
      Get
        Return "Developer Samples"
      End Get
    End Property

    Public Overrides Sub OnClick()
      Try
        ExportToText()
      Catch exception As Exception
        MessageBox.Show(exception.Message, "Error")
      End Try

    End Sub
#End Region

#Region "Overridden INAWindowCommand Methods"
    Public Function Applies(ByVal naLayer As INALayer, ByVal Category As INAWindowCategory) As Boolean Implements ESRI.ArcGIS.NetworkAnalystUI.INAWindowCommand.Applies
      Return True
    End Function
#End Region

    Private Sub ExportToText()
      Dim sfDialog As SaveFileDialog = New SaveFileDialog()
      SetUpSaveDialog(sfDialog)
      ' generate the dialog and verify the user successfully clicked save
      Dim dResult As DialogResult = sfDialog.ShowDialog()

      If dResult = System.Windows.Forms.DialogResult.OK Then
        ' set up the text file to be written
        Dim t As FileInfo = New FileInfo(sfDialog.FileName)
        Dim swText As StreamWriter = t.CreateText()

        Dim table As ITable = TryCast(GetActiveCategory().DataLayer, ITable)

        ' write the first line of the text file as column headers
        swText.WriteLine(GenerateColumnHeaderString(table))

        ' iterate through the table associated with the class
        ' to write out each line of data into the text file
        Dim cursor As ICursor = table.Search(Nothing, True)
        Dim row As IRow = cursor.NextRow()
        Do While Not row Is Nothing
          swText.WriteLine(GenerateDataString(row))
          row = cursor.NextRow()
        Loop
        swText.Close()
      End If
    End Sub

    Private Sub SetUpSaveDialog(ByRef sfDialog As SaveFileDialog)
      sfDialog.AddExtension = True
      sfDialog.Title = "Save an export of the specified class in the active analysis..."
      sfDialog.DefaultExt = "txt"
      sfDialog.OverwritePrompt = True
      sfDialog.FileName = "ClassExport.txt"
      sfDialog.Filter = "Text files (*.txt;*.csv;*.asc;*.tab)|*.txt;*.tab;*.asc;*.csv"
      sfDialog.InitialDirectory = "c:\"
    End Sub

    Private Function GenerateColumnHeaderString(ByRef table As ITable) As String
      Dim field As IField = Nothing

      ' export the names of the fields (tab delimited) as the first line of the export
      Dim fieldNames As String = ""
      Dim i As Integer = 0
      Do While i < table.Fields.FieldCount
        field = table.Fields.Field(i)
        If i > 0 Then
          fieldNames &= DELIMITER
        End If

        Dim columnName As String = field.Name.ToString()

        ' point classes have a special output of X and Y, other classes just output "Shape"
        If field.Type = esriFieldType.esriFieldTypeGeometry Then
          If field.GeometryDef.GeometryType = esriGeometryType.esriGeometryPoint Then
            columnName = "X"
            columnName &= DELIMITER
            columnName &= "Y"
          End If
        End If
        fieldNames &= columnName
        i += 1
      Loop
      Return fieldNames
    End Function

    Private Function GenerateDataString(ByRef row As IRow) As String
      Dim textOut As String = ""

      ' On a zero-based index, iterate through the fields in the collection.
      Dim fieldIndex As Integer = 0
      Do While fieldIndex < row.Fields.FieldCount
        If fieldIndex > 0 Then
          textOut &= DELIMITER
        End If
        Dim field As IField = row.Fields.Field(fieldIndex)

        ' for shape fields in a point layer, export the associated X and Y coordinates
        If field.Type = esriFieldType.esriFieldTypeGeometry Then
          If field.GeometryDef.GeometryType = esriGeometryType.esriGeometryPoint Then
            ' x y location information must be retrieved from the Feature
            Dim point As IPoint = TryCast(row.Value(fieldIndex), ESRI.ArcGIS.Geometry.Point)
            textOut &= point.X.ToString()
            textOut &= DELIMITER
            textOut &= point.Y.ToString()
          ElseIf field.GeometryDef.GeometryType = esriGeometryType.esriGeometryPolygon Or field.GeometryDef.GeometryType = esriGeometryType.esriGeometryPolyline Then
            Dim stringBuffer As New StringBuilder()
            Dim pointCollection As IPointCollection = TryCast(row.Value(fieldIndex), IPointCollection)
            For pointIndex As Integer = 0 To pointCollection.PointCount - 1
              Dim point As IPoint = pointCollection.Point(pointIndex)
              If pointIndex > 0 Then
                stringBuffer.Append(",")
              End If
              stringBuffer.Append("{")
              stringBuffer.Append(point.X)
              stringBuffer.Append(",")
              stringBuffer.Append(point.Y)
              stringBuffer.Append(",")
              stringBuffer.Append(point.Z)
              stringBuffer.Append(",")
              stringBuffer.Append(point.M)
              stringBuffer.Append("}")
            Next
            textOut += stringBuffer.ToString()
          Else
            textOut &= "Shape"
          End If
          ' Handle the Locations field for polyline and polygon barrier classes
        ElseIf field.Name = "Locations" And field.Type = esriFieldType.esriFieldTypeBlob Then
          Dim stringBuffer As New StringBuilder()

          ' get the location ranges out of the barrier feature
          Dim naLocRangesObject As INALocationRangesObject = TryCast(row, INALocationRangesObject)
          If naLocRangesObject Is Nothing Then
            ' Not a location ranges object
            textOut += row.Value(fieldIndex).ToString()
          End If

          Dim naLocRanges As INALocationRanges = naLocRangesObject.NALocationRanges
          If naLocRanges Is Nothing Then
            ' does not have any location ranges
            textOut += row.Value(fieldIndex).ToString()
          End If

          ' add all of the junctions included in the barrier to the Junctions dataGrid
          stringBuffer.Append("{Junctions:{")
          Dim junctionCount As Integer = naLocRanges.JunctionCount
          Dim junctionEID As Integer = -1
          For i As Integer = 0 To junctionCount - 1
            naLocRanges.QueryJunction(i, junctionEID)

            If i > 0 Then
              stringBuffer.Append(",")
            End If
            stringBuffer.Append("{")
            stringBuffer.Append(junctionEID)
            stringBuffer.Append("}")
          Next
          stringBuffer.Append("}")

          ' add all of the edges included in the barrier to the Edges dataGrid
          stringBuffer.Append(",EdgeRanges:{")
          Dim edgeRangeCount As Integer = naLocRanges.EdgeRangeCount
          Dim edgeEID As Integer = -1
          Dim fromPosition As Double, toPosition As Double
          Dim edgeDirection As esriNetworkEdgeDirection = esriNetworkEdgeDirection.esriNEDNone
          For i As Integer = 0 To edgeRangeCount - 1
            naLocRanges.QueryEdgeRange(i, edgeEID, edgeDirection, fromPosition, toPosition)

            Dim directionValue As String = ""
            If edgeDirection = esriNetworkEdgeDirection.esriNEDAlongDigitized Then
              directionValue = "Along Digitized"
            ElseIf edgeDirection = esriNetworkEdgeDirection.esriNEDAgainstDigitized Then
              directionValue = "Against Digitized"
            End If

            If i > 0 Then
              stringBuffer.Append(",")
            End If
            stringBuffer.Append("{")
            stringBuffer.Append(edgeEID)
            stringBuffer.Append(",")
            stringBuffer.Append(directionValue)
            stringBuffer.Append(",")
            stringBuffer.Append(fromPosition)
            stringBuffer.Append(",")
            stringBuffer.Append(toPosition)
            stringBuffer.Append("}")
          Next
          stringBuffer.Append("}")

          textOut += stringBuffer.ToString()
        Else
          textOut &= row.Value(fieldIndex).ToString()
        End If
        fieldIndex += 1
      Loop
      Return textOut
    End Function
  End Class
End Namespace
