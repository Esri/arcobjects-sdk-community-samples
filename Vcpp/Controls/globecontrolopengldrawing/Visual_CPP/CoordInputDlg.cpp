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

