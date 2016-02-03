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



#include "StdAfx.h"
#include "PathUtilities.h"

// Function to remove the file name from the full path and return the
// path to the directory. Caller is responsible for freeing the memory
// in outFilePath (or pass in a CComBSTR which has been cast to a BSTR
// to let the CComBSTR handle memory management).
HRESULT GetParentDirFromFullPath(const char* inFullPath, BSTR* outFilePath)
{
  if (!inFullPath || !outFilePath) 
    return E_POINTER;

  // Initialize output
  *outFilePath = 0;
  
  const char *pathEnd = strrchr(inFullPath, '/');  // UNIX
  if (pathEnd == 0)
    pathEnd = strrchr(inFullPath, '\\');           // Windows

  if (pathEnd == 0) 
    return E_FAIL;
  
  int size = strlen(inFullPath) - strlen(pathEnd);
  char *tmp = new char[size+1];
  strncpy_s(tmp, size + 1, inFullPath, size);
  *(tmp+size) = '\0';

  CComBSTR bsTmp (tmp);
  delete[] tmp;
  if (!bsTmp) 
    return E_OUTOFMEMORY;
  *outFilePath = bsTmp.Detach();
  
  return S_OK;
}

// Function to extract the file (or directory) name from the full path
// of the file. Caller is responsible for freeing the memory in
// outFileName (or pass in a CComBSTR which has been cast to a BSTR
// to let the CComBSTR handle memory management).
HRESULT GetFileFromFullPath(const char* inFullPath, BSTR *outFileName)
{
  if (!inFullPath || !outFileName)  
    return E_POINTER;

  *outFileName = 0;
  
  const char* name = strrchr(inFullPath, '/');     // UNIX
  if (name == 0) 
    name = strrchr(inFullPath, '\\');              // Windows

  if (name == 0) 
    return E_FAIL;

  name++;
  char* tmp = new char[strlen(name)+1];
  strcpy_s(tmp, strlen(name) + 1, name);

  CComBSTR bsTmp (tmp);
  delete[] tmp;
  if (!bsTmp) 
    return E_OUTOFMEMORY;
  *outFileName = bsTmp.Detach();
  
  return S_OK;
}

