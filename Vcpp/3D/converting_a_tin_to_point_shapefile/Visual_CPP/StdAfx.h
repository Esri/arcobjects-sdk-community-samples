// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__131BC9F3_F0EF_4F19_AFB8_B4DBE21EF166__INCLUDED_)
#define AFX_STDAFX_H__131BC9F3_F0EF_4F19_AFB8_B4DBE21EF166__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers

#include <iostream>
#include <atlbase.h>
using namespace std;

#pragma warning(push)
#pragma warning(disable : 4146)
#pragma warning(disable : 4192) 
//import esriSystem.olb	"libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriSystem.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("XMLSerializer","EsriSystemXMLSerializer"), rename("GetObject", "EsriSystemGetObject"), rename("GetJob", "EsriSystemGetJob")
//import esriSystemUI.olb "libid:4ECCA6E2-B16B-4ACA-BD17-E74CAE4C150A"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriSystemUI.olb" raw_interfaces_only raw_native_types no_namespace named_guids
//import esriGeometry.olb "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeometry.olb" raw_interfaces_only raw_native_types no_namespace named_guids
//import esriDisplay.olb "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDisplay.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("CMYK", "EsriDisplayCMYK"), rename("DrawText", "EsriDisplayDrawText"), rename("RGB","EsriRGB"), rename("ResetDC","EsriResetDC")
//import esriGeoDatabase.olb "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeoDatabase.olb" raw_interfaces_only raw_native_types no_namespace named_guids  rename("GetMessage","esriGeoDatabaseGetMessage")
//import esriGeoAnalyst.olb "libid:5C54042B-B2ED-4889-8C40-2D89C19DB41D"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeoAnalyst.olb" raw_interfaces_only raw_native_types no_namespace named_guids
//import esriDataSourcesFile.olb "libid:1CE6AC65-43F5-4529-8FC0-D7ED298E4F1A"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDataSourcesFile.olb" raw_interfaces_only raw_native_types no_namespace named_guids
#pragma warning(pop)

/* Engine Version support "libid:6FCCEDE0-179D-4D12-B586-58C88D26CA78" */
#import "\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISVersion.dll" no_namespace raw_interfaces_only no_implementation rename("esriProductCode", "esriVersionProductCode")
#define ESRI_SET_VERSION(prod) \
{\
  HRESULT hr; \
  VARIANT_BOOL vb; \
  IArcGISVersionPtr ipVersion(__uuidof(VersionManager)); \
  if(!SUCCEEDED(hr = ipVersion->LoadVersion(prod, L"", &vb))) \
    fprintf(stderr, "LoadVersion() failed with code 0x%.8x\n", hr); \
  else if(vb != VARIANT_TRUE) \
    fprintf(stderr, "LoadVersion() failed\n"); \
}

// TODO: reference additional headers your program requires here

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__131BC9F3_F0EF_4F19_AFB8_B4DBE21EF166__INCLUDED_)
