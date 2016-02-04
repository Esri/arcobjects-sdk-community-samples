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

#ifndef __LICENSEUTLITIES_ESRICPP_h__
#define __LICENSEUTLITIES_ESRICPP_h__

#include <iostream>

// Initialize the application and check out a license if needed.
bool InitializeApp(esriLicenseExtensionCode license = 
                   (esriLicenseExtensionCode)0);

// Attempt to initialize
bool InitAttemptWithoutExtension(esriLicenseProductCode product);
bool InitAttemptWithExtension(esriLicenseProductCode product,
                              esriLicenseExtensionCode extension);

// Shutdown the application and check in the license if needed.
HRESULT ShutdownApp(esriLicenseExtensionCode license = 
                    (esriLicenseExtensionCode)0);

#endif    // __LICENSEUTLITIES_ESRICPP_h__
