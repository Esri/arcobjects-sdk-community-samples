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

#ifndef __TIN2POINT_SAMPLE_H__
#define __TIN2POINT_SAMPLE_H__

#include <iostream>
#include "LicenseUtilities.h"
#include "PathUtilities.h"
#include <math.h>


HRESULT ConvertTin2Point(char* tinFullPath, BSTR shapePath, BSTR shapeFile);
HRESULT CreateBasicFields(esriGeometryType shapeType, 
                          VARIANT_BOOL hasM, 
                          VARIANT_BOOL hasZ, 
                          ISpatialReference* pSpatialRef, 
                          IFields** ppFields);

#endif   // __TIN2POINT_SAMPLE_H__
