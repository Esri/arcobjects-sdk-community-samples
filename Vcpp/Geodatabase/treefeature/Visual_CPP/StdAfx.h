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
//      or project specific include files that are used frequently,
//      but are changed infrequently

#if !defined(AFX_STDAFX_H__D52B8F34_1B1F_11D6_8A9D_00104BB6FCCB__INCLUDED_)
#define AFX_STDAFX_H__D52B8F34_1B1F_11D6_8A9D_00104BB6FCCB__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define STRICT
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0400
#endif
#define _ATL_APARTMENT_THREADED

#include <atlbase.h>
//You may derive a class from CComModule and use it if you want to override
//something, but do not change the name of _Module
extern CComModule _Module;
#include <atlcom.h>

#include "/Program Files (x86)/ArcGIS/DeveloperKit10.5/include/CATIDs/ArcCATIDs.h"

#pragma warning(push)
#pragma warning(disable : 4146)
#pragma warning(disable : 4192)
//import esriSystem.olb	
#import "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "ICursorPtr", "VARTYPE"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("XMLSerializer","EsriSystemXMLSerializer"), rename("GetObject", "EsriSystemGetObject"), rename("GetJob", "EsriSystemGetJob")
//import esriGeometry.olb
#import "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68" raw_interfaces_only raw_native_types no_namespace named_guids
//import esriGeoDatabase.olb
#import "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2" raw_interfaces_only raw_native_types no_namespace named_guids rename("GetMessage", "esriGeoDatabaseGetMessage")
#pragma warning(pop)


//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__D52B8F34_1B1F_11D6_8A9D_00104BB6FCCB__INCLUDED)
