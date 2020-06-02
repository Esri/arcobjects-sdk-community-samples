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
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace PointsAlongLine
{
  /// <summary>
  /// Summary description for PointsAlongLine.
  /// </summary>
  [Guid("1f7cc037-07c1-497d-83a5-bfdd98eb8dd8")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("PointsAlongLine.PointsAlongLine")]
  public sealed class PointsAlongLineTool : BaseTool, IShapeConstructorTool, ISketchTool
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
      FeatureConstructionPointTools.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);
      FeatureConstructionPointTools.Unregister(regKey);

    }

    #endregion
    #endregion

    private IApplication m_application;
    private IEditor3 m_editor;
    private IEditEvents_Event m_editEvents;
    private IEditEvents5_Event m_editEvents5;
    private IEditSketch3 m_edSketch;
    private IShapeConstructor m_csc;

    private PointsAlongLineForm m_form;

    public PointsAlongLineTool()
    {
      base.m_category = "Developer Samples"; //localizable text 
      base.m_caption = "Points along a line";  //localizable text 
      base.m_message = "Creates points at regular intervals along a sketch";  //localizable text
      base.m_toolTip = "Points along a line tool";  //localizable text
      base.m_name = "DeveloperSamples_PointsAlongLine";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
        //base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

    #region ISketchTool Members
    //pass to constructor
    public void AddPoint(IPoint point, bool Clone, bool allowUndo)
    {
      m_csc.AddPoint(point, Clone, allowUndo);
    }

    public IPoint Anchor
    {
      get { return m_csc.Anchor; }
    }

    public double AngleConstraint
    {
      get { return m_csc.AngleConstraint; }
      set { m_csc.AngleConstraint = value; }
    }

    public esriSketchConstraint Constraint
    {
      get { return m_csc.Constraint; }
      set { m_csc.Constraint = value; }
    }

    public double DistanceConstraint
    {
      get { return m_csc.DistanceConstraint; }
      set { m_csc.DistanceConstraint = value; }
    }

    public bool IsStreaming
    {
      get { return m_csc.IsStreaming; }
      set { m_csc.IsStreaming = value; }
    }

    public IPoint Location
    {
      get { return m_csc.Location; }
    }

    #endregion

    #region ITool Members
    //pass to constructor
    public override void OnMouseDown(int Button, int Shift, int X, int Y)
    {
      m_csc.OnMouseDown(Button, Shift, X, Y);
    }

    public override void OnMouseMove(int Button, int Shift, int X, int Y)
    {
      m_csc.OnMouseMove(Button, Shift, X, Y);
    }

    public override void OnMouseUp(int Button, int Shift, int X, int Y)
    {
      m_csc.OnMouseUp(Button, Shift, X, Y);
    }

    public override bool OnContextMenu(int X, int Y)
    {
      return m_csc.OnContextMenu(X, Y);
    }

    public override void OnKeyDown(int keyCode, int Shift)
    {
      m_csc.OnKeyDown(keyCode, Shift);
    }

    public override void OnKeyUp(int keyCode, int Shift)
    {
      m_csc.OnKeyUp(keyCode, Shift);
    }

    public override void Refresh(int hDC)
    {
      m_csc.Refresh(hDC);
    }

    public override int Cursor
    {
      get { return m_csc.Cursor; }
    }

    public override void OnDblClick()
    {
      if (Control.ModifierKeys == Keys.Shift)
      {
        ISketchOperation pso = new SketchOperation();
        pso.MenuString_2 = "Finish Sketch Part";
        pso.Start(m_editor);
        m_edSketch.FinishSketchPart();
        pso.Finish(null);
      }
      else
        m_edSketch.FinishSketch();
    }

    public override bool Deactivate()
    {
      //unsubscribe events
      m_editEvents.OnSketchModified -= m_editEvents_OnSketchModified;
      m_editEvents5.OnShapeConstructorChanged -= m_editEvents5_OnShapeConstructorChanged;
      m_editEvents.OnSketchFinished -= m_editEvents_OnSketchFinished;
      return base.Deactivate();
    }

    #endregion

    /// <summary>
    /// Occurs when this tool is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      m_application = hook as IApplication;

      //get the editor
      UID editorUid = new UID();
      editorUid.Value = "esriEditor.Editor";
      m_editor = m_application.FindExtensionByCLSID(editorUid) as IEditor3;
      m_editEvents = m_editor as IEditEvents_Event;
      m_editEvents5 = m_editor as IEditEvents5_Event;
    }

    /// <summary>
    /// Occurs when this tool is clicked
    /// </summary>
    public override void OnClick()
    {
      m_edSketch = m_editor as IEditSketch3;

      //Restrict to line constructors (for this tool)
      m_edSketch.GeometryType = esriGeometryType.esriGeometryPolyline;

      //Activate a shape constructor based on the current sketch geometry
      if (m_edSketch.GeometryType == esriGeometryType.esriGeometryPoint)
        m_csc = new PointConstructorClass();
      else
        m_csc = new StraightConstructorClass();
      m_csc.Initialize(m_editor);
      m_edSketch.ShapeConstructor = m_csc;
      m_csc.Activate();

      //set the current task to null
      m_editor.CurrentTask = null;

      //setup events
      m_editEvents.OnSketchModified += new IEditEvents_OnSketchModifiedEventHandler(m_editEvents_OnSketchModified);
      m_editEvents5.OnShapeConstructorChanged += new IEditEvents5_OnShapeConstructorChangedEventHandler(m_editEvents5_OnShapeConstructorChanged);
      m_editEvents.OnSketchFinished += new IEditEvents_OnSketchFinishedEventHandler(m_editEvents_OnSketchFinished);
    }

    void m_editEvents_OnSketchFinished()
    {
      //send a space to hide the construction toolbar
      SendKeys.SendWait(" ");

      //Create form and pass initialization parameters
      m_form = new PointsAlongLineForm(m_editor);

      //Show the dialog modal
      m_form.ShowDialog();
    }

    private void m_editEvents_OnSketchModified()
    {
      m_csc.SketchModified();
    }

    private void m_editEvents5_OnShapeConstructorChanged()
    {
      //activate new constructor
      m_csc.Deactivate();
      m_csc = null;
      m_csc = m_edSketch.ShapeConstructor;
      m_csc.Activate();
    }
  }
}
