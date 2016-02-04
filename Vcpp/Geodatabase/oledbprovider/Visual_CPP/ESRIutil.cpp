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
#include "stdafx.h"

#include "ESRIutil.h"


HRESULT ESRIutil::GetSpatialReference(ISpatialReference *pSpatialRef, BSTR * spatialRefBuf)
{
  HRESULT hr;
  *spatialRefBuf = 0;
  IESRISpatialReferencePtr ipESRIspatialRef( pSpatialRef );

  if( ipESRIspatialRef == 0 )
    return E_FAIL;

  long nBytes;
  if( FAILED( hr = ipESRIspatialRef->get_ESRISpatialReferenceSize( &nBytes ) ) )
    return hr;

  *spatialRefBuf = ::SysAllocStringLen(0, nBytes);
  if( FAILED( hr = ipESRIspatialRef->ExportToESRISpatialReference( *spatialRefBuf, &nBytes ) ) )
    return hr;

  return S_OK;
}


HRESULT ESRIutil::GetSpatialReference(IUnknown *pUnknown, OLECHAR ** spatialRefBuf)
{
  HRESULT hr;

  *spatialRefBuf = 0;

  IGeoDatasetPtr ipGeoDataset( pUnknown );
  if( ipGeoDataset == 0 )
		return E_FAIL;

  ISpatialReferencePtr ipSpatialRef;
  if( FAILED( hr = ipGeoDataset->get_SpatialReference( &ipSpatialRef ) ) )
    return hr;

  return GetSpatialReference( ipSpatialRef, spatialRefBuf );
}

HRESULT ESRIutil::GetGeometryAndOIDFields(
  IDatasetName *pDatasetName,
  IField **ppGeomtryField,
  IField **ppOIDField
)
{
  *ppGeomtryField = 0;
  *ppOIDField = 0;

  INamePtr ipName(pDatasetName);
  if (ipName == 0)
		return E_FAIL;

  IUnknownPtr ipUnknown;
  if( FAILED( ipName->Open(&ipUnknown)))
		return E_FAIL;

  ITablePtr ipTable( ipUnknown );
  if( ipTable == 0 )
		return E_FAIL;

  IFieldsPtr ipColFields;
  if( FAILED( ipTable->get_ESRIFields( &ipColFields )))
		return E_FAIL;

  long fieldCount = 0;
  if( FAILED( ipColFields->get_FieldCount( &fieldCount )))
		return E_FAIL;

  while( --fieldCount >= 0 )
  {
    IFieldPtr ipField;

    if( FAILED( ipColFields->get_ESRIField( fieldCount, &ipField )))
  		return E_FAIL;

    esriFieldType fieldType;
    ipField->get_Type(&fieldType);
    if (fieldType == esriFieldTypeGeometry)
    {
      *ppGeomtryField = ipField;
      (*ppGeomtryField)->AddRef();
    }
    else if (fieldType == esriFieldTypeOID)
    {
      *ppOIDField = ipField;
      (*ppOIDField)->AddRef();
    }
  }

  return S_OK;
}


HRESULT ESRIutil::GetOIDAndGeometryColNames(
  IDatasetName *pDatasetName,
  BSTR *oidName,
  BSTR *blobName
)
{
  INamePtr ipName( pDatasetName );
  if( ipName == 0 )
		return E_FAIL;

  IUnknownPtr ipUnknown;
  if( FAILED( ipName->Open( &ipUnknown ) ))
		return E_FAIL;

  ITablePtr ipTable( ipUnknown );
  if( ipTable == 0 )
		return E_FAIL;

  IFieldsPtr ipColFields;
  if( FAILED( ipTable->get_ESRIFields( &ipColFields )))
		return E_FAIL;

  long fieldCount = 0;
  if( FAILED( ipColFields->get_FieldCount( &fieldCount )))
		return E_FAIL;

  int nFound = 0;
  while( --fieldCount >= 0 )
  {
    IFieldPtr ipField;

    if( FAILED( ipColFields->get_ESRIField( fieldCount, &ipField )))
  		return E_FAIL;

    esriFieldType fieldType;
    ipField->get_Type( &fieldType );
    if( fieldType == esriFieldTypeOID )
    {
      ipField->get_Name( oidName );
      nFound++;
    }
    else if( fieldType == esriFieldTypeGeometry )
    {
      ipField->get_Name( blobName );   
      nFound++;
    }
  }

  if( nFound < 2 )
    return S_FALSE;

  return S_OK;
}

HRESULT ESRIutil::GetOGISWkb(IUnknown *pUnkVal, IUnknown **pIUnknown )
{
  IWkbPtr ipWkb(pUnkVal);

  HRESULT hr;

  IMemoryBlobStreamPtr ipMemBlobStream(CLSID_MemoryBlobStream);
  BYTE* pWkbBytes = 0;

  long nBytes;
  if( FAILED( hr = ipWkb->get_WkbSize( &nBytes ) ) )
    return E_FAIL;

  long transferOwnership;

  pWkbBytes = (BYTE *)::HeapAlloc( ::GetProcessHeap(), HEAP_ZERO_MEMORY, nBytes );
  if( pWkbBytes == 0 )
    return E_OUTOFMEMORY;
  transferOwnership = TRUE;

  long nBytesWritten;
  if( FAILED( hr = ipWkb->ExportToWkb(&nBytesWritten, pWkbBytes) ) )
    return E_FAIL;
    
  ATLASSERT( nBytesWritten <= nBytes );
  if( FAILED( hr = ipMemBlobStream->AttachToMemory(pWkbBytes, nBytesWritten, transferOwnership)))
    return E_FAIL;

  if( FAILED( hr = ipMemBlobStream->QueryInterface(IID_IUnknown, (void **)pIUnknown ) ) )
    return E_FAIL;

  return S_OK;
}

HRESULT ESRIutil::GetOGISWkb(IUnknown *pUnkVal, BYTE array[], long nSize )
{
  IWkbPtr ipWkb(pUnkVal);

  long nBytes;
  HRESULT hr;
  if( FAILED( hr = ipWkb->get_WkbSize( &nBytes ) ) )
    return E_FAIL;

  ATLASSERT(nBytes <= nSize);
  long nBytesWritten;
  if( FAILED( hr = ipWkb->ExportToWkb(&nBytesWritten, array) ) )
    return E_FAIL;
    
  ATLASSERT( nBytesWritten <= nBytes );

  return S_OK;
}


