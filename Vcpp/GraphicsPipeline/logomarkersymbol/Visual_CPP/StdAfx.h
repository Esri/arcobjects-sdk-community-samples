// Copyright 2015 ESRI
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



// stdafx.h : include file for standard system include files,
//      or project specific include files that are used frequently,
//      but are changed infrequently

#if !defined(AFX_STDAFX_H__D7F5BF2B_852A_11D5_A161_00508BA08E68__INCLUDED_)
#define AFX_STDAFX_H__D7F5BF2B_852A_11D5_A161_00508BA08E68__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define STRICT
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0501
#endif
#define _ATL_APARTMENT_THREADED

#include <atlbase.h>
//You may derive a class from CComModule and use it if you want to override
//something, but do not change the name of _Module
extern CComModule _Module;
#include <atlcom.h>


#pragma warning(push)
#pragma warning(disable : 4192)
#pragma warning(disable : 4146)
#pragma warning(disable : 4099)
//import esriSystem.olb	"libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriSystem.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("XMLSerializer","EsriSystemXMLSerializer"), rename("GetObject", "EsriSystemGetObject"), rename("GetJob", "EsriSystemGetJob")
//import esriGeometry.olb "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriGeometry.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("ISegment", "ISegmentSample")
//import esriDisplay.olb "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDisplay.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("CMYK", "EsriDisplayCMYK"), rename("DrawText", "EsriDisplayDrawText")
//import esriFramework.olb "libid:866AE5D3-530C-11D2-A2BD-0000F8774FB5"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriFramework.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE", "UINT_PTR")
//import esriDisplayUI.olb "libid:016DF9D3-7E81-11D2-A2D1-0000F8774FB5"
#import "\Program Files (x86)\ArcGIS\Desktop10.4\com\esriDisplayUI.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE")
#pragma warning(pop)
#include "\Program Files (x86)\ArcGIS\DeveloperKit10.4\Include\CATIDS\ArcCATIDs.h"
#include <atlctl.h>
#include <richedit.h>
#include <commctrl.h>

#undef _ATL_MIN_CRT
//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__D7F5BF2B_852A_11D5_A161_00508BA08E68__INCLUDED)
