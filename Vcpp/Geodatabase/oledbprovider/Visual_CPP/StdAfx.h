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
// stdafx.h : include file for standard system include files,
//      or project specific include files that are used frequently,
//      but are changed infrequently

#if !defined(AFX_STDAFX_H__C839067D_76B7_44B9_9F93_01FC18D59A11__INCLUDED_)
#define AFX_STDAFX_H__C839067D_76B7_44B9_9F93_01FC18D59A11__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


#define STRICT
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0501
#endif
#define _ATL_APARTMENT_THREADED

#include <afxwin.h>
#include <afxdisp.h>
#include <afxtempl.h>

#include <atlbase.h>
//You may derive a class from CComModule and use it if you want to override
//something, but do not change the name of _Module
extern CComModule _Module;
#include <atlcom.h>
#include <atlctl.h>
#include <atldb.h>

#pragma warning(disable: 4146 4192)
//import esriSystem.olb
#import "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED" named_guids no_namespace raw_interfaces_only no_implementation exclude("OLE_COLOR","OLE_HANDLE", "VARTYPE")
//import esriSystemUI.olb
#import "libid:4ECCA6E2-B16B-4ACA-BD17-E74CAE4C150A" named_guids no_namespace raw_interfaces_only no_implementation \
				rename("ICommand","IESRICommand"), exclude("IProgressDialog")
//import esriGeometry.olb
#import "libid:C4B094C2-FF32-4FA1-ABCB-7820F8D6FB68" named_guids no_namespace raw_interfaces_only no_implementation \
				rename("Parameter","ESRIParameter"), exclude("ISegment") 
//import esriDisplay.olb
#import "libid:59FCCD31-434C-4017-BDEF-DB4B7EDC9CE0" named_guids no_namespace raw_interfaces_only no_implementation
//import esriGeoDatabase.olb
#import "libid:0475BDB1-E5B2-4CA2-9127-B4B1683E70C2" named_guids no_namespace raw_interfaces_only no_implementation \
				rename("Field","ESRIField") rename("Fields","ESRIFields") rename("IRow","IESRIRow") rename("ICursor", "IESRICursor") rename ("IRelationship", "esriIRelationship")
//import esriDataSourcesGDB.olb
#import "libid:4A037613-879A-484D-AF82-0802947C627B" named_guids no_namespace raw_interfaces_only no_implementation
//import esriDataSourcesRaster.olb
#import "libid:8F0541A3-D5BE-4B3F-A8D9-062D5579E19B" named_guids no_namespace raw_interfaces_only no_implementation exclude("OLE_COLOR","OLE_HANDLE")
				
//import esriCarto.olb
#import "libid:45AC68FF-DEFF-4884-B3A9-7D882EDCAEF1" named_guids no_namespace raw_interfaces_only no_implementation \
rename("ITableDefinition","IESRITableDefinition"), exclude("UINT_PTR")

#include "oledbgis.h"

#include "ESRIutil.h"

inline ULONG MapESRIGeomTypeToOGISGeomType( esriGeometryType esriGT )
{
  switch( esriGT )
  {
    default:
      ATLASSERT( 0 );
    break;

	  case esriGeometryPoint:
    return DBGEOM_POINT;

	  case esriGeometryMultipoint:
    return DBGEOM_MULTIPOINT;

	  case esriGeometryPolyline:
    return DBGEOM_MULTILINESTRING;

	  case esriGeometryPolygon:
    return DBGEOM_MULTIPOLYGON;

    case esriGeometryNull:      // FD_1 table returns Null geometry columns
    break;
  }
  return 0;
}

inline esriSpatialRelEnum MapOGISSpatialOpToESRISpatialRel( ULONG spatialOp )
{
  switch( spatialOp )
  {
    default :
    case DBPROP_OGIS_DISJOINT:
    return esriSpatialRelUndefined;

    case DBPROP_OGIS_TOUCHES:
    return esriSpatialRelTouches;

    case DBPROP_OGIS_WITHIN:
    return esriSpatialRelWithin;

    case DBPROP_OGIS_CONTAINS:
    return esriSpatialRelContains;

    case DBPROP_OGIS_CROSSES:
    return esriSpatialRelCrosses;

    case DBPROP_OGIS_OVERLAPS:
    return esriSpatialRelOverlaps;

    case DBPROP_OGIS_INTERSECT:
    return esriSpatialRelIntersects;

    case DBPROP_OGIS_ENVELOPE_INTERSECTS:
    return esriSpatialRelEnvelopeIntersects;

    case DBPROP_OGIS_INDEX_INTERSECTS:
    return esriSpatialRelIndexIntersects;
  }
}

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__C839067D_76B7_44B9_9F93_01FC18D59A11__INCLUDED)
