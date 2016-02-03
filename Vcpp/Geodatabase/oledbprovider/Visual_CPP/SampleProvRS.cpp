
// Implementation of the CSampleProvCommand
#include "stdafx.h"
#include "SampleProvider.h"
#include "SampleProvRS.h"

static HRESULT ParseCommand(
  CComBSTR&       cmd,
  CString&        colNames,
  int&            colNamesBegin,
  int&            colNamesEnd,
  CString&        tblNames,
  int&            tblNameBegin,
  int&            tblNameEnd,
  CString&        whereClause,
  int&            whereClauseBegin,
  int&            whereClauseEnd
)
{
  CString lowerCmd(cmd);
  int length = cmd.Length();

  lowerCmd.MakeLower();

//  Parse out table name(s)
  tblNameBegin = lowerCmd.Find(_T("from "));

//  Illegal SELECT command
  if( tblNameBegin == -1 || lowerCmd.Find(_T("select ")) == -1 )
    return DB_E_ERRORSINCOMMAND;

  tblNameBegin += 5;

  int wherePos = lowerCmd.Find(_T("where "));
  if (wherePos != -1)
  {
    tblNameEnd = wherePos;
// Parse out the where clause
//
    whereClauseBegin = wherePos + 6;
    whereClauseEnd = length;
    if (whereClause)
      whereClause = lowerCmd.Mid(whereClauseBegin, whereClauseEnd - whereClauseBegin);
  }
  else
  {
    tblNameEnd = length;
  }

  int orderbyClause;
  if ((orderbyClause = lowerCmd.Find(_T("order "))) != -1 ||
      (orderbyClause = lowerCmd.Find(_T("group "))) != -1)
  {
    return E_FAIL;
  }

  if (tblNames)
  {
    tblNames = lowerCmd.Mid(tblNameBegin, tblNameEnd - tblNameBegin);
    tblNames.TrimLeft();
    tblNames.TrimRight();
  }

// Parse out the column name(s)
//
  int selectPos = lowerCmd.Find(_T("select "));

  colNamesBegin = selectPos + 7;
  colNamesEnd = tblNameBegin - 6;

  if (colNames)
  {
    colNames = lowerCmd.Mid(colNamesBegin, colNamesEnd - colNamesBegin);
    colNames.TrimLeft();
    colNames.TrimRight();
  }

  return S_OK;
}

//////////////////////////////////////////////////////////////////////////////
// CSampleProvCommand methods
HRESULT CSampleProvCommand::SetupSpatialFilter(
  DBPARAMS*         pParams,
  IGeometry**       ppGeometry,
  esriSpatialRelEnum* pSpatialRel,
  OLECHAR**         ppSpatialColName
)
{
  HRESULT hr = S_OK;

  *pSpatialRel = esriSpatialRelUndefined;

  if (pParams == 0 || m_nSetParams == 0)    // No parameters
    return S_FALSE;

  struct SpatialParamsDef
  {
    const OLECHAR *name;
    DBTYPE        type;
  };
  static SpatialParamsDef spatialParams[] =
  {
    { OLESTR("SPATIAL_FILTER"),         DBTYPE_VARIANT },
    { OLESTR("SPATIAL_OPERATOR"),       DBTYPE_UI4 },
    { OLESTR("SPATIAL_GEOM_COL_NAME"),  DBTYPE_BSTR }
  };
  ULONG spatialFilterParam = 0,
        spatialOpParam = 0,
        spatialGeoColNameParam = 0;

  // Check for the OGIS spatial parameters.  Currently, these are the only parameters we expect
  for (ULONG j = 0; j < m_nSetParams; j++)
  {
    ParamInfo &paramInfo = m_paramInfo[j];
		
		ULONG i;
    for (i = 0; i < sizeof(spatialParams) / sizeof(spatialParams[0]); i++)
    {
      // ADO does not pass parameter names down, so we must check type
      if( (paramInfo.pwszName != 0 && ::wcsicmp(paramInfo.pwszName, spatialParams[i].name) == 0) ||
          (paramInfo.wType == spatialParams[i].type))
      {
        switch (i)
        {
          case 0:
          case 3:
            spatialFilterParam = paramInfo.iOrdinal;
          break;

          case 1:
            spatialOpParam = paramInfo.iOrdinal;
          break;

          case 2:
            spatialGeoColNameParam = paramInfo.iOrdinal;
          break;
        }
        break;
      }
    }
//  Didn't find a spatial filter parameter - for now just return OK
    if (i == sizeof(spatialParams) / sizeof(spatialParams[0]))
    {
      return S_OK;
    }
  }

  // Get the values of the parameters & convert to ESRI objects
  ATLBINDINGS *pBind = (ATLBINDINGS *)pParams->hAccessor;
  if( pBind->cBindings != 3 )
    return E_FAIL;

  ULONG ogisSpatialOp = 0;
  WCHAR *pSpatialGeoColName = 0;
  VARIANT *pFilter = 0;

  for( ULONG i = 0; i < pBind->cBindings; i++ )
  {
    const DBBINDING &binding = pBind->pBindings[i];
    char* pValue = (char*)pParams->pData + binding.obValue;
    VARIANT *pVariant = (VARIANT *)pValue;

    if (binding.iOrdinal == spatialFilterParam)
    {
      if (binding.wType != DBTYPE_VARIANT || V_VT( pVariant ) != (VT_ARRAY|VT_UI1))
        return E_FAIL;
      pFilter = pVariant;
    }
    else if (binding.iOrdinal == spatialOpParam)
    {
      if (binding.wType == DBTYPE_VARIANT)
      {
        if (V_VT(pVariant) != VT_UI4 && V_VT(pVariant) != VT_I4)
          return E_FAIL;
        ogisSpatialOp = (ULONG)V_I4(pVariant);
      }
      else if (binding.wType == DBTYPE_UI4 || binding.wType == DBTYPE_I4)
      {
        ogisSpatialOp = (ULONG)*pValue;
      }
    }
    else if (binding.iOrdinal == spatialGeoColNameParam)
    {
      if (binding.wType == DBTYPE_VARIANT)
      {
        if (V_VT(pVariant) != VT_BSTR)
          return E_FAIL;
        pSpatialGeoColName = (WCHAR *)V_BSTR(pVariant);
      }
      else if (binding.wType == DBTYPE_WSTR || binding.wType == DBTYPE_BSTR)
      {
        pSpatialGeoColName = (WCHAR *)pValue;
      }
    }
    else
    {
      ASSERT( 0 );
      return E_FAIL;
    }
  }

  // Convert the WKB back to an ESRI geometry object
  ASSERT(pFilter != 0);
  BYTE* pBytes = 0;
  if( FAILED(hr = ::SafeArrayAccessData(V_ARRAY(pFilter), (void **)&pBytes)))
    return E_FAIL;

  long cBytesRead;
  IGeometryFactoryPtr ipGeomFact(CLSID_GeometryEnvironment);
  hr = ipGeomFact->CreateGeometryFromWkb(&cBytesRead, pBytes, ppGeometry);
  ::SafeArrayUnaccessData(V_ARRAY(pFilter));
  if (FAILED(hr))
    return E_FAIL;

  *ppSpatialColName = pSpatialGeoColName;

  if( (*pSpatialRel = ::MapOGISSpatialOpToESRISpatialRel(ogisSpatialOp)) == esriSpatialRelUndefined )
    return E_FAIL;

  return hr;
}

