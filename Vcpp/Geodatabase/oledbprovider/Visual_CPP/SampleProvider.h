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


/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Thu Apr 01 15:13:29 2010
 */
/* Compiler settings for .\SampleProvider.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __SampleProvider_h__
#define __SampleProvider_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __SampleProv_FWD_DEFINED__
#define __SampleProv_FWD_DEFINED__

#ifdef __cplusplus
typedef class SampleProv SampleProv;
#else
typedef struct SampleProv SampleProv;
#endif /* __cplusplus */

#endif 	/* __SampleProv_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __SAMPLEPROVIDERLib_LIBRARY_DEFINED__
#define __SAMPLEPROVIDERLib_LIBRARY_DEFINED__

/* library SAMPLEPROVIDERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_SAMPLEPROVIDERLib;

EXTERN_C const CLSID CLSID_SampleProv;

#ifdef __cplusplus

class DECLSPEC_UUID("4FE4A56C-386B-4701-A021-11BA959E2EC1")
SampleProv;
#endif
#endif /* __SAMPLEPROVIDERLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


