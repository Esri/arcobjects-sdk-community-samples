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



#include "stdafx.h"
#include "ConnSolverPropPage.h"

// ConnSolverPropPage

/////////////////////////////////////////////////////////////////////////////
// IPropertyPage

STDMETHODIMP ConnSolverPropPage::Show(UINT nCmdShow)
{
  // If we are showing the property page, propulate it
  // from the connectivity solver object.
  if ((nCmdShow & (SW_SHOW|SW_SHOWDEFAULT)))
  {
    // Set dialog radio button states based on the current solver state
    outputConnectivityType connectivityOutput = outputDisconnectedLines;
    m_ipConnectivitySolver->get_OutputConnectivity(&connectivityOutput);

    // Set "Disconnected Lines" radio button
    WPARAM checkState = (WPARAM)(connectivityOutput == outputDisconnectedLines ? BST_CHECKED : BST_UNCHECKED);
    ::SendMessage(m_hDisconnected, BM_SETCHECK, checkState, 0);

    // Set "Connected Lines" radio button
    checkState = (WPARAM)(connectivityOutput == outputConnectedLines ? BST_CHECKED : BST_UNCHECKED);
    ::SendMessage(m_hConnected, BM_SETCHECK, checkState, 0);

    // Set output shape type combo based on the current solver state
    esriNAOutputLineType outputType;
    m_ipConnectivitySolver->get_OutputLines(&outputType);

    // Set "Output Shape" combo box
    WPARAM selectedIndex;
    switch(outputType)
    {
      case esriNAOutputLineNone:
      {
        selectedIndex = 0;
        break;
      }
      case esriNAOutputLineTrueShape:
      {
        selectedIndex = 1;
        break;
      }
    }
    ::SendMessage(m_hOutputShape, CB_SETCURSEL, selectedIndex, 0);
  }

  // Let the IPropertyPageImpl deal with displaying the page
  return IPropertyPageImpl<ConnSolverPropPage>::Show(nCmdShow);
}

STDMETHODIMP ConnSolverPropPage::SetObjects(ULONG nObjects, IUnknown** ppUnk)
{
  m_ipNALayer = 0;
  m_ipConnectivitySolver = 0;
  m_ipDENet = 0;

  // Loop through the objects to find one that supports
  // the INALayer interface.
  for (ULONG i=0; i < nObjects; i ++)
  {
    INALayerPtr ipNALayer(ppUnk[i]);
    if (!ipNALayer)
      continue;

    VARIANT_BOOL         isValid;
    INetworkDatasetPtr   ipNetworkDataset;
    IDEDatasetPtr        ipDEDataset;
    IDENetworkDatasetPtr ipDENet;
    INASolverPtr         ipSolver;
    INAContextPtr        ipNAContext;

    ipNALayer->get_Context(&ipNAContext);
    if (!ipNAContext)
      continue;
    ipNAContext->get_Solver(&ipSolver);
    if (!(IConnectivitySolverPtr)ipSolver)
      continue;

    ((ILayerPtr)ipNALayer)->get_Valid(&isValid);
    if (isValid == VARIANT_FALSE)
      continue;

    ipNAContext->get_NetworkDataset(&ipNetworkDataset);
    if (!ipNetworkDataset)
      continue;

    ((IDatasetComponentPtr)ipNetworkDataset)->get_DataElement(&ipDEDataset);
    ipDENet = ipDEDataset;

    if (!ipDENet)
      continue;

    m_ipNALayer = ipNALayer;
    m_ipConnectivitySolver = ipSolver;
    m_ipDENet = ipDENet;
  }

  // Let the IPropertyPageImpl know what objects we have
  return IPropertyPageImpl<ConnSolverPropPage>::SetObjects(nObjects, ppUnk);
}

