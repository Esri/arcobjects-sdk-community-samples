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
// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 


#pragma once

#include "resource.h"                                           // main symbols
#include "/Program Files (x86)/ArcGIS/DeveloperKit10.6/include/CATIDs/ArcCATIDs.h"     // component category IDs
#include "ConnectivitySolver.h"

// ConnSolverPropPage
[
  coclass,
  default(IUnknown),
  threading(apartment),
  vi_progid("CustomSolver.ConnSolverPropPage"),
  progid("CustomSolver.ConnSolverPropPage.1"),
  version(1.0),
  uuid("241CF566-AFEF-40D3-8ADC-3914AF4CED16"),
  helpstring("ConnSolverPropPage Class")
]
class ATL_NO_VTABLE ConnSolverPropPage :
  public IPropertyPageImpl<ConnSolverPropPage>,
  public CDialogImpl<ConnSolverPropPage>,
  public IPropertyPageContext
{
public:
  ConnSolverPropPage()
  {
    m_dwTitleID = IDS_TITLECONNSOLVERPROPPAGE;
    m_dwHelpFileID = IDS_HELPFILECONNSOLVERPROPPAGE;
    m_dwDocStringID = IDS_DOCSTRINGCONNSOLVERPROPPAGE;
  }

  DECLARE_PROTECT_FINAL_CONSTRUCT()

  // Register the property page in the Property Page and Layer Property Page component categories so that it can be dynamically discovered
  // as an available property page for the ConnectivitySolver.
  BEGIN_CATEGORY_MAP(ConnectivitySymbolizer)
    IMPLEMENTED_CATEGORY(__uuidof(CATID_PropertyPages))
    IMPLEMENTED_CATEGORY(__uuidof(CATID_LayerPropertyPages))
  END_CATEGORY_MAP()

  HRESULT FinalConstruct()
  {
    return S_OK;
  }

  void FinalRelease()
  {
  }

  enum {IDD = IDD_CONNSOLVERPROPPAGE};

  BEGIN_MSG_MAP(ConnSolverPropPage)
    CHAIN_MSG_MAP(IPropertyPageImpl<ConnSolverPropPage>)
    MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
    COMMAND_HANDLER(IDC_RADIO_CONNECTED, BN_CLICKED, OnBnClickedRadioConnected)
    COMMAND_HANDLER(IDC_RADIO_DISCONNECTED, BN_CLICKED, OnBnClickedRadioDisconnected)
    COMMAND_HANDLER(IDC_COMBO_SHAPE, CBN_SELCHANGE, OnCbnSelchangeComboShape)
  END_MSG_MAP()

  // Handler prototypes:
  //  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
  //  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
  //  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

  // IPropertyPage

  STDMETHOD(Apply)(void);
  STDMETHOD(Show)(UINT nCmdShow);
  STDMETHOD(SetObjects)(ULONG nObjects, IUnknown** ppUnk);

  // IPropertyPageContext

  STDMETHOD(get_Priority)(LONG* pPriority);
  STDMETHOD(Applies)(VARIANT unkArray, VARIANT_BOOL* pApplies);
  STDMETHOD(CreateCompatibleObject)(VARIANT kind, VARIANT* pNewObject);
  STDMETHOD(QueryObject)(VARIANT theObject);
  STDMETHOD(GetHelpFile)(LONG controlID, BSTR* pHelpFile);
  STDMETHOD(GetHelpId)(LONG controlID, LONG* pHelpID);
  STDMETHOD(Cancel)();

  // Dialog

  LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
  LRESULT OnBnClickedRadioConnected(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
  LRESULT OnBnClickedRadioDisconnected(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
  LRESULT OnCbnSelchangeComboShape(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);

private:
  INALayerPtr             m_ipNALayer;
  IConnectivitySolverPtr	m_ipConnectivitySolver;
  IDENetworkDatasetPtr    m_ipDENet;  

  HWND										m_hConnected;
  HWND                    m_hDisconnected;
  HWND                    m_hOutputShape;
};



