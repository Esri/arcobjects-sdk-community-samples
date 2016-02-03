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

// Function name	: isDirectory
// Description	    : determine if the specified path is a directory or not
// Return type		: bool (return true if the specified path is a directoy false if not)
bool IsDirectory(LPCTSTR path)
{
	TCHAR absPath[_MAX_PATH];
	::_tfullpath(absPath, path, _MAX_PATH);

	DWORD attr = ::GetFileAttributes(absPath);
	if (attr == 0xFFFFFFFF || !(attr & FILE_ATTRIBUTE_DIRECTORY))
		return false;   // does not exist or is not a directory
	else
		return true;
}

// Function name	: returnFileExtension
void ReturnFileExtension(LPCTSTR path, BSTR * extension)
{

	if (extension == NULL)
		return;

	TCHAR absPath[_MAX_PATH];
	TCHAR ext[_MAX_EXT];

	::_tfullpath(absPath, path, _MAX_PATH);
	::_tsplitpath_s(absPath, NULL, 0, NULL, 0, NULL, 0, ext, _MAX_EXT);
	SysReAllocString(extension, ext);
}

// Function name	: fileTypeExists
// Return type		: bool (true if file exists / false if not)
// Argument         : LPCTSTR dirpath (in format c:\temp)
// Argument         : LPCTSTR filter  (filter *.abc )
bool FileTypeExists(LPCTSTR dirpath, LPCTSTR filter)
{
	TCHAR path[_MAX_PATH];
	::_tcscpy_s(path, _MAX_PATH, dirpath);

	// prepare to construct the filter.
	if (path[_tcslen(path) - 1] != _T('\\'))
	::_tcscat_s(path, _MAX_PATH, TEXT("\\"));

	// append the filter
	::_tcscat_s(path, _MAX_PATH, filter);

	// search
	HANDLE hSearch;
	WIN32_FIND_DATA findData;
	hSearch = ::FindFirstFile(path, &findData);

	if (hSearch == INVALID_HANDLE_VALUE)
		return false;

	::FindClose(hSearch);
	return true;
}


// Function name	: fileExists
// Description	    : Return the existance of a file
// Return type		: bool (TRUE if exists FALSE if not)
bool FileExists(LPCTSTR loc)
{
	return (::GetFileAttributes(loc) != -1);
}

// Function name	: returnFilesystemPath
void ReturnFilesystemPath(BSTR fileLoc, BSTR * path)
{

	USES_CONVERSION;
	
	TCHAR tDrive[_MAX_DRIVE];
	TCHAR tDir[_MAX_DIR]; 

	::_tsplitpath_s( OLE2T(fileLoc), tDrive, _MAX_DRIVE, tDir, _MAX_DIR, NULL, 0, NULL, 0);
 
	CComBSTR outPath(tDrive);
	outPath.Append(tDir);

	::SysReAllocString(path, outPath);

	return;
}

// Function name	: returnFile
void ReturnFile(BSTR loc, BSTR * fileName)
{

	USES_CONVERSION;

	TCHAR tFname[_MAX_FNAME];
	TCHAR tExt[_MAX_EXT];

	::_tsplitpath_s( OLE2T(loc), NULL, 0, NULL, 0, tFname, _MAX_FNAME, tExt, _MAX_EXT);

	CComBSTR name(tFname);
	name.Append(tExt);
	
	::SysReAllocString(fileName, name);

	return;
}

