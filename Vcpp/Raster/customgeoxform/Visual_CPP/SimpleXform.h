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


// SimpleXform.h : Declaration of the SimpleXform

#ifndef __SIMPLEXFORM_H_
#define __SIMPLEXFORM_H_

#include "resource.h"       // main symbols

_COM_SMARTPTR_TYPEDEF(ISimpleXform, __uuidof(ISimpleXform));

/////////////////////////////////////////////////////////////////////////////
// SimpleXform
class ATL_NO_VTABLE SimpleXform : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<SimpleXform, &CLSID_SimpleXform>,
	public ISupportErrorInfo,
	public IGeodataXform,
	public ISimpleXform,
	public IPersistStream,
  public IClone
{
public:
	SimpleXform();

  DECLARE_REGISTRY_RESOURCEID(IDR_CUSTOMXFORM)
  DECLARE_PROTECT_FINAL_CONSTRUCT()

  BEGIN_COM_MAP(SimpleXform)
	  COM_INTERFACE_ENTRY(ISupportErrorInfo)
	  COM_INTERFACE_ENTRY(IGeodataXform)
	  COM_INTERFACE_ENTRY(ISimpleXform)
	  COM_INTERFACE_ENTRY(IPersistStream)
	  COM_INTERFACE_ENTRY(IClone)
  END_COM_MAP()

  HRESULT FinalConstruct();
  void    FinalRelease();

public:
	// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

	// ISimpleXform
	STDMETHOD(get_Name)(BSTR* pName);
	STDMETHOD(put_Name)(BSTR name);
  
  // IGeodataXform methods
  STDMETHOD(get_SpatialReference)(ISpatialReference** ppSpatialReference);
  STDMETHOD(putref_SpatialReference)(ISpatialReference* pSpatialReference);
  STDMETHOD(get_Domains)(IGeometryCollection** ppDomains);
  STDMETHOD(get_IsIdentity)(VARIANT_BOOL* pIsIdentity);
  STDMETHOD(Transform)(esriTransformDirection direction, long npoints, WKSPoint* points);
  STDMETHOD(TransformCellsize)(esriTransformDirection direction, double* dx, double* dy, IEnvelope* pAreaOfInterest);
  STDMETHOD(TransformExtent)(esriTransformDirection direction, IEnvelope* pExtent);
  STDMETHOD(TransformPoints)(esriTransformDirection direction, IPointCollection* pPoints);

	// IPersistStream
	STDMETHOD(IsDirty)();
	STDMETHOD(Load)(IStream * pstm);
	STDMETHOD(Save)(IStream * pstm, BOOL fClearDirty);
	STDMETHOD(GetSizeMax)(_ULARGE_INTEGER * pcbSize);
	STDMETHOD(GetClassID)(CLSID* pClassID);

  // IClone methods
  STDMETHOD(Clone)(IClone** ppClone);
  STDMETHOD(Assign)(IClone* pOther);
  STDMETHOD(IsEqual)(IClone* pOther, VARIANT_BOOL* pbEqual);
  STDMETHOD(IsIdentical)(IClone* pOther, VARIANT_BOOL* pbIsIdentical);

private:
  ISpatialReferencePtr   m_ipSpatialReference;
  IGeometryCollectionPtr m_ipDomains;

  ISpatialReferencePtr   m_ipUnknownCoordinateSystem;

  CComBSTR               m_name;
  double                 m_dx;
  double                 m_dy;

  WKSPoint*              m_points;
  long                   m_size;
};

#endif
