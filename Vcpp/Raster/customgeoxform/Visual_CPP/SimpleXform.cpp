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



// CustomXform.cpp : Implementation of SimpleXform
#include "stdafx.h"
#include "SimpleXform.h"

#include <math.h>
#include <float.h>

static const short cCurVers = 1;

SimpleXform::SimpleXform()
: m_ipSpatialReference(CLSID_UnknownCoordinateSystem),
  m_ipUnknownCoordinateSystem(CLSID_UnknownCoordinateSystem),
  m_dx(2.0),
  m_dy(1.5)
{
}

/////////////////////////////////////////////////////////////////////////////
// SimpleXform

STDMETHODIMP SimpleXform::InterfaceSupportsErrorInfo(REFIID riid)
{
  static const IID* arr[] = 
  {
    &IID_ISimpleXform
  };
  for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
  {
    if (InlineIsEqualGUID(*arr[i],riid))
      return S_OK;
  }
  return S_FALSE;
}

HRESULT SimpleXform::FinalConstruct()
{
  m_name = "SimpleXform";
  return S_OK;
}

void SimpleXform::FinalRelease()
{
}

/////////////////////////////////////////////////////////////////////////////
// ISimpleXform
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP SimpleXform::get_Name(BSTR* pName)
{
  if (pName == 0)
    return E_POINTER;

  *pName = ::SysAllocString(m_name);
  return S_OK;
}

STDMETHODIMP SimpleXform::put_Name(BSTR name)
{
  m_name = name;
  return S_OK;
}

///////////////////////////////////////////////////////////////////////////////
// IGeodataXform methods
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP SimpleXform::get_SpatialReference(ISpatialReference** ppSpatialReference)
{
  if (ppSpatialReference == 0)
    return E_POINTER;

  if ((*ppSpatialReference) = m_ipSpatialReference)
    (*ppSpatialReference)->AddRef();

  return S_OK;
}

STDMETHODIMP SimpleXform::putref_SpatialReference(ISpatialReference* pSpatialReference)
{
  m_ipSpatialReference = pSpatialReference;
  return S_OK;
}

STDMETHODIMP SimpleXform::get_Domains(IGeometryCollection** ppDomains)
{
  if (ppDomains == 0)
    return E_POINTER;

  if ((*ppDomains = m_ipDomains))
    (*ppDomains)->AddRef();

  return S_OK;
}

STDMETHODIMP SimpleXform::get_IsIdentity(VARIANT_BOOL* pIsIdentity)
{
  if (pIsIdentity == 0)
    return E_POINTER;
 
  *pIsIdentity = VARIANT_FALSE;

  return S_OK;
}

STDMETHODIMP SimpleXform::Transform(esriTransformDirection direction, long npoints, WKSPoint* points)
{
  for (long i = 0; i < npoints; ++i)
  {
    if (!_isnan(points[i].X))
    {
      if (direction == esriTransformForward)
      {
        points[i].X *= m_dx;
        points[i].Y *= m_dy;
      }
      else
      {
        points[i].X /= m_dx;
        points[i].Y /= m_dy;
      }
    }
  }

  return S_OK;
}

STDMETHODIMP SimpleXform::TransformCellsize(esriTransformDirection direction, double* dx, double* dy, IEnvelope* pAreaOfInterest)
{
  double minx = 0;
  double miny = 0;
  double maxx = *dx;
  double maxy = *dy;
  if (pAreaOfInterest)
  {
    VARIANT_BOOL empty;
    pAreaOfInterest->get_IsEmpty(&empty);
    if (empty != VARIANT_TRUE)
      pAreaOfInterest->QueryCoords(&minx, &miny, &maxx, &maxy);
  }

  IEnvelopePtr ipExtent(CLSID_Envelope);
  ipExtent->PutCoords(minx, miny, maxx, maxy);
  
  HRESULT hr;
  if (FAILED(hr = TransformExtent(direction, ipExtent)))
    return hr;

  // calculates cellsize factor based cell area
  double area;
  IAreaPtr(ipExtent)->get_Area(&area);
  area = sqrt(area/((maxx-minx)*(maxy-miny)));
  *dx *= area;
  *dy *= area;
  return S_OK;
}

STDMETHODIMP SimpleXform::TransformExtent(esriTransformDirection direction, IEnvelope* pExtent)
{
  if (pExtent)
  {
    double minx;
    double miny;
    double maxx;
    double maxy;
    pExtent->QueryCoords(&minx, &miny, &maxx, &maxy);
    WKSPoint points[4];
    points[0].X = minx;
    points[0].Y = miny;
    points[1].X = minx;
    points[1].Y = maxy;
    points[2].X = maxx;
    points[2].Y = maxy;
    points[3].X = maxx;
    points[3].Y = miny;
    Transform(direction, 4, points);
    for (long i = 0; i < 4; ++i)
    {
      if (i == 0 || minx > points[i].X) minx = points[i].X;
      if (i == 0 || miny > points[i].Y) miny = points[i].Y;
      if (i == 0 || maxx < points[i].X) maxx = points[i].X;
      if (i == 0 || maxy < points[i].Y) maxy = points[i].Y;
    }

    pExtent->PutCoords(minx, miny, maxx, maxy);
    if (direction == esriTransformForward)
      pExtent->putref_SpatialReference(m_ipSpatialReference);
    else
      pExtent->putref_SpatialReference(m_ipUnknownCoordinateSystem);
  }

  return S_OK;
}

