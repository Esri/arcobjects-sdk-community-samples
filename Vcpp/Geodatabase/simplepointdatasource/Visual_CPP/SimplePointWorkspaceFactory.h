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


// SimplePointWorkspaceFactory.h : Declaration of the CSimplePointWorkspaceFactory

#ifndef __SIMPLEPOINTWORKSPACEFACTORY_H_
#define __SIMPLEPOINTWORKSPACEFACTORY_H_

#include "resource.h"       // main symbols
#include "ClassFactory.h"		// custom class factory for singletons

// add DTC smart pointer for our interface
_COM_SMARTPTR_TYPEDEF(ISimplePointWorkspaceHelper, __uuidof(ISimplePointWorkspaceHelper));

// Use standard map template class to provide data structure
// for a set of <folder name, plug-in workspace helper> pairs.
#include <map>
typedef std::map<CComBSTR, IPlugInWorkspaceHelperPtr> PlugInWorkspaces;

/////////////////////////////////////////////////////////////////////////////
// CSimplePointWorkspaceFactory
class ATL_NO_VTABLE CSimplePointWorkspaceFactory : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSimplePointWorkspaceFactory, &CLSID_SimplePointWorkspaceFactory>,
	public ISupportErrorInfo,
	public ISimplePointWorkspaceFactory,
	public IPlugInWorkspaceFactoryHelper
{
public:
	CSimplePointWorkspaceFactory()
	{
	}

// Use a custom class factory - we want the class to be a singleton per-thread
DECLARE_CLASSFACTORY_EX(SingletonClassFactory<CSimplePointWorkspaceFactory>)

DECLARE_REGISTRY_RESOURCEID(IDR_SIMPLEPOINTWORKSPACEFACTORY)
DECLARE_GET_CONTROLLING_UNKNOWN()

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSimplePointWorkspaceFactory)
	COM_INTERFACE_ENTRY(ISimplePointWorkspaceFactory)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IPlugInWorkspaceFactoryHelper)
	COM_INTERFACE_ENTRY_AGGREGATE_BLIND(m_pInnerUnk)
END_COM_MAP()

BEGIN_CATEGORY_MAP(CSimplePointWorkspaceFactory)
	IMPLEMENTED_CATEGORY(__uuidof(CATID_WorkspaceFactory))
	IMPLEMENTED_CATEGORY(__uuidof(CATID_GxEnabledWorkspaceFactories))
END_CATEGORY_MAP()

	void FinalRelease();
	HRESULT FinalConstruct();

// ISupportsErrorInfo
  STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

public:
	// IPlugInWorkspaceFactoryHelper
	STDMETHOD(get_DataSourceName)(BSTR *Name);
	STDMETHOD(get_DatasetDescription)(esriDatasetType DatasetType, BSTR *dsDesc);
	STDMETHOD(get_WorkspaceDescription)(VARIANT_BOOL plural, BSTR *wksDesc);
	STDMETHOD(get_WorkspaceFactoryTypeID)(IUID **wksFactID);
	STDMETHOD(get_WorkspaceType)(esriWorkspaceType *wksType);
	STDMETHOD(get_CanSupportSQL)(VARIANT_BOOL *CanSupportSQL);
	STDMETHOD(IsWorkspace)(BSTR wksString, VARIANT_BOOL *isWks);
	STDMETHOD(ContainsWorkspace)(BSTR parentDirectory, IFileNames *FileNames, VARIANT_BOOL *ContainsWorkspace);
	STDMETHOD(GetWorkspaceString)(BSTR parentDirectory, IFileNames *FileNames, BSTR *wksString);
	STDMETHOD(OpenWorkspace)(BSTR wksString, IPlugInWorkspaceHelper **wksHelper);

	IUnknown *m_pInnerUnk; // PlugInWorkspaceFactory Aggregation
	PlugInWorkspaces m_mapWorkspaces; // cache of open workspace pointers 
};

#endif //__SIMPLEPOINTWORKSPACEFACTORY_H_
