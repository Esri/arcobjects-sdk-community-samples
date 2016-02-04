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
// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.3/userestrictions.txt.
// 



// TreeFeature.cpp : Implementation of CTreeFeature
#include "stdafx.h"
#include "Tree.h"
#include "TreeFeature.h"
#include <windows.h>

/////////////////////////////////////////////////////////////////////////////
// CTreeFeature

STDMETHODIMP CTreeFeature::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_ITreeFeature
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

HRESULT CTreeFeature::FinalConstruct()
{
	HRESULT hr;

	// Get outer object since this object may be aggregated in as well.
  IUnknown *pOuter = GetControllingUnknown();

	// Aggregate in ESRI's simple Feature object
  hr = CoCreateInstance(CLSID_Feature,
												pOuter,
												CLSCTX_INPROC_SERVER,
												IID_IUnknown,
												(void**) &m_pInnerUnk);
	if (FAILED(hr)) return E_FAIL;

	hr = m_pInnerUnk->QueryInterface(IID_IFeature, (void**)&m_pFeature);
	if (FAILED(hr)) return E_FAIL;
	// To make the Final Release easier, this interface pointer has been declared as
	// a weak reference, so we need to do a release. This should be done on the
	// outer unknown since the inner object will have delegated the Addref call to the 
	// outer unknown.
  pOuter->Release();

  return S_OK;
}

void CTreeFeature::FinalRelease()
{
  // release reference to inner IUnknown
  m_pInnerUnk->Release();
}


STDMETHODIMP CTreeFeature::get_Age(long *pVal)
{
	HRESULT hr;

	IFieldsPtr ipFields;
	hr = m_pFeature->get_Fields(&ipFields);
	if (FAILED(hr)) return E_FAIL;

	long lPlantedYearField;
	hr = ipFields->FindField(L"YEAR_PLANTED",&lPlantedYearField);
	if (FAILED(hr)) return E_FAIL;

  if (lPlantedYearField == -1) 
	{
		AtlReportError(CLSID_TreeFeature, _T("Required YEAR_PLANTED field not found"), IID_ITreeFeature, E_FAIL);
		return E_FAIL;
	}

	long lPlantedYear;
	CComVariant vVal;
	hr = m_pFeature->get_Value(lPlantedYearField, &vVal);
  if (vVal.vt == VT_I4)
		lPlantedYear = vVal.lVal;
	else if (vVal.vt == VT_I2)
		lPlantedYear = vVal.iVal;
  else 
	{
		AtlReportError(CLSID_TreeFeature, _T("YEAR_PLANTED not numeric"), IID_ITreeFeature, E_FAIL);
		return E_FAIL;
	}

	SYSTEMTIME current_time;
	::GetLocalTime(&current_time);
	
	long age;
	age = current_time.wYear - lPlantedYear;

	if (age <= 0)
	{
		AtlReportError(CLSID_TreeFeature, _T("Invalid value for YEAR_PLANTED found"), IID_ITreeFeature, E_FAIL);
		return E_FAIL;
	}

  *pVal = age;  	
	return S_OK;
}

