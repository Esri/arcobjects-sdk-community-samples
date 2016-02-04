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


// SimplePointWorkspaceHelper.h : Declaration of the CSimplePointWorkspaceHelper

#ifndef __SIMPLEPOINTWORKSPACEHELPER_H_
#define __SIMPLEPOINTWORKSPACEHELPER_H_

#include "resource.h"       // main symbols

// add DTC smart pointer for our interface
_COM_SMARTPTR_TYPEDEF(ISimplePointDatasetHelper, __uuidof(ISimplePointDatasetHelper));

// Use standard map template class to provide data structure
// for a set of <filename, plug-in dataset helper> pairs.
#include <map>
typedef std::map<CComBSTR, IPlugInDatasetInfoPtr> PlugInDatasets;

/////////////////////////////////////////////////////////////////////////////
// CSimplePointWorkspaceHelper
class ATL_NO_VTABLE CSimplePointWorkspaceHelper : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSimplePointWorkspaceHelper, &CLSID_SimplePointWorkspaceHelper>,
	public ISupportErrorInfo,
	public ISimplePointWorkspaceHelper,
	public IPlugInWorkspaceHelper 
{
public:
	CSimplePointWorkspaceHelper()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SIMPLEPOINTWORKSPACEHELPER)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSimplePointWorkspaceHelper)
	COM_INTERFACE_ENTRY(ISimplePointWorkspaceHelper)
	COM_INTERFACE_ENTRY(IPlugInWorkspaceHelper)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// ISimplePointWorkspaceHelper
	STDMETHOD(put_WorkspacePath)(BSTR newVal);
// IPlugInWorkspaceHelper
	STDMETHOD(get_RowCountIsCalculated)(VARIANT_BOOL *rowCountCalculated);
	STDMETHOD(get_OIDIsRecordNumber)(VARIANT_BOOL *OIDIsRecordNumber);
	STDMETHOD(get_NativeType)(esriDatasetType DatasetType, BSTR localName, INativeType **ppNativeType);
	STDMETHOD(get_DatasetNames)(esriDatasetType DatasetType, IArray **DatasetNames);
	STDMETHOD(OpenDataset)(BSTR localName, IPlugInDatasetHelper **datasetHelper);

private:
	HRESULT CreatePlugInDatasetHelper(BSTR fileName, IPlugInDatasetInfo ** ppPlugInDatasetInfo);
	CComBSTR m_sWorkspacePath;    // file path to the workspace
	PlugInDatasets m_mapDatasets; // cache of open dataset pointers 

};

#endif //__SIMPLEPOINTWORKSPACEHELPER_H_
