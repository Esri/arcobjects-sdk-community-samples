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
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Output
Imports System.Windows.Forms

Public Class ExportActiveViewVB_Net
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    ' gdi and win32 functions
    Private Declare Auto Function SystemParametersInfo Lib "user32" (ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByRef pvParam As UInteger, ByVal fWinIni As UInteger) As Boolean

    Public Const SPI_GETFONTSMOOTHING = 74
    Public Const SPI_SETFONTSMOOTHING = 75
    Public Const SPIF_UPDATEINIFILE = &H1

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        'The OnClick Method calls ExportActiveViewParameterized with some arguments.  The first argument is a string
        'which represents which type of output format to create.  The second is a number which represents resolution
        'in dpi.  The third is the resample ratio, valid values are integers between 1(best) and 5(fastest).  Lastly,
        'the last argument is a boolean which controls whether the output is clipped to graphics extent (layout only).

    ExportActiveViewParameterized("JPEG", 300, 1, False)
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub

    Public Sub ExportActiveViewParameterized(ByVal ExportFormat As String, ByVal iOutputResolution As Long, ByVal lResampleRatio As Long, ByVal bClipToGraphicsExtent As Boolean)

        'Export the active view using the specified parameters
        Dim docActiveView As IActiveView
        Dim docExport As IExport
        Dim docPrintAndExport As IPrintAndExport
        Dim RasterSettings As IOutputRasterSettings
        Dim sNameRoot As String
        Dim sOutputDir As String
        Dim bReenable As Boolean


        If (GetFontSmoothing()) Then
            bReenable = True
            DisableFontSmoothing()
            If (GetFontSmoothing()) Then
                MsgBox("Cannot Disable Font Smoothing.  Exiting.")
                Return
            End If
        End If


        docActiveView = My.ArcMap.Document.ActiveView

        ' Create an Export* object and cast the docExport interface pointer onto it.
        ' To export to any format, we simply create the desired Class here
        Select Case ExportFormat
            Case "PDF"
                docExport = New ExportPDF
            Case "EMF"
                docExport = New ExportEMF
            Case "BMP"
                docExport = New ExportBMP
            Case "EPS"
                docExport = New ExportPS
            Case "SVG"
                docExport = New ExportSVG
            Case "GIF"
                docExport = New ExportGIF
            Case "TIF"
                docExport = New ExportTIFF
            Case "JPEG"
                docExport = New ExportJPEG
            Case "PNG"
                docExport = New ExportPNG
            Case "AI"
                docExport = New ExportAI
            Case Else
                MessageBox.Show("Unrecognized output format, defaulting to EMF")
                ExportFormat = "EMF"
                docExport = New ExportEMF
        End Select

        docPrintAndExport = New PrintAndExport

        ' Output Image Quality of the export.  The value here will only be used if the export
        '  object is a format that allows setting of Output Image Quality, i.e. a vector exporter.
        '  The value assigned to ResampleRatio should be in the range 1 to 5.
        '  1 corresponds to "Best", 5 corresponds to "Fast"

        If TypeOf docExport Is IOutputRasterSettings Then
          ' for vector formats, assign a ResampleRatio to control drawing of raster layers at export time
          RasterSettings = docExport
          RasterSettings.ResampleRatio = lResampleRatio

          ' NOTE: for raster formats output quality of the DISPLAY is set to 1 for image export 
          ' formats by default which is what should be used
        End If

        'assign the output path and filename.  We can use the Filter property of the export object to
        ' automatically assign the proper extension to the file.

        sOutputDir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\"
        sNameRoot = "VBExportActiveViewSampleOutput"
        docExport.ExportFileName = sOutputDir & sNameRoot & "." & Right(Split(docExport.Filter, "|")(1), _
                                 Len(Split(docExport.Filter, "|")(1)) - 2)

        docPrintAndExport.Export(docActiveView, docExport, iOutputResolution, bClipToGraphicsExtent, Nothing)

        MessageBox.Show("Finished Exporting " & sOutputDir & sNameRoot & "." & Right(Split(docExport.Filter, "|")(1), _
                                 Len(Split(docExport.Filter, "|")(1)) - 2) & ".", "Export Active View Sample")
        'cleanup for the exporter
        docExport.Cleanup()

        If (bReenable) Then
            EnableFontSmoothing()
            bReenable = False
        End If
    End Sub

    Function GetFontSmoothing() As Boolean
        Dim iResults As Boolean
        Dim pv As Integer

        'get font smoothing value and return true if font smoothing is turned on.
        iResults = SystemParametersInfo(SPI_GETFONTSMOOTHING, 0, pv, 0)

        If pv > 0 Then
            GetFontSmoothing = True
        Else
            GetFontSmoothing = False
        End If
    End Function

    Sub EnableFontSmoothing()
        Dim iResults As Boolean
        Dim pv As Integer

        ' Call systemparametersinfo to turn on fontsmoothing
        iResults = SystemParametersInfo(SPI_SETFONTSMOOTHING, 1, pv, SPIF_UPDATEINIFILE)

    End Sub

    Sub DisableFontSmoothing()
        Dim iResults As Boolean
        Dim pv As Integer

        ' Call systemparametersinfo to turn off font smoothing 
        iResults = SystemParametersInfo(SPI_SETFONTSMOOTHING, 0, pv, SPIF_UPDATEINIFILE)

    End Sub
End Class
