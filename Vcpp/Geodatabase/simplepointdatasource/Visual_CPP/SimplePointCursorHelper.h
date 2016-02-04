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


// SimplePointCursorHelper.h : Declaration of the CSimplePointCursorHelper

#ifndef __SIMPLEPOINTCURSORHELPER_H_
#define __SIMPLEPOINTCURSORHELPER_H_

#include "resource.h"       // main symbols
#include <iostream>
#include <fstream>
using namespace std;

const int c_iMaxRowLen = 80;

/////////////////////////////////////////////////////////////////////////////
// CSimplePointCursorHelper
class ATL_NO_VTABLE CSimplePointCursorHelper : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSimplePointCursorHelper, &CLSID_SimplePointCursorHelper>,
	public ISupportErrorInfo,
	public IPlugInCursorHelper,
	public IPlugInFastQueryValues,
	public ISimplePointCursorHelper
{
public:
	CSimplePointCursorHelper()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SIMPLEPOINTCURSORHELPER)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSimplePointCursorHelper)
	COM_INTERFACE_ENTRY(ISimplePointCursorHelper)
	COM_INTERFACE_ENTRY(IPlugInCursorHelper)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IPlugInFastQueryValues)
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IPlugInCursorHelper
  STDMETHOD(NextRecord)();
  STDMETHOD(IsFinished)(VARIANT_BOOL *finished);
  STDMETHOD(QueryValues)(IRowBuffer *Row, long *OID);
  STDMETHOD(QueryShape)(IGeometry *pGeometry);

// IPlugInFastQueryValues
	STDMETHOD(FastQueryValues)(tagFieldValue * Values);

// ISimplePointCursorHelper
	STDMETHOD(put_FilePath)(BSTR newVal);
	STDMETHOD(put_FieldMap)(VARIANT fieldMap);
	STDMETHOD(put_OID)(long lOID);
	STDMETHOD(putref_QueryEnvelope)(IEnvelope* pEnvelope);
	STDMETHOD(putref_Fields)(IFields* pFields);


// FinalConstruct/FinalRelease prototypes
  HRESULT FinalConstruct();
  void    FinalRelease();

private:
	IEnvelopePtr m_ipQueryEnv; // Envelope, for a cursor with spatial search
	long         m_lOID;       // Object ID for a cursor to fetch a single object
	long         m_lCurLineNum;// Current line number within data file
	CComBSTR     m_sFilePath;  // File spec of file holding data
	VARIANT      m_vFieldMap;  // Field map indicating which attributes to fetch
  long *       m_lFieldMap;  // the contents of the fieldmap safe array;

  char         m_sCurrentRow[c_iMaxRowLen]; // buffer for current row in data file
	IGeometryPtr m_ipWorkPoint;// Point geometry to work with
  ifstream     m_fDataFile;  // File stream for text file
	IFieldsPtr   m_ipFields;   // The fields collection of the dataset, used by FastQueryValues

};

#endif //__SIMPLEPOINTCURSORHELPER_H_
