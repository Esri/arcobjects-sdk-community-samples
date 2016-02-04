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
// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.2/userestrictions.txt.
// 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.OutputExtensions;
using System.Windows.Forms;

namespace PrintActiveViewArcPressCS_Net_Addin
{
  public class PrintActiveViewArcPressCS_Net_Addin : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    public PrintActiveViewArcPressCS_Net_Addin()
    {
    }

    protected override void OnClick()
    {
      PrintActiveViewArcPressParameterized(3);
    }
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    public void PrintActiveViewArcPressParameterized(int iResampleRatio)
    {

      /* Prints the Active View of the document with ArcPress */
      IPrintAndExport PrintAndExport = new PrintAndExportClass();
      IActiveView docActiveView = ArcMap.Document.ActiveView;
      IPrinter docPrinter;
      IArcPressPrinter docArcPressPrinter;
      string sNameRoot;
      short iNumPages;

      // assign the output path and filename.  We can use the Filter property of the export object to
      // automatically assign the proper extension to the file.
      sNameRoot = ArcMap.Application.Document.Title.Substring(0, ArcMap.Application.Document.Title.Length - 4);

      //create new ArcPress printer classes
      docPrinter = new ArcPressPrinterClass();
      docArcPressPrinter = new ArcPressPrinterClass();

      try
      {
          //auto-select the driver based on the printer's name.
          docArcPressPrinter.AutoSelectDriverByName(ArcMap.ThisApplication.Paper.PrinterName);

          //if the printer cannot be auto-selected, exit.
          if (docArcPressPrinter.SelectedDriverId == null)
          {
              MessageBox.Show("Could not auto-select ArcPress driver for printer");
              return;
          }

          //transfer the ArcPress printer properties into docPrinter
          docPrinter = (IPrinter)docArcPressPrinter;

          //the paper should be the same as the application's paper.
          docPrinter.Paper = ArcMap.ThisApplication.Paper;

          //make sure the paper orientation is set to the orientation matching the current view.
          docPrinter.Paper.Orientation = ArcMap.Document.PageLayout.Page.Orientation;

          //set the spool filename (this is the job name that shows up in the print queue)
          docPrinter.SpoolFileName = sNameRoot;

          // Find out how many printer pages the output will cover.  iNumPages will always be 1 
          // unless the user explicitly sets the tiling options in the file->Print dialog.  
          if (ArcMap.Document.ActiveView is IPageLayout)
          {
              ArcMap.Document.PageLayout.Page.PrinterPageCount(docPrinter, 0, out iNumPages);
          }
          else
          {
              iNumPages = 1;
          }


          for (short lCurrentPageNum = 1; lCurrentPageNum <= iNumPages; lCurrentPageNum++)
          {

              //the StepProgressor is what creates the bar graph of the print's progress.
              docPrinter.StepProgressor = ArcMap.Application.StatusBar.ProgressBar;
              try
              {
                  PrintAndExport.Print(ArcMap.Document.ActiveView, docPrinter, ArcMap.Document.PageLayout.Page, lCurrentPageNum, iResampleRatio, null);
              }
              catch
              {
                  MessageBox.Show("Error printing page " + lCurrentPageNum);
              }

          }

      }
      catch 
      {
          MessageBox.Show("Could not auto-select ArcPress driver for this printer");
      }
      
    }

  }

}
