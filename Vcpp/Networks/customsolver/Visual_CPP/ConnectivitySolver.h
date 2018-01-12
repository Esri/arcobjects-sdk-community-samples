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
#include "\Program Files (x86)\ArcGIS\DeveloperKit10.6\Include\CatIDs\ArcCATIDs.h"     // component category IDs
#include "DataStructures.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

// outputConnectivityType enum
[
  uuid("461F2460-19D4-4289-8663-3A52C439F82B"),
  helpstring("Options for connectivity output"),
  export
]
enum outputConnectivityType
{
  outputConnectedLines = 0,
  outputDisconnectedLines = 1
};

// IConnectivitySolver
[
  object,
  uuid("C9D1BFF5-B12B-48C4-A631-3FE0CEFEC631"),
  oleautomation,	helpstring("IConnectivitySolver Interface"),
  pointer_default(unique)
]
__interface IConnectivitySolver : IUnknown
{
  [propget, helpstring("Indicates the line output type")] HRESULT OutputLines([out, retval] enum esriNAOutputLineType * pVal);
  [propput, helpstring("Indicates the line output type")] HRESULT OutputLines([in] enum esriNAOutputLineType newVal);
  [propget, helpstring("Indicates the connectivity output type")] HRESULT OutputConnectivity([out, retval] enum outputConnectivityType* pVal);
  [propput, helpstring("Indicates the connectivity output type")] HRESULT OutputConnectivity([in] enum outputConnectivityType newVal);
};


