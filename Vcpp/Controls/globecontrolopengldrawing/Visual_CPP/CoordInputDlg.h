

// CoordInputDlg.h : Declaration of the CCoordInputDlg

#ifndef __COORDINPUTDLG_H_
#define __COORDINPUTDLG_H_

#pragma once

#include "StdAfx.h"
#include "resource.h"       // main symbols
#include <atlhost.h>
#include <atlwin.h>
/////////////////////////////////////////////////////////////////////////////
// CCoordInputDlg
class CCoordInputDlg : 
	public CAxDialogImpl<CCoordInputDlg>
{
public:
	CCoordInputDlg()
	{
    m_bstrLon = CComBSTR(L"");
    m_bstrLat = CComBSTR(L"");
	}

	~CCoordInputDlg()
	{
	}

	enum { IDD = IDD_COORDINPUTDLG };

BEGIN_MSG_MAP(CCoordInputDlg)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	COMMAND_ID_HANDLER(IDOK, OnOK)
	COMMAND_ID_HANDLER(IDCANCEL, OnCancel)
END_MSG_MAP()



// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

  LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
  LRESULT OnOK(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
  LRESULT OnCancel(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);

  
  CComBSTR m_bstrLon;
  CComBSTR m_bstrLat;
};

#endif //__COORDINPUTDLG_H_

