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

// 
// based upon SimpleXform.cpp sample, also built into CustomXforms.dll
//
// CustomXform.cpp : Implementation of RMCXform
#include "stdafx.h"
#include "RMCXform.h"
#include <math.h>
#include <float.h>

static const short cCurVers = 1;

RMCXform::RMCXform()
: m_ipSpatialReference(CLSID_UnknownCoordinateSystem),
  m_ipUnknownCoordinateSystem(CLSID_UnknownCoordinateSystem)
{
  // set GCS_WGS_1984 and name
  IGeographicCoordinateSystemPtr ipGCS;
  ISpatialReferenceFactory2Ptr ipFactory(CLSID_SpatialReferenceEnvironment);
  ipFactory->CreateGeographicCoordinateSystem(4326, &ipGCS); // GCS WGS 1984
  m_ipSpatialReference = (ISpatialReferencePtr)ipGCS;
  m_name = "RMCXform";
  
  // placeholder values, pending Intialization or put_coefficients
  m_pixSize = 1.0; 
  m_lon0 = 0.0;
  m_lat0 = 0.0; 
}

/////////////////////////////////////////////////////////////////////////////
// RMCXform
///////////////////////////////////////////////////////////////////////////////
STDMETHODIMP RMCXform::InterfaceSupportsErrorInfo(REFIID riid)
{
  static const IID* arr[] = 
  {
    &IID_IRMCXform
  };
  for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
  {
    if (InlineIsEqualGUID(*arr[i],riid))
      return S_OK;
  }
  return S_FALSE;
}

HRESULT RMCXform::FinalConstruct()
{
  return S_OK;
}

void RMCXform::FinalRelease()
{
}

/////////////////////////////////////////////////////////////////////////////
// IRMCXform
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP RMCXform::get_Name(BSTR* pName)
{
  if (pName == 0)
    return E_POINTER;

  *pName = ::SysAllocString(m_name);
  return S_OK;
}

STDMETHODIMP RMCXform::put_Name(BSTR name)
{
  return E_NOTIMPL;
}
STDMETHODIMP RMCXform::get_Coefficients(double* coefs)
{
  if (coefs == 0)
    return E_POINTER;

  for (int i=0; i<3; i++) coefs[i] = m_coef[i];
  return S_OK;
}
STDMETHODIMP RMCXform::put_Coefficients(double* coefs)
{
  for (int i=0; i<3; i++) m_coef[i] = coefs[i];
  m_lon0 = m_coef[0];
  m_lat0 = m_coef[1];
  m_pixSize = m_coef[2];

  return S_OK;
}

///////////////////////////////////////////////////////////////////////////////
// ICustomXform methods
///////////////////////////////////////////////////////////////////////////////
STDMETHODIMP RMCXform::Initialize(BSTR filename, BSTR data)
{
  // extract three coefficients from data
  // two cases: filename is passed in or data is passed in (not both)
  double coefs[3];
  char   argString[400];
  long   datalen;
  char * workString;

  datalen = (data) ? wcslen(data) : wcslen(filename);
  if (datalen > sizeof(argString) - 1) datalen = sizeof(argString) - 1;
  if (data) 
    for (int i=0; i<datalen; i++) argString[i] = (char)data[i];
  else
    for (int i=0; i<datalen; i++) argString[i] = (char)filename[i];
  argString[datalen] = 0;
  workString = argString;

  if (data)
  {
    // coefficients passed in as a space delimited string.  parse into coefs
    // triggered if GDAL driver saves custom geotransform data as XML Metadata
    for (int i=0; i<3; i++)
    {
      if (!workString) 
        return E_FAIL;
      coefs[i] = atof(workString);
      workString = strchr(workString, ' ') + 1;
    }
  }
  else
  {
    // RMC filename passed in.  parse coefficients from that file
    // triggered if file extension is listed in CustomXForms.dat
	  FILE *fp;
	  
	  errno_t err = fopen_s(&fp, workString, "r");
    if (err == 0)
      return E_FAIL;
    // skip the first six lines - could check first line 'rmcdata'
    for (int i=0; i<6; i++) 
      fgets(argString, 100, fp);
    // next three lines have the coefficients
    for (int i=0; i<3; i++)
    {
      fgets(argString, 100, fp);
      coefs[i] = atof(argString);
    }
  }

  // store with object
  put_Coefficients(coefs);

  return S_OK;
}

