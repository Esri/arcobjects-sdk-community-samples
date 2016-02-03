#ifndef __I_COLUMNS_ROWSET_IMPL__INCLUDED__
#define __I_COLUMNS_ROWSET_IMPL__INCLUDED__

#include <atlcom.h>
#include <atldb.h>

#define PROVIDER_COLUMN_ENTRY_DBID(name, dbid, ordinal, member) \
{ \
	(LPOLESTR)OLESTR(name), \
	(ITypeInfo*)NULL, \
	(ULONG)ordinal, \
	DBCOLUMNFLAGS_ISFIXEDLENGTH, \
   (ULONG)sizeof(((_Class*)0)->member), \
	_GetOleDBType(((_Class*)0)->member), \
	(BYTE)0, \
	(BYTE)0, \
	{ \
		EXPANDGUID(dbid.uGuid.guid), \
		(DWORD)dbid.eKind, \
      (LPOLESTR)dbid.uName.ulPropid\
	}, \
   offsetof(_Class, member) \
},


class CColumnsRowsetRow
{
public:
	WCHAR    m_DBCOLUMN_IDNAME[129];
	GUID     m_DBCOLUMN_GUID;
  ULONG    m_DBCOLUMN_PROPID;
  WCHAR    m_DBCOLUMN_NAME[129];
  ULONG    m_DBCOLUMN_NUMBER;
  USHORT   m_DBCOLUMN_TYPE;
  IUnknown *m_DBCOLUMN_TYPEINFO;
  ULONG    m_DBCOLUMN_COLUMNSIZE;
  USHORT   m_DBCOLUMN_PRECISION;
  SHORT    m_DBCOLUMN_SCALE;
  ULONG    m_DBCOLUMN_FLAGS;

  // OGIS stuff
  ULONG    m_nGeomType;
  LONG     m_nSpatialRefId;
  WCHAR    m_pszSpatialRefSystem[1024];  // BSTR?

	CColumnsRowsetRow()
	{
		ClearMembers();
	}

	void ClearMembers()
	{
    m_DBCOLUMN_IDNAME[0] = NULL;
    m_DBCOLUMN_GUID = GUID_NULL;
    m_DBCOLUMN_PROPID = 0;
    m_DBCOLUMN_NAME[0] = 0;
    m_DBCOLUMN_NUMBER = 0;
    m_DBCOLUMN_TYPE = 0;
    m_DBCOLUMN_TYPEINFO = 0;
    m_DBCOLUMN_COLUMNSIZE = 0;
    m_DBCOLUMN_PRECISION = 0;
    m_DBCOLUMN_SCALE = 0;
    m_DBCOLUMN_FLAGS = 0;
    // Optional/additional columns
    m_nGeomType = 0;
    m_nSpatialRefId = 0;
    m_pszSpatialRefSystem[0] = L'\0';
  }


BEGIN_PROVIDER_COLUMN_MAP(CColumnsRowsetRow)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_IDNAME", DBCOLUMN_IDNAME, 1, m_DBCOLUMN_IDNAME)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_GUID", DBCOLUMN_GUID, 2, m_DBCOLUMN_GUID)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_PROPID", DBCOLUMN_PROPID, 3, m_DBCOLUMN_PROPID)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_NAME", DBCOLUMN_NAME, 4, m_DBCOLUMN_NAME)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_NUMBER", DBCOLUMN_NUMBER, 5, m_DBCOLUMN_NUMBER)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_TYPE", DBCOLUMN_TYPE, 6, m_DBCOLUMN_TYPE)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_TYPEINFO", DBCOLUMN_TYPEINFO, 7, m_DBCOLUMN_TYPEINFO)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_COLUMNSIZE", DBCOLUMN_COLUMNSIZE, 8, m_DBCOLUMN_COLUMNSIZE)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_PRECISION", DBCOLUMN_PRECISION, 9, m_DBCOLUMN_PRECISION)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_SCALE", DBCOLUMN_SCALE, 10, m_DBCOLUMN_SCALE)
  PROVIDER_COLUMN_ENTRY_DBID("DBCOLUMN_FLAGS", DBCOLUMN_FLAGS, 11, m_DBCOLUMN_FLAGS)

  PROVIDER_COLUMN_ENTRY("GEOM_TYPE",12,m_nGeomType)
  PROVIDER_COLUMN_ENTRY("SPATIAL_REF_SYSTEM_ID",13,m_nSpatialRefId)
  PROVIDER_COLUMN_ENTRY("SPATIAL_REF_SYSTEM_WKT",14,m_pszSpatialRefSystem)
