// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
// 


#pragma once

#ifndef STRICT
#define STRICT
#endif

// Modify the following defines if you have to target a platform prior to the ones specified below.
// Refer to MSDN for the latest info on corresponding values for different platforms.
#ifndef WINVER				// Allow use of features specific to Windows XP or later.
#define WINVER 0x0501		// Change this to the appropriate value to target other versions of Windows.
#endif

#ifndef _WIN32_WINNT		// Allow use of features specific to Windows XP or later.                   
#define _WIN32_WINNT 0x0501	// Change this to the appropriate value to target other versions of Windows.
#endif						

#ifndef _WIN32_WINDOWS		// Allow use of features specific to Windows 98 or later.
#define _WIN32_WINDOWS 0x0410 // Change this to the appropriate value to target Windows Me or later.
#endif

#ifndef _WIN32_IE			// Allow use of features specific to IE 6.0 or later.
#define _WIN32_IE 0x0600	// Change this to the appropriate value to target other versions of IE.
#endif

#define _ATL_APARTMENT_THREADED
#define _ATL_NO_AUTOMATIC_NAMESPACE

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit

#include <atlbase.h>
#include <atlcom.h>
#include <atlwin.h>
#include <atltypes.h>
#include <atlctl.h>
#include <atlhost.h>

using namespace ATL;

#pragma warning(push)
#pragma warning(disable : 4192) /* Ignore warnings for types that are duplicated in win32 header files */
#pragma warning(disable : 4146) /* Ignore warnings for use of minus on unsigned types */
#pragma warning(disable : 4278) /* Ignore warnings for use of duplicate macros */

// Be sure to set these paths to the version of the software against which you want to run
////import esriSystem.olb
//#import "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED" named_guids no_namespace raw_interfaces_only no_implementation \
//  exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE")
////import esriSystemUI.olb
//#import "libid:4ECCA6E2-B16B-4ACA-BD17-E74CAE4C150A" named_guids no_namespace raw_interfaces_only no_implementation
////import esriFramework.olb
//#import "libid:866AE5D3-530C-11D2-A2BD-0000F8774FB5" named_guids no_namespace raw_interfaces_only no_implementation  \
//  exclude("UINT_PTR")
////import esriGeometry.olb
//#import "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68" named_guids no_namespace raw_interfaces_only no_implementation \
//  rename("ISegment", "ISegmentESRI")
////import esriGeoDatabase.olb
//#import "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2" raw_interfaces_only, raw_native_types, no_namespace, named_guids
////import esriNetworkAnalyst.olb
//#import "libid:9B4F73F7-90C0-11D5-A6C3-0008C7DF88AB" named_guids no_namespace raw_interfaces_only no_implementation
////import esriDisplay.olb
//#import "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0" raw_interfaces_only, raw_native_types, no_namespace, named_guids
////import esriDataSourcesRaster.olb
//#import "libid:8F0541A3-D5BE-4B3F-A8D9-062D5579E19B" raw_interfaces_only, raw_native_types, no_namespace, no_implementation named_guids
////import esriCarto.olb
//#import "libid:45AC68FF-DEFF-4884-B3A9-7D882EDCAEF1" raw_interfaces_only, raw_native_types, no_namespace, named_guids \
//  exclude("UINT_PTR", "IConvertCacheStorageFormatJob", "IMapCacheExporterJob")

//import esriSystem.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriSystem.olb" named_guids no_namespace raw_interfaces_only no_implementation \
  exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("XMLSerializer","EsriSystemXMLSerializer"), rename("GetObject", "EsriSystemGetObject"), rename("GetJob", "EsriSystemGetJob")
//import esriSystemUI.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriSystemUI.olb" named_guids no_namespace raw_interfaces_only no_implementation
//import esriFramework.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriFramework.olb" named_guids no_namespace raw_interfaces_only no_implementation  \
  exclude("UINT_PTR")
//import esriGeometry.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriGeometry.olb" named_guids no_namespace raw_interfaces_only no_implementation \
  rename("ISegment", "ISegmentESRI")
//import esriGeoDatabase.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriGeoDatabase.olb" raw_interfaces_only, raw_native_types, no_namespace, named_guids
//import esriNetworkAnalyst.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriNetworkAnalyst.olb" named_guids, no_namespace, raw_interfaces_only, no_implementation
//import esriDisplay.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriDisplay.olb" raw_interfaces_only, raw_native_types, no_namespace, named_guids
//import esriDataSourcesRaster.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriDataSourcesRaster.olb" raw_interfaces_only, raw_native_types, no_namespace, no_implementation named_guids
//import esriCarto.olb
#import "\Program Files (x86)\ArcGIS\Desktop10.4\COM\esriCarto.olb" raw_interfaces_only, raw_native_types, no_namespace, named_guids \
  exclude("UINT_PTR", "IConvertCacheStorageFormatJob", "IMapCacheExporterJob")

// This is included below so we can refer to CLSID_, IID_, etc. defined within
// this project.


//#include "_CustomSolver_i.c"

#pragma warning(pop)


