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
using ESRI.ArcGIS.Editor;

namespace Events
{
  public class EventListener
  {
    #region Enums
    //contains all edit events listed on IEditEvents through IEditEvents4
    public enum EditorEvent
    {
      AfterDrawSketch,
      OnChangeFeature,
      OnConflictsDetected,
      OnCreateFeature, 
      OnCurrentLayerChanged,
      OnCurrentTaskChanged,
      OnDeleteFeature,
      OnRedo,
      OnSelectionChanged,
      OnSketchFinished,
      OnSketchModified,
      OnStartEditing,
      OnStopEditing,
      OnUndo,
      BeforeStopEditing,
      BeforeStopOperation,
      OnVertexAdded,
      OnVertexMoved,
      OnVertexDeleted,
      BeforeDrawSketch,
      OnAngularCorrectionOffsetChanged,
      OnDistanceCorrectionFactorChanged,
      OnUseGroundToGridChanged,
    };
    #endregion

    #region Constructors
    public EventListener(IEditor editor)
    {
      if (editor == null)
      {
        throw new ArgumentNullException();
      }

      m_editor = editor;

    }
    public EventListener(IEditor editor, EditorEvent editEvent)
    {
      if (editor == null)
      {
        throw new ArgumentNullException();
      }

      m_editor = editor;

    }
    EventListener(IEditor editor, bool bListenAll)
    {
      if (editor == null)
      {
        throw new ArgumentNullException();
      }

      m_editor = editor;
    }
    #endregion