END_PROVIDER_COLUMN_MAP()
};


template <class T, class CreatorClass>
class ATL_NO_VTABLE IColumnsRowsetImpl : public IColumnsRowset
{
  public:

    class CColumnsRowsetRowset : public CRowsetImpl< CColumnsRowsetRowset , CColumnsRowsetRow, CreatorClass>
    {
     public:
      DBSTATUS GetDBStatus(CSimpleRow*pRC, ATLCOLUMNINFO*pColInfo)
      {
        T* pT = (T*)this;

        if (::wcscmp(pColInfo->pwszName, OLESTR("SPATIAL_REF_SYSTEM_ID")) == 0 ||
            ::wcscmp(pColInfo->pwszName, OLESTR("SPATIAL_REF_SYSTEM_WKT")) == 0 )
          return DBSTATUS_S_ISNULL;

        if( ::wcscmp(pColInfo->pwszName, OLESTR("GEOM_TYPE")) == 0)
        {
          CColumnsRowsetRow *psRow = &(m_rgRowData[pRC->m_iRowset]);
          if (psRow->m_nGeomType == 0)
            return DBSTATUS_S_ISNULL;
        }

        return DBSTATUS_S_OK;
      }

       HRESULT PopulateRowset(ITable* pTable)
        {
          HRESULT hr;

	        IFieldsPtr ipFields;
          long count;
          if( FAILED(hr = pTable->get_ESRIFields(&ipFields)) ||
              FAILED(hr = ipFields->get_FieldCount(&count)))
            return hr;

          long ordinal = 0;
          for (long i = 0; i < count; i++)
          {
    	      IFieldPtr ipField;
            esriFieldType fieldType;
		        if( FAILED(hr = ipFields->get_ESRIField(i, &ipField)) ||
                FAILED(hr = ipField->get_Type(&fieldType)))
              return hr;

            if (fieldType != esriFieldTypeOID && fieldType != esriFieldTypeGeometry)
              continue;

            CColumnsRowsetRow data;

            CComBSTR name;
            ipField->get_Name(&name);
            ::wcscpy(data.m_DBCOLUMN_IDNAME, name);
	          data.m_DBCOLUMN_GUID = GUID_NULL;
            data.m_DBCOLUMN_PROPID = 0;
            ::wcscpy(data.m_DBCOLUMN_NAME, name);
            data.m_DBCOLUMN_NUMBER = ++ordinal;
            if (fieldType == esriFieldTypeOID)
            {
              data.m_DBCOLUMN_TYPE = DBTYPE_I4;
            }
            else
            {
              data.m_DBCOLUMN_TYPE = DBTYPE_IUNKNOWN;
              IGeometryDefPtr ipGeometryDef;
              hr = ipField->get_GeometryDef(&ipGeometryDef);

              esriGeometryType esriGT;
              ipGeometryDef->get_GeometryType(&esriGT);

              data.m_nGeomType = ::MapESRIGeomTypeToOGISGeomType(esriGT);
            }
            data.m_nSpatialRefId = 0;     // Don't handle spatial Reference ID for now (too slow)
            data.m_DBCOLUMN_COLUMNSIZE = 4;
            data.m_DBCOLUMN_PRECISION = ~0;
            data.m_DBCOLUMN_SCALE = ~0;
            data.m_DBCOLUMN_FLAGS = 0;

            if (!m_rgRowData.Add(data))
            {
			        return E_OUTOFMEMORY;
            }
           }

           return S_OK;
        }
      };

