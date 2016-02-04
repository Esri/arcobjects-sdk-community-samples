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
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MultivariateRenderers
{
	public enum EColorCombinationType: int
	{
		enuComponents,
		enuCIELabColorRamp,
		enuLabLChColorRamp,
		enuRGBAverage,
		enuCIELabMatrix
	}

    [Guid("6A921DB3-5D31-4D85-9857-687CEDBC0D29")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [ProgId("MultiVariateRenderers.MultiVariateRendererCS")]
	public class MultivariateRenderer : ExportSupport, IExportSupport, IFeatureRenderer, IMultivariateRenderer, ILegendInfo, IPersistVariant, IRotationRenderer, ITransparencyRenderer
	{

		// class definition for MultivariateRenderer, a custom multivariate feature renderer
		//   consisting of 

		// data members
		private EColorCombinationType m_eColorCombinationMethod = EColorCombinationType.enuComponents;
		private IFeatureRenderer m_pShapePatternRend;
		private IFeatureRenderer m_pColorRend1;
		private IFeatureRenderer m_pColorRend2;
		private IFeatureRenderer m_pSizeRend;
		// for the renderer's TOC and legend entry 
		// current implementation is simple, but could be extended
		private ILegendGroup[] m_pLegendGroups;
		private string m_sRotationField;
        
		private esriSymbolRotationType m_eRotationType = esriSymbolRotationType.esriRotateSymbolGeographic;
		private string m_sTransparencyField;
		private IFeatureRenderer m_pMainRend; // as renderers are assigned, use this to keep track of which one has the base symbols
		private esriGeometryType m_ShapeType;
		private IFeatureClass m_pFeatureClass;
		private IQueryFilter m_pQueryFilter;
		private long[,] m_OLEColorMatrix = new long[4,4];

		public MultivariateRenderer()
		{
      
		}

		~MultivariateRenderer()
		{
			m_pShapePatternRend = null;
			m_pColorRend1 = null;
			m_pColorRend2 = null;
		}

		public void CreateLegend()
		{
			// NOT IMPL

		}

		public bool CanRender(IFeatureClass featClass, IDisplay Display)
		{
			// only use this renderer if we have points, lines, or polygons
			return (featClass.ShapeType == esriGeometryType.esriGeometryPoint) | (featClass.ShapeType == esriGeometryType.esriGeometryPolyline) | (featClass.ShapeType == esriGeometryType.esriGeometryPolygon);
		}

		public void Draw(IFeatureCursor cursor, esriDrawPhase DrawPhase, IDisplay Display, ITrackCancel trackCancel)
		{
			string ActiveErrorHandler = null;

	try
	{
			// loop through and draw each feature

			IFeature pFeat = null;
			IFeatureRenderer pRend = null;

			bool bContinue = false;

			// do not draw features if no display
			if (Display == null)
				return;

			// we can't draw without somewhere to get our base symbols from
			if (m_pMainRend == null)
				return;

			if (m_pSizeRend != null)
			{
				// size varies
				if (m_ShapeType == esriGeometryType.esriGeometryPoint | m_ShapeType == esriGeometryType.esriGeometryPolyline)
				{
					if (DrawPhase == esriDrawPhase.esriDPGeography)
					{
						// draw symbols in order from large to small
						DrawSymbolsInOrder(cursor, DrawPhase, Display, trackCancel);
					}
				}
				else if (m_ShapeType == esriGeometryType.esriGeometryPolygon)
				{
					if (DrawPhase == esriDrawPhase.esriDPAnnotation)
					{
						// draw primary symbology from large to small
						DrawSymbolsInOrder(cursor, DrawPhase, Display, trackCancel);
					}
					else if (DrawPhase == esriDrawPhase.esriDPGeography)
					{
						// draw background symbology
						pFeat = cursor.NextFeature();
						bContinue = true;

						// while there are still more features and drawing has not been cancelled
                        IFillSymbol pBackFillSym;

                        
						while ((pFeat != null) & (bContinue == true))
						{
							// draw the feature
                            IFeatureDraw pFeatDraw = pFeat as IFeatureDraw;
							if (m_pSizeRend is IClassBreaksRenderer)
							{
                                IClassBreaksRenderer pCBRend = m_pSizeRend as IClassBreaksRenderer;
								pBackFillSym = pCBRend.BackgroundSymbol;
							}
							else
							{
                                IProportionalSymbolRenderer pPropRend = m_pSizeRend as IProportionalSymbolRenderer;
								pBackFillSym = pPropRend.BackgroundSymbol;
							}
							Display.SetSymbol(pBackFillSym as ISymbol);

              //implementation of IExportSupport
              BeginFeature(pFeat, Display);
              
							pFeatDraw.Draw(DrawPhase, Display, pBackFillSym as ISymbol, true, null, esriDrawStyle.esriDSNormal);

              //implementation of IExportSupport
							GenerateExportInfo(pFeat, Display);
							EndFeature(Display);
							
							pFeat = cursor.NextFeature();
              if (trackCancel != null)
                bContinue = trackCancel.Continue();
						}
					}
					else
					{
                        Marshal.ThrowExceptionForHR(147500037); //E_FAIL
					}
				}

			}
			else
			{
				// size does not vary
				if (DrawPhase != esriDrawPhase.esriDPGeography)
				{
                    Marshal.ThrowExceptionForHR(147500037); //E_FAIL
				}
				else
					DrawSymbols(cursor, DrawPhase, Display, trackCancel);
			}

}

catch
{
}
		}

		public IFeatureIDSet ExclusionSet
		{
			set
			{
				// NOT IMPL    
			}
		}

		public void PrepareFilter(IFeatureClass fc, IQueryFilter queryFilter)
		{
			// prepare filter for drawing

			// must add OID
			queryFilter.AddField(fc.OIDFieldName);

			m_ShapeType = fc.ShapeType;
			if (m_ShapeType == esriGeometryType.esriGeometryPoint)
			{
                if (m_sRotationField != null)
                {
                    if (m_sRotationField != "")
                    {
                        queryFilter.AddField(m_sRotationField);
                    }
                }
			}

			// save the fc and the query filter so that multiple cursors can be built in DrawSymbols
			m_pFeatureClass = fc;
			m_pQueryFilter = queryFilter;

			// prepare filters on constituent renderers so I can use SymbolByFeature in Draw
			if (m_pShapePatternRend != null)
					m_pShapePatternRend.PrepareFilter(fc, queryFilter);
			if (m_pColorRend1 != null)
					m_pColorRend1.PrepareFilter(fc, queryFilter);
			if (m_pColorRend2 != null)
					m_pColorRend2.PrepareFilter(fc, queryFilter);
			if (m_pSizeRend != null)
					m_pSizeRend.PrepareFilter(fc, queryFilter);

			// if we're combining colors from two (sequential) quantitative schemes, build color matrix now
			//   this gives flexibility to extend in future
			// in current impl. we determine combined color based on two colors, one from each constituent 
			//   ClassBreaksRenderer.  so, we could determine color on demand when drawing. but, by creating 
			//   the color matrix here and storing for later use, we leave open the possibility of swapping in 
			//   different logic for determining combined colors based on all known colors in each constituent
			//   renderer, not just the colors for the given feature
			if ((m_pColorRend1 != null) & (m_pColorRend2 != null))
			{
				if (! (m_eColorCombinationMethod == EColorCombinationType.enuComponents))
					BuildColorMatrix();
			}
			
			//implementation of IExportSupport
			AddExportFields(fc, queryFilter);

		}

    bool IFeatureRenderer.get_RenderPhase(ESRI.ArcGIS.esriSystem.esriDrawPhase DrawPhase)
        {
            return (DrawPhase == esriDrawPhase.esriDPGeography) | (DrawPhase == esriDrawPhase.esriDPAnnotation);
        }
    ESRI.ArcGIS.Display.ISymbol IFeatureRenderer.get_SymbolByFeature(ESRI.ArcGIS.Geodatabase.IFeature Feature)
        {
            return GetFeatureSymbol(Feature);
        }

    ESRI.ArcGIS.Carto.ILegendGroup ILegendInfo.get_LegendGroup(int Index)
        {
            string strHeading = null;
            ILegendInfo pLegendInfo = null;
            switch (Index)
            {
                case 0:
                    pLegendInfo = m_pMainRend as ILegendInfo;
                    if (m_pMainRend == m_pShapePatternRend)
                        strHeading = "Shape/Pattern: ";
                    else if (m_pMainRend == m_pSizeRend)
                        strHeading = "Size: ";
                    else
                        strHeading = "Color 1: ";
                    break;
                case 1:
                    if (m_pShapePatternRend != null)
                    {
                        if (m_pSizeRend != null)
                        {
                            pLegendInfo = m_pSizeRend as ILegendInfo;
                            strHeading = "Size: ";
                        }
                        else
                        {
                            pLegendInfo = m_pColorRend1 as ILegendInfo;
                            strHeading = "Color 1: ";
                        }
                    }
                    else
                    {
                        if (m_pSizeRend != null)
                        {
                            pLegendInfo = m_pColorRend1 as ILegendInfo;
                            strHeading = "Color 1: ";
                        }
                        else
                        {
                            pLegendInfo = m_pColorRend2 as ILegendInfo;
                            strHeading = "Color 2: ";
                        }
                    }
                    break;
                case 2:
                    pLegendInfo = m_pColorRend1 as ILegendInfo;
                    strHeading = "Color 1: ";
                    break;
                case 3:
                    pLegendInfo = m_pColorRend2 as ILegendInfo;
                    strHeading = "Color 2: ";

                    break;
            }

            ILegendGroup pLegendGroup = null;
            pLegendGroup = pLegendInfo.get_LegendGroup(0);
            //pLegendGroup.Heading = strHeading & pLegendGroup.Heading

            return pLegendGroup;
        }

		public int LegendGroupCount
		{
			get
			{
				ILegendInfo pLegInfo = null;
				int n = 0;

				n = 0;
				if (m_pSizeRend != null)
				{
					pLegInfo = m_pSizeRend as ILegendInfo;
					if (pLegInfo.get_LegendGroup(0) != null)
							n = n + 1;
				}
				if (m_pShapePatternRend != null)
				{
					pLegInfo = m_pShapePatternRend as ILegendInfo;
					if (pLegInfo.get_LegendGroup(0) != null)
							n = n + 1;
				}
				if (m_pColorRend1 != null)
				{
					pLegInfo = m_pColorRend1 as ILegendInfo;
					if (pLegInfo.get_LegendGroup(0) != null)
							n = n + 1;
				}
				if (m_pColorRend2 != null & ! (m_pColorRend2 == m_pColorRend1))
				{
					//If Not m_pColorRend2 Is Nothing Then
					pLegInfo = m_pColorRend2 as ILegendInfo;
					if (pLegInfo.get_LegendGroup(0) != null)
							n = n + 1;
				}

				return n;
			}
		}

		public ILegendItem LegendItem
		{
			get
			{
				return null;
			}
		}

		public bool SymbolsAreGraduated
		{
			get
			{
				return false;
			}
			set
			{
				// NOT IMPL
			}
		}

		public UID ID
		{
			get
			{
				UID pUID = new UID();
        pUID.Value = "MultivariateRenderers.MultiVariateRendererCS";
				//pUID.Value = ClassId
				return pUID;
			}
		}

		public void Load(IVariantStream Stream)
		{
			//load the persisted parameters of the renderer

			m_eColorCombinationMethod = (EColorCombinationType)Stream.Read();
			m_pShapePatternRend = Stream.Read() as IFeatureRenderer;
			m_pColorRend1 = Stream.Read() as IFeatureRenderer;
			m_pColorRend2 = Stream.Read() as IFeatureRenderer;
			m_pSizeRend = Stream.Read() as IFeatureRenderer;
			//m_pLegendGroups = = Stream.Read
			m_sRotationField = (string)Stream.Read();
            m_eRotationType = (esriSymbolRotationType)Stream.Read();
			m_sTransparencyField = (String)Stream.Read();
			m_pMainRend = Stream.Read() as IFeatureRenderer;

			//CreateLegend() ' not needed now
		}

		public void Save(IVariantStream Stream)
		{
			//persist the settings for the renderer

			Stream.Write(m_eColorCombinationMethod);
			Stream.Write(m_pShapePatternRend);
			Stream.Write(m_pColorRend1);
			Stream.Write(m_pColorRend2);
			Stream.Write(m_pSizeRend);
			//Stream.Write(m_pLegendGroups)
			Stream.Write(m_sRotationField);
			Stream.Write(m_eRotationType);
			Stream.Write(m_sTransparencyField);
			Stream.Write(m_pMainRend);
		}

		private ISymbol GetFeatureSymbol(IFeature pFeat)
		{

			ISymbol pSym = null;

			// get base symbol
			pSym = m_pMainRend.get_SymbolByFeature(pFeat);

			// modify base symbol as necessary

			if ((m_pSizeRend != null) && (! (m_pMainRend == m_pSizeRend)) && (pSym != null))
				pSym = ApplySize(pSym, pFeat);

			
			if (((m_pColorRend1 != null) | (m_pColorRend2 != null)) && (pSym != null))
				pSym = ApplyColor(pSym, pFeat);

			if (((m_ShapeType == esriGeometryType.esriGeometryPoint) | ((m_ShapeType == esriGeometryType.esriGeometryPolygon) & pSym is IMarkerSymbol)) && (pSym != null))
			{
                if (m_sRotationField != null)
                {
                    if ((m_sRotationField != "") && (m_sRotationField != null))
                    {
                        pSym = ApplyRotation(pSym as IMarkerSymbol, pFeat as IFeature) as ISymbol;
                    }
                }
			}

			// support for point, line, and poly features
			
            if (m_sTransparencyField != null)
            {
                if (m_sTransparencyField != "")
                {
                    pSym = ApplyTransparency(pSym);
                }
            }
			

			return pSym;

		}


		private IFeatureCursor SortData(IFeatureCursor pCursor, ITrackCancel pTrackCancel)
		{
			// sort in descending by value
			ITable pTable = null;
			pTable = m_pFeatureClass as ITable;

			ITableSort pTableSort = null;
			pTableSort = new TableSort();
			pTableSort.Table = pTable;
			pTableSort.Cursor = pCursor as ICursor;

			//set up the query filter.
			IQueryFilter pQF = null;
			pQF = new QueryFilter();
			pQF.SubFields = "*";
			pQF.WhereClause = m_pQueryFilter.WhereClause;
			pTableSort.QueryFilter = pQF;

			IProportionalSymbolRenderer pPSRend = null;
      pPSRend = m_pSizeRend as IProportionalSymbolRenderer;
			string strValueField = null;
			strValueField = pPSRend.Field;
			pTableSort.Fields = strValueField;
			pTableSort.set_Ascending(strValueField, false);

			IDataNormalization pDataNorm = null;
            pDataNorm = pPSRend as IDataNormalization;
			if (pDataNorm.NormalizationType == esriDataNormalization.esriNormalizeByField)
			{
				// comparison is not simple comparison of field values, use callback to do custom compare

				// get normalization field and add to table sort
				string strFields = "";
				strFields = strFields + strValueField;
				string strNormField = null;
				strNormField = pDataNorm.NormalizationField;
				strFields = strFields + ",";
				strFields = strFields + strNormField;
				pTableSort.Fields = strFields;
				pTableSort.set_Ascending(strNormField, false);

				// create new custom table call sort object and connect to the TableSort object
				ITableSortCallBack pTableSortCallBack = null;
				pTableSortCallBack = new SortCallBack(pTable.Fields.FindField(strValueField), pTable.Fields.FindField(strNormField));
				pTableSort.Compare = pTableSortCallBack;
			}

			// call the sort
			pTableSort.Sort(pTrackCancel);

			// retrieve the sorted rows
			IFeatureCursor pSortedCursor = null;
			pSortedCursor = pTableSort.Rows as IFeatureCursor;

			return pSortedCursor;
		}

		private void DrawSymbolsInOrder(IFeatureCursor Cursor, esriDrawPhase drawPhase, IDisplay Display, ITrackCancel trackCancel)
		{
			// this sub draws either markers or line symbols from large small so that the smallest symbols will be drawn on top

			// in graduated symbol case, a cursor is built and parsed n times for n size classes
			// in proportional symbol case, symbols are sorted and drawn from largest to smallest

			int iSizeIndex = 0;
			int iCurrentDrawableSymbolIndex = 0;
			IFeatureCursor pMyCursor = null;
			IFeature pFeat = null;
			IFeatureDraw pFeatDraw = null;
			bool bContinue = true;
			ISymbol pSizeSym = null;
			ISymbol pDrawSym = null;
			IFeatureCursor pSortedCursor = null;

			if (m_pSizeRend is IProportionalSymbolRenderer)
			{
				// sort 
				pSortedCursor = SortData(Cursor, trackCancel);

				// draw
				pFeat = pSortedCursor.NextFeature();
				while (pFeat != null)
				{
					pDrawSym = GetFeatureSymbol(pFeat);
					// draw the feature
					pFeatDraw = pFeat as IFeatureDraw;
					Display.SetSymbol(pDrawSym);

          //implementation of IExportSupport
          BeginFeature(pFeat, Display);
          					
					pFeatDraw.Draw(drawPhase, Display, pDrawSym, true, null, esriDrawStyle.esriDSNormal);

          //implementation of IExportSupport
          GenerateExportInfo(pFeat, Display);
          EndFeature(Display);
					
					// get next feature
					pFeat = pSortedCursor.NextFeature();
					if (trackCancel != null)
							bContinue = trackCancel.Continue();
				}

			}
			else
			{
				IClassBreaksRenderer pSizeCBRend = null;
                pSizeCBRend = m_pSizeRend as IClassBreaksRenderer;
				pMyCursor = Cursor;
				for (iCurrentDrawableSymbolIndex = (pSizeCBRend.BreakCount - 1); iCurrentDrawableSymbolIndex >= 0; iCurrentDrawableSymbolIndex--)
				{
					// do not build a cursor the 1st time because we already have one
					if (iCurrentDrawableSymbolIndex < (pSizeCBRend.BreakCount - 1))
					{
						// build pMyCursor
						pMyCursor = m_pFeatureClass.Search(m_pQueryFilter, true);
					}
					pFeat = pMyCursor.NextFeature();
					while (pFeat != null)
					{
						// check to see if we will draw in this pass
						pSizeSym = m_pSizeRend.get_SymbolByFeature(pFeat);
						iSizeIndex = GetSymbolIndex(pSizeSym, pSizeCBRend);
						if (iSizeIndex == iCurrentDrawableSymbolIndex)
						{
							// go ahead and draw the symbol
							// get symbol to draw
							pDrawSym = GetFeatureSymbol(pFeat);

							// draw the feature
							pFeatDraw = pFeat as IFeatureDraw;
							Display.SetSymbol(pDrawSym);

              //implementation of IExportSupport
              BeginFeature(pFeat, Display);
							
							pFeatDraw.Draw(drawPhase, Display, pDrawSym, true, null, esriDrawStyle.esriDSNormal);

              //implementation of IExportSupport
              GenerateExportInfo(pFeat, Display);
              EndFeature(Display);
              
							if (trackCancel != null)
									bContinue = trackCancel.Continue();
						}

						pFeat = pMyCursor.NextFeature();
					}

				} // increment DOWN to next symbol size

			}

		}

		private void DrawSymbols(IFeatureCursor Cursor, esriDrawPhase drawPhase, IDisplay Display, ITrackCancel trackCancel)
		{

			IFeature pFeat = null;
			IFeatureDraw pFeatDraw = null;
			bool bContinue = true;
			ISymbol pDrawSym = null;

			pFeat = Cursor.NextFeature();
			bContinue = true;
			// while there are still more features and drawing has not been cancelled
			while ((pFeat != null) & (bContinue == true))
			{
				// get symbol to draw
				pDrawSym = GetFeatureSymbol(pFeat);
				// draw the feature
				pFeatDraw = pFeat as IFeatureDraw;
				Display.SetSymbol(pDrawSym);

        //implementation of IExportSupport
        BeginFeature(pFeat, Display);
        
				pFeatDraw.Draw(drawPhase, Display, pDrawSym, true, null, esriDrawStyle.esriDSNormal);

        //implementation of IExportSupport
        GenerateExportInfo(pFeat, Display);
        EndFeature(Display);
        
				// get next feature
				pFeat = Cursor.NextFeature();
				if (trackCancel != null)
						bContinue = trackCancel.Continue();
			}

		}


		private ESRI.ArcGIS.Display.IColor GetCombinedColor(IColor pColor1, IColor pColor2, EColorCombinationType eCombinationMethod)
		{
			return GetCombinedColor(pColor1, pColor2, eCombinationMethod, null);
		}



		private ESRI.ArcGIS.Display.IColor GetCombinedColor(IColor pColor1, IColor pColor2, EColorCombinationType eCombinationMethod, IColor pOriginColor)
		{
			// combines the input colors based on m_eColorCombinationMethod

			// (11/08/04) -- RGB and enuLabLChColorRamp aren't used by GUI

			IColor pOutColor = null;

			long MyOLE_COLOR = 0; // As OLE_COLOR in VB6
			IRgbColor pMainRGBColor = null;
			IRgbColor pVariationRGBColor = null;
			IRgbColor pMergedRGBColor = null;
			bool bOK = false;
			IAlgorithmicColorRamp pAlgorithmicCR = null;

			// if either of the colors are null, then don't run the color through any algorithm,
			//   instead, just return the other color.  if both are null, then return a null color
			if (pColor1.NullColor)
			{
				pOutColor = pColor2;

			}
			else if (pColor2.NullColor)
			{
				pOutColor = pColor1;

			}
			else if (eCombinationMethod == EColorCombinationType.enuComponents)
			{
				// HSV components
				// create a new HSV color
				IHsvColor pHSVDrawColor = null;
				pHSVDrawColor = new HsvColor();
				// get HSV values from Color1 and Color2 and assign to pHSVDrawColor
				IHsvColor pHSVColor1 = null;
				IHsvColor pHSVColor2 = null;

				// (new 4/27/04) didn't think I had to do this...
				//pHSVColor1 = pColor1
				//pHSVColor2 = pColor2
				pHSVColor1 = new HsvColor();
				pHSVColor1.RGB = pColor1.RGB;
				pHSVColor2 = new HsvColor();
				pHSVColor2.RGB = pColor2.RGB;

				pHSVDrawColor.Hue = pHSVColor1.Hue;
				pHSVDrawColor.Saturation = pHSVColor2.Saturation;
				pHSVDrawColor.Value = pHSVColor2.Value;

				pOutColor = pHSVDrawColor;

			}
			else if (eCombinationMethod == EColorCombinationType.enuRGBAverage)
			{
				// use additive color model to merge the two colors
				MyOLE_COLOR = pColor1.RGB;
				pMainRGBColor = new RgbColor();
				pMainRGBColor.RGB = (int)MyOLE_COLOR;
				MyOLE_COLOR = pColor2.RGB;
				pVariationRGBColor = new RgbColor();
				pVariationRGBColor.RGB = (int)MyOLE_COLOR;
				// merged color = RGB average of the two colors
				pMergedRGBColor = new RgbColor();
				pMergedRGBColor.Red = (pMainRGBColor.Red + pVariationRGBColor.Red) / 2;
				pMergedRGBColor.Green = (pMainRGBColor.Green + pVariationRGBColor.Green) / 2;
				pMergedRGBColor.Blue = (pMainRGBColor.Blue + pVariationRGBColor.Blue) / 2;

				pOutColor = pMergedRGBColor;
			}
			else if ((eCombinationMethod == EColorCombinationType.enuCIELabColorRamp) | (eCombinationMethod == EColorCombinationType.enuLabLChColorRamp))
			{
				// use color ramp and take central color between the two colors
				pAlgorithmicCR = new AlgorithmicColorRamp();
				if (m_eColorCombinationMethod == EColorCombinationType.enuCIELabColorRamp)
					pAlgorithmicCR.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;
				else
					pAlgorithmicCR.Algorithm = esriColorRampAlgorithm.esriLabLChAlgorithm;
				pAlgorithmicCR.Size = 3;
				pAlgorithmicCR.FromColor = pColor1;
				pAlgorithmicCR.ToColor = pColor2;
				pAlgorithmicCR.CreateRamp(out bOK);

				pOutColor = pAlgorithmicCR.get_Color(1); // middle color in ramp
			}
			else // EColorCombinationType.enuCIELabMatrix
			{

				double[] iLab1 = new double[4]; // L, a, b values for Color1
				double[] iLab2 = new double[4]; // L, a, b values for Color2
				double[] iLabOrig = new double[4]; // L, a, b values for pOriginColor
				pColor1.GetCIELAB(out iLab1[0], out iLab1[1], out iLab1[2]);
				pColor2.GetCIELAB(out iLab2[0], out iLab2[1], out iLab2[2]);
				pOriginColor.GetCIELAB(out iLabOrig[0], out iLabOrig[1], out iLabOrig[2]);

				double[] iLabOut = new double[4];
				// add color1 vector and color2 vector, then subtract the origin color vector
				iLabOut[0] = iLab1[0] + iLab2[0] - iLabOrig[0];
				iLabOut[1] = iLab1[1] + iLab2[1] - iLabOrig[1];
				iLabOut[2] = iLab1[2] + iLab2[2] - iLabOrig[2];

				CorrectLabOutofRange(ref iLabOut[0], ref iLabOut[1], ref iLabOut[2]);

				IHsvColor pHSVColor = null;
				pHSVColor = new HsvColor();
				pHSVColor.SetCIELAB(iLabOut[0], iLabOut[1], iLabOut[2]);
				pOutColor = pHSVColor;
			}

			return pOutColor;

		}

		private void CorrectLabOutofRange(ref double L, ref double a, ref double b)
		{

			if (L > 100)
				L = 100;
			else if (L < 0)
				L = 0;

			if (a > 120)
				a = 120;
			else if (a < -120)
				a = -120;

			if (b > 120)
				b = 120;
			else if (b < -120)
				b = -120;

		}

		private void RemoveLegend()
		{

			int i = 0;

			if (m_pLegendGroups != null)
			{

				int tempFor1 = m_pLegendGroups.GetUpperBound(0);
				for (i = 0; i <= tempFor1; i++)
				{
					m_pLegendGroups[i] = null;
				}
			}
		}

		private IFeatureRenderer CalcMainRend()
		{
			// consider using an internal array to keep track of active arrays in correct order, this will make it easier to implement ILegendInfo

			if (m_pShapePatternRend != null)
			{
				if ((m_ShapeType == esriGeometryType.esriGeometryPolygon) & m_pSizeRend != null)
					return m_pSizeRend;
				else
					return m_pShapePatternRend;
			}
			else if (m_pSizeRend != null)
				return m_pSizeRend;
			else if (m_pColorRend1 != null)
				return m_pColorRend1;
			else if (m_pColorRend2 != null)
				return m_pColorRend2;
			else
				return null; // must have shape or color or size, if not you can't render...

		}

		public EColorCombinationType ColorCombinationMethod
		{
			get
			{
				return m_eColorCombinationMethod;
			}
			set
			{
				m_eColorCombinationMethod = value;
			}
		}

		public ESRI.ArcGIS.Carto.IFeatureRenderer ColorRend1
		{
			get
			{
				return m_pColorRend1;
			}
			set
			{
				m_pColorRend1 = value;
				m_pMainRend = CalcMainRend();
			}
		}

		public ESRI.ArcGIS.Carto.IFeatureRenderer ColorRend2
		{
			get
			{
				return m_pColorRend2;
			}
			set
			{
				m_pColorRend2 = value;
			}
		}

		public ESRI.ArcGIS.Carto.IFeatureRenderer ShapePatternRend
		{
			get
			{
				return m_pShapePatternRend;
			}
			set
			{
				m_pShapePatternRend = value;
				m_pMainRend = CalcMainRend();
			}
		}

		public ESRI.ArcGIS.Carto.IFeatureRenderer SizeRend
		{
			get
			{
				return m_pSizeRend;
			}
			set
			{
				m_pSizeRend = value;
				m_pMainRend = CalcMainRend();
			}
		}

		private IMarkerSymbol ApplyRotation(IMarkerSymbol pMarkerSym, IFeature pFeat)
		{

			double lAngle = 0;
            int tempoindex = 0;

            tempoindex = pFeat.Fields.FindField(m_sRotationField);

            lAngle = Convert.ToDouble(pFeat.get_Value(tempoindex));
          
            
			if (m_eRotationType == esriSymbolRotationType.esriRotateSymbolGeographic)
				pMarkerSym.Angle = pMarkerSym.Angle - lAngle;
			else
				pMarkerSym.Angle = pMarkerSym.Angle + lAngle - 90;

			return pMarkerSym;
		}

		private ISymbol ApplyTransparency(ISymbol pSym)
		{

			// TODO

			return pSym;
		}

		private ISymbol ApplyColor(ISymbol pSym, IFeature pFeat)
		{
	try
	{
			ISymbol pSym1 = null;
			ISymbol pSym2 = null;
			IColor pColor = null;
			IHsvColor pHSVColor = null;

			if ((m_pColorRend1 != null) & (m_pColorRend2 != null)) // for now both color renderers need to be set to apply color
			{
				pSym1 = m_pColorRend1.get_SymbolByFeature(pFeat);
				pSym2 = m_pColorRend2.get_SymbolByFeature(pFeat);
				// only use GetCombinedColor for HSV component-type combination method
				if (m_eColorCombinationMethod == EColorCombinationType.enuComponents)
				{
					pColor = GetCombinedColor(GetSymbolColor(pSym1), GetSymbolColor(pSym2), m_eColorCombinationMethod);

					
					// Hue is good when I do this...
					pHSVColor = pColor as IHsvColor;
					//'MsgBox(Str(pHSVColor.Hue) & " " & Str(pHSVColor.Saturation) & " " & " " & Str(pHSVColor.Value()))

				}
				else
				{
					
					pColor = new RgbColor();
                    pColor.RGB = (int)m_OLEColorMatrix[GetSymbolIndex(pSym1 as ISymbol, m_pColorRend1 as IClassBreaksRenderer), GetSymbolIndex(pSym2 as ISymbol, m_pColorRend2 as IClassBreaksRenderer)];
					
					
				}


				if (pSym is IMarkerSymbol)
				{
					IMarkerSymbol pMarkerSym = null;
                    pMarkerSym = pSym as IMarkerSymbol;
					pMarkerSym.Color = pColor;
				}
				else if (pSym is ILineSymbol)
				{
					ILineSymbol pLineSym = null;
                    pLineSym = pSym as ILineSymbol;
					pLineSym.Color = pColor;
				}
				else if (pSym != null)
				{
					IFillSymbol pFillSym = null;
					pFillSym = pSym as IFillSymbol;
					pFillSym.Color = pColor;
					

				}


			}

			return pSym;
			return null;
}

catch
{
    return null;
}
		}

		private ISymbol ApplySize(ISymbol pSym, IFeature pFeat)
		{

			if (pSym is IMarkerSymbol)
			{
				// Marker Symbol
				IMarkerSymbol pTargetMarkerSym = null;
                pTargetMarkerSym = pSym as IMarkerSymbol;
				IMarkerSymbol pSourceMarkerSym = null;
                pSourceMarkerSym = m_pSizeRend.get_SymbolByFeature(pFeat) as IMarkerSymbol;
                if (pSourceMarkerSym != null)
                {
                    pTargetMarkerSym.Size = pSourceMarkerSym.Size;
                }

			}
			else
			{
				// Line Symbol
				ILineSymbol pTargetLineSym = null;
				pTargetLineSym = pSym as ILineSymbol;
				ILineSymbol pSourceLineSym = null;
				pSourceLineSym = m_pSizeRend.get_SymbolByFeature(pFeat) as ILineSymbol;
                if (pSourceLineSym != null)
                {
                    pTargetLineSym.Width = pSourceLineSym.Width;
                }

			}

			return pSym;
		}
		


		public string RotationField
		{
			get
			{
				return m_sRotationField;
			}
			set
			{
				m_sRotationField = value;
			}
		}

		public string TransparencyField
		{
			get
			{
				return m_sTransparencyField;
			}
			set
			{
				m_sTransparencyField = value;
			}
		}

		public ESRI.ArcGIS.Carto.esriSymbolRotationType RotationType
		{
			get
			{
				return m_eRotationType;
			}
			set
			{
				m_eRotationType = value;
			}
		}

		private int GetSymbolIndex(ISymbol pSym, IClassBreaksRenderer pRend)
		{
			// given an input symbol and a renderer, this function returns the index of
			//   the class that the symbol represents in the renderer

			int i = 0;
			int iNumBreaks = 0;

		
			iNumBreaks = pRend.BreakCount;
			i = 0;
			ILegendInfo pLegendInfo = null;
			pLegendInfo = pRend as ILegendInfo;
			while (i < iNumBreaks - 1)
			{
				if (pLegendInfo.SymbolsAreGraduated)
				{
					// compare based on size
					if (SymbolsAreSameSize(pSym, pRend.get_Symbol(i)))
							break;
				}
				else
				{
					// compare based on color
					if (SymbolsAreSameColor(pSym, pRend.get_Symbol(i)))
							break;
				}
				i = i + 1;
			}

			return i;

			// NOTE: for some reason we can't test that the symbol objects are the same, so above we do quick test for equal properties instead
			//Do While (i < iNumBreaks - 1)
			//    If pSym Is pRend.Symbol(i) Then Exit Do
			//    i = i + 1
			//Loop

			//Return i

			// (I think this only works for renderer that does Graduated symbols)
			//If m_ShapeType = esriGeometryType.esriGeometryPoint Or m_ShapeType = esriGeometryType.esriGeometryPolygon Then
			//    ' determine the symbol index based on marker symbol size
			//    pInMarkerSym = pSym
			//    i = 0
			//    pClassMarkerSym = pRend.Symbol(0)
			//    dblSize = pClassMarkerSym.Size
			//    Do While (i < iNumBreaks - 1) And (pInMarkerSym.Size > dblSize)
			//        pClassMarkerSym = pRend.Symbol(i)
			//        dblSize = pClassMarkerSym.Size
			//        i = i + 1
			//    Loop
			//    iReturnVal = i
			//Else ' m_shapetype = esriGeometryLine
			//    ' determine the symbol index based on line symbol width
			//    pInLineSym = pSym
			//    i = 0
			//    pClassLineSym = pRend.Symbol(0)
			//    dblWidth = pClassLineSym.Width
			//    Do While (i < iNumBreaks - 1) And (pInLineSym.Width > dblWidth)
			//        pClassLineSym = pRend.Symbol(i)
			//        dblSize = pClassLineSym.Width
			//        i = i + 1
			//    Loop
			//    iReturnVal = i
			//End If

		}

		private bool SymbolsAreSameSize(ISymbol pSym1, ISymbol psym2)
		{

			if (pSym1 is IMarkerSymbol)
			{
				IMarkerSymbol pMS1 = null;
				IMarkerSymbol pMS2 = null;
				pMS1 = pSym1 as IMarkerSymbol;
                pMS2 = psym2 as IMarkerSymbol;
				return pMS1.Size == pMS2.Size;
			}
			else
			{
				ILineSymbol pLS1 = null;
				ILineSymbol pLS2 = null;
				pLS1 = pSym1 as ILineSymbol;
                pLS2 = psym2 as ILineSymbol;
				return pLS1.Width == pLS2.Width;
			}

		}

		private bool SymbolsAreSameColor(ISymbol pSym1, ISymbol psym2)
		{

			IColor pColor1 = null;
			IColor pColor2 = null;
			pColor1 = GetSymbolColor(pSym1);
			pColor2 = GetSymbolColor(psym2);
			return pColor1.RGB == pColor2.RGB;

		}

		private void BuildColorMatrix()
		{
	try
	{
//			On Error GoTo ErrHand

			IClassBreaksRenderer pCBRend1 = null;
			IClassBreaksRenderer pCBRend2 = null;
            pCBRend1 = m_pColorRend1 as IClassBreaksRenderer;
            pCBRend2 = m_pColorRend2 as IClassBreaksRenderer;

			int i = 0;
			int j = 0;
			IColor pColor1 = null;
			IColor pColor2 = null;
			IColor pColor = null;

			if (m_eColorCombinationMethod == EColorCombinationType.enuCIELabMatrix)
			{
				// new (11/5/04) 

				// origin (CIELab average now, but would be better to extend both lines to intersection point,
				//   or average of points where they are closest)
				pColor1 = GetSymbolColor(pCBRend1.get_Symbol(0));
				pColor2 = GetSymbolColor(pCBRend2.get_Symbol(0));
				pColor = GetCombinedColor(pColor1, pColor2, EColorCombinationType.enuCIELabColorRamp);
				IColor pOriginColor = null;
				pOriginColor = pColor;
				m_OLEColorMatrix[i, j] = pColor.RGB;

				// bottom edge (known)

				int tempFor1 = pCBRend1.BreakCount;
				for (i = 1; i < tempFor1; i++)
				{
					pColor = GetSymbolColor(pCBRend1.get_Symbol(i));
					m_OLEColorMatrix[i, 0] = pColor.RGB;
				}

				// left edge (known)

				int tempFor2 = pCBRend2.BreakCount;
				for (j = 1; j < tempFor2; j++)
				{
					pColor = GetSymbolColor(pCBRend2.get_Symbol(j));
					m_OLEColorMatrix[0, j] = pColor.RGB;
				}

				// remaining values (interpolated)

				int tempFor3 = pCBRend1.BreakCount;
				for (i = 1; i < tempFor3; i++)
				{

					int tempFor4 = pCBRend2.BreakCount;
					for (j = 1; j < tempFor4; j++)
					{
						pColor1 = GetSymbolColor(pCBRend1.get_Symbol(i));
						pColor2 = GetSymbolColor(pCBRend2.get_Symbol(j));
						pColor = GetCombinedColor(pColor1, pColor2, EColorCombinationType.enuCIELabMatrix, pOriginColor);
						m_OLEColorMatrix[i, j] = pColor.RGB;
						//m_pColorMatrix(i, j) = GetCombinedColor(pColor1, pColor2)
					}
				}

			}
			else
			{

		
				int tempFor5 = pCBRend1.BreakCount;
				for (i = 0; i < tempFor5; i++)
				{

					int tempFor6 = pCBRend2.BreakCount;
					for (j = 0; j < tempFor6; j++)
					{
						pColor1 = GetSymbolColor(pCBRend1.get_Symbol(i));
						pColor2 = GetSymbolColor(pCBRend2.get_Symbol(j));
						pColor = GetCombinedColor(pColor1, pColor2, m_eColorCombinationMethod);
						m_OLEColorMatrix[i, j] = pColor.RGB;
						//m_pColorMatrix(i, j) = GetCombinedColor(pColor1, pColor2)
					}
				}

			}


			return;
}

catch
{
    Console.WriteLine("error");
}
		}

		private IColor GetSymbolColor(ISymbol pSym)
		{
			IMarkerSymbol pMarkerSym = null;
			ILineSymbol pLineSym = null;
			IFillSymbol pFillSym = null;
			IColor pColor = null;

			if (pSym is IMarkerSymbol)
			{
				pMarkerSym = pSym as IMarkerSymbol;
				pColor = pMarkerSym.Color;
			}
			else if (pSym is ILineSymbol)
			{
				pLineSym = pSym as ILineSymbol;
				pColor = pLineSym.Color;
			}
			else
			{
				pFillSym = pSym as IFillSymbol;
				pColor = pFillSym.Color;
			}

			return pColor;

		}


	}

  //implementation of IExportSupport
  // ExportSupport is a private helper class to help the renderer implement of IExportSupport.  This class contains
  // the reference to the ExportInfoGenerator object used by the renderer.
  public class ExportSupport : IExportSupport
  {
    ISymbologyEnvironment2 m_symbologyEnvironment2;
    IFeatureExportInfoGenerator m_exportInfoGenerator;
    bool m_exportAttributes;
    bool m_exportHyperlinks;

    public ExportSupport()
    {
      m_exportAttributes = false;
      m_exportHyperlinks = false;
    }

    ~ExportSupport()
    {
      m_symbologyEnvironment2 = null;
      m_exportInfoGenerator = null;
    }

    public void GetExportSettings()
    {
      m_exportAttributes = false;
      m_exportHyperlinks = false;
      if (m_exportInfoGenerator == null)
        return;

      if (m_symbologyEnvironment2 == null)
        m_symbologyEnvironment2 = new SymbologyEnvironmentClass();

      m_exportAttributes = m_symbologyEnvironment2.OutputGDICommentForFeatureAttributes;
      m_exportHyperlinks = m_symbologyEnvironment2.OutputGDICommentForHyperlinks;
    }

    public void GenerateExportInfo(IFeature feature, IDisplay display)
    {
      if (m_exportInfoGenerator == null)
        return;
        
      if (m_exportAttributes)
        m_exportInfoGenerator.GenerateFeatureInfo(feature, display);
      if (m_exportHyperlinks)
        m_exportInfoGenerator.GenerateHyperlinkInfo(feature, display);
    }

    public void GenerateExportInfo(IFeatureDraw featureDraw, IDisplay display)
    {
      if (m_exportInfoGenerator == null)
        return;
      
      if (m_exportAttributes)
        m_exportInfoGenerator.GenerateFeatureInfo(featureDraw as IFeature, display);
      if (m_exportHyperlinks)
        m_exportInfoGenerator.GenerateHyperlinkInfo(featureDraw as IFeature, display);
    }

    public void AddExportFields(IFeatureClass fc, IQueryFilter queryFilter)
    {
      if (m_exportInfoGenerator == null)
        return;

      GetExportSettings();

      if (m_exportAttributes || m_exportHyperlinks)
        m_exportInfoGenerator.PrepareExportFilter(fc, queryFilter);
    }

    public void BeginFeature(IFeature feature, IDisplay display)
    {
      if (m_exportInfoGenerator == null)
        return;

      m_exportInfoGenerator.BeginFeature(feature, display);
    }

    public void EndFeature(IDisplay display)
    {
      if (m_exportInfoGenerator == null)
        return;

      m_exportInfoGenerator.EndFeature(display);
    }

    #region IExportSupport Members

    public IFeatureExportInfoGenerator ExportInfo
    {
      set
      {
        m_exportInfoGenerator = value;
      }
    }

    #endregion
  }

    [Guid("13137B0F-2255-46a4-9D6E-0A68FA560379")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [ProgId("MultiVariateRenderers.SortCallBack")]
	public class SortCallBack : ITableSortCallBack
	{

		// class definition for SortCallBack which implements custom table sorting based on field / normalization field

		// data members
		private Microsoft.VisualBasic.VariantType m_value1;
		private Microsoft.VisualBasic.VariantType m_value2;
		private int m_iValueIndex;
		private int m_iNormIndex;

		public SortCallBack(int ValueIndex, int NormIndex)
		{
			m_iValueIndex = ValueIndex;
			m_iNormIndex = NormIndex;

		}

		public int Compare(object value1, object value2, int FieldIndex, int fieldSortIndex)
		{
			int tempCompare = 0;

			// sort normalized values 

			if (FieldIndex == m_iValueIndex)
			{

                m_value1 = (Microsoft.VisualBasic.VariantType)value1;
                m_value2 = (Microsoft.VisualBasic.VariantType)value2;
				return 0; // ?
			}

			if (FieldIndex == m_iNormIndex)
			{

                if (((double)value1 == 0) | ((double)value2 == 0)) // ?
						return 0;

				double dblNormedVal1 = 0;
				double dblNormedVal2 = 0;

				dblNormedVal1 = (double)m_value1 / (double)value1;
                dblNormedVal2 = (double)m_value2 / (double)value2;

				if (dblNormedVal1 > dblNormedVal2)
					tempCompare = 1;
				else if (dblNormedVal1 < dblNormedVal2)
					tempCompare = -1;
				else
					tempCompare = 0;
			}

			return tempCompare;
		}

	}
} //end of root namespace