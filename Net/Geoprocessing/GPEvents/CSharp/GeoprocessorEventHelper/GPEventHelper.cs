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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;

namespace GeoprocessorEventHelper
{
  //declare the event argument classes for the different GP events
  public sealed class GPMessageEventArgs : EventArgs
  {
    private string m_message = string.Empty;
    private esriGPMessageType m_messageType = esriGPMessageType.esriGPMessageTypeEmpty;
    private int m_errorCode = -1;

    #region class constructors
    public GPMessageEventArgs() : base()
    {
      
    }

    public GPMessageEventArgs(string message, esriGPMessageType messageType, int errorCode) : this()
    {
      m_message = message;
      m_messageType = messageType;
      m_errorCode = errorCode;
    }
    #endregion

    #region properties
    public string Message
    {
      get { return m_message; }
      set { m_message = value; }
    }
    public esriGPMessageType MessageType
    {
      get { return m_messageType; }
      set { m_messageType = value; }
    }
    public int ErrorCode
    {
      get { return m_errorCode; }
      set { m_errorCode = value; }
    }
    #endregion
  }
  public sealed class GPPostToolExecuteEventArgs : EventArgs
  {
    #region class members
    private GPMessageEventArgs[] m_messages;
    private int m_result = 0;
    private string m_displayName = string.Empty;
    private string m_name = string.Empty;
    private string m_pathName = string.Empty;
    private string m_toolbox = string.Empty;
    private string m_toolCategory = string.Empty;
    private esriGPToolType m_toolType = esriGPToolType.esriGPCustomTool;
    private string m_description = string.Empty;
    #endregion

    #region calss constructor
    public GPPostToolExecuteEventArgs()
      : base()
    {

    }
    #endregion

    #region properties
    public GPMessageEventArgs[] Messages
    {
      get { return m_messages; }
      set { m_messages = value; }
    }
    public int Result
    {
      get { return m_result; }
      set { m_result = value; }
    }
    public string DisplayName
    {
      get { return m_displayName; }
      set { m_displayName = value; }
    }
    public string Name
    {
      get { return m_name; }
      set { m_name = value; }
    }
    public string Toolbox
    {
      get { return m_toolbox; }
      set { m_toolbox = value; }
    }
    public string ToolCategory
    {
      get { return m_toolCategory; }
      set { m_toolCategory = value; }
    }
    public esriGPToolType ToolType
    {
      get { return m_toolType; }
      set { m_toolType = value; }
    }
    public string Description
    {
      get { return m_description; }
      set { m_description = value; }
    }
    public string PathName
    {
      get { return m_pathName; }
      set { m_pathName = value; }
    }
    #endregion
  }
  public sealed class GPPreToolExecuteEventArgs : EventArgs
  {
    #region class members
    private int m_processID = 0;
    private string m_displayName = string.Empty;
    private string m_name = string.Empty;
    private string m_pathName = string.Empty;
    private string m_toolbox = string.Empty;
    private string m_toolCategory = string.Empty;
    private esriGPToolType m_toolType = esriGPToolType.esriGPCustomTool;
    private string m_description = string.Empty;
    #endregion

    #region calss constructor
    public GPPreToolExecuteEventArgs()
      : base()
    {

    }
    #endregion

    #region properties
    public int ProcessID
    {
      get { return m_processID; }
      set { m_processID = value; }
    }
    public string DisplayName
    {
      get { return m_displayName; }
      set { m_displayName = value; }
    }
    public string Name
    {
      get { return m_name; }
      set { m_name = value; }
    }
    public string Toolbox
    {
      get { return m_toolbox; }
      set { m_toolbox = value; }
    }
    public string ToolCategory
    {
      get { return m_toolCategory; }
      set { m_toolCategory = value; }
    }
    public esriGPToolType ToolType
    {
      get { return m_toolType; }
      set { m_toolType = value; }
    }
    public string Description
    {
      get { return m_description; }
      set { m_description = value; }
    }
    public string PathName
    {
      get { return m_pathName; }
      set { m_pathName = value; }
    }
    #endregion
  }

