using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AngleAngle
{
  class AngleAngleCstr : IShapeConstructor, IPersist
  {
    IEditor m_editor;
    IEditSketch3 m_edSketch;
    ISnappingEnvironment m_snappingEnv;
    IPointSnapper m_snapper;
    ISnappingFeedback m_snappingFeedback;
    
    // Declare 3 points
    IPoint m_firstPoint;
    IPoint m_secondPoint;
    IPoint m_activePoint;
    // Declare 2 angles
    double m_firstAngle;
    double m_secondAngle;

    enum ToolPhase { Inactive, SecondPoint, Intersection }
    ToolPhase m_etoolPhase;

    #region IShapeConstructor Members

    public void Activate()
    {
    }

    public bool Active
    {
      get { return true; }
    }

    public void AddPoint(IPoint point, bool Clone, bool allowUndo)
    {
    }

    public IPoint Anchor
    {
      get { return Anchor; }
    }

    public double AngleConstraint
    {
      get
      {
        return AngleConstraint;
      }
      set
      {
        AngleConstraint = value;
      }
    }

    public esriSketchConstraint Constraint
    {
      get
      {
        return Constraint;
      }
      set
      {
        Constraint = value;
      }
    }

    public int Cursor
    {
      get { return 0; }
    }

    public void Deactivate()
    {
    }

    public double DistanceConstraint
    {
      get
      {
        return DistanceConstraint;
      }
      set
      {
        DistanceConstraint = value;
      }
    }

    public bool Enabled
    {
      get { return true; }
    }

    public string ID
    {
      get { return ("Angle Angle Constructor"); }
    }

    public void Initialize(IEditor pEditor)
    {
      // Initialize the constructor
      m_editor = pEditor as IEditor;
      m_edSketch = pEditor as IEditSketch3;

      //Get the snap environment
      m_snappingEnv = m_editor.Parent.FindExtensionByName("ESRI Snapping") as ISnappingEnvironment;
      m_snapper = m_snappingEnv.PointSnapper;
      m_snappingFeedback = new SnappingFeedbackClass();
      m_snappingFeedback.Initialize(m_editor.Parent, m_snappingEnv, true);

      m_firstPoint = new PointClass();
      m_secondPoint = new PointClass();
      m_activePoint = new PointClass();

      // Set the phase to inactive so we start at the beginning 
      m_etoolPhase = ToolPhase.Inactive;
    }

    public bool IsStreaming
    {
      get
      {
        return IsStreaming;
      }
      set
      {
        IsStreaming = value;
      }
    }

    public IPoint Location
    {
      get { return Location; }
    }

    public bool OnContextMenu(int X, int Y)
    {
      return true;
    }

    public void OnKeyDown(int keyState, int shift)
    {
      // If the escape key is used, throw away the calculated point 
      if (keyState == (int)Keys.Escape)
        m_etoolPhase = ToolPhase.Inactive;
    }

    public void OnKeyUp(int keyState, int shift)
    {
    }

    public void OnMouseDown(int Button, int shift, int X, int Y)
    {
      if (Button != (int)Keys.LButton) return;
      switch (m_etoolPhase)
      {
        case (ToolPhase.Inactive):
          GetFirstPoint();
          break;
        case (ToolPhase.SecondPoint):
          GetSecondPoint();
          break;
        case (ToolPhase.Intersection):
          GetIntersection();
          break;
      }
    }

    public void OnMouseMove(int Button, int shift, int X, int Y)
    {
      //Snap the mouse location
      if (m_etoolPhase != ToolPhase.Intersection)
      {
        m_activePoint = m_editor.Display.DisplayTransformation.ToMapPoint(X, Y);
        ISnappingResult snapResult = m_snapper.Snap(m_activePoint);
        m_snappingFeedback.Update(snapResult, 0);

        if (snapResult != null)
          m_activePoint = snapResult.Location;
      }
    }

    public void OnMouseUp(int Button, int shift, int X, int Y)
    {
    }

    public void Refresh(int hdc)
    {
      m_snappingFeedback.Refresh(hdc);
    }

    public void SketchModified()
    {
    }
    #endregion

    private void GetFirstPoint()
    {
      INumberDialog numDialog = new NumberDialogClass();
      // Set first point to the active point which may have been snapped
      m_firstPoint = m_activePoint;
      // Get the angle
      if (numDialog.DoModal("Angle 1", 45, 2, m_editor.Display.hWnd))
      {
        m_firstAngle = numDialog.Value * Math.PI / 180;
        m_etoolPhase = ToolPhase.SecondPoint;
      }
    }

    private void GetSecondPoint()
    {
      INumberDialog numDialog = new NumberDialogClass();
      // Set the second point equal to the active point which may have been snapped 
      m_secondPoint = m_activePoint;

      // Get the angle
      if (numDialog.DoModal("Angle 2", -45, 2, m_editor.Display.hWnd))
      {
        m_secondAngle = numDialog.Value * Math.PI / 180;
      }
      else
      {
        m_etoolPhase = ToolPhase.Inactive;
        return;
      }

      // Get the intersection point
      IConstructPoint constructPoint = new PointClass();
      constructPoint.ConstructAngleIntersection(m_firstPoint, m_firstAngle, m_secondPoint, m_secondAngle);

      IPoint point = constructPoint as IPoint;
      if (point.IsEmpty)
      {
        m_etoolPhase = ToolPhase.Inactive;
        MessageBox.Show("No Point Calculated");
        return;
      }

      // Draw the calculated intersection point and erase previous snap feedback
      m_activePoint = point;
      m_etoolPhase = ToolPhase.Intersection;
      m_snappingFeedback.Update(null, 0);
      DrawPoint(m_activePoint);
    }

    private void GetIntersection()
    {
      IEditSketch editSketch = m_editor as IEditSketch;
      editSketch.AddPoint(m_activePoint, true);
      // Set the phase to inactive, back to beginning
      m_etoolPhase = ToolPhase.Inactive;
    }

    private void DrawPoint(IPoint pPoint)
    {
      //Draw a red graphic dot on the display at the given point location
      IRgbColor color = null;
      ISimpleMarkerSymbol marker = null;
      IAppDisplay appDisplay = m_editor.Display as IAppDisplay;
      
      //Set the symbol (red, size 8)
      color = new RgbColor();
      color.Red = 255;
      color.Green = 0;
      color.Blue = 0;
      marker = new SimpleMarkerSymbol();
      marker.Color = color;
      marker.Size = 8;

      //Draw the point
      appDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);
      appDisplay.SetSymbol(marker as ISymbol);
      appDisplay.DrawPoint(pPoint);
      appDisplay.FinishDrawing();
    }

    #region IPersist Members

    public void GetClassID(out Guid pClassID)
    {
      //Explicitly set a guid. Used to set command.checked property
      pClassID = new Guid("edb83080-999d-11de-8a39-0800200c9a66");
    }

    #endregion
  }
}