STDMETHODIMP SimpleXform::TransformPoints(esriTransformDirection direction, IPointCollection* pPoints)
{
  HRESULT hr;
  if (pPoints == 0)
    return E_INVALIDARG;

  long npoints;
  pPoints->get_PointCount(&npoints);
  if (npoints > m_size)
  {
    delete[] m_points;
    m_points = 0;
    m_points = new WKSPoint[npoints];
    m_size   = npoints;
  }

  pPoints->QueryWKSPoints(0, npoints, m_points);
  if (FAILED(hr = Transform(direction, npoints, m_points)))
    return hr;

  if (direction == esriTransformForward)
    IGeometryPtr(pPoints)->putref_SpatialReference(m_ipSpatialReference);
  else
    IGeometryPtr(pPoints)->putref_SpatialReference(m_ipUnknownCoordinateSystem);

  return pPoints->SetWKSPoints(npoints, m_points);
}

///////////////////////////////////////////////////////////////////////////////
// IPersistStream
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP SimpleXform::Load(IStream * pStm)
{
  short vers;
  if (FAILED(pStm->Read(&vers, sizeof(vers), 0)) || vers > cCurVers)
    return E_FAIL;

  m_name.Empty();
  m_name.ReadFromStream(pStm);
  pStm->Read(&m_dx, sizeof(m_dx), 0);
  pStm->Read(&m_dy, sizeof(m_dy), 0);

  IObjectStreamPtr ipObjectStream(CLSID_ObjectStream);
  ipObjectStream->putref_Stream(pStm);

  IUnknownPtr ipUnk;
  HRESULT hr;

  if (FAILED(hr = ipObjectStream->LoadObject((GUID*) &IID_ISpatialReference, 0, &ipUnk)))
    return hr;

  m_ipSpatialReference = ipUnk;

  return S_OK;
}

STDMETHODIMP SimpleXform::Save(IStream * pStm, BOOL fClearDirty)
{
  if (FAILED(pStm->Write(&cCurVers , sizeof(cCurVers), 0)))
    return E_FAIL;

  m_name.WriteToStream(pStm);
  pStm->Write(&m_dx, sizeof(m_dx), 0);
  pStm->Write(&m_dy, sizeof(m_dy), 0);

  IObjectStreamPtr ipObjectStream(CLSID_ObjectStream);
  ipObjectStream->putref_Stream(pStm);
  return ipObjectStream->SaveObject(m_ipSpatialReference);
}

STDMETHODIMP SimpleXform::IsDirty()
{
  return E_NOTIMPL;
}

STDMETHODIMP SimpleXform::GetSizeMax(_ULARGE_INTEGER * pcbSize)
{
  if (pcbSize == NULL)
    return E_POINTER;
    
  return E_NOTIMPL;
}

STDMETHODIMP SimpleXform::GetClassID(CLSID* pClassID)
{
  *pClassID = CLSID_SimpleXform;
  return S_OK;
}

///////////////////////////////////////////////////////////////////////////////
// IClone methods
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP SimpleXform::Clone(IClone** ppClone)
{
  IClonePtr ipClone(CLSID_SimpleXform);
  ipClone->Assign(this);
  (*ppClone) = ipClone;
  (*ppClone)->AddRef();
  return S_OK;
}

STDMETHODIMP SimpleXform::Assign(IClone* pSrc)
{
  ISimpleXformPtr ipXform(pSrc);
  if (ipXform == 0)
    return E_INVALIDARG;
 
  ipXform->get_Name(&m_name);

  return S_OK;
}

STDMETHODIMP SimpleXform::IsEqual(IClone* pOther, VARIANT_BOOL* pbEqual)
{
  ISimpleXformPtr ipXf(pOther);
  if (ipXf)
  {
    CComBSTR name;
    ipXf->get_Name(&name);
    if (m_name == name)
      *pbEqual = VARIANT_TRUE;
  }

  return *pbEqual == VARIANT_TRUE ? S_OK : S_FALSE;
}

STDMETHODIMP SimpleXform::IsIdentical(IClone* pOther, VARIANT_BOOL* pbIdentical)
{
  if (pbIdentical == 0)
    return E_POINTER;

  if ((*pbIdentical = (this == pOther) ? VARIANT_TRUE : VARIANT_FALSE) == VARIANT_TRUE)
    return S_OK;

  return S_FALSE;
}