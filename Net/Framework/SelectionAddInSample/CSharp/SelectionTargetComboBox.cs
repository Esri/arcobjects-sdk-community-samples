using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;
namespace SelectionSample
{
  public class SelectionTargetComboBox : ESRI.ArcGIS.Desktop.AddIns.ComboBox
  {
    private static SelectionTargetComboBox s_comboBox;
    private int m_selAllCookie;

    public SelectionTargetComboBox()
    {      
      m_selAllCookie = -1;
      s_comboBox = this;
    }
    
    internal static SelectionTargetComboBox GetSelectionComboBox()
    {
      return s_comboBox;
    }

    internal void AddItem(string itemName, IFeatureLayer layer)
    {
      if (s_comboBox.items.Count == 0)
      {
        m_selAllCookie = s_comboBox.Add("Select All");
        s_comboBox.Select(m_selAllCookie);
      }

      // Add each item to combo box.
      int cookie = s_comboBox.Add(itemName, layer);
    }

    internal void ClearAll()
    {
      m_selAllCookie = -1;
      s_comboBox.Clear();
    }

    protected override void OnUpdate()
    {
      this.Enabled = SelectionExtension.IsExtensionEnabled();
    }

    protected override void OnSelChange(int cookie)
    {
      if (cookie == -1)
        return;

      foreach (ComboBox.Item item in this.items)
      {
        // All feature layers are selectable if "Select All" is selected;
        // otherwise, only the selected layer is selectable.
        IFeatureLayer fl = item.Tag as IFeatureLayer;
        if (fl == null)
          continue;

        if (cookie == item.Cookie)
        {
          fl.Selectable = true;
          continue;
        }

        fl.Selectable = (cookie == m_selAllCookie)? true : false;
      }

      // Fire ContentsChanged event to cause TOC to refresh with new selected layers.
      ArcMap.Document.ActiveView.ContentsChanged(); ;

    }
  }

}
 