STDMETHODIMP CSampleProvCommand::SetCommandText(REFGUID rguidDialect, LPCOLESTR pwszCommand)
{
  HRESULT hr = ICommandTextImpl<CSampleProvCommand>::SetCommandText(rguidDialect, pwszCommand);

  if (FAILED(hr))
    return hr;

  m_ipTable = 0;
  m_ipQueryFilter = 0;

  return S_OK;
}

HRESULT CSampleProvCommand::Execute(IUnknown * pUnkOuter, REFIID riid, DBPARAMS * pParams, 
								 LONG * pcRowsAffected, IUnknown ** ppRowset)
{
	CSampleProvRowset* pRowset;

  HRESULT hr;

  // Get back to the Session object so we can get the Workspace
	ATLASSERT(m_spUnkSite != 0);
  CComPtr<IGetDataSource> ipGDS;
	m_spUnkSite->QueryInterface(IID_IGetDataSource, (void **)&ipGDS);

  CSampleProvSession *pSess = static_cast<CSampleProvSession *>((IGetDataSource*)ipGDS);

  IFeatureWorkspacePtr ipFWS(pSess->m_ipWS);
  if( ipFWS == 0 )
    return E_FAIL;

  int colNamesStart,
      colNamesEnd,
      tblNameStart,
      tblNameEnd,
      whereClauseStart,
      whereClauseEnd;
  CString colNames,
         tableNames,
         whereClause;

  // Parse the SQL query
  if (FAILED(hr = ParseCommand(this->m_strCommandText, colNames, colNamesStart, colNamesEnd,
          tableNames, tblNameStart, tblNameEnd, whereClause, whereClauseStart, whereClauseEnd)))
    return hr;

  // Setup the spatial parameters (if any)
  IGeometryPtr ipGeometry;
  esriSpatialRelEnum spatialRel;
  OLECHAR* pSpatialColName;
  if (FAILED(hr = SetupSpatialFilter(pParams, &ipGeometry, &spatialRel, &pSpatialColName)))
    return hr;

  // Create the query filter objects
  if (ipGeometry != 0 && spatialRel != esriSpatialRelUndefined && pSpatialColName != 0)
  {
    ISpatialFilterPtr ipSQF(CLSID_SpatialFilter);

    hr = ipSQF->putref_Geometry(ipGeometry);
    hr = ipSQF->put_SpatialRel(spatialRel);
    hr = ipSQF->put_GeometryField(CComBSTR(pSpatialColName));
    m_ipQueryFilter = ipSQF;
  }
  else
  {
    IQueryFilterPtr ipQF(CLSID_QueryFilter);
    m_ipQueryFilter = ipQF;
  }

  m_ipQueryFilter->put_SubFields(CComBSTR(colNames));
  if (! whereClause.IsEmpty())
    m_ipQueryFilter->put_WhereClause(CComBSTR(whereClause));

  if (m_ipTable == 0)
    if (FAILED(hr = ipFWS->OpenTable(CComBSTR(tableNames), &m_ipTable)))
      return hr;

	return CreateRowset(pUnkOuter, riid, pParams, pcRowsAffected, ppRowset, pRowset);
}