STDMETHODIMP ConnSolverPropPage::Apply(void)
{
  // Pass the m_ipConnectivitySolver member variable to the QueryObject method
  HRESULT hr = QueryObject(CComVariant((IUnknown*) m_ipConnectivitySolver));

  // Set the page to not dirty
  SetDirty(FALSE);

  return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IPropertyPageContext

STDMETHODIMP ConnSolverPropPage::get_Priority(LONG* pPriority)
{
  if (pPriority == NULL)
    return E_POINTER;

  (*pPriority) = 152;

  return S_OK;
}

STDMETHODIMP ConnSolverPropPage::Applies(VARIANT unkArray, VARIANT_BOOL* pApplies)
{
  if (pApplies == NULL)
    return E_INVALIDARG;

  (*pApplies) = VARIANT_FALSE;

  if (V_VT(&unkArray) != (VT_ARRAY|VT_UNKNOWN))
    return E_INVALIDARG;

  // Retrieve the safe array and retrieve the data
  SAFEARRAY *saArray = unkArray.parray;
  HRESULT hr = ::SafeArrayLock(saArray);

  IUnknownPtr *pUnk;
  hr = ::SafeArrayAccessData(saArray,reinterpret_cast<void**> (&pUnk));
  if (FAILED(hr)) return hr;

  // Loop through the elements and see if an NALayer with a valid IConnectivitySolver is present
  long lNumElements = saArray->rgsabound->cElements;
  for (long i = 0; i < lNumElements; i++)
  {
    INALayerPtr ipNALayer(pUnk[i]);
    if (!ipNALayer)
      continue;

    VARIANT_BOOL         isValid;
    INetworkDatasetPtr   ipNetworkDataset;
    IDEDatasetPtr        ipDEDataset;
    IDENetworkDatasetPtr ipDENet;
    INASolverPtr         ipSolver;
    INAContextPtr        ipNAContext;

    ipNALayer->get_Context(&ipNAContext);
    if (!ipNAContext)
      continue;
    ipNAContext->get_Solver(&ipSolver);
    if (!(IConnectivitySolverPtr)ipSolver)
      continue;

    ((ILayerPtr)ipNALayer)->get_Valid(&isValid);
    if (isValid == VARIANT_FALSE)
      continue;

    ipNAContext->get_NetworkDataset(&ipNetworkDataset);
    if (!ipNetworkDataset)
      continue;

    ((IDatasetComponentPtr)ipNetworkDataset)->get_DataElement(&ipDEDataset);
    ipDENet = ipDEDataset;

    if (!ipDENet)
      continue;

    (*pApplies) = VARIANT_TRUE;
    break;
  }

  // Cleanup
  hr = ::SafeArrayUnaccessData(saArray);
  if (FAILED(hr)) return hr;

  hr = ::SafeArrayUnlock(saArray);
  if (FAILED(hr)) return hr;

  return S_OK;
}

STDMETHODIMP ConnSolverPropPage::CreateCompatibleObject(VARIANT kind, VARIANT* pNewObject)
{
  if (pNewObject == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}

STDMETHODIMP ConnSolverPropPage::QueryObject(VARIANT theObject)
{
  // Check if we have a marker symbol
  // If we do, apply the setting from the page.
  CComVariant vObject(theObject);
  if (vObject.vt != VT_UNKNOWN) return E_UNEXPECTED;
  // Try and QI to IConnectivitySolver
  IConnectivitySolverPtr ipSolver(vObject.punkVal);
  if (ipSolver != 0)
  {
    // Set properties of the solver object
    LRESULT checkState = ::SendMessage(m_hDisconnected, BM_GETCHECK, 0, 0);
    outputConnectivityType connectivityOutput = (checkState == BST_CHECKED) ? outputDisconnectedLines : outputConnectedLines;
    ipSolver->put_OutputConnectivity(connectivityOutput);
    LRESULT selectedIndex = ::SendMessage(m_hOutputShape, CB_GETCURSEL, 0, 0);
    switch(selectedIndex)
    {
      case 0:
      {
        ipSolver->put_OutputLines(esriNAOutputLineNone);
        break;
      }
      case 1:
      {
        ipSolver->put_OutputLines(esriNAOutputLineTrueShape);
        break;
      }
    }
  }
  return S_OK;
}

STDMETHODIMP ConnSolverPropPage::GetHelpFile(LONG controlID, BSTR* pHelpFile)
{
  if (pHelpFile == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}

STDMETHODIMP ConnSolverPropPage::GetHelpId(LONG controlID, LONG* pHelpID)
{
  if (pHelpID == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}

STDMETHODIMP ConnSolverPropPage::Cancel()
{
  // In this case do nothing
  return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// Dialog

LRESULT ConnSolverPropPage::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{
  m_hConnected = GetDlgItem(IDC_RADIO_CONNECTED);
  m_hDisconnected = GetDlgItem(IDC_RADIO_DISCONNECTED);
  m_hOutputShape = GetDlgItem(IDC_COMBO_SHAPE);
  LPARAM noShape = (LPARAM)_T("None");
  LPARAM trueShape = (LPARAM)_T("True Shape");
  ::SendMessage(m_hOutputShape, CB_ADDSTRING, 0, noShape);
  ::SendMessage(m_hOutputShape, CB_ADDSTRING, 0, trueShape);
  return 0;
}

LRESULT ConnSolverPropPage::OnBnClickedRadioConnected(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
  SetDirty(TRUE);
  //refresh property sheet
  m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
  return 0;
}

LRESULT ConnSolverPropPage::OnBnClickedRadioDisconnected(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
  SetDirty(TRUE);
  //refresh property sheet
  m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
  return 0;
}

LRESULT ConnSolverPropPage::OnCbnSelchangeComboShape(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled)
{
  SetDirty(TRUE);
  //refresh property sheet
  m_pPageSite->OnStatusChange(PROPPAGESTATUS_DIRTY);
  return 0;
}

