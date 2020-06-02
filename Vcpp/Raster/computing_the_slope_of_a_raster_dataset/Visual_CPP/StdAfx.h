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

// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__1B56DE64_CC81_44E3_A998_897E7FB6EC51__INCLUDED_)
#define AFX_STDAFX_H__1B56DE64_CC81_44E3_A998_897E7FB6EC51__INCLUDED_

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

//import esriSystem.olb "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriSystem.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("XMLSerializer","EsriSystemXMLSerializer"), rename("GetObject", "EsriSystemGetObject"), rename("GetJob", "EsriSystemGetJob")
//import esriSystemUI.olb "libid:4ECCA6E2-B16B-4ACA-BD17-E74CAE4C150A"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriSystemUI.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_HANDLE", "OLE_COLOR")
//import esriGeometry.olb "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriGeometry.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_HANDLE", "OLE_COLOR")
//import esriDisplay.olb "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriDisplay.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_HANDLE", "OLE_COLOR"), rename("CMYK", "EsriDisplayCMYK"), rename("DrawText", "EsriDisplayDrawText"), rename ("RGB","EsriRGB"), rename("ResetDC", "EsriResetDC")
//import esriGeoDatabase.olb "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriGeoDatabase.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE"), rename("GetMessage", "esriGeoDatabaseGetMessage")
//import esriGeoAnalyst.olb "libid:5C54042B-B2ED-4889-8C40-2D89C19DB41D"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriGeoAnalyst.olb" raw_interfaces_only raw_native_types no_namespace named_guids
//import esriDataSourcesRaster.olb "libid:8F0541A3-D5BE-4B3F-A8D9-062D5579E19B"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriDataSourcesRaster.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE"), rename("StartService", "esriDataSourcesRasterStartService")

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

#pragma warning(pop)

// TODO: reference additional headers your program requires here

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__1B56DE64_CC81_44E3_A998_897E7FB6EC51__INCLUDED_)
