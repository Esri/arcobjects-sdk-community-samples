

// PathUtilities.cpp
//
// Implementation for various functions which deal with files/file paths

#include "StdAfx.h"
#include <sys/stat.h> // stat
#include <string>
using std::wstring;
#include "PathUtilities.h"

// Function to remove the file name from the full path and return the
// path to the directory.  Caller is responsible for freeing the memory
// in outFilePath (or pass in a CComBSTR which has been cast to a BSTR
// to let the CComBSTR handle memory management).
HRESULT GetParentDirFromFullPath(const char* inFullPath, BSTR *outFilePath, bool keepBackSlash /*  = false */)
{
  if (!inFullPath || !outFilePath) return E_POINTER;

  // Initialize output
  *outFilePath = 0;
  
  const char *pathEnd = strrchr(inFullPath, '/'); //UNIX
  if (pathEnd == 0)
    pathEnd = strrchr(inFullPath, '\\'); //Windows

  if (pathEnd == 0) return E_FAIL;
  
  if (keepBackSlash) ++pathEnd;
  int size = strlen(inFullPath) - strlen(pathEnd);
  char *tmp = new char[size+1];
  strncpy_s(tmp, size + 1, inFullPath, size);
  *(tmp+size) = '\0';

  CComBSTR bsTmp (tmp);
  delete [] tmp;
  if (!bsTmp) return E_OUTOFMEMORY;
  *outFilePath = bsTmp.Detach();
  
  return S_OK;
}

// Function to extract the file (or directory) name from the full path
// of the file.  Caller is responsible for freeing the memory in
// outFileName (or pass in a CComBSTR which has been cast to a BSTR
// to let the CComBSTR handle memory management).
HRESULT GetFileFromFullPath(const char* inFullPath, BSTR *outFileName)
{
  if (!inFullPath || !outFileName) return E_POINTER;

  *outFileName = 0;
  
  const char* name = strrchr(inFullPath, '/'); //UNIX
  if (name == 0)
    name = strrchr(inFullPath, '\\'); //Windows

  if (name == 0) return E_FAIL;

  name++;
  char* tmp = new char[strlen(name)+1];
  strcpy_s(tmp, strlen(name) + 1, name);

  CComBSTR bsTmp (tmp);
  delete [] tmp;
  if (!bsTmp) return E_OUTOFMEMORY;
  *outFileName = bsTmp.Detach();
  
  return S_OK;
}

// Function to remove the file name from the fullpath and return the
// directory.  Caller is responsible for freeing the memory in
// outFilePath.
void GetParentDirFromFullPath(const char* inFullPath, char** outFilePath, bool keepBackSlash)
{
  *outFilePath = 0;

  const char *pathEnd = strrchr(inFullPath, '/'); //UNIX
  if (pathEnd == 0)
    pathEnd = strrchr(inFullPath, '\\'); //Windows
  if (pathEnd == 0)
  {
    cerr << "GetDirFromFullPath couldn't find dir in " << inFullPath << endl;
    return;
  }
  if (keepBackSlash) ++pathEnd;
  int size = strlen(inFullPath) - strlen(pathEnd);
  char *tmp = new char[size+1];
  strncpy_s(tmp, size + 1, inFullPath, size);
  *(tmp+size) = '\0';
  *outFilePath = tmp;

  return;
}

// Function to extract the file (or directory) name from the fullpath
// of the file.  Caller is responsible for freeing the memory in
// outFileName.
void GetFileFromFullPath(const char* inFullPath, char** outFileName)
{
  *outFileName = 0;

  const char* name = strrchr(inFullPath, '/'); //UNIX
  if (name == 0)
    name = strrchr(inFullPath, '\\'); //Windows
  if (name == 0)
  {
    cerr << "GetFileFromFullPath couldn't find file in " << inFullPath << endl;
    return;
  }
  name++;
  char* tmp = new char[strlen(name)+1];
  strcpy_s(tmp, strlen(name) + 1, name);
  *outFileName = tmp;

  return;
}

