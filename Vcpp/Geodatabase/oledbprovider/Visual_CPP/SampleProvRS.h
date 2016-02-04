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

// SampleProvRS.h : Declaration of the CSampleProvRowset
#ifndef __CSampleProvRowset_H_
#define __CSampleProvRowset_H_
#include "resource.h"       // main symbols
#include "SampleProvSess.h"
#include "IColumnsRowsetImpl.h"
#include "ICommandWithParametersImpl.h"

// class that containins the data for each row in our spatial tables
class CSampleProvFeatureRowData
{
public:
  long m_oidColumn;
  IUnknown* m_shapeColumn;

  CSampleProvFeatureRowData()
  {
    m_oidColumn = 0;
    m_shapeColumn = 0;
  }

  ~CSampleProvFeatureRowData()
  {
    m_oidColumn = 0;
    if (m_shapeColumn)
      m_shapeColumn->Release();
  }

  CSampleProvFeatureRowData(const CSampleProvFeatureRowData &obj)
  {
    m_oidColumn = obj.m_oidColumn;
    if (m_shapeColumn)
      m_shapeColumn->Release();
    m_shapeColumn = obj.m_shapeColumn;
    m_shapeColumn->AddRef();
  }

  void operator=(const CSampleProvFeatureRowData &obj)
  {
    m_oidColumn = obj.m_oidColumn;
    if (m_shapeColumn)
      m_shapeColumn->Release();
    m_shapeColumn = obj.m_shapeColumn;
    if (m_shapeColumn)
      m_shapeColumn->AddRef();
  }

BEGIN_PROVIDER_COLUMN_MAP(CSampleProvFeatureRowData)
	PROVIDER_COLUMN_ENTRY("OID", 1, m_oidColumn)
	PROVIDER_COLUMN_ENTRY("SHAPE", 2, m_shapeColumn)
END_PROVIDER_COLUMN_MAP()
public:
};

// class used to hold the current row in memory
template <class T>
class CVirtualArray
{
public:
	CVirtualArray() : m_nArraySize(0), m_shapeFieldIndex(-1)
  {}

	~CVirtualArray()
  {
    RemoveAll();
  }
	void	RemoveAll()
  {}
	
  void	Initialize(long nRows, IESRICursor *pCursor)
  {
    m_nArraySize = nRows;
    m_ipCursor = pCursor;
  }

	T &operator[](int iIndex)
  {
  	T data;

    IESRIRowPtr ipRow;
    HRESULT hr = m_ipCursor->NextRow(&ipRow);
    ATLASSERT(hr == S_OK);

    ipRow->get_OID(&data.m_oidColumn);

    if (m_shapeFieldIndex == -1)
    {
      IFieldsPtr ipFields;
      ipRow->get_ESRIFields(&ipFields);
      ipFields->FindField(CComBSTR(OLESTR("Shape")), &m_shapeFieldIndex);
    }

    if (m_shapeFieldIndex != -1)
    {
      _variant_t var;
      ipRow->get_Value(m_shapeFieldIndex, &var);

      ESRIutil::GetOGISWkb(var.punkVal, &data.m_shapeColumn);
    }

    m_currRec = data;
		return m_currRec;
  }

	int	GetSize() const
  {
    return m_nArraySize;
  }

  int	GetCount() const
  {
    return GetSize();
  }

private:
	int	m_nArraySize;
  long m_shapeFieldIndex;
  IESRICursorPtr m_ipCursor;
  T m_currRec;
};


