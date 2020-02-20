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
// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 


// CustomSolver.cpp : Implementation of DLL Exports.

#include "stdafx.h"
#include "resource.h"

[importlib("\Program Files (x86)\ArcGIS\Desktop10.7\com\esrinetworkanalyst.olb")];

// The module attribute causes DllMain, DllRegisterServer and DllUnregisterServer to be automatically implemented for you
[ module(dll, uuid = "{F25947D1-9C81-48A6-9BFF-CF9EB158FFD7}", 
         name = "CustomSolver", 
         helpstring = "CustomSolver 1.0 Type Library",
         resource_name = "IDR_CUSTOMSOLVER") ]
class CustomSolverModule
{
public:
  // Override CAtlDllModuleT members
};


