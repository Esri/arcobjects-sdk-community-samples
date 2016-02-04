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
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlgorithmicColorRamp
{
  [System.Runtime.InteropServices.ProgId("AlgorithmicColorRamp.clsAlgColorRamp")]
  public class AlgorithmicColorRamp : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    private frmAlgorithmicColorRamp frmAlgoColorRamp = new frmAlgorithmicColorRamp();
  
    public AlgorithmicColorRamp()
    {
    }

    protected override void OnClick()
    {
      //
      // When the utility is selected, check that we have a currently selected
      // feature layer with a ClassBreaksRenderer already set. First we get the contents view.
      //
      IContentsView ContentsView = null;
      ContentsView = ArcMap.Document.CurrentContentsView;
      //
      // If we have a DisplayView active
      //
      object VarSelectedItem = null;
      IGeoFeatureLayer GeoFeatureLayer = null;
      IClassBreaksRenderer ClassBreaksRenderer = null;
      IEnumColors pColors = null;
      int lngCount = 0;
      IHsvColor HsvColor = null;
      IClone ClonedSymbol = null;
      ISymbol NewSymbol = null;
      IActiveView ActiveView = null; //AlgorithimcColorRamp contains HSV colors.
      if (ContentsView is TOCDisplayView)
      {
        if (ContentsView.SelectedItem is DBNull)
        {
          //
          // If we don't have anything selected.
          //
          MessageBox.Show("SelectedItem is Null C#." + "Select a layer in the Table of Contents.", "No Layer Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }
        //
        // Get the selected Item.
        //
        VarSelectedItem = ContentsView.SelectedItem;
        //
        // Selected Item should implement the IGeoFeatureLayer interface - therefore we
        // have selected a feature layer with a Renderer property (Note: Other interfaces
        // also have a Renderer property, which may behave differently.
        //
        if (VarSelectedItem is IGeoFeatureLayer)
        {
          GeoFeatureLayer = (IGeoFeatureLayer)VarSelectedItem;
          //
          // Set the cached property to true, so we can refresh this layer
          // without refreshing all the layers, when we have changed the symbols.
          //
          GeoFeatureLayer.Cached = true;
          //
          // Check we have an existing ClassBreaksRenderer.
          //
          if (GeoFeatureLayer.Renderer is IClassBreaksRenderer)
          {
            ClassBreaksRenderer = (IClassBreaksRenderer)GeoFeatureLayer.Renderer;
            //
            // If successful so far we can go ahead and open the Form. This allows the
            // user to change the properties of the new RandomColorRamp.
            //
            frmAlgoColorRamp.m_lngClasses = ClassBreaksRenderer.BreakCount;
            frmAlgoColorRamp.ShowDialog();
            //
            // Return the selected colors enumeration.
            pColors = frmAlgoColorRamp.m_enumNewColors;
            if (pColors == null)
            {
              //
              // User has cancelled the form, or not set a ramp.
              //
              //MsgBox("Colors object is empty. Exit Sub")

              return;
            }
            //
            // Set the new random colors onto the Symbol array of the ClassBreaksRenderer.
            //
            pColors.Reset(); // Because you never know if the enumeration has been
            // iterated before being passed back.

            int tempFor1 = ClassBreaksRenderer.BreakCount;
            for (lngCount = 0; lngCount < tempFor1; lngCount++)
            {
              //
              // For each Value in the ClassBreaksRenderer, we clone the existing
              // Fill symbol (so that all the properties are faithful preserved,
              // and set its color from our new AlgorithmicColorRamp.
              //
              IClone symClone;
              symClone = (IClone)ClassBreaksRenderer.get_Symbol(lngCount);

              ClonedSymbol = CloneMe(ref (symClone));
              //
              // Now the ClonedSymbol variable holds a copy of the existing
              // Symbol, we can change the assigned Color. We set the new
              // symbol onto the Symbol array of the Renderer.          '
              //
              HsvColor = (IHsvColor)pColors.Next();
              NewSymbol = SetColorOfUnknownSymbol(ClonedSymbol, HsvColor);
              if (NewSymbol != null)
                ClassBreaksRenderer.set_Symbol(lngCount, NewSymbol);
            }
            //
            // Refresh the table of contents and the changed layer.
            //
            ActiveView = (IActiveView)ArcMap.Document.FocusMap;
            ActiveView.ContentsChanged();
            ArcMap.Document.UpdateContents();
            ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, GeoFeatureLayer, null);
          }
        }
      }
    }

    public IClone CloneMe(ref IClone OriginalClone)
    {
      IClone tempCloneMe = null;
      //
      // This function clones the input object.
      //
      tempCloneMe = null;
      if (OriginalClone != null)
        tempCloneMe = OriginalClone.Clone();
      return tempCloneMe;
    }

    private ISymbol SetColorOfUnknownSymbol(IClone ClonedSymbol, IColor Color)
    {
      ISymbol tempSetColorOfUnknownSymbol = null;
      //
      // This function takes an IClone interface, works out the underlying coclass
      // (which should be some kind of symbol) and then sets the Color property
      // according to the passed in color.
      //
      tempSetColorOfUnknownSymbol = null;
      if (ClonedSymbol == null)
        return tempSetColorOfUnknownSymbol;
      //
      // Here we figure out which kind of symbol we have. For the simple symbol
      // types, simply setting the color property is OK. However, more complex
      // symbol types require further investigation.
      //
      IFillSymbol FillSymbol = null;
      IMarkerFillSymbol MarkerFillSymbol = null;
      IMarkerSymbol MarkerSymbol_A = null;
      ILineFillSymbol LineFillSymbol = null;
      ILineSymbol LineSymbol = null;
      IPictureFillSymbol PictureFillSymbol = null;
      IMarkerSymbol MarkerSymbol_B = null;
      IPictureMarkerSymbol PictureMarkerSymbol = null;
      IMarkerLineSymbol MarkerLineSymbol = null;
      IMarkerSymbol MarkerSymbol_C = null;
      ILineSymbol LineSymbol_B = null;
      if (ClonedSymbol is ISymbol)
      {
        //
        // Check for Fill symbols.
        //
        if (ClonedSymbol is IFillSymbol)
        {
          //
          // Check for SimpleFillSymbols or MultiLevelFillSymbols.
          //
          if ((ClonedSymbol is ISimpleFillSymbol) | (ClonedSymbol is IMultiLayerFillSymbol))
          {
            FillSymbol = (IFillSymbol)ClonedSymbol;
            //
            // Here we simply change the color of the Fill.
            //
            FillSymbol.Color = Color;
            tempSetColorOfUnknownSymbol = (ISymbol)FillSymbol;
            //
            // Check for MarkerFillSymbols.
            //
          }
          else if (ClonedSymbol is IMarkerFillSymbol)
          {
            MarkerFillSymbol = (IMarkerFillSymbol)ClonedSymbol;
            //
            // Here we change the color of the MarkerSymbol.
            //
            MarkerSymbol_A = (IMarkerSymbol)SetColorOfUnknownSymbol((IClone)MarkerFillSymbol.MarkerSymbol, Color);
            MarkerFillSymbol.MarkerSymbol = MarkerSymbol_A;
            tempSetColorOfUnknownSymbol = (ISymbol)MarkerFillSymbol;
            //
            // Check for LineFillSymbols.
            //
          }
          else if (ClonedSymbol is ILineFillSymbol)
          {
            LineFillSymbol = (ILineFillSymbol)ClonedSymbol;
            //
            // Here we change the color of the LineSymbol.
            //
            LineSymbol = (ILineSymbol)SetColorOfUnknownSymbol((IClone)LineFillSymbol.LineSymbol, Color);
            LineFillSymbol.LineSymbol = LineSymbol;
            tempSetColorOfUnknownSymbol = (ISymbol)LineFillSymbol;
            //
            // Check for PictureFillSymbols.
            //
          }
          else if (ClonedSymbol is IPictureFillSymbol)
          {
            PictureFillSymbol = (IPictureFillSymbol)ClonedSymbol;
            //
            // Here we simply change the color of the BackgroundColor.
            //
            PictureFillSymbol.BackgroundColor = Color;
            tempSetColorOfUnknownSymbol = (ISymbol)PictureFillSymbol;
          }
          //
          // Check for Marker symbols.
          //
        }
        else if (ClonedSymbol is IMarkerSymbol)
        {
          //
          // Check for SimpleMarkerSymbols, ArrowMarkerSymbols or
          // CharacterMarkerSymbols.
          //
          if ((ClonedSymbol is IMultiLayerMarkerSymbol) | (ClonedSymbol is ISimpleMarkerSymbol) | (ClonedSymbol is IArrowMarkerSymbol) | (ClonedSymbol is ICharacterMarkerSymbol))
          {
            MarkerSymbol_B = (IMarkerSymbol)ClonedSymbol;
            //
            // For these types, we simply change the color property.
            //
            MarkerSymbol_B.Color = Color;
            tempSetColorOfUnknownSymbol = (ISymbol)MarkerSymbol_B;
            //
            // Check for PictureMarkerSymbols.
            //
          }
          else if (ClonedSymbol is IPictureMarkerSymbol)
          {
            PictureMarkerSymbol = (IPictureMarkerSymbol)ClonedSymbol;
            //
            // Here we change the BackgroundColor property.
            //
            PictureMarkerSymbol.Color = Color;
            tempSetColorOfUnknownSymbol = (ISymbol)PictureMarkerSymbol;
          }
          //
          // Check for Line symbols.
          //
        }
        else if (ClonedSymbol is ILineSymbol)
        {
          //
          // Check for MarkerLine symbols.
          //
          if (ClonedSymbol is IMarkerLineSymbol)
          {
            MarkerLineSymbol = (IMarkerLineSymbol)ClonedSymbol;
            //
            // Here we change the color of the MarkerSymbol.
            //
            MarkerSymbol_C = (IMarkerSymbol)SetColorOfUnknownSymbol((IClone)MarkerLineSymbol.MarkerSymbol, Color);
            MarkerLineSymbol.MarkerSymbol = MarkerSymbol_C;
            tempSetColorOfUnknownSymbol = (ISymbol)MarkerLineSymbol;
            //
            // Check for other Line symbols.
            //
          }
          else if ((ClonedSymbol is ISimpleLineSymbol) | (ClonedSymbol is IHashLineSymbol) | (ClonedSymbol is ICartographicLineSymbol))
          {
            LineSymbol_B = (ILineSymbol)ClonedSymbol;
            LineSymbol_B.Color = Color;
            tempSetColorOfUnknownSymbol = (ISymbol)LineSymbol_B;
          }
        }
      }

      return tempSetColorOfUnknownSymbol;
    }
    
    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }
  }

}
