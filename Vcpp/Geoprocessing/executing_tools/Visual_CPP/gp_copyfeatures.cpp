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

// gp_copyfeatures.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

// gp_copyfeatures.cpp : A sample C++ executable which uses the IGeoprocessor object to perform a number of
//                       Geoprocessing operations. This sample will extract features to a new feature class
//                       based on a Location and an attribute query.


int _tmain(int argc, _TCHAR* argv[])
{
	HRESULT hr;

	// Intialize the COM subsystem
	::CoInitialize(NULL);
	{
		// set VersionManager
		ArcGISVersionLib::IArcGISVersionPtr ipArcGISVersion(__uuidof(ArcGISVersionLib::VersionManager));
		if (0 == ipArcGISVersion)
		{
		   //wcout << L"Failed to CoCreate ArcGISVersion." << endl;
		   return -1;
		}
		// Set ARcGIS version 
		VARIANT_BOOL bSuccess(VARIANT_FALSE);
		CComBSTR version = L"10.2";
		if (FAILED(ipArcGISVersion->LoadVersion(ArcGISVersionLib::esriArcGISDesktop, version, &bSuccess)))
		{
		   //wcout << L"Load version operation failed" << endl;
		   return -2;
		}
		if (VARIANT_FALSE == bSuccess)
		{
		   //wcout << L"Failed to load version.  Success is false." << endl;
		   return -3;
		}
		IAoInitializePtr ipInit(__uuidof(AoInitialize));
		if (0 == ipInit)
		{
		   //wcout << L"Failed to CoCreate AoInitialize." << endl;
		   return -1;
		}

		// Intialize the Geoprocessor COM Object
		IGeoProcessorPtr ipGP(CLSID_GeoProcessor);

		// Set OverwriteOutput option to true
		_variant_t vOverwriteOption(VARIANT_TRUE);
		ipGP->put_OverwriteOutput(vOverwriteOption);

		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// STEP 1: Make a layer using the MakeFeatureLayer tool for the inputs to the SelectByLocation tool.
		///////////////////////////////////////////////////////////////////////////////////////////////////////////
	
		// Build variant of feature class of the contaminated sites 
		_variant_t vContaminatedFC(L"C:\\data\\barisal.gdb\\contamination");
		
		// Build variant of output layer name
		_variant_t vContaminatedLYR(L"contamination_lyr");

		// Build the array of variant paramters
		IVariantArrayPtr ipValues(CLSID_VarArray);
		ipValues->Add(vContaminatedFC);
		ipValues->Add(vContaminatedLYR);

		// Execute MakeFeatureLayer tool by name creating a feature layer of the feature class of the contaminated sites
		IGeoProcessorResultPtr ipResult;  // is of type _com_ptr_t 
		hr = ipGP->Execute(L"MakeFeatureLayer_management", ipValues, 0, &ipResult);
		
		// Generate BSTR for the bsMessages
		BSTR bsMessages;

		// Print the bsMessages
		ipResult->GetMessages(0, &bsMessages);
		wprintf(bsMessages);
		::SysFreeString(bsMessages);

		// If error occurred, exit
		if (FAILED(hr))
		  return 1;

		// Build variant of districts feature class
		_variant_t vDistrictsFC(L"C:\\data\\barisal.gdb\\districts");

		// Build variant of output layer name
		_variant_t vDistrictsLYR(L"districts_layer");

		// Build the array of variant paramters
		ipValues.CreateInstance(CLSID_VarArray);
		ipValues->Add(vDistrictsFC);
		ipValues->Add(vDistrictsLYR);
 
		// Execute MakeFeatureLayer tool by name creating a feature layer of the districts feature class
		ipResult = 0;
		hr = ipGP->Execute(L"MakeFeatureLayer_management", ipValues, 0, &ipResult);
	
		// Print the bsMessages
		ipResult->GetMessages(0, &bsMessages);
		wprintf(bsMessages);
		::SysFreeString(bsMessages);

		// If error occurred, exit
		if (FAILED(hr))
		  return 1;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// STEP 2: Execute SelectByLocation using the layers 
		//         to select all contaminated sites that intersect the districts polygons
		///////////////////////////////////////////////////////////////////////////////////////////////////////////
	
		// Build the array of variant parameters
		ipValues.CreateInstance(CLSID_VarArray);

		// Build variant for overlap type
		_variant_t vOverlapType(L"INTERSECT");

		ipValues->Add(vContaminatedLYR);
		ipValues->Add(vOverlapType);
		ipValues->Add(vDistrictsLYR);

		// Execute SelectByLocation
		ipResult = 0;
		hr = ipGP->Execute(L"SelectLayerByLocation_management", ipValues, 0, &ipResult);

		// Print the bsMessages
		ipResult->GetMessages(0, &bsMessages);
		wprintf(bsMessages);
		::SysFreeString(bsMessages);
	
		// If error occurred, exit
		if (FAILED(hr))
		  return 1;

		////////////////////////////////////////////////////////////////////////////////////////////
		// STEP 3: Execute SelectByAttribute to select all contaminated sites that have contamination level higher than 0.04
		////////////////////////////////////////////////////////////////////////////////////////////

		// Build the array of variant parameters
		ipValues.CreateInstance(CLSID_VarArray);
	
		// Build variant for selection type
		_variant_t vSelectType(L"SUBSET_SELECTION");

		// Build variant for the expression
		_variant_t vExp(L"ARSENIC > .04");

		ipValues->Add(vContaminatedLYR);
		ipValues->Add(vSelectType);
		ipValues->Add(vExp);

		// Execute SelectByAttribute tool
		ipResult = 0;
		hr = ipGP->Execute(L"SelectLayerByAttribute_management", ipValues, 0, &ipResult);
	
		// Print the bsMessages
		ipResult->GetMessages(0, &bsMessages);
		wprintf(bsMessages);
		::SysFreeString(bsMessages);

		// If error occurred, exit
		if (FAILED(hr))
		  return 1;

		//////////////////////////////////////////////////////////////////////////////////////////////////////
		// STEP 4: Execute CopyFeatures tool to create new feature class to persist the output layer
		//         The output will contain all contamination sites with higher contamination level and falls within district boundaries
		//////////////////////////////////////////////////////////////////////////////////////////////////////

		// Build the array of variant parameters
		ipValues.CreateInstance(CLSID_VarArray);

		// Build variant for the output feature class
		_variant_t vOutputContSites(L"C:\\data\\barisal.gdb\\contaminated_areas");

		ipValues->Add(vContaminatedLYR);
		ipValues->Add(vOutputContSites);

		// Execute CopyFeatures tool and get the Geoprocessing Results
		ipResult = 0;
		hr = ipGP->Execute(L"CopyFeatures_management", ipValues, 0, &ipResult);
		
		// Print the bsMessages
		ipResult->GetMessages(0, &bsMessages);
		wprintf(bsMessages);
		::SysFreeString(bsMessages);

		// If error occurred, exit
		if (FAILED(hr))
		  return 1;
	}
	::CoUninitialize();
	return 0;
}