// ConnectivitySolver
[
  coclass,
  default(IConnectivitySolver),
  threading(apartment),
  vi_progid("CustomSolver.ConnectivitySolver"),
  progid("CustomSolver.ConnectivitySolver.1"),
  version(1.0),
  uuid(D60D514D-EAB0-49D7-B5EB-C2533E91837C),
  helpstring("ConnectivitySolver Class")
]
class ATL_NO_VTABLE ConnectivitySolver :
  public IConnectivitySolver,
  public INASolver,
  public INASolverSettings,
  public IPersistStream
{
public:
  ConnectivitySolver() :
    m_outputConnectivityType(outputDisconnectedLines),
    m_outputLineType(esriNAOutputLineTrueShape),
    m_bPersistDirty(false),
    c_version(1),
    c_featureRetrievalInterval(500)
  {
  }

  DECLARE_PROTECT_FINAL_CONSTRUCT()

  // Register the solver in the ArcGIS Network Analyst extension solvers component category so that it can be dynamically discovered as an available solver.
  // For example, this will allow the creation of a new Connectivity Solver analysis layer from the menu dropdown on the Network Analyst toolbar.
  BEGIN_CATEGORY_MAP(ConnectivitySolver)
    IMPLEMENTED_CATEGORY(__uuidof(CATID_NetworkAnalystSolver))
  END_CATEGORY_MAP()

  HRESULT FinalConstruct()
  {
    return S_OK;
  }

  void FinalRelease() 
  {
  }

public:

  // IConnectivitySolver

  STDMETHOD(get_OutputLines)(esriNAOutputLineType* pVal);
  STDMETHOD(put_OutputLines)(esriNAOutputLineType newVal);
  STDMETHOD(get_OutputConnectivity)(outputConnectivityType* pVal);
  STDMETHOD(put_OutputConnectivity)(outputConnectivityType newVal);

  // INASolver 

  STDMETHOD(get_Name)(BSTR* pName);
  STDMETHOD(get_DisplayName)(BSTR* pName);
  STDMETHOD(get_ClassDefinitions)(INamedSet** ppDefinitions);
  STDMETHOD(get_CanUseHierarchy)(VARIANT_BOOL* pCanUseHierarchy);
  STDMETHOD(get_CanAccumulateAttributes)(VARIANT_BOOL* pCanAccumulateAttrs);
  STDMETHOD(get_Properties)(IPropertySet** ppPropSet);
  STDMETHOD(CreateLayer)(INAContext* pContext, INALayer** pplayer);
  STDMETHOD(UpdateLayer)(INALayer* player, VARIANT_BOOL* pLayerUpdated);
  STDMETHOD(Solve)(INAContext* pNAContext, IGPMessages* pMessages, ITrackCancel* pTrackCancel, VARIANT_BOOL* pIsPartialSolution);
  STDMETHOD(CreateContext)(IDENetworkDataset* pNetwork, BSTR contextName, INAContext** ppNAContext);
  STDMETHOD(UpdateContext)(INAContext* pNAContext, IDENetworkDataset* pNetwork, IGPMessages* pMessages);
  STDMETHOD(Bind)(INAContext* pContext, IDENetworkDataset* pNetwork, IGPMessages* pMessages);

  // INASolverSettings 

  STDMETHOD(get_AccumulateAttributeNames)(IStringArray** ppAttributeNames);
  STDMETHOD(putref_AccumulateAttributeNames)(IStringArray* pAttributeNames);
  STDMETHOD(put_ImpedanceAttributeName)(BSTR attributeName);
  STDMETHOD(get_ImpedanceAttributeName)(BSTR* pAttributeName);
  STDMETHOD(put_IgnoreInvalidLocations)(VARIANT_BOOL ignoreInvalidLocations);
  STDMETHOD(get_IgnoreInvalidLocations)(VARIANT_BOOL* pIgnoreInvalidLocations);
  STDMETHOD(get_RestrictionAttributeNames)(IStringArray** ppAttributeName);
  STDMETHOD(putref_RestrictionAttributeNames)(IStringArray* pAttributeName);
  STDMETHOD(put_RestrictUTurns)(esriNetworkForwardStarBacktrack backtrack);
  STDMETHOD(get_RestrictUTurns)(esriNetworkForwardStarBacktrack* pBacktrack);
  STDMETHOD(put_UseHierarchy)(VARIANT_BOOL useHierarchy);
  STDMETHOD(get_UseHierarchy)(VARIANT_BOOL* pUseHierarchy);
  STDMETHOD(put_HierarchyAttributeName)(BSTR attributeName);
  STDMETHOD(get_HierarchyAttributeName)(BSTR* pAttributeName);
  STDMETHOD(put_HierarchyLevelCount)(long Count);
  STDMETHOD(get_HierarchyLevelCount)(long* pCount);
  STDMETHOD(put_MaxValueForHierarchy)(long level, long value);
  STDMETHOD(get_MaxValueForHierarchy)(long level, long* pValue);
  STDMETHOD(put_NumTransitionToHierarchy)(long toLevel, long value);
  STDMETHOD(get_NumTransitionToHierarchy)(long toLevel, long* pValue);

  // IPersistStream 

  STDMETHOD(IsDirty)();
  STDMETHOD(Load)(IStream* pStm);
  STDMETHOD(Save)(IStream* pstm, BOOL fClearDirty);
  STDMETHOD(GetSizeMax)(_ULARGE_INTEGER* pCbSize);
  STDMETHOD(GetClassID)(CLSID *pClassID);

private:

  HRESULT BuildClassDefinitions(ISpatialReference* pSpatialRef, INamedSet** ppDefinitions, IDENetworkDataset* pDENDS);
  HRESULT CreateSideOfEdgeDomain(IDomain** ppDomain);
  HRESULT CreateCurbApproachDomain(IDomain** ppDomain);
  HRESULT CreateStatusCodedValueDomain(ICodedValueDomain* pCodedValueDomain);
  HRESULT AddLocationFields(IFieldsEdit* pFieldsEdit, IDENetworkDataset* pDENDS);
  HRESULT AddLocationFieldTypes(IFields* pFields, INAClassDefinitionEdit* pClassDef);
  HRESULT GetNAClassTable(INAContext* pContext, BSTR className, ITable** ppTable);
  HRESULT LoadBarriers(ITable* pTable, INetworkQuery* pNetworkQuery, INetworkForwardStarEx* pNetworkForwardStarEx);
  HRESULT OutputLineFeatures(long sourceID, long featureCount, segment_hash& segmentRecordHashTable, IFeatureClass* pSourceFC, IFeatureCursor* pInsertFeatureCursor, IFeatureBuffer* pFeatureBuffer);

  outputConnectivityType  m_outputConnectivityType;
  esriNAOutputLineType    m_outputLineType;
  bool                    m_bPersistDirty;

  const long              c_version;
  const long              c_featureRetrievalInterval;
};

// Smart Pointer for IConnectivitySolver (for use within this project)
_COM_SMARTPTR_TYPEDEF(IConnectivitySolver, __uuidof(IConnectivitySolver));

// Simple helper class for managing the cancel tracker object during Solve
class CancelTrackerHelper
{
public:
  ~CancelTrackerHelper()
  {
    if (m_ipTrackCancel && m_ipProgressor)
    {
      m_ipTrackCancel->put_Progressor(m_ipProgressor);
      m_ipProgressor->Hide();
    }
  }

  void ManageTrackCancel(ITrackCancel* pTrackCancel)
  {
    m_ipTrackCancel = pTrackCancel;
    if (m_ipTrackCancel)
    {
      m_ipTrackCancel->get_Progressor(&m_ipProgressor);
      m_ipTrackCancel->put_Progressor(0);
    }
  }

private:
  ITrackCancelPtr m_ipTrackCancel;
  IProgressorPtr  m_ipProgressor;
};