      STDMETHOD(GetAvailableColumns)(
         ULONG *pcOptColumns,
         DBID **prgOptColumns)
      {
		    ATLTRACE2(atlTraceDBProvider, 0, "IColumnsRowsetImpl::GetAvailableColumns()\n");

         if (!pcOptColumns || !prgOptColumns)
         {
            return E_INVALIDARG;
         }

         const ULONG c_numOptColumns = 3;

         *pcOptColumns = c_numOptColumns;

         DBID *pOptCols = (DBID*)CoTaskMemAlloc(sizeof(DBID) * c_numOptColumns);

         ::memset(pOptCols, 0, sizeof(DBID) * c_numOptColumns);

          static const DBID optColumns[] =
          {
            { DB_NULLGUID, DBKIND_NAME, (LPOLESTR)OLESTR( "DBCOLUMN_GEOM_TYPE" ) },
            { DB_NULLGUID, DBKIND_NAME, (LPOLESTR)OLESTR( "DBCOLUMN_SPATIAL_REF_SYSTEM_ID" ) },
            { DB_NULLGUID, DBKIND_NAME, (LPOLESTR)OLESTR( "DBCOLUMN_SPATIAL_REF_SYSTEM_WKT" ) }
          };

         ::memcpy(pOptCols, optColumns, sizeof(optColumns));
      
         *pcOptColumns = c_numOptColumns;
         *prgOptColumns = pOptCols;

         return S_OK;
      }

      STDMETHOD(GetColumnsRowset)(
         IUnknown *pUnkOuter,
         ULONG cOptColumns,
         const DBID rgOptColumns[],
         REFIID riid,
         ULONG cPropertySets,
         DBPROPSET rgPropertySets[],
         IUnknown **ppColRowset)
      {
		    ATLTRACE2(atlTraceDBProvider, 0, "IColumnsRowsetImpl::GetColumnsRowset()\n");

        // need to create our columns rowset, 
        // then populate it from the actual rowset that we represent...
      
        // We can do that by using IColumnsInfo...

        CColumnsRowsetRowset *pColRowset = 0;

        HRESULT hr;
         
        if (FAILED(hr = CreateRowset(pUnkOuter, riid, cPropertySets, rgPropertySets, pColRowset, ppColRowset)))
          return hr;

        if (pColRowset)
        {
          T *pT = (T*)this;

          CComPtr<ICommand> ipIC;
  	      if (FAILED(hr = pT->GetSpecification(IID_ICommand, (IUnknown **)&ipIC)))
            return hr;

          CSampleProvCommand *pCom = static_cast<CSampleProvCommand *>((ICommand*)ipIC);

          hr = pColRowset->PopulateRowset(pCom->m_ipTable);
        }
        else
        {
          hr = E_UNEXPECTED;
        }
      
        return hr;
      }

   private :

