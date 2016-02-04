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
#ifndef __I_COMMAND_WITH_PARAMETERS_IMPL__INCLUDED__
#define __I_COMMAND_WITH_PARAMETERS_IMPL__INCLUDED__

#include <atlcom.h>
#include <atldb.h>

///////////////////////////////////////////////////////////////////////////
// class ICommandWithPamametersImpl
static const struct StandardDataType
{
  const OLECHAR *typeName;
  DBTYPE dbType;
} standardDataTypes[] =
{
  { OLESTR("DBTYPE_I2"), DBTYPE_I2 },
  { OLESTR("DBTYPE_UI2"), DBTYPE_I2 },
  { OLESTR("DBTYPE_I4"), DBTYPE_I4 },
  { OLESTR("DBTYPE_UI4"), DBTYPE_UI4 },
  { OLESTR("DBTYPE_R4"), DBTYPE_R4 },
  { OLESTR("DBTYPE_R8"), DBTYPE_R8 },
  { OLESTR("DBTYPE_BOOL"), DBTYPE_I2 },
  { OLESTR("DBTYPE_VARIANT"), DBTYPE_VARIANT },
  { OLESTR("DBTYPE_IUNKNOWN"), DBTYPE_IUNKNOWN },
  { OLESTR("DBTYPE_DATE"), DBTYPE_DATE },
  { OLESTR("DBTYPE_BSTR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_CHAR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_WSTR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_VARCHAR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_LONGVARCHAR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_WCHAR"), DBTYPE_BSTR },
  { OLESTR("DBTYPE_BINARY"), DBTYPE_IUNKNOWN },
  { OLESTR("DBTYPE_VARBINARY"), DBTYPE_IUNKNOWN },
  { OLESTR("DBTYPE_LONGVARBINARY"), DBTYPE_IUNKNOWN },
  { OLESTR("DBTYPE_GEOMETRY"), DBTYPE_IUNKNOWN },
  { OLESTR("DBTYPE_VARIANT"), DBTYPE_VARIANT }
};


