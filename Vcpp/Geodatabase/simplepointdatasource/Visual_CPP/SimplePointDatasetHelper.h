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


// SimplePointDatasetHelper.h : Declaration of the CSimplePointDatasetHelper

#ifndef __SIMPLEPOINTDATASETHELPER_H_
#define __SIMPLEPOINTDATASETHELPER_H_

#include "resource.h"       // main symbols

// add DTC smart pointer for our interface
_COM_SMARTPTR_TYPEDEF(ISimplePointCursorHelper, __uuidof(ISimplePointCursorHelper));

/////////////////////////////////////////////////////////////////////////////
// CSimplePointDatasetHelper
class ATL_NO_VTABLE CSimplePointDatasetHelper : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSimplePointDatasetHelper, &CLSID_SimplePointDatasetHelper>,
	public ISupportErrorInfo,
	public IPlugInDatasetHelper,
	public IPlugInDatasetInfo,
	public ISimplePointDatasetHelper
{
public:
	CSimplePointDatasetHelper()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SIMPLEPOINTDATASETHELPER)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSimplePointDatasetHelper)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IPlugInDatasetHelper)
	COM_INTERFACE_ENTRY(IPlugInDatasetInfo)
	COM_INTERFACE_ENTRY(ISimplePointDatasetHelper)
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IPlugInDatasetInfo
	STDMETHOD(get_LocalDatasetName)(BSTR *localName);
	STDMETHOD(get_DatasetType)(esriDatasetType *DatasetType);
	STDMETHOD(get_GeometryType)(esriGeometryType *GeometryType);
	STDMETHOD(get_ShapeFieldName)(BSTR *ShapeFieldName);

// IPlugInDatasetHelper
	STDMETHOD(get_ClassCount)(long *Count);
	STDMETHOD(get_ClassName)(long Index, BSTR *Name);
	STDMETHOD(get_ClassIndex)(BSTR Name, long *Index);
	STDMETHOD(get_Bounds)(IEnvelope **Bounds);
	STDMETHOD(get_Fields)(long ClassIndex, IFields **FieldSet);
	STDMETHOD(get_OIDFieldIndex)(long ClassIndex, long *OIDFieldIndex);
	STDMETHOD(get_ShapeFieldIndex)(long ClassIndex, long *ShapeFieldIndex);
	STDMETHOD(FetchByID)(long ClassIndex, long ID, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper);
	STDMETHOD(FetchAll)(long ClassIndex, BSTR WhereClause, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper);
	STDMETHOD(FetchByEnvelope)(long ClassIndex, IEnvelope *env, VARIANT_BOOL strictSearch, BSTR WhereClause, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper);

// ISimplePointDatasetHelper
	STDMETHOD(put_DatasetName)(BSTR newVal);
	STDMETHOD(put_WorkspacePath)(BSTR newVal);
private:
	CComBSTR m_sDatasetName;
	CComBSTR m_sWorkspacePath;
};

#endif //__SIMPLEPOINTDATASETHELPER_H_