// Function to extract the file name (without the file extension, if
// any exists) from the full path of the file.  Caller is responsible
// for freeing the memory in outFileName.
void GetFileNoExtensionFromFullPath(const char* inFullPath, char** outFileName)
{
  *outFileName = 0;

  char* tmp; // local pointer to returned string

  const char* name = strrchr(inFullPath, '/'); //UNIX
  if (name == 0)
    name = strrchr(inFullPath, '\\'); //Windows
  if (name == 0)
  {
    cerr << "GetFileNoExtensionFromFullPath couldn't find file in " << inFullPath << endl;
    return;
  }
  name++;

  const char* extension = strrchr(name, '.');
  if (extension == 0)
  {
    // file must not have an extension, just return file
    tmp = new char[strlen(name)+1];
	strcpy_s(tmp, strlen(name) + 1, name);
    *outFileName = tmp;
    return;
  }

  int charsToCopy = strlen(name) - strlen(extension);
  tmp = new char[charsToCopy + 1];
  strncpy_s(tmp, charsToCopy + 1, name, charsToCopy);
  tmp[charsToCopy] = '\0';
  *outFileName = tmp;

  return;
}

// Function to append an extension to a file if it's not there already
//
// Example usage:
//
// CComBSTR bsOut;
// CComBSTR bsIn(L"filename.txt");
// HRESULT hr = AppendFileExtensionIfNeeded(bsIn1, CComBSTR(L".mxd"), &bsOut1);
// if (SUCCEEDED(hr)) && bsOut1.Length() != 0)
// ...

HRESULT AppendFileExtensionIfNeeded(BSTR inFileName, BSTR inExtensionWithDot, BSTR *outFileName)
{
  if (!inFileName || !inExtensionWithDot || !outFileName) return E_POINTER;

  *outFileName = 0;

  wstring wsFileName (inFileName);
  wstring wsRetVal;

  int pos = wsFileName.rfind(L".");
  if (pos != wstring::npos)
    wsRetVal = wsFileName.substr(0, pos);    
  else
    wsRetVal = wsFileName;

  wsRetVal.append(inExtensionWithDot);
  CComBSTR bsTmp (wsRetVal.c_str());
  if (!bsTmp) return E_OUTOFMEMORY;
  *outFileName = bsTmp.Detach();

  return S_OK;
}

// Function to append an extension to a file if it's not there already
//
// Example usage:
//
// char *fullName = 0;
// const char *file = "FileName.txt";
// const char *ext = ".mxd";
// AppendFileExtensionIfNeeded (file, ext, &fullName);
// if (fullName)
// { ...
void AppendFileExtensionIfNeeded(const char* inFileName, const char *inExtensionWithDot, char** outFileName)
{
  *outFileName = 0;

  if ( (!inFileName) || (!inExtensionWithDot) || strlen(inFileName) == 0 || strlen(inExtensionWithDot) == 0 ) 
  {
    cerr << "AppendFileExtensionIfNeeded: NULL or empty parameter passed, returning\n";
    return;
  }

  char *tmp; // local pointer to returned string

  const char *extension = strrchr(inFileName, '.');
  if (!extension)
  {
    // inFileName must not have an extension, just append the provided extension
	  int iTempLen = strlen(inFileName) + strlen(inExtensionWithDot) + 1;
	  tmp = new char[iTempLen];
	  strcpy_s(tmp, iTempLen, inFileName);
	  strcat_s(tmp, iTempLen, inExtensionWithDot);
    *outFileName = tmp;
    return;
  }

  // inFileName has an extension, replace it w/ the provided one
  int charsToCopy = strlen(inFileName) - strlen(extension);
  int iTmpLen = charsToCopy + strlen(inExtensionWithDot) + 1;
  tmp = new char[iTmpLen];
  strncpy_s(tmp, iTmpLen, inFileName, charsToCopy);
  tmp[charsToCopy] = '\0';
  strcat_s(tmp, iTmpLen, inExtensionWithDot);
  *outFileName = tmp;

  return;
}

// Function which checks whether an file path is a directory.  Should
// be portable b/w Windows and UNIX.
bool IsDirectory(const char* inFullPath)
{
  struct stat dirstat;
  if (stat(inFullPath, &dirstat) == 0)
  {
    if ((dirstat.st_mode & S_IFDIR) == S_IFDIR)
      return true;
  }
  return false;
}

