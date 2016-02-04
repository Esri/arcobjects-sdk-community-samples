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
' Copyright 2011 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
' 
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at <your ArcGIS install location>/DeveloperKit10.2/userestrictions.txt.
' 

Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.OutputExtensions
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms

Public Class PrintActiveViewArcPressVB_Net_Addin
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        'calls the PrintActiveViewVBParameterized function with an argument.
        PrintActiveViewVBParameterized(3)
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub

    Private Sub PrintActiveViewVBParameterized(ByVal iResampleRatio As Integer)
        'Prints the active view of the document to the default printer using the 
        'ArcPress Printer Engine.  If the printer is not supported by the ArcPress
        'printer engine's "auto-select" driver selection method, the function exits and
        'no print is made.
        Dim docActiveView As IActiveView = My.ArcMap.Document.ActiveView
        Dim docPrinter As IPrinter
        Dim PrintAndExport As IPrintAndExport = New PrintAndExport
        Dim docArcPressPrinter As IArcPressPrinter
        Dim sNameRoot As String
        Dim iNumPages As Short

        docArcPressPrinter = New ArcPressPrinter()
        docPrinter = New ArcPressPrinter()

        Try

            'Attempt to auto-select a driver based on the printer's name.  If no driver can be 
            'auto-selected, exit the function and show a message about the failure.
            docArcPressPrinter.AutoSelectDriverByName(My.ArcMap.ThisApplication.Paper.PrinterName)
            If docArcPressPrinter.SelectedDriverId Is Nothing Then
                MessageBox.Show("Cannot auto-select ArcPress Driver for " & My.ArcMap.ThisApplication.Paper.PrinterName & ".", "ArcPress Driver Auto-select Error")
                Exit Sub
            End If

            'Pass the newly created ArcPressPrinter to docPrinter.
            docPrinter = docArcPressPrinter
            sNameRoot = "PrintActiveViewArcPressSample"

            'using the current printer.
            docPrinter.Paper = My.ArcMap.ThisApplication.Paper

            'make sure the paper orientation is set to the orientation matching the current view.
            docPrinter.Paper.Orientation = My.ArcMap.Document.PageLayout.Page.Orientation

            'set the spool filename (this is the job name that shows up in the print queue)
            docPrinter.SpoolFileName = sNameRoot

            ' Find out how many printer pages the output will cover.  iNumPages will always be 1 
            ' unless the user explicitly sets the tiling options in the file->Print dialog.  
            If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then
                My.ArcMap.Document.PageLayout.Page.PrinterPageCount(docPrinter, 0, iNumPages)
            Else
                'always set the number of pages to 1 for a data view.
                iNumPages = 1
            End If

            Dim lCurrentPageNum As Short
            For lCurrentPageNum = 1 To iNumPages Step lCurrentPageNum + 1
                Try
                    PrintAndExport.Print(docActiveView, docPrinter, My.ArcMap.Document.PageLayout.Page, lCurrentPageNum, iResampleRatio, Nothing)
                Catch ex As Exception
                    MessageBox.Show("Printing cancelled for page " + lCurrentPageNum)
                End Try

            Next

        Catch ex As Exception
            MessageBox.Show(My.ArcMap.ThisApplication.Paper.PrinterName & " is not a supported printer.", "ArcPress Driver Auto-select Error")
        End Try

    End Sub

End Class

 
