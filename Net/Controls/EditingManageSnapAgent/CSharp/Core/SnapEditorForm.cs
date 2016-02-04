/*

   Copyright 2016 Esri

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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF.CATIDs;

namespace Core
{
  public partial class SnapEditor : Form
  {
    IEngineEditor editor;
    IEngineEditLayers editLayers;
    IEngineSnapEnvironment snapEnvironment;
       
    public SnapEditor()
    {
      InitializeComponent();

      //get the snapEnvironment
      editor = new EngineEditorClass();
      editLayers = editor as IEngineEditLayers;
      snapEnvironment = editor as IEngineSnapEnvironment;    
  
      //Update this form with the snap environment and snap tips
      //Note: from here on we expect all snap changes to be made via this form, so it is not updated after direct changes to the snapping 
      //environment or the Arc Engine Snap Window. 
      RefreshDisplay();
    }

    /// <summary>
    /// Update the window with the current snap environment and snap tip settings
    /// </summary>
    private void RefreshDisplay()
    {
      snapTolerance.Text = snapEnvironment.SnapTolerance.ToString();
      snapTolUnits.SelectedIndex = (int)snapEnvironment.SnapToleranceUnits;      
      snapTips.Checked = ((IEngineEditProperties2)editor).SnapTips;
      
      //remove all feature snap agents from the data grid view
      snapAgents.Rows.Clear();
               
      //display feature snap and snap agents that are active
      for (int i = 0; i < snapEnvironment.SnapAgentCount; i++)
      {
        try
        {
          IEngineSnapAgent snapAgent = snapEnvironment.get_SnapAgent(i);
          IEngineFeatureSnapAgent ftrSnapAgent = snapAgent as IEngineFeatureSnapAgent;

          if (ftrSnapAgent != null)
          {
            //for feature snap agents add a row to the data view grid 
            esriGeometryHitPartType hitType = ftrSnapAgent.HitType;
            bool vertex, edge, end;
            vertex = (hitType & esriGeometryHitPartType.esriGeometryPartVertex) == esriGeometryHitPartType.esriGeometryPartVertex;
            edge = (hitType & esriGeometryHitPartType.esriGeometryPartBoundary) == esriGeometryHitPartType.esriGeometryPartBoundary;
            end = (hitType & esriGeometryHitPartType.esriGeometryPartEndpoint) == esriGeometryHitPartType.esriGeometryPartEndpoint;
            string vertexString = vertex ? "vertex" : "      ";
            string edgeString = edge ? " edge" : "     ";
            string endString = end ? " end  " : "     ";
            string hitTypes = vertexString + edgeString + endString;
            object[] rowData = { snapAgent.Name.ToString(), ftrSnapAgent.FeatureClass.AliasName, hitTypes };
            snapAgents.Rows.Add(rowData);
          }
          else
          {
            //add the active edit sketch snap agents    
            object[] rowData = { snapAgent.Name.ToString(), "<not applicable>", "<not applicable>" };
            snapAgents.Rows.Add(rowData);
          }
        }
        catch (Exception)
        {
        }

      }
    }

    #region Button Handlers
    private void clearAgents_Click(object sender, EventArgs e)
    {
      snapEnvironment.ClearSnapAgents();
            
      //refresh this window
      RefreshDisplay();
    }

    /// <summary>
    /// Turns off feature snap agents and turns off edit sketch snap agents by removing them
    /// </summary>
    /// <remarks>
    /// Using Clear or Remove feature snap agents can be deactivated so that the user can't enable them.
    /// Here we simply turn off all the agents, which will allow the user to turn them on.
    /// </remarks>   
    private void turnOffAgents_Click(object sender, EventArgs e)
    {
      TurnOffAgents();       
    }
    
    private void reverseAgentsPriority_Click(object sender, EventArgs e)
    {
      //get all the snap agents in reverse order and then deactivate them
      ArrayList snapAgentList = new ArrayList();

      for (int i = snapEnvironment.SnapAgentCount - 1; i >= 0; i--)
      {
        IEngineSnapAgent tmpAgent = snapEnvironment.get_SnapAgent(i);
        snapAgentList.Add(tmpAgent);
        snapEnvironment.RemoveSnapAgent(i);
      }

      //add the agents back to the environment
      foreach (IEngineSnapAgent agent in snapAgentList)
      {
        snapEnvironment.AddSnapAgent(agent);
      }

      //refresh this window
      RefreshDisplay();
    }  

    /// <summary>
    /// Adds a feature snap agent for the target layer and turn on  all options.
    /// </summary>
    /// <remarks>
    /// This method does not check if there already exists a feature snap agent for the 
    /// target layer. It is recommended that you do so since duplicate agents can appear
    /// on the Snap Settings Form
    /// </remarks>    
    private void addFeatureSnapAgent_Click(object sender, EventArgs e)
    {
      IEngineFeatureSnapAgent featureSnapAgent = new EngineFeatureSnap();
      
      if (editLayers.TargetLayer == null)
      {
        System.Windows.Forms.MessageBox.Show("Please start an edit session");
        return;
      }
      
      featureSnapAgent.FeatureClass = editLayers.TargetLayer.FeatureClass; ;
      featureSnapAgent.HitType = esriGeometryHitPartType.esriGeometryPartVertex | esriGeometryHitPartType.esriGeometryPartBoundary | esriGeometryHitPartType.esriGeometryPartEndpoint;
      snapEnvironment.AddSnapAgent(featureSnapAgent); 
     
      //refresh this window
      RefreshDisplay();
    }

    /// <summary>
    /// Adds, and hence turns on, Edit Sketch snap agents
    /// </summary>
    private void addSketchSnapAgent_Click(object sender, EventArgs e)
    {
      AddSketchSnapAgents();
      RefreshDisplay();
    }  
    #endregion

    #region Snap Tips and Tolerance Handlers
    private void snapTips_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        //turn snap tips on/off      
        ((IEngineEditProperties2)editor).SnapTips = snapTips.Checked;
      }
      catch
      {
        snapTips.Checked = ((IEngineEditProperties2)editor).SnapTips;
      }
    }
       
    private void snapTolerance_TypeValidationEventHandler(object sender, TypeValidationEventArgs e)
    {
      try
      {
        snapEnvironment.SnapTolerance = Convert.ToDouble(snapTolerance.Text);
      }
      catch
      {
        snapTolerance.Text = snapEnvironment.SnapTolerance.ToString();
      }      
    }

    private void snapTolUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        snapEnvironment.SnapToleranceUnits = (esriEngineSnapToleranceUnits)snapTolUnits.SelectedIndex;
      }
      catch
      {
        snapTolUnits.SelectedIndex = (int)snapEnvironment.SnapToleranceUnits;
      }
    }
    #endregion
            
    #region Helper Methods
    /// <summary>
    /// Adds, and hence turns on, Edit Sketch snap agents
    /// </summary>
    private void AddSketchSnapAgents()
    {
      //give anchor snap (i.e. vertex snap) priority to get more accurate snap tips while snapped to a vertex
      Type t = Type.GetTypeFromProgID("esriControls.EngineAnchorSnap");
      System.Object obj = Activator.CreateInstance(t);
      IEngineSnapAgent snapAgent = (IEngineSnapAgent)obj;
      snapEnvironment.AddSnapAgent(snapAgent);

      //edge sketch edges
      t = Type.GetTypeFromProgID("esriControls.EngineSketchSnap");
      obj = Activator.CreateInstance(t);
      snapAgent = (IEngineSnapAgent)obj;
      snapEnvironment.AddSnapAgent(snapAgent);

      //perpendicular to edit sketch
      t = Type.GetTypeFromProgID("esriControls.EnginePerpendicularSnap");
      obj = Activator.CreateInstance(t);
      snapAgent = (IEngineSnapAgent)obj;
      snapEnvironment.AddSnapAgent(snapAgent);
    }

    /// <summary>
    /// Turns off feature snap agents and turns off edit sketch snap agents by removing them
    /// </summary>
    /// <remarks>
    /// Using Clear or Remove feature snap agents can be deactivated so that the user can't enable them.
    /// Here we simply turn off all the agents, which will allow the user to turn them on.
    /// </remarks>   
    private void TurnOffAgents()
    {
      for (int i = snapEnvironment.SnapAgentCount - 1; i >= 0; i--)
      {
        IEngineSnapAgent snapAgent = snapEnvironment.get_SnapAgent(i);
        IEngineFeatureSnapAgent ftrSnapAgent = snapAgent as IEngineFeatureSnapAgent;
        if (ftrSnapAgent != null)
        {
          //turn off the feature snap agent
          ftrSnapAgent.HitType = esriGeometryHitPartType.esriGeometryPartNone;
        }
        else
        {
          //there is no way to turn snap agents off, they must be removed 
          snapEnvironment.RemoveSnapAgent(i);
        }
      }

      //refresh this window
      RefreshDisplay();
    }
    #endregion
  }
}