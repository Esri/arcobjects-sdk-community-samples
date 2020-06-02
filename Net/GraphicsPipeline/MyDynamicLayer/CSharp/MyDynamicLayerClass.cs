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
using System.Data;
using System.Drawing;
using System.Timers;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF;

namespace MyDynamicLayer
{
  /// <summary>
  /// This layer implements a DynamicLayer based on the DynamicLayerBase base-class
  /// </summary>
  [Guid("27FB44EB-0426-415f-BFA3-D1581056C0C4")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MyDynamicLayer.MyDynamicLayerClass")]
  public sealed class MyDynamicLayerClass : ESRI.ArcGIS.ADF.BaseClasses.BaseDynamicLayer
  {
    #region class members
    private IPoint                    m_point                   = null;
    private bool                      m_bDynamicGlyphsCreated   = false;
    private bool                      m_bOnce                   = true;
    private IDynamicGlyph[]           m_markerGlyphs            = null;
    private IDynamicGlyph             m_textGlyph               = null;
    private Timer                     m_updateTimer             = null;
    private double                    m_extentMaxX              = 1000.0;
    private double                    m_extentMaxY              = 1000.0;
    private double                    m_extentMinX              = 0.0;
    private double                    m_extentMinY              = 0.0;
    private IDynamicGlyphFactory2      m_dynamicGlyphFactory     = null;
    private IDynamicSymbolProperties  m_dynamicSymbolProps      = null;
    private IDynamicCompoundMarker    m_dynamicCompoundMarker   = null;
		private DataTable									m_table										= null;
		private const int									m_nNumOfItems							= 100;
    #endregion

    #region class constructor
    public MyDynamicLayerClass() : base()
    {
			m_table = new DataTable ("rows");
			m_table.Columns.Add("OID",			typeof (int));	//0
      m_table.Columns.Add("X",        typeof(double)); //1
      m_table.Columns.Add("Y",        typeof(double)); //2
      m_table.Columns.Add("STEPX",    typeof(double)); //3
      m_table.Columns.Add("STEPY",    typeof(double)); //4
      m_table.Columns.Add("HEADING",  typeof(double)); //5
      m_table.Columns.Add("TYPE",     typeof(int));    //6

      //set the ID column to be AutoIncremented
      m_table.Columns[0].AutoIncrement = true;

      m_point = new PointClass();

      //set an array to store the glyphs used to symbolize the tracked object
      m_markerGlyphs = new IDynamicGlyph[3];

      //set the update timer for the layer
      m_updateTimer = new Timer(50);
      m_updateTimer.Enabled = false;
      m_updateTimer.Elapsed += new ElapsedEventHandler(OnLayerUpdateEvent);

    }
    #endregion

    #region overriden methods
    /// <summary>
    /// The dynamic layer draw method
    /// </summary>
    /// <param name="DynamicDrawPhase">the current drawphase of the dynamic drawing</param>
    /// <param name="Display">the ActiveView's display</param>
    /// <param name="DynamicDisplay">the ActiveView's dynamic display</param>
    public override void DrawDynamicLayer(ESRI.ArcGIS.Display.esriDynamicDrawPhase DynamicDrawPhase, ESRI.ArcGIS.Display.IDisplay Display, ESRI.ArcGIS.Display.IDynamicDisplay DynamicDisplay)
    {
      try
      {
        //make sure that the display is valid as well as that the layer is visible
        if (null == DynamicDisplay || null == Display || !this.m_visible)
          return;

        //make sure that the current drawphase is immediate. In this sample there is no use of the
        //compiled drawPhase. Use the esriDDPCompiled drawPhase in order to draw semi-static items (items
        //which have update rate lower than the display update rate).
				if (DynamicDrawPhase != esriDynamicDrawPhase.esriDDPImmediate)
          return;

        if (m_bOnce)
        {
          //cast the DynamicDisplay into DynamicGlyphFactory
          m_dynamicGlyphFactory = DynamicDisplay.DynamicGlyphFactory as IDynamicGlyphFactory2;
          //cast the DynamicDisplay into DynamicSymbolProperties
          m_dynamicSymbolProps = DynamicDisplay as IDynamicSymbolProperties;
          //cast the compound marker symbol
          m_dynamicCompoundMarker = DynamicDisplay as IDynamicCompoundMarker;

					IntializeLayerData (Display.DisplayTransformation);

          GetLayerExtent();

          m_bOnce = false;
        }

        //get the display fitted bounds
        m_extentMaxX = Display.DisplayTransformation.FittedBounds.XMax;
        m_extentMaxY = Display.DisplayTransformation.FittedBounds.YMax;
        m_extentMinX = Display.DisplayTransformation.FittedBounds.XMin;
        m_extentMinY = Display.DisplayTransformation.FittedBounds.YMin;

        //create the dynamic symbols for the layer
        if (!m_bDynamicGlyphsCreated)
        {
          this.CreateDynamicSymbols(m_dynamicGlyphFactory);
          m_bDynamicGlyphsCreated = true;
        }

        double X, Y, heading;
        int type;
        //iterate through the layers' items
        foreach (DataRow r in m_table.Rows)
        {
          if (r[1] is DBNull || r[2] is DBNull)
            continue;

          //get the item's coordinate, heading and type
          X = Convert.ToDouble(r[1]);
          Y = Convert.ToDouble(r[2]);
          heading = Convert.ToDouble(r[5]);
          type = Convert.ToInt32(r[6]);

          //assign the items' coordinate to the cached point
          m_point.PutCoords(X, Y);

          //set the symbol's properties
          switch (type)
          {
            case 0:
              //set the heading of the current symbols' text
              m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolText, 0.0f);
              m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, 0.0f);

              //set the symbol alignment so that it will align with the screen
              m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRAScreen);

              //set the text alignment so that it will also align with the screen
              m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolText, esriDynamicSymbolRotationAlignment.esriDSRAScreen);

              //scale the item
              m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 0.8f, 0.8f);
              //set the items' color (blue)
              m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0f, 0.0f, 1.0f, 1.0f); // Blue
              //assign the item's glyph to the dynamic-symbol
              m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_markerGlyphs[0]);
              //set the labels text glyph
              m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolText, m_textGlyph);
              //set the color of the text
              m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolText, 1.0f, 1.0f, 0.0f, 1.0f); // Yellow

              //draw the item as a compound marker. This means that you do not have to draw the items and their
              //accompanying labels separately, and thus allow you to write less code as well as get better
              //performance.  
              m_dynamicCompoundMarker.DrawCompoundMarker4
                (m_point,
                //"TOP",
                //"BOTTOM",
                "Item " + Convert.ToString(r[0]),
                heading.ToString("###.##"),
                m_point.X.ToString("###.#####"),
                m_point.Y.ToString("###.#####"));
              break;
            case 1:
              //set the heading of the current symbol
              m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, (float)heading);

              //set the symbol alignment so that it will align with towards the symbol heading
              m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRANorth);

              m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f);
              m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 0.0f, 1.0f, 0.6f, 1.0f); // GREEN
              m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_markerGlyphs[1]);

              //draw the current location
              DynamicDisplay.DrawMarker(m_point);
              break;
            case 2:
              //set the heading of the current symbol
              m_dynamicSymbolProps.set_Heading(esriDynamicSymbolType.esriDSymbolMarker, (float)heading);

              //set the symbol alignment so that it will align with towards the symbol heading
              m_dynamicSymbolProps.set_RotationAlignment(esriDynamicSymbolType.esriDSymbolMarker, esriDynamicSymbolRotationAlignment.esriDSRANorth);

              m_dynamicSymbolProps.SetScale(esriDynamicSymbolType.esriDSymbolMarker, 1.1f, 1.1f);
              m_dynamicSymbolProps.SetColor(esriDynamicSymbolType.esriDSymbolMarker, 1.0f, 1.0f, 1.0f, 1.0f); // WHITE
              m_dynamicSymbolProps.set_DynamicGlyph(esriDynamicSymbolType.esriDSymbolMarker, m_markerGlyphs[2]);

              //draw the current location
              DynamicDisplay.DrawMarker(m_point);
              break;
          }
        }

       // by setting immediate flag to false, we signal the dynamic display that the layer is current.
        base.m_bIsImmediateDirty = false;
				
      }     
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }
		
    /// <summary>
    /// Returns the UID (ProgID or CLSID)
    /// </summary>
    public override UID ID
    {
      get
      {
        m_uid.Value = "MyDynamicLayer.MyDynamicLayerClass";
        return m_uid;
      }
    }
    #endregion

    #region public methods
    public void Connect()
    {
      m_updateTimer.Enabled = true;
    }

    public void Disconnect()
    {
      m_updateTimer.Enabled = false;
    }

		public DataRow NewItem ()
		{
			if (m_table == null)
				return null;
			else
				return m_table.NewRow ();
		}

		public void AddItem (DataRow row)
		{
			if (row == null)
				return;
			else
				m_table.Rows.Add (row);
		}
    #endregion

    #region private utility methods
    /// <summary>
    /// create the layer's glyphs used to set the symbol of the dynamic-layer items
    /// </summary>
    /// <param name="pDynamicGlyphFactory"></param>
    private void CreateDynamicSymbols(IDynamicGlyphFactory2 pDynamicGlyphFactory)
    {
      try
      {
        //set the background color
				IColor color = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor (Color.FromArgb (255, 255, 255)) as IColor;

        // Create Character Marker Symbols glyph
        // --------------------------------------
				ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbolClass ();
				characterMarkerSymbol.Color = color as IColor;
                characterMarkerSymbol.Font = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(new Font("ESRI Environmental & Icons", 32));
				characterMarkerSymbol.Size = 40;
				characterMarkerSymbol.Angle = 0;
				characterMarkerSymbol.CharacterIndex = 36;

        //create the glyph from the marker symbol
        m_markerGlyphs[0] = pDynamicGlyphFactory.CreateDynamicGlyph(characterMarkerSymbol as ISymbol);

        characterMarkerSymbol.Size = 32;
        characterMarkerSymbol.CharacterIndex = 224;

        //create the glyph from the marker symbol
        m_markerGlyphs[1] = pDynamicGlyphFactory.CreateDynamicGlyph(characterMarkerSymbol as ISymbol);

        // Create the glyph from embedded bitmap
        // -----------------------------------
        // Sets the transparency color
        IColor transparentColor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(Color.FromArgb(255, 255, 255)) as IColor;

				Bitmap bitmap = new Bitmap (GetType (), "B2.bmp");
				m_markerGlyphs[2] = pDynamicGlyphFactory.CreateDynamicGlyphFromBitmap(esriDynamicGlyphType.esriDGlyphMarker, bitmap.GetHbitmap ().ToInt32 (), false, transparentColor);
     
        // Create a glyph for the labels text, use the first 'internal' text glyph 
        // ------------------------------------------------------------------------
        m_textGlyph = pDynamicGlyphFactory.get_DynamicGlyph(1, esriDynamicGlyphType.esriDGlyphText, 1);

      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }
    
    /// <summary>
    /// timer elapsed event handler, used to update the layer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks>This layer has synthetic data, and therefore need the timer in order
    /// to update the layers' items.</remarks>
    private void OnLayerUpdateEvent(object sender, ElapsedEventArgs e)
    {
      try
      {
        double X, Y, stepX, stepY, heading;

        //iterate through the layers' records
				foreach (DataRow r in m_table.Rows)
        {
          if (r[1] is DBNull || r[2] is DBNull)
            continue;

          //get the current item location and the item's steps
          X = Convert.ToDouble(r[1]);
          Y = Convert.ToDouble(r[2]);
          stepX = Convert.ToDouble(r[3]);
          stepY = Convert.ToDouble(r[4]);

          //increment the item's location
          X += stepX;
          Y += stepY;

          //test that the item's location is within the fitted bounds
          if (X > m_extentMaxX) stepX = -Math.Abs(stepX);
          if (X < m_extentMinX) stepX = Math.Abs(stepX);
          if (Y > m_extentMaxY) stepY = -Math.Abs(stepY);
          if (Y < m_extentMinY) stepY = Math.Abs(stepY);
          //calculate the item's heading
          heading = (360.0 + 90.0 - Math.Atan2(stepY, stepX) * 180 / Math.PI) % 360.0;

          //update the item's record
          r[1] = X;
          r[2] = Y;
          r[3] = stepX;
          r[4] = stepY;
          r[5] = heading;
          lock (m_table)
          {
            r.AcceptChanges();
          }

					//set the dirty flag to true in order to let the DynamicDisplay that the layer needs redraw.
					base.m_bIsImmediateDirty = true;
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Calculates the layer extent
    /// </summary>
    private void GetLayerExtent()
    {
      if (null == m_table)
        return;

      IEnvelope env = new EnvelopeClass();
      env.SpatialReference = base.m_spatialRef;
      IPoint point = new PointClass();
			foreach (DataRow r in m_table.Rows)
      {
        if (r[1] is DBNull || r[2] is DBNull)
          continue;

        point.Y = Convert.ToDouble(r[3]);
        point.X = Convert.ToDouble(r[4]);

        env.Union(point.Envelope);
      }

      base.m_extent = env;
    }

		/// <summary>
		/// Initialize the synthetic data of the layer
		/// </summary>
		private void IntializeLayerData (IDisplayTransformation displayTransformation)
		{
			try
			{

				//get the map's fitted bounds
				IEnvelope extent = displayTransformation.FittedBounds;

				//calculate the step for which will be used to increment the items
				double XStep = extent.Width / 5000.0;
				double YStep = extent.Height / 5000.0;

				Random rnd = new Random ();
				double stepX, stepY;

				//generate the items
				for (int i = 0; i < m_nNumOfItems; i++)
				{
					//calculate the step for each item
					stepX = XStep * rnd.NextDouble ();
					stepY = YStep * rnd.NextDouble ();

					//create new record
					DataRow r = NewItem ();
					//set the item's coordinate
					r[1] = extent.XMin + rnd.NextDouble () * (extent.XMax - extent.XMin);
					r[2] = extent.YMin + rnd.NextDouble () * (extent.YMax - extent.YMin);
					//set the item's steps
					r[3] = stepX;
					r[4] = stepY;
					//calculate the heading
					r[5] = (360.0 + 90.0 - Math.Atan2 (stepY, stepX) * 180 / Math.PI) % 360.0;

					//add a type ID in order to define the symbol for the item
					switch (i % 3)
					{
						case 0:
							r[6] = 0;
							break;
						case 1:
							r[6] = 1;
							break;
						case 2:
							r[6] = 2;
							break;
					}

					//add the new item record to the table
					AddItem (r);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine (ex.Message);
			}
		}
    #endregion

  }
}
