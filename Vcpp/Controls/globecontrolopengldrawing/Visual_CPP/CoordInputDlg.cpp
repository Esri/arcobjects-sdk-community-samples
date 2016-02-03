

// CoordInputDlg.cpp : Implementation of CCoordInputDlg
#include "stdafx.h"
#include "CoordInputDlg.h"

/////////////////////////////////////////////////////////////////////////////
// CCoordInputDlg

LRESULT CCoordInputDlg::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
	SetDlgItemText(IDC_LONGITUDE, L"");
  SetDlgItemText(IDC_LATITUDE, L"");
  
  return 1;  // Let the system set the focus
}

LRESULT CCoordInputDlg::OnOK(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	GetDlgItemText(IDC_LONGITUDE, m_bstrLon.m_str);
  GetDlgItemText(IDC_LATITUDE, m_bstrLat.m_str);
  EndDialog(wID);
	return 0;
}

LRESULT CCoordInputDlg::OnCancel(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
	return EndDialog(wID);
}

