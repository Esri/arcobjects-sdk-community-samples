/*

   Copyright 2019 Esri

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
using System.Collections.Generic;
using System.Text;
using System.IO;
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

namespace PrintActiveViewCS_Net_Addin
{

  /// <summary>
  /// PrintActiveViewCS_Net creates a command which will print the active view to the default printer's default page.
  /// </summary>
  public class PrintActiveViewCS_Net_Addin : ESRI.ArcGIS.Desktop.AddIns.Button
  {

    public PrintActiveViewCS_Net_Addin()
    {
    }

    protected override void OnClick()
    {
      PrintActiveViewParameterized(3);
    }
    protected override void OnUpdate()
    {
			Enabled = ArcMap.Application != null;
    }

    public void PrintActiveViewParameterized(int iResampleRatio)
    {
    
      /* Prints the Active View of the document to selected output format. */
      //          
			IActiveView docActiveView = ArcMap.Document.ActiveView;
      IPrinter docPrinter;
      IPrintAndExport PrintAndExport = new PrintAndExportClass();
      IPaper docPaper;
      /* printdocument is from the .NET assembly system.drawing.printing */
      System.Drawing.Printing.PrintDocument sysPrintDocumentDocument;
      short iNumPages;

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

      // Find out how many printer pages the output will cover. 
			if (ArcMap.Document.ActiveView is IPageLayout)
      {
				ArcMap.Document.PageLayout.Page.PrinterPageCount (docPrinter, 0, out iNumPages);
      }
      else
      {
        iNumPages = 1;
      }

      for (short lCurrentPageNum = 1; lCurrentPageNum <= iNumPages; lCurrentPageNum++)
      {
          try
          {
              PrintAndExport.Print(ArcMap.Document.ActiveView, docPrinter, ArcMap.Document.PageLayout.Page, lCurrentPageNum, iResampleRatio, null);
          }
          catch
          {
              //need to catch exceptions in PrintAndExport.Print - for instance if the job is cancelled.
              MessageBox.Show("An error has occurred.");
          }
      }
       
    }
  }
  
  
}
