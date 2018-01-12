

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Wed Nov 15 13:02:42 2017
 */
/* Compiler settings for LogoMarkerSymbolVC.idl:
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


#ifndef __LogoMarkerSymbolVC_h__
#define __LogoMarkerSymbolVC_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ILogoMarkerSymbol_FWD_DEFINED__
#define __ILogoMarkerSymbol_FWD_DEFINED__
typedef interface ILogoMarkerSymbol ILogoMarkerSymbol;

#endif 	/* __ILogoMarkerSymbol_FWD_DEFINED__ */


#ifndef __LogoMarkerSymbol_FWD_DEFINED__
#define __LogoMarkerSymbol_FWD_DEFINED__

#ifdef __cplusplus
typedef class LogoMarkerSymbol LogoMarkerSymbol;
#else
typedef struct LogoMarkerSymbol LogoMarkerSymbol;
#endif /* __cplusplus */

#endif 	/* __LogoMarkerSymbol_FWD_DEFINED__ */


#ifndef __LogoMarkerPropertyPage_FWD_DEFINED__
#define __LogoMarkerPropertyPage_FWD_DEFINED__

#ifdef __cplusplus
typedef class LogoMarkerPropertyPage LogoMarkerPropertyPage;
#else
typedef struct LogoMarkerPropertyPage LogoMarkerPropertyPage;
#endif /* __cplusplus */

#endif 	/* __LogoMarkerPropertyPage_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __LOGOMARKERSYMBOLVCLib_LIBRARY_DEFINED__
#define __LOGOMARKERSYMBOLVCLib_LIBRARY_DEFINED__

/* library LOGOMARKERSYMBOLVCLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_LOGOMARKERSYMBOLVCLib;

#ifndef __ILogoMarkerSymbol_INTERFACE_DEFINED__
#define __ILogoMarkerSymbol_INTERFACE_DEFINED__

/* interface ILogoMarkerSymbol */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ILogoMarkerSymbol;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("D7F5BF34-852A-11D5-A161-00508BA08E68")
    ILogoMarkerSymbol : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ColorBorder( 
            /* [retval][out] */ /* external definition not present */ IColor **ppColor) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ColorBorder( 
            /* [in] */ /* external definition not present */ IColor *pColor) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ColorLeft( 
            /* [retval][out] */ /* external definition not present */ IColor **ppColor) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ColorLeft( 
            /* [in] */ /* external definition not present */ IColor *pColor) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ColorRight( 
            /* [retval][out] */ /* external definition not present */ IColor **ppColor) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ColorRight( 
            /* [in] */ /* external definition not present */ IColor *pColor) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ColorTop( 
            /* [retval][out] */ /* external definition not present */ IColor **ppColor) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ColorTop( 
            /* [in] */ /* external definition not present */ IColor *pColor) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ILogoMarkerSymbolVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ILogoMarkerSymbol * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ILogoMarkerSymbol * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ILogoMarkerSymbol * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ColorBorder )( 
            ILogoMarkerSymbol * This,
            /* [retval][out] */ /* external definition not present */ IColor **ppColor);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ColorBorder )( 
            ILogoMarkerSymbol * This,
            /* [in] */ /* external definition not present */ IColor *pColor);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ColorLeft )( 
            ILogoMarkerSymbol * This,
            /* [retval][out] */ /* external definition not present */ IColor **ppColor);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ColorLeft )( 
            ILogoMarkerSymbol * This,
            /* [in] */ /* external definition not present */ IColor *pColor);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ColorRight )( 
            ILogoMarkerSymbol * This,
            /* [retval][out] */ /* external definition not present */ IColor **ppColor);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ColorRight )( 
            ILogoMarkerSymbol * This,
            /* [in] */ /* external definition not present */ IColor *pColor);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ColorTop )( 
            ILogoMarkerSymbol * This,
            /* [retval][out] */ /* external definition not present */ IColor **ppColor);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ColorTop )( 
            ILogoMarkerSymbol * This,
            /* [in] */ /* external definition not present */ IColor *pColor);
        
        END_INTERFACE
    } ILogoMarkerSymbolVtbl;

    interface ILogoMarkerSymbol
    {
        CONST_VTBL struct ILogoMarkerSymbolVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ILogoMarkerSymbol_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ILogoMarkerSymbol_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ILogoMarkerSymbol_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ILogoMarkerSymbol_get_ColorBorder(This,ppColor)	\
    ( (This)->lpVtbl -> get_ColorBorder(This,ppColor) ) 

#define ILogoMarkerSymbol_put_ColorBorder(This,pColor)	\
    ( (This)->lpVtbl -> put_ColorBorder(This,pColor) ) 

#define ILogoMarkerSymbol_get_ColorLeft(This,ppColor)	\
    ( (This)->lpVtbl -> get_ColorLeft(This,ppColor) ) 

#define ILogoMarkerSymbol_put_ColorLeft(This,pColor)	\
    ( (This)->lpVtbl -> put_ColorLeft(This,pColor) ) 

#define ILogoMarkerSymbol_get_ColorRight(This,ppColor)	\
    ( (This)->lpVtbl -> get_ColorRight(This,ppColor) ) 

#define ILogoMarkerSymbol_put_ColorRight(This,pColor)	\
    ( (This)->lpVtbl -> put_ColorRight(This,pColor) ) 

#define ILogoMarkerSymbol_get_ColorTop(This,ppColor)	\
    ( (This)->lpVtbl -> get_ColorTop(This,ppColor) ) 

#define ILogoMarkerSymbol_put_ColorTop(This,pColor)	\
    ( (This)->lpVtbl -> put_ColorTop(This,pColor) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ILogoMarkerSymbol_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_LogoMarkerSymbol;

#ifdef __cplusplus

class DECLSPEC_UUID("D7F5BF35-852A-11D5-A161-00508BA08E68")
LogoMarkerSymbol;
#endif

EXTERN_C const CLSID CLSID_LogoMarkerPropertyPage;

#ifdef __cplusplus

class DECLSPEC_UUID("520120CD-8B8F-11D5-A162-00508BA08E68")
LogoMarkerPropertyPage;
#endif
#endif /* __LOGOMARKERSYMBOLVCLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


