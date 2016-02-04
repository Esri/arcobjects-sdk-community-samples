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



#include "stdafx.h"
#include "NameConstants.h"
#include "ConnectivitySolver.h"
#include "ConnectivitySymbolizer.h"



// ConnectivitySymbolizer

/////////////////////////////////////////////////////////////////////
// INASymbolizer

STDMETHODIMP ConnectivitySymbolizer::Applies(INAContext* pNAContext, VARIANT_BOOL* pFlag)
{
  if (!pFlag)
    return E_POINTER;

  // This symbolizer is only intended to be used with the sample connectivity solver.
  // Get the solver from the context and QI for IConnectivitySolver to see if it
  // applies.

  *pFlag = VARIANT_FALSE;

  if (!pNAContext)
    return S_OK;

  INASolverPtr ipNASolver;
  pNAContext->get_Solver(&ipNASolver);

  IConnectivitySolverPtr ipConnectivitySolver(ipNASolver);

  if (ipConnectivitySolver)
    *pFlag = VARIANT_TRUE;

  return S_OK;
}

STDMETHODIMP ConnectivitySymbolizer::get_Priority(long* pPriority)
{
  if (!pPriority)
    return E_POINTER;

  // In the event that there are several symbolizers that Applies() to a solver,
  // priority is used to decide which one to use.  Higher number => higher priority.
  // This symbolizer only applies to this solver and is the only one, so we could
  // return anything here.  ArcGIS Network Analyst extension solver symbolizers typically
  // return 100 by default.

  *pPriority = 100;

  return S_OK;
}