      HRESULT CreateRowset(
        IUnknown * pUnkOuter,	
	      REFIID riid,				
        ULONG cPropertySets,
        DBPROPSET rgPropertySets[],
        CColumnsRowsetRowset *&pRowsetObj,
	      IUnknown **ppRowset)
      {
	      HRESULT hr;

         T* pT = (T*)this;

	      if (ppRowset != NULL)
         {
		      *ppRowset = NULL;
         }

	      if ((pUnkOuter != NULL) && !InlineIsEqualUnknown(riid))
         {
		      return DB_E_NOAGGREGATION;
         }

	      CComPolyObject<CColumnsRowsetRowset>* pPolyObj;
	      
         if (FAILED(hr = CComPolyObject<CColumnsRowsetRowset>::CreateInstance(pUnkOuter, &pPolyObj)))
         {
		      return hr;
         }
	      
         // Ref the created COM object and Auto release it on failure
	      
         CComPtr<IUnknown> spUnk;
	      
         hr = pPolyObj->QueryInterface(&spUnk);
	      
         if (FAILED(hr))
	      {
		      delete pPolyObj; // must hand delete as it is not ref'd
		      return hr;
	      }
	      
         // Get a pointer to the Rowset instance
	      pRowsetObj = &(pPolyObj->m_contained);

	      if (FAILED(hr = pRowsetObj->FInit(pT)))
         {
		      return hr;
         }

         // Set Properties that were passed in.

         const GUID* ppGuid[1];
         ppGuid[0] = &DBPROPSET_ROWSET;

         // Call SetProperties.  The true in the last parameter indicates
         // the special behavior that takes place on rowset creation (i.e.
         // it succeeds as long as any of the properties were not marked
         // as DBPROPS_REQUIRED.

         hr = pRowsetObj->SetProperties(0, cPropertySets, rgPropertySets, 1, ppGuid, true);

         if (FAILED(hr))
         {
            return hr;
         }

	      pRowsetObj->SetSite(pT->GetUnknown());

	      if (InlineIsEqualGUID(riid, IID_NULL) || ppRowset == NULL)
	      {
		      if (ppRowset != NULL)
			      *ppRowset = NULL;
		      return hr;
	      }

		   if (InlineIsEqualGUID(riid, IID_NULL) || ppRowset == NULL)
		   {
			   if (ppRowset != NULL)
				   *ppRowset = NULL;
			   return hr;
		   }
		   hr = pPolyObj->QueryInterface(riid, (void**)ppRowset);
		   if (FAILED(hr))
			   return hr;
		   for (int iBind = 0; iBind < pT->m_rgBindings.GetCount(); iBind++)
		   {
			   T::_BindType* pBind = NULL;
			   T::_BindType* pBindSrc = NULL;
			   ATLTRY(pBind = new T::_BindType);
			   if (pBind == NULL)
			   {
				   ATLTRACE2(atlTraceDBProvider, 0, "Failed to allocate memory for new Binding\n");
				   return E_OUTOFMEMORY;
			   }
			   // auto cleanup on failure
			   pBindSrc = pT->m_rgBindings.GetValueAt((POSITION)iBind);
			   if (pBindSrc == NULL)
			   {
				   ATLTRACE2(atlTraceDBProvider, 0, "The map appears to be corrupted, failing!!\n");
				   return E_FAIL;
			   }
			   if (!pRowsetObj->m_rgBindings.SetAt(pT->m_rgBindings.GetKeyAt((POSITION)iBind), pBind))
			   {
				   ATLTRACE2(atlTraceDBProvider, 0, "Failed to add hAccessor to Map\n");
				   return E_OUTOFMEMORY;
			   }
			   if (pBindSrc->cBindings)
			   {
				   ATLTRY(pBind->pBindings = new DBBINDING[pBindSrc->cBindings])
				   if (pBind->pBindings == NULL)
				   {
					   ATLTRACE2(atlTraceDBProvider, 0, "Failed to Allocate dbbinding Array\n");
					   // We added it, must now remove on failure
						 pRowsetObj->m_rgBindings.RemoveAtPos((POSITION)pT->m_rgBindings.GetKeyAt((POSITION)iBind));
					   return E_OUTOFMEMORY;
				   }
			   }
			   else
			   {
				   pBind->pBindings = NULL; // NULL Accessor
			   }

			   pBind->dwAccessorFlags = pBindSrc->dwAccessorFlags;
			   pBind->cBindings = pBindSrc->cBindings;
			   pBind->dwRef = 1;
			   memcpy (pBind->pBindings, pBindSrc->pBindings, (pBindSrc->cBindings)*sizeof(DBBINDING));
		   }

	      return hr;
      }

};

#endif // __I_COLUMNS_ROWSET_IMPL__INCLUDED__