STDMETHODIMP RMCXform::get_Approximation(VARIANT_BOOL* pApprox)
{
  *pApprox = VARIANT_FALSE;
  return S_OK;
}


///////////////////////////////////////////////////////////////////////////////
// IGeodataXform methods
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP RMCXform::get_SpatialReference(ISpatialReference** ppSpatialReference)
{
  if (ppSpatialReference == 0)
    return E_POINTER;

  // Spatial Reference will always be GCS_WGS84
  if ((*ppSpatialReference) = m_ipSpatialReference)
    (*ppSpatialReference)->AddRef();

  return S_OK;
}

STDMETHODIMP RMCXform::putref_SpatialReference(ISpatialReference* pSpatialReference)
{
  // NOOP. always GCS_WGS84, set on init
  return E_NOTIMPL;
}

STDMETHODIMP RMCXform::get_Domains(IGeometryCollection** ppDomains)
{
  return E_NOTIMPL;
}

STDMETHODIMP RMCXform::get_IsIdentity(VARIANT_BOOL* pIsIdentity)
{
  if (pIsIdentity == 0)
    return E_POINTER;
  *pIsIdentity = VARIANT_FALSE;
  return S_OK;
}

STDMETHODIMP RMCXform::Transform(esriTransformDirection direction, long npoints, WKSPoint* points)
{
  // RMC Transform.  Transform "points" in place
  // scan-line orientation with south at top (note +y in fwd xform).  GCS_WGS84.
  // input image space is Cartesian (row index increases upwards)
  for (long i = 0; i < npoints; ++i)
  {
    if (!_isnan(points[i].X))
    {
      if (direction == esriTransformForward)
      {
        double imageX = points[i].X;  // extra variables for clarity
        double imageY = points[i].Y;
        // column (ImageX) and row (ImageY) to geographic values
        points[i].X = m_lon0 + imageX * m_pixSize;
        // points[i].Y = m_lat0 + imageY * m_pixSize;   // for conventional north up
        points[i].Y = m_lat0 - imageY * m_pixSize;  // for south up
      }
      else
      {
        double mapX = points[i].X;
        double mapY = points[i].Y;
        // geographic coordinates (mapX, mapY) to image coordinates
        points[i].X = (mapX - m_lon0) / m_pixSize;
        // points[i].Y = (mapY - m_lat0) / m_pixSize;   // for conventional north up
        points[i].Y = (m_lat0 - mapY) / m_pixSize;  // for south up
      }
    }
  }
  return S_OK;
}

STDMETHODIMP RMCXform::TransformCellsize(esriTransformDirection direction, double* dx, double* dy, IEnvelope* pAreaOfInterest)
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

  // calculates cellsize multiplier based cell area
  double area;
  IAreaPtr(ipExtent)->get_Area(&area);
  double areaMult = sqrt(area/((maxx-minx)*(maxy-miny)));
  *dx *= areaMult;
  *dy *= areaMult;
  return S_OK;
}

STDMETHODIMP RMCXform::TransformExtent(esriTransformDirection direction, IEnvelope* pExtent)
{
  if (pExtent)
  {
    double minx, miny, maxx, maxy;
    pExtent->QueryCoords(&minx, &miny, &maxx, &maxy);
    WKSPoint points[4];
    points[0].X = points[1].X = minx;
    points[2].X = points[3].X = maxx;
    points[0].Y = points[3].Y = miny;
    points[1].Y = points[2].Y = maxy;

    Transform(direction, 4, points);
    
    // determine the new extent after transform
    minx = points[0].X;
    miny = points[0].Y;
    maxx = points[0].X;
    maxy = points[0].Y;
    for (long i = 1; i < 4; ++i)
    {
      if (points[i].X < minx) minx = points[i].X;
      if (points[i].Y < miny) miny = points[i].Y;
      if (points[i].X > maxx) maxx = points[i].X;
      if (points[i].Y > maxy) maxy = points[i].Y;
    }

    pExtent->PutCoords(minx, miny, maxx, maxy);
    if (direction == esriTransformForward)
      pExtent->putref_SpatialReference(m_ipSpatialReference);
    else
      pExtent->putref_SpatialReference(m_ipUnknownCoordinateSystem);
  }

  return S_OK;
}

