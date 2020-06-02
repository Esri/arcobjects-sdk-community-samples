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
using System.Drawing;
using System.Timers;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;

namespace MyDynamicDisplayApp
{
  public sealed class MyDynamicLayer : BaseDynamicLayer
  {
    public bool m_bOnce = true;
    private IDynamicGlyph m_myGlyph = null;
    private IDynamicSymbolProperties2 m_dynamicSymbolProps = null;
    private IPoint m_point = null;
    private double m_stepX = 0;
    private double m_stepY = 0;
    private Timer m_updateTimer = null;

    public MyDynamicLayer() : base()
    {
      base.m_sName = "My Dynamic layer";
      m_updateTimer = new Timer(15);
      m_updateTimer.Enabled = false;
      m_updateTimer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
    }

    public override void DrawDynamicLayer(esriDynamicDrawPhase DynamicDrawPhase, IDisplay Display, IDynamicDisplay DynamicDisplay)
    {
      if (DynamicDrawPhase != esriDynamicDrawPhase.esriDDPImmediate)
        return;

      if (!m_bValid || !m_visible)
        return;

      IEnvelope visibleExtent = Display.DisplayTransformation.FittedBounds;

      if (m_bOnce)
      {
        IDynamicGlyphFactory dynamicGlyphFactory = DynamicDisplay.DynamicGlyphFactory;
        m_dynamicSymbolProps = DynamicDisplay as IDynamicSymbolProperties2;


        ICharacterMarkerSymbol markerSymbol = new CharacterMarkerSymbolClass();
        markerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(new Font("ESRI Default Marker", 25.0f, FontStyle.Bold));
        markerSymbol.Size = 25.0;
        // set the symbol color to white
        markerSymbol.Color = (IColor)ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255));
        markerSymbol.CharacterIndex = 92;

        // create the dynamic glyph
        m_myGlyph = dynamicGlyphFactory.CreateDynamicGlyph((ISymbol)markerSymbol);


        Random r = new Random();
        double X = visibleExtent.XMin + r.NextDouble() * visibleExtent.Width;
        double Y = visibleExtent.YMin + r.NextDouble() * visibleExtent.Height;
        m_point = new PointClass();
        m_point.PutCoords(X, Y);

        m_stepX = visibleExtent.Width / 250;
        m_stepY = visibleExtent.Height / 250;

        // start the update timer
        m_updateTimer.Enabled = true;

        m_bOnce = false;
      }

      // draw the marker
      m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_myGlyph);
      m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 0.0f, 0.0f, 1.0f);
      m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f);
      DynamicDisplay.DrawMarker(m_point);

      // update the point location for the next draw cycle
      m_point.X += m_stepX;
      m_point.Y += m_stepY;

      // make sure that the point fall within the visible extent
      if (m_point.X > visibleExtent.XMax) m_stepX = -Math.Abs(m_stepX);
      if (m_point.X < visibleExtent.XMin) m_stepX = Math.Abs(m_stepX);
      if (m_point.Y > visibleExtent.YMax) m_stepY = -Math.Abs(m_stepY);
      if (m_point.Y < visibleExtent.YMin) m_stepY = Math.Abs(m_stepY);

      // set the dirty flag to false since drawing is done.
      base.m_bIsImmediateDirty = false;
    }

    void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
      // set the dirty flag to true in order to assure the next drawing cycle
      base.m_bIsImmediateDirty = true;
    }
  }
}
