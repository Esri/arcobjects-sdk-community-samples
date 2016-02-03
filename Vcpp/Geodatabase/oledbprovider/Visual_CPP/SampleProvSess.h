// Session.h : Declaration of the CSampleProvSession
#ifndef __CSampleProvSession_H_
#define __CSampleProvSession_H_
#include "resource.h"       // main symbols

class CSampleProvSessionTRSchemaRowset;
class CSampleProvSessionColSchemaRowset;
class CSampleProvSessionPTSchemaRowset;

// OGIS schema rowset classes
class CSampleProvSessionSchemaOGISTables;
class CSampleProvSchemaOGISGeoColumns;
class CSampleProvSessionSchemaSpatRef;

class CSampleProvRowset;
class CSampleProvCommand;

/////////////////////////////////////////////////////////////////////////////
// CSampleProvSession
class ATL_NO_VTABLE CSampleProvSession : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public IGetDataSourceImpl<CSampleProvSession>,
	public IOpenRowsetImpl<CSampleProvSession>,
	public ISessionPropertiesImpl<CSampleProvSession>,
	public IObjectWithSiteSessionImpl<CSampleProvSession>,
	public IDBSchemaRowsetImpl<CSampleProvSession>,
	public IDBCreateCommandImpl<CSampleProvSession, CSampleProvCommand>
{
public:
	CSampleProvSession()
	{
	}
	HRESULT FinalConstruct()
	{
		return FInit();
	}
	STDMETHOD(OpenRowset)(IUnknown *pUnk, DBID *pTID, DBID *pInID, REFIID riid,
					   ULONG cSets, DBPROPSET rgSets[], IUnknown **ppRowset)
	{
		CSampleProvRowset* pRowset;
		return CreateRowset(pUnk, pTID, pInID, riid, cSets, rgSets, ppRowset, pRowset);
	}
BEGIN_PROPSET_MAP(CSampleProvSession)
	BEGIN_PROPERTY_SET(DBPROPSET_SESSION)
		PROPERTY_INFO_ENTRY(SESS_AUTOCOMMITISOLEVELS)
	END_PROPERTY_SET(DBPROPSET_SESSION)
END_PROPSET_MAP()
BEGIN_COM_MAP(CSampleProvSession)
	COM_INTERFACE_ENTRY(IGetDataSource)
	COM_INTERFACE_ENTRY(IOpenRowset)
	COM_INTERFACE_ENTRY(ISessionProperties)
	COM_INTERFACE_ENTRY(IObjectWithSite)
	COM_INTERFACE_ENTRY(IDBCreateCommand)
	COM_INTERFACE_ENTRY(IDBSchemaRowset)
END_COM_MAP()
BEGIN_SCHEMA_MAP(CSampleProvSession)
	SCHEMA_ENTRY(DBSCHEMA_TABLES, CSampleProvSessionTRSchemaRowset)
	SCHEMA_ENTRY(DBSCHEMA_COLUMNS, CSampleProvSessionColSchemaRowset)
	SCHEMA_ENTRY(DBSCHEMA_PROVIDER_TYPES, CSampleProvSessionPTSchemaRowset)
  // OGIS schema entry definitions
	SCHEMA_ENTRY(DBSCHEMA_OGIS_FEATURE_TABLES, CSampleProvSessionSchemaOGISTables)
	SCHEMA_ENTRY(DBSCHEMA_OGIS_GEOMETRY_COLUMNS,CSampleProvSchemaOGISGeoColumns)
	SCHEMA_ENTRY(DBSCHEMA_OGIS_SPATIAL_REF_SYSTEMS,CSampleProvSessionSchemaSpatRef);
END_SCHEMA_MAP()

public:
  IWorkspacePtr   m_ipWS;
};

