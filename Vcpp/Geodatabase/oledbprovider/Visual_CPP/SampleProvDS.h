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
// SampleProvDS.h : Declaration of the CSampleProvSource
#ifndef __CSampleProvSource_H_
#define __CSampleProvSource_H_
#include "resource.h"       // main symbols
#include "SampleProvRS.h"

/////////////////////////////////////////////////////////////////////////////
// CDataSource
class ATL_NO_VTABLE CSampleProvSource : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSampleProvSource, &CLSID_SampleProv>,
	public IDBCreateSessionImpl<CSampleProvSource, CSampleProvSession>,
	public IDBInitializeImpl<CSampleProvSource>,
	public IDBPropertiesImpl<CSampleProvSource>,
	public IPersistImpl<CSampleProvSource>,
	public IInternalConnectionImpl<CSampleProvSource>
{
public:
	HRESULT FinalConstruct()
	{
		return FInit();
	}
DECLARE_REGISTRY_RESOURCEID(IDR_SAMPLEPROV)
BEGIN_PROPSET_MAP(CSampleProvSource)
	BEGIN_PROPERTY_SET(DBPROPSET_DATASOURCEINFO)
		PROPERTY_INFO_ENTRY(ACTIVESESSIONS)
		PROPERTY_INFO_ENTRY(DATASOURCEREADONLY)
		PROPERTY_INFO_ENTRY(BYREFACCESSORS)
		PROPERTY_INFO_ENTRY(OUTPUTPARAMETERAVAILABILITY)
		PROPERTY_INFO_ENTRY(PROVIDEROLEDBVER)
		PROPERTY_INFO_ENTRY(DSOTHREADMODEL)
		PROPERTY_INFO_ENTRY(SUPPORTEDTXNISOLEVELS)
		PROPERTY_INFO_ENTRY(USERNAME)
	END_PROPERTY_SET(DBPROPSET_DATASOURCEINFO)
	BEGIN_PROPERTY_SET(DBPROPSET_DBINIT)
		PROPERTY_INFO_ENTRY(AUTH_PASSWORD)
		PROPERTY_INFO_ENTRY(AUTH_PERSIST_SENSITIVE_AUTHINFO)
		PROPERTY_INFO_ENTRY(AUTH_USERID)
		PROPERTY_INFO_ENTRY(INIT_DATASOURCE)
		PROPERTY_INFO_ENTRY(INIT_HWND)
		PROPERTY_INFO_ENTRY(INIT_LCID)
		PROPERTY_INFO_ENTRY(INIT_LOCATION)
		PROPERTY_INFO_ENTRY(INIT_MODE)
		PROPERTY_INFO_ENTRY(INIT_PROMPT)
		PROPERTY_INFO_ENTRY(INIT_PROVIDERSTRING)
		PROPERTY_INFO_ENTRY(INIT_TIMEOUT)
	END_PROPERTY_SET(DBPROPSET_DBINIT)
	CHAIN_PROPERTY_SET(CSampleProvCommand)
END_PROPSET_MAP()
BEGIN_COM_MAP(CSampleProvSource)
	COM_INTERFACE_ENTRY(IDBCreateSession)
	COM_INTERFACE_ENTRY(IDBInitialize)
	COM_INTERFACE_ENTRY(IDBProperties)
	COM_INTERFACE_ENTRY(IPersist)
	COM_INTERFACE_ENTRY(IInternalConnection)
END_COM_MAP()
public:

  STDMETHOD(Initialize)(void)
	{
    HRESULT hr;
    if (FAILED(hr = IDBInitializeImpl<CSampleProvSource>::Initialize()))
      return hr;

    // Get the database property from the OLE DB properties
    DBPROPIDSET propIDSet;
    DBPROPID    propID = DBPROP_INIT_DATASOURCE;

    propIDSet.rgPropertyIDs   = &propID;
    propIDSet.cPropertyIDs    = 1;
    propIDSet.guidPropertySet = DBPROPSET_DBINIT;

    ULONG nProps;
    DBPROPSET* propSet = 0;

    if (FAILED(hr = GetProperties(1, &propIDSet, &nProps, &propSet)))
      return E_FAIL;

    IPropertySetPtr ipConnProps(CLSID_PropertySet);

    ipConnProps->SetProperty(CComBSTR(OLESTR("DATABASE")), propSet->rgProperties[0].vValue);

    ::VariantClear(&propSet->rgProperties[0].vValue);
    ::CoTaskMemFree(propSet->rgProperties);
    ::CoTaskMemFree(propSet);

    // Create an Access WorkspaceFactory and open the Workspace
    IWorkspaceFactoryPtr ipAccessWSF(CLSID_AccessWorkspaceFactory);
    if (FAILED(hr = ipAccessWSF->Open(ipConnProps, 0, &m_ipWS)))
      return E_FAIL;

    return hr;
	}

  STDMETHOD(Uninitialize)(void)
  {
    m_ipWS = 0;
    return IDBInitializeImpl<CSampleProvSource>::Uninitialize();
  }

	STDMETHOD(CreateSession)(IUnknown *pUnkOuter, REFIID riid, IUnknown **ppDBSession)
  {
    HRESULT hr = IDBCreateSessionImpl<CSampleProvSource, CSampleProvSession>::CreateSession(pUnkOuter,
                        riid, ppDBSession);

    if (FAILED(hr))
      return hr;

    CComPtr<IGetDataSource> ipGDS;

    (*ppDBSession)->QueryInterface(IID_IGetDataSource, (void **)&ipGDS );

    CSampleProvSession *pSess = static_cast<CSampleProvSession *>((IGetDataSource*)ipGDS);
    pSess->m_ipWS = m_ipWS;

    return S_OK;
  }

  IWorkspacePtr   m_ipWS;
};

#endif //__CSampleProvSource_H_
