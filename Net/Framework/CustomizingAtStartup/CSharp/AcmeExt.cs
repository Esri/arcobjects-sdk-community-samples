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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;


namespace ACME.GIS.SampleExt
{

  [Guid("148C17C0-C680-4269-B358-09A59A771822")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("ACME.Extension")]
  public class AcmeExt : IExtension, IExtensionConfig, IPersistVariant
  {
    #region COM Registration Functions
    // Register the Extension in the ESRI MxExtensions Component Category
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxExtension.Register(regKey);
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxExtension.Unregister(regKey);
    }
    #endregion

    private IApplication m_application;
    private IApplicationStatus m_appStatus;
    private esriExtensionState m_extensionState = esriExtensionState.esriESDisabled;
    private bool m_isMenuPresent = false;

    private IDocumentEvents_Event m_docEvents;
    private IApplicationStatusEvents_Event m_mapStatusEvents;

    private const string c_extName = "ACME Extension (C#)";
    private const string c_menuID = "ACME.MainMenu";
    private const string c_mainMenuID = "{1E739F59-E45F-11D1-9496-080009EEBECB}";  // Main menubar

    #region IExtension Members

    public string Name
    {
      get { return c_extName; }
    }

    public void Shutdown()
    {
      m_docEvents = null;
      m_application = null;
    }

    public void Startup(ref object initializationData)
    {
      m_application = initializationData as IApplication;
      m_appStatus = m_application as IApplicationStatus;
      // Wireup the events
      SetupEvents();
    }

    #endregion

    #region IExtensionConfig Members

    public string Description
    {
      get { return "Sample extension showing IApplicationStatusEvents"; }
    }

    public string ProductName
    {
      get { return c_extName; }
    }

    public esriExtensionState State
    {
      get
      {
        return m_extensionState;
      }
      set
      {
        // Bail if requested state matches current state
        if (value == m_extensionState)
          return;

        // Cache the state
        m_extensionState = value;

        if (m_application == null) // Don't assume Startup has been called (JIT extensions)
          return;
        
        // Enable, add the menu
        if (m_extensionState == esriExtensionState.esriESEnabled)
        {
          if (!m_isMenuPresent)
            CheckMenuValidity();
          return;
        }

        // Disable, remove the menu
        if (m_extensionState == esriExtensionState.esriESDisabled)
        {
          if (m_isMenuPresent)
            UnLoadCustomizations();
          return;
        }
      }
    }

    #endregion

    #region IPersistVariant Members

    public UID ID
    {
      get
      {
        UID extUID = new UID();
        extUID.Value = GetType().GUID.ToString("B");
        return extUID; ;
      }
    }

    public void Load(IVariantStream Stream)
    {
    }

    public void Save(IVariantStream Stream)
    {
    }

    #endregion

    # region Events

    private void SetupEvents()
    {
      // Make sure we're dealing with ArcMap
      if (m_application.Document.Parent is IMxApplication)
      {
        m_docEvents = m_application.Document as IDocumentEvents_Event;
        m_docEvents.NewDocument += new IDocumentEvents_NewDocumentEventHandler(OnNewDocument);
        m_docEvents.OpenDocument += new IDocumentEvents_OpenDocumentEventHandler(OnOpenDocument);

        m_mapStatusEvents = m_application.Document.Parent as IApplicationStatusEvents_Event;
        m_mapStatusEvents.Initialized += new IApplicationStatusEvents_InitializedEventHandler(OnInitialized);
      }
    }

    void OnOpenDocument()
    {
      CheckMenuValidity();
    }

    void OnNewDocument()
    {
      CheckMenuValidity();
    }

    // Called when the framework is fully initialized
    // After this event fires, it is safe to make UI customizations
    void OnInitialized()
    {
      CheckMenuValidity();
    }
    #endregion

    private void CheckMenuValidity()
    {
      // Wait for framework to initialize before making ui customizations
      // Check framework initialization flag
      if (!m_appStatus.Initialized)
        return;

      // Make sure the extension is enabled
      if (m_extensionState != esriExtensionState.esriESEnabled)
        return;
      
      // Perform the customization
      LoadCustomizations();
    }

    private void LoadCustomizations()
    {
      ICommandBar topMenuBar = GetMainBar();

      if (topMenuBar == null)
        return;

      // Add AcmeMenu
      UID uid = new UID();
      uid.Value = c_menuID;
      Object indexObj = Type.Missing;
      ICommandBar myMenu = topMenuBar.Add(uid, ref indexObj) as ICommandBar;
      ((ICommandItem)topMenuBar).Refresh();

      m_isMenuPresent = true;
    }

    private void UnLoadCustomizations()
    {
      ICommandBar topMenuBar = GetMainBar();

      if (topMenuBar == null)
        return;

      // Remove AcmeMenu
      UID uid = new UID();
      uid.Value = c_menuID;
      ICommandBar myMenu = topMenuBar.Find(uid, false) as ICommandBar;
      ICommandItem myMenuItem = myMenu as ICommandItem;

      myMenuItem.Delete();

      ((ICommandItem)topMenuBar).Refresh();

      m_isMenuPresent = false;
    }

    private ICommandBar GetMainBar()
    {
      try
      {
        //Grab the root menu bar
        UID uid = new UID();
        uid.Value = c_mainMenuID;
        MxDocument mx = (MxDocument)m_application.Document;
        ICommandBars cmdBars = mx.CommandBars;
        ICommandItem x = cmdBars.Find(uid, false, false);
        return cmdBars.Find(uid, false, false) as ICommandBar;
      }
      catch
      {
        return null;
      }
    }
  }
}