STDMETHODIMP ConnectivitySymbolizer::CreateLayer(INAContext* pNAContext, INALayer** ppNALayer)
{
  if (!pNAContext || !ppNALayer)
    return E_POINTER;

  *ppNALayer = 0;

  // Create the main analysis layer
  HRESULT             hr = S_OK;
  INamedSetPtr        ipNAClasses;
  CComBSTR            layerName;
  INALayerPtr         ipNALayer(CLSID_NALayer);
  IUnknownPtr         ipUnknown;
  CString             szSubLayerName;
  IFeatureRendererPtr ipFeatureRenderer;
  IFeatureLayerPtr    ipSubFeatureLayer;

  pNAContext->get_NAClasses(&ipNAClasses);

  // Give the new layer a context and a name
  ipNALayer->putref_Context(pNAContext);
  pNAContext->get_Name(&layerName);
  ((ILayerPtr)ipNALayer)->put_Name(layerName);

  // Build solver colors out of a single random base color.
  IColorPtr ipSolverColor;
  CreateRandomColor(&ipSolverColor);

  // Create a standard selection color
  IColorPtr ipSelectionColor(CLSID_RgbColor);
  ipSelectionColor->put_RGB(RGB(0,255,255));

  /////////////////////////////////////////////////////////////////////////////////////////
  // Seed Points layer
  //

  // Get the Seed Points NAClass/FeatureClass
  if (FAILED(hr = ipNAClasses->get_ItemByName(CComBSTR(CS_SEED_POINTS_NAME), &ipUnknown)))
    return hr;

  IFeatureClassPtr ipSeedsFC(ipUnknown);
  if (!ipSeedsFC)
    return E_UNEXPECTED;

  // Create a new feature layer for the Seed Points feature class
  IFeatureLayerPtr ipSeedsFeatureLayer(CLSID_FeatureLayer);
  ipSeedsFeatureLayer->putref_FeatureClass(ipSeedsFC);
  ipSeedsFeatureLayer->put_Name(CComBSTR(CS_SEED_POINTS_LAYER_NAME));

  // Give the Seed Points layer a renderer and a unique value property page
  CreateSeedPointRenderer(ipSolverColor, &ipFeatureRenderer);

  IGeoFeatureLayerPtr ipGeoFeatureLayer(ipSeedsFeatureLayer);
  if (!ipGeoFeatureLayer)
    return S_OK;

  if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
    return hr;

  IUIDPtr ipUniqueValuePropertyPageUID(CLSID_UID);

  if (FAILED(ipUniqueValuePropertyPageUID->put_Value(CComVariant(L"esriCartoUI.UniqueValuePropertyPage"))))
  {  
    // Renderer Property Pages are not installed with Engine. In this
    // case getting the property page by PROGID is an expected failure.
    ipUniqueValuePropertyPageUID = 0;
  }

  if (ipUniqueValuePropertyPageUID)
  {
    if (FAILED(hr = ipGeoFeatureLayer->put_RendererPropertyPageClassID(ipUniqueValuePropertyPageUID)))
      return hr;
  }

  ((IFeatureSelectionPtr)ipSeedsFeatureLayer)->putref_SelectionColor(ipSelectionColor);

  // Add the new seed points layer as a sub-layer in the new NALayer
  ipNALayer->Add(ipSeedsFeatureLayer);

  ///////////////////////////////////////////////////////////////////////////////////////////
  // Barriers layer
  //

  // Get the Barriers NAClass/FeatureClass
  if (FAILED(hr = ipNAClasses->get_ItemByName(CComBSTR(CS_BARRIERS_NAME), &ipUnknown)))
    return hr;

  IFeatureClassPtr ipBarriersFC(ipUnknown);
  if (!ipBarriersFC)
    return E_UNEXPECTED;

  // Create a new feature layer for the Barriers feature class
  IFeatureLayerPtr ipBarrierFeatureLayer(CLSID_FeatureLayer);
  ipBarrierFeatureLayer->putref_FeatureClass(ipBarriersFC);
  ipBarrierFeatureLayer->put_Name(CComBSTR(CS_BARRIERS_LAYER_NAME));

  // Give the Barriers layer a unique value renderer and a unique value property page
  CreateBarrierRenderer(ipSolverColor, &ipFeatureRenderer);

  ipGeoFeatureLayer = ipBarrierFeatureLayer;
  if (!ipGeoFeatureLayer)
    return S_OK;

  if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
    return hr;

  if (ipUniqueValuePropertyPageUID)
  {
    if (FAILED(hr = ipGeoFeatureLayer->put_RendererPropertyPageClassID(ipUniqueValuePropertyPageUID)))
      return hr;
  }

  ((IFeatureSelectionPtr)ipBarrierFeatureLayer)->putref_SelectionColor(ipSelectionColor);

  // Add the new barriers layer as a sub-layer in the new NALayer
  ipNALayer->Add(ipBarrierFeatureLayer);

  /////////////////////////////////////////////////////////////////////////////////////////////
  // Lines layer
  //

  // Get the Lines NAClass/FeatureClass
  if (FAILED(hr = ipNAClasses->get_ItemByName(CComBSTR(CS_LINES_NAME), &ipUnknown)))
    return hr;

  IFeatureClassPtr ipLinesFC(ipUnknown);
  if (!ipLinesFC)
    return E_UNEXPECTED;

  // Create a new feature layer for the Lines feature class
  IFeatureLayerPtr ipLinesFeatureLayer(CLSID_FeatureLayer);
  ipLinesFeatureLayer->putref_FeatureClass(ipLinesFC);
  ipLinesFeatureLayer->put_Name(CComBSTR(CS_LINES_LAYER_NAME));

  // Give the Lines layer a simple renderer and a single symbol property page
  CreateLineRenderer(ipSolverColor, &ipFeatureRenderer);

  ipGeoFeatureLayer = ipLinesFeatureLayer;
  if (!ipGeoFeatureLayer)
    return S_OK;

  if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
    return hr;

  IUIDPtr ipSingleSymbolPropertyPageUID(CLSID_UID);

  if (FAILED(ipSingleSymbolPropertyPageUID->put_Value(CComVariant(L"esriCartoUI.SingleSymbolPropertyPage"))))
  {
    // Renderer Property Pages are not installed with Engine. In this
    // case getting the property page by PROGID is an expected failure.
    ipSingleSymbolPropertyPageUID = 0; 
  }

  if (ipSingleSymbolPropertyPageUID)
  {
    if (FAILED(hr = ipGeoFeatureLayer->put_RendererPropertyPageClassID(ipSingleSymbolPropertyPageUID)))
      return hr;
  }

  ((IFeatureSelectionPtr)ipLinesFeatureLayer)->putref_SelectionColor(ipSelectionColor);

  // Add the new lines layer as a sub-layer in the new NALayer
  ipNALayer->Add(ipLinesFeatureLayer);

  // Return the newly created NALayer
  (*ppNALayer) = ipNALayer;

  if (*ppNALayer)
    (*ppNALayer)->AddRef();

  return S_OK;
}

