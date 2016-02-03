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


#ifndef __RASTERSLOPE_ESRISCENARIO_h__
#define __RASTERSLOPE_ESRISCENARIO_h__

#include <iostream>
#include "PathUtilities.h"

bool InitializeWithExtension(esriLicenseProductCode product,
                             esriLicenseExtensionCode extension);
void ShutdownApp(esriLicenseExtensionCode license);
HRESULT CalculateSlope(BSTR inPath, BSTR inName, BSTR outFile);

#endif    // __RASTERSLOPE_ESRISCENARIO_h__
