using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;

namespace CustomTool
{
    /// <summary>
    /// Summary description for DrawGraphicLine.
    /// </summary>
    [Guid("001c57ca-c292-459d-95a7-9984d78d0d93")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomTool.DrawGraphicLine")]
    public sealed class DrawGraphicLine : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        public DrawGraphicLine()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Walkthroughs"; //localizable text 
            base.m_caption = "Draw Graphic Line";  //localizable text 
            base.m_message = "";  //localizable text
            base.m_toolTip = "Draws a graphic line in the map window of ArcMap.";  //localizable text
            base.m_name = "CustomTool_DrawGraphicLine";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add DrawGraphicLine.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //TODO: Add DrawGraphicLine_VB.OnMouseDown implementation

            //Get the active view from the application object (ie. hook)
            IActiveView activeView = GetActiveViewFromArcMap(m_application);

            //Get the polyline object from the users mouse clicks
            IPolyline polyline = GetPolylineFromMouseClicks(activeView);

            //Make a color to draw the polyline 
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = 255; 

            //Add the users drawn graphics as persistent on the map
            AddGraphicToMap(activeView.FocusMap, polyline, rgbColor, rgbColor);

            //Only redraw the portion of the active view that contains the graphics 
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add DrawGraphicLine.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add DrawGraphicLine.OnMouseUp implementation
        }
        #endregion

        //#### ArcGIS Snippets ####

        #region "Get ActiveView from ArcMap"

        ///<summary>Get ActiveView from ArcMap</summary>
        ///  
        ///<param name="application">An IApplication interface that is the ArcMap application.</param>
        ///   
        ///<returns>An IActiveView interface.</returns>
        ///   
        ///<remarks></remarks>
        public IActiveView GetActiveViewFromArcMap(IApplication application)
        {
          if (application == null)
          {
            return null;
          }
          IMxDocument mxDocument = application.Document as IMxDocument; // Dynamic Cast
          IActiveView activeView = mxDocument.ActiveView;

          return activeView;
        }
        #endregion
        
        #region "Get Polyline From Mouse Clicks"

        ///<summary>
        ///Create a polyline geometry object using the RubberBand.TrackNew method when a user click the mouse on the map control. 
        ///</summary>
        ///<param name="activeView">An ESRI.ArcGIS.Carto.IActiveView interface that will user will interace with to draw a polyline.</param>
        ///<returns>An ESRI.ArcGIS.Geometry.IPolyline interface that is the polyline the user drew</returns>
        ///<remarks>Double click the left mouse button to end tracking the polyline.</remarks>
        public IPolyline GetPolylineFromMouseClicks(IActiveView activeView)
        {

          IScreenDisplay screenDisplay = activeView.ScreenDisplay;

          IRubberBand rubberBand = new RubberLineClass();
          IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);

          IPolyline polyline = (IPolyline)geometry;

          return polyline;

        }
        #endregion

        #region "Add Graphic to Map"

        ///<summary>Draw a specified graphic on the map using the supplied colors.</summary>
        ///      
        ///<param name="map">An IMap interface.</param>
        ///<param name="geometry">An IGeometry interface. It can be of the geometry type: esriGeometryPoint, esriGeometryPolyline, or esriGeometryPolygon.</param>
        ///<param name="rgbColor">An IRgbColor interface. The color to draw the geometry.</param>
        ///<param name="outlineRgbColor">An IRgbColor interface. For those geometry's with an outline it will be this color.</param>
        ///      
        ///<remarks>Calling this function will not automatically make the graphics appear in the map area. Refresh the map area after after calling this function with Methods like IActiveView.Refresh or IActiveView.PartialRefresh.</remarks>
        public void AddGraphicToMap(IMap map, IGeometry geometry, IRgbColor rgbColor, IRgbColor outlineRgbColor)
        {
          IGraphicsContainer graphicsContainer = (IGraphicsContainer)map; // Explicit Cast
          IElement element = null;
          if ((geometry.GeometryType) == esriGeometryType.esriGeometryPoint)
          {
            // Marker symbols
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
            simpleMarkerSymbol.Color = rgbColor;
            simpleMarkerSymbol.Outline = true;
            simpleMarkerSymbol.OutlineColor = outlineRgbColor;
            simpleMarkerSymbol.Size = 15;
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

            IMarkerElement markerElement = new MarkerElementClass();
            markerElement.Symbol = simpleMarkerSymbol;
            element = (IElement)markerElement; // Explicit Cast
          }
          else if ((geometry.GeometryType) == esriGeometryType.esriGeometryPolyline)
          {
            //  Line elements
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Color = rgbColor;
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            simpleLineSymbol.Width = 5;

            ILineElement lineElement = new LineElementClass();
            lineElement.Symbol = simpleLineSymbol;
            element = (IElement)lineElement; // Explicit Cast
          }
          else if ((geometry.GeometryType) == esriGeometryType.esriGeometryPolygon)
          {
            // Polygon elements
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Color = rgbColor;
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
            IFillShapeElement fillShapeElement = new PolygonElementClass();
            fillShapeElement.Symbol = simpleFillSymbol;
            element = (IElement)fillShapeElement; // Explicit Cast
          }
          if (!(element == null))
          {
            element.Geometry = geometry;
            graphicsContainer.AddElement(element, 0);
          }
        }
        #endregion
    }
}
