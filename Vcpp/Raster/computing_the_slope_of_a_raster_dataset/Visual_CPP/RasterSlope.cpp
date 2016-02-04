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

#include "StdAfx.h"
#include "RasterSlope.h"

using namespace std;

VARIANT_BOOL InitializeLicense(esriLicenseProductCode product);

int main(int argc, char* argv[])
{
	CoInitialize(0);

  if (argc != 3)
  {
    std::cerr << "Usage: RasterSlope [sourceFile] [outputFile]" << std::endl; 
    return 0;
  }

  char* source = argv[1];
  char* result = argv[2];

  // Parse path
  CComBSTR sourceFilePath;
  CComBSTR sourceFileName;
  HRESULT hr = GetParentDirFromFullPath(source, &sourceFilePath);
  if (FAILED(hr) || sourceFilePath.Length() <= 0)
  {
    std::cerr << "Couldn't parse source file path." << std::endl;
    return 0;
  }
  hr = GetFileFromFullPath(source, &sourceFileName);
  if (FAILED(hr) || sourceFileName.Length() <= 0)
  {
    std::cerr << "Couldn't parse source file name." << std::endl;
    return 0;
  }

  if (!InitializeWithExtension(esriLicenseProductCodeEngine, esriLicenseExtensionCodeSpatialAnalyst))
    if (!InitializeWithExtension(esriLicenseProductCodeBasic, esriLicenseExtensionCodeSpatialAnalyst))
      if (!InitializeWithExtension(esriLicenseProductCodeStandard, esriLicenseExtensionCodeSpatialAnalyst))
        if (!InitializeWithExtension(esriLicenseProductCodeAdvanced, esriLicenseExtensionCodeSpatialAnalyst))
        {
          std::cerr << "Exiting Application: Engine Initialization failed" << std::endl;	
          ShutdownApp(esriLicenseExtensionCodeSpatialAnalyst);
          return 0;
        }
	
  hr = CalculateSlope(sourceFilePath, sourceFileName, CComBSTR(result));
  if (FAILED(hr))	
    std::cerr << "The slope calculation failed." << std::endl;
  else
    std::wcerr << L"The slope of " << (BSTR) sourceFileName << L" has been calculated." << std::endl;

  ShutdownApp(esriLicenseExtensionCodeSpatialAnalyst);

	CoUninitialize();
  return 0;
}

VARIANT_BOOL InitializeLicense(esriLicenseProductCode product)
{
	ESRI_SET_VERSION(esriArcGISEngine);

  IAoInitializePtr ipInit(CLSID_AoInitialize);
  esriLicenseStatus licenseStatus = esriLicenseFailure;
  
	ipInit->IsProductCodeAvailable(product, &licenseStatus);
  
	if (licenseStatus == esriLicenseAvailable)
    ipInit->Initialize(product, &licenseStatus);

  return (licenseStatus == esriLicenseCheckedOut);
}

bool InitializeWithExtension(esriLicenseProductCode product,
                             esriLicenseExtensionCode extension)
{
	ESRI_SET_VERSION(esriArcGISEngine);

  IAoInitializePtr ipInit(CLSID_AoInitialize);
  esriLicenseStatus licenseStatus = esriLicenseFailure;
  ipInit->IsExtensionCodeAvailable(product, extension, &licenseStatus);
  if (licenseStatus == esriLicenseAvailable)
  {
    ipInit->Initialize(product, &licenseStatus);
    if (licenseStatus == esriLicenseCheckedOut)
      ipInit->CheckOutExtension(extension, &licenseStatus);
  }

  return (licenseStatus == esriLicenseCheckedOut);
}

void ShutdownApp(esriLicenseExtensionCode license)
{
  // Scope ipInit so released before AoUninitialize call
  {
    IAoInitializePtr ipInit(CLSID_AoInitialize);
    esriLicenseStatus status;
    ipInit->CheckInExtension(license, &status);
    ipInit->Shutdown(); 
  }
}

HRESULT CalculateSlope(BSTR inPath, BSTR inName, BSTR outFile)
{
  // Open the workspace
  IWorkspaceFactoryPtr ipWorkspaceFactory(CLSID_RasterWorkspaceFactory);
  IWorkspacePtr ipWorkspace;
  HRESULT hr = ipWorkspaceFactory->OpenFromFile(inPath, 0, &ipWorkspace); 
  if (FAILED(hr) || ipWorkspace == 0)
  {
    std::cerr << "Could not open the workspace factory." << std::endl;
    return E_FAIL;   
  }

  // Open the raster dataset
  IRasterWorkspacePtr ipRastWork(ipWorkspace);
  IRasterDatasetPtr ipRastDataset;
  hr = ipRastWork->OpenRasterDataset(inName, &ipRastDataset);
  if (FAILED(hr) || ipRastDataset == 0)
  {
    std::cerr << "Could not open the raster dataset." << std::endl;
    return E_FAIL;
  }

  // Check for existence of a dataset with the desired output name.
  IRasterDatasetPtr ipExistsCheck;
  hr = ipRastWork->OpenRasterDataset(outFile, &ipExistsCheck);
  if (SUCCEEDED(hr))  
  {
    std::cerr << "A dataset with the output name already exists!" << std::endl;
    return E_FAIL;
  }

  // Set up the ISurfaceOp interface to calculate slope
  IRasterAnalysisEnvironmentPtr ipRastAnalEnv(CLSID_RasterSurfaceOp);
  ipRastAnalEnv->putref_OutWorkspace(ipWorkspace);
  ISurfaceOpPtr ipSurfOp(ipRastAnalEnv);

  IGeoDatasetPtr ipGeoDataIn(ipRastDataset);
  IGeoDatasetPtr ipGeoDataOut;
  HRESULT slopeHR = ipSurfOp->Slope(ipGeoDataIn, esriGeoAnalysisSlopeDegrees, 0, &ipGeoDataOut);
  if (FAILED(slopeHR) || ipGeoDataOut == 0)
  {
    std::cerr << "slopeHR = " << slopeHR << std::endl;
    return slopeHR;
  }	

  // Persist the result	
  IRasterBandCollectionPtr ipRastBandColl(ipGeoDataOut);
  IDatasetPtr ipOutDataset;
  ipRastBandColl->SaveAs(outFile, ipWorkspace, CComBSTR(L"GRID"), &ipOutDataset);

  return slopeHR;
}

