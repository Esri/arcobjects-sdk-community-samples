

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Fri Aug 12 12:59:49 2016
 */
/* Compiler settings for CustomXform.idl:
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


#ifndef __CustomXform_h__
#define __CustomXform_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISimpleXform_FWD_DEFINED__
#define __ISimpleXform_FWD_DEFINED__
typedef interface ISimpleXform ISimpleXform;

#endif 	/* __ISimpleXform_FWD_DEFINED__ */


#ifndef __IRMCXform_FWD_DEFINED__
#define __IRMCXform_FWD_DEFINED__
typedef interface IRMCXform IRMCXform;

#endif 	/* __IRMCXform_FWD_DEFINED__ */


#ifndef __SimpleXform_FWD_DEFINED__
#define __SimpleXform_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimpleXform SimpleXform;
#else
typedef struct SimpleXform SimpleXform;
#endif /* __cplusplus */

#endif 	/* __SimpleXform_FWD_DEFINED__ */


#ifndef __RMCXform_FWD_DEFINED__
#define __RMCXform_FWD_DEFINED__

#ifdef __cplusplus
typedef class RMCXform RMCXform;
#else
typedef struct RMCXform RMCXform;
#endif /* __cplusplus */

#endif 	/* __RMCXform_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __CustomXformLib_LIBRARY_DEFINED__
#define __CustomXformLib_LIBRARY_DEFINED__

/* library CustomXformLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_CustomXformLib;

#ifndef __ISimpleXform_INTERFACE_DEFINED__
#define __ISimpleXform_INTERFACE_DEFINED__

/* interface ISimpleXform */
/* [helpstring][uuid][unique][oleautomation][object] */ 


EXTERN_C const IID IID_ISimpleXform;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7E536337-CB2A-47ed-BA32-071C74728361")
    ISimpleXform : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR *pName) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
            /* [in] */ BSTR name) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISimpleXformVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimpleXform * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimpleXform * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimpleXform * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Name )( 
            ISimpleXform * This,
            /* [retval][out] */ BSTR *pName);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Name )( 
            ISimpleXform * This,
            /* [in] */ BSTR name);
        
        END_INTERFACE
    } ISimpleXformVtbl;

    interface ISimpleXform
    {
        CONST_VTBL struct ISimpleXformVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimpleXform_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimpleXform_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimpleXform_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimpleXform_get_Name(This,pName)	\
    ( (This)->lpVtbl -> get_Name(This,pName) ) 

#define ISimpleXform_put_Name(This,name)	\
    ( (This)->lpVtbl -> put_Name(This,name) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimpleXform_INTERFACE_DEFINED__ */


#ifndef __IRMCXform_INTERFACE_DEFINED__
#define __IRMCXform_INTERFACE_DEFINED__

/* interface IRMCXform */
/* [helpstring][uuid][unique][oleautomation][object] */ 


EXTERN_C const IID IID_IRMCXform;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("144C653A-50FC-4d61-BED9-859EF234ED14")
    IRMCXform : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR *pName) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
            /* [in] */ BSTR name) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Coefficients( 
            /* [retval][out] */ double *coefficients) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Coefficients( 
            /* [in] */ double *coefficients) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IRMCXformVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IRMCXform * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IRMCXform * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IRMCXform * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Name )( 
            IRMCXform * This,
            /* [retval][out] */ BSTR *pName);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Name )( 
            IRMCXform * This,
            /* [in] */ BSTR name);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Coefficients )( 
            IRMCXform * This,
            /* [retval][out] */ double *coefficients);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Coefficients )( 
            IRMCXform * This,
            /* [in] */ double *coefficients);
        
        END_INTERFACE
    } IRMCXformVtbl;

    interface IRMCXform
    {
        CONST_VTBL struct IRMCXformVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IRMCXform_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IRMCXform_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IRMCXform_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IRMCXform_get_Name(This,pName)	\
    ( (This)->lpVtbl -> get_Name(This,pName) ) 

#define IRMCXform_put_Name(This,name)	\
    ( (This)->lpVtbl -> put_Name(This,name) ) 

#define IRMCXform_get_Coefficients(This,coefficients)	\
    ( (This)->lpVtbl -> get_Coefficients(This,coefficients) ) 

#define IRMCXform_put_Coefficients(This,coefficients)	\
    ( (This)->lpVtbl -> put_Coefficients(This,coefficients) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IRMCXform_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_SimpleXform;

#ifdef __cplusplus

class DECLSPEC_UUID("2FA1F0BB-E7DA-4a7a-A11A-56E9D712C3EF")
SimpleXform;
#endif

EXTERN_C const CLSID CLSID_RMCXform;

#ifdef __cplusplus

class DECLSPEC_UUID("22FF5E72-3D6A-4dee-B353-6677A78F378F")
RMCXform;
#endif
#endif /* __CustomXformLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


