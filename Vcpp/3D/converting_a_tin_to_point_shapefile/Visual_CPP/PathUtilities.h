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

// PathUtilities.h
//
// This file contains declarations of various utility functions
// dealing with file paths and associated structures and macros.

#ifndef _DEV_CPP_PATH_UTILITIES_H_
#define _DEV_CPP_PATH_UTILITIES_H_

#if defined(ESRI_UNIX) || defined(ESRI_WINDOWS)
#include <ArcSDK.h>
#else
#include <atlbase.h>
#endif

// Versions which return a BSTR
//
// Function to remove the file name from the full path and return the path to the parent directory
HRESULT GetParentDirFromFullPath(const char* inFullPath, BSTR *outFilePath, bool keepBackSlash = false);

// Function to extract the file name from the full path of the file
HRESULT GetFileFromFullPath(const char* inFullPath, BSTR *outFileName);

// Function to append an extension to a file if it's not there already
HRESULT AppendFileExtensionIfNeeded(BSTR inFileName, BSTR inExtensionWithDot, BSTR *outFileName);

// char * versions
//
// Function to remove the file name from the full path and return the path to the parent directory
void GetParentDirFromFullPath(const char* inFullPath, char** outFilePath, bool keepBackSlash);

// Function to extract the file name from the full path of the file
void GetFileFromFullPath(const char* inFullPath, char** outFileName);

// Function to append an extension to a file if it's not there already
void AppendFileExtensionIfNeeded(const char* inFileName, const char *inExtensionWithDot, char** outFileName);

// Function to extract the file name (minus extension) from the full path of the file
void GetFileNoExtensionFromFullPath(const char* inFullPath, char** outFileName);

// Function which checks whether an file path is a directory
bool IsDirectory(const char* FileName);

// Function which checks that a full path
bool FullPathHasFileName (const char *inFullPath);

#endif // _DEV_CPP_PATH_UTILITIES_H_
