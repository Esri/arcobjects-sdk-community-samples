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
Imports ESRI.ArcGIS.esriSystem

Public Class PrintMultiPagesVB_NET_Addin
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    ' gdi and win32 functions - the parameters are INTEGER because in .NET integer is 32 bits.
    Private Declare Function GetDeviceCaps Lib "gdi32" (ByVal hDC As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function GetDC Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByVal lpInitData As IntPtr) As Integer
    Private Declare Function ReleaseDC Lib "user32" _
      (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        PrintMultiPageParameterizedVB(3)
    End Sub

    Protected Overrides Sub OnUpdate()
    End Sub

    Private Sub PrintMultiPageParameterizedVB(ByVal iResampleRatio As Long)

        '  Prints tiled map using IPrinterMPage 
        Dim docActiveView As IActiveView = My.ArcMap.Document.ActiveView
        Dim docPrinter As IPrinter
        Dim iPrevOutputImageQuality As Long
        Dim docOutputRasterSettings As IOutputRasterSettings

        Dim deviceRECT As ESRI.ArcGIS.esriSystem.tagRECT
        Dim docPaper As IPaper
        Dim sNameRoot As String

        Dim iNumPages As Short
        Dim docPrinterBounds As IEnvelope
        Dim VisibleBounds As IEnvelope
        Dim sysPrintDocument As System.Drawing.Printing.PrintDocument

        docPrinterBounds = New EnvelopeClass()
        VisibleBounds = New EnvelopeClass()

        ' save the previous output image quality, so that when the export is complete it will be set back.
        docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation
        iPrevOutputImageQuality = docOutputRasterSettings.ResampleRatio

        ' then set the output quality to the desired ratio
        SetOutputQuality(docActiveView, iResampleRatio)

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

        'assign docPrinter's paper.  We have to do this in two steps because you cannot change a 
        'printers' paper after it's assigned.  That's why we set docPaper.PrinterName first.
        docPrinter.Paper = docPaper

        'set the spoolfilename (this is the job name that shows up in the print queue)
        docPrinter.SpoolFileName = sNameRoot

        ' Get the printer's hDC, so we can use the Win32 GetDeviceCaps function to
        '  get Printer's Physical Printable Area x and y margins
        Dim hInfoDC As Integer
        hInfoDC = CreateDC(docPrinter.DriverName, docPrinter.Paper.PrinterName, "", IntPtr.Zero)

        ' Find out how many printer pages the output will cover.  iNumPages will always be 1 
        ' unless the user explicitly sets the tiling options in the file->Print dialog.  
        If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then
            My.ArcMap.Document.PageLayout.Page.PrinterPageCount(docPrinter, 0, iNumPages)
        Else
            'it's data view, and so we always just need one page.
            iNumPages = 1
        End If

        If iNumPages < 2 Then
            MsgBox("Sample requires map in Layout View and map to be tiled to printer paper")
        End If


        Dim PrintMPage As IPrinterMPage
        Dim intPageHandle As Integer
        PrintMPage = docPrinter

        'Code inside StartMapDocument <-----> EndMapDocument is hard coded to print the first two pages of the tiled print
        'Printer and Page Bounds need to be calculated for each corresponding tile that gets printed

        PrintMPage.StartMapDocument()

        'calculate printer bounds for the first page
        My.ArcMap.Document.PageLayout.Page.GetDeviceBounds(docPrinter, 1, 0, docPrinter.Resolution, docPrinterBounds)

        'Transfer PrinterBounds envelope, offsetting by PHYSICALOFFSETX
        ' the Win32 constant for PHYSICALOFFSETX is 112
        ' the Win32 constant for PHYSICALOFFSETY is 113
        deviceRECT.bottom = CType((docPrinterBounds.YMax - GetDeviceCaps(hInfoDC, 113)), Integer)
        deviceRECT.left = CType((docPrinterBounds.XMin - GetDeviceCaps(hInfoDC, 112)), Integer)
        deviceRECT.right = CType((docPrinterBounds.XMax - GetDeviceCaps(hInfoDC, 112)), Integer)
        deviceRECT.top = CType((docPrinterBounds.YMin - GetDeviceCaps(hInfoDC, 113)), Integer)

        ' Transfer offsetted PrinterBounds envelope back to the deviceRECT
        docPrinterBounds.PutCoords(0, 0, deviceRECT.right - deviceRECT.left, deviceRECT.bottom - deviceRECT.top)

        If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then

            'get the visible bounds for this layout, based on the current page number.
            My.ArcMap.Document.PageLayout.Page.GetPageBounds(docPrinter, 1, 0, VisibleBounds)

        Else
            MsgBox("Please Use Map Layout View for this Sample")
            Exit Sub
        End If


        'start printing the first page bracket, returns handle of the current page.  
        'handle is then passed to the ActiveView.Output()
        intPageHandle = PrintMPage.StartPage(docPrinterBounds, hInfoDC)

        My.ArcMap.Document.ActiveView.Output(intPageHandle, docPrinter.Resolution, deviceRECT, VisibleBounds, Nothing)

        PrintMPage.EndPage() 'end printing the first page bracket

        'calculate printer bounds for the second page
        My.ArcMap.Document.PageLayout.Page.GetDeviceBounds(docPrinter, 2, 0, docPrinter.Resolution, docPrinterBounds)

        'Transfer PrinterBounds envelope, offsetting by PHYSICALOFFSETX
        ' the Win32 constant for PHYSICALOFFSETX is 112
        ' the Win32 constant for PHYSICALOFFSETY is 113
        deviceRECT.bottom = CType((docPrinterBounds.YMax - GetDeviceCaps(hInfoDC, 113)), Integer)
        deviceRECT.left = CType((docPrinterBounds.XMin - GetDeviceCaps(hInfoDC, 112)), Integer)
        deviceRECT.right = CType((docPrinterBounds.XMax - GetDeviceCaps(hInfoDC, 112)), Integer)
        deviceRECT.top = CType((docPrinterBounds.YMin - GetDeviceCaps(hInfoDC, 113)), Integer)

        ' Transfer offsetted PrinterBounds envelope back to the deviceRECT
        docPrinterBounds.PutCoords(0, 0, deviceRECT.right - deviceRECT.left, deviceRECT.bottom - deviceRECT.top)

        If TypeOf My.ArcMap.Document.ActiveView Is IPageLayout Then

            'get the visible bounds for this layout, based on the current page number.
            My.ArcMap.Document.PageLayout.Page.GetPageBounds(docPrinter, 2, 0, VisibleBounds)

        Else
            MsgBox("Please Use Map Layout View for this Sample")
            Exit Sub
        End If


        'start printing the second page bracket, returns handle of the current page.  
        'handle is then passed to the ActiveView.Output()
        intPageHandle = PrintMPage.StartPage(docPrinterBounds, hInfoDC)

        My.ArcMap.Document.ActiveView.Output(intPageHandle, docPrinter.Resolution, deviceRECT, VisibleBounds, Nothing)

        PrintMPage.EndPage() 'end printing the second page bracket

        PrintMPage.EndMapDocument()

        'now set the output quality back to the previous output quality.
        SetOutputQuality(docActiveView, iPrevOutputImageQuality)


        'release the DC...
        ReleaseDC(0, hInfoDC)

    End Sub


    Private Sub SetOutputQuality(ByVal docActiveView As IActiveView, ByVal iResampleRatio As Long)
        '  This function sets OutputImageQuality for the active view.  If the active view is a pagelayout, then
        '  it must also set the output image quality for EACH of the Maps in the pagelayout.

        Dim docGraphicsContainer As IGraphicsContainer
        Dim docElement As IElement
        Dim docOutputRasterSettings As IOutputRasterSettings
        Dim docMapFrame As IMapFrame
        Dim tmpActiveView As IActiveView

        If TypeOf docActiveView Is IMap Then
            docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation
            docOutputRasterSettings.ResampleRatio = CType(iResampleRatio, Integer)
        ElseIf TypeOf docActiveView Is IPageLayout Then
            'assign ResampleRatio for PageLayout
            docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation
            docOutputRasterSettings.ResampleRatio = CType(iResampleRatio, Integer)
            'and assign ResampleRatio to the Maps in the PageLayout
            docGraphicsContainer = docActiveView
            docGraphicsContainer.Reset()

            docElement = docGraphicsContainer.Next()
            While Not docElement Is Nothing
                If TypeOf docElement Is IMapFrame Then
                    docMapFrame = docElement
                    tmpActiveView = docMapFrame.Map
                    docOutputRasterSettings = tmpActiveView.ScreenDisplay.DisplayTransformation
                    docOutputRasterSettings.ResampleRatio = CType(iResampleRatio, Integer)
                End If
                docElement = docGraphicsContainer.Next()
            End While

            docMapFrame = Nothing
            docGraphicsContainer = Nothing
            tmpActiveView = Nothing
        End If
        docOutputRasterSettings = Nothing


    End Sub

End Class
