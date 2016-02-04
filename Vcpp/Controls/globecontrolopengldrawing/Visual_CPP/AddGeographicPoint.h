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


// AddGeographicPoint.h : Declaration of the CAddGeographicPoint

#ifndef __ADDGEOGRAPHICPOINT_H_
#define __ADDGEOGRAPHICPOINT_H_

#include "resource.h"       // main symbols
/////////////////////////////////////////////////////////////////////////////
// CAddGeographicPoint
class ATL_NO_VTABLE CAddGeographicPoint : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CAddGeographicPoint, &CLSID_AddGeographicPoint>,
	public ISupportErrorInfo,
	public IDispatchImpl<IAddGeographicPoint, &IID_IAddGeographicPoint, &LIBID_GlobeDigitizePoint>,
	public ICommand,
	public IGlobeDisplayEvents
{
public:
	CAddGeographicPoint();
	~CAddGeographicPoint();
	HRESULT FinalConstruct();
	void FinalRelease();


DECLARE_REGISTRY_RESOURCEID(IDR_ADDGEOGRAPHICPOINT)
DECLARE_NOT_AGGREGATABLE(CAddGeographicPoint)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CAddGeographicPoint)
	COM_INTERFACE_ENTRY(IAddGeographicPoint)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(ICommand)
	COM_INTERFACE_ENTRY(IGlobeDisplayEvents)
END_COM_MAP()

BEGIN_CATEGORY_MAP(__uuidof(CATID_ControlsCommands))
	IMPLEMENTED_CATEGORY(__uuidof(CATID_ControlsCommands))
END_CATEGORY_MAP()


// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IAddGeographicPoint

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
public:

private:
  typedef vector<WKSPointZ> OGLPoints;         


  HCURSOR                           m_hCursor;
  HBITMAP							              m_hBitmap;
	
  IGlobeHookHelperPtr               m_ipGlobeHookHelper; 
	IGlobeViewUtilPtr                 m_ipGlobeViewUtil;
  IGlobePtr								          m_ipGlobe;
  IGlobeDisplayPtr						      m_ipGlobeDisplay;	
  ISceneViewerPtr							      m_ipViewer;
  IUnitConverterPtr                 m_ipUnitConverter;

  
  OGLPoints                         m_OGLPoints;

  UINT                              m_scrX;
  UINT                              m_scrY;
  bool                              m_bDrawPoint;

  DWORD									            m_dwGlobeDisplayEventsCookie;
  DWORD									            m_dwActiveViewEventsCookie;

  HRESULT TurnOnGlobeDisplayEvents();
	HRESULT TurnOffGlobeDisplayEvents();	
};

#endif //__ADDGEOGRAPHICPOINT_H_

