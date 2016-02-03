using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;

namespace MapControlSaveLayerFile
{
  /// <summary>
  /// Context menu class for Engine applications.
  ///</summary>
  [Guid("94e68d22-88b1-4294-ae29-0ae80ffc8468")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MapControlSaveLayerFile.ContextMenuClass")]
  public class ContextMenuClass
  {
    private IToolbarMenu2 m_toolbarMenu = null;
    private bool m_beginGroupFlag = false;

    public ContextMenuClass()
    {
    }

    /// <summary>
    /// Instantiate the underlying ToolbarMenu and set the hook object to be
    /// passed into the OnCreate event of each command item.
    /// </summary>
    public void SetHook(object hook)
    {
      m_toolbarMenu = new ToolbarMenuClass();
      m_toolbarMenu.SetHook(hook);

      //
      // TODO: Define context menu items here
      //
      //AddItem("esriControls.ControlsMapZoomOutFixedCommand", -1);
      //AddItem("esriControls.ControlsMapZoomInFixedCommand", -1);
      //BeginGroup(); //Separator
      //AddItem("{380FB31E-6C24-4F5C-B1DF-47F33586B885}", -1); //undo command
      //AddItem(new Guid("B0675372-0271-4680-9A2C-269B3F0C01E8"), -1); //redo command
      //BeginGroup(); //Separator
      //AddItem("MyCustomCommandCLSIDorProgID", -1);
    }

    /// <summary>
    /// Popup the context menu at the given location
    /// </summary>
    /// <param name="X">X coordinate where to popup the menu</param>
    /// <param name="Y">Y coordinate where to popup the menu</param>
    /// <param name="hWndParent">Handle to the parent window</param>
    public void PopupMenu(int X, int Y, int hWndParent)
    {
      if (m_toolbarMenu != null)
        m_toolbarMenu.PopupMenu(X, Y, hWndParent);
    }

    /// <summary>
    /// Retrieve the ToolbarMenu object in case if needed to be modified at
    /// run time.
    /// </summary>
    public IToolbarMenu2 ContextMenu
    {
      get
      {
        return m_toolbarMenu;
      }
    }

    #region Helper methods to add items to the context menu
    /// <summary>
    /// Adds a separator bar on the command bar to begin a group. 
    /// </summary>
    private void BeginGroup()
    {
      m_beginGroupFlag = true;
    }

    /// <summary>
    /// Add a command item to the command bar by an Unique Identifier Object (UID).
    /// </summary>
    private void AddItem(UID itemUID)
    {
      m_toolbarMenu.AddItem(itemUID.Value, itemUID.SubType, -1, m_beginGroupFlag, esriCommandStyles.esriCommandStyleIconAndText);
      m_beginGroupFlag = false; //Reset group flag
    }

    /// <summary>
    /// Add a command item to the command bar by an identifier string
    /// and a subtype index
    /// </summary>
    private void AddItem(string itemID, int subtype)
    {
      UID itemUID = new UIDClass();
      try
      {
        itemUID.Value = itemID;
      }
      catch
      {
        //Add an empty guid to indicate something's wrong "Missing"
        itemUID.Value = Guid.Empty.ToString("B");
      }

      if (subtype > 0)
        itemUID.SubType = subtype;
      AddItem(itemUID);

    }

    /// <summary>
    /// Add a command item to the command bar by the Guid 
    /// and a subtype index.
    /// </summary>
    private void AddItem(Guid itemGuid, int subtype)
    {
      AddItem(itemGuid.ToString("B"), subtype);
    }

    /// <summary>
    /// Add a command item to the command bar by a type
    /// and a subtype index.
    /// </summary>
    private void AddItem(Type itemType, int subtype)
    {
      if (itemType != null)
        AddItem(itemType.GUID, subtype);
    }

    #endregion

  }
}
