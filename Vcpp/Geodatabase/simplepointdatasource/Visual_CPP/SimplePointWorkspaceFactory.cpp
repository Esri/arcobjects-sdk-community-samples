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



// SimplePointWorkspaceFactory.cpp : Implementation of CSimplePointWorkspaceFactory
#include "stdafx.h"
#include "SimplePointVC.h"
#include "SimplePointWorkspaceFactory.h"
#include "GeneralUtils.h"

/////////////////////////////////////////////////////////////////////////////
// CSimplePointWorkspaceFactory

STDMETHODIMP CSimplePointWorkspaceFactory::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_ISimplePointWorkspaceFactory
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}


HRESULT CSimplePointWorkspaceFactory::FinalConstruct()
{
	// CoCreate the PlugInWorkspaceFactory that is aggregated into our WorkspaceFactory implementation
  IUnknown* pOuter = GetControllingUnknown();
  if (FAILED (CoCreateInstance(__uuidof(PlugInWorkspaceFactory), pOuter, CLSCTX_INPROC_SERVER, IID_IUnknown, (void**) &m_pInnerUnk)))
    return E_FAIL;
	
	return S_OK;
}

void CSimplePointWorkspaceFactory::FinalRelease()
{
	// Free the aggregated PlugInWorkspaceFactory object
	m_pInnerUnk->Release();


	// tidy up the cache in the class factory
	ISingletonRelease *pClassFactory;

	HRESULT hr;
	hr = _Module.GetClassObject(CLSID_SimplePointWorkspaceFactory, IID_ISingletonRelease, (void **) &pClassFactory);
	if (hr == S_OK)
		if (pClassFactory != NULL)
			pClassFactory->ReleaseInstance();
}


