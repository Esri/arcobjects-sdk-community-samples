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
// Copyright 2011 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 


#pragma once

#include "resource.h"                                           // main symbols
#include "\Program Files (x86)\ArcGIS\DeveloperKit10.7\Include\CatIDs\ArcCATIDs.h"     // component category IDs

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

// ConnectivitySymbolizer
[
  coclass,
  default(INASymbolizer2),
  threading(apartment),
  vi_progid("CustomSolver.ConnectivitySymbolizer"),
  progid("CustomSolver.ConnectivitySymbolizer.1"),
  version(1.0),
  uuid("4D82C01E-0DDE-40D6-9FA3-BABE65383C9C"),
  helpstring("ConnectivitySymbolizer Class")
]
class ATL_NO_VTABLE ConnectivitySymbolizer :
  public INASymbolizer2
{
public:
  ConnectivitySymbolizer() :
    c_randomColorHSVSaturation(100),
    c_baseRandomColorHSVValue(75),
    c_maxAboveBaseRandomColorHSVValue(25),
    c_maxFadedColorHSVSaturation(20)
  {
  }

  DECLARE_PROTECT_FINAL_CONSTRUCT()

  // Register the symbolizer in the ArcGIS Network Analyst extension symbolizers component category so that it can be dynamically applied
  // to a Connectivity Solver where appropriate.  For example, on the Reset Symbology" context menu item of the NALayer.
  BEGIN_CATEGORY_MAP(ConnectivitySymbolizer)
    IMPLEMENTED_CATEGORY(__uuidof(CATID_NetworkAnalystSymbolizer))
  END_CATEGORY_MAP()

  HRESULT FinalConstruct()
  {
    return S_OK;
  }

  void FinalRelease() 
  {
  }

public:

  // INASymbolizer

  STDMETHOD(Applies)(INAContext* pNAContext, VARIANT_BOOL* pFlag);
  STDMETHOD(get_Priority)(long* pPriority);
  STDMETHOD(CreateLayer)(INAContext* pNAContext, INALayer** ppNALayer);
  STDMETHOD(UpdateLayer)(INALayer* pNALayer, VARIANT_BOOL* pUpdated);
  
  // INASymbolizer2 methods

  STDMETHOD(ResetRenderers)(IColor* pSolverColor, INALayer* pNALayer);

private:

  HRESULT CreateRandomColor(IColor** ppColor);
  HRESULT CreateSeedPointRenderer(IColor* pPointColor, IFeatureRenderer** ppFRenderer);
  HRESULT CreateBarrierRenderer(IColor* pBarrierColor, IFeatureRenderer** ppFRenderer);
  HRESULT CreateLineRenderer(IColor* pLineColor, IFeatureRenderer** ppFeatureRenderer);
  HRESULT CreateUnlocatedSymbol(ISymbol* pLocatedMarkerSymbol, ISymbol** ppUnlocatedMarkerSymbol);
  HRESULT CreateCharacterMarkerSymbol(CString   fontName,
                                      IColor*   pMarkerColor,
                                      IColor*   pMarkerBackgroundColor,
                                      long      characterIndex,
                                      long      backgoundCharacterIndex,
                                      double    markerSize,
                                      double    makerBackgroundSize,
                                      double    markerAngle,
                                      ISymbol** ppMarkerSymbol);

  const long c_randomColorHSVSaturation;
  const long c_baseRandomColorHSVValue;
  const long c_maxAboveBaseRandomColorHSVValue;
  const long c_maxFadedColorHSVSaturation;
};