// CSampleProvCommand - the Command object
class ATL_NO_VTABLE CSampleProvCommand : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public IAccessorImpl<CSampleProvCommand>,
	public ICommandTextImpl<CSampleProvCommand>,
	public ICommandPropertiesImpl<CSampleProvCommand>,
	public IObjectWithSiteImpl<CSampleProvCommand>,
	public IConvertTypeImpl<CSampleProvCommand>,
	public IColumnsInfoImpl<CSampleProvCommand>,
  public ICommandWithPamametersImpl<CSampleProvCommand>
{
public:
BEGIN_COM_MAP(CSampleProvCommand)
	COM_INTERFACE_ENTRY(ICommand)
	COM_INTERFACE_ENTRY(IObjectWithSite)
	COM_INTERFACE_ENTRY(IAccessor)
	COM_INTERFACE_ENTRY(ICommandProperties)
	COM_INTERFACE_ENTRY2(ICommandText, ICommand)
	COM_INTERFACE_ENTRY(IColumnsInfo)
	COM_INTERFACE_ENTRY(IConvertType)
  COM_INTERFACE_ENTRY(ICommandWithParameters)
END_COM_MAP()
// ICommand
public:
	HRESULT FinalConstruct()
	{
		HRESULT hr = CConvertHelper::FinalConstruct();
		if (FAILED (hr))
			return hr;
		hr = IAccessorImpl<CSampleProvCommand>::FinalConstruct();
		if (FAILED(hr))
			return hr;
		return CUtlProps<CSampleProvCommand>::FInit();
	}
	void FinalRelease()
	{
		IAccessorImpl<CSampleProvCommand>::FinalRelease();
	}
	HRESULT WINAPI Execute(IUnknown * pUnkOuter, REFIID riid, DBPARAMS * pParams, 
						  LONG * pcRowsAffected, IUnknown ** ppRowset);
	static ATLCOLUMNINFO* GetColumnInfo(CSampleProvCommand* pv, ULONG* pcInfo)
	{
		return CSampleProvFeatureRowData::GetColumnInfo(pv,pcInfo);
	}

	STDMETHOD(SetCommandText)(REFGUID rguidDialect, LPCOLESTR pwszCommand);

BEGIN_PROPSET_MAP(CSampleProvCommand)
	BEGIN_PROPERTY_SET(DBPROPSET_ROWSET)
		PROPERTY_INFO_ENTRY(IAccessor)
		PROPERTY_INFO_ENTRY(IColumnsInfo)
		PROPERTY_INFO_ENTRY(IConvertType)
		PROPERTY_INFO_ENTRY(IRowset)
		PROPERTY_INFO_ENTRY(IRowsetIdentity)
		PROPERTY_INFO_ENTRY(IRowsetInfo)
		PROPERTY_INFO_ENTRY(IRowsetLocate)
		PROPERTY_INFO_ENTRY(BOOKMARKS)
		PROPERTY_INFO_ENTRY(BOOKMARKSKIPPED)
		PROPERTY_INFO_ENTRY(BOOKMARKTYPE)
		PROPERTY_INFO_ENTRY(CANFETCHBACKWARDS)
		PROPERTY_INFO_ENTRY(CANHOLDROWS)
		PROPERTY_INFO_ENTRY(CANSCROLLBACKWARDS)
		PROPERTY_INFO_ENTRY(LITERALBOOKMARKS)
		PROPERTY_INFO_ENTRY(ORDEREDBOOKMARKS)
	END_PROPERTY_SET(DBPROPSET_ROWSET)
END_PROPSET_MAP()

public:
  ITablePtr                   m_ipTable;
  IQueryFilterPtr             m_ipQueryFilter;
  HRESULT SetupSpatialFilter(DBPARAMS*, IGeometry**, esriSpatialRelEnum*, OLECHAR**);
};

// CSampleProvRowset - the Rowset object
class CSampleProvRowset :
  public CRowsetImpl< CSampleProvRowset, CSampleProvFeatureRowData, CSampleProvCommand, CVirtualArray<CSampleProvFeatureRowData>, CSimpleRow>,
  public IColumnsRowsetImpl<CSampleProvRowset, CSampleProvCommand>
{
public:
BEGIN_COM_MAP(CSampleProvRowset)
	COM_INTERFACE_ENTRY(IAccessor)
	COM_INTERFACE_ENTRY(IObjectWithSite)
	COM_INTERFACE_ENTRY(IRowsetInfo)
	COM_INTERFACE_ENTRY(IColumnsInfo)
	COM_INTERFACE_ENTRY(IConvertType)
	COM_INTERFACE_ENTRY(IRowsetIdentity)
	COM_INTERFACE_ENTRY(IRowset)
	COM_INTERFACE_ENTRY(IColumnsRowset)
END_COM_MAP()

	HRESULT Execute(DBPARAMS * pParams, LONG* pcRowsAffected)
	{
		USES_CONVERSION;

	  HRESULT hr;

    // Get the Command object so we can get the table object
    CComPtr<ICommand> ipIC;
  	if (FAILED(hr = GetSpecification(IID_ICommand, (IUnknown **)&ipIC)))
      return hr;
    CSampleProvCommand *pCom = static_cast<CSampleProvCommand *>((ICommand*)ipIC);

    IESRICursorPtr ipCursor;
    if (FAILED(hr = pCom->m_ipTable->Search(pCom->m_ipQueryFilter, VARIANT_TRUE, &ipCursor)))
      return hr;

    long nRows;
    if (FAILED(hr = pCom->m_ipTable->RowCount(pCom->m_ipQueryFilter, &nRows)))
      return hr;

  	m_rgRowData.Initialize(nRows, ipCursor);

		return S_OK;
	}
};
#endif //__CSampleProvRowset_H_
