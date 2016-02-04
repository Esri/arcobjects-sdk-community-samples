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
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace TimeSeriesGraphAddIn
{
    public class TimeSeriesGraphAddIn : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public TimeSeriesGraphAddIn()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            int X = arg.X;
            int Y = arg.Y;
            IMxApplication pMxApp = null;
            IMxDocument pMxDoc = null;
            pMxApp = (IMxApplication)ArcMap.Application;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            // calculate tolerance rectangle to identify features inside it
            int Tolerance = 0;
            Tolerance = pMxDoc.SearchTolerancePixels;

            IDisplayTransformation pDispTrans = null;
            pDispTrans = pMxApp.Display.DisplayTransformation;
            tagRECT pToleranceRect = new tagRECT();
            pToleranceRect.left = X - Tolerance;
            pToleranceRect.right = X + Tolerance;
            pToleranceRect.top = Y - Tolerance;
            pToleranceRect.bottom = Y + Tolerance;

            IEnvelope pSearchEnvelope = null;
            pSearchEnvelope = new EnvelopeClass();
            pDispTrans.TransformRect(pSearchEnvelope, ref pToleranceRect, (int)(esriDisplayTransformationEnum.esriTransformPosition | esriDisplayTransformationEnum.esriTransformToMap));

            // identify feature points of measurement
            IBasicDocument pBasicDoc = null;
            pBasicDoc = (IBasicDocument)ArcMap.Application.Document;
            pSearchEnvelope.SpatialReference = pMxDoc.ActiveView.FocusMap.SpatialReference;

            IIdentify pIdentify = null;
            pIdentify = (IIdentify)pMxDoc.FocusMap.get_Layer(0);
            if (pIdentify == null)
            {
                MessageBox.Show("No layer");
                return;
            }

            IArray pIDArray = null;
            pIDArray = pIdentify.Identify(pSearchEnvelope);

            // get object from feature point
            IIdentifyObj pIDObj = null;
            if (pIDArray != null)
                pIDObj = (IIdentifyObj)pIDArray.get_Element(0);

            if (pIDObj == null)
            {
                MessageBox.Show("No feature was identified");
                return;
            }

            // get the name of the layer containing feature points
            ILayer pLayer = null;
            pLayer = pMxDoc.FocusMap.get_Layer(0);

            string layerName = null;
            layerName = pLayer.Name;

            // get primary display field for measurement values and set names of a date/time field and gage ID field
            IFeatureLayer pFeatLayer = null;
            pFeatLayer = (IFeatureLayer)pLayer;
            string dataFldName = null;
            string timefldName = null;
            string gageIDFldName = null;
            dataFldName = "TSValue";
            timefldName = "TSDateTime"; // substitute data/time field name for different dataset
            gageIDFldName = "Name"; // substitute gage ID field name for different dataset

            // get display table from layer
            ITable pTable = null;
            IDisplayTable pDisplayTable = null;
            pDisplayTable = (IDisplayTable)pLayer;
            if (pDisplayTable != null)
            {
                pTable = pDisplayTable.DisplayTable;
                if (pTable == null)
                    goto THEEND;
            }

            // get fields from display table
            IFields pFields = null;
            pFields = pTable.Fields;
            long fldCount = 0;
            fldCount = pFields.FieldCount;

            // create WHERE clause from identified objects of measurement points
            int gageIDFldIdx = 0;
            gageIDFldIdx = pFields.FindField(gageIDFldName);

            IRowIdentifyObject pRowIDObj = null;
            pRowIDObj = (IRowIdentifyObject)pIDObj;

            string gageID = null;
            gageID = (string)pRowIDObj.Row.get_Value(gageIDFldIdx);

            IFeatureLayerDefinition pFeatureLayerDef = null;
            pFeatureLayerDef = (IFeatureLayerDefinition)pLayer;
            string definitionExpression = null;
            definitionExpression = pFeatureLayerDef.DefinitionExpression;

            string whereClause = null;
            if (definitionExpression == "")
                whereClause = "[" + gageIDFldName + "] = '" + gageID + "'";
            else
                whereClause = "[" + gageIDFldName + "] = '" + gageID + "' AND " + definitionExpression;

            //find color for the identified object from feature layer's renderer
            IGeoFeatureLayer pGeoFeatureLayer = null;
            pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;

            ILookupSymbol pLookupSymbol = null;
            pLookupSymbol = (ILookupSymbol)pGeoFeatureLayer.Renderer;

            IFeature pFeature = null;
            pFeature = (IFeature)pRowIDObj.Row;

            IMarkerSymbol pSymbol = null;
            pSymbol = (IMarkerSymbol)pLookupSymbol.LookupSymbol(false, pFeature);

            // Find an opened GraphWindow
            IDataGraphBase pDataGraphBase = null;
            IDataGraphT pDataGraphT = null;
            IDataGraphWindow2 pDGWin = null;

            IDataGraphCollection pDataGraphs = null;
            pDataGraphs = (IDataGraphCollection)pMxDoc;
            int grfCount = 0;
            grfCount = pDataGraphs.DataGraphCount;
            for (int i = 0; i < grfCount; i++)
            {
                pDataGraphBase = pDataGraphs.get_DataGraph(i);
                pDGWin = FindGraphWindow(ref pDataGraphBase);
                if (pDGWin != null)
                    break;
            }

            // if there is not an opened graph window - create a new graph for
            if (pDGWin == null)
            {
                // create graph
                pDataGraphT = new DataGraphTClass();
                pDataGraphBase = (IDataGraphBase)pDataGraphT;

                // load template from <ARCGISHOME>\GraphTemplates\
                string strPath = null;
                strPath = Environment.GetEnvironmentVariable("ARCGISHOME");
                try
                {
                    pDataGraphT.LoadTemplate(strPath + @"GraphTemplates\timeseries.tee");
                }
                catch
                { }

                // graph, axis and legend titles. Substitute them for different input layer
                pDataGraphT.GeneralProperties.Title = "Daily Streamflow for Guadalupe Basin in 1999";
                pDataGraphT.LegendProperties.Title = "Monitoring Point";
                pDataGraphT.get_AxisProperties(0).Title = "Streamflow (cfs)";
                pDataGraphT.get_AxisProperties(0).Logarithmic = true;
                pDataGraphT.get_AxisProperties(2).Title = "Date";
                pDataGraphBase.Name = layerName;
            }
            else // get graph from the opened window
                pDataGraphT = (IDataGraphT)pDataGraphBase;

            // create vertical line series for all measurements for the identified gage
            ISeriesProperties pSP = null;
            pSP = pDataGraphT.AddSeries("line:vertical");
            pSP.ColorType = esriGraphColorType.esriGraphColorCustomAll;
            pSP.CustomColor = pSymbol.Color.RGB;
            pSP.WhereClause = whereClause;
            pSP.InLegend = true;
            pSP.Name = gageID;

            pSP.SourceData = pLayer;
            pSP.SetField(0, timefldName);
            pSP.SetField(1, dataFldName);
            IDataSortSeriesProperties pSortFlds = null;
            pSortFlds = (IDataSortSeriesProperties)pSP;
            int idx = 0;
            pSortFlds.AddSortingField(timefldName, true, ref idx);


            pDataGraphBase.UseSelectedSet = true;

            ITrackCancel pCancelTracker = null;
            pCancelTracker = new CancelTracker();
            pDataGraphT.Update(pCancelTracker);

            // create data graph window if there is not any opened one
            if (pDGWin == null)
            {
                pDGWin = new DataGraphWindowClass();
                pDGWin.DataGraphBase = pDataGraphBase;
                pDGWin.Application = ArcMap.Application;
                pDGWin.Show(true);

                pDataGraphs.AddDataGraph(pDataGraphBase);
            }

        THEEND:
            return;

            //base.OnMouseDown(arg);
        }

        // finds an opened graph window
        private IDataGraphWindow2 FindGraphWindow(ref IDataGraphBase pDataGraphBase)
        {

            IApplicationWindows pApplicationWindows = null;
            pApplicationWindows = (IApplicationWindows)ArcMap.Application;

            ISet pDataWindows = null;
            pDataWindows = pApplicationWindows.DataWindows;
            int winCount = 0;
            winCount = pDataWindows.Count;
            if (winCount <= 0)
                return null;

            pDataWindows.Reset();

            for (int i = 0; i < winCount; i++)
            {
                IDataGraphWindow2 pDataGraphWindow2 = null;
                pDataGraphWindow2 = (IDataGraphWindow2)pDataWindows.Next();
                if (pDataGraphWindow2 != null)
                {
                    IDataGraphBase pDataGraphTmp = null;
                    pDataGraphTmp = pDataGraphWindow2.DataGraphBase;
                    if (pDataGraphBase == pDataGraphTmp)
                        return pDataGraphWindow2;
                }
            }

            return null;
        }
    }

}