template <class T>
class ATL_NO_VTABLE ICommandWithPamametersImpl :
  public ICommandWithParameters
{
public:
  ICommandWithPamametersImpl(void) :
    m_nSetParams(0)
  {
  }

  STDMETHOD(GetParameterInfo)(
     ULONG *pcParams, 
     DBPARAMINFO **prgParamInfo, 
     OLECHAR **ppNamesBuffer)
  {
    ATLTRACE2(atlTraceDBProvider, 0, "ICommandWithPamameterImpl::GetParametersInfo\n");

    HRESULT hr = E_FAIL;

    if (!pcParams || !prgParamInfo || !ppNamesBuffer)
    {
	    ATLTRACE2(atlTraceDBProvider, 0, 
          "ICommandWithPamameterImpl::GetParametersInfo: - Error - a pointer is null\n");
   
       return E_INVALIDARG;
    }

    T *pT = 0;

    *pcParams = 0;

    // The way that we use the command parameters means that this never gets called...

    return DB_E_PARAMUNAVAILABLE;
  }
	
  STDMETHOD(MapParameterNames)(
    ULONG cParamNames, 
    const OLECHAR *rgParamNames[], 
    LONG rgParamOrdinals[])
  {
    ATLTRACE2(atlTraceDBProvider, 0, "ICommandWithPamametersImpl::MapParameterNames\n");

    if( cParamNames == 0 )
      return S_OK;

    if (cParamNames > 0 && (rgParamNames == 0 || rgParamOrdinals == 0))
      return E_INVALIDARG;

    for (ULONG i = 0; i < cParamNames; i++)
    {
      rgParamOrdinals[i] = GetSpatialParamOrdinal(rgParamNames[i]);
    }

    return S_OK;
  }

  STDMETHOD(SetParameterInfo)(
    ULONG cParams, 
    const ULONG rgParamOrdinals[], 
    const DBPARAMBINDINFO rgParamBindInfo[])
  {
    ATLTRACE2(atlTraceDBProvider, 0, "ICommandWithPamametersImpl::SetParameterInfo\n");

    HRESULT hr;

    if (cParams == 0)
    {
      m_paramInfo.RemoveAll();
      m_nSetParams = 0;
      return S_OK;
    }

    if (cParams > 0 && rgParamOrdinals == 0)
      return E_INVALIDARG;

    if (rgParamBindInfo != 0)
    {
      bool allParamNamesSet = true,
           oneParamNameSet = false;

      for (ULONG i = 0; i < cParams; i++)
      {
  //  We do not handle default parameter conversion
        if (rgParamBindInfo[i].pwszDataSourceType == 0)
          return E_INVALIDARG;

       DBTYPE dbType;
        if ((dbType = CheckDataType(rgParamBindInfo[i].pwszDataSourceType)) == DBTYPE_EMPTY)
          return DB_E_BADTYPENAME;

        if( rgParamBindInfo[i].pwszName == 0 )
          allParamNamesSet = false;
        else
          oneParamNameSet = true;

        if( rgParamBindInfo[i].dwFlags & ~(DBPARAMIO_INPUT | DBPARAMIO_OUTPUT) )
          return E_INVALIDARG;
      }

      if( oneParamNameSet && ! allParamNamesSet )
        return DB_E_BADPARAMETERNAME;
    }

    for (ULONG i = 0; i < cParams; i++)
    {
  //  Look for the parameter already in the list
      int j;
			for (j = m_nSetParams; --j >= 0;)
      {
        if( rgParamOrdinals[i] == m_paramInfo[j].iOrdinal )
          break;
      }

      if (j >= 0)
      {
  //  Discard the type info. for this parameter
        if( rgParamBindInfo == 0 )
        {
          m_paramInfo[j].~ParamInfo();
          while( j < (int)m_nSetParams )
          {
            m_paramInfo[j] = m_paramInfo[j + 1];
            j++;
          }
          --m_nSetParams;
        }
  //  Change parameter type info???
        else
        {
        }
      }
      else if (rgParamBindInfo != 0)
      {
        ParamInfo tempInfo;

        m_paramInfo.SetAtGrow( m_nSetParams, tempInfo );

        ParamInfo &paramInfo = m_paramInfo[m_nSetParams];
        if (FAILED(hr = paramInfo.Set( rgParamOrdinals[i], rgParamBindInfo[i])))
          return hr;
        m_nSetParams++;
      }
    }

    return S_OK;
  }

  static DBTYPE CheckDataType( const OLECHAR *pDataTypeName )
  {
    const StandardDataType *pDataTypes = standardDataTypes;

    ATLASSERT(pDataTypeName);
    for (int i = sizeof(standardDataTypes)/sizeof(standardDataTypes[0]); --i >= 0;)
    {
      if (::wcsicmp( pDataTypes->typeName, pDataTypeName) == 0)
        return pDataTypes->dbType;
      pDataTypes++;
    }

    return DBTYPE_EMPTY;
  }

protected:
  static ULONG GetSpatialParamOrdinal(const OLECHAR *paramName)
  {
    static const OLECHAR *paramNames[] =
    {
      OLESTR( "SPATIAL_FILTER" ),
      OLESTR( "SPATIAL_OPERATOR" ),
      OLESTR( "SPATIAL_GEOM_COL_NAME" )
    };

    for (int i = 0; i < sizeof(paramNames)/sizeof(paramNames[0]); i++)
    {
      if (::wcsicmp(paramNames[i], paramName) == 0)
      {
        return i + 1;
      }
    }

    return 0;
  }

  struct ParamInfo : public DBPARAMINFO
  {
    ParamInfo(void)
    {
      this->iOrdinal = 0;
      this->pwszName = 0;
      this->ulParamSize = 0;
      this->dwFlags = DBPARAMFLAGS_ISINPUT;
      this->bPrecision = 0;
      this->bScale = 0;
      this->wType = 0;
      this->pTypeInfo = 0;
    }

    ~ParamInfo( void )
    {
      if (this->pwszName != 0)
        delete [] this->pwszName;
    }

    HRESULT Set(ULONG ordinal, const DBPARAMBINDINFO &rParamBindInfo)
    {
      this->iOrdinal = ordinal;
      if (this->pwszName != 0)
      {
        delete [] this->pwszName;
        this->pwszName = 0;
      }

      if (rParamBindInfo.pwszName != 0)
      {
        this->pwszName = new OLECHAR[::wcslen( rParamBindInfo.pwszName ) + 1];
        if (this->pwszName == 0)
          return E_OUTOFMEMORY;
        ::wcscpy( this->pwszName, rParamBindInfo.pwszName );
      }

      ATLASSERT(rParamBindInfo.pwszDataSourceType);
      this->wType = CheckDataType(rParamBindInfo.pwszDataSourceType);
      this->ulParamSize = rParamBindInfo.ulParamSize;
      this->dwFlags = rParamBindInfo.dwFlags; 
      this->bPrecision = rParamBindInfo.bPrecision;
      this->bScale = rParamBindInfo.bScale;

      return S_OK;
    }
  };
  CArray<ParamInfo, ParamInfo &> m_paramInfo;
  ULONG                          m_nSetParams;
};


#endif // __I_COMMAND_WITH_PARAMETERS_IMPL__INCLUDED__
