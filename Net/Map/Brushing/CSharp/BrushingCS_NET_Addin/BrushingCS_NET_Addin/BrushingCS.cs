using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices; 

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework; 

namespace Brushing_Addin
{
  
  public class BrushingCS : ESRI.ArcGIS.Desktop.AddIns.Tool
  {

    private object m_gSelectTool;
    private bool m_bAction; 
  
    public BrushingCS()
    {
      m_bAction = false; 
    }

    protected override void OnUpdate()
    {
      //Enabled = ArcMap.Application != null;
    }

    #region ITool Members
    protected override void OnMouseDown(MouseEventArgs arg)
    {
      if (m_bAction == true)
      {
        return;
      }

      Type oType = Type.GetTypeFromProgID("esriArcMapUI.SelectTool");
      if (oType != null)
      {
        m_gSelectTool = Activator.CreateInstance(oType);
      }

      // create and initialize SelectTool command
      ICommand pCommand;
      pCommand = (ICommand)m_gSelectTool;
      pCommand.OnCreate(ArcMap.Application);

      // emulate mouse click for m_gSelectTool
      ITool pTool;
      pTool = (ITool)m_gSelectTool;

      pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);
      pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);   

      m_bAction = SelectFromGraphics();

      // if there's selected graphics then start moving it
      if (m_bAction == true)
      {
        pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);  
      }
      else
      {
        m_gSelectTool = null;
      } 
        
    }

    protected override void OnMouseUp(MouseEventArgs arg)
    {
      if (m_bAction == false)
      {
        return;
      }
      ITool pTool;
      pTool = (ITool)m_gSelectTool;
      pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);
      

      SelectFromGraphics();

      // release object
      m_gSelectTool = null;
      m_bAction = false;

      MouseCursor cursor;
      cursor = new MouseCursor();
      cursor.SetCursor(esriSystemMouseCursor.esriSystemMouseCursorDefault); 
    }

    protected override void OnMouseMove(MouseEventArgs arg)
    {
      if (m_bAction == false)
      {
        return;
      }

      // 1 move graphics
      // 2 set new position
      // 3 continue moving
      // 4 update selection
      ITool pTool;
      pTool = (ITool)m_gSelectTool;
      pTool.OnMouseMove(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);
      // comment out the next 3 line to speed up, but selection only gets updated by mouse up
      // comment out the next 3 line to speed up, but selection only gets updated by mouse up
      pTool.OnMouseUp(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);
      pTool.OnMouseDown(GetButtonCode(arg), Convert.ToInt32(arg.Shift), arg.X, arg.Y);
      SelectFromGraphics(); 
          
    }
    #endregion

    bool SelectFromGraphics()
    {
      IMxApplication pMxApp;
      IMxDocument pMxDoc;
      pMxApp = (IMxApplication)ArcMap.Application;
      pMxDoc = (IMxDocument)ArcMap.Application.Document;
      IGraphicsContainerSelect pGC;
      pGC = (IGraphicsContainerSelect)pMxDoc.FocusMap;

      // find first selected graphic object
      if (pGC.ElementSelectionCount > 0)
      {
        IElement pElem;
        pElem = pGC.SelectedElement(0);
        IGeometry pGeometry;
        pGeometry = pElem.Geometry;
        pMxDoc.FocusMap.SelectByShape(pGeometry, null, false);
        pMxDoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        return true;
      }
      else
      {
        return false;
      }
    }
    
    int GetButtonCode(MouseEventArgs pArg)
    {
       int intButtonCode = -1;
       System.Windows.Forms.MouseButtons  pButton = pArg.Button;
       
       switch (pButton.ToString())
       {
          case "Left":
            intButtonCode = 1;
          break;

          case "Right":
            intButtonCode = 2;
          break;

          case "Middle":
            intButtonCode = 4;
          break;
       }
       
       return intButtonCode;
    }
    
    
    
  }
  
  

}
