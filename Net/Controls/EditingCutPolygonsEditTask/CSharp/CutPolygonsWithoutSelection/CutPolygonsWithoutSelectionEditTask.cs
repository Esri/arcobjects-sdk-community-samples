using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Text;

using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace CutPolygonsWithoutSelection_CS
{
  [Guid("637BF886-E416-4bcc-92A5-C0E455739346")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("CutPolygonsWithoutSelection_CS.CutPolygonWithoutSelection")]
  public class CutPolygonsWithoutSelectionEditTask : ESRI.ArcGIS.Controls.IEngineEditTask
  {
    #region Private Members
    IEngineEditor m_engineEditor;
    IEngineEditSketch m_editSketch;
    IEngineEditLayers m_editLayer;
    #endregion

    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      EngineEditTasks.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      EngineEditTasks.Unregister(regKey);

    }

    #endregion
    #endregion

    #region IEngineEditTask Implementations
    public void Activate(ESRI.ArcGIS.Controls.IEngineEditor editor, ESRI.ArcGIS.Controls.IEngineEditTask oldTask)
    {      
        if (editor == null)
          return;

        //Initialize class member variables.
        m_engineEditor = editor;
        m_editSketch = editor as IEngineEditSketch;
        m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline;
        m_editLayer = m_editSketch as IEngineEditLayers;

        //Wire editor events.        
        ((IEngineEditEvents_Event)m_editSketch).OnTargetLayerChanged += new IEngineEditEvents_OnTargetLayerChangedEventHandler(OnTargetLayerChanged);
        ((IEngineEditEvents_Event)m_editSketch).OnCurrentTaskChanged += new IEngineEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
    }

    public void Deactivate()
    {
      //Stop listening for editor events.
      ((IEngineEditEvents_Event)m_engineEditor).OnTargetLayerChanged -= OnTargetLayerChanged;
      ((IEngineEditEvents_Event)m_engineEditor).OnCurrentTaskChanged -= OnCurrentTaskChanged;

      //Release object references.
      m_engineEditor = null;
      m_editSketch = null;
      m_editLayer = null;
    }

    public string GroupName
    {
      get
      {
        return "Modify Tasks";
      }
    }

    public string Name
    {
      get
      {
        return "Cut Polygons Without Selection (C#)";
      }
    }

    //This method is not expected to be fired since we have unregistered from the event in Deactivate
    public void OnCurrentTaskChanged()
    {
      UpdateSketchToolStatus();
    }

    public void OnTargetLayerChanged()
    {
      UpdateSketchToolStatus();
    }

    public void OnDeleteSketch()
    { 
    }
    
    public void OnFinishSketch()
    {
      if (m_editSketch == null)
        return;

      bool hasCutPolygons = false;

      //Change the cursor to be hourglass shape.
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

      try
      {
        //Get the geometry that performs the cut from the edit sketch.
        IGeometry cutGeometry = m_editSketch.Geometry;

        //The sketch geometry is simplified to deal with a multi-part sketch as well
        //as the case where the sketch loops back over itself.
        ITopologicalOperator2 topoOperator = cutGeometry as ITopologicalOperator2;
        topoOperator.IsKnownSimple_2 = false;
        topoOperator.Simplify();

        //Create the spatial filter to search for features in the target feature class.
        //The spatial relationship we care about is whether the interior of the line 
        //intersects the interior of the polygon.
        ISpatialFilter spatialFilter = new SpatialFilterClass();
        spatialFilter.Geometry = m_editSketch.Geometry;
        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

        //Find the polygon features that cross the sketch.
        IFeatureClass featureClass = m_editLayer.TargetLayer.FeatureClass;
        IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

        //Only do work if there are features that intersect the edit sketch.
        IFeature origFeature = featureCursor.NextFeature();
        if (origFeature != null)
        {
          //Check the first feature to see if it is ZAware and if it needs to make the
          //cut geometry ZAware.
          IZAware zAware = origFeature.Shape as IZAware;
          if (zAware.ZAware)
          {
            zAware = cutGeometry as IZAware;
            zAware.ZAware = true;
          }

          ArrayList comErrors = new ArrayList();       
            
          //Start an edit operation so we can have undo/redo.
          m_engineEditor.StartOperation();

          //Cycle through the features, cutting with the sketch.
          while (origFeature != null)
          {
            try
            { 
              //Split the feature. Use the IFeatureEdit::Split method which ensures
              //the attributes are correctly dealt with.
              IFeatureEdit featureEdit = origFeature as IFeatureEdit;
              //Set to hold the new features that are created by the Split.            
              ISet newFeaturesSet = featureEdit.Split(cutGeometry);

              //New features have been created.
              if (newFeaturesSet != null)
              {
                newFeaturesSet.Reset();
                hasCutPolygons = true;
              }
            }
            catch (COMException comExc)
            {
                comErrors.Add(String.Format("OID: {0}, Error: {1} , {2}",origFeature.OID.ToString(), comExc.ErrorCode,comExc.Message));
            }
            finally
            {
              //Continue to work on the next feature if it fails to split the current one.
              origFeature = featureCursor.NextFeature();
            }
          }
          //If any polygons were cut, refresh the display and stop the edit operation.
          if (hasCutPolygons)
          {
            //Clear the map's selection.
            m_engineEditor.Map.ClearSelection();

            //Refresh the display including modified layer and any previously selected component. 
            IActiveView activeView = m_engineEditor.Map as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);

            //Complete the edit operation.
            m_engineEditor.StopOperation("Cut Polygons Without Selection");
          }
          else
          {
            m_engineEditor.AbortOperation();
          }

          //report any errors that have arisen while splitting features
          if (comErrors.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder("The following features could not be split: \n", 200);
            foreach (string comError in comErrors)
            {
              stringBuilder.AppendLine(comError);
            }

            MessageBox.Show(stringBuilder.ToString(), "Cut Errors");
          }
        }
      }
      catch (Exception e)
      {
        MessageBox.Show("Unable to perform the cut task.\n" + e.Message);
        m_engineEditor.AbortOperation();
      }
      finally
      {
        //Change the cursor shape to default.
        System.Windows.Forms.Cursor.Current = Cursors.Default;
      }
    }

    public string UniqueName
    {
      get
      {
        return "CutPolygonsWithoutSelection_CS_CutPolygonsWithoutSelection_CSharp";
      }
    }
    #endregion

    #region IEngineEditTask private methods
    private void UpdateSketchToolStatus()
    {
      if (m_editLayer == null)
        return;

      //Only enable the sketch tool if there is a polygon target layer.
      if (m_editLayer.TargetLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
        m_editSketch.GeometryType = esriGeometryType.esriGeometryNull;
      else
        //Set the edit sketch geometry type to be esriGeometryPolyline.
        m_editSketch.GeometryType = esriGeometryType.esriGeometryPolyline;
    }
    #endregion

  }
}
