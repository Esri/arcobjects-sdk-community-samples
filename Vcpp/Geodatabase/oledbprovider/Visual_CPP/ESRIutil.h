#if ! defined( _DATAUTIL_H )
#define _DATAUTIL_H

namespace ESRIutil
{
  HRESULT GetSpatialReference(ISpatialReference *pSpatialRef, BSTR* spatialRefBuf);

  HRESULT GetSpatialReference(IUnknown *pUnknown, BSTR *spatialRefBuf);

  HRESULT GetGeometryAndOIDFields(IDatasetName *pDatasetName, IField **ppGeomtryField, IField **ppOIDField);

  HRESULT GetOIDAndGeometryColNames(
    IDatasetName *pDatasetName,
    BSTR *oidName,
    BSTR *blobName
  );
  
  HRESULT GetOGISWkb(IUnknown *pUnkVal, IUnknown **pIUnknown);

  HRESULT GetOGISWkb(IUnknown *pUnkVal, BYTE array[], long nSize);
};

#endif
