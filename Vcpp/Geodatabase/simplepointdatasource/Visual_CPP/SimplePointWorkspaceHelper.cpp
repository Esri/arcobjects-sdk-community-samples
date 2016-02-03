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



// SimplePointWorkspaceHelper.cpp : Implementation of CSimplePointWorkspaceHelper
#include "stdafx.h"
#include "SimplePointVC.h"
#include "SimplePointWorkspaceHelper.h"
#include "GeneralUtils.h"

/////////////////////////////////////////////////////////////////////////////
// CSimplePointWorkspaceHelper

STDMETHODIMP CSimplePointWorkspaceHelper::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_ISimplePointWorkspaceHelper
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

// IPlugInWorkspaceHelper
STDMETHODIMP CSimplePointWorkspaceHelper::get_RowCountIsCalculated(VARIANT_BOOL *rowCountCalculated)
{
	if (! rowCountCalculated) return E_POINTER;

  *rowCountCalculated = VARIANT_TRUE;
  return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceHelper::get_OIDIsRecordNumber(VARIANT_BOOL *OIDIsRecordNumber)
{
	if (! OIDIsRecordNumber) return E_POINTER;

  *OIDIsRecordNumber = VARIANT_TRUE;
  return S_OK;

}

STDMETHODIMP CSimplePointWorkspaceHelper::get_NativeType(esriDatasetType DatasetType, BSTR localName, INativeType **ppNativeType)
{
  // We are not implementing native types
	return E_NOTIMPL;

}

STDMETHODIMP CSimplePointWorkspaceHelper::get_DatasetNames(esriDatasetType DatasetType, IArray **DatasetNames)
{
	HRESULT hr;
	USES_CONVERSION;

	if (! DatasetNames) return E_POINTER;

	// Create an empty array
	IArrayPtr ipArray;
	hr = ipArray.CreateInstance(CLSID_Array);
  if (FAILED(hr)) return hr;

	// the only two that we support
	if (DatasetType != esriDTAny && DatasetType != esriDTFeatureClass)
  {
	  *DatasetNames = ipArray.Detach(); // pass ownership of object to client;		
		return S_FALSE;
	}
	// look for each file with the correct extension
	// first make a file filter path e.g. "D:/data/*.spt"
  CComBSTR sPathFilter = m_sWorkspacePath;
	sPathFilter.Append(L"*");
	sPathFilter.Append(g_sExt);

	// try to find the first file.
	HANDLE hSearch;
	WIN32_FIND_DATA findData;
	hSearch = ::FindFirstFile( OLE2CT(sPathFilter), &findData);
	if (hSearch == INVALID_HANDLE_VALUE)
  {
	  *DatasetNames = ipArray.Detach(); // pass ownership of object to client;		
		return S_FALSE;
	}
	
	// Create the dataset helper for the first file 
	IPlugInDatasetInfoPtr ipPlugInDatasetInfo;
	CComBSTR fileName = T2OLE(findData.cFileName);
	hr = CreatePlugInDatasetHelper(fileName, &ipPlugInDatasetInfo);
	if (FAILED(hr)) return hr;

	// Add it to the array - no need to call Addref, since the Add method will do it.
	IUnknownPtr ipUnk = ipPlugInDatasetInfo;
	ipArray->Add(ipUnk);

	// for each additional file
	while (0 != FindNextFile(hSearch, &findData))
	{
		fileName = findData.cFileName;
		// Create the the dataset helper
		// note - the & operator releases the previous object
  	hr = CreatePlugInDatasetHelper(fileName, &ipPlugInDatasetInfo);
	  if (FAILED(hr)) return hr;
		
		IUnknownPtr ipUnk = ipPlugInDatasetInfo;
		ipArray->Add(ipUnk);
	}
	
	*DatasetNames = ipArray.Detach(); // pass ownership of object to client;

	::FindClose(hSearch);
	return S_OK;

}

STDMETHODIMP CSimplePointWorkspaceHelper::OpenDataset(BSTR localName, IPlugInDatasetHelper **datasetHelper)
{
  HRESULT hr;
	USES_CONVERSION;

  if (! datasetHelper) return E_POINTER;

	CComBSTR sFileName = localName;

  // Check if the dataset is valid
	CComBSTR sFullPath = m_sWorkspacePath;
	sFullPath.Append(sFileName);
	
	if (!FileExists(OLE2CT(sFullPath)))
	{
		CComBSTR sError(L"Dataset does not exist: ");
		sError.Append(localName);
  	AtlReportError(CLSID_SimplePointWorkspaceHelper, sError, IID_IPlugInWorkspaceHelper, E_FAIL);
		return E_FAIL;
	}

  // Create the dataset helper
	IPlugInDatasetInfoPtr ipPlugInDatasetInfo;
	hr = CreatePlugInDatasetHelper(sFileName, &ipPlugInDatasetInfo);
	if (FAILED(hr)) return hr;

	IPlugInDatasetHelperPtr ipPlugInDatasetHelper = ipPlugInDatasetInfo;
  if (ipPlugInDatasetHelper == NULL) return E_FAIL;

	*datasetHelper = ipPlugInDatasetHelper.Detach(); // pass ownership of object to client;

	return S_OK;
}

// ISimplePointWorkspaceHelper methods
STDMETHODIMP CSimplePointWorkspaceHelper::put_WorkspacePath(BSTR newVal)
{
	m_sWorkspacePath = newVal;

	if (m_sWorkspacePath.Length() == 0) return E_FAIL;

	if (m_sWorkspacePath[m_sWorkspacePath.Length() - 1] != L'\\')
		m_sWorkspacePath.Append(L"\\");

	return S_OK;
}

// helper functions
HRESULT CSimplePointWorkspaceHelper::CreatePlugInDatasetHelper(BSTR fileName, IPlugInDatasetInfo ** ppPlugInDatasetInfo)
{
	HRESULT hr;
	if (! ppPlugInDatasetInfo) return E_POINTER;

	IPlugInDatasetInfoPtr ipPlugInDatasetInfo;
	
	// First check to see if the specified dataset is already in our cache
	PlugInDatasets::iterator p;
	p = m_mapDatasets.find(fileName);
	if (p != m_mapDatasets.end())
	{
		ipPlugInDatasetInfo = p->second;
	}
  else 
	{
		// Create the new object
		ISimplePointDatasetHelperPtr ipSimplePointDatasetHelper;
		hr = ipSimplePointDatasetHelper.CreateInstance(CLSID_SimplePointDatasetHelper);
		if (FAILED(hr)) return hr;

		// Initialize it with the dataset name
		hr = ipSimplePointDatasetHelper->put_DatasetName(fileName);
		if (FAILED(hr)) return hr;

		// Initialize it with the workspace path
		hr = ipSimplePointDatasetHelper->put_WorkspacePath(m_sWorkspacePath);
		if (FAILED(hr)) return hr;

		// QI for the IPlugInDatasetInfo
		ipPlugInDatasetInfo = ipSimplePointDatasetHelper;
		if (ipPlugInDatasetInfo == NULL) return E_FAIL;

		// Add it to the cache
    m_mapDatasets.insert(PlugInDatasets::value_type(fileName, ipPlugInDatasetInfo));
	}

  *ppPlugInDatasetInfo = ipPlugInDatasetInfo.Detach();

	return S_OK;
}

