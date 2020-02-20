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
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports System.Windows.Forms

Public Class PrintActiveViewVB_Net_Addin
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
       PrintActiveViewVB(3)
  End Sub

  Protected Overrides Sub OnUpdate()
  End Sub

    Private Sub PrintActiveViewVB(ByVal iResampleRatio As Integer)

        '  Prints the Active View of the document to selected output format. 

        Dim docActiveView As IActiveView = My.ArcMap.Document.ActiveView
        Dim docPrinter As IPrinter
        Dim PrintAndExport As IPrintAndExport = New PrintAndExport
        Dim docPaper As IPaper
        Dim sNameRoot As String

        Dim iNumPages As Short
        Dim sysPrintDocument As System.Drawing.Printing.PrintDocument

        ' assign the output filename.  
        sNameRoot = "PrintActiveViewVBSample"

        ' testing to see if printer instantiated in sysPrintDocument is the 
        ' default printer.  It SHOULD be, but this is just a reality check.
        docPrinter = New EmfPrinter()
        sysPrintDocument = New System.Drawing.Printing.PrintDocument()
        docPaper = New Paper()

        If sysPrintDocument.PrinterSettings.IsDefaultPrinter Then

            'Set docPaper's printername to the printername of the default printer
            docPaper.PrinterName = sysPrintDocument.PrinterSettings.PrinterName
        Else

            'if we get an unexpected result, return.
            MsgBox("Error getting default printer info, exiting...")
            Return
        End If

        'make sure the paper orientation is set to the orientation matching the current view.
        docPaper.Orientation = My.ArcMap.Document.PageLayout.Page.Orientation

        'assing docPrinter's paper.  We have to do this in two steps because you cannot change a 
        'printers' paper after it's assigned.  That's why we set docPaper.PrinterName first.
        docPrinter.Paper = docPaper

        'set the spoolfilename (this is the job name that shows up in the print queue)
        docPrinter.SpoolFileName = sNameRoot

        ' Find out how many printer pages the output will cover.  iNumPages will always be 1 
        ' unless the user explicitly sets the tiling options in the file->Print dialog.  
        If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then
            My.ArcMap.Document.PageLayout.Page.PrinterPageCount(docPrinter, 0, iNumPages)
        Else
            'it's data view, and so we always just need one page.
            iNumPages = 1
        End If

        Dim lCurrentPageNum As Short
        For lCurrentPageNum = 1 To iNumPages Step lCurrentPageNum + 1
            Try
                PrintAndExport.Print(docActiveView, docPrinter, My.ArcMap.Document.PageLayout.Page, lCurrentPageNum, iResampleRatio, Nothing)
            Catch ex As Exception
                MessageBox.Show("An error has occurred: " + ex.Message)
            End Try

        Next

    End Sub

End Class
