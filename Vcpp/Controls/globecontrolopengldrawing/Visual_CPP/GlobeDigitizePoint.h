

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Wed Nov 15 12:46:38 2017
 */
/* Compiler settings for GlobeDigitizePoint.idl:
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


#ifndef __GlobeDigitizePoint_h__
#define __GlobeDigitizePoint_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IGlobeDigitizePointTool_FWD_DEFINED__
#define __IGlobeDigitizePointTool_FWD_DEFINED__
typedef interface IGlobeDigitizePointTool IGlobeDigitizePointTool;

#endif 	/* __IGlobeDigitizePointTool_FWD_DEFINED__ */


#ifndef __IAddGeographicPoint_FWD_DEFINED__
#define __IAddGeographicPoint_FWD_DEFINED__
typedef interface IAddGeographicPoint IAddGeographicPoint;

#endif 	/* __IAddGeographicPoint_FWD_DEFINED__ */


#ifndef __GlobeDigitizePointTool_FWD_DEFINED__
#define __GlobeDigitizePointTool_FWD_DEFINED__

#ifdef __cplusplus
typedef class GlobeDigitizePointTool GlobeDigitizePointTool;
#else
typedef struct GlobeDigitizePointTool GlobeDigitizePointTool;
#endif /* __cplusplus */

#endif 	/* __GlobeDigitizePointTool_FWD_DEFINED__ */


#ifndef __AddGeographicPoint_FWD_DEFINED__
#define __AddGeographicPoint_FWD_DEFINED__

#ifdef __cplusplus
typedef class AddGeographicPoint AddGeographicPoint;
#else
typedef struct AddGeographicPoint AddGeographicPoint;
#endif /* __cplusplus */

#endif 	/* __AddGeographicPoint_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __GlobeDigitizePoint_LIBRARY_DEFINED__
#define __GlobeDigitizePoint_LIBRARY_DEFINED__

/* library GlobeDigitizePoint */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_GlobeDigitizePoint;

#ifndef __IGlobeDigitizePointTool_INTERFACE_DEFINED__
#define __IGlobeDigitizePointTool_INTERFACE_DEFINED__

/* interface IGlobeDigitizePointTool */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IGlobeDigitizePointTool;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B705200F-B199-4DC6-9E54-2488A5A3B938")
    IGlobeDigitizePointTool : public IDispatch
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IGlobeDigitizePointToolVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IGlobeDigitizePointTool * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IGlobeDigitizePointTool * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IGlobeDigitizePointTool * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IGlobeDigitizePointTool * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IGlobeDigitizePointTool * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IGlobeDigitizePointTool * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IGlobeDigitizePointTool * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IGlobeDigitizePointToolVtbl;

    interface IGlobeDigitizePointTool
    {
        CONST_VTBL struct IGlobeDigitizePointToolVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IGlobeDigitizePointTool_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IGlobeDigitizePointTool_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IGlobeDigitizePointTool_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IGlobeDigitizePointTool_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IGlobeDigitizePointTool_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IGlobeDigitizePointTool_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IGlobeDigitizePointTool_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IGlobeDigitizePointTool_INTERFACE_DEFINED__ */


#ifndef __IAddGeographicPoint_INTERFACE_DEFINED__
#define __IAddGeographicPoint_INTERFACE_DEFINED__

/* interface IAddGeographicPoint */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IAddGeographicPoint;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("AFB86606-46D4-4463-BCE8-83963124277E")
    IAddGeographicPoint : public IDispatch
    {
    public:
    };
    
    
#else 	/* C style interface */

    typedef struct IAddGeographicPointVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IAddGeographicPoint * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IAddGeographicPoint * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IAddGeographicPoint * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IAddGeographicPoint * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IAddGeographicPoint * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IAddGeographicPoint * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IAddGeographicPoint * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } IAddGeographicPointVtbl;

    interface IAddGeographicPoint
    {
        CONST_VTBL struct IAddGeographicPointVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAddGeographicPoint_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IAddGeographicPoint_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IAddGeographicPoint_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IAddGeographicPoint_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IAddGeographicPoint_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IAddGeographicPoint_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IAddGeographicPoint_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IAddGeographicPoint_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_GlobeDigitizePointTool;

#ifdef __cplusplus

class DECLSPEC_UUID("B87A598C-A8C0-41BA-AC35-450416C38A35")
GlobeDigitizePointTool;
#endif

EXTERN_C const CLSID CLSID_AddGeographicPoint;

#ifdef __cplusplus

class DECLSPEC_UUID("EA9A6EEA-10A1-4F25-9979-F96EDA7750B5")
AddGeographicPoint;
#endif
#endif /* __GlobeDigitizePoint_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


