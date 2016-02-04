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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;

namespace ExportActiveViewCS_Net_Addin
{

  /// <summary>
  /// This class creates a command that will export the active view to any supported format.
  /// </summary>
  public class ExportActiveViewCS_Net : ESRI.ArcGIS.Desktop.AddIns.Button
  {

    /* GDI delegate to SystemParametersInfo function */
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);

    /* constants used for user32 calls */
    const uint SPI_GETFONTSMOOTHING = 74;
    const uint SPI_SETFONTSMOOTHING = 75;
    const uint SPIF_UPDATEINIFILE = 0x1;
  
    public ExportActiveViewCS_Net()
    {
      /* Class constructor */
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    protected override void OnClick()
    {
    
      /* The OnClick method calls the ExportActiveViewParameterized function with some parameters
       * which you can of course change.  The first parameter is resolution in dpi, the second is the resample ratio 
       * from 1(best quality) to 5 (fastest).  the third is a string which represents which export type you want to 
       * output (JPEG, PDF, etc.), the fourth is the directory to which you'd like to write, and the last is a 
       * boolean which determines whether or not the output will be clipped to graphics extent (for layouts).
       */
      string exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      ExportActiveViewParameterized(300, 1, "JPEG", exportFolder + "\\", false);
    }
    
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }
    
    #endregion

    private void ExportActiveViewParameterized(long iOutputResolution, long lResampleRatio, string ExportType, string sOutputDir, Boolean bClipToGraphicsExtent)
    {
    
      /* EXPORT PARAMETER: (iOutputResolution) the resolution requested.
       * EXPORT PARAMETER: (lResampleRatio) Output Image Quality of the export.  The value here will only be used if the export
       * object is a format that allows setting of Output Image Quality, i.e. a vector exporter.
       * The value assigned to ResampleRatio should be in the range 1 to 5.
       * 1 corresponds to "Best", 5 corresponds to "Fast"
       * EXPORT PARAMETER: (ExportType) a string which contains the export type to create.
       * EXPORT PARAMETER: (sOutputDir) a string which contains the directory to output to.
       * EXPORT PARAMETER: (bClipToGraphicsExtent) Assign True or False to determine if export image will be clipped to the graphic 
       * extent of layout elements.  This value is ignored for data view exports
       */

      /* Exports the Active View of the document to selected output format. */
      
                                  // using predefined static member
      IActiveView docActiveView = ArcMap.Document.ActiveView;
      IExport docExport;  
      IPrintAndExport docPrintExport;
      IOutputRasterSettings RasterSettings;    
      string sNameRoot;
      bool bReenable = false;

      if (GetFontSmoothing())
      {
        /* font smoothing is on, disable it and set the flag to reenable it later. */
        bReenable = true;
        DisableFontSmoothing();
        if (GetFontSmoothing())
        {
          //font smoothing is NOT successfully disabled, error out.
          return;
        }
        //else font smoothing was successfully disabled.
      }

      // The Export*Class() type initializes a new export class of the desired type.
      if (ExportType == "PDF")
      {
        docExport = new ExportPDFClass();
      }
      else if (ExportType == "EPS")
      {
        docExport = new ExportPSClass();
      }
      else if (ExportType == "AI")
      {
        docExport = new ExportAIClass();
      }
      else if (ExportType == "BMP")
      {

        docExport = new ExportBMPClass();
      }
      else if (ExportType == "TIFF")
      {
        docExport = new ExportTIFFClass();
      }
      else if (ExportType == "SVG")
      {
        docExport = new ExportSVGClass();
      }
      else if (ExportType == "PNG")
      {
        docExport = new ExportPNGClass();
      }
      else if (ExportType == "GIF")
      {
        docExport = new ExportGIFClass();
      }
      else if (ExportType == "EMF")
      {
        docExport = new ExportEMFClass();
      }
      else if (ExportType == "JPEG")
      {
        docExport = new ExportJPEGClass();
      }
      else
      {
        MessageBox.Show("Unsupported export type " + ExportType + ", defaulting to EMF.");
        ExportType = "EMF";
        docExport = new ExportEMFClass();
      }

      docPrintExport = new PrintAndExportClass();
     
      //set the name root for the export
      sNameRoot = "ExportActiveViewSampleOutput";

      //set the export filename (which is the nameroot + the appropriate file extension)
      docExport.ExportFileName = sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0];

      //Output Image Quality of the export.  The value here will only be used if the export
      // object is a format that allows setting of Output Image Quality, i.e. a vector exporter.
      // The value assigned to ResampleRatio should be in the range 1 to 5.
      // 1 corresponds to "Best", 5 corresponds to "Fast"

      // check if export is vector or raster
      if (docExport is IOutputRasterSettings)
      {
        // for vector formats, assign the desired ResampleRatio to control drawing of raster layers at export time   
        RasterSettings = (IOutputRasterSettings)docExport;
        RasterSettings.ResampleRatio = (int)lResampleRatio;
        
        // NOTE: for raster formats output quality of the DISPLAY is set to 1 for image export 
        // formats by default which is what should be used
      }
      
      docPrintExport.Export(docActiveView, docExport, iOutputResolution, bClipToGraphicsExtent, null);
  
      MessageBox.Show("Finished exporting " + sOutputDir + sNameRoot + "." + docExport.Filter.Split('.')[1].Split('|')[0].Split(')')[0] + ".", "Export Active View Sample");

      if (bReenable)
      {
        /* reenable font smoothing if we disabled it before */
        EnableFontSmoothing();
        bReenable = false;
        if (!GetFontSmoothing())
        {
          //error: cannot reenable font smoothing.
          MessageBox.Show("Unable to reenable Font Smoothing", "Font Smoothing error");
        }
      }
    }

    private void DisableFontSmoothing()
    {
      bool iResult;
      int pv = 0;

      /* call to systemparametersinfo to set the font smoothing value */
      iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 0, ref pv, SPIF_UPDATEINIFILE);
    }

    private void EnableFontSmoothing()
    {
      bool iResult;
      int pv = 0;

      /* call to systemparametersinfo to set the font smoothing value */
      iResult = SystemParametersInfo(SPI_SETFONTSMOOTHING, 1, ref pv, SPIF_UPDATEINIFILE);

    }

    private Boolean GetFontSmoothing()
    {
      bool iResult;
      int pv = 0;

      /* call to systemparametersinfo to get the font smoothing value */
      iResult = SystemParametersInfo(SPI_GETFONTSMOOTHING, 0, ref pv, 0);

      if (pv > 0)
      {
        //pv > 0 means font smoothing is ON.
        return true;
      }
      else
      {
        //pv == 0 means font smoothing is OFF.
        return false;
      }

    }
  }

}
