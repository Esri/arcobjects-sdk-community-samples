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



// LogoMarkerPropertyPage.cpp : Implementation of CLogoMarkerPropertyPage
#include "stdafx.h"
#include "LogoMarkerSymbolVC.h"
#include "LogoMarkerPropertyPage.h"
#include "stdio.h"
/////////////////////////////////////////////////////////////////////////////
// CLogoMarkerPropertyPage


/////////////////////////////////////////////////////////////////////////////
// IPropertyPage
STDMETHODIMP CLogoMarkerPropertyPage::Show(UINT nCmdShow)
{
	USES_CONVERSION;

	// If we are showing the property page propulate it
	// from the Marker symbol object.
	if ((nCmdShow & (SW_SHOW|SW_SHOWDEFAULT)))
	{
		//colors
		IColorPtr ipColor;
		m_ipLogoMarker->get_ColorTop(&ipColor);
		ipColor->get_RGB(&m_colTop);
		::SendMessage(m_hRchTop, EM_SETBKGNDCOLOR, 0, 
															(LPARAM) m_colTop);

		m_ipLogoMarker->get_ColorRight(&ipColor);
		ipColor->get_RGB(&m_colRight);
		::SendMessage(m_hRchRight, EM_SETBKGNDCOLOR, 0, 
															(LPARAM) m_colRight);
	
		m_ipLogoMarker->get_ColorLeft(&ipColor);
		ipColor->get_RGB(&m_colLeft);
		::SendMessage(m_hRchLeft, EM_SETBKGNDCOLOR, 0, 
															(LPARAM) m_colLeft);
		
		m_ipLogoMarker->get_ColorBorder(&ipColor);
		ipColor->get_RGB(&m_colBorder);
		::SendMessage(m_hRchBorder, EM_SETBKGNDCOLOR, 0, 
															(LPARAM) m_colBorder);
		
		//angle, size and offsets
		char  sText[10];
		double dText;
		IMarkerSymbolPtr ipMSymbol(m_ipLogoMarker);
		ipMSymbol->get_Size(&dText);
		sprintf_s( sText, sizeof(sText), "%.2f\0", dText);
		::SendMessage(m_hSpnSize, UDM_SETPOS, 0, MAKELPARAM((int)(dText), 0));
		::SetWindowText(m_hEdtSize, A2T(sText));
		ipMSymbol->get_Angle(&dText);
		sprintf_s( sText, sizeof(sText), "%d\0", (int)dText);
		::SendMessage(m_hSpnAngle, UDM_SETPOS, 0, MAKELPARAM((int)(dText), 0));
		::SetWindowText(m_hEdtAngle, A2T(sText));
		ipMSymbol->get_XOffset(&dText);
		sprintf_s( sText, sizeof(sText), "%+.2f\0", dText);
		::SendMessage(m_hSpnXOffset, UDM_SETPOS, 0, MAKELPARAM((int)(dText), 0));
		::SetWindowText(m_hEdtXOffset, A2T(sText));
		ipMSymbol->get_YOffset(&dText);
		sprintf_s( sText, sizeof(sText), "%+.2f\0", dText);
		::SendMessage(m_hSpnYOffset, UDM_SETPOS, 0, MAKELPARAM((int)(dText), 0));
		::SetWindowText(m_hEdtYOffset, A2T(sText));

		m_bShown = true;
	}

	// Let the IPropertyPageImpl deal with displaying the page
	return IPropertyPageImpl<CLogoMarkerPropertyPage>::Show(nCmdShow);
}

double PointsToUnits(double dVal, esriUnits Units)
{
	switch (Units)
	{
		case esriPoints:
			return dVal;
			break;
		case esriInches:
			return dVal / 72.0;
			break;
		case esriCentimeters:
			return dVal / 72.0 * 2.54;
			break;
		case esriMillimeters:
			return dVal / 72.0 * 25.4;
			break; 
		default:
			return dVal;
	}
}

double UnitsToPoints(double dVal, esriUnits Units)
{
	switch (Units)
	{
		case esriPoints:
			return dVal;
			break;
		case esriInches:
			return dVal * 72.0;
			break;
		case esriCentimeters:
			return dVal * 72.0 / 2.54;
			break;
		case esriMillimeters:
			return dVal * 72.0 / 25.4;
			break; 
		default:
			return dVal;
	}
}


void CLogoMarkerPropertyPage::UpdateUnits()
{

}

STDMETHODIMP CLogoMarkerPropertyPage::SetObjects(ULONG nObjects, IUnknown **ppUnk)
{
	// Loop through the objects to find one that supports
	// the ILogoMarkerSymbol interface.
	for (ULONG i=0; i < nObjects; i ++)
	{
		ILogoMarkerSymbolPtr ipLogo(ppUnk[i]);
		if (ipLogo != 0)
		{
			m_ipLogoMarker = ipLogo;
			m_ipMSymbol = ipLogo;
			m_bShown = false;
			break;
		}
	}

	// Let the IPropertyPageImpl know what objects we have
	return IPropertyPageImpl<CLogoMarkerPropertyPage>::SetObjects(nObjects, ppUnk);
}


