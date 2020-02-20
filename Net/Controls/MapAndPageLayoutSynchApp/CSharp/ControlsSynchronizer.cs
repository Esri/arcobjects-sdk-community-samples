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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace MapAndPageLayoutSynchApp
{
  /// <summary>
  /// This class is used to synchronize a given PageLayoutControl and a MapControl.
  /// When initialized, the user must pass the reference of these control to the class, bind
  /// the control together by calling 'BindControls' which in turn sets a joined Map referenced
  /// by both control; and set all the buddy controls joined between these two controls.
  /// When alternating between the MapControl and PageLayoutControl, you should activate the visible control 
  /// and deactivate the other by calling ActivateXXX.
  /// This class is limited to a situation where the controls are not simultaneously visible. 
  /// </summary>
  public class ControlsSynchronizer
  {
    #region class members
    private IMapControl3 m_mapControl = null;
    private IPageLayoutControl2 m_pageLayoutControl = null;
    private ITool m_mapActiveTool = null;
    private ITool m_pageLayoutActiveTool = null;
    private bool m_IsMapCtrlactive = true;

    private ArrayList m_frameworkControls = null;
    #endregion

    #region constructor

    /// <summary>
    /// default constructor
    /// </summary>
    public ControlsSynchronizer()
    {
      //initialize the underlying ArrayList
      m_frameworkControls = new ArrayList();
    }

    /// <summary>
    /// class constructor
    /// </summary>
    /// <param name="mapControl"></param>
    /// <param name="pageLayoutControl"></param>
    public ControlsSynchronizer(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl)
      : this()
    {
      //assign the class members
      m_mapControl = mapControl;
      m_pageLayoutControl = pageLayoutControl;
    }
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the MapControl
    /// </summary>
    public IMapControl3 MapControl
    {
      get { return m_mapControl; }
      set { m_mapControl = value; }
    }

    /// <summary>
    /// Gets or sets the PageLayoutControl
    /// </summary>
    public IPageLayoutControl2 PageLayoutControl
    {
      get { return m_pageLayoutControl; }
      set { m_pageLayoutControl = value; }
    }

    /// <summary>
    /// Get an indication of the type of the currently active view
    /// </summary>
    public string ActiveViewType
    {
      get
      {
        if (m_IsMapCtrlactive)
          return "MapControl";
        else
          return "PageLayoutControl";
      }
    }

    /// <summary>
    /// get the active control
    /// </summary>
    public object ActiveControl
    {
      get
      {
        if (m_mapControl == null || m_pageLayoutControl == null)
          throw new Exception("ControlsSynchronizer::ActiveControl:\r\nEither MapControl or PageLayoutControl are not initialized!");

        if (m_IsMapCtrlactive)
          return m_mapControl.Object;
        else
          return m_pageLayoutControl.Object;
      }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Activate the MapControl and deactivate the PagleLayoutControl
    /// </summary>
    public void ActivateMap()
    {
      try
      {
        if (m_pageLayoutControl == null || m_mapControl == null)
          throw new Exception("ControlsSynchronizer::ActivateMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

        //cache the current tool of the PageLayoutControl
        if (m_pageLayoutControl.CurrentTool != null) m_pageLayoutActiveTool = m_pageLayoutControl.CurrentTool;

        //deactivate the PagleLayout
        m_pageLayoutControl.ActiveView.Deactivate();

        //activate the MapControl
        m_mapControl.ActiveView.Activate(m_mapControl.hWnd);

        //assign the last active tool that has been used on the MapControl back as the active tool
        if (m_mapActiveTool != null) m_mapControl.CurrentTool = m_mapActiveTool;

        m_IsMapCtrlactive = true;

        //on each of the framework controls, set the Buddy control to the MapControl
        this.SetBuddies(m_mapControl.Object);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", ex.Message));
      }
    }

    /// <summary>
    /// Activate the PagleLayoutControl and deactivate the MapCotrol
    /// </summary>
    public void ActivatePageLayout()
    {
      try
      {
        if (m_pageLayoutControl == null || m_mapControl == null)
          throw new Exception("ControlsSynchronizer::ActivatePageLayout:\r\nEither MapControl or PageLayoutControl are not initialized!");

        //cache the current tool of the MapControl
        if (m_mapControl.CurrentTool != null) m_mapActiveTool = m_mapControl.CurrentTool;

        //deactivate the MapControl
        m_mapControl.ActiveView.Deactivate();

        //activate the PageLayoutControl
        m_pageLayoutControl.ActiveView.Activate(m_pageLayoutControl.hWnd);

        //assign the last active tool that has been used on the PageLayoutControl back as the active tool
        if (m_pageLayoutActiveTool != null) m_pageLayoutControl.CurrentTool = m_pageLayoutActiveTool;

        m_IsMapCtrlactive = false;

        //on each of the framework controls, set the Buddy control to the PageLayoutControl
        this.SetBuddies(m_pageLayoutControl.Object);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", ex.Message));
      }
    }

    /// <summary>
    /// given a new map, replaces the PageLayoutControl and the MapControl's focus map
    /// </summary>
    /// <param name="newMap"></param>
    public void ReplaceMap(IMap newMap)
    {
      if (newMap == null)
        throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");

      if (m_pageLayoutControl == null || m_mapControl == null)
        throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");

      //create a new instance of IMaps collection which is needed by the PageLayout
      IMaps maps = new Maps();
      //add the new map to the Maps collection
      maps.Add(newMap);

      bool bIsMapActive = m_IsMapCtrlactive;

      //call replace map on the PageLayout in order to replace the focus map
      //we must call ActivatePageLayout, since it is the control we call 'ReplaceMaps'
      this.ActivatePageLayout();
      m_pageLayoutControl.PageLayout.ReplaceMaps(maps);

      //assign the new map to the MapControl
      m_mapControl.Map = newMap;

      //reset the active tools
      m_pageLayoutActiveTool = null;
      m_mapActiveTool = null;

      //make sure that the last active control is activated
      if (bIsMapActive)
      {
        this.ActivateMap();
        m_mapControl.ActiveView.Refresh();
      }
      else
      {
        this.ActivatePageLayout();
        m_pageLayoutControl.ActiveView.Refresh();
      }
    }

    /// <summary>
    /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map
    /// </summary>
    /// <param name="mapControl"></param>
    /// <param name="pageLayoutControl"></param>
    /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
    public void BindControls(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl, bool activateMapFirst)
    {
      if (mapControl == null || pageLayoutControl == null)
        throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");

      m_mapControl = MapControl;
      m_pageLayoutControl = pageLayoutControl;

      this.BindControls(activateMapFirst);
    }

    /// <summary>
    /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map 
    /// </summary>
    /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
    public void BindControls(bool activateMapFirst)
    {
      if (m_pageLayoutControl == null || m_mapControl == null)
        throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");

      //create a new instance of IMap
      IMap newMap = new MapClass();
      newMap.Name = "Map";

      //create a new instance of IMaps collection which is needed by the PageLayout
      IMaps maps = new Maps();
      //add the new Map instance to the Maps collection
      maps.Add(newMap);

      //call replace map on the PageLayout in order to replace the focus map
      m_pageLayoutControl.PageLayout.ReplaceMaps(maps);
      //assign the new map to the MapControl
      m_mapControl.Map = newMap;

      //reset the active tools
      m_pageLayoutActiveTool = null;
      m_mapActiveTool = null;

      //make sure that the last active control is activated
      if (activateMapFirst)
        this.ActivateMap();
      else
        this.ActivatePageLayout();
    }

    /// <summary>
    ///by passing the application's toolbars and TOC to the synchronization class, it saves you the
    ///management of the buddy control each time the active control changes. This method ads the framework
    ///control to an array; once the active control changes, the class iterates through the array and 
    ///calls SetBuddyControl on each of the stored framework control.
    /// </summary>
    /// <param name="control"></param>
    public void AddFrameworkControl(object control)
    {
      if (control == null)
        throw new Exception("ControlsSynchronizer::AddFrameworkControl:\r\nAdded control is not initialized!");

      m_frameworkControls.Add(control);
    }

    /// <summary>
    /// Remove a framework control from the managed list of controls
    /// </summary>
    /// <param name="control"></param>
    public void RemoveFrameworkControl(object control)
    {
      if (control == null)
        throw new Exception("ControlsSynchronizer::RemoveFrameworkControl:\r\nControl to be removed is not initialized!");

      m_frameworkControls.Remove(control);
    }

    /// <summary>
    /// Remove a framework control from the managed list of controls by specifying its index in the list
    /// </summary>
    /// <param name="index"></param>
    public void RemoveFrameworkControlAt(int index)
    {
      if (m_frameworkControls.Count < index)
        throw new Exception("ControlsSynchronizer::RemoveFrameworkControlAt:\r\nIndex is out of range!");

      m_frameworkControls.RemoveAt(index);
    }

    /// <summary>
    /// when the active control changes, the class iterates through the array of the framework controls
    ///  and calls SetBuddyControl on each of the controls.
    /// </summary>
    /// <param name="buddy">the active control</param>
    private void SetBuddies(object buddy)
    {
      try
      {
        if (buddy == null)
          throw new Exception("ControlsSynchronizer::SetBuddies:\r\nTarget Buddy Control is not initialized!");

        foreach (object obj in m_frameworkControls)
        {
          if (obj is IToolbarControl)
          {
            ((IToolbarControl)obj).SetBuddyControl(buddy);
          }
          else if (obj is ITOCControl)
          {
            ((ITOCControl)obj).SetBuddyControl(buddy);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("ControlsSynchronizer::SetBuddies:\r\n{0}", ex.Message));
      }
    }
    #endregion
  }
}
