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


 /* File created by MIDL compiler version 8.00.0603 */
/* at Tue Dec 15 09:08:38 2015
 */
/* Compiler settings for SimplePointVC.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

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


#ifndef __SimplePointVC_h__
#define __SimplePointVC_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISingletonRelease_FWD_DEFINED__
#define __ISingletonRelease_FWD_DEFINED__
typedef interface ISingletonRelease ISingletonRelease;

#endif 	/* __ISingletonRelease_FWD_DEFINED__ */


#ifndef __ISimplePointWorkspaceFactory_FWD_DEFINED__
#define __ISimplePointWorkspaceFactory_FWD_DEFINED__
typedef interface ISimplePointWorkspaceFactory ISimplePointWorkspaceFactory;

#endif 	/* __ISimplePointWorkspaceFactory_FWD_DEFINED__ */


#ifndef __ISimplePointWorkspaceHelper_FWD_DEFINED__
#define __ISimplePointWorkspaceHelper_FWD_DEFINED__
typedef interface ISimplePointWorkspaceHelper ISimplePointWorkspaceHelper;

#endif 	/* __ISimplePointWorkspaceHelper_FWD_DEFINED__ */


#ifndef __ISimplePointDatasetHelper_FWD_DEFINED__
#define __ISimplePointDatasetHelper_FWD_DEFINED__
typedef interface ISimplePointDatasetHelper ISimplePointDatasetHelper;

#endif 	/* __ISimplePointDatasetHelper_FWD_DEFINED__ */


#ifndef __ISimplePointCursorHelper_FWD_DEFINED__
#define __ISimplePointCursorHelper_FWD_DEFINED__
typedef interface ISimplePointCursorHelper ISimplePointCursorHelper;

#endif 	/* __ISimplePointCursorHelper_FWD_DEFINED__ */


#ifndef __SimplePointWorkspaceFactory_FWD_DEFINED__
#define __SimplePointWorkspaceFactory_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimplePointWorkspaceFactory SimplePointWorkspaceFactory;
#else
typedef struct SimplePointWorkspaceFactory SimplePointWorkspaceFactory;
#endif /* __cplusplus */

#endif 	/* __SimplePointWorkspaceFactory_FWD_DEFINED__ */


#ifndef __SimplePointWorkspaceHelper_FWD_DEFINED__
#define __SimplePointWorkspaceHelper_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimplePointWorkspaceHelper SimplePointWorkspaceHelper;
#else
typedef struct SimplePointWorkspaceHelper SimplePointWorkspaceHelper;
#endif /* __cplusplus */

#endif 	/* __SimplePointWorkspaceHelper_FWD_DEFINED__ */


#ifndef __SimplePointDatasetHelper_FWD_DEFINED__
#define __SimplePointDatasetHelper_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimplePointDatasetHelper SimplePointDatasetHelper;
#else
typedef struct SimplePointDatasetHelper SimplePointDatasetHelper;
#endif /* __cplusplus */

#endif 	/* __SimplePointDatasetHelper_FWD_DEFINED__ */


#ifndef __SimplePointCursorHelper_FWD_DEFINED__
#define __SimplePointCursorHelper_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimplePointCursorHelper SimplePointCursorHelper;
#else
typedef struct SimplePointCursorHelper SimplePointCursorHelper;
#endif /* __cplusplus */

#endif 	/* __SimplePointCursorHelper_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __SIMPLEPOINTVCLib_LIBRARY_DEFINED__
#define __SIMPLEPOINTVCLib_LIBRARY_DEFINED__

/* library SIMPLEPOINTVCLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_SIMPLEPOINTVCLib;

#ifndef __ISingletonRelease_INTERFACE_DEFINED__
#define __ISingletonRelease_INTERFACE_DEFINED__

/* interface ISingletonRelease */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISingletonRelease;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DA8C16B8-00C8-490C-8CCE-80202C76CED3")
    ISingletonRelease : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE ReleaseInstance( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISingletonReleaseVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISingletonRelease * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISingletonRelease * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISingletonRelease * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *ReleaseInstance )( 
            ISingletonRelease * This);
        
        END_INTERFACE
    } ISingletonReleaseVtbl;

    interface ISingletonRelease
    {
        CONST_VTBL struct ISingletonReleaseVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISingletonRelease_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISingletonRelease_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISingletonRelease_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISingletonRelease_ReleaseInstance(This)	\
    ( (This)->lpVtbl -> ReleaseInstance(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISingletonRelease_INTERFACE_DEFINED__ */


#ifndef __ISimplePointWorkspaceFactory_INTERFACE_DEFINED__
#define __ISimplePointWorkspaceFactory_INTERFACE_DEFINED__

/* interface ISimplePointWorkspaceFactory */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISimplePointWorkspaceFactory;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("65E5BA1D-721A-11D6-8AD9-00104BB6FCCB")
    ISimplePointWorkspaceFactory : public IUnknown
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct ISimplePointWorkspaceFactoryVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimplePointWorkspaceFactory * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimplePointWorkspaceFactory * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimplePointWorkspaceFactory * This);
        
        END_INTERFACE
    } ISimplePointWorkspaceFactoryVtbl;

    interface ISimplePointWorkspaceFactory
    {
        CONST_VTBL struct ISimplePointWorkspaceFactoryVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimplePointWorkspaceFactory_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimplePointWorkspaceFactory_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimplePointWorkspaceFactory_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimplePointWorkspaceFactory_INTERFACE_DEFINED__ */


#ifndef __ISimplePointWorkspaceHelper_INTERFACE_DEFINED__
#define __ISimplePointWorkspaceHelper_INTERFACE_DEFINED__

/* interface ISimplePointWorkspaceHelper */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISimplePointWorkspaceHelper;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("79D3B763-730F-11D6-8ADA-00104BB6FCCB")
    ISimplePointWorkspaceHelper : public IUnknown
    {
    public:
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_WorkspacePath( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISimplePointWorkspaceHelperVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimplePointWorkspaceHelper * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimplePointWorkspaceHelper * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimplePointWorkspaceHelper * This);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_WorkspacePath )( 
            ISimplePointWorkspaceHelper * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } ISimplePointWorkspaceHelperVtbl;

    interface ISimplePointWorkspaceHelper
    {
        CONST_VTBL struct ISimplePointWorkspaceHelperVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimplePointWorkspaceHelper_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimplePointWorkspaceHelper_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimplePointWorkspaceHelper_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimplePointWorkspaceHelper_put_WorkspacePath(This,newVal)	\
    ( (This)->lpVtbl -> put_WorkspacePath(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimplePointWorkspaceHelper_INTERFACE_DEFINED__ */


#ifndef __ISimplePointDatasetHelper_INTERFACE_DEFINED__
#define __ISimplePointDatasetHelper_INTERFACE_DEFINED__

/* interface ISimplePointDatasetHelper */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISimplePointDatasetHelper;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("79D3B765-730F-11D6-8ADA-00104BB6FCCB")
    ISimplePointDatasetHelper : public IUnknown
    {
    public:
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_DatasetName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_WorkspacePath( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISimplePointDatasetHelperVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimplePointDatasetHelper * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimplePointDatasetHelper * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimplePointDatasetHelper * This);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_DatasetName )( 
            ISimplePointDatasetHelper * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_WorkspacePath )( 
            ISimplePointDatasetHelper * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } ISimplePointDatasetHelperVtbl;

    interface ISimplePointDatasetHelper
    {
        CONST_VTBL struct ISimplePointDatasetHelperVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimplePointDatasetHelper_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimplePointDatasetHelper_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimplePointDatasetHelper_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimplePointDatasetHelper_put_DatasetName(This,newVal)	\
    ( (This)->lpVtbl -> put_DatasetName(This,newVal) ) 

#define ISimplePointDatasetHelper_put_WorkspacePath(This,newVal)	\
    ( (This)->lpVtbl -> put_WorkspacePath(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimplePointDatasetHelper_INTERFACE_DEFINED__ */


#ifndef __ISimplePointCursorHelper_INTERFACE_DEFINED__
#define __ISimplePointCursorHelper_INTERFACE_DEFINED__

/* interface ISimplePointCursorHelper */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISimplePointCursorHelper;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A049B833-73D7-11D6-8ADB-00104BB6FCCB")
    ISimplePointCursorHelper : public IUnknown
    {
    public:
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_FilePath( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_FieldMap( 
            /* [in] */ VARIANT fieldmap) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_OID( 
            /* [in] */ long lOID) = 0;
        
        virtual /* [helpstring][propputref] */ HRESULT STDMETHODCALLTYPE putref_QueryEnvelope( 
            /* [in] */ /* external definition not present */ IEnvelope *pEnvelope) = 0;
        
        virtual /* [helpstring][propputref] */ HRESULT STDMETHODCALLTYPE putref_Fields( 
            /* [in] */ /* external definition not present */ IFields *pFields) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISimplePointCursorHelperVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimplePointCursorHelper * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimplePointCursorHelper * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimplePointCursorHelper * This);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_FilePath )( 
            ISimplePointCursorHelper * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_FieldMap )( 
            ISimplePointCursorHelper * This,
            /* [in] */ VARIANT fieldmap);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_OID )( 
            ISimplePointCursorHelper * This,
            /* [in] */ long lOID);
        
        /* [helpstring][propputref] */ HRESULT ( STDMETHODCALLTYPE *putref_QueryEnvelope )( 
            ISimplePointCursorHelper * This,
            /* [in] */ /* external definition not present */ IEnvelope *pEnvelope);
        
        /* [helpstring][propputref] */ HRESULT ( STDMETHODCALLTYPE *putref_Fields )( 
            ISimplePointCursorHelper * This,
            /* [in] */ /* external definition not present */ IFields *pFields);
        
        END_INTERFACE
    } ISimplePointCursorHelperVtbl;

    interface ISimplePointCursorHelper
    {
        CONST_VTBL struct ISimplePointCursorHelperVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimplePointCursorHelper_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimplePointCursorHelper_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimplePointCursorHelper_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimplePointCursorHelper_put_FilePath(This,newVal)	\
    ( (This)->lpVtbl -> put_FilePath(This,newVal) ) 

#define ISimplePointCursorHelper_put_FieldMap(This,fieldmap)	\
    ( (This)->lpVtbl -> put_FieldMap(This,fieldmap) ) 

#define ISimplePointCursorHelper_put_OID(This,lOID)	\
    ( (This)->lpVtbl -> put_OID(This,lOID) ) 

#define ISimplePointCursorHelper_putref_QueryEnvelope(This,pEnvelope)	\
    ( (This)->lpVtbl -> putref_QueryEnvelope(This,pEnvelope) ) 

#define ISimplePointCursorHelper_putref_Fields(This,pFields)	\
    ( (This)->lpVtbl -> putref_Fields(This,pFields) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimplePointCursorHelper_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_SimplePointWorkspaceFactory;

#ifdef __cplusplus

class DECLSPEC_UUID("65E5BA1E-721A-11D6-8AD9-00104BB6FCCB")
SimplePointWorkspaceFactory;
#endif

EXTERN_C const CLSID CLSID_SimplePointWorkspaceHelper;

#ifdef __cplusplus

class DECLSPEC_UUID("79D3B764-730F-11D6-8ADA-00104BB6FCCB")
SimplePointWorkspaceHelper;
#endif

EXTERN_C const CLSID CLSID_SimplePointDatasetHelper;

#ifdef __cplusplus

class DECLSPEC_UUID("79D3B766-730F-11D6-8ADA-00104BB6FCCB")
SimplePointDatasetHelper;
#endif

EXTERN_C const CLSID CLSID_SimplePointCursorHelper;

#ifdef __cplusplus

class DECLSPEC_UUID("A049B834-73D7-11D6-8ADB-00104BB6FCCB")
SimplePointCursorHelper;
#endif
#endif /* __SIMPLEPOINTVCLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