    public void ListenToEvents(EditorEvent editEvent, bool start)
    {
      switch (editEvent)
      {
        case EditorEvent.AfterDrawSketch:
          if (start)
            ((IEditEvents_Event)m_editor).AfterDrawSketch += new 
              IEditEvents_AfterDrawSketchEventHandler(EventListener_AfterDrawSketch);
          else
            ((IEditEvents_Event)m_editor).AfterDrawSketch -= new
              IEditEvents_AfterDrawSketchEventHandler(EventListener_AfterDrawSketch);
          break;
        case EditorEvent.OnChangeFeature:
          if (start)
            ((IEditEvents_Event)m_editor).OnChangeFeature += new 
              IEditEvents_OnChangeFeatureEventHandler(EventListener_OnChangeFeature); 
          else
            ((IEditEvents_Event)m_editor).OnChangeFeature -= new
              IEditEvents_OnChangeFeatureEventHandler(EventListener_OnChangeFeature);
          break;
        case EditorEvent.OnConflictsDetected:
          if (start)
            ((IEditEvents_Event)m_editor).OnConflictsDetected += new 
              IEditEvents_OnConflictsDetectedEventHandler(EventListener_OnConflictsDetected);
          else
            ((IEditEvents_Event)m_editor).OnConflictsDetected -= new
              IEditEvents_OnConflictsDetectedEventHandler(EventListener_OnConflictsDetected);
          break;
        case EditorEvent.OnCreateFeature:
          if (start)
            ((IEditEvents_Event)m_editor).OnCreateFeature += new 
              IEditEvents_OnCreateFeatureEventHandler(EventListener_OnCreateFeature);
          else
            ((IEditEvents_Event)m_editor).OnCreateFeature -= new
              IEditEvents_OnCreateFeatureEventHandler(EventListener_OnCreateFeature);
          break;

        case EditorEvent.OnCurrentLayerChanged:
          if (start)
            ((IEditEvents_Event)m_editor).OnCurrentLayerChanged += new 
              IEditEvents_OnCurrentLayerChangedEventHandler(EventListener_OnCurrentLayerChanged); 
          else
            ((IEditEvents_Event)m_editor).OnCurrentLayerChanged -= new
              IEditEvents_OnCurrentLayerChangedEventHandler(EventListener_OnCurrentLayerChanged);
          break;
        case EditorEvent.OnCurrentTaskChanged:
          if (start)
          ((IEditEvents_Event)m_editor).OnCurrentTaskChanged += new 
            IEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
          else
          ((IEditEvents_Event)m_editor).OnCurrentTaskChanged -= new
            IEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
          break;
        case EditorEvent.OnDeleteFeature:
          if (start)
            ((IEditEvents_Event)m_editor).OnDeleteFeature += new 
              IEditEvents_OnDeleteFeatureEventHandler(EventListener_OnDeleteFeature);
          else
            ((IEditEvents_Event)m_editor).OnDeleteFeature -= new
              IEditEvents_OnDeleteFeatureEventHandler(EventListener_OnDeleteFeature);
          break;
        case EditorEvent.OnRedo:
          if (start)
            ((IEditEvents_Event)m_editor).OnRedo += new 
              IEditEvents_OnRedoEventHandler(EventListener_OnRedo); 
          else
            ((IEditEvents_Event)m_editor).OnRedo -= new
              IEditEvents_OnRedoEventHandler(EventListener_OnRedo);
          break;
        case EditorEvent.OnSelectionChanged:
          if (start)
            ((IEditEvents_Event)m_editor).OnSelectionChanged +=new 
              IEditEvents_OnSelectionChangedEventHandler(EventListener_OnSelectionChanged); 
          else
            ((IEditEvents_Event)m_editor).OnSelectionChanged -= new
              IEditEvents_OnSelectionChangedEventHandler(EventListener_OnSelectionChanged);
          break;
        case EditorEvent.OnSketchFinished:
          if (start)
            ((IEditEvents_Event)m_editor).OnSketchFinished += new 
              IEditEvents_OnSketchFinishedEventHandler(EventListener_OnSketchFinished);
          else
            ((IEditEvents_Event)m_editor).OnSketchFinished -= new
              IEditEvents_OnSketchFinishedEventHandler(EventListener_OnSketchFinished); 
          break;
        case EditorEvent.OnSketchModified:
          if (start)
            ((IEditEvents_Event)m_editor).OnSketchModified += new 
              IEditEvents_OnSketchModifiedEventHandler(EventListener_OnSketchModified);
          else
            ((IEditEvents_Event)m_editor).OnSketchModified -= new
              IEditEvents_OnSketchModifiedEventHandler(EventListener_OnSketchModified);
          break;
        case EditorEvent.OnStartEditing:
          if (start)
            ((IEditEvents_Event)m_editor).OnStartEditing += new
              IEditEvents_OnStartEditingEventHandler(OnStartEditing);
          else
            ((IEditEvents_Event)m_editor).OnStartEditing -= new
              IEditEvents_OnStartEditingEventHandler(OnStartEditing); 
          break;          
        case EditorEvent.OnStopEditing:
          if (start)
            ((IEditEvents_Event)m_editor).OnStopEditing += new 
              IEditEvents_OnStopEditingEventHandler(OnStopEditing);
          else
            ((IEditEvents_Event)m_editor).OnStopEditing -= new
              IEditEvents_OnStopEditingEventHandler(OnStopEditing); 
          break;
        case EditorEvent.OnUndo:
          if (start)
            ((IEditEvents_Event)m_editor).OnUndo += new 
              IEditEvents_OnUndoEventHandler(EventListener_OnUndo); 
          else
            ((IEditEvents_Event)m_editor).OnUndo -= new
              IEditEvents_OnUndoEventHandler(EventListener_OnUndo);
          break;
        case EditorEvent.BeforeStopEditing:
          if (start)
            ((IEditEvents2_Event)m_editor).BeforeStopEditing += new 
              IEditEvents2_BeforeStopEditingEventHandler(EventListener_BeforeStopEditing); 
          else
            ((IEditEvents2_Event)m_editor).BeforeStopEditing -= new
              IEditEvents2_BeforeStopEditingEventHandler(EventListener_BeforeStopEditing);
          break;
        case EditorEvent.BeforeStopOperation:
          if (start)
            ((IEditEvents2_Event)m_editor).BeforeStopOperation += new 
              IEditEvents2_BeforeStopOperationEventHandler(EventListener_BeforeStopOperation);
          else
            ((IEditEvents2_Event)m_editor).BeforeStopOperation -= new
              IEditEvents2_BeforeStopOperationEventHandler(EventListener_BeforeStopOperation);
          break;
        case EditorEvent.OnVertexAdded:
          if (start)
            ((IEditEvents2_Event)m_editor).OnVertexAdded +=new 
              IEditEvents2_OnVertexAddedEventHandler(EventListener_OnVertexAdded); 
          else
            ((IEditEvents2_Event)m_editor).OnVertexAdded -= new
              IEditEvents2_OnVertexAddedEventHandler(EventListener_OnVertexAdded);
          break;
        case EditorEvent.OnVertexMoved:
          if (start)
            ((IEditEvents2_Event)m_editor).OnVertexMoved += new 
              IEditEvents2_OnVertexMovedEventHandler(EventListener_OnVertexMoved); 
          else
            ((IEditEvents2_Event)m_editor).OnVertexMoved -= new
              IEditEvents2_OnVertexMovedEventHandler(EventListener_OnVertexMoved);
          break;
        case EditorEvent.OnVertexDeleted:
          if (start)
            ((IEditEvents2_Event)m_editor).OnVertexDeleted += new 
              IEditEvents2_OnVertexDeletedEventHandler(EventListener_OnVertexDeleted);              
          else
            ((IEditEvents2_Event)m_editor).OnVertexDeleted -= new
              IEditEvents2_OnVertexDeletedEventHandler(EventListener_OnVertexDeleted);
          break;
        case EditorEvent.BeforeDrawSketch:
          if (start)
            ((IEditEvents3_Event)m_editor).BeforeDrawSketch += new 
              IEditEvents3_BeforeDrawSketchEventHandler(EventListener_BeforeDrawSketch);              
          else
            ((IEditEvents3_Event)m_editor).BeforeDrawSketch -= new 
              IEditEvents3_BeforeDrawSketchEventHandler(EventListener_BeforeDrawSketch);              
          break;
        case EditorEvent.OnAngularCorrectionOffsetChanged:
          if (start)
            ((IEditEvents4_Event)m_editor).OnAngularCorrectionOffsetChanged += new 
              IEditEvents4_OnAngularCorrectionOffsetChangedEventHandler(EventListener_OnAngularCorrectionOffsetChanged);              
          else
            ((IEditEvents4_Event)m_editor).OnAngularCorrectionOffsetChanged -= new 
              IEditEvents4_OnAngularCorrectionOffsetChangedEventHandler(EventListener_OnAngularCorrectionOffsetChanged);              
          break;
        case EditorEvent.OnDistanceCorrectionFactorChanged:
          if (start)
            ((IEditEvents4_Event)m_editor).OnDistanceCorrectionFactorChanged += new 
              IEditEvents4_OnDistanceCorrectionFactorChangedEventHandler(EventListener_OnDistanceCorrectionFactorChanged); 
          else
            ((IEditEvents4_Event)m_editor).OnDistanceCorrectionFactorChanged -= new 
              IEditEvents4_OnDistanceCorrectionFactorChangedEventHandler(EventListener_OnDistanceCorrectionFactorChanged); 
          break;
        case EditorEvent.OnUseGroundToGridChanged:
          if (start)
            ((IEditEvents4_Event)m_editor).OnUseGroundToGridChanged += new 
              IEditEvents4_OnUseGroundToGridChangedEventHandler(EventListener_OnUseGroundToGridChanged);
        
          else
            ((IEditEvents4_Event)m_editor).OnUseGroundToGridChanged -= new 
              IEditEvents4_OnUseGroundToGridChangedEventHandler(EventListener_OnUseGroundToGridChanged);
          break;
        default:
          break;

      }

    }

