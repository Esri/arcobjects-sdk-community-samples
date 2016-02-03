
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
