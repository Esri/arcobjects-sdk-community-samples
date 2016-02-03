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

[importlib("\Program Files (x86)\ArcGIS\Desktop10.4\com\esrinetworkanalyst.olb")];

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


