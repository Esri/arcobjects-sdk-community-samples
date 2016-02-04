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



#ifndef _GENERALUTILS_H
#define _GENERALUTILS_H

const CComBSTR g_sExt(L".spt");
const CComBSTR g_sExtUpper(L".SPT");
const CComBSTR g_sShapeFieldName(L"Shape");

// Function name	: isDirectory
// Description	    : determine if the specified path is a directory or not
// Return type		: bool (return true if the specified path is a directoy false if not)
bool IsDirectory(LPCTSTR path);

// Function name	: returnFileExtension
// void returnFileExtension(LPCTSTR path, _TCHAR * extension);
void ReturnFileExtension(LPCTSTR path, BSTR * extension);

// Function name	: fileTypeExists
// Return type		: bool (true if file exists / false if not)
// Argument         : LPCTSTR dirpath (in format c:\temp)
// Argument         : LPCTSTR filter  (filter *.abc )
bool FileTypeExists(LPCTSTR dirpath, LPCTSTR filter);

// Function name	: fileExists
// Description	    : Return the existance of a file
// Return type		: bool (TRUE if exists FALSE if not)
bool FileExists(LPCTSTR filePath);

// Function name	: returnFilesystemPath
// Description	    : return the path part of a file name including the trailing slash
void ReturnFilesystemPath(BSTR fileLoc, BSTR * path);

// Function name	: returnFile
// Description	    : return the file from a provide path 'c:\temp\file.ext becomes' 'file.ext'
void ReturnFile(BSTR loc, BSTR * fileName);

#endif // _GENERALUTILS_H
