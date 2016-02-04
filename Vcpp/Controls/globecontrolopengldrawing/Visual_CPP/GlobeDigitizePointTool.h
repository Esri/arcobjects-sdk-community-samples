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


// GlobeDigitizePointTool.h : Declaration of the CGlobeDigitizePointTool

#ifndef __GLOBEDIGITIZEPOINTTOOL_H_
#define __GLOBEDIGITIZEPOINTTOOL_H_

#include "resource.h"       // main symbols


/////////////////////////////////////////////////////////////////////////////
// CGlobeDigitizePointTool
class ATL_NO_VTABLE CGlobeDigitizePointTool : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CGlobeDigitizePointTool, &CLSID_GlobeDigitizePointTool>,
	public IDispatchImpl<IGlobeDigitizePointTool, &IID_IGlobeDigitizePointTool, &LIBID_GlobeDigitizePoint>,
	public ITool,
	public ICommand,
	public IGlobeDisplayEvents
{
public:
	CGlobeDigitizePointTool();
	~CGlobeDigitizePointTool();
	HRESULT FinalConstruct();
	void FinalRelease();

  

DECLARE_REGISTRY_RESOURCEID(IDR_GLOBEDIGITIZEPOINTTOOL)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CGlobeDigitizePointTool)
	COM_INTERFACE_ENTRY(IGlobeDigitizePointTool)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ITool)
	COM_INTERFACE_ENTRY(ICommand)
	COM_INTERFACE_ENTRY(IGlobeDisplayEvents)
END_COM_MAP()

BEGIN_CATEGORY_MAP(__uuidof(CATID_ControlsCommands))
	IMPLEMENTED_CATEGORY(__uuidof(CATID_ControlsCommands))
END_CATEGORY_MAP()

// IGlobeDigitizePointTool
public:
// ICommand
	STDMETHOD(get_Enabled)(VARIANT_BOOL * Enabled);
	STDMETHOD(get_Checked)(VARIANT_BOOL * Checked);
	STDMETHOD(get_Name)(BSTR * Name);
	STDMETHOD(get_Caption)(BSTR * Caption);
	STDMETHOD(get_Tooltip)(BSTR * Tooltip);
	STDMETHOD(get_Message)(BSTR * Message);
	STDMETHOD(get_HelpFile)(BSTR * HelpFile);
	STDMETHOD(get_HelpContextID)(LONG * helpID);
	STDMETHOD(get_Bitmap)(OLE_HANDLE * Bitmap);
	STDMETHOD(get_Category)(BSTR * categoryName);
	STDMETHOD(OnCreate)(IDispatch * hook);
	STDMETHOD(OnClick)();

// ITool
	STDMETHOD(get_Cursor)(OLE_HANDLE * Cursor);
	STDMETHOD(OnMouseDown)(LONG button, LONG shift, LONG x, LONG y);
	STDMETHOD(OnMouseMove)(LONG button, LONG shift, LONG x, LONG y);
	STDMETHOD(OnMouseUp)(LONG button, LONG shift, LONG x, LONG y);
	STDMETHOD(OnDblClick)();
	STDMETHOD(OnKeyDown)(LONG keyCode, LONG shift);
	STDMETHOD(OnKeyUp)(LONG keyCode, LONG shift);
	STDMETHOD(OnContextMenu)(LONG x, LONG y, VARIANT_BOOL * handled);
	STDMETHOD(Refresh)(OLE_HANDLE hdc);
	STDMETHOD(Deactivate)(VARIANT_BOOL * complete);

// IGlobeDisplayEvents
	STDMETHOD(ActiveViewerChanged)(ISceneViewer * pViewer);
	STDMETHOD(ViewerAdded)(ISceneViewer * pViewer);
	STDMETHOD(ViewerRemoved)(ISceneViewer * pViewer);
	STDMETHOD(InteractionStopped)();
	STDMETHOD(BatchTileGenerationStarted)(BSTR Name);
	STDMETHOD(BatchTileGenerationStopped)();
	STDMETHOD(BeforeDraw)(ISceneViewer * pViewer, VARIANT_BOOL * pbHandled);
	STDMETHOD(AfterDraw)(ISceneViewer * pViewer);
	STDMETHOD(VectorOverflow)(ILayer * pLayer);
	STDMETHOD(TileOverflow)(ILayer * pLayer);

private:

  HCURSOR                           m_hCursor;
  HBITMAP							              m_hBitmap;

	IGlobeHookHelperPtr               m_ipGlobeHookHelper;
	IGlobeViewUtilPtr                 m_ipGlobeViewUtil;
  IGlobePtr								          m_ipGlobe;
  IGlobeDisplayPtr						      m_ipGlobeDisplay;	
  ISceneViewerPtr							      m_ipViewer;

  UINT                              m_scrX;
  UINT                              m_scrY;
  bool                              m_bDrawPoint;

  DWORD									            m_dwGlobeDisplayEventsCookie;
  DWORD									            m_dwActiveViewEventsCookie;

  HRESULT TurnOnGlobeDisplayEvents();
	HRESULT TurnOffGlobeDisplayEvents();
};

#endif //__GLOBEDIGITIZEPOINTTOOL_H_