//  Tables schema rowset
class CSampleProvSessionTRSchemaRowset : 
	public CRowsetImpl< CSampleProvSessionTRSchemaRowset, CTABLESRow, CSampleProvSession>
{
public:
	HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
	{
		USES_CONVERSION;

    HRESULT hr;

    // Get back to the Session object
    CComPtr<IGetDataSource> ipGDS;
  	if (FAILED(hr = GetSpecification(IID_IGetDataSource, (IUnknown **)&ipGDS)))
      return hr;
    CSampleProvSession *pSess = static_cast<CSampleProvSession *>((IGetDataSource*)ipGDS);

    IEnumDatasetNamePtr ipNames;
    hr = pSess->m_ipWS->get_DatasetNames(esriDTTable, &ipNames);

    long count = 0;
    IDatasetNamePtr ipName;
    while (ipNames->Next(&ipName) == S_OK)
    {
      CComBSTR name;
      ipName->get_Name(&name);

  		CTABLESRow trData;

  		::wcscpy(trData.m_szType, OLESTR("TABLE"));
      ::wcscpy(trData.m_szTable, name);

      count++;
      m_rgRowData.Add(trData);
    }

    *pcRowsAffected = count;

		return S_OK;
	}

	DBSTATUS GetDBStatus(CSimpleRow*, ATLCOLUMNINFO* pInfo)
	{
		if (pInfo->iOrdinal == 1 || pInfo->iOrdinal == 2)
			return DBSTATUS_S_ISNULL;
		return DBSTATUS_S_OK;
	}
};

//  Not implemented
class CSampleProvSessionColSchemaRowset : 
	public CRowsetImpl< CSampleProvSessionColSchemaRowset, CCOLUMNSRow, CSampleProvSession>
{
public:
	HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
	{
		USES_CONVERSION;

    return S_OK;
	}
	DBSTATUS GetDBStatus(CSimpleRow*, ATLCOLUMNINFO* pInfo)
	{
		switch(pInfo->iOrdinal)
		{
		case 1:
		case 2:
		case 19:
		case 20:
		case 22:
		case 23:
		case 25:
		case 26:
			return DBSTATUS_S_ISNULL;
		default:
			return DBSTATUS_S_OK;
		}
	}
};
class CSampleProvSessionPTSchemaRowset : 
	public CRowsetImpl< CSampleProvSessionPTSchemaRowset, CPROVIDER_TYPERow, CSampleProvSession>
{
public:
	HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
	{
		return S_OK;
	}
};

/////////////////////////////////////////////////////////////////////////////
// CSampleProvSessionSchemaOGISTables 
class OGISTables_Row
{
public:
	WCHAR	m_szAlias[4];
	WCHAR	m_szCatalog[4];
	WCHAR	m_szSchema[4];
	WCHAR	m_szTableName[129];
	WCHAR	m_szColumnName[129];
	WCHAR	m_szDGName[129];

	OGISTables_Row()
	{
		m_szAlias[0] = L'\0';
		m_szCatalog[0] = L'\0';
		m_szSchema[0] = L'\0';
		m_szTableName[0] = L'\0';
		m_szColumnName[0] = L'\0';
		m_szDGName[0] = L'\0';
	}

BEGIN_PROVIDER_COLUMN_MAP(OGISTables_Row)
	PROVIDER_COLUMN_ENTRY("FEATURE_TABLE_ALIAS",1,m_szAlias)
	PROVIDER_COLUMN_ENTRY("TABLE_CATALOG",2,m_szCatalog)
	PROVIDER_COLUMN_ENTRY("TABLE_SCHEMA",3,m_szSchema)
	PROVIDER_COLUMN_ENTRY("TABLE_NAME",4,m_szTableName)
	PROVIDER_COLUMN_ENTRY("ID_COLUMN_NAME",5,m_szColumnName)
	PROVIDER_COLUMN_ENTRY("DG_COLUMN_NAME",6,m_szDGName)
END_PROVIDER_COLUMN_MAP()
};