// IPlugInWorkspaceFactoryHelper
STDMETHODIMP CSimplePointWorkspaceFactory::get_DataSourceName(BSTR *Name)
{
	if (! Name) return E_POINTER;
		
  CComBSTR str(L"SimplePoint");
	*Name = str.Detach();
	return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::get_DatasetDescription(esriDatasetType DatasetType, BSTR *dsDesc)
{
	if (! dsDesc) return E_POINTER;

	CComBSTR str;
	// we only support feature classes
  switch (DatasetType)
  {
  case esriDTFeatureClass:
    str = L"SimplePoint Feature Class";
    break;
  default:
    str = L"";
  }

	*dsDesc = str.Detach();
	return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::get_WorkspaceDescription(VARIANT_BOOL plural, BSTR *wksDesc)
{
	if (! wksDesc) return E_POINTER;

  CComBSTR str(L"SimplePoint data");
	*wksDesc = str.Detach();
	return S_OK; 
}

//Returns the CLSID of this class.  
//This is needed by the IWorkspaceFactory interface of the PlugInWorkspaceFactory class
STDMETHODIMP CSimplePointWorkspaceFactory::get_WorkspaceFactoryTypeID(IUID **wksFactID)
{
	HRESULT hr;
	if (! wksFactID) return E_POINTER;

  IUIDPtr ipUID(CLSID_UID);
	hr = ipUID->put_Value(CComVariant("{65E5BA1E-721A-11D6-8AD9-00104BB6FCCB}"));
	if (FAILED(hr)) return hr;

  *wksFactID = ipUID.Detach(); // pass ownership of object to client;
	return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::get_WorkspaceType(esriWorkspaceType *wksType)
{
	if (! wksType) return E_POINTER;

  *wksType = esriFileSystemWorkspace;
  return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::get_CanSupportSQL(VARIANT_BOOL *CanSupportSQL)
{
	if (! CanSupportSQL) return E_POINTER;

  *CanSupportSQL = VARIANT_FALSE;
  return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::IsWorkspace(BSTR wksString, VARIANT_BOOL *isWks)
{
	USES_CONVERSION;
	*isWks = VARIANT_FALSE;

	if (!IsDirectory(wksString))
		return S_FALSE;

	// verify that a .spt file exists
	CComBSTR sFilter = "*";
	sFilter.Append(g_sExt);

	if (!FileTypeExists(OLE2CT(wksString), OLE2CT(sFilter)))
		return S_FALSE;
	
	*isWks = VARIANT_TRUE;
	return S_OK;
}

STDMETHODIMP CSimplePointWorkspaceFactory::ContainsWorkspace(BSTR parentDirectory, IFileNames *FileNames, VARIANT_BOOL *ContainsWorkspace)
{
  if (! ContainsWorkspace) return E_POINTER;
	if (! parentDirectory) return E_POINTER;

  if (! FileNames)
    return IsWorkspace(parentDirectory, ContainsWorkspace);

	*ContainsWorkspace = VARIANT_FALSE;

	if (!IsDirectory(parentDirectory))
		return S_FALSE;

	CComBSTR sName; 
	CComBSTR sExt;
  VARIANT_BOOL bIsDir;
  FileNames->Reset();
  
	while (S_OK == FileNames->Next(&sName))
  {
    FileNames->IsDirectory(&bIsDir); // is the current file a directory?
    if (bIsDir)
      continue;

		ReturnFileExtension(sName, &sExt);
		sExt.ToUpper();
		if (sExt == g_sExtUpper)
    {
      *ContainsWorkspace = VARIANT_TRUE;
      return S_OK;
    }
  }

	return S_FALSE;
}

STDMETHODIMP CSimplePointWorkspaceFactory::GetWorkspaceString(BSTR parentDirectory, IFileNames *FileNames, BSTR *wksString)
{
	HRESULT hr;
	if (!parentDirectory || ! wksString) return E_POINTER;

	// Initialize return string to null
	*wksString = 0;

	VARIANT_BOOL bIsWorkspace = VARIANT_FALSE;

	if (! FileNames)
	{
		 hr = IsWorkspace(parentDirectory, &bIsWorkspace);
     if (!bIsWorkspace)
			 return S_FALSE;
	}
  else 
	{
  	if (!IsDirectory(parentDirectory))
	  	return S_FALSE;

		CComBSTR sName;
		CComBSTR sExt;
		VARIANT_BOOL bIsDir = VARIANT_FALSE;
		FileNames->Reset();
  
		while (S_OK == FileNames->Next(&sName))
		{
			FileNames->IsDirectory(&bIsDir); // is the current file a directory?
			if (bIsDir) continue;

			ReturnFileExtension(sName, &sExt);
			sExt.ToUpper();
			if (sExt == g_sExtUpper)
			{
				bIsWorkspace = VARIANT_TRUE;
				FileNames->Remove();
			}
		}
  }

	// if it is a workspace, set the workspace string to be the parent directory
	if (bIsWorkspace)
	{
		CComBSTR sWorkspaceString(parentDirectory);		
		*wksString = sWorkspaceString.Detach();
		return S_OK;
	}

	return S_FALSE;
}

STDMETHODIMP CSimplePointWorkspaceFactory::OpenWorkspace(BSTR wksString, IPlugInWorkspaceHelper **wksHelper)
{
	HRESULT hr;
	USES_CONVERSION;

	if (! wksHelper ) return E_POINTER;
	
	(*wksHelper) = NULL;

	// Check its a folder
 	if (!IsDirectory(wksString))
	{
		CComBSTR sError(L"Workspace string invalid: ");
		sError.Append(wksString);
  	AtlReportError(CLSID_SimplePointWorkspaceFactory, sError, IID_IPlugInWorkspaceFactoryHelper, E_FAIL);
		return E_FAIL;
	}

  IPlugInWorkspaceHelperPtr ipPlugInWorkspaceHelper;

	// Check if the workspace is in our cache (i.e. is already open)
	PlugInWorkspaces::iterator p;
	p = m_mapWorkspaces.find(wksString);
	if (p != m_mapWorkspaces.end())
	{
		ipPlugInWorkspaceHelper = p->second;
	}
  else
  {
		// create the plug-in workspace helper
		ISimplePointWorkspaceHelperPtr ipSimplePointWorkspaceHelper;
		hr = ipSimplePointWorkspaceHelper.CreateInstance(CLSID_SimplePointWorkspaceHelper);
		if (FAILED(hr)) return hr;
			
		// initialize the workspace helper
		hr = ipSimplePointWorkspaceHelper->put_WorkspacePath(wksString);
		if (FAILED(hr)) return hr;

		ipPlugInWorkspaceHelper = ipSimplePointWorkspaceHelper;
		if (ipPlugInWorkspaceHelper == NULL) return E_FAIL;

		// add to the cache
		m_mapWorkspaces.insert(PlugInWorkspaces::value_type(wksString, ipPlugInWorkspaceHelper));
  }

  *wksHelper = ipPlugInWorkspaceHelper.Detach(); // pass ownership of pointer to client
	return S_OK;
}


