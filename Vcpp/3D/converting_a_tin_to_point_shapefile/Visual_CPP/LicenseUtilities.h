
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