STDMETHODIMP RMCXform::TransformPoints(esriTransformDirection direction, IPointCollection* pPoints)
{
  HRESULT hr;
  if (pPoints == 0)
    return E_INVALIDARG;

  long npoints;
  pPoints->get_PointCount(&npoints);

  // temp array of WKSPoint to pass to Transform
  WKSPoint* ipWKSpoints = new WKSPoint[npoints];

  pPoints->QueryWKSPoints(0, npoints, ipWKSpoints);
  if (FAILED(hr = Transform(direction, npoints, ipWKSpoints)))
    return hr;

  if (direction == esriTransformForward)
    IGeometryPtr(pPoints)->putref_SpatialReference(m_ipSpatialReference);
  else
    IGeometryPtr(pPoints)->putref_SpatialReference(m_ipUnknownCoordinateSystem);

  hr = pPoints->SetWKSPoints(npoints, ipWKSpoints);

  // clean up temp array
  delete[] ipWKSpoints;
  ipWKSpoints = 0;

  return hr;
}


///////////////////////////////////////////////////////////////////////////////
// IPersistStream
///////////////////////////////////////////////////////////////////////////////
STDMETHODIMP RMCXform::Load(IStream * pStm)
{
  short vers;
  if (FAILED(pStm->Read(&vers, sizeof(vers), 0)) || vers > cCurVers)
    return E_FAIL;

  m_name.Empty();
  m_name.ReadFromStream(pStm);
  pStm->Read(m_coef, sizeof(m_coef), 0);

  return S_OK;
}

STDMETHODIMP RMCXform::Save(IStream * pStm, BOOL fClearDirty)
{
  if (FAILED(pStm->Write(&cCurVers , sizeof(cCurVers), 0)))
    return E_FAIL;

  m_name.WriteToStream(pStm);
  pStm->Write(m_coef, sizeof(m_coef), 0);

  return S_OK;
}

STDMETHODIMP RMCXform::IsDirty()
{
  return E_NOTIMPL;
}

STDMETHODIMP RMCXform::GetSizeMax(_ULARGE_INTEGER * pcbSize)
{
  return E_NOTIMPL;
}

STDMETHODIMP RMCXform::GetClassID(CLSID* pClassID)
{
  *pClassID = CLSID_RMCXform;
  return S_OK;
}


///////////////////////////////////////////////////////////////////////////////
// IClone methods
///////////////////////////////////////////////////////////////////////////////

STDMETHODIMP RMCXform::Clone(IClone** ppClone)
{
  // create a new object and copy the members
  IClonePtr ipClone(CLSID_RMCXform);
  ipClone->Assign(this);

  (*ppClone) = ipClone;
  (*ppClone)->AddRef();
  return S_OK;
}

STDMETHODIMP RMCXform::Assign(IClone* pSrc)
{
  IRMCXformPtr ipXform(pSrc);
  if (ipXform == 0)
    return E_INVALIDARG;
 
  // retrieve the name from the source, store it in the module
  ipXform->get_Name(&m_name);

  // get the coefficients from the source, initialize with put_Coefficients
  double coefs[3];
  ipXform->get_Coefficients(coefs);
  put_Coefficients(coefs);

  return S_OK;
}

STDMETHODIMP RMCXform::IsEqual(IClone* pOther, VARIANT_BOOL* pbEqual)
{
  VARIANT_BOOL isSame = FALSE;
  IRMCXformPtr ipXf(pOther);
  if ((ipXf) && (pbEqual))
  {
    CComBSTR name;
    ipXf->get_Name(&name);

    double coef[3];
    ipXf->get_Coefficients(coef);

    if (m_name == name) 
      isSame = VARIANT_TRUE;

    for (int i=0; i<3; i++) 
      if (m_coef[i] != coef[i]) isSame = VARIANT_FALSE;
  }
  else
  {
    return E_INVALIDARG;
  }

  // should always return S_OK, even if *pbEqual is FALSE
  *pbEqual = isSame;
  return S_OK;
}

STDMETHODIMP RMCXform::IsIdentical(IClone* pOther, VARIANT_BOOL* pbIdentical)
{
  if (pbIdentical == 0)
    return E_POINTER;

  *pbIdentical = (this == pOther) ? VARIANT_TRUE : VARIANT_FALSE;
  return S_OK;
}