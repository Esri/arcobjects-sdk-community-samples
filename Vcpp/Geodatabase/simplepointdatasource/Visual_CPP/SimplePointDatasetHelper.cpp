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



// SimplePointDatasetHelper.cpp : Implementation of CSimplePointDatasetHelper
#include "stdafx.h"
#include "SimplePointVC.h"
#include "SimplePointDatasetHelper.h"
#include "GeneralUtils.h"

/////////////////////////////////////////////////////////////////////////////
// CSimplePointDatasetHelper

STDMETHODIMP CSimplePointDatasetHelper::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_ISimplePointDatasetHelper
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

// IPlugInDatasetInfo
STDMETHODIMP CSimplePointDatasetHelper::get_LocalDatasetName(BSTR *localName)
{
	if (! localName) return E_POINTER;
  
	return m_sDatasetName.CopyTo(localName);
}

STDMETHODIMP CSimplePointDatasetHelper::get_DatasetType(esriDatasetType *DatasetType)
{
	if (! DatasetType) return E_POINTER;
  
	*DatasetType = esriDTFeatureClass;
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_GeometryType(esriGeometryType *GeometryType)
{
	if (! GeometryType) return E_POINTER;
  
	*GeometryType = esriGeometryPoint;
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_ShapeFieldName(BSTR *ShapeFieldName)
{
	if (! ShapeFieldName) return E_POINTER;

	CComBSTR str(L"Shape");
	*ShapeFieldName = str.Detach();
	return S_OK;
}

// IPlugInDatasetHelper
STDMETHODIMP CSimplePointDatasetHelper::get_ClassCount(long *Count)
{
	if (! Count) return E_POINTER;

	// This is a standalone feature class
  *Count = 1;

	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_ClassName(long Index, BSTR *Name)
{
	if (! Name) return E_POINTER;

	return m_sDatasetName.CopyTo(Name);
}

STDMETHODIMP CSimplePointDatasetHelper::get_ClassIndex(BSTR Name, long *Index)
{
	if (! Index) return E_POINTER;

	// This is a standalone feature class, so class index doesnt apply
  *Index = 0;

	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_Bounds(IEnvelope **Bounds)
{
	HRESULT hr;
	if (! Bounds) return E_POINTER;

  // Get an envelope for the extent of the dataset
  // We will have to calculate the extent by opening a cursor
  // on the dataset, and building up a minimum bounding rectangle 
  
  // Prepare to open a cursor.
  // Make the fieldmap, with values of -1 (attributes do not need fetching)

	long lNumFields;
	IFieldsPtr ipFields;
	
	hr = get_Fields(0, &ipFields);
	if (FAILED(hr)) return hr;

	hr = ipFields->get_FieldCount(&lNumFields);
	if (FAILED(hr)) return hr;

	CComVariant vFieldMap;
	vFieldMap.vt = VT_ARRAY | VT_I4;
  vFieldMap.parray = ::SafeArrayCreateVector(VT_I4, 0, lNumFields);
  long HUGEP *pMap = 0; // raw pointer to array
  hr = SafeArrayAccessData(vFieldMap.parray, (void HUGEP **)&pMap);
  if (FAILED(hr)) return hr;
	long i;
	for (i=0; i < lNumFields; i++)
		pMap[i] = -1;
	SafeArrayUnaccessData(vFieldMap.parray);  

	// Open the cursor
  IPlugInCursorHelperPtr ipPlugInCursor;
  hr = FetchAll(0, L"", vFieldMap, &ipPlugInCursor);
	if (FAILED(hr)) return hr;

	// loop through the data recording min/max X and Y values.
	double dxMin =  9999999;
  double dxMax = -9999999;
  double dyMin =  9999999;
  double dyMax = -9999999;
	double x,y;

	VARIANT_BOOL bEOF = VARIANT_FALSE;
	IGeometryPtr ipGeom;
	hr = ipGeom.CreateInstance(CLSID_Point);
	if (FAILED(hr)) return hr;

  IPointPtr ipPoint;

	long lNumRecords = 0;
  do
	{
		lNumRecords++;
		hr = ipPlugInCursor->QueryShape(ipGeom);
    if (FAILED(hr)) return hr;
    
		ipPoint = ipGeom;
		if (ipPoint == NULL) return E_FAIL;
    hr = ipPoint->QueryCoords(&x, &y);
		if (FAILED(hr)) return hr;

    if (x > dxMax) dxMax = x;
		if (x < dxMin) dxMin = x;
    if (y > dyMax) dyMax = y;
		if (y < dyMin) dyMin = y;

		hr = ipPlugInCursor->NextRecord();
		if (hr != S_OK)
		{
			hr = ipPlugInCursor->IsFinished(&bEOF);
      if (FAILED(hr)) return hr;
		}
	} while (!bEOF);

  // Handle special case of single point in file
  // - add a small amount, so that we will end up with an envelope rather than a point
  if (lNumRecords == 1)
	{
	  double dDelta = 0.01;
		if (dxMax != 0) 
			dDelta = dxMax / 1000;
    dxMax += dDelta;
		dyMax += dDelta;
	}

	// Create the envelope object, setting the appropriate spatial reference
  IEnvelopePtr ipEnv;
	hr = ipEnv.CreateInstance(CLSID_Envelope);
	if (FAILED(hr)) return hr;

	ISpatialReferencePtr ipSR;
	hr = ipSR.CreateInstance(CLSID_UnknownCoordinateSystem);
  if (FAILED(hr)) return hr;

	hr = ipEnv->putref_SpatialReference(ipSR);
	if (FAILED(hr)) return hr;

  hr = ipEnv->PutCoords(dxMin, dyMin, dxMax, dyMax);
	if (FAILED(hr)) return hr;

  *Bounds = ipEnv.Detach(); // pass ownership of object to client
	
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_Fields(long ClassIndex, IFields **FieldSet)
{
	if (! FieldSet) return E_POINTER;
	
	HRESULT hr;
  
	// First get the standard fields for an feature class
	IObjectClassDescriptionPtr ipOCDesc;
	hr = ipOCDesc.CreateInstance(CLSID_FeatureClassDescription);
	if (FAILED(hr)) return hr;

	IFieldsPtr ipFields;
	hr = ipOCDesc->get_RequiredFields(&ipFields);
	if (FAILED(hr)) return hr;

  IFieldPtr ipField;

  // We will have: a shape field name of "shape"
  // an UnknownCoordinateSystem
  // Just need to change the geometry type to Point
	long i;
	long lNumFields;
	esriFieldType eFieldType;
	hr = ipFields->get_FieldCount(&lNumFields);
	if (FAILED(hr)) return hr;

	for (i=0; i < lNumFields; i++)
  {
    hr = ipFields->get_Field(i, &ipField);
		if (FAILED(hr)) return hr;

		hr = ipField->get_Type(&eFieldType);
		if (eFieldType == esriFieldTypeGeometry)
		{
			IGeometryDefPtr ipGeomDef;
			hr = ipField->get_GeometryDef(&ipGeomDef);
			if (FAILED(hr)) return hr;

  		IGeometryDefEditPtr ipGeomDefEdit = ipGeomDef;
			if (ipGeomDefEdit == NULL) return E_FAIL;

			hr = ipGeomDefEdit->put_GeometryType(esriGeometryPoint);
			if (FAILED(hr)) return hr;
		}
	}

		// Add the extra text field
	IFieldEditPtr ipFieldEdit;
	hr = ipFieldEdit.CreateInstance(CLSID_Field);
  if (FAILED(hr)) return hr;

	hr = ipFieldEdit->put_Name(L"Column1");
	if (FAILED(hr)) return hr;
	
	hr = ipFieldEdit->put_Type(esriFieldTypeString);
	if (FAILED(hr)) return hr;

	hr = ipFieldEdit->put_Length(1);
	if (FAILED(hr)) return hr;

  IFieldsEditPtr ipFieldsEdit = ipFields;
  if (ipFieldsEdit == NULL) return E_FAIL;

	ipField = ipFieldEdit;
	if (ipField == NULL) return E_FAIL;

  hr = ipFieldsEdit->AddField(ipField);
	if (FAILED(hr)) return hr;

  *FieldSet = ipFields.Detach(); // pass ownership of object to client 

	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_OIDFieldIndex(long ClassIndex, long *OIDFieldIndex)
{
	if (! OIDFieldIndex) return E_POINTER;

	*OIDFieldIndex = 0;

	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::get_ShapeFieldIndex(long ClassIndex, long *ShapeFieldIndex)
{
	if (! ShapeFieldIndex) return E_POINTER;
  
	*ShapeFieldIndex = 1;

	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::FetchByID(long ClassIndex, long ID, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper)
{
	HRESULT hr;
	if (! cursorHelper) return E_POINTER;

	// Some parameters can be ignored since,
  //   ClassIndex is only relevant to feature datasets
  //   WhereClause is not appropriate since we are not supporting SQL
  ISimplePointCursorHelperPtr ipSimplePointCursorHelper;
  hr = ipSimplePointCursorHelper.CreateInstance(CLSID_SimplePointCursorHelper);
  if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->put_FieldMap(FieldMap);
	if (FAILED(hr)) return hr;

	IFieldsPtr ipFields;
	hr = get_Fields(0, &ipFields);
	if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->putref_Fields(ipFields);
	if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->put_OID(ID);
	if (FAILED(hr)) return hr;

	CComBSTR sFilePath;
	sFilePath = m_sWorkspacePath;
	sFilePath.Append(m_sDatasetName);
  hr = ipSimplePointCursorHelper->put_FilePath(sFilePath);
	if (FAILED(hr)) return hr;

  IPlugInCursorHelperPtr ipPlugInCursorHelper;
	ipPlugInCursorHelper = ipSimplePointCursorHelper;
	if (ipPlugInCursorHelper == NULL) return E_FAIL;

  *cursorHelper = ipPlugInCursorHelper.Detach(); // Pass ownership of object back to client
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::FetchAll(long ClassIndex, BSTR WhereClause, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper)
{
	HRESULT hr;
	if (! cursorHelper) return E_POINTER;

	// Some parameters can be ignored since,
  //   ClassIndex is only relevant to feature datasets
  //   WhereClause is not appropriate since we are not supporting SQL
  ISimplePointCursorHelperPtr ipSimplePointCursorHelper;
  hr = ipSimplePointCursorHelper.CreateInstance(CLSID_SimplePointCursorHelper);
  if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->put_FieldMap(FieldMap);
	if (FAILED(hr)) return hr;

  IFieldsPtr ipFields;
	hr = get_Fields(0, &ipFields);
	if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->putref_Fields(ipFields);
	if (FAILED(hr)) return hr;

	CComBSTR sFilePath;
	sFilePath = m_sWorkspacePath;
	sFilePath.Append(m_sDatasetName);
  hr = ipSimplePointCursorHelper->put_FilePath(sFilePath);
	if (FAILED(hr)) return hr;

  IPlugInCursorHelperPtr ipPlugInCursorHelper;
	ipPlugInCursorHelper = ipSimplePointCursorHelper;
	if (ipPlugInCursorHelper == NULL) return E_FAIL;

  *cursorHelper = ipPlugInCursorHelper.Detach(); // Pass ownership of object back to client
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::FetchByEnvelope(long ClassIndex, IEnvelope *env, VARIANT_BOOL strictSearch, BSTR WhereClause, VARIANT FieldMap, IPlugInCursorHelper **cursorHelper)
{
	HRESULT hr;
	if (! cursorHelper) return E_POINTER;

	// Some parameters can be ignored since,
  //   ClassIndex is only relevant to feature datasets
  //   WhereClause is not appropriate since we are not supporting SQL
  ISimplePointCursorHelperPtr ipSimplePointCursorHelper;
  hr = ipSimplePointCursorHelper.CreateInstance(CLSID_SimplePointCursorHelper);
  if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->put_FieldMap(FieldMap);
	if (FAILED(hr)) return hr;

	IFieldsPtr ipFields;
	hr = get_Fields(0, &ipFields);
	if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->putref_Fields(ipFields);
	if (FAILED(hr)) return hr;

	hr = ipSimplePointCursorHelper->putref_QueryEnvelope(env);
	if (FAILED(hr)) return hr;

	CComBSTR sFilePath;
	sFilePath = m_sWorkspacePath;
	sFilePath.Append(m_sDatasetName);
  hr = ipSimplePointCursorHelper->put_FilePath(sFilePath);
	if (FAILED(hr)) return hr;

  IPlugInCursorHelperPtr ipPlugInCursorHelper;
	ipPlugInCursorHelper = ipSimplePointCursorHelper;
	if (ipPlugInCursorHelper == NULL) return E_FAIL;

  *cursorHelper = ipPlugInCursorHelper.Detach(); // Pass ownership of object back to client
	return S_OK;
}


// ISimplePointDatasetHelper methods
STDMETHODIMP CSimplePointDatasetHelper::put_WorkspacePath(BSTR newVal)
{
	m_sWorkspacePath = newVal;
	return S_OK;
}

STDMETHODIMP CSimplePointDatasetHelper::put_DatasetName(BSTR newVal)
{
	m_sDatasetName = newVal;
	return S_OK;
}