class CSampleProvSessionSchemaOGISTables:
  public CRowsetImpl <CSampleProvSessionSchemaOGISTables, OGISTables_Row, CSampleProvSession>
{
public:
  HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
  {
    USES_CONVERSION;

    HRESULT hr;

    // Get back to the Session object
    CComPtr<IGetDataSource> ipGDS;
  	if (FAILED(hr = GetSpecification(IID_IGetDataSource, (IUnknown **)&ipGDS)))
      return hr;
    CSampleProvSession *pSess = static_cast<CSampleProvSession *>((IGetDataSource*)ipGDS);

    // Add the Features Classes within a workspace
    IEnumDatasetNamePtr ipDatasetNameEnum,
                        ipFeatureNameEnum;
    IDatasetNamePtr datasetName;
    hr = pSess->m_ipWS->get_DatasetNames(esriDTAny, &ipDatasetNameEnum);
    long count = 0;

    for (;;)
    {
  //  We are looping on feature class names
      if (ipFeatureNameEnum != 0 )
      {
        hr = ipFeatureNameEnum->Next(&datasetName);
        if (hr == S_OK)
          goto NEXT_FEATURE;
        ipFeatureNameEnum = 0;
      }

  //  Get a new Name
      hr = ipDatasetNameEnum->Next(&datasetName);
      if (FAILED(hr) || hr != S_OK)
      {
        if( SUCCEEDED( hr ) )
          hr = S_FALSE;
        break;
      }

  NEXT_FEATURE:
      esriDatasetType dsType;
      if (FAILED(hr = datasetName->get_Type(&dsType)))
        return hr;

  //  Skip feature dataset names
      if (dsType == esriDTFeatureDataset)
      {
        IFeatureDatasetNamePtr ipFDSN(datasetName);

        if (FAILED(hr = ipFDSN->get_FeatureClassNames(&ipFeatureNameEnum)))
          return hr;
      }
  //  Skip over anything that is not a feature class
      else if (dsType == esriDTFeatureClass)
      {
        CComBSTR name;
        datasetName->get_Name(&name);

        OGISTables_Row trData;

        ::wcscpy(trData.m_szTableName, name);
        CComBSTR geoColName,
                 oidColName;
        hr = ESRIutil::GetOIDAndGeometryColNames(datasetName, &oidColName, &geoColName);

        ::wcscpy(trData.m_szDGName, geoColName);
        ::wcscpy(trData.m_szColumnName, oidColName);
        count++;
        m_rgRowData.Add(trData);
      }
    }

    *pcRowsAffected = count;

    return S_OK;
  }
};

/////////////////////////////////////////////////////////////////////////////
// CSampleProvSchemaOGISGeoColumns
class OGISGeometry_Row
{
public:
	WCHAR	m_szCatalog[4];
	WCHAR	m_szSchema[4];
	WCHAR	m_szTableName[129];
	WCHAR	m_szColumnName[129];
	unsigned long m_nGeomType;
	int		m_nSpatialRefId;
	
	OGISGeometry_Row()
	{
		m_szCatalog[0] = L'\0';
		m_szSchema[0] = L'\0';
		m_szTableName[0] = L'\0';
		m_szColumnName[0] = L'\0';
		m_nGeomType = 0;
		m_nSpatialRefId = 0;
	}

BEGIN_PROVIDER_COLUMN_MAP(OGISGeometry_Row)
	PROVIDER_COLUMN_ENTRY("TABLE_CATALOG",1,m_szCatalog)
	PROVIDER_COLUMN_ENTRY("TABLE_SCHEMA",2,m_szSchema)
	PROVIDER_COLUMN_ENTRY("TABLE_NAME",3,m_szTableName)
	PROVIDER_COLUMN_ENTRY("COLUMN_NAME",4,m_szColumnName)
	PROVIDER_COLUMN_ENTRY("GEOM_TYPE",5,m_nGeomType)
	PROVIDER_COLUMN_ENTRY("SPATIAL_REF_SYSTEM_ID",6,m_nSpatialRefId)
END_PROVIDER_COLUMN_MAP()
};

