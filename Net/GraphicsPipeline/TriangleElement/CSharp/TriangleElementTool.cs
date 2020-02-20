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
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;


namespace TriangleElement
{
  /// <summary>
  /// Summary description for TriangleElementTool.
  /// </summary>
  [Guid("2d0c353b-2a0e-4fdf-90ae-fc8e1314a989")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("TriangleElement.TriangleElementTool")]
  public sealed class TriangleElementTool : BaseTool
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
      ControlsCommands.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);
      ControlsCommands.Unregister(regKey);
    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper = null;

    public TriangleElementTool()
    {
      base.m_category = ".NET Samples";
      base.m_caption = "Triangle Element";
      base.m_message = "Add Triangle Element";
      base.m_toolTip = "Add Triangle Element";
      base.m_name = "TriangleElement_TriangleElementTool";  
      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
        base.m_cursor = new Cursor(GetType(), GetType().Name + ".cur");
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
      try
      {
        m_hookHelper = new HookHelperClass();
        m_hookHelper.Hook = hook;
        if (m_hookHelper.ActiveView == null)
        {
          m_hookHelper = null;
        }
      }
      catch
      {
        m_hookHelper = null;
      }

      if (m_hookHelper == null)
        base.m_enabled = false;
      else
        base.m_enabled = true;
    }

    /// <summary>
    /// Occurs when this tool is clicked
    /// </summary>
    public override void OnClick()
    {
      // TODO: Add TriangleElementTool.OnClick implementation
    }

    public override void OnMouseDown(int Button, int Shift, int X, int Y)
    {
      //set the color for the symbol's outline
      IColor color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.Black);

      //create the triangle outline symbol
      ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
      lineSymbol.Color = color;
      lineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
      lineSymbol.Width = 2.0;

      //create the triangle's fill symbol
      color = (IColor)Converter.ToRGBColor(Color.Gold);
      color.Transparency = 50;
      ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
      simpleFillSymbol.Color = color;
      simpleFillSymbol.Outline = (ILineSymbol)lineSymbol;
      simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;

      //create a new triangle element
      ITriangleElement triangleElement = new TriangleElementClass();
      triangleElement.Angle = 40.0;
      triangleElement.Size = 25;
      triangleElement.FillSymbol = simpleFillSymbol;

      //set the geometry of the element
      IPoint point = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
      IElement element = (IElement)triangleElement;
      element.Geometry = (IGeometry)point;

      //add the graphics element to the map
      IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_hookHelper.FocusMap;
      graphicsContainer.AddElement(element, 0);
      m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
    }

    public override void OnMouseMove(int Button, int Shift, int X, int Y)
    {
      // TODO:  Add TriangleElementTool.OnMouseMove implementation
    }

    public override void OnMouseUp(int Button, int Shift, int X, int Y)
    {
      // TODO:  Add TriangleElementTool.OnMouseUp implementation
    }
    #endregion
  }
}
