/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using System.Collections;
using System.Windows.Forms;

namespace PrintMultiPagesCS_NET_Addin
{
  public class PrintMultiPagesCS_NET_Addin : ESRI.ArcGIS.Desktop.AddIns.Button
  {

    /* GDI callback to GetDeviceCaps and CreateDC function */
    [DllImport("GDI32.dll")]
    public static extern int GetDeviceCaps(int hdc, int nIndex);

    [DllImport("GDI32.dll")]
    public static extern int CreateDC(string strDriver, string strDevice, string strOutput, IntPtr pData);

    [DllImport("User32.dll")]
    public static extern int ReleaseDC(int hWnd, int hDC);

    public PrintMultiPagesCS_NET_Addin()
    {
    }

    protected override void OnClick()
    {
      PrintMultiPageParameterized(3);
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    public void PrintMultiPageParameterized(long iResampleRatio)
    {

      /* Prints tiled map by using IPrinterMPage. */
        
      IActiveView docActiveView = ArcMap.Document.ActiveView;
      IPrinter docPrinter;
      long iPrevOutputImageQuality;
      IOutputRasterSettings docOutputRasterSettings;
      tagRECT deviceRECT;
      IPaper docPaper;
      /* printdocument is from the .NET assembly system.drawing.printing */
      System.Drawing.Printing.PrintDocument sysPrintDocumentDocument;


      short iNumPages;
      IEnvelope docPrinterBounds;
      IEnvelope VisibleBounds;


      docPrinterBounds = new EnvelopeClass();
      VisibleBounds = new EnvelopeClass();

      // save the previous output image quality, so that when the export is complete it will be set back.
      docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
      iPrevOutputImageQuality = docOutputRasterSettings.ResampleRatio;

      SetOutputQuality(docActiveView, iResampleRatio);


      /* Now we need to get the default printer name.  Since this is a generic command,
       * we can't use the printername property of the document.  So instead, we use the 
       * System.Drawing.Printing objects to find the default printer.
       */
      docPrinter = new EmfPrinterClass();
      sysPrintDocumentDocument = new System.Drawing.Printing.PrintDocument();
      docPaper = new PaperClass();

      /* testing to see if printer instantiated in sysPrintDocumentDocument is the 
       * default printer.  It SHOULD be, but this is just a reality check.
       */
      bool isDefault = sysPrintDocumentDocument.PrinterSettings.IsDefaultPrinter;

      if (isDefault)
      {
        //Set docPaper's printername to the printername of the default printer
        docPaper.PrinterName = sysPrintDocumentDocument.PrinterSettings.PrinterName;

      }
      else
      {
        //if we get an unexpected result, return.
        MessageBox.Show("Error getting default printer info, exiting...");
        return;
      }


      //make sure the paper orientation is set to the orientation matching the current view.
      docPaper.Orientation = ArcMap.Document.PageLayout.Page.Orientation;

      /* Now assign docPrinter the paper and with it the printername.  This process is two steps
       * because you cannot change an IPrinter's printer except by passing it as a part of 
       * the IPaper.  That's why we setup docPrinter.Paper.PrinterName first.
       */
      docPrinter.Paper = docPaper;

      //set the spoolfilename (this is the job name that shows up in the print queue)
      docPrinter.SpoolFileName = "PrintActiveViewSample";

      // Get the printer's hDC, so we can use the Win32 GetDeviceCaps function to
      //  get Printer's Physical Printable Area x and y margins
      int hInfoDC;
      hInfoDC = CreateDC(docPrinter.DriverName, docPrinter.Paper.PrinterName, "", IntPtr.Zero);

      // Find out how many printer pages the output will cover. 
      if (ArcMap.Document.ActiveView is IPageLayout)
      {
        ArcMap.Document.PageLayout.Page.PrinterPageCount(docPrinter, 0, out iNumPages);
      }
      else
      {
        iNumPages = 1;
      }
      
      if (iNumPages < 2)
      {
        MessageBox.Show("Sample requires map in Layout View and map to be tiled to printer paper");
        return;
      }

      IPrinterMPage PrintMPage;
      int intPageHandle;
      
      PrintMPage = docPrinter as IPrinterMPage;

      // Code inside StartMapDocument <-----> EndMapDocument is hard coded to print the first two pages of the tiled print
      // Printer and Page Bounds need to be calculated for each corresponding tile that gets printed

      PrintMPage.StartMapDocument();  

          //calculate printer bounds for the first page
          ArcMap.Document.PageLayout.Page.GetDeviceBounds(docPrinter, 1, 0, docPrinter.Resolution, docPrinterBounds); // get device bounds of the first page

          //Transfer PrinterBounds envelope, offsetting by PHYSICALOFFSETX
          // the Win32 constant for PHYSICALOFFSETX is 112
          // the Win32 constant for PHYSICALOFFSETY is 113
          deviceRECT.bottom = (int)(docPrinterBounds.YMax - GetDeviceCaps(hInfoDC, 113));
          deviceRECT.left = (int)(docPrinterBounds.XMin - GetDeviceCaps(hInfoDC, 112));
          deviceRECT.right = (int)(docPrinterBounds.XMax - GetDeviceCaps(hInfoDC, 112));
          deviceRECT.top = (int)(docPrinterBounds.YMin - GetDeviceCaps(hInfoDC, 113));

          // Transfer offsetted PrinterBounds envelope back to the deviceRECT
          docPrinterBounds.PutCoords(0, 0, deviceRECT.right - deviceRECT.left, deviceRECT.bottom - deviceRECT.top);

          if (ArcMap.Document.ActiveView is IPageLayout)
          {
            //get the visible bounds for this layout, based on the current page number.
            ArcMap.Document.PageLayout.Page.GetPageBounds(docPrinter, 1, 0, VisibleBounds); // get visible bounds of the first page
          }
          else
          {
            MessageBox.Show("Please Use Map Layout View for this Sample");
            return;
          }

          // start printing the first page bracket, returns handle of the current page.  
          // handle is then passed to the ActiveView.Output()
          intPageHandle = PrintMPage.StartPage(docPrinterBounds, hInfoDC);
                
                ArcMap.Document.ActiveView.Output(intPageHandle, docPrinter.Resolution, ref deviceRECT, VisibleBounds, null);
          
          PrintMPage.EndPage(); //end printing the first page bracket

          
          // calculate printer bounds for the second page
          ArcMap.Document.PageLayout.Page.GetDeviceBounds(docPrinter, 2, 0, docPrinter.Resolution, docPrinterBounds); // get device bounds of the first page
          
          //Transfer PrinterBounds envelope, offsetting by PHYSICALOFFSETX
          // the Win32 constant for PHYSICALOFFSETX is 112
          // the Win32 constant for PHYSICALOFFSETY is 113
          deviceRECT.bottom = (int)(docPrinterBounds.YMax - GetDeviceCaps(hInfoDC, 113));
          deviceRECT.left = (int)(docPrinterBounds.XMin - GetDeviceCaps(hInfoDC, 112));
          deviceRECT.right = (int)(docPrinterBounds.XMax - GetDeviceCaps(hInfoDC, 112));
          deviceRECT.top = (int)(docPrinterBounds.YMin - GetDeviceCaps(hInfoDC, 113));

          // Transfer offsetted PrinterBounds envelope back to the deviceRECT
          docPrinterBounds.PutCoords(0, 0, deviceRECT.right - deviceRECT.left, deviceRECT.bottom - deviceRECT.top);

          if (ArcMap.Document.ActiveView is IPageLayout)
          {
            //get the visible bounds for this layout, based on the current page number.
            ArcMap.Document.PageLayout.Page.GetPageBounds(docPrinter, 2, 0, VisibleBounds); // get visible bounds of the first page
          }
          else
          {
            MessageBox.Show("Please Use Map Layout View for this Sample");
            return;
          }

          // start printing the second page bracket, returns handle of the current page.  
          // handle is then passed to the ActiveView.Output()
          intPageHandle = PrintMPage.StartPage(VisibleBounds, hInfoDC);
              
              ArcMap.Document.ActiveView.Output(intPageHandle, docPrinter.Resolution, ref deviceRECT, VisibleBounds, null);

              PrintMPage.EndPage(); //end printing the second page bracket

       

      PrintMPage.EndMapDocument();


      //now set the output quality back to the previous output quality.
      SetOutputQuality(docActiveView, iPrevOutputImageQuality);

      //release the DC...
      ReleaseDC(0, hInfoDC);
    }

    private void SetOutputQuality(IActiveView docActiveView, long iResampleRatio)
    {
      /* This function sets OutputImageQuality for the active view.  If the active view is a pagelayout, then
       * it must also set the output image quality for EACH of the Maps in the pagelayout.
       */
      IGraphicsContainer docGraphicsContainer;
      IElement docElement;
      IOutputRasterSettings docOutputRasterSettings;
      IMapFrame docMapFrame;
      IActiveView tmpActiveView;

      if (docActiveView is IMap)
      {
        docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
        docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
      }
      else if (docActiveView is IPageLayout)
      {
        //assign ResampleRatio for PageLayout
        docOutputRasterSettings = docActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
        docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
        //and assign ResampleRatio to the Maps in the PageLayout
        docGraphicsContainer = docActiveView as IGraphicsContainer;
        docGraphicsContainer.Reset();

        docElement = docGraphicsContainer.Next();
        while (docElement != null)
        {
          if (docElement is IMapFrame)
          {
            docMapFrame = docElement as IMapFrame;
            tmpActiveView = docMapFrame.Map as IActiveView;
            docOutputRasterSettings = tmpActiveView.ScreenDisplay.DisplayTransformation as IOutputRasterSettings;
            docOutputRasterSettings.ResampleRatio = (int)iResampleRatio;
          }
          docElement = docGraphicsContainer.Next();
        }

        docMapFrame = null;
        docGraphicsContainer = null;
        tmpActiveView = null;
      }
      docOutputRasterSettings = null;

    }

  }

}