STDMETHODIMP CLogoMarkerPropertyPage::Apply(void)
{
	// Pass the m_ipLogoMarker member variable to the QueryObject method
	HRESULT hr = QueryObject(CComVariant((IUnknown*) m_ipLogoMarker));
	
	// Set the page to not dirty
	SetDirty(FALSE);

	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IPropertyPageContext
STDMETHODIMP CLogoMarkerPropertyPage::get_Priority(LONG * Priority)
{
	if (Priority == NULL)
		return E_POINTER;
		
	*Priority = 2;

	return S_OK;
}

STDMETHODIMP CLogoMarkerPropertyPage::Applies(VARIANT unkArray, VARIANT_BOOL * Applies)
{
	if (Applies == NULL)
		return E_INVALIDARG;
	
	if (V_VT(&unkArray) != (VT_ARRAY|VT_UNKNOWN))
		return E_INVALIDARG;

	// Retrieve the safe array and retrieve the data
	SAFEARRAY *saArray = unkArray.parray;
	HRESULT hr = ::SafeArrayLock(saArray);

	IUnknownPtr *pUnk;
	hr = ::SafeArrayAccessData(saArray,reinterpret_cast<void**> (&pUnk));
	if (FAILED(hr)) return hr;

	// Loop through the elements looking to see if an object
	// implementing the IMap interface is present
	long lNumElements = saArray->rgsabound->cElements;
	for (long i = 0; i < lNumElements; i++)
	{
		ILogoMarkerSymbolPtr ipLogo(pUnk[i]);
		if (ipLogo != 0)
		{
			*Applies = VARIANT_TRUE;
			break;
		}
	}

	// Cleanup
	hr = ::SafeArrayUnaccessData(saArray);
	if (FAILED(hr)) return hr;

	hr = ::SafeArrayUnlock(saArray);
	if (FAILED(hr)) return hr;

	return S_OK;
}

STDMETHODIMP CLogoMarkerPropertyPage::CreateCompatibleObject(VARIANT kind, VARIANT * pNewObject)
{
	if (pNewObject == NULL)
		return E_POINTER;

	return E_NOTIMPL;
}

STDMETHODIMP CLogoMarkerPropertyPage::QueryObject(VARIANT theObject)
{
	// Check if we have a marker symbol
	// If we do, apply the setting from the page.
	CComVariant vObject(theObject);
	if (vObject.vt != VT_UNKNOWN) return E_UNEXPECTED;
	// Try and QI to markersymbol
	ILogoMarkerSymbolPtr ipLogo(vObject.punkVal);
	if (ipLogo != 0)
	{
		// Set properties in the symbol object
		//colors
		IColorPtr ipColor(CLSID_RgbColor);
		ipColor->put_RGB(m_colTop);
		ipLogo->put_ColorTop(ipColor);
		ipColor->put_RGB(m_colLeft);
		ipLogo->put_ColorLeft(ipColor);
		ipColor->put_RGB(m_colRight);
		ipLogo->put_ColorRight(ipColor);
		ipColor->put_RGB(m_colBorder);
		ipLogo->put_ColorBorder(ipColor);


		CComBSTR bsText;
		double dText;
		IMarkerSymbolPtr ipMSymbol(ipLogo);

		//size
		GetDlgItemText(IDC_EDT_SIZE, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		ipMSymbol->put_Size(dText);

		//angle
		GetDlgItemText(IDC_EDT_ANGLE, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		ipMSymbol->put_Angle(dText);

		//X and Y offsets
		GetDlgItemText(IDC_EDT_XOFFSET, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		ipMSymbol->put_XOffset(dText);
		GetDlgItemText(IDC_EDT_YOFFSET, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		ipMSymbol->put_YOffset(dText);
	}
	return S_OK;
}
STDMETHODIMP CLogoMarkerPropertyPage::GetHelpFile(LONG controlID, BSTR * HelpFile)
{
	if (HelpFile == NULL)
		return E_POINTER;
		
	return E_NOTIMPL;
}

STDMETHODIMP CLogoMarkerPropertyPage::GetHelpId(LONG controlID, LONG * helpID)
{
	if (helpID == NULL)
		return E_POINTER;
		
	return E_NOTIMPL;
}
STDMETHODIMP CLogoMarkerPropertyPage::Cancel()
{
	// In this case do nothing
	return S_OK;
}


/////////////////////////////////////////////////////////////////////////////
// ISymbolPropertyPage
STDMETHODIMP CLogoMarkerPropertyPage::put_Units(esriUnits Units)
{
	if (m_lUnits != Units)
	{
		m_lUnits = Units;
		UpdateUnits();
	}

	return S_OK;
}
STDMETHODIMP CLogoMarkerPropertyPage::get_Units(esriUnits * Units)
{
	if (Units == NULL)
		return E_POINTER;
		
	*Units = m_lUnits;
	return S_OK;
}


/////////////////////////////////////////////////////////////////////////////
// Dialog
LRESULT CLogoMarkerPropertyPage::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	m_Bitmap = ::LoadBitmap(_Module.m_hInst, MAKEINTRESOURCE(IDB_BMP_BUTTON));
	::SendMessage(GetDlgItem(IDC_BTN_TOP), BM_SETIMAGE, IMAGE_BITMAP, (LPARAM)m_Bitmap);
	::SendMessage(GetDlgItem(IDC_BTN_LEFT), BM_SETIMAGE, IMAGE_BITMAP, (LPARAM)m_Bitmap);
	::SendMessage(GetDlgItem(IDC_BTN_RIGHT), BM_SETIMAGE, IMAGE_BITMAP, (LPARAM)m_Bitmap);
	::SendMessage(GetDlgItem(IDC_BTN_BORDER), BM_SETIMAGE, IMAGE_BITMAP, (LPARAM)m_Bitmap);

	m_hEdtSize = GetDlgItem(IDC_EDT_SIZE);
	m_hEdtAngle = GetDlgItem(IDC_EDT_ANGLE);
	m_hEdtXOffset = GetDlgItem(IDC_EDT_XOFFSET);
	m_hEdtYOffset = GetDlgItem(IDC_EDT_YOFFSET);

	m_hSpnSize = GetDlgItem(IDC_SPIN_SIZE);
	m_hSpnAngle = GetDlgItem(IDC_SPIN_ANGLE);
	m_hSpnXOffset = GetDlgItem(IDC_SPIN_XOFFSET);
	m_hSpnYOffset = GetDlgItem(IDC_SPIN_YOFFSET);
	::SendMessage(m_hSpnSize, UDM_SETRANGE, 0, MAKELPARAM(0, 100));
	::SendMessage(m_hSpnXOffset, UDM_SETRANGE, 0, MAKELPARAM(-100, 100));
	::SendMessage(m_hSpnYOffset, UDM_SETRANGE, 0, MAKELPARAM(-100, 100));
	::SendMessage(m_hSpnAngle, UDM_SETRANGE, 0, MAKELPARAM(-360, 360));

	m_hRchLeft = GetDlgItem(IDC_RICH_LEFT);
	m_hRchRight = GetDlgItem(IDC_RICH_RIGHT);
	m_hRchTop = GetDlgItem(IDC_RICH_TOP);
	m_hRchBorder = GetDlgItem(IDC_RICH_BORDER);


	return 0;
}


LRESULT CLogoMarkerPropertyPage::OnClickedBtn_border(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	IColorPtr ipColor(CLSID_RgbColor);
	ipColor->put_UseWindowsDithering(VARIANT_TRUE);
	ipColor->put_RGB(m_colBorder);

	VARIANT_BOOL bColorSet;
	IColorPalettePtr ipPalette(CLSID_ColorPalette);
	RECT rect;
	::GetWindowRect(m_hRchBorder, &rect);
	ipPalette->TrackPopupMenu(&rect, ipColor, VARIANT_TRUE, (OLE_HANDLE)m_hWnd, &bColorSet);
  if (bColorSet)
	{
		//get newly chosen color
		ipPalette->get_Color(&ipColor);
		ipColor->get_RGB(&m_colBorder);
		::SendMessage(m_hRchBorder, EM_SETBKGNDCOLOR, 0, (LPARAM)m_colBorder);
		//apply color to displayed symbol
		m_ipLogoMarker->put_ColorBorder(ipColor);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}
LRESULT CLogoMarkerPropertyPage::OnClickedBtn_left(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	IColorPtr ipColor(CLSID_RgbColor);
	ipColor->put_UseWindowsDithering(VARIANT_TRUE);
	ipColor->put_RGB(m_colLeft);

	VARIANT_BOOL bColorSet;
	IColorPalettePtr ipPalette(CLSID_ColorPalette);
	RECT rect;
	::GetWindowRect(m_hRchLeft, &rect);
	ipPalette->TrackPopupMenu(&rect, ipColor, VARIANT_TRUE, (OLE_HANDLE)m_hWnd, &bColorSet);
  if (bColorSet)
	{
		//get newly chosen color
		ipPalette->get_Color(&ipColor);
		ipColor->get_RGB(&m_colLeft);
		::SendMessage(m_hRchLeft, EM_SETBKGNDCOLOR, 0, (LPARAM)m_colLeft);
		//apply color to displayed symbol
		m_ipLogoMarker->put_ColorLeft(ipColor);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}
LRESULT CLogoMarkerPropertyPage::OnClickedBtn_right(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	IColorPtr ipColor(CLSID_RgbColor);
	ipColor->put_UseWindowsDithering(VARIANT_TRUE);
	ipColor->put_RGB(m_colRight);

	VARIANT_BOOL bColorSet;
	IColorPalettePtr ipPalette(CLSID_ColorPalette);
	RECT rect;
	::GetWindowRect(m_hRchRight, &rect);
	ipPalette->TrackPopupMenu(&rect, ipColor, VARIANT_TRUE, (OLE_HANDLE)m_hWnd, &bColorSet);
  if (bColorSet)
	{
		//get newly chosen color
		ipPalette->get_Color(&ipColor);
		ipColor->get_RGB(&m_colRight);
		::SendMessage(m_hRchRight, EM_SETBKGNDCOLOR, 0, (LPARAM)m_colRight);
		//apply color to displayed symbol
		m_ipLogoMarker->put_ColorRight(ipColor);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}

LRESULT CLogoMarkerPropertyPage::OnClickedBtn_top(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	IColorPtr ipColor(CLSID_RgbColor);
	ipColor->put_UseWindowsDithering(VARIANT_TRUE);
	ipColor->put_RGB(m_colTop);

	VARIANT_BOOL bColorSet;
	IColorPalettePtr ipPalette(CLSID_ColorPalette);
	RECT rect;
	::GetWindowRect(m_hRchTop, &rect);
	ipPalette->TrackPopupMenu(&rect, ipColor, VARIANT_TRUE, (OLE_HANDLE)m_hWnd, &bColorSet);
  if (bColorSet)
	{
		//get newly chosen color
		ipPalette->get_Color(&ipColor);
		ipColor->get_RGB(&m_colTop);
		::SendMessage(m_hRchTop, EM_SETBKGNDCOLOR, 0, (LPARAM)m_colTop);
		//apply color to displayed symbol
		m_ipLogoMarker->put_ColorTop(ipColor);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}

LRESULT CLogoMarkerPropertyPage::OnChangeEdt_angle(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	if (m_bShown)
	{
		CComBSTR bsText;
		double dText;
		GetDlgItemText(IDC_EDT_ANGLE, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		m_ipMSymbol->put_Angle(dText);

		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}
LRESULT CLogoMarkerPropertyPage::OnChangeEdt_size(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	if (m_bShown)
	{
		CComBSTR bsText;
		double dText;
		GetDlgItemText(IDC_EDT_SIZE, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		m_ipMSymbol->put_Size(dText);

		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}
LRESULT CLogoMarkerPropertyPage::OnChangeEdt_xoffset(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	if (m_bShown)
	{
		CComBSTR bsText;
		double dText;
		GetDlgItemText(IDC_EDT_XOFFSET, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		m_ipMSymbol->put_XOffset(dText);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}
LRESULT CLogoMarkerPropertyPage::OnChangeEdt_yoffset(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	if (m_bShown)
	{
		CComBSTR bsText;
		double dText;
		GetDlgItemText(IDC_EDT_YOFFSET, bsText.m_str);
		VarR8FromStr(bsText,1033,0, &dText);
		m_ipMSymbol->put_YOffset(dText);
		// Change the page to dirty
		SetDirty(TRUE);
		//refresh property sheet
		m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
	}
	return 0;
}


LRESULT CLogoMarkerPropertyPage::OnVScroll(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	USES_CONVERSION;
	HWND hThisSB = (HWND)lParam;
	
	if (LOWORD(wParam) == SB_ENDSCROLL)
		return 0; //reject spurious messages

	//get position
	int nPos = (int)::SendMessage(hThisSB, UDM_GETPOS, 0, 0L);
	char  sText[10];
	if (hThisSB == m_hSpnSize)
	{
		sprintf_s( sText, sizeof(sText), "%.2f\0", (double)nPos);
		::SetWindowText(m_hEdtSize, A2T(sText));
	}
	else if (hThisSB == m_hSpnXOffset)
	{
		sprintf_s( sText, sizeof(sText), "%+.2f\0", (double)nPos);
		::SetWindowText(m_hEdtXOffset, A2T(sText));	
	}
	else if (hThisSB == m_hSpnYOffset)
	{
		sprintf_s( sText, sizeof(sText), "%+.2f\0", (double)nPos);
		::SetWindowText(m_hEdtYOffset, A2T(sText));	
	}
/*	else if (hThisSB == m_hSpnAngle)
	{
		sprintf( sText, "%.2f\0", (double)nPos / 100);
		::SetWindowText(m_hEdtSize, sText);
	}*/
	return 0;

}

