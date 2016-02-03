using System;
using System.Data;
using System.Collections;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF;

namespace MapAndPageLayoutSynchApp
{
  /// <summary>
  /// Implementation of interface IMaps which is eventually a collection of Maps
  /// </summary>
  public class Maps : IMaps, IDisposable
  {
    //class member - using internally an ArrayList to manage the Maps collection
    private ArrayList m_array = null;

    #region class constructor
    public Maps()
    {
      m_array = new ArrayList();
    }
    #endregion

    #region IDisposable Members

    /// <summary>
    /// Dispose the collection
    /// </summary>
    public void Dispose()
    {
      if (m_array != null)
      {
        m_array.Clear();
        m_array = null;
      }
    }

    #endregion

    #region IMaps Members

    /// <summary>
    /// Remove the Map at the given index
    /// </summary>
    /// <param name="Index"></param>
    public void RemoveAt(int Index)
    {
      if (Index > m_array.Count || Index < 0)
        throw new Exception("Maps::RemoveAt:\r\nIndex is out of range!");

      m_array.RemoveAt(Index);
    }

    /// <summary>
    /// Reset the Maps array
    /// </summary>
    public void Reset()
    {
      m_array.Clear();
    }

    /// <summary>
    /// Get the number of Maps in the collection
    /// </summary>
    public int Count
    {
      get
      {
        return m_array.Count;
      }
    }

    /// <summary>
    /// Return the Map at the given index
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    public IMap get_Item(int Index)
    {
      if (Index > m_array.Count || Index < 0)
        throw new Exception("Maps::get_Item:\r\nIndex is out of range!");

      return m_array[Index] as IMap;
    }

    /// <summary>
    /// Remove the instance of the given Map
    /// </summary>
    /// <param name="Map"></param>
    public void Remove(IMap Map)
    {
      m_array.Remove(Map);
    }

    /// <summary>
    /// Create a new Map, add it to the collection and return it to the caller
    /// </summary>
    /// <returns></returns>
    public IMap Create()
    {
      IMap newMap = new MapClass();
      m_array.Add(newMap);

      return newMap;
    }

    /// <summary>
    /// Add the given Map to the collection
    /// </summary>
    /// <param name="Map"></param>
    public void Add(IMap Map)
    {
      if (Map == null)
        throw new Exception("Maps::Add:\r\nNew Map is mot initialized!");

      m_array.Add(Map);
    }

    #endregion
  }
}
