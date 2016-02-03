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


#ifndef __PATHUTILITIES_ESRISCENARIO_H__
#define __PATHUTILITIES_ESRISCENARIO_H__

// Extract the shape file name from the full path of the file
HRESULT GetFileFromFullPath(const char* inFullPath, BSTR* outFileName);

// Remove the file name from the full path and return the directory
HRESULT GetParentDirFromFullPath(const char* inFullPath, BSTR* outFilePath);

#endif  // __PATHUTILITIES_ESRISCENARIO_H__
 
