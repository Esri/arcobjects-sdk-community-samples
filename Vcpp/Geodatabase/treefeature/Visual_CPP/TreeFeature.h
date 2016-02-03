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


// TreeFeature.h : Declaration of the CTreeFeature

#ifndef __TREEFEATURE_H_
#define __TREEFEATURE_H_

#include "resource.h"       // main symbols

_COM_SMARTPTR_TYPEDEF(ITreeFeature, __uuidof(ITreeFeature));

/////////////////////////////////////////////////////////////////////////////
// CTreeFeature
class ATL_NO_VTABLE CTreeFeature : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CTreeFeature, &CLSID_TreeFeature>,
	public ISupportErrorInfo,
	public ITreeFeature
{
public:
	CTreeFeature() :
		m_pInnerUnk(NULL),
		m_pFeature(NULL)
	{
	}

DECLARE_GET_CONTROLLING_UNKNOWN()

DECLARE_REGISTRY_RESOURCEID(IDR_TREEFEATURE)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CTreeFeature)
	COM_INTERFACE_ENTRY(ITreeFeature)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY_AGGREGATE_BLIND(m_pInnerUnk)
END_COM_MAP()

BEGIN_CATEGORY_MAP(CTreeFeature)
	IMPLEMENTED_CATEGORY(__uuidof(CATID_GeoObjects))
END_CATEGORY_MAP()
	
// FinalConstruct/FinalRelease prototypes
HRESULT	FinalConstruct();
void    FinalRelease();

// ISupportsErrorInfo
STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// ITreeFeature
STDMETHOD(get_Age)(/*[out, retval]*/ long *pVal);

IUnknown*     m_pInnerUnk;     // pointer to inner unknown
IFeature*     m_pFeature;			 // weak reference

};

#endif //__TREEFEATURE_H_