STDMETHODIMP ConnectivitySymbolizer::UpdateLayer(INALayer* pNALayer, VARIANT_BOOL* pUpdated)
{
  return E_NOTIMPL;
}

STDMETHODIMP ConnectivitySymbolizer::ResetRenderers(IColor *pSolverColor, INALayer *pNALayer)
{
  if (!pNALayer)
    return E_POINTER;

  HRESULT hr;

  // ResetRenderers() provides a method for updating the entire NALayer color scheme
  // based on the color passed in to this method. We must get each feature layer in the 
  // composite NALayer and update its renderer's colors appropriately.

  // Get the NALayer's context and make sure that this symbolizer applies
  INAContextPtr ipContext;
  pNALayer->get_Context(&ipContext);
  if (!ipContext)
    return S_OK;

  VARIANT_BOOL doesApply = VARIANT_FALSE;
  Applies(ipContext, &doesApply);
  if (!doesApply)
    return S_OK;

  // Check to make sure a valid color was passed in; if not, we create a random one
  if (!pSolverColor)
    CreateRandomColor(&pSolverColor);

  IFeatureRendererPtr ipFeatureRenderer;
  ILayerPtr ipSubLayer;
  IGeoFeatureLayerPtr ipGeoFeatureLayer;
  
  ////////////////////////////////////////////////////////////////////////////////////////////////////
  // Seed Points
  //
  pNALayer->get_LayerByNAClassName(CComBSTR(CS_SEED_POINTS_NAME), &ipSubLayer);
  if (ipSubLayer)
  {
    ipGeoFeatureLayer = ipSubLayer;
    if (FAILED(hr = CreateSeedPointRenderer(pSolverColor, &ipFeatureRenderer)))
      return hr;
    if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
      return hr;
  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////
  // Barriers
  //
  pNALayer->get_LayerByNAClassName(CComBSTR(CS_BARRIERS_NAME), &ipSubLayer);
  if (ipSubLayer)
  {
    ipGeoFeatureLayer = ipSubLayer;
    if (FAILED(hr = CreateBarrierRenderer(pSolverColor, &ipFeatureRenderer)))
      return hr;
    if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
      return hr;
  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////
  // Lines
  //
  pNALayer->get_LayerByNAClassName(CComBSTR(CS_LINES_NAME), &ipSubLayer);
  if (ipSubLayer)
  {
    ipGeoFeatureLayer = ipSubLayer;
    if (FAILED(hr = CreateLineRenderer(pSolverColor, &ipFeatureRenderer)))
      return hr;
    if (FAILED(hr = ipGeoFeatureLayer->putref_Renderer(ipFeatureRenderer)))
      return hr;
  }

  return S_OK;  
}

/////////////////////////////////////////////////////////////////////
// private methods

HRESULT ConnectivitySymbolizer::CreateRandomColor(IColor** ppColor)
{
  if (!ppColor)
    return E_POINTER;

  *ppColor = 0;

  IHsvColorPtr ipHsvColor(CLSID_HsvColor);

  ipHsvColor->put_Hue(::GetTickCount() % 360L);
  ipHsvColor->put_Saturation(c_randomColorHSVSaturation);
  ipHsvColor->put_Value(c_baseRandomColorHSVValue + (::GetTickCount() % c_maxAboveBaseRandomColorHSVValue));

  *ppColor = static_cast<IColor*>(ipHsvColor);

  if (*ppColor)
    (*ppColor)->AddRef();

  return S_OK;
}

HRESULT ConnectivitySymbolizer::CreateBarrierRenderer(IColor* pBarrierColor, IFeatureRenderer** ppFRenderer)
{
  if (!ppFRenderer || !pBarrierColor)
    return E_POINTER;

  IUniqueValueRendererPtr ipRenderer(CLSID_UniqueValueRenderer);

  // Three symbols: Located, Unlocated and Error (default) are created for the renderer
  HRESULT     hr;
  IColorPtr   ipBackgroundColor(CLSID_RgbColor),
              ipUnlocatedColor(CLSID_RgbColor),
              ipErrorColor(CLSID_RgbColor);
  ISymbolPtr  ipBarrierSymbol,
              ipUnlocatedBarrierSymbol,
              ipErrorSymbol;
  COLORREF    colorRef;

  ipErrorColor->put_RGB(RGB(255,0,0));
  ipBackgroundColor->put_RGB(RGB(255,255,255));
  pBarrierColor->get_RGB(&colorRef);

  IHsvColorPtr ipHsvColor(CLSID_HsvColor);
  ipHsvColor->put_RGB(colorRef);
  long s;
  ipHsvColor->get_Saturation(&s);
  if (s > c_maxFadedColorHSVSaturation)
  {    
    ipHsvColor->put_Saturation(c_maxFadedColorHSVSaturation);
    ipHsvColor->get_RGB(&colorRef);
  }
  ipUnlocatedColor->put_RGB(colorRef); 

  if (FAILED(hr = CreateCharacterMarkerSymbol(CString(_T("ESRI Default Marker")), pBarrierColor, ipBackgroundColor, 67, 33, 18, 10, 45.0, &ipBarrierSymbol)))
    return hr;
  if (FAILED(hr = CreateUnlocatedSymbol(ipBarrierSymbol, &ipUnlocatedBarrierSymbol)))
    return hr;
  if (FAILED(hr = CreateCharacterMarkerSymbol(CString(_T("ESRI Default Marker")), ipErrorColor, ipBackgroundColor, 67, 33, 18, 10, 45.0, &ipErrorSymbol)))
    return hr;

  ipRenderer->put_FieldCount(1);
  ipRenderer->put_Field(0, CComBSTR(CS_FIELD_STATUS));

  ipRenderer->put_DefaultSymbol(ipErrorSymbol);
  ipRenderer->put_DefaultLabel(CComBSTR(L"Error"));
  ipRenderer->put_UseDefaultSymbol(VARIANT_TRUE);

  ipRenderer->AddValue(CComBSTR(L"0"), CComBSTR(L""), ipBarrierSymbol);
  ipRenderer->put_Label(CComBSTR(L"0"), CComBSTR(L"Located"));

  ipRenderer->AddValue(CComBSTR(L"1"), CComBSTR(L""), ipUnlocatedBarrierSymbol);
  ipRenderer->put_Label(CComBSTR(L"1"), CComBSTR(L"Unlocated"));

  *ppFRenderer = (IFeatureRendererPtr)ipRenderer;
  if (*ppFRenderer)
    (*ppFRenderer)->AddRef();

  return S_OK;
}

HRESULT ConnectivitySymbolizer::CreateCharacterMarkerSymbol(CString   fontName,
                                                            IColor*   pMarkerColor,
                                                            IColor*   pMarkerBackgroundColor,
                                                            long      characterIndex, 
                                                            long      backgoundCharacterIndex, 
                                                            double    markerSize,
                                                            double    makerBackgroundSize,
                                                            double    markerAngle,
                                                            ISymbol** ppMarkerSymbol)
{
  if (!ppMarkerSymbol || !pMarkerColor || !pMarkerBackgroundColor)
    return E_POINTER;

  IMultiLayerMarkerSymbolPtr ipMultiLayerSymbol(CLSID_MultiLayerMarkerSymbol);

  *ppMarkerSymbol = (ISymbolPtr)ipMultiLayerSymbol;
  if (*ppMarkerSymbol)
    (*ppMarkerSymbol)->AddRef();

  // Check font is available
  IFontDispPtr ipFontDisp;
  FONTDESC     fontDesc;

  ::memset(&fontDesc, 0, sizeof(FONTDESC));
  fontDesc.cbSizeofstruct = sizeof(FONTDESC);
  fontDesc.lpstrName      = CComBSTR(fontName);
  ::OleCreateFontIndirect(&fontDesc, IID_IFontDisp, (void**)&ipFontDisp);
  if (!ipFontDisp)
    return E_INVALIDARG;

  ICharacterMarkerSymbolPtr ipCharacterMarkerSymbol;

  // Background symbol
  if (makerBackgroundSize)
  {
    ipCharacterMarkerSymbol.CreateInstance(CLSID_CharacterMarkerSymbol);

    ipCharacterMarkerSymbol->put_Font(ipFontDisp);
    ipCharacterMarkerSymbol->put_CharacterIndex(backgoundCharacterIndex);
    ipCharacterMarkerSymbol->put_Color(pMarkerBackgroundColor);
    ipCharacterMarkerSymbol->put_Size(makerBackgroundSize);

    ipMultiLayerSymbol->AddLayer((IMarkerSymbolPtr)ipCharacterMarkerSymbol);
    ((ILayerColorLockPtr)ipMultiLayerSymbol)->put_LayerColorLock(0, VARIANT_TRUE);
  }

  // Foreground symbol
  ipCharacterMarkerSymbol.CreateInstance(CLSID_CharacterMarkerSymbol);

  ipCharacterMarkerSymbol->put_Font(ipFontDisp);
  ipCharacterMarkerSymbol->put_CharacterIndex(characterIndex);
  ipCharacterMarkerSymbol->put_Color(pMarkerColor);
  ipCharacterMarkerSymbol->put_Size(markerSize);
  ipCharacterMarkerSymbol->put_Angle(markerAngle);

  ipMultiLayerSymbol->AddLayer((IMarkerSymbolPtr)ipCharacterMarkerSymbol);
  ((ILayerColorLockPtr)ipMultiLayerSymbol)->put_LayerColorLock(0, VARIANT_FALSE);


  return S_OK;
}

HRESULT ConnectivitySymbolizer::CreateUnlocatedSymbol(ISymbol* pLocatedMarkerSymbol, ISymbol** ppUnlocatedMarkerSymbol)
{
  if (!ppUnlocatedMarkerSymbol || !pLocatedMarkerSymbol)
    return E_POINTER;

  if (IMarkerSymbolPtr(pLocatedMarkerSymbol) == NULL)
    return E_INVALIDARG;

  IMultiLayerMarkerSymbolPtr ipMultiLayerSymbol;

  if (IMultiLayerMarkerSymbolPtr(pLocatedMarkerSymbol) == NULL)
  {
    ipMultiLayerSymbol.CreateInstance(CLSID_MultiLayerMarkerSymbol);

    IClonePtr ipCloned;

    ((IClonePtr)pLocatedMarkerSymbol)->Clone(&ipCloned);
    ipMultiLayerSymbol->AddLayer((IMarkerSymbolPtr)ipCloned);
    ((ILayerColorLockPtr)ipMultiLayerSymbol)->put_LayerColorLock(0, VARIANT_FALSE);
  }
  else
  {
    IClonePtr ipCloned;

    ((IClonePtr)pLocatedMarkerSymbol)->Clone(&ipCloned);
    ipMultiLayerSymbol = ipCloned;
  }

  *ppUnlocatedMarkerSymbol = (ISymbolPtr)ipMultiLayerSymbol;
  if (*ppUnlocatedMarkerSymbol)
    (*ppUnlocatedMarkerSymbol)->AddRef();

  // Wash the symbol color
  double    symbolSize;
  IColorPtr ipLocatedColor;
  IColorPtr ipUnlocatedColor(CLSID_RgbColor);
  COLORREF  colorRef;

  ((IMarkerSymbolPtr)ipMultiLayerSymbol)->get_Size(&symbolSize);
  ((IMarkerSymbolPtr)ipMultiLayerSymbol)->get_Color(&ipLocatedColor);
  ipLocatedColor->get_RGB(&colorRef);

  IHsvColorPtr ipHsvColor(CLSID_HsvColor);
  ipHsvColor->put_RGB(colorRef);
  long s;
  ipHsvColor->get_Saturation(&s);
  if (s > c_maxFadedColorHSVSaturation)
  {    
    ipHsvColor->put_Saturation(c_maxFadedColorHSVSaturation);
    ipHsvColor->get_RGB(&colorRef);
  }

  ipUnlocatedColor->put_RGB(colorRef); 

  ((IMarkerSymbolPtr)ipMultiLayerSymbol)->put_Color(ipUnlocatedColor);

  // Add a foreground locked red exclamation mark
  IFontDispPtr ipFontDisp;
  FONTDESC     fontDesc;

  ::memset(&fontDesc, 0, sizeof(FONTDESC));
  fontDesc.cbSizeofstruct = sizeof(FONTDESC);
  fontDesc.lpstrName      = CComBSTR(L"Arial Black");
  ::OleCreateFontIndirect(&fontDesc, IID_IFontDisp, (void**)&ipFontDisp);
  if (!ipFontDisp)
    return E_INVALIDARG;

  IColorPtr ipRedColor(CLSID_RgbColor);

  ipRedColor->put_RGB(RGB(255, 0, 0));

  ICharacterMarkerSymbolPtr ipCharacterMarkerSymbol;
  ipCharacterMarkerSymbol.CreateInstance(CLSID_CharacterMarkerSymbol);

  ipCharacterMarkerSymbol->put_Font(ipFontDisp);
  ipCharacterMarkerSymbol->put_CharacterIndex(63);
  ipCharacterMarkerSymbol->put_Color(ipRedColor);
  ipCharacterMarkerSymbol->put_Size(10);
  ipCharacterMarkerSymbol->put_XOffset( -9);
  ipCharacterMarkerSymbol->put_YOffset(1);

  ipMultiLayerSymbol->AddLayer((IMarkerSymbolPtr)ipCharacterMarkerSymbol);
  ((ILayerColorLockPtr)ipMultiLayerSymbol)->put_LayerColorLock(0, VARIANT_TRUE);

  return S_OK;
}

HRESULT ConnectivitySymbolizer::CreateSeedPointRenderer(IColor* pPointColor, IFeatureRenderer** ppFRenderer)
{
  if (!pPointColor || !ppFRenderer)
    return E_POINTER;

  IUniqueValueRendererPtr ipRenderer(CLSID_UniqueValueRenderer);

  // Three symbols: Located, Unlocated and Error (default) are created for the renderer
  HRESULT     hr;
  IColorPtr   ipErrorColor(CLSID_RgbColor);
  ipErrorColor->put_RGB(RGB(255,0,0));

  ISymbolPtr ipSeedPointSymbol(CLSID_SimpleMarkerSymbol);
  ISimpleMarkerSymbolPtr ipSimpleMarkerSeedPointSymbol(ipSeedPointSymbol);
  ipSimpleMarkerSeedPointSymbol->put_Style(esriSMSDiamond);
  ipSimpleMarkerSeedPointSymbol->put_Color(pPointColor);
  ipSimpleMarkerSeedPointSymbol->put_Size(12);

  ISymbolPtr ipErrorSeedPointSymbol(CLSID_SimpleMarkerSymbol);
  ISimpleMarkerSymbolPtr ipSimpleMarkerErrorSeedPointSymbol(ipErrorSeedPointSymbol);
  ipSimpleMarkerErrorSeedPointSymbol->put_Style(esriSMSDiamond);
  ipSimpleMarkerErrorSeedPointSymbol->put_Color(ipErrorColor);
  ipSimpleMarkerErrorSeedPointSymbol->put_Size(12);

  ISymbolPtr ipUnlocatedSeedPointSymbol;
  if (FAILED(hr = CreateUnlocatedSymbol(ipSeedPointSymbol, &ipUnlocatedSeedPointSymbol)))
    return hr;

  ipRenderer->put_FieldCount(1);
  ipRenderer->put_Field(0, CComBSTR(CS_FIELD_STATUS));

  ipRenderer->put_DefaultSymbol(ipErrorSeedPointSymbol);
  ipRenderer->put_DefaultLabel(CComBSTR(L"Error"));
  ipRenderer->put_UseDefaultSymbol(VARIANT_TRUE);

  ipRenderer->AddValue(CComBSTR(L"0"), CComBSTR(L""), ipSeedPointSymbol);
  ipRenderer->put_Label(CComBSTR(L"0"), CComBSTR(L"Located"));

  ipRenderer->AddValue(CComBSTR(L"1"), CComBSTR(L""), ipUnlocatedSeedPointSymbol);
  ipRenderer->put_Label(CComBSTR(L"1"), CComBSTR(L"Unlocated"));

  *ppFRenderer = (IFeatureRendererPtr)ipRenderer;
  if (*ppFRenderer)
    (*ppFRenderer)->AddRef();

  return S_OK;
}


HRESULT ConnectivitySymbolizer::CreateLineRenderer(IColor* pLineColor, IFeatureRenderer** ppFeatureRenderer)
{
  if (!pLineColor || !ppFeatureRenderer)
    return E_POINTER;

  ISimpleRendererPtr ipRenderer(CLSID_SimpleRenderer);
  ISimpleLineSymbolPtr    ipLineSymbol(CLSID_SimpleLineSymbol);

  ipLineSymbol->put_Style(esriSLSSolid);
  ipLineSymbol->put_Color(pLineColor);
  ipLineSymbol->put_Width(3);

  ipRenderer->putref_Symbol((ISymbolPtr)ipLineSymbol);

  *ppFeatureRenderer = (IFeatureRendererPtr)ipRenderer;
  if (*ppFeatureRenderer)
    (*ppFeatureRenderer)->AddRef();

  return S_OK;
}