    // Invoke the Changed event
    protected virtual void OnChanged(EditorEventArgs e)
    {
      if (Changed != null)
        Changed(this, e);
    }

    #region Event Handlers
    void EventListener_OnCreateFeature(ESRI.ArcGIS.Geodatabase.IObject obj)
    {
      EditorEventArgs e = new EditorEventArgs("OnCreateFeature");
      OnChanged(e); 
    }
    void EventListener_OnChangeFeature(ESRI.ArcGIS.Geodatabase.IObject obj)
    {
      EditorEventArgs e = new EditorEventArgs("OnChangeFeature");
      OnChanged(e); 
    }
    void EventListener_OnConflictsDetected()
    {
      EditorEventArgs e = new EditorEventArgs("OnConflictsDetected");
      OnChanged(e); 
    }
    void EventListener_OnCurrentLayerChanged()
    {
      EditorEventArgs e = new EditorEventArgs("OnCurrentLayerChanged");
      OnChanged(e); 
    }
    void EventListener_OnDeleteFeature(ESRI.ArcGIS.Geodatabase.IObject obj)
    {
      EditorEventArgs e = new EditorEventArgs("OnDeleteFeature");
      OnChanged(e); 
    }
    void EventListener_OnRedo()
    {
      EditorEventArgs e = new EditorEventArgs("OnRedo");
      OnChanged(e); 
    }
    void EventListener_OnSelectionChanged()
    {
      EditorEventArgs e = new EditorEventArgs("OnSelectionChanged");
      OnChanged(e); 
    }
    void EventListener_OnSketchFinished()
    {
      EditorEventArgs e = new EditorEventArgs("OnSketchFinished");
      OnChanged(e); 
    }
    void EventListener_OnSketchModified()
    {
      EditorEventArgs e = new EditorEventArgs("OnSketchModified");
      OnChanged(e); 
    }
    void EventListener_OnUndo()
    {
      EditorEventArgs e = new EditorEventArgs("OnUndo");
      OnChanged(e); 
    }
    void EventListener_BeforeStopEditing(bool save)
    {
      EditorEventArgs e = new EditorEventArgs("BeforeStopEditing");
      OnChanged(e); 
    }
    void EventListener_BeforeStopOperation()
    {
      EditorEventArgs e = new EditorEventArgs("BeforeStopOperation");
      OnChanged(e); 
    }
    void EventListener_OnVertexAdded(ESRI.ArcGIS.Geometry.IPoint point)
    {
      EditorEventArgs e = new EditorEventArgs("OnVertexAdded");
      OnChanged(e);     
    }
    void EventListener_OnVertexMoved(ESRI.ArcGIS.Geometry.IPoint point)
    {
      EditorEventArgs e = new EditorEventArgs("OnVertexMoved");
      OnChanged(e);    
    }
    void EventListener_OnVertexDeleted(ESRI.ArcGIS.Geometry.IPoint point)
    {
      EditorEventArgs e = new EditorEventArgs("OnVertexDeleted");
      OnChanged(e);    
    }
    void EventListener_OnAngularCorrectionOffsetChanged(double angOffset)
    {
      EditorEventArgs e = new EditorEventArgs("OnAngularCorrectionOffsetChanged");
      OnChanged(e);     
    }
    void EventListener_OnDistanceCorrectionFactorChanged(double distFactor)
    {
      EditorEventArgs e = new EditorEventArgs("OnDistanceCorrectionFactorChanged");
      OnChanged(e);    
    }
    void EventListener_OnUseGroundToGridChanged(bool g2g)
    {
      EditorEventArgs e = new EditorEventArgs("OnUseGroundToGridChanged");
      OnChanged(e); 
    }
    void EventListener_BeforeDrawSketch(ESRI.ArcGIS.Display.IDisplay pDpy)
    {
      EditorEventArgs e = new EditorEventArgs("BeforeDrawSketch");
      OnChanged(e); 
    }
    void EventListener_AfterDrawSketch(ESRI.ArcGIS.Display.IDisplay pDpy)
    {
      EditorEventArgs e = new EditorEventArgs("AfterDrawSketch");
      OnChanged(e); 
    }
    void OnCurrentTaskChanged()
    {
      EditorEventArgs e = new EditorEventArgs("OnCurrentTaskChanged");
      OnChanged(e);   
    }
    void OnStopEditing(bool SaveEdits)
    {
      EditorEventArgs e = new EditorEventArgs("OnStopEditing");
      OnChanged(e);    
    }
    void OnStartEditing()
    {
      EditorEventArgs e = new EditorEventArgs("OnStartEditing");
      OnChanged(e);
    }
    #endregion

    #region Members

    public event ChangedEventHandler Changed;
    
    IEditor m_editor;
    #endregion
  }

  public class EditorEventArgs : EventArgs
  {
    public EditorEventArgs(string eventType)
    {
      this.eventType = eventType;
    }
    public string eventType;
  }

  public delegate void ChangedEventHandler(object sender, EditorEventArgs e);
}