  // A delegate type for hooking up change notifications.
  public delegate void MessageEventHandler(object sender, GPMessageEventArgs e);
  public delegate void ToolboxChangedEventHandler(object sender, EventArgs e);
  public delegate void PostToolExecuteEventHandler(object sender, GPPostToolExecuteEventArgs e);
  public delegate void PreToolExecuteEventHandler(object sender, GPPreToolExecuteEventArgs e);
    
  [
    Guid("0CC39861-B4FE-45ea-8919-8295AF25F311"),
    ProgId("GeoprocessorEventHelper.GPMessageEventHandler"),
    ComVisible(true),
    Serializable
  ]
  /// <summary>
  ///A class that sends event notifications whenever the Messages are added.
  /// </summary>
  public class GPMessageEventHandler : IGeoProcessorEvents
  {
    // An event that clients can use to be notified whenever a GP message is posted.
    public event MessageEventHandler GPMessage;
    //an event notifying that a toolbox has changed
    public event ToolboxChangedEventHandler GPToolboxChanged;
    //an event which gets fired right after a tool finish execute
    public event PostToolExecuteEventHandler GPPostToolExecute;
    //an event which gets fired before a tool gets executed
    public event PreToolExecuteEventHandler GPPreToolExecute;

    #region IGeoProcessorEvents Members

    /// <summary>
    /// Called when a message has been posted while executing a SchematicGeoProcessing
    /// </summary>
    /// <param name="message"></param>
    void IGeoProcessorEvents.OnMessageAdded(IGPMessage message)
    {
      //fire the GPMessage event
      if (GPMessage != null)
        GPMessage(this, new GPMessageEventArgs(message.Description, message.Type, message.ErrorCode));
    }

    /// <summary>
    /// Called immediately after a tool is executed by the GeoProcessor.
    /// </summary>
    /// <param name="Tool"></param>
    /// <param name="Values"></param>
    /// <param name="result"></param>
    /// <param name="Messages"></param>
    void IGeoProcessorEvents.PostToolExecute(IGPTool Tool, IArray Values, int result, IGPMessages Messages)
    {
      GPMessageEventArgs[] messages = new GPMessageEventArgs[Messages.Count];
      IGPMessage gpMessage = null;
      for (int i = 0; i < Messages.Count; i++)
      {
        gpMessage = Messages.GetMessage(i);
        GPMessageEventArgs message = new GPMessageEventArgs(gpMessage.Description, gpMessage.Type, gpMessage.ErrorCode);
        messages[i] = message;
      }

      //create a new instance of GPPostToolExecuteEventArgs
      GPPostToolExecuteEventArgs e = new GPPostToolExecuteEventArgs();
      e.DisplayName = Tool.DisplayName;
      e.Name = Tool.Name;
      e.PathName = Tool.PathName;
      e.Toolbox = Tool.Toolbox.Alias;
      e.ToolCategory = Tool.ToolCategory;
      e.ToolType = Tool.ToolType;
      e.Description = Tool.Description;
      e.Result = result;

      //fire the Post tool event
      if (null != GPPostToolExecute)
        GPPostToolExecute(this, e);
    }

    /// <summary>
    /// Called immediately prior to the GeoProcessor executing a tool.
    /// </summary>
    /// <param name="Tool"></param>
    /// <param name="Values"></param>
    /// <param name="processID"></param>
    void IGeoProcessorEvents.PreToolExecute(IGPTool Tool, IArray Values, int processID)
    {
      //create a new instance of GPPreToolExecuteEventArgs
      GPPreToolExecuteEventArgs e = new GPPreToolExecuteEventArgs();
      e.DisplayName = Tool.DisplayName;
      e.Name = Tool.Name;
      e.PathName = Tool.PathName;
      e.Toolbox = Tool.Toolbox.Alias;
      e.ToolCategory = Tool.ToolCategory;
      e.ToolType = Tool.ToolType;
      e.Description = Tool.Description;
      e.ProcessID = processID;

      //fire the PreTool event
      if (null != GPPreToolExecute)
        GPPreToolExecute(this, e);

    }

    /// <summary>
    /// Called when a toolbox is added or removed from the GeoProcessor.
    /// </summary>
    void IGeoProcessorEvents.ToolboxChange()
    {
      if (GPToolboxChanged != null)
        GPToolboxChanged(this, new EventArgs());
    }

    #endregion
  }

}