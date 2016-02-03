// stdafx.h : include file for standard system include files,
//      or project specific include files that are used frequently,
//      but are changed infrequently

#if !defined(AFX_STDAFX_H__E977B41A_8727_4F3D_BF08_8F7B63C036E0__INCLUDED_)
#define AFX_STDAFX_H__E977B41A_8727_4F3D_BF08_8F7B63C036E0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define STRICT
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0500
#endif
#define _ATL_APARTMENT_THREADED
#define WINVER 0x0500

#include <atlbase.h>
//You may derive a class from CComModule and use it if you want to override
//something, but do not change the name of _Module
extern CComModule _Module;


#include <stdlib.h>
#include <math.h>
#include <wchar.h>
#include <tchar.h>
#include <ocidl.h>
#include <map>
#include <vector>
#include <gl\gl.h>
#include <gl\glu.h>

#define ISegment IMSSegment
#include <atlcom.h>
#include <atlwin.h>

using namespace std;

#undef ISegment
#undef GUID
#undef XMLSerializer
#ifndef M_PI
#define M_PI            3.14159265358979323846
#endif

#define RAD2DEG(r) ((double)(r) * (180.0 / M_PI))
#define DEG2RAD(d) ((double)(d) * (M_PI / 180.0))

#pragma warning(push)
#pragma warning(disable : 4146)
#pragma warning(disable : 4192)
#pragma warning(disable : 4278)

//import esriSystem.olb "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriSystem.olb"  raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE", "XMLSerializer")
//import esriSystemUI.olb "libid:4ECCA6E2-B16B-4ACA-BD17-E74CAE4C150A"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriSystemUI.olb"  raw_interfaces_only raw_native_types no_namespace named_guids exclude("IProgressDialog")
//import esriGeometry.olb "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeometry.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esriDisplay.olb "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDisplay.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esriGeoDatabase.olb "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeoDatabase.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esriDataSourcesRaster.olb "libid:8F0541A3-D5BE-4B3F-A8D9-062D5579E19B"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDataSourcesRaster.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esriCarto.olb "libid:45AC68FF-DEFF-4884-B3A9-7D882EDCAEF1"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriCarto.olb"  raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE", "UINT_PTR")
//import esriOutput.olb "libid:7DB92CEC-CB65-420A-8737-FCD0722FD436"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriOutput.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esriGeoprocessing.olb "libid:C031A050-82C6-4F8F-8836-5692631CFFE6"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeoprocessing.olb"  raw_interfaces_only raw_native_types no_namespace named_guids
//import esri3DAnalyst.olb "libid:639FE90A-CC9A-48C6-AC1D-105FE50915B5"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esri3DAnalyst.olb"  raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE")
//import esriGlobeCore.olb "libid:00B329B5-265E-11d6-B2B4-00508BCDDE28"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGlobeCore.olb"  raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE")
//import esriControls.olb "libid:033364CA-47F9-4251-98A5-C88CD8D3C808"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriControls.olb"  raw_interfaces_only raw_native_types no_namespace named_guids

//change this to the appropriate path if necessary
#include "\Program Files (x86)\ArcGIS\DeveloperKit10.4\include\CatIDs\ArcCATIDs.h"
#pragma warning(pop)


//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__E977B41A_8727_4F3D_BF08_8F7B63C036E0__INCLUDED)

