// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
// 


// LogoMarkerPropertyPage.h : Declaration of the CLogoMarkerPropertyPage

#ifndef __LOGOMARKERPROPERTYPAGE_H_
#define __LOGOMARKERPROPERTYPAGE_H_

#include "resource.h"       // main symbols

EXTERN_C const CLSID CLSID_LogoMarkerPropertyPage;
_COM_SMARTPTR_TYPEDEF(ILogoMarkerSymbol, __uuidof(ILogoMarkerSymbol));

/////////////////////////////////////////////////////////////////////////////
// CLogoMarkerPropertyPage
class ATL_NO_VTABLE CLogoMarkerPropertyPage :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CLogoMarkerPropertyPage, &CLSID_LogoMarkerPropertyPage>,
	public IPropertyPageImpl<CLogoMarkerPropertyPage>,
	public CDialogImpl<CLogoMarkerPropertyPage>,
	public IPropertyPageContext,
	public ISymbolPropertyPage
{

private:
	HBITMAP m_Bitmap;
	HINSTANCE								m_hLibRichEdit;	//for loading RICHED32.DLL
																					// for the rich edit control
	ILogoMarkerSymbolPtr		m_ipLogoMarker;
	IMarkerSymbolPtr				m_ipMSymbol;
	COLORREF								m_colTop, m_colRight, m_colLeft, m_colBorder;
	esriUnits								m_lUnits;
	HWND										m_hSpnSize, m_hSpnAngle, m_hSpnXOffset, m_hSpnYOffset,
													m_hEdtSize, m_hEdtAngle, m_hEdtXOffset, m_hEdtYOffset,
													m_hRchTop, m_hRchLeft, m_hRchRight, m_hRchBorder; 
	bool										m_bShown;

	void UpdateUnits();
public:
	CLogoMarkerPropertyPage() 
	{
		m_dwTitleID = IDS_TITLELogoMarkerPropertyPage;
		m_dwHelpFileID = IDS_HELPFILELogoMarkerPropertyPage;
		m_dwDocStringID = IDS_DOCSTRINGLogoMarkerPropertyPage;
		m_hLibRichEdit = LoadLibraryA("RICHED32.DLL"); //for rich edit control
		m_colTop = RGB(250, 0, 0);
		m_colLeft = RGB(200, 0, 0);
		m_colRight = RGB(150, 0, 0);
		m_colBorder = RGB(0,0,0);
		m_lUnits = esriPoints;
	}
	~CLogoMarkerPropertyPage() 
	{
		FreeLibrary(m_hLibRichEdit);	//for rich edit control
	}

	enum {IDD = IDD_LOGOMARKERPROPERTYPAGE};

DECLARE_REGISTRY_RESOURCEID(IDR_LOGOMARKERPROPERTYPAGE)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CLogoMarkerPropertyPage) 
	COM_INTERFACE_ENTRY(IPropertyPage)
	COM_INTERFACE_ENTRY(IPropertyPageContext)
	COM_INTERFACE_ENTRY(ISymbolPropertyPage)
END_COM_MAP()

BEGIN_CATEGORY_MAP(CLogoMarkerPropertyPage)
	IMPLEMENTED_CATEGORY(__uuidof(CATID_PropertyPages))
	IMPLEMENTED_CATEGORY(__uuidof(CATID_SymbolPropertyPages))
END_CATEGORY_MAP()

BEGIN_MSG_MAP(CLogoMarkerPropertyPage)
	CHAIN_MSG_MAP(IPropertyPageImpl<CLogoMarkerPropertyPage>)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	COMMAND_HANDLER(IDC_BTN_BORDER, BN_CLICKED, OnClickedBtn_border)
	COMMAND_HANDLER(IDC_BTN_LEFT, BN_CLICKED, OnClickedBtn_left)
	COMMAND_HANDLER(IDC_BTN_RIGHT, BN_CLICKED, OnClickedBtn_right)
	COMMAND_HANDLER(IDC_BTN_TOP, BN_CLICKED, OnClickedBtn_top)
	COMMAND_HANDLER(IDC_EDT_ANGLE, EN_CHANGE, OnChangeEdt_angle)
	COMMAND_HANDLER(IDC_EDT_SIZE, EN_CHANGE, OnChangeEdt_size)
	COMMAND_HANDLER(IDC_EDT_XOFFSET, EN_CHANGE, OnChangeEdt_xoffset)
	COMMAND_HANDLER(IDC_EDT_YOFFSET, EN_CHANGE, OnChangeEdt_yoffset)
	MESSAGE_HANDLER(WM_VSCROLL, OnVScroll)
END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

// IPropertyPage
	STDMETHOD(Apply)(void);
	STDMETHOD(Show)(UINT nCmdShow);
	STDMETHOD(SetObjects)(ULONG nObjects, IUnknown **ppUnk);


// IPropertyPageContext
	STDMETHOD(get_Priority)(LONG * Priority);
	STDMETHOD(Applies)(VARIANT unkArray, VARIANT_BOOL * Applies);
	STDMETHOD(CreateCompatibleObject)(VARIANT kind, VARIANT * pNewObject);
	STDMETHOD(QueryObject)(VARIANT theObject);
	STDMETHOD(GetHelpFile)(LONG controlID, BSTR * HelpFile);
	STDMETHOD(GetHelpId)(LONG controlID, LONG * helpID);
	STDMETHOD(Cancel)();

// ISymbolPropertyPage
	STDMETHOD(put_Units)(esriUnits Units);
	STDMETHOD(get_Units)(esriUnits * Units);

	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnClickedBtn_border(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnClickedBtn_left(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnClickedBtn_right(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnClickedBtn_top(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);

	LRESULT OnChangeEdt_angle(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnChangeEdt_size(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnChangeEdt_xoffset(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnChangeEdt_yoffset(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
	LRESULT OnVScroll(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
};

#endif //__LOGOMARKERPROPERTYPAGE_H_
