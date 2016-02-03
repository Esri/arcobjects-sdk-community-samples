
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Events
{
  public class EventListener
  {
    #region "Members"
    public event ChangedEventHandler Changed;
    IEngineEditor m_editor;    

    //contains all edit events listed on IEngineEditEvents
    public enum EditorEvent
    {
      OnAbort,
      OnAfterDrawSketch,
      OnBeforeStopEditing,
      OnBeforeStopOperation,
      OnChangeFeature,
      OnConflictsDetected,
      OnCreateFeature, 
      OnCurrentTaskChanged,
      OnCurrentZChanged,
      OnDeleteFeature,
      OnSaveEdits,
      OnSelectionChanged,
      OnSketchFinished,
      OnSketchModified,
      OnStartEditing,
      OnStartOperation,
      OnStopEditing,
      OnStopOperation,
      OnTargetLayerChanged,
      OnVertexAdded,
      OnVertexMoved,
      OnVertexDeleted
    };
    #endregion

    #region "Constructor"
    public EventListener(IEngineEditor editor)
    {
      if (editor == null)
      {
        throw new ArgumentNullException();
      }
      m_editor = editor;
    }
    #endregion

    #region "Event Registration and Handling"
    void OnEvent()
    {
      string eventName = GetEventName();
      UpdateEventList(eventName);
    }

    void OnEvent<T>(T param)
    {
      string eventName = GetEventName();
      UpdateEventList(eventName);
    }   

    public void ListenToEvents(EditorEvent editEvent, bool start)
    {
      switch (editEvent)
      {
        case EditorEvent.OnAbort:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnAbort += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnAbort -= OnEvent;
          break;
        case EditorEvent.OnAfterDrawSketch:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnAfterDrawSketch += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnAfterDrawSketch -= OnEvent;
          break;
        case EditorEvent.OnBeforeStopEditing:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnBeforeStopEditing += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnBeforeStopEditing -= OnEvent;
          break;
        case EditorEvent.OnBeforeStopOperation:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnBeforeStopOperation += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnBeforeStopOperation -= OnEvent;
          break;
        case EditorEvent.OnChangeFeature:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnChangeFeature += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnChangeFeature -= OnEvent;
          break;
        case EditorEvent.OnConflictsDetected:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnConflictsDetected += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnConflictsDetected -= OnEvent;
          break;
        case EditorEvent.OnCreateFeature:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnCreateFeature += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnCreateFeature -= OnEvent;
          break;
        case EditorEvent.OnCurrentTaskChanged:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnCurrentTaskChanged += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnCurrentTaskChanged -= OnEvent;
          break;
        case EditorEvent.OnCurrentZChanged:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnCurrentZChanged += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnCurrentZChanged -= OnEvent;
          break;
        case EditorEvent.OnDeleteFeature:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnDeleteFeature += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnDeleteFeature -= OnEvent;
          break;
        case EditorEvent.OnSaveEdits:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnSaveEdits += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnSaveEdits -= OnEvent;
          break;
        case EditorEvent.OnSelectionChanged:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnSelectionChanged += OnEvent; 
          else
            ((IEngineEditEvents_Event)m_editor).OnSelectionChanged -= OnEvent;
          break;
        case EditorEvent.OnSketchFinished:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnSketchFinished += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnSketchFinished -= OnEvent; 
          break;
        case EditorEvent.OnSketchModified:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnSketchModified += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnSketchModified -= OnEvent;
          break;
        case EditorEvent.OnStartEditing:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnStartEditing += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnStartEditing -= OnEvent; 
          break;
        case EditorEvent.OnStartOperation:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnStartOperation += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnStartOperation -= OnEvent;
          break;
        case EditorEvent.OnStopEditing:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnStopEditing += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnStopEditing -= OnEvent; 
          break;
        case EditorEvent.OnStopOperation:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnStopOperation += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnStopOperation -= OnEvent;
          break;
        case EditorEvent.OnTargetLayerChanged:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnTargetLayerChanged += OnEvent;
          else
            ((IEngineEditEvents_Event)m_editor).OnTargetLayerChanged -= OnEvent;
          break;
        case EditorEvent.OnVertexAdded:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnVertexAdded += OnEvent; 
          else
            ((IEngineEditEvents_Event)m_editor).OnVertexAdded -= OnEvent;
          break;
        case EditorEvent.OnVertexMoved:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnVertexMoved += OnEvent; 
          else
            ((IEngineEditEvents_Event)m_editor).OnVertexMoved -= OnEvent;
          break;
        case EditorEvent.OnVertexDeleted:
          if (start)
            ((IEngineEditEvents_Event)m_editor).OnVertexDeleted += OnEvent;              
          else
            ((IEngineEditEvents_Event)m_editor).OnVertexDeleted -= OnEvent;
          break;
        default:
          throw new ArgumentOutOfRangeException();     
      }

    }
        
    string GetEventName()
    {
      //Get the name of the ArcEngine calling method and use this to indicate the event that was fired
      StackTrace st = new System.Diagnostics.StackTrace();
      StackFrame sf = st.GetFrame(2);
      return (sf.GetMethod().Name);
    }
    void UpdateEventList(string eventName)
    {
      EditorEventArgs e = new EditorEventArgs(eventName);
      if (Changed != null)
        Changed(this, e);
    }
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
