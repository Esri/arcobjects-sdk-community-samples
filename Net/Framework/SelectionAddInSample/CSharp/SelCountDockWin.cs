using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;

namespace SelectionSample
{
  public partial class SelCountDockWin : UserControl
  {
    private static System.Windows.Forms.ListView s_listView;
    private static Label s_label;

    private static bool s_enabled;

    public SelCountDockWin(object hook)
    {
      InitializeComponent();
      this.Hook = hook;
      
      s_listView = listView1;
      s_label = label1;
      listView1.View = View.Details;
    }

    internal static bool Exists
    {
      get
      {
        return (s_listView == null) ? false : true;
      }
    }

    internal static void Clear()
    {
      if (s_listView != null) 
        s_listView.Items.Clear();
    }

    internal static void AddItem(string layerName, int selectionCount)
    {
      if (s_listView == null)
        return;

      ListViewItem item = new ListViewItem(layerName);
      item.SubItems.Add(selectionCount.ToString());
      s_listView.Items.Add(item);
    }

    internal static void SetEnabled(bool enabled)
    {
      s_enabled = enabled;

      // if the dockable window was never displayed, listview could be null
      if (s_listView == null)
        return;

      if (enabled)
      {
        s_label.Visible = false;
        s_listView.Visible = true;
      }
      else
      {
        Clear();
        s_label.Visible = true;
        s_listView.Visible = false;
      }
    }

    /// <summary>
    /// Host object of the dockable window
    /// </summary>
    private object Hook
    {
      get;
      set;
    }

    /// <summary>
    /// Implementation class of the dockable window add-in. It is responsible for 
    /// creating and disposing the user interface class of the dockable window.
    /// </summary>
    public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
    {
      private SelCountDockWin m_windowUI;

      public AddinImpl()
      {
      }

      protected override IntPtr OnCreateChild()
      {
        m_windowUI = new SelCountDockWin(this.Hook);
        return m_windowUI.Handle;
      }

      protected override void Dispose(bool disposing)
      {
        if (m_windowUI != null)
          m_windowUI.Dispose(disposing);

        base.Dispose(disposing);
      }

    }
  }
}
