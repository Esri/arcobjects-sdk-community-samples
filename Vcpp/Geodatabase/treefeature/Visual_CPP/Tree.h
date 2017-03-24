

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Aug 12 12:59:15 2016
 */
/* Compiler settings for Tree.idl:
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

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __Tree_h__
#define __Tree_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ITreeFeature_FWD_DEFINED__
#define __ITreeFeature_FWD_DEFINED__
typedef interface ITreeFeature ITreeFeature;

#endif 	/* __ITreeFeature_FWD_DEFINED__ */


#ifndef __TreeFeature_FWD_DEFINED__
#define __TreeFeature_FWD_DEFINED__

#ifdef __cplusplus
typedef class TreeFeature TreeFeature;
#else
typedef struct TreeFeature TreeFeature;
#endif /* __cplusplus */

#endif 	/* __TreeFeature_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ITreeFeature_INTERFACE_DEFINED__
#define __ITreeFeature_INTERFACE_DEFINED__

/* interface ITreeFeature */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ITreeFeature;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("D52B8F3D-1B1F-11D6-8A9D-00104BB6FCCB")
    ITreeFeature : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Age( 
            /* [retval][out] */ long *pVal) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ITreeFeatureVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ITreeFeature * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ITreeFeature * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ITreeFeature * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Age )( 
            ITreeFeature * This,
            /* [retval][out] */ long *pVal);
        
        END_INTERFACE
    } ITreeFeatureVtbl;

    interface ITreeFeature
    {
        CONST_VTBL struct ITreeFeatureVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ITreeFeature_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ITreeFeature_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ITreeFeature_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ITreeFeature_get_Age(This,pVal)	\
    ( (This)->lpVtbl -> get_Age(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ITreeFeature_INTERFACE_DEFINED__ */



#ifndef __TREELib_LIBRARY_DEFINED__
#define __TREELib_LIBRARY_DEFINED__

/* library TREELib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_TREELib;

EXTERN_C const CLSID CLSID_TreeFeature;

#ifdef __cplusplus

class DECLSPEC_UUID("D52B8F3E-1B1F-11D6-8A9D-00104BB6FCCB")
TreeFeature;
#endif
#endif /* __TREELib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


