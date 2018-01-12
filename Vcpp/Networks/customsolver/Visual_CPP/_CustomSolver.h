

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Wed Nov 15 13:03:35 2017
 */
/* Compiler settings for _CustomSolver.idl:
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

#ifndef ___CustomSolver_h__
#define ___CustomSolver_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IConnectivitySolver_FWD_DEFINED__
#define __IConnectivitySolver_FWD_DEFINED__
typedef interface IConnectivitySolver IConnectivitySolver;

#endif 	/* __IConnectivitySolver_FWD_DEFINED__ */


#ifndef __ConnectivitySolver_FWD_DEFINED__
#define __ConnectivitySolver_FWD_DEFINED__

#ifdef __cplusplus
typedef class ConnectivitySolver ConnectivitySolver;
#else
typedef struct ConnectivitySolver ConnectivitySolver;
#endif /* __cplusplus */

#endif 	/* __ConnectivitySolver_FWD_DEFINED__ */


#ifndef __ConnectivitySymbolizer_FWD_DEFINED__
#define __ConnectivitySymbolizer_FWD_DEFINED__

#ifdef __cplusplus
typedef class ConnectivitySymbolizer ConnectivitySymbolizer;
#else
typedef struct ConnectivitySymbolizer ConnectivitySymbolizer;
#endif /* __cplusplus */

#endif 	/* __ConnectivitySymbolizer_FWD_DEFINED__ */


#ifndef __ConnSolverPropPage_FWD_DEFINED__
#define __ConnSolverPropPage_FWD_DEFINED__

#ifdef __cplusplus
typedef class ConnSolverPropPage ConnSolverPropPage;
#else
typedef struct ConnSolverPropPage ConnSolverPropPage;
#endif /* __cplusplus */

#endif 	/* __ConnSolverPropPage_FWD_DEFINED__ */


/* header files for imported files */
#include "prsht.h"
#include "mshtml.h"
#include "mshtmhst.h"
#include "exdisp.h"
#include "objsafe.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf__CustomSolver_0000_0000 */
/* [local] */ 

/* [helpstring][uuid] */ 
enum  DECLSPEC_UUID("461F2460-19D4-4289-8663-3A52C439F82B") outputConnectivityType
    {
        outputConnectedLines	= 0,
        outputDisconnectedLines	= 1
    } ;


extern RPC_IF_HANDLE __MIDL_itf__CustomSolver_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf__CustomSolver_0000_0000_v0_0_s_ifspec;

#ifndef __IConnectivitySolver_INTERFACE_DEFINED__
#define __IConnectivitySolver_INTERFACE_DEFINED__

/* interface IConnectivitySolver */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IConnectivitySolver;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("C9D1BFF5-B12B-48C4-A631-3FE0CEFEC631")
    IConnectivitySolver : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_OutputLines( 
            /* [retval][out] */ enum /* external definition not present */ esriNAOutputLineType *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_OutputLines( 
            /* [in] */ enum /* external definition not present */ esriNAOutputLineType newVal) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_OutputConnectivity( 
            /* [retval][out] */ enum outputConnectivityType *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_OutputConnectivity( 
            /* [in] */ enum outputConnectivityType newVal) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IConnectivitySolverVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IConnectivitySolver * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IConnectivitySolver * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IConnectivitySolver * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_OutputLines )( 
            IConnectivitySolver * This,
            /* [retval][out] */ enum /* external definition not present */ esriNAOutputLineType *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_OutputLines )( 
            IConnectivitySolver * This,
            /* [in] */ enum /* external definition not present */ esriNAOutputLineType newVal);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_OutputConnectivity )( 
            IConnectivitySolver * This,
            /* [retval][out] */ enum outputConnectivityType *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_OutputConnectivity )( 
            IConnectivitySolver * This,
            /* [in] */ enum outputConnectivityType newVal);
        
        END_INTERFACE
    } IConnectivitySolverVtbl;

    interface IConnectivitySolver
    {
        CONST_VTBL struct IConnectivitySolverVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IConnectivitySolver_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IConnectivitySolver_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IConnectivitySolver_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IConnectivitySolver_get_OutputLines(This,pVal)	\
    ( (This)->lpVtbl -> get_OutputLines(This,pVal) ) 

#define IConnectivitySolver_put_OutputLines(This,newVal)	\
    ( (This)->lpVtbl -> put_OutputLines(This,newVal) ) 

#define IConnectivitySolver_get_OutputConnectivity(This,pVal)	\
    ( (This)->lpVtbl -> get_OutputConnectivity(This,pVal) ) 

#define IConnectivitySolver_put_OutputConnectivity(This,newVal)	\
    ( (This)->lpVtbl -> put_OutputConnectivity(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IConnectivitySolver_INTERFACE_DEFINED__ */



#ifndef __CustomSolver_LIBRARY_DEFINED__
#define __CustomSolver_LIBRARY_DEFINED__

/* library CustomSolver */
/* [helpstring][uuid][version] */ 


EXTERN_C const IID LIBID_CustomSolver;

EXTERN_C const CLSID CLSID_ConnectivitySolver;

#ifdef __cplusplus

class DECLSPEC_UUID("D60D514D-EAB0-49D7-B5EB-C2533E91837C")
ConnectivitySolver;
#endif

EXTERN_C const CLSID CLSID_ConnectivitySymbolizer;

#ifdef __cplusplus

class DECLSPEC_UUID("4D82C01E-0DDE-40D6-9FA3-BABE65383C9C")
ConnectivitySymbolizer;
#endif

EXTERN_C const CLSID CLSID_ConnSolverPropPage;

#ifdef __cplusplus

class DECLSPEC_UUID("241CF566-AFEF-40D3-8ADC-3914AF4CED16")
ConnSolverPropPage;
#endif
#endif /* __CustomSolver_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


