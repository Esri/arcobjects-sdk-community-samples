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

#pragma once

#include <stdio.h>
#include <tchar.h>
#include <atlbase.h>
#include <atlstr.h>

// Reference to additional headers for your program
//ArcGISVersion "libid:6FCCEDE0-179D-4D12-B586-58C88D26CA78"
#import "\Program Files (x86)\Common Files\ArcGIS\bin\ArcGISVersion.dll" raw_interfaces_only no_implementation
//esriSystem "libid:5E1F7BC3-67C5-4AEE-8EC6-C4B73AAC42ED"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriSystem.olb" raw_interfaces_only raw_native_types no_namespace named_guids exclude("OLE_COLOR", "OLE_HANDLE", "VARTYPE", "IStream", "ISequentialStream", "_LARGE_INTEGER", "_ULARGE_INTEGER", "tagSTATSTG", "_FILETIME", "IPersist", "IPersistStream", "ISupportErrorInfo", "IErrorInfo", "tagRECT"), rename("min", "EsriSystemmin"), rename("max", "EsriSystemmax"), rename("GetMessage", "EsriSystemGetMessage"), rename("GetJob", "EsriSystemGetJob"), rename("GetObject", "EsriSystemGetObject")
//esriGeoprocessing "libid:C031A050-82C6-4F8F-8836-5692631CFFE6"
#import "\Program Files (x86)\ArcGIS\Desktop10.8\com\esriGeoprocessing.olb" raw_interfaces_only, raw_native_types, no_namespace, named_guids, rename("GetMessage", "EsriGeoprocessingGetMessage"), rename("GetObject", "EsriGeoprocessingGetObject")