class CSampleProvSchemaOGISGeoColumns:
  public CRowsetImpl<CSampleProvSchemaOGISGeoColumns, OGISGeometry_Row, CSampleProvSession>
{
public:
  HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
  {
    USES_CONVERSION;

		HRESULT hr;

    CComPtr<IGetDataSource> ipGDS;
  	if (FAILED(hr = GetSpecification(IID_IGetDataSource, (IUnknown **)&ipGDS)))
      return hr;
    CSampleProvSession *pSess = static_cast<CSampleProvSession *>((IGetDataSource*)ipGDS);

    // Add the Features Classes within a workspace
    IEnumDatasetNamePtr ipDatasetNameEnum,
                        ipFeatureNameEnum;
    IDatasetNamePtr datasetName;
    hr = pSess->m_ipWS->get_DatasetNames(esriDTAny, &ipDatasetNameEnum);
    long count = 0;

    for (;;)
    {
  //  We are looping on feature class names
      if (ipFeatureNameEnum != 0 )
      {
        hr = ipFeatureNameEnum->Next(&datasetName);
        if (hr == S_OK)
          goto NEXT_FEATURE;
        ipFeatureNameEnum = 0;
      }

  //  Get a new Name
      hr = ipDatasetNameEnum->Next(&datasetName);
      if (FAILED(hr) || hr != S_OK)
      {
        if( SUCCEEDED( hr ) )
          hr = S_FALSE;
        break;
      }

  NEXT_FEATURE:
      esriDatasetType dsType;
      if (FAILED(hr = datasetName->get_Type(&dsType)))
        return hr;

  //  Skip feature dataset names
      if (dsType == esriDTFeatureDataset)
      {
        IFeatureDatasetNamePtr ipFDSN(datasetName);

        if (FAILED(hr = ipFDSN->get_FeatureClassNames(&ipFeatureNameEnum)))
          return hr;
      }
  //  Skip over anything that is not a feature class
      else if (dsType == esriDTFeatureClass)
      {
        CComBSTR name;
        datasetName->get_Name(&name);

        OGISGeometry_Row trData;

        ::wcscpy(trData.m_szTableName, name);

        IFieldPtr ipGeoField,
                  ipOIDField;
        hr = ESRIutil::GetGeometryAndOIDFields(datasetName, &ipGeoField, &ipOIDField);

        ipGeoField->get_Name(&name);
        ::wcscpy(trData.m_szColumnName, name);

        IGeometryDefPtr ipGeometryDef;
        hr = ipGeoField->get_GeometryDef(&ipGeometryDef);

        esriGeometryType esriGT;
        ipGeometryDef->get_GeometryType(&esriGT);

        trData.m_nGeomType = ::MapESRIGeomTypeToOGISGeomType(esriGT);
        trData.m_nSpatialRefId = 0;     // Don't handle spatial Reference ID for now (too slow)

        m_rgRowData.Add(trData);
      }
    }

    *pcRowsAffected = count;

    return S_OK;
	}
};


/////////////////////////////////////////////////////////////////////////////
// Spatial Reference schema rowset - not implemented
class OGISSpat_Row
{
public:
  int		m_nSpatialRefId;
  WCHAR	m_szAuthorityName[129];
  int /*WCHAR*/	m_nAuthorityId;
  WCHAR	m_pszSpatialRefSystem[2048];

  OGISSpat_Row()
	{
    m_nSpatialRefId = 0;
    m_szAuthorityName[0] = NULL;
    m_nAuthorityId = 0;
    ::wcscpy(m_pszSpatialRefSystem,L"" );
	}

BEGIN_PROVIDER_COLUMN_MAP(OGISSpat_Row)
	PROVIDER_COLUMN_ENTRY("SPATIAL_REF_SYSTEM_ID",1,m_nSpatialRefId)
	PROVIDER_COLUMN_ENTRY("AUTHORITY_NAME",2,m_szAuthorityName)
	PROVIDER_COLUMN_ENTRY("AUTHORITY_ID",3,m_nAuthorityId)
	PROVIDER_COLUMN_ENTRY_WSTR("SPATIAL_REF_SYSTEM_WKT",4,m_pszSpatialRefSystem)
END_PROVIDER_COLUMN_MAP()
};


class CSampleProvSessionSchemaSpatRef:
  public CRowsetImpl<CSampleProvSessionSchemaSpatRef,OGISSpat_Row,CSampleProvSession>
{
public:
  HRESULT Execute(LONG* pcRowsAffected, ULONG, const VARIANT*)
	{
    USES_CONVERSION;
    *pcRowsAffected = 0;
    return S_OK;
	}
};


#endif //__CSampleProvSession_H_
