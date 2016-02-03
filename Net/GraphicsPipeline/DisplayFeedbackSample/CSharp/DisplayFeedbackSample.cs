using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace DisplayFeedbackSample
{
  public class DisplayFeedbackSample : ESRI.ArcGIS.Desktop.AddIns.Tool
  {

    private INewEnvelopeFeedback NewEnvelopeFeedback;
    private IEnvelope feedbackEnv;
    private IElement feedbackElement;
    private IScreenDisplay feedbackScreenDisplay;
    private ISimpleLineSymbol feedbackLineSymbol;
    private ESRI.ArcGIS.Geometry.Point feedbackStartPoint;
    private ESRI.ArcGIS.Geometry.Point feedbackMovePoint;
  
    public DisplayFeedbackSample()
    {
    }

    protected override void OnUpdate()
    {
      Enabled = ArcMap.Application != null;
    }

    protected override void OnMouseDown(MouseEventArgs Args)
    {
      //initialize all the variables.
      feedbackEnv = new EnvelopeClass();
      feedbackStartPoint = new ESRI.ArcGIS.Geometry.PointClass();
      feedbackMovePoint = new ESRI.ArcGIS.Geometry.PointClass();
      feedbackLineSymbol = new SimpleLineSymbolClass();
      feedbackScreenDisplay = ArcMap.Document.ActiveView.ScreenDisplay;

      feedbackLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;

      //initialize a new Envelope feedback class and pass it the symbol and display
      NewEnvelopeFeedback = new NewEnvelopeFeedbackClass();
      NewEnvelopeFeedback.Display = feedbackScreenDisplay;
      NewEnvelopeFeedback.Symbol = feedbackLineSymbol as ISymbol;


      //pass the start point from the mouse position, transforming it to an appropriate map point.
      feedbackStartPoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(Args.X, Args.Y) as ESRI.ArcGIS.Geometry.Point;
      NewEnvelopeFeedback.Start(feedbackStartPoint);
    }

    protected override void OnMouseMove(MouseEventArgs Args)
    {

      //only pass the point if the mouse button is down
      if (Args.Button.ToString() == "Left")
      {
        //pass X and Y to feedbackMovePoint to transfer to NewEnvelopeFeedback
        feedbackMovePoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(Args.X, Args.Y) as ESRI.ArcGIS.Geometry.Point;
        NewEnvelopeFeedback.MoveTo(feedbackMovePoint);
      }
    }

    protected override void OnMouseUp(MouseEventArgs Args)
    {
      //when mouse comes up, end the new envelope and pass it to feedbackEnv.
      feedbackEnv = NewEnvelopeFeedback.Stop();

      //initialize a new RectangleElementClass
      feedbackElement = new RectangleElementClass();

      //pass the new rectangle element, the geometry defined by our feedback object
      feedbackElement.Geometry = feedbackEnv;

      //make sure the element is activated in the current view
      feedbackElement.Activate(feedbackScreenDisplay);

      //now add the newly created element to the ActiveView with default symbology.
      ArcMap.Document.ActiveView.GraphicsContainer.AddElement(feedbackElement, 0);

      //and refresh the view so we can see the changes.
      ArcMap.Document.ActiveView.Refresh();
    }
    
  }

}
