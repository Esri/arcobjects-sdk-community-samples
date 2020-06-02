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
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports ESRI.ArcGIS.ArcMap
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports System.IO

Public Class ExportReport
#Region "Member Variables"
    Private m_FileLocation As String
    Private reports As Dictionary(Of Int32, String)
#End Region

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' store the templates information
        reports = New Dictionary(Of Int32, String)()
        m_FileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        m_FileLocation = Path.Combine(m_FileLocation, "ArcGIS\data\California\Report Templates")
        If (not System.IO.File.Exists(m_FileLocation)) Then Throw New Exception(String.Format("Fix code to point to your sample data: {0} was not found", m_FileLocation))
    

        Dim filePaths As String() = Directory.GetFiles(m_FileLocation, "*.rlf")
        Dim doc As XmlDocument = New XmlDocument()
        Try
            Dim c As Int32 = 0
            Do While c <= (filePaths.Count() - 1)
                Dim fileLocation As String = filePaths(c)
                doc.Load(fileLocation)
                Dim title As String = Me.ReadRTFElement(doc, "/Report", "Name")
                reports.Add(c, fileLocation)
                Me.lstReports.Items.Add(title)
                c += 1
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            doc = Nothing
        End Try
    End Sub

    Private Function ReadRTFElement(ByVal doc As XmlDocument, ByVal tagLocation As String, ByVal elementName As String) As String

        Dim retValue As String = ""
        ' Get and display all the book titles.
        Dim root As XmlElement = doc.DocumentElement
        Dim elemList As XmlNodeList = root.SelectNodes(tagLocation)
        Try
            For Each title As XmlNode In elemList
                If retValue = "" Then
                    retValue = title.Attributes(elementName).Value & Environment.NewLine
                Else
                    retValue = retValue & title.Attributes(elementName).Value & Environment.NewLine
                End If
            Next title
            Return retValue
        Catch ex As Exception
            Throw ex
        Finally
            root = Nothing
            elemList = Nothing
        End Try
    End Function

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim savDialog As SaveFileDialog = New SaveFileDialog()
        savDialog.Filter = "PDF(*.pdf)""|*.pdf"
        savDialog.AddExtension = True
        Dim result As DialogResult
        result = savDialog.ShowDialog()
        Dim savFile As String = savDialog.FileName

        If result = System.Windows.Forms.DialogResult.OK Then
            Dim mxDoc As IMxDocument
            Dim rDS As IReportDataSource = New Report()
            Dim rwTemplate As IReportTemplate
            Dim rwEngine As IReportEngine
            Dim openPDFProcess As Process = New Process()
            Try
                mxDoc = CType(My.ArcMap.Application.Document, IMxDocument)

                Dim i As Integer = 0
                For i = 0 To mxDoc.FocusMap.LayerCount - 1
                    If mxDoc.FocusMap.Layer(i).Name = "Counties" Then
                        rDS.Layer = mxDoc.FocusMap.Layer(i)
                        Exit For
                    End If
                Next i
                rwTemplate = CType(rDS, IReportTemplate)
                rwTemplate.LoadReportTemplate(m_FileLocation)
                rwEngine = CType(rDS, IReportEngine)
                rwEngine.RunReport(Nothing)
                Dim pdfReport As String = (savFile)
                rwEngine.ExportReport(pdfReport, "1", esriReportExportType.esriReportExportPDF)

                ' launch PDF
                openPDFProcess.StartInfo.UseShellExecute = True
                openPDFProcess.StartInfo.CreateNoWindow = True
                openPDFProcess.StartInfo.FileName = pdfReport
                openPDFProcess.Start()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            Finally
                mxDoc = Nothing
                rDS = Nothing
                rwTemplate = Nothing
                rwEngine = Nothing
                openPDFProcess = Nothing
            End Try
        Else
            savDialog.Dispose()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub lstReports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstReports.SelectedIndexChanged
        m_FileLocation = reports(lstReports.SelectedIndex)
        Me.txtReportInformation.Clear()
        Dim doc As XmlDocument = New XmlDocument()
        Try
            doc.Load(m_FileLocation)
            ' Data source
            txtReportInformation.Text = "DataSource:" & Environment.NewLine & ReadRTFElement(doc, "/Report/DataSource/Workspace", "PathName")
            ' Data name
            txtReportInformation.Text = txtReportInformation.Text & Environment.NewLine & "Name:" & Environment.NewLine & ReadRTFElement(doc, "/Report/DataSource", "Name")
            ' Fields
            txtReportInformation.Text = txtReportInformation.Text & Environment.NewLine & "Fields:" & Environment.NewLine & ReadRTFElement(doc, "/Report/ReportFields/Field", "Name")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            doc = Nothing
        End Try
    End Sub
End Class