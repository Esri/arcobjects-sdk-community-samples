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



// SimplePointCursorHelper.cpp : Implementation of CSimplePointCursorHelper
#include "stdafx.h"
#include "SimplePointVC.h"
#include "SimplePointCursorHelper.h"

/////////////////////////////////////////////////////////////////////////////
// CSimplePointCursorHelper

STDMETHODIMP CSimplePointCursorHelper::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_ISimplePointCursorHelper
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}


HRESULT CSimplePointCursorHelper::FinalConstruct()
{
  HRESULT hr;

	m_lCurLineNum = 0;
  m_lOID = -1;

	hr = m_ipWorkPoint.CreateInstance(CLSID_Point);
  if (FAILED(hr)) return hr;

	ISpatialReferencePtr ipSR;
	hr = ipSR.CreateInstance(CLSID_UnknownCoordinateSystem);
  if (FAILED(hr)) return hr;

	hr = m_ipWorkPoint->putref_SpatialReference(ipSR);
  if (FAILED(hr)) return hr;

  return S_OK;
}

void CSimplePointCursorHelper::FinalRelease()
{
	if (m_fDataFile)
		m_fDataFile.close();

	SafeArrayUnaccessData(m_vFieldMap.parray);  
}


// IPlugInCursorHelper
STDMETHODIMP CSimplePointCursorHelper::NextRecord()
{
	HRESULT hr;
	// We will take the line number in the file to be the OID of the feature,
	// keep track of this in the m_lCurLineNum variable.
	// 
  // E_FAIL is returned if there are no more records
	
	// If we are searching by OID, skip to the correct line
  if (m_lOID != -1)
	{
    for ( ; m_lCurLineNum < m_lOID && (!m_fDataFile.eof()); m_lCurLineNum++ )
		{
      m_fDataFile.getline(m_sCurrentRow, c_iMaxRowLen);
		}
		if (m_lCurLineNum != m_lOID) // i.e. EOF before OID found
		{
			 m_sCurrentRow[0] = '\0';
       return E_FAIL;
		}
		else 
			 return S_OK;
	}

  // Read current row
	if (!m_fDataFile.eof())
	{
	  m_fDataFile.getline(m_sCurrentRow, c_iMaxRowLen);
		m_lCurLineNum++;
		// if its a blank line then we are at the end, so return failure
		if (m_sCurrentRow[0] == '\0')
			return E_FAIL;
	}
	else
	{
	  m_sCurrentRow[0] = '\0';
    return E_FAIL;
	}

	// If we are finding by envelope, check the current record
  //   if its not in the envelope, make a recursive call to move on to the next record
  if (m_ipQueryEnv != NULL)
	{
	  hr = QueryShape(m_ipWorkPoint);
		if (FAILED(hr)) return hr;
    
    IRelationalOperatorPtr ipRelOp = m_ipWorkPoint;
		if (ipRelOp == NULL) return E_FAIL;

		VARIANT_BOOL bWithin;
		hr = ipRelOp->Within(m_ipQueryEnv, &bWithin);
		if (FAILED(hr)) return hr;

		if (!bWithin) // current feature is not within the query envelope
		{
      hr = NextRecord();
			if (FAILED(hr)) return hr;
		}
	}

	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::IsFinished(VARIANT_BOOL *finished)
{
	if (! finished) return E_POINTER;
	
	if ( (m_fDataFile.eof()) && (m_sCurrentRow[0] == '\0') )
		*finished = VARIANT_TRUE;
	else
		*finished = VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::QueryValues(IRowBuffer *Row, long *OID)
{
	HRESULT hr;
	if (! OID || ! Row) return E_POINTER;
	
  // At end of file, return -1
  if (m_sCurrentRow[0] == '\0')
  {
    *OID = -1;
		return S_OK;
	}

	// First, parse the attribute out of the current row.
  // We know this data source has just one attribute, which is one char wide.
  char sAtt[2];
  strncpy_s(sAtt, sizeof(sAtt), m_sCurrentRow + 12, 1);
	sAtt[1] = '\0'; // add null terminator

	CComVariant vAtt = sAtt;
	if (vAtt.vt == VT_ERROR) return E_FAIL;

	// Check field map has same number of elements as there are fields
	IFieldsPtr ipFields;
	hr = Row->get_Fields(&ipFields);
	if (FAILED(hr)) return hr;

	long lNumFields;
	hr = ipFields->get_FieldCount(&lNumFields);
	if (FAILED(hr)) return hr;

  long lUBound, lLBound;
	hr =  SafeArrayGetLBound(m_vFieldMap.parray, 1, &lLBound);
	if (FAILED(hr)) return hr;
	hr =  SafeArrayGetUBound(m_vFieldMap.parray, 1, &lUBound);
	if (FAILED(hr)) return hr;

  if ( (lUBound - lLBound) + 1   != lNumFields)
  {
  	AtlReportError(CLSID_SimplePointCursorHelper, L"SimplePoint Data Source: Unexepected situation: Number of elements in Fieldmap does not match number of fields", IID_IPlugInCursorHelper, E_FAIL);
    return E_FAIL;
	}

	// For each field, copy its value into the row object.
  // (don't copy shape, object ID or where the field map indicates no values required)
  // Note, although we know there is only one attribute in the data source,
  // this loop has been coded generically in case support needs to be added for more attributes
	long i;
	esriFieldType eFieldType;

	for (i=0; i < lNumFields; i++)
  {
		IFieldPtr ipField;
    hr = ipFields->get_Field(i, &ipField);
		if (FAILED(hr)) return hr;

		hr = ipField->get_Type(&eFieldType);
		if (FAILED(hr)) return hr;

		if (eFieldType     != esriFieldTypeGeometry &&
			  eFieldType     != esriFieldTypeOID &&
				m_lFieldMap[i] != -1)
		{
      hr = Row->put_Value(i, vAtt);
			if (FAILED(hr)) return hr;
		}
	}
	// Return value is taken as the OID.
  // Use the line number (stream will currently be pointing at next line)
	*OID = m_lCurLineNum;
	
	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::QueryShape(IGeometry *pGeometry)
{
	HRESULT hr;
	if (! pGeometry) return E_POINTER;

  // If there is no current row, set the geometry to be empty
  if (m_sCurrentRow[0] == '\0')
  {
		hr = pGeometry->SetEmpty();
	  if (FAILED(hr)) return hr;

		return S_OK;
	}

	double x,y;
	char* end;
	char buf[6];

  // Parse the X and Y values out of the current row and into the geometry
	strncpy_s(buf, sizeof(buf), m_sCurrentRow, 6);
	x = strtod(buf,&end);
	strncpy_s(buf, sizeof(buf), m_sCurrentRow + 6, 6);
	y = strtod(buf,&end);

  IPointPtr ipPoint = pGeometry;
	if (ipPoint == NULL) return E_FAIL;

	hr = ipPoint->PutCoords(x,y);
	if (FAILED(hr)) return hr;

  // Note - in our case there is no need to handle the strictSearch test for a cursor
  // created with FetchByEnvelope. We have already tested that the feature is within
  // the envelope on the NextRecord call, so there is no possibility of the test
  // failing here.	

	return S_OK;
}


// IPlugInFastQueryValues
STDMETHODIMP CSimplePointCursorHelper::FastQueryValues(tagFieldValue *Values)
{
	HRESULT hr;
  USES_CONVERSION;
	if (! Values) return E_POINTER;

  // If at end of file, return S_FALSE
  if (m_sCurrentRow[0] == '\0')
		return S_FALSE;

  // First, parse the attribute out of the current row.
  // We know this data source has just one attribute, which is one char wide.
  char sAtt[2];
  strncpy_s(sAtt, sizeof (sAtt), m_sCurrentRow + 12, 1);
	sAtt[1] = '\0'; // add null terminator

  // For this sample, there is just one attribute field to handle,
	// but for demonstration, more generic code follows:
	if (m_ipFields == NULL) return E_FAIL;
  
	IFieldPtr ipField;
	esriFieldType eFieldType;
	long lFieldCount;
	hr = m_ipFields->get_FieldCount(&lFieldCount);
	if (FAILED(hr)) return hr;

	for (long lFieldIndex = 0; lFieldIndex < lFieldCount; lFieldIndex++)
	{
		// if the supplied value in the field map is -1 then this means
		// that we don't have to populate it with data.
		if (m_lFieldMap[lFieldIndex] == -1)
			continue;

		// If the field map indicator is not -1,
		// it is the location in the FieldValue array that we should place our data.
		// For instance field map array is 0,1,2,3,4 with values of 1,0,-1,4,3
		//  the returned field order (that of the Values array) would be:
		//            Field1,Field0,Field2=NULL,Field4,Field3
		long lFieldLoc = m_lFieldMap[lFieldIndex];

		hr = m_ipFields->get_Field(lFieldIndex, &ipField);
		if (FAILED(hr)) return hr;

		hr = ipField->get_Type(&eFieldType);
		if (FAILED(hr)) return hr;

    switch (eFieldType)
    {
      case esriFieldTypeSmallInteger:
				Values[lFieldLoc].m_value.vt   = VT_I2;
				// following code is commented out - it just indicates the kind
				// of code that would go in if there were lots of different attributes in
				// the data source
        // field value = GetFieldValue(lFieldIndex);
        // Values[lngFieldLoc].m_value.iVal = field value;
				break;      
			case esriFieldTypeInteger:
				Values[lFieldLoc].m_value.vt   = VT_I4;
        // field value = GetFieldValue(lFieldIndex);
        // Values[lngFieldLoc].m_value.lVal = field value;
				break;
      case esriFieldTypeSingle:
				Values[lFieldLoc].m_value.vt   = VT_R4;
				// field value = GetFieldValue(lFieldIndex);
        // Values[lngFieldLoc].m_value.fltVal = field value;
				break;
			case esriFieldTypeDouble:
				Values[lFieldLoc].m_value.vt   = VT_R8;
				// field value = GetFieldValue(lFieldIndex);
        // Values[lngFieldLoc].m_value.dblVal = field value;
				break;
			case esriFieldTypeDate:
				Values[lFieldLoc].m_value.vt   = VT_DATE;
				// field value = GetFieldValue(lFieldIndex);
        // Values[lngFieldLoc].m_value.date = some value;
				break;
			case esriFieldTypeString:
				// if the string already exists then just reallocate it otherwise create
        if (Values[lFieldLoc].m_value.bstrVal != NULL)
				{
          INT success = ::SysReAllocString(&(Values[lFieldLoc].m_value.bstrVal), A2COLE(sAtt) );
				  if (!success) return E_FAIL;  
        }
        else
				{
          Values[lFieldLoc].m_value.bstrVal = ::SysAllocString(A2COLE(sAtt));
					if (Values[lFieldLoc].m_value.bstrVal == NULL) return E_FAIL;
        }
				Values[lFieldLoc].m_value.vt = VT_BSTR;
				break;
			case esriFieldTypeGeometry:
				// we should never copy the shape field, as QueryShape deals with that
				Values[lFieldLoc].m_value.vt = VT_NULL;
				Values[lFieldLoc].m_value.punkVal = 0;
				break;
			case esriFieldTypeOID:
				Values[lFieldLoc].m_value.vt   = VT_I4;
        Values[lFieldLoc].m_value.lVal = m_lCurLineNum;
			case esriFieldTypeBlob:
				break;
			}
	}

	return S_OK;
}

// ISimplePointCursorHelper methods
STDMETHODIMP CSimplePointCursorHelper::put_FilePath(BSTR newVal)
{
	HRESULT hr;
	USES_CONVERSION;

	m_sFilePath = newVal;

	// Open the text file for reading
  m_fDataFile.open(OLE2CA(m_sFilePath));
	if (!m_fDataFile)
	{
		CComBSTR sError(L"Could not open data file for reading: ");
		sError.Append(m_sFilePath);
  	AtlReportError(CLSID_SimplePointCursorHelper, sError, IID_IPlugInCursorHelper, E_FAIL);
		return E_FAIL;
	}

	// First record should be fetched on creation
  hr = NextRecord();
	if (FAILED(hr)) return hr;

	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::put_FieldMap(VARIANT fieldMap)
{
	m_vFieldMap = fieldMap;
	
	HRESULT hr = SafeArrayAccessData(m_vFieldMap.parray, (void HUGEP**)&m_lFieldMap);
	if (FAILED(hr)) return hr;

	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::put_OID(long lOID)
{
	m_lOID = lOID;
	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::putref_QueryEnvelope(IEnvelope* pEnvelope)
{
	m_ipQueryEnv = pEnvelope;
	
	return S_OK;
}

STDMETHODIMP CSimplePointCursorHelper::putref_Fields(IFields* pFields)
{
	// This property exists so we can pass in the fields, to prevent FastQueryValues
	// having to fetch them each time. 
	// We know that the fields will stay constant over the lifetime of the cursor.
	m_ipFields = pFields;
	
	return S_OK;
}


