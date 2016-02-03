using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;

namespace RubberBandZoom
{
  public class RubberBandZoom : ESRI.ArcGIS.Desktop.AddIns.Tool
  {

    private IRubberBand m_RubberBand = null;
    private IGeometry docGeometry = null;
  
    public RubberBandZoom()
    {
    }

    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    protected override void OnActivate()
    {

      //initialize variables.
      docGeometry = null;
      m_RubberBand = null;
      base.OnActivate();
    }


    protected override bool OnContextMenu(int Button, int Shift)
    {
      //returning true for this function overrides the default context menus - we don't want them to come up.
      return true;
    }

    protected override void OnMouseDown(MouseEventArgs Args)//int Button, int Shift, int X, int Y)
    {
      m_RubberBand = new RubberRectangularPolygonClass();


      if (Args.Button.ToString() == "Left")  //left click
      {
        //create a new rubberband polygon.
        docGeometry = m_RubberBand.TrackNew(ArcMap.Document.ActiveView.ScreenDisplay, null);

        //zoom to the selected envelope as long as it is not zero.
        if (!docGeometry.IsEmpty)
        {
          if (docGeometry.Envelope.Height != 0 && docGeometry.Envelope.Width != 0)
          {
            ArcMap.Document.ActiveView.Extent = docGeometry.Envelope;

            //refresh to show changes.
            ArcMap.Document.ActiveView.Refresh();
          }
        }

      }
      else if (Args.Button.ToString() == "Right") //right click
      {
        //zoom out either to previous extent, or to the full extent of the active view.
        if (ArcMap.Document.ActiveView.ExtentStack.CanUndo())
        {
          //if we can, go to a previous zoom extent.
          ArcMap.Document.ActiveView.ExtentStack.Undo();
          ArcMap.Document.ActiveView.Refresh();

        }
        else
        {

          //or if no previous extents exist, go to the full extent of the active view.
          ArcMap.Document.ActiveView.Extent = ArcMap.Document.ActiveView.FullExtent.Envelope;
          ArcMap.Document.ActiveView.Refresh();
        }

      }
    }
  }

